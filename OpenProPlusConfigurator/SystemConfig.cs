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
    * \brief     <b>SystemConfig</b> is a class to store info about system configuration
    * \details   This class stores info related to openpro+ configuration. It retrieves/stores 
    * various parameters like Log server, NTP server, etc. It also exports the XML node 
    * related to this object.
    * 
    */
    public class SystemConfig
    {
        #region Declaration
        enum redundancyMode
        {
            //Namrata: 24/5/2017
            None,
            Primary,
            Secondary
        };
        enum timeSyncSource
        {
            //Namrata:1/7/2017
            NTP,
            None,
            SlaveNum1,
            SlaveNum2,
            SlaveNum3,
            SlaveNum4,
            IRIGB,
        };
        enum logProtocol
        {
            TCP
        };
        private bool isNodeComment = false;
        private string comment = "";
        private string rnName = "SystemConfig";
        private redundancyMode rMode = redundancyMode.Secondary;
        //Namrata:1/07/2017
        private timeSyncSource tSyncSrc = timeSyncSource.NTP;
        private string redundantSystemIP = null;
        //Namrata:1/7/2017
        private string ntpServer1 = null;
        private string ntpServer2 = null;
        private bool logLocal = true;
        private bool logRemote = false;
        private string logServerIP = "";
        private int logServerPort = -1;
        private logProtocol lProtocol = logProtocol.TCP;
        private bool edit = false;
        private bool ntpUsed = false;
        private int maxDataPoint = 0;
        List<string> TimeZoneList = new List<string>();
        //Namrata: 31/08/2017
        private string tZone = "Asia/Dubai";
        ucSystemConfig ucsc = new ucSystemConfig();
        //Namarta:21/04/2018
        private bool dbSync = true;
        private string[] arrAttributes = { "RedundancyMode", "RedundantSystemIP", "TimesyncSource","DBSync", "NTPServer1", "NTPServer2", "LOGlocal", "LOGremote", "LOGServerIP", "LOGServerPort", "LOGProtocol", "NTPUsed", "MaxDataPoint", "TimeZone" };
        #endregion Declaration
        public SystemConfig()
        {
            string strRoutineName = "SystemConfig";
            try
            {
                
                ucsc.CmbTimeZoneTextUpdate += new System.EventHandler(this.CmbTimeZone_TextUpdate);
                ucsc.cmbRedundancyModeSelectedValueChanged += new System.EventHandler(this.cmbRedundancyMode_SelectedValueChanged);
                ucsc.cmbTimeSyncSourceSelectedIndexChanged += new System.EventHandler(this.cmbTimeSyncSource_SelectedIndexChanged);
                this.fillOptions();
                loadDefaults();
                //Namrata: 08/09/2017
                //ucsc.cmbRedundancyModeSelectedIndexChanged += new System.EventHandler(this.cmbRedundancyMode_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CmbTimeZone_TextUpdate(object sender, EventArgs e)
        {
            string strRoutineName = "SystemConfig";
            try
            {
                if (!TimeZoneList.Where(x => x.ToLower().StartsWith(ucsc.CmbTimeZone.Text)).Any())
                {
                    MessageBox.Show("Please Select valid timezone", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Namrata: 06/09/2017
        //Create DI in Virtual Mode
        private void cmbRedundancyMode_SelectedValueChanged(object sender, EventArgs e)
        {
            string strRoutineName = "cmbRedundancyMode_SelectedValueChanged";
            try
            {
                Utils.RedundancyMode = ucsc.cmbRedundancyMode.Text;
                if (ucsc.cmbRedundancyMode.Text == "None")
                {
                    ucsc.txtRedundantSystemIP.Enabled = false;
                }
                else
                {
                    ucsc.txtRedundantSystemIP.Enabled = true;
                }

                //Namrata:24/01/2018
                if (ucsc.cmbRedundancyMode.GetItemText(ucsc.cmbRedundancyMode.SelectedItem) == "Primary")
                {
                    if (Utils.DummyDI.Select(x => x.ResponseType).ToList().Contains("PrimaryDevice"))
                    {

                    }
                    else
                    {
                        Utils.CreateDI4SystemPortForPrimaryDevice();
                        Utils.CreateDI4SystemPortForSecondaryDevice();
                    }

                }
                if (ucsc.cmbRedundancyMode.GetItemText(ucsc.cmbRedundancyMode.SelectedItem) == "Secondary")
                {
                    if (Utils.DummyDI.Select(x => x.ResponseType).ToList().Contains("SecDevice"))
                    {

                    }
                    else
                    {
                        Utils.CreateDI4SystemPortForPrimaryDevice();
                        Utils.CreateDI4SystemPortForSecondaryDevice();
                    }
                }
                if (ucsc.cmbRedundancyMode.GetItemText(ucsc.cmbRedundancyMode.SelectedItem) == "None")
                {
                    if ((Utils.DummyDI.Select(x => x.ResponseType).ToList().Contains("PrimaryDevice")) && (Utils.DummyDI.Select(x => x.ResponseType).ToList().Contains("SecDevice")))
                    {
                        Utils.RemoveDI4IEDystemPortForPrimaryDevice("SystemConfig", 0, 0, Int32.Parse((Globals.DINo).ToString()), "PrimaryDevice");
                        Utils.RemoveDI4IEDystemPortForSecondaryDevice("SystemConfig", 0, 0, Int32.Parse((Globals.DINo).ToString()), "SecDevice");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbRedundancyMode_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void cmbTimeSyncSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "cmbTimeSyncSource_SelectedIndexChanged";
            try
            {
                Console.WriteLine("*** Caught TSS change!!! text: {0}", ucsc.cmbTimeSyncSource.GetItemText(ucsc.cmbTimeSyncSource.SelectedItem));
                if (ucsc.cmbTimeSyncSource.GetItemText(ucsc.cmbTimeSyncSource.SelectedItem) == "NTP")
                {
                    ucsc.grpNTP.Enabled = true;
                }
                else
                {
                    //ucsc.grpNTP.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "loadDefaults";
            try
            {
                ucsc.txtLogServerPort.Text = Globals.LOG_SERVER_PORT.ToString();
                ucsc.cmbLogProtocol.FindStringExact("TCP");
                ucsc.txtMaxDataPoints.Text = Globals.MAX_DATA_POINTS.ToString();
                //Namrata: 31/08/2017
                ucsc.CmbTimeZone.SelectedIndex = ucsc.CmbTimeZone.FindStringExact("Asia/Kolkata");
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fillOptions()
        {
            string strRoutineName = "fillOptions";
            try
            {
                //Namrata: 31/08/2017
                //Fill TimeZone ..
                #region Fill TimeZone
                ucsc.CmbTimeZone.Items.Clear();
                string[] lineOfContents = File.ReadAllLines(Globals.ZONE_RESOURCES_PATH + Globals.TIME_ZONE_LIST);
                foreach (var line in lineOfContents)
                {
                    string[] tokens = line.Split('	');     //Split by \t From txt File .
                    ucsc.CmbTimeZone.Items.Add(tokens[2]);  // get the 3rd element (the 1st item is always item 0)
                }
                ucsc.CmbTimeZone.SelectedIndex = 0;
                //Namrata: 18/1/2017
                TimeZoneList = ucsc.CmbTimeZone.Items.OfType<string>().Select(x => x).ToList();

                #endregion  Fill TimeZone
                #region Fill Redundancy Mode...
                //Fill Redundancy Mode...
                ucsc.cmbRedundancyMode.Items.Clear();
                foreach (redundancyMode rm in Enum.GetValues(typeof(redundancyMode)))
                {
                    ucsc.cmbRedundancyMode.Items.Add(rm.ToString());
                }
                ucsc.cmbRedundancyMode.SelectedIndex = 0;
                #endregion  Fill Redundancy Mode...
                //Namrata:1/07/2017
                #region Fill Time Sync
                //Fill Time Sync
                ucsc.cmbTimeSyncSource.Items.Clear();
                foreach (timeSyncSource ts in Enum.GetValues(typeof(timeSyncSource)))
                {
                    ucsc.cmbTimeSyncSource.Items.Add(ts.ToString());
                }
                ucsc.cmbTimeSyncSource.SelectedIndex = 0;
                #endregion Fill Time Sync
                #region Fill Log Protocol...
                //Fill Log Protocol...
                ucsc.cmbLogProtocol.Items.Clear();
                foreach (logProtocol lp in Enum.GetValues(typeof(logProtocol)))
                {
                    ucsc.cmbLogProtocol.Items.Add(lp.ToString());
                }
                ucsc.cmbLogProtocol.SelectedIndex = 0;
                #endregion Fill Log Protocol...
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fillTSS()
        {
            string strRoutineName = "fillTSS";
            try
            {
                string oldTSS = "";
                int oldTSSidx = -1;
                //Try to retain the old selection...
                if (ucsc.cmbTimeSyncSource.SelectedIndex >= 0) oldTSS = ucsc.cmbTimeSyncSource.GetItemText(ucsc.cmbTimeSyncSource.SelectedItem);
                else oldTSS = "NTP";
                foreach (IEC104Slave iec104 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC104Group().getIEC104Slaves())
                {
                    ucsc.cmbTimeSyncSource.Items.Add("IEC104Slave_" + iec104.SlaveNum);
                }
                oldTSSidx = ucsc.cmbTimeSyncSource.FindStringExact(oldTSS);
                if (oldTSSidx >= 0)
                    ucsc.cmbTimeSyncSource.SelectedIndex = oldTSSidx;
                else
                    ucsc.cmbTimeSyncSource.SelectedIndex = 0;//NTP...
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            fillTSS();//Check if any IEC104 slave info updated...
            return ucsc;
        }
        public XmlNode exportXMLnode()
        {
            //Namrata : 06/09/2017
            // Disable event of RedundancyMode .
            //ucsc.cmbRedundancyModeSelectedIndexChanged -= new System.EventHandler(this.cmbRedundancyMode_SelectedIndexChanged);
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
        public void parseSystemConfigNode(XmlNode mNode)
        {
            string strRoutineName = "parseSystemConfigNode";
            try
            {
                //Namrata : 06/09/2017
                //Disable event of RedundancyMode .
                //Namrata: 25/1/2018
                ucsc.cmbRedundancyModeSelectedValueChanged -= new System.EventHandler(this.cmbRedundancyMode_SelectedValueChanged);
                ucsc.cmbRedundancyModeSelectedIndexChanged -= new System.EventHandler(this.cmbRedundancyMode_SelectedIndexChanged);
                //Parse n store values...
                Utils.WriteLine(VerboseLevel.DEBUG, "ptDetNode name: '{0}'", mNode.Name);
                rnName = mNode.Name;
                if (mNode.Attributes != null)
                {
                    //Namrata: 24/02/2018
                    //It TimeZone attaribute not present in XML
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(Utils.XMLFileName);
                    XmlAttribute attrName = xmlDoc.CreateAttribute("TimeZone");
                    attrName.Value = "Asia/Kolkata";
                    //Namrata:21/04/2018
                    XmlAttribute DBSync = xmlDoc.CreateAttribute("DBSync");
                    DBSync.Value = "DISABLE";
                    bool IsExist = false;
                    
                    arrAttributes.OfType<string>().ToList().ForEach(item =>
                    {
                        if (!mNode.Attributes.OfType<XmlNode>().ToList().Select(x => x.Name).ToList().Contains(item))
                        {
                            IsExist = true;
                        }
                    });
                    if (IsExist)
                    {
                        mNode.Attributes.SetNamedItem(attrName);
                        mNode.Attributes.SetNamedItem(DBSync);
                    } 
                    mNode.Attributes.OfType<XmlNode>().ToList().ForEach(x =>
                    {
                        //Namrata: 06/03/2018
                        if (mNode.Attributes[1].Name == "ResponseType")
                        {
                            if (mNode.Attributes[1].Value == "")
                            {
                                mNode.Attributes[1].Value = "NTP";
                                tSyncSrc = timeSyncSource.NTP;
                            }
                        }
                        else
                        {
                            if (this.GetType().GetProperty(x.Name) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(x.Name).SetValue(this, x.Value);
                            }
                        }
                    });
                }
                else if (mNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = mNode.Value;
                }
                //Namrata: 25/1/2018
                ucsc.cmbRedundancyModeSelectedValueChanged += new System.EventHandler(this.cmbRedundancyMode_SelectedValueChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void parseSystemConfigNodeForprimarySecDevice(XmlNode mNode)
        {
            string strRoutineName = "parseSystemConfigNodeForprimarySecDevice";
            try
            {
                //Namrata : 06/09/2017
                //Disable event of RedundancyMode .
                ucsc.cmbRedundancyModeSelectedValueChanged -= new System.EventHandler(this.cmbRedundancyMode_SelectedValueChanged);
                ucsc.cmbRedundancyModeSelectedIndexChanged -= new System.EventHandler(this.cmbRedundancyMode_SelectedIndexChanged);
                rnName = mNode.Name;

                Utils.RedundancyMode = ucsc.cmbRedundancyMode.Text;
                if (mNode.Attributes != null)
                {
                    #region Sujit code Commented
                    //foreach (XmlAttribute item in mNode.Attributes)
                    //{
                    //    try
                    //    {
                    //        this.GetType().GetProperty(item.Name).SetValue(this, item.Value);
                    //    }
                    //    catch (System.NullReferenceException)
                    //    {
                    //        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", item.Name, item.Value);
                    //    }
                    //}
                    #endregion Sujit code Commented

                    //Namrata: 24/02/2018
                    //It TimeZone attaribute not present in XML
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(Utils.XMLFileName);
                    XmlAttribute attrName = xmlDoc.CreateAttribute("TimeZone");
                    attrName.Value = "Asia/Kolkata";
                    bool IsExist = false;

                    #region Primary ,Secondary Device
                    if (mNode.Attributes[0].Value == "Primary")
                    {
                        if (Utils.DiListForVirualDIImport.Select(x => x.ResponseType).ToList().Contains("PrimaryDevice"))
                        {

                        }
                        else
                        {
                            Utils.CreateDI4SystemPortForPrimaryDeviceAfterXMLImport1();
                            Utils.CreateDI4SystemPortForSecondaryDeviceAfterXMLImport1();
                        }
                    }
                    #endregion Primary ,Secondary Device

                    arrAttributes.OfType<string>().ToList().ForEach(item =>
                    {
                        if (!mNode.Attributes.OfType<XmlNode>().ToList().Select(x => x.Name).ToList().Contains(item))
                        {
                            IsExist = true;
                        }
                    });
                    if (IsExist)
                    {
                        mNode.Attributes.SetNamedItem(attrName);
                    }
                    mNode.Attributes.OfType<XmlNode>().ToList().ForEach(x =>
                    {
                        if (this.GetType().GetProperty(x.Name) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(x.Name).SetValue(this, x.Value);
                        }
                    });
                }
                else if (mNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = mNode.Value;
                }
                //Namrata: 25/1/2018
                ucsc.cmbRedundancyModeSelectedValueChanged += new System.EventHandler(this.cmbRedundancyMode_SelectedValueChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public string RedundancyMode
        {
            get
            {
                rMode = (redundancyMode)Enum.Parse(typeof(redundancyMode), ucsc.cmbRedundancyMode.GetItemText(ucsc.cmbRedundancyMode.SelectedItem));
                return rMode.ToString();
            }
            set
            {
                try
                {
                    rMode = (redundancyMode)Enum.Parse(typeof(redundancyMode), value);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", value);
                }
                ucsc.cmbRedundancyMode.SelectedIndex = ucsc.cmbRedundancyMode.FindStringExact(value);
            }
        }
        public string RedundantSystemIP
        {
            get
            {
                if (ucsc.txtRedundantSystemIP.Text == "")
                {
                    redundantSystemIP = "0.0.0.0";
                }
                else
                {
                    redundantSystemIP = ucsc.txtRedundantSystemIP.Text;
                }
                return redundantSystemIP;
            }
            set { redundantSystemIP = ucsc.txtRedundantSystemIP.Text = value; }
        }

        public string TimesyncSource
        {
            #region Namrata Commented Sujit's Code 
            //Namrata:1/7/2017
            //Namrata Commented Sujit's Code
            //get { return tSyncSrc = ucsc.cmbTimeSyncSource.GetItemText(ucsc.cmbTimeSyncSource.SelectedItem); }
            //set
            //{
            //    tSyncSrc = value;
            //    ucsc.cmbTimeSyncSource.SelectedIndex = ucsc.cmbTimeSyncSource.FindStringExact(value);
            //}
            #endregion Namrata Commented Sujit's Code 

            //Namrata:1/7/2017
            get
            {
                tSyncSrc = (timeSyncSource)Enum.Parse(typeof(timeSyncSource), ucsc.cmbTimeSyncSource.GetItemText(ucsc.cmbTimeSyncSource.SelectedItem));
                return tSyncSrc.ToString();
            }
            set
            {
                try
                {
                    tSyncSrc = (timeSyncSource)Enum.Parse(typeof(timeSyncSource), value);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", value);
                }
                ucsc.cmbTimeSyncSource.SelectedIndex = ucsc.cmbTimeSyncSource.FindStringExact(value);
            }
        }

        public string NTPServer1
        {
            get { if (String.IsNullOrWhiteSpace(ucsc.txtNTPServer1.Text)) ucsc.txtNTPServer1.Text = "0.0.0.0"; return ntpServer1 = ucsc.txtNTPServer1.Text; }
            set { ntpServer1 = ucsc.txtNTPServer1.Text = value; }
        }

        public string NTPServer2
        {
            get { if (String.IsNullOrWhiteSpace(ucsc.txtNTPServer2.Text)) ucsc.txtNTPServer2.Text = "0.0.0.0"; return ntpServer2 = ucsc.txtNTPServer2.Text; }
            set { ntpServer2 = ucsc.txtNTPServer2.Text = value; }
        }

        public string LOGlocal
        {
            get { logLocal = ucsc.chkLogLocal.Checked; return (logLocal == true ? "ENABLE" : "DISABLE"); }
            set
            {
                logLocal = (value.ToLower() == "enable") ? true : false;
                if (logLocal == true) ucsc.chkLogLocal.Checked = true;
                else ucsc.chkLogLocal.Checked = false;
            }
        }
        public string DBSync
        {
            get { dbSync = ucsc.chkDBSync.Checked; return (dbSync == true ? "ENABLE" : "DISABLE"); }
            set
            {
                dbSync = (value.ToLower() == "enable") ? true : false;
                if (dbSync == true) ucsc.chkDBSync.Checked = true;
                else ucsc.chkDBSync.Checked = false;
            }
        }
        public string LOGremote
        {
            get { logRemote = ucsc.chkLogRemote.Checked; return (logRemote == true ? "ENABLE" : "DISABLE"); }
            set
            {
                logRemote = (value.ToLower() == "enable") ? true : false;
                if (logRemote == true) ucsc.chkLogRemote.Checked = true;
                else ucsc.chkLogRemote.Checked = false;
            }
        }

        public string LOGServerIP
        {
            get { if (String.IsNullOrWhiteSpace(ucsc.txtLogServerIP.Text)) ucsc.txtLogServerIP.Text = "0.0.0.0"; return logServerIP = ucsc.txtLogServerIP.Text; }
            set { logServerIP = ucsc.txtLogServerIP.Text = value; }
        }

        public string LOGServerPort
        {
            get
            {
                try
                {
                    logServerPort = Int32.Parse(ucsc.txtLogServerPort.Text);
                }
                catch (System.FormatException)
                {
                    logServerPort = -1;
                    ucsc.txtLogServerPort.Text = logServerPort.ToString();
                }
                return logServerPort.ToString();
            }
            set { logServerPort = Int32.Parse(value); ucsc.txtLogServerPort.Text = value; }
        }

        public string LOGProtocol
        {
            get
            {
                lProtocol = (logProtocol)Enum.Parse(typeof(logProtocol), ucsc.cmbLogProtocol.GetItemText(ucsc.cmbLogProtocol.SelectedItem));
                return lProtocol.ToString();
            }
            set
            {
                try
                {
                    lProtocol = (logProtocol)Enum.Parse(typeof(logProtocol), value);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", value);
                }
                ucsc.cmbLogProtocol.SelectedIndex = ucsc.cmbLogProtocol.FindStringExact(value);
            }
        }
        public string Edit
        {
            get { edit = ucsc.chkEdit.Checked; return (edit == true ? "YES" : "NO"); }
            set
            {
                edit = (value.ToLower() == "yes") ? true : false;
                if (edit == true) ucsc.chkEdit.Checked = true;
                else ucsc.chkEdit.Checked = false;
            }
        }
        public string NTPUsed
        {
            get { ntpUsed = ucsc.chkNTP.Checked; return (ntpUsed == true ? "YES" : "NO"); }
            set
            {
                ntpUsed = (value.ToLower() == "yes") ? true : false;
                if (ntpUsed == true) ucsc.chkNTP.Checked = true;
                else ucsc.chkNTP.Checked = false;
            }
        }
        public string MaxDataPoint
        {
            get
            {
                try
                {
                    maxDataPoint = Int32.Parse(ucsc.txtMaxDataPoints.Text);
                }
                catch (System.FormatException)
                {
                    maxDataPoint = 0;
                    ucsc.txtMaxDataPoints.Text = maxDataPoint.ToString();
                }
                return maxDataPoint.ToString();
            }
            set { maxDataPoint = Int32.Parse(value); ucsc.txtMaxDataPoints.Text = value; }
        }

        //Namrata: 31/08/2017
        public string TimeZone
        {
            get { return tZone = ucsc.CmbTimeZone.GetItemText(ucsc.CmbTimeZone.SelectedItem); }
            set
            {
                tZone = value;
                ucsc.CmbTimeZone.SelectedIndex = ucsc.CmbTimeZone.FindStringExact(value);
            }
        }
    }
}
