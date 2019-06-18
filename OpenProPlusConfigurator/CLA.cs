using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Data;
using System.Windows.Forms;
namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>CLA</b> is a class to store info about Closed Loop Action
    * \details   This class stores info related to Closed Loop Action. It retrieves/stores 
    * various parameters like AINo, High limit, Low limit, etc. It also exports the XML node 
    * related to this object.
    * 
    */
    public class CLA
    {
        #region Declaration
        private bool isNodeComment = false;
        private string comment = "";
        private bool isReindexedAINo1 = false;
        private bool isReindexedAINo2 = false;
        private bool isReindexedDONo = false;
        private string rnName = "";
        private int claIndex = -1;
        private int aiNo1 = -1;
        private int aiNo2 = -1;
        private int doNo = -1;
        private int high = -1;
        private int low = -1;
        private int delaySec = -1;
        private string[] arrAttributes = { "CLAIndex", "AINo1", "AINo2", "DONo", "High", "Low", "DelaySec" };
        #endregion Declaration
        public CLA(string claName, List<KeyValuePair<string, string>> claData)
        {
            string strRoutineName = "CLA";
            try
            {
                rnName = claName;
                if (claData != null && claData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> clakp in claData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", clakp.Key, clakp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(clakp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(clakp.Key).SetValue(this, clakp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", clakp.Key, clakp.Value);
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
        public CLA(XmlNode cNode)
        {
            string strRoutineName = "CLA";
            try
            {
                //Parse n store values...
                Utils.WriteLine(VerboseLevel.DEBUG, "cNode name: '{0}' {1}", cNode.Name, cNode.Value);
                rnName = cNode.Name;
                if (cNode.Attributes != null)
                {
                    foreach (XmlAttribute item in cNode.Attributes)
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
                else if (cNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = cNode.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> claData)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (claData != null && claData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> clakp in claData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", clakp.Key, clakp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(clakp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(clakp.Key).SetValue(this, clakp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", clakp.Key, clakp.Value);
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
        public bool IsReindexedDONo
        {
            get { return isReindexedDONo; }
            set { isReindexedDONo = value; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string CLAIndex
        {
            get { return claIndex.ToString(); }
            set { claIndex = Int32.Parse(value); Globals.CLANo = Int32.Parse(value); }
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
        public string DONo
        {
            get { return doNo.ToString(); }
            set { doNo = Int32.Parse(value); }
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
