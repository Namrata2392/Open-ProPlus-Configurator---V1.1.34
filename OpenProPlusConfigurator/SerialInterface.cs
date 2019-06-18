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
    * \brief     <b>SerialInterface</b> is a class to store info about serial interface
    * \details   This class stores info related to serial interface. It retrieves/stores 
    * various parameters like Baud Rates, Databits, Stopbits, etc. It also exports the XML node 
    * related to this object.
    * 
    */
    public class SerialInterface
    {
        #region Declaration
        enum serialType
        {
            Port
        };
        //Namrata : 14/09/2017
        private bool enable = false;
        private bool isNodeComment = false;
        private string comment = "";
        private serialType sType = serialType.Port;
        private int portNum = -1;
        private string bRate = "";
        private string dBits = "";
        private string sBits = "";
        private string fControl = "";
        private string pSetting ="";
        private int rtsPreTime;
        private int rtsPostTime;
        private string portName;
        private int tcpPort;
        private string[] arrAttributes = { "PortNum", "BaudRate", "Databits", "Stopbits", "FlowControl", "Parity", "RtsPreTime", "RtsPostTime", "PortName", "TcpPort", "Enable" };
        //private string[] arrAttributes = { "PortNum", "BaudRate", "Databits", "Stopbits", "FlowControl", "Parity", "RtsPreTime", "RtsPostTime", "PortName", "TcpPort" };
        private string[] arrBaudRates;
        private string[] arrDataBits;
        private string[] arrStopBits;
        private string[] arrFlowControls;
        private string[] arrParities;
        #endregion Declaration
        private void SetSupportedBaudRates()
        {
            string strRoutineName = "SetSupportedBaudRates";
            try
            {
                arrBaudRates = Utils.getOpenProPlusHandle().getDataTypeValues("BaudRate").ToArray();
                if (arrBaudRates.Length > 0) bRate = arrBaudRates[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedDataBits()
        {
            string strRoutineName = "SetSupportedDataBits";
            try
            {
                arrDataBits = Utils.getOpenProPlusHandle().getDataTypeValues("Databits").ToArray();
            if (arrDataBits.Length > 0) dBits = arrDataBits[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedStopBits()
        {
            string strRoutineName = "SetSupportedStopBits";
            try
            {
                arrStopBits = Utils.getOpenProPlusHandle().getDataTypeValues("Stopbits").ToArray();
            if (arrStopBits.Length > 0) sBits = arrStopBits[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedFlowControls()
        {
            string strRoutineName = "SetSupportedFlowControls";
            try
            {
                arrFlowControls = Utils.getOpenProPlusHandle().getDataTypeValues("FlowControl").ToArray();
            if (arrFlowControls.Length > 0) fControl = arrFlowControls[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetSupportedParities()
        {
            string strRoutineName = "SetSupportedParities";
            try
            {
                arrParities = Utils.getOpenProPlusHandle().getDataTypeValues("Parity").ToArray();
            if (arrParities.Length > 0) pSetting = arrParities[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<string> getBaudRates()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("BaudRate");
        }
        public static List<string> getDataBits()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("Databits");
        }
        public static List<string> getStopBits()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("Stopbits");
        }
        public static List<string> getFlowControls()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("FlowControl");
        }
        public static List<string> getParities()
        {
            return Utils.getOpenProPlusHandle().getDataTypeValues("Parity");
        }
        public SerialInterface(string siName, List<KeyValuePair<string, string>> siData)
        {
            string strRoutineName = "SerialInterface";
            try
            {
                SetSupportedBaudRates();
            SetSupportedDataBits();
            SetSupportedStopBits();
            SetSupportedFlowControls();
            SetSupportedParities();

            //First set the root element value...
            try
            {
                sType = (serialType)Enum.Parse(typeof(serialType), siName);
            }
            catch (System.ArgumentException)
            {
                Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", siName);
            }

            //Parse n store values...
            if (siData != null && siData.Count > 0)
            {
                foreach (KeyValuePair<string, string> sikp in siData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", sikp.Key, sikp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(sikp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(sikp.Key).SetValue(this, sikp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", sikp.Key, sikp.Value);
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
        public SerialInterface(XmlNode siNode)
        {
            string strRoutineName = "SerialInterface";
            try
            {
                SetSupportedBaudRates();
            SetSupportedDataBits();
            SetSupportedStopBits();
            SetSupportedFlowControls();
            SetSupportedParities();

            //Parse n store values...
            Utils.WriteLine(VerboseLevel.DEBUG, "siNode name: '{0}'", siNode.Name);
            if (siNode.Attributes != null)
            {
                //First set the root element value...
                try
                {
                    sType = (serialType)Enum.Parse(typeof(serialType), siNode.Name);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", siNode.Name);
                }
                    #region Sujit's Code Commented
                    //foreach (XmlAttribute item in siNode.Attributes)
                    //{
                    //    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", item.Name, item.Value);
                    //    try
                    //    {
                    //        this.GetType().GetProperty(item.Name).SetValue(this, item.Value);
                    //    } catch (System.NullReferenceException) {
                    //        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", item.Name, item.Value);
                    //    }
                    //}
                    #endregion Sujit's Code Commented

                    //Namrata: 24/02/2018
                    //It TimeZone attaribute not present in XML
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(Utils.XMLFileName);
                    XmlAttribute attrName = xmlDoc.CreateAttribute("Enable");
                    attrName.Value = "NO"; 
                    bool IsExist = false;

                    arrAttributes.OfType<string>().ToList().ForEach(item =>
                    {
                        if (!siNode.Attributes.OfType<XmlNode>().ToList().Select(x => x.Name).ToList().Contains(item))
                        {
                            IsExist = true;

                        }
                    });
                    if (IsExist)
                    {
                        siNode.Attributes.SetNamedItem(attrName);
                    }
                    siNode.Attributes.OfType<XmlNode>().ToList().ForEach(x =>
                    {
                        if (this.GetType().GetProperty(x.Name) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(x.Name).SetValue(this, x.Value);
                        }
                    });

                }
            else if (siNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = siNode.Value;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> siData)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (siData != null && siData.Count > 0)
            {
                foreach (KeyValuePair<string, string> sikp in siData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", sikp.Key, sikp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(sikp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(sikp.Key).SetValue(this, sikp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", sikp.Key, sikp.Value);
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
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }

        public string PortNum
        {
            get { return portNum.ToString(); }
            set { portNum = Int32.Parse(value); }
        }

        public string BaudRate
        {
            get { return bRate; }
            set {
                bRate = value;
            }
        }

        public string Databits
        {
            get { return dBits; }
            set {
                dBits = value;
            }
        }

        public string Stopbits
        {
            get { return sBits; }
            set {
                sBits = value;
            }
        }

        public string FlowControl
        {
            get { return fControl; }
            set {
                fControl = value;
            }
        }

        public string Parity
        {
            get { return pSetting; }
            set {
                pSetting = value;
            }
        }

        public string RtsPreTime
        {
            get { return rtsPreTime.ToString(); }
            set { rtsPreTime = Int32.Parse(value); }
        }

        public string RtsPostTime
        {
            get { return rtsPostTime.ToString(); }
            set { rtsPostTime = Int32.Parse(value); }
        }

        public string PortName
        {
            get { return portName; }
            set { portName = value; }
        }

        public string TcpPort
        {
            get { return tcpPort.ToString(); }
            set { tcpPort = Int32.Parse(value); }
        }
        //Namrata : 14/09/2017
        public string Enable
        {
            get { return (enable == true ? "YES" : "NO"); }
            set { enable = (value.ToLower() == "yes") ? true : false; }
        }
    }
}
