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
    * \brief     <b>EN</b> is a class to store info about Energy parameters
    * \details   This class stores info related to Energy parameters. It retrieves/stores various parameters like ResponseType, DataType, etc. 
    * depending on the master type this object belongs to. It also exports the XML node related to this object.
    * 
    */
    public class EN
    {
        enum enType
        {
            EN
        };
        private bool isNodeComment = false;
        private string comment = "";
        private enType ennType = enType.EN;
        private int enNum = -1;
        private string rType = "";
        private int idx = -1;
        private int sidx = -1;
        private string dType = "";
        private double mult = 1;
        private double cnst = 0;
        private string enDesc = "";
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        private string[] arrAttributes;// = { "ENNo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "Description" };
        private string[] arrResponseTypes;
        private string[] arrDataTypes;
        //Namrata: 03/09/2017
        private string Iec61850Index = "";
        private string Iec61850ResponseType = "";
        private string Iec61850Iedname = "";
        //Namrata:10/04/2018
        private string fc = "";
        private void SetSupportedAttributes()
        {
            if (masterType == MasterTypes.IEC103 || masterType == MasterTypes.MODBUS|| masterType == MasterTypes.Virtual || masterType == MasterTypes.IEC101 || masterType == MasterTypes.ADR)
                arrAttributes = new string[] { "ENNo", "ResponseType", "Index", "SubIndex", "DataType", "Multiplier", "Constant", "Description" };
            if (masterType == MasterTypes.IEC61850Client)
                arrAttributes = new string[] { "ENNo" };
        }

        private void SetSupportedResponseTypes()
        {
            if (masterType == MasterTypes.IEC103)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_EN_ResponseType").ToArray();
            else if (masterType == MasterTypes.ADR)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("ADR_EN_ResponseType").ToArray();
            else if (masterType == MasterTypes.MODBUS)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_EN_ResponseType").ToArray();
            else if (masterType == MasterTypes.Virtual)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_EN_ResponseType").ToArray();
            //Namrata:7/7/2017
            else if (masterType == MasterTypes.IEC101)
                arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_EN_ResponseType").ToArray();
            //else if (masterType == MasterTypes.IEC61850Client)
            //    arrResponseTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_EN_ResponseType").ToArray();
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
        private void SetSupportedDataTypes()
        {
            if (masterType == MasterTypes.IEC103)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_EN_DataType").ToArray();
            else if (masterType == MasterTypes.ADR)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("ADR_EN_DataType").ToArray();
            else if (masterType == MasterTypes.MODBUS)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_EN_DataType").ToArray();
            else if (masterType == MasterTypes.Virtual)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_EN_DataType").ToArray();
            //Namrata:7/72017
            else if (masterType == MasterTypes.IEC101)
                arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_EN_DataType").ToArray();
            //else if (masterType == MasterTypes.IEC61850Client)
            //    arrDataTypes = Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_EN_DataType").ToArray();
            //if (arrDataTypes.Length > 0) dType = arrDataTypes[0];
            //Namrata: 24/04/2018
            if (arrDataTypes == null)
            {

            }
            else
            {
                if (arrDataTypes.Length > 0) dType = arrDataTypes[0];
            }
        }
        public static List<string> getResponseTypes(MasterTypes masterType)
        {
            if (masterType == MasterTypes.IEC103)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_EN_ResponseType");
            else if (masterType == MasterTypes.ADR)
                return Utils.getOpenProPlusHandle().getDataTypeValues("ADR_EN_ResponseType");
            else if (masterType == MasterTypes.MODBUS)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_EN_ResponseType");
            else if (masterType == MasterTypes.Virtual)
                return Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_EN_ResponseType");
            //Namrata:7/7/2017
            if (masterType == MasterTypes.IEC101)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_EN_ResponseType");
            //else if (masterType == MasterTypes.IEC61850Client)
            //    return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_EN_ResponseType");
            else
                return null;
        }
        public static List<string> getDataTypes(MasterTypes masterType)
        {
            if (masterType == MasterTypes.IEC103)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC103_EN_DataType");
            else if (masterType == MasterTypes.ADR)
                return Utils.getOpenProPlusHandle().getDataTypeValues("ADR_EN_DataType");
            else if (masterType == MasterTypes.MODBUS)
                return Utils.getOpenProPlusHandle().getDataTypeValues("MODBUS_EN_DataType");
            else if (masterType == MasterTypes.Virtual)
                return Utils.getOpenProPlusHandle().getDataTypeValues("Virtual_EN_DataType");
            //Namarta:7/72017
            else if (masterType == MasterTypes.IEC101)
                return Utils.getOpenProPlusHandle().getDataTypeValues("IEC101_EN_DataType");
            //else if (masterType == MasterTypes.IEC61850Client)
            //    return Utils.getOpenProPlusHandle().getDataTypeValues("IEC61850_EN_DataType");
            else
                return new List<string>();
        }
        public EN(string enName, List<KeyValuePair<string, string>> enData, TreeNode tn, MasterTypes mType, int mNo, int iNo)
        {
            masterType = mType;
            masterNo = mNo;
            IEDNo = iNo;
            SetSupportedAttributes();//IMP: Call only after master types are set...
            SetSupportedResponseTypes();//IMP: Call only after master types are set...
            SetSupportedDataTypes();//IMP: Call only after master types are set...

            //First set the root element value...
            try
            {
                ennType = (enType)Enum.Parse(typeof(enType), enName);
            }
            catch (System.ArgumentException)
            {
                Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", enName);
            }

            //Parse n store values...
            if (enData != null && enData.Count > 0)
            {
                foreach (KeyValuePair<string, string> enkp in enData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", enkp.Key, enkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(enkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(enkp.Key).SetValue(this, enkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", enkp.Key, enkp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
        }

        //public EN(XmlNode enNode, MasterTypes mType, int mNo, int iNo, bool imported)
        //{
        //    masterType = mType;
        //    masterNo = mNo;
        //    IEDNo = iNo;
        //    SetSupportedAttributes();//IMP: Call only after master types are set...
        //    SetSupportedResponseTypes();//IMP: Call only after master types are set...
        //    SetSupportedDataTypes();//IMP: Call only after master types are set...
        //    //Parse n store values...
        //    Utils.WriteLine(VerboseLevel.DEBUG, "enNode name: '{0}'", enNode.Name);
        //    if (enNode.Attributes != null)
        //    {
        //        //First set the root element value...
        //        try
        //        {
        //            ennType = (enType)Enum.Parse(typeof(enType), enNode.Name);
        //        }
        //        catch (System.ArgumentException)
        //        {
        //            Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", enNode.Name);
        //        }
        //        foreach (XmlAttribute item in enNode.Attributes)
        //        {
        //            Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", item.Name, item.Value);
        //            try
        //            {
        //                this.GetType().GetProperty(item.Name).SetValue(this, item.Value);
        //            } catch (System.NullReferenceException) {
        //                Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", item.Name, item.Value);
        //            }
        //        }
        //        Utils.Write(VerboseLevel.DEBUG, "\n");
        //    }
        //    else if (enNode.NodeType == XmlNodeType.Comment)
        //    {
        //        isNodeComment = true;
        //        comment = enNode.Value;
        //    }
        //    if (imported)//Generate a new unique id...
        //    {
        //        this.ENNo = (Globals.ENNo + 1).ToString();
        //        if (masterType == MasterTypes.IEC103)
        //        {
        //            //Commented as asked by Amol, 29/12/2k16: this.Index = (Globals.ENIndex + 1).ToString();
        //        }
        //        else if (masterType == MasterTypes.ADR)
        //        {
        //            //Commented as asked by Amol, 29/12/2k16: this.Index = (Globals.ENIndex + 1).ToString();
        //        }
        //        else if (masterType == MasterTypes.MODBUS)
        //        {
        //            //Commented as asked by Amol, 29/12/2k16: this.Index = (Globals.ENIndex + 1).ToString();
        //        }
        //        else if (masterType == MasterTypes.IEC101)
        //        {
        //            //Commented as asked by Amol, 29/12/2k16: this.Index = (Globals.ENIndex + 1).ToString();
        //        }
        //    }
        //}
        public EN(XmlNode enNode, MasterTypes mType, int mNo, int iNo, bool imported)
        {
            masterType = mType;
            masterNo = mNo;
            IEDNo = iNo;
            SetSupportedAttributes();//IMP: Call only after master types are set...
            SetSupportedResponseTypes();//IMP: Call only after master types are set...
            SetSupportedDataTypes();//IMP: Call only after master types are set...
            //Parse n store values...
            Utils.WriteLine(VerboseLevel.DEBUG, "enNode name: '{0}'", enNode.Name);
            if (enNode.Attributes != null)
            {
                //First set the root element value...
                try
                {
                    ennType = (enType)Enum.Parse(typeof(enType), enNode.Name);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", enNode.Name);
                }
                if (masterType == MasterTypes.ADR || masterType == MasterTypes.IEC101 || masterType == MasterTypes.IEC103 || masterType == MasterTypes.MODBUS || masterType == MasterTypes.Virtual)
                {
                    foreach (XmlAttribute item in enNode.Attributes)
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
                    if (enNode.Name == "EN")
                    {
                        foreach (XmlAttribute xmlattribute in enNode.Attributes)
                        {
                            try
                            {

                                if (xmlattribute.Name == "ResponseType")
                                {
                                    Iec61850ResponseType = enNode.Attributes[1].Value;
                                }
                                else if (xmlattribute.Name == "Index")
                                {
                                    Iec61850Index = enNode.Attributes[2].Value;
                                }
                                else if (xmlattribute.Name == "FC")
                                {
                                    fc = enNode.Attributes[3].Value;
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
            else if (enNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = enNode.Value;
            }
            if (imported)//Generate a new unique id...
            {
                this.ENNo = (Globals.ENNo + 1).ToString();
                if (masterType == MasterTypes.IEC103)
                {
                    //Commented as asked by Amol, 29/12/2k16: this.Index = (Globals.ENIndex + 1).ToString();
                }
                else if (masterType == MasterTypes.ADR)
                {
                    //Commented as asked by Amol, 29/12/2k16: this.Index = (Globals.ENIndex + 1).ToString();
                }
                else if (masterType == MasterTypes.MODBUS)
                {
                    //Commented as asked by Amol, 29/12/2k16: this.Index = (Globals.ENIndex + 1).ToString();
                }
                else if (masterType == MasterTypes.IEC101)
                {
                    //Commented as asked by Amol, 29/12/2k16: this.Index = (Globals.ENIndex + 1).ToString();
                }
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> enData)
        {
            if (enData != null && enData.Count > 0)
            {
                foreach (KeyValuePair<string, string> enkp in enData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", enkp.Key, enkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(enkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(enkp.Key).SetValue(this, enkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", enkp.Key, enkp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
        }

        //public XmlNode exportXMLnode()
        //{
        //    XmlDocument xmlDoc = new XmlDocument();
        //    StringWriter stringWriter = new StringWriter();
        //    XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
        //    XmlNode rootNode = null;

        //    if (isNodeComment)
        //    {
        //        rootNode = xmlDoc.CreateComment(comment);
        //        xmlDoc.AppendChild(rootNode);
        //        return rootNode;
        //    }

        //    rootNode = xmlDoc.CreateElement(ennType.ToString());
        //    xmlDoc.AppendChild(rootNode);

        //    foreach (string attr in arrAttributes)
        //    {
        //        XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
        //        attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
        //        rootNode.Attributes.Append(attrName);
        //    }

        //    return rootNode;
        //}
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
            rootNode = xmlDoc.CreateElement(ennType.ToString());

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

        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }

        public string ENNo
        {
            get { return enNum.ToString(); }
            set { enNum = Int32.Parse(value); Globals.ENNo = Int32.Parse(value); }
        }

        public string ResponseType
        {
            get { return rType; }
            set {
                rType = value;
            }
        }

        public string Index
        {
            get { return idx.ToString(); }
            set { idx = Int32.Parse(value); Globals.ENIndex = Int32.Parse(value); }
        }

        public string SubIndex
        {
            get { return sidx.ToString(); }
            set { sidx = Int32.Parse(value); }
        }

        public string DataType
        {
            get { return dType; }
            set {
                dType = value;
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
            get { return enDesc; }
            set { enDesc = value; }
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
        public string FC
        {
            get { return fc; }
            set { fc = value; }
        }
    }
}
