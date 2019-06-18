//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml;
//using System.IO;
//using System.Windows.Forms;

//namespace OpenProPlusConfigurator
//{
//    public class RCB
//    {
//        enum RcbType
//        {
//            RCB
//        };
//        private bool isNodeComment = false;
//        private string comment = "";
//        private RcbType ainType = RcbType.RCB;
//        private int aiNum = -1;
//        private int sidx = -1;
//        private double mult = 1;
//        private double cnst = 0;
//        private string triggerOptions = "";
//        private string address = "";
//        private string no = "";
//        private string dataset = "";
//        private string bufferTime = "";
//        private string integrityPeriod = "";
//        private string configRevision = "";
//        private MasterTypes masterType = MasterTypes.UNKNOWN;
//        private int masterNo = -1;
//        public int IEDNo = -1;
//        private string[] arrAttributes;
//        private void SetSupportedRCBAttributes()
//        {
//            if (masterType == MasterTypes.IEC61850Client)
//                arrAttributes = new string[] { "Address", "BufferTime", "ConfigRevision", "Dataset", "IntegrityPeriod", "TriggerOptions" };
//            //arrAttributes = new string[] { "address", "dataset", "triggerOptions", "bufferTime", "integrityPeriod", "configRevision" };
//        }
//        public RCB(string aiName, List<KeyValuePair<string, string>> aiData, TreeNode tn, MasterTypes mType, int mNo, int iNo)
//        {

//            masterType = mType;
//            masterNo = mNo;
//            IEDNo = iNo;
//            SetSupportedRCBAttributes();
//            try
//            {
//                ainType = (RcbType)Enum.Parse(typeof(RcbType), aiName);
//            }
//            catch (System.ArgumentException)
//            {
//                Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", aiName);
//            }

//            //Parse n store values...
//            if (aiData != null && aiData.Count > 0)
//            {
//                foreach (KeyValuePair<string, string> aikp in aiData)
//                {
//                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", aikp.Key, aikp.Value);
//                    try
//                    {
//                        this.GetType().GetProperty(aikp.Key).SetValue(this, aikp.Value);
//                    }
//                    catch (System.NullReferenceException)
//                    {
//                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", aikp.Key, aikp.Value);
//                    }
//                }
//                Utils.Write(VerboseLevel.DEBUG, "\n");
//            }

//        }

//        public RCB(XmlNode aiNode, MasterTypes mType, int mNo, int iNo, bool imported)
//        {
//            string strRoutineName = "AI";
//            try
//            {
//                masterType = mType;
//                masterNo = mNo;
//                IEDNo = iNo;
//                SetSupportedRCBAttributes();

//                //Parse n store values...
//                Utils.WriteLine(VerboseLevel.DEBUG, "aiNode name: '{0}'", aiNode.Name);
//                if (aiNode.Attributes != null)
//                {
//                    //First set the root element value...
//                    try
//                    {
//                        ainType = (RcbType)Enum.Parse(typeof(RcbType), aiNode.Name);
//                    }
//                    catch (System.ArgumentException)
//                    {
//                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", aiNode.Name);
//                    }

//                    foreach (XmlAttribute item in aiNode.Attributes)
//                    {
//                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", item.Name, item.Value);
//                        try
//                        {
//                            this.GetType().GetProperty(item.Name).SetValue(this, item.Value);
//                        }
//                        catch (System.NullReferenceException)
//                        {
//                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", item.Name, item.Value);
//                        }
//                    }
//                    Utils.Write(VerboseLevel.DEBUG, "\n");
//                }
//                else if (aiNode.NodeType == XmlNodeType.Comment)
//                {
//                    isNodeComment = true;
//                    comment = aiNode.Value;
//                }

//                if (imported)//Generate a new unique id...
//                {
//                    //this.AINo = (Globals.AINo + 1).ToString();
//                    //if (masterType == MasterTypes.IEC103)
//                    //{
//                    //    //Commented as asked by Amol, 29 / 12 / 2k16:
//                    //    //this.Index = (Globals.AIIndex + 1).ToString();
//                    //}
//                    //else if (masterType == MasterTypes.ADR)
//                    //{
//                    //    //Commented as asked by Amol, 29/12/2k16: this.Index = (Globals.AIIndex + 1).ToString();
//                    //}
//                    //else if (masterType == MasterTypes.MODBUS)
//                    //{
//                    //    //Commented as asked by Amol, 29 / 12 / 2k16: this.Index = (Globals.AIIndex + 1).ToString();
//                    //}
//                }
//            }
//            catch (Exception Ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        public void updateAttributes(List<KeyValuePair<string, string>> aiData)
//        {
//            string strRoutineName = "updateAttributes";
//            try
//            {
//                if (aiData != null && aiData.Count > 0)
//                {
//                    foreach (KeyValuePair<string, string> aikp in aiData)
//                    {
//                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", aikp.Key, aikp.Value);
//                        try
//                        {
//                            this.GetType().GetProperty(aikp.Key).SetValue(this, aikp.Value);
//                        }
//                        catch (System.NullReferenceException)
//                        {
//                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", aikp.Key, aikp.Value);
//                        }
//                    }
//                    Utils.Write(VerboseLevel.DEBUG, "\n");
//                }
//            }
//            catch (Exception Ex)
//            {
//                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//        public XmlNode exportXMLnode()
//        {
//            string strRoutineName = "exportXMLnode";
//            try
//            {
//                XmlDocument xmlDoc = new XmlDocument();
//                StringWriter stringWriter = new StringWriter();
//                XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
//                XmlNode rootNode = null;

