using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;

namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>IED</b> is a class to store data for all data points and there corresponding mapping.
    * \details   This class stores all data points and there corresponding mapping info. The mapping info
    * per slave per data point is stored. It also stores parameters related to IED like Clock Sync Interval,
     * Refresh Interval, GiTime, etc. It also exports the XML node related to this object.
    * 
    */
    public class IED
    {
        #region Declarations
        enum iedType
        {
            IED
        };

        private bool isNodeComment = false;
        private string comment = "";
        private iedType iType = iedType.IED;
        public int unitID = -1;
        private int asduAddr = -1;
        private string device = "";
        private string remoteIP = "";
        private int tcpPort = 0;
        private int retries = 3;
        private int timeoutms = 100;
        private string descr = "";
      
        private bool dr = false;
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private string linkaddresssize = "";
        private AIMap aimap = null;// = new AIConfiguration();
        private AIConfiguration aicNode = null;// = new AIConfiguration();
        private AOConfiguration aocNode = null;// = new AIConfiguration();
        private DIConfiguration dicNode = null;// = new DIConfiguration();
        private DOConfiguration docNode = null;// = new DOConfiguration();
        private ENConfiguration encNode = null;// = new ENConfiguration();
        private RCBConfiguration RcbNode = null;// = new ENConfiguration();
        private SlaveMapping smNode = null;// = new SlaveMapping(iec104Grp, ref dicNode);

        private string asSize = "";
        private string ioaSz = "";
        private string cSize = "";
        ucIED ucied = new ucIED();
        ucMaster61850Server UcMaster61850Server = new ucMaster61850Server();
        private TreeNode IEDTreeNode;
      
        private string[] arrAttributes;// = { "UnitID", "ASDUAddr", "Device", "Retries", "TimeOutMS", "Description", "DR" };
        private string[] arrASDUSizes;
        private string[] arrIOASizes;
        private string[] arrCOTSizes;
        private string[] arrLinkAddressSizes;
        OpenProPlus_Config opcHandle = null;//new OpenProPlus_Config();
        private string icdFilePath = "";
        #endregion Declarations

        #region Supported Attributes
        private void SetSupportedAttributes()
        {
            string strRoutineName = "SetSupportedAttributes";
            try
            {
                if (masterType == MasterTypes.IEC103)
                    arrAttributes = new string[] { "UnitID", "ASDUAddr", "Device", "Retries", "TimeOutMS", "Description", "DR" };
                else if (masterType == MasterTypes.IEC101)
                    arrAttributes = new string[] { "UnitID", "ASDUAddr", "Device", "Retries", "TimeOutMS", "LinkAddressSize", "ASDUSize", "IOASize", "COTSize", "Description", };
                else if (masterType == MasterTypes.MODBUS)
                    arrAttributes = new string[] { "UnitID", "Device", "RemoteIP", "TCPPort", "Retries", "TimeOutMS", "Description" };
                else if (masterType == MasterTypes.Virtual)
                    arrAttributes = new string[] { "UnitID", "Device", "Description" };
                //Namarta:11/7/2017
                else if (masterType == MasterTypes.ADR)
                    arrAttributes = new string[] { "UnitID", "Device", "Description", "Retries", "TimeOutMS" };
                else if (masterType == MasterTypes.IEC61850Client)
                    arrAttributes = new string[] { "UnitID", "Device", "RemoteIP", "TCPPort", "Retries", "TimeOutMS", "Description", "SCLName" };
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
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
        //Namrata:7/7/2017
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
        #endregion Supported Attributes

        #region Virtual Master..
        public IED(int unit, string devicename, TreeNode tn, MasterTypes mType, int mNo)
        {
            string strRoutineName = "IED";
            try
            {
                masterType = mType;
                masterNo = mNo;
                SetSupportedAttributes();//IMP: Call only after master types are set...
                SetSupportedASDUSizes();
                SetSupportedIOASizes();
                SetSupportedCOTSizes();
                SetSupportedLinkAddressSizes();
                if (tn != null) IEDTreeNode = tn;
                aicNode = new AIConfiguration(mType, mNo, unit);
                aocNode = new AOConfiguration(mType, mNo, unit);
                dicNode = new DIConfiguration(mType, mNo, unit);
                docNode = new DOConfiguration(mType, mNo, unit);
                encNode = new ENConfiguration(mType, mNo, unit);
                smNode = new SlaveMapping(aicNode, aocNode,/*ref */dicNode, docNode, encNode);
                addListHeaders();
                UnitID = unit.ToString();
                Device = devicename;
                if (tn != null) tn.Text = "IED " + this.Description;
                if (tn != null) tn.Nodes.Add("AI", "AI", "AI", "AI");
                aicNode.parseAICNode(null, false);
                if (tn != null) tn.Nodes.Add("AO", "AO", "AO", "AO");
                aocNode.parseAICNode(null, false);
                if (tn != null) tn.Nodes.Add("DI", "DI", "DI", "DI");
                dicNode.parseDICNode(null, false);
                if (tn != null) tn.Nodes.Add("DO", "DO", "DO", "DO");
                docNode.parseDOCNode(null, false);
                if (tn != null) tn.Nodes.Add("EN", "EN", "EN", "EN");
                encNode.parseENCNode(null, false);
                refreshList();
                ucied.lblIED.Text = "IED Details (Unit: " + unitID.ToString() + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion Virtual Master..

        #region IEC61850Client Master...
        public IED(string iedName, string devicename, List<KeyValuePair<string, string>> iedData, TreeNode tn, MasterTypes mType, int mNo)
        {
            string strRoutineName = "IED";
            try
            {
                masterType = mType;
            masterNo = mNo;
            SetSupportedAttributes();//IMP: Call only after master types are set...
            SetSupportedASDUSizes();
            SetSupportedIOASizes();
            SetSupportedCOTSizes();
            SetSupportedLinkAddressSizes();
            if (tn != null) IEDTreeNode = tn;
            addListHeaders();
            Device = devicename;
            try
            {
                iType = (iedType)Enum.Parse(typeof(iedType), iedName);
            }
            catch (System.ArgumentException)
            {
                Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", iedName);
            }

            if (iedData != null && iedData.Count > 0)  //Parse n store values...
            {
                foreach (KeyValuePair<string, string> iedkp in iedData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", iedkp.Key, iedkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(iedkp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(iedkp.Key).SetValue(this, iedkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", iedkp.Key, iedkp.Value);
                        }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
            if (tn != null) tn.Text = "IED " + this.Description;
            RcbNode = new RCBConfiguration(mType, mNo, Int32.Parse(this.UnitID));  
            aicNode = new AIConfiguration(mType, mNo, Int32.Parse(this.UnitID));
            aocNode = new AOConfiguration(mType, mNo, Int32.Parse(this.UnitID));
            dicNode = new DIConfiguration(mType, mNo, Int32.Parse(this.UnitID));
            docNode = new DOConfiguration(mType, mNo, Int32.Parse(this.UnitID));
            encNode = new ENConfiguration(mType, mNo, Int32.Parse(this.UnitID));
            smNode = new SlaveMapping(aicNode, null, /*ref */dicNode, docNode, encNode);
            if (tn != null) tn.Nodes.Add("RCB", "RCB", "RCB", "RCB");
            RcbNode.parseAICNode(null, false);
            if (tn != null) tn.Nodes.Add("AI", "AI", "AI", "AI");
            aicNode.parseAICNode(null, false);
            if (tn != null) tn.Nodes.Add("AO", "AO", "AO", "AO");
            aocNode.parseAICNode(null, false);
            if (tn != null) tn.Nodes.Add("DI", "DI", "DI", "DI");
            dicNode.parseDICNode(null, false);
            if (tn != null) tn.Nodes.Add("DO", "DO", "DO", "DO");
            docNode.parseDOCNode(null, false);
            if (tn != null) tn.Nodes.Add("EN", "EN", "EN", "EN");
            encNode.parseENCNode(null, false);
            refreshList();
            ucied.lblIED.Text = "IED Details (Unit: " + unitID.ToString() + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion IEC61850Client Master...

        public IED(string iedName, List<KeyValuePair<string, string>> iedData, TreeNode tn, MasterTypes mType, int mNo)
        {
            string strRoutineName = "IED";
            try
            {
                masterType = mType;
                masterNo = mNo;
                SetSupportedAttributes();//IMP: Call only after master types are set...
                SetSupportedASDUSizes();
                SetSupportedIOASizes();
                SetSupportedCOTSizes();
                SetSupportedLinkAddressSizes();
                if (tn != null) IEDTreeNode = tn;
                addListHeaders();
                try
                {
                    iType = (iedType)Enum.Parse(typeof(iedType), iedName);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", iedName);
                }
                if (iedData != null && iedData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> iedkp in iedData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", iedkp.Key, iedkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(iedkp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(iedkp.Key).SetValue(this, iedkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", iedkp.Key, iedkp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                if (tn != null) tn.Text = "IED " + this.Description;
                aicNode = new AIConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                aocNode = new AOConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                dicNode = new DIConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                docNode = new DOConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                encNode = new ENConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                smNode = new SlaveMapping(aicNode, aocNode,dicNode, docNode, encNode);
                if (tn != null) tn.Nodes.Add("AI", "AI", "AI", "AI");
                aicNode.parseAICNode(null, false);
                if (tn != null) tn.Nodes.Add("AO", "AO", "AO", "AO");
                aocNode.parseAICNode(null, false);
                if (tn != null) tn.Nodes.Add("DI", "DI", "DI", "DI");
                dicNode.parseDICNode(null, false);
                if (tn != null) tn.Nodes.Add("DO", "DO", "DO", "DO");
                docNode.parseDOCNode(null, false);
                if (tn != null) tn.Nodes.Add("EN", "EN", "EN", "EN");
                encNode.parseENCNode(null, false);
                refreshList();
                ucied.lblIED.Text = "IED Details (Unit: " + unitID.ToString() + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public IED(XmlNode iNode, TreeNode tn, MasterTypes mType, int mNo, bool imported)
       {
            string strRoutineName = "IED";
            try
            {
                masterType = mType;
                masterNo = mNo;
                SetSupportedAttributes();
                if (tn != null) IEDTreeNode = tn;
                addListHeaders();
                Utils.WriteLine(VerboseLevel.DEBUG, "iNode name: '{0}'", iNode.Name);
                if (iNode.Attributes != null)
                {
                    try
                    {
                        iType = (iedType)Enum.Parse(typeof(iedType), iNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", iNode.Name);
                    }
                    foreach (XmlAttribute item in iNode.Attributes)
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
                else if (iNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = iNode.Value;
                }
                if (imported)//Generate a new unique id...
                {
                    if (masterType == MasterTypes.ADR)//ADR
                    {
                        //if (Utils.ADRMasteriedList.Select(x => x.unitID).ToList().Contains(this.unitID))
                        //{
                        //    this.UnitID = (Globals.getADRIEDNo(masterNo) + 1).ToString();
                        //}
                        //else
                        //{
                        //    this.UnitID = (Globals.getADRIEDNo(masterNo) + 1).ToString();
                        //}
                    }
                    else if (masterType == MasterTypes.IEC101)//IEC101
                    {
                        //Namrata: 20/11/2017
                        if (Utils.IEC101MasteriedList.Select(x => x.unitID).ToList().Contains(this.unitID))
                        {
                            this.UnitID = (Globals.getIEC101IEDNo(masterNo) + 1).ToString();
                            //this.Description = "IEC101_" + (Globals.MasterNo + 1).ToString();
                        }
                        else
                        {
                            this.UnitID = (Globals.getIEC101IEDNo(masterNo)).ToString();
                        }
                    }
                    else if (masterType == MasterTypes.IEC103)
                    {//IEC103
                     //Namrata: 25/11/2017
                        if (Utils.IEC103MasteriedList.Select(x => x.unitID).ToList().Contains(this.unitID))
                        {
                            this.UnitID = (Globals.getIEC103IEDNo(masterNo) + 1).ToString();

                        }
                        else
                        {
                            this.UnitID = (Globals.getIEC103IEDNo(masterNo)).ToString();
                        }
                    }
                    else if (masterType == MasterTypes.MODBUS)//MODBUS
                    {
                        //Namrata: 25/11/2017
                        if (Utils.MODBUSMasteriedList.Select(x => x.unitID).ToList().Contains(this.unitID))
                        {
                            this.UnitID = (Globals.getMODBUSIEDNo(masterNo) + 1).ToString();
                        }
                        else
                        {
                            this.UnitID = (Globals.getMODBUSIEDNo(masterNo)).ToString();
                        }
                    }
                    else if (masterType == MasterTypes.IEC61850Client)//MODBUS
                    {
                        //Namrata: 25/11/2017
                        if (Utils.IEC61850MasteriedList.Select(x => x.unitID).ToList().Contains(this.unitID))
                        {
                            this.UnitID = (Globals.get61850IEDNo(masterNo) + 1).ToString();
                        }
                        else
                        {
                            this.UnitID = (Globals.get61850IEDNo(masterNo)).ToString();
                        }
                    }
                }
                if (tn != null) tn.Text = "IED " + this.Description;
                RcbNode = new RCBConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                aicNode = new AIConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                aocNode = new AOConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                dicNode = new DIConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                docNode = new DOConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                encNode = new ENConfiguration(mType, mNo, Int32.Parse(this.UnitID));
                smNode = new SlaveMapping(aicNode, aocNode, /*ref */dicNode, docNode, encNode);
                foreach (XmlNode node in iNode)
                {
                    //Namrata: 14/9/2017
                    //For IEC61850
                    if (node.Name == "ControlBlock")
                    {
                        TreeNode tmp = tn.Nodes.Add("RCB", "RCB", "RCB", "RCB");
                        RcbNode.parseAICNode(node, imported);
                    }

                    else if (node.Name == "AIConfiguration")
                    {
                        TreeNode tmp = tn.Nodes.Add("AI", "AI", "AI", "AI");
                        aicNode.parseAICNode(node, imported);
                    }
                    else if (node.Name == "AOConfiguration")
                    {
                        TreeNode tmp = tn.Nodes.Add("AO", "AO", "AO", "AO");
                        aocNode.parseAICNode(node, imported);
                    }
                    else if (node.Name == "DIConfiguration")
                    {
                        TreeNode tmp = tn.Nodes.Add("DI", "DI", "DI", "DI");
                        dicNode.parseDICNode(node, imported);
                    }
                    else if (node.Name == "DOConfiguration")
                    {
                        TreeNode tmp = tn.Nodes.Add("DO", "DO", "DO", "DO");
                        docNode.parseDOCNode(node, imported);
                    }
                    else if (node.Name == "ENConfiguration")
                    {
                        TreeNode tmp = tn.Nodes.Add("EN", "EN", "EN", "EN");
                        encNode.parseENCNode(node, imported);
                    }
                    else if (node.Name == "SlaveMapping")
                    {
                        smNode.parseSMNode(node);
                    }
                }
                refreshList();
                ucied.lblIED.Text = "IED Details (Unit: " + unitID.ToString() + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Specially used by Virtual Master during loadXML, use to fill info...
        public void parseIEDNode(XmlNode iNode)
        {
            string strRoutineName = "parseIEDNode";
            try
            {
                if (aicNode == null) aicNode = new AIConfiguration(masterType, masterNo, Int32.Parse(this.UnitID));
                if (aocNode == null) aocNode = new AOConfiguration(masterType, masterNo, Int32.Parse(this.UnitID));
                if (dicNode == null) dicNode = new DIConfiguration(masterType, masterNo, Int32.Parse(this.UnitID));
                if (docNode == null) docNode = new DOConfiguration(masterType, masterNo, Int32.Parse(this.UnitID));
                if (encNode == null) encNode = new ENConfiguration(masterType, masterNo, Int32.Parse(this.UnitID));
                if (smNode == null) smNode = new SlaveMapping(aicNode, aocNode, /*ref */dicNode, docNode, encNode);
                Utils.WriteLine(VerboseLevel.DEBUG, "iNode name: '{0}'", iNode.Name);
                if (iNode.Attributes != null)
                {
                    try
                    {
                        iType = (iedType)Enum.Parse(typeof(iedType), iNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", iNode.Name);
                    }
                    foreach (XmlAttribute item in iNode.Attributes)
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
                else if (iNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = iNode.Value;
                }
                if (IEDTreeNode != null) IEDTreeNode.Text = "IED " + this.Description;
                foreach (XmlNode node in iNode)
                {
                    if (node.Name == "AIConfiguration")
                    {
                        aicNode.parseAICNode(node, false);
                    }
                    else if (node.Name == "AOConfiguration")
                    {
                        aocNode.parseAICNode(node, false);
                    }
                    else if (node.Name == "DIConfiguration")
                    {
                        dicNode.parseDICNode(node, false);
                    }
                    else if (node.Name == "DOConfiguration")
                    {
                        docNode.parseDOCNode(node, false);
                    }
                    else if (node.Name == "ENConfiguration")
                    {
                        encNode.parseENCNode(node, false);
                    }
                    else if (node.Name == "SlaveMapping")
                    {
                        smNode.parseSMNode(node);
                    }
                }
                refreshList();
                ucied.lblIED.Text = "IED Details (Unit: " + unitID.ToString() + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> iedData)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (iedData != null && iedData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> iedkp in iedData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", iedkp.Key, iedkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(iedkp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(iedkp.Key).SetValue(this, iedkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", iedkp.Key, iedkp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }

                ucied.lblIED.Text = "IED Details (Unit: " + unitID.ToString() + ")";
                if (IEDTreeNode != null) IEDTreeNode.Text = "IED " + this.Description;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void changeCLASequence(int oCLANo, int nCLANo)
        {
            if (dicNode != null) dicNode.changeCLASequence(oCLANo, nCLANo);
        }
        public void changeProfileSequence(int oProfileNo, int nProfileNo)
        {
            if (dicNode != null) dicNode.changeProfileSequence(oProfileNo, nProfileNo);
        }
        public void changeMDSequence(int oMDNo, int nMDNo)
        {
            if (aicNode != null) aicNode.changeMDSequence(oMDNo, nMDNo);
            if (dicNode != null) dicNode.changeMDSequence(oMDNo, nMDNo);
        }
        public void changeDPSequence(int oDPNo, int nDPNo)
        {
            if (aicNode != null) aicNode.changeDPSequence(oDPNo, nDPNo);
        }
        public void changeDDSequence(int oDDNo, int nDDNo)
        {
            if (dicNode != null) dicNode.changeDDSequence(oDDNo, nDDNo);
        }
        public void regenerateAISequence()
        {
            if (aicNode != null) aicNode.regenerateAISequence();
        }
        public void regenerateAOSequence()
        {
            if (aocNode != null) aocNode.regenerateAOSequence();
        }
        public void regenerateDISequence()
        {
            if (dicNode != null) dicNode.regenerateDISequence();
        }
        public void regenerateDOSequence()
        {
            if (docNode != null) docNode.regenerateDOSequence();
        }
        public void regenerateENSequence()
        {
            if (encNode != null) encNode.regenerateENSequence();
        }
        private void addListHeaders()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                ucied.lvIEDDetails.Columns.Add("No.", 50, HorizontalAlignment.Left);
                ucied.lvIEDDetails.Columns.Add("Type", 62, HorizontalAlignment.Left);
                ucied.lvIEDDetails.Columns.Add("Count", 60, HorizontalAlignment.Left);
                ucied.lvIEDDetails.Columns.Add("Map", 70, HorizontalAlignment.Left);
                ucied.lvIEDDetails.Columns.Add("MapCount", 70, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "refreshList";
            try
            {
                int rowCnt = 0;
                ucied.lvIEDDetails.Items.Clear();
                string[] row1 = { "1", "AI", aicNode.getCount().ToString(), "AI Map", aicNode.getAIMapCount().ToString() };
                ListViewItem lvItem1 = new ListViewItem(row1);
                if (rowCnt++ % 2 == 0) lvItem1.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucied.lvIEDDetails.Items.Add(lvItem1);
                //Namrata: 21/11/2017
                string[] row5 = { "2", "AO", aocNode.getCount().ToString(), "AO Map", aocNode.getENMapCount().ToString() };
                ListViewItem lvItem5 = new ListViewItem(row5);
                if (rowCnt++ % 2 == 0) lvItem5.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucied.lvIEDDetails.Items.Add(lvItem5);
                string[] row2 = { "3", "DI", dicNode.getCount().ToString(), "DI Map", dicNode.getDIMapCount().ToString() };
                ListViewItem lvItem2 = new ListViewItem(row2);
                if (rowCnt++ % 2 == 0) lvItem2.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucied.lvIEDDetails.Items.Add(lvItem2);
                string[] row3 = { "4", "DO", docNode.getCount().ToString(), "DO Map", docNode.getDOMapCount().ToString() };
                ListViewItem lvItem3 = new ListViewItem(row3);
                if (rowCnt++ % 2 == 0) lvItem3.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucied.lvIEDDetails.Items.Add(lvItem3);
                string[] row4 = { "5", "EN", encNode.getCount().ToString(), "EN Map", encNode.getENMapCount().ToString() };
                ListViewItem lvItem4 = new ListViewItem(row4);
                if (rowCnt++ % 2 == 0) lvItem4.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucied.lvIEDDetails.Items.Add(lvItem4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IED_")) { refreshList(); return ucied; }
            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("RCB_"))
            {
                return RcbNode.getView(kpArr);
            }
            if (kpArr.ElementAt(0).Contains("AI_"))
            {
                return aicNode.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("AO_"))
            {
                return aocNode.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("DI_"))
            {
                return dicNode.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("DO_"))
            {
                return docNode.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("EN_"))
            {
                return encNode.getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "***** IED: View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }

            return null;
        }
        public TreeNode getTreeNode()
        {
            return IEDTreeNode;
        }
        public XmlNode exportXMLnode1()
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
            rootNode = xmlDoc.CreateElement(iType.ToString());
            xmlDoc.AppendChild(rootNode);
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }
            XmlNode importrcbICNode = rootNode.OwnerDocument.ImportNode(RcbNode.exportXMLnode(), true);
            rootNode.AppendChild(importrcbICNode);
            XmlNode importAICNode = rootNode.OwnerDocument.ImportNode(aicNode.exportXMLnode(), true);
            rootNode.AppendChild(importAICNode);
            XmlNode importAOCNode = rootNode.OwnerDocument.ImportNode(aocNode.exportXMLnode(), true);
            rootNode.AppendChild(importAOCNode);
            XmlNode importDICNode = rootNode.OwnerDocument.ImportNode(dicNode.exportXMLnode(), true);
            rootNode.AppendChild(importDICNode);
            XmlNode importDOCNode = rootNode.OwnerDocument.ImportNode(docNode.exportXMLnode(), true);
            rootNode.AppendChild(importDOCNode);
            XmlNode importENCNode = rootNode.OwnerDocument.ImportNode(encNode.exportXMLnode(), true);
            rootNode.AppendChild(importENCNode);
            XmlNode importSMNode = rootNode.OwnerDocument.ImportNode(smNode.exportXMLnode(), true);
            rootNode.AppendChild(importSMNode);
            return rootNode;
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
            rootNode = xmlDoc.CreateElement(iType.ToString());
            xmlDoc.AppendChild(rootNode);
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }
                XmlNode importAICNode = rootNode.OwnerDocument.ImportNode(aicNode.exportXMLnode(), true);
                rootNode.AppendChild(importAICNode);
                XmlNode importAOCNode = rootNode.OwnerDocument.ImportNode(aocNode.exportXMLnode(), true);
               rootNode.AppendChild(importAOCNode);
               XmlNode importDICNode = rootNode.OwnerDocument.ImportNode(dicNode.exportXMLnode(), true);
                rootNode.AppendChild(importDICNode);
                XmlNode importDOCNode = rootNode.OwnerDocument.ImportNode(docNode.exportXMLnode(), true);
                rootNode.AppendChild(importDOCNode);
                XmlNode importENCNode = rootNode.OwnerDocument.ImportNode(encNode.exportXMLnode(), true);
                rootNode.AppendChild(importENCNode);
                XmlNode importSMNode = rootNode.OwnerDocument.ImportNode(smNode.exportXMLnode(), true);
                rootNode.AppendChild(importSMNode);
            return rootNode;
        }
        public XmlNode exportXMLMODBUSnode()
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

            rootNode = xmlDoc.CreateElement(iType.ToString());
            xmlDoc.AppendChild(rootNode);

            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }

            //XmlNode importrcbICNode = rootNode.OwnerDocument.ImportNode(RcbNode.exportXMLnode(), true);
            //rootNode.AppendChild(importrcbICNode);

            XmlNode importAICNode = rootNode.OwnerDocument.ImportNode(aicNode.exportXMLnode(), true);
            rootNode.AppendChild(importAICNode);

            XmlNode importAOCNode = rootNode.OwnerDocument.ImportNode(aocNode.exportXMLnode(), true);
            rootNode.AppendChild(importAOCNode);

            XmlNode importDICNode = rootNode.OwnerDocument.ImportNode(dicNode.exportXMLnode(), true);
            rootNode.AppendChild(importDICNode);

            XmlNode importDOCNode = rootNode.OwnerDocument.ImportNode(docNode.exportXMLnode(), true);
            rootNode.AppendChild(importDOCNode);

            XmlNode importENCNode = rootNode.OwnerDocument.ImportNode(encNode.exportXMLnode(), true);
            rootNode.AppendChild(importENCNode);

            XmlNode importSMNode = rootNode.OwnerDocument.ImportNode(smNode.exportXMLnode(), true);
            rootNode.AppendChild(importSMNode);
            return rootNode;
        }
        public XmlNode exportIEDnode()
        {
            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            XmlNode rootNode = null;
            XmlNode rootIEDNode = null;
            rootNode = xmlDoc.CreateElement("IEDexport");
            xmlDoc.AppendChild(rootNode);
            string rattr = "MasterType";
            XmlAttribute rattrName = xmlDoc.CreateAttribute(rattr);
            rattrName.Value = masterType.ToString();
            rootNode.Attributes.Append(rattrName);
            rootIEDNode = xmlDoc.CreateElement(iType.ToString());
            rootNode.AppendChild(rootIEDNode);
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootIEDNode.Attributes.Append(attrName);
            }
            XmlNode importAICNode = rootIEDNode.OwnerDocument.ImportNode(aicNode.exportXMLnode(), true);
            rootIEDNode.AppendChild(importAICNode);
            XmlNode importAOCNode = rootIEDNode.OwnerDocument.ImportNode(aocNode.exportXMLnode(), true);
            rootIEDNode.AppendChild(importAOCNode);
            XmlNode importDICNode = rootIEDNode.OwnerDocument.ImportNode(dicNode.exportXMLnode(), true);
            rootIEDNode.AppendChild(importDICNode);
            XmlNode importDOCNode = rootIEDNode.OwnerDocument.ImportNode(docNode.exportXMLnode(), true);
            rootIEDNode.AppendChild(importDOCNode);
            XmlNode importENCNode = rootIEDNode.OwnerDocument.ImportNode(encNode.exportXMLnode(), true);
            rootIEDNode.AppendChild(importENCNode);
            return rootNode;
        }
        public string exportIED()
        {
            XmlNode xmlNode = exportIEDnode();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 2; //default is 1.
            xmlNode.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();
            return stringWriter.ToString();
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";

            if (element == "AI")
            {
                iniData = aicNode.exportINI(slaveNum, slaveID, element, ref ctr);
                Console.WriteLine("***** AI ctr = {0}", ctr);
            }
            //Ajay: 28/12/2018
            if (element == "AO")
            {
                iniData = aocNode.exportINI(slaveNum, slaveID, element, ref ctr);
                Console.WriteLine("***** AO ctr = {0}", ctr);
            }
            else if (element == "DI")
            {
                iniData = dicNode.exportINI(slaveNum, slaveID, element, ref ctr);
            }
            else if (element == "DO")
            {
                iniData = docNode.exportINI(slaveNum, slaveID, element, ref ctr);
            }
            else if (element == "EN")
            {
                iniData = encNode.exportINI(slaveNum, slaveID, element, ref ctr);
                Console.WriteLine("***** EN ctr = {0}", ctr);
            }
            else if (element == "DeadBandAI")
            {
                //Ajay: 28/12/2018
                //iniData = aicNode.exportINI(slaveNum, slaveID, element, ref ctr);
                iniData = aicNode.exportINI_DeadBandAI(slaveNum, slaveID, element, ref ctr);
            }
            else if (element == "DeadBandEN")
            {
                //Ajay: 28/12/2018
                //iniData = encNode.exportINI(slaveNum, slaveID, element, ref ctr);
                iniData = encNode.exportINI_DeadBandEN(slaveNum, slaveID, element, ref ctr);
            }

            return iniData;
        }
        //Namrata: 24/11/2017
        public AOConfiguration getAOConfiguration()
        {
            return aocNode;
        }
        public AIConfiguration getAIConfiguration()
        {
            return aicNode;
        }
        public DIConfiguration getDIConfiguration()
        {
            return dicNode;
        }
        public DOConfiguration getDOConfiguration()
        {
            return docNode;
        }
        public ENConfiguration getENConfiguration()
        {
            return encNode;
        }
        public string getIEDID
        {
            get { return "IED_" + masterType.ToString() + "_" + masterNo + "_" + UnitID; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string UnitID
        {
            get { unitID = Int32.Parse(ucied.txtUnitID.Text); return unitID.ToString(); }
            set
            {
                unitID = Int32.Parse(value); ucied.txtUnitID.Text = value;
                if (masterType == MasterTypes.IEC103)//IEC103
                    Globals.setIEC103IEDNo(masterNo, Int32.Parse(value));
                //Namrata:3/6/2017
                else if (masterType == MasterTypes.IEC101)//IEC101
                    Globals.setIEC101IEDNo(masterNo, Int32.Parse(value));

                else if (masterType == MasterTypes.MODBUS)//MODBUS
                    Globals.setMODBUSIEDNo(masterNo, Int32.Parse(value));

                else if (masterType == MasterTypes.IEC61850Client)//IEC61850Client
                    Globals.set61850IEDNo(masterNo, Int32.Parse(value));
            }
        }
        public string ASDUAddr
        {
            get
            {
                try
                {
                    asduAddr = Int32.Parse(ucied.txtASDUaddress.Text);
                }
                catch (System.FormatException)
                {
                    asduAddr = -1;
                    ucied.txtASDUaddress.Text = asduAddr.ToString();
                }

                return asduAddr.ToString();
            }
            set
            {
                asduAddr = Int32.Parse(value); ucied.txtASDUaddress.Text = value;
                if (masterType == MasterTypes.IEC103)//IEC103
                    Globals.setIEC103ASDUAddress(masterNo, Int32.Parse(value));
                //Namrata:7/7/2017
                if (masterType == MasterTypes.IEC101)//IEC101
                    Globals.setIEC101ASDUAddress(masterNo, Int32.Parse(value));
                //Namarta:3/6/2017
                //else if (masterType == MasterTypes.ADR)//ADR
                //    Globals.setADRASDUAddress(masterNo, Int32.Parse(value));
            }
        }
        public string Device
        {
            get { return device = ucied.txtDevice.Text; }
            set { device = ucied.txtDevice.Text = value; }
        }
        public string RemoteIP
        {
            get { remoteIP = ucied.txtRemoteIP.Text; if (String.IsNullOrWhiteSpace(remoteIP)) remoteIP = "0.0.0.0"; return remoteIP; }
            set { remoteIP = ucied.txtRemoteIP.Text = value; }
        }
        public string TCPPort
        {
            get
            {
                try
                {
                    tcpPort = Int32.Parse(ucied.txtTCPPort.Text);
                }
                catch (System.FormatException)
                {
                    tcpPort = 0;
                }
                return tcpPort.ToString();
            }
            set
            {
                try
                {
                    tcpPort = Int32.Parse(value);
                }
                catch (System.FormatException)
                {
                    tcpPort = 0;
                }
                ucied.txtTCPPort.Text = tcpPort.ToString();
            }
        }
        public string Retries
        {
            get
            {
                try
                {
                    retries = Int32.Parse(ucied.txtRetries.Text);
                }
                catch (System.FormatException)
                {
                }
                return retries.ToString();
            }
            set { retries = Int32.Parse(value); ucied.txtRetries.Text = value; }
        }
        public string TimeOutMS
        {
            get
            {
                try
                {
                    timeoutms = Int32.Parse(ucied.txtTimeOut.Text);
                }
                catch (System.FormatException)
                {
                }
                return timeoutms.ToString();
            }
            set { timeoutms = Int32.Parse(value); ucied.txtTimeOut.Text = value; }
        }
        public string Description
        {
            get { return descr = ucied.txtDescription.Text; }
            set { descr = ucied.txtDescription.Text = value; }
        }
        public string DR
        {
            get { dr = ucied.chkDR.Checked; return (dr == true ? "ENABLE" : "DISABLE"); }
            set
            {
                dr = (value.ToLower() == "enable") ? true : false;
                if (dr == true) ucied.chkDR.Checked = true;
                else ucied.chkDR.Checked = false;
            }
        }
        public string LinkAddressSize
        {
            get { return linkaddresssize; }
            set { linkaddresssize = value; }
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

        public string SCLName
        {
            get
            {
                return icdFilePath;
            }
            set
            {
                icdFilePath = value;
            }
        }
    }
}
