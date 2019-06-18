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
    * \brief     <b>DP</b> is a class to store info about derived parameter
    * \details   This class stores info related to derived parameter. It retrieves/stores 
    * various parameters like AINo, operation, etc. It also exports the XML node 
    * related to this object.
    * 
    */
    public class DP
    { 
        private bool isNodeComment = false;
        private string comment = "";
        private bool isReindexedAINo1 = false;
        private bool isReindexedAINo2 = false;
        private string rnName = "";
        private int dpIndex = -1;
        private int aiNo1 = -1;
        private int aiNo2 = -1;
        private string opr = "";
        private int delayms = 5;
        private string[] arrAttributes = { "DPIndex", "AINo1", "AINo2", "Operation", "DelayMS" };
        private string[] arrOperations;

        private void SetSupportedOperations()
        {
            arrOperations = Utils.getOpenProPlusHandle().getDataTypeValues("Operation_Arithmatic").ToArray();
            if (arrOperations.Length > 0) opr = arrOperations[0];
        }

        public static List<string> getOperations()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("Operation_Arithmatic");
        }

        public DP(string dpName, List<KeyValuePair<string, string>> dpData)
        {
            SetSupportedOperations();
            rnName = dpName;
            //Parse n store values...
            if (dpData != null && dpData.Count > 0)
            {
                foreach (KeyValuePair<string, string> dpkp in dpData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", dpkp.Key, dpkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(dpkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(dpkp.Key).SetValue(this, dpkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", dpkp.Key, dpkp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
        }

        public DP(XmlNode dNode)
        {
            SetSupportedOperations();
            //Parse n store values...
            Utils.WriteLine(VerboseLevel.DEBUG, "dNode name: '{0}' {1}", dNode.Name, dNode.Value);
            rnName = dNode.Name;
            if (dNode.Attributes != null)
            {
                foreach (XmlAttribute item in dNode.Attributes)
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
            else if (dNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = dNode.Value;
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> dpData)
        {
            if (dpData != null && dpData.Count > 0)
            {
                foreach (KeyValuePair<string, string> dpkp in dpData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", dpkp.Key, dpkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(dpkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(dpkp.Key).SetValue(this, dpkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", dpkp.Key, dpkp.Value);
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
        public bool IsReindexedAINo1
        {
            get { return isReindexedAINo1; }
            set { isReindexedAINo1 = value; }
        }
        public bool IsReindexedAINo2
        {
            get { return isReindexedAINo2; }
            set { isReindexedAINo2 = value; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string DPIndex
        {
            get { return dpIndex.ToString(); }
            set { dpIndex = Int32.Parse(value); Globals.DPNo = Int32.Parse(value); }
        }
        public string AINo1
        {
            get { return aiNo1.ToString(); }
            set { aiNo1 = Int32.Parse(value); }
        }
        public string AINo2
        {
            get { return aiNo2.ToString(); }
            set { aiNo2 = Int32.Parse(value); }
        }
        public string Operation
        {
            get { return opr; }
            set
            {
                opr = value;
            }
        }
        public string DelayMS
        {
            get { return delayms.ToString(); }
            set { delayms = Int32.Parse(value); }
        }
    }
}
