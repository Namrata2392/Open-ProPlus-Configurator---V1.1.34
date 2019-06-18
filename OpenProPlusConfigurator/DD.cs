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
    * \brief     <b>DD</b> is a class to store info about derived data input
    * \details   This class stores info related to derived data input. It retrieves/stores 
    * various parameters like DINo, Operation, etc. It also exports the XML node 
    * related to this object.
    * 
    */
    public class DD
    {
        #region Declaration
        private bool isNodeComment = false;
        private string comment = "";
        private bool isReindexedDINo1 = false;
        private bool isReindexedDINo2 = false;
        private string rnName = "";
        private int ddIndex = -1;
        private int diNo1 = -1;
        private int diNo2 = -1;
        private string opr = "";
        private int delayms = 5;
        private string[] arrAttributes = { "DDIndex", "DINo1", "DINo2", "Operation", "DelayMS" };
        private string[] arrOperations;
        #endregion Declaration
        private void SetSupportedOperations()
        {
            string strRoutineName = "SetSupportedOperations";
            try
            {
                arrOperations = Utils.getOpenProPlusHandle().getDataTypeValues("Operation_Logical").ToArray();
                if (arrOperations.Length > 0) opr = arrOperations[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<string> getOperations()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("Operation_Logical");
        }
        public DD(string ddName, List<KeyValuePair<string, string>> ddData)
        {
            string strRoutineName = "DD";
            try
            {
                SetSupportedOperations();
                rnName = ddName;
                //Parse n store values...
                if (ddData != null && ddData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> ddkp in ddData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", ddkp.Key, ddkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(ddkp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(ddkp.Key).SetValue(this, ddkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", ddkp.Key, ddkp.Value);
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
        public DD(XmlNode dNode)
        {
            string strRoutineName = "DD";
            try
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
                        catch (System.NullReferenceException)
                        {
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
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> ddData)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (ddData != null && ddData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> ddkp in ddData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", ddkp.Key, ddkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(ddkp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(ddkp.Key).SetValue(this, ddkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", ddkp.Key, ddkp.Value);
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
        public bool IsReindexedDINo1
        {
            get { return isReindexedDINo1; }
            set { isReindexedDINo1 = value; }
        }
        public bool IsReindexedDINo2
        {
            get { return isReindexedDINo2; }
            set { isReindexedDINo2 = value; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string DDIndex
        {
            get { return ddIndex.ToString(); }
            set { ddIndex = Int32.Parse(value); Globals.DDNo = Int32.Parse(value); }
        }
        public string DINo1
        {
            get { return diNo1.ToString(); }
            set { diNo1 = Int32.Parse(value); }
        }
        public string DINo2
        {
            get { return diNo2.ToString(); }
            set { diNo2 = Int32.Parse(value); }
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
