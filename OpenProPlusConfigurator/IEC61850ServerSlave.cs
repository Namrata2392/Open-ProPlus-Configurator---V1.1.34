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
    public class IEC61850ServerSlave
    {
        #region Declaration
        enum slaveType
        {
            IEC61850Server
        };
        enum protocolType
        {
            RTU,
            ASCII,
            TCP
        };
        enum edition
        {
          Ed1,
          Ed2
        };
        private edition edi = edition.Ed1;
        private bool isNodeComment = false;
        private string comment = "";
        private slaveType sType = slaveType.IEC61850Server;
        private protocolType pt = protocolType.RTU;
        private int slaveNum = -1;
        private int portNum = -1;
        private int tcpPort;
        private string remoteIP = "";
        private int unitID = -1;
        private int eventQSize;
        private string appFirmwareVersion;
        private int debug;
        //Namrata: 10/5/2017 
        private string localIP = "";
        //Namrata:24/10/2017
        private string Iedname = "";
        private string manufactuer = "";
        private string ldopp = "";
        private string ldname = "";
        private bool run = true;
        private string portName;
        private string[] arrAttributes = { "SlaveNum", "Edition", "TcpPort", "RemoteIP", "LocalIP","Manufacturer","IEDName", "LDevice", "AppFirmwareVersion","DEBUG","Run" };
        #endregion Declaration
        public static List<string> getProtocolTypes()
        {
            List<string> lpt = new List<string>();
            foreach (protocolType ptv in Enum.GetValues(typeof(protocolType)))
            {
                lpt.Add(ptv.ToString());
            }
            return lpt;
        }
        public IEC61850ServerSlave(string mbsName, List<KeyValuePair<string, string>> mbsData, TreeNode tn)
        {
            string strRoutineName = "IEC61850ServerSlave";
            try
            {
            try
            {
                sType = (slaveType)Enum.Parse(typeof(slaveType), mbsName);
            }
            catch (System.ArgumentException)
            {
                Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", mbsName);
            }
            if (mbsData != null && mbsData.Count > 0)
            {
                foreach (KeyValuePair<string, string> mbskp in mbsData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", mbskp.Key, mbskp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(mbskp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(mbskp.Key).SetValue(this, mbskp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", mbskp.Key, mbskp.Value);
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
        public IEC61850ServerSlave(XmlNode sNode, TreeNode tn)
        {
            string strRoutineName = "IEC61850ServerSlave";
            try
            {
            Utils.WriteLine(VerboseLevel.DEBUG, "sNode name: '{0}'", sNode.Name);
            if (sNode.Attributes != null)
            {
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
        }
        public void updateAttributes(List<KeyValuePair<string, string>> mbsData)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (mbsData != null && mbsData.Count > 0)
            {
                foreach (KeyValuePair<string, string> mbskp in mbsData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", mbskp.Key, mbskp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(mbskp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(mbskp.Key).SetValue(this, mbskp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value:{1}", mbskp.Key, mbskp.Value);
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
        public string getSlaveID
        {
            get { return "IEC61850ServerSlave_" + SlaveNum; }
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
        public string ProtocolType
        {
            get { return pt.ToString(); }
            set
            {
                try
                {
                    pt = (protocolType)Enum.Parse(typeof(protocolType), value);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", value);
                }
            }
        }
        public string Edition
        {
            get { return edi.ToString(); }
            set
            {
                try
                {
                    edi = (edition)Enum.Parse(typeof(edition), value);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", value);
                }
            }
        }
        public string PortNum
        {
            get { return portNum.ToString(); }
            set
            {
                try
                {
                    portNum = Int32.Parse(value);
                }
                catch (System.FormatException)
                {
                    portNum = -1;
                }
            }
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
            //get { return remoteIP; }
            //set { remoteIP = value; }
            get { if (String.IsNullOrWhiteSpace(remoteIP)) remoteIP = "0.0.0.0"; return remoteIP; }
            set { remoteIP = value; }
        }
        public string UnitID
        {
            get { return unitID.ToString(); }
            set
            {
                unitID = Int32.Parse(value);
                /*
                if (slaveType == SlaveTypes.IEC104)//IEC103
                    Globals.setIEC103IEDNo(slaveNo, Int32.Parse(value));
                else if (slaveType == SlaveTypes.MODBUSSLAVE)//MODBUS
                    Globals.setMODBUSIEDNo(slaveNo, Int32.Parse(value))
                        */
            }
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
        //Namrata:10/05/2017
        public string LocalIP
        {
            get { return localIP; }
            set { localIP = value; }
        }
        public string IEDName
        {
            get { return Iedname; }
            set { Iedname = value; }
        }
        public string Manufacturer
        {
            get { return manufactuer; }
            set { manufactuer = value; }
        }
        public string LDevice
        {
            get { return ldname; }
            set { ldname = value; }
        }
        public string LDOPP
        {
            get { return ldopp; }
            set { ldopp = value; }
        }
        public string Run
        {
            get { return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
            }
        }
        public string PortName
        {
            get { return portName; }
            set { portName = value; }
        }
    }
}
