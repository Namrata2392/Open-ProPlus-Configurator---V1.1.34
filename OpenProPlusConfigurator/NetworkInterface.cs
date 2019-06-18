using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Reflection;
namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>NetworkInterface</b> is a class to store info about network interface
    * \details   This class stores info related to network interface. It retrieves/stores 
    * various parameters like Address Type, Connection Type, etc. It also exports the XML node 
    * related to this object.
    * 
    */
    public class NetworkInterface
    {
        #region Declaration
        enum networkType
        {
            Lan,
            NetworkInterface  //Ajay: 17/07/2018 Runtime Error on App load fixed---NetworkInterface Added
        }
        enum addressType
        {
            STATIC,
            DHCP
        };
        enum connectionType
        {
            Wired,
            Wireless,
            Bond
        };
        //rata:1/7/2017
        enum primaryDevice
        {
            eth0,
            eth1
        };
        enum bondMode
        {
            ActiveBackup
        };
        private bool isNodeComment = false;
        private string comment = "";
        private networkType nType = networkType.Lan;
        private addressType aType = addressType.DHCP;
        private string ip = null;
        private string gateway = null;
        private string subnetMask = null;
        private int portNum = -1;
        private bool edit = false;
        private string portName = null;
        private bool enable = false;
        //Namrata:1/7/2017
        private primaryDevice pType = primaryDevice.eth0;
        private connectionType cType = connectionType.Wired;
        private bondMode bMode = bondMode.ActiveBackup;
        private int bondPort0 = 0;
        private int bondPort1 = 0;
        private int linkUpDelay = 100;
        private int linkDownDelay = 100;
        //Namrata:30/5/2017
        //private string primaryDevice = null;
        private string virtualIP = "";
        private string[] arrAttributes = { "AddressType", "IP", "VirtualIP", "GateWay", "SubNetMask", "PortNum", "PortName", "PrimaryDevice", "Enable", "ConnectionType", "Mode", "BondPort0", "BondPort1", "LinkUpDelay", "LinkDownDelay" };
        #endregion Declaration
        public static List<string> getAddressTypes()
        {
            List<string> lat = new List<string>();
            foreach (addressType at in Enum.GetValues(typeof(addressType)))
            {
                lat.Add(at.ToString());
            }
            return lat;
        }

        public static List<string> getConnectionTypes()
        {
            List<string> lct = new List<string>();
            foreach (connectionType ct in Enum.GetValues(typeof(connectionType)))
            {
                lct.Add(ct.ToString());
            }
            return lct;
        }

        //Namarta:1/7/2017
        public static List<string> getPortName()
        {
            List<string> lct1 = new List<string>();
            foreach (primaryDevice pn in Enum.GetValues(typeof(primaryDevice)))
            {
                lct1.Add(pn.ToString());
            }
            return lct1;
        }

        public NetworkInterface(string niName, List<KeyValuePair<string, string>> niData)
        {
            string strRoutineName = "NetworkInterface";
            try
            {
                //First set the root element value...
                try
                {
                    //Ajay: 17/07/2018 Runtime Error on App load fixed
                    //nType = (networkType)Enum.Parse(typeof(networkType), niName);
                    Enum.TryParse(niName, out nType);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", niName);
                }

                //Parse n store values...
                if (niData != null && niData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> nikp in niData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", nikp.Key, nikp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(nikp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(nikp.Key).SetValue(this, nikp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. KeyValuePair and class fields mismatch!!!");
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

        public NetworkInterface(XmlNode niNode)
        {
            string strRoutineName = "NetworkInterface";
            try
            {
                if (niNode.Attributes != null)
                {
                    try
                    {
                        nType = (networkType)Enum.Parse(typeof(networkType), niNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", niNode.Name);
                    }

                    foreach (XmlAttribute item in niNode.Attributes)
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
                else if (niNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = niNode.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void updateAttributes(List<KeyValuePair<string, string>> niData)
        {
            string strRoutineName = "txtTcpPortKeyPress";
            try
            {
                if (niData != null && niData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> nikp in niData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", nikp.Key, nikp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(nikp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(nikp.Key).SetValue(this, nikp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. KeyValuePair and class fields mismatch!!!");
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

            rootNode = xmlDoc.CreateElement(nType.ToString());
            xmlDoc.AppendChild(rootNode);

            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }

            return rootNode;
        }

        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string AddressType
        {
            get { return aType.ToString(); }
            set
            {
                try
                {
                    aType = (addressType)Enum.Parse(typeof(addressType), value);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", value);
                }
            }
        }
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }
        public string GateWay
        {
            get { return gateway; }
            set { gateway = value; }
        }
        public string SubNetMask
        {
            get { return subnetMask; }
            set { subnetMask = value; }
        }
        public string PortNum
        {
            get { return portNum.ToString(); }
            set { portNum = Int32.Parse(value); }
        }
        public string Edit
        {
            get { return (edit == true ? "YES" : "NO"); }
            set { edit = (value.ToLower() == "yes") ? true : false; }
        }
        public string PortName
        {
            get { return portName; }
            set { portName = value; }
        }
        //Namrata:30/5.2017
        public string PrimaryDevice
        {
            //get { return primaryDevice; }
            //set { primaryDevice = value; }
            get { return pType.ToString(); }
            set
            {
                try
                {
                    pType = (primaryDevice)Enum.Parse(typeof(primaryDevice), value);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", value);
                }
            }
        }
        public string Enable
        {
            get { return (enable == true ? "YES" : "NO"); }
            set { enable = (value.ToLower() == "yes") ? true : false; }
        }
        public string ConnectionType
        {
            get { return cType.ToString(); }
            set
            {
                try
                {
                    cType = (connectionType)Enum.Parse(typeof(connectionType), value);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", value);
                }
            }
        }
        public string VirtualIP
        {
            get { if (String.IsNullOrWhiteSpace(virtualIP)) virtualIP = "0.0.0.0"; return virtualIP; }
            set { virtualIP = value; }
        }
        public string Mode
        {
            get { return bMode.ToString(); }
            set
            {
                try
                {
                    bMode = (bondMode)Enum.Parse(typeof(bondMode), value);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", value);
                }
            }
        }

        public string BondPort0
        {
            get { return bondPort0.ToString(); }
            set { bondPort0 = Int32.Parse(value); }
        }

        public string BondPort1
        {
            get { return bondPort1.ToString(); }
            set { bondPort1 = Int32.Parse(value); }
        }

        public string LinkUpDelay
        {
            get { return linkUpDelay.ToString(); }
            set { linkUpDelay = Int32.Parse(value); }
        }

        public string LinkDownDelay
        {
            get { return linkDownDelay.ToString(); }
            set { linkDownDelay = Int32.Parse(value); }
        }
    }
}
