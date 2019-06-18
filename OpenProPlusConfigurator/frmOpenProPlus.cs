using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Xml.Schema;
using System.Xml;
using System.IO.Compression;
using System.Threading;
using System.Net;
using System.Diagnostics;
using Microsoft.Win32;
//Swati: 19/11/2015
using System.Drawing.Drawing2D;

namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>frmOpenProPlus</b> is the main user interface to use the tool
    * \details   This is the main user interface to use the tool. It displays 
    * various menus, toolbars to access different software functionalities. In left-side panel,
    * we can see various nodes in treeview and there corresponding views are shown in right-side panel.
    * 
    */
    public partial class frmOpenProPlus : Form
    {
        #region Declarations
        OpenProPlus_Config opcHandle = null;
        frmProgess fp = null; //Namrata: 12/01/2018
        frmOverview fo = null;
        private bool showExitMessage = false;
        private string xmlFile = null;
        private bool iec104EventHandled = false;
        ucGroupIEC104 ucs104 = null;
        //Namrata:6/7/2017
        ucGroupIEC101Slave ucs101 = null;
        private bool iec101EventHandled = false;
        public const int COLS_B4_MULTISLAVE = 13;
        public const int TOTAL_MAP_PARAMS = 7;
        public const int FILTER_PANEL_HEIGHT = 70;
        private int sortColumn = -1;
        ucRCBList ucRCB = null;
        //Namrata: 06/09/2017
        //View Recent File
        private MruList MyMruList;
        ucAIlist ucsai = null;
        ucAOList ucsao = null;
        ucDIlist ucsdi = null;
        ucDOlist ucsdo = null;
        ucENlist ucsen = null;
        private bool IsXmlValid = false;
        ucRCBList uc = null; //Namrata: 12/01/2018
        List<RCB> aiList = new List<RCB>();
        ucAIlist ucai = null;//Namrata: 12/01/2018
        private AIConfiguration aicNodeforiec61850Client = null;
        private AOConfiguration aocNodeforiec61850Client = null;
        private DIConfiguration dicNodeforiec61850Client = null;
        private DOConfiguration docNodeforiec61850Client = null;
        private ENConfiguration encNodeforiec61850Client = null;
        private RCBConfiguration RCBNodeforiec61850Client = null;
        #endregion Declarations
        public frmOpenProPlus()
        {
            InitializeComponent();
            //Ajay: 21/11/2017
            //opcHandle = new OpenProPlus_Config();
            //if (!opcHandle.IsFileOpen)
            //{ EnDs_Save_SaveAs_Exit(false); }

            //Namrata: 05/01/2017
            //if (opcHandle.IsFileOpen)
            //{
            //    saveToolStripMenuItem.Enabled = newToolStripMenuItem.Enabled = false;
            //    toolStripStatusLabel1.Visible = false;
            //}
        }
        private void UpdateXMLFile(string FileName)
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                xmlFile = FileName;
                tspFileName.Text = FileName;
            }
            else
            {
                xmlFile = tspFileName.Text = string.Empty;
            }
        }
        private void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                //Delete all files from the Directory
                foreach (string file in Directory.GetFiles(path))
                {
                    File.Delete(file);
                }
                //Delete all child Directories
                foreach (string directory in Directory.GetDirectories(path))
                {
                    DeleteDirectory(directory);
                }
                //Delete a Directory
                Directory.Delete(path, true);
            }
        }
        private void frmParser_Load(object sender, EventArgs e)
        {
            try
            {
                //Namrata: 18/04/2018
                //Delete Folder from AppData
                Utils.ICDFiles = System.IO.Path.GetTempPath() + @"\" + "Protocol";
                /*Utils.ICDFiles = System.IO.Path.GetTempPath() + @"\" + "Protocol" + @"\" + "IEC61850Client";*//*System.IO.Path.GetTempPath() + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";*/
                if (Directory.Exists(Utils.ICDFiles))
                {
                    string DirectoryNameDelete = System.IO.Path.GetTempPath() + @"\" + "Protocol"; /*System.IO.Path.GetTempPath() + @"\" + "IEC61850_Client";*/
                    FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                    FileDirectoryOperations.DeleteDirectory(DirectoryNameDelete);
                }
                //Namrata: 23/04/2018
                //Delete Folder from ProgramData
                string DPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;
                if (DPath != "")
                {
                    if (ofdXMLFile.FileName != "")
                    {
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "Protocol" + "\\" + "IEC61850Client";/* Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration";*/
                        string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
                        if (Directory.Exists(path))
                        {
                            FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                            FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
                        }
                    }
                    else
                    {
                        FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                        FileDirectoryOperations.DeleteDirectory(DPath);
                    }
                }
                fp = new frmProgess();
                uc = new ucRCBList();
                ucai = new ucAIlist();
                toolStripStatusLabel1.Visible = false;
                lvValidationMessages.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
                lvValidationMessages.Scrollable = true;
                Utils.SetFormIcon(this);
                //Namrata: 31/08/2017
                //Check if Zone file exists...
                if (!File.Exists(Globals.ZONE_RESOURCES_PATH + Globals.TIME_ZONE_LIST))
                {
                    MessageBox.Show("Zone file (" + Globals.ZONE_RESOURCES_PATH + Globals.TIME_ZONE_LIST + ") is missing. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                #region XSD Validations
                //Check if XSD file exists...
                if (!File.Exists(Globals.RESOURCES_PATH + Globals.XSD_FILENAME))
                {
                    MessageBox.Show("Schema file (" + Globals.RESOURCES_PATH + Globals.XSD_FILENAME + ") is missing. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                if (!File.Exists(Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME))
                {
                    MessageBox.Show("Schema file (" + Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME + ") is missing. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                if (!Utils.IsXMLWellFormed(Globals.RESOURCES_PATH + Globals.XSD_FILENAME))
                {
                    MessageBox.Show("Schema file (" + Globals.RESOURCES_PATH + Globals.XSD_FILENAME + ") is not a valid XML. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                if (!Utils.IsXMLWellFormed(Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME))
                {
                    MessageBox.Show("Schema file (" + Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME + ") is not a valid XML. Exiting application!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                #endregion XSD Validations
                showExitMessage = true;
                lvValidationMessages.Columns.Add("Validation Messages...", 1000, HorizontalAlignment.Left);
                ResetConfiguratorState(true);
                HandleMapViewChange();
                //Namrata: 28/12/2017
                MyMruList = new MruList(Application.ProductName, this.recentFilesToolStripMenuItem, 10, this.myOwnRecentFilesGotCleared_handler);
                MyMruList.FileSelected += MyMruList_FileSelected;
                //Namrata: 19/12/2017
                TreeNode searchNode = tvItems.Nodes[0].Nodes[5];
                if (searchNode != null)
                    searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void myOwnRecentFilesGotCleared_handler(object obj, EventArgs evt)
        {
        }
        private void MyMruList_FileSelected(string file_name)
        {
            try
            {
                OpenFile(file_name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OpenFile11(string file_name)
        {
            try
            {
                RichTextBox RichtextBox = new RichTextBox();
                // Load the file.
                RichtextBox.Clear();
                if (file_name.ToLower().EndsWith(".rtf"))
                {
                    RichtextBox.LoadFile(file_name);
                }
                else
                {
                    RichtextBox.Text = File.ReadAllText(file_name);
                    int result = 0;
                    string errMsg = "XML file is valid...";
                    ResetConfiguratorState(false);
                    showLoading();
                    Console.WriteLine("*** Opening file: {0}", file_name);
                    xmlFile = "";//reset old filename...
                    ListViewItem lvi;
                    lvi = new ListViewItem("Validating file: " + file_name);
                    //Namrata: 27/7/2017
                    toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                    tspFileName.Text = file_name;
                    lvValidationMessages.Items.Add(lvi);
                    lvi = new ListViewItem("");
                    lvValidationMessages.Items.Add(lvi);
                    result = opcHandle.loadXML(file_name, tvItems, out IsXmlValid);
                    if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                    else { pnlValidationMessages.Visible = true; pnlValidationMessages.BringToFront(); }
                    if (result == -1) errMsg = "File doesnot exist!!!";
                    else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                    else if (result == -3) errMsg = "XSD file is not valid!!!";
                    else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                    lvi = new ListViewItem("");
                    lvValidationMessages.Items.Add(lvi);
                    lvi = new ListViewItem(errMsg);
                    lvValidationMessages.Items.Add(lvi);
                    lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                    if (result < 0)
                    {
                        hideLoading();
                        MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    xmlFile = ofdXMLFile.FileName;//Assign only after loading file...
                    tvItems.SelectedNode = tvItems.Nodes[0];
                    //Namrata: 05/01/2018
                    #region Treeview Collapse Common Node
                    TreeNode searchNode = tvItems.Nodes[0].Nodes[5];
                    if (searchNode != null)
                        searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                    #endregion Treeview Collapse Common Node
                    tvItems.Nodes[0].EnsureVisible();
                    hideLoading();
                }
                MyMruList.AddFile(file_name); // Add the file to the MRU list.
            }
            catch (Exception ex)
            {
                MyMruList.RemoveFile(file_name);// Remove the file from the MRU list.
                MessageBox.Show(ex.Message);
            }
        }
        private void OpenFile(string file_name)
        {
            try
            {
                RichTextBox RichtextBox = new RichTextBox();
                // Load the file.
                RichtextBox.Clear();
                if (file_name.ToLower().EndsWith(".rtf"))
                {
                    RichtextBox.LoadFile(file_name);
                }
                else
                {
                    #region ClearDatasets
                    Utils.DsRCB.Clear();
                    Utils.DsRCBData.Clear();
                    Utils.dsResponseType.Clear();
                    Utils.DsResponseType.Clear();
                    Utils.dsIED.Clear();
                    Utils.dsIEDName.Clear();
                    Utils.DsAllConfigurationData.Clear();
                    Utils.DsAllConfigureData.Clear();
                    Utils.DsRCBDataset.Clear();
                    Utils.DsRCBDS.Clear();
                    Utils.DtRCBdata.Clear();
                    #endregion ClearDatasets


                    RichtextBox.Text = File.ReadAllText(file_name);
                    Utils.XMLFolderPath = Path.GetDirectoryName(file_name);
                    int result = 0;
                    string errMsg = "XML file is valid...";
                    ResetConfiguratorState(false);
                    showLoading();
                    Console.WriteLine("*** Opening file: {0}", file_name);
                    xmlFile = "";//reset old filename...
                    ListViewItem lvi;
                    lvi = new ListViewItem("Validating file: " + file_name);
                    //Namrata: 27/7/2017
                    toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                    tspFileName.Text = file_name;
                    lvValidationMessages.Items.Add(lvi);
                    lvi = new ListViewItem("");
                    lvValidationMessages.Items.Add(lvi);
                    result = opcHandle.loadXML(file_name, tvItems, out IsXmlValid);
                    if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                    else { pnlValidationMessages.Visible = true; pnlValidationMessages.BringToFront(); }
                    if (result == -1) errMsg = "File doesnot exist!!!";
                    else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                    else if (result == -3) errMsg = "XSD file is not valid!!!";
                    else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                    lvi = new ListViewItem("");
                    lvValidationMessages.Items.Add(lvi);
                    lvi = new ListViewItem(errMsg);
                    lvValidationMessages.Items.Add(lvi);
                    lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                    if (result < 0)
                    {
                        hideLoading();
                        MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (!string.IsNullOrEmpty(ofdXMLFile.FileName) && File.Exists(ofdXMLFile.FileName)) //Ajay: 17/12/2018
                    { 
                        xmlFile = ofdXMLFile.FileName;//Assign only after loading file...
                    }
                    //Ajay: 17/12/2018
                    else
                    {
                        if (!string.IsNullOrEmpty(file_name) && File.Exists(file_name))
                        {
                            xmlFile = file_name;
                        }
                    }
                    tvItems.SelectedNode = tvItems.Nodes[0];
                    //Namrata: 05/01/2018
                    #region Treeview Collapse Common Node
                    TreeNode searchNode = tvItems.Nodes[0].Nodes[5];
                    if (searchNode != null)
                        searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                    #endregion Treeview Collapse Common Node
                    tvItems.Nodes[0].EnsureVisible();
                    hideLoading();
                }
                MyMruList.AddFile(file_name); // Add the file to the MRU list.
            }
            catch (Exception ex)
            {
                MyMruList.RemoveFile(file_name);// Remove the file from the MRU list.
                MessageBox.Show(ex.Message);
            }
        }
        private void OpenFile12(string file_name)
        {
            try
            {
                string DirectoryExtention = Path.GetExtension(file_name);
                RichTextBox RichtextBox = new RichTextBox();
                // Load the file.
                RichtextBox.Clear();
                if (file_name.ToLower().EndsWith(".rtf"))
                {
                    RichtextBox.LoadFile(file_name);
                }
                else
                {

                    if (DirectoryExtention == ".oppc")
                    {
                        FileDirectoryOperations.DecompressToDirectory(file_name, Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(file_name) + "\\" + "Protocol" + "\\" + "IEC61850Client");
                        string XMLFilePathFinal = file_name;
                        Utils.XMLPath = XMLFilePathFinal; //Namrata: 18/04/2018
                        Utils.DirectoryPath = file_name;
                        string DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(file_name) + "\\" + "Protocol" + "\\" + "IEC61850Client";
                        string DirectoryLocation = Path.GetDirectoryName(file_name);
                        Utils.ExtractedFileName = Path.GetFileNameWithoutExtension(file_name);
                        //Namrata: 29/03/2018
                        #region ClearDatasets
                        Utils.DsRCB.Clear();
                        Utils.DsRCBData.Clear();
                        Utils.dsResponseType.Clear();
                        Utils.DsResponseType.Clear();
                        Utils.dsIED.Clear();
                        Utils.dsIEDName.Clear();
                        Utils.DsAllConfigurationData.Clear();
                        Utils.DsAllConfigureData.Clear();
                        Utils.DsRCBDataset.Clear();
                        Utils.DsRCBDS.Clear();
                        Utils.DtRCBdata.Clear();
                        #endregion ClearDatasets
                        Utils.ICDFiles = System.IO.Path.GetTempPath() + @"\" + "Protocol" + @"\" + "IEC61850Client"; //Application.StartupPath + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";
                        string fileName = string.Empty;
                        string destFile = string.Empty;
                        if (Directory.Exists(Utils.ICDFiles))
                        {
                            DeleteDirectory(Utils.ICDFiles);
                        }
                        if (!Directory.Exists(Utils.ICDFiles))
                        {
                            Directory.CreateDirectory(Utils.ICDFiles);
                            if (System.IO.Directory.Exists(DirectoryPath))
                            {
                                string[] files = System.IO.Directory.GetFiles(DirectoryPath);//Get Files From Directory
                                foreach (string s in files)
                                {
                                    fileName = System.IO.Path.GetFileName(s);
                                    destFile = System.IO.Path.Combine(Utils.ICDFiles, fileName);
                                    System.IO.File.Copy(s, destFile, true);
                                }
                            }
                            else
                            {
                            }
                        }
                        string[] fileEntries = Directory.GetFiles(Utils.ICDFiles, "*.xml");
                        string XMLFilePath = fileEntries[0].ToString();
                        RichtextBox.Text = File.ReadAllText(XMLFilePath);
                        int result = 0;
                        string errMsg = "XML file is valid...";
                        ResetConfiguratorState(false);
                        showLoading();
                        xmlFile = "";//reset old filename...
                        ListViewItem lvi;
                        lvi = new ListViewItem("Validating file: " + XMLFilePath);// file_name);
                        //Namrata: 27/7/2017
                        toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                        tspFileName.Text = file_name;
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        result = opcHandle.loadXML(XMLFilePath, tvItems, out IsXmlValid);// (file_name, tvItems, out IsXmlValid);
                        if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                        else { pnlValidationMessages.Visible = true; pnlValidationMessages.BringToFront(); }
                        if (result == -1) errMsg = "File doesnot exist!!!";
                        else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                        else if (result == -3) errMsg = "XSD file is not valid!!!";
                        else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem(errMsg);
                        lvValidationMessages.Items.Add(lvi);
                        lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                        if (result < 0)
                        {
                            hideLoading();
                            MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        xmlFile = ofdXMLFile.FileName;//Assign only after loading file...
                        tvItems.SelectedNode = tvItems.Nodes[0];
                        //Namrata: 05/01/2018
                        #region Treeview Collapse Common Node
                        TreeNode searchNode = tvItems.Nodes[0].Nodes[5];
                        if (searchNode != null)
                            searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                        #endregion Treeview Collapse Common Node
                        tvItems.Nodes[0].EnsureVisible();
                        hideLoading();
                    }
                    else
                    {
                        RichtextBox.Text = File.ReadAllText(file_name);
                        int result = 0;
                        string errMsg = "XML file is valid...";
                        ResetConfiguratorState(false);
                        showLoading();
                        xmlFile = "";//reset old filename...
                        ListViewItem lvi;
                        lvi = new ListViewItem("Validating file: " + file_name);
                        //Namrata: 27/7/2017
                        toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                        tspFileName.Text = file_name;
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        result = opcHandle.loadXML(file_name, tvItems, out IsXmlValid);
                        if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                        else { pnlValidationMessages.Visible = true; pnlValidationMessages.BringToFront(); }
                        if (result == -1) errMsg = "File doesnot exist!!!";
                        else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                        else if (result == -3) errMsg = "XSD file is not valid!!!";
                        else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem(errMsg);
                        lvValidationMessages.Items.Add(lvi);
                        lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                        if (result < 0)
                        {
                            hideLoading();
                            MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        xmlFile = ofdXMLFile.FileName;//Assign only after loading file...
                        tvItems.SelectedNode = tvItems.Nodes[0];
                        //Namrata: 05/01/2018
                        #region Treeview Collapse Common Node
                        TreeNode searchNode = tvItems.Nodes[0].Nodes[5];
                        if (searchNode != null)
                            searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                        #endregion Treeview Collapse Common Node
                        tvItems.Nodes[0].EnsureVisible();
                        hideLoading();
                    }
                }
                MyMruList.AddFile(file_name); // Add the file to the MRU list.
            }
            catch (Exception ex)
            {
                MyMruList.RemoveFile(file_name);// Remove the file from the MRU list.
                MessageBox.Show(ex.Message);
            }
        }
        private void ValidationCallBack(object sender, oppcValidationMessages e)
        {
            try
            {
                if (e.Severity == XmlSeverityType.Warning)
                {
                    ListViewItem lvi = new ListViewItem("Warning: Matching schema not found.  No validation occurred." + e.Message);
                    lvValidationMessages.Items.Add(lvi);
                }
                else
                {
                    Console.WriteLine("\tValidation error Line No. {0}: {1}", e.LineNo, e.Message);
                    ListViewItem lvi = new ListViewItem("Validation error Line No. " + e.LineNo + ": " + e.Message);
                    lvValidationMessages.Items.Add(lvi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public OpenProPlus_Config getOpenProPlusHandle()
        {
            return opcHandle;
        }
        private void handleAbout()
        {
            frmAbout fa = new frmAbout();
            fa.ShowDialog();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            handleAbout();
        }
        private void tsbAbout_Click(object sender, EventArgs e)
        {
            handleAbout();
        }
        private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolbarToolStripMenuItem.Checked) tsParser.Visible = true;
            else tsParser.Visible = false;
        }
        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private string SCLFileName = "";
        private void tvItems_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            #region IEC61850Client
            if (Utils.IEC61850ServerList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 3))
            {
                if (e.Node.Index <= Utils.IEC61850ServerList.Count - 1)
                {

                    Utils.MasterNumForIEC61850Client = Utils.IEC61850ServerList[e.Node.Index].MasterNum.ToString();
                }
            }
            if (Utils.IEC61850ServerList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 4))
            {
                if (e.Node.Parent.Index <= Utils.IEC61850ServerList.Count - 1)
                {
                    Utils.MasterNumForIEC61850Client = Utils.IEC61850ServerList[e.Node.Parent.Index].MasterNum.ToString();
                }
            }
            else if (Utils.IEC61850ServerList.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 5))
            {
                if (e.Node.Parent.Index <= Utils.IEC61850ServerList.Count - 1)
                {
                    Utils.MasterNumForIEC61850Client = Utils.IEC61850ServerList[e.Node.Parent.Parent.Index].MasterNum.ToString();
                }
            }
            if (Utils.IEC61850MasteriedListGetIEDNo.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 4))
            {
                if (e.Node.Parent.Index <= Utils.IEC61850MasteriedListGetIEDNo.Count - 1)
                {
                    Utils.UnitIDForIEC61850Client = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Parent.Index].UnitID.ToString();
                }
            }
            else if (Utils.IEC61850MasteriedListGetIEDNo.Count > 0 && e.Node.FullPath.Contains("IEC61850 Group") && (e.Node.Level == 5))
            {
                Utils.UnitIDForIEC61850Client = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Parent.Index].UnitID.ToString();
            }
            #endregion IEC61850Client

            if (e.Node.Parent != null)
            {
                if (e.Node.Parent.Text == "IEC61850 Group")
                {
                    Utils.strFrmOpenproplusTreeNode = e.Node.Text;
                    Utils.strFrmOpenproplusIEdname = "";
                }
            }

            //if (e.Node.Parent.Parent.Text != null)
            //{
            //    if (e.Node.Parent.Parent.Text == "IEC61850 Group")
            //    {
            //        if (e.Button == MouseButtons.Right)
            //        {
            //            if (e.Node.Text.Contains("IED "))
            //            {
            //                Utils.SCLFileName = Utils.IEC61850MasteriedListGetIEDNo[e.Node.Index].SCLName.ToString();
            //                ContextMenuStrip cm = opcHandle.GetToolstripMenu();
            //                if (cm != null)
            //                {
            //                    Point pt = new Point(e.X, e.Y);
            //                    cm.Show(tvItems, pt);
            //                }
            //            }
            //        }
            //    }
            //}
            //Namrata: 15/7/2017 
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node.Text == "Master Configuration")
                {
                    ContextMenuStrip cm = opcHandle.getContextMenu();
                    if (cm != null)
                    {
                        Point pt = new Point(e.X, e.Y);
                        cm.Show(tvItems, pt);
                    }
                }
            }
            else
            {
            }
        }
        ucMaster61850Server ucmod = null;
        private RCBConfiguration RCBNode = null;
        private void tvItems_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                try
                {
                    if (e.Node != null)
                    { Console.WriteLine("### kp array: {0}", Utils.getKeyPathArray(e.Node).ToString()); }
                    if (e.Node != null && e.Node.Parent != null)
                    {
                        Console.WriteLine("***** Parent Node of {0} is: {1}", e.Node.Text, e.Node.Parent.Text);
                        Console.WriteLine("***** Key for Parent Node of {0} is: {1} depth: {2}", e.Node.Name, e.Node.Parent.Name, e.Node.Level);
                    }
                }
                catch (System.NullReferenceException)
                {
                    Console.WriteLine("*** NullReferenceException handled...");
                }
                this.scParser.Panel2.Controls.Clear();//Remove all previous controls...
                Control ucrp = opcHandle.getView(Utils.getKeyPathArray(e.Node));

                //Namrata: 26/10/2017
                if (e.Node.Text == "AI")
                {
                    if (ucrp is ucAIlist)
                    {
                        ucsai = (ucAIlist)ucrp;
                        ucsai.ucAIlist_Load(null, null);
                        if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
                        {
                            Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
                            //Namrata: 04/04/2018
                            List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                            string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                            if (tblName != null)
                            {
                                string[] tokens = tblName.Split('_');
                                Utils.Iec61850IEDname = tokens[3];
                            }

                            aicNodeforiec61850Client = new AIConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                        }
                    }
                }
                //Namrata:21/04/2018
                //Namrata: 26/10/2017
                if (e.Node.Text == "AO")
                {
                    if (ucrp is ucAOList)
                    {
                        ucsao = (ucAOList)ucrp;
                        //ucsao.ucAIlist_Load(null, null);
                        if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
                        {
                            Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
                            //Namrata: 04/04/2018
                            List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                            string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                            if (tblName != null)
                            {
                                string[] tokens = tblName.Split('_');
                                Utils.Iec61850IEDname = tokens[3];
                            }
                            aocNodeforiec61850Client = new AOConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                        }
                    }
                }
                //Namrata: 26/10/2017
                if (e.Node.Text == "DI")
                {
                    if (ucrp is ucDIlist)
                    {
                        ucsdi = (ucDIlist)ucrp;
                        ucsdi.ucDIlist_Load(null, null);
                        if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
                        {
                            Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
                            //Namrata: 04/04/2018
                            List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                            string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                            if (tblName != null)
                            {
                                string[] tokens = tblName.Split('_');
                                Utils.Iec61850IEDname = tokens[3];
                            }
                            dicNodeforiec61850Client = new DIConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                        }
                    }
                }
                //Namrata: 26/10/2017
                if (e.Node.Text == "DO")
                {
                    if (ucrp is ucDOlist)
                    {
                        ucsdo = (ucDOlist)ucrp;
                        if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
                        {
                            Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
                            //Namrata: 04/04/2018
                            List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                            string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                            if (tblName != null)
                            {
                                string[] tokens = tblName.Split('_');
                                Utils.Iec61850IEDname = tokens[3];
                            }
                            docNodeforiec61850Client = new DOConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                        }
                    }
                }
                //Namrata: 26/10/2017
                if (e.Node.Text == "EN")
                {
                    if (ucrp is ucENlist)
                    {
                        ucsen = (ucENlist)ucrp;
                        if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
                        {
                            Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
                            //Namrata: 04/04/2018
                            List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                            string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                            if (tblName != null)
                            {
                                string[] tokens = tblName.Split('_');
                                Utils.Iec61850IEDname = tokens[3];
                            }
                            encNodeforiec61850Client = new ENConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                        }
                    }
                }
                if (ucrp is ucGroupIEC104)
                {
                    iec104EventHandled = false; //Ajay: 26/12/2018
                    if (!iec104EventHandled)
                    {
                        ucs104 = (ucGroupIEC104)ucrp;
                        Console.WriteLine("***### boom handle event for 104 slave INI export");
                        ((ucGroupIEC104)ucrp).btnExportINIClick += new System.EventHandler(this.btnExportINI_Click);
                        iec104EventHandled = true;
                    }
                }
                //Namrata:3/7/2017
                if (ucrp is ucGroupIEC101Slave)
                {
                    iec101EventHandled = false; //Ajay: 26/12/2018
                    if (!iec101EventHandled)
                    {
                        ucs101 = (ucGroupIEC101Slave)ucrp;
                        Console.WriteLine("***### boom handle event for 101 slave INI export");
                        ((ucGroupIEC101Slave)ucrp).btnexportIEC101INIClick += new System.EventHandler(this.btnExportINIIEC101_Click);
                        iec101EventHandled = true;
                    }
                }
                //Namrata: 26/09/2017
                if (e.Node.Text == "RCB")
                {
                    //if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
                    //{
                    //    //ucRCB = (ucRCBList)ucrp;
                    //    //ucRCB.ucRCBList_Load(null, null);
                    //    Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
                    //    RCBNodeforiec61850Client = new RCBConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                    //    RCBNodeforiec61850Client.FillRCBList();
                    //}
                    if (ucrp is ucRCBList)
                    {
                        //Namrata: 21/03/2018
                        ucRCB = (ucRCBList)ucrp;
                        ucRCB.ucRCBList_Load(null, null);
                        if (e.Node.Parent.Parent.Parent.Text == "IEC61850 Group")
                        {
                            //ucRCB = (ucRCBList)ucrp;
                            //ucRCB.ucRCBList_Load(null, null);
                            Utils.strFrmOpenproplusTreeNode = e.Node.Parent.Parent.Text;
                            RCBNodeforiec61850Client = new RCBConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                            RCBNodeforiec61850Client.FillRCBList();
                        }
                    }
                }
                this.scParser.Panel2.Controls.Add(ucrp);
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("*** NullReferenceException handled...");
            }
        }
        private void btnExportINIIEC101_Click(object sender, EventArgs e)
        {
            if (ucs101.INIExported) return; //Ajay: 28/12/2018
            if (ucs101 == null) return;
            if (ucs101.lvIEC101Slave.CheckedItems.Count != 1)
            {
                MessageBox.Show("Select single slave for INI export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (xmlFile == null || xmlFile == "")
            {
                MessageBox.Show("Save file before INI export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ListViewItem lvi1 = ucs101.lvIEC101Slave.CheckedItems[0];
            saveINIFile101Slave(lvi1.Text, "IEC101Slave_" + lvi1.Text); //saveINIFile(lvi1.Text, "IEC101_" + lvi1.Text);
        }
        private void btnExportINI_Click(object sender, EventArgs e)
        {
            if (ucs104.INIExported) return; //Ajay: 28/12/2018
            if (ucs104 == null) return;
            if (ucs104.lvIEC104Slave.CheckedItems.Count != 1)
            {
                MessageBox.Show("Select Single Slave For INI Export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (xmlFile == null || xmlFile == "")
            {
                MessageBox.Show("Save File Before INI Export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ListViewItem lvi = ucs104.lvIEC104Slave.CheckedItems[0];
            saveINIFile104Slave(lvi.Text, "IEC104_" + lvi.Text);
        }
        private void saveINIFile104Slave(string slaveNum, string slaveID)
        {
            Console.WriteLine("*** INI file for: {0} {1}", slaveNum, slaveID);
            sfdXMLFile.Filter = "INI Files|*.ini";
            if (sfdXMLFile.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine("*** Saving to file: {0}", sfdXMLFile.FileName);
                writeINIFile(sfdXMLFile.FileName, slaveNum, slaveID);
                //Ajay: 21/11/2017 Show message box with file path  "\"" + sfdXMLFile.FileName + "\"
                MessageBox.Show("\"" + sfdXMLFile.FileName + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ucs104.INIExported = true;
            }
        }
        private void saveINIFile101Slave(string slaveNum, string slaveID)
        {
            Console.WriteLine("*** INI file for: {0} {1}", slaveNum, slaveID);
            sfdXMLFile.Filter = "INI Files|*.ini";
            if (sfdXMLFile.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine("*** Saving to file: {0}", sfdXMLFile.FileName);
                writeINIFile(sfdXMLFile.FileName, slaveNum, slaveID);
                //Ajay: 21/11/2017 Show message box with file path  "\"" + sfdXMLFile.FileName + "\"
                MessageBox.Show("\"" + sfdXMLFile.FileName + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ucs101.INIExported = true;
            }
        }
        private void writeINIFile(string fName, string slaveNum, string slaveID)
        {
            File.WriteAllText(fName, opcHandle.getINIData(xmlFile, slaveNum, slaveID));
        }

        //private void saveXMLFile()
        //{
        //    if (xmlFile == null || xmlFile == "")
        //    {
        //        sfdXMLFile.Filter = "XML Files|*.xml";
        //        sfdXMLFile.Title = "Save XML File";
        //        if (sfdXMLFile.ShowDialog() == DialogResult.OK)
        //        {
        //            xmlFile = sfdXMLFile.FileName;
        //            {
        //                //Namrata: 21/03/2018
        //                Utils.XMLFileIEC61850Client = Path.GetFileName(xmlFile);
        //                Utils.FilenameWithoutExtension = Path.GetFileNameWithoutExtension(xmlFile);
        //                Utils.AppPath = Path.GetDirectoryName(xmlFile);


        //                writeXMLFile(xmlFile);
        //                UpdateXMLFile(xmlFile);
        //                #region Commented
        //                ////Namrata: 20/03/2018
        //                Utils.XMLFileIEC61850Client = Path.GetFileName(xmlFile);
        //                string FilenameWithoutExtension = Path.GetFileNameWithoutExtension(xmlFile);
        //                string appPath = Path.GetDirectoryName(xmlFile);
        //                Utils.ICDFiles = Application.StartupPath + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";
        //                if (!Directory.Exists(Utils.ICDFiles))
        //                {
        //                    Directory.CreateDirectory(Utils.ICDFiles);
        //                    if (!FileExists(Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client))
        //                    {
        //                        File.Move(sfdXMLFile.FileName, Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client);
        //                    }
        //                    else { }
        //                }
        //                else
        //                {
        //                    if (!FileExists(Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client))
        //                    {
        //                        File.Move(sfdXMLFile.FileName, Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client);
        //                    }
        //                    else { }
        //                }
        //                string XMLPath = appPath + @"\" + FilenameWithoutExtension; //Create Folder using XMLFile Name
        //                string ZipFilePath = appPath + @"\" + FilenameWithoutExtension + ".zip"; // create ZipFolder with XMLFile Name
        //                DirectoryInfo src = new DirectoryInfo(Utils.ICDFiles);
        //                DirectoryInfo dest = new DirectoryInfo(XMLPath);
        //                if (!Directory.Exists(XMLPath))
        //                {
        //                    Directory.CreateDirectory(XMLPath);
        //                    CopyDirectory(src, dest);
        //                    ZipFile.CreateFromDirectory(XMLPath, ZipFilePath, CompressionLevel.Optimal, true);
        //                    Directory.Delete(XMLPath, true); //Delete Directory after zip file created
        //                }
        //                #endregion Commented
        //                MessageBox.Show("\"" + xmlFile + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        writeXMLFile(xmlFile);
        //        UpdateXMLFile(xmlFile);
        //        MessageBox.Show("\"" + xmlFile + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        private void saveXMLFile()
        {
            if (xmlFile == null || xmlFile == "")
            {
                sfdXMLFile.Filter = "XML Files|*.xml";
                sfdXMLFile.Title = "Save XML File";
                if (sfdXMLFile.ShowDialog() == DialogResult.OK)
                {
                    xmlFile = sfdXMLFile.FileName;
                    {
                        //Namrata: 21/03/2018
                        Utils.DirectoryNAME = Path.GetDirectoryName(xmlFile); //Get DirectoryName
                        Utils.XMLFileNAME = Path.GetFileName(xmlFile); // Get XML Name
                        Utils.XMLFileFullPath = xmlFile; // XML with full path
                        Utils.XMLFileNameWithoutExtension = Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
                        //Utils.XMLFilePath = sfdXMLFile.FileName;
                        //Utils.XMLFileIEC61850Client = Path.GetFileName(xmlFile);
                        //Utils.FilenameWithoutExtension = Path.GetFileNameWithoutExtension(xmlFile);
                        //Utils.DName = Path.GetDirectoryName(xmlFile);
                        //Utils.a = Path.GetFileNameWithoutExtension(sfdXMLFile.FileName);
                        //Utils.XMLFileIEC61850Client = Path.GetFileName(xmlFile); // XMLFIle name with extension
                        //Utils.FilenameWithoutExtension = Path.GetFileNameWithoutExtension(xmlFile);
                        //Utils.AppPath = Path.GetDirectoryName(xmlFile);
                        writeXMLFile(xmlFile);
                        UpdateXMLFile(xmlFile);
                        if (Utils.IEC61850ServerList.Count > 0)
                        {
                            MessageBox.Show("\"" + Utils.finalxmlfilename + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //MessageBox.Show("\"" + Utils.ZipFilePath + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("\"" + xmlFile + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            else
            {

                //string a = Utils.XMLFileFullPath;
                //Utils.XMLFileFullPath = xmlFile; 


                // XML with full path
                //Utils.XMLFilePath = xmlFile;//sfdXMLFile.FileName;
                //Utils.XMLFileIEC61850Client = Path.GetFileName(xmlFile);
                //Utils.FilenameWithoutExtension = "";
                //// Utils.FilenameWithoutExtension = Path.GetFileNameWithoutExtension(xmlFile);
                ////Namarta:26/04/2018
                //Utils.DName = Path.GetDirectoryName(Utils.XMLFilePath);
                //if (Utils.DName != Utils.AppPath)
                //{

                //    Utils.a = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileIEC61850Client);
                //}
                //else if(Utils.AppPath == "")
                //{
                //    Utils.a = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileIEC61850Client);
                //}
                //else if(Utils.DName == Utils.AppPath)
                //{
                //    Utils.DName = Path.GetDirectoryName(Utils.XMLFilePath);
                //    //Utils.a = Path.GetFileNameWithoutExtension(Utils.XMLFileIEC61850Client);
                //}            {
                //sfdXMLFile.FileName = Utils.DirectoryPath;


                //Utils.AppPath = Path.GetDirectoryName(xmlFile);
                writeXMLFile(xmlFile);
                UpdateXMLFile(xmlFile);
                if (Utils.IEC61850ServerList.Count > 0)
                {
                    MessageBox.Show("\"" + Utils.finalxmlfilename + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //MessageBox.Show("\"" + Utils.ZipFilePath + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("\"" + xmlFile + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //writeXMLFile(xmlFile);
                //UpdateXMLFile(xmlFile);
                //MessageBox.Show("\"" + xmlFile + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                file.MoveTo(Path.Combine(destination.FullName, file.Name));
            }
            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                string destinationDir = Path.Combine(destination.FullName, dir.Name);  //Get destination directory.
                CopyDirectory(dir, new DirectoryInfo(destinationDir)); //Call CopyDirectory() recursively.
            }
        }
        static IEnumerable<string> GetFileSearchPaths(string fileName)
        {
            yield return fileName;
            yield return Path.Combine(Directory.GetParent(Path.GetDirectoryName(fileName)).FullName, Path.GetFileName(fileName));
        }
        static bool FileExists(string fileName)
        {
            return GetFileSearchPaths(fileName).Any(File.Exists);
        }
        static bool FileExists1(string fileName)
        {
            return GetFileSearchPaths1(fileName).Any(File.Exists);
        }
        static IEnumerable<string> GetFileSearchPaths1(string fileName)
        {
            yield return fileName;
            yield return Path.Combine(Directory.GetParent(Path.GetDirectoryName(fileName)).FullName, Path.GetFileName(fileName));
        }

        private void saveAsXMLFile123()
        {
            sfdXMLFile.Filter = "OPP Files (*.oppc)|*.oppc|XML Files|*.xml";// "XML Files|*.xml";
            sfdXMLFile.Title = "Save As XML";
            if (!string.IsNullOrEmpty(xmlFile)) { sfdXMLFile.FileName = xmlFile; Utils.XMLOldFileName = xmlFile; }
            if (sfdXMLFile.ShowDialog() == DialogResult.OK)
            {
                //Namrata: 21/03/2018
                Utils.XMLFilePath = sfdXMLFile.FileName;
                string exten = Path.ChangeExtension(Utils.XMLFilePath, ".xml");
                sfdXMLFile.FileName = exten;
                Utils.XMLFilePath = sfdXMLFile.FileName;
                Utils.XMLFileIEC61850Client = Path.GetFileName(Utils.XMLFilePath);
                Utils.FilenameWithoutExtension = Path.GetFileNameWithoutExtension(Utils.XMLFilePath);
                Utils.AppPath = Path.GetDirectoryName(Utils.XMLFilePath);
                writeXMLFile(sfdXMLFile.FileName);
                UpdateXMLFile(sfdXMLFile.FileName);
                Utils.XMLUpdatedFileName = sfdXMLFile.FileName;
                //Namrata: 29/01/2018
                MyMruList = new MruList(Application.ProductName, this.recentFilesToolStripMenuItem, 10, sfdXMLFile.FileName, this.myOwnRecentFilesGotCleared_handler);
                MyMruList.FileSelected += MyMruList_FileSelected;
                if (Utils.IEC61850ServerList.Count > 0)
                {
                }
                else
                {
                    MessageBox.Show("\"" + sfdXMLFile.FileName + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void saveAsXMLFile1()
        {
            sfdXMLFile.Filter = "XML Files|*.xml";
            sfdXMLFile.Title = "Save As XML";
            if (!string.IsNullOrEmpty(xmlFile)) { sfdXMLFile.FileName = xmlFile; Utils.XMLOldFileName = xmlFile; }
            if (sfdXMLFile.ShowDialog() == DialogResult.OK)
            {
                //Namrata: 21/03/2018
                Utils.XMLFilePath = sfdXMLFile.FileName; // XML with full path
                //Namarta:26/04/2018
                Utils.DName = Path.GetDirectoryName(Utils.XMLFilePath);
                Utils.a = Path.GetFileNameWithoutExtension(sfdXMLFile.FileName);
                Utils.XMLFileIEC61850Client = Path.GetFileName(Utils.XMLFilePath); // XMLFIle name with extension
                Utils.FilenameWithoutExtension = Path.GetFileNameWithoutExtension(Utils.XMLFilePath);
                Utils.AppPath = Path.GetDirectoryName(Utils.XMLFilePath);
                writeXMLFile(sfdXMLFile.FileName);
                UpdateXMLFile(sfdXMLFile.FileName);
                Utils.XMLUpdatedFileName = sfdXMLFile.FileName;
                //Namrata: 29/01/2018
                MyMruList = new MruList(Application.ProductName, this.recentFilesToolStripMenuItem, 10, sfdXMLFile.FileName, this.myOwnRecentFilesGotCleared_handler);
                MyMruList.FileSelected += MyMruList_FileSelected;
                if (Utils.IEC61850ServerList.Count > 0)
                {
                    MessageBox.Show("\"" + Utils.finalxmlfilename + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("\"" + sfdXMLFile.FileName + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void saveAsXMLFile()
        {
            sfdXMLFile.Filter = "XML Files|*.xml";
            sfdXMLFile.Title = "Save As XML";
            if (!string.IsNullOrEmpty(xmlFile)) { sfdXMLFile.FileName = xmlFile; Utils.XMLOldFileName = xmlFile; }
            if (sfdXMLFile.ShowDialog() == DialogResult.OK)
            {
                //Namrata: 27/04/2018
                Utils.DirectoryNAME = Path.GetDirectoryName(sfdXMLFile.FileName); //Get DirectoryName
                Utils.XMLFileNAME = Path.GetFileName(sfdXMLFile.FileName); // Get XML Name
                Utils.XMLFileFullPath = sfdXMLFile.FileName; // XML with full path
                Utils.XMLFileNameWithoutExtension = Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
                writeXMLFile(sfdXMLFile.FileName);
                UpdateXMLFile(sfdXMLFile.FileName);
                Utils.XMLUpdatedFileName = sfdXMLFile.FileName;
                //Namrata: 29/01/2018
                MyMruList = new MruList(Application.ProductName, this.recentFilesToolStripMenuItem, 10, sfdXMLFile.FileName, this.myOwnRecentFilesGotCleared_handler);
                MyMruList.FileSelected += MyMruList_FileSelected;
                if (Utils.IEC61850ServerList.Count > 0)
                {
                    MessageBox.Show("\"" + Utils.finalxmlfilename + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("\"" + sfdXMLFile.FileName + "\" saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void writeXMLFile1(string wFile)
        {
            File.WriteAllText(wFile, opcHandle.getXMLData());

            #region For IEC61850Client
            //Namrata: 21/03/2018
            if (Utils.IEC61850ServerList.Count > 0)
            {
                XmlDocument Xmldoc = new XmlDocument();
                Utils.XMLFilecopy = wFile;
                Xmldoc.Load(Utils.XMLFilecopy);
                Utils.ICDFiles = System.IO.Path.GetTempPath() + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";// Application.StartupPath + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";
                string fileName = string.Empty;
                string destFile = string.Empty;
                string XMLPath = Utils.AppPath + @"\" + Utils.FilenameWithoutExtension;
                string ZipFilePath = Utils.AppPath + @"\" + Utils.FilenameWithoutExtension;
                string FolderNamewithoutexstension = Path.GetDirectoryName(Utils.XMLFilePath) + @"\" + Utils.ExtractedFileName;

                if (!Directory.Exists(Utils.ICDFiles))
                {
                    Directory.CreateDirectory(Utils.ICDFiles);
                    if (System.IO.Directory.Exists(Utils.ICDFiles))
                    {
                        string[] files = System.IO.Directory.GetFiles(Utils.ICDFiles);//Get Files From Directory
                        foreach (string s in files)
                        {
                            fileName = System.IO.Path.GetFileName(s);
                            destFile = System.IO.Path.Combine(Utils.ICDFiles, fileName);
                            File.Move(Utils.XMLFilePath, Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client);
                        }
                    }
                    else { }
                    //if (!FileExists(Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client))
                    //{
                    //    File.Move(Utils.XMLFilePath, Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client);
                    //}
                    //else { }
                }
                else
                {
                    if (!FileExists(Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client))
                    {
                        File.Move(Utils.XMLFilePath, Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client);
                    }
                    //if (FileExists(Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client))
                    //{
                    //    if (System.IO.Directory.Exists(Utils.ICDFiles))
                    //    {
                    //        string[] files = System.IO.Directory.GetFiles(Utils.ICDFiles);//Get Files From Directory

                    //        foreach (string s in files)
                    //        {
                    //            fileName = System.IO.Path.GetFileName(s);
                    //            //destFile = System.IO.Path.Combine(Utils.ICDFiles, fileName);
                    //            if (FileExists1(FolderNamewithoutexstension + @"\" + fileName))
                    //            {
                    //                File.Delete(fileName);
                    //            }
                    //            else
                    //            {
                    //            }
                    //        }
                    //    }
                    //    else { }
                    //}
                }
                if (Utils.ExtractedFileName != "")
                {
                    FileDirectoryOperations FileDirectoryOperations = new FileDirectoryOperations();
                    if (FileDirectoryOperations.CompressDirectory(Utils.ICDFiles, FolderNamewithoutexstension + ".oppc")) ;
                    MessageBox.Show(FolderNamewithoutexstension + ".oppc" + " is saved successfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    FileDirectoryOperations FileDirectoryOperations = new FileDirectoryOperations();
                    if (FileDirectoryOperations.CompressDirectory(Utils.ICDFiles, ZipFilePath + ".oppc")) ;
                    MessageBox.Show(ZipFilePath + ".oppc" + " is saved successfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Namrata: 14/04/2018
                if (Directory.Exists(Utils.ICDFiles))
                {
                    FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                    FileDirectoryOperations.DeleteDirectory(Utils.ICDFiles);
                }
                //Namrata: 12/04/2018
                //FileDirectoryOperations FileDirectoryOperations = new FileDirectoryOperations();
                //if (FileDirectoryOperations.CompressDirectory(Utils.ICDFiles, ZipFilePath + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration" + ".oppc")) ;
                //if (!Directory.Exists(XMLPath))
                //{
                //    //Directory.CreateDirectory(XMLPath);
                //    CopyDirectory(src, dest);
                //    ZipFile.CreateFromDirectory(XMLPath, ZipFilePath, CompressionLevel.Optimal, true);
                //    if (FileDirectoryOperations.CompressDirectory(XMLPath, ZipFilePath + ".oppc")) ;
                //    //Directory.Delete(XMLPath, true); //Delete Directory after zip file created
                //}
            }
            #endregion For IEC61850Client
        }
        string FolderNamewithoutexstension = ""; string XMLPath = ""; string CreateDirectory = "";
        private void writeXMLFile1pp(string wFile)
        {
            File.WriteAllText(wFile, opcHandle.getXMLData());

            #region For IEC61850Client
            //Namrata: 21/03/2018
            if (Utils.IEC61850ServerList.Count > 0)
            {
                XmlDocument Xmldoc = new XmlDocument();
                Utils.XMLFilecopy = wFile;
                Xmldoc.Load(Utils.XMLFilecopy);
                Utils.AppPath = Path.GetDirectoryName(wFile);
                Utils.ICDFiles = System.IO.Path.GetTempPath() + @"\" + "Protocol" + @"\" + "IEC61850Client";//System.IO.Path.GetTempPath() + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";
                CreateDirectory = Utils.DName + @"\" + Utils.a + @"\" + "Protocol" + @"\" + "IEC61850Client"; //Create DirectoryName
                string fileName = string.Empty;
                string destFile = string.Empty;

                if (Utils.FilenameWithoutExtension != "")
                {
                    XMLPath = Utils.AppPath + @"\" + Utils.FilenameWithoutExtension;
                }
                else
                {
                    XMLPath = Utils.AppPath;
                }

                //Namrata: 21/04/2018
                string updatedXMLFile = Path.GetFileNameWithoutExtension(wFile);
                bool NewRecordingExists;
                string[] fileNames = Directory.GetFiles(Utils.ICDFiles, "*.xml", SearchOption.TopDirectoryOnly);
                if (fileNames.Length != 0)
                {
                    NewRecordingExists = true;
                    foreach (string fileName1 in fileNames)
                    {
                        File.Delete(fileNames[0]);
                    }
                }
                else
                {
                    NewRecordingExists = false;
                }

                if (Utils.XMLFilePath != "")
                {
                    FolderNamewithoutexstension = Path.GetDirectoryName(Utils.XMLFilePath) + @"\" + Utils.ExtractedFileName;
                    //Namrata:26/04/2018
                    //Utils.XMLFilePath = FolderNamewithoutexstension;
                }
                if (!Directory.Exists(Utils.ICDFiles))
                {
                    Directory.CreateDirectory(Utils.ICDFiles);
                    if (System.IO.Directory.Exists(Utils.ICDFiles))
                    {
                        string[] files = System.IO.Directory.GetFiles(Utils.ICDFiles);//Get Files From Directory
                        foreach (string s in files)
                        {
                            fileName = System.IO.Path.GetFileName(s);
                            destFile = System.IO.Path.Combine(Utils.ICDFiles, fileName);
                            //File.Copy(Utils.XMLFilePath, Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client);
                            File.Move(Utils.XMLFilePath, Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client);
                        }
                    }
                    else { }
                }
                else
                {
                    if (!FileExists(Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client))
                    {
                        File.Move(Utils.XMLFilePath, Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client);
                        //File.Copy(Utils.XMLFilePath, Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client);
                    }
                }

                //Namrata: 26/04/2018
                //As per Naina Requirement
                //string DirectoryName = Path.GetDirectoryName(wFile); //Get DirectoryName
                //string XMLFileName = Path.GetFileNameWithoutExtension(wFile); //Get only XMLFilename
                string GetFilewithextension = Path.GetFileName(wFile);
                Utils.finalxmlfilename = Utils.DName + @"\" + Utils.a + @"\" + GetFilewithextension;
                //if (Utils.a != "")
                //{
                //    CreateDirectory = Utils.DName + @"\" + Utils.a + @"\" + "Protocol" + @"\" + "IEC61850Client"; //Create DirectoryName
                //}
                //else
                //{
                //    CreateDirectory = Utils.DName + @"\" + "Protocol" + @"\" + "IEC61850Client";
                //}

                string MoveXMLFiles = Utils.DName + @"\" + Utils.a; // For Moving XMLFile
                if (!Directory.Exists(XMLPath))
                {
                    Directory.CreateDirectory(XMLPath);
                    string[] fileEntries1 = Directory.GetFiles(Utils.ICDFiles, "*.xml");
                    string filexmlName = string.Empty;
                    string destxmlFile = string.Empty;
                    foreach (string s in fileEntries1)
                    {
                        filexmlName = System.IO.Path.GetFileName(s);
                        destxmlFile = System.IO.Path.Combine(MoveXMLFiles, filexmlName);
                        if (File.Exists(destxmlFile))
                        {
                            System.IO.File.Delete(destxmlFile);
                        }
                        System.IO.File.Copy(s, destxmlFile, true);
                        //System.IO.File.Move(s, destxmlFile);
                    }
                    //Create folders in folder
                    if (!Directory.Exists(CreateDirectory))
                    {
                        Directory.CreateDirectory(CreateDirectory);
                        string[] fileEntries123 = Directory.GetFiles(Utils.ICDFiles, "*.icd");
                        string filexmlName1 = string.Empty;
                        string destxmlFile1 = string.Empty;
                        foreach (string s in fileEntries123)
                        {
                            filexmlName1 = System.IO.Path.GetFileName(s);
                            destxmlFile1 = System.IO.Path.Combine(CreateDirectory, filexmlName1);
                            if (File.Exists(destxmlFile1))
                            {
                                System.IO.File.Delete(destxmlFile1);
                            }
                            System.IO.File.Copy(s, destxmlFile1);
                            //System.IO.File.Move(s, destxmlFile1);
                        }
                    }

                }
                else
                {
                    string[] fileEntriesxml = Directory.GetFiles(Utils.ICDFiles, "*.xml");
                    string filexmlNameXML = string.Empty;
                    string destxmlFileXML = string.Empty;
                    foreach (string s in fileEntriesxml)
                    {
                        filexmlNameXML = System.IO.Path.GetFileName(s);
                        destxmlFileXML = System.IO.Path.Combine(MoveXMLFiles, filexmlNameXML);
                        if (File.Exists(destxmlFileXML))
                        {
                            System.IO.File.Delete(destxmlFileXML);
                        }
                        System.IO.File.Copy(s, destxmlFileXML);
                    }
                    if (!Directory.Exists(CreateDirectory))
                    {
                        Directory.CreateDirectory(CreateDirectory);
                        string[] fileEntries123 = Directory.GetFiles(Utils.ICDFiles, "*.icd");
                        string filexmlName1 = string.Empty;
                        string destxmlFile1 = string.Empty;
                        foreach (string s in fileEntries123)
                        {
                            filexmlName1 = System.IO.Path.GetFileName(s);
                            destxmlFile1 = System.IO.Path.Combine(CreateDirectory, filexmlName1);
                            if (File.Exists(destxmlFile1))
                            {
                                System.IO.File.Delete(destxmlFile1);
                            }
                            System.IO.File.Copy(s, destxmlFile1);
                        }
                    }
                    else
                    {
                        string[] fileEntries1234 = Directory.GetFiles(Utils.ICDFiles, "*.icd");
                        string filexmlName14 = string.Empty;
                        string destxmlFile14 = string.Empty;
                        foreach (string s in fileEntries1234)
                        {
                            filexmlName14 = System.IO.Path.GetFileName(s);
                            destxmlFile14 = System.IO.Path.Combine(CreateDirectory, filexmlName14);
                            if (File.Exists(destxmlFile14))
                            {
                                System.IO.File.Delete(destxmlFile14);
                            }
                            System.IO.File.Copy(s, destxmlFile14);
                        }
                    }
                }
                //if (!Directory.Exists(createfolder))
                //{
                //    Directory.CreateDirectory(createfolder);
                //    if (System.IO.Directory.Exists(createfolder))
                //    {
                //        string[] fileEntries123 = Directory.GetFiles(Utils.ICDFiles, "*.icd");
                //        string filexmlName1 = string.Empty;
                //        string destxmlFile1 = string.Empty;
                //        foreach (string s in fileEntries123)
                //        {
                //            filexmlName1 = System.IO.Path.GetFileName(s);
                //            destxmlFile1 = System.IO.Path.Combine(createfolder, filexmlName1);
                //            System.IO.File.Move(s, destxmlFile1);
                //            //System.IO.File.Copy(s, destxmlFile1, true);
                //        }
                //    }
                //}
                //else if (Directory.Exists(createfolder))
                //{

                //    string[] fileEntries123 = Directory.GetFiles(Utils.ICDFiles, "*.icd");
                //    string filexmlName1 = string.Empty;
                //    string destxmlFile1 = string.Empty;
                //    foreach (string s in fileEntries123)
                //    {
                //        filexmlName1 = System.IO.Path.GetFileName(s);
                //        destxmlFile1 = System.IO.Path.Combine(createfolder, filexmlName1);
                //        //first, delete target file if exists, as File.Move() does not support overwrite
                //        if (File.Exists(destxmlFile1))
                //        {
                //            System.IO.File.Delete(destxmlFile1);
                //        }
                //        System.IO.File.Move(s, destxmlFile1);

                //        //System.IO.File.Copy(s, destxmlFile1, true);
                //    }

                //}
            }
            #endregion For IEC61850Client
        }

        private void writeXMLFile(string wFile)
        {
            File.WriteAllText(wFile, opcHandle.getXMLData());

            #region For IEC61850Client
            //Namrata: 21/03/2018
            if (Utils.IEC61850ServerList.Count > 0)
            {
                XmlDocument Xmldoc = new XmlDocument();
                Utils.XMLFilecopy = wFile;
                Xmldoc.Load(Utils.XMLFilecopy);
                Utils.ICDFiles = System.IO.Path.GetTempPath() + @"\" + "Protocol" + @"\" + "IEC61850Client";//System.IO.Path.GetTempPath() + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";

                if (Utils.XMLFileNameWithoutExtension != "")
                {
                    Utils.CreateXMLFolder = Utils.DirectoryNAME + @"\" + Utils.XMLFileNameWithoutExtension + @"\" + "Protocol" + @"\" + "IEC61850Client"; //Create DirectoryName
                }
                else
                {
                    Utils.CreateXMLFolder = Utils.DirectoryNAME + @"\" + "Protocol" + @"\" + "IEC61850Client"; //Create DirectoryName
                }
                string fileName = string.Empty;
                string destFile = string.Empty;
                XMLPath = Utils.DirectoryNAME + @"\" + Utils.XMLFileNameWithoutExtension;
                //Namrata: 21/04/2018
                string updatedXMLFile = Path.GetFileNameWithoutExtension(wFile);
                bool NewRecordingExists;
                string[] fileNames = Directory.GetFiles(Utils.ICDFiles, "*.xml", SearchOption.TopDirectoryOnly);
                if (fileNames.Length != 0)
                {
                    NewRecordingExists = true;
                    foreach (string fileName1 in fileNames)
                    {
                        File.Delete(fileNames[0]);
                    }
                }
                else
                {
                    NewRecordingExists = false;
                }
                if (Utils.XMLFilePath != "")
                {
                    FolderNamewithoutexstension = Path.GetDirectoryName(Utils.XMLFilePath) + @"\" + Utils.ExtractedFileName;

                }
                if (!Directory.Exists(Utils.ICDFiles))
                {
                    Directory.CreateDirectory(Utils.ICDFiles);
                    if (System.IO.Directory.Exists(Utils.ICDFiles))
                    {
                        string[] files = System.IO.Directory.GetFiles(Utils.ICDFiles);//Get Files From Directory
                        foreach (string s in files)
                        {
                            fileName = System.IO.Path.GetFileName(s);
                            destFile = System.IO.Path.Combine(Utils.ICDFiles, fileName);
                            //File.Copy(Utils.XMLFilePath, Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client);
                            File.Move(Utils.XMLFileFullPath, Utils.ICDFiles + @"\" + Utils.XMLFileNameWithoutExtension);
                        }
                    }
                    else { }
                }
                else
                {
                    if (!FileExists(Utils.ICDFiles + @"\" + Utils.XMLFileNAME))
                    {
                        File.Move(Utils.XMLFileFullPath, Utils.ICDFiles + @"\" + Utils.XMLFileNAME);
                        //File.Copy(Utils.XMLFilePath, Utils.ICDFiles + @"\" + Utils.XMLFileIEC61850Client);
                    }
                }
                //Namrata: 26/04/2018
                //As per Naina Requirement
                string GetFilewithextension = Path.GetFileName(wFile);
                string MoveXMLFiles = Utils.DirectoryNAME + @"\" + Utils.XMLFileNameWithoutExtension; // For Moving XMLFile
                if (!Directory.Exists(XMLPath))
                {
                    Directory.CreateDirectory(XMLPath);
                    string[] fileEntries1 = Directory.GetFiles(Utils.ICDFiles, "*.xml");
                    string filexmlName = string.Empty;
                    string destxmlFile = string.Empty;
                    foreach (string s in fileEntries1)
                    {
                        filexmlName = System.IO.Path.GetFileName(s);
                        destxmlFile = System.IO.Path.Combine(MoveXMLFiles, filexmlName);
                        if (File.Exists(destxmlFile))
                        {
                            System.IO.File.Delete(destxmlFile);
                        }
                        System.IO.File.Copy(s, destxmlFile, true);
                        //System.IO.File.Move(s, destxmlFile);
                    }
                    //Create folders in folder
                    if (!Directory.Exists(Utils.CreateXMLFolder))
                    {
                        Directory.CreateDirectory(Utils.CreateXMLFolder);
                        string[] fileEntries123 = Directory.GetFiles(Utils.ICDFiles, "*.icd");
                        string filexmlName1 = string.Empty;
                        string destxmlFile1 = string.Empty;
                        foreach (string s in fileEntries123)
                        {
                            filexmlName1 = System.IO.Path.GetFileName(s);
                            destxmlFile1 = System.IO.Path.Combine(Utils.CreateXMLFolder, filexmlName1);
                            if (File.Exists(destxmlFile1))
                            {
                                System.IO.File.Delete(destxmlFile1);
                            }
                            System.IO.File.Copy(s, destxmlFile1);
                            //System.IO.File.Move(s, destxmlFile1);
                        }
                    }

                }
                else
                {
                    string[] fileEntriesxml = Directory.GetFiles(Utils.ICDFiles, "*.xml");
                    string filexmlNameXML = string.Empty;
                    string destxmlFileXML = string.Empty;
                    foreach (string s in fileEntriesxml)
                    {
                        filexmlNameXML = System.IO.Path.GetFileName(s);
                        destxmlFileXML = System.IO.Path.Combine(MoveXMLFiles, filexmlNameXML);
                        if (File.Exists(destxmlFileXML))
                        {
                            System.IO.File.Delete(destxmlFileXML);
                        }
                        System.IO.File.Copy(s, destxmlFileXML);
                    }
                    if (!Directory.Exists(Utils.CreateXMLFolder))
                    {
                        Directory.CreateDirectory(Utils.CreateXMLFolder);
                        string[] fileEntries123 = Directory.GetFiles(Utils.ICDFiles, "*.icd");
                        string filexmlName1 = string.Empty;
                        string destxmlFile1 = string.Empty;
                        foreach (string s in fileEntries123)
                        {
                            filexmlName1 = System.IO.Path.GetFileName(s);
                            destxmlFile1 = System.IO.Path.Combine(Utils.CreateXMLFolder, filexmlName1);
                            if (File.Exists(destxmlFile1))
                            {
                                System.IO.File.Delete(destxmlFile1);
                            }
                            System.IO.File.Copy(s, destxmlFile1);
                        }
                    }
                    else
                    {
                        string[] fileEntries1234 = Directory.GetFiles(Utils.ICDFiles, "*.icd");
                        string filexmlName14 = string.Empty;
                        string destxmlFile14 = string.Empty;
                        foreach (string s in fileEntries1234)
                        {
                            filexmlName14 = System.IO.Path.GetFileName(s);
                            destxmlFile14 = System.IO.Path.Combine(Utils.CreateXMLFolder, filexmlName14);
                            if (File.Exists(destxmlFile14))
                            {
                                System.IO.File.Delete(destxmlFile14);
                            }
                            System.IO.File.Copy(s, destxmlFile14);
                        }
                    }
                }
                Utils.finalxmlfilename = MoveXMLFiles + @"\" + GetFilewithextension;
                if (Utils.finalxmlfilename.Contains("\\\\"))
                {
                    string Removebackslash = Utils.finalxmlfilename.Replace("\\\\", "\\");
                    Utils.finalxmlfilename = Removebackslash;
                }
            }
            #endregion For IEC61850Client
        }
        OpenFileDialog fd = new OpenFileDialog();
        private void openXMLFile11()
        {
            string strRoutineName = "";
            try
            {
                ofdXMLFile.DefaultExt = ".xml|.oppc";
                ofdXMLFile.Filter = "OPP Files (*.oppc)|*.oppc|XML Files|*.xml";
                //ofdXMLFile.Filter = "XML Files|*.xml|*.oppc";
                //ofdXMLFile.Filter = "XML Files|*.xml|ZIP|*.zip";
                ofdXMLFile.FilterIndex = 1;
                ofdXMLFile.RestoreDirectory = true;
                ofdXMLFile.Title = "Browse File";
                if (ofdXMLFile.ShowDialog() == DialogResult.OK)
                {
                    #region IEC61850Client
                    //Namrata: 13/04/2018
                    FileDirectoryOperations.DecompressToDirectory(ofdXMLFile.FileName, Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration");
                    string XMLFilePathFinal = ofdXMLFile.FileName;
                    Utils.XMLPath = XMLFilePathFinal; //Namrata: 18/04/2018
                    Utils.DirectoryPath = ofdXMLFile.FileName;
                    string DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration";
                    string DirectoryLocation = Path.GetDirectoryName(ofdXMLFile.FileName);
                    string DirectoryExtention = Path.GetExtension(ofdXMLFile.FileName);
                    Utils.ExtractedFileName = Path.GetFileNameWithoutExtension(ofdXMLFile.FileName);
                    if (DirectoryExtention == ".oppc")
                    {
                        //Namrata: 29/03/2018
                        #region ClearDatasets
                        Utils.DsRCB.Clear();
                        Utils.DsRCBData.Clear();
                        Utils.dsResponseType.Clear();
                        Utils.DsResponseType.Clear();
                        Utils.dsIED.Clear();
                        Utils.dsIEDName.Clear();
                        Utils.DsAllConfigurationData.Clear();
                        Utils.DsAllConfigureData.Clear();
                        Utils.DsRCBDataset.Clear();
                        Utils.DsRCBDS.Clear();
                        Utils.DtRCBdata.Clear();
                        #endregion ClearDatasets
                        Utils.ICDFiles = System.IO.Path.GetTempPath() + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration"; //Application.StartupPath + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";
                        string fileName = string.Empty;
                        string destFile = string.Empty;
                        if (Directory.Exists(Utils.ICDFiles))
                        {
                            DeleteDirectory(Utils.ICDFiles);
                        }
                        if (!Directory.Exists(Utils.ICDFiles))
                        {
                            Directory.CreateDirectory(Utils.ICDFiles);
                            if (System.IO.Directory.Exists(DirectoryPath))
                            {
                                string[] files = System.IO.Directory.GetFiles(DirectoryPath);//Get Files From Directory
                                foreach (string s in files)
                                {
                                    fileName = System.IO.Path.GetFileName(s);
                                    destFile = System.IO.Path.Combine(Utils.ICDFiles, fileName);
                                    System.IO.File.Copy(s, destFile, true);
                                }
                            }
                            else
                            {
                            }
                            string[] fileEntries = Directory.GetFiles(Utils.ICDFiles, "*.xml");
                            string XMLFilePath = fileEntries[0].ToString();
                            ofdXMLFile.FileName = XMLFilePath;
                            string a = Path.GetFileName(XMLFilePath);
                            string openedFile = ofdXMLFile.FileName; // Namrata: 18/11/2017
                            this.MyMruList.AddFile(Utils.XMLPath); //Namrata: 18/04/2018
                            //this.MyMruList.AddFile(openedFile);//Now give it to the MRUManager
                            int result = 0;
                            string errMsg = "XML file is valid...";
                            ResetConfiguratorState(false);
                            showLoading();
                            xmlFile = "";//reset old filename...
                            pnlValidationMessages.Visible = true;
                            ListViewItem lvi;
                            lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                            //Namrata: 27/7/2017
                            toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                            tspFileName.Text = Utils.DirectoryPath;//ofdXMLFile.FileName;
                            lvValidationMessages.Items.Add(lvi);
                            lvi = new ListViewItem("");
                            lvValidationMessages.Items.Add(lvi);
                            result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                            if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                            else
                            {
                                pnlValidationMessages.Visible = true;
                                pnlValidationMessages.BringToFront();
                            }
                            if (result == -1) errMsg = "File doesnot exist!!!";
                            else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                            else if (result == -3) errMsg = "XSD file is not valid!!!";
                            else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                            else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                            lvi = new ListViewItem("");
                            lvValidationMessages.Items.Add(lvi);
                            lvi = new ListViewItem(errMsg);
                            lvValidationMessages.Items.Add(lvi);
                            lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                            if (result < 0)
                            {
                                hideLoading();
                                MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            xmlFile = ofdXMLFile.FileName;//Assign only after loading file...
                            TreeNode searchNode = tvItems.Nodes[0].Nodes[5]; //Namrata: 19/12/2017
                            if (searchNode != null)
                                searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                            tvItems.SelectedNode = tvItems.Nodes[0];
                            tvItems.Nodes[0].EnsureVisible();
                            hideLoading();
                        }
                    }
                    #endregion IEC61850Client
                    else
                    {
                        string openedFile = ofdXMLFile.FileName;  //Namrata: 18/11/2017
                        this.MyMruList.AddFile(openedFile);  //Now give it to the MRUManager
                        int result = 0;
                        string errMsg = "XML file is valid...";
                        ResetConfiguratorState(false);
                        showLoading();
                        xmlFile = "";  //reset old filename...
                        pnlValidationMessages.Visible = true;
                        ListViewItem lvi;
                        lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                        //Namrata: 27/7/2017
                        toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                        tspFileName.Text = ofdXMLFile.FileName;
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                        if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                        else
                        {
                            pnlValidationMessages.Visible = true;
                            pnlValidationMessages.BringToFront();
                        }
                        if (result == -1) errMsg = "File doesnot exist!!!";
                        else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                        else if (result == -3) errMsg = "XSD file is not valid!!!";
                        else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                        else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem(errMsg);
                        lvValidationMessages.Items.Add(lvi);
                        lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                        if (result < 0)
                        {
                            hideLoading();
                            MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
                        TreeNode searchNode = tvItems.Nodes[0].Nodes[5]; //Namrata: 19/12/2017
                        if (searchNode != null)
                            searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                        tvItems.SelectedNode = tvItems.Nodes[0];
                        tvItems.Nodes[0].EnsureVisible();
                        hideLoading();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void openXMLFilewithoppc()
        {
            string strRoutineName = "";
            try
            {
                ofdXMLFile.DefaultExt = ".xml|.oppc";
                ofdXMLFile.Filter = "OPP Files (*.oppc)|*.oppc|XML Files|*.xml";
                ofdXMLFile.FilterIndex = 1;
                ofdXMLFile.RestoreDirectory = true;
                ofdXMLFile.Title = "Browse File";
                if (ofdXMLFile.ShowDialog() == DialogResult.OK)
                {
                    #region IEC61850Client
                    //Namrata: 13/04/2018
                    string DirectoryExtention = Path.GetExtension(ofdXMLFile.FileName);
                    //Utils.ExtractedFileName = Path.GetFileNameWithoutExtension(ofdXMLFile.FileName);
                    if (DirectoryExtention == ".oppc")
                    {
                        //FileDirectoryOperations.DecompressToDirectory(ofdXMLFile.FileName, Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration");
                        //string DecompressDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration";
                        //string XMLMoveLocation = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName);

                        FileDirectoryOperations.DecompressToDirectory(ofdXMLFile.FileName, Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "Protocol" + "\\" + "IEC61850Client");
                        string DecompressDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "Protocol" + "\\" + "IEC61850Client";
                        string XMLMoveLocation = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName);
                        string XMLFilePathFinal = ofdXMLFile.FileName;
                        Utils.XMLPath = XMLFilePathFinal; //Namrata: 18/04/2018
                        Utils.DirectoryPath = ofdXMLFile.FileName;
                        string DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "Protocol" + "\\" + "IEC61850Client";
                        //string DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration";
                        string DirectoryLocation = Path.GetDirectoryName(ofdXMLFile.FileName);
                        //string DirectoryExtention = Path.GetExtension(ofdXMLFile.FileName);
                        Utils.ExtractedFileName = Path.GetFileNameWithoutExtension(ofdXMLFile.FileName);
                        //Namrata: 29/03/2018
                        #region ClearDatasets
                        Utils.DsRCB.Clear();
                        Utils.DsRCBData.Clear();
                        Utils.dsResponseType.Clear();
                        Utils.DsResponseType.Clear();
                        Utils.dsIED.Clear();
                        Utils.dsIEDName.Clear();
                        Utils.DsAllConfigurationData.Clear();
                        Utils.DsAllConfigureData.Clear();
                        Utils.DsRCBDataset.Clear();
                        Utils.DsRCBDS.Clear();
                        Utils.DtRCBdata.Clear();
                        #endregion ClearDatasets
                        Utils.ICDFiles = System.IO.Path.GetTempPath() + @"\" + "Protocol";
                        //Utils.ICDFiles = System.IO.Path.GetTempPath() + @"\" + "Protocol" + @"\" + "IEC61850Client"; //Application.StartupPath + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";
                        string fileName = string.Empty;
                        string destFile = string.Empty;
                        if (Directory.Exists(Utils.ICDFiles))
                        {
                            DeleteDirectory(Utils.ICDFiles);
                        }
                        if (!Directory.Exists(Utils.ICDFiles))
                        {
                            Directory.CreateDirectory(Utils.ICDFiles);
                            if (System.IO.Directory.Exists(DirectoryPath))
                            {
                                string[] files = System.IO.Directory.GetFiles(DirectoryPath);//Get Files From Directory
                                foreach (string s in files)
                                {
                                    fileName = System.IO.Path.GetFileName(s);
                                    destFile = System.IO.Path.Combine(Utils.ICDFiles, fileName);
                                    System.IO.File.Copy(s, destFile, true);
                                }
                            }
                            else
                            {
                            }
                            //Namrata:21/04/2018
                            string[] fileEntries1 = Directory.GetFiles(DecompressDirectory, "*.xml");
                            string fileName1 = string.Empty;
                            string destFile1 = string.Empty;
                            foreach (string s in fileEntries1)
                            {
                                fileName1 = System.IO.Path.GetFileName(s);
                                destFile1 = System.IO.Path.Combine(XMLMoveLocation, fileName1);
                                System.IO.File.Move(s, destFile1);
                            }
                            string[] fileEntries = Directory.GetFiles(Utils.ICDFiles, "*.xml");
                            //Namrata:21/04/2018
                            foreach (string s in fileEntries)
                            {
                                fileName = System.IO.Path.GetFileName(s);
                                destFile = System.IO.Path.Combine(XMLMoveLocation, fileName);
                                System.IO.File.Copy(s, destFile, true);
                            }
                            string XMLFilePath = fileEntries[0].ToString();
                            ofdXMLFile.FileName = XMLFilePath;
                            string a = Path.GetFileName(XMLFilePath);
                            string openedFile = ofdXMLFile.FileName; // Namrata: 18/11/2017
                            this.MyMruList.AddFile(Utils.XMLPath); //Namrata: 18/04/2018
                            //this.MyMruList.AddFile(openedFile);//Now give it to the MRUManager
                            int result = 0;
                            string errMsg = "XML file is valid...";
                            ResetConfiguratorState(false);
                            showLoading();
                            xmlFile = "";//reset old filename...
                            pnlValidationMessages.Visible = true;
                            ListViewItem lvi;
                            lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                            //Namrata: 27/7/2017
                            toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                            tspFileName.Text = Utils.DirectoryPath;//ofdXMLFile.FileName;
                            lvValidationMessages.Items.Add(lvi);
                            lvi = new ListViewItem("");
                            lvValidationMessages.Items.Add(lvi);
                            result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                            if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                            else
                            {
                                pnlValidationMessages.Visible = true;
                                pnlValidationMessages.BringToFront();
                            }
                            if (result == -1) errMsg = "File doesnot exist!!!";
                            else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                            else if (result == -3) errMsg = "XSD file is not valid!!!";
                            else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                            else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                            lvi = new ListViewItem("");
                            lvValidationMessages.Items.Add(lvi);
                            lvi = new ListViewItem(errMsg);
                            lvValidationMessages.Items.Add(lvi);
                            lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                            if (result < 0)
                            {
                                hideLoading();
                                MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            xmlFile = ofdXMLFile.FileName;//Assign only after loading file...
                            TreeNode searchNode = tvItems.Nodes[0].Nodes[5]; //Namrata: 19/12/2017
                            if (searchNode != null)
                                searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                            tvItems.SelectedNode = tvItems.Nodes[0];
                            tvItems.Nodes[0].EnsureVisible();
                            hideLoading();
                        }
                    }
                    #endregion IEC61850Client
                    else
                    {
                        string openedFile = ofdXMLFile.FileName;  //Namrata: 18/11/2017
                        this.MyMruList.AddFile(openedFile);  //Now give it to the MRUManager
                        int result = 0;
                        string errMsg = "XML file is valid...";
                        ResetConfiguratorState(false);
                        showLoading();
                        xmlFile = "";  //reset old filename...
                        pnlValidationMessages.Visible = true;
                        ListViewItem lvi;
                        lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                        //Namrata: 27/7/2017
                        toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                        tspFileName.Text = ofdXMLFile.FileName;
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                        if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                        else
                        {
                            pnlValidationMessages.Visible = true;
                            pnlValidationMessages.BringToFront();
                        }
                        if (result == -1) errMsg = "File doesnot exist!!!";
                        else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                        else if (result == -3) errMsg = "XSD file is not valid!!!";
                        else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                        else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                        lvi = new ListViewItem("");
                        lvValidationMessages.Items.Add(lvi);
                        lvi = new ListViewItem(errMsg);
                        lvValidationMessages.Items.Add(lvi);
                        lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                        if (result < 0)
                        {
                            hideLoading();
                            MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        xmlFile = ofdXMLFile.FileName; //Assign only after loading file...
                        TreeNode searchNode = tvItems.Nodes[0].Nodes[5]; //Namrata: 19/12/2017
                        if (searchNode != null)
                            searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                        tvItems.SelectedNode = tvItems.Nodes[0];
                        tvItems.Nodes[0].EnsureVisible();
                        hideLoading();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void openXMLFile()
        {
            string strRoutineName = "";
            try
            {
                ofdXMLFile.Filter = "XML Files|*.xml";
                ofdXMLFile.FilterIndex = 1;
                ofdXMLFile.RestoreDirectory = true;
                ofdXMLFile.Title = "Browse XML File";
                if (ofdXMLFile.ShowDialog() == DialogResult.OK)
                {

                    string openedFile = ofdXMLFile.FileName; //Namrata: 18/11/2017
                    //Namrata:26/04/2018
                    Utils.XMLFolderPath = Path.GetDirectoryName(ofdXMLFile.FileName);

                    //Namrata: 27/04/2018
                    Utils.DirectoryNAME = Path.GetDirectoryName(ofdXMLFile.FileName); //Get DirectoryName
                    Utils.XMLFileNAME = Path.GetFileName(ofdXMLFile.FileName); // Get XML Name
                    Utils.XMLFileFullPath = ofdXMLFile.FileName; // XML with full path
                    Utils.XMLFileNameWithoutExtension = "";// Path.GetFileNameWithoutExtension(Utils.XMLFileFullPath);
                    //Namrata: 29/03/2018
                    #region ClearDatasets
                    Utils.dsIED.Clear();
                    Utils.dsIED.Tables.Clear();
                    Utils.DsRCB.Clear();
                    Utils.DsRCBData.Clear();
                    Utils.dsResponseType.Clear();
                    Utils.DsResponseType.Clear();
                    Utils.dsIED.Clear();
                    Utils.dsIEDName.Clear();
                    Utils.DsAllConfigurationData.Clear();
                    Utils.DsAllConfigureData.Clear();
                    Utils.DsRCBDataset.Clear();
                    Utils.DsRCBDS.Clear();
                    Utils.DtRCBdata.Clear();
                    #endregion ClearDatasets
                    this.MyMruList.AddFile(openedFile); //Now give it to the MRUManager
                    int result = 0;
                    string errMsg = "XML file is valid...";
                    ResetConfiguratorState(false);
                    showLoading();
                    xmlFile = "";//reset old filename...
                    pnlValidationMessages.Visible = true;
                    ListViewItem lvi;
                    lvi = new ListViewItem("Validating file: " + ofdXMLFile.FileName);
                    //Namrata: 27/7/2017
                    toolStripStatusLabel1.Visible = true;  //Display File Name in Toolstrip 
                    tspFileName.Text = ofdXMLFile.FileName;
                    lvValidationMessages.Items.Add(lvi);
                    lvi = new ListViewItem("");
                    lvValidationMessages.Items.Add(lvi);
                    result = opcHandle.loadXML(ofdXMLFile.FileName, tvItems, out IsXmlValid);
                    if (IsXmlValid) { pnlValidationMessages.Visible = false; pnlValidationMessages.SendToBack(); }
                    else
                    {
                        pnlValidationMessages.Visible = true;
                        pnlValidationMessages.BringToFront();
                    }
                    if (result == -1) errMsg = "File doesnot exist!!!";
                    else if (result == -2) errMsg = "File is not a well-formed XML!!!";
                    else if (result == -3) errMsg = "XSD file is not valid!!!";
                    else if (result == -4) errMsg = "File is not a valid XML, as per the schema!!!";
                    else if (result == -5) errMsg = "MODBUS Slave Port is Not Valid as per the schema";
                    lvi = new ListViewItem("");
                    lvValidationMessages.Items.Add(lvi);
                    lvi = new ListViewItem(errMsg);
                    lvValidationMessages.Items.Add(lvi);
                    lvValidationMessages.EnsureVisible(lvValidationMessages.Items.Count - 1);
                    if (result < 0)
                    {
                        hideLoading();
                        MessageBox.Show(errMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    xmlFile = ofdXMLFile.FileName;//Assign only after loading file...
                    TreeNode searchNode = tvItems.Nodes[0].Nodes[5]; //Namrata: 19/12/2017
                    if (searchNode != null)
                        searchNode.Nodes.OfType<TreeNode>().ToList().ForEach(x => { x.Collapse(); });
                    tvItems.SelectedNode = tvItems.Nodes[0];
                    tvItems.Nodes[0].EnsureVisible();
                    hideLoading();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void showLoading()
        {
            // Configure a BackgroundWorker to perform your long running operation.
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += new DoWorkEventHandler(bg_DoWork);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
            Control.CheckForIllegalCrossThreadCalls = false;
            bgw.RunWorkerAsync();
            System.Threading.Thread.Sleep(1000);
        }
        private void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            // Display the loading form.
            Console.WriteLine("*** showLoading form created...");
            fp.TopMost = true;
            fp.ShowDialog();
        }
        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("*** bg_RunWorkerCompleted called!!!");
        }
        private void hideLoading()
        {
            if (fp != null)
            {
                fp.Hide();
            }
            else
            {
                Console.WriteLine("*** hideLoading called w/o showLoading...");
            }
            this.TopMost = true;
            this.TopMost = false;//Force it on top n then disable...
            this.TopLevel = true;
            Control.CheckForIllegalCrossThreadCalls = true;
        }
        private void handleSaveAs()
        {
            //opcHandle.regenerateSequence(); //Hack... Forcing reindexing on every save...
            saveAsXMLFile();
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            handleSaveAs();
        }
        //private void handleSave()
        //{
        //    DialogResult result = MessageBox.Show("Do you want to save changes in " + xmlFile + "?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
        //    if (result == DialogResult.Yes)
        //    {
        //        //Namrata:09/02/2018
        //        List<string> NetworkList = Utils.DtNetworkConfiguration.Rows.OfType<DataRow>()
        //       .Where(y => !string.IsNullOrEmpty(y.Field<string>("PortName")) && y.Field<string>("Enable") == "YES")
        //       .Select(y => y.Field<string>("PortName")).ToList();

        //        #region IEC104Slave
        //        List<string> IEC104SlavePortList = Utils.dtIEC104Slave.Rows.OfType<DataRow>()
        //        .Where(x => !string.IsNullOrEmpty(x.Field<string>("PortName")))
        //        .Select(x => x.Field<string>("PortName")).ToList();
        //        bool IsNwPortExist = IEC104SlavePortList.Where(z => NetworkList.Any(p => p == z)).Any();
        //        #endregion IEC104Slave

        //        #region MODBUS Slave
        //        List<string> MODBUSSlavePortList = Utils.DtMODBUSSlave.Rows.OfType<DataRow>()
        //       .Where(x => !string.IsNullOrEmpty(x.Field<string>("PortName")))
        //       .Select(x => x.Field<string>("PortName")).ToList();

        //        bool IsMODBUSPortExist = MODBUSSlavePortList.Where(z => NetworkList.Any(p => p == z)).Any();
        //        #endregion MODBUS Slave

        //        List<string> IEC61850ServerSlavePortList = Utils.DtIEC61850ServerSlave.Rows.OfType<DataRow>()
        //       .Where(x => !string.IsNullOrEmpty(x.Field<string>("PortName")))
        //       .Select(x => x.Field<string>("PortName")).ToList();

        //        bool IsIEC61850ServerPortExist = IEC61850ServerSlavePortList.Where(z => NetworkList.Any(p => p == z)).Any();

        //        //bool IsNwPortExist = Utils.dtIEC104Slave.Rows.OfType<DataRow>()
        //        //.Where(x => !string.IsNullOrEmpty(x.Field<string>("PortName")))
        //        //.Select(x => x.Field<string>("PortName")).ToList().Where(z => Utils.dtNetwork.Rows.OfType<DataRow>()
        //        //.Where(y => !string.IsNullOrEmpty(y.Field<string>("PortName")) && y.Field<string>("Enable") == "YES")
        //        //.Select(y => y.Field<string>("PortName")).ToList().Any(p => p == z)).Any();

        //        if (IEC104SlavePortList.Count > 0)
        //        {
        //            if (!IsNwPortExist)
        //            {
        //                MessageBox.Show("Please make sure that " + IEC104SlavePortList[0].ToString() + " enable 'YES' in NetworkConfiguration", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }
        //            else
        //            {
        //                saveXMLFile();
        //            }
        //        }
        //        if (MODBUSSlavePortList.Count > 0)
        //        {
        //            if (!IsMODBUSPortExist)
        //            {
        //                MessageBox.Show("Please make sure that " + MODBUSSlavePortList[0].ToString() + " enable 'YES' in NetworkConfiguration", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }
        //            else
        //            {
        //                saveXMLFile();
        //            }
        //        }
        //        if (IEC61850ServerSlavePortList.Count > 0)
        //        {
        //            if (!IsIEC61850ServerPortExist)
        //            {
        //                MessageBox.Show("Please make sure that " + IEC61850ServerSlavePortList[0].ToString() + " enable 'YES' in NetworkConfiguration", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }
        //            else
        //            {
        //                saveXMLFile();
        //            }
        //        }
        //        else
        //        {
        //            saveXMLFile();
        //        }
        //    }
        //    else if (result == DialogResult.No)
        //    {
        //        return;
        //    }
        //}
        private void handleSave()
        {
            DialogResult result = MessageBox.Show("Do you want to save changes?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                saveXMLFile();
            }
            else if (result == DialogResult.No)
            {
            }
        }
        private void handleSave1()
        {
            DialogResult result = MessageBox.Show("Do you want to save changes in " + xmlFile + "?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                //Namrata:09/02/2018
                List<string> NetworkList = Utils.DtNetworkConfiguration.Rows.OfType<DataRow>()
               .Where(y => !string.IsNullOrEmpty(y.Field<string>("PortName")) && y.Field<string>("Enable") == "YES")
               .Select(y => y.Field<string>("PortName")).ToList();

                #region IEC104Slave
                List<string> IEC104SlavePortList = Utils.dtIEC104Slave.Rows.OfType<DataRow>()
                .Where(x => !string.IsNullOrEmpty(x.Field<string>("PortName")))
                .Select(x => x.Field<string>("PortName")).ToList();
                bool IsNwPortExist = IEC104SlavePortList.Where(z => NetworkList.Any(p => p == z)).Any();
                #endregion IEC104Slave

                #region MODBUS Slave
                List<string> MODBUSSlavePortList = Utils.DtMODBUSSlave.Rows.OfType<DataRow>()
               .Where(x => !string.IsNullOrEmpty(x.Field<string>("PortName")))
               .Select(x => x.Field<string>("PortName")).ToList();

                bool IsMODBUSPortExist = MODBUSSlavePortList.Where(z => NetworkList.Any(p => p == z)).Any();
                #endregion MODBUS Slave

                List<string> IEC61850ServerSlavePortList = Utils.DtIEC61850ServerSlave.Rows.OfType<DataRow>()
               .Where(x => !string.IsNullOrEmpty(x.Field<string>("PortName")))
               .Select(x => x.Field<string>("PortName")).ToList();

                bool IsIEC61850ServerPortExist = IEC61850ServerSlavePortList.Where(z => NetworkList.Any(p => p == z)).Any();

                //bool IsNwPortExist = Utils.dtIEC104Slave.Rows.OfType<DataRow>()
                //.Where(x => !string.IsNullOrEmpty(x.Field<string>("PortName")))
                //.Select(x => x.Field<string>("PortName")).ToList().Where(z => Utils.dtNetwork.Rows.OfType<DataRow>()
                //.Where(y => !string.IsNullOrEmpty(y.Field<string>("PortName")) && y.Field<string>("Enable") == "YES")
                //.Select(y => y.Field<string>("PortName")).ToList().Any(p => p == z)).Any();

                if (IEC104SlavePortList.Count > 0)
                {
                    if (!IsNwPortExist)
                    {
                        MessageBox.Show("Please make sure that " + IEC104SlavePortList[0].ToString() + " enable 'YES' in NetworkConfiguration", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (MODBUSSlavePortList.Count > 0)
                {
                    if (!IsMODBUSPortExist)
                    {
                        MessageBox.Show("Please make sure that " + MODBUSSlavePortList[0].ToString() + " enable 'YES' in NetworkConfiguration", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (!IsIEC61850ServerPortExist)
                {
                    MessageBox.Show("Please make sure that " + IEC61850ServerSlavePortList[0].ToString() + " enable 'YES' in NetworkConfiguration", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    saveXMLFile();
                }
            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "saveToolStripMenuItem_Click";
            try
            {
                handleSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            handleSave();
        }
        private void frmParser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!showExitMessage) return;

            DialogResult result = MessageBox.Show("Do you want to save any changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                saveXMLFile();

                //Namrata: 14/04/2018
                //Delete Folder from ProgramData
                if (Utils.DirectoryPath != "")
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration";
                    string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
                    if (Directory.Exists(path))
                    {
                        FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                        FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
                    }
                }
                //Namrata: 18/04/2018
                //Delete Folder from AppData
                Utils.ICDFiles = System.IO.Path.GetTempPath() + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";// Application.StartupPath + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";
                if (Directory.Exists(Utils.ICDFiles))
                {
                    string DirectoryNameDelete = System.IO.Path.GetTempPath() + @"\" + "IEC61850_Client";
                    FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                    FileDirectoryOperations.DeleteDirectory(DirectoryNameDelete);
                }
            }
            else if (result == DialogResult.No)
            {
                //Namrata: 28/04/2018
                Utils.AIMapReindex.Clear(); //to clear all  data on new 

                //Namrata: 14/04/2018
                if (Utils.DirectoryPath != "")
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(ofdXMLFile.FileName) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration";
                    string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
                    if (Directory.Exists(path))
                    {
                        FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                        FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
                    }
                }
                //Namrata: 18/04/2018
                //Delete Folder from AppData
                Utils.ICDFiles = System.IO.Path.GetTempPath() + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";// Application.StartupPath + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";
                if (Directory.Exists(Utils.ICDFiles))
                {
                    string DirectoryNameDelete = System.IO.Path.GetTempPath() + @"\" + "IEC61850_Client";
                    FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                    FileDirectoryOperations.DeleteDirectory(DirectoryNameDelete);
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {   //Ajay: 21/11/2017
            //this.Close();

            //Ajay: 21/11/2017
            //if(opcHandle.IsFileOpen)
            //{
            //    string strMsg = "Do You Want To Save Any Changes ?";
            //    if (!string.IsNullOrEmpty(xmlFile))
            //    {
            //        strMsg = strMsg.TrimEnd('?');
            //        strMsg += " to \"" + xmlFile + "\" ?";
            //    }
            //    DialogResult result = MessageBox.Show(strMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            //    if (result == DialogResult.Yes)
            //    {
            //        saveXMLFile();
            //    }
            //    xmlFile = null;

            //    EnDs_Save_SaveAs_Exit(false);
            //    //opcHandle.IsFileOpen = false;

            handleNew();
        }
        private void HandleMapViewChange()
        {
            //IMP: scParser and grpMapping should be added first in forms (Designer.cs). Ex.
            //Else docking won't work properly
            if (tsbMappingView.Checked)
            {
                scParser.Dock = DockStyle.None;
                scParser.Visible = false;
                grpMappingView.Dock = DockStyle.Fill;
                grpMappingView.Visible = true;
            }
            else
            {
                grpMappingView.Dock = DockStyle.None;
                grpMappingView.Visible = false;
                scParser.Dock = DockStyle.Fill;
                scParser.Visible = true;
            }
        }
        private void tsbMappingView_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("***** Mapping view changed...");
        }
        private void tsbMappingView_Click(object sender, EventArgs e)
        {
            handleShowOverview();
        }
        private void ResetConfiguratorState(bool isNew)
        {
            opcHandle = null;
            tvItems.Nodes.Clear();
            opcHandle = new OpenProPlus_Config();
            opcHandle.ShowValidationMessages += this.ValidationCallBack;
            Utils.setOpenProPlusHandle(getOpenProPlusHandle());
            if (fo != null) fo.setOpenProPlusHandle(getOpenProPlusHandle());
            Globals.resetUniqueNos(ResetUniqueNos.ALL);
            //Patch for SerialConfiguration, call in context: opcHandle.loadSchema(Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME);
            opcHandle.loadDefaultItems(tvItems);
            tvItems.SelectedNode = tvItems.Nodes[0];
            //IMP: Create all default entries ONLY after resetting unique nos and creating treenodes...
            if (isNew) Utils.CreateDefaultEntries();
            int a = Utils.DummyDI.Count();
        }
        //Ajay: 21/11/2017
        private void EnDs_Save_SaveAs_Exit(bool IsEnabled)
        {
            saveToolStripMenuItem.Enabled = saveAsToolStripMenuItem.Enabled = exitToolStripMenuItem.Enabled = IsEnabled;
        }
        private void handleOpen1()
        {
            //Ajay: 21/11/2017 All edited
            if (opcHandle.IsFileOpen)
            {
                string strMsg = "Do you want to save any changes ?";
                if (!string.IsNullOrEmpty(xmlFile))
                {
                    strMsg = strMsg.TrimEnd('?');
                    strMsg += " to \"" + xmlFile + "\" ?";
                }
                DialogResult result = MessageBox.Show(strMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    saveXMLFile();
                }
                else { ofdXMLFile.FileName = ""; }
            }
            openXMLFile();
            opcHandle.IsFileOpen = true;
            Utils.IsOpen = true;
            EnDs_Save_SaveAs_Exit(true);
            saveToolStripMenuItem.Enabled = true;
            newToolStripMenuItem.Enabled = true;

            //Ajay: 21/11/2017 Original code commented
            //DialogResult result = MessageBox.Show("Do You Want To Save Any Changes ? ", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            //if (result == DialogResult.Yes)
            //{
            //    saveXMLFile();
            //    openXMLFile();
            //}
            //else if (result == DialogResult.No)
            //{
            //    openXMLFile();
            //}
            //else//cancel
            //{
            //}
        }
        private void handleOpen()
        {
            string strRoutineName = "";
            try
            {
                //Ajay: 21/11/2017 All edited
                if (opcHandle.IsFileOpen)
                {
                    string strMsg = "Do you want to save any changes ?";
                    if (!string.IsNullOrEmpty(xmlFile))
                    {
                        strMsg = strMsg.TrimEnd('?');
                        strMsg += " to \"" + xmlFile + "\" ?";
                    }
                    if (xmlFile != null)
                    {
                        DialogResult result = MessageBox.Show(strMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                        if (result == DialogResult.Yes)
                        {
                            saveXMLFile();
                        }
                        else
                        {
                            ofdXMLFile.FileName = "";
                            //Namrata: 28/04/2018
                            Utils.AIMapReindex.Clear(); //to clear all  data on new 
                        }
                    }
                }
                openXMLFile();
                opcHandle.IsFileOpen = true;
                Utils.IsOpen = true;
                EnDs_Save_SaveAs_Exit(true);
                saveToolStripMenuItem.Enabled = true;
                newToolStripMenuItem.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "openToolStripMenuItem_Click";
            try
            {
                handleOpen();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tsbOpen_Click(object sender, EventArgs e)
        {
            handleOpen();
        }
        private void handleNew1()
        {
            //Ajay: 21/11/2017 All edited

            if (opcHandle.IsFileOpen)
            {
                string strMsg = "Do you want to save any changes ?";
                if (!string.IsNullOrEmpty(xmlFile))
                {
                    strMsg = strMsg.TrimEnd('?');
                    strMsg += " to \"" + xmlFile + "\" ?";
                }
                DialogResult result = MessageBox.Show(strMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    saveXMLFile();
                    xmlFile = null;
                }
                else
                {
                    xmlFile = null;
                    //ResetConfiguratorState(true);
                    //opcHandle.IsFileOpen = true;
                }
            }
            ResetConfiguratorState(true);
            opcHandle.IsFileOpen = true;
            EnDs_Save_SaveAs_Exit(true);
            saveToolStripMenuItem.Enabled = false;
            toolStripStatusLabel1.Visible = false;
            tspFileName.Text = "";


            #region //Ajay: 21/11/2017  Original code commented
            //DialogResult result = MessageBox.Show("Do you want to save any changes?", "New XML file...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            //if (result == DialogResult.Yes)
            //{
            //    saveXMLFile();
            //    xmlFile = null;
            //    ResetConfiguratorState(true);
            //}
            //else if (result == DialogResult.No)
            //{
            //    xmlFile = null;
            //    ResetConfiguratorState(true);
            //}
            //else//cancel
            //{
            //}
            #endregion
        }
        private void handleNew()
        {
            //Ajay: 21/11/2017 All edited
            if (opcHandle.IsFileOpen)
            {
                string strMsg = "Do you want to save any changes ?";
                if (!string.IsNullOrEmpty(xmlFile))
                {
                    strMsg = strMsg.TrimEnd('?');
                    strMsg += " to \"" + xmlFile + "\" ?";
                }
                if (xmlFile != null)
                {
                    DialogResult result = MessageBox.Show(strMsg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        saveXMLFile();
                        //Namrata: 18/04/2018
                        //Delete Folder from AppData
                        if (Directory.Exists(Utils.ICDFiles))
                        {
                            string DirectoryNameDelete = System.IO.Path.GetTempPath() + @"\" + "IEC61850_Client";
                            FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                            FileDirectoryOperations.DeleteDirectory(DirectoryNameDelete);
                        }
                        if (Utils.DirectoryPath != "")
                        {
                            string FileNameWithoutExtesion = Path.GetFileNameWithoutExtension(Utils.DirectoryPath);
                            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(FileNameWithoutExtesion) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration";
                            string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
                            if (Directory.Exists(path))
                            {
                                FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                                FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
                            }
                        }
                        xmlFile = null;
                    }
                    else
                    {
                        //Namrata: 18/04/2018
                        //Delete Folder from AppData
                        if (Directory.Exists(Utils.ICDFiles))
                        {
                            string DirectoryNameDelete = System.IO.Path.GetTempPath() + @"\" + "IEC61850_Client";
                            FileDirectoryOperations fileDirectoryOperations = new FileDirectoryOperations();
                            FileDirectoryOperations.DeleteDirectory(DirectoryNameDelete);
                        }
                        if (Utils.DirectoryPath != "")
                        {
                            string FileNameWithoutExtesion = Path.GetFileNameWithoutExtension(Utils.DirectoryPath);
                            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(FileNameWithoutExtesion) + "\\" + "IEC61850_Client" + "\\" + "ProtocolConfiguration";
                            string ImportedDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\" + Path.GetFileNameWithoutExtension(Utils.DirectoryPath);// 
                            if (Directory.Exists(path))
                            {
                                FileDirectoryOperations fileDirectoryOperation = new FileDirectoryOperations();
                                FileDirectoryOperations.DeleteDirectory(ImportedDirectoryPath);
                            }
                        }
                        xmlFile = null;
                        //ResetConfiguratorState(true);
                        //opcHandle.IsFileOpen = true;
                    }
                }
            }
            ResetConfiguratorState(true);
            opcHandle.IsFileOpen = true;
            EnDs_Save_SaveAs_Exit(true);
            //saveToolStripMenuItem.Enabled = false;
            toolStripStatusLabel1.Visible = false;
            tspFileName.Text = "";


            #region //Ajay: 21/11/2017  Original code commented
            //DialogResult result = MessageBox.Show("Do you want to save any changes?", "New XML file...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            //if (result == DialogResult.Yes)
            //{
            //    saveXMLFile();
            //    xmlFile = null;
            //    ResetConfiguratorState(true);
            //}
            //else if (result == DialogResult.No)
            //{
            //    xmlFile = null;
            //    ResetConfiguratorState(true);
            //}
            //else//cancel
            //{
            //}
            #endregion
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            handleNew();
        }
        private void tsbNew_Click(object sender, EventArgs e)
        {
            //Namrata: 28/04/2018
            Utils.AIMapReindex.Clear(); //to clear all  data on new 
            handleNew();
        }
        private void grpMappingView_Resize(object sender, EventArgs e)
        {
            lvMappingView.Width = grpMappingView.Width - 10;
            lvMappingView.Height = grpMappingView.Height - FILTER_PANEL_HEIGHT - 10;
        }
        private void lvMappingView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            //IMP: No 'Console.Write' statements here...
            //Console.WriteLine("*** boom boom boom boom bash");
            e.DrawDefault = true;
        }
        private void lvMappingView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            //IMP: No 'Console.Write' statements here...
            if (e.ColumnIndex >= COLS_B4_MULTISLAVE && e.SubItem.Text == "yes")
            {
                e.DrawDefault = false;
                e.DrawBackground();
                Rectangle tPos = new Rectangle(e.SubItem.Bounds.X + ((e.SubItem.Bounds.Width - e.SubItem.Bounds.Height) / 2), e.SubItem.Bounds.Y, e.SubItem.Bounds.Height, e.SubItem.Bounds.Height);
                e.Graphics.DrawImage((Image)Properties.Resources.ResourceManager.GetObject("greenindicator"), tPos);
            }
            else if (e.SubItem.Text == "transparentimg")
            {
                e.DrawDefault = false;
                e.DrawBackground();
                Rectangle tPos = new Rectangle(e.SubItem.Bounds.X + ((e.SubItem.Bounds.Width - e.SubItem.Bounds.Height) / 2), e.SubItem.Bounds.Y, e.SubItem.Bounds.Height, e.SubItem.Bounds.Height);
                e.Graphics.DrawImage((Image)Properties.Resources.ResourceManager.GetObject("transparent"), tPos);
            }
            else
            {
                e.DrawDefault = true;
            }
        }
        private void lvMappingView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != sortColumn)  // Determine whether the column is the same as the last column clicked.
            {
                sortColumn = e.Column;
                lvMappingView.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (lvMappingView.Sorting == SortOrder.Ascending) // Determine what the last sort order was and change it.
                    lvMappingView.Sorting = SortOrder.Descending;
                else
                    lvMappingView.Sorting = SortOrder.Ascending;
            }
            lvMappingView.Sort();  // Call the sort method to manually sort.
            lvMappingView.ListViewItemSorter = new ListViewItemComparer(e.Column, lvMappingView.Sorting, lvMappingView.Columns[e.Column].Tag);
        }
        // Implements the manual sorting of items by column.
        class ListViewItemComparer : IComparer
        {
            private int col;
            private SortOrder order;
            private int dataType = 0; //0->string, 1->int
            public ListViewItemComparer()
            {
                col = 0;
                order = SortOrder.Ascending;
            }
            public ListViewItemComparer(int column, SortOrder order, object objType)
            {
                col = column;
                this.order = order;
                if (objType != null && objType.ToString() == "int") dataType = 1;
            }
            public int Compare(object x, object y)
            {
                int returnVal = -1;
                if (dataType == 0)
                {//string
                    returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                        ((ListViewItem)y).SubItems[col].Text);
                }
                else if (dataType == 1)
                {//int
                    string ix = "0", iy = "0";
                    if (((ListViewItem)x).SubItems[col].Text == "") ix = "0";
                    else ix = ((ListViewItem)x).SubItems[col].Text;

                    if (((ListViewItem)y).SubItems[col].Text == "") iy = "0";
                    else iy = ((ListViewItem)y).SubItems[col].Text;

                    returnVal = CompareInt(ix, iy);
                }
                // Determine whether the sort order is descending.
                // Invert the value returned by String.Compare.
                if (order == SortOrder.Descending) returnVal *= -1;
                return returnVal;
            }
            private int CompareInt(string x, string y)
            {
                int ix, iy;
                try
                {
                    ix = Int32.Parse(x);
                }
                catch (System.FormatException)
                {
                    ix = 0;
                }
                try
                {
                    iy = Int32.Parse(y);
                }
                catch (System.FormatException)
                {
                    iy = 0;
                }
                return ix - iy;
            }
        }
        private void handleShowOverview()
        {
            if (fo == null)
            {
                fo = new frmOverview(getOpenProPlusHandle());
                fo.FormClosed += fo_FormClosed;
                fo.Show();
            }
            else
            {
                fo.TopMost = true;
                fo.TopMost = false;
            }
        }
        private void showOverviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            handleShowOverview();
        }
        private void fo_FormClosed(object sender, FormClosedEventArgs e)
        {
            fo = null;
        }

        private void frmParser_Resize(object sender, EventArgs e)
        {
            pnlValidationMessages.Left = (this.Width - pnlValidationMessages.Width) / 2;
            pnlValidationMessages.Top = (this.Height - (this.mnuParser.Height + this.tsParser.Height + this.ssParser.Height + pnlValidationMessages.Height));
            Console.WriteLine("*** Validation Messages panel visible");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlValidationMessages.Visible = false;
        }
        private void scParser_Panel2_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.Width = scParser.Panel2.Width; //Resize the control in right-side panel, got using getView()
            e.Control.Height = scParser.Panel2.Height;
        }
        private void scParser_Panel2_Resize(object sender, EventArgs e)
        {
            if (scParser.Panel2.Controls.Count <= 0) return;
            Control ctrl = scParser.Panel2.Controls[0];
            if (ctrl != null)
            {
                ctrl.Width = scParser.Panel2.Width;
                ctrl.Height = scParser.Panel2.Height;
            }
        }
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tsbReindexData_Click(object sender, EventArgs e)
        {
            opcHandle.regenerateSequence();
        }
        private void handleHelp()
        {
            Help.ShowHelp(this, Globals.RESOURCES_PATH + "OpenPro+ Help.chm");
        }
        private void tsbHelp_Click(object sender, EventArgs e)
        {
            handleHelp();
        }
        private void shelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            handleHelp();
        }
        private void openProUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOpenPro_UI FrmOpenPro_UIs = new frmOpenPro_UI();
            FrmOpenPro_UIs.Show();
        }
        private void ssParser_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        DataTable table1 = new DataTable("IEC61850");

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, Globals.RESOURCES_PATH + "OpenProPlus Configurator - User Manual.pdf");
        }
        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {
            //opcHandle.regenerateIEDSequence();
        }
        private void recentFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MyMruList.FileSelected += MyMruList_FileSelected;
        }

        private void tspImportICD_Click(object sender, EventArgs e)
        {
            frmOpenPro_UI FrmOpenPro_UIs = new frmOpenPro_UI();
            FrmOpenPro_UIs.Show();
        }

        private void tspManual_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, Globals.RESOURCES_PATH + "OpenProPlus Configurator - User Manual.pdf");
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
