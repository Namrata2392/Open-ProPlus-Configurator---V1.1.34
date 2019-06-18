using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace OpenProPlusConfigurator
{
    public class PLUMaster
    {
        enum masterType
        {
            PLU
        };
        private bool isNodeComment = false;
        private string comment = "";
        private masterType mType = masterType.PLU;
        private int masterNum = -1;
        private bool run = true;
        private string[] arrAttributes = { "MasterNum", "Run" };

        public PLUMaster(string s104Name, List<KeyValuePair<string, string>> s104Data, TreeNode tn)
        {
            this.fillOptions();

            //IMP: Use tn when we want to further add child nodes to 'IEC104'. Check if it's null...

            //First set the root element value...
            try
            {
                mType = (masterType)Enum.Parse(typeof(masterType), s104Name);
            }
            catch (System.ArgumentException)
            {
                Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", s104Name);
            }

            //Parse n store values...
            if (s104Data != null && s104Data.Count > 0)
            {
                foreach (KeyValuePair<string, string> s104kp in s104Data)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", s104kp.Key, s104kp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(s104kp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(s104kp.Key).SetValue(this, s104kp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", s104kp.Key, s104kp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }

            //Not Used: uciec.lblMain.Text += slaveNum;
        }
        public PLUMaster(XmlNode sNode, TreeNode tn)
        {
            this.fillOptions();
            //Parse n store values...
            Utils.WriteLine(VerboseLevel.DEBUG, "sNode name: '{0}'", sNode.Name);
            if (sNode.Attributes != null)
            {
                //First set the root element value...
                try
                {
                    mType = (masterType)Enum.Parse(typeof(masterType), sNode.Name);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", sNode.Name);
                }

                foreach (XmlAttribute item in sNode.Attributes)
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
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", item.Name, item.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
            else if (sNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = sNode.Value;
            }
            //Not Used: uciec.lblMain.Text += slaveNum;
        }
        public void updateAttributes(List<KeyValuePair<string, string>> s104Data)
        {
            if (s104Data != null && s104Data.Count > 0)
            {
                foreach (KeyValuePair<string, string> s104kp in s104Data)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", s104kp.Key, s104kp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(s104kp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(s104kp.Key).SetValue(this, s104kp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", s104kp.Key, s104kp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
        }
        private void fillOptions()
        {
        }
        public Control getView(List<string> kpArr)
        {
            return null;
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
            rootNode = xmlDoc.CreateElement(mType.ToString());
            xmlDoc.AppendChild(rootNode);
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }
            return rootNode;
        }
        public string getMasterID
        {
            get { return "PLU_" + MasterNum; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string MasterNum
        {
            get { return masterNum.ToString(); }
            set { masterNum = Int32.Parse(value); Globals.MasterNo = Int32.Parse(value); }
        }
        public string Run
        {
            get { return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
            }
        }
    }
}
