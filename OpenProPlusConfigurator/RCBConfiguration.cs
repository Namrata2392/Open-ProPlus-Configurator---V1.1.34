using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Reflection;

namespace OpenProPlusConfigurator
{
    public class RCBConfiguration
    {
        #region Declarations
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        int DopTrigger = 0;
        int trigger = 0;
        string DSAddress = "";
        string Address = "";
        string TriggerOption = "";
        string BufferTime = "";
        string IntegrityPeriod = "";
        string configRevision = "";
        //Namrata: 28/09/2017
        string DatasetAddress = "";
        string ied = "";
        protected string iName;
        protected string iID;
        protected MasterTypes mt;
        private string rnName = "";
        private int editIndex = -1;
        private bool isNodeComment = false;
        private string comment = "";
        private Mode mode = Mode.NONE;
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        List<RCB> aiList = new List<RCB>();
        ucRCBList ucai = new ucRCBList();
        private Report report;
        private Configure config;
        private string dynamicDS = "";
        private string newDataSet = "";
        private string dynamicDSdots = "";
        private string dataSet = "";
        private string dataSetName = "";
        private Boolean changed = false;
        private Boolean CheckListChanged = false;
        int introw = 0;
        int intcolumns = 0;
        DataTable DTRCBTriggeredItems = new DataTable();
        public string Text { get; private set; }
        #endregion Declarations
        public RCBConfiguration(MasterTypes mType, int mNo, int iNo)
        {
            string strRoutineName = "RCBConfiguration";
            try
            {
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                report = null;
                config = null;
                ucai.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucai.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucai.lvAIlistDoubleClick += new System.EventHandler(this.lvAIlist_DoubleClick);
                ucai.ucRCBListLoad += new System.EventHandler(this.ucRCBList_Load);
                ucai.comboBox1KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyUp);
                ucai.ChkListTriOptn.Click += new System.EventHandler(this.checkedListBox1_Click);
                addListHeaders();
                fillOptions();
                DTRCBTriggeredItems.Columns.Add("RCBAddress", typeof(string));
                DTRCBTriggeredItems.Columns.Add("TriggeredItems", typeof(string));
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string strRoutineName = "comboBox1_KeyUp";
            try
            {
                var combo = sender as ComboBox;
                if (combo == null)
                {
                    return;
                }
                if (e.KeyCode == Keys.Enter)
                {
                    string text = combo.Text.Trim();
                    combo.Text = text;
                    CkeckComboBox(combo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ucRCBList_Load(object sender, EventArgs e)
        {
            string strRoutineName = "ucRCBList_Load";
            try
            {
                FillRCBList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FillRCBList()
        {
            string strRoutineName = "FillRCBList";
            try
            {
                List<KeyValuePair<string, string>> RcbData = Utils.getKeyValueAttributes(ucai.grpAO);
                List<string> tblNameList = Utils.dsRCBData.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                string RCBTable = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                DataSet ds = Utils.DsRCB;
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        Address = dr["Address"].ToString();
                        BufferTime = dr["BufTime"].ToString();
                        configRevision = dr["ConRev"].ToString();
                        DSAddress = dr["DSAddress"].ToString();
                        IntegrityPeriod = dr["IntgPD"].ToString();
                        TriggerOption = dr["trigOptNum"].ToString();
                        if (Utils.dsRCBData.Tables.Count > 0)
                        {
                            if (RCBTable != null)
                            {
                                for (introw = 0; introw < Utils.dsRCBData.Tables[RCBTable].Rows.Count; introw++)
                                    for (intcolumns = 0; intcolumns < Utils.dsRCBData.Tables[RCBTable].Columns.Count; intcolumns++)
                                    {
                                        DatasetAddress = Utils.dsRCBData.Tables[RCBTable].Rows[introw].ItemArray[0].ToString();
                                        ied = Utils.dsRCBData.Tables[RCBTable].Rows[introw].ItemArray[1].ToString();
                                        if ((DatasetAddress == Address) && (ied == IEDNo.ToString()))
                                        {
                                            RCB NewDO = new RCB("RCB", RcbData, null, MasterTypes.IEC61850Client, masterNo, IEDNo);
                                            NewDO.Address = Address.ToString();
                                            NewDO.BufferTime = BufferTime.ToString();
                                            NewDO.ConfigRevision = configRevision.ToString();
                                            NewDO.Dataset = DSAddress.ToString();
                                            NewDO.IntegrityPeriod = IntegrityPeriod.ToString();
                                            NewDO.TriggerOptions = TriggerOption.ToString();
                                            bool boolPresent = false;
                                            for (int i = 0; i < aiList.Count; i++)
                                            {
                                                if (aiList[i].Address == NewDO.Address.ToString())
                                                {
                                                    boolPresent = true;
                                                }
                                            }
                                            if (!boolPresent)
                                            {
                                                if (NewDO.Address != "")
                                                {
                                                    aiList.Add(NewDO);
                                                    refreshList();
                                                    break;
                                                }
                                            }
                                        }
                                    }
                            }
                        }
                    }
                }      
                ucai.grpAO.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                int aa = ucai.lvRCBlist.Items.Count;
                ucai.lvRCBlist.View = View.Details;
                ucai.lvRCBlist.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void fillOptions()
        {
            string strRoutineName = "fillOptions";
            try
            {
                //ucai.lvRCBlist.Items.Clear();
                ucai.comboBox1.Items.Clear();
                if (Utils.DsRCBDataset.Tables.Count > 0)
                {
                    ucai.comboBox1.DataSource = null;
                    List<string> tblNameList = Utils.DsRCBDataset.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    string CurrentIED = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                    List<string> DistinctDataSet = Utils.DsRCBDataset.Tables[CurrentIED].Rows.OfType<DataRow>()
                    .Where(x => !string.IsNullOrEmpty(x.Field<string>("DataSetRCB"))).Select(x => x.Field<string>("DataSetRCB")).Distinct().ToList();
                    ucai.comboBox1.DataSource = DistinctDataSet;
                    ucai.comboBox1.DisplayMember = "Dataset";
                }
                //Namrata: 10/09/2017
                for (int i = 0; i < ucai.ChkListTriOptn.Items.Count - 1; i++)
                    ucai.ChkListTriOptn.SetItemChecked(i, true);
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
                ucai.lvRCBlist.Columns.Add("Address", 240, HorizontalAlignment.Left);
                ucai.lvRCBlist.Columns.Add("BufferTime", 90, HorizontalAlignment.Left);
                ucai.lvRCBlist.Columns.Add("Config Revision", 90, HorizontalAlignment.Left);
                ucai.lvRCBlist.Columns.Add("Dataset", 240, HorizontalAlignment.Left);
                ucai.lvRCBlist.Columns.Add("Integrity Period", 120, HorizontalAlignment.Left);
                ucai.lvRCBlist.Columns.Add("Trigger Options", 150, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CkeckComboBox(ComboBox combo)
        {
            string strRoutineName = "CkeckComboBox";
            try
            {
                newDataSet = "";
                string[] newDynamiDS;
                string[] dataSetArr;
                bool addedNew = false;
                if (!combo.Items.Contains(combo.Text)) //check if item is already in drop down, if not, add it to all
                {
                    if (config != null)
                    {
                        newDataSet = combo.Text;

                        if (newDataSet.Length != 0 && Regex.IsMatch(newDataSet, "^[a-zA-Z0-9_@/.]+$"))
                        {
                            if (newDataSet.StartsWith("@", StringComparison.CurrentCulture))
                            {
                                dynamicDS = report.IedName + report.LdeviceName + "/" + report.LNName + "." + combo.Text;
                                dynamicDSdots = newDataSet + "." + report.IedName + "." + report.LdeviceName + "." + report.LNName;

                                if (!config.IdynamicDataSetsInUse.ContainsKey(dynamicDS))
                                {
                                    if (!config.IdynamicDataSets.ContainsKey(dynamicDSdots))
                                    {
                                        if (!combo.Items.Contains(combo.Text))
                                        {
                                            combo.Items.Add(combo.Text);
                                            ucai.comboBox1.Text = combo.Text;
                                        }
                                        config.ItempDataSets.Add(dynamicDSdots, null);
                                        ucai.Text = combo.Text;
                                        changed = true;
                                        addedNew = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Specified dataset already exists", "Adding new dataset error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                        dynamicDS = "";
                                        dynamicDSdots = "";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Specified dataset already exists", "Adding new dataset error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    dynamicDS = "";
                                    dynamicDSdots = "";
                                }
                            }
                            else if (Regex.IsMatch(newDataSet, "^[a-zA-Z0-9_]+$"))
                            {
                                dynamicDS = report.IedName + report.LdeviceName + "/" + report.LNName + "." + combo.Text;
                                dynamicDSdots = combo.Text + "." + report.IedName + "." + report.LdeviceName + "." + report.LNName;

                                if (!config.IdynamicDataSets.ContainsKey(dynamicDSdots))
                                {
                                    if (!combo.Items.Contains(dynamicDS))
                                    {
                                        combo.Items.Add(dynamicDS);
                                    }
                                    config.ItempDataSets.Add(dynamicDSdots, null);
                                    combo.Text = dynamicDS;
                                    addedNew = true;
                                }
                                else
                                {
                                    MessageBox.Show("Specified dataset already exists", "Adding new dataset error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    dynamicDS = "";
                                    dynamicDSdots = "";
                                }
                            }
                            else if (newDataSet.Contains("/"))
                            {
                                newDynamiDS = newDataSet.Split('/', '.');
                                if (newDynamiDS.Length == 3
                                    && (newDynamiDS[0].Equals(report.IedName + report.LdeviceName)
                                    && newDynamiDS[1].Equals(report.LNName)))
                                {
                                    dynamicDSdots = newDynamiDS[2] + "." + report.IedName + "." + report.LdeviceName + "." + report.LNName;

                                    if (!config.IdynamicDataSets.ContainsKey(dynamicDSdots))
                                    {
                                        config.ItempDataSets.Add(dynamicDSdots, null);
                                        if (!combo.Items.Contains(newDataSet))
                                        {
                                            combo.Items.Add(newDataSet);
                                        }

                                        ucai.comboBox1.Text = newDataSet;
                                        changed = true;
                                        addedNew = true;
                                    }
                                }
                            }
                            if (config.Ireports.ContainsValue(report))
                            {
                                dynamicDS = combo.Text;
                            }
                            if (addedNew == true)
                            {
                                ucai.btnDone.Enabled = true;
                            }
                            else
                            {
                                ucai.comboBox1.Text = "";
                            }
                        }
                        else
                        {
                            ucai.comboBox1.Text = "";
                            changed = true;
                        }
                        ucai.comboBox1.Refresh();
                    }
                }
                else
                {
                    dataSet = combo.Text;
                    if (dataSet.StartsWith("@", StringComparison.CurrentCulture))
                    {
                        newDataSet = dataSet;
                        dynamicDS = dataSet;

                        foreach (var key in config.IdynamicDataSets.Keys)
                        {
                            if (key.Contains(dataSet + ".") && key.Contains(report.IedName))
                            {
                                dynamicDSdots = key;
                            }
                        }
                    }
                    else
                    {
                        dataSetArr = dataSet.Split('/', '.');
                        dataSetName = dataSetArr[2];
                        //dataSetDots = dataSetArr[2] + "." + report.IedName + "." + dataSetArr[0].Replace(report.IedName, "") + "." + dataSetArr[1];
                    }
                    ucai.btnDone.Enabled = true;
                    changed = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void checkedListBox1_Click(object sender, EventArgs e)
        {
            string strRoutineName = "checkedListBox1_Click";
            try
            {
                changed = true;
                CheckListChanged = true;
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
                Console.WriteLine("*** ucai btnDone_Click clicked in class!!!");
                List<KeyValuePair<string, string>> aiData = Utils.getKeyValueAttributes(ucai.grpAO);
                if (mode == Mode.ADD)
                {
                }
                else if (mode == Mode.EDIT)
                {
                    aiList[editIndex].updateAttributes(aiData);
                    string oldDataSetAddress = DSAddress;
                    CkeckComboBox(ucai.comboBox1);
                    if (CheckListChanged)
                    {
                        string strxyz = "";
                        for (int i = 0; i < ucai.ChkListTriOptn.Items.Count; i++)
                        {
                            if (ucai.ChkListTriOptn.GetItemChecked(i))
                            {
                                trigger = trigger + (1 << (i + 1));
                                strxyz += ucai.ChkListTriOptn.Items[i].ToString() + ",";
                            }
                            
                        }
                        if (!DTRCBTriggeredItems.Rows.OfType<DataRow>().Where(x => !string.IsNullOrEmpty(x.Field<string>("RCBAddress"))).Select(x => x.Field<string>("RCBAddress")).ToList().Contains(aiList[editIndex].Address))
                        {
                            DTRCBTriggeredItems.Rows.Add(aiList[editIndex].Address, strxyz);
                        }
                        else
                        {
                            DataRow Rw = DTRCBTriggeredItems.Rows.OfType<DataRow>().Where(x => x.Field<string>("RCBAddress") == aiList[editIndex].Address).Select(x => x).Single();
                            Rw["TriggeredItems"] = strxyz;
                        }
                        ucai.txtTriggerOptionBinary.Text = trigger.ToString(CultureInfo.InvariantCulture);
                        aiList[editIndex].TriggerOptions = ucai.txtTriggerOptionBinary.Text;
                    }
                    aiList[editIndex].Dataset = ucai.comboBox1.Text;
                    aiList[editIndex].BufferTime = ucai.buffTime.Text;
                    aiList[editIndex].ConfigRevision = ucai.cRev.Text;
                    aiList[editIndex].IntegrityPeriod = ucai.ipd.Text;
                    //aiList[editIndex].TriggerOptions1 = ucai.textBox1.Text;
                }
                refreshList();
                ucai.grpAO.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                //Namrata: 25/01/2018
                changed = false;
                CheckListChanged = false;
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
                RCB ai = aiList.ElementAt(editIndex);
                if (ai != null)
                {
                    ucai.comboBox1.SelectedIndex = ucai.comboBox1.FindStringExact(ai.Dataset);
                    ucai.ipd.Text = ai.IntegrityPeriod;
                    ucai.cRev.Text = ai.ConfigRevision;
                    ucai.buffTime.Text = ai.BufferTime;
                    ucai.ChkListTriOptn.Text = ai.TriggerOptions1;
                    //ucai.textBox1.Text = ai.TriggerOptions1;
                    ucai.txtTriggerOptionBinary.Text = ai.TriggerOptions;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void refreshList()
        {
            string strRoutineName = "refreshList";
            try
            {
                int cnt = 0;
                //Utils.RCB.Clear();
                ucai.lvRCBlist.Items.Clear();
                foreach (RCB ai in aiList)
                {
                    string[] row = new string[7];
                    if (ai.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = ai.Address;
                        row[1] = ai.BufferTime;
                        row[2] = ai.ConfigRevision;
                        row[3] = ai.Dataset;
                        row[4] = ai.IntegrityPeriod;
                        row[5] = ai.TriggerOptions;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucai.lvRCBlist.Items.Add(lvItem);

                    //Distinct Items From ListView
                    ListViewItem[] arr = ucai.lvRCBlist.Items.OfType<ListViewItem>().Select(x => x).Distinct().ToArray();
                    ucai.lvRCBlist.Items.Clear();
                    ucai.lvRCBlist.Items.AddRange(arr);
                    ucai.lvRCBlist.View = View.Details;
                    ucai.lblAORecords.Text = aiList.Count.ToString();
                    //ucai.lvRCBlist.View = View.Details;
                    Utils.RCB.AddRange(aiList);
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
                Console.WriteLine("*** ucai btnCancel_Click clicked in class!!!");
                ucai.grpAO.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucai.grpAO);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvAIlist_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvAIlist_DoubleClick";
            try
            {
                if (ucai.lvRCBlist.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucai.lvRCBlist.SelectedItems[0];
                Utils.UncheckOthers(ucai.lvRCBlist, lvi.Index);
                if (aiList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucai.grpAO.Visible = true;
                //Namrata:13/03/2018
                ucai.comboBox1.DataSource = null;
                if (Utils.DsRCBDataset.Tables.Count > 0)
                {
                    ucai.comboBox1.DataSource = null;
                    List<string> tblNameList = Utils.DsRCBDataset.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                    string CurrentIED = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                    List<string> DistinctDataSet = Utils.DsRCBDataset.Tables[CurrentIED].Rows.OfType<DataRow>()
                    .Where(x => !string.IsNullOrEmpty(x.Field<string>("DataSetRCB"))).Select(x => x.Field<string>("DataSetRCB")).Distinct().ToList();
                    ucai.comboBox1.DataSource = DistinctDataSet;
                    ucai.comboBox1.DisplayMember = "Dataset";
                }
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucai.grpAO, true);
                loadValues();
                List<String> CheckedItemsList = new List<string>();
                if (DTRCBTriggeredItems.Rows.Count > 0)
                {
                    DTRCBTriggeredItems.Rows.OfType<DataRow>().ToList().ForEach(Rw =>
                    {
                        if (Rw[0].ToString() == aiList[editIndex].Address)
                        {
                            CheckedItemsList = Rw[1].ToString().Split(',').ToList();
                        }
                    });
                    for (int i = 0; i < ucai.ChkListTriOptn.Items.Count; i++)
                    {
                        if (CheckedItemsList.Contains(ucai.ChkListTriOptn.Items[i].ToString()))
                        {
                            ucai.ChkListTriOptn.SetItemChecked(i, true);
                        }
                        else
                        {
                            ucai.ChkListTriOptn.SetItemChecked(i, false);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Usage example:
        public XmlNode exportXMLnode()
        {
            FillRCBList();
            //ucai.ucRCBListLoad -= new System.EventHandler(this.ucRCBList_Load); // Commented Namrata: 16/03/2018
            string strRoutineName = "exportXMLnode";
            try
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
                rootNode = xmlDoc.CreateElement(rnName);
                xmlDoc.AppendChild(rootNode);
                //var myDistinctList = Utils.RCB.GroupBy(i => i.Address).Select(g => g.First()).ToList();
                var myDistinctList = aiList.GroupBy(i => i.Address).Select(g => g.First()).ToList();
                foreach (RCB ai in myDistinctList)
                {
                    XmlNode importNode = rootNode.OwnerDocument.ImportNode(ai.exportXMLnode(), true);
                    rootNode.AppendChild(importNode);
                }
                return rootNode;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public Control getView(List<string> kpArr)
        {
            string strRoutineName = "getView";
            try
            {
                if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("RCB_"))
                {
                    return ucai;
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void parseAICNode(XmlNode aicNode, bool imported)
        {
            //ucai.ucRCBListLoad -= new System.EventHandler(this.ucRCBList_Load);
            string strRoutineName = "parseAICNode";
            try
            {
                if (aicNode == null)
                {
                    rnName = "ControlBlock";
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
                    aiList.Add(new RCB(node, masterType, masterNo, IEDNo, imported));
                }
                refreshList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


