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
    * \brief     <b>AIMap</b> is a class to store info about AI mapping's for various slaves.
    * \details   This class stores info related to AI mapping's for various slaves. It retrieves/stores various 
    * mapping parameters like DataType, Deadband, etc. depending on the slave type this object belongs to. 
    * It also exports the XML node related to this object.
    * 
    */
    public class AIMap
    {
        #region Declaration
        enum aimType
        {
            AI
        };
        private bool isNodeComment = false;
        private string comment = "";
        private bool isReindexed = false;
        private aimType aimnType = aimType.AI;
        private int aiNum = -1;
        private int reportingIdx = -1;
        private string dType = "";
        private string description = "";
        private string cType = "";
        private double dBand = 1;
        private double mult = 1;
        private double cnst = 0;
        public SlaveTypes slaveType = SlaveTypes.UNKNOWN;
        private string[] arrAttributes;
        private string[] arrDataTypes;
        private string[] arrCommandTypes;
        //Namrata: 02/11/2017
        private string iec61850reportingindex = "";
        #endregion Declaration
        private void SetSupportedAttributes()
        {
            string strRoutineName = "SetSupportedAttributes";
            try
            {
                if (slaveType == SlaveTypes.IEC104)
                arrAttributes = new string[] { "AINo", "ReportingIndex", "DataType", "Deadband", "Multiplier", "Constant","Description" };
            else if (slaveType == SlaveTypes.MODBUSSLAVE)
                arrAttributes = new string[] { "AINo", "ReportingIndex", "DataType", "CommandType", "Deadband", "Multiplier", "Constant", "Description" };
            //Namrata:07/07/2017
            else if (slaveType == SlaveTypes.IEC101SLAVE)
                arrAttributes = new string[] { "AINo", "ReportingIndex", "DataType", "Deadband", "Multiplier", "Constant", "Description" };
            else if (slaveType == SlaveTypes.IEC61850Server)
                arrAttributes = new string[] { "AINo" };
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedDataTypes()
        {
            string strRoutineName = "SetSupportedDataTypes";
            try
            {
                if (slaveType == SlaveTypes.IEC104)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_AI_DataType").ToArray();
            else if (slaveType == SlaveTypes.MODBUSSLAVE)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_AI_DataType").ToArray();
            //Namrata:07/07/2017
            else if (slaveType == SlaveTypes.IEC101SLAVE)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101Slave_AI_DataType").ToArray();
            else if (slaveType == SlaveTypes.IEC61850Server)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_AI_DataType").ToArray();
            if (arrDataTypes.Length > 0) dType = arrDataTypes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedCommandTypes()
        {
            string strRoutineName = "SetSupportedCommandTypes";
            try
            {
                //Only MODBUSlave supports this...
                if (slaveType == SlaveTypes.MODBUSSLAVE)
                arrCommandTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_AI_CommandType").ToArray();
            if (slaveType == SlaveTypes.IEC61850Server)
                arrCommandTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_AI_CommandType").ToArray();
            if (arrCommandTypes != null && arrCommandTypes.Length > 0) cType = arrCommandTypes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<string> getDataTypes(SlaveTypes slaveType)
        {
            if (slaveType == SlaveTypes.IEC104)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC104_AI_DataType");
            else if (slaveType == SlaveTypes.MODBUSSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_AI_DataType");
            //Namrata:07/07/2017
            else if (slaveType == SlaveTypes.IEC101SLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101Slave_AI_DataType");
            else if (slaveType == SlaveTypes.IEC61850Server)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_AI_DataType");
            else
                return new List<string>();
        }
        public static List<string> getCommandTypes(SlaveTypes slaveType)
        {
            //Only MODBUSlave supports this...
            if (slaveType == SlaveTypes.MODBUSSLAVE)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUSSlave_AI_CommandType");
            if (slaveType == SlaveTypes.IEC61850Server)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850Slave_AI_CommandType");
            else
                return new List<string>();
        }
        public AIMap(string aimName, List<KeyValuePair<string, string>> aimData, SlaveTypes sType)
        {
            string strRoutineName = "AIMap";
            try
            {
                slaveType = sType;
            SetSupportedAttributes();//IMP: Call only after slave types are set...
            SetSupportedDataTypes();
            SetSupportedCommandTypes();
            //First set the root element value...
            try
            {
                aimnType = (aimType)Enum.Parse(typeof(aimType), aimName);
            }
            catch (System.ArgumentException)
            {
                Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", aimName);
            }
            //Parse n store values...
            if (aimData != null && aimData.Count > 0)
            {
                foreach (KeyValuePair<string, string> aimkp in aimData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", aimkp.Key, aimkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(aimkp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(aimkp.Key).SetValue(this, aimkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", aimkp.Key, aimkp.Value);
                        }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public AIMap(XmlNode aimNode, SlaveTypes sType)
        {
            string strRoutineName = "AIMap";
            try
            {
                slaveType = sType;
            SetSupportedAttributes();//IMP: Call only after slave types are set...
            SetSupportedDataTypes();
            SetSupportedCommandTypes();
            Utils.WriteLine(VerboseLevel.DEBUG, "aimNode name: '{0}'", aimNode.Name); //Parse n store values...
            if (aimNode.Attributes != null)
            {
                //First set the root element value...
                try
                {
                    aimnType = (aimType)Enum.Parse(typeof(aimType), aimNode.Name);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", aimNode.Name);
                }
                //Namrata: 07/11/2017
                if ((slaveType == SlaveTypes.IEC101SLAVE) || (slaveType == SlaveTypes.IEC104) || (slaveType == SlaveTypes.MODBUSSLAVE) || (slaveType == SlaveTypes.UNKNOWN))
                {
                    foreach (XmlAttribute item in aimNode.Attributes)
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
                    if (aimNode.Name == "AI")
                    {
                        foreach (XmlAttribute xmlattribute in aimNode.Attributes)
                        {
                            Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", xmlattribute.Name, xmlattribute.Value);
                            try
                            {

                                if (xmlattribute.Name == "ReportingIndex")
                                {
                                    iec61850reportingindex = aimNode.Attributes[1].Value;
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
            else if (aimNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = aimNode.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> aimData)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (aimData != null && aimData.Count > 0)
            {
                foreach (KeyValuePair<string, string> aimkp in aimData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", aimkp.Key, aimkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(aimkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(aimkp.Key).SetValue(this, aimkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", aimkp.Key, aimkp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            rootNode = xmlDoc.CreateElement(aimnType.ToString());
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

                XmlAttribute attrDeadband = xmlDoc.CreateAttribute("Deadband");
                attrDeadband.Value = Deadband.ToString();
                rootNode.Attributes.Append(attrDeadband);

                XmlAttribute attrMultiplier = xmlDoc.CreateAttribute("Multiplier");
                attrMultiplier.Value = Multiplier.ToString();
                rootNode.Attributes.Append(attrMultiplier);

                XmlAttribute attrConstant = xmlDoc.CreateAttribute("Constant");
                attrConstant.Value = Multiplier.ToString();
                rootNode.Attributes.Append(attrConstant);

                XmlAttribute attrDescription = xmlDoc.CreateAttribute("Description");
                attrDescription.Value = Description.ToString();
                rootNode.Attributes.Append(attrDescription);
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
        public Control getView(List<string> kpArr)
        {
            return null;
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
        public string AINo
        {
            get { return aiNum.ToString(); }
            set { aiNum = Int32.Parse(value); }
        }
        public string ReportingIndex
        {
            get { return reportingIdx.ToString(); }
            set { reportingIdx = Int32.Parse(value); Globals.AIReportingIndex = Int32.Parse(value); }
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
