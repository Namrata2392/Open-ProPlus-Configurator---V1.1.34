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
namespace OpenProPlusConfigurator
{
        #region Declarations
    public class IEC61850ServerSlaveGroup
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        enum edition
        {
            Ed1,
            Ed2
        };
        private string rnName = "IEC61850ServerGroup";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        public List<IEC61850ServerSlave> mbsList = new List<IEC61850ServerSlave>();//Expose the list to others for slave mapping...
        ucGroup61850ServerSlave ucmbs = new ucGroup61850ServerSlave();
        ucGroupIEC104 ucGroupIEC104 = new ucGroupIEC104();
        private TreeNode MODBUSSlaveGroupTreeNode;
        //Namrata: 30/10/2017
        private Configure configuration;
        private string currentIedName = "";
        private string currentReport = "";
        string Iec61850Dataset = "";
        public bool MappingChanged
        {
            get; set;
        }
        int gridItemsAll = 0;
        //Namrata: 11/09/2017
        //Fill IEDName
        public DataGridView DataGridViewIEDNAme = new DataGridView();
        public DataTable IEDData = new DataTable();
        string IEDnames = "";
        //Namrata: 15/09/2017
        //Fill IEDName
        public DataGridView DataGridViewAddress = new DataGridView();
        public DataTable AddressData = new DataTable();
        public string IEDname = "";
        string add = "";
        string BufTime = "";
        string ConRev = "";
        string DSAddress = "";
        string IntgPD = "";
        string trigOptNum = "";
        DataSet dsIEDName = new DataSet();
        DataSet dsAddress = new DataSet();
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        //Namrata: 11/09/2017
        //Fill AI DropdownList
        public DataGridView DataGridViewFetchData = new DataGridView();
        public DataTable StoreData = new DataTable();
        DataRow ObjectRefreeence;
        DataSet DsAISlave = new DataSet();
        //Namrata: 11/09/2017
        //Fill DIMap DropdownList
        public DataGridView DataGridViewDI = new DataGridView();
        public DataTable StoreDI = new DataTable();
        DataRow DIObjectRefreeence;
        DataSet DsDISlave = new DataSet();

