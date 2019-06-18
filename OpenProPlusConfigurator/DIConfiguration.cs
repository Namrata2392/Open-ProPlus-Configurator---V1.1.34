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
    /**
    * \brief     <b>DIConfiguration</b> is a class to store info about all DI's and there corresponding mapping infos.
    * \details   This class stores info related to all DI's and there corresponding mapping's for various slaves. It allows
    * user to add multiple DI's. The user can map this DI's to various slaves created in the system, along with the mapping parameters
    * for those slave types. It also exports the XML node related to this object.
    * 
    */
    public class DIConfiguration
    {
        #region Declaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private Mode mapMode = Mode.NONE;
        private int mapEditIndex = -1;
        private bool isNodeComment = false;
        private string comment = "";
        private string currentSlave = "";
        Dictionary<string, List<DIMap>> slavesDIMapList = new Dictionary<string, List<DIMap>>();
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        private const int COL_CMD_TYPE_WIDTH = 130;
        //Namrata: 11/09/2017
        //Fill RessponseType in All Configuration . 
        public DataGridView dataGridViewDataSet = new DataGridView();
        public DataTable dtdataset = new DataTable();
        DataRow datasetRow;
        private string Response = "";
        List<DI> diList = new List<DI>();
        ucDIlist ucdi = new ucDIlist();
        DataSet DsRCB = new DataSet();
        List<DIMap> slaveDIMapList;
        private RCBConfiguration RCBNode = null;
        #endregion Declaration
        private string getDescription(ListView lstview, string ainno)
        {
            int iColIndex = ucdi.lvDIlist.Columns["Description"].Index;
            var query = lstview.Items
                    .Cast<ListViewItem>()
                    .Where(item => item.SubItems[0].Text == ainno).Select(s => s.SubItems[iColIndex].Text).Single();
            return query.ToString();
        }
        public DIConfiguration(MasterTypes mType, int mNo, int iNo)
        {
            string strRoutineName = "DIConfiguration";
            try
            {
                masterType = mType;
                masterNo = mNo;
                IEDNo = iNo;
                ucdi.ucDIlistLoad += new System.EventHandler(this.ucDIlist_Load);
                ucdi.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucdi.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucdi.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucdi.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucdi.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucdi.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucdi.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucdi.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucdi.linkDOClick += new System.EventHandler(this.linkDO_Click);
                ucdi.linkLabel1Click += new System.EventHandler(this.linkLabel1_Click);
                ucdi.cmbIEDName.SelectedIndexChanged += new System.EventHandler(this.cmbIEDName_SelectedIndexChanged);
                ucdi.cmb61850DIResponseType.SelectedIndexChanged += new System.EventHandler(this.cmb61850DIResponseType_SelectedIndexChanged);
                ucdi.cmb61850DIIndex.SelectedIndexChanged += new System.EventHandler(this.cmb61850DIIndex_SelectedIndexChanged);
                //ucdi.lvDIlistSelectedIndexChanged += new System.EventHandler(this.lvDIlist_SelectedIndexChanged); //---For Alternate Colour
                //ucdi.lvDIMapSelectedIndexChanged += new System.EventHandler(this.lvDIMap_SelectedIndexChanged); //---For Alternate Colour
                ucdi.lvDIlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIlist_ItemSelectionChanged);
                //Namrata: 27/7/2017
                ucdi.lvDIMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIMap_ItemSelectionChanged);
                if (mType == MasterTypes.Virtual)//Disable add/edit/delete/dblclick n remove checkboxes...
                {
                    EnableEventsVirtualOnLoad();
                }
                if (mType == MasterTypes.IEC101)
                {
                    EnableEventsIEC101AndIEC103OnLoad();
                }
                if (mType == MasterTypes.IEC103)
                {
                    EnableEventsIEC101AndIEC103OnLoad();
                }
                if (mType == MasterTypes.ADR)
                {
                    EnableEventsADRLoad();
                }
                if (mType == MasterTypes.IEC61850Client)
                {
                    EnableEventsIEC61850OnLoad();
                }
                if (mType == MasterTypes.MODBUS)
                {
                    EnableEventsMODBUSOnLoad();
                }
                ucdi.btnDIMDeleteClick += new System.EventHandler(this.btnDIMDelete_Click);
                ucdi.btnDIMDoneClick += new System.EventHandler(this.btnDIMDone_Click);
                ucdi.btnDIMCancelClick += new System.EventHandler(this.btnDIMCancel_Click);
                ucdi.lvDIMapDoubleClick += new System.EventHandler(this.lvDIMap_DoubleClick);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int SelectedIndex;
        private void lvDIlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color GreenColour = Color.FromArgb(82, 208, 23);
            if (ucdi.lvDIlist.SelectedIndices.Count > 0)
            {
                SelectedIndex = Convert.ToInt32(ucdi.lvDIlist.SelectedItems[0].Text);
                ucdi.lvDIMap.SelectedItems.Clear();
                ucdi.lvDIMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucdi.lvDIMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucdi.lvDIMap.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);

                ucdi.lvDIlist.SelectedItems.Clear();
                ucdi.lvDIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucdi.lvDIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucdi.lvDIlist.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
            }
        }
        private void lvDIMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color GreenColour = Color.FromArgb(82, 208, 23);
            if (ucdi.lvDIMap.SelectedIndices.Count > 0)
            {
                SelectedIndex = Convert.ToInt32(ucdi.lvDIMap.SelectedItems[0].Text);
                ucdi.lvDIlist.SelectedItems.Clear();
                ucdi.lvDIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucdi.lvDIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucdi.lvDIlist.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);

                ucdi.lvDIMap.SelectedItems.Clear();
                ucdi.lvDIMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucdi.lvDIMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucdi.lvDIMap.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
            }
        }
        private void ucDIlist_Load(object sender, EventArgs e)
        {
            string strRoutineName = "ucDIlist_Load";
            try
            {
                foreach (var L in Utils.VirtualPLU)
                {
                    if (L.Run == "YES")
                    {
                        ucdi.btnAdd.Enabled = true;
                        ucdi.btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FetchComboboxData()
        {
            //Namrata: 13/03/2018
            ucdi.cmbIEDName.DataSource = null;
            List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
            string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
            //Namrata: 26/04/2018
            if (tblName != null)
            {
                ucdi.cmbIEDName.DataSource = Utils.dsIED.Tables[tblName];
                ucdi.cmbIEDName.DisplayMember = "IEDName";
                //Namrata: 21/03/2018
                ucdi.cmb61850DIResponseType.DataSource = Utils.dsResponseType.Tables[tblName];
                ucdi.cmb61850DIResponseType.DisplayMember = "Address";
                //Namrata: 29/03/2018
                ucdi.cmb61850DIIndex.DataSource = Utils.DsAllConfigurationData.Tables[tblName + "_On Request"];
                ucdi.cmb61850DIIndex.DisplayMember = "ObjectReferrence";
                ucdi.cmb61850DIIndex.ValueMember = "Node";
            }
            else
            {
                //MessageBox.Show("ICD File Missing !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutinename = "btnAdd_Click";
            try
            {
                if (diList.Count >= getMaxDIs())
                {
                    MessageBox.Show("Maximum " + getMaxDIs() + " DI's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;

                if ((masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC101))
                {
                    EnableEventsIEC101AndIEC103OnLoad();
                }
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    EnableEventsIEC61850OnLoad();
                    ucdi.txtFC.Enabled = false;
                    FetchComboboxData();
                }
                else if (masterType == MasterTypes.MODBUS)
                {
                    EnableEventsMODBUSOnLoad();
                }
                else if (masterType == MasterTypes.ADR)
                {
                    EnableEventsADRLoad();
                }
                else if (masterType == MasterTypes.Virtual)
                {
                    EnableEventsVirtualOnLoad();
                }
                Utils.resetValues(ucdi.grpDI);
                Utils.showNavigation(ucdi.grpDI, false);
                loadDefaults();
                ucdi.grpDI.Visible = true;
                ucdi.cmbResponseType.Focus();
                //Namrata: 04/04/2018
                if (masterType == MasterTypes.IEC61850Client)
                {
                    if (ucdi.cmbIEDName.SelectedIndex != -1)
                    {
                        ucdi.cmbIEDName.SelectedIndex = ucdi.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                        ucdi.txtFC.Text = ((DataRowView)ucdi.cmb61850DIIndex.SelectedItem).Row[2].ToString();
                        //Namrata: 10/04/2018
                        if ((ucdi.cmb61850DIIndex.Text.ToString() == "") || (ucdi.cmb61850DIResponseType.Text.ToString() == ""))
                        {
                            MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        ucdi.cmbIEDName.Visible = false;
                        MessageBox.Show("ICD File Missing !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutinename + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void linkDO_Click(object sender, EventArgs e)
        {
            string strRoutineName = "linkDO_Click";
            try
            {
                foreach (ListViewItem listItem in ucdi.lvDIlist.Items)
                {
                    listItem.Checked = true;
                }
                DialogResult result = MessageBox.Show("Do you want to delete all records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    foreach (ListViewItem listItem in ucdi.lvDIlist.Items)
                    {
                        listItem.Checked = false;
                    }
                    return;
                }
                for (int i = ucdi.lvDIlist.Items.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    if (Utils.IsExistDIinPLC(diList.ElementAt(i).DINo))
                    {
                        DialogResult ask = MessageBox.Show("DI " + diList.ElementAt(i).DINo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (ask == DialogResult.No)
                        {
                            continue;
                        }
                        Utils.DeleteDIFromPLC(diList.ElementAt(i).DINo);
                    }
                    deleteDIFromMaps(diList.ElementAt(i).DINo);
                    diList.RemoveAt(i);
                    ucdi.lvDIlist.Items.Clear();
                }
                Console.WriteLine("*** diList count: {0} lv count: {1}", diList.Count, ucdi.lvDIlist.Items.Count);
                refreshList();
                refreshCurrentMapList();   //Refresh map listview...
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void linkLabel1_Click(object sender, EventArgs e)
        {
            string strRoutineName = "linkLabel1_Click";
            try
            {
                List<DIMap> slaveDIMapList;
                if (!slavesDIMapList.TryGetValue(currentSlave, out slaveDIMapList))
                {
                    Console.WriteLine("##### Slave entries does not exists");
                    MessageBox.Show("Error deleting DI map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (ListViewItem listItem in ucdi.lvDIMap.Items)
                {
                    listItem.Checked = true;
                }
                DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    foreach (ListViewItem listItem in ucdi.lvDIMap.Items)
                    {
                        listItem.Checked = false;
                    }
                    return;
                }
                for (int i = ucdi.lvDIMap.Items.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    //if (i <= slaveDIMapList.Count)
                    {
                        slaveDIMapList.RemoveAt(i);
                    }
                }
                ucdi.lvDIMap.Items.Clear();

                //for (int i = ucdi.lvDIMap.Items.Count - 1; i >= 0; i--)
                //{
                //    Console.WriteLine("*** removing indices: {0}", i);
                //    if (slaveDIMapList.Where(y => y.DINo == ucdi.lvDIMap.Items[i].Text).Any())
                //    {
                //        slaveDIMapList.RemoveAt(i);
                //        ucdi.lvDIMap.Items.Clear();
                //    }
                //}

                //ucdi.lvDIMap.Items.Cast<ListViewItem>().ToList().ForEach(s =>
                //{
                //    if (slaveDIMapList.Where(y => y.DINo == s.Text).Any())
                //    {
                //        slaveDIMapList.Remove((DIMap)slaveDIMapList.Select(z => z.DINo == s.Text));
                //    }
                //});



                Console.WriteLine("*** slaveDIMapList count: {0} lv count: {1}", slaveDIMapList.Count, ucdi.lvDIMap.Items.Count);
                refreshMapList(slaveDIMapList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDelete_Click";
            try
            {
                var LitsItemsChecked = new List<KeyValuePair<int, int>>();
                DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    return;
                }
                for (int i = ucdi.lvDIlist.Items.Count - 1; i >= 0; i--)
                {
                    if (ucdi.lvDIlist.Items[i].Checked)
                    {
                        if (Utils.IsExistDIinPLC(diList.ElementAt(i).DINo))
                        {
                            DialogResult ask = MessageBox.Show("DI " + diList.ElementAt(i).DINo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", "Delete DI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (ask == DialogResult.No)
                            {
                                continue;
                            }
                            Utils.DeleteDIFromPLC(diList.ElementAt(i).DINo);
                        }
                        deleteDIFromMaps(diList.ElementAt(i).DINo);
                        diList.RemoveAt(i);
                        ucdi.lvDIlist.Items[i].Remove();
                    }
                }
                refreshList();
                refreshCurrentMapList();//Refresh map listview...
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbIEDName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "DI : cmbIEDName_SelectedIndexChanged";
            try
            {
                //Namrata: 04/04/2018
                if (ucdi.cmbIEDName.Focused == false)
                {

                }
                else
                {
                    Utils.Iec61850IEDname = ucdi.cmbIEDName.Text;
                    List<DataTable> dtList = Utils.dsResponseType.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                    if (dtList.Count == 0)
                    {
                        ucdi.cmb61850DIResponseType.DataSource = null;
                        ucdi.cmb61850DIIndex.DataSource = null;
                        ucdi.cmb61850DIResponseType.Enabled = false;
                        ucdi.cmb61850DIIndex.Enabled = false;
                        ucdi.txtFC.Text = "";
                    }
                    else
                    {
                        ucdi.cmb61850DIResponseType.Enabled = true;
                        ucdi.cmb61850DIIndex.Enabled = true;
                        ucdi.cmb61850DIResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//[Utils.strFrmOpenproplusTreeNode + "/" + "Undefined" + "/" + Utils.Iec61850IEDname];
                        ucdi.cmb61850DIResponseType.DisplayMember = "Address";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmb61850DIIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "DI : cmb61850DIIndex_SelectedIndexChanged";
            try
            {
                if (ucdi.cmb61850DIIndex.Items.Count > 0)
                {
                    if (ucdi.cmb61850DIIndex.SelectedIndex != -1)
                    {
                        ucdi.txtFC.Text = ((DataRowView)ucdi.cmb61850DIIndex.SelectedItem).Row[2].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void cmb61850DIResponseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "DI: cmb61850ResponseType_SelectedIndexChanged";
            try
            {
                if (ucdi.cmb61850DIResponseType.Items.Count > 1)
                {
                    if ((ucdi.cmb61850DIResponseType.SelectedIndex != -1))
                    {
                        //Namrata: 04/04/2018
                        Utils.Iec61850IEDname = ucdi.cmbIEDName.Text;
                        //Utils.Iec61850IEDname = ucai.cmbIEDName.Items.OfType<DataRowView>().Select(x => x.Row[0].ToString()).FirstOrDefault().ToString();
                        List<DataTable> dtList = Utils.DsAllConfigurationData.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                        DataSet dsdummy = new DataSet();
                        dtList.ForEach(tbl => { DataTable dt = tbl.Copy(); dsdummy.Tables.Add(dt); });
                        ucdi.cmb61850DIIndex.DataSource = dsdummy.Tables[ucdi.cmb61850DIResponseType.SelectedIndex];
                        ucdi.cmb61850DIIndex.DisplayMember = "ObjectReferrence";
                        ucdi.cmb61850DIIndex.ValueMember = "Node";
                    }
                }
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
                Utils.diList1.Clear();
                //if (!Validate()) return;
                Console.WriteLine("*** ucai btnDone_Click clicked in class!!!");
                List<KeyValuePair<string, string>> diData = Utils.getKeyValueAttributes(ucdi.grpDI);
                #region Fill Address to Datatable for RCBConfiguration 
                //Namrata: 27/09/2017
                //fill Address to Datatable for RCBConfiguration 
                if (masterType == MasterTypes.IEC61850Client)
                {
                    Response = ucdi.cmb61850DIResponseType.Text;
                    DataColumn dcAddressColumn;
                    if (!dtdataset.Columns.Contains("Address"))
                    { dcAddressColumn = dtdataset.Columns.Add("Address", typeof(string)); }
                    if (!dtdataset.Columns.Contains("IED"))
                    { dtdataset.Columns.Add("IED", typeof(string)); }
                    datasetRow = dtdataset.NewRow();
                    datasetRow["Address"] = Response.ToString();
                    datasetRow["IED"] = IEDNo.ToString();
                    dtdataset.Rows.Add(datasetRow);
                    //Namrata: 15/03/2018
                    dataGridViewDataSet.DataSource = dtdataset;
                    dtdataset.TableName = Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname;
                    string Index112 = "";
                    string[] arr112 = new string[dtdataset.Rows.Count];
                    string[] arrCo1l12 = new string[dtdataset.Rows.Count];
                    DataRow row112;
                    if (Utils.dsRCBDI.Tables.Contains(dtdataset.TableName))
                    {
                        Utils.dsRCBDI.Tables[dtdataset.TableName].Clear();
                    }
                    else
                    {
                        Utils.dsRCBDI.Tables.Add(dtdataset.TableName);
                        Utils.dsRCBDI.Tables[dtdataset.TableName].Columns.Add("ObjectReferrence");
                        Utils.dsRCBDI.Tables[dtdataset.TableName].Columns.Add("Node");
                    }
                    for (int i = 0; i < dtdataset.Rows.Count; i++)
                    {
                        row112 = Utils.dsRCBDI.Tables[dtdataset.TableName].NewRow();
                        Utils.dsRCBDI.Tables[dtdataset.TableName].NewRow();
                        for (int j = 0; j < dtdataset.Columns.Count; j++)
                        {
                            Index112 = dtdataset.Rows[i][j].ToString();
                            row112[j] = Index112.ToString();
                        }
                        Utils.dsRCBDI.Tables[dtdataset.TableName].Rows.Add(row112);
                    }
                    Utils.dsRCBData = Utils.dsRCBDI;
                    Utils.dsRCBData.Merge(Utils.dsRCBAI, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBAO, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBDO, false, MissingSchemaAction.Add);
                    Utils.dsRCBData.Merge(Utils.dsRCBEN, false, MissingSchemaAction.Add);
                }
                #endregion Fill Address to Datatable for RCBConfiguration 
                if (mode == Mode.ADD)
                {
                    int intStart = Convert.ToInt32(diData[10].Value); // DINo
                    int intRange = Convert.ToInt32(diData[7].Value); //AutoMapRange
                    int intDIIndex = Convert.ToInt32(diData[12].Value); // AIIndex
                    int DINumber1 = 0, DIINdex1 = 0;
                    string DIDescription = "";
                    //Namrata: 23/11/2017
                    if (intRange > getMaxDIs())
                    {
                        MessageBox.Show("Maximum " + getMaxDIs() + " DI's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        for (int i = intStart; i <= intStart + intRange - 1; i++)
                        {
                            DINumber1 = Globals.DINo;
                            DINumber1 += 1;
                            DIINdex1 = intDIIndex++;
                            #region Description For All Masters
                            if (masterType == MasterTypes.ADR)
                            {
                                DIDescription = ucdi.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.IEC101)
                            {
                                DIDescription = ucdi.txtDescription.Text;
                            }
                            if (masterType == MasterTypes.IEC103)
                            {
                                DIDescription = ucdi.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.MODBUS)
                            {
                                DIDescription = ucdi.txtDescription.Text;
                            }
                            else if (masterType == MasterTypes.IEC61850Client)
                            {
                                DIDescription = ucdi.txtDescription.Text;
                            }
                            //Namrata: 31/10/2017
                            else if (masterType == MasterTypes.Virtual)
                            {
                                DIDescription = ucdi.txtDescription.Text;
                            }
                            #endregion Description For All Masters
                            DI NewDI = new DI("DI", diData, null, masterType, masterNo, IEDNo);
                            NewDI.DINo = DINumber1.ToString();
                            NewDI.Index = DIINdex1.ToString();
                            NewDI.Description = DIDescription;
                            NewDI.IEDName = ucdi.cmbIEDName.Text.ToString();
                            NewDI.IEC61850Index = ucdi.cmb61850DIIndex.Text.ToString();
                            NewDI.IEC61850ResponseType = ucdi.cmb61850DIResponseType.Text.ToString();
                            //Namrata: 10/04/2018
                            if (masterType == MasterTypes.IEC61850Client)
                            {
                                if((ucdi.cmb61850DIIndex.Text.ToString() == "") || (ucdi.cmb61850DIResponseType.Text.ToString() == ""))
                                {
                                    MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                           diList.Add(NewDI);
                        }
                    }
                    Utils.diList1.AddRange(diList);
                }
                else if (mode == Mode.EDIT)
                {
                    diList[editIndex].updateAttributes(diData);
                    //Namrata: 10/04/2018
                    if (masterType == MasterTypes.IEC61850Client)
                    {
                        if ((ucdi.cmb61850DIIndex.Text.ToString() == "") || (ucdi.cmb61850DIResponseType.Text.ToString() == ""))
                        {
                            MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                refreshList();
                //Namrata: 15/03/2018
                if (masterType == MasterTypes.IEC61850Client)
                {
                    RCBNode = new RCBConfiguration(MasterTypes.IEC61850Client, Convert.ToInt32(Utils.MasterNumForIEC61850Client), Convert.ToInt32(Utils.UnitIDForIEC61850Client));
                    RCBNode.FillRCBList();
                }
                if (sender != null && e != null)
                {
                    ucdi.grpDI.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnCancel_Click";
            try
            {
                Console.WriteLine("*** ucdi btnCancel_Click clicked in class!!!");
                ucdi.grpDI.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucdi.grpDI);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnFirst_Click";
            try
            {
                Console.WriteLine("*** ucdi btnFirst_Click clicked in class!!!");
                if (ucdi.lvDIlist.Items.Count <= 0) return;
                if (diList.ElementAt(0).IsNodeComment) return;
                editIndex = 0;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnPrev_Click";
            try
            {
                //Namrata: 27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** ucdi btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (diList.ElementAt(editIndex - 1).IsNodeComment) return;
                editIndex--;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnNext_Click";
            try
            {
                //Namrata: 27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** ucdi btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= ucdi.lvDIlist.Items.Count) return;
                if (diList.ElementAt(editIndex + 1).IsNodeComment) return;
                editIndex++;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnLast_Click";
            try
            {
                Console.WriteLine("*** ucdi btnLast_Click clicked in class!!!");
                if (ucdi.lvDIlist.Items.Count <= 0) return;
                if (diList.ElementAt(diList.Count - 1).IsNodeComment) return;
                editIndex = diList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvDIlist_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvDIlist_DoubleClick";
            try
            {
                int ListIndex = ucdi.lvDIlist.FocusedItem.Index;
                //Namrata: 10/09/2017
                ucdi.txtAutpMapNumber.Text = "0";
                ucdi.txtAutpMapNumber.Enabled = false;
                ucdi.lblAutoMapNumber.Enabled = false;
                //if (ucdi.lvDIlist.SelectedItems.Count <= 0) return;
                //ListViewItem lvi = ucdi.lvDIlist.SelectedItems[0];
                //Namrata: 07/03/2018
                ListViewItem lvi = ucdi.lvDIlist.Items[ListIndex];//ucai.lvAIlist.SelectedItems[0];
                Utils.UncheckOthers(ucdi.lvDIlist, lvi.Index);
                if (diList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucdi.txtEvent_F.Text = "0";
                ucdi.txtEvent_T.Text = "0";
                ucdi.grpDI.Visible = true;
                #region Enable/Disable Controls Masterwise
                if (masterType == MasterTypes.IEC103 || masterType == MasterTypes.IEC101)
                {
                    VisibleFalseEventsOnButtonClick();
                }
                else if (masterType == MasterTypes.MODBUS)
                {
                    EnableDisbleonDoubleClickModbus();
                }
                else if (masterType == MasterTypes.ADR)
                {
                    EnaleEventsOnDoubleClikADR();
                }
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    EnableEvent61850OnDoubleClick();
                    FetchComboboxData();
                    //Namrata: 04/04/2018
                    ucdi.cmbIEDName.SelectedIndex = ucdi.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                    ucdi.txtFC.Text = ((DataRowView)ucdi.cmb61850DIIndex.SelectedItem).Row[2].ToString();
                    
                        if ((ucdi.cmb61850DIIndex.Text.ToString() == "") || (ucdi.cmb61850DIResponseType.Text.ToString() == ""))
                        {
                            MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    
                }
                else if (masterType == MasterTypes.Virtual)
                {
                    EnableEventVirtualDoubleClick();
                }
                #endregion Enable/Disable Controls Masterwise
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucdi.grpDI, true);
                loadValues();
                ucdi.cmbResponseType.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvDIlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "lvDIlist_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    Console.WriteLine("*** selected DI: {0}", diIndex);
                    //Namrata: 27/7/2017
                    ucdi.lvDIMapItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIMap_ItemSelectionChanged);
                    ucdi.lvDIMap.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucdi.lvDIMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucdi.lvDIMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucdi.lvDIMap);
                    //Namrata: 27/7/2017
                    ucdi.lvDIMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucdi.lvDIlist.SelectedItems.Clear();
                    ucdi.lvDIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucdi.lvDIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucdi.lvDIlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucdi.lvDIMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIMap_ItemSelectionChanged);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 27/7/2017
        private void lvDIMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "lvDIMap_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    Console.WriteLine("*** selected DI: {0}", diIndex);
                    //Namrata: 27/7/2017
                    ucdi.lvDIlistItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIlist_ItemSelectionChanged);
                    //Remove selection from DIMap...
                    ucdi.lvDIlist.SelectedItems.Clear();
                    ucdi.lvDIlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucdi.lvDIlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewMapItem(diIndex, ucdi.lvDIlist);
                    //Namrata: 27/7/2017
                    ucdi.lvDIlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucdi.lvDIMap.SelectedItems.Clear();
                    ucdi.lvDIMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucdi.lvDIMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucdi.lvDIMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucdi.lvDIlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIlist_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private void lvDIlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    string strRoutineName = "lvDIlist_ItemSelectionChanged";
        //    try
        //    {
        //        if (e.IsSelected)
        //        {
        //            string diIndex = e.Item.Text;
        //            Console.WriteLine("*** selected DI: {0}", diIndex);
        //            //Namrata: 27/7/2017
        //            ucdi.lvDIMapItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIMap_ItemSelectionChanged);
        //            ucdi.lvDIMap.SelectedItems.Clear();   //Remove selection from DIMap...
        //            Utils.highlightListviewItem(diIndex, ucdi.lvDIMap);
        //            //Namrata: 27/7/2017
        //            ucdi.lvDIMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIMap_ItemSelectionChanged);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        ////Namrata: 27/7/2017
        //private void lvDIMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    string strRoutineName = "lvDIMap_ItemSelectionChanged";
        //    try
        //    {
        //        if (e.IsSelected)
        //        {
        //            string diIndex = e.Item.Text;
        //            Console.WriteLine("*** selected DI: {0}", diIndex);
        //            //Namrata: 27/7/2017
        //            ucdi.lvDIlistItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIlist_ItemSelectionChanged);
        //            //Remove selection from DIMap...
        //            ucdi.lvDIlist.SelectedItems.Clear();
        //            Utils.highlightListviewMapItem(diIndex, ucdi.lvDIlist);
        //            //Namrata: 27/7/2017
        //            ucdi.lvDIlistItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDIlist_ItemSelectionChanged);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private void loadDefaults()
        {
            string strRoutineName = "loadDefaults";
            try
            {
                ucdi.txtAutpMapNumber.Text = "1";
                ucdi.txtDINo.Text = (Globals.DINo + 1).ToString();
                ucdi.txtIndex.Text = "5";
                ucdi.txtSubIndex.Text = "1";
                ucdi.txtEvent_T.Text = "257";
                ucdi.txtEvent_F.Text = "258";
                if (masterType == MasterTypes.ADR)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact("ADR_DI");
                    ucdi.txtDescription.Text = "ADR_DI";// + (Globals.DINo + 1).ToString();
                }
                else if (masterType == MasterTypes.IEC101)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact("DoublePoint");
                    ucdi.txtDescription.Text = "IEC101_DI";// + (Globals.DINo + 1).ToString();
                }
                if (masterType == MasterTypes.IEC103)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact("TimeTaggedMessage");
                    ucdi.txtDescription.Text = "IEC103_DI";// + (Globals.DINo + 1).ToString();
                }
                else if (masterType == MasterTypes.MODBUS)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact("ReadCoil");
                    ucdi.txtDescription.Text = "MODBUS_DI";// + (Globals.DINo + 1).ToString();
                }
                else if (masterType == MasterTypes.IEC61850Client)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact("ReadCoil");
                    ucdi.txtDescription.Text = "IEC61850_DI";// + (Globals.DINo + 1).ToString();
                }
                else if (masterType == MasterTypes.Virtual)
                {
                    if (ucdi.lvDIlist.Items.Count - 1 >= 0)
                    {
                        ucdi.txtIndex.Text = Convert.ToString(Convert.ToInt32(diList[diList.Count - 1].Index) + 1);
                    }
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact("DeviceMode");
                    ucdi.txtDescription.Text = "Virtual_DI";// + (Globals.DINo + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata:15/6/2017
        public bool UpdateDI(string responseType, int Idx, int SubOldIdx, int SubNewIdx, List<KeyValuePair<string, string>> diData)
        {
            bool Updated = false;
            for (int i = 0; i < diList.Count; i++)
            {
                if (diList[i].IsNodeComment) continue;
                DI tmp = diList[i];
                if (tmp.Index == Idx.ToString() && tmp.SubIndex == SubOldIdx.ToString() && tmp.ResponseType == responseType.ToString())
                {
                    diList[i].updateAttributes(diData);
                    Updated = true;
                    break;
                }
            }
            return Updated;
        }

        //Namrata: 27/09/2017
        //Remove Entry From Virtual DI when Active is "NO".
        public bool removeDINetwork(string responseType, int Idx, int SubOldIdx, int SubNewIdx, List<KeyValuePair<string, string>> diData)
        {
            bool removed = false;
            for (int i = 0; i < diList.Count; i++)
            {
                if (diList[i].IsNodeComment) continue;
                DI tmp = diList[i];
                if (tmp.Index == Idx.ToString() && tmp.SubIndex == SubOldIdx.ToString() && tmp.ResponseType == responseType)
                {
                    diList.RemoveAt(i);
                    removed = true;
                    break;
                }
            }
            return removed;
        }
        private void loadValues()
        {
            string strRoutineName = "loadValues";
            try
            {
                DI di = diList.ElementAt(editIndex);
                if (di != null)
                {
                    ucdi.txtDINo.Text = di.DINo;
                    ucdi.cmbResponseType.SelectedIndex = ucdi.cmbResponseType.FindStringExact(di.ResponseType);
                    ucdi.txtIndex.Text = di.Index;
                    ucdi.txtSubIndex.Text = di.SubIndex;
                    ucdi.txtDescription.Text = di.Description;
                    ucdi.txtEvent_T.Text = di.Event_T;
                    ucdi.txtEvent_F.Text = di.Event_F;
                    ucdi.cmbIEDName.SelectedIndex = ucdi.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);// (di.IEDName);
                    ucdi.cmb61850DIResponseType.SelectedIndex = ucdi.cmb61850DIResponseType.FindStringExact(di.IEC61850ResponseType);
                    ucdi.cmb61850DIIndex.SelectedIndex = ucdi.cmb61850DIIndex.FindStringExact(di.IEC61850Index);
                    //Namrata: 17/10/2017
                    ucdi.CmbDataType.SelectedIndex = ucdi.CmbDataType.FindStringExact(di.DataType);
                    ucdi.txtFC.Text = di.FC;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucdi.grpDI))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private int getMaxDIs()
        {
            if (masterType == MasterTypes.IEC103) return Globals.MaxIEC103DI;
            else if (masterType == MasterTypes.MODBUS) return Globals.MaxMODBUSDI;
            //Namrata:13/7/2017
            else if (masterType == MasterTypes.ADR) return Globals.MaxADRDI;
            else if (masterType == MasterTypes.IEC101) return Globals.MaxIEC101DI;
            else if (masterType == MasterTypes.IEC61850Client) return Globals.MaxIEC61850DI;
            //Namrata: 26/10/2017
            else if (masterType == MasterTypes.Virtual) return Globals.MaxPLUDI;
            else return 0;
        }
        public void refreshListOld()
        {
            string strRoutineName = "refreshList";
            try
            {
                int cnt = 0;
                ucdi.lvDIlist.Items.Clear();
                Utils.DIlistforDescription.Clear();
                //Namrata: 25/10/2017
                if (masterType == MasterTypes.Virtual)
                {
                    Utils.DummyDI.Clear();
                    foreach (DI di in diList)
                    {
                        string[] row = new string[5];
                        if (di.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = di.DINo;
                            row[1] = di.ResponseType;
                            row[2] = di.Index;
                            row[3] = di.SubIndex;
                            row[4] = di.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdi.lvDIlist.Items.Add(lvItem);
                    }
                }
                if (masterType == MasterTypes.MODBUS)
                {
                    foreach (DI di in diList)
                    {
                        string[] row = new string[9];
                        if (di.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = di.DINo;
                            row[1] = di.ResponseType;
                            row[2] = di.Index;
                            row[3] = di.SubIndex;
                            row[4] = di.DataType;
                            row[5] = di.Description;
                            row[6] = di.Event_T;
                            row[7] = di.Event_F;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdi.lvDIlist.Items.Add(lvItem);
                    }
                }
                if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.ADR))
                {
                    foreach (DI di in diList)
                    {
                        string[] row = new string[8];
                        if (di.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = di.DINo;
                            row[1] = di.ResponseType;
                            row[2] = di.Index;
                            row[3] = di.SubIndex;
                            row[4] = di.Event_T;
                            row[5] = di.Event_F;
                            row[6] = di.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdi.lvDIlist.Items.Add(lvItem);
                    }
                }
                if (masterType == MasterTypes.IEC61850Client)
                {
                    foreach (DI di in diList)
                    {
                        string[] row = new string[7];
                        if (di.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = di.DINo;
                            row[1] = di.IEDName;
                            row[2] = di.IEC61850ResponseType;
                            row[3] = di.IEC61850Index;
                            row[4] = di.SubIndex;
                            row[5] = di.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdi.lvDIlist.Items.Add(lvItem);
                    }
                }
                ucdi.lblDIRecords.Text = diList.Count.ToString();
                Utils.DilistRegenerateIndex.AddRange(diList);
                Utils.DummyDI.AddRange(diList);
                Utils.EnableDI.AddRange(diList);
                Utils.DIlistforDescription.AddRange(diList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void refreshList()
        {
            string strRoutineName = "DIConfiguration: refreshList";
            try
            {
                int cnt = 0;
                ucdi.lvDIlist.Items.Clear();
                Utils.DIlistforDescription.Clear();
                #region VirtualMaster
                //Namrata: 25/10/2017
                if (masterType == MasterTypes.Virtual)
                {
                    Utils.DummyDI.Clear();
                    foreach (DI di in diList)
                    {
                        string[] row = new string[5];
                        if (di.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = di.DINo;
                            row[1] = di.ResponseType;
                            row[2] = di.Index;
                            row[3] = di.SubIndex;
                            row[4] = di.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdi.lvDIlist.Items.Add(lvItem);
                    }
                }
                #endregion VirtualMaster

                #region MODBUS
                if (masterType == MasterTypes.MODBUS)
                {
                    foreach (DI di in diList)
                    {
                        string[] row = new string[9];
                        if (di.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = di.DINo;
                            row[1] = di.ResponseType;
                            row[2] = di.Index;
                            row[3] = di.SubIndex;
                            row[4] = di.DataType;
                            row[5] = di.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdi.lvDIlist.Items.Add(lvItem);
                    }
                }
                #endregion MODBUS

                #region ADRMaster
                if (masterType == MasterTypes.ADR)
                {
                    foreach (DI di in diList)
                    {
                        string[] row = new string[8];
                        if (di.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = di.DINo;
                            row[1] = di.ResponseType;
                            row[2] = di.Index;
                            row[3] = di.SubIndex;
                            row[4] = di.Event_T;
                            row[5] = di.Event_F;
                            row[6] = di.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdi.lvDIlist.Items.Add(lvItem);
                    }
                }
                #endregion ADRMaster

                #region IEC103master,IEC101master,ADRMaster
                if ((masterType == MasterTypes.IEC103) || (masterType == MasterTypes.IEC101))
                {
                    foreach (DI di in diList)
                    {
                        string[] row = new string[6];
                        if (di.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = di.DINo;
                            row[1] = di.ResponseType;
                            row[2] = di.Index;
                            row[3] = di.SubIndex;
                            row[4] = di.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdi.lvDIlist.Items.Add(lvItem);
                    }
                }
                #endregion IEC103master,IEC101master,ADRMaster

                #region IEC61850Client
                if (masterType == MasterTypes.IEC61850Client)
                {
                    foreach (DI di in diList)
                    {
                        string[] row = new string[8];
                        if (di.IsNodeComment)
                        {
                            row[0] = "Comment...";
                        }
                        else
                        {
                            row[0] = di.DINo;
                            row[1] = di.IEDName;
                            row[2] = di.IEC61850ResponseType;
                            row[3] = di.IEC61850Index;
                            row[4] = di.FC;
                            row[5] = di.SubIndex;
                            row[6] = di.Description;
                        }
                        ListViewItem lvItem = new ListViewItem(row);
                        if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                        ucdi.lvDIlist.Items.Add(lvItem);
                    }
                }
                #endregion IEC61850Client

                ucdi.lblDIRecords.Text = diList.Count.ToString();
                Utils.DilistRegenerateIndex.AddRange(diList); // For DI Reindex
                Utils.DummyDI.AddRange(diList); //For VirtualDI AutomaticEnteries
                Utils.EnableDI.AddRange(diList); // Validate DI exist in DOConfiguration
                Utils.DIlistforDescription.AddRange(diList); //For Description not present in XML 
                Utils.DiListForVirualDIImport.AddRange(diList); 
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /* ============================================= Below this, DI Map logic... ============================================= */
        private void CreateNewSlave(string slaveNum, string slaveID, XmlNode dimNode)
        {
            string strRoutineName = "CreateNewSlave";
            try
            {
                List<DIMap> sdimList = new List<DIMap>();
                slavesDIMapList.Add(slaveID, sdimList);
                if (dimNode != null)
                {
                    foreach (XmlNode node in dimNode)
                    {
                        if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                        sdimList.Add(new DIMap(node, Utils.getSlaveTypes(slaveID)));
                    }
                }
                AddMap2SlaveButton(Int32.Parse(slaveNum), slaveID);

                //Namrata: 24/02/2018
                //If Description attribute not exist in XML 
                sdimList.AsEnumerable().ToList().ForEach(item =>
                {
                    string strDONo = item.DINo;
                    item.Description = Utils.DIlistforDescription.AsEnumerable().Where(x => x.DINo == strDONo).Select(x => x.Description).FirstOrDefault();
                });
                refreshMapList(sdimList);
                currentSlave = slaveID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteSlave(string slaveID)
        {
            string strRoutineName = "DeleteSlave";
            try
            {
                slavesDIMapList.Remove(slaveID);
                RadioButton rb = null;
                foreach (Control ctrl in ucdi.flpMap2Slave.Controls)
                {
                    if (ctrl.Tag.ToString() == slaveID)
                    {
                        rb = (RadioButton)ctrl;
                        break;
                    }
                }
                if (rb != null) ucdi.flpMap2Slave.Controls.Remove(rb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckIEC104SlaveStatusChanges()
        {
            string strRoutineName = "CheckIEC104SlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (IEC104Slave slv104 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC104Group().getIEC104Slaves())//Loop thru slaves...
                {
                    string slaveID = "IEC104_" + slv104.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DIMap>> sn in slavesDIMapList)
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
                foreach (KeyValuePair<string, List<DIMap>> sdin in slavesDIMapList)//Loop thru slaves...
                {
                    string slaveID = sdin.Key;
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
                    if (ucdi.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdi.lvDIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata:13/7/2017
        private void CheckIEC101SlaveStatusChanges()
        {
            string strRoutineName = "CheckIEC101SlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (IEC101Slave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
                {
                    string slaveID = "IEC101Slave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DIMap>> sn in slavesDIMapList)
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
                foreach (KeyValuePair<string, List<DIMap>> sain in slavesDIMapList)//Loop thru slaves...
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
                    if (ucdi.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdi.lvDIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckMODBUSSlaveStatusChanges()
        {
            string strRoutineName = "CheckMODBUSSlaveStatusChanges";
            try
            {
                //Check for slave addition...
                foreach (MODBUSSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())//Loop thru slaves...
                {
                    string slaveID = "MODBUSSlave_" + slvMB.SlaveNum;
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DIMap>> sn in slavesDIMapList)
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
                foreach (KeyValuePair<string, List<DIMap>> sdin in slavesDIMapList)//Loop thru slaves...
                {
                    string slaveID = sdin.Key;
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
                    if (ucdi.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdi.lvDIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckIEc61850SlaveStatusChanges()
        {
            string strRoutineName = "CheckIEc61850SlaveStatusChanges";
            try
            {
                Console.WriteLine("*** CheckMODBUSSlaveStatusChanges");
                //Check for slave addition...
                foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())//Loop thru slaves...
                {
                    string slaveID = "IEC61850Server_" + slvMB.SlaveNum; //"61850ServerSlaveGroup_"
                    bool slaveAdded = true;
                    foreach (KeyValuePair<string, List<DIMap>> sn in slavesDIMapList)
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
                foreach (KeyValuePair<string, List<DIMap>> sdin in slavesDIMapList)//Loop thru slaves...
                {
                    string slaveID = sdin.Key;
                    bool slaveDeleted = true;
                    if (Utils.getSlaveTypes(slaveID) != SlaveTypes.IEC61850Server) continue;
                    foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())
                    {
                        if (slaveID == "IEC61850Server_" + slvMB.SlaveNum) //61850ServerSlaveGroup_
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
                    if (ucdi.flpMap2Slave.Controls.Count > 0)
                    {
                        ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Checked = true;
                        currentSlave = ((RadioButton)ucdi.flpMap2Slave.Controls[0]).Tag.ToString();
                        refreshCurrentMapList();
                    }
                    else
                    {
                        ucdi.lvDIMap.Items.Clear();
                        currentSlave = "";
                    }
                }
                fillMapOptions(Utils.getSlaveTypes(currentSlave));
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void deleteDIFromMaps(string diNo)
        {
            string strRoutineName = "deleteDIFromMaps";
            try
            {
                foreach (KeyValuePair<string, List<DIMap>> sdin in slavesDIMapList)//Loop thru slaves...
                {
                    List<DIMap> delEntry = sdin.Value;
                    foreach (DIMap dimn in delEntry)
                    {
                        if (dimn.DINo == diNo)
                        {
                            delEntry.Remove(dimn);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //--------------------------//
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC101SLAVE)
                    rb.Text = "IEC101 " + slaveNum;
                else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC61850Server)
                    rb.Text = "IEC61850 " + slaveNum;
                else
                    rb.Text = "Unknown " + slaveNum;
                rb.Padding = new Padding(0, 0, 0, 0);
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.MODBUSSLAVE)
                    rb.Text = "MODBUS " + slaveNum;
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC101SLAVE)
                    rb.Text = "IEC101 " + slaveNum;
                if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC61850Server)
                    rb.Text = "IEC61850 " + slaveNum;
                //End Namrata Code
                rb.TextAlign = ContentAlignment.TopCenter;
                rb.BackColor = ColorTranslator.FromHtml("#f2f2f2");
                rb.Appearance = Appearance.Button;
                rb.AutoSize = true;
                rb.Image = Properties.Resources.SlaveRadioButton;
                rb.Click += rbGrpMap2Slave_Click;
                ucdi.flpMap2Slave.Controls.Add(rb);
                rb.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void EnableMappingEvents()
        {
            string strRoutineName = "EnableMappingEvents";
            try
            {
                ucdi.grpDIMap.Location = new Point(450, 371);//new Point(450, 325);
                ucdi.grpDIMap.Size = new Size(300, 260);
                ucdi.pbDIMHdr.Width = 300;
                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(122, 30);
                ucdi.txtDIMNo.Size = new Size(150, 20);

                ucdi.lblDMRI.Visible = true;
                ucdi.txtDIMReportingIndex.Visible = true;
                ucdi.lblDMRI.Location = new Point(12, 55);
                ucdi.txtDIMReportingIndex.Location = new Point(121, 55);
                ucdi.txtDIMReportingIndex.Size = new Size(150, 20);

                ucdi.lbl61850reporting.Visible = false;
                ucdi.CmbReportingIndex.Visible = false;

                ucdi.lblDMDT.Location = new Point(12, 81);
                ucdi.cmbDIMDataType.Location = new Point(121, 81);
                ucdi.cmbDIMDataType.Size = new Size(150, 20);

                ucdi.lblDMCT.Location = new Point(12, 108);
                ucdi.cmbDIMCommandType.Location = new Point(121, 108);
                ucdi.cmbDIMCommandType.Size = new Size(150, 20);


                ucdi.lblDMBP.Location = new Point(12, 134);
                ucdi.txtDIMBitPos.Location = new Point(121, 134);
                ucdi.txtDIMBitPos.Size = new Size(150, 20);

                ucdi.lblMapdesc.Location = new Point(12, 161);
                ucdi.txtMapDescription.Location = new Point(121, 161);
                ucdi.txtMapDescription.Size = new Size(150, 20);

                ucdi.lblMapAutoMapRange.Visible = true;
                ucdi.txtDIAutoMap.Visible = true;
                ucdi.lblMapAutoMapRange.Location = new Point(12, 187);
                ucdi.txtDIAutoMap.Location = new Point(121, 187);
                ucdi.txtDIAutoMap.Size = new Size(150, 20);

                ucdi.chkComplement.Location = new Point(13, 215);

                ucdi.btnDIMDone.Location = new Point(120, 215);
                ucdi.btnDIMCancel.Location = new Point(200, 215);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void EnableMappingEventsOnDoubleClick()
        {
            string strRoutineName = "EnableMappingEventsOnDoubleClick";
            try
            {
                ucdi.lblMapAutoMapRange.Visible = false;
                ucdi.txtDIAutoMap.Visible = false;
                ucdi.grpDIMap.Location = new Point(450, 352);
                ucdi.grpDIMap.Size = new Size(300, 260);

                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(122, 30);
                ucdi.txtDIMNo.Size = new Size(139, 20);

                ucdi.lblDMRI.Visible = true;
                ucdi.txtDIMReportingIndex.Visible = true;
                ucdi.lblDMRI.Location = new Point(12, 55);
                ucdi.txtDIMReportingIndex.Location = new Point(121, 55);
                ucdi.txtDIMReportingIndex.Size = new Size(139, 20);

                ucdi.lbl61850reporting.Visible = false;
                ucdi.CmbReportingIndex.Visible = false;

                ucdi.lblDMDT.Location = new Point(12, 81);
                ucdi.cmbDIMDataType.Location = new Point(121, 81);
                ucdi.cmbDIMDataType.Size = new Size(139, 20);

                ucdi.lblDMCT.Location = new Point(12, 108);
                ucdi.cmbDIMCommandType.Location = new Point(121, 108);
                ucdi.cmbDIMCommandType.Size = new Size(139, 20);
                ucdi.lblDMBP.Location = new Point(12, 134);
                ucdi.txtDIMBitPos.Location = new Point(121, 134);
                ucdi.txtDIMBitPos.Size = new Size(139, 20);
                ucdi.lblMapdesc.Location = new Point(12, 161);
                ucdi.txtMapDescription.Location = new Point(121, 161);
                ucdi.txtMapDescription.Size = new Size(139, 20);
                ucdi.chkComplement.Location = new Point(13, 187);
                ucdi.btnDIMDone.Location = new Point(120, 188);
                ucdi.btnDIMCancel.Location = new Point(200, 188);
                ucdi.grpDIMap.Size = new Size(306, 230);
                ucdi.grpDIMap.Location = new Point(500, 380);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void IEC61850Events()
        {
            string strRoutineName = "IEC61850Events";
            try
            {
                ucdi.lblMapAutoMapRange.Visible = true;
                ucdi.txtDIAutoMap.Visible = true;
                ucdi.pbDIMHdr.Size = new Size(405, 22);
                ucdi.grpDIMap.Size = new Size(405, 255);
                ucdi.grpDIMap.Location = new Point(400, 340);

                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(121, 30);

                ucdi.lblDMRI.Visible = false;
                ucdi.txtDIMReportingIndex.Visible = false;

                ucdi.lbl61850reporting.Visible = true;
                ucdi.CmbReportingIndex.Visible = true;
                ucdi.lbl61850reporting.Location = new Point(12, 55);
                ucdi.CmbReportingIndex.Location = new Point(121, 55);
                ucdi.txtDIMNo.Size = new Size(270, 21);
                ucdi.CmbReportingIndex.Size = new Size(270, 21);

                ucdi.lblDMDT.Location = new Point(12, 81);
                ucdi.cmbDIMDataType.Location = new Point(121, 81);
                ucdi.cmbDIMDataType.Size = new Size(270, 21);

                ucdi.lblDMCT.Location = new Point(12, 108);
                ucdi.cmbDIMCommandType.Location = new Point(121, 108);
                ucdi.cmbDIMCommandType.Size = new Size(270, 21);


                ucdi.lblDMBP.Location = new Point(12, 134);
                ucdi.txtDIMBitPos.Location = new Point(121, 134);
                ucdi.txtDIMBitPos.Size = new Size(270, 21);

                ucdi.lblMapdesc.Location = new Point(11, 161);
                ucdi.txtMapDescription.Location = new Point(121, 161);
                ucdi.txtMapDescription.Size = new Size(270, 21);

                ucdi.lblMapAutoMapRange.Location = new Point(11, 187);
                ucdi.txtDIAutoMap.Location = new Point(121, 187);
                ucdi.txtDIAutoMap.Size = new Size(270, 21);

                ucdi.chkComplement.Location = new Point(12, 215);

                ucdi.btnDIMDone.Location = new Point(170, 217);
                ucdi.btnDIMCancel.Location = new Point(250, 217);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EnableMappingEventsOnDoubleClickIEC61850()
        {
            string strRoutineName = "IEC61850Events";
            try
            {
                ucdi.lblMapAutoMapRange.Visible = false;
                ucdi.txtDIAutoMap.Visible = false;
                ucdi.pbDIMHdr.Size = new Size(405, 22);
                ucdi.grpDIMap.Size = new Size(405, 270);
                ucdi.grpDIMap.Location = new Point(400, 340);

                ucdi.lblDNM.Location = new Point(12, 30);
                ucdi.txtDIMNo.Location = new Point(121, 30);

                ucdi.lblDMRI.Visible = false;
                ucdi.txtDIMReportingIndex.Visible = false;

                ucdi.lbl61850reporting.Visible = true;
                ucdi.CmbReportingIndex.Visible = true;
                ucdi.lbl61850reporting.Location = new Point(12, 55);
                ucdi.CmbReportingIndex.Location = new Point(121, 55);
                ucdi.txtDIMNo.Size = new Size(270, 21);
                ucdi.CmbReportingIndex.Size = new Size(270, 21);

                ucdi.lblDMDT.Location = new Point(12, 81);
                ucdi.cmbDIMDataType.Location = new Point(121, 81);
                ucdi.cmbDIMDataType.Size = new Size(270, 21);

                ucdi.lblDMCT.Location = new Point(12, 108);
                ucdi.cmbDIMCommandType.Location = new Point(121, 108);
                ucdi.cmbDIMCommandType.Size = new Size(270, 21);


                ucdi.lblDMBP.Location = new Point(12, 134);
                ucdi.txtDIMBitPos.Location = new Point(121, 134);
                ucdi.txtDIMBitPos.Size = new Size(270, 21);

                ucdi.lblMapdesc.Location = new Point(11, 161);
                ucdi.txtMapDescription.Location = new Point(121, 161);
                ucdi.txtMapDescription.Size = new Size(270, 21);

                ucdi.chkComplement.Location = new Point(12, 187);

                ucdi.btnDIMDone.Location = new Point(170, 190);
                ucdi.btnDIMCancel.Location = new Point(270, 190);
                ucdi.grpDIMap.Size = new Size(400, 240);
                ucdi.grpDIMap.Location = new Point(450, 370);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void rbGrpMap2Slave_Click(object sender, EventArgs e)
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
                if (ucdi.lvDIlist.SelectedItems.Count > 0)
                {
                    //If possible highlight the map for new slave selected...
                    //Remove selection from DIMap...
                    ucdi.lvDIMap.SelectedItems.Clear();
                    Utils.highlightListviewItem(ucdi.lvDIlist.SelectedItems[0].Text, ucdi.lvDIMap);
                }
            }
            ShowHideSlaveColumns();
            if (rbChanged && ucdi.lvDIlist.CheckedItems.Count <= 0) return;//We supported map listing only
            DI mapDI = null;
            if (ucdi.lvDIlist.CheckedItems.Count != 1)
            {
                MessageBox.Show("Select single DI element to map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int i = 0; i < ucdi.lvDIlist.Items.Count; i++)
            {
                if (ucdi.lvDIlist.Items[i].Checked)
                {
                    Console.WriteLine("*** Mapping index: {0}", i);
                    mapDI = diList.ElementAt(i);
                    ucdi.lvDIlist.Items[i].Checked = false;//Now we can uncheck in listview...
                    break;
                }
            }
            if (mapDI == null) return;
            bool alreadyMapped = false;  //Search if already mapped for current slave...
            List<DIMap> slaveDIMapList;
            if (!slavesDIMapList.TryGetValue(currentSlave, out slaveDIMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Slave entry doesnot exist!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Console.WriteLine("##### Slave entries exists");
            }
            //Namrata Commented: 28/04/2018
            //foreach (DIMap sdim in slaveDIMapList)
            //{
            //    if (sdim.DINo == mapDI.DINo)
            //    {
            //        Console.WriteLine("##### Hoorray, already mapped...");
            //        MessageBox.Show("DI already mapped!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        alreadyMapped = true;
            //        break;
            //    }
            //}
            if (!alreadyMapped)
            {
                mapMode = Mode.ADD;
                mapEditIndex = -1;
                Utils.resetValues(ucdi.grpDIMap);
                ucdi.txtDIMNo.Text = mapDI.DINo;
                ucdi.txtMapDescription.Text = getDescription(ucdi.lvDIlist, mapDI.DINo.ToString());
                //Namrata:1/7/2017
                ucdi.txtDIAutoMap.Text = "1";
                loadMapDefaults();
                //Namrata: 4/11/2017
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                {
                    //Namrata: 4/11/2017
                    Utils.CurrentSlaveFinal = currentSlave;
                    ucdi.CmbReportingIndex.DataSource = null;
                    ucdi.CmbReportingIndex.Items.Clear();
                    ucdi.CmbReportingIndex.DataSource = Utils.DSDISlave.Tables[Utils.CurrentSlaveFinal];
                    ucdi.CmbReportingIndex.DisplayMember = "ObjectReferrence";
                    IEC61850Events();
                }
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
                {
                    EnableMappingEvents(); ucdi.cmbDIMCommandType.Enabled = true;
                }
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
                {
                    EnableMappingEvents(); ucdi.cmbDIMCommandType.Enabled = false;
                }
                //if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) ucdi.cmbDIMCommandType.Enabled = true;
                //else ucdi.cmbDIMCommandType.Enabled = false;
                ucdi.grpDIMap.Visible = true;
                ucdi.txtDIMReportingIndex.Focus();
            }
        }
        private void btnDIMDelete_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucdi btnDIMDelete_Click clicked in class!!!");
            DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                return;
            }
            List<DIMap> slaveDIMapList;
            if (!slavesDIMapList.TryGetValue(currentSlave, out slaveDIMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error deleting DI map!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = ucdi.lvDIMap.Items.Count - 1; i >= 0; i--)
            {
                if (ucdi.lvDIMap.Items[i].Checked)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    slaveDIMapList.RemoveAt(i);
                    ucdi.lvDIMap.Items[i].Remove();
                }
            }
            Console.WriteLine("*** slaveDIMapList count: {0} lv count: {1}", slaveDIMapList.Count, ucdi.lvDIMap.Items.Count);
            refreshMapList(slaveDIMapList);
        }
        private void btnDIMDone_Click(object sender, EventArgs e)
        {
            //if (!ValidateMap()) return;
            Console.WriteLine("*** ucdi btnDIMDone_Click clicked in class!!!");
            if (!slavesDIMapList.TryGetValue(currentSlave, out slaveDIMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error adding DI map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<KeyValuePair<string, string>> dimData = Utils.getKeyValueAttributes(ucdi.grpDIMap);
            if (mapMode == Mode.ADD)
            {
                int intStart = Convert.ToInt32(dimData[6].Value); // DINo
                int intRange = Convert.ToInt32(dimData[2].Value); //AutoMapRange
                int intDIIndex = Convert.ToInt32(dimData[8].Value); // DIReportingIndex
                int DINumber1 = 0, DIINdex1 = 0;
                string DIDescription = "";
                //Namrata:8/7/2017
                //Find Index Of ListView
                ListViewItem item = ucdi.lvDIlist.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Text == intStart.ToString());
                string ind = ucdi.lvDIlist.Items.IndexOf(item).ToString();
                //Namrata:31/7/2017
                //Eliminate Duplicate Record From lvAIMAp List
                ListViewItem ExistDIMap = ucdi.lvDIMap.FindItemWithText(ucdi.txtDIMNo.Text);

                if (diList.Count >= 0)
                {
                    //Namrata:31/7/2017
                    //if (ExistDIMap != null)
                    //{
                    //    MessageBox.Show("Map Entry Already Exist In " + currentSlave.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    //else
                    //{
                    if ((intRange + Convert.ToInt16(ind)) > ucdi.lvDIlist.Items.Count)
                    {
                        MessageBox.Show("Slave Entry Does Not Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        //Ajay: 21/11/2017
                        for (int i = intStart; i <= intStart + intRange - 1; i++)
                        {
                            //Ajay: 21/11/2017
                            if (diList.Select(x => x.DINo).ToList().Contains(i.ToString()))
                            {
                                DINumber1 = i;
                                DIINdex1 = intDIIndex++;
                                ucdi.txtMapDescription.Text = getDescription(ucdi.lvDIlist, DINumber1.ToString());
                                DIDescription = ucdi.txtMapDescription.Text;

                                DIMap NewDIMap = new DIMap("DI", dimData, Utils.getSlaveTypes(currentSlave));
                                NewDIMap.DINo = DINumber1.ToString();
                                NewDIMap.ReportingIndex = DIINdex1.ToString();
                                NewDIMap.Description = DIDescription;
                                slaveDIMapList.Add(NewDIMap);
                            }
                            else
                            {
                                intRange++;
                            }
                        }
                    }
                    //}
                }
            }
            else if (mapMode == Mode.EDIT)
            {
                slaveDIMapList[mapEditIndex].updateAttributes(dimData);
            }
            refreshMapList(slaveDIMapList);
            ucdi.grpDIMap.Visible = false;
            mapMode = Mode.NONE;
            mapEditIndex = -1;
        }
        private void btnDIMCancel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucdi btnDIMCancel_Click clicked in class!!!");
            ucdi.grpDIMap.Visible = false;
            mapMode = Mode.NONE;
            mapEditIndex = -1;
            Utils.resetValues(ucdi.grpDIMap);
        }
        private void lvDIMap_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucdi lvDIMap_DoubleClick clicked in class!!!");
            List<DIMap> slaveDIMapList;
            if (!slavesDIMapList.TryGetValue(currentSlave, out slaveDIMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error editing DI map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //if (ucdi.lvDIMap.SelectedItems.Count <= 0) return;
            //ListViewItem lvi = ucdi.lvDIMap.SelectedItems[0];

            //Namrata: 07/03/2018
            int ListIndex = ucdi.lvDIMap.FocusedItem.Index;
            ListViewItem lvi = ucdi.lvDIMap.Items[ListIndex];

            Utils.UncheckOthers(ucdi.lvDIMap, lvi.Index);
            if (slaveDIMapList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
            {
                EnableMappingEventsOnDoubleClick();
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server))
            {
                EnableMappingEventsOnDoubleClickIEC61850();
            }

            ucdi.txtDIAutoMap.Text = "0";
            ucdi.grpDIMap.Visible = true;
            mapMode = Mode.EDIT;
            mapEditIndex = lvi.Index;
            loadMapValues();
            ucdi.txtDIMReportingIndex.Focus();
        }
        private void loadMapDefaults()
        {
            ucdi.txtDIMReportingIndex.Text = (Globals.DIReportingIndex + 1).ToString();
            ucdi.txtDIMBitPos.Text = "0";
        }
        private void loadMapValues()
        {
            List<DIMap> slaveDIMapList;
            if (!slavesDIMapList.TryGetValue(currentSlave, out slaveDIMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error loading DI map data for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DIMap dimn = slaveDIMapList.ElementAt(mapEditIndex);
            if (dimn != null)
            {
                ucdi.txtDIMNo.Text = dimn.DINo;
                ucdi.txtDIMReportingIndex.Text = dimn.ReportingIndex;
                ucdi.cmbDIMDataType.SelectedIndex = ucdi.cmbDIMDataType.FindStringExact(dimn.DataType);
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                {
                    ucdi.cmbDIMCommandType.SelectedIndex = ucdi.cmbDIMCommandType.FindStringExact(dimn.CommandType);
                    ucdi.cmbDIMCommandType.Enabled = true;
                }
                else
                {
                    ucdi.cmbDIMCommandType.Enabled = false;
                }
                ucdi.txtDIMBitPos.Text = dimn.BitPos;
                //Namrata: 18/11/2017
                ucdi.txtMapDescription.Text = dimn.Description;
                if (dimn.Complement.ToLower() == "yes") ucdi.chkComplement.Checked = true;
                else ucdi.chkComplement.Checked = false;
            }
        }
        private bool ValidateMap()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucdi.grpDIMap))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void refreshMapList(List<DIMap> tmpList)
        {
            int cnt = 0;
            ucdi.lvDIMap.Items.Clear();

            ucdi.lblDIMRecords.Text = "0";
            if (tmpList == null) return;
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE))
            {
                foreach (DIMap dimp in tmpList)
                {
                    string[] row = new string[7];
                    if (dimp.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = dimp.DINo;
                        row[1] = dimp.ReportingIndex;
                        row[2] = dimp.DataType;
                        row[3] = dimp.CommandType;
                        row[4] = dimp.BitPos;
                        row[5] = dimp.Complement;
                        row[6] = dimp.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucdi.lvDIMap.Items.Add(lvItem);
                }
            }
            if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
            {
                foreach (DIMap dimp in tmpList)
                {
                    string[] row = new string[7];
                    if (dimp.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = dimp.DINo;
                        row[1] = dimp.IEC61850ReportingIndex;
                        row[2] = dimp.DataType;
                        row[3] = dimp.CommandType;
                        row[4] = dimp.BitPos;
                        row[5] = dimp.Complement;
                        row[6] = dimp.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucdi.lvDIMap.Items.Add(lvItem);
                }
            }
            ucdi.lblDIMRecords.Text = tmpList.Count.ToString();
        }
        private void refreshCurrentMapList()
        {
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
            //List<DIMap> sdimList; //Ajay: 08/08/2018 commented
            //Ajay: 08/08/2018 if condition commented
            //Namrata:02/05/2018
            //if (slaveDIMapList != null)
            //{
            //    slaveDIMapList = slaveDIMapList.OrderBy(x => Convert.ToInt32(x.DINo)).ToList();
            //}
            //if (!slavesDIMapList.TryGetValue(currentSlave, out sdimList))   //Ajay: 08/08/2018 commented
            if (!slavesDIMapList.TryGetValue(currentSlave, out slaveDIMapList))  //Ajay: 08/08/2018
            {
                refreshMapList(null);
            }
            else
            {
                //Namrata:02/05/2018
                //Namrata:17/05/2018
                //if(sdimList.Count > 0 && slaveDIMapList !=null)  //Ajay: 08/08/2018 if condition commented
                if (slaveDIMapList != null && slaveDIMapList.Count > 0) //Ajay: 08/08/2018 
                {
                    //sdimList = slaveDIMapList.OrderBy(x => Convert.ToInt32(x.DINo)).ToList(); //Ajay: 08/08/2018 commented
                    //slaveDIMapList = sdimList; //Namrata:15/05/2018 //Ajay: 08/08/2018 commented
                    slaveDIMapList = slaveDIMapList.OrderBy(x => Convert.ToInt32(x.DINo)).ToList();
                }
                //refreshMapList(sdimList); //Ajay: 08/08/2018 commented
                refreshMapList(slaveDIMapList); //Ajay: 08/08/2018
            }
        }
        /* ============================================= Above this, DI Map logic... ============================================= */
        private void fillOptions()
        {
            string strRoutineName = "DI : fillOptions";
            try
            {
                DataColumn dcAddressColumn = dtdataset.Columns.Add("Address", typeof(string));
                dtdataset.Columns.Add("IED", typeof(string));

                //Fill IED Name
                ucdi.cmbIEDName.Items.Clear();
                //Namrata: 31/10/2017
                ucdi.cmbIEDName.DataSource = Utils.dsIED.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//Utils.DtIEDName;
                ucdi.cmbIEDName.DisplayMember = "IEDName";
                //Namrata: 04/04/2018
                if (Utils.Iec61850IEDname != "")
                {
                    ucdi.cmbIEDName.Text = Utils.Iec61850IEDname;
                }
                //Namrata: 15/9/2017
                //Fill ResponseType For IEC61850Client
                ucdi.cmb61850DIResponseType.Items.Clear();
                //Namrata: 31/10/2017
                ucdi.cmb61850DIResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];
                ucdi.cmb61850DIResponseType.DisplayMember = "Address";
                //Fill Response Type...
                if (masterType == MasterTypes.IEC61850Client)
                {
                    ucdi.cmbResponseType.Items.Clear();
                }
                else
                {
                    ucdi.cmbResponseType.Items.Clear();
                    foreach (String rt in DI.getResponseTypes(masterType))
                    {
                        ucdi.cmbResponseType.Items.Add(rt.ToString());
                    }
                    ucdi.cmbResponseType.SelectedIndex = 0;
                }
                //Fill Data Type...
                if (masterType == MasterTypes.IEC61850Client)
                {
                    ucdi.CmbDataType.Items.Clear();
                }
                else
                {
                    ucdi.CmbDataType.Items.Clear();
                    foreach (String dt in DI.getDataTypes(masterType))
                    {
                        ucdi.CmbDataType.Items.Add(dt.ToString());
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //ucdi.CmbDataType.SelectedIndex = 0;
        }

        private void fillMapOptions(SlaveTypes sType)
        {
            /***** Fill Map details related combobox ******/
            try
            {
                //Fill Data Type...
                ucdi.cmbDIMDataType.Items.Clear();
                foreach (String dt in DIMap.getDataTypes(sType))
                {
                    ucdi.cmbDIMDataType.Items.Add(dt.ToString());
                }
                if (ucdi.cmbDIMDataType.Items.Count > 0) ucdi.cmbDIMDataType.SelectedIndex = 0;
            }
            catch (System.NullReferenceException)
            {
                Utils.WriteLine(VerboseLevel.ERROR, "DI Map DataType does not exist for Slave Type: {0}", sType.ToString());
            }
            try
            {
                //Fill Command Type...
                ucdi.cmbDIMCommandType.Items.Clear();
                foreach (String ct in DIMap.getCommandTypes(sType))
                {
                    ucdi.cmbDIMCommandType.Items.Add(ct.ToString());
                }
                if (ucdi.cmbDIMCommandType.Items.Count > 0) ucdi.cmbDIMCommandType.SelectedIndex = 0;
            }
            catch (System.NullReferenceException)
            {
                Utils.WriteLine(VerboseLevel.ERROR, "DI Map CommandType does not exist for Slave Type: {0}", sType.ToString());
            }
        }

        private void addListHeaders()
        {
            //Namrata: 12/09/2017
            if (masterType == MasterTypes.IEC61850Client)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("IEDName", 100, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Response Type", 210, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Index", 320, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("FC", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Sub Index", 100, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                //Namrata: 15/9/2017
                //Hide IED Name
                ucdi.lvDIlist.Columns[1].Width = 0;
            }
            else if (masterType == MasterTypes.MODBUS)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Sub Index", 120, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Data Type", 100, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            else if (masterType == MasterTypes.ADR)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Sub Index", 120, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Event_T", 100, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Event_F", 100, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            else if (masterType == MasterTypes.IEC101)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Sub Index", 120, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            else if (masterType == MasterTypes.IEC103)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Sub Index", 120, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            else if (masterType == MasterTypes.Virtual)
            {
                ucdi.lvDIlist.Columns.Add("DI No.", 70, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Sub Index", 120, HorizontalAlignment.Left);
                ucdi.lvDIlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
            }
            //Add DI map headers...
            ucdi.lvDIMap.Columns.Add("DI No.", "DI No.", 70, HorizontalAlignment.Left, null);
            ucdi.lvDIMap.Columns.Add("Reporting Index", "Reporting Index", 130, HorizontalAlignment.Left, null);
            ucdi.lvDIMap.Columns.Add("Data Type", "Data Type", 130, HorizontalAlignment.Left, null);
            ucdi.lvDIMap.Columns.Add("Command Type", "Command Type", 0, HorizontalAlignment.Left, null);
            ucdi.lvDIMap.Columns.Add("Bit Pos.", "Bit Pos.", 80, HorizontalAlignment.Left, null);
            ucdi.lvDIMap.Columns.Add("Complement", "Complement", -2, HorizontalAlignment.Left, null);
            ucdi.lvDIMap.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, null);
        }
        private void ShowHideSlaveColumns()
        {
            if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) Utils.getColumnHeader(ucdi.lvDIMap, "Command Type").Width = COL_CMD_TYPE_WIDTH;
            else Utils.getColumnHeader(ucdi.lvDIMap, "Command Type").Width = 0;//Hide...
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
            rootNode = xmlDoc.CreateElement("DIConfiguration");//rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            //Namrata: 17/11/2017
            if (masterType == MasterTypes.Virtual)
            {
                //var myDistinctList = ((diList.GroupBy(i => new { i.Index, i.SubIndex, i.ResponseType }).Select(g => g.First()).ToList())&&); //Distinct items in list
                var myDistinctList = diList.GroupBy(i => new { i.Index, i.SubIndex, i.ResponseType }).Select(g => g.First()).ToList(); //Distinct items in list
                foreach (DI ai in myDistinctList)
                {
                    XmlNode importNode = rootNode.OwnerDocument.ImportNode(ai.exportXMLnode(), true);
                    rootNode.AppendChild(importNode);
                }
            }
            else
            {
                foreach (DI di in diList)
                {
                    XmlNode importNode = rootNode.OwnerDocument.ImportNode(di.exportXMLnode(), true);
                    rootNode.AppendChild(importNode);
                }
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
            rootNode = xmlDoc.CreateElement("DIMap");
            xmlDoc.AppendChild(rootNode);
            List<DIMap> slaveDIMapList;
            if (!slavesDIMapList.TryGetValue(slaveID, out slaveDIMapList))
            {
                return rootNode;
            } 
            //Namarta:15/05/2018
            List<DIMap> sdimList = slaveDIMapList.OrderBy(x =>Convert.ToInt32( x.DINo)).ToList();
            slaveDIMapList = sdimList;
            var sortedPhotos = slaveDIMapList.OrderBy(x => x.DINo);
            foreach (DIMap dimn in slaveDIMapList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(dimn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            } 
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            List<DIMap> slaveDIMapList;
            if (!slavesDIMapList.TryGetValue(slaveID, out slaveDIMapList))
            {
                return iniData;
            }
            //IMP: If "DoublePoint", create only single IOA in .INI file...
            Dictionary<string, string> riList = new Dictionary<string, string>();
            foreach (DIMap dimn in slaveDIMapList)
            {
                int ri;
                try
                {
                    ri = Int32.Parse(dimn.ReportingIndex);
                }
                catch (System.FormatException)
                {
                    ri = 0;
                }

                if (dimn.DataType == "DoublePoint" && IsRIinINI(riList, dimn.ReportingIndex)) continue;
                if (!riList.ContainsKey(dimn.ReportingIndex)) //Ajay: 26/12/2018
                {
                    iniData += "DI_" + ctr++ + "=" + Utils.GenerateIndex("DI", Utils.GetDataTypeIndex(dimn.DataType), ri).ToString() + Environment.NewLine;
                    riList.Add(dimn.ReportingIndex, dimn.DataType);
                }
                //Ajay: 26/12/2018
                else
                {
                    MessageBox.Show("Duplicate Reporting Index (" + dimn.ReportingIndex + ") found of DI No " + dimn.DINo, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //iniData += "DI_" + ctr++ + "=" + Utils.GenerateIndex("DI", Utils.GetDataTypeIndex(dimn.DataType), ri).ToString() + Environment.NewLine;
            }

            return iniData;
        }
        private bool IsRIinINI(Dictionary<string, string> ril, string ri)
        {
            string tmp;
            if (!ril.TryGetValue(ri, out tmp)) return false;
            else if (tmp != "DoublePoint")
            {//Got ri, check if DoublePoint
                return false;
            }
            else return true;
        }
        public void changeIEC104Sequence(int oSlaveNo, int nSlaveNo)
        {
            if (oSlaveNo == nSlaveNo) return;
            Utils.ChangeKey(slavesDIMapList, "IEC104_" + oSlaveNo, "IEC104_" + nSlaveNo);
            //Change radio button Tag n Text...
            foreach (Control ctrl in ucdi.flpMap2Slave.Controls)
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
        public void changeMODBUSSlaveSequence(int oSlaveNo, int nSlaveNo)
        {
            if (oSlaveNo == nSlaveNo) return;
            Utils.ChangeKey(slavesDIMapList, "MODBUSSlave_" + oSlaveNo, "MODBUSSlave_" + nSlaveNo);
            //Change radio button Tag n Text...
            foreach (Control ctrl in ucdi.flpMap2Slave.Controls)
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
        public void changeIEC101laveSequence(int oSlaveNo, int nSlaveNo)
        {
            if (oSlaveNo == nSlaveNo) return;
            Utils.ChangeKey(slavesDIMapList, "IEC101Slave_" + oSlaveNo, "IEC101Slave_" + nSlaveNo);
            //Change radio button Tag n Text...
            foreach (Control ctrl in ucdi.flpMap2Slave.Controls)
            {
                RadioButton rb = (RadioButton)ctrl;
                if (rb.Tag.ToString() == "IEC101Slave_" + oSlaveNo)
                {
                    rb.Tag = "IEC101Slave_" + nSlaveNo;//Ex. 'MODBUSSlave_1'
                    rb.Text = "IEC101 " + nSlaveNo;
                    break;
                }
            }
            //Check currentSlave var...
            if (currentSlave == "IEC101Slave_" + oSlaveNo) currentSlave = "IEC101Slave_" + nSlaveNo;
        }
        public void changeCLASequence(int oCLANo, int nCLANo)
        {
            if (oCLANo == nCLANo) return;
            foreach (DI din in diList)
            {
                if (din.ResponseType == "CLA" && din.Index == oCLANo.ToString() && din.SubIndex == "0")
                {
                    din.Index = nCLANo.ToString();
                    if (din.Description == "CLA_" + oCLANo) din.Description = "CLA_" + nCLANo;
                    break;
                }
            }
        }
        public void changeProfileSequence(int oProfileNo, int nProfileNo)
        {
            if (oProfileNo == nProfileNo) return;
            foreach (DI din in diList)
            {
                if (din.ResponseType == "Profile" && din.Index == oProfileNo.ToString() && din.SubIndex == "0")
                {
                    din.Index = nProfileNo.ToString();
                    if (din.Description == "Profile_" + oProfileNo) din.Description = "Profile_" + nProfileNo;
                    break;
                }
            }
        }
        public void changeMDSequence(int oMDNo, int nMDNo)
        {
            if (oMDNo == nMDNo) return;
            foreach (DI din in diList)
            {
                if (din.ResponseType == "MDAlarm" && din.Index == oMDNo.ToString() && din.SubIndex == "0")
                {
                    din.Index = nMDNo.ToString();
                    if (din.Description == "MDAlarm_" + oMDNo) din.Description = "MDAlarm_" + nMDNo;
                    break;
                }
            }
        }
        public void changeDDSequence(int oDDNo, int nDDNo)
        {
            if (oDDNo == nDDNo) return;
            foreach (DI din in diList)
            {
                if (din.ResponseType == "DerivedDI" && din.Index == oDDNo.ToString() && din.SubIndex == "0")
                {
                    din.Index = nDDNo.ToString();
                    if (din.Description == "DerivedDI_" + oDDNo) din.Description = "DerivedDI_" + nDDNo;
                    break;
                }
            }
        }
        public void regenerateDISequencel()
        {
            Utils.IsAI = true;
            List<DIMap> sdimList = new List<DIMap>(); ;
            List<DIMap> OrignalList = new List<DIMap>();
            List<DIMap> ReIndexedList = new List<DIMap>();
            foreach (DI din in diList)
            {
                int oDINo = Int32.Parse(din.DINo);
                int nDINo = Globals.DINo++;
                din.DINo = nDINo.ToString();

                //Now change in map...
                foreach (KeyValuePair<string, List<DIMap>> maps in slavesDIMapList)
                {
                    if (OrignalList != null && OrignalList.Count <= 0)
                    {
                        OrignalList = maps.Value;
                    }
                    sdimList = maps.Value;
                    foreach (DIMap dim in sdimList)
                    {
                        if (dim.DINo == oDINo.ToString() && !dim.IsReindexed)
                        {
                            //Namrata:02/05/2018
                            List<DIMap> tmpaimap = OrignalList.Where(x => x.DINo == oDINo.ToString()).Select(x => x).ToList();
                            if (tmpaimap != null && tmpaimap.Count > 0)
                            {
                                tmpaimap.ForEach(itm =>
                                {
                                    itm.DINo = nDINo.ToString();
                                    itm.IsReindexed = true;
                                    ReIndexedList.Add(itm);
                                });
                            }
                            //dim.AINo = nDINo.ToString();
                            //dim.IsReindexed = true;
                            break;
                        }
                        if (dim.DINo != oDINo.ToString() && !dim.IsReindexed)
                        {
                            //Namrata:02/05/2018
                            List<DIMap> tmpaimap = OrignalList.Where(x => x.DINo == oDINo.ToString()).Select(x => x).ToList();
                            if (tmpaimap != null && tmpaimap.Count > 0)
                            {
                                tmpaimap.ForEach(itm =>
                                {
                                    itm.DINo = nDINo.ToString();
                                    itm.IsReindexed = true;
                                    ReIndexedList.Add(itm);
                                });
                            }
                            //dim.AINo = oDINo.ToString(); //nDINo.ToString();
                            //dim.IsReindexed = true;
                            break;
                        }
                    }
                }
                //Now change in Parameter Load nodes...
                Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeDISequence(oDINo, nDINo);
            }
            //Reset reindexing status, for next use...
            foreach (KeyValuePair<string, List<DIMap>> maps in slavesDIMapList)
            {
                sdimList = maps.Value; //List<AIMap> sdimList = maps.Value;
                foreach (DIMap dim in sdimList)
                {
                    dim.IsReindexed = false;
                }
            }
            //Namrata:02/05/2018
            sdimList = ReIndexedList;
            refreshList();
            refreshCurrentMapList();
        }
        public void regenerateDISequence12()
        {
            foreach (DI din in diList)
            {
                int oDINo = Int32.Parse(din.DINo);
                int nDINo = Globals.DINo++;
                din.DINo = nDINo.ToString();

                //Now change in map...
                foreach (KeyValuePair<string, List<DIMap>> maps in slavesDIMapList)
                {
                    List<DIMap> sdimList = maps.Value;
                    foreach (DIMap dim in sdimList)
                    {
                        if (dim.DINo == oDINo.ToString() && !dim.IsReindexed)
                        {
                            dim.DINo = nDINo.ToString();
                            dim.IsReindexed = true;
                            break;
                        }
                    }
                }

                //Now change in Parameter Load nodes...
                Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeDISequence(oDINo, nDINo);
            }
            //Reset reindexing status, for next use...
            foreach (KeyValuePair<string, List<DIMap>> maps in slavesDIMapList)
            {
                List<DIMap> sdimList = maps.Value;
                foreach (DIMap dim in sdimList)
                {
                    dim.IsReindexed = false;
                }
            }
            refreshList();
            refreshCurrentMapList();
        }
        public void regenerateDISequence()
        {
            foreach (DI din in diList)
            {
                int oDINo = Int32.Parse(din.DINo);
                int nDINo = Globals.DINo++;
                din.DINo = nDINo.ToString();

                //Now change in map...
                foreach (KeyValuePair<string, List<DIMap>> maps in slavesDIMapList)
                {
                    List<DIMap> sdimList = maps.Value;
                    foreach (DIMap dim in sdimList)
                    {
                        if (dim.DINo == oDINo.ToString() && !dim.IsReindexed)
                        {
                            //Ajay: 08/08/2018 if same AO mapped again it should take same AO no on reindex.
                            //dim.DINo = nDINo.ToString();
                            //dim.IsReindexed = true;
                            sdimList.Where(x => x.DINo == oDINo.ToString()).ToList().ForEach(x => { x.DINo = nDINo.ToString(); x.IsReindexed = true; });
                            break;
                        }
                        //if (dim.DINo != oDINo.ToString() && !dim.IsReindexed)
                        //{
                        //    dim.DINo = nDINo.ToString();
                        //    dim.IsReindexed = true;
                        //    break;
                        //}
                    }
                }
                //Now change in Parameter Load nodes...
                Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeDISequence(oDINo, nDINo);
            }
            //Reset reindexing status, for next use...
            foreach (KeyValuePair<string, List<DIMap>> maps in slavesDIMapList)
            {
                List<DIMap> sdimList = maps.Value;
                foreach (DIMap dim in sdimList)
                {
                    dim.IsReindexed = false;
                }
            }
            refreshList();
            refreshCurrentMapList();
        }
        public int GetReportingIndex(string slaveNum, string slaveID, int value)
        {
            int ret = 0;

            List<DIMap> slaveDIMapList;
            if (!slavesDIMapList.TryGetValue(slaveID, out slaveDIMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                return ret;
            }

            foreach (DIMap dim in slaveDIMapList)
            {
                if (dim.DINo == value.ToString()) return Int32.Parse(dim.ReportingIndex);
            }

            return ret;
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("DI_"))
            {
                //If a IEC104 slave added/deleted, reflect in UI as well as objects.
                CheckIEC104SlaveStatusChanges();
                //If a MODBUS slave added/deleted, reflect in UI as well as objects.
                CheckMODBUSSlaveStatusChanges();
                CheckIEc61850SlaveStatusChanges();
                //Namrata:13/7/2017
                //If a IEC101 slave added/deleted, reflect in UI as well as objects.
                CheckIEC101SlaveStatusChanges();
                ShowHideSlaveColumns();
                return ucdi;
            }
            return null;
        }
        public void parseDICNode(XmlNode dicNode, bool imported)
        {
            if (dicNode == null)
            {
                rnName = "DIConfiguration";
                return;
            }
            //First set root node name...
            rnName = dicNode.Name;
            if (dicNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = dicNode.Value;
            }
            foreach (XmlNode node in dicNode)
            {
                //Console.WriteLine("***** node type: {0}", node.NodeType);
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                diList.Add(new DI(node, masterType, masterNo, IEDNo, imported));
            }
            refreshList();
        }
        public void parseDIMNode(string slaveNum, string slaveID, XmlNode dimNode)
        {
            CreateNewSlave(slaveNum, slaveID, dimNode);
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (DI diNode in diList)
            {
                if (diNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<DI> getDIs()
        {
            return diList;
        }
        //Namrata:27/7/2017
        public int getDIMapCount()
        {
            int ctr = 0;
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
            List<DIMap> sdimList;
            if (!slavesDIMapList.TryGetValue(currentSlave, out sdimList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                refreshMapList(null);
            }
            else
            {
                refreshMapList(sdimList);
            }
            if (sdimList == null)
            {
                return 0;
            }
            else
            {
                foreach (DIMap asaa in sdimList)
                {
                    if (asaa.IsNodeComment) continue;
                    ctr++;
                }
            }

            return ctr;
        }
        public bool removeDI(string responseType, int Idx, int subIdx)
        {
            bool removed = false;
            for (int i = 0; i < diList.Count; i++)
            {
                if (diList[i].IsNodeComment) continue;
                DI tmp = diList[i];
                if (tmp.Index == Idx.ToString() && tmp.SubIndex == subIdx.ToString() && tmp.ResponseType == responseType)
                {
                    diList.RemoveAt(i);
                    removed = true;
                    break;
                }
            }
            return removed;
        }
        public List<DIMap> getSlaveDIMaps(string slaveID)
        {
            List<DIMap> slaveDIMapList;
            slavesDIMapList.TryGetValue(slaveID, out slaveDIMapList);
            return slaveDIMapList;
        }

        public void EnableEventsVirtualOnLoad()
        {
            ucdi.grpDI.Location = new Point(172, 52);
            ucdi.txtAutpMapNumber.Enabled = true;
            ucdi.lblAutoMapNumber.Enabled = true;
            ucdi.txtAutpMapNumber.Visible = true;
            ucdi.lblAutoMapNumber.Visible = true;
            //Namrata: 12/09/2017
            ucdi.txtDescription.Location = new Point(110, 131);
            ucdi.lblDesc.Location = new Point(15, 134);
            ucdi.label1.Visible = false;
            ucdi.CmbDataType.Visible = false;
            ucdi.label2.Visible = false;
            ucdi.label3.Visible = false;
            ucdi.label4.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.cmbIEDName.Visible = false;
            ucdi.cmb61850DIIndex.Visible = false;
            ucdi.cmb61850DIResponseType.Visible = false;
            ucdi.lblEventTrue.Visible = false;
            ucdi.lblEventFalse.Visible = false;
            ucdi.txtEvent_T.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.txtAutpMapNumber.Location = new Point(110, 157);
            ucdi.lblAutoMapNumber.Location = new Point(15, 160);
            ucdi.btnDone.Location = new Point(110, 185);
            ucdi.btnCancel.Location = new Point(214, 185);
            ucdi.btnFirst.Location = new Point(15, 220);
            ucdi.btnPrev.Location = new Point(87, 220);
            ucdi.btnNext.Location = new Point(189, 220);
            ucdi.btnLast.Location = new Point(231, 220);
            ucdi.grpDI.Location = new Point(250, 53);
            ucdi.grpDI.Height = 225;
            ucdi.lvDIlistDoubleClick += new System.EventHandler(this.lvDIlist_DoubleClick);
        }
        public void EnableEventsADRLoad()
        {
            ucdi.grpDI.Location = new Point(172, 52);
            ucdi.txtAutpMapNumber.Enabled = true;
            ucdi.lblAutoMapNumber.Enabled = true;
            ucdi.txtAutpMapNumber.Visible = true;
            ucdi.lblAutoMapNumber.Visible = true;
           
            ucdi.CmbDataType.Visible = false;
            ucdi.label1.Visible = false;
            ucdi.lblDN.Location = new Point(12, 33);
            ucdi.lblRT.Location = new Point(12, 58);
            ucdi.lblIdx.Location = new Point(12, 84);
            ucdi.lblSIdx.Location = new Point(12, 109); 
            ucdi.lblEventTrue.Location = new Point(12, 135);
            ucdi.txtEvent_T.Location = new Point(110, 135);
            ucdi.lblEventFalse.Location = new Point(12, 162);
            ucdi.txtEvent_F.Location = new Point(110, 162);
            ucdi.lblDesc.Location = new Point(12, 188);
            ucdi.txtDescription.Location = new Point(110, 188);
            ucdi.txtAutpMapNumber.Location = new Point(110, 214);
            ucdi.lblAutoMapNumber.Location = new Point(12, 214);
            ucdi.btnDone.Location = new Point(110, 242);
            ucdi.btnCancel.Location = new Point(212, 242);
            ucdi.btnFirst.Location = new Point(17, 271);
            ucdi.btnPrev.Location = new Point(88, 271);
            ucdi.btnNext.Location = new Point(167, 271);
            ucdi.btnLast.Location = new Point(238, 271);
            ucdi.grpDI.Size = new Size(320, 282);//ucdi.grpDI.Size = new Size(310,300);
            ucdi.pbHdr.Width = 320;
            ucdi.lvDIlistDoubleClick += new System.EventHandler(this.lvDIlist_DoubleClick);
            //Namrata: 12/09/2017
            ucdi.label2.Visible = false;
            ucdi.label3.Visible = false;
            ucdi.label4.Visible = false;
            ucdi.cmbIEDName.Visible = false;
            ucdi.cmb61850DIIndex.Visible = false;
            ucdi.cmb61850DIResponseType.Visible = false;
            //Namrata:27-04-2018
            ucdi.txtDINo.Size = new Size(190, 20);
            ucdi.cmbResponseType.Size = new Size(190, 20);
            ucdi.txtIndex.Size = new Size(190, 20);
            ucdi.txtSubIndex.Size = new Size(190, 20);
            ucdi.txtEvent_F.Size = new Size(190, 20);
            ucdi.txtEvent_T.Size = new Size(190, 20);
            ucdi.txtDescription.Size = new Size(190, 20);
            ucdi.txtAutpMapNumber.Size = new Size(190, 20);
        }
        public void EnableEventsMODBUSOnLoad()
        {
            ucdi.grpDI.Location = new Point(172, 52);
          
            ucdi.txtAutpMapNumber.Enabled = true;
            ucdi.lblAutoMapNumber.Enabled = true;
            ucdi.txtAutpMapNumber.Visible = true;
            ucdi.lblAutoMapNumber.Visible = true;
           
            ucdi.grpDI.Height = 250;// 280;
            ucdi.lblEventTrue.Visible = false;
            ucdi.lblEventFalse.Visible = false;
            ucdi.txtEvent_T.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.txtDescription.Location = new Point(110, 157);
            ucdi.lblDesc.Location = new Point(15, 160);
            ucdi.txtAutpMapNumber.Location = new Point(110, 182);
            ucdi.lblAutoMapNumber.Location = new Point(15, 185);
            ucdi.btnDone.Location = new Point(110, 210);
            ucdi.btnCancel.Location = new Point(214, 210);
            ucdi.btnFirst.Location = new Point(17, 210);
            ucdi.btnPrev.Location = new Point(88, 250);
            ucdi.btnNext.Location = new Point(167, 250);
            ucdi.btnLast.Location = new Point(238, 250);
            //Namrata: 12/09/2017
            ucdi.label2.Visible = false;
            ucdi.label3.Visible = false;
            ucdi.label4.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.cmbIEDName.Visible = false;
            ucdi.cmb61850DIIndex.Visible = false;
            ucdi.cmb61850DIResponseType.Visible = false;
            ucdi.lvDIlistDoubleClick += new System.EventHandler(this.lvDIlist_DoubleClick);
            //ucdi.grpDI.Size = new Size(340, 250);//ucdi.grpDI.Size = new Size(310,300);
        }
        public void EnableEventsIEC61850OnLoad()
        {
            ucdi.grpDI.Location = new Point(172, 52);
            ucdi.txtAutpMapNumber.Enabled = true;
            ucdi.lblAutoMapNumber.Enabled = true;
            ucdi.txtAutpMapNumber.Visible = true;
            ucdi.lblAutoMapNumber.Visible = true;
            ucdi.lblEventTrue.Visible = false;
            ucdi.txtEvent_T.Visible = false;
            ucdi.lblEventFalse.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.lblRT.Visible = false;
            ucdi.cmbResponseType.Visible = false;
            ucdi.lblIdx.Visible = false;
            ucdi.txtIndex.Visible = false;
            ucdi.lblDN.Location = new Point(15, 33);
            ucdi.txtDINo.Size = new Size(300, 21);

            //Namrata: 12/09/2017
            ucdi.label2.Location = new Point(15, 58);
            ucdi.cmbIEDName.Location = new Point(110, 55);
            ucdi.cmbIEDName.Size = new Size(300, 21);

            ucdi.label3.Location = new Point(15, 84);
            ucdi.cmb61850DIResponseType.Location = new Point(110, 81);
            ucdi.cmb61850DIResponseType.Size = new Size(300, 21);

            ucdi.label4.Location = new Point(15, 109);
            ucdi.cmb61850DIIndex.Size = new Size(300, 21);
            ucdi.cmb61850DIIndex.Location = new Point(110, 106);

            ucdi.lblFC.Location = new Point(15, 134);
            ucdi.txtFC.Location = new Point(110, 131);
            ucdi.txtFC.Size = new Size(300, 21);
            ucdi.txtFC.Enabled = false;

            ucdi.lblSIdx.Location = new Point(15, 160);
            ucdi.txtSubIndex.Location = new Point(110, 157);
            ucdi.txtSubIndex.Size = new Size(300, 21);
            ucdi.lblDesc.Location = new Point(15, 185);
            ucdi.txtDescription.Location = new Point(110, 182);
            ucdi.txtDescription.Size = new Size(300, 21);

            ucdi.lblAutoMapNumber.Location = new Point(15, 210);
            ucdi.txtAutpMapNumber.Location = new Point(110, 207);
            ucdi.txtAutpMapNumber.Size = new Size(300, 21);

            ucdi.btnDone.Location = new Point(170, 235);
            ucdi.btnCancel.Location = new Point(270, 235);
            
           
            ucdi.grpDI.Size = new Size(520, 270);
            ucdi.grpDI.Width = 430;
            ucdi.pbHdr.Width = 530;
            ucdi.CmbDataType.Visible = false;
            ucdi.label1.Visible = false;
            ucdi.lvDIlistDoubleClick += new System.EventHandler(this.lvDIlist_DoubleClick);
        }
        public void EnableEventsIEC101AndIEC103OnLoad()
        {
            ucdi.txtAutpMapNumber.Enabled = true;
            ucdi.lblAutoMapNumber.Enabled = true;
            ucdi.txtAutpMapNumber.Visible = true;
            ucdi.lblAutoMapNumber.Visible = true;
            ucdi.grpDI.Location = new Point(172, 52);
            ucdi.grpDI.Height = 230;
            ucdi.lblEventTrue.Visible = false;
            ucdi.lblEventFalse.Visible = false;
            ucdi.txtEvent_T.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.txtDescription.Location = new Point(110, 131);
            ucdi.lblDesc.Location = new Point(15, 134);
            ucdi.txtAutpMapNumber.Location = new Point(110, 157);
            ucdi.lblAutoMapNumber.Location = new Point(15, 160);
            ucdi.btnDone.Location = new Point(110, 185);
            ucdi.btnCancel.Location = new Point(214, 185);
            ucdi.btnFirst.Location = new Point(17, 220);
            ucdi.btnPrev.Location = new Point(88, 220);
            ucdi.btnNext.Location = new Point(167, 220);
            ucdi.btnLast.Location = new Point(238, 220);
            ucdi.lvDIlistDoubleClick += new System.EventHandler(this.lvDIlist_DoubleClick);
            //Namrata: 12/09/2017
            ucdi.label2.Visible = false;
            ucdi.label3.Visible = false;
            ucdi.label4.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.cmbIEDName.Visible = false;
            ucdi.cmb61850DIIndex.Visible = false;
            ucdi.cmb61850DIResponseType.Visible = false;
            ucdi.CmbDataType.Visible = false;
            ucdi.label1.Visible = false;
         
        }
        public void VisibleFalseEventsOnButtonClick()
        {
            ucdi.grpDI.Location = new Point(172, 52);
            ucdi.grpDI.Height = 220;
            ucdi.lblEventTrue.Visible = false;
            ucdi.lblEventFalse.Visible = false;
            ucdi.txtEvent_T.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.txtDescription.Location = new Point(110, 131);
            ucdi.lblDesc.Location = new Point(15, 134);
            ////ucdi.txtDescription.Location = new Point(110, 135);
            ////ucdi.lblDesc.Location = new Point(12, 135);
            ucdi.txtAutpMapNumber.Visible = false;
            ucdi.lblAutoMapNumber.Visible = false;
            ucdi.btnDone.Location = new Point(110, 160);
            ucdi.btnCancel.Location = new Point(212, 160);
            ucdi.btnFirst.Location = new Point(15, 190);
            ucdi.btnPrev.Location = new Point(87, 190);
            ucdi.btnNext.Location = new Point(159, 190);
            ucdi.btnLast.Location = new Point(231, 190);
            ucdi.lvDIlistDoubleClick += new System.EventHandler(this.lvDIlist_DoubleClick);
            //Namrata: 12/09/2017
            ucdi.label2.Visible = false;
            ucdi.label3.Visible = false;
            ucdi.label4.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.cmbIEDName.Visible = false;
            ucdi.cmb61850DIIndex.Visible = false;
            ucdi.cmb61850DIResponseType.Visible = false;
            ucdi.CmbDataType.Visible = false;
            ucdi.label1.Visible = false;
        }
        public void EnableDisbleonDoubleClickModbus()
        {
            ucdi.grpDI.Location = new Point(172, 52);
            ucdi.grpDI.Height = 245;
            ucdi.lblEventTrue.Visible = false;
            ucdi.lblEventFalse.Visible = false;
            ucdi.txtEvent_T.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.txtDescription.Location = new Point(110, 157);
            ucdi.lblDesc.Location = new Point(15, 160);
            ucdi.btnDone.Location = new Point(110, 185);
            ucdi.btnCancel.Location = new Point(212, 185);
            ucdi.btnFirst.Location = new Point(15, 215);
            ucdi.btnPrev.Location = new Point(87, 215);
            ucdi.btnNext.Location = new Point(159, 215);
            ucdi.btnLast.Location = new Point(231, 215);
            //ucdi.txtDescription.Location = new Point(110, 162);
            //ucdi.lblDesc.Location = new Point(12, 162);
            ucdi.txtAutpMapNumber.Visible = false;
            ucdi.lblAutoMapNumber.Visible = false;
            //ucdi.btnDone.Location = new Point(110, 190);
            //ucdi.btnCancel.Location = new Point(212, 190);
            //ucdi.btnFirst.Location = new Point(17, 220);
            //ucdi.btnPrev.Location = new Point(88, 220);
            //ucdi.btnNext.Location = new Point(167, 220);
            //ucdi.btnLast.Location = new Point(238, 220);
            //Namrata: 12/09/2017
            ucdi.label2.Visible = false;
            ucdi.label3.Visible = false;
            ucdi.label4.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.cmbIEDName.Visible = false;
            ucdi.cmb61850DIIndex.Visible = false;
            ucdi.cmb61850DIResponseType.Visible = false;
        }
        public void EnaleEventsOnDoubleClikADR()
        {
            ucdi.grpDI.Location = new Point(172, 52);
            ucdi.CmbDataType.Visible = false;
            ucdi.label1.Visible = false;

            //ucdi.lblDesc.Location = new Point(12, 135);
            //ucdi.txtDescription.Location = new Point(110, 135);
            //ucdi.lblEventTrue.Location = new Point(12, 162);
            //ucdi.txtEvent_T.Location = new Point(110, 162);
            //ucdi.lblEventFalse.Location = new Point(12, 188);
            //ucdi.txtEvent_F.Location = new Point(110, 188);

            ucdi.lblEventTrue.Location = new Point(12, 135);
            ucdi.txtEvent_T.Location = new Point(110, 135);

            ucdi.lblEventFalse.Location = new Point(12, 162);
            ucdi.txtEvent_F.Location = new Point(110, 162);

            ucdi.lblDesc.Location = new Point(12, 188);
            ucdi.txtDescription.Location = new Point(110, 188);


            ucdi.txtAutpMapNumber.Visible = false;
            ucdi.lblAutoMapNumber.Visible = false;
            ucdi.btnDone.Location = new Point(110, 215);
            ucdi.btnCancel.Location = new Point(212, 215);
            ucdi.btnFirst.Location = new Point(17, 245);
            ucdi.btnPrev.Location = new Point(88, 245);
            ucdi.btnNext.Location = new Point(167, 245);
            ucdi.btnLast.Location = new Point(238, 245);
            ucdi.grpDI.Size = new Size(310, 278);
            //Namrata: 12/09/2017
            ucdi.label2.Visible = false;
            ucdi.label3.Visible = false;
            ucdi.label4.Visible = false;
            ucdi.cmbIEDName.Visible = false;
            ucdi.cmb61850DIIndex.Visible = false;
            ucdi.cmb61850DIResponseType.Visible = false;
        }
        public void EnableEventVirtualDoubleClick()
        {
            ucdi.lblDesc.Location = new Point(15, 134);
            ucdi.txtDescription.Location = new Point(110, 131);
            ucdi.label1.Visible = false;
            ucdi.CmbDataType.Visible = false;
            ucdi.label2.Visible = false;
            ucdi.label3.Visible = false;
            ucdi.label4.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.cmbIEDName.Visible = false;
            ucdi.cmb61850DIIndex.Visible = false;
            ucdi.cmb61850DIResponseType.Visible = false;
            ucdi.lblAutoMapNumber.Visible = false;
            ucdi.txtAutpMapNumber.Visible = false;
            ucdi.lblEventTrue.Visible = false;
            ucdi.lblEventFalse.Visible = false;
            ucdi.txtEvent_T.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.grpDI.Location = new Point(250, 53);
            ucdi.btnDone.Location = new Point(110, 160);
            ucdi.btnCancel.Location = new Point(214, 160);
            ucdi.btnFirst.Location = new Point(15, 195);
            ucdi.btnPrev.Location = new Point(87, 195);
            ucdi.btnNext.Location = new Point(159, 195);
            ucdi.btnLast.Location = new Point(231, 195);
            //ucdi.btnDone.Location = new Point(110, 165);
            //ucdi.btnCancel.Location = new Point(194, 165);
            //ucdi.btnFirst.Location = new Point(20, 200);
            //ucdi.btnPrev.Location = new Point(91, 200);
            //ucdi.btnNext.Location = new Point(170, 200);
            //ucdi.btnLast.Location = new Point(241, 200);
            ucdi.grpDI.Height = 220;
        }
        public void EnableEvent61850OnDoubleClick()
        {
            //ucdi.grpDI.Location = new Point(175, 55);
            //ucdi.txtAutpMapNumber.Enabled = true;
            //ucdi.lblAutoMapNumber.Enabled = true;
            //ucdi.txtAutpMapNumber.Visible = true;
            //ucdi.lblAutoMapNumber.Visible = true;
            //ucdi.lblEventTrue.Visible = false;
            //ucdi.txtEvent_T.Visible = false;
            //ucdi.lblEventFalse.Visible = false;
            //ucdi.txtEvent_F.Visible = false;
            //ucdi.lblRT.Visible = false;
            //ucdi.cmbResponseType.Visible = false;
            //ucdi.lblIdx.Visible = false;
            //ucdi.txtIndex.Visible = false;
            //ucdi.lblDN.Location = new Point(15, 33);
            //ucdi.txtDINo.Size = new Size(390, 21);
            ////Namrata: 12/09/2017
            //ucdi.lblSIdx.Location = new Point(15, 58);
            //ucdi.txtSubIndex.Location = new Point(110, 55);
            //ucdi.txtSubIndex.Size = new Size(390, 21);

            //ucdi.label2.Location = new Point(15, 84);
            //ucdi.cmbIEDName.Location = new Point(110, 81);
            //ucdi.cmbIEDName.Size = new Size(390, 21);

            //ucdi.label3.Location = new Point(15, 109);
            //ucdi.cmb61850DIResponseType.Size = new Size(390, 21);
            //ucdi.cmb61850DIResponseType.Location = new Point(110, 106);

            //ucdi.label4.Location = new Point(15, 134);
            //ucdi.cmb61850DIIndex.Size = new Size(390, 21);
            //ucdi.cmb61850DIIndex.Location = new Point(110, 131);

            //ucdi.lblDesc.Location = new Point(15, 160);
            //ucdi.txtDescription.Location = new Point(110, 157);
            //ucdi.txtDescription.Size = new Size(390, 21);

            //ucdi.lblAutoMapNumber.Visible = false;
            //ucdi.txtAutpMapNumber.Visible = false;

            //ucdi.btnDone.Location = new Point(200, 185);
            //ucdi.btnCancel.Location = new Point(300, 185);
            //ucdi.btnFirst.Location = new Point(110, 215);
            //ucdi.btnPrev.Location = new Point(210, 215);
            //ucdi.btnNext.Location = new Point(310, 215);
            //ucdi.btnLast.Location = new Point(410, 215);

            //ucdi.grpDI.Size = new Size(520, 245); //ucdi.grpDI.Size = new Size(530, 250);
            //ucdi.pbHdr.Width = 530;
            //ucdi.CmbDataType.Visible = false;
            //ucdi.label1.Visible = false;

            ucdi.grpDI.Location = new Point(172, 52);
            ucdi.txtAutpMapNumber.Enabled = true;
            ucdi.lblAutoMapNumber.Enabled = true;
            ucdi.txtAutpMapNumber.Visible = true;
            ucdi.lblAutoMapNumber.Visible = true;
            ucdi.lblEventTrue.Visible = false;
            ucdi.txtEvent_T.Visible = false;
            ucdi.lblEventFalse.Visible = false;
            ucdi.txtEvent_F.Visible = false;
            ucdi.lblRT.Visible = false;
            ucdi.cmbResponseType.Visible = false;
            ucdi.lblIdx.Visible = false;
            ucdi.txtIndex.Visible = false;
            ucdi.lblDN.Location = new Point(15, 33);
            ucdi.txtDINo.Size = new Size(300, 21);
            //Namrata: 12/09/2017
            //ucdi.lblSIdx.Location = new Point(15, 58);
            //ucdi.txtSubIndex.Location = new Point(110, 55);
            //ucdi.txtSubIndex.Size = new Size(300, 21);

            //ucdi.label2.Location = new Point(15, 84);
            //ucdi.cmbIEDName.Location = new Point(110, 81);
            //ucdi.cmbIEDName.Size = new Size(300, 21);

            //ucdi.label3.Location = new Point(15, 109);
            //ucdi.cmb61850DIResponseType.Size = new Size(300, 21);
            //ucdi.cmb61850DIResponseType.Location = new Point(110, 106);

            //ucdi.label4.Location = new Point(15, 134);
            //ucdi.cmb61850DIIndex.Size = new Size(300, 21);
            //ucdi.cmb61850DIIndex.Location = new Point(110, 131);

            //ucdi.lblFC.Location = new Point(15, 160);
            //ucdi.txtFC.Location = new Point(110, 157);
            //ucdi.txtFC.Size = new Size(300, 21);

            //ucdi.lblDesc.Location = new Point(15, 185);
            //ucdi.txtDescription.Location = new Point(110, 182);
            //ucdi.txtDescription.Size = new Size(300, 21);
            ucdi.label2.Location = new Point(15, 58);
            ucdi.cmbIEDName.Location = new Point(110, 55);
            ucdi.cmbIEDName.Size = new Size(300, 21);

            ucdi.label3.Location = new Point(15, 84);
            ucdi.cmb61850DIResponseType.Location = new Point(110, 81);
            ucdi.cmb61850DIResponseType.Size = new Size(300, 21);

            ucdi.label4.Location = new Point(15, 109);
            ucdi.cmb61850DIIndex.Size = new Size(300, 21);
            ucdi.cmb61850DIIndex.Location = new Point(110, 106);

            ucdi.lblFC.Location = new Point(15, 134);
            ucdi.txtFC.Location = new Point(110, 131);
            ucdi.txtFC.Size = new Size(300, 21);
            ucdi.txtFC.Enabled = false;

            ucdi.lblSIdx.Location = new Point(15, 160);
            ucdi.txtSubIndex.Location = new Point(110, 157);
            ucdi.txtSubIndex.Size = new Size(300, 21);
            ucdi.lblDesc.Location = new Point(15, 185);
            ucdi.txtDescription.Location = new Point(110, 182);
            ucdi.txtDescription.Size = new Size(300, 21);
            ucdi.lblAutoMapNumber.Visible = false;
            ucdi.txtAutpMapNumber.Visible = false;

            ucdi.btnDone.Location = new Point(150, 210);
            ucdi.btnCancel.Location = new Point(250, 210);
            ucdi.btnFirst.Location = new Point(90, 240);
            ucdi.btnPrev.Location = new Point(180, 240);
            ucdi.btnNext.Location = new Point(270, 240);
            ucdi.btnLast.Location = new Point(360, 240);

            ucdi.grpDI.Size = new Size(520, 265); //ucdi.grpDI.Size = new Size(530, 250);
            ucdi.grpDI.Width = 430; ucdi.pbHdr.Width = 530;
            ucdi.CmbDataType.Visible = false;
            ucdi.label1.Visible = false;
        }

        //Ajay: 25/09/2018
        public bool IsDIExist(string RT, int iIndex)
        {
            return diList.Where(x => x.Index == iIndex.ToString() && x.SubIndex == "0" && x.ResponseType == RT).Any();
        }
    }
}
