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

namespace OpenProPlusConfigurator
{
    public class AIConfiguration
    {
        #region Declarations
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        protected string iName;
        protected string iID;
        protected MasterTypes mt;
        private string rnName = "";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private Mode mapMode = Mode.NONE;
        private int mapEditIndex = -1;
        private bool isNodeComment = false;
        private string comment = "";
        private string currentSlave = "";
        Dictionary<string, List<AIMap>> slavesAIMapList = new Dictionary<string, List<AIMap>>();
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        List<AI> aiList = new List<AI>();
        ucAIlist ucai = new ucAIlist();
        private const int COL_CMD_TYPE_WIDTH = 130;
        Configure con = new Configure();
        //Namrata: 11/09/2017
        //Fill RessponseType in All Configuration . 
        public DataGridView dataGridViewDataSet = new DataGridView();
        public DataTable dtdataset = new DataTable();
        DataRow datasetRow;
        private string Response = "";
        private string ied = "";
        //Namrata: 11/09/2017
        //Fill AI DropdownList
        public DataGridView DataGridViewFetchData = new DataGridView();
        public DataTable StoreData = new DataTable();
        DataSet ds = new DataSet();
        //DataSet DsRcb = new DataSet();
        private string aimapDatatype = "";
        private string AICDatatype = "";
        List<AIMap> slaveAIMapList;
        public string AIDescription = "";
        public DataTable DtIEC61850Index = new DataTable();
        //DataSet DsRCB = new DataSet();
        DataTable dt = new DataTable();
        private int SelectedIndex;
        #endregion Declarations
        public AIConfiguration(MasterTypes mType, int mNo, int iNo)
        {
            string strRoutineName = "AIConfiguration";
            try
            {
                //Utils.dtDataSet.Rows.Clear();
                //Utils.dtDataSet.Columns.Clear();
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                ucai.ucAIlistLoad += new System.EventHandler(this.ucAIlist_Load);
                //ucai.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucai.lvAIlistDoubleClick += new System.EventHandler(this.lvAIlist_DoubleClick);
                ucai.lvAIlistItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvAIlist_ItemCheck);
                ucai.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucai.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucai.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucai.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucai.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucai.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucai.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucai.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
                ucai.LinkDeleteConfigue.Click += new System.EventHandler(this.LinkDeleteConfigue_Click);
                ucai.cmbIEDName.SelectedIndexChanged += new System.EventHandler(this.cmbIEDName_SelectedIndexChanged);
                ucai.cmb61850ResponseType.SelectedIndexChanged += new System.EventHandler(this.cmb61850ResponseType_SelectedIndexChanged);
                ucai.cmb61850Index.SelectedIndexChanged += new System.EventHandler(this.cmb61850Index_SelectedIndexChanged);
                ucai.lvAIMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIMap_ItemSelectionChanged);
                ucai.lvAIlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIlist_ItemSelectionChanged);
                ////ucai.lvAIlistSelectedIndexChanged += new System.EventHandler(this.lvAIlist_SelectedIndexChanged); //---For Alternate Colour
                ////ucai.lvAIMapSelectedIndexChanged += new System.EventHandler(this.lvAIMap_SelectedIndexChanged); //---For Alternate Colour

