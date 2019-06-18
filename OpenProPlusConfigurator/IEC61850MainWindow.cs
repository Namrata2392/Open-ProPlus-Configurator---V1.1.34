using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.XPath;
using System.Drawing;
using System.ComponentModel;
using System.Threading;

namespace OpenProPlusConfigurator
{
    public class IEC61850MainWindow
    {
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        ucIEC61850MainWindow ucIEC61850MainWindow = new ucIEC61850MainWindow();
        private Configure configuration;
        private IXPathNavigable Configuration
        {
            get
            {
                return configuration;
            }
            set
            {
                configuration = value as Configure;
            }
        }
        private string currentIedName = "";
        private string currentReport = "";
        public bool MappingChanged
        {
            get;
            set;
        }
        public string Text { get; private set; }

        private int rcbRowIndex = 0;
        private int rcbColIndex = 0;
        //EditReportForm rpt = null;
        string fLabelFC = "FC filter: ";
        string fLabelType = "Type filter: ";
        string filtersFC = "";
        string filtersType = "";
        int gridItemsAll = 0;
        public IEC61850MainWindow(MasterTypes mType, int mNo, int iNo)
        {
            masterType = mType;
            masterNo = mNo;
            IEDNo = iNo;
            MappingChanged = false;
            ucIEC61850MainWindow.dataGridView1.SortCompare += customSortCompare;
            ucIEC61850MainWindow.openToolStripMenuItemClick += new System.EventHandler(this.openToolStripMenuItem_Click);
            ucIEC61850MainWindow.exitToolStripMenuItemClick += new System.EventHandler(this.exitToolStripMenuItem_Click);
            ucIEC61850MainWindow.ucIEC61850MainWindowLoad += new System.EventHandler(this.ucIEC61850MainWindow_Load);
            ucIEC61850MainWindow.saveToolStripMenuItemClick += new System.EventHandler(this.saveToolStripMenuItem_Click);
            ucIEC61850MainWindow.saveAsToolStripMenuItemClick += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            ucIEC61850MainWindow.QuickFindReferenceTextChanged += new System.EventHandler(this.QuickFindReference_TextChanged);
            ucIEC61850MainWindow.fcFilterLeave += new System.EventHandler(this.fcFilter_Leave);
            ucIEC61850MainWindow.fcFilterSelectedIndexChanged += new System.EventHandler(this.fcFilter_SelectedIndexChanged);
            ucIEC61850MainWindow.typeFilterSelectedIndexChanged += new System.EventHandler(this.typeFilter_SelectedIndexChanged);
            ucIEC61850MainWindow.typeFilterLeave += new System.EventHandler(this.typeFilter_Leave);
            ucIEC61850MainWindow.dataGridView1SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView1_SortCompare);
            ucIEC61850MainWindow.dataGridView1ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            ucIEC61850MainWindow.dataGridView1CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            ucIEC61850MainWindow.treeView1AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            ucIEC61850MainWindow.dataGridView1CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            ucIEC61850MainWindow.dataGridView1KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyUp);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "exitToolStripMenuItem_Click";
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "openToolStripMenuItem_Click";
            try
            {
                Boolean isCanceled = false;
                if (this.MappingChanged)
                {
                    DialogResult ds = MessageBox.Show("Do you want to save changes?", "Confirm Close", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    if (ds == DialogResult.Yes)
                    {
                        if (configuration != null)
                        {
                            this.configuration.SaveConfiguration(configuration.IFullFileNamePath);
                            this.SetWindowTitle();
                        }
                    }
                    if (ds == DialogResult.No)
                    {
                        this.SetWindowTitle();
                    }
                    if (ds == DialogResult.Cancel)
                    {
                        isCanceled = true;
                    }
                }
                if (!isCanceled)
                {
                    Stream myStream = null;
                    ucIEC61850MainWindow.dataGridView1.Visible = true;
                    TreeNode firstIedNode = null;
                    ucIEC61850MainWindow.openFileDialog1.AddExtension = true;
                    ucIEC61850MainWindow.openFileDialog1.InitialDirectory = Application.ExecutablePath;
                    ucIEC61850MainWindow.openFileDialog1.FileName = "";
                    ucIEC61850MainWindow.openFileDialog1.DefaultExt = "icd";
                    ucIEC61850MainWindow.openFileDialog1.Filter = "SCL files|*.icd;*.cid;*.iid;*.ssd;*.scd|IED Capability Description|*.icd|Instantiated IED Description|*.iid|System Specification Description|*.ssd|Substation Configuration Description|*.scd|Configured IED Description|*.cid|All files|*.*";
                    ucIEC61850MainWindow.openFileDialog1.FilterIndex = 1;
                    ucIEC61850MainWindow.openFileDialog1.RestoreDirectory = true;

                    if (ucIEC61850MainWindow.openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            if ((myStream = ucIEC61850MainWindow.openFileDialog1.OpenFile()) != null)
                            {
                                using (myStream)
                                {
                                    configuration = new Configure();
                                    try
                                    {
                                        configuration.Load(myStream);
                                        configuration.IOpenedFileName = ucIEC61850MainWindow.openFileDialog1.SafeFileName;
                                        configuration.IFullFileNamePath = ucIEC61850MainWindow.openFileDialog1.FileName;
                                        this.SetWindowTitle();
                                        ucIEC61850MainWindow.treeView1.Nodes.Clear();
                                        configuration.IIedMappings.Clear();
                                        ucIEC61850MainWindow.treeView1.Visible = true;
                                        ucIEC61850MainWindow.QuickFindReference.Visible = true;
                                        ucIEC61850MainWindow.splitContainer1.Visible = true;
                                        ucIEC61850MainWindow.label2.Visible = true;
                                        ucIEC61850MainWindow.StatusLabel.Text = "Parsing file";
                                        ucIEC61850MainWindow.statusStrip1.Refresh();
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
                                            MessageBox.Show(exc.Message.ToString() + "\n\nTry to open valid SCL file or modify existing one.", "Xml file parsing error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                            this.MappingChanged = false;
                                        }

                                        myStream.Close();
                                        this.MappingChanged = false;
                                        int index = 0;
                                        //check if ied is empty
                                        int notEmptyIeds = 0;
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
                                        TreeNode treeNode = new TreeNode (ucIEC61850MainWindow.openFileDialog1.SafeFileName);
                                        ucIEC61850MainWindow.treeView1.Nodes.Add(treeNode);
                                        foreach (KeyValuePair<string, XPathNodeIterator> t in configuration.Iieds)
                                        {
                                            int count = 0;
                                            index = 0;
                                            TreeNode tNode = new TreeNode(t.Key, 3, 3);

                                            if (firstIedNode == null)
                                            {
                                                firstIedNode = treeNode;
                                                ucIEC61850MainWindow.treeView1.SelectedNode = firstIedNode;
                                                foreach (TreeNode tnp in ucIEC61850MainWindow.treeView1.Nodes)
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
                                            }
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
                                            tNode.Nodes.AddRange(array);
                                            ucIEC61850MainWindow.treeView1.Nodes.Add(tNode);
                                        }
                                        ucIEC61850MainWindow.treeView1.Visible = true;
                                        ucIEC61850MainWindow.treeView1.AutoSize = true;
                                        //Namrata : 16/08/2017
                                        //treeView1.ExpandAll();
                                        ucIEC61850MainWindow.StatusLabel.Text = "File parsed";
                                        ucIEC61850MainWindow.statusStrip1.Refresh();
                                        Cursor.Current = Cursors.Default;
                                        RefreshGrid(configuration.IgridItems);
                                        RefreshGridFilters();

                                        gridItemsAll = configuration.IgridItems.Count;
                                        ucIEC61850MainWindow.DaCount.Text = "DA: " + gridItemsAll.ToString(CultureInfo.InvariantCulture) + "/" + gridItemsAll.ToString(CultureInfo.InvariantCulture);
                                        if (configuration.Iieds.Count == 0 && !configuration.WrongEntry)
                                        {
                                            MessageBox.Show("Parsing file failed: This is not valid SCL file", "SCL file paring error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                            ucIEC61850MainWindow.treeView1.Nodes.Clear();
                                            ucIEC61850MainWindow.treeView1.Visible = false;
                                            ucIEC61850MainWindow.splitContainer1.Visible = false;
                                            ucIEC61850MainWindow.statusStrip1.Text = "";
                                            ucIEC61850MainWindow.StatusLabel.Text = "";
                                            ucIEC61850MainWindow.IedTextLabel.Text = "";
                                            configuration.WrongEntry = false;
                                            configuration = null;
                                            this.MappingChanged = false;
                                        }
                                    }
                                    catch (System.Xml.XmlException ex)
                                    {
                                        MessageBox.Show("Parsing file failed: " + ex.Message, "Xml file parsing error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                        ucIEC61850MainWindow.treeView1.Nodes.Clear();
                                        ucIEC61850MainWindow.treeView1.Visible = false;
                                        ucIEC61850MainWindow.splitContainer1.Visible = false;
                                        ucIEC61850MainWindow.statusStrip1.Text = "";
                                        ucIEC61850MainWindow.StatusLabel.Text = "";
                                        ucIEC61850MainWindow.IedTextLabel.Text = "";
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
                        this.SetWindowTitle();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ucIEC61850MainWindow_Load(object sender, EventArgs e)
        {
            string strRoutineName = "mainWindow_Load";
            try
            {
                ucIEC61850MainWindow.saveAsToolStripMenuItem.Enabled = false;
                ucIEC61850MainWindow.saveToolStripMenuItem.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "saveToolStripMenuItem_Click";
            try
            {
                if (configuration != null)
                {
                    this.configuration.SaveConfiguration(configuration.IFullFileNamePath);
                    this.MappingChanged = false;
                    this.SetWindowTitle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRoutineName = "saveAsToolStripMenuItem_Click";
            try
            {
                ucIEC61850MainWindow.saveConfigDialog.InitialDirectory = Application.ExecutablePath;
                ucIEC61850MainWindow.saveConfigDialog.FileName = "";
                ucIEC61850MainWindow.saveConfigDialog.DefaultExt = "icd";
                ucIEC61850MainWindow.saveConfigDialog.Filter = "SCL files|*.icd;*.cid;*.iid;*.ssd;*.scd|IED Capability Description|*.icd|Instantiated IED Description|*.iid|System Specification Description|*.ssd|Substation Configuration Description|*.scd|Configured IED Description|*.cid|All files|*.*";
                ucIEC61850MainWindow.saveConfigDialog.FilterIndex = 1;
                if (ucIEC61850MainWindow.saveConfigDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (configuration != null)
                        {
                            this.configuration.SaveConfiguration(ucIEC61850MainWindow.saveConfigDialog.FileName);
                            this.MappingChanged = false;
                            this.SetWindowTitle();
                        }
                    }
                    catch (System.FieldAccessException ex)
                    {
                        MessageBox.Show("Error: Could not save file from disk. Original error: " + ex.Message, "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
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
                ucIEC61850MainWindow.StatusLabel.Text = "Wait...";
                Cursor.Current = Cursors.WaitCursor;
                ucIEC61850MainWindow.statusStrip1.Refresh();
                if (items == null)
                {
                    Cursor.Current = Cursors.Default;
                    ucIEC61850MainWindow.dataGridView1.Rows.Clear();
                    ucIEC61850MainWindow.StatusLabel.Text = "All members of Dataset do not exist in IED";
                    ucIEC61850MainWindow.statusStrip1.Refresh();
                }
                else
                {
                    ucIEC61850MainWindow.dataGridView1.Rows.Clear();
                    foreach (GridItem item in items)
                    {
                        int i = ucIEC61850MainWindow.dataGridView1.Rows.Add();
                        ucIEC61850MainWindow.dataGridView1.Rows[i].Cells[0].Value = item.Id.ToString(CultureInfo.InvariantCulture);
                        DataGridViewComboBoxCell cell = ucIEC61850MainWindow.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Mapping Type"]] as DataGridViewComboBoxCell;
                        if (cell != null)
                        {
                            if (!String.IsNullOrEmpty(item.MappingType.Trim()))
                            {
                                cell.Value = item.MappingType;
                            }
                        }
                        DataGridViewComboBoxCell indexCell = ucIEC61850MainWindow.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Index"]] as DataGridViewComboBoxCell;

                        if (indexCell != null)
                        {
                            indexCell.Items.Clear();
                            indexCell.Items.Add(item.Index);
                            indexCell.Value = item.Index;
                        }

                        DataGridViewComboBoxCell fTypeCell = ucIEC61850MainWindow.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Refresh Type"]] as DataGridViewComboBoxCell;
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
                        ucIEC61850MainWindow.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Type"]].Value = item.GridItemType;
                        ucIEC61850MainWindow.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Object Reference"]].Value = item.ObjectReference;
                        ucIEC61850MainWindow.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["FC"]].Value = item.FC;
                        ucIEC61850MainWindow.dataGridView1.Rows[i].Cells[configuration.IColumnHeaders["Description"]].Value = item.Description;
                    }
                }
                ucIEC61850MainWindow.StatusLabel.Text = "";

                List<GridItem> listItems = items as List<GridItem>;
                if (listItems != null)
                {
                    ucIEC61850MainWindow.DaCount.Text = "DA:" + listItems.Count.ToString(CultureInfo.InvariantCulture) + "/" + gridItemsAll.ToString(CultureInfo.InvariantCulture);
                    if (listItems.Count == 0)
                    {
                        ucIEC61850MainWindow.StatusLabel.Text = "All members of Dataset do not exist in IED";
                    }
                }
                else
                {
                    ucIEC61850MainWindow.DaCount.Text = "DA: 0/" + gridItemsAll.ToString(CultureInfo.InvariantCulture);
                    ucIEC61850MainWindow.StatusLabel.Text = "All members of Dataset do not exist in IED";
                }
                ucIEC61850MainWindow.statusStrip1.Refresh();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public  void QuickFindReference_TextChanged(object sender, EventArgs e)
        {
            string strRoutineName = "QuickFindReferenceTextChanged";
            try
            {
                if (configuration == null)
                {
                    return;
                }
                ucIEC61850MainWindow.dataGridView1.Rows.Clear();
                ICollection<GridItem> listGridIt = new List<GridItem>();

                listGridIt = UseFCFilter(configuration.IgridItems as List<GridItem>);
                listGridIt = UseTypeFilter(listGridIt as List<GridItem>);
                listGridIt = UseReferenceFilter(listGridIt as List<GridItem>);
                listGridIt = ShowCurrentIed(listGridIt as List<GridItem>);
                listGridIt = ShowCurrentReport(listGridIt as List<GridItem>);

                RefreshGrid(listGridIt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fcFilter_Leave(object sender, EventArgs e)
        {
            string strRoutineName = "FCFilterLeave";
            try
            {
                if (ucIEC61850MainWindow.fcFilter.Visible)
                {
                    ucIEC61850MainWindow.fcFilter.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fcFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "FCFilterSelectedIndexChanged";
            try
            {
                CheckedListBox tF = sender as CheckedListBox;
                filtersFC = "";

                if (Configuration == null)
                {
                    return;
                }
                if (tF.CheckedItems.Count == tF.Items.Count || tF.CheckedItems.Count == 0)
                {
                    ucIEC61850MainWindow.dataGridView1.Columns["FC"].HeaderText = "FC";
                }
                else
                {
                    ucIEC61850MainWindow.dataGridView1.Columns["FC"].HeaderText = "FC"; //FC.HeaderText = "* FC";
                }

                if (tF.CheckedItems.Count == 0 || tF.Items.Count == tF.CheckedItems.Count)
                {
                    ucIEC61850MainWindow.FilterFCLabel.Text = "No FC filters";
                }
                else
                {
                    foreach (String chItem in tF.CheckedItems)
                    {
                        filtersFC = filtersFC + chItem + "|";
                    }
                    ucIEC61850MainWindow.FilterFCLabel.Text = fLabelFC + "|" + filtersFC;
                }
                RefreshGridFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void typeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtersType = "";
            CheckedListBox tF = sender as CheckedListBox;
            if (configuration == null)
            {
                return;
            }
            if (tF.CheckedItems.Count == tF.Items.Count || tF.CheckedItems.Count == 0)
            {
                ucIEC61850MainWindow.dataGridView1.Columns["Type"].HeaderText = "Type";
                //Type.HeaderText = "Type";
            }
            else
            {
                ucIEC61850MainWindow.dataGridView1.Columns["Type"].HeaderText = "* Type";
                //Type.HeaderText = "* Type";
            }
            if (tF.CheckedItems.Count == 0 || tF.Items.Count == tF.CheckedItems.Count)
            {
                ucIEC61850MainWindow.FilterTypeLabel.Text = "No Type filters";
            }
            else
            {
                foreach (String chItem in tF.CheckedItems)
                {
                    filtersType = filtersType + chItem + "|";
                }
                ucIEC61850MainWindow.FilterTypeLabel.Text = fLabelType + "|" + filtersType;
            }
            RefreshGridFilters();
        }
        private void typeFilter_Leave(object sender, EventArgs e)
        {
            if (ucIEC61850MainWindow.typeFilter.Visible)
            {
                ucIEC61850MainWindow.typeFilter.Visible = false;
            }
        }

        public ICollection<GridItem> UseFCFilter(ICollection<GridItem> listGridIt)
        {
            if (listGridIt == null)
            {
                return null;
            }
            if (ucIEC61850MainWindow.fcFilter.CheckedItems.Count == 0)
            {
                return listGridIt;
            }
            List<GridItem> tmp = new List<GridItem>();
            foreach (GridItem i in listGridIt as List<GridItem>)
            {
                foreach (var fc in ucIEC61850MainWindow.fcFilter.CheckedItems)
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
            if (ucIEC61850MainWindow.typeFilter.CheckedItems.Count == 0)
            {
                return listGridIt;
            }
            List<GridItem> tmp = new List<GridItem>();

            foreach (GridItem i in listGridIt as List<GridItem>)
            {
                foreach (var ty in ucIEC61850MainWindow.typeFilter.CheckedItems)
                {
                    if (i.GridItemType.Contains(ty.ToString()))
                    {
                        tmp.Add(i);
                    }
                }
            }
            return tmp.Count == 0 ? null : tmp;
        }
        public ICollection<GridItem> UseReferenceFilter(ICollection<GridItem> listGridIt)
        {
            if (listGridIt == null)
            {
                return null;
            }
            if (ucIEC61850MainWindow.QuickFindReference.Text.Length == 0)
            {
                return listGridIt;
            }
            List<GridItem> tmp = new List<GridItem>();
            foreach (GridItem i in listGridIt as List<GridItem>)
            {
                if (i.ObjectReference.ToUpperInvariant().Contains(ucIEC61850MainWindow.QuickFindReference.Text.ToUpperInvariant()))
                {
                    tmp.Add(i);
                }
            }
            return tmp.Count == 0 ? null : tmp;
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
                                    //if (gItem.FC.Equals("MX") || gItem.FC.Equals("ST"))
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
                ucIEC61850MainWindow.StatusLabel.Text = "All members of Dataset do not exist in IED";
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
        private void customSortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            int a, b;
            bool aIsNum = false, bIsNum = false;

            if (e.CellValue1 != null && e.CellValue1.ToString() == string.Empty)
            {
                a = 0;
            }
            else if (e.CellValue1 == null)
            {
                a = 0;
            }
            else
            {
                aIsNum = int.TryParse(e.CellValue1.ToString(), out a);
            }

            if (e.CellValue2 != null && e.CellValue2.ToString() == string.Empty)
            {
                b = 0;
            }
            else if (e.CellValue2 == null)
            {
                b = 0;
            }
            else
            {
                bIsNum = int.TryParse(e.CellValue2.ToString(), out b);
            }

            if (aIsNum && bIsNum)
            {
                e.SortResult = a.CompareTo(b);
                e.Handled = true;
            }
            else
            {
                if (e.CellValue1 == null && e.CellValue2 == null)
                {
                    e.SortResult = 0;
                    e.Handled = true;
                }
                else if (e.CellValue1 == null && e.CellValue2 != null)
                {
                    e.SortResult = -1;
                    e.Handled = true;
                }
                else if (e.CellValue1 != null && e.CellValue2 == null)
                {
                    e.SortResult = 1;
                    e.Handled = true;
                }
                else
                {
                    e.SortResult = e.CellValue1.ToString().CompareTo(e.CellValue2.ToString());
                    e.Handled = true;
                }
            }
        }
        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {

        }
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int col = e.ColumnIndex;
                switch (col)
                {
                    case 4:
                        {
                            if (ucIEC61850MainWindow.typeFilter.Visible == false)
                            {
                                int offset = ucIEC61850MainWindow.dataGridView1.Columns[1].Width / 2 + ucIEC61850MainWindow.dataGridView1.Columns[1].Width + ucIEC61850MainWindow.dataGridView1.Columns[2].Width + ucIEC61850MainWindow.dataGridView1.Columns[3].Width;
                                ucIEC61850MainWindow.typeFilter.Location = new System.Drawing.Point(ucIEC61850MainWindow.dataGridView1.Location.X + offset, ucIEC61850MainWindow.dataGridView1.Location.Y + 20);

                                if (ucIEC61850MainWindow.typeFilter.Items.Count == 0)
                                {
                                    foreach (var t in configuration.Itypes)
                                    {
                                        ucIEC61850MainWindow.typeFilter.Items.Add(t.Value);
                                    }
                                    for (int i = 0; i < ucIEC61850MainWindow.typeFilter.Items.Count; i++)
                                    {
                                        ucIEC61850MainWindow.typeFilter.SetItemCheckState(i, CheckState.Unchecked);
                                    }
                                }
                                ucIEC61850MainWindow.typeFilter.BringToFront();
                                ucIEC61850MainWindow.typeFilter.AutoSize = true;
                                ucIEC61850MainWindow.typeFilter.Visible = true;
                                ucIEC61850MainWindow.typeFilter.Focus();
                            }
                            break;
                        }
                    case 6:
                        {
                            if (ucIEC61850MainWindow.fcFilter.Visible == false)
                            {
                                int offset = ucIEC61850MainWindow.dataGridView1.Columns[1].Width / 2 + ucIEC61850MainWindow.dataGridView1.Columns[1].Width +
                               ucIEC61850MainWindow.dataGridView1.Columns[2].Width + ucIEC61850MainWindow.dataGridView1.Columns[3].Width + ucIEC61850MainWindow.dataGridView1.Columns[4].Width + ucIEC61850MainWindow.dataGridView1.Columns[5].Width;
                                ucIEC61850MainWindow.fcFilter.Location = new System.Drawing.Point(ucIEC61850MainWindow.dataGridView1.Location.X + offset, ucIEC61850MainWindow.dataGridView1.Location.Y + 20);//453, 47);
                                if (ucIEC61850MainWindow.fcFilter.Items.Count == 0)
                                {
                                    foreach (var t in configuration.ISupportedFC)
                                    {
                                        ucIEC61850MainWindow.fcFilter.Items.Add(t.Key);
                                    }
                                    for (int i = 0; i < ucIEC61850MainWindow.fcFilter.Items.Count; i++)
                                    {
                                        ucIEC61850MainWindow.fcFilter.SetItemCheckState(i, CheckState.Unchecked);
                                    }
                                }
                                ucIEC61850MainWindow.fcFilter.AutoSize = true;
                                ucIEC61850MainWindow.fcFilter.BringToFront();
                                ucIEC61850MainWindow.fcFilter.Visible = true;
                                ucIEC61850MainWindow.fcFilter.Focus();
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string fetchType = "";
            string repFromAddres = "";
            string previousRefresh = "";
            int itemIndex = -1;
            int selectedItem = 0;

            PrivateTag privTag;
            PrivateTag tempPrivTag;
            PrivateTag removedPrivTag;
            PrivateTag changedPrivTag;
            int size = 0;
            int lastIndex = 0;
            int actualIndex = 0;
            int newIndex = 0;

            var cell = ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            DataGridViewComboBoxCell cellMapping = (DataGridViewComboBoxCell)ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Mapping Type"]];
            DataGridViewComboBoxCell cellFetchType = (DataGridViewComboBoxCell)ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Refresh Type"]];
            DataGridViewComboBoxCell cellIndex = (DataGridViewComboBoxCell)ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Index"]];
            DataGridViewTextBoxCell cellDesc = (DataGridViewTextBoxCell)ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Description"]];

            if (ucIEC61850MainWindow.dataGridView1.EditingControl != null)
            {
                ucIEC61850MainWindow.dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            }
            else
            {
                ucIEC61850MainWindow.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Selected = true;
            }

            if (e.ColumnIndex != configuration.IColumnHeaders["Mapping Type"] && e.ColumnIndex != configuration.IColumnHeaders["Description"] && e.ColumnIndex != configuration.IColumnHeaders["Index"] && e.ColumnIndex != configuration.IColumnHeaders["Refresh Type"])
            {
                return;
            }
            foreach (var item in configuration.IgridItems)
            {
                if ((item.ObjectReference == ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Object Reference"]].Value.ToString()) && item.Id.ToString(CultureInfo.InvariantCulture).Equals(ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Id"]].Value.ToString()))
                {
                    if (e.ColumnIndex == configuration.IColumnHeaders["Mapping Type"])
                    {
                        if (Equals(cell.Value, "") || cell.Value == null)
                        {
                            item.IsChecked = false;
                            item.MappingType = "";
                            item.Index = "";
                            item.RefreshType = "";
                        }
                        else
                        {
                            item.IsChecked = true;
                            item.MappingType = cell.Value.ToString();
                            item.RefreshType = cellFetchType.Value.ToString();
                            item.Index = cellIndex.Value.ToString();

                        }
                        this.MappingChanged = true;
                    }
                    if (e.ColumnIndex == configuration.IColumnHeaders["Index"])
                    {
                        if (Convert.ToInt32(item.Index) == Convert.ToInt32(cellIndex.Value))
                        {
                            return;
                        }
                        if (cell.Value == null || String.IsNullOrEmpty(cell.Value.ToString()))
                        {
                            item.Index = "";
                        }
                        else
                        {
                            if (!cellIndex.Items.Contains(cell.Value))
                            {
                                cellIndex.Items.Add(cell.Value);
                            }
                            actualIndex = Convert.ToInt32(item.Index, CultureInfo.InvariantCulture);
                            newIndex = Convert.ToInt32(cellIndex.Value, CultureInfo.InvariantCulture);
                            tempPrivTag = configuration.IIedMappings[item.IedName + "." + item.MappingType][actualIndex - 1];

                            if (actualIndex > newIndex)
                            {
                                size = configuration.IIedMappings[item.IedName + "." + item.MappingType].Count;
                                lastIndex = Convert.ToInt32(configuration.IIedMappings[item.IedName + "." + item.MappingType][size - 1].Index, CultureInfo.InvariantCulture);
                                configuration.IIedMappings[item.IedName + "." + item.MappingType][actualIndex - 1] = null;

                                if (lastIndex == actualIndex)
                                {
                                    configuration.IIedMappings[item.IedName + "." + item.MappingType].RemoveAt(lastIndex - 1);
                                }

                                item.Index = cellIndex.Value.ToString();
                                tempPrivTag.Index = item.Index;
                                configuration.IIedMappings[item.IedName + "." + item.MappingType][newIndex - 1] = tempPrivTag;
                            }
                            else
                            {
                                size = configuration.IIedMappings[item.IedName + "." + item.MappingType].Count;
                                lastIndex = Convert.ToInt32(configuration.IIedMappings[item.IedName + "." + item.MappingType][size - 1].Index, CultureInfo.InvariantCulture);

                                if (!item.Index.Equals(cellIndex.Value.ToString()))
                                {
                                    configuration.IIedMappings[item.IedName + "." + item.MappingType][Convert.ToInt32(item.Index, CultureInfo.InvariantCulture) - 1] = null;
                                    item.Index = cellIndex.Value.ToString();
                                    tempPrivTag.Index = item.Index;

                                    if (newIndex > lastIndex)
                                    {
                                        configuration.IIedMappings[item.IedName + "." + item.MappingType].Add(tempPrivTag);
                                    }
                                    else
                                    {
                                        configuration.IIedMappings[item.IedName + "." + item.MappingType][newIndex - 1] = tempPrivTag;
                                    }
                                }
                            }
                        }
                        this.MappingChanged = true;
                    }

                    if (e.ColumnIndex == configuration.IColumnHeaders["Refresh Type"])
                    {
                        if (Equals(cell.Value, "") || cell.Value == null)
                        {
                            item.RefreshType = "";
                        }
                        else
                        {
                            previousRefresh = item.RefreshType;
                            item.RefreshType = cellFetchType.Value.ToString();
                        }
                    }
                    if (e.ColumnIndex == configuration.IColumnHeaders["Description"])
                    {
                        if (cell.Value != null && ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Mapping Type"]].Value != null && !ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Mapping Type"]].Value.Equals(""))
                        {
                            item.Description = cell.Value.ToString();
                        }
                        else
                        {
                            item.Description = "";
                            cell.Value = "";
                        }
                        //update if added to mapping
                        if (configuration.IadditionalTags.ContainsKey(item.ObjectReference + item.FC + item.IdDI))
                        {
                            var tmp = configuration.IadditionalTags[item.ObjectReference + item.FC + item.IdDI];
                            tmp.Description = item.Description;
                            this.MappingChanged = true;
                        }
                    }
                    if (item.IsChecked && !(String.IsNullOrEmpty(item.Index.Trim()) || String.IsNullOrEmpty(item.RefreshType.Trim()) || String.IsNullOrEmpty(item.MappingType.Trim())))
                    {
                        fetchType = item.RefreshType;
                        if (!configuration.IadditionalTags.ContainsKey(item.ObjectReference + item.FC + item.IdDI))
                        {
                            if (!fetchType.Equals("On Request"))
                            {
                                repFromAddres = configuration.FindReportFromAddress(fetchType);
                                if (!String.IsNullOrEmpty(item.RefreshType) && !configuration.IUsedRcb.Contains(configuration.Ireports[repFromAddres]))
                                {
                                    configuration.IUsedRcb.Add(configuration.Ireports[repFromAddres]);
                                }
                                else
                                {
                                    fetchType = item.RefreshType;
                                }
                            }
                            else
                            {
                                fetchType = item.RefreshType;
                            }
                            if (!String.IsNullOrEmpty(previousRefresh))
                            {
                                repFromAddres = configuration.FindReportFromAddress(previousRefresh);
                                configuration.IUsedRcb.Remove(configuration.Ireports[repFromAddres]);
                            }
                            int count = configuration.IIedMappings[item.IedName + "." + item.MappingType].Count;

                            if (Convert.ToInt32(item.Index, CultureInfo.InvariantCulture) > count + 1)
                            {
                                configuration.IIedMappings[item.IedName + "." + item.MappingType].Add(null);
                            }
                            privTag = new PrivateTag(item.IdDI, item.MappingType, item.Index, item.ObjectReference, item.FC, item.Description, item.IedName, item.RefreshType);
                            configuration.IadditionalTags.Add(item.ObjectReference + item.FC + item.IdDI, privTag);
                            configuration.IIedMappings[item.IedName + "." + item.MappingType].Add(configuration.IadditionalTags[item.ObjectReference + item.FC + item.IdDI]);
                            cellIndex.Value = item.Index;
                        }
                        else
                        {
                            changedPrivTag = configuration.IadditionalTags[item.ObjectReference + item.FC + item.IdDI];
                            size = configuration.IIedMappings[changedPrivTag.IedName + "." + changedPrivTag.MappingType].Count - 1;
                            itemIndex = Convert.ToInt32(changedPrivTag.Index, CultureInfo.InvariantCulture);

                            if (!(cellIndex.Value.Equals(changedPrivTag.Index) && cellMapping.Value.Equals(changedPrivTag.MappingType) && cellFetchType.Value.Equals(changedPrivTag.Rtype)))
                            {
                                if (!(cellIndex.Value.Equals(changedPrivTag.Index) && cellMapping.Value.Equals(changedPrivTag.MappingType)))
                                {
                                    configuration.IIedMappings[changedPrivTag.IedName + "." + changedPrivTag.MappingType][itemIndex - 1] = null;
                                    configuration.IadditionalTags.Remove(item.ObjectReference + item.FC + item.IdDI);
                                    selectedItem = size;

                                    while ((selectedItem >= 0) && (configuration.IIedMappings[changedPrivTag.IedName + "." + changedPrivTag.MappingType][selectedItem] == null))
                                    {
                                        configuration.IIedMappings[changedPrivTag.IedName + "." + changedPrivTag.MappingType].RemoveAt(selectedItem);
                                        selectedItem--;
                                    }
                                }
                                if (!fetchType.Equals("On Request"))
                                {
                                    repFromAddres = configuration.FindReportFromAddress(fetchType);

                                    if (!String.IsNullOrEmpty(item.RefreshType.Trim()) && !configuration.IUsedRcb.Contains(configuration.Ireports[repFromAddres]))
                                    {
                                        configuration.IUsedRcb.Add(configuration.Ireports[repFromAddres]);
                                    }
                                    else
                                    {
                                        fetchType = item.RefreshType;
                                    }
                                }
                                else
                                {
                                    fetchType = item.RefreshType;
                                }

                                changedPrivTag.Index = item.Index;
                                changedPrivTag.MappingType = item.MappingType;
                                changedPrivTag.Rtype = fetchType;
                                if (!configuration.IadditionalTags.ContainsKey(item.ObjectReference + item.FC + item.IdDI))
                                {
                                    configuration.IadditionalTags.Add(item.ObjectReference + item.FC + item.IdDI, changedPrivTag);
                                }
                                if (!configuration.IIedMappings[item.IedName + "." + item.MappingType].Contains(changedPrivTag))
                                {
                                    configuration.IIedMappings[item.IedName + "." + item.MappingType].Add(changedPrivTag);
                                }
                                this.MappingChanged = true;
                            }
                        }
                    }
                    else
                    {
                        if (configuration.IadditionalTags.ContainsKey(item.ObjectReference + item.FC + item.IdDI))
                        {
                            removedPrivTag = configuration.IadditionalTags[item.ObjectReference + item.FC + item.IdDI];
                            configuration.IadditionalTags.Remove(item.ObjectReference + item.FC + item.IdDI);

                            size = configuration.IIedMappings[removedPrivTag.IedName + "." + removedPrivTag.MappingType].Count - 1;
                            itemIndex = Convert.ToInt32(removedPrivTag.Index, CultureInfo.InvariantCulture);
                            configuration.IIedMappings[removedPrivTag.IedName + "." + removedPrivTag.MappingType][itemIndex - 1] = null;
                            selectedItem = size;
                            while ((selectedItem >= 0) && (configuration.IIedMappings[removedPrivTag.IedName + "." + removedPrivTag.MappingType][selectedItem] == null))
                            {
                                configuration.IIedMappings[removedPrivTag.IedName + "." + removedPrivTag.MappingType].RemoveAt(selectedItem);
                                selectedItem--;
                            }
                        }
                    }
                    UpdateMappingStatistic(item.IedName);
                    break;
                }
            }
            //this.isComboChangeCommited = false;
            ucIEC61850MainWindow.dataGridView1.Refresh();
            this.SetWindowTitle();
        }
        public void SetWindowTitle()
        {
            if (this.configuration != null)
            {
                this.Text = (this.MappingChanged ? "*" : "") + this.configuration.IFullFileNamePath + " - " + "Ashida Iec61850 RT Configuration Tool";

                if (this.MappingChanged)
                {
                    ucIEC61850MainWindow.saveAsToolStripMenuItem.Enabled = true;
                    ucIEC61850MainWindow.saveToolStripMenuItem.Enabled = true;
                }
                else
                {
                    ucIEC61850MainWindow.saveAsToolStripMenuItem.Enabled = false;
                    ucIEC61850MainWindow.saveToolStripMenuItem.Enabled = false;
                }
            }
            else
            {
                this.Text = (this.MappingChanged ? "*" : "") + "Ashida Iec61850 RT Configuration Tool";
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string[] reportKey;
            string[] iedKey;
            string repTemp = "";

            foreach (TreeNode tnp in e.Node.TreeView.Nodes)
            {
                tnp.BackColor = Color.White;
                tnp.ForeColor = Color.Black;
                foreach (TreeNode tn in tnp.Nodes)
                {
                    tn.BackColor = Color.White;
                    tn.ForeColor = Color.Black;
                }
            }
            e.Node.BackColor = Color.LightBlue;
            e.Node.ForeColor = Color.Black;

            if (!configuration.Iieds.ContainsKey(e.Node.Text))
            {
                currentIedName = "";
                currentReport = "";

                foreach (var r in configuration.Ireports)
                {
                    if (e.Node.Parent != null)
                    {
                        reportKey = e.Node.Text.Split('.');
                        iedKey = reportKey[0].Split('/');
                        repTemp = reportKey[1] + "." + e.Node.Parent.Text + "." + iedKey[0] + "." + iedKey[1];

                        if (r.Key.Contains(repTemp))
                        {
                            currentIedName = e.Node.Parent.Text;
                            currentReport = repTemp;
                            break;
                        }
                    }
                }
            }
            else
            {
                currentIedName = e.Node.Text;
                currentReport = "";
            }
            if (configuration == null)
            {
                return;
            }

            RefreshGridFilters();

        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            rcbRowIndex = e.RowIndex;
            rcbColIndex = e.ColumnIndex;
            string mapping = "";
            if (e.RowIndex >= 0)
            {
                if (ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Mapping Type"]].Value != null && !ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Mapping Type"]].Value.Equals(""))
                {
                    mapping = ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Mapping Type"]].Value.ToString();
                    ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Mapping Type"]].ReadOnly = false;
                    ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Index"]].ReadOnly = false;
                    ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Refresh Type"]].ReadOnly = false;
                    ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Description"]].ReadOnly = false;
                }
                else
                {
                    ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Mapping Type"]].ReadOnly = false;
                    ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Index"]].ReadOnly = true;
                    ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Refresh Type"]].ReadOnly = true;
                    ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Description"]].ReadOnly = true;
                }

                if (ucIEC61850MainWindow.dataGridView1.EditingControl != null)
                {
                    ucIEC61850MainWindow.dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                }
                else
                {
                    ucIEC61850MainWindow.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Selected = true;
                }
                if (ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["FC"]].Value.ToString() == "CO")
                {
                    string model = "";
                    foreach (var tmp in configuration.IgridItems)
                    {
                        if (tmp.ObjectReference == ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Object Reference"]].Value.ToString())
                        {
                            model = tmp.ControlModel;
                            break;
                        }
                    }

                    ucIEC61850MainWindow.StatusLabel.Text = model;
                }
                else
                {
                    ucIEC61850MainWindow.StatusLabel.Text = "";
                }
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == configuration.IColumnHeaders["Mapping Type"])
            {
                string fc = ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["FC"]].Value.ToString();
                string reference = ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[configuration.IColumnHeaders["Object Reference"]].Value.ToString();

                int index = 0;

                FindRCBfromItem(reference, fc);

                ucIEC61850MainWindow.dataGridView1.BeginEdit(true);

                if (ucIEC61850MainWindow.dataGridView1.EditingControl != null)
                {
                    ComboBox comboBox = (ComboBox)ucIEC61850MainWindow.dataGridView1.EditingControl;
                    comboBox.Items.Clear();

                    switch (fc)
                    {
                        case "CO":
                            {
                                //comboBox.Items.Add(" ");
                                comboBox.Items.Add("Control");
                                break;
                            }
                        default:
                            {
                                //comboBox.Items.Add(" ");
                                comboBox.Items.Add("Analog");
                                comboBox.Items.Add("IedDin");
                                comboBox.Items.Add("Parameter");
                                break;
                            }
                    }
                    index = comboBox.FindStringExact(mapping);
                    comboBox.SelectedIndex = index;
                    comboBox.SelectionChangeCommitted += new EventHandler(comboBoxMappingSelectionChangeCommitted);
                }
            }
            else
                if (e.RowIndex >= 0 && e.ColumnIndex == configuration.IColumnHeaders["Index"])
            {
                if (!String.IsNullOrEmpty(mapping.Trim()))
                {
                    string fc = ucIEC61850MainWindow.dataGridView1.Rows[rcbRowIndex].Cells[configuration.IColumnHeaders["FC"]].Value.ToString();
                    int cellId = Int32.Parse(ucIEC61850MainWindow.dataGridView1.Rows[rcbRowIndex].Cells[configuration.IColumnHeaders["Id"]].Value.ToString(), CultureInfo.InvariantCulture);
                    List<PrivateTag> pTag;
                    string selItem = "";
                    string cellValue = "";
                    int listIter = 0;
                    int lastIndex = 0;
                    int i = 0;

                    ucIEC61850MainWindow.dataGridView1.BeginEdit(true);
                    if (ucIEC61850MainWindow.dataGridView1.EditingControl != null &&
                        ucIEC61850MainWindow.dataGridView1[configuration.IColumnHeaders["Mapping Type"], rcbRowIndex].Value != null &&
                        !String.IsNullOrEmpty(ucIEC61850MainWindow.dataGridView1[configuration.IColumnHeaders["Mapping Type"], rcbRowIndex].Value.ToString().Trim()))
                    {
                        ComboBox comboBox1 = (ComboBox)ucIEC61850MainWindow.dataGridView1.EditingControl;

                        selItem = ucIEC61850MainWindow.dataGridView1[configuration.IColumnHeaders["Mapping Type"], rcbRowIndex].Value.ToString();
                        cellValue = ucIEC61850MainWindow.dataGridView1[configuration.IColumnHeaders["Index"], rcbRowIndex].Value.ToString();
                        comboBox1.Sorted = true;

                        comboBox1.Items.Clear();
                        if (cellValue != null && !String.IsNullOrEmpty(cellValue.Trim()))
                        {
                            if (!comboBox1.Items.Contains(cellValue))
                            {
                                comboBox1.Items.Add(cellValue);
                            }
                            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(cellValue);
                        }

                        pTag = configuration.IIedMappings[(configuration.IgridItems as List<GridItem>)[cellId].IedName + "." + selItem];

                        foreach (PrivateTag idx in pTag)
                        {
                            listIter++;
                            if (idx == null && !comboBox1.Items.Contains(listIter.ToString(CultureInfo.InvariantCulture)))
                            {
                                comboBox1.Items.Add(listIter.ToString(CultureInfo.InvariantCulture));
                            }
                            i++;
                            if (i == pTag.Count)
                            {
                                listIter++;
                                lastIndex = Convert.ToInt32(pTag[i - 1].Index);
                                if (!comboBox1.Items.Contains((lastIndex + 1).ToString(CultureInfo.InvariantCulture)))
                                {
                                    comboBox1.Items.Add((lastIndex + 1).ToString(CultureInfo.InvariantCulture));
                                }
                            }
                        }
                        comboBox1.Sorted = true;
                        comboBox1.Visible = true;
                    }
                }
            }
            else
            if (e.ColumnIndex == configuration.IColumnHeaders["Refresh Type"] && e.RowIndex >= 0 && ucIEC61850MainWindow.dataGridView1[configuration.IColumnHeaders["Mapping Type"], rcbRowIndex].Value != null)
            {
                if (!String.IsNullOrEmpty(ucIEC61850MainWindow.dataGridView1[configuration.IColumnHeaders["Mapping Type"], rcbRowIndex].Value.ToString().Trim()))
                {
                    string fc = ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    string reference = ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    string oldRefreshType = ucIEC61850MainWindow.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    DataGridViewComboBoxCell refTypeCell = (DataGridViewComboBoxCell)ucIEC61850MainWindow.dataGridView1.Rows[rcbRowIndex].Cells[3];

                    FindRCBfromItem(reference, fc);
                    ucIEC61850MainWindow.dataGridView1.BeginEdit(true);
                    if (ucIEC61850MainWindow.dataGridView1.EditingControl != null)
                    {
                        ComboBox comboBox2 = (ComboBox)ucIEC61850MainWindow.dataGridView1.EditingControl;
                        string selItem = "";

                        if (!comboBox2.Items.Contains(oldRefreshType))
                        {
                            comboBox2.Items.Add(oldRefreshType);
                        }
                        comboBox2.SelectedIndex = comboBox2.Items.IndexOf(oldRefreshType);

                        if (comboBox2.SelectedItem == null || String.IsNullOrEmpty(comboBox2.SelectedItem.ToString().Trim()))
                        {
                            ucIEC61850MainWindow.dataGridView1[2, rcbRowIndex].Value = "";
                            ucIEC61850MainWindow.dataGridView1[3, rcbRowIndex].Value = "";
                        }
                        else
                        {
                            selItem = comboBox2.SelectedItem.ToString();

                            if (!comboBox2.Items.Contains("On Request"))
                            {
                                comboBox2.Items.Add("On Request");
                            }
                            switch (fc)
                            {
                                case "CO":
                                    {
                                        // Update preferenced Refresh type for CO
                                        ucIEC61850MainWindow.dataGridView1[3, rcbRowIndex].Value = "On Request";
                                        break;
                                    }
                                default:
                                    {
                                        string firstKey;
                                        List<Report> locRCBtmp = configuration.ILocalRcb as List<Report>;
                                        List<Report>.Enumerator en = locRCBtmp.GetEnumerator();
                                        if (en.MoveNext())
                                        {
                                            firstKey = en.Current.Address;
                                            foreach (var v in configuration.ILocalRcb)
                                            {
                                                if (!comboBox2.Items.Contains(v.Address))
                                                {
                                                    comboBox2.Items.Add(v.Address);
                                                }
                                                if (!refTypeCell.Items.Contains(v.Address))
                                                {
                                                    refTypeCell.Items.Add(v.Address);
                                                }
                                            }
                                            if (ucIEC61850MainWindow.dataGridView1[3, rcbRowIndex].Value != null)
                                            {
                                                if (String.IsNullOrEmpty(ucIEC61850MainWindow.dataGridView1[3, rcbRowIndex].Value.ToString().Trim()))
                                                {
                                                    ucIEC61850MainWindow.dataGridView1[3, rcbRowIndex].Value = firstKey;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ucIEC61850MainWindow.dataGridView1[3, rcbRowIndex].Value = "On Request";
                                        }
                                        break;
                                    }
                            }
                        }
                        comboBox2.Visible = true;
                    }
                }
            }
        }
        void comboBoxMappingSelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string fc = ucIEC61850MainWindow.dataGridView1.Rows[rcbRowIndex].Cells[6].Value.ToString();
            var cell = ucIEC61850MainWindow.dataGridView1.Rows[rcbRowIndex].Cells[rcbColIndex];
            DataGridViewComboBoxCell mappingCell = (DataGridViewComboBoxCell)ucIEC61850MainWindow.dataGridView1.Rows[rcbRowIndex].Cells[1];
            DataGridViewComboBoxCell indexCell = (DataGridViewComboBoxCell)ucIEC61850MainWindow.dataGridView1.Rows[rcbRowIndex].Cells[2];
            DataGridViewComboBoxCell refTypeCell = (DataGridViewComboBoxCell)ucIEC61850MainWindow.dataGridView1.Rows[rcbRowIndex].Cells[3];
            int cellId = Int32.Parse(ucIEC61850MainWindow.dataGridView1.Rows[rcbRowIndex].Cells[0].Value.ToString(), CultureInfo.InvariantCulture);

            List<PrivateTag> pTag;
            string selItem = "";
            string lastIndex = "";
            string count = "";
            string nextIndex = "";

            if (String.IsNullOrEmpty(cb.SelectedItem.ToString().Trim()))
            {
                ucIEC61850MainWindow.dataGridView1[configuration.IColumnHeaders["Mapping Type"], rcbRowIndex].Value = "";
                ucIEC61850MainWindow.dataGridView1[configuration.IColumnHeaders["Index"], rcbRowIndex].Value = "";
                ucIEC61850MainWindow.dataGridView1[configuration.IColumnHeaders["Refresh Type"], rcbRowIndex].Value = "";
                ucIEC61850MainWindow.dataGridView1[configuration.IColumnHeaders["Description"], rcbRowIndex].Value = "";
                ucIEC61850MainWindow.dataGridView1.EndEdit();
                return;
            }

            if (rcbColIndex == 1)
            {
                selItem = cb.SelectedItem.ToString();

                if ((mappingCell.Value != null && !mappingCell.Value.Equals(selItem)) || (mappingCell.Value == null && selItem != null))
                {
                    mappingCell.Value = selItem;

                    if (!refTypeCell.Items.Contains("On Request"))
                    {
                        refTypeCell.Items.Add("On Request");
                    }
                    switch (fc)
                    {
                        case "CO":
                            {
                                // Update preferenced Refresh type for CO
                                ucIEC61850MainWindow.dataGridView1[3, rcbRowIndex].Value = "On Request";
                                break;
                            }
                        default:
                            {
                                string firstKey;
                                List<Report> locRCBtmp = configuration.ILocalRcb as List<Report>;
                                List<Report>.Enumerator en = locRCBtmp.GetEnumerator();
                                if (en.MoveNext())
                                {
                                    firstKey = en.Current.Address;
                                    foreach (var v in configuration.ILocalRcb)
                                    {
                                        if (!cb.Items.Contains(v.Address) && !refTypeCell.Items.Contains(v.Address))
                                        {
                                            refTypeCell.Items.Add(v.Address);
                                        }
                                    }
                                    ucIEC61850MainWindow.dataGridView1[3, rcbRowIndex].Value = firstKey;
                                }
                                else
                                {
                                    ucIEC61850MainWindow.dataGridView1[3, rcbRowIndex].Value = "On Request";
                                }

                                break;
                            }
                    }

                    indexCell.Items.Clear();
                    indexCell.Sorted = true;
                    pTag = configuration.IIedMappings[(configuration.IgridItems as List<GridItem>)[cellId].IedName + "." + selItem];
                    count = pTag.Count.ToString(CultureInfo.InvariantCulture);
                    if (count.Equals("0"))
                    {
                        nextIndex = "1";
                    }
                    else
                    {
                        lastIndex = pTag[Convert.ToInt32(count, CultureInfo.InvariantCulture) - 1].Index;
                        nextIndex = (Convert.ToInt32(lastIndex, CultureInfo.InvariantCulture) + 1).ToString(CultureInfo.InvariantCulture);
                    }

                    if (!indexCell.Items.Contains(nextIndex))
                    {
                        indexCell.Items.Add(nextIndex);
                        indexCell.Value = nextIndex;
                    }
                }
            }
            if (rcbColIndex == 2)
            {
                selItem = cb.SelectedItem.ToString();

                if (!cb.Items.Contains(selItem))
                {
                    cb.Sorted = true;
                    cb.Items.Add(selItem);
                }
                cb.SelectedItem = selItem;
            }
            ucIEC61850MainWindow.dataGridView1.EndEdit();
            //this.isComboChangeCommited = true;
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
           
            ucIEC61850MainWindow.MappedAnalog.Text = "A: " + lastIndexAnalog.ToString(CultureInfo.InvariantCulture);
            ucIEC61850MainWindow.MappedDin.Text = "D: " + lastIndexIedDin.ToString(CultureInfo.InvariantCulture);
            ucIEC61850MainWindow.MappedParameter.Text = "P: " + lastIndexParameter.ToString(CultureInfo.InvariantCulture);
            ucIEC61850MainWindow.MappedControl.Text = "C: " + lastIndexControl.ToString(CultureInfo.InvariantCulture);

            if (configuration.Iieds.ContainsKey(iedName))
            {
                ucIEC61850MainWindow.IedTextLabel.Text = "IED: " + iedName;
            }
            else
            {
                ucIEC61850MainWindow.IedTextLabel.Text = "";
            }

            ucIEC61850MainWindow.statusStrip1.Refresh();
        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeView tView = sender as TreeView;

            foreach (TreeNode tnp in e.Node.TreeView.Nodes)
            {
                tnp.BackColor = Color.White;
                tnp.ForeColor = Color.Black;
                foreach (TreeNode tn in tnp.Nodes)
                {
                    tn.BackColor = Color.White;
                    tn.ForeColor = Color.Black;
                }
            }
            e.Node.BackColor = Color.LightBlue;
            e.Node.ForeColor = Color.Black;
            tView.SelectedNode = e.Node;

            if (e.Button == MouseButtons.Right)
            {
                //rpt = new EditReportForm();
                if (!configuration.Iieds.ContainsKey(e.Node.Text))
                {
                    //rpt.Text = e.Node.Text;
                    string iedName = e.Node.Parent.Text;
                    string[] reportKey;
                    configuration.Ireports.ContainsKey(iedName);
                    Report report = null;
                    foreach (var r in configuration.Ireports)
                    {
                        if (e.Node.Parent != null)
                        {
                            reportKey = e.Node.Text.Split('.', '/');

                            if (r.Key.Contains(reportKey[2] + "." + iedName + "." + reportKey[0] + "." + reportKey[1]))
                            {
                                report = r.Value;
                                break;
                            }
                        }
                    }
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
                                if (tab[1].Equals(iedName))
                                {
                                    dsName = tab[1] + tab[2] + "/" + tab[3] + "." + tab[0];
                                    list.Add(dsName);
                                }
                            }
                        }
                    }

                    list.Sort();
                    //rpt.SetReport(configuration, report, list, report.DSName);
                    //rpt.ShowDialog(this);
                }
            }
        }
        public void RefreshGridFilters()
        {
            ucIEC61850MainWindow.dataGridView1.Rows.Clear();
            ICollection<GridItem> listGridIt = new List<GridItem>();
            listGridIt = UseFCFilter(configuration.IgridItems as List<GridItem>);
            listGridIt = UseTypeFilter(listGridIt as List<GridItem>);
            listGridIt = UseReferenceFilter(listGridIt as List<GridItem>);
            listGridIt = ShowCurrentIed(listGridIt as List<GridItem>);
            listGridIt = ShowCurrentReport(listGridIt as List<GridItem>);
            RefreshGrid(listGridIt);
        }

        public void FindRCBfromItem(string reference, string fc)
        {
            string dataSet;
            string[] tempSplit;

            configuration.ILocalRcb.Clear();

            foreach (var tmp in configuration.IdataSets)
            {
                dataSet = tmp.Key;
                if (dataSet.Length > 0)
                {
                    tempSplit = tmp.Key.Split('.');
                    if (((tempSplit.Length > 2) && (reference.Contains(tempSplit[1]))) || (dataSet.StartsWith("@", StringComparison.CurrentCulture) && reference.Contains(tempSplit[1])))
                    {
                        //add current RCB
                        string[] tempSplit2;
                        tempSplit2 = currentReport.Split('.');
                        if (((tempSplit2.Length > 2) && (reference.Contains(tempSplit2[1]))) || (dataSet.StartsWith("@", StringComparison.CurrentCulture) && reference.Contains(tempSplit2[1])))
                        {
                            foreach (var v in configuration.Ireports)
                            {
                                if (!configuration.ILocalRcb.Contains(v.Value) && v.Value.ReportName == tempSplit2[0])
                                {
                                    configuration.ILocalRcb.Add(v.Value);
                                }
                            }
                        }

                        List<Fcda> list = tmp.Value;
                        if (list == null)
                        {//added new 

                            foreach (var v in configuration.Ireports)
                            {
                                if (!configuration.ILocalRcb.Contains(v.Value) && v.Value.DSName == tempSplit[0])
                                {
                                    configuration.ILocalRcb.Add(v.Value);
                                }
                            }
                        }
                        else
                        {
                            foreach (Fcda fcda in list)
                            {
                                if (reference.Contains(fcda.Address))
                                {
                                    foreach (var v in configuration.Ireports)
                                    {
                                        if (v.Value.IedName == fcda.IedName &&
                                            v.Value.DSName == fcda.DataSetName &&
                                            !configuration.ILocalRcb.Contains(v.Value) &&
                                            fcda.FC.Equals(fc))
                                        {
                                            configuration.ILocalRcb.Add(v.Value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach (var tmp in configuration.IdynamicDataSets)
            {
                dataSet = tmp.Key;
                if (dataSet.Length > 0)
                {
                    tempSplit = tmp.Key.Split('.');
                    if (((tempSplit.Length > 2) && (reference.Contains(tempSplit[1]))) || (dataSet.StartsWith("@", StringComparison.CurrentCulture) && reference.Contains(tempSplit[1])))
                    {
                        List<Fcda> list = tmp.Value;
                        if (list == null)
                        {//added new 

                            foreach (var v in configuration.Ireports)
                            {
                                if (!configuration.ILocalRcb.Contains(v.Value) && v.Value.DSName.Contains(tempSplit[0]))
                                {
                                    configuration.ILocalRcb.Add(v.Value);
                                }
                            }
                        }
                        else
                        {
                            foreach (Fcda fcda in list)
                            {
                                if (reference.Contains(fcda.Address))
                                {
                                    foreach (var v in configuration.Ireports)
                                    {
                                        if (v.Value.IedName == fcda.IedName &&
                                            v.Value.DSName == fcda.DataSetName &&
                                            !configuration.ILocalRcb.Contains(v.Value) &&
                                            fcda.FC.Equals(fc))
                                        {
                                            configuration.ILocalRcb.Add(v.Value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Up || e.KeyValue == (int)Keys.Down)
            {
                DataGridViewRow row = ucIEC61850MainWindow.dataGridView1.CurrentRow;
                if (row.Cells[configuration.IColumnHeaders["FC"]].Value.ToString() == "CO")
                {
                    string model = "";
                    foreach (var tmp in configuration.IgridItems)
                    {
                        if (tmp.ObjectReference == row.Cells[configuration.IColumnHeaders["Object Reference"]].Value.ToString())
                        {
                            model = tmp.ControlModel;
                            break;
                        }
                    }
                    ucIEC61850MainWindow.StatusLabel.Text = model;
                }
                else
                {
                    ucIEC61850MainWindow.StatusLabel.Text = "";
                }
            }
        }
        private string rnName = "";
        private bool isNodeComment = false;
        private string comment = "";
        public void parseIEC61850Node(XmlNode aicNode, bool imported)
        {
            string strRoutineName = "parseAICNode";
            try
            {
                if (aicNode == null)
                {
                    rnName = "IEC61850MainWindow";
                    return;
                }
                //First set root node name...
                rnName = aicNode.Name;

                if (aicNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = aicNode.Value;
                }

                foreach (XmlNode node in aicNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    //aiList.Add(new AI(node, masterType, masterNo, IEDNo, imported));
                }
                //refreshList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public static class ExtensionMethods
    {
        /// <summary>
        /// Method used to convert ICollection to Collection
        /// </summary>
        /// <typeparam name="T">Parameter type of input ICollection</typeparam>
        /// <param name="items">ICollection items</param>
        /// <returns>ICollection items converted to Collection</returns>
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
        /// <summary>
        /// Method used to convert Collection to ICollection
        /// </summary>
        /// <typeparam name="T">Parameter type of input Collection</typeparam>
        /// <param name="items">Collection items</param>
        /// <returns>Collection items converted to ICollection</returns>
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
