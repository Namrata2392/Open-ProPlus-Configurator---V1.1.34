using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>ENMap</b> is a class to store info about EN mapping's for various slaves.
    * \details   This class stores info related to EN mapping's for various slaves. It retrieves/stores various 
    * mapping parameters like DataType, Deadband, etc. depending on the slave type this object belongs to. 
    * It also exports the XML node related to this object.
    * 
    */
    public class ENMap
    {
        enum enmType
        {
           EN
        };
        private bool isNodeComment = false;
        private string comment = "";
        private bool isReindexed = false;
        private enmType enmnType = enmType.EN;
        private int enNum = -1;
        private int reportingIdx = -1;
        private string dType = "";
        private string cType = "";
        private double dBand = 1;
        private double mult = 1;
        private double cnst = 0;
        private string description = "";
        private SlaveTypes slaveType = SlaveTypes.UNKNOWN;
        private string[] arrAttributes;// = { "ENNo", "ReportingIndex", "DataType", "Deadband", "Multiplier", "Constant" };
        private string[] arrDataTypes;
        private string[] arrCommandTypes;

        private void SetSupportedAttributes() 
        {
            if (slaveType == SlaveTypes.IEC104)
                arrAttributes = new string[] { "ENNo", "ReportingIndex", "DataType", "Deadband", "Multiplier", "Constant", "Description" };
            else if (slaveType == SlaveTypes.MODBUSSLAVE)
                arrAttributes = new string[] { "ENNo", "ReportingIndex", "DataType", "CommandType", "Deadband", "Multiplier", "Constant", "Description" };
            else if (slaveType == SlaveTypes.IEC101SLAVE)
                arrAttributes = new string[] { "ENNo", "ReportingIndex", "DataType", "Deadband", "Multiplier", "Constant", "Description" };
            else if (slaveType == SlaveTypes.IEC61850Server)
                arrAttributes = new string[] { "ENNo", "ReportingIndex", "DataType", "CommandType", "Deadband", "Multiplier", "Constant", "Description" };
        }

        private void SetSupportedDataTypes()
        {
            if (slaveType == SlaveTypes.IEC104)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_EN_DataType").ToArray();
            else if (slaveType == SlaveTypes.MODBUSSLAVE)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_EN_DataType").ToArray();
            else if (slaveType == SlaveTypes.IEC61850Server)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_EN_DataType").ToArray();
            //Namarta:7/7/2017
            else if (slaveType == SlaveTypes.IEC101SLAVE)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101Slave_EN_DataType").ToArray();
            if (arrDataTypes.Length > 0) dType = arrDataTypes[0];
        }
        private void SetSupportedCommandTypes()
        {
            //Only MODBUSlave supports this...
            if (slaveType == SlaveTypes.MODBUSSLAVE)
                arrCommandTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_EN_CommandType").ToArray();
            if (slaveType == SlaveTypes.IEC61850Server)
                arrCommandTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_EN_CommandType").ToArray();
            if (arrCommandTypes != null && arrCommandTypes.Length > 0) cType = arrCommandTypes[0];
        }
        public static List<string> getDataTypes(SlaveTypes slaveType)
        {
            if (slaveType == SlaveTypes.IEC104)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_EN_DataType");
            else if (slaveType == SlaveTypes.MODBUSSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_EN_DataType");
            else if (slaveType == SlaveTypes.IEC61850Server)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_EN_DataType");
            //Namrata:7/7/2017
            else if (slaveType == SlaveTypes.IEC101SLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101Slave_EN_DataType");
            else
                return new List<string>();
        }
        public static List<string> getCommandTypes(SlaveTypes slaveType)
        {
            //Only MODBUSlave supports this...
            if (slaveType == SlaveTypes.MODBUSSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_EN_CommandType");
            if (slaveType == SlaveTypes.IEC61850Server)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_EN_CommandType");
            else
                return new List<string>();
        }
        public ENMap(string enmName, List<KeyValuePair<string, string>> enmData, SlaveTypes sType)
        {
            slaveType = sType;
            SetSupportedAttributes();//IMP: Call only after slave types are set...
            SetSupportedDataTypes();
            SetSupportedCommandTypes();
            //First set the root element value...
            try
            {
                enmnType = (enmType)Enum.Parse(typeof(enmType), enmName);
            }
            catch (System.ArgumentException)
            {
                Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", enmName);
            }
            //Parse n store values...
            if (enmData != null && enmData.Count > 0)
            {
                foreach (KeyValuePair<string, string> enmkp in enmData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", enmkp.Key, enmkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(enmkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(enmkp.Key).SetValue(this, enmkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", enmkp.Key, enmkp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
        }

        public ENMap(XmlNode enmNode, SlaveTypes sType)
        {
            slaveType = sType;
            SetSupportedAttributes();//IMP: Call only after slave types are set...
            SetSupportedDataTypes();
            SetSupportedCommandTypes();

            //Parse n store values...
            Utils.WriteLine(VerboseLevel.DEBUG, "enmNode name: '{0}'", enmNode.Name);
            if (enmNode.Attributes != null)
            {
                //First set the root element value...
                try
                {
                    enmnType = (enmType)Enum.Parse(typeof(enmType), enmNode.Name);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", enmNode.Name);
                }

                foreach (XmlAttribute item in enmNode.Attributes)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", item.Name, item.Value);
                    try
                    {
                        if (this.GetType().GetProperty(item.Name) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(item.Name).SetValue(this, item.Value);
                        }
                    }
                    catch (System.NullReferenceException) {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", item.Name, item.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
            else if (enmNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = enmNode.Value;
            }
        }

        public void updateAttributes(List<KeyValuePair<string, string>> enmData)
        {
            if (enmData != null && enmData.Count > 0)
            {
                foreach (KeyValuePair<string, string> enmkp in enmData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", enmkp.Key, enmkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(enmkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(enmkp.Key).SetValue(this, enmkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", enmkp.Key, enmkp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
        }

        public XmlNode exportXMLnode()
        {
            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            XmlNode rootNode = null;

            if (isNodeComment)
            {
                rootNode = xmlDoc.CreateComment(comment);
                xmlDoc.AppendChild(rootNode);

                return rootNode;
            }
            rootNode = xmlDoc.CreateElement(enmnType.ToString());
            xmlDoc.AppendChild(rootNode);
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }
            return rootNode;
        }

        public string exportXML()
        {
            XmlNode xmlNode = exportXMLnode();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 2; //default is 1.
            xmlNode.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();

            return stringWriter.ToString();
        }

        public bool IsReindexed
        {
            get { return isReindexed; }
            set { isReindexed = value; }
        }

        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }

        public string ENNo
        {
            get { return enNum.ToString(); }
            set { enNum = Int32.Parse(value); }
        }

        public string ReportingIndex
        {
            get { return reportingIdx.ToString(); }
            set { reportingIdx = Int32.Parse(value); Globals.ENReportingIndex = Int32.Parse(value); }
        }

        public string DataType
        {
            get { return dType; }
            set {
                dType = value;
            }
        }

        public string CommandType
        {
            get { return cType; }
            set
            {
                cType = value;
            }
        }

        public string Deadband
        {
            get { return dBand.ToString(); }
            set
            {
                try
                {
                    dBand = Double.Parse(value);
                }
                catch (System.FormatException)
                {
                    dBand = 1;
                }
            }
        }

        public string Multiplier
        {
            get { return mult.ToString(); }
            set
            {
                try
                {
                    mult = Double.Parse(value);
                }
                catch (System.FormatException)
                {
                    mult = 1;
                }
            }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Constant
        {
            get { return cnst.ToString(); }
            set
            {
                try
                {
                    cnst = Double.Parse(value);
                }
                catch (System.FormatException)
                {
                    cnst = 0;
                }
            }
        }
    }
}
