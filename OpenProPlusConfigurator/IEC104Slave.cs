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
    * \brief     <b>IEC104Slave</b> is a class to store info about IEC104 slave
    * \details   This class stores info related to IEC104 slave. It retrieves/stores 
    * various parameters like IOA size, COT size, Cyclic Interval, etc. It also exports the XML node 
    * related to this object.
    * 
    */
    public class IEC104Slave
    {
        #region Decalration
        enum slaveType
        {
            IEC104
        };
        ucGroupIEC104 UcGroupIEC104 = new ucGroupIEC104();
        private bool isNodeComment = false;
        private string comment = "";
        private slaveType sType = slaveType.IEC104;
        private int slaveNum = -1;
        private string localIP = "";
        private int tcpPort;
        private string remoteIP = "";
        private int asduAddress = -1;
        private string asSize = "";
        private string ioaSz = "";
        private string cSize = "";
        private int cyclicInterval = 0;
        private int t0;
        private int t1;
        private int t2;
        private int t3;
        private int w;
        private int k;
        private int eventQSize;
        private int debug;
        private string appFirmwareVersion;
        private string portName;
        private bool run = true;
        //Namrata: 8/12/2017
        private string secremoteIP = "";
        //Not Used: ucSlaveIEC104 uciec = new ucSlaveIEC104();
        private bool eventWithoutTime = true;
        private string[] arrAttributes = { "SlaveNum", "LocalIP", "TcpPort", "RemoteIP", "SecRemoteIP", "ASDUAddress", "ASDUSize", "IOASize", "COTSize", "CyclicInterval", "T0", "T1", "T2", "T3", "W", "K", "EventQSize", "DEBUG", "AppFirmwareVersion", "Run","EventWithoutTime" };
        private string[] arrASDUSizes;
        private string[] arrIOASizes;
        private string[] arrCOTSizes;
        #endregion Decalration
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
        public static List<string> getASDUsizes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte");
        }
        public static List<string> getIOAsizes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("ThreeByte");
        }
        public static List<string> getCOTsizes()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("TwoByte");
        }
        public IEC104Slave(string s104Name, List<KeyValuePair<string, string>> s104Data, TreeNode tn)
        {
            string strRoutineName = "IEC104Slave";
            try
            {
                SetSupportedASDUSizes();
            SetSupportedIOASizes();
            SetSupportedCOTSizes();
            this.fillOptions();
            try
            {
                sType = (slaveType)Enum.Parse(typeof(slaveType), s104Name);
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
        }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}
        public IEC104Slave(XmlNode sNode, TreeNode tn)
        {
            string strRoutineName = "IEC104Slave";
            try
            {
                SetSupportedASDUSizes();
            SetSupportedIOASizes();
            SetSupportedCOTSizes();
            this.fillOptions();

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
        public void updateAttributes(List<KeyValuePair<string, string>> s104Data)
        {
            string strRoutineName = "updateAttributes";
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

    //Not Used: uciec.lblMain.Text += slaveNum;
}
        private void fillOptions()
        {
        }
        public Control getView(List<string> kpArr)
        {
            //Not Used: if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IEC104_")) return uciec;

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
            get { return "IEC104_" + SlaveNum; }
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

        public string LocalIP
        {
            get { return localIP; }
            set { localIP = value; }
        }

        public string TcpPort
        {
            get { return tcpPort.ToString(); }
            set
            {
                try
                {
                    tcpPort = Int32.Parse(value);
                }
                catch (System.FormatException)
                {
                    tcpPort = -1;
                }
            }
        }

        public string RemoteIP
        {
            get { return remoteIP; }
            set { remoteIP = value; }
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

        public string T0
        {
            get { return t0.ToString(); }
            set { t0 = Int32.Parse(value); }
        }

        public string T1
        {
            get { return t1.ToString(); }
            set { t1 = Int32.Parse(value); }
        }

        public string T2
        {
            get { return t2.ToString(); }
            set { t2 = Int32.Parse(value); }
        }

        public string T3
        {
            get { return t3.ToString(); }
            set { t3 = Int32.Parse(value); }
        }

        public string W
        {
            get { return w.ToString(); }
            set { w = Int32.Parse(value); }
        }

        public string K
        {
            get { return k.ToString(); }
            set { k = Int32.Parse(value); }
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
        public string PortName
        {
            get { return portName; }
            set { portName = value; }
        }
        public string Run
        {
            get { return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
            }
        }

        //Namrata:08/12/2017
        public string SecRemoteIP
        {
            get { if (String.IsNullOrWhiteSpace(secremoteIP)) secremoteIP = "0.0.0.0"; return secremoteIP; }
            set { secremoteIP = value; }
        }
        public string EventWithoutTime
        {
            get { eventWithoutTime = UcGroupIEC104.chkEventWithoutTime.Checked; return (eventWithoutTime == true ? "YES" : "NO"); }
            set
            {
                eventWithoutTime = (value.ToLower() == "yes") ? true : false;
                if (eventWithoutTime == true) UcGroupIEC104.chkEventWithoutTime.Checked = true;
                else UcGroupIEC104.chkEventWithoutTime.Checked = false;
            }
        }
    }
}