//                if (isNodeComment)
//                {
//                    rootNode = xmlDoc.CreateComment(comment);
//                    xmlDoc.AppendChild(rootNode);

//                    return rootNode;
//                }
//                rootNode = xmlDoc.CreateElement(ainType.ToString());
//                foreach (string attr in arrAttributes)
//                {
//                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
//                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
//                    rootNode.Attributes.Append(attrName);
//                }
//                return rootNode;
//            }
//            catch (Exception Ex)
//            {
//                throw Ex;
//            }
//        }

//        public bool IsNodeComment
//        {
//            get { return isNodeComment; }
//        }
//        //arrAttributes = new string[] { "Address", "BufferTime", "ConfigRevision", "Dataset", "IntegrityPeriod", "TriggerOptions" };

//        public string Address
//        {
//            get { return address; }
//            set
//            {
//                address = value;
//            }
//        }
//        public string Dataset
//        {
//            get { return dataset; }
//            set
//            {
//                dataset = value;
//            }
//        }
//        public string TriggerOptions
//        {
//            get { return triggerOptions; }
//            set
//            {
//                triggerOptions = value;
//            }
//        }
//        public string BufferTime
//        {
//            get { return bufferTime; }
//            set
//            {
//                bufferTime = value;
//            }
//        }
//        public string IntegrityPeriod
//        {
//            get { return integrityPeriod; }
//            set
//            {
//                integrityPeriod = value;
//            }
//        }
//        public string ConfigRevision
//        {
//            get { return configRevision; }
//            set
//            {
//                configRevision = value;
//            }
//        }

//        //public string RowNo
//        //{
//        //    get { return no; }
//        //    set
//        //    {
//        //        no = value;
//        //    }
//        //}

//    }
//}

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
    public class RCB
    {
        enum RcbType
        {
            RCB
        };
        private bool isNodeComment = false;
        private string comment = "";
        private RcbType ainType = RcbType.RCB;
        private string triggerOptions = "";
        private string address = "";
        private string dataset = "";
        private string bufferTime = "";
        private string integrityPeriod = "";
        private string configRevision = "";
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        public int IEDNo = -1;
        private string[] arrAttributes;
        //Namrata: 27/01/2018
        private string triggeroptions = "";
        private void SetSupportedRCBAttributes()
        {
            if (masterType == MasterTypes.IEC61850Client)
                arrAttributes = new string[] { "Address", "BufferTime", "ConfigRevision", "Dataset", "IntegrityPeriod", "TriggerOptions" };
        }
        public RCB(string aiName, List<KeyValuePair<string, string>> aiData, TreeNode tn, MasterTypes mType, int mNo, int iNo)
        {

            masterType = mType;
            masterNo = mNo;
            IEDNo = iNo;
            SetSupportedRCBAttributes();
            try
            {
                ainType = (RcbType)Enum.Parse(typeof(RcbType), aiName);
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

        public RCB(XmlNode aiNode, MasterTypes mType, int mNo, int iNo, bool imported)
        {
            string strRoutineName = "AI";
            try
            {
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                SetSupportedRCBAttributes();

                //Parse n store values...
                Utils.WriteLine(VerboseLevel.DEBUG, "aiNode name: '{0}'", aiNode.Name);
                if (aiNode.Attributes != null)
                {
                    //First set the root element value...
                    try
                    {
                        ainType = (RcbType)Enum.Parse(typeof(RcbType), aiNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", aiNode.Name);
                    }

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
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                else if (aiNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = aiNode.Value;
                }

                if (imported)//Generate a new unique id...
                {
                   
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
            string strRoutineName = "exportXMLnode";
            try
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
                foreach (string attr in arrAttributes)
                {
                    XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                    attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                    rootNode.Attributes.Append(attrName);
                }
                return rootNode;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        //arrAttributes = new string[] { "Address", "BufferTime", "ConfigRevision", "Dataset", "IntegrityPeriod", "TriggerOptions" };

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
            }
        }
        public string Dataset
        {
            get { return dataset; }
            set
            {
                dataset = value;
            }
        }
        public string TriggerOptions
        {
            get { return triggeroptions; }
            set
            {
                triggeroptions = value;
            }
        }
        //public string 
        //{
        //    get { return triggerOptions; }
        //    set
        //    {
        //        triggerOptions = value;
        //    }
        //}
        public string TriggerOptions1
        {
            get { return triggerOptions; }
            set
            {
                triggerOptions = value;
            }
        }
        public string BufferTime
        {
            get { return bufferTime; }
            set
            {
                bufferTime = value;
            }
        }
        public string IntegrityPeriod
        {
            get { return integrityPeriod; }
            set
            {
                integrityPeriod = value;
            }
        }
        public string ConfigRevision
        {
            get { return configRevision; }
            set
            {
                configRevision = value;
            }
        }

        //public string RowNo
        //{
        //    get { return no; }
        //    set
        //    {
        //        no = value;
        //    }
        //}

    }
}
