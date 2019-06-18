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
    * \brief     <b>DOMap</b> is a class to store info about DO mapping's for various slaves.
    * \details   This class stores info related to DO mapping's for various slaves. It retrieves/stores various 
    * mapping parameters like DataType, BitPos, etc. depending on the slave type this object belongs to. 
    * It also exports the XML node related to this object.
    * 
    */
    public class DOMap
    {
        #region Declaration
        enum domType
        {
            DO
        };
        //Namrata: 02/11/2017
        private string iec61850reportingindex = "";
        private string description = "";
        private bool isNodeComment = false;
        private string comment = "";
        private bool isReindexed = false;
        private domType domnType = domType.DO;
        private int doNum = -1;
        private int reportingIdx = -1;
        private string dType = "";
        private string cType = "";
        private int bitPos = -1;
        private bool selectRequired = false;
        private SlaveTypes slaveType = SlaveTypes.UNKNOWN;
        private string[] arrAttributes;
        private string[] arrDataTypes;
        private string[] arrCommandTypes;
        #endregion Declaration
        private void SetSupportedAttributes()
        {
            if (slaveType == SlaveTypes.IEC104)
                arrAttributes = new string[] { "DONo", "ReportingIndex", "DataType", "BitPos", "Select", "Description" };
            else if (slaveType == SlaveTypes.MODBUSSLAVE)
                arrAttributes = new string[] { "DONo", "ReportingIndex", "DataType", "CommandType", "BitPos", "Description" };
            else if (slaveType == SlaveTypes.IEC101SLAVE)
                arrAttributes = new string[] { "DONo", "ReportingIndex", "DataType", "BitPos", "Select", "Description" };
            else if (slaveType == SlaveTypes.IEC61850Server)
                //arrAttributes = new string[] { "DONo", "ReportingIndex", "DataType", "CommandType", "BitPos", "Description" };
            arrAttributes = new string[] { "DONo" };
        }
        private void SetSupportedDataTypes()
        {
            if (slaveType == SlaveTypes.IEC104)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_DO_DataType").ToArray();
            else if (slaveType == SlaveTypes.MODBUSSLAVE)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_DO_DataType").ToArray();
            else if (slaveType == SlaveTypes.IEC61850Server)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_DO_DataType").ToArray();
            //Namrata:7/7/2017
            else if (slaveType == SlaveTypes.IEC101SLAVE)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101Slave_DO_DataType").ToArray();
            if (arrDataTypes.Length > 0) dType = arrDataTypes[0];
        }
        private void SetSupportedCommandTypes()
        {
            //Only MODBUSlave supports this...
            if (slaveType == SlaveTypes.MODBUSSLAVE)
                arrCommandTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_DO_CommandType").ToArray();
            if (slaveType == SlaveTypes.IEC61850Server)
                arrCommandTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_DO_CommandType").ToArray();
            if (arrCommandTypes != null && arrCommandTypes.Length > 0) cType = arrCommandTypes[0];
        }
        public static List<string> getDataTypes(SlaveTypes slaveType)
        {
            if (slaveType == SlaveTypes.IEC104)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_DO_DataType");
            else if (slaveType == SlaveTypes.MODBUSSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_DO_DataType");
            else if (slaveType == SlaveTypes.IEC61850Server)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_DO_DataType");
            //Namrata:7/7/2017
            else if (slaveType == SlaveTypes.IEC101SLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101Slave_DO_DataType");
            else
                return new List<string>();
        }
        public static List<string> getCommandTypes(SlaveTypes slaveType)
        {
            //Only MODBUSlave supports this...
            if (slaveType == SlaveTypes.MODBUSSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_DO_CommandType");
            if (slaveType == SlaveTypes.IEC61850Server)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_DO_CommandType");
            else
                return new List<string>();
        }
        public DOMap(string domName, List<KeyValuePair<string, string>> domData, SlaveTypes sType)
        {
            slaveType = sType;
            SetSupportedAttributes();//IMP: Call only after slave types are set...
            SetSupportedDataTypes();
            SetSupportedCommandTypes();
            //First set the root element value...
            try
            {
                domnType = (domType)Enum.Parse(typeof(domType), domName);
            }
            catch (System.ArgumentException)
            {
                Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", domName);
            }
            //Parse n store values...
            if (domData != null && domData.Count > 0)
            {
                foreach (KeyValuePair<string, string> domkp in domData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", domkp.Key, domkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(domkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(domkp.Key).SetValue(this, domkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", domkp.Key, domkp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
        }
        public DOMap(XmlNode domNode, SlaveTypes sType)
        {
            slaveType = sType;
            SetSupportedAttributes();//IMP: Call only after slave types are set...
            SetSupportedDataTypes();
            SetSupportedCommandTypes();
            //Parse n store values...
            Utils.WriteLine(VerboseLevel.DEBUG, "domNode name: '{0}'", domNode.Name);
            if (domNode.Attributes != null)
            {
                //First set the root element value...
                try
                {
                    domnType = (domType)Enum.Parse(typeof(domType), domNode.Name);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", domNode.Name);
                }
                if ((slaveType == SlaveTypes.IEC101SLAVE) || (slaveType == SlaveTypes.IEC104) || (slaveType == SlaveTypes.MODBUSSLAVE) || (slaveType == SlaveTypes.UNKNOWN))
                {
                    foreach (XmlAttribute item in domNode.Attributes)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", item.Name, item.Value);
                        try
                        {
                            if (this.GetType().GetProperty(item.Name) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(item.Name).SetValue(this, item.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", item.Name, item.Value);
                        }
                    }
                }
                //Namrata: 07/11/2017
                // If slaveType Type is IEC61850Server
                if (slaveType == SlaveTypes.IEC61850Server)
                {
                    if (domNode.Name == "DO")
                    {
                        foreach (XmlAttribute xmlattribute in domNode.Attributes)
                        {
                            Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", xmlattribute.Name, xmlattribute.Value);
                            try
                            {

                                if (xmlattribute.Name == "ReportingIndex")
                                {
                                    iec61850reportingindex = domNode.Attributes[1].Value;
                                }
                                else
                                {
                                    if (this.GetType().GetProperty(xmlattribute.Name) != null) //Ajay: 10/08/2018
                                    {
                                        this.GetType().GetProperty(xmlattribute.Name).SetValue(this, xmlattribute.Value);
                                    }
                                }
                            }
                            catch (System.NullReferenceException)
                            {
                                Utils.WriteLine(VerboseLevel.WARNING, "DI: Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", xmlattribute.Name, xmlattribute.Value);
                            }
                        }
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
            else if (domNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = domNode.Value;
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> domData)
        {
            if (domData != null && domData.Count > 0)
            {
                foreach (KeyValuePair<string, string> domkp in domData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", domkp.Key, domkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(domkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(domkp.Key).SetValue(this, domkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", domkp.Key, domkp.Value);
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
            rootNode = xmlDoc.CreateElement(domnType.ToString());
            xmlDoc.AppendChild(rootNode);
            if (slaveType == SlaveTypes.IEC61850Server)
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
               
                XmlAttribute attr2 = xmlDoc.CreateAttribute("ReportingIndex");
                attr2.Value = iec61850reportingindex.ToString();
                rootNode.Attributes.Append(attr2);

                XmlAttribute attrDataType = xmlDoc.CreateAttribute("DataType");
                attrDataType.Value = DataType.ToString();
                rootNode.Attributes.Append(attrDataType);

                XmlAttribute attrCommandType = xmlDoc.CreateAttribute("CommandType");
                attrCommandType.Value = CommandType.ToString();
                rootNode.Attributes.Append(attrCommandType);

                XmlAttribute attrDeadband = xmlDoc.CreateAttribute("BitPos");
                attrDeadband.Value = BitPos.ToString();
                rootNode.Attributes.Append(attrDeadband);

                XmlAttribute attrConstant = xmlDoc.CreateAttribute("Description");
                attrConstant.Value = Description.ToString();
                rootNode.Attributes.Append(attrConstant);
            }
            if ((slaveType == SlaveTypes.IEC101SLAVE) || (slaveType == SlaveTypes.IEC104) || (slaveType == SlaveTypes.MODBUSSLAVE) || (slaveType == SlaveTypes.UNKNOWN))
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
            }
            return rootNode;
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
        public string DONo
        {
            get { return doNum.ToString(); }
            set { doNum = Int32.Parse(value); }
        }
        public string ReportingIndex
        {
            get { return reportingIdx.ToString(); }
            set { reportingIdx = Int32.Parse(value); Globals.DOReportingIndex = Int32.Parse(value); }
        }
        public string DataType
        {
            get { return dType; }
            set
            {
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
        public string BitPos
        {
            get { return bitPos.ToString(); }
            set { bitPos = Int32.Parse(value); }
        }
        public string Select
        {
            get { return (selectRequired == true ? "ENABLE" : "DISABLE"); }
            set { selectRequired = (value.ToLower() == "enable") ? true : false; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string IEC61850ReportingIndex
        {
            get { return iec61850reportingindex; }
            set
            {
                iec61850reportingindex = value;
            }
        }
    }
}
