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
    public class IEC101Slave
    {
        #region Declarations
        enum slaveType
        {
            IEC101Slave,
        };
        private bool isNodeComment = false;
        private string comment = "";
        private slaveType sType = slaveType.IEC101Slave;
        private int slaveNum = -1;
        private string portNum = "";
        private int asduAddress = -1;
        private string asSize = "";
        private string ioaSz = "";
        private string cSize = "";
        private int cyclicInterval = 0;
        private int eventQSize;
        private int debug;
        private string appFirmwareVersion;
        private string linkAddressSize = "";
        private string linkAddres = "";
        private bool run = true;
        private string[] arrAttributes = { "SlaveNum", "PortNum","LinkAddress","ASDUAddress", "LinkAddressSize", "ASDUSize", "IOASize", "COTSize", "CyclicInterval","EventQSize", "DEBUG", "AppFirmwareVersion", "Run" };
        private string[] arrASDUSizes;
        private string[] arrIOASizes;
        private string[] arrCOTSizes;
        private string[] arrPortNum;
        private string[] arrLinkAddressSizes;
        #endregion Declarations
        //Namrata: 14/02/2018
        private void SetSupportedLinkAddressSizes()
        {
            string strRoutineName = "SetSupportedLinkAddressSizes";
            try
            {
                arrLinkAddressSizes = Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte").ToArray();
                if (arrLinkAddressSizes.Length > 0) asSize = arrLinkAddressSizes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<string> getLinkAddresssizes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte");
        }
        private void SetSupportedASDUSizes()
        {
            string strRoutineName = "SetSupportedASDUSizes";
            try
            {
                arrASDUSizes = Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte").ToArray();
            if (arrASDUSizes.Length > 0) asSize = arrASDUSizes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedIOASizes()
        {
            string strRoutineName = "SetSupportedIOASizes";
            try
            {
                arrIOASizes = Utils.getOpenProPlusHandle().getDataTypeValues("ThreeByte").ToArray();
                if (arrIOASizes.Length > 0) ioaSz = arrIOASizes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}
        private void SetSupportedCOTSizes()
        {
            string strRoutineName = "SetSupportedCOTSizes";
            try
            {
                arrCOTSizes = Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte").ToArray();
            if (arrCOTSizes.Length > 0) cSize = arrCOTSizes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedPortNumber()
        {
            string strRoutineName = "SetSupportedPortNumber";
            try
            {
                arrPortNum = Utils.getOpenProPlusHandle().getDataTypeValues("UartNum").ToArray();
            if (arrPortNum.Length > 0) portNum = arrPortNum[0];
        }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}
        public static List<string> getASDUsizes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte");
        }
        public static List<string> getIOAsizes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("ThreeByte");
        }
        public static List<string> getPortNo()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("UartNum");
        }
        public static List<string> getCOTsizes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte");
        }
        public IEC101Slave(string s101Name , List<KeyValuePair<string,string>>s101Data,TreeNode tn)
        {
            string strRoutineName = "IEC101Slave";
            try
            {
                SetSupportedASDUSizes();
            SetSupportedIOASizes();
            SetSupportedCOTSizes();
            SetSupportedPortNumber();
            SetSupportedLinkAddressSizes();
            try
            {
                sType = (slaveType)Enum.Parse(typeof(slaveType), s101Name);
            }
            catch(System.ArgumentException)
            {
                Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!",s101Name);
            }
            //Parse n store values...
            if (s101Data != null && s101Data.Count > 0)
            {
                foreach (KeyValuePair<string, string> s104kp in s101Data)
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
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public IEC101Slave(XmlNode sNode, TreeNode tn)
        {
            string strRoutineName = "IEC101Slave";
            try
            {
                SetSupportedASDUSizes();
            SetSupportedIOASizes();
            SetSupportedCOTSizes();
            SetSupportedPortNumber();
            SetSupportedLinkAddressSizes();
            //IMP: Use tn when we want to further add child nodes to 'IEC104'. Check if it's null...
            //Parse n store values...
            Utils.WriteLine(VerboseLevel.DEBUG, "sNode name: '{0}'", sNode.Name);
            if (sNode.Attributes != null)
            {
                //First set the root element value...
                try
                {
                    sType = (slaveType)Enum.Parse(typeof(slaveType), sNode.Name);
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
        }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    //Not Used: uciec.lblMain.Text += slaveNum;
}
        public void updateAttributes(List<KeyValuePair<string, string>> s101Data)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (s101Data != null && s101Data.Count > 0)
            {
                foreach (KeyValuePair<string, string> s101kp in s101Data)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", s101kp.Key, s101kp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(s101kp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(s101kp.Key).SetValue(this, s101kp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", s101kp.Key, s101kp.Value);
                        }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Not Used: uciec.lblMain.Text += slaveNum;
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

            rootNode = xmlDoc.CreateElement(sType.ToString());
            xmlDoc.AppendChild(rootNode);

            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }

            return rootNode;
        }
        public string exportINI()
        {
            return this.SlaveNum;
        }
        public string getSlaveID
        {
            get { return "IEC101Slave_" + SlaveNum; }
        }

        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string SlaveNum
        {
            get { return slaveNum.ToString(); }
            set { slaveNum = Int32.Parse(value); Globals.SlaveNo = Int32.Parse(value); }
        }

        public string PortNum
        {
            get { return portNum; }
            set
            {
                portNum = value;
            }
        }
        public string ASDUAddress
        {
            get { return asduAddress.ToString(); }
            set { asduAddress = Int32.Parse(value); }
        }

        public string ASDUSize
        {
            get { return asSize; }
            set { asSize = value; }
        }

        public string IOASize
        {
            get
            {
                return ioaSz;
            }
            set
            {
                ioaSz = value;
            }
        }
        public string COTSize
        {
            get { return cSize; }
            set { cSize = value; }
        }

        public string CyclicInterval
        {
            get { return cyclicInterval.ToString(); }
            set { cyclicInterval = Int32.Parse(value); }
        }
        public string EventQSize
        {
            get { return eventQSize.ToString(); }
            set { eventQSize = Int32.Parse(value); }
        }

        public string DEBUG
        {
            get { return debug.ToString(); }
            set { debug = Int32.Parse(value); }
        }

        public string AppFirmwareVersion
        {
            get { return appFirmwareVersion; }
            set { appFirmwareVersion = value; }
        }

        public string Run
        {
            get { return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
            }
        }

        public string LinkAddress
        {
            get { return linkAddres.ToString(); }
            set { linkAddres = value; }
        }

        public string LinkAddressSize
        {
            get { return linkAddressSize; }
            set { linkAddressSize = value; }
        }




    }
}
