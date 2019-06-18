using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>MD</b> is a class to store info about max. demanded power
    * \details   This class stores info related to max. power demanded. It retrieves/stores 
    * various parameters like V_AINo, I1_AINo, High limit, etc. It also exports the XML node 
    * related to this object.
    * 
    */
    public class MD
    {
        private bool isNodeComment = false;
        private string comment = "";
        private bool isReindexedV_AINo = false;
        private bool isReindexedI1_AINo = false;
        private bool isReindexedI2_AINo = false;
        private bool isReindexedI3_AINo = false;
        private string rnName = "";
        private int mdIndex = -1;
        private int vaiNo = -1;
        private int i1aiNo = -1;
        private int i2aiNo = -1;
        private int i3aiNo = -1;
        private double multiplier = 1;
        private int high = -1;
        private string[] arrAttributes = { "MDIndex", "V_AINo", "I1_AINo", "I2_AINo", "I3_AINo", "Multiplier", "High", /*"DI",*/ };

        public MD(string mdName, List<KeyValuePair<string, string>> mdData)
        {
            rnName = mdName;

            //Parse n store values...
            if (mdData != null && mdData.Count > 0)
            {
                foreach (KeyValuePair<string, string> mdkp in mdData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", mdkp.Key, mdkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(mdkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(mdkp.Key).SetValue(this, mdkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", mdkp.Key, mdkp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
        }
        public MD(XmlNode mNode)
        {
            //Parse n store values...
            Utils.WriteLine(VerboseLevel.DEBUG, "mNode name: '{0}' {1}", mNode.Name, mNode.Value);
            rnName = mNode.Name;
            if (mNode.Attributes != null)
            {
                foreach (XmlAttribute item in mNode.Attributes)
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
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
            else if (mNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = mNode.Value;
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> mdData)
        {
            if (mdData != null && mdData.Count > 0)
            {
                foreach (KeyValuePair<string, string> mdkp in mdData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", mdkp.Key, mdkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(mdkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(mdkp.Key).SetValue(this, mdkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", mdkp.Key, mdkp.Value);
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

            rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);

            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }

            return rootNode;
        }
        public bool IsReindexedV_AINo
        {
            get { return isReindexedV_AINo; }
            set { isReindexedV_AINo = value; }
        }
        public bool IsReindexedI1_AINo
        {
            get { return isReindexedI1_AINo; }
            set { isReindexedI1_AINo = value; }
        }
        public bool IsReindexedI2_AINo
        {
            get { return isReindexedI2_AINo; }
            set { isReindexedI2_AINo = value; }
        }
        public bool IsReindexedI3_AINo
        {
            get { return isReindexedI3_AINo; }
            set { isReindexedI3_AINo = value; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string MDIndex
        {
            get { return mdIndex.ToString(); }
            set { mdIndex = Int32.Parse(value); Globals.MDNo = Int32.Parse(value); }
        }
        public string V_AINo
        {
            get { return vaiNo.ToString(); }
            set { vaiNo = Int32.Parse(value); }
        }
        public string I1_AINo
        {
            get { return i1aiNo.ToString(); }
            set { i1aiNo = Int32.Parse(value); }
        }
        public string I2_AINo
        {
            get { return i2aiNo.ToString(); }
            set { i2aiNo = Int32.Parse(value); }
        }
        public string I3_AINo
        {
            get { return i3aiNo.ToString(); }
            set { i3aiNo = Int32.Parse(value); }
        }
        public string Multiplier
        {
            get { return multiplier.ToString(); }
            set
            {
                try
                {
                    multiplier = Double.Parse(value);
                }
                catch (System.FormatException)
                {
                    multiplier = 1;
                }
            }
        }
        public string High
        {
            get { return high.ToString(); }
            set { high = Int32.Parse(value); }
        }
    }
}
