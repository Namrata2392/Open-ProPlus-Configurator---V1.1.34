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
    * \brief     <b>AI</b> is a class to store info about Analog Input's
    * \details   This class stores info related to Analog Input's. It retrieves/stores various parameters like ResponseType, DataType, etc. 
    * depending on the master type this object belongs to. It also exports the XML node related to this object.
    * 
    */
    public class AI
    {
        #region Declaration
        enum aiType
        {
            AI
        };
        ucMasterIEC103 uciec = new ucMasterIEC103();
        private bool isNodeComment = false;
        private string comment = "";
        private aiType ainType = aiType.AI;
        private int aiNum = -1;
        private string rType = "";
        private int idx = -1;
        private int sidx = -1;
        private string dType = "";
        private double mult = 1;
        private double cnst = 0;
        private string aiDesc = "";
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        public int IEDNo = -1;
        private string[] arrAttributes;
        private string[] arrResponseTypes;
        private string[] arrDataTypes;
        //Namrata: 03/09/2017
        private string Iec61850Index = "";
        private string Iec61850ResponseType = "";
        private string Iec61850Iedname = "";
        //Namrata:10/04/2018
        private string fc= "";

        #endregion Declaration
        private void SetSupportedAttributes()
        {
            string strRoutineName = "SetSupportedAttributes";
            try
            {
                if (masterType == MasterTypes.IEC103)
                arrAttributes = new string[] { "AINo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "Description" };
            else if (masterType == MasterTypes.MODBUS)
                arrAttributes = new string[] { "AINo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "Description" };
            else if (masterType == MasterTypes.Virtual)
                arrAttributes = new string[] { "AINo", "ResponseType", "Index", "DataType", "Multiplier", "Constant", "Description" };
            //Namarta:3/6/2017
            else if (masterType == MasterTypes.ADR)
                arrAttributes = new string[] { "AINo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "Description" };
            //Namrata:7/7/2017
            else if (masterType == MasterTypes.IEC101)
                arrAttributes = new string[] { "AINo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "Description" };
            else if (masterType == MasterTypes.IEC61850Client)
                //Namrata :23/8/2017
                arrAttributes = new string[] { "AINo" };
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedResponseTypes()
        {
            string strRoutineName = "SetSupportedResponseTypes";
            try
            {
                if (masterType == MasterTypes.IEC103)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_AI_ResponseType").ToArray();
            else if (masterType == MasterTypes.MODBUS)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_AI_ResponseType").ToArray();
            else if (masterType == MasterTypes.Virtual)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_AI_ResponseType").ToArray();
            //Namrata:7/7/2017
            else if (masterType == MasterTypes.IEC101)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AI_ResponseType").ToArray();
            else if (masterType == MasterTypes.ADR)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("ADR_AI_ResponseType").ToArray();
                //else if (masterType == MasterTypes.IEC61850Client)
                //    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_AI_ResponseType").ToArray();
                //if (arrResponseTypes.Length > 0) rType = arrResponseTypes[0];
                //Namrata: 24/04/2018
                if (arrResponseTypes == null)
                {

                }
                else
                {
                    if (arrResponseTypes.Length > 0) dType = arrResponseTypes[0];
                }
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
                if (masterType == MasterTypes.IEC103)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_AI_DataType").ToArray();
            else if (masterType == MasterTypes.ADR)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("ADR_AI_DataType").ToArray();
            else if (masterType == MasterTypes.MODBUS)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_AI_DataType").ToArray();
            //else if (masterType == MasterTypes.IEC61850Client)
            //    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_AI_DataType").ToArray();
            else if (masterType == MasterTypes.Virtual)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_AI_DataType").ToArray();
            //Namrata:7/7/2017
            else if (masterType == MasterTypes.IEC101)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AI_DataType").ToArray();
            //if (arrDataTypes.Length > 0) dType = arrDataTypes[0];
            //Namrata: 22/01/2018
            if (arrDataTypes == null)
            {
               
            }
            else
            {
                if (arrDataTypes.Length > 0) dType = arrDataTypes[0];
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<string> getResponseTypes(MasterTypes masterType)
        {
        
                if (masterType == MasterTypes.IEC103)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_AI_ResponseType");
            else if (masterType == MasterTypes.ADR)
                return Utils.getOpenProPlusHandle().getDataTypeValues("ADR_AI_ResponseType");
            else if (masterType == MasterTypes.MODBUS)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_AI_ResponseType");
            else if (masterType == MasterTypes.Virtual)
                return Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_AI_ResponseType");
            //Namrata:7/7/2017
            else if (masterType == MasterTypes.IEC101)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AI_ResponseType");
            //else if (masterType == MasterTypes.IEC61850Client)
            //    return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_AI_ResponseType");
            else
                return new List<string>();
            
           
        }
        public static List<string> getDataTypes(MasterTypes masterType)
        {
            if (masterType == MasterTypes.IEC103)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_AI_DataType");
            else if (masterType == MasterTypes.ADR)
                return Utils.getOpenProPlusHandle().getDataTypeValues("ADR_AI_DataType");
            else if (masterType == MasterTypes.MODBUS)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_AI_DataType");
            else if (masterType == MasterTypes.Virtual)
                return Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_AI_DataType");
            //Namrata:7/7/2017
            else if (masterType == MasterTypes.IEC101)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_AI_DataType");
            //else if (masterType == MasterTypes.IEC61850Client)
            //    return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_AI_DataType");
            else
                return new List<string>();
        }
        public AI(string aiName, List<KeyValuePair<string, string>> aiData, TreeNode tn, MasterTypes mType, int mNo, int iNo)
        {
            string strRoutineName = "AI";
            try
            {
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                SetSupportedAttributes();
                SetSupportedResponseTypes();
                SetSupportedDataTypes();
                try
                {
                    ainType = (aiType)Enum.Parse(typeof(aiType), aiName);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", aiName);
                }
                //Parse n store values...
                if (aiData != null && aiData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> aikp in aiData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", aikp.Key, aikp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(aikp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(aikp.Key).SetValue(this, aikp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", aikp.Key, aikp.Value);
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
        public AI(XmlNode aiNode, MasterTypes mType, int mNo, int iNo, bool imported)
        {
            string strRoutineName = "AI";
            try
            {
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                SetSupportedAttributes();
                SetSupportedResponseTypes();
                SetSupportedDataTypes();
                Utils.WriteLine(VerboseLevel.DEBUG, "aiNode name: '{0}'", aiNode.Name);
                if (aiNode.Attributes != null)
                {
                    try
                    {
                        ainType = (aiType)Enum.Parse(typeof(aiType), aiNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", aiNode.Name);
                    }
                    if (masterType == MasterTypes.ADR || masterType == MasterTypes.IEC101 || masterType == MasterTypes.IEC103 || masterType == MasterTypes.MODBUS || masterType == MasterTypes.Virtual)
                    {
                        foreach (XmlAttribute item in aiNode.Attributes)
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
                    //Namrata: 17/7/2017
                    // If master Type is IEC61850Client
                    if (masterType == MasterTypes.IEC61850Client)
                    {
                        if (aiNode.Name == "AI")
                        {
                            foreach (XmlAttribute xmlattribute in aiNode.Attributes)
                            {
                                Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", xmlattribute.Name, xmlattribute.Value);
                                try
                                {
                                    if (xmlattribute.Name == "ResponseType")
                                    {
                                        Iec61850ResponseType = aiNode.Attributes[1].Value;
                                    }
                                    else if (xmlattribute.Name == "Index")
                                    {
                                        Iec61850Index = aiNode.Attributes[2].Value;
                                    }
                                    else if (xmlattribute.Name == "FC")
                                    {
                                        fc = aiNode.Attributes[3].Value;
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
                else if (aiNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = aiNode.Value;
                }
                if (imported)//Generate a new unique id...
                {
                    this.AINo = (Globals.AINo + 1).ToString();
                    if (masterType == MasterTypes.IEC103)
                    {
                        //Commented as asked by Amol, 29 / 12 / 2k16:
                        //this.Index = (Globals.AIIndex + 1).ToString();
                    }
                    else if (masterType == MasterTypes.ADR)
                    {
                        //Commented as asked by Amol, 29/12/2k16: this.Index = (Globals.AIIndex + 1).ToString();
                    }
                    else if (masterType == MasterTypes.MODBUS)
                    {
                        //Commented as asked by Amol, 29 / 12 / 2k16: this.Index = (Globals.AIIndex + 1).ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> aiData)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (aiData != null && aiData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> aikp in aiData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", aikp.Key, aikp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(aikp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(aikp.Key).SetValue(this, aikp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", aikp.Key, aikp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            rootNode = xmlDoc.CreateElement(ainType.ToString());

            #region MasterType IEC61850
            if (masterType == MasterTypes.IEC61850Client)
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
                XmlAttribute attr1 = xmlDoc.CreateAttribute("ResponseType");
                attr1.Value = Iec61850ResponseType.ToString();
                rootNode.Attributes.Append(attr1);

                XmlAttribute attr2 = xmlDoc.CreateAttribute("Index");
                attr2.Value = Iec61850Index.ToString();
                rootNode.Attributes.Append(attr2);

                XmlAttribute attFC = xmlDoc.CreateAttribute("FC");
                attFC.Value = FC.ToString();
                rootNode.Attributes.Append(attFC);

                XmlAttribute attr3 = xmlDoc.CreateAttribute("SubIndex");
                attr3.Value = SubIndex.ToString();
                rootNode.Attributes.Append(attr3);

                //XmlAttribute attr4 = xmlDoc.CreateAttribute("DataType");
                //attr4.Value = DataType.ToString();
                //rootNode.Attributes.Append(attr4);

                XmlAttribute attr5 = xmlDoc.CreateAttribute("Multiplier");
                attr5.Value = Multiplier.ToString();
                rootNode.Attributes.Append(attr5);

                XmlAttribute attr6 = xmlDoc.CreateAttribute("Constant");
                attr6.Value = Constant.ToString();
                rootNode.Attributes.Append(attr6);

                XmlAttribute attr7 = xmlDoc.CreateAttribute("Description");
                attr7.Value = Description.ToString();
                rootNode.Attributes.Append(attr7);
            }
            #endregion MasterType IEC61850

            #region MasterType IEC103,ADR,IEC101,MODBUS,Virtual
            if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.MODBUS) || (masterType == MasterTypes.Virtual))
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
            }
            #endregion MasterType IEC103,ADR,IEC101,MODBUS,Virtual

            return rootNode;
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string AINo
        {
            get { return aiNum.ToString(); }
            set { aiNum = Int32.Parse(value); Globals.AINo = Int32.Parse(value); }
        }
        public string ResponseType
        {
            get { return rType; }
            set
            {
                rType = value;
            }
        }
        public string Index
        {
            get { return idx.ToString(); }
            set { idx = Int32.Parse(value); Globals.AIIndex = Int32.Parse(value); }
        }
        public string SubIndex
        {
            get { return sidx.ToString(); }
            set { sidx = Int32.Parse(value); }
        }
        public string DataType
        {
            get { return dType; }
            set
            {
                dType = value;
            }
        }
        public string IEDName
        {
            get { return Iec61850Iedname; }
            set
            {
                Iec61850Iedname = value;
            }
        }
        public string IEC61850ResponseType
        {
            get { return Iec61850ResponseType; }
            set
            {
                Iec61850ResponseType = value;
            }
        }
        public string IEC61850Index
        {
            get { return Iec61850Index; }
            set
            {
                Iec61850Index = value;
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
        public string Description
        {
            get { return aiDesc; }
            set { aiDesc = value; }
        }
        public string FC
        {
            get { return fc; }
            set { fc= value; }
        }
    }
}