        //Namrata: 11/09/2017
        //Fill DOMap DropdownList
        public DataGridView DataGridViewDO = new DataGridView();
        public DataTable StoreDO = new DataTable();
        DataRow DOObjectRefreeence;
        DataSet DsDOSlave = new DataSet();
        #endregion Declarations
        public IEC61850ServerSlaveGroup()
        {
            string strRoutineName = "IEC61850ServerSlaveGroup";
            try
            {
                //Namrata: 30/10/2017
                MappingChanged = false;
                ucmbs.dataGridView1.Visible = false;
                ucmbs.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucmbs.lv61850ServerSlaveItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lv61850ServerSlave_ItemCheck);
                ucmbs.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucmbs.btnExportINIClick += new System.EventHandler(this.btnExportINI_Click);
                ucmbs.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucmbs.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucmbs.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucmbs.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucmbs.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucmbs.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucmbs.lv61850ServerSlaveDoubleClick += new System.EventHandler(this.lv61850ServerSlave_DoubleClick);
                ucmbs.PCDFileClick += new System.EventHandler(this.PCDFile_Click);
                ucmbs.CmbPortNameSelectedIndexChanged += new System.EventHandler(this.CmbPortName_SelectedIndexChanged);
                ucmbs.ucGroup61850ServerSlaveLoad += new System.EventHandler(this.ucGroup61850ServerSlave_Load);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CmbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "CmbPortName_SelectedIndexChanged";
            try
            {
                foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                {
                    if (ucmbs.CmbPortName.Text == ni.PortName)
                    {
                        ucmbs.CmbPortName.Text = ni.IP;
                        if (ni.Enable.ToUpper() == "YES")
                        {
                            ucmbs.PbOff.Visible = false;
                            ucmbs.PbOn.Visible = true;
                            ucmbs.PbOn.BringToFront();
                        }
                        else
                        {
                            ucmbs.PbOn.Visible = false;
                            ucmbs.PbOff.Visible = true;
                            ucmbs.PbOff.BringToFront();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lv61850ServerSlave_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lv61850ServerSlave_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucmbs.lv61850ServerSlave.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
                    {
                        item.Checked = false;
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ucGroup61850ServerSlave_Load(object sender, EventArgs e)
        {
            string strRoutineName = "ucGroup61850ServerSlave_Load";
            try
            {
                ucmbs.Ttbtn.SetToolTip(ucmbs.BtnICD, "Import ICD");
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public ICollection<GridItem> FillCurrentReport(ICollection<GridItem> listGridIt)
        {
            List<GridItem> tmp = new List<GridItem>();
            foreach (GridItem item in configuration.IgridItems)
            {
                if (item.FC == "MX")
                {
                    string a = item.ObjectReference.ToString();
                    ObjectRefreeence = StoreData.NewRow();
                    ObjectRefreeence["ObjectReferrence"] = a.ToString();
                    ObjectRefreeence["Node"] = Utils.IEC61850ServerICDName;
                    StoreData.Rows.Add(ObjectRefreeence);
                }
                if (item.FC == "ST")
                {
                    string DI = item.ObjectReference.ToString();
                    DIObjectRefreeence = StoreDI.NewRow();
                    DIObjectRefreeence["ObjectReferrence"] = DI;
                    DIObjectRefreeence["Node"] = Utils.IEC61850ServerICDName;
                    StoreDI.Rows.Add(DIObjectRefreeence);
                }

                //Namrata: 22/12/2017
                if (item.FC == "CO")
                {
                    string DO = item.ObjectReference.ToString();
                    DOObjectRefreeence = StoreDO.NewRow();
                    DOObjectRefreeence["ObjectReferrence"] = DO;
                    DOObjectRefreeence["Node"] = Utils.IEC61850ServerICDName;
                    StoreDO.Rows.Add(DOObjectRefreeence);
                }
            }
            #region Fill AIMap
            //Fill AIMap
            DataGridViewFetchData.DataSource = StoreData;
            Utils.dtAISlave = StoreData; //Fill DataSet Names 
            StoreData.TableName = "IEC61850Server" + "_" + Utils.IEC61850ServerSlaveNo;
            string Indexaddress = "";
            string[] arraddress = new string[StoreData.Rows.Count];
            string[] arrColaddress = new string[StoreData.Rows.Count];
            DataRow rowaddress;
            if (DsAISlave.Tables.Contains(StoreData.TableName))
            {
                DsAISlave.Tables[StoreData.TableName].Clear();
            }
            else
            {
                DsAISlave.Tables.Add(StoreData.TableName);
                DsAISlave.Tables[StoreData.TableName].Columns.Add("ObjectReferrence");
                DsAISlave.Tables[StoreData.TableName].Columns.Add("Node");
            }
            for (int i = 0; i < StoreData.Rows.Count; i++)
            {
                rowaddress = DsAISlave.Tables[StoreData.TableName].NewRow();
                DsAISlave.Tables[StoreData.TableName].NewRow();
                for (int j = 0; j < StoreData.Columns.Count; j++)
                {
                    Indexaddress = StoreData.Rows[i][j].ToString();
                    rowaddress[j] = Indexaddress.ToString();
                }
                DsAISlave.Tables[StoreData.TableName].Rows.Add(rowaddress);
            }
            Utils.dsAISlave = DsAISlave;
            #endregion Fill AIMap

            #region Fill DIMap
            //Fill DIMap
            DataGridViewDI.DataSource = StoreDI;
            Utils.dtDISlave = StoreDI; //Fill DataSet Names 
            StoreDI.TableName = "IEC61850Server" + "_" + Utils.IEC61850ServerSlaveNo;
            string IndexDIaddress = "";
            string[] arrDIaddress = new string[StoreDI.Rows.Count];
            string[] arrDIColaddress = new string[StoreDI.Rows.Count];
            DataRow rowDIaddress;
            if (DsDISlave.Tables.Contains(StoreDI.TableName))
            {
                DsDISlave.Tables[StoreDI.TableName].Clear();
            }
            else
            {
                DsDISlave.Tables.Add(StoreDI.TableName);
                DsDISlave.Tables[StoreDI.TableName].Columns.Add("ObjectReferrence");
                DsDISlave.Tables[StoreDI.TableName].Columns.Add("Node");
            }
            for (int i = 0; i < StoreDI.Rows.Count; i++)
            {
                rowDIaddress = DsDISlave.Tables[StoreDI.TableName].NewRow();
                DsDISlave.Tables[StoreDI.TableName].NewRow();
                for (int j = 0; j < StoreDI.Columns.Count; j++)
                {
                    IndexDIaddress = StoreDI.Rows[i][j].ToString();
                    rowDIaddress[j] = IndexDIaddress.ToString();
                }
                DsDISlave.Tables[StoreDI.TableName].Rows.Add(rowDIaddress);
            }
            Utils.DSDISlave = DsDISlave;
            #endregion Fill DIMap

            #region Fill DOMap
            //Fill DOMap
            DataGridViewDO.DataSource = StoreDO;
            Utils.dtDOSlave = StoreDO; //Fill DataSet Names 
            StoreDO.TableName = "IEC61850Server" + "_" + Utils.IEC61850ServerSlaveNo;
            string IndexDOaddress = "";
            string[] arrDOaddress = new string[StoreDO.Rows.Count];
            string[] arrDOColaddress = new string[StoreDO.Rows.Count];
            DataRow rowDOaddress;
            if (DsDOSlave.Tables.Contains(StoreDO.TableName))
            {
                DsDOSlave.Tables[StoreDO.TableName].Clear();
            }
            else
            {
                DsDOSlave.Tables.Add(StoreDO.TableName);
                DsDOSlave.Tables[StoreDO.TableName].Columns.Add("ObjectReferrence");
                DsDOSlave.Tables[StoreDO.TableName].Columns.Add("Node");
            }
            for (int i = 0; i < StoreDO.Rows.Count; i++)
            {
                rowDOaddress = DsDOSlave.Tables[StoreDO.TableName].NewRow();
                DsDOSlave.Tables[StoreDO.TableName].NewRow();
                for (int j = 0; j < StoreDO.Columns.Count; j++)
                {
                    IndexDOaddress = StoreDO.Rows[i][j].ToString();
                    rowDOaddress[j] = IndexDOaddress.ToString();
                }
                DsDOSlave.Tables[StoreDO.TableName].Rows.Add(rowDOaddress);
            }
            Utils.DSDOSlave = DsDOSlave;
            #endregion Fill DOMap
            if (tmp.Count == 0)
            {

            }
            return tmp.Count == 0 ? null : tmp;
        }
        private void PCDFile_Click(object sender, EventArgs e)
        {
            string strRoutineName = "PCDFile_Click";
            try
            {
                #region ClearDataTables
                Utils.Filename = ""; //Namrata:14/10/2017
                DsAISlave.Tables.Clear();
                DsAISlave.Clear();
                DsDISlave.Tables.Clear();
                DsDISlave.Clear();
                DsDOSlave.Clear();
                //Namrata: 07/09/2017
                //fill  AI Slave DropdownList
                DataGridViewFetchData.DataSource = null;
                DataGridViewFetchData.Rows.Clear();
                DataGridViewFetchData.Visible = false;
                StoreData.Rows.Clear(); StoreData.Columns.Clear();
                DataColumn dccolumn = StoreData.Columns.Add("ObjectReferrence", typeof(string));
                StoreData.Columns.Add("Node", typeof(string));
                //Namrata: 07/09/2017
                //fill  DI Slave DropdownList
                DataGridViewDI.DataSource = null;
                DataGridViewDI.Rows.Clear();
                DataGridViewDI.Visible = false;
                StoreDI.Rows.Clear(); StoreDI.Columns.Clear();
                DataColumn dcDI = StoreDI.Columns.Add("ObjectReferrence", typeof(string));
                StoreDI.Columns.Add("Node", typeof(string));
                //Namrata: 07/09/2017
                //fill  DI Slave DropdownList
                DataGridViewDO.DataSource = null;
                DataGridViewDO.Rows.Clear();
                DataGridViewDO.Visible = false;
                StoreDO.Rows.Clear(); StoreDO.Columns.Clear();
                DataColumn dcDO = StoreDO.Columns.Add("ObjectReferrence", typeof(string));
                StoreDO.Columns.Add("Node", typeof(string));
                #endregion ClearDataTables
                Boolean isCanceled = false;
                if (!isCanceled)
                {
                    Stream myStream = null;
                    ucmbs.dataGridView1.Visible = true;
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
                                        //Namrata: 24/10/2017
                                        Utils.ICDName = openFileDialog1.SafeFileName;
                                        ucmbs.treeView1.Nodes.Clear();
                                        configuration.IIedMappings.Clear();
                                        ucmbs.treeView1.Visible = false;
                                        ucmbs.splitContainer1.Visible = false;
                                        Cursor.Current = Cursors.WaitCursor;
                                        try
                                        {
                                            configuration.ParseScl();
                                            if (configuration.MappingChanged)
                                            {
                                                this.MappingChanged = true;
                                            }
                                        }
                                        catch (System.ArgumentException exc)
                                        {
                                            MessageBox.Show(exc.Message.ToString() + "\n\nTry to open valid SCL file or modify existing one.", "Xml file parsing error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                            this.MappingChanged = false;
                                        }
                                        myStream.Close();
                                        this.MappingChanged = false;
                                        int index = 0;
                                        int notEmptyIeds = 0; //check if ied is empty
                                        ArrayList toErease = new ArrayList();
                                        foreach (var ied in configuration.Iieds)
                                        {
                                            int count = 0;
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
                                        //Namrata: 06/11/2017
                                        ucmbs.txtIEDName.Enabled = true;
                                        //Namrata: 02/11/2017
                                        ucmbs.txtmanfacturer.Text = Utils.Manufacture;
                                        ucmbs.txtLDName.Text = Utils.LogicalDeviceName;
                                        ucmbs.txtICDPath.Text = openFileDialog1.SafeFileName;
                                        TreeNode treeNode = new TreeNode(openFileDialog1.SafeFileName);
                                        ucmbs.treeView1.Nodes.Add(treeNode);
                                        foreach (KeyValuePair<string, XPathNodeIterator> t in configuration.Iieds)
                                        {
                                            int count = 0;
                                            index = 0;
                                            TreeNode tNode = new TreeNode(t.Key, 3, 3);
                                            if (firstIedNode == null)
                                            {
                                                firstIedNode = treeNode;
                                                ucmbs.treeView1.SelectedNode = firstIedNode;
                                                foreach (TreeNode tnp in ucmbs.treeView1.Nodes)
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
                                            foreach (var v in configuration.Ireports)
                                            {
                                                if (v.Key.Contains(t.Key))
                                                {
                                                    count++;
                                                }
                                                IEDnames = t.Key.ToString();
                                            }
                                            //Namrata: 02/11/2017
                                            Utils.IEC61850ServerICDName = IEDnames;
                                            ucmbs.txtIEDName.Text = IEDnames.ToString();
                                            TreeNode[] array = new TreeNode[count];
                                            foreach (var v in configuration.Ireports)
                                            {
                                                if (v.Key.Contains(t.Key))
                                                {
                                                    string rcbNode = "";
                                                    rcbNode = v.Value.Address.Replace(v.Value.IedName, "");
                                                    if (v.Value.Buffered == "true")
                                                    {
                                                        TreeNode n = new TreeNode(rcbNode, 1, 1);
                                                        array.SetValue(n, index++);
                                                    }
                                                    else
                                                    {
                                                        TreeNode n = new TreeNode(rcbNode, 2, 2);
                                                        array.SetValue(n, index++);
                                                    }
                                                }
                                            }
                                            RefreshGridFilters();
                                            tNode.Nodes.AddRange(array);
                                            ucmbs.treeView1.Nodes.Add(tNode);
                                        }
                                        ucmbs.treeView1.Visible = false;
                                        ucmbs.treeView1.AutoSize = true;
                                        Cursor.Current = Cursors.Default;
                                        RefreshGrid(configuration.IgridItems);
                                        gridItemsAll = configuration.IgridItems.Count;
                                        if (configuration.Iieds.Count == 0 && !configuration.WrongEntry)
                                        {
                                            MessageBox.Show("Parsing file failed: This is not valid SCL file", "SCL file paring error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                            ucmbs.treeView1.Nodes.Clear();
                                            ucmbs.treeView1.Visible = false;
                                            ucmbs.splitContainer1.Visible = false;
                                            configuration.WrongEntry = false;
                                            configuration = null;
                                            this.MappingChanged = false;
                                        }
                                    }
                                    catch (System.Xml.XmlException ex)
                                    {
                                        MessageBox.Show("Parsing file failed: " + ex.Message, "Xml file parsing error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                        ucmbs.treeView1.Nodes.Clear();
                                        ucmbs.treeView1.Visible = false;
                                        ucmbs.splitContainer1.Visible = false;
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
                        //this.SetWindowTitle();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void RefreshGrid(IEnumerable<GridItem> items)
        {
            string strRoutineName = "RefreshGrid";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (items == null)
                {
                    Cursor.Current = Cursors.Default;
                    ucmbs.dataGridView1.Rows.Clear();
                }
                else
                {
                    ucmbs.dataGridView1.Rows.Clear();
                    foreach (GridItem item in items)
                    {
                        int i = ucmbs.dataGridView1.Rows.Add();
                        ucmbs.dataGridView1.Rows[i].Cells[0].Value = item.Id.ToString(CultureInfo.InvariantCulture);
                        DataGridViewComboBoxCell cell = ucmbs.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Mapping Type"]] as DataGridViewComboBoxCell;
                        if (cell != null)
                        {
                            if (!String.IsNullOrEmpty(item.MappingType.Trim()))
                            {
                                cell.Value = item.MappingType;
                            }
                        }
                        DataGridViewComboBoxCell indexCell = ucmbs.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Index"]] as DataGridViewComboBoxCell;

                        if (indexCell != null)
                        {
                            indexCell.Items.Clear();
                            indexCell.Items.Add(item.Index);
                            indexCell.Value = item.Index;
                        }
                        DataGridViewComboBoxCell fTypeCell = ucmbs.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Refresh Type"]] as DataGridViewComboBoxCell;
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
                        ucmbs.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Type"]].Value = item.GridItemType;
                        ucmbs.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Object Reference"]].Value = item.ObjectReference;
                        ucmbs.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["FC"]].Value = item.FC;
                        ucmbs.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Description"]].Value = item.Description;
                    }
                }
                List<GridItem> listItems = items as List<GridItem>;
                if (listItems != null)
                {
                }
                else
                {
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public ICollection<GridItem> ShowCurrentReport(ICollection<GridItem> listGridIt)
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
                    add = rep.Address.ToString();
                    BufTime = rep.BufTime.ToString();
                    ConRev = rep.ConfRev.ToString();
                    DSAddress = rep.DSAddress.ToString();
                    IntgPD = rep.IntgPD.ToString();
                    trigOptNum = rep.TrgOptNum.ToString();
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
                                    {
                                        tmp.Add(gItem);
                                    }
                                }
                            }
                        }
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
            string strRoutineName = "UpdateMappingStatistic";
            try
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
                { }
                else
                { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void RefreshGridFilters()
        {
            string strRoutineName = "RefreshGridFilters";
            try
            {
                ucmbs.dataGridView1.Rows.Clear();
                ICollection<GridItem> listGridIt = new List<GridItem>();
                listGridIt = ShowCurrentIed(listGridIt as List<GridItem>);
                listGridIt = ShowCurrentReport(configuration.IgridItems as List<GridItem>);
                listGridIt = FillCurrentReport(configuration.IgridItems as List<GridItem>);
                RefreshGrid(listGridIt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAdd_Click";
            try
            {
                if (mbsList.Count >= Globals.MaxMODBUSSlave)
                {
                    MessageBox.Show("Maximum " + Globals.MaxMODBUSSlave + " 61850Server Slaves are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucmbs.grpIEC61850Slave);
                Utils.showNavigation(ucmbs.grpIEC61850Slave, false);
                loadDefaults();
                ucmbs.txtSlaveNum.Text = (Globals.SlaveNo + 1).ToString();
                //Namrata: 02/11/2017
                Utils.IEC61850ServerSlaveNo = ucmbs.txtSlaveNum.Text;
                ucmbs.grpIEC61850Slave.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDone_Click";
            try
            {
                if (!Validate()) return;
                List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(ucmbs.grpIEC61850Slave);
                if (ucmbs.txtIEDName.Text == "")
                {
                    MessageBox.Show("Please import .icd files", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #region AI
                //Namrata:02/11/2017
                DataTable dtTempGetOriginalData;
                DataTable dtTempGetModifiedlData;
                //Step getactual data>> store in temp tbl
                dtTempGetOriginalData = DsAISlave.Tables[0];
                DataTable dt1 = dtTempGetOriginalData; string strVal = ""; string strOldName = ""; string strTablename = "";
                strOldName = dtTempGetOriginalData.TableName; ;
                for (int c = 0; c < dtTempGetOriginalData.Columns.Count; c++)
                {
                    foreach (DataRow dr in dtTempGetOriginalData.Rows)
                    {
                        strVal = dr[c].ToString();
                        strVal = strVal.Replace(dr[1].ToString(), ucmbs.txtIEDName.Text);
                        dr[c] = strVal;
                    }
                }
                strTablename = dtTempGetOriginalData.TableName;
                dtTempGetOriginalData.TableName = strTablename;
                Utils.CurrentSlave = strTablename;
                dtTempGetModifiedlData = dtTempGetOriginalData;
                string Indexaddress = "";
                string[] arraddress = new string[dtTempGetModifiedlData.Rows.Count];
                string[] arrColaddress = new string[dtTempGetModifiedlData.Rows.Count];
                DataRow rowaddress;
                if (Utils.Dumyds.Tables.Contains(dtTempGetModifiedlData.TableName))
                {
                    Utils.Dumyds.Tables.Remove(dtTempGetModifiedlData.TableName);
                    Utils.Dumyds.Tables.Add(dtTempGetModifiedlData.TableName);
                    Utils.Dumyds.Tables[dtTempGetModifiedlData.TableName].Columns.Add("ObjectReferrence");
                    Utils.Dumyds.Tables[dtTempGetModifiedlData.TableName].Columns.Add("Node");
                }
                else
                {
                    Utils.Dumyds.Tables.Add(dtTempGetModifiedlData.TableName);
                    Utils.Dumyds.Tables[dtTempGetModifiedlData.TableName].Columns.Add("ObjectReferrence");
                    Utils.Dumyds.Tables[dtTempGetModifiedlData.TableName].Columns.Add("Node");
                }
                for (int R = 0; R < dtTempGetModifiedlData.Rows.Count; R++)
                {
                    rowaddress = Utils.Dumyds.Tables[dtTempGetModifiedlData.TableName].NewRow();
                    Utils.Dumyds.Tables[dtTempGetModifiedlData.TableName].NewRow();
                    for (int C = 0; C < dtTempGetModifiedlData.Columns.Count; C++)
                    {
                        Indexaddress = dtTempGetModifiedlData.Rows[R][C].ToString();
                        rowaddress[C] = Indexaddress.ToString();
                    }
                    Utils.Dumyds.Tables[dtTempGetModifiedlData.TableName].Rows.Add(rowaddress);
                }
                Utils.dsAISlave = Utils.Dumyds;
                #endregion AI

                #region DI
                //Namrata:02/11/2017
                DataTable dtTempGetOriginalDataDI;
                DataTable dtTempGetModifiedlDataDI;
                //Step getactual data>> store in temp tbl
                dtTempGetOriginalDataDI = DsDISlave.Tables[0];
                DataTable dtDI = dtTempGetOriginalDataDI; string strValDI = ""; string strOldNameDI = ""; string strTablenameDI = "";
                strOldName = dtTempGetOriginalDataDI.TableName; ;
                for (int c = 0; c < dtTempGetOriginalDataDI.Columns.Count; c++)
                {
                    foreach (DataRow dr in dtTempGetOriginalDataDI.Rows)
                    {
                        strValDI = dr[c].ToString();
                        strValDI = strValDI.Replace(dr[1].ToString(), ucmbs.txtIEDName.Text);
                        dr[c] = strValDI;
                    }
                }
                strTablenameDI = dtTempGetOriginalDataDI.TableName;
                dtTempGetOriginalDataDI.TableName = strTablenameDI;
                Utils.CurrentSlave = strTablenameDI;
                dtTempGetModifiedlDataDI = dtTempGetOriginalDataDI;
                string IndexaddressDI = "";
                string[] arraddressDI = new string[dtTempGetModifiedlDataDI.Rows.Count];
                string[] arrColaddressDI = new string[dtTempGetModifiedlDataDI.Rows.Count];
                DataRow rowaddressDI;
                if (Utils.DumydsDI.Tables.Contains(dtTempGetModifiedlDataDI.TableName))
                {
                    Utils.DumydsDI.Tables.Remove(dtTempGetModifiedlDataDI.TableName);
                    Utils.DumydsDI.Tables.Add(dtTempGetModifiedlDataDI.TableName);
                    Utils.DumydsDI.Tables[dtTempGetModifiedlDataDI.TableName].Columns.Add("ObjectReferrence");
                    Utils.DumydsDI.Tables[dtTempGetModifiedlDataDI.TableName].Columns.Add("Node");
                }
                else
                {
                    Utils.DumydsDI.Tables.Add(dtTempGetModifiedlDataDI.TableName);
                    Utils.DumydsDI.Tables[dtTempGetModifiedlDataDI.TableName].Columns.Add("ObjectReferrence");
                    Utils.DumydsDI.Tables[dtTempGetModifiedlDataDI.TableName].Columns.Add("Node");
                }
                for (int R = 0; R < dtTempGetModifiedlDataDI.Rows.Count; R++)
                {
                    rowaddressDI = Utils.DumydsDI.Tables[dtTempGetModifiedlDataDI.TableName].NewRow();
                    Utils.DumydsDI.Tables[dtTempGetModifiedlDataDI.TableName].NewRow();
                    for (int C = 0; C < dtTempGetModifiedlDataDI.Columns.Count; C++)
                    {
                        IndexaddressDI = dtTempGetModifiedlDataDI.Rows[R][C].ToString();
                        rowaddressDI[C] = IndexaddressDI.ToString();
                    }
                    Utils.DumydsDI.Tables[dtTempGetModifiedlDataDI.TableName].Rows.Add(rowaddressDI);
                }
                Utils.DSDISlave = Utils.DumydsDI;
                #endregion DI

                #region DO
                //Namrata:02/11/2017
                DataTable dtTempGetOriginalDataDO;
                DataTable dtTempGetModifiedlDataDO;
                //Step getactual data>> store in temp tbl
                dtTempGetOriginalDataDO = DsDOSlave.Tables[0];
                DataTable dtDO = dtTempGetOriginalDataDO;
                string strValDO = ""; string strTablenameDO = "";
                strOldName = dtTempGetOriginalDataDO.TableName; ;
                for (int c = 0; c < dtTempGetOriginalDataDO.Columns.Count; c++)
                {
                    foreach (DataRow dr in dtTempGetOriginalDataDO.Rows)
                    {
                        strValDO = dr[c].ToString();
                        strValDO = strValDO.Replace(dr[1].ToString(), ucmbs.txtIEDName.Text);
                        dr[c] = strValDO;
                    }
                }
                strTablenameDO = dtTempGetOriginalDataDO.TableName;
                dtTempGetOriginalDataDO.TableName = strTablenameDO;
                Utils.CurrentSlave = strTablenameDO;
                dtTempGetModifiedlDataDO = dtTempGetOriginalDataDO;
                string IndexaddressDO = "";
                string[] arraddressDO = new string[dtTempGetModifiedlDataDO.Rows.Count];
                string[] arrColaddressDO = new string[dtTempGetModifiedlDataDO.Rows.Count];
                DataRow rowaddressDO;
                if (Utils.DumydsDO.Tables.Contains(dtTempGetModifiedlDataDO.TableName))
                {
                    Utils.DumydsDO.Tables.Remove(dtTempGetModifiedlDataDO.TableName);
                    Utils.DumydsDO.Tables.Add(dtTempGetModifiedlDataDO.TableName);
                    Utils.DumydsDO.Tables[dtTempGetModifiedlDataDO.TableName].Columns.Add("ObjectReferrence");
                    Utils.DumydsDO.Tables[dtTempGetModifiedlDataDO.TableName].Columns.Add("Node");
                }
                else
                {
                    Utils.DumydsDO.Tables.Add(dtTempGetModifiedlDataDO.TableName);
                    Utils.DumydsDO.Tables[dtTempGetModifiedlDataDO.TableName].Columns.Add("ObjectReferrence");
                    Utils.DumydsDO.Tables[dtTempGetModifiedlDataDO.TableName].Columns.Add("Node");
                }
                for (int R = 0; R < dtTempGetModifiedlDataDO.Rows.Count; R++)
                {
                    rowaddressDO = Utils.DumydsDO.Tables[dtTempGetModifiedlDataDO.TableName].NewRow();
                    Utils.DumydsDO.Tables[dtTempGetModifiedlDataDO.TableName].NewRow();
                    for (int C = 0; C < dtTempGetModifiedlDataDO.Columns.Count; C++)
                    {
                        IndexaddressDO = dtTempGetModifiedlDataDO.Rows[R][C].ToString();
                        rowaddressDO[C] = IndexaddressDO.ToString();
                    }
                    Utils.DumydsDO.Tables[dtTempGetModifiedlDataDO.TableName].Rows.Add(rowaddressDO);
                }
                Utils.DSDOSlave = Utils.DumydsDO;
                #endregion DO

                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    mbsList.Add(new IEC61850ServerSlave("IEC61850Server", mbsData, tmp));
                }
                else if (mode == Mode.EDIT)
                {
                    mbsList[editIndex].updateAttributes(mbsData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    ucmbs.grpIEC61850Slave.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnCancel_Click";
            try
            {
                ucmbs.grpIEC61850Slave.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucmbs.grpIEC61850Slave);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDelete_Click";
            try
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "*** mbsList count: {0} lv count: {1}", mbsList.Count, ucmbs.lv61850ServerSlave.Items.Count);
                if (ucmbs.lv61850ServerSlave.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one slave ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucmbs.lv61850ServerSlave.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Slave No." + ucmbs.lv61850ServerSlave.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucmbs.lv61850ServerSlave.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            mbsList.RemoveAt(iIndex);
                            ucmbs.lv61850ServerSlave.Items[iIndex].Remove();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one Slave ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    Utils.WriteLine(VerboseLevel.DEBUG, "*** mbsList count: {0} lv count: {1}", mbsList.Count, ucmbs.lv61850ServerSlave.Items.Count);
                    refreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExportINI_Click(object sender, EventArgs e)
        {
            //FIXME: Check if the 'xmlFile' exists, meaning the filename exist...
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnFirst_Click";
            try
            {
                Console.WriteLine("*** ucmbs btnFirst_Click clicked in class!!!");
                if (ucmbs.lv61850ServerSlave.Items.Count <= 0) return;
                if (mbsList.ElementAt(0).IsNodeComment) return;
                editIndex = 0;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnPrev_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** ucmbs btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (mbsList.ElementAt(editIndex - 1).IsNodeComment) return;
                editIndex--;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnNext_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** ucmbs btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= ucmbs.lv61850ServerSlave.Items.Count) return;
                if (mbsList.ElementAt(editIndex + 1).IsNodeComment) return;
                editIndex++;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnLast_Click";
            try
            {
                Console.WriteLine("*** ucmbs btnLast_Click clicked in class!!!");
                if (ucmbs.lv61850ServerSlave.Items.Count <= 0) return;
                if (mbsList.ElementAt(mbsList.Count - 1).IsNodeComment) return;
                editIndex = mbsList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lv61850ServerSlave_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lv61850ServerSlave_DoubleClick";
            try
            {
                if (ucmbs.lv61850ServerSlave.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucmbs.lv61850ServerSlave.SelectedItems[0];
                Utils.UncheckOthers(ucmbs.lv61850ServerSlave, lvi.Index);
                if (mbsList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucmbs.grpIEC61850Slave.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucmbs.grpIEC61850Slave, true);
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void loadDefaults()
        {
            string strRoutineName = "loadDefaults";
            try
            {
                ucmbs.cmbEdition.SelectedIndex = ucmbs.cmbEdition.FindStringExact("Ed1");
                if (ucmbs.cmbEdition.Items.Count > 0) ucmbs.cmbEdition.SelectedIndex = 0;
                ucmbs.txtTCPPort.Text = "102";
                ucmbs.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION;
                ucmbs.cmbDebug.SelectedIndex = ucmbs.cmbDebug.FindStringExact("3");
                ucmbs.chkRun.Checked = true;
                ucmbs.txtRemoteIP.Text = "192.168.1.1";
                //Namrata: 06/11/2017
                ucmbs.txtmanfacturer.Enabled = false;
                ucmbs.txtIEDName.Enabled = false;
                ucmbs.txtLDName.Enabled = false;
                ucmbs.txtmanfacturer.Text = "";
                ucmbs.txtIEDName.Text = "";
                ucmbs.txtLDName.Text = "";
                foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                {
                    if (ni.Enable == "YES")
                    {
                        ucmbs.CmbPortName.Text = ni.PortName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            string strRoutineName = "loadValues";
            try
            {
                IEC61850ServerSlave mbs = mbsList.ElementAt(editIndex);
                if (mbs != null)
                {
                    ucmbs.txtSlaveNum.Text = mbs.SlaveNum;
                    ucmbs.cmbLocalIP.SelectedIndex = ucmbs.cmbLocalIP.FindStringExact(mbs.LocalIP);
                    ucmbs.cmbEdition.SelectedIndex = ucmbs.cmbEdition.FindStringExact(mbs.PortNum);
                    ucmbs.txtTCPPort.Text = mbs.TcpPort;
                    ucmbs.txtRemoteIP.Text = mbs.RemoteIP;
                    ucmbs.txtFirmwareVersion.Text = mbs.AppFirmwareVersion;
                    ucmbs.cmbDebug.SelectedIndex = ucmbs.cmbDebug.FindStringExact(mbs.DEBUG);
                    ucmbs.cmbEdition.SelectedIndex = ucmbs.cmbEdition.FindStringExact(mbs.Edition);
                    //Namrata: 24/10/2017
                    ucmbs.txtmanfacturer.Text = mbs.Manufacturer;
                    ucmbs.txtIEDName.Text = mbs.IEDName;
                    ucmbs.txtLDName.Text = mbs.LDevice;
                    //ucmbs.txtLDOpp.Text = mbs.LDOPP;
                    if (mbs.Run.ToLower() == "yes") ucmbs.chkRun.Checked = true;
                    else ucmbs.chkRun.Checked = false;
                    foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                    {
                        if (ni.Enable == "YES")
                        {
                            ucmbs.cmbLocalIP.Text = ni.PortName;
                            ucmbs.CmbPortName.SelectedIndex = ucmbs.cmbDebug.FindStringExact(mbs.PortName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucmbs.grpIEC61850Slave))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check Remote IP...
            if (!Utils.IsValidIPv4(ucmbs.txtRemoteIP.Text))
            {
                MessageBox.Show("Invalid Remote IP.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check LocalIP ..
            //Namrata:10/5/2017
            if (!Utils.IsValidIPv4(ucmbs.cmbLocalIP.Text))
            {
                MessageBox.Show("Invalid Local IP", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!Utils.IsValidIPv4(ucmbs.txtRemoteIP.Text))
            {
                MessageBox.Show("Invalid Remote IP", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check TCP Port...
            if (!Utils.IsValidTCPPort(Int32.Parse(ucmbs.txtTCPPort.Text)))
            {
                MessageBox.Show("Invalid TCP Port.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            string strRoutineName = "fillOptions";
            try
            {
                //Fill Debug levels...
                ucmbs.cmbDebug.Items.Clear();
                for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
                {
                    ucmbs.cmbDebug.Items.Add(i.ToString());
                }
                ucmbs.cmbDebug.SelectedIndex = 0;

                //Namrata: 10/10/2017
                //Fill Protocol Types...
                ucmbs.cmbEdition.Items.Clear();
                foreach (edition ptv in Enum.GetValues(typeof(edition)))
                {
                    ucmbs.cmbEdition.Items.Add(ptv.ToString());
                }
                if (ucmbs.cmbEdition.Items.Count > 0) ucmbs.cmbEdition.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addListHeaders()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                ucmbs.lv61850ServerSlave.Columns.Add("Slave No.", 60, HorizontalAlignment.Left);
                ucmbs.lv61850ServerSlave.Columns.Add("Edition", 60, HorizontalAlignment.Left);
                ucmbs.lv61850ServerSlave.Columns.Add("TCP Port", 70, HorizontalAlignment.Left);
                ucmbs.lv61850ServerSlave.Columns.Add("Remote IP Address", 150, HorizontalAlignment.Left);
                ucmbs.lv61850ServerSlave.Columns.Add("Local IP Address", 120, HorizontalAlignment.Left); //Namrata: 10/05/2017
                ucmbs.lv61850ServerSlave.Columns.Add("Manufacturer", 100, HorizontalAlignment.Left);  //Namrata: 24/10/2017
                ucmbs.lv61850ServerSlave.Columns.Add("IEDName", 100, HorizontalAlignment.Left);
                ucmbs.lv61850ServerSlave.Columns.Add("LD_Name", 100, HorizontalAlignment.Left);
                ucmbs.lv61850ServerSlave.Columns.Add("Firmware Version", 120, HorizontalAlignment.Left);
                ucmbs.lv61850ServerSlave.Columns.Add("Debug Level", 80, HorizontalAlignment.Left);
                ucmbs.lv61850ServerSlave.Columns.Add("Run", -2, HorizontalAlignment.Left);
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
                int cnt = 0;
                ucmbs.lv61850ServerSlave.Items.Clear();
                foreach (IEC61850ServerSlave sl in mbsList)
                {
                    string[] row = new string[12];
                    int rNo = 0;
                    if (sl.IsNodeComment)
                    {
                        row[rNo] = "Comment...";
                    }
                    else
                    {
                        row[0] = sl.SlaveNum;
                        row[1] = sl.Edition;
                        row[2] = sl.TcpPort;
                        row[3] = sl.RemoteIP;
                        //Namrata:10/05/2017
                        row[4] = sl.LocalIP;
                        row[5] = sl.Manufacturer;
                        row[6] = sl.IEDName;
                        row[7] = sl.LDevice;
                        row[8] = sl.AppFirmwareVersion;
                        row[9] = sl.DEBUG;
                        row[10] = sl.Run;
                        row[11] = sl.PortName;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucmbs.lv61850ServerSlave.Items.Add(lvItem);
                    ListToDataTable(mbsList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable ListToDataTable<IEC61850ServerSlave>(IList<IEC61850ServerSlave> varlist)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (typeof(IEC61850ServerSlave).IsValueType || typeof(IEC61850ServerSlave).Equals(typeof(string)))
            {
                DataColumn dc = new DataColumn("Values");
                dt.Columns.Add(dc);
                foreach (IEC61850ServerSlave item in varlist)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item;
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                PropertyInfo[] propT = typeof(IEC61850ServerSlave).GetProperties(); //find all the public properties of this Type using reflection;
                foreach (PropertyInfo pi in propT)
                {
                    DataColumn dc = new DataColumn(pi.Name, pi.PropertyType);
                    dt.Columns.Add(dc);
                }
                for (int item = 0; item < varlist.Count(); item++)
                {
                    DataRow dr = dt.NewRow();
                    for (int property = 0; property < propT.Length; property++)
                    {
                        dr[property] = propT[property].GetValue(varlist[item], null);
                    }
                    dt.Rows.Add(dr);
                }
            }
            dt.AcceptChanges();
            ds.Tables.Add(dt);
            Utils.DsIEC61850ServerSlave = ds;
            Utils.DtIEC61850ServerSlave = dt;
            return dt;
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IEC61850ServerGroup_"))//IEC61850ServerSlaveGroup
            {
                ucmbs.cmbLocalIP.Items.Clear();
                ucmbs.CmbPortName.Items.Clear();
                foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                {
                    if (ni.Enable == "YES")
                    {
                        //Namrata:20/1/2018
                        ucmbs.CmbPortName.Items.Add(ni.PortName);
                        if (ni.VirtualIP != "0.0.0.0")
                        {
                            ucmbs.cmbLocalIP.Items.Add(ni.VirtualIP);
                        }
                        else
                        {
                            ucmbs.cmbLocalIP.Items.Add(ni.IP);
                        }
                    }
                    //ucmbs.cmbLocalIP.Items.Add(ni.IP);
                    //ucmbs.CmbPortName.Items.Add(ni.PortName);
                }
                if (ucmbs.cmbLocalIP.Items.Count > 0) ucmbs.cmbLocalIP.SelectedIndex = 0;
                //Fill Port No.
                //fillPortNos();
                return ucmbs;
            }

            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("IEC61850ServerGroup_"))//IEC61850ServerSlaveGroup
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Utils.WriteLine(VerboseLevel.DEBUG, "$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (mbsList.Count <= 0) return null;
                return mbsList[idx].getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "***** View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }
            return null;
        }
        public XmlNode exportXMLnode()
        {
            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            XmlNode rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            foreach (IEC61850ServerSlave sn in mbsList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(sn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public void parse61850ServerSlaveGNode(XmlNode mbsgNode, TreeNode tn)
        {
            string strRoutineName = "parse61850ServerSlaveGNode";
            try
            {
                //First set root node name...
                rnName = mbsgNode.Name;
                tn.Nodes.Clear();
                MODBUSSlaveGroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
                foreach (XmlNode node in mbsgNode)
                {
                    TreeNode tmp = null;
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    mbsList.Add(new IEC61850ServerSlave(node, tmp));
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (IEC61850ServerSlave s104Node in mbsList)
            {
                if (s104Node.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<IEC61850ServerSlave> getMODBUSSlaves()
        {
            return mbsList;
        }
        public List<IEC61850ServerSlave> getMODBUSSlavesByFilter(string slaveID)
        {
            List<IEC61850ServerSlave> mbsList1 = new List<IEC61850ServerSlave>();
            if (slaveID.ToLower() == "all") return mbsList;
            else
                foreach (IEC61850ServerSlave mbs in mbsList)
                {
                    if (mbs.getSlaveID == slaveID)
                    {
                        mbsList1.Add(mbs);
                        break;
                    }
                }
            return mbsList1;
        }
    }

}