                #region Enable/Disable Events
                if (mType == MasterTypes.Virtual)
                {
                    EnableEventsVirtualOnLoad();
                }
                //else
                //{
                //    ucai.lvAIlistDoubleClick += new System.EventHandler(this.lvAIlist_DoubleClick);
                //}
                if (mType == MasterTypes.IEC61850Client)//Disable add/edit/delete/dblclick n remove checkboxes...
                {
                    EnableEventsForIEC61850AI();
                }
                if (mType == MasterTypes.IEC103)
                {
                    EnabledisableEvents();
                }
                if (masterType == MasterTypes.ADR)
                {
                    EnabledisableEvents();
                }
                if (masterType == MasterTypes.IEC101)
                {
                    EnabledisableEvents();
                }
                if (masterType == MasterTypes.MODBUS)
                {
                    EnabledisableEvents();
                }
                #endregion Enable/Disable Events
                ucai.btnAIMDeleteClick += new System.EventHandler(this.btnAIMDelete_Click);
                ucai.btnAIMDoneClick += new System.EventHandler(this.btnAIMDone_Click);
                ucai.cmbAIMDataTypeSelectedIndexChanged += new System.EventHandler(this.cmbAIMDataType_SelectedIndexChanged);
                ucai.btnAIMCancelClick += new System.EventHandler(this.btnAIMCancel_Click);
                ucai.lvAIMapDoubleClick += new System.EventHandler(this.lvAIMap_DoubleClick);
                ucai.cmbDataTypeSelectedIndexChanged += new System.EventHandler(this.cmbDataType_SelectedIndexChanged);
                addListHeaders();
                fillOptions();
                ucai.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvAIlist_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lvAIlist_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucai.lvAIlist.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
        //Namrata: 03/03/2018
        private void lvAIMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color GreenColour = Color.FromArgb(0, 232, 0); //Color.FromArgb(82, 208, 23);
            if (ucai.lvAIMap.SelectedIndices.Count > 0)
            {
                SelectedIndex = Convert.ToInt32(ucai.lvAIMap.SelectedItems[0].Text);
                ucai.lvAIlist.SelectedItems.Clear();
                ucai.lvAIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucai.lvAIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucai.lvAIlist.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                ucai.lvAIMap.SelectedItems.Clear();
                ucai.lvAIMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucai.lvAIMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucai.lvAIMap.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
            }
        }
        private void lvAIlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color GreenColour = Color.FromArgb(0, 232, 0);//Color.FromArgb(82, 208, 23);
            if (ucai.lvAIlist.SelectedIndices.Count > 0)
            {
                SelectedIndex = Convert.ToInt32(ucai.lvAIlist.SelectedItems[0].Text);
                ucai.lvAIMap.SelectedItems.Clear();
                ucai.lvAIMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucai.lvAIMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucai.lvAIMap.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);

                ucai.lvAIlist.SelectedItems.Clear();
                ucai.lvAIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucai.lvAIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucai.lvAIlist.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
            }
        }
        //Namrata: 26/10/2017
        private void ucAIlist_Load(object sender, EventArgs e)
        {
            string strRoutineName = "ucAIlist_Load";
            //ucai.lvAIlist.OwnerDraw = true;
            try
            {
                foreach (var L in Utils.VirtualPLU)
                {
                    if (L.Run == "YES")
                    {
                        ucai.btnAdd.Enabled = true;
                        ucai.btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void linkLabel1_Click(object sender, EventArgs e)
        {
            string strRoutineName = "linkLabel1_Click";
            try
            {
                List<AIMap> slaveAIMapList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    Console.WriteLine("##### Slave entries does not exists");
                    MessageBox.Show("Error Deleting AI map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (ListViewItem listItem in ucai.lvAIMap.Items)
                {
                    listItem.Checked = true;
                }
                DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    foreach (ListViewItem listItem in ucai.lvAIMap.Items)
                    {
                        listItem.Checked = false;
                    }
                    return;
                }
                for (int i = ucai.lvAIMap.Items.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    slaveAIMapList.RemoveAt(i);
                    ucai.lvAIMap.Items.Clear();
                }
                Console.WriteLine("*** slaveAIMapList count: {0} lv count: {1}", slaveAIMapList.Count, ucai.lvAIMap.Items.Count);
                refreshMapList(slaveAIMapList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FetchComboboxData()
        {
            //Namrata: 13/03/2018
            ucai.cmbIEDName.DataSource = null;
            List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
            string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
            //Namrata: 26/04/2018
            if (tblName != null)
            {
                ucai.cmbIEDName.DataSource = Utils.dsIED.Tables[tblName];
                ucai.cmbIEDName.DisplayMember = "IEDName";
                //Namrata: 21/03/2018
                ucai.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[tblName];
                ucai.cmb61850ResponseType.DisplayMember = "Address";
                //Namrata: 29/03/2018
                ucai.cmb61850Index.DataSource = Utils.DsAllConfigurationData.Tables[tblName + "_On Request"];
                ucai.cmb61850Index.DisplayMember = "ObjectReferrence";
                ucai.cmb61850Index.ValueMember = "Node";
            }
            else
            {
                //MessageBox.Show("ICD File Missing !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }
        }
        public void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAdd_Click";
            try
            {
                if (aiList.Count >= getMaxAIs())
                {
                    MessageBox.Show("Maximum " + getMaxAIs() + " AI's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                if (masterType == MasterTypes.IEC61850Client)
                {
                    EnableEventsForIEC61850AI();
                    FetchComboboxData();
                }
                else if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC101)|| (masterType == MasterTypes.MODBUS))
                {
                    EnabledisableEvents();
                }
                if (masterType == MasterTypes.IEC103)
                {
                    EnabledisableEventsIEC103();
                }
                else if (masterType == MasterTypes.Virtual)
                {
                    EnableEventsVirtualOnLoad();
                }
                Utils.resetValues(ucai.grpAI);
                Utils.showNavigation(ucai.grpAI, false);
                loadDefaults();
                ucai.grpAI.Visible = true;
                ucai.cmbResponseType.Focus();
                //Namrata: 04/04/2018
                if (masterType == MasterTypes.IEC61850Client)
                {
                    //Namrata:26/04/2018
                    if (ucai.cmbIEDName.SelectedIndex != -1)
                    {
                        ucai.cmbIEDName.SelectedIndex = ucai.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                        ucai.txtFC.Text = ((DataRowView)ucai.cmb61850Index.SelectedItem).Row[2].ToString();
                        //Namrata: 10/04/2018
                        if ((ucai.cmb61850Index.Text.ToString() == "") || (ucai.cmb61850ResponseType.Text.ToString() == ""))
                        {
                            MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        ucai.grpAI.Visible = false;
                        MessageBox.Show("ICD file missing !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LinkDeleteConfigue_Click(object sender, EventArgs e)
        {
            string strRoutineName = "LinkDeleteConfigue_Click";
            try
            {
                foreach (ListViewItem listItem in ucai.lvAIlist.Items)
                {
                    listItem.Checked = true;
                }
                DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    foreach (ListViewItem listItem in ucai.lvAIlist.Items)
                    {
                        listItem.Checked = false;
                    }
                    return;
                }
                for (int i = ucai.lvAIlist.Items.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    if (Utils.IsExistAIinPLC(aiList.ElementAt(i).AINo))
                    {
                        DialogResult ask = MessageBox.Show("AI " + aiList.ElementAt(i).AINo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", "Delete AI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (ask == DialogResult.No)
                        {
                            continue;
                        }
                        Utils.DeleteAIFromPLC(aiList.ElementAt(i).AINo);
                    }
                    deleteAIFromMaps(aiList.ElementAt(i).AINo);
                    aiList.RemoveAt(i);
                    ucai.lvAIlist.Items.Clear();
                }
                Console.WriteLine("*** aiList count: {0} lv count: {1}", aiList.Count, ucai.lvAIlist.Items.Count);
                refreshList();
                refreshCurrentMapList();//Refresh map listview...
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDelete_Click";
            try
            {
                DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    return;
                }
                for (int i = ucai.lvAIlist.Items.Count - 1; i >= 0; i--)
                {
                    if (ucai.lvAIlist.Items[i].Checked)
                    {
                        if (Utils.IsExistAIinPLC(aiList.ElementAt(i).AINo))
                        {
                            DialogResult ask = MessageBox.Show("AI" + aiList.ElementAt(i).AINo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", "Delete AI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (ask == DialogResult.No)
                            {
                                continue;
                            }
                            Utils.DeleteAIFromPLC(aiList.ElementAt(i).AINo);
                        }
                        deleteAIFromMaps(aiList.ElementAt(i).AINo);
                        aiList.RemoveAt(i);
                        ucai.lvAIlist.Items[i].Remove();
                    }
                }
                refreshList();
                refreshCurrentMapList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbAIMDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "cmbAIMDataType_SelectedIndexChanged";
            try
            {
                aimapDatatype = ucai.cmbAIMDataType.Text.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "cmbAIMDataType_SelectedIndexChanged";
            try
            {
                AICDatatype = ucai.cmbDataType.Text.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private RCBConfiguration RCBNode = null;
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDone_Click";
            try
            {
                int iIncrement = 1;
                Utils.DummyslaveAIMapList1.Clear();
                List<KeyValuePair<string, string>> aiData = Utils.getKeyValueAttributes(ucai.grpAI);
                //Namrata: 27/09/2017
                //fill Address to Datatable for RCBConfiguration
                if (masterType == MasterTypes.IEC61850Client)
                {
                    Response = ucai.cmb61850ResponseType.Text;
                    ied = IEDNo.ToString(); DataColumn dcAddressColumn;
                    //Namrata: 15/03/2018
                    if (!dtdataset.Columns.Contains("Address"))
                    { dcAddressColumn = dtdataset.Columns.Add("Address", typeof(string)); }
                    if (!dtdataset.Columns.Contains("IED"))
                    { dtdataset.Columns.Add("IED", typeof(string)); }
                    datasetRow = dtdataset.NewRow();
                    datasetRow["Address"] = Response.ToString();
                    datasetRow["IED"] = IEDNo.ToString();
                    dtdataset.Rows.Add(datasetRow);
                    Utils.dtDataSet = dtdataset;
                    //Namrata: 15/03/2018
                    dataGridViewDataSet.DataSource = Utils.dtDataSet;
                    Utils.dtDataSet.TableName = Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname;
                    string Index112 = "";
                    DataRow row112;
                    if (Utils.dsRCBAI.Tables.Contains(Utils.dtDataSet.TableName))
                    {
                        Utils.dsRCBAI.Tables[Utils.dtDataSet.TableName].Clear();
                    }
                    else
                    {
                        Utils.dsRCBAI.Tables.Add(Utils.dtDataSet.TableName);
                        Utils.dsRCBAI.Tables[Utils.dtDataSet.TableName].Columns.Add("ObjectReferrence");
                        Utils.dsRCBAI.Tables[Utils.dtDataSet.TableName].Columns.Add("Node");
                    }
                    for (int i = 0; i < Utils.dtDataSet.Rows.Count; i++)
                    {
                        row112 = Utils.dsRCBAI.Tables[Utils.dtDataSet.TableName].NewRow();
                        Utils.dsRCBAI.Tables[Utils.dtDataSet.TableName].NewRow();
                        for (int j = 0; j < Utils.dtDataSet.Columns.Count; j++)
                        {
                            Index112 = Utils.dtDataSet.Rows[i][j].ToString();
                            row112[j] = Index112.ToString();
                        }
                        Utils.dsRCBAI.Tables[Utils.dtDataSet.TableName].Rows.Add(row112);
                    }
                    Utils.dsRCBData = Utils.dsRCBAI;
                    Utils.dsRCBData.Merge(Utils.dsRCBAO, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBDI, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBDO, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBEN, false, MissingSchemaAction.Add);
                }
                #region Add
                if (mode == Mode.ADD)
                {
                    //Namrata:29/6/2017
                    int intStart = Convert.ToInt32(aiData[10].Value);
                    int intRange = Convert.ToInt32(aiData[4].Value);
                    int intAIIndex = Convert.ToInt32(aiData[12].Value);
                    int intAIIndex1 = Convert.ToInt32(aiData[12].Value);
                    int AIINdex1 = 0;
                    int Index = 0;
                    int AINumber = 0, AIINdex = 0;

                    for (int i = intStart; i <= intStart + intRange - 1; i++)
                    {
                        AIDescription = "";
                        if ((i == Globals.AINo) && (i == Globals.AIIndex))
                        {
                            AINumber = Globals.AINo;
                            AIINdex = intAIIndex++;
                            if (masterType == MasterTypes.ADR)
                            {
                                AIDescription = ucai.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.IEC101)
                            {
                                AIDescription = ucai.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.IEC103)
                            {
                                AIDescription = ucai.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.MODBUS)
                            {
                                AIDescription = ucai.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.IEC61850Client)
                            {
                                AIDescription = ucai.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.Virtual)
                            {
                                AIDescription = ucai.txtDescription.Text;
                            }
                        }
                        else
                        {
                            AINumber = i;
                            AIINdex = intAIIndex++;
                            AIINdex1 = intAIIndex1++;
                            if (masterType == MasterTypes.ADR)
                            {
                                AIDescription = ucai.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.IEC101)
                            {
                                AIDescription = ucai.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.IEC103)
                            {
                                AIDescription = ucai.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.MODBUS)
                            {
                                AIDescription = ucai.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.IEC61850Client)
                            {
                                AIDescription = ucai.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.Virtual)
                            {
                                AIDescription = ucai.txtDescription.Text;
                            }
                        }
                        AI NewAI = new AI("AI", aiData, null, masterType, masterNo, IEDNo);
                        NewAI.AINo = AINumber.ToString();
                        //if (masterType == MasterTypes.MODBUS)
                        //{
                        //    if ((ucai.cmbDataType.Text == "UnsignedInt32_LSB_MSB") || (ucai.cmbDataType.Text == "SignedInt32_LSB_MSB") || (ucai.cmbDataType.Text == "UnsignedInt32_MSB_LSB") || (ucai.cmbDataType.Text == "SignedInt32_MSB_LSB") || (ucai.cmbDataType.Text == "Float_LSB_MSB") || (ucai.cmbDataType.Text == "Float_MSB_LSB") || (ucai.cmbDataType.Text == "UnsignedLong32_Bit_MSWord_LSWord") || (ucai.cmbDataType.Text == "UnsignedLong32_Bit_LSWord_MSWord") || (ucai.cmbDataType.Text == "SignedLong32_Bit_MSWord_LSWord") || (ucai.cmbDataType.Text == "SignedLong32_Bit_LSWord_MSWord") || (ucai.cmbDataType.Text == "Float_MSWord_LSWord") || (ucai.cmbDataType.Text == "Float_LSWord_MSWord"))
                        //    {
                        //        if (aiList.Count == 0)
                        //        {
                        //            Index = Convert.ToInt32(ucai.txtIndex.Text);
                        //        }
                        //        else
                        //        {
                        //            Index = intAIIndex1++;
                        //            NewAI.Index = Index.ToString();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        NewAI.Index = AIINdex.ToString();
                        //    }
                        //}
                        if (masterType == MasterTypes.MODBUS)
                        {
                            //Ajay: 11/06/2018
                            //if ((ucai.cmbDataType.Text == "UnsignedInt32_LSB_MSB") || (ucai.cmbDataType.Text == "SignedInt32_LSB_MSB") || (ucai.cmbDataType.Text == "UnsignedInt32_MSB_LSB") || (ucai.cmbDataType.Text == "SignedInt32_MSB_LSB") || (ucai.cmbDataType.Text == "Float_LSB_MSB") || (ucai.cmbDataType.Text == "Float_MSB_LSB") || (ucai.cmbDataType.Text == "UnsignedLong32_Bit_MSWord_LSWord") || (ucai.cmbDataType.Text == "UnsignedLong32_Bit_LSWord_MSWord") || (ucai.cmbDataType.Text == "SignedLong32_Bit_MSWord_LSWord") || (ucai.cmbDataType.Text == "SignedLong32_Bit_LSWord_MSWord") || (ucai.cmbDataType.Text == "Float_MSWord_LSWord") || (ucai.cmbDataType.Text == "Float_LSWord_MSWord"))
                            //{
                            //    if (aiList.Count == 0)
                            //    {
                            //        Index = Convert.ToInt32(ucai.txtIndex.Text);
                            //    }
                            //    else
                            //    {
                            //        //Namrata: 28/11/2017
                            //        intAIIndex1 = Convert.ToInt32(aiList.Select(x => x.Index).LastOrDefault());
                            //        if (intAIIndex1 > 0)
                            //        {
                            //            Index = intAIIndex1 + iIncrement; // intAIIndex1++;
                            //            NewAI.Index = (Index).ToString();
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    NewAI.Index = AIINdex.ToString();
                            //}
                            NewAI.Index = AIINdex.ToString();
                            if ((ucai.cmbDataType.Text == "UnsignedInt32_LSB_MSB") || (ucai.cmbDataType.Text == "SignedInt32_LSB_MSB") || (ucai.cmbDataType.Text == "UnsignedInt32_MSB_LSB") || (ucai.cmbDataType.Text == "SignedInt32_MSB_LSB") || (ucai.cmbDataType.Text == "Float_LSB_MSB") || (ucai.cmbDataType.Text == "Float_MSB_LSB") || (ucai.cmbDataType.Text == "UnsignedLong32_Bit_MSWord_LSWord") || (ucai.cmbDataType.Text == "UnsignedLong32_Bit_LSWord_MSWord") || (ucai.cmbDataType.Text == "SignedLong32_Bit_MSWord_LSWord") || (ucai.cmbDataType.Text == "SignedLong32_Bit_LSWord_MSWord") || (ucai.cmbDataType.Text == "Float_MSWord_LSWord") || (ucai.cmbDataType.Text == "Float_LSWord_MSWord") || (ucai.cmbDataType.Text == "UnsignedInt24_LSB_MSB") || (ucai.cmbDataType.Text == "SignedInt24_LSB_MSB") || (ucai.cmbDataType.Text == "UnsignedInt24_MSB_LSB") || (ucai.cmbDataType.Text == "SignedInt24_MSB_LSB"))
                            {
                                intAIIndex++; intAIIndex1++;
                            }
                        }
                        else
                        {
                            NewAI.Index = AIINdex.ToString();
                        }
                        NewAI.Description = AIDescription;
                        NewAI.IEDName = ucai.cmbIEDName.Text.ToString();
                        NewAI.IEC61850Index = ucai.cmb61850Index.Text.ToString();
                        NewAI.IEC61850ResponseType = ucai.cmb61850ResponseType.Text.ToString();
                        aiList.Add(NewAI);
                        Utils.DummyslaveAIMapList1.Add(NewAI);
                        if (iIncrement == 1) { iIncrement = 2; }
                    }
                }
                #endregion Add

                #region Edit
                else if (mode == Mode.EDIT)
                {
                    aiList[editIndex].updateAttributes(aiData);
                }
                #endregion Edit
                refreshList();
                //Namrata: 15/03/2018
                if (masterType == MasterTypes.IEC61850Client)
                {
                    RCBNode = new RCBConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                    RCBNode.FillRCBList();
                }
                //Namrata: 27/7/2017 
                //To Change Multiple Records at A Time .
                if (sender != null && e != null)
                {
                    ucai.grpAI.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAIMDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAIMDone_Click";
            try
            {
                int iIncrement = 1;
                List<KeyValuePair<string, string>> aimData = Utils.getKeyValueAttributes(ucai.grpAIMap);
                int intStart = Convert.ToInt32(aimData[8].Value); // AINo
                int intRange = Convert.ToInt32(aimData[2].Value); //AutoMapRange
                int intAIIndex = Convert.ToInt32(aimData[9].Value); // AIReportingIndex
                //Namrata:24/7/2017
                //For Modbus Slave
                int intAIIndexMODBUS = Convert.ToInt32(aimData[8].Value); // For MODBUSSlave AI Index Incremented by 1
                int intMODBUSAIIndex = 0;
                int AINumber = 0, AIINdex = 0;
                int ReportingIndex = 0;
                string AIDescription = "";
                //Namrata:8/7/2017
                ListViewItem item = ucai.lvAIlist.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Text == intStart.ToString()); //Find Index Of ListView
                string ind = ucai.lvAIlist.Items.IndexOf(item).ToString();
                //Namrata:31/7/2017
                ListViewItem ExistAIMap = ucai.lvAIMap.FindItemWithText(ucai.txtAIMNo.Text);   //Eliminate Duplicate Record From lvAIMAp List
                //if (!ValidateMap()) return;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    MessageBox.Show("Error adding AI map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (mapMode == Mode.ADD)
                {
                    if (aiList.Count >= 0)
                    {
                        if ((intRange + Convert.ToInt16(ind)) > ucai.lvAIlist.Items.Count)
                        {
                            MessageBox.Show("Slave Entry Does Not Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            for (int i = intStart; i <= intStart + intRange - 1; i++)
                            {
                                if (aiList.Select(x => x.AINo).ToList().Contains(i.ToString()))
                                {
                                    AINumber = i;
                                    AIINdex = intAIIndex++;
                                    intMODBUSAIIndex = intAIIndexMODBUS++;
                                    //Namrata: 16/11/2017
                                    ucai.txtMapDescription.Text = getDescription(ucai.lvAIlist, AINumber.ToString());
                                    AIDescription = ucai.txtMapDescription.Text;
                                    AIMap NewAIMap = new AIMap("AI", aimData, Utils.getSlaveTypes(currentSlave));
                                    NewAIMap.AINo = AINumber.ToString();
                                    NewAIMap.Description = AIDescription;
                                    if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server))
                                    {
                                        if ((ucai.cmbAIMDataType.SelectedItem.ToString() == "Float_MSB_LSB") || (ucai.cmbAIMDataType.Text == "Float_LSB_MSB") || (ucai.cmbAIMDataType.Text == "Float_MSWord_LSWord") || (ucai.cmbAIMDataType.Text == "Float_LSWord_MSWord") || (ucai.cmbAIMDataType.Text == "UnsignedInt32_LSB_MSB") || (ucai.cmbAIMDataType.Text == "SignedInt32_LSB_MSB") || (ucai.cmbAIMDataType.Text == "UnsignedInt32_MSB_LSB") || (ucai.cmbAIMDataType.Text == "SignedInt32_MSB_LSB") || (ucai.cmbAIMDataType.Text == "UnsignedLong32_Bit_MSWord_LSWord") || (ucai.cmbAIMDataType.Text == "UnsignedLong32_Bit_LSWord_MSWord") || (ucai.cmbAIMDataType.Text == "SignedLong32_Bit_MSWord_LSWord") || (ucai.cmbAIMDataType.Text == "SignedLong32_Bit_LSWord_MSWord"))
                                        {
                                            if ((slaveAIMapList.Count == 0) || (intRange == 1))
                                            {
                                                AIINdex = Convert.ToInt32(ucai.txtAIMReportingIndex.Text);
                                            }
                                            else
                                            {
                                                intAIIndexMODBUS = Convert.ToInt32(slaveAIMapList.Select(x => x.ReportingIndex).LastOrDefault());
                                                if (intAIIndexMODBUS > 0)
                                                {
                                                    ReportingIndex = intAIIndexMODBUS + iIncrement; // intAIIndex1++;
                                                    NewAIMap.ReportingIndex = (ReportingIndex).ToString();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            NewAIMap.ReportingIndex = (AIINdex).ToString();
                                        }
                                    }
                                    else if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE))
                                    {
                                        NewAIMap.ReportingIndex = (AIINdex).ToString();
                                    }
                                    if (masterType == MasterTypes.IEC61850Client)
                                    {
                                        //Ajay: 12/10/2018
                                        //Namrata: 10/04/2018
                                        //if ((ucai.cmb61850Index.Text.ToString() == "") || (ucai.cmb61850ResponseType.Text.ToString() == ""))
                                        if ((ucai.cmb61850Index.Text.ToString() == "") && Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                                        {
                                            MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                    slaveAIMapList.Add(NewAIMap);
                                    //Namrata: 28/11/2017
                                    if (iIncrement == 1) { iIncrement = 2; }
                                }
                            }
                        }
                    }
                }
                else if (mapMode == Mode.EDIT)
                {
                    slaveAIMapList[mapEditIndex].updateAttributes(aimData);
                    if (masterType == MasterTypes.IEC61850Client)
                    {
                        //Namrata: 10/04/2018
                        if ((ucai.cmb61850Index.Text.ToString() == "") || (ucai.cmb61850ResponseType.Text.ToString() == ""))
                        {
                            MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                refreshMapList(slaveAIMapList);
                ucai.grpAIMap.Visible = false;
                mapMode = Mode.NONE;
                mapEditIndex = -1;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 04/04/2018
        private void cmbIEDName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Namrata: 04/04/2018
            if (ucai.cmbIEDName.Focused == false)
            {

            }
            else
            {
                Utils.Iec61850IEDname = ucai.cmbIEDName.Text;
                List<DataTable> dtList = Utils.dsResponseType.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                if (dtList.Count == 0)
                {
                    ucai.cmb61850ResponseType.DataSource = null;
                    ucai.cmb61850Index.DataSource = null;
                    ucai.cmb61850ResponseType.Enabled = false;
                    ucai.cmb61850Index.Enabled = false;
                    ucai.txtFC.Text = "";
                }
                else
                {
                    ucai.cmb61850ResponseType.Enabled = true;
                    ucai.cmb61850Index.Enabled = true;
                    ucai.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//[Utils.strFrmOpenproplusTreeNode + "/" + "Undefined" + "/" + Utils.Iec61850IEDname];
                    ucai.cmb61850ResponseType.DisplayMember = "Address";
                }
            }
        }
        private void cmb61850ResponseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "cmb61850ResponseType_SelectedIndexChanged";
            try
            {
                if (ucai.cmb61850ResponseType.Items.Count > 1)
                {
                    if ((ucai.cmb61850ResponseType.SelectedIndex != -1))
                    {
                        //Namrata: 04/04/2018
                        Utils.Iec61850IEDname = ucai.cmbIEDName.Text;
                        //Utils.Iec61850IEDname = ucai.cmbIEDName.Items.OfType<DataRowView>().Select(x => x.Row[0].ToString()).FirstOrDefault().ToString();
                        List<DataTable> dtList = Utils.DsAllConfigurationData.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                        DataSet dsdummy = new DataSet();
                        dtList.ForEach(tbl => { DataTable dt = tbl.Copy(); dsdummy.Tables.Add(dt); });
                        ucai.cmb61850Index.DataSource = dsdummy.Tables[ucai.cmb61850ResponseType.SelectedIndex];
                        ucai.cmb61850Index.DisplayMember = "ObjectReferrence";
                        ucai.cmb61850Index.ValueMember = "Node";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmb61850Index_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "AI : cmb61850Index_SelectedIndexChanged";
            try
            {
                if (ucai.cmb61850Index.Items.Count > 0)
                {
                    if (ucai.cmb61850Index.SelectedIndex != -1)
                    {
                        ucai.txtFC.Text = ((DataRowView)ucai.cmb61850Index.SelectedItem).Row[2].ToString(); // ucai.cmb61850Index.Items.OfType<DataRowView>().Select(x => x.Row[2].ToString()).FirstOrDefault().ToString();
                    }
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
                ucai.grpAI.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucai.grpAI);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnFirst_Click";
            try
            {
                //Namrata:27/7/2017
                if (ucai.lvAIlist.Items.Count <= 0) return;
                if (aiList.ElementAt(0).IsNodeComment) return;
                editIndex = 0;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnPrev_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                if (editIndex - 1 < 0) return;
                if (aiList.ElementAt(editIndex - 1).IsNodeComment) return;
                editIndex--;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnNext_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                if (editIndex + 1 >= ucai.lvAIlist.Items.Count) return;
                if (aiList.ElementAt(editIndex + 1).IsNodeComment) return;
                editIndex++;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnLast_Click";
            try
            {
                if (ucai.lvAIlist.Items.Count <= 0) return;
                if (aiList.ElementAt(aiList.Count - 1).IsNodeComment) return;
                editIndex = aiList.Count - 1;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvAIlist_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvAIlist_DoubleClick";
            try
            {
                int ListIndex = ucai.lvAIlist.FocusedItem.Index;
                //Namrata: 10/09/2017
                ucai.txtAIAutoMapRange.Text = "0";
                ucai.txtAIAutoMapRange.Enabled = false;
                ucai.lblAutoMap.Enabled = false;
                if ((masterType == MasterTypes.MODBUS) || (masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC103))
                {
                    EnabledisableEventsOnDoubleClick();
                }
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    EnableEventsForIEC61850AIOnDoubleClick();
                    FetchComboboxData();
                    //Namrata: 04/04/2018
                    ucai.cmbIEDName.SelectedIndex = ucai.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                    ucai.txtFC.Text = ((DataRowView)ucai.cmb61850Index.SelectedItem).Row[2].ToString();
                    //Namrata: 10/04/2018
                    if ((ucai.cmb61850Index.Text.ToString() == "") || (ucai.cmb61850ResponseType.Text.ToString() == ""))
                    {
                        MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //if (ucai.lvAIlist.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucai.lvAIlist.Items[ListIndex];//ucai.lvAIlist.SelectedItems[0];
                Utils.UncheckOthers(ucai.lvAIlist, lvi.Index);
                if (aiList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucai.grpAI.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucai.grpAI, true);
                loadValues();
                ucai.cmbResponseType.Focus();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //string strRoutineName = "lvAIlist_DoubleClick";
            //try
            //{
            //    Console.WriteLine("*** ucai lvAIlist_DoubleClick clicked in class!!!");
            //    //Namrata: 10/09/2017
            //    ucai.txtAIAutoMapRange.Text = "0";
            //    ucai.txtAIAutoMapRange.Enabled = false;
            //    ucai.lblAutoMap.Enabled = false;
            //    if ((masterType == MasterTypes.MODBUS) || (masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC103))
            //    {
            //        EnabledisableEventsOnDoubleClick();
            //    }
            //    else if (masterType == MasterTypes.IEC61850Client)
            //    {
            //        EnableEventsForIEC61850AIOnDoubleClick();
            //    }
            //    if (ucai.lvAIlist.SelectedItems.Count <= 0) return;

            //    ListViewItem lvi = ucai.lvAIlist.SelectedItems[0];
            //    Utils.UncheckOthers(ucai.lvAIlist, lvi.Index);
            //    if (aiList.ElementAt(lvi.Index).IsNodeComment)
            //    {
            //        MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //    ucai.grpAI.Visible = true;
            //    mode = Mode.EDIT;
            //    editIndex = lvi.Index;
            //    Utils.showNavigation(ucai.grpAI, true);
            //    loadValues();
            //    ucai.cmbResponseType.Focus();
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        private void lvAIlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "lvAIlist_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    Console.WriteLine("*** selected DI: {0}", diIndex);
                    //Namrata: 27/7/2017
                    ucai.lvAIMapItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIMap_ItemSelectionChanged);
                    ucai.lvAIMap.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucai.lvAIMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucai.lvAIMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucai.lvAIMap);
                    //Namrata: 27/7/2017
                    ucai.lvAIMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucai.lvAIlist.SelectedItems.Clear();
                    ucai.lvAIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucai.lvAIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucai.lvAIlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucai.lvAIMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIMap_ItemSelectionChanged);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //string strRoutineName = "lvAIlist_ItemSelectionChanged";
            //try
            //{
            //    if (e.IsSelected)
            //    {
            //        string aiIndex = e.Item.Text;
            //        Console.WriteLine("*** selected AI: {0}", aiIndex);
            //        //Namrata:27 / 7 / 2017
            //        ucai.lvAIMapItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIMap_ItemSelectionChanged);
            //        ucai.lvAIMap.SelectedItems.Clear();
            //        Utils.highlightListviewItem(aiIndex, ucai.lvAIMap);
            //        //Namrata: 27 / 7 / 2017
            //        ucai.lvAIMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIMap_ItemSelectionChanged);
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        private void lvAIMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "lvAIMap_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    Console.WriteLine("*** selected DI: {0}", diIndex);
                    //Namrata: 27/7/2017
                    ucai.lvAIlistItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIlist_ItemSelectionChanged);
                    ucai.lvAIlist.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucai.lvAIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucai.lvAIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucai.lvAIlist);
                    //Namrata:lvAIlist 27/7/2017
                    ucai.lvAIlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucai.lvAIMap.SelectedItems.Clear();
                    ucai.lvAIMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucai.lvAIMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucai.lvAIMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucai.lvAIlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIlist_ItemSelectionChanged);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //string strRoutineName = "lvAIMap_ItemSelectionChanged";
            //try
            //{
            //    if (e.IsSelected)
            //    {
            //        string aiIndex = e.Item.Text;
            //        Console.WriteLine("*** selected AI: {0}", aiIndex);
            //        //Namrata: 27/7/2017
            //        ucai.lvAIlistItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIlist_ItemSelectionChanged);
            //        ucai.lvAIlist.SelectedItems.Clear();
            //        Utils.highlightListviewMapItem(aiIndex, ucai.lvAIlist);
            //        //Namrata: 27/7/2017
            //        ucai.lvAIlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvAIlist_ItemSelectionChanged);
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        private void loadDefaults()
        {
            string strRoutineName = "loadDefaults";
            try
            {
                ucai.txtIndex.Clear();
                ucai.txtAINo.Text = (Globals.AINo + 1).ToString();
                ucai.txtAIAutoMapRange.Text = "1";
                ucai.txtMultiplier.Text = "1";
                ucai.txtConstant.Text = "0";
                ucai.txtIndex.Text = "1";
                if (masterType == MasterTypes.ADR)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {
                        //ucai.txtAINo.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].AINo) + 1); //For Reindexing increment
                        ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                    }
                    ucai.txtSubIndex.Text = "1";
                    ucai.cmbResponseType.SelectedIndex = ucai.cmbResponseType.FindStringExact("ADR_AI");
                    ucai.txtDescription.Text = "ADR_AI_" + (Globals.AINo + 1).ToString();
                }
                else if (masterType == MasterTypes.IEC101)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {
                        //ucai.txtAINo.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].AINo) - 1); //(Globals.AINo + 1).ToString();
                        ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                    }
                    ucai.txtSubIndex.Text = "0";
                    ucai.cmbResponseType.SelectedIndex = ucai.cmbResponseType.FindStringExact("ShortFloatingPoint");
                    ucai.txtDescription.Text = "IEC101_AI";
                }
                else if (masterType == MasterTypes.IEC103)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                    }
                    ucai.txtSubIndex.Text = "0";
                    ucai.cmbResponseType.SelectedIndex = ucai.cmbResponseType.FindStringExact("Measurand_II");
                    ucai.txtDescription.Text = "IEC103_AI";// + (Globals.AINo + 1).ToString();
                }
                else if (masterType == MasterTypes.MODBUS)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {
                        //Ajay: 11/06/2018
                        //ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                        switch (aiList[aiList.Count - 1].DataType)
                        {
                            case "UnsignedInt32_LSB_MSB":
                            case "SignedInt32_LSB_MSB":
                            case "UnsignedInt32_MSB_LSB":
                            case "SignedInt32_MSB_LSB":
                            case "Float_LSB_MSB":
                            case "Float_MSB_LSB":
                            case "UnsignedLong32_Bit_MSWord_LSWord":
                            case "UnsignedLong32_Bit_LSWord_MSWord":
                            case "SignedLong32_Bit_MSWord_LSWord":
                            case "SignedLong32_Bit_LSWord_MSWord":
                            case "Float_MSWord_LSWord":
                            case "Float_LSWord_MSWord":
                            case "UnsignedInt24_LSB_MSB":
                            case "SignedInt24_LSB_MSB":
                            case "UnsignedInt24_MSB_LSB":
                            case "SignedInt24_MSB_LSB":
                                ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 2);
                                break;
                            default:
                                ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                                break;
                        }
                    }
                    ucai.txtSubIndex.Text = "0";
                    ucai.cmbResponseType.SelectedIndex = ucai.cmbResponseType.FindStringExact("ReadInputRegister");
                    ucai.txtDescription.Text = "MODBUS_AI";// + (Globals.AINo + 1).ToString();
                }
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {

                        ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                    }
                    ucai.txtSubIndex.Text = "0";
                    ucai.txtDescription.Text = "IEC61850_AI";// + (Globals.AINo + 1).ToString();
                }
                else if (masterType == MasterTypes.Virtual)
                {
                    if (ucai.lvAIlist.Items.Count - 1 >= 0)
                    {
                        ucai.txtIndex.Text = Convert.ToString(Convert.ToInt32(aiList[aiList.Count - 1].Index) + 1);
                    }
                    ucai.txtSubIndex.Text = "1";
                    ucai.cmbResponseType.SelectedIndex = ucai.cmbResponseType.FindStringExact("PLU_AI");
                    ucai.txtDescription.Text = "Virtual_AI";// + (Globals.AINo + 1).ToString();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            string strRoutineName = "loadValues";
            try
            {
                AI ai = aiList.ElementAt(editIndex);
                if (ai != null)
                {
                    ucai.txtAINo.Text = ai.AINo;
                    ucai.cmbResponseType.SelectedIndex = ucai.cmbResponseType.FindStringExact(ai.ResponseType);
                    ucai.txtIndex.Text = ai.Index;
                    ucai.txtSubIndex.Text = ai.SubIndex;
                    ucai.cmbDataType.SelectedIndex = ucai.cmbDataType.FindStringExact(ai.DataType);
                    ucai.txtMultiplier.Text = ai.Multiplier;
                    ucai.txtConstant.Text = ai.Constant;
                    ucai.txtDescription.Text = ai.Description;
                    ucai.txtFC.Text = ai.FC;
                    ucai.cmbIEDName.SelectedIndex = ucai.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                    //ucai.cmbIEDName.SelectedIndex = ucai.cmbIEDName.FindStringExact(ai.IEDName);
                    ucai.cmb61850ResponseType.SelectedIndex = ucai.cmb61850ResponseType.FindStringExact(ai.IEC61850ResponseType);
                    ucai.cmb61850Index.SelectedIndex = ucai.cmb61850Index.FindStringExact(ai.IEC61850Index);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Validate()
        {
            string strRoutineName = "Validate";
            try
            {
                bool status = true;
                //Check empty field's
                if (Utils.IsEmptyFields(ucai.grpAI))
                {
                    MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return status;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void refreshList()
        {
            string strRoutineName = "refreshList";
            try
            {
                int cnt = 0;
                Utils.AIlistforDescription.Clear();
                ucai.lvAIlist.Items.Clear();
                if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.ADR) || (masterType == MasterTypes.Virtual))
                {
                    foreach (AI ai in aiList)
                    {
                        string[] row = new string[8]; //string[] row = new string[8];
                        if (ai.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = ai.AINo;
                            row[1] = ai.ResponseType;
                            row[2] = ai.Index;
                            row[3] = ai.SubIndex;
                            row[4] = ai.DataType;
                            row[5] = ai.Multiplier;
                            row[6] = ai.Constant;
                            row[7] = ai.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucai.lvAIlist.Items.Add(lvItem);
                    }
                }
                if (masterType == MasterTypes.IEC61850Client)
                {
                    foreach (AI ai in aiList)
                    {
                        string[] row = new string[9]; //string[] row = new string[8];
                        if (ai.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = ai.AINo;
                            row[1] = ai.IEDName;
                            row[2] = ai.IEC61850ResponseType;
                            row[3] = ai.IEC61850Index;
                            row[4] = ai.FC;
                            row[5] = ai.SubIndex;
                            row[6] = ai.Multiplier;
                            row[7] = ai.Constant;
                            //Namrata: 11/9/2017
                            row[8] = ai.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucai.lvAIlist.Items.Add(lvItem);
                    }

                }
                ucai.lblAIRecords.Text = aiList.Count.ToString();
                Utils.AilistRegenerateIndex.AddRange(aiList);
                Utils.AIlistforDescription.AddRange(aiList);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int getMaxAIs()
        {
            if (masterType == MasterTypes.IEC103) return Globals.MaxIEC103AI;
            else if (masterType == MasterTypes.ADR) return Globals.MaxADRAI;
            else if (masterType == MasterTypes.IEC101) return Globals.MaxIEC101AI;
            else if (masterType == MasterTypes.MODBUS) return Globals.MaxMODBUSAI;
            else if (masterType == MasterTypes.IEC61850Client) return Globals.MaxMODBUSAI1;
            else if (masterType == MasterTypes.Virtual) return Globals.MaxPLUAI;
            else return 0;
        }
        /* ============================================= Below this, AI Map logic... ============================================= */
        private void DeleteSlave(string slaveID)
        {
            string strRoutineName = "DeleteSlave";
            try
            {
                Console.WriteLine("*** Deleting slave {0}", slaveID);
                slavesAIMapList.Remove(slaveID);
                RadioButton rb = null;
                foreach (Control ctrl in ucai.flpMap2Slave.Controls)
                {
                    if (ctrl.Tag.ToString() == slaveID)
                    {
                        rb = (RadioButton)ctrl;
                        break;
                    }
                }
                if (rb != null) ucai.flpMap2Slave.Controls.Remove(rb);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CreateNewSlave(string slaveNum, string slaveID, XmlNode aimNode)
        {
            string strRoutineName = "CreateNewSlave";
            try
            {
                List<AIMap> saimList = new List<AIMap>();
                slavesAIMapList.Add(slaveID, saimList);
                if (aimNode != null)
                {
                    foreach (XmlNode node in aimNode)
                    {
                        if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                        saimList.Add(new AIMap(node, Utils.getSlaveTypes(slaveID)));
                    }
                }
                AddMap2SlaveButton(Int32.Parse(slaveNum), slaveID);
                //Namrata: 24/02/2018
                //If Description attribute not exist in XML 
                saimList.AsEnumerable().ToList().ForEach(item =>
                {
                    string strAINo = item.AINo;
                    item.Description = Utils.AIlistforDescription.AsEnumerable().Where(x => x.AINo == strAINo).Select(x => x.Description).FirstOrDefault();
                });
                refreshMapList(saimList);
                currentSlave = slaveID;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckIEC104SlaveStatusChanges()
        {
            string strRoutineName = "CheckIEC104SlaveStatusChanges";
            try
            {
                Console.WriteLine("*** CheckIEC104SlaveStatusChanges");
                //Check for slave addition...
                foreach (IEC104Slave slv104 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC104Group().getIEC104Slaves())//Loop thru slaves...
                {
                    string slaveID = "IEC104_" + slv104.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AIMap>> sn in slavesAIMapList)
                    {
                        if (sn.Key == slaveID)
                        {
                            slaveAdded = false;
                            break;
                        }
                    }
                    if (slaveAdded)
                    {
                        CreateNewSlave(slv104.SlaveNum, slaveID, null);
                    }
                }
                //Check for slave deletion...
                List<string> delSlaves = new List<string>();
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    string slaveID = sain.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.IEC104) continue;
                    foreach (IEC104Slave slv104 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC104Group().getIEC104Slaves())
                    {
                        if (slaveID == "IEC104_" + slv104.SlaveNum)
                        {
                            slaveDeleted = false;
                            break;
                        }
                    }
                    if (slaveDeleted)
                    {
                        delSlaves.Add(slaveID);//We cannot delete from collection now as we r looping...
                    }
                }
                foreach (string delslave in delSlaves)
                {
                    DeleteSlave(delslave);
                }
                if (delSlaves.Count > 0)//Select some new slave button n refresh list...
                {
                    if (ucai.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucai.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucai.flpMap2Slave.Controls[0]).Tag.ToString();

                        //Namrata:28/04/2018
                        //if(Utils.IsAI)
                        //{
                        //   Utils.AIMapReindex.AddRange()
                        refreshCurrentMapList();
                        //}

                    }
                    else
                    {
                        ucai.lvAIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckMODBUSSlaveStatusChanges()
        {
            string strRoutineName = "CheckMODBUSSlaveStatusChanges";
            try
            {
                Console.WriteLine("*** CheckMODBUSSlaveStatusChanges");
                //Check for slave addition...
                foreach (MODBUSSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())//Loop thru slaves...
                {
                    string slaveID = "MODBUSSlave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AIMap>> sn in slavesAIMapList)
                    {
                        if (sn.Key == slaveID)
                        {
                            slaveAdded = false;
                            break;
                        }
                    }
                    if (slaveAdded)
                    {
                        CreateNewSlave(slvMB.SlaveNum, slaveID, null);
                    }
                }
                //Check for slave deletion...
                List<string> delSlaves = new List<string>();
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    string slaveID = sain.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.MODBUSSLAVE) continue;
                    foreach (MODBUSSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())
                    {
                        if (slaveID == "MODBUSSlave_" + slvMB.SlaveNum)
                        {
                            slaveDeleted = false;
                            break;
                        }
                    }
                    if (slaveDeleted)
                    {
                        delSlaves.Add(slaveID);//We cannot delete from collection now as we r looping...
                    }
                }
                foreach (string delslave in delSlaves)
                {
                    DeleteSlave(delslave);
                }
                if (delSlaves.Count > 0)//Select some new slave button n refresh list...
                {
                    if (ucai.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucai.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucai.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucai.lvAIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckIEC61850laveStatusChanges()
        {
            string strRoutineName = "CheckIEC61850laveStatusChanges";
            try
            {
                Console.WriteLine("*** CheckMODBUSSlaveStatusChanges");
                //Check for slave addition...
                foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())//Loop thru slaves...
                {
                    //string slaveID = "IEC61850ServerGroup_" + slvMB.SlaveNum;
                    string slaveID = "IEC61850Server_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AIMap>> sn in slavesAIMapList)
                    {
                        if (sn.Key == slaveID)
                        {
                            slaveAdded = false;
                            break;
                        }
                    }
                    if (slaveAdded)
                    {
                        CreateNewSlave(slvMB.SlaveNum, slaveID, null);
                    }
                }
                //Check for slave deletion...
                List<string> delSlaves = new List<string>();
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    string slaveID = sain.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.IEC61850Server) continue;
                    foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())
                    {
                        if (slaveID == "IEC61850Server_" + slvMB.SlaveNum)
                        {
                            slaveDeleted = false;
                            break;
                        }
                    }
                    if (slaveDeleted)
                    {
                        delSlaves.Add(slaveID); //We cannot delete from collection now as we r looping...
                    }
                }
                foreach (string delslave in delSlaves)
                {
                    DeleteSlave(delslave);
                }
                if (delSlaves.Count > 0) //Select some new slave button n refresh list...
                {
                    if (ucai.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucai.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucai.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucai.lvAIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckIEC101SlaveStatusChanges()
        {
            string strRoutineName = "CheckIEC101SlaveStatusChanges";
            try
            {
                Console.WriteLine("*** CheckIEC101SlaveStatusChanges");
                //Check for slave addition...
                foreach (IEC101Slave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
                {
                    string slaveID = "IEC101Slave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<AIMap>> sn in slavesAIMapList)
                    {
                        if (sn.Key == slaveID)
                        {
                            slaveAdded = false;
                            break;
                        }
                    }
                    if (slaveAdded)
                    {
                        CreateNewSlave(slvMB.SlaveNum, slaveID, null);
                    }
                }
                //Check for slave deletion...
                List<string> delSlaves = new List<string>();
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    string slaveID = sain.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.IEC101SLAVE) continue;
                    foreach (IEC101Slave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
                    {
                        if (slaveID == "IEC101Slave_" + slvMB.SlaveNum)
                        {
                            slaveDeleted = false;
                            break;
                        }
                    }
                    if (slaveDeleted)
                    {
                        delSlaves.Add(slaveID);//We cannot delete from collection now as we r looping...
                    }
                }
                foreach (string delslave in delSlaves)
                {
                    DeleteSlave(delslave);
                }
                if (delSlaves.Count > 0)//Select some new slave button n refresh list...
                {
                    if (ucai.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucai.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucai.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucai.lvAIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void deleteAIFromMaps(string aiNo)
        {
            string strRoutineName = "deleteAIFromMaps";
            try
            {
                Console.WriteLine("*** Deleting element no. {0}", aiNo);
                foreach (KeyValuePair<string, List<AIMap>> sain in slavesAIMapList)//Loop thru slaves...
                {
                    List<AIMap> delEntry = sain.Value;
                    foreach (AIMap aimn in delEntry)
                    {
                        if (aimn.AINo == aiNo)
                        {
                            delEntry.Remove(aimn);
                            break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddMap2SlaveButton(int slaveNum, string slaveID)
        {
            string strRoutineName = "AddMap2SlaveButton";
            try
            {
                RadioButton rb = new RadioButton();
                rb.Name = slaveID;
                rb.Tag = slaveID;//Ex. 'IEC104_1'/'MODBUSSlave_1'
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC104)
                    rb.Text = "IEC104 " + slaveNum;
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.MODBUSSLAVE)
                    rb.Text = "MODBUS " + slaveNum;
                //Namrata:7/7/17
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC101SLAVE)
                    rb.Text = "IEC101 " + slaveNum;
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC61850Server)
                    rb.Text = "IEC61850 " + slaveNum;
                else
                    rb.Text = "Unknown " + slaveNum;
                //rb.Padding = new Padding(0, 0, 0, 0);
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC104)
                    rb.Text = "IEC104 " + slaveNum;
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.MODBUSSLAVE)
                    rb.Text = "MODBUS " + slaveNum;
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC101SLAVE)
                    rb.Text = "IEC101 " + slaveNum;
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC61850Server)
                    rb.Text = "IEC61850 " + slaveNum;
                rb.TextAlign = ContentAlignment.TopCenter;
                rb.BackColor = ColorTranslator.FromHtml("#f2f2f2");
                rb.Appearance = Appearance.Button;
                rb.AutoSize = true;
                rb.Image = Properties.Resources.SlaveRadioButton;
                rb.Click += rbGrpMap2Slave_Click;

                ucai.flpMap2Slave.Controls.Add(rb);
                rb.Checked = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IEC61850mappingEvents()
        {
            string strRoutineName = "IEC61850mappingEvents";
            try
            {
                ucai.pbAIMHdr.Size = new Size(405, 22);
                ucai.lblANM.Location = new Point(12, 30);
                ucai.txtAIMNo.Location = new Point(118, 30);
                ucai.txtAIMNo.Size = new Size(270, 21);
                ucai.lblMapIndex.Visible = true;
                ucai.grpAIMap.Size = new Size(400, 310);
                ucai.grpAIMap.Location = new Point(405, 300);
                ucai.lblAMRI.Visible = false;
                ucai.txtAIMReportingIndex.Visible = false;
                ucai.lblMapIndex.Location = new Point(12, 56);
                ucai.CmbReportingIndex.Location = new Point(118, 56);
                ucai.CmbReportingIndex.Visible = true;
                ucai.CmbReportingIndex.Size = new Size(270, 21);
                ucai.lblAMDT.Location = new Point(12, 82);
                ucai.cmbAIMDataType.Location = new Point(118, 82);
                ucai.cmbAIMDataType.Size = new Size(270, 21);
                ucai.lblCT.Location = new Point(12, 109);
                ucai.cmbAIMCommandType.Location = new Point(118, 109);
                ucai.cmbAIMCommandType.Size = new Size(270, 21);
                ucai.lblAMDB.Location = new Point(12, 136);
                ucai.txtAIMDeadBand.Location = new Point(118, 136);
                ucai.txtAIMDeadBand.Size = new Size(270, 21);
                ucai.lblAMM.Location = new Point(12, 163);
                ucai.txtAIMMultiplier.Location = new Point(118, 163);
                ucai.txtAIMMultiplier.Size = new Size(270, 21);
                ucai.lblAMC.Location = new Point(12, 188);
                ucai.txtAIMConstant.Location = new Point(118, 188);
                ucai.txtAIMConstant.Size = new Size(270, 21);
                ucai.lblMDec.Location = new Point(12, 214);
                ucai.txtMapDescription.Location = new Point(117, 214);
                ucai.txtMapDescription.Size = new Size(270, 21);
                ucai.AutoMapRange.Visible = true;
                ucai.txtAutoMapM.Visible = true;
                ucai.AutoMapRange.Location = new Point(12, 240);
                ucai.txtAutoMapM.Location = new Point(118, 240);
                ucai.txtAutoMapM.Size = new Size(270, 21);
                ucai.btnAIMDone.Location = new Point(170, 270);
                ucai.btnAIMCancel.Location = new Point(265, 270);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MappingEvents()
        {
            string strRoutineName = "MappingEvents";
            try
            {
                ucai.AutoMapRange.Visible = true;
                ucai.txtAutoMapM.Visible = true;
                ucai.lblAMRI.Visible = true;
                ucai.txtAIMReportingIndex.Visible = true;
                ucai.txtAIMReportingIndex.Size = new Size(165, 21);
                ucai.txtAIMNo.Size = new Size(165, 21);
                ucai.grpAIMap.Size = new Size(307, 310);
                ucai.grpAIMap.Location = new Point(450, 325);
                //ucai.grpAIMap.Location = new Point(450, 310);
                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;
                ucai.lblAMDT.Location = new Point(12, 82);
                ucai.cmbAIMDataType.Location = new Point(118, 82);
                ucai.cmbAIMDataType.Visible = true;
                ucai.cmbAIMDataType.Size = new Size(165, 21);
                ucai.lblCT.Location = new Point(12, 109);
                ucai.cmbAIMCommandType.Location = new Point(118, 109);
                ucai.cmbAIMCommandType.Visible = true;
                ucai.cmbAIMCommandType.Size = new Size(165, 21);
                ucai.lblAMDB.Location = new Point(12, 136);
                ucai.txtAIMDeadBand.Location = new Point(118, 136);
                ucai.txtAIMDeadBand.Visible = true;
                ucai.txtAIMDeadBand.Size = new Size(165, 21);
                ucai.lblAMM.Location = new Point(12, 163);
                ucai.txtAIMMultiplier.Location = new Point(118, 163);
                ucai.txtAIMMultiplier.Size = new Size(165, 21);
                ucai.lblAMC.Location = new Point(12, 188);
                ucai.txtAIMConstant.Location = new Point(118, 188);
                ucai.txtAIMConstant.Visible = true;
                ucai.txtAIMConstant.Size = new Size(165, 21);
                ucai.lblMDec.Location = new Point(12, 214);
                ucai.txtMapDescription.Location = new Point(117, 214);
                ucai.txtMapDescription.Visible = true;
                ucai.txtMapDescription.Size = new Size(165, 21);
                ucai.AutoMapRange.Location = new Point(12, 240);
                ucai.txtAutoMapM.Location = new Point(118, 240);
                ucai.txtAutoMapM.Visible = true;
                ucai.txtAutoMapM.Size = new Size(165, 21);
                ucai.btnAIMDone.Location = new Point(110, 270);
                ucai.btnAIMCancel.Location = new Point(210, 270);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IEC61850mappingEventsOnDoubleClick()
        {
            string strRoutineName = "IEC61850mappingEventsOnDoubleClick";
            try
            {
                ucai.AutoMapRange.Visible = false;
                ucai.txtAutoMapM.Visible = false;
                ucai.pbAIMHdr.Size = new Size(405, 22);
                ucai.lblANM.Location = new Point(12, 30);
                ucai.txtAIMNo.Location = new Point(118, 30);
                ucai.txtAIMNo.Size = new Size(270, 21);
                ucai.lblMapIndex.Visible = true;
                ucai.grpAIMap.Size = new Size(400, 310);
                ucai.grpAIMap.Location = new Point(405, 300);
                ucai.lblAMRI.Visible = false;
                ucai.txtAIMReportingIndex.Visible = false;
                ucai.lblMapIndex.Location = new Point(12, 56);
                ucai.CmbReportingIndex.Location = new Point(118, 56);
                ucai.CmbReportingIndex.Visible = true;
                ucai.CmbReportingIndex.Size = new Size(270, 21);
                ucai.lblAMDT.Location = new Point(12, 82);
                ucai.cmbAIMDataType.Location = new Point(118, 82);
                ucai.cmbAIMDataType.Size = new Size(270, 21);
                ucai.lblCT.Location = new Point(12, 109);
                ucai.cmbAIMCommandType.Location = new Point(118, 109);
                ucai.cmbAIMCommandType.Size = new Size(270, 21);
                ucai.lblAMDB.Location = new Point(12, 136);
                ucai.txtAIMDeadBand.Location = new Point(118, 136);
                ucai.txtAIMDeadBand.Size = new Size(270, 21);
                ucai.lblAMM.Location = new Point(12, 163);
                ucai.txtAIMMultiplier.Location = new Point(118, 163);
                ucai.txtAIMMultiplier.Size = new Size(270, 21);
                ucai.lblAMC.Location = new Point(12, 188);
                ucai.txtAIMConstant.Location = new Point(118, 188);
                ucai.txtAIMConstant.Size = new Size(270, 21);
                ucai.lblMDec.Location = new Point(12, 214);
                ucai.txtMapDescription.Location = new Point(117, 214);
                ucai.txtMapDescription.Size = new Size(270, 21);
                ucai.btnAIMDone.Location = new Point(170, 245);
                ucai.btnAIMCancel.Location = new Point(265, 245);
                ucai.grpAIMap.Size = new Size(400, 285);
                ucai.grpAIMap.Location = new Point(450, 330);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MappingEventsOnDoubleClick()
        {
            string strRoutineName = "txtTcpPortKeyPress";
            try
            {
                ucai.lblAMRI.Visible = true;
                ucai.txtAIMReportingIndex.Visible = true;
                ucai.txtAIMReportingIndex.Size = new Size(165, 21);
                ucai.txtAIMNo.Size = new Size(165, 21);
                ucai.grpAIMap.Size = new Size(307, 310);
                ucai.grpAIMap.Location = new Point(450, 310);
                ucai.lblMapIndex.Visible = false;
                ucai.CmbReportingIndex.Visible = false;
                ucai.lblAMDT.Location = new Point(12, 82);
                ucai.cmbAIMDataType.Location = new Point(118, 82);
                ucai.cmbAIMDataType.Visible = true;
                ucai.cmbAIMDataType.Size = new Size(165, 21);
                ucai.lblCT.Location = new Point(12, 109);
                ucai.cmbAIMCommandType.Location = new Point(118, 109);
                ucai.cmbAIMCommandType.Visible = true;
                ucai.cmbAIMCommandType.Size = new Size(165, 21);
                ucai.lblAMDB.Location = new Point(12, 136);
                ucai.txtAIMDeadBand.Location = new Point(118, 136);
                ucai.txtAIMDeadBand.Visible = true;
                ucai.txtAIMDeadBand.Size = new Size(165, 21);
                ucai.lblAMM.Location = new Point(12, 163);
                ucai.txtAIMMultiplier.Location = new Point(118, 163);
                ucai.txtAIMMultiplier.Size = new Size(165, 21);
                ucai.lblAMC.Location = new Point(12, 188);
                ucai.txtAIMConstant.Location = new Point(118, 188);
                ucai.txtAIMConstant.Visible = true;
                ucai.txtAIMConstant.Size = new Size(165, 21);
                ucai.lblMDec.Location = new Point(12, 214);
                ucai.txtMapDescription.Location = new Point(117, 214);
                ucai.txtMapDescription.Visible = true;
                ucai.txtMapDescription.Size = new Size(165, 21);
                ucai.AutoMapRange.Visible = false;
                ucai.txtAutoMapM.Visible = false;
                ucai.btnAIMDone.Location = new Point(110, 245);
                ucai.btnAIMCancel.Location = new Point(210, 245);
                ucai.grpAIMap.Size = new Size(300, 285);
                ucai.grpAIMap.Location = new Point(450, 330);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void rbGrpMap2Slave_Click(object sender, EventArgs e)
        {
            string strRoutineName = "rbGrpMap2Slave_Click";
            try
            {
                bool rbChanged = false;
                RadioButton currRB = ((RadioButton)sender);
                Console.WriteLine("*** Goood, radiobtn grp clicked...");
                Console.WriteLine("*** current radbtn name: {0}", currRB.Name);
                if (currentSlave != currRB.Tag.ToString())
                {
                    currentSlave = currRB.Tag.ToString();
                    rbChanged = true;
                    refreshCurrentMapList();
                    if (ucai.lvAIlist.SelectedItems.Count > 0)
                    {
                        //If possible highlight the map for new slave selected...
                        //Remove selection from AIMap...
                        ucai.lvAIMap.SelectedItems.Clear();
                        Utils.highlightListviewItem(ucai.lvAIlist.SelectedItems[0].Text, ucai.lvAIMap);
                    }
                }
                ShowHideSlaveColumns();
                if (rbChanged && ucai.lvAIlist.CheckedItems.Count <= 0) return;//We supported map listing only
                AI mapAI = null;
                if (ucai.lvAIlist.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Select Single AI Element To Map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                for (int i = 0; i < ucai.lvAIlist.Items.Count; i++)
                {
                    if (ucai.lvAIlist.Items[i].Checked)
                    {
                        Console.WriteLine("*** Mapping index: {0}", i);
                        mapAI = aiList.ElementAt(i);
                        ucai.lvAIlist.Items[i].Checked = false;//Now we can uncheck in listview...
                        break;
                    }
                }
                if (mapAI == null) return;
                //Search if already mapped for current slave...
                bool alreadyMapped = false;
                List<AIMap> slaveAIMapList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    Console.WriteLine("##### Slave entries does not exists");
                    MessageBox.Show("Slave Entry Doesnot Exist!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    Console.WriteLine("##### Slave entries exists");
                }
                //foreach (AIMap saim in slaveAIMapList)
                //{
                //    if (slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                //    {
                //        if (saim.AINo == mapAI.AINo)
                //        {
                //            Console.WriteLine("##### Hoorray, already mapped...");
                //            MessageBox.Show("AI already mapped!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            alreadyMapped = true;
                //            break;
                //        }
                //    }
                //}
                if (!alreadyMapped)
                {
                    mapMode = Mode.ADD;
                    mapEditIndex = -1;
                    Utils.resetValues(ucai.grpAIMap);
                    ucai.txtAIMNo.Text = mapAI.AINo;
                    //Namrata: 16/11/2017
                    string str = getDescription(ucai.lvAIlist, mapAI.AINo);
                    ucai.txtMapDescription.Text = str;
                    ucai.txtAutoMapM.Text = "1";
                    loadMapDefaults();
                    //Namrata: 4/11/2017
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                    {
                        //Namrata: 4/11/2017
                        Utils.CurrentSlaveFinal = currentSlave;
                        ucai.CmbReportingIndex.DataSource = null;
                        ucai.CmbReportingIndex.Items.Clear();
                        ucai.CmbReportingIndex.DataSource = Utils.dsAISlave.Tables[Utils.CurrentSlaveFinal];
                        ucai.CmbReportingIndex.DisplayMember = "ObjectReferrence";
                        IEC61850mappingEvents();
                    }
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE || Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE || Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104)
                    {
                        MappingEvents();
                    }
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) ucai.cmbAIMCommandType.Enabled = true;
                    else ucai.cmbAIMCommandType.Enabled = false;
                    //if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                    //{
                    //    ucai.txtAIMReportingIndex.Text = (Convert.ToInt32(ucai.txtAIMReportingIndex.Text) - 1).ToString();
                    //}
                    //if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server) ucai.cmbAIMCommandType.Enabled = true;
                    //else ucai.cmbAIMCommandType.Enabled = false;
                    ucai.grpAIMap.Visible = true;
                    ucai.txtAIMReportingIndex.Focus();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAIMDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAIMDelete_Click";
            try
            {
                Console.WriteLine("*** ucai btnAIMDelete_Click clicked in class!!!");
                DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.No)
                {
                    return;
                }
                List<AIMap> slaveAIMapList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    Console.WriteLine("##### Slave entries does not exists");
                    MessageBox.Show("Error deleting AI map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int i = ucai.lvAIMap.Items.Count;
                for (i = ucai.lvAIMap.Items.Count - 1; i >= 0; i--)
                {
                    if (ucai.lvAIMap.Items[i].Checked)
                    {
                        Console.WriteLine("*** removing indices: {0}", i);
                        slaveAIMapList.RemoveAt(i);
                        ucai.lvAIMap.Items[i].Remove();
                        //refreshMapList1(slaveAIMapList);
                    }
                }
                Console.WriteLine("*** slaveAIMapList count: {0} lv count: {1}", slaveAIMapList.Count, ucai.lvAIMap.Items.Count);
                refreshMapList(slaveAIMapList);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAIMCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAIMCancel_Click";
            try
            {
                Console.WriteLine("*** ucai btnAIMCancel_Click clicked in class!!!");
                ucai.grpAIMap.Visible = false;
                mapMode = Mode.NONE;
                mapEditIndex = -1;
                Utils.resetValues(ucai.grpAIMap);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvAIMap_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvAIMap_DoubleClick";
            try
            {
                Console.WriteLine("*** ucai lvAIMap_DoubleClick clicked in class!!!");
                List<AIMap> slaveAIMapList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    Console.WriteLine("##### Slave entries does not exists");
                    MessageBox.Show("Error editing AI map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int ListIndex = ucai.lvAIMap.FocusedItem.Index;
                ListViewItem lvi = ucai.lvAIMap.Items[ListIndex];//ucai.lvAIlist.SelectedItems[0];
                //if (ucai.lvAIMap.SelectedItems.Count <= 0) return;
                //ListViewItem lvi = ucai.lvAIMap.SelectedItems[0];
                Utils.UncheckOthers(ucai.lvAIMap, lvi.Index);
                if (slaveAIMapList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE || Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE || Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104)
                {
                    MappingEventsOnDoubleClick();
                }
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                {
                    IEC61850mappingEventsOnDoubleClick();
                }
                ucai.txtAutoMapM.Text = "0";
                ucai.grpAIMap.Visible = true;
                mapMode = Mode.EDIT;
                mapEditIndex = lvi.Index;
                loadMapValues();
                ucai.txtAIMReportingIndex.Focus();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string getDescription(ListView lstview, string ainno)
        {
            try
            {
                int iColIndex = ucai.lvAIlist.Columns["Description"].Index;
                var query = lstview.Items.Cast<ListViewItem>().Where(item => item.SubItems[0].Text == ainno).Select(s => s.SubItems[iColIndex].Text).Single();
                return query.ToString();
            }
            catch
            {
                return null;
            }
        }
        private void loadMapDefaults()
        {
            string strRoutineName = "loadMapDefaults";
            try
            {
                ucai.txtAIMReportingIndex.Text = (Globals.AIReportingIndex + 1).ToString();
                ucai.txtAIMDeadBand.Text = "1";
                ucai.txtAIMMultiplier.Text = "1";
                ucai.txtAIMConstant.Text = "0";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadMapValues()
        {
            string strRoutineName = "loadMapValues";
            try
            {
                List<AIMap> slaveAIMapList;
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))
                {
                    Console.WriteLine("##### Slave entries does not exists");
                    MessageBox.Show("Error loading AI map data for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                AIMap aimn = slaveAIMapList.ElementAt(mapEditIndex);
                if (aimn != null)
                {
                    ucai.txtAIMNo.Text = aimn.AINo;
                    ucai.txtAIMReportingIndex.Text = aimn.ReportingIndex;
                    ucai.cmbAIMDataType.SelectedIndex = ucai.cmbAIMDataType.FindStringExact(aimn.DataType);
                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                    {
                        ucai.cmbAIMCommandType.SelectedIndex = ucai.cmbAIMCommandType.FindStringExact(aimn.CommandType);
                        ucai.cmbAIMCommandType.Enabled = true;
                    }
                    else
                    {
                        ucai.cmbAIMCommandType.Enabled = false;
                    }
                    ucai.txtAIMDeadBand.Text = aimn.Deadband;
                    ucai.txtAIMMultiplier.Text = aimn.Multiplier;
                    ucai.txtAIMConstant.Text = aimn.Constant;
                    //Namrata: 18/11/2017
                    ucai.txtMapDescription.Text = aimn.Description;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateMap()
        {
            string strRoutineName = "ValidateMap";
            try
            {
                bool status = true;
                //Check empty field's
                if (Utils.IsEmptyFields(ucai.grpAIMap))
                {
                    MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return status;
            }
            catch (Exception Ex)
            {
                throw Ex;
                //MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshMapList(List<AIMap> tmpList)
        {
            string strRoutineName = "refreshMapList";
            try
            {
                int cnt = 0;
                ucai.lvAIMap.Items.Clear();
                //ucai.lblAIMRecords.Text = "0";
                if (tmpList == null) return;
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE))
                {
                    foreach (AIMap aimp in tmpList)
                    {
                        string[] row = new string[8];
                        if (aimp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = aimp.AINo;
                            row[1] = aimp.ReportingIndex;
                            row[2] = aimp.DataType;
                            row[3] = aimp.CommandType;
                            row[4] = aimp.Deadband;
                            row[5] = aimp.Multiplier;
                            row[6] = aimp.Constant;
                            row[7] = aimp.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucai.lvAIMap.Items.Add(lvItem);
                    }
                }
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                {
                    foreach (AIMap aimp in tmpList)
                    {
                        string[] row = new string[8];
                        if (aimp.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = aimp.AINo;
                            row[1] = aimp.IEC61850ReportingIndex;
                            row[2] = aimp.DataType;
                            row[3] = aimp.CommandType;
                            row[4] = aimp.Deadband;
                            row[5] = aimp.Multiplier;
                            row[6] = aimp.Constant;
                            row[7] = aimp.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucai.lvAIMap.Items.Add(lvItem);
                    }
                }

                ucai.lblAIMRecords.Text = tmpList.Count.ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshCurrentMapList()
        {
            string strRoutineName = "refreshCurrentMapList";
            try
            {
                //fillMapOptions(Utils.getSlaveTypes(currentSlave));
                //List<AIMap> saimList;
                ////Namrata:02/05/2018
                //if (slaveAIMapList != null)
                //{
                //    slaveAIMapList = slaveAIMapList.OrderBy(x => x.AINo).ToList();
                //}

                //if (!slavesAIMapList.TryGetValue(currentSlave, out saimList))
                //{
                //    Console.WriteLine("##### Slave entries does not exists");
                //    refreshMapList(null);

                //}
                //else
                //{
                //    saimList = slaveAIMapList.OrderBy(x => x.AINo).ToList();
                //    refreshMapList(saimList);
                //    //refreshMapList(slaveAIMapList);
                //}

                fillMapOptions(Utils.getSlaveTypes(currentSlave));
                //List<AIMap> saimList;  //Ajay: 08/08/2018 commented
                //Ajay: 08/08/2018 if condition commented
                //Namrata:02/05/2018
                //if (slaveAIMapList != null)
                //{
                //    slaveAIMapList = slaveAIMapList.OrderBy(x => Convert.ToInt32(x.AINo)).ToList();
                //}
                //if (!slavesAIMapList.TryGetValue(currentSlave, out saimList)) //Ajay: 08/08/2018 commented
                if (!slavesAIMapList.TryGetValue(currentSlave, out slaveAIMapList))  //Ajay: 08/08/2018
                {
                    refreshMapList(null);
                }
                else
                {
                    //Namrata:02/05/2018
                    //if (saimList.Count > 0 && slaveAIMapList != null) //Ajay: 08/08/2018 if condition commented
                    if (slaveAIMapList != null && slaveAIMapList.Count > 0) //Ajay: 08/08/2018 
                    {
                        //Ajay: 08/08/2018 commented
                        //saimList = slaveAIMapList.OrderBy(x => Convert.ToInt32(x.AINo)).ToList();
                        //slaveAIMapList = saimList; //Namarta:15/05/2018
                        slaveAIMapList = slaveAIMapList.OrderBy(x => Convert.ToInt32(x.AINo)).ToList();
                    }
                    //refreshMapList(saimList); //Ajay: 08/08/2018 commented
                    refreshMapList(slaveAIMapList); //Ajay: 08/08/2018
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private void refreshCurrentMapList()
        //{
        //    string strRoutineName = "refreshCurrentMapList";
        //    try
        //    {
        //        fillMapOptions(Utils.getSlaveTypes(currentSlave));
        //        //List<AIMap> saimList = new List<AIMap>();
        //        List<AIMap> saimList;
        //        if (Utils.IsAI)
        //        {
        //            saimList = Utils.AIMapReindex;
        //            refreshMapList(saimList);
        //        }
        //        else
        //        {
        //            if (!slavesAIMapList.TryGetValue(currentSlave, out saimList))
        //            {
        //                Console.WriteLine("##### Slave entries does not exists");
        //                refreshMapList(null);

        //            }
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        /* ============================================= Above this, AI Map logic... ============================================= */
        string ai = "";
        private void fillOptions()
        {
            string strRoutineName = "AI : fillOptions";
            try
            {
                DataColumn dcAddressColumn = dtdataset.Columns.Add("Address", typeof(string));
                dtdataset.Columns.Add("IED", typeof(string));

                //Fill IED Name
                ucai.cmbIEDName.Items.Clear();
                List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
                string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
                ucai.cmbIEDName.DataSource = Utils.dsIED.Tables[tblName];
                ucai.cmbIEDName.DisplayMember = "IEDName";
                //Namrata: 04/04/2018
                if (Utils.Iec61850IEDname != "")
                {
                    ucai.cmbIEDName.Text = Utils.Iec61850IEDname;
                }


                //Namrata: 15/9/2017
                //Fill ResponseType For IEC61850Client
                ucai.cmb61850ResponseType.Items.Clear();
                //Namrata: 31/10/2017
                DataSet ds11 = Utils.dsResponseType;
                ucai.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//[Utils.strFrmOpenproplusTreeNode + "/" + "Undefined" + "/" + Utils.Iec61850IEDname];
                ucai.cmb61850ResponseType.DisplayMember = "Address";

                //Fill Response Type...
                if (masterType == MasterTypes.IEC61850Client)
                {
                    ucai.cmbResponseType.Items.Clear();
                }
                else
                {
                    ucai.cmbResponseType.Items.Clear();
                    foreach (String rt in AI.getResponseTypes(masterType))
                    {
                        ucai.cmbResponseType.Items.Add(rt.ToString());
                    }
                    ucai.cmbResponseType.SelectedIndex = 0;
                }

                //Fill Data Type...
                if (masterType == MasterTypes.IEC61850Client)
                {
                    ucai.cmbDataType.Items.Clear();
                }
                else
                {
                    ucai.cmbDataType.Items.Clear();
                    foreach (String dt in AI.getDataTypes(masterType))
                    {
                        ucai.cmbDataType.Items.Add(dt.ToString());
                    }
                    ucai.cmbDataType.SelectedIndex = 0;
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fillMapOptions(SlaveTypes sType)
        {
            string strRoutineName = "fillMapOptions";
            try
            {
                /***** Fill Map details related combobox ******/
                try
                {
                    //Fill Data Type...
                    ucai.cmbAIMDataType.Items.Clear();
                    foreach (String dt in AIMap.getDataTypes(sType))
                    {
                        ucai.cmbAIMDataType.Items.Add(dt.ToString());
                    }
                    if (ucai.cmbAIMDataType.Items.Count > 0) ucai.cmbAIMDataType.SelectedIndex = 0;
                }
                catch (System.NullReferenceException)
                {
                    Utils.WriteLine(VerboseLevel.ERROR, "AI Map DataType does not exist for Slave Type: {0}", sType.ToString());
                }
                try
                {
                    //Fill Command Type...
                    ucai.cmbAIMCommandType.Items.Clear();
                    foreach (String ct in AIMap.getCommandTypes(sType))
                    {
                        ucai.cmbAIMCommandType.Items.Add(ct.ToString());
                    }
                    if (ucai.cmbAIMCommandType.Items.Count > 0) ucai.cmbAIMCommandType.SelectedIndex = 0;
                }
                catch (System.NullReferenceException)
                {
                    Utils.WriteLine(VerboseLevel.ERROR, "AI Map CommandType does not exist for Slave Type: {0}", sType.ToString());
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addListHeaders()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                if (masterType == MasterTypes.IEC103)
                {
                    ucai.lvAIlist.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                    //ucai.lvAIlist.Columns.Add("Description", -2, HorizontalAlignment.Left);
                }
                else if (masterType == MasterTypes.IEC101)
                {
                    ucai.lvAIlist.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                    //ucai.lvAIlist.Columns.Add("Description", -2, HorizontalAlignment.Left);
                }
                else if (masterType == MasterTypes.ADR)
                {
                    ucai.lvAIlist.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                    //ucai.lvAIlist.Columns.Add("Description", -2, HorizontalAlignment.Left);
                }
                else if (masterType == MasterTypes.MODBUS)
                {
                    ucai.lvAIlist.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                    //ucai.lvAIlist.Columns.Add("Description", -2, HorizontalAlignment.Left);
                }
                else if (masterType == MasterTypes.Virtual)
                {
                    ucai.lvAIlist.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Data Type", 200, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                    //ucai.lvAIlist.Columns.Add("Description", -2, HorizontalAlignment.Left);
                }
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    ucai.lvAIlist.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("IEDName", 50, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Response Type", 270, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Index", 350, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("FC", 100, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Sub Index", 65, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Constant", 70, HorizontalAlignment.Left);
                    ucai.lvAIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                    //Namrata: 15/9/2017
                    //Hide IED Name
                    ucai.lvAIlist.Columns[1].Width = 0;
                }
                //Add AI map headers..
                ucai.lvAIMap.Columns.Add("AI No.", 70, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Reporting Index", 200, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Data Type", 205, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Command Type", 0, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Deadband", 70, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Multiplier", 70, HorizontalAlignment.Left);
                ucai.lvAIMap.Columns.Add("Constant", -2, HorizontalAlignment.Left);
                //Namrata:16/11/2017
                ucai.lvAIMap.Columns.Add("Description", 100, HorizontalAlignment.Left);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowHideSlaveColumns()
        {
            string strRoutineName = "ShowHideSlaveColumns";
            try
            {
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) Utils.getColumnHeader(ucai.lvAIMap, "Command Type").Width = COL_CMD_TYPE_WIDTH;
                else Utils.getColumnHeader(ucai.lvAIMap, "Command Type").Width = 0;//Hide...
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            rootNode = xmlDoc.CreateElement("AIConfiguration");//rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            foreach (AI ai in aiList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(ai.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public XmlNode exportMapXMLnode(String slaveID)
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
            rootNode = xmlDoc.CreateElement("AIMap");
            xmlDoc.AppendChild(rootNode);
            List<AIMap> slaveAIMapList;
           
            if (!slavesAIMapList.TryGetValue(slaveID, out slaveAIMapList))
            {
                Console.WriteLine("##### Slave entries for {0} does not exists", slaveID);
                return rootNode;
            }
            //Namrata:15/05/2018
            List<AIMap> sdimList = slaveAIMapList.OrderBy(x => Convert.ToInt32(x.AINo)).ToList();
            slaveAIMapList = sdimList;
            foreach (AIMap aimn in slaveAIMapList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(aimn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            List<AIMap> slaveAIMapList;
            if (!slavesAIMapList.TryGetValue(slaveID, out slaveAIMapList))
            {
                Console.WriteLine("AI INI: ##### Slave entries for {0} does not exists", slaveID);
                return iniData;
            }
            //Ajay: 26/12/2018
            //if(slaveAIMapList != null && slaveAIMapList.Count > 0)
            //{
            //    try
            //    {
            //        var hash = new HashSet<int>();
            //        List<string> duplicateRIList = slaveAIMapList.Select(x => x.ReportingIndex).ToList().Where(y => !hash.Add(Convert.ToInt32(y))).ToList();
            //        if (duplicateRIList != null && duplicateRIList.Count > 0)
            //        {
            //            MessageBox.Show("AI: Duplicate Reporting Index (" + string.Join(",", duplicateRIList) + ") found!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //    }
            //    catch { }
            //}

            if (element == "DeadBandAI")
            {
                foreach (AIMap aimn in slaveAIMapList)
                {
                    iniData += "DeadBand_" + ctr++ + "=" + Utils.GetDataTypeShortNotation(aimn.DataType) + "," + aimn.ReportingIndex + "," + aimn.Deadband + Environment.NewLine;
                }
            }
            else if (element == "AI")
            {
                foreach (AIMap aimn in slaveAIMapList)
                {
                    int ri;
                    try
                    {
                        ri = Int32.Parse(aimn.ReportingIndex);
                    }
                    catch (System.FormatException)
                    {
                        ri = 0;
                    }
                    if (slaveAIMapList.Where(x => x.ReportingIndex == ri.ToString()).Select(x => x).Count() > 1) //Ajay: 28/12/2018
                    {
                        MessageBox.Show("Duplicate Reporting Index (" + aimn.ReportingIndex + ") found of AI No " + aimn.AINo, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        iniData += "AI_" + ctr++ + "=" + Utils.GenerateIndex("AI", Utils.GetDataTypeIndex(aimn.DataType), ri).ToString() + Environment.NewLine;
                    }
                }
            }
            return iniData;
        }
        //Ajay: 28/12/2018
        public string exportINI_DeadBandAI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            List<AIMap> slaveAIMapList;
            if (!slavesAIMapList.TryGetValue(slaveID, out slaveAIMapList))
            {
                Console.WriteLine("AI INI: ##### Slave entries for {0} does not exists", slaveID);
                return iniData;
            }
            //Ajay: 26/12/2018
            //Ajay: 28/12/2018 Commented
            //if (slaveAIMapList != null && slaveAIMapList.Count > 0)
            //{
            //    try
            //    {
            //        var hash = new HashSet<int>();
            //        List<string> duplicateRIList = slaveAIMapList.Select(x => x.ReportingIndex).ToList().Where(y => !hash.Add(Convert.ToInt32(y))).ToList();
            //        if (duplicateRIList != null && duplicateRIList.Count > 0)
            //        {
            //            MessageBox.Show("AI: Duplicate Reporting Index (" + string.Join(",", duplicateRIList) + ") found!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //    }
            //    catch { }
            //}

            if (element == "DeadBandAI")
            {
                foreach (AIMap aimn in slaveAIMapList)
                {
                    iniData += "DeadBand_" + ctr++ + "=" + Utils.GetDataTypeShortNotation(aimn.DataType) + "," + aimn.ReportingIndex + "," + aimn.Deadband + Environment.NewLine;
                }
            }
            else if (element == "AI")
            {
                foreach (AIMap aimn in slaveAIMapList)
                {
                    int ri;
                    try
                    {
                        ri = Int32.Parse(aimn.ReportingIndex);
                    }
                    catch (System.FormatException)
                    {
                        ri = 0;
                    }
                    iniData += "AI_" + ctr++ + "=" + Utils.GenerateIndex("AI", Utils.GetDataTypeIndex(aimn.DataType), ri).ToString() + Environment.NewLine;
                }
            }
            return iniData;
        }
        public void changeIEC104Sequence(int oSlaveNo, int nSlaveNo)
        {
            string strRoutineName = "changeIEC104Sequence";
            try
            {
                if (oSlaveNo == nSlaveNo) return;
                Utils.ChangeKey(slavesAIMapList, "IEC104_" + oSlaveNo, "IEC104_" + nSlaveNo);
                //Change radio button Tag n Text...
                foreach (Control ctrl in ucai.flpMap2Slave.Controls)
                {
                    RadioButton rb = (RadioButton)ctrl;
                    if (rb.Tag.ToString() == "IEC104_" + oSlaveNo)
                    {
                        rb.Tag = "IEC104_" + nSlaveNo;//Ex. 'IEC104_1'
                        rb.Text = "IEC104 " + nSlaveNo;
                        break;
                    }
                }
                //Check currentSlave var...
                if (currentSlave == "IEC104_" + oSlaveNo) currentSlave = "IEC104_" + nSlaveNo;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void changeMODBUSSlaveSequence(int oSlaveNo, int nSlaveNo)
        {
            string strRoutineName = "changeMODBUSSlaveSequence";
            try
            {
                if (oSlaveNo == nSlaveNo) return;
                Utils.ChangeKey(slavesAIMapList, "MODBUSSlave_" + oSlaveNo, "MODBUSSlave_" + nSlaveNo);
                //Change radio button Tag n Text...
                foreach (Control ctrl in ucai.flpMap2Slave.Controls)
                {
                    RadioButton rb = (RadioButton)ctrl;
                    if (rb.Tag.ToString() == "MODBUSSlave_" + oSlaveNo)
                    {
                        rb.Tag = "MODBUSSlave_" + nSlaveNo;//Ex. 'MODBUSSlave_1'
                        rb.Text = "MODBUS " + nSlaveNo;
                        break;
                    }
                }
                //Check currentSlave var...
                if (currentSlave == "MODBUSSlave_" + oSlaveNo) currentSlave = "MODBUSSlave_" + nSlaveNo;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void changeIEC101SlaveSequence(int oSlaveNo, int nSlaveNo)
        {
            string strRoutineName = "changeIEC101SlaveSequence";
            try
            {
                if (oSlaveNo == nSlaveNo) return;
                Utils.ChangeKey(slavesAIMapList, "IEC101Slave_" + oSlaveNo, "IEC101Slave_" + nSlaveNo);
                //Change radio button Tag n Text...
                foreach (Control ctrl in ucai.flpMap2Slave.Controls)
                {
                    RadioButton rb = (RadioButton)ctrl;
                    if (rb.Tag.ToString() == "IEC101Slave_" + oSlaveNo)
                    {
                        rb.Tag = "IEC101Slave_" + nSlaveNo;//Ex. 'MODBUSSlave_1'
                        rb.Text = "IEC101" + nSlaveNo;
                        break;
                    }
                }
                //Check currentSlave var...
                if (currentSlave == "IEC101Slave_" + oSlaveNo) currentSlave = "IEC101Slave_" + nSlaveNo;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void changeMDSequence(int oMDNo, int nMDNo)
        {
            string strRoutineName = "changeMDSequence";
            try
            {
                if (oMDNo == nMDNo) return;
                foreach (AI ain in aiList)
                {
                    if (ain.ResponseType == "MDSlindingWindow" && ain.Index == oMDNo.ToString() && ain.SubIndex == "0")
                    {
                        ain.Index = nMDNo.ToString();
                        if (ain.Description == "MDSlindingWindow_" + oMDNo) ain.Description = "MDSlindingWindow_" + nMDNo;
                        break;
                    }
                }

                foreach (AI ain in aiList)
                {
                    if (ain.ResponseType == "MDWindow" && ain.Index == oMDNo.ToString() && ain.SubIndex == "0")
                    {
                        ain.Index = nMDNo.ToString();
                        if (ain.Description == "MDWindow_" + oMDNo) ain.Description = "MDWindow_" + nMDNo;
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void changeDPSequence(int oDPNo, int nDPNo)
        {
            string strRoutineName = "changeDPSequence";
            try
            {
                if (oDPNo == nDPNo) return;
                foreach (AI ain in aiList)
                {
                    if (ain.ResponseType == "DerivedParam" && ain.Index == oDPNo.ToString() && ain.SubIndex == "0")
                    {
                        ain.Index = nDPNo.ToString();
                        if (ain.Description == "DerivedParam_" + oDPNo) ain.Description = "DerivedParam_" + nDPNo;
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateAISequence()
        {
            string strRoutineName = "regenerateAISequence";
            try
            {
                foreach (AI ain in aiList)
                {
                    int oAINo = Int32.Parse(ain.AINo);
                    int nAINo = Globals.AINo++;
                    ain.AINo = nAINo.ToString();
                    List<string> ReIndexedList = new List<string>();
                    //Now Change in Map...
                    foreach (KeyValuePair<string, List<AIMap>> maps in slavesAIMapList)
                    {
                        List<AIMap> saimList = maps.Value;
                        foreach (AIMap aim in saimList)
                        {
                            if (aim.AINo == oAINo.ToString() && !aim.IsReindexed)
                            {
                                //Ajay: 30/07/2018 if same AI mapped again it should take same AI no on reindex.
                                //aim.AINo = nAINo.ToString();
                                //aim.IsReindexed = true;
                                saimList.Where(x => x.AINo == oAINo.ToString()).ToList().ForEach(x => { x.AINo = nAINo.ToString(); x.IsReindexed = true; });
                                break;
                            }
                        }
                    }
                    //Now change in Parameter Load nodes...
                    Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeAISequence(oAINo, nAINo);
                }
                //Reset reindex status, for next use...
                foreach (KeyValuePair<string, List<AIMap>> maps in slavesAIMapList)
                {
                    List<AIMap> saimList = maps.Value;
                    foreach (AIMap aim in saimList)
                    {
                        aim.IsReindexed = false;
                    }
                }
                refreshList();
                refreshCurrentMapList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        public int GetReportingIndex(string slaveNum, string slaveID, int value)
        {
            int ret = 0;
            List<AIMap> slaveAIMapList;
            if (!slavesAIMapList.TryGetValue(slaveID, out slaveAIMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                return ret;
            }

            foreach (AIMap aim in slaveAIMapList)
            {
                if (aim.AINo == value.ToString()) return Int32.Parse(aim.ReportingIndex);
            }
            return ret;
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("AI_"))
            {
                CheckIEC104SlaveStatusChanges();  //If a IEC104 slave added/deleted, reflect in UI as well as objects.
                CheckMODBUSSlaveStatusChanges();  //If a MODBUS slave added/deleted, reflect in UI as well as objects.
                CheckIEC61850laveStatusChanges(); //If a IEC61850 slave added/deleted, reflect in UI as well as objects.
                CheckIEC101SlaveStatusChanges(); //If a IEC101 slave added/deleted, reflect in UI as well as objects.
                ShowHideSlaveColumns();
                return ucai;
            }
            return null;
        }
        public void parseAICNode(XmlNode aicNode, bool imported)
        {
            string strRoutineName = "parseAICNode";
            try
            {
                if (aicNode == null)
                {
                    rnName = "AIConfiguration";
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
                    aiList.Add(new AI(node, masterType, masterNo, IEDNo, imported));
                }
                refreshList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void parseAIMNode(string slaveNum, string slaveID, XmlNode aimNode)
        {
            CreateNewSlave(slaveNum, slaveID, aimNode);
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        //Namrata:27/7/2017
        public int getAIMapCount()
        {
            int ctr = 0;
            List<AIMap> saimList;
            if (!slavesAIMapList.TryGetValue(currentSlave, out saimList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                refreshMapList(null);
            }
            else
            {
                refreshMapList(saimList);
            }
            if (saimList == null)
            {
                return 0;
            }
            else
            {
                foreach (AIMap asaa in saimList)
                {
                    if (asaa.IsNodeComment) continue;
                    ctr++;
                }
            }
            return ctr;
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (AI aiNode in aiList)
            {
                if (aiNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<AI> getAIs()
        {
            return aiList;
        }
        public bool removeAI(string responseType, int Idx, int subIdx)
        {
            bool removed = false;
            for (int i = 0; i < aiList.Count; i++)
            {
                if (aiList[i].IsNodeComment) continue;
                AI tmp = aiList[i];
                if (tmp.Index == Idx.ToString() && tmp.SubIndex == subIdx.ToString() && tmp.ResponseType == responseType)
                {
                    aiList.RemoveAt(i);
                    removed = true;
                    break;
                }
            }
            return removed;
        }
        public List<AIMap> getSlaveAIMaps(string slaveID)
        {
            List<AIMap> slaveAIMapList;
            slavesAIMapList.TryGetValue(slaveID, out slaveAIMapList);
            return slaveAIMapList;
        }
        public void EnableEventsVirtualOnLoad()
        {
            string strRoutineName = "EnableEventsVirtualOnLoad";
            try
            {
                ucai.btnAdd.Enabled = false;
                ucai.btnDelete.Enabled = false;
                EnabledisableEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //public void EnableEventsForIEC61850AI()
        //{
        //    string strRoutineName = "EnableEventsForIEC61850AI";
        //    try
        //    {
        //        ucai.cmbDataType.Visible = false;
        //        ucai.lblDT.Visible = false;
        //        ucai.txtAIAutoMapRange.Enabled = true;
        //        ucai.lblAutoMap.Enabled = true;
        //        ucai.txtAIAutoMapRange.Visible = true;
        //        ucai.lblAutoMap.Visible = true;
        //        ucai.lblRT.Visible = false;
        //        ucai.lblRT.Enabled = false;
        //        ucai.cmbResponseType.Enabled = false;
        //        ucai.cmbResponseType.Visible = false;
        //        ucai.txtIndex.Visible = false;
        //        ucai.lblIdx.Visible = false;
        //        ucai.txtAINo.Size = new Size(390, 21);
        //        ucai.lblSIdx.Location = new Point(13, 58);
        //        ucai.txtSubIndex.Location = new Point(102, 55);
        //        ucai.txtSubIndex.Size = new Size(390, 21);
        //        ucai.lblM.Location = new Point(13, 84);
        //        ucai.txtMultiplier.Location = new Point(102, 81);
        //        ucai.txtMultiplier.Size = new Size(390, 21);
        //        ucai.lblC.Location = new Point(13, 109);
        //        ucai.txtConstant.Location = new Point(102, 106);
        //        ucai.txtConstant.Size = new Size(390, 21);
        //        ucai.label2.Location = new Point(13, 134);
        //        ucai.cmbIEDName.Location = new Point(102, 131);
        //        ucai.cmbIEDName.Size = new Size(390, 21);
        //        ucai.label3.Location = new Point(13, 160);
        //        ucai.cmb61850ResponseType.Location = new Point(102, 157);
        //        ucai.cmb61850ResponseType.Size = new Size(390, 21);
        //        ucai.label4.Location = new Point(13, 185);
        //        ucai.cmb61850Index.Location = new Point(102, 182);
        //        ucai.cmb61850Index.Size = new Size(390, 21);

        //        ucai.lblFC.Location = new Point(13, 210);
        //        ucai.txtFC.Location = new Point(102, 207);
        //        ucai.txtFC.Size = new Size(390, 21);
        //        ucai.lblDesc.Location = new Point(13, 236);
        //        ucai.txtDescription.Location = new Point(102, 233);
        //        ucai.txtDescription.Size = new Size(390, 21);
        //        ucai.lblAutoMap.Location = new Point(13, 262);
        //        ucai.txtAIAutoMapRange.Location = new Point(102, 259);
        //        ucai.txtAIAutoMapRange.Size = new Size(390, 21);

        //        ucai.btnDone.Location = new Point(200, 300);
        //        ucai.btnCancel.Location = new Point(300, 300);
        //        ucai.grpAI.Size = new Size(510, 325);
        //        ucai.grpAI.Location = new Point(172, 48);
        //        ucai.pbHdr.Width = 510;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        public void EnableEventsForIEC61850AI()
        {
            string strRoutineName = "AI: EnableEventsForIEC61850AI";
            try
            {
                ucai.cmbDataType.Visible = false;
                ucai.lblDT.Visible = false;
                ucai.txtAIAutoMapRange.Enabled = true;
                ucai.lblAutoMap.Enabled = true;
                ucai.txtAIAutoMapRange.Visible = true;
                ucai.lblAutoMap.Visible = true;
                ucai.lblRT.Visible = false;
                ucai.lblRT.Enabled = false;
                ucai.cmbResponseType.Enabled = false;
                ucai.cmbResponseType.Visible = false;
                ucai.txtIndex.Visible = false;
                ucai.lblIdx.Visible = false;


                ucai.txtAINo.Size = new Size(300, 21);

                ucai.label2.Location = new Point(13, 58);
                ucai.cmbIEDName.Location = new Point(102, 55);
                ucai.cmbIEDName.Size = new Size(300, 21);

                ucai.label3.Location = new Point(13, 84);
                ucai.cmb61850ResponseType.Location = new Point(102, 81);
                ucai.cmb61850ResponseType.Size = new Size(300, 21);

                ucai.label4.Location = new Point(13, 109);
                ucai.cmb61850Index.Location = new Point(102, 106);
                ucai.cmb61850Index.Size = new Size(300, 21);

                ucai.lblFC.Location = new Point(13, 134);
                ucai.txtFC.Location = new Point(102, 131);
                ucai.txtFC.Size = new Size(300, 21);
                ucai.txtFC.Enabled = false;

                ucai.lblSIdx.Location = new Point(13, 160);
                ucai.txtSubIndex.Location = new Point(102, 157);
                ucai.txtSubIndex.Size = new Size(300, 21);

                ucai.lblM.Location = new Point(13, 185);
                ucai.txtMultiplier.Location = new Point(102, 182);
                ucai.txtMultiplier.Size = new Size(300, 21);

                ucai.lblC.Location = new Point(13, 210);
                ucai.txtConstant.Location = new Point(102, 207);
                ucai.txtConstant.Size = new Size(300, 21);

                //ucai.label2.Location = new Point(13, 134);
                //ucai.cmbIEDName.Location = new Point(102, 131);
                //ucai.cmbIEDName.Size = new Size(300, 21);

                //ucai.label3.Location = new Point(13, 160);
                //ucai.cmb61850ResponseType.Location = new Point(102, 157);
                //ucai.cmb61850ResponseType.Size = new Size(300, 21);

                //ucai.label4.Location = new Point(13, 185);
                //ucai.cmb61850Index.Location = new Point(102, 182);
                //ucai.cmb61850Index.Size = new Size(300, 21);

                //ucai.lblFC.Location = new Point(13, 210);
                //ucai.txtFC.Location = new Point(102, 207);
                //ucai.txtFC.Size = new Size(300, 21);
                //ucai.txtFC.Enabled = false;

                ucai.lblDesc.Location = new Point(13, 236);
                ucai.txtDescription.Location = new Point(102, 233);
                ucai.txtDescription.Size = new Size(300, 21);

                ucai.lblAutoMap.Location = new Point(13, 262);
                ucai.txtAIAutoMapRange.Location = new Point(102, 259);
                ucai.txtAIAutoMapRange.Size = new Size(300, 21);

                ucai.btnDone.Location = new Point(170, 285);
                ucai.btnCancel.Location = new Point(270, 285);
                ucai.grpAI.Size = new Size(510, 325);
                ucai.grpAI.Width = 430;
                ucai.grpAI.Location = new Point(172, 52);
                ucai.pbHdr.Width = 510;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //public void EnableEventsForIEC61850AI()
        //{
        //    string strRoutineName = "EnableEventsForIEC61850AI";
        //    try
        //    {
        //        ucai.cmbDataType.Visible = false;
        //        ucai.lblDT.Visible = false;
        //        ucai.txtAIAutoMapRange.Enabled = true;
        //        ucai.lblAutoMap.Enabled = true;
        //        ucai.txtAIAutoMapRange.Visible = true;
        //        ucai.lblAutoMap.Visible = true;
        //        ucai.lblRT.Visible = false;
        //        ucai.lblRT.Enabled = false;
        //        ucai.cmbResponseType.Enabled = false;
        //        ucai.cmbResponseType.Visible = false;
        //        ucai.txtIndex.Visible = false;
        //        ucai.lblIdx.Visible = false;


        //        ucai.txtAINo.Size = new Size(300, 21);
        //        ucai.lblSIdx.Location = new Point(13, 58);
        //        ucai.txtSubIndex.Location = new Point(102, 55);
        //        ucai.txtSubIndex.Size = new Size(300, 21);
        //        ucai.lblM.Location = new Point(13, 84);
        //        ucai.txtMultiplier.Location = new Point(102, 81);
        //        ucai.txtMultiplier.Size = new Size(300, 21);
        //        ucai.lblC.Location = new Point(13, 109);
        //        ucai.txtConstant.Location = new Point(102, 106);
        //        ucai.txtConstant.Size = new Size(300, 21);
        //        ucai.label2.Location = new Point(13, 134);
        //        ucai.cmbIEDName.Location = new Point(102, 131);
        //        ucai.cmbIEDName.Size = new Size(300, 21);
        //        ucai.label3.Location = new Point(13, 160);
        //        ucai.cmb61850ResponseType.Location = new Point(102, 157);
        //        ucai.cmb61850ResponseType.Size = new Size(300, 21);
        //        ucai.label4.Location = new Point(13, 185);


        //        ucai.cmb61850Index.Location = new Point(102, 182);
        //        ucai.cmb61850Index.Size = new Size(300, 21);

        //        ucai.lblFC.Location = new Point(13, 210);
        //        ucai.txtFC.Location = new Point(102, 207);
        //        ucai.txtFC.Size = new Size(300, 21);
        //        ucai.txtFC.Enabled = false;
        //        ucai.lblDesc.Location = new Point(13, 236);
        //        ucai.txtDescription.Location = new Point(102, 233);
        //        ucai.txtDescription.Size = new Size(300, 21);
        //        ucai.lblAutoMap.Location = new Point(13, 262);
        //        ucai.txtAIAutoMapRange.Location = new Point(102, 259);
        //        ucai.txtAIAutoMapRange.Size = new Size(300, 21);

        //        ucai.btnDone.Location = new Point(170, 285);
        //        ucai.btnCancel.Location = new Point(270, 285);
        //        ucai.grpAI.Size = new Size(510, 325);
        //        ucai.grpAI.Width = 430;
        //        ucai.grpAI.Location = new Point(172, 52);
        //        ucai.pbHdr.Width = 510;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        public void EnabledisableEvents()
        {
            string strRoutineName = "EnabledisableEvents";
            try
            {
                ucai.txtAIAutoMapRange.Enabled = true;
                ucai.lblAutoMap.Enabled = true;
                ucai.txtAIAutoMapRange.Visible = true;
                ucai.lblAutoMap.Visible = true;
                ucai.label2.Visible = false;
                ucai.cmbIEDName.Visible = false; ;
                ucai.label3.Visible = false;
                ucai.cmb61850ResponseType.Visible = false;
                ucai.label4.Visible = false;
                ucai.cmb61850Index.Visible = false;
                ucai.label2.Enabled = false;
                ucai.cmbIEDName.Enabled = false;
                ucai.label3.Enabled = false;
                ucai.cmb61850ResponseType.Enabled = false;
                ucai.label4.Enabled = false;
                ucai.cmb61850Index.Enabled = false;
                ucai.lblDesc.Location = new Point(13, 210);
                ucai.txtDescription.Location = new Point(102, 207);
                ucai.lblAutoMap.Location = new Point(13, 236);
                ucai.txtAIAutoMapRange.Location = new Point(102, 233);
                ucai.btnDone.Location = new Point(102, 260);
                ucai.btnCancel.Location = new Point(223, 260);
                ucai.btnFirst.Location = new Point(55, 300);
                ucai.btnPrev.Location = new Point(130, 300);
                ucai.btnNext.Location = new Point(190, 300);
                ucai.btnLast.Location = new Point(240, 300);
                ucai.grpAI.Height = 300;// 310;
                ucai.grpAI.Location = new Point(172, 52);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EnabledisableEventsIEC103()
        {
            string strRoutineName = "EnabledisableEvents";
            try
            {
                ucai.txtAIAutoMapRange.Enabled = true;
                ucai.lblAutoMap.Enabled = true;
                ucai.txtAIAutoMapRange.Visible = true;
                ucai.lblAutoMap.Visible = true;
                ucai.label2.Visible = false;
                ucai.cmbIEDName.Visible = false; ;
                ucai.label3.Visible = false;
                ucai.cmb61850ResponseType.Visible = false;
                ucai.label4.Visible = false;
                ucai.cmb61850Index.Visible = false;
                ucai.label2.Enabled = false;
                ucai.cmbIEDName.Enabled = false;
                ucai.label3.Enabled = false;
                ucai.cmb61850ResponseType.Enabled = false;
                ucai.label4.Enabled = false;
                ucai.cmb61850Index.Enabled = false;
                ucai.lblDesc.Location = new Point(13, 210);
                ucai.txtDescription.Location = new Point(102, 207);
                ucai.lblAutoMap.Location = new Point(13, 236);
                ucai.txtAIAutoMapRange.Location = new Point(102, 233);
                ucai.btnDone.Location = new Point(102, 260);
                ucai.btnCancel.Location = new Point(223, 260);
                ucai.btnFirst.Location = new Point(55, 300);
                ucai.btnPrev.Location = new Point(130, 300);
                ucai.btnNext.Location = new Point(190, 300);
                ucai.btnLast.Location = new Point(240, 300);
                ucai.grpAI.Height = 300;// 310;
                ucai.grpAI.Location = new Point(172, 52);

                //Namrata:27/04/2018
                ucai.txtAINo.Size = new Size(230, 20);
                ucai.cmbResponseType.Size = new Size(230, 20);
                ucai.txtIndex.Size = new Size(230, 20);
                ucai.txtSubIndex.Size = new Size(230, 20);
                ucai.cmbDataType.Size = new Size(230, 20);
                ucai.txtMultiplier.Size = new Size(230, 20);
                ucai.txtConstant.Size = new Size(230, 20);
                ucai.txtDescription.Size = new Size(230, 20);
                ucai.txtAIAutoMapRange.Size = new Size(230, 20);
                ucai.grpAI.Size = new Size(350, 300);
                ucai.pbHdr.Width = 350;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void EnabledisableEventsOnDoubleClick()
        {
            string strRoutineName = "EnabledisableEventsOnDoubleClick";
            try
            {
                ucai.label2.Visible = false;
                ucai.cmbIEDName.Visible = false; ;
                ucai.label3.Visible = false;
                ucai.cmb61850ResponseType.Visible = false;
                ucai.label4.Visible = false;
                ucai.cmb61850Index.Visible = false;
                ucai.label2.Enabled = false;
                ucai.cmbIEDName.Enabled = false;
                ucai.label3.Enabled = false;
                ucai.cmb61850ResponseType.Enabled = false;
                ucai.label4.Enabled = false;
                ucai.cmb61850Index.Enabled = false;
                ucai.lblDesc.Location = new Point(13, 210);
                ucai.txtDescription.Location = new Point(102, 207);
                ucai.lblAutoMap.Visible = false;
                ucai.txtAIAutoMapRange.Visible = false;
                ucai.btnDone.Location = new Point(102, 235);
                ucai.btnCancel.Location = new Point(223, 235);
                ucai.btnFirst.Location = new Point(13, 265);
                ucai.btnPrev.Location = new Point(88, 265);
                ucai.btnNext.Location = new Point(163, 265);
                ucai.btnLast.Location = new Point(238, 265);
                ucai.grpAI.Location = new Point(172, 52);
                ucai.grpAI.Height = 297;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void EnableEventsForIEC61850AIOnDoubleClick()
        {
            string strRoutineName = "EnableEventsForIEC61850AIOnDoubleClick";
            try
            {
                ucai.lblRT.Visible = false;
                ucai.lblRT.Enabled = false;
                ucai.cmbResponseType.Enabled = false;
                ucai.cmbResponseType.Visible = false;
                ucai.txtIndex.Visible = false;
                ucai.lblIdx.Visible = false;
                ucai.txtAINo.Size = new Size(300, 21);

                ucai.label2.Location = new Point(13, 58);
                ucai.cmbIEDName.Location = new Point(102, 55);
                ucai.cmbIEDName.Size = new Size(300, 21);

                ucai.label3.Location = new Point(13, 84);
                ucai.cmb61850ResponseType.Location = new Point(102, 81);
                ucai.cmb61850ResponseType.Size = new Size(300, 21);

                ucai.label4.Location = new Point(13, 109);
                ucai.cmb61850Index.Location = new Point(102, 106);
                ucai.cmb61850Index.Size = new Size(300, 21);

                ucai.lblFC.Location = new Point(13, 134);
                ucai.txtFC.Location = new Point(102, 131);
                ucai.txtFC.Size = new Size(300, 21);
                ucai.txtFC.Enabled = false;

                ucai.lblSIdx.Location = new Point(13, 160);
                ucai.txtSubIndex.Location = new Point(102, 157);
                ucai.txtSubIndex.Size = new Size(300, 21);

                ucai.lblM.Location = new Point(13, 185);
                ucai.txtMultiplier.Location = new Point(102, 182);
                ucai.txtMultiplier.Size = new Size(300, 21);

                ucai.lblC.Location = new Point(13, 210);
                ucai.txtConstant.Location = new Point(102, 207);
                ucai.txtConstant.Size = new Size(300, 21);

                ucai.lblDesc.Location = new Point(13, 236);
                ucai.txtDescription.Location = new Point(102, 233);
                ucai.txtDescription.Size = new Size(300, 21);
                ucai.lblAutoMap.Visible = false;
                ucai.txtAIAutoMapRange.Visible = false;

                ucai.btnDone.Location = new Point(160, 260);
                ucai.btnCancel.Location = new Point(260, 260);
                ucai.btnFirst.Location = new Point(100, 290);
                ucai.btnPrev.Location = new Point(180, 290);
                ucai.btnNext.Location = new Point(260, 290);
                ucai.btnLast.Location = new Point(340, 290);
                ucai.grpAI.Size = new Size(510, 320);
                ucai.grpAI.Width = 430;
                ucai.grpAI.Location = new Point(172, 52);
                ucai.pbHdr.Width = 510;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Ajay: 25/09/2018
        public bool IsAIExist(string RT, int iIndex)
        {
            return aiList.Where(x => x.Index == iIndex.ToString() && x.SubIndex == "0" && x.ResponseType == RT).Any();
        }
    }
}
