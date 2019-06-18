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
    * \brief     <b>DI</b> is a class to store info about Digital Input's
    * \details   This class stores info related to Digital Input's. It retrieves/stores various parameters like ResponseType, etc. 
    * depending on the master type this object belongs to. It also exports the XML node related to this object.
    * 
    */
    public class DI
    {
        #region Declarations
        enum diType
        {
            DI
        };
        private bool isNodeComment = false;
        private string comment = "";
        private diType dinType = diType.DI;
        private int diNum = -1;
        private string rType = "";
        private int idx = 0;
        private int sidx = 0;
        private string diDesc = "";
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        private string[] arrAttributes;
        //Namrata: 17/7/2017
        private string[] arrResponseTypes;
        private string[] arrAttributesEvents;
        public List<DI> MODBUSList = new List<DI>();
        ucDIlist ucdi = new ucDIlist();
        //Namrata: 03/09/2017
        private string Iec61850DIIndex = "";
        private string Iec61850DIResponseType = "";
        private string Iec61850DIIedname = "";
        //namarta: 17/10.2017
        private string[] arrDataTypes;
        private string dType = "";
        //Namrata:10/04/2018
        private string fc = "";
        #endregion Declarations
        private void SetSupportedAttributes()
        {
            string strRoutineName = "SetSupportedAttributes";
            try
            {
                if (masterType == MasterTypes.IEC103 || masterType == MasterTypes.Virtual || masterType == MasterTypes.IEC101)
                    arrAttributes = new string[] { "DINo", "ResponseType", "Index", "SubIndex", "Description" };
                else if (masterType == MasterTypes.ADR)
                    arrAttributes = new string[] { "DINo", "ResponseType", "Index", "SubIndex", "Description" };
                    arrAttributesEvents = new string[] { "Event_T", "Event_F" };   //Namrata: 17/7/2017
                if (masterType == MasterTypes.IEC61850Client)
                    arrAttributes = new string[] { "DINo" };
                //Namarta: 17/10/2017
                if (masterType == MasterTypes.MODBUS)
                    arrAttributes = new string[] { "DINo", "ResponseType", "Index", "SubIndex", "DataType", "Description" };
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
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_DI_DataType").ToArray();
                //else if (masterType == MasterTypes.IEC61850Client)
                //    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_AI_DataType").ToArray();
                else if (masterType == MasterTypes.Virtual)
                    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_DI_DataType").ToArray();
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
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedResponseTypes()
        {
            string strRoutineName = "SetSupportedResponseTypes";
            try
            {
                if (masterType == MasterTypes.IEC103)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_DI_ResponseType").ToArray();
                //Namarta:07/07/2017
                else if (masterType == MasterTypes.ADR)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("ADR_DI_ResponseType").ToArray();
                else if (masterType == MasterTypes.MODBUS)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_DI_ResponseType").ToArray();
                //else if (masterType == MasterTypes.IEC61850Client)
                //    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_DI_ResponseType").ToArray();
                else if (masterType == MasterTypes.Virtual)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_DI_ResponseType").ToArray();
                //Namarta:07/07/2017
                else if (masterType == MasterTypes.IEC101)
                    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_DI_ResponseType").ToArray();
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
        public static List<string> getDataTypes(MasterTypes masterType)
        {
            if (masterType == MasterTypes.MODBUS)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_DI_DataType");
            //Namrata: 31/10/2017
            if (masterType == MasterTypes.Virtual)
                return Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_DI_DataType");
            else
                return new List<string>();
        }
        public static List<string> getResponseTypes(MasterTypes masterType)
        {
            if (masterType == MasterTypes.IEC103)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_DI_ResponseType");
            else if (masterType == MasterTypes.ADR)
                return Utils.getOpenProPlusHandle().getDataTypeValues("ADR_DI_ResponseType");
            else if (masterType == MasterTypes.MODBUS)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_DI_ResponseType");
            else if (masterType == MasterTypes.Virtual)
                return Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_DI_ResponseType");
            //Namarta:07/07/2017
            else if (masterType == MasterTypes.IEC101)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_DI_ResponseType");
            //else if (masterType == MasterTypes.IEC61850Client)
            //    return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_DI_ResponseType");
            else
                return null;
        }
        public DI(string diName, List<KeyValuePair<string, string>> diData, TreeNode tn, MasterTypes mType, int mNo, int iNo)
        {
            string strRoutineName = "DI";
            try
            {
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                SetSupportedAttributes();
                SetSupportedResponseTypes();
                //Namrata: 17/10/2017
                SetSupportedDataTypes();
                try
                {
                    dinType = (diType)Enum.Parse(typeof(diType), diName);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", diName);
                }
                if (diData != null && diData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> dikp in diData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", dikp.Key, dikp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(dikp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(dikp.Key).SetValue(this, dikp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "DI: Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", dikp.Key, dikp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DI(XmlNode diNode, MasterTypes mType, int mNo, int iNo, bool imported)
        {
            string StrRoutineName = "DI";
            try
            {
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                SetSupportedAttributes();
                SetSupportedResponseTypes();
                //Namrata: 17/10/2017
                SetSupportedDataTypes();
                Utils.WriteLine(VerboseLevel.DEBUG, "diNode name: '{0}'", diNode.Name);
                if (diNode.Attributes != null)
                {
                    try
                    {
                        dinType = (diType)Enum.Parse(typeof(diType), diNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", diNode.Name);
                    }
                    if (masterType == MasterTypes.ADR || masterType == MasterTypes.IEC101 || masterType == MasterTypes.IEC103 || masterType == MasterTypes.MODBUS || masterType == MasterTypes.Virtual)
                    {
                        foreach (XmlAttribute item in diNode.Attributes)
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
                                Utils.WriteLine(VerboseLevel.WARNING, "DI: Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", item.Name, item.Value);
                            }
                        }
                        //Namrata: 17/7/2017
                        if (masterType == MasterTypes.ADR)
                        {
                            if (diNode.HasChildNodes)
                            {
                                if (diNode.ChildNodes[0].Name == "Event")
                                {
                                    foreach (XmlAttribute xmlattribute in diNode.ChildNodes[0].Attributes)
                                    {
                                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", xmlattribute.Name, xmlattribute.Value);
                                        try
                                        {
                                            //To Get Values From XML
                                            if (this.GetType().GetProperty(xmlattribute.Name) != null) //Ajay: 10/08/2018
                                            {
                                                this.GetType().GetProperty(xmlattribute.Name).SetValue(this, xmlattribute.Value);
                                            }
                                        }
                                        catch (System.NullReferenceException)
                                        {
                                            Utils.WriteLine(VerboseLevel.WARNING, "DI: Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", xmlattribute.Name, xmlattribute.Value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //Namrata: 17/7/2017
                    if (masterType == MasterTypes.IEC61850Client)
                    {
                        if (diNode.Name == "DI")
                        {
                            foreach (XmlAttribute xmlattribute in diNode.Attributes)
                            {
                                Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", xmlattribute.Name, xmlattribute.Value);
                                try
                                {

                                    if (xmlattribute.Name == "ResponseType")
                                    {
                                        Iec61850DIResponseType = diNode.Attributes[1].Value;
                                    }
                                    else if (xmlattribute.Name == "Index")
                                    {
                                        Iec61850DIIndex = diNode.Attributes[2].Value;
                                    }
                                    else if(xmlattribute.Name == "FC")
                                    {
                                        fc = diNode.Attributes[3].Value;
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
                else if (diNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = diNode.Value;
                }
                if (imported)//Generate a new unique id...
                {
                    this.DINo = (Globals.DINo + 1).ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(StrRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> diData)
        {
            if (diData != null && diData.Count > 0)
            {
                foreach (KeyValuePair<string, string> dikp in diData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", dikp.Key, dikp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(dikp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(dikp.Key).SetValue(this, dikp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "DI: Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", dikp.Key, dikp.Value);
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
            rootNode = xmlDoc.CreateElement(dinType.ToString());
            xmlDoc.AppendChild(rootNode);
            if (masterType == MasterTypes.IEC61850Client)
            {
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
                XmlAttribute attr1 = xmlDoc.CreateAttribute("ResponseType");
                attr1.Value = Iec61850DIResponseType.ToString();
                rootNode.Attributes.Append(attr1);

                XmlAttribute attr2 = xmlDoc.CreateAttribute("Index");
                attr2.Value = Iec61850DIIndex.ToString();
                rootNode.Attributes.Append(attr2);
                
                XmlAttribute attrfc = xmlDoc.CreateAttribute("FC");
                attrfc.Value = FC.ToString();
                rootNode.Attributes.Append(attrfc);

                XmlAttribute attr3 = xmlDoc.CreateAttribute("SubIndex");
                attr3.Value = SubIndex.ToString();
                rootNode.Attributes.Append(attr3);

                XmlAttribute attr7 = xmlDoc.CreateAttribute("Description");
                attr7.Value = Description.ToString();
                rootNode.Attributes.Append(attr7);
            }

            else
            {
                //Namrata: 17/7/2017
                //Create New Element in XML
                XmlNode Data = xmlDoc.CreateNode(XmlNodeType.Element, "Event", null);
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
                //Namrata: 17/7/2017
                if (masterType == MasterTypes.ADR)
                {
                    foreach (string attr1 in arrAttributesEvents)
                    {
                        XmlAttribute attrName = xmlDoc.CreateAttribute(attr1);
                        Data.Attributes.Append(attrName);
                        attrName.Value = (string)this.GetType().GetProperty(attr1).GetValue(this);
                        rootNode.AppendChild(Data);
                    }
                }
            }
            return rootNode;
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string DINo
        {
            get { return diNum.ToString(); }
            set { diNum = Int32.Parse(value); Globals.DINo = Int32.Parse(value); }
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
            set { idx = Int32.Parse(value); Globals.DIIndex = Int32.Parse(value); }
        }
        public string SubIndex
        {
            get { return sidx.ToString(); }
            set { sidx = Int32.Parse(value); }
        }
        public string Event_T
        {
            get { return ucdi.txtEvent_T.Text; }
            set { ucdi.txtEvent_T.Text = value; }
        }
        public string Event_F
        {
            get { return ucdi.txtEvent_F.Text; }
            set { ucdi.txtEvent_F.Text = value; }
        }
        public string Description
        {
            get { return diDesc; }
            set { diDesc = value; }
        }
        public string FC
        {
            get { return fc; }
            set { fc = value; }
        }

        public string IEDName
        {
            get { return Iec61850DIIedname; }
            set
            {
                Iec61850DIIedname = value;
            }
        }
        public string IEC61850ResponseType
        {
            get { return Iec61850DIResponseType; }
            set
            {
                Iec61850DIResponseType = value;
            }
        }

        public string IEC61850Index
        {
            get { return Iec61850DIIndex; }
            set
            {
                Iec61850DIIndex = value;
            }
        }

        public string DataType
        {
            get { return dType; }
            set
            {
                dType = value;
            }
        }

    }
}
