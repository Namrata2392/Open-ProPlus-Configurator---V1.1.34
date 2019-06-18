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
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml.XPath;
using System.Globalization;
using System.Reflection;
using System.Configuration;
using System.IO.Compression;
namespace OpenProPlusConfigurator
{
    public class IEC61850ServerMaster
    {
        #region Declarations
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        enum masterType
        {
            IEC61850Client
        };
        enum edition
        {
            Ed1,
            Ed2
        };
        private bool isNodeComment = false;
        private string comment = "";
        string RCBDatasetName = "";
        private masterType mType = masterType.IEC61850Client;
        private int masterNum = -1;
        private int portNum = -1;
        private bool run = true;
        private int debug = -1;
        private int pollingIntervalmSec = -1;
        private int portTimesyncSec = -1;
        private int refreshInterval = 120;
        private string appFirmwareVersion;
        private string desc = "";
        //Namrata:15/6/2017
        private int SelectedIndex;
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        List<IED> iedList = new List<IED>();
        ucMaster61850Server ucmod = new ucMaster61850Server();
        private TreeNode IEC6160ServerMasterTreeNode;//Need To change
        private string[] arrAttributes = { "Edition", "MasterNum", "PortNum", "Run", "DEBUG", "PollingIntervalmSec", "PortTimesyncSec", "RefreshInterval", "AppFirmwareVersion", "Description" };
        #endregion Declarations

        #region Import ICD Declaration
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        private OpenFileDialog ofdXMLFile = new OpenFileDialog();
        
        //Namrata: 30/10/2017
        private Configure configuration;
        private string currentIedName = "";
        private string currentReport = "";
        string IEDNodeName = "";
        string RCBAddress = "";
        string RCBBufferTime = "";
        string RCBConfigRevision = "";
        string RCBDSAddress = "";
        string RCBIntegrityPeriod = "";
        string RCBTriggerOptionNum = "";
        public bool MappingChanged
        {
            get; set;
        }
        int gridItemsAll = 0;
        int NodeCount = 0;

        //Namrata: 11/09/2017
        //Fill All Configuration Data
        public DataGridView DgvAllData = new DataGridView();
        public DataTable DtAllConfigData = new DataTable();
        DataRow DrAllConfigData;

        //Fill OnRequest Configuration Data
        public DataGridView DgvOnRequestData = new DataGridView();

        //Namrata: 11/09/2017
        //Fill IEDName
        public DataGridView DgvIEDName = new DataGridView();
        public DataTable IEDData = new DataTable();
        DataRow IEDName;
        string IEDnames = "";
      
        //Namrata: 15/09/2017
        //Fill ResponseType
        public DataGridView DgvResponseType = new DataGridView();
        public DataTable DtResponseType = new DataTable();
        DataRow DrResponseType;
      
        //Namrata: 11/09/2017
        //Fill RCB
        public DataGridView DgvRCBData = new DataGridView();
        public DataTable RCBData = new DataTable();
        DataRow RCBRow;

