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
    * \brief     <b>Profile</b> is a class to store info about profiling
    * \details   This class stores info related to profiling. It retrieves/stores 
    * various parameters like AINo, High limit, Low limit, etc. It also exports the XML node 
    * related to this object.
    * 
    */
    public class Profile
    {
        private bool isNodeComment = false;
        private string comment = "";
        private bool isReindexedAINo = false;
        private string rnName = "";
        private int profileIndex = -1;
        private int aiNo = -1;
        private int high = -1;
        private int low = -1;
        private int delaySec = -1;

        private string[] arrAttributes = { "ProfileIndex", "AINo", "High", "Low", "DelaySec" };

        public Profile(string pName, List<KeyValuePair<string, string>> pData)
        {
            rnName = pName;

            //Parse n store values...
            if (pData != null && pData.Count > 0)
            {
                foreach (KeyValuePair<string, string> pkp in pData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", pkp.Key, pkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(pkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(pkp.Key).SetValue(this, pkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", pkp.Key, pkp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
        }

        public Profile(XmlNode pNode)
        {
            //Parse n store values...
            Utils.WriteLine(VerboseLevel.DEBUG, "pNode name: '{0}' {1}", pNode.Name, pNode.Value);
            rnName = pNode.Name;
            if (pNode.Attributes != null)
            {
                foreach (XmlAttribute item in pNode.Attributes)
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
            else if (pNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = pNode.Value;
            }
        }

        public void updateAttributes(List<KeyValuePair<string, string>> pData)
        {
            if (pData != null && pData.Count > 0)
            {
                foreach (KeyValuePair<string, string> pkp in pData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", pkp.Key, pkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(pkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(pkp.Key).SetValue(this, pkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", pkp.Key, pkp.Value);
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

        public bool IsReindexedAINo
        {
            get { return isReindexedAINo; }
            set { isReindexedAINo = value; }
        }

        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }

        public string ProfileIndex
        {
            get { return profileIndex.ToString(); }
            set { profileIndex = Int32.Parse(value); Globals.ProfileNo = Int32.Parse(value); }
        }

        public string AINo
        {
            get { return aiNo.ToString(); }
            set { aiNo = Int32.Parse(value); }
        }

        public string High
        {
            get { return high.ToString(); }
            set { high = Int32.Parse(value); }
        }

        public string Low
        {
            get { return low.ToString(); }
            set { low = Int32.Parse(value); }
        }

        public string DelaySec
        {
            get { return delaySec.ToString(); }
            set { delaySec = Int32.Parse(value); }
        }
    }
}