        //Namrata: 11/09/2017
        //Fill Dataset in RCB Combobox
        public DataGridView DgvRCBDS = new DataGridView();
        public DataTable DtRCBDS = new DataTable();
        DataRow DRRCBDS;
        #endregion Import ICD Declaration
        public static List<string> getEditionTypes()
        {
            List<string> led = new List<string>();
            foreach (edition edi in Enum.GetValues(typeof(edition)))
            {
                led.Add(edi.ToString());
            }
            return led;
        }
        public IEC61850ServerMaster(string mbName, List<KeyValuePair<string, string>> mbData, TreeNode tn)
        {
            string strRoutineName = "IEC61850ServerMaster";
            try
            {
                //Namrata: 30/10/2017
                MappingChanged = false;
                ucmod.dataGridView1.Visible = false;
                ucmod.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucmod.lvIEDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
                ucmod.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucmod.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
                ucmod.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
                ucmod.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucmod.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucmod.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucmod.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucmod.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucmod.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucmod.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
                ucmod.PboxImportICDClick += new System.EventHandler(this.PboxImportICD_Click);
                ucmod.ucMaster61850ServerLoad += new System.EventHandler(this.ucMaster61850Server_Load);
                addListHeaders();
                this.fillOptions();
                //First set the root element value...
                try
                {
                    mType = (masterType)Enum.Parse(typeof(masterType), mbName);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", mbName);
                }
                //Parse n store values...
                if (mbData != null && mbData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> mbkp in mbData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", mbkp.Key, mbkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(mbkp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(mbkp.Key).SetValue(this, mbkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", mbkp.Key, mbkp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                if (tn != null) tn.Nodes.Clear();
                IEC6160ServerMasterTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
                //Treenode Name
                if (tn != null) tn.Text = "IEC61850 " + this.Description;
                refreshList();
                ucmod.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public IEC61850ServerMaster(XmlNode mNode, TreeNode tn)
        {
            string strRoutineName = "IEC61850ServerMaster";
            try
            {
                ucmod.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucmod.lvIEDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
                ucmod.PboxImportICDClick += new System.EventHandler(this.PboxImportICD_Click);
                ucmod.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucmod.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
                ucmod.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
                ucmod.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucmod.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucmod.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucmod.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucmod.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucmod.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucmod.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
                ucmod.ucMaster61850ServerLoad += new System.EventHandler(this.ucMaster61850Server_Load);
                addListHeaders();
                this.fillOptions();
                #region UpdateValues
                if (mNode.Attributes != null)
                {
                    try
                    {
                        mType = (masterType)Enum.Parse(typeof(masterType), mNode.Name);
                    }
                    catch (System.ArgumentException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", mNode.Name);
                    }
                    foreach (XmlAttribute item in mNode.Attributes)
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
                #endregion UpdateValues
                else if (mNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = mNode.Value;
                }
                if (tn != null) tn.Nodes.Clear();
                IEC6160ServerMasterTreeNode = tn;
                if (tn != null) tn.Text = "IEC61850 " + this.Description;
                foreach (XmlNode node in mNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    TreeNode tmp = tn.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                    iedList.Add(new IED(node, tmp, MasterTypes.IEC61850Client, Int32.Parse(MasterNum), false));
                    //For Importing IEC61850Client Data
                    IEDNodeName = "IEC61850 " + mNode.Attributes[9].Value.ToString();
                    UnitIDdummy = iedList[iedList.Count - 1].unitID.ToString();
                    strICDPath = iedList[iedList.Count - 1].SCLName.ToString();
                    ConcatKeyValue = Utils.MasterNum + "_" + UnitIDdummy;

                    if (!string.IsNullOrEmpty(strICDPath))
                    {

                        string FullICDFilePath = Utils.XMLFolderPath + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath) + "\\" + "Protocol" + "\\" + "IEC61850Client" + @"\" + strICDPath;
                        if (FileExists(FullICDFilePath))
                        {
                            //string FullICDPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration" + @"\" + strICDPath;
                            ImportICDData(FullICDFilePath);
                        }
                        else if (!FileExists(Utils.ICDFiles + @"\" + strICDPath))
                        {
                            //MessageBox.Show("ICD not exist", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            //return;
                        }
                    }
                    else
                    {
                        //MessageBox.Show("ICD not exist", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        //return;
                    }

                    //if (!string.IsNullOrEmpty(strICDPath))
                    //{

                    //    string a = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration" + @"\" + strICDPath;
                    //    if (FileExists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration" + @"\" + strICDPath))
                    //    {
                    //        string FullICDPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration" + @"\" + strICDPath;
                    //        ImportICDData(FullICDPath);
                    //    }
                    //    else if (!FileExists(Utils.ICDFiles + @"\" + strICDPath))
                    //    {
                    //        MessageBox.Show("ICD not exist", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    //MessageBox.Show("ICD not exist", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    //    //return;
                    //}
                }
                refreshList();
                ucmod.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> mbData)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (mbData != null && mbData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> mbkp in mbData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", mbkp.Key, mbkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(mbkp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(mbkp.Key).SetValue(this, mbkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", mbkp.Key, mbkp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                ucmod.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
                if (IEC6160ServerMasterTreeNode != null) IEC6160ServerMasterTreeNode.Text = "IEC61850Server " + this.Description;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearData()
        {
            Utils.Filename = "";  //Namrata:14/10/2017
            ucmod.CmbDeviceName.DataSource = null;

            #region OnRequest Data
            DgvOnRequestData.DataSource = null;
            DgvOnRequestData.Rows.Clear();
            DgvOnRequestData.Visible = false;
            #endregion OnRequest Data

            #region All Configuration Data
            //Namrata: 07/09/2017
            DgvAllData.DataSource = null;
            DgvAllData.Rows.Clear();
            DgvAllData.Visible = false;
            DtAllConfigData.Rows.Clear(); DtAllConfigData.Columns.Clear();
            DataColumn dccolumn = DtAllConfigData.Columns.Add("ObjectReferrence", typeof(string));
            DtAllConfigData.Columns.Add("Node", typeof(string));
            DtAllConfigData.Columns.Add("FC", typeof(string));
            #endregion All Configuration Data

            #region ResopnseType
            //Namrata: 15/09/2017
            DgvResponseType.DataSource = null;
            DtResponseType.Clear();
            DgvResponseType.Rows.Clear();
            DgvResponseType.Visible = false;
            DtResponseType.Rows.Clear(); DtResponseType.Columns.Clear();
            DataColumn dcAddressColumn = DtResponseType.Columns.Add("Address", typeof(string));
            #endregion ResopnseType

            #region IEDName
            //Namrata: 12/09/2017
            DgvIEDName.DataSource = null;
            DgvIEDName.Rows.Clear();
            DgvIEDName.Visible = false;
            IEDData.Rows.Clear(); IEDData.Columns.Clear();
            DataColumn dcIEDColumn = IEDData.Columns.Add("IEDName", typeof(string));
            #endregion IEDName

            #region RCB
            //Namrata: 10/03/2017
            DgvRCBData.DataSource = null;
            DgvRCBData.Rows.Clear();
            DgvRCBData.Visible = false;
            RCBData.Rows.Clear(); RCBData.Columns.Clear();
            DataColumn dcRCBColumn = RCBData.Columns.Add("Address", typeof(string));
            RCBData.Columns.Add("BufTime", typeof(string));
            RCBData.Columns.Add("ConRev", typeof(string));
            RCBData.Columns.Add("DSAddress", typeof(string));
            RCBData.Columns.Add("IntgPD", typeof(string));
            RCBData.Columns.Add("trigOptNum", typeof(string));
            #endregion RCB

            #region Dataset RCB
            //Namrata: 12/09/2017
            DgvRCBDS.DataSource = null;
            DgvRCBDS.Rows.Clear();
            DtRCBDS.Rows.Clear(); DtRCBDS.Columns.Clear();
            DataColumn dcDataestColumn = DtRCBDS.Columns.Add("Dataset", typeof(string));
            #endregion Dataset RCB
        }
        private void PboxImportICD_Click(object sender, EventArgs e)
        {
            string strRoutineName = "IEC61850ServerMaster: PboxImportICD_Click";
            try
            {
                ClearData();
                Stream myStream = null;
                ucmod.dataGridView1.Visible = true;
                TreeNode firstIedNode = null;
                openFileDialog1.AddExtension = true;
                openFileDialog1.InitialDirectory = Application.ExecutablePath;
                openFileDialog1.FileName = "";
                openFileDialog1.DefaultExt = "icd";
                openFileDialog1.Filter = "SCL files|*.icd;*.cid;*.iid;*.ssd;*.scd|IED Capability Description|*.icd|Instantiated IED Description|*.iid|System Specification Description|*.ssd|Substation Configuration Description|*.scd|Configured IED Description|*.cid|All files|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                configuration = new Configure();
                                try
                                {
                                    configuration.Load(myStream);
                                    configuration.IOpenedFileName = openFileDialog1.SafeFileName;
                                    configuration.IFullFileNamePath = openFileDialog1.FileName;

                                    ucmod.treeView1.Nodes.Clear();
                                    configuration.IIedMappings.Clear();
                                    ucmod.treeView1.Visible = false;
                                    ucmod.splitContainer1.Visible = false;
                                    ucmod.label1.Visible = false;
                                    
                                    Cursor.Current = Cursors.WaitCursor;
                                    try
                                    {
                                        configuration.ParseScl();
                                    }
                                    catch (System.ArgumentException exc)
                                    {
                                        MessageBox.Show(exc.Message.ToString() + "\n\nTry to open valid SCL file or modify existing one.", "Xml file parsing error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                        this.MappingChanged = false;
                                    }
                                    myStream.Close();


                                    this.MappingChanged = false;
                                    int index = 0;
                                    int notEmptyIeds = 0; //check if ied is empty
                                    ArrayList toErease = new ArrayList();
                                   
                                    foreach (var ied in configuration.Iieds)
                                    {
                                        Cursor.Current = Cursors.WaitCursor;  //Namrata: 09/04/2018
                                        int count = 0;

                                        #region IEDName
                                        //Namrata: 04/04/2018
                                        IEDName = IEDData.NewRow();
                                        IEDName["IEDName"] = ied.Key;
                                        IEDData.Rows.Add(IEDName);
                                        bool contains = IEDData.AsEnumerable().Any(row => IEDnames == row.Field<string>("IEDName"));
                                        RemoveDuplicateRows(IEDData, "IEDName");
                                        #endregion IEDName

                                        foreach (GridItem item in configuration.IgridItems)
                                        {
                                            if (item.IedName == ied.Key)
                                            {
                                                count++;
                                            }
                                            if (count > 0)
                                            {
                                                notEmptyIeds++;
                                                break;
                                            }
                                        }
                                        if (count == 0)
                                        {
                                            toErease.Add(ied.Key);
                                        }
                                    }
                                    foreach (var er in toErease)
                                    {
                                        if (configuration.Iieds.ContainsKey(er.ToString()))
                                        {
                                            configuration.Iieds.Remove(er.ToString());
                                        }

                                    }
                                    TreeNode treeNode = new TreeNode(openFileDialog1.SafeFileName);
                                    ucmod.treeView1.Nodes.Add(treeNode);
                                    foreach (KeyValuePair<string, XPathNodeIterator> t in configuration.Iieds)
                                    {
                                        int count = 0;
                                        index = 0;
                                        TreeNode tNode = new TreeNode(t.Key, 3, 3);

                                        if (firstIedNode == null)
                                        {
                                            firstIedNode = treeNode;
                                            ucmod.treeView1.SelectedNode = firstIedNode;

                                            foreach (TreeNode tnp in ucmod.treeView1.Nodes)
                                            {
                                                tnp.BackColor = Color.White;
                                                tnp.ForeColor = Color.Black;
                                                foreach (TreeNode tn in tnp.Nodes)
                                                {
                                                    tn.BackColor = Color.White;
                                                    tn.ForeColor = Color.Black;
                                                }
                                            }
                                            firstIedNode.BackColor = Color.LightBlue;
                                            firstIedNode.ForeColor = Color.Black;

                                            ShowCurrentIed(configuration.IgridItems as List<GridItem>);
                                            firstIedNode = null;
                                        }

                                        #region Fill ResponseType
                                        DtResponseType.Rows.Clear();
                                        DrResponseType = DtResponseType.NewRow();
                                        DrResponseType[0] = "On Request";
                                        DtResponseType.Rows.Add(DrResponseType);
                                        #endregion Fill ResponseType

                                        foreach (var v in configuration.Ireports)
                                        {
                                            if (v.Key.Contains(t.Key))
                                            {
                                                count++;
                                            }
                                            //Namrata: 31/10/2017
                                            Utils.Iec61850IEDname = t.Key;

                                            #region ResponseType
                                            DrResponseType = DtResponseType.NewRow();
                                            DrResponseType["Address"] = v.Value.Address.ToString();
                                            DtResponseType.Rows.Add(DrResponseType);
                                            #endregion ResponseType
                                            
                                            #region Fill RCB 
                                            //Namrata:10/10/2017
                                            RCBAddress = v.Value.Address.ToString();
                                            RCBBufferTime = v.Value.BufTime.ToString();
                                            RCBConfigRevision = v.Value.ConfRev.ToString();
                                            RCBDSAddress = v.Value.DSAddress.ToString();
                                            RCBIntegrityPeriod = v.Value.IntgPD.ToString();
                                            RCBTriggerOptionNum = v.Value.TrgOptNum.ToString();
                                            RCBRow = RCBData.NewRow();
                                            RCBRow["Address"] = RCBAddress;
                                            RCBRow["BufTime"] = RCBBufferTime.ToString();
                                            RCBRow["ConRev"] = RCBConfigRevision.ToString();
                                            RCBRow["DSAddress"] = RCBDSAddress;
                                            RCBRow["IntgPD"] = RCBIntegrityPeriod.ToString();
                                            RCBRow["trigOptNum"] = RCBTriggerOptionNum.ToString();
                                            RCBData.Rows.Add(RCBRow);
                                            DgvRCBData.DataSource = RCBData;
                                            Utils.DtRCBdata = RCBData;
                                            FillRCBData(RCBAddress);
                                            #endregion Fill RCB 
                                        }
                                        FillIEDName();

                                        FillResponseTypeData();

                                        TreeNode[] array = new TreeNode[count];
                                        foreach (var v in configuration.Ireports)
                                        {
                                            Cursor.Current = Cursors.WaitCursor;
                                            if (v.Key.Contains(t.Key))
                                            {
                                                string rcbNode = "";
                                                rcbNode = v.Value.Address.Replace(v.Value.IedName, "");
                                                if (v.Value.Buffered == "true")
                                                {
                                                    TreeNode n = new TreeNode(rcbNode, 1, 1);
                                                    array.SetValue(n, index++);
                                                    currentReport = v.Key.ToString();
                                                }
                                                else
                                                {
                                                    TreeNode n = new TreeNode(rcbNode, 2, 2);
                                                    array.SetValue(n, index++);
                                                    currentReport = v.Key.ToString();
                                                }
                                                //RefreshGrid(configuration.IgridItems);
                                                DtAllConfigData.Rows.Clear();  //Namrata: 09/04/2018
                                                RefreshGridFilters();
                                            }
                                            NodeCount++;
                                        }
                                        tNode.Nodes.AddRange(array);
                                        ucmod.treeView1.Nodes.Add(tNode);

                                    }
                                    BindDatasets();
                                    //Create Directory to store .icd files
                                    CreateDirectory(openFileDialog1.SafeFileName, openFileDialog1.FileName);
                                    //Cursor.Current = Cursors.Default;
                                    RefreshGrid(configuration.IgridItems);
                                    gridItemsAll = configuration.IgridItems.Count;
                                    //Namrata: 14/10/2017
                                    Utils.Filename = openFileDialog1.SafeFileName;
                                    ucmod.txtICDPath.Text = openFileDialog1.SafeFileName;
                                    ucmod.txtDescription.Text = openFileDialog1.SafeFileName;
                                    ucmod.txtRemoteIP.Text = Utils.RemoteIPAddress;
                                    ucmod.txtRemoteIP.Enabled = true;
                                    ucmod.txtDescription.Enabled = true;
                                    if (configuration.Iieds.Count == 0 && !configuration.WrongEntry)
                                    {
                                        MessageBox.Show("Parsing file failed: This is not valid SCL file", "SCL file paring error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                        MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                        ucmod.treeView1.Nodes.Clear();
                                        ucmod.treeView1.Visible = false;
                                        ucmod.splitContainer1.Visible = false;
                                        configuration.WrongEntry = false;
                                        configuration = null;
                                        this.MappingChanged = false;
                                    }
                                }
                                catch (System.Xml.XmlException ex)
                                {
                                    MessageBox.Show("Parsing file failed: " + ex.Message, "Xml file parsing error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    ucmod.treeView1.Nodes.Clear();
                                    ucmod.treeView1.Visible = false;
                                    ucmod.splitContainer1.Visible = false;
                                    configuration.WrongEntry = false;
                                    configuration = null;
                                    this.MappingChanged = false;
                                }
                            }
                        }
                    }
                    catch (FileNotFoundException ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message, "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                        this.MappingChanged = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        string Filename = null;
        public void ImportICDData(string Path)
        {
            string strRoutineName = "IEC61850ServerMaster: ImportICDData";
            try
            {
                ClearData();
                Stream myStream = null;
                ucmod.dataGridView1.Visible = true;
                TreeNode firstIedNode = null;
                openFileDialog1.FileName = Path;
                
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                configuration = new Configure();
                                try
                                {
                                    #region Get UnitID,TreeNode,ICDFilePath
                                    //Namrata: 12/04/2018
                                    Utils.UnitIDForIEC61850Client = UnitIDdummy;
                                    Utils.strFrmOpenproplusTreeNode = IEDNodeName;
                                    Utils.ICDFilePath = openFileDialog1.FileName;
                                    Filename = openFileDialog1.SafeFileName;
                                    Utils.Filename = Filename;
                                    ucmod.txtICDPath.Text = openFileDialog1.SafeFileName;
                                    #endregion Get UnitID,TreeNode,ICDFilePath

                                    configuration.Load(myStream);
                                    configuration.IOpenedFileName = openFileDialog1.SafeFileName;
                                    configuration.IFullFileNamePath = openFileDialog1.FileName;
                                    
                                   
                                    ucmod.treeView1.Nodes.Clear();
                                    configuration.IIedMappings.Clear();
                                    ucmod.treeView1.Visible = false;
                                    ucmod.splitContainer1.Visible = false;
                                    ucmod.label1.Visible = false;

                                    Cursor.Current = Cursors.WaitCursor;
                                    try
                                    {
                                        configuration.ParseScl();
                                    }
                                    catch (System.ArgumentException exc)
                                    {
                                        MessageBox.Show(exc.Message.ToString() + "\n\nTry to open valid SCL file or modify existing one.", "Xml file parsing error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                        this.MappingChanged = false;
                                    }
                                    myStream.Close();


                                    this.MappingChanged = false;
                                    int index = 0;
                                    int notEmptyIeds = 0; //check if ied is empty
                                    ArrayList toErease = new ArrayList();

                                    foreach (var ied in configuration.Iieds)
                                    {
                                        Cursor.Current = Cursors.WaitCursor;  //Namrata: 09/04/2018
                                        int count = 0;

                                        #region IEDName
                                        //Namrata: 04/04/2018
                                        IEDName = IEDData.NewRow();
                                        IEDName["IEDName"] = ied.Key;
                                        IEDData.Rows.Add(IEDName);
                                        bool contains = IEDData.AsEnumerable().Any(row => IEDnames == row.Field<string>("IEDName"));
                                        RemoveDuplicateRows(IEDData, "IEDName");
                                        #endregion IEDName

                                        foreach (GridItem item in configuration.IgridItems)
                                        {
                                            if (item.IedName == ied.Key)
                                            {
                                                count++;
                                            }
                                            if (count > 0)
                                            {
                                                notEmptyIeds++;
                                                break;
                                            }
                                        }
                                        if (count == 0)
                                        {
                                            toErease.Add(ied.Key);
                                        }
                                    }
                                    foreach (var er in toErease)
                                    {
                                        if (configuration.Iieds.ContainsKey(er.ToString()))
                                        {
                                            configuration.Iieds.Remove(er.ToString());
                                        }

                                    }
                                    TreeNode treeNode = new TreeNode(openFileDialog1.SafeFileName);
                                    ucmod.treeView1.Nodes.Add(treeNode);
                                    foreach (KeyValuePair<string, XPathNodeIterator> t in configuration.Iieds)
                                    {
                                        int count = 0;
                                        index = 0;
                                        TreeNode tNode = new TreeNode(t.Key, 3, 3);

                                        if (firstIedNode == null)
                                        {
                                            firstIedNode = treeNode;
                                            ucmod.treeView1.SelectedNode = firstIedNode;

                                            foreach (TreeNode tnp in ucmod.treeView1.Nodes)
                                            {
                                                tnp.BackColor = Color.White;
                                                tnp.ForeColor = Color.Black;
                                                foreach (TreeNode tn in tnp.Nodes)
                                                {
                                                    tn.BackColor = Color.White;
                                                    tn.ForeColor = Color.Black;
                                                }
                                            }
                                            firstIedNode.BackColor = Color.LightBlue;
                                            firstIedNode.ForeColor = Color.Black;

                                            ShowCurrentIed(configuration.IgridItems as List<GridItem>);
                                            firstIedNode = null;
                                        }

                                        #region Fill ResponseType
                                        DtResponseType.Rows.Clear();
                                        DrResponseType = DtResponseType.NewRow();
                                        DrResponseType[0] = "On Request";
                                        DtResponseType.Rows.Add(DrResponseType);
                                        #endregion Fill ResponseType

                                        foreach (var v in configuration.Ireports)
                                        {
                                            if (v.Key.Contains(t.Key))
                                            {
                                                count++;
                                            }
                                            //Namrata: 31/10/2017
                                            Utils.Iec61850IEDname = t.Key;

                                            #region ResponseType
                                            DrResponseType = DtResponseType.NewRow();
                                            DrResponseType["Address"] = v.Value.Address.ToString();
                                            DtResponseType.Rows.Add(DrResponseType);
                                            #endregion ResponseType

                                            #region Fill RCB 
                                            //Namrata:10/10/2017
                                            RCBAddress = v.Value.Address.ToString();
                                            RCBBufferTime = v.Value.BufTime.ToString();
                                            RCBConfigRevision = v.Value.ConfRev.ToString();
                                            RCBDSAddress = v.Value.DSAddress.ToString();
                                            RCBIntegrityPeriod = v.Value.IntgPD.ToString();
                                            RCBTriggerOptionNum = v.Value.TrgOptNum.ToString();
                                            RCBRow = RCBData.NewRow();
                                            RCBRow["Address"] = RCBAddress;
                                            RCBRow["BufTime"] = RCBBufferTime.ToString();
                                            RCBRow["ConRev"] = RCBConfigRevision.ToString();
                                            RCBRow["DSAddress"] = RCBDSAddress;
                                            RCBRow["IntgPD"] = RCBIntegrityPeriod.ToString();
                                            RCBRow["trigOptNum"] = RCBTriggerOptionNum.ToString();
                                            RCBData.Rows.Add(RCBRow);
                                            DgvRCBData.DataSource = RCBData;
                                            Utils.DtRCBdata = RCBData;
                                            FillRCBData(RCBAddress);
                                            #endregion Fill RCB 
                                        }
                                        FillIEDName();

                                        FillResponseTypeData();

                                        TreeNode[] array = new TreeNode[count];
                                        foreach (var v in configuration.Ireports)
                                        {
                                            Cursor.Current = Cursors.WaitCursor;
                                            if (v.Key.Contains(t.Key))
                                            {
                                                string rcbNode = "";
                                                rcbNode = v.Value.Address.Replace(v.Value.IedName, "");
                                                if (v.Value.Buffered == "true")
                                                {
                                                    TreeNode n = new TreeNode(rcbNode, 1, 1);
                                                    array.SetValue(n, index++);
                                                    currentReport = v.Key.ToString();
                                                }
                                                else
                                                {
                                                    TreeNode n = new TreeNode(rcbNode, 2, 2);
                                                    array.SetValue(n, index++);
                                                    currentReport = v.Key.ToString();
                                                }
                                                //RefreshGrid(configuration.IgridItems);
                                                DtAllConfigData.Rows.Clear();  //Namrata: 09/04/2018
                                                RefreshGridFilters();
                                            }
                                            NodeCount++;
                                        }
                                        tNode.Nodes.AddRange(array);
                                        ucmod.treeView1.Nodes.Add(tNode);

                                    }
                                    BindDatasets();
                                    CreateDirectory(openFileDialog1.SafeFileName, openFileDialog1.FileName);
                                    //Cursor.Current = Cursors.Default;
                                    RefreshGrid(configuration.IgridItems);
                                    gridItemsAll = configuration.IgridItems.Count;
                                    //Namrata: 14/10/2017
                                    Utils.Filename = openFileDialog1.SafeFileName;
                                    ucmod.txtICDPath.Text = openFileDialog1.SafeFileName;
                                    ucmod.txtDescription.Text = openFileDialog1.SafeFileName;
                                    ucmod.txtRemoteIP.Text = Utils.RemoteIPAddress;
                                    ucmod.txtRemoteIP.Enabled = true;
                                    ucmod.txtDescription.Enabled = true;
                                    if (configuration.Iieds.Count == 0 && !configuration.WrongEntry)
                                    {
                                        MessageBox.Show("Parsing file failed: This is not valid SCL file", "SCL file paring error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                        MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                        ucmod.treeView1.Nodes.Clear();
                                        ucmod.treeView1.Visible = false;
                                        ucmod.splitContainer1.Visible = false;
                                        configuration.WrongEntry = false;
                                        configuration = null;
                                        this.MappingChanged = false;
                                    }
                                }
                                catch (System.Xml.XmlException ex)
                                {
                                    MessageBox.Show("Parsing file failed: " + ex.Message, "Xml file parsing error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    ucmod.treeView1.Nodes.Clear();
                                    ucmod.treeView1.Visible = false;
                                    ucmod.splitContainer1.Visible = false;
                                    configuration.WrongEntry = false;
                                    configuration = null;
                                    this.MappingChanged = false;
                                }
                            }
                        }
                    }
                    catch (FileNotFoundException ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message, "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                        this.MappingChanged = false;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private void CreateDirectory(string SafeFileName , string Filename)
        //{
        //    Utils.ICDFiles = Application.StartupPath + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";
        //    if (!Directory.Exists(Utils.ICDFiles))
        //    {
        //        Directory.CreateDirectory(Utils.ICDFiles);
        //        if (!FileExists(Utils.ICDFiles + @"\" + SafeFileName))
        //        {
        //            File.Copy(Filename, Utils.ICDFiles + @"\" + SafeFileName);
        //        }
        //        else { }
        //    }
        //    else
        //    {
        //        if (!FileExists(Utils.ICDFiles + @"\" + SafeFileName))
        //        {
        //            File.Copy(Filename, Utils.ICDFiles + @"\" + SafeFileName);
        //        }
        //        else { }
        //    }
        //}

        //Namrata:14/04/2018
        //Keep .icd file in AppData
        private void CreateDirectory(string SafeFileName, string Filename)
        {
            Utils.ICDFiles = System.IO.Path.GetTempPath() + @"\" + "Protocol" + @"\" + "IEC61850Client";
            if (!Directory.Exists(Utils.ICDFiles))
            {
                Directory.CreateDirectory(Utils.ICDFiles);
                if (!FileExists(Utils.ICDFiles + @"\" + SafeFileName))
                {
                    File.Copy(Filename, Utils.ICDFiles + @"\" + SafeFileName);
                }
                else { }
            }
            else
            {
                if (!FileExists(Utils.ICDFiles + @"\" + SafeFileName))
                {
                    File.Copy(Filename, Utils.ICDFiles + @"\" + SafeFileName);
                }
                else { }
            }
        }
        private void FillRCBData(string Address)
        {
            RCBData.TableName = Address;
            string RCBColumnsValue = "";
            DataRow RCBRow;
            if (Utils.DsRCBData.Tables.Contains(Address))
            {
                Utils.DsRCBData.Tables[Address].Clear();
            }
            else
            {
                Utils.DsRCBData.Tables.Add(Address);
                RCBRow = Utils.DsRCBData.Tables[Address].NewRow();
                Utils.DsRCBData.Tables[Address].Columns.Add("Address");
                Utils.DsRCBData.Tables[Address].Columns.Add("BufTime");
                Utils.DsRCBData.Tables[Address].Columns.Add("ConRev");
                Utils.DsRCBData.Tables[Address].Columns.Add("DSAddress");
                Utils.DsRCBData.Tables[Address].Columns.Add("IntgPD");
                Utils.DsRCBData.Tables[Address].Columns.Add("trigOptNum");
            }
            for (int i = 0; i < RCBData.Rows.Count; i++)
            {
                if (Address == RCBData.Rows[i][0].ToString())
                {
                    RCBRow = Utils.DsRCBData.Tables[Address].NewRow();
                    Utils.DsRCBData.Tables[Address].NewRow();
                    for (int j = 0; j < RCBData.Columns.Count; j++)
                    {
                        RCBColumnsValue = RCBData.Rows[i][j].ToString();
                        RCBRow[j] = RCBColumnsValue;
                    }
                    Utils.DsRCBData.Tables[Address].Rows.Add(RCBRow);
                    Utils.DsRCB = Utils.DsRCBData;
                }
            }
        }
        private void FillResponseTypeData()
        {
            //Namrata: 10/10/2017
            DgvResponseType.DataSource = DtResponseType;
            DtResponseType.TableName = Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname;
            string ColumnsValues = "";
            DataRow DrResponseType;
            if (Utils.DsResponseType.Tables.Contains(DtResponseType.TableName))
            {
                Utils.DsResponseType.Tables[DtResponseType.TableName].Clear();
            }
            else
            {
                Utils.DsResponseType.Tables.Add(DtResponseType.TableName);
                Utils.DsResponseType.Tables[DtResponseType.TableName].Columns.Add("Address");
            }
            for (int i = 0; i < DtResponseType.Rows.Count; i++)
            {
                DrResponseType = Utils.DsResponseType.Tables[DtResponseType.TableName].NewRow();
                Utils.DsResponseType.Tables[DtResponseType.TableName].NewRow();
                for (int j = 0; j < DtResponseType.Columns.Count; j++)
                {
                    ColumnsValues = DtResponseType.Rows[i][j].ToString();
                    DrResponseType[j] = ColumnsValues.ToString();
                }
                Utils.DsResponseType.Tables[DtResponseType.TableName].Rows.Add(DrResponseType);
            }   
            Utils.dsResponseType = Utils.DsResponseType;
        }
        private void FillIEDName()
        {
            //Namrata: 10/10/2017
            DgvIEDName.DataSource = IEDData;
            IEDData.TableName = Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname;
            string IEDColumnsValues = "";
            DataRow DrIEDName;
            if (Utils.dsIEDName.Tables.Contains(IEDData.TableName))
            {
                Utils.dsIEDName.Tables[IEDData.TableName].Clear();
            }
            else
            {
                Utils.dsIEDName.Tables.Add(IEDData.TableName);
                Utils.dsIEDName.Tables[IEDData.TableName].Columns.Add("IEDName");
            }
            for (int i = 0; i < IEDData.Rows.Count; i++)
            {
                DrIEDName = Utils.dsIEDName.Tables[IEDData.TableName].NewRow();
                Utils.dsIEDName.Tables[IEDData.TableName].NewRow();
                for (int j = 0; j < IEDData.Columns.Count; j++)
                {
                    IEDColumnsValues = IEDData.Rows[i][j].ToString();
                    DrIEDName[j] = IEDColumnsValues;
                }
                Utils.dsIEDName.Tables[IEDData.TableName].Rows.Add(DrIEDName);
            }
            Utils.dsIED = Utils.dsIEDName;
            //Namrata: 04/04/2018
            ucmod.CmbDeviceName.DataSource = Utils.dsIED.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];
            ucmod.CmbDeviceName.DisplayMember = "IEDName";
        }
        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }
        //Namrata: 20/03/2018
        //Check if File Exist in Directory
        static IEnumerable<string> GetFileSearchPaths(string fileName)
        {
            yield return fileName;
            yield return Path.Combine(Directory.GetParent(Path.GetDirectoryName(fileName)).FullName, Path.GetFileName(fileName));
        }
        static bool FileExists(string fileName)
        {
            return GetFileSearchPaths(fileName).Any(File.Exists);
        }
        static IEnumerable<string> GetFileSearchPaths1(string fileName)
        {
            yield return fileName;
            yield return Path.Combine(Directory.GetParent(Path.GetDirectoryName(fileName)).FullName, Path.GetFileName(fileName));
        }
        //static bool FileExists1(string fileName)
        //{
        //    return GetFileSearchPaths1(fileName).Any(File.Exists);
        //}
      
        private void lvIEDList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucmod.lvIEDList.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
                    {
                        item.Checked = false;
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ucMaster61850Server_Load(object sender, EventArgs e)
        {

        }
        public void BindDatasets()
        {
            List<string> list = new List<string>();
            string dsName = "";
            string s;
            string[] dsNameTemp;
            foreach (var v in configuration.IdataSets)
            {
                dsName = "";
                s = v.Key;
                if (s.StartsWith("@", StringComparison.CurrentCulture))
                {
                    dsNameTemp = v.Key.Split('.');
                    list.Add(dsNameTemp[0]);
                }
                else
                {
                    string[] tab = s.Split('.');
                    if (tab.Length == 4)
                    {
                        //if (Utils.dsIED.Tables.OfType<DataTable>().ToList().Where(tbl => tbl.Rows[0][0].ToString() == tab[1]).Any())
                        //{
                        dsName = tab[1] + tab[2] + "/" + tab[3] + "." + tab[0];
                        Utils.dsNameTemp = new string[] { tab[1].ToString() };
                        list.Add(dsName);
                        RCBDatasetName = dsName;
                        DRRCBDS = DtRCBDS.NewRow();
                        DRRCBDS["Dataset"] = RCBDatasetName.ToString();
                        DtRCBDS.Rows.Add(DRRCBDS);
                        DgvRCBDS.DataSource = DtRCBDS;
                        DtRCBDS.TableName = Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname;// Utils.strFrmOpenproplusTreeNode + "/" + "Undefined" + "/" + Utils.Iec61850IEDname;
                        string Indexied = "";
                        DataRow rowied;
                        if (Utils.DsRCBDS.Tables.Contains(DtRCBDS.TableName))
                        {
                            Utils.DsRCBDS.Tables[DtRCBDS.TableName].Clear();
                        }
                        else
                        {
                            Utils.DsRCBDS.Tables.Add(DtRCBDS.TableName);
                            Utils.DsRCBDS.Tables[DtRCBDS.TableName].Columns.Add("DataSetRCB");
                        }
                        for (int i = 0; i < DtRCBDS.Rows.Count; i++)
                        {
                            rowied = Utils.DsRCBDS.Tables[DtRCBDS.TableName].NewRow();
                            Utils.DsRCBDS.Tables[DtRCBDS.TableName].NewRow();
                            for (int j = 0; j < DtRCBDS.Columns.Count; j++)
                            {
                                Indexied = DtRCBDS.Rows[i][j].ToString();
                                rowied[j] = Indexied.ToString();
                            }
                            Utils.DsRCBDS.Tables[DtRCBDS.TableName].Rows.Add(rowied);
                        }
                        Utils.DsRCBDataset = Utils.DsRCBDS;
                    }
                }
            }
        }
        public void RefreshGrid(IEnumerable<GridItem> items)
        {

            if (items == null)
            {

                Cursor.Current = Cursors.Default;
                ucmod.dataGridView1.Rows.Clear();

            }
            else
            {
                ucmod.dataGridView1.Rows.Clear();
                foreach (GridItem item in items)
                {
                    int i = ucmod.dataGridView1.Rows.Add();
                    ucmod.dataGridView1.Rows[i].Cells[0].Value = item.Id.ToString(CultureInfo.InvariantCulture);
                    DataGridViewComboBoxCell cell = ucmod.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Mapping Type"]] as DataGridViewComboBoxCell;
                    if (cell != null)
                    {
                        if (!String.IsNullOrEmpty(item.MappingType.Trim()))
                        {
                            cell.Value = item.MappingType;
                        }
                    }

                    DataGridViewComboBoxCell indexCell = ucmod.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Index"]] as DataGridViewComboBoxCell;

                    if (indexCell != null)
                    {
                        indexCell.Items.Clear();
                        indexCell.Items.Add(item.Index);
                        indexCell.Value = item.Index;
                    }

                    DataGridViewComboBoxCell fTypeCell = ucmod.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Refresh Type"]] as DataGridViewComboBoxCell;
                    if (fTypeCell != null)
                    {
                        if (!String.IsNullOrEmpty(item.MappingType.Trim()))
                        {
                            fTypeCell.Items.Add("On Request");
                            if (!item.RefreshType.Equals("On Request"))
                            {
                                fTypeCell.Items.Add(item.RefreshType);
                            }

                            fTypeCell.Value = item.RefreshType;

                        }
                    }

                    ucmod.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Type"]].Value = item.GridItemType;
                    ucmod.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Object Reference"]].Value = item.ObjectReference;
                    ucmod.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["FC"]].Value = item.FC;
                    ucmod.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Description"]].Value = item.Description;
                }
            }


            List<GridItem> listItems = items as List<GridItem>;
            if (listItems != null)
            {

                if (listItems.Count == 0)
                {

                }
            }
            else
            {

            }


            Cursor.Current = Cursors.Default;
        }
        public void RefreshGrid1(IEnumerable<GridItem> items)
        {
            string strRoutineName = "RefreshGrid";
            try
            {
                if (items == null)
                {
                    Cursor.Current = Cursors.Default;
                    ucmod.dataGridView1.Rows.Clear();
                }
                else
                {
                    ucmod.dataGridView1.Rows.Clear();
                    foreach (GridItem item in items)
                    {
                        int i = ucmod.dataGridView1.Rows.Add();
                        ucmod.dataGridView1.Rows[i].Cells[0].Value = item.Id.ToString(CultureInfo.InvariantCulture);
                        DataGridViewComboBoxCell cell = ucmod.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Mapping Type"]] as DataGridViewComboBoxCell;
                        if (cell != null)
                        {
                            if (!String.IsNullOrEmpty(item.MappingType.Trim()))
                            {
                                cell.Value = item.MappingType;
                            }
                        }
                        DataGridViewComboBoxCell indexCell = ucmod.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Index"]] as DataGridViewComboBoxCell;
                        if (indexCell != null)
                        {
                            indexCell.Items.Clear();
                            indexCell.Items.Add(item.Index);
                            indexCell.Value = item.Index;
                        }
                        DataGridViewComboBoxCell fTypeCell = ucmod.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Refresh Type"]] as DataGridViewComboBoxCell;
                        if (fTypeCell != null)
                        {
                            if (!String.IsNullOrEmpty(item.MappingType.Trim()))
                            {
                                fTypeCell.Items.Add("On Request");
                                if (!item.RefreshType.Equals("On Request"))
                                {
                                    fTypeCell.Items.Add(item.RefreshType);
                                }
                                fTypeCell.Value = item.RefreshType;
                            }
                        }
                        ucmod.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Type"]].Value = item.GridItemType;
                        ucmod.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Object Reference"]].Value = item.ObjectReference;
                        ucmod.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["FC"]].Value = item.FC;
                        ucmod.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Description"]].Value = item.Description;
                    }
                }
                List<GridItem> listItems = items as List<GridItem>;
                if (listItems != null)
                {
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public ICollection<GridItem> FillCurrentReport(ICollection<GridItem> listGridIt)
        {
            if (listGridIt == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(currentReport))
            {
                return listGridIt;
            }
            List<GridItem> tmp = new List<GridItem>();
            Report rep = null;
            foreach (var r in configuration.Ireports)
            {
                if (r.Key.Contains(currentReport))
                {
                    rep = r.Value;
                    break;
                }
            }
            tmp.Clear();
            if (rep != null)
            {
                string dsName = rep.DSAddressDots;
                string dsKey = dsName;
                if (configuration.IdataSets.ContainsKey(dsKey))
                {
                    Collection<Fcda> list = configuration.IdataSets[dsKey].ToCollection<Fcda>();
                    #region
                    if (list == null)
                    {
                        currentIedName = rep.IedName;
                        listGridIt = ShowCurrentIed(listGridIt) as List<GridItem>;
                        return listGridIt;
                    }
                    else
                    {
                        foreach (GridItem gItem in listGridIt as List<GridItem>)
                        {
                            foreach (Fcda f in list)
                            {
                                if (gItem.ObjectReference.Contains(f.Address) && (!tmp.Contains(gItem) || f.FC.Equals("CO")))
                                {
                                    tmp.Add(gItem);
                                    DrAllConfigData = DtAllConfigData.NewRow();
                                    DrAllConfigData["ObjectReferrence"] = gItem.ObjectReference.ToString();
                                    DrAllConfigData["Node"] = currentReport;
                                    //Namrata: 09/04/2018
                                    DrAllConfigData["FC"] = gItem.FC.ToString();
                                    DtAllConfigData.Rows.Add(DrAllConfigData);
                                }
                            }
                        }
                        #endregion
                        #region Fill All Configuration OnRequest Data
                        DgvOnRequestData.DataSource = Utils.OnReqData;
                        Utils.OnReqData.TableName = Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname + "_" + "On Request";
                        string OnReqIndex = ""; DataRow OnReqRow;
                        if (Utils.DsAllConfigureData.Tables.Contains(Utils.OnReqData.TableName))
                        {
                            Utils.DsAllConfigureData.Tables[Utils.OnReqData.TableName].Clear();
                        }
                        else
                        {
                            Utils.DsAllConfigureData.Tables.Add(Utils.OnReqData.TableName);
                            Utils.DsAllConfigureData.Tables[Utils.OnReqData.TableName].Columns.Add("ObjectReferrence");
                            Utils.DsAllConfigureData.Tables[Utils.OnReqData.TableName].Columns.Add("Node");
                            Utils.DsAllConfigureData.Tables[Utils.OnReqData.TableName].Columns.Add("FC");
                        }
                        for (int i = 0; i < Utils.OnReqData.Rows.Count; i++)
                        {
                            OnReqRow = Utils.DsAllConfigureData.Tables[Utils.OnReqData.TableName].NewRow();
                            Utils.DsAllConfigureData.Tables[Utils.OnReqData.TableName].NewRow();
                            for (int j = 0; j < Utils.OnReqData.Columns.Count; j++)
                            {
                                OnReqIndex = Utils.OnReqData.Rows[i][j].ToString();
                                OnReqRow[j] = OnReqIndex.ToString();
                            }
                            Utils.DsAllConfigureData.Tables[Utils.OnReqData.TableName].Rows.Add(OnReqRow);
                        }
                        Utils.DsAllConfigurationData = Utils.DsAllConfigureData;
                        #endregion Fill All Configuration OnRequest Data
                        #region Fill All Configuration Data with respect to "CurrentReport"
                        DgvAllData.DataSource = DtAllConfigData;
                        DtAllConfigData.TableName = Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname + "_" + currentReport;
                        string ConfigurationIndex = ""; DataRow ConfigurationRow;
                        if (Utils.DsAllConfigureData.Tables.Contains(DtAllConfigData.TableName))
                        {
                            Utils.DsAllConfigureData.Tables[DtAllConfigData.TableName].Clear();
                        }
                        else
                        {
                            Utils.DsAllConfigureData.Tables.Add(DtAllConfigData.TableName);
                            Utils.DsAllConfigureData.Tables[DtAllConfigData.TableName].Columns.Add("ObjectReferrence");
                            Utils.DsAllConfigureData.Tables[DtAllConfigData.TableName].Columns.Add("Node");
                            Utils.DsAllConfigureData.Tables[DtAllConfigData.TableName].Columns.Add("FC");
                        }
                        for (int i = 0; i < DtAllConfigData.Rows.Count; i++)
                        {
                            ConfigurationRow = Utils.DsAllConfigureData.Tables[DtAllConfigData.TableName].NewRow();
                            Utils.DsAllConfigureData.Tables[DtAllConfigData.TableName].NewRow();
                            for (int j = 0; j < DtAllConfigData.Columns.Count; j++)
                            {
                                ConfigurationIndex = DtAllConfigData.Rows[i][j].ToString();
                                ConfigurationRow[j] = ConfigurationIndex.ToString();
                            }
                            Utils.DsAllConfigureData.Tables[DtAllConfigData.TableName].Rows.Add(ConfigurationRow);
                        }
                        Utils.DsAllConfigurationData = Utils.DsAllConfigureData;
                        #endregion Fill All Configuration Data with respect to "CurrentReport"
                    }
                }
               

                if (configuration.IdynamicDataSets.ContainsKey(dsKey))
                {
                    List<Fcda> list = configuration.IdynamicDataSets[dsKey];
                    if (list == null)
                    {
                        currentIedName = rep.IedName;
                        listGridIt = ShowCurrentIed(listGridIt) as List<GridItem>;
                        UpdateMappingStatistic(currentIedName);
                        return listGridIt;
                    }
                    else
                    {
                        foreach (GridItem gItem in listGridIt as List<GridItem>)
                        {
                            foreach (Fcda f in list)
                            {
                                if (gItem.ObjectReference.Contains(f.Address))
                                {
                                    tmp.Add(gItem);
                                }
                            }
                        }
                    }
                }
            }
            if (tmp.Count == 0)
            {
                //Namrata: 19/01/2017
                DtAllConfigData.TableName = Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname + "_" + currentReport;// Utils.strFrmOpenproplusTreeNode + "/" + "Undefined" + "/" + Utils.Iec61850IEDname + "/" + currentReport;
                if (Utils.DsAllConfigureData.Tables.Contains(DtAllConfigData.TableName))
                {

                }
                else
                {
                    Utils.DsAllConfigureData.Tables.Add(DtAllConfigData.TableName);
                }
                Utils.DsAllConfigurationData = Utils.DsAllConfigureData;
            }
            return tmp.Count == 0 ? null : tmp;
        }
        public ICollection<GridItem> ShowCurrentIed(ICollection<GridItem> listGridIt)
        {
            if (listGridIt == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(currentIedName))
            {
                UpdateMappingStatistic(currentIedName);
                return listGridIt;
            }
            UpdateMappingStatistic(currentIedName);
            List<GridItem> tmp = new List<GridItem>();
            foreach (GridItem i in listGridIt as List<GridItem>)
            {
                if (i.IedName == currentIedName)
                {
                    tmp.Add(i);
                }
            }
            return tmp.Count == 0 ? null : tmp;
        }
        public void UpdateMappingStatistic(string iedName)
        {
            string lastIndexAnalog = "";
            string lastIndexIedDin = "";
            string lastIndexControl = "";
            string lastIndexParameter = "";
            if (!String.IsNullOrEmpty(iedName))
            {
                int lastMappedAnalog = configuration.IIedMappings[iedName + ".Analog"].Count;
                if (lastMappedAnalog > 0 && configuration.IIedMappings[iedName + ".Analog"][lastMappedAnalog - 1] != null)
                {
                    lastIndexAnalog = configuration.IIedMappings[iedName + ".Analog"][lastMappedAnalog - 1].Index;
                }
                else
                {
                    lastIndexAnalog = "0";
                }

                int lastMappedIedDin = configuration.IIedMappings[iedName + ".IedDin"].Count;
                if (lastMappedIedDin > 0 && configuration.IIedMappings[iedName + ".IedDin"][lastMappedIedDin - 1] != null)
                {
                    lastIndexIedDin = configuration.IIedMappings[iedName + ".IedDin"][lastMappedIedDin - 1].Index;
                }
                else
                {
                    lastIndexIedDin = "0";
                }
                int lastMappedControl = configuration.IIedMappings[iedName + ".Control"].Count;
                if (lastMappedControl > 0 && configuration.IIedMappings[iedName + ".Control"][lastMappedControl - 1] != null)
                {
                    lastIndexControl = configuration.IIedMappings[iedName + ".Control"][lastMappedControl - 1].Index;
                }
                else
                {
                    lastIndexControl = "0";
                }
                int lastMappedParameter = configuration.IIedMappings[iedName + ".Parameter"].Count;
                if (lastMappedParameter > 0 && configuration.IIedMappings[iedName + ".Parameter"][lastMappedParameter - 1] != null)
                {
                    lastIndexParameter = configuration.IIedMappings[iedName + ".Parameter"][lastMappedParameter - 1].Index;
                }
                else
                {
                    lastIndexParameter = "0";
                }
            }
            else
            {
                lastIndexParameter = "0";
                lastIndexControl = "0";
                lastIndexIedDin = "0";
                lastIndexAnalog = "0";
            }
            if (configuration.Iieds.ContainsKey(iedName))
            {
            }
            else
            {
            }
        }
        public ICollection<GridItem> UseFCFilter(ICollection<GridItem> listGridIt)
        {
            if (listGridIt == null)
            {
                return null;
            }
            if (ucmod.fcFilter.CheckedItems.Count == 0)
            {
                return listGridIt;
            }
            List<GridItem> tmp = new List<GridItem>();

            foreach (GridItem i in listGridIt as List<GridItem>)
            {
                foreach (var fc in ucmod.fcFilter.CheckedItems)
                {
                    if (i.FC.Contains(fc.ToString()))
                    {
                        tmp.Add(i);
                    }
                }
            }
            return tmp.Count == 0 ? null : tmp;
        }
        public ICollection<GridItem> UseTypeFilter(ICollection<GridItem> listGridIt)
        {
            if (listGridIt == null)
            {
                return null;
            }
            if (ucmod.typeFilter.CheckedItems.Count == 0)
            {
                return listGridIt;
            }
            List<GridItem> tmp = new List<GridItem>();

            foreach (GridItem i in listGridIt as List<GridItem>)
            {
                foreach (var ty in ucmod.typeFilter.CheckedItems)
                {
                    if (i.GridItemType.Contains(ty.ToString()))
                    {
                        tmp.Add(i);
                    }

                }
            }

            return tmp.Count == 0 ? null : tmp;
        }
        public void RefreshGridFilters()
        {
            ucmod.dataGridView1.Rows.Clear();
            ICollection<GridItem> listGridIt = new List<GridItem>();
            listGridIt = UseFCFilter(configuration.IgridItems as List<GridItem>);
            //listGridIt = UseTypeFilter(listGridIt as List<GridItem>);
            //listGridIt = ShowCurrentIed(listGridIt as List<GridItem>);
            listGridIt = FillCurrentReport(listGridIt as List<GridItem>);
            RefreshGrid(listGridIt);
            //ucmod.dataGridView1.Rows.Clear();
            //ICollection<GridItem> listGridIt = new List<GridItem>();
            //listGridIt = FillCurrentReport(configuration.IgridItems as List<GridItem>);
            //RefreshGrid(configuration.IgridItems as List<GridItem>);//(listGridIt);


        }
        private void loadDefaults()
        {
            //Namrata: 16/10/2017
            ucmod.txtDevice.Text = "";
            ucmod.txtTCPPort.Text = "102";//"502";
            ucmod.txtRetries.Text = "3";
            ucmod.txtTimeOut.Text = "100";
            //ucmod.txtDescription.Text = "IEC61850_IED_" + (Globals.get61850IEDNo(Int32.Parse(MasterNum)) + 1).ToString();
            //Namrata: 10/12/2017
            ucmod.txtRemoteIP.Text = "0.0.0.0";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "IEC61850ServerMaster: btnAdd_Click";
            try
            {
                if (iedList.Count >= Globals.MaIEC61850IED)
                {
                    MessageBox.Show("Maximum " + Globals.MaIEC61850IED + " IED's in MODBUS Masters are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucmod.grpIED);
                Utils.showNavigation(ucmod.grpIED, false);
                //Namrata: 15/03/2018
                ucmod.CmbDeviceName.DataSource = null;
                ucmod.txtRemoteIP.Enabled = false;
                ucmod.txtDevice.Enabled = false;
                ucmod.txtDescription.Enabled = false;
                loadDefaults();
                ucmod.txtUnitID.Text = (Globals.get61850IEDNo(Int32.Parse(MasterNum)) + 1).ToString();
                //Namrata: 23/02/2018
                Utils.UnitIDForIEC61850Client = ucmod.txtUnitID.Text;
                ucmod.grpIED.Visible = true;
                ucmod.txtUnitID.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            //if (!Validate()) return;
            List<KeyValuePair<string, string>> iedData = Utils.getKeyValueAttributes(ucmod.grpIED);
            if (mode == Mode.ADD)
            {
                if (Utils.Filename == "")
                {
                    MessageBox.Show("Please import .icd file", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    //Namrata: 04/04/2018
                    Utils.Iec61850IEDname = ucmod.CmbDeviceName.Text;
                    List<string> tblNameList = Utils.DsAllConfigurationData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).Select(x => x).FirstOrDefault();
                    if (!Utils.DsAllConfigurationData.Tables.Contains(tblName))
                    {
                        MessageBox.Show("Data does not exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    TreeNode tmp = IEC6160ServerMasterTreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                    iedList.Add(new IED("IED", "IEC61850Client_IED", iedData, tmp, MasterTypes.IEC61850Client, Int32.Parse(MasterNum)));
                    Utils.CreateDI4IED(MasterTypes.IEC61850Client, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
                }
                Utils.Filename = "";
            }
            else if (mode == Mode.EDIT)
            {
                iedList[editIndex].updateAttributes(iedData);
                if (ucmod.lvIEDList.SelectedIndices.Count > 0) //Namrata:14/06/2017
                {
                    SelectedIndex = Convert.ToInt16(ucmod.lvIEDList.FocusedItem.Index);
                    Utils.IEDUnitID = Convert.ToInt16(ucmod.lvIEDList.Items[SelectedIndex].Text);
                }
                //Namrata:14/06/2017
                Utils.UpdateDI4IED(MasterTypes.IEC61850Client, Int32.Parse(MasterNum), Utils.IEDUnitID, Int32.Parse(iedList[SelectedIndex].UnitID), iedList[iedList.Count - 1].Device);
            }
            refreshList();
            //Namrata: 09/08/2017
            if (sender != null && e != null)
            {
                ucmod.grpIED.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
        }
        string UnitIDdummy = "";
        string ConcatKeyValue = "";
        string strICDPath = "";
        private void refreshList()
        {
            int cnt = 0;
            Utils.IEC61850MasteriedListGetIEDNo.Clear();
            ucmod.lvIEDList.Items.Clear();
            foreach (IED ied in iedList)
            {
                string[] row = new string[8]; //string[] row = new string[7];
                if (ied.IsNodeComment)
                {
                    row[0] = "Comment...";
                }
                else
                {
                    row[0] = ied.UnitID;
                    row[1] = ied.TCPPort;
                    row[2] = ied.RemoteIP;
                    row[3] = ied.Retries;
                    row[4] = ied.TimeOutMS;
                    row[5] = ied.Device;
                    row[6] = ied.Description;
                    row[7] = ied.SCLName;
                }
                ListViewItem lvItem = new ListViewItem(row);
                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucmod.lvIEDList.Items.Add(lvItem);
                //Namrata:13-03-2018
                UnitIDdummy = ied.UnitID;
            }
            if (UnitIDdummy.ToString() != "")
            {
                Utils.MasterNum = Utils.MasterNumForIEC61850Client;
                ConcatKeyValue = Utils.MasterNum + "_" + UnitIDdummy;
                //AddConfigSetting(ConcatKeyValue, Utils.ICDFilePath);
                //strICDPath = ReadConfig(ConcatKeyValue);
            }

            //Namarta: 20/11/2017
            Utils.IEC61850MasteriedList.AddRange(iedList);
            Utils.IEC61850MasteriedListGetIEDNo.AddRange(iedList);
        }

        public void ClearDataOnBtnCancel_Click()
        {
            #region Clear IED Data
            List<string> DsIEDList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
            DsIEDList.Select(x => x).Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).ToList().ForEach(tbl =>
            {
                if (Utils.dsIED.Tables.Contains(tbl))
                {
                    Utils.dsIED.Tables.Remove(tbl);
                }
            });
            #endregion Clear IED Data

            #region Clear ResponseType Data
            List<string> DsResponseTypeList = Utils.DsResponseType.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
            DsResponseTypeList.Select(x => x).Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).ToList().ForEach(tbl =>
            {
                if (Utils.DsResponseType.Tables.Contains(tbl))
                {
                    Utils.DsResponseType.Tables.Remove(tbl);
                }
            });
            #endregion Clear ResponseType Data

            #region Clear RCB Data
            List<string> DsRCBList = Utils.DsRCB.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
            DsRCBList.Select(x => x).Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).ToList().ForEach(tbl =>
            {
                if (Utils.DsRCB.Tables.Contains(tbl))
                {
                    Utils.DsRCB.Tables.Remove(tbl);
                }
            });
            #endregion Clear RCB Data

            #region Clear RCB Data
            List<string> DsRCBDatasetList = Utils.DsRCBDataset.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
            DsRCBDatasetList.Select(x => x).Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).ToList().ForEach(tbl =>
            {
                if (Utils.DsRCBDataset.Tables.Contains(tbl))
                {
                    Utils.DsRCBDataset.Tables.Remove(tbl);
                }
            });
            #endregion Clear RCB Data

            #region Clear All Configuration Data
            List<string> DsAllConfigList = Utils.DsAllConfigurationData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
            DsAllConfigList.Select(x => x).Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).ToList().ForEach(tbl =>
            {
                if (Utils.DsAllConfigurationData.Tables.Contains(tbl))
                {
                    Utils.DsAllConfigurationData.Tables.Remove(tbl);
                }
            });
            #endregion Clear All Configuration Data
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (mode == Mode.ADD)
            {
                ClearDataOnBtnCancel_Click();
            }
            ucmod.grpIED.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
            Utils.resetValues(ucmod.grpIED);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucmod.lvIEDList.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one IED ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucmod.lvIEDList.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete IED " + ucmod.lvIEDList.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucmod.lvIEDList.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            IEC6160ServerMasterTreeNode.Nodes.Remove(iedList.ElementAt(iIndex).getTreeNode());
                            Utils.RemoveDI4IED(MasterTypes.IEC61850Client, Int32.Parse(MasterNum), Int32.Parse(iedList[iIndex].UnitID));
                            iedList.RemoveAt(iIndex);
                            ucmod.lvIEDList.Items[iIndex].Remove();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one IED ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    refreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            //Utils.WriteLine(VerboseLevel.DEBUG, "*** iedList count: {0} lv count: {1}", iedList.Count, ucmod.lvIEDList.Items.Count);
            //DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            //if (result == DialogResult.No)
            //{
            //    return;
            //}
            //for (int i = ucmod.lvIEDList.Items.Count - 1; i >= 0; i--)
            //{
            //    if (ucmod.lvIEDList.Items[i].Checked)
            //    {
            //        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", i);
            //        IEC6160ServerMasterTreeNode.Nodes.Remove(iedList.ElementAt(i).getTreeNode());
            //        Utils.RemoveDI4IED(MasterTypes.IEC61850Client, Int32.Parse(MasterNum), Int32.Parse(iedList[i].UnitID));
            //        iedList.RemoveAt(i);
            //        ucmod.lvIEDList.Items[i].Remove();
            //    }
            //}
            //Utils.WriteLine(VerboseLevel.DEBUG, "*** iedList count: {0} lv count: {1}", iedList.Count, ucmod.lvIEDList.Items.Count);
            //refreshList();
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucmod btnFirst_Click clicked in class!!!");
            if (ucmod.lvIEDList.Items.Count <= 0) return;
            if (iedList.ElementAt(0).IsNodeComment) return;
            editIndex = 0;
            loadValues();
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            //Namrata:27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucmod btnPrev_Click clicked in class!!!");
            if (editIndex - 1 < 0) return;
            if (iedList.ElementAt(editIndex - 1).IsNodeComment) return;
            editIndex--;
            loadValues();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            //Namrata:27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucmod btnNext_Click clicked in class!!!");
            if (editIndex + 1 >= ucmod.lvIEDList.Items.Count) return;
            if (iedList.ElementAt(editIndex + 1).IsNodeComment) return;
            editIndex++;
            loadValues();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucmod btnLast_Click clicked in class!!!");
            if (ucmod.lvIEDList.Items.Count <= 0) return;
            if (iedList.ElementAt(iedList.Count - 1).IsNodeComment) return;
            editIndex = iedList.Count - 1;
            loadValues();
        }
        private void btnExportIED_Click(object sender, EventArgs e)
        {
            if (ucmod.lvIEDList.CheckedItems.Count != 1)
            {
                MessageBox.Show("Select single IED for export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ListViewItem lvi = ucmod.lvIEDList.CheckedItems[0];
            IED expObj = null;
            //Now get the IED object...
            foreach (IED iedObj in iedList)
            {
                if (iedObj.UnitID == lvi.Text)
                {
                    expObj = iedObj;
                    break;
                }
            }
            Utils.SaveIEDFile(expObj.exportIED());
        }
        private void btnImportIED_Click(object sender, EventArgs e)
        {
            if (ofdXMLFile.ShowDialog() == DialogResult.OK)
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "*** Opening file: {0}", ofdXMLFile.FileName);
                if (!Utils.IsXMLWellFormed(ofdXMLFile.FileName))
                {
                    MessageBox.Show("Selected file is not a valid XML!!!.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ofdXMLFile.FileName);
                XmlNodeList nodeList = xmlDoc.SelectNodes("IEDexport");
                Utils.WriteLine(VerboseLevel.BOMBARD, "nodeList count: {0}", nodeList.Count);

                if (nodeList.Count <= 0)
                {
                    MessageBox.Show("Selected file is not an IED exported node.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                XmlNode rootNode = nodeList.Item(0);
                Utils.WriteLine(VerboseLevel.DEBUG, "*** Exported IED Node name: {0}", rootNode.Name);
                if (rootNode.Attributes != null)
                {
                    foreach (XmlAttribute item in rootNode.Attributes)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", item.Name, item.Value);
                        if (item.Name == "MasterType")
                        {
                            if (item.Value != mType.ToString())
                            {
                                MessageBox.Show("Invalid Master Type (" + item.Value + ") to import!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
                foreach (XmlNode node in rootNode)
                {
                    Utils.WriteLine(VerboseLevel.BOMBARD, "node value: '{0}' child count {1}", node.Name, node.ChildNodes.Count);
                    if (node.Name == "IED")
                    {
                        if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                        TreeNode tmp = IEC6160ServerMasterTreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                        iedList.Add(new IED(node, tmp, MasterTypes.IEC61850Client, Int32.Parse(MasterNum), true));
                        Utils.CreateDI4IED(MasterTypes.IEC61850Client, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
                        tmp.Expand();
                    }
                }
                refreshList();
                MessageBox.Show("IED imported successfully!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }//File selected thru open dialog...
        }


        private void lvIEDList_DoubleClick(object sender, EventArgs e)
        {
            if (ucmod.lvIEDList.SelectedItems.Count <= 0) return;
            ListViewItem lvi = ucmod.lvIEDList.SelectedItems[0];
            Utils.UncheckOthers(ucmod.lvIEDList, lvi.Index);
            if (iedList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ucmod.grpIED.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            Utils.showNavigation(ucmod.grpIED, true);
            ucmod.txtRemoteIP.Enabled = true;
            ucmod.txtDevice.Enabled = true;
            ucmod.txtDescription.Enabled = true;
            loadValues();
            //Namrata: 10/03/2018
            Utils.UnitIDForIEC61850Client = ucmod.txtUnitID.Text;
            ucmod.txtUnitID.Focus();
        }
        private void loadValues()
        {
            IED eied = iedList.ElementAt(editIndex);
            if (eied != null)
            {
                ucmod.txtUnitID.Text = eied.UnitID;
                ucmod.txtRemoteIP.Text = eied.RemoteIP;
                ucmod.txtTCPPort.Text = eied.TCPPort;
                ucmod.txtDevice.Text = eied.Device;
                ucmod.txtRetries.Text = eied.Retries;
                ucmod.txtTimeOut.Text = eied.TimeOutMS;
                if (eied.DR.ToLower() == "enable") ucmod.chkDR.Checked = true;
                else ucmod.chkDR.Checked = false;
                ucmod.txtDescription.Text = eied.Description;
                //Namrata : 1/11/2017
                ucmod.txtICDPath.Text = eied.SCLName;// openFileDialog1.SafeFileName;
            }
        }
        private bool IsUnitIDUnique(string unitID)
        {
            for (int i = 0; i < iedList.Count; i++)
            {
                IED ied = iedList.ElementAt(i);
                if (ied.UnitID == unitID && (mode == Mode.ADD || editIndex != i)) return false;
            }
            return true;
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucmod.grpIED))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //IED-UnitID should be unique...
            if (!IsUnitIDUnique(ucmod.txtUnitID.Text))
            {
                MessageBox.Show("IED Unit ID must be unique!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check Remote IP...
            if (!Utils.IsValidIPv4(ucmod.txtRemoteIP.Text))
            {
                MessageBox.Show("Invalid Remote IP.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check TCP Port...
            if (!Utils.IsValidTCPPort(Int32.Parse(ucmod.txtTCPPort.Text)))
            {
                MessageBox.Show("Invalid TCP Port.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            //Namrata: 16/10/2017
            ucmod.CmbEdition.Items.Clear();
            foreach (edition edi in Enum.GetValues(typeof(edition)))
            {
                ucmod.CmbEdition.Items.Add(edi.ToString());
            }
            ucmod.CmbEdition.SelectedIndex = 0;
        }
        private void addListHeaders()
        {
            ucmod.lvIEDList.Columns.Add("Unit ID", 60, HorizontalAlignment.Left);
            ucmod.lvIEDList.Columns.Add("TCP Port", 90, HorizontalAlignment.Left);
            ucmod.lvIEDList.Columns.Add("Remote IP Address", 140, HorizontalAlignment.Left);
            ucmod.lvIEDList.Columns.Add("Retires", 60, HorizontalAlignment.Left);
            ucmod.lvIEDList.Columns.Add("Time Out(ms)", 110, HorizontalAlignment.Left);
            ucmod.lvIEDList.Columns.Add("Device", 90, HorizontalAlignment.Left);
            ucmod.lvIEDList.Columns.Add("Description", -2, HorizontalAlignment.Left);
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IEC61850Client_")) return ucmod;
            kpArr.RemoveAt(0);
            Utils.WriteLine(VerboseLevel.DEBUG, "$$$$$ elem: {0}", kpArr.ElementAt(0));
            if (kpArr.ElementAt(0).Contains("IED_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Utils.WriteLine(VerboseLevel.DEBUG, "$$$$ After split elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (iedList.Count <= 0) return null;
                return iedList[idx].getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "***** MODBUSMaster: View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }
            return null;
        }
        public TreeNode getTreeNode()
        {
            return IEC6160ServerMasterTreeNode;
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
            foreach (IED iedn in iedList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(iedn.exportXMLnode1(), true);
                rootNode.AppendChild(importNode);
            }

            return rootNode;

        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";

            if (element == "IED")
            {
                foreach (IED ied in iedList)
                {
                    iniData += "IED_" + ctr++ + "=" + ied.Description + "," + ied.Device + "," + (ied.DR.ToLower() == "enable" ? "YES" : "NO") + Environment.NewLine;
                }
            }
            else
            {
                foreach (IED ied in iedList)
                {
                    iniData += ied.exportINI(slaveNum, slaveID, element, ref ctr);
                }
            }
            return iniData;
        }
        public List<IED> getIEDs()
        {
            return iedList;
        }
        public List<IED> getIEDsByFilter(string iedID)
        {
            List<IED> iList = new List<IED>();
            if (iedID.ToLower() == "all") return iedList;
            else
                foreach (IED i in iedList)
                {
                    if (i.getIEDID == iedID)
                    {
                        iList.Add(i);
                        break;
                    }
                }

            return iList;
        }
        public string getMasterID
        {
            get { return "IEC61850Client_" + MasterNum; }//  get { return "_61850_" + MasterNum; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        //Namrata: 03/09/2017
        private string IEC61850Edition = "";
        public string Edition
        {
            get { return IEC61850Edition; }
            set
            {
                IEC61850Edition = value;
            }
        }
        public string MasterNum
        {
            get { masterNum = Int32.Parse(ucmod.txtMasterNo.Text); return masterNum.ToString(); }
            set { masterNum = Int32.Parse(value); ucmod.txtMasterNo.Text = value; Globals.MasterNo = Int32.Parse(value); }
        }
        public string PortNum
        {
            get { portNum = Int32.Parse(ucmod.txtPortNo.Text); return portNum.ToString(); }
            set { portNum = Int32.Parse(value); ucmod.txtPortNo.Text = value; }
        }
        public string Run
        {
            get { run = ucmod.chkRun.Checked; return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
                if (run == true) ucmod.chkRun.Checked = true;
                else ucmod.chkRun.Checked = false;
            }
        }
        public string DEBUG
        {
            get { debug = Int32.Parse(ucmod.txtDebug.Text); return debug.ToString(); }
            set { debug = Int32.Parse(value); ucmod.txtDebug.Text = value; }
        }
        public string PollingIntervalmSec
        {
            get { pollingIntervalmSec = Int32.Parse(ucmod.txtPollingInterval.Text); return pollingIntervalmSec.ToString(); }
            set { pollingIntervalmSec = Int32.Parse(value); ucmod.txtPollingInterval.Text = value; }
        }
        public string PortTimesyncSec
        {
            get { portTimesyncSec = Int32.Parse(ucmod.txtPortTimeSync.Text); return portTimesyncSec.ToString(); }
            set { portTimesyncSec = Int32.Parse(value); ucmod.txtPortTimeSync.Text = value; }
        }
        public string RefreshInterval
        {
            get
            {
                try
                {
                    refreshInterval = Int32.Parse(ucmod.txtRefreshInterval.Text);
                }
                catch (System.FormatException)
                {
                    refreshInterval = 120;
                    ucmod.txtRefreshInterval.Text = refreshInterval.ToString();
                }
                return refreshInterval.ToString();
            }
            set { refreshInterval = Int32.Parse(value); ucmod.txtRefreshInterval.Text = value; }
        }
        public string AppFirmwareVersion
        {
            get { return appFirmwareVersion = ucmod.txtFirmwareVer.Text; }
            set { appFirmwareVersion = ucmod.txtFirmwareVer.Text = value; }
        }
        public string Description
        {
            get { return desc = ucmod.txtMBDescription.Text; }
            set { desc = ucmod.txtMBDescription.Text = value; }
        }
    }

    public static class ExtensionMethods
    {
        public static Collection<T> ToCollection<T>(this ICollection<T> items)
        {
            Collection<T> collection = new Collection<T>();
            List<T> lt = items as List<T>;

            for (int i = 0; i < lt.Count; i++)
            {
                collection.Add(lt[i]);
            }
            return collection;
        }

        public static ICollection<T> ToICollection<T>(this Collection<T> items)
        {
            if (items == null)
            {
                return null;
            }
            List<T> list = new List<T>();

            for (int i = 0; i < items.Count; i++)
            {
                list.Add(items[i]);
            }
            return list;
        }
    }
}
