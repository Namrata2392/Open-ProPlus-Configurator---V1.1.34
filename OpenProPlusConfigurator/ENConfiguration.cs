using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data;
namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>ENConfiguration</b> is a class to store info about all EN's and there corresponding mapping infos.
    * \details   This class stores info related to all EN's and there corresponding mapping's for various slaves. It allows
    * user to add multiple EN's. The user can map this EN's to various slaves created in the system, along with the mapping parameters
    * for those slave types. It also exports the XML node related to this object.
    * 
    */
    public class ENConfiguration
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
        Dictionary<string, List<ENMap>> slavesENMapList = new Dictionary<string, List<ENMap>>();
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        List<EN> enList = new List<EN>();
        ucENlist ucen = new ucENlist();
        private const int COL_CMD_TYPE_WIDTH = 130;
        //Namrata: 11/09/2017
        //Fill RessponseType in All Configuration . 
        public DataGridView dataGridViewDataSet = new DataGridView();
        public DataTable dtdataset = new DataTable();
        DataRow datasetRow;
        #endregion Declaration
        public ENConfiguration(MasterTypes mType, int mNo, int iNo)
        {
            masterType = mType;
            masterNo = mNo;
            IEDNo = iNo;
            ucen.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucen.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucen.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucen.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucen.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            ucen.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            ucen.btnNextClick += new System.EventHandler(this.btnNext_Click);
            ucen.btnLastClick += new System.EventHandler(this.btnLast_Click);
            ucen.lvENlistDoubleClick += new System.EventHandler(this.lvENlist_DoubleClick);
            ucen.LinkDeleteConfigue.Click += new System.EventHandler(this.LinkDeleteConfigue_Click);
            ucen.lnkENMapClick += new System.EventHandler(this.lnkENMap_Click);
            ucen.cmbIEDName.SelectedIndexChanged += new System.EventHandler(this.cmbIEDName_SelectedIndexChanged);
            ucen.cmb61850ResponseType.SelectedIndexChanged += new System.EventHandler(this.cmb61850ResponseType_SelectedIndexChanged);
            ucen.cmb61850Index.SelectedIndexChanged += new System.EventHandler(this.cmb61850Index_SelectedIndexChanged);
            //ucen.lvENlistSelectedIndexChanged += new System.EventHandler(this.lvENlist_SelectedIndexChanged); //---For Alternate Colour
            //ucen.lvENMapSelectedIndexChanged += new System.EventHandler(this.lvENMap_SelectedIndexChanged); //---For Alternate Colour
            ucen.lvENlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENlist_ItemSelectionChanged);
            ucen.lvENMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENMap_ItemSelectionChanged);
            if (mType == MasterTypes.Virtual)//Disable add/edit/delete/dblclick n remove checkboxes...
            {
                ucen.btnAdd.Enabled = false;
                ucen.btnDelete.Enabled = false;
            }
            else
            {
                ucen.lvENlistDoubleClick += new System.EventHandler(this.lvENlist_DoubleClick);
            }
            if (mType == MasterTypes.ADR)
            {
                EnableEventsonLoad();
            }
            if (mType == MasterTypes.IEC101)
            {
                EnableEventsonLoad();
            }
            if (mType == MasterTypes.IEC103)
            {
                EnableEventsonLoad();
            }
            if (mType == MasterTypes.MODBUS)
            {
                EnableEventsonLoad();
            }
            if (mType == MasterTypes.IEC61850Client)
            {
                EnableEventsIEC61850onLoad();
            }
            ucen.btnENMDeleteClick += new System.EventHandler(this.btnENMDelete_Click);
            ucen.btnENMDoneClick += new System.EventHandler(this.btnENMDone_Click);
            ucen.btnENMCancelClick += new System.EventHandler(this.btnENMCancel_Click);
            ucen.lvENMapDoubleClick += new System.EventHandler(this.lvENMap_DoubleClick);

            //ucen.lvENlistColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvENlist_ColumnClick);
            //ucen.lvENMapColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvENMap_ColumnClick);
            addListHeaders();
            fillOptions();
        }
        private void cmbIEDName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "DI : cmbIEDName_SelectedIndexChanged";
            try
            {
                //Namrata: 04/04/2018
                if (ucen.cmbIEDName.Focused == false)
                {

                }
                else
                {
                    Utils.Iec61850IEDname = ucen.cmbIEDName.Text;
                    List<DataTable> dtList = Utils.dsResponseType.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                    if (dtList.Count == 0)
                    {
                        ucen.cmb61850ResponseType.DataSource = null;
                        ucen.cmb61850Index.DataSource = null;
                        ucen.cmb61850ResponseType.Enabled = false;
                        ucen.cmb61850Index.Enabled = false;
                        ucen.txtFC.Text = "";
                    }
                    else
                    {
                        ucen.cmb61850ResponseType.Enabled = true;
                        ucen.cmb61850Index.Enabled = true;
                        ucen.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//[Utils.strFrmOpenproplusTreeNode + "/" + "Undefined" + "/" + Utils.Iec61850IEDname];
                        ucen.cmb61850ResponseType.DisplayMember = "Address";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmb61850ResponseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "DI: cmb61850ResponseType_SelectedIndexChanged";
            try
            {
                if (ucen.cmb61850ResponseType.Items.Count > 1)
                {
                    if ((ucen.cmb61850ResponseType.SelectedIndex != -1))
                    {
                        //Namrata: 04/04/2018
                        Utils.Iec61850IEDname = ucen.cmbIEDName.Text;
                        //Utils.Iec61850IEDname = ucai.cmbIEDName.Items.OfType<DataRowView>().Select(x => x.Row[0].ToString()).FirstOrDefault().ToString();
                        List<DataTable> dtList = Utils.DsAllConfigurationData.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                        DataSet dsdummy = new DataSet();
                        dtList.ForEach(tbl => { DataTable dt = tbl.Copy(); dsdummy.Tables.Add(dt); });
                        ucen.cmb61850Index.DataSource = dsdummy.Tables[ucen.cmb61850ResponseType.SelectedIndex];
                        ucen.cmb61850Index.DisplayMember = "ObjectReferrence";
                        ucen.cmb61850Index.ValueMember = "Node";
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
            string strRoutineName = "DI : cmb61850DIIndex_SelectedIndexChanged";
            try
            {
                if (ucen.cmb61850Index.Items.Count > 0)
                {
                    if (ucen.cmb61850Index.SelectedIndex != -1)
                    {
                        ucen.txtFC.Text = ((DataRowView)ucen.cmb61850Index.SelectedItem).Row[2].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void EnableEvent()
        {
            ucen.lblAutomapNumber.Visible = true;
            ucen.txtAutoMapNumber.Visible = true;
            ucen.lblAutomapNumber.Enabled = true;
            ucen.txtAutoMapNumber.Enabled = true;
            ucen.btnDone.Location = new Point(121, 262);
            ucen.btnCancel.Location = new Point(218, 262);
            ucen.grpEN.Size = new Size(314, 300);
            ucen.grpEN.Location = new Point(172, 48);
            ucen.lvENlistDoubleClick += new System.EventHandler(this.lvENlist_DoubleClick);
        }
        private int SelectedIndex;
        private void lvENlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color GreenColour = Color.FromArgb(82, 208, 23);
            if (ucen.lvENlist.SelectedIndices.Count > 0)
            {
                SelectedIndex = Convert.ToInt32(ucen.lvENlist.SelectedItems[0].Text);
                ucen.lvENMap.SelectedItems.Clear();
                ucen.lvENMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucen.lvENMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucen.lvENMap.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);

                ucen.lvENlist.SelectedItems.Clear();
                ucen.lvENlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucen.lvENlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucen.lvENlist.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
            }
        }

        private void lvENMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color GreenColour = Color.FromArgb(82, 208, 23);
            if (ucen.lvENMap.SelectedIndices.Count > 0)
            {
                SelectedIndex = Convert.ToInt32(ucen.lvENMap.SelectedItems[0].Text);
                ucen.lvENlist.SelectedItems.Clear();
                ucen.lvENlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucen.lvENlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucen.lvENlist.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);

                ucen.lvENMap.SelectedItems.Clear();
                ucen.lvENMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucen.lvENMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucen.lvENMap.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
            }
        }
        private string getDescription(ListView lstview, string ainno)
        {
            int iColIndex = ucen.lvENlist.Columns["Description"].Index;
            var query = lstview.Items
                    .Cast<ListViewItem>()
                    .Where(item => item.SubItems[0].Text == ainno).Select(s => s.SubItems[iColIndex].Text).Single();
            return query.ToString();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (enList.Count >= getMaxENs())
            {
                MessageBox.Show("Maximum " + getMaxENs() + " EN's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            mode = Mode.ADD;
            editIndex = -1;
           if((masterType == MasterTypes.ADR)||(masterType == MasterTypes.IEC101)||(masterType == MasterTypes.IEC103)||(masterType == MasterTypes.MODBUS))
            {
               EnableEventsonLoad();
               //EnableEvent();
            }
            if (masterType == MasterTypes.IEC61850Client)
            {
                EnableEventsIEC61850onLoad();
                ucen.txtFC.Enabled = false;
                FetchComboboxData();
               
            }
            Utils.resetValues(ucen.grpEN);
            Utils.showNavigation(ucen.grpEN, false);
            loadDefaults();
            ucen.grpEN.Visible = true;
            ucen.cmbResponseType.Focus();
            //Namrata: 04/04/2018
            if (masterType == MasterTypes.IEC61850Client)
            {
                if (ucen.cmbIEDName.SelectedIndex != -1)
                {
                    ucen.cmbIEDName.SelectedIndex = ucen.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                    ucen.txtFC.Text = ((DataRowView)ucen.cmb61850Index.SelectedItem).Row[2].ToString();
                }
                else
                {
                    ucen.cmbIEDName.Visible = false;
                    MessageBox.Show("ICD File Missing !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        private void FetchComboboxData()
        {
            //Namrata: 13/03/2018
            ucen.cmbIEDName.DataSource = null;
            List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
            string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
            //Namrata: 26/04/2018
            if (tblName != null)
            {
                ucen.cmbIEDName.DataSource = Utils.dsIED.Tables[tblName];
                ucen.cmbIEDName.DisplayMember = "IEDName";
                //Namrata: 21/03/2018
                ucen.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[tblName];
                ucen.cmb61850ResponseType.DisplayMember = "Address";
                //Namrata: 29/03/2018
                ucen.cmb61850Index.DataSource = Utils.DsAllConfigurationData.Tables[tblName + "_On Request"];
                ucen.cmb61850Index.DisplayMember = "ObjectReferrence";
                ucen.cmb61850Index.ValueMember = "Node";
            }
            else
            {
                //MessageBox.Show("ICD File Missing !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }
        }
        private void LinkDeleteConfigue_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listItem in ucen.lvENlist.Items)
            {
                listItem.Checked = true;
            }
            DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            //DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                foreach (ListViewItem listItem in ucen.lvENlist.Items)
                {
                    listItem.Checked = false;
                }
                return;
            }

            for (int i = ucen.lvENlist.Items.Count - 1; i >= 0; i--)
            {
                Console.WriteLine("*** removing indices: {0}", i);
                deleteENFromMaps(enList.ElementAt(i).ENNo);
                enList.RemoveAt(i);
                ucen.lvENlist.Items.Clear();
            }
            Console.WriteLine("*** enList count: {0} lv count: {1}", enList.Count, ucen.lvENlist.Items.Count);
            refreshList();
            //Refresh map listview...
            refreshCurrentMapList();
        }
        private void lnkENMap_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucen btnENMDelete_Click clicked in class!!!");

            foreach (ListViewItem listItem in ucen.lvENMap.Items)
            {
                listItem.Checked = true;
            }
            DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            //DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                foreach (ListViewItem listItem in ucen.lvENMap.Items)
                {
                    listItem.Checked = false;
                }
                return;
            }
            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(currentSlave, out slaveENMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error deleting EN map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = ucen.lvENMap.Items.Count - 1; i >= 0; i--)
            {

                Console.WriteLine("*** removing indices: {0}", i);
                slaveENMapList.RemoveAt(i);
                ucen.lvENMap.Items.Clear();
            }
            Console.WriteLine("*** slaveENMapList count: {0} lv count: {1}", slaveENMapList.Count, ucen.lvENMap.Items.Count);
            refreshMapList(slaveENMapList);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucen btnDelete_Click clicked in class!!!");
            Console.WriteLine("*** enList count: {0} lv count: {1}", enList.Count, ucen.lvENlist.Items.Count);
            DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                return;
            }

            for (int i = ucen.lvENlist.Items.Count - 1; i >= 0; i--)
            {
                if (ucen.lvENlist.Items[i].Checked)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    deleteENFromMaps(enList.ElementAt(i).ENNo);
                    enList.RemoveAt(i);
                    ucen.lvENlist.Items[i].Remove();
                }
            }
            Console.WriteLine("*** enList count: {0} lv count: {1}", enList.Count, ucen.lvENlist.Items.Count);
            refreshList();
            //Refresh map listview...
            refreshCurrentMapList();
        }
        int ENNo = 0;
        int ENINdex = 0;
        string ENDescription = "";
        private string Response = "";
        private RCBConfiguration RCBNode = null;
        private void btnDone_Click(object sender, EventArgs e)
        {
            //if (!Validate()) return;
            List<KeyValuePair<string, string>> enData = Utils.getKeyValueAttributes(ucen.grpEN);
            #region Fill Address to Datatable for RCBConfiguration 
            //Namrata: 27/09/2017
            //fill Address to Datatable for RCBConfiguration 
            if (masterType == MasterTypes.IEC61850Client)
            {
                Response = ucen.cmb61850ResponseType.Text;
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
                if (Utils.dsRCBEN.Tables.Contains(dtdataset.TableName))
                {
                    Utils.dsRCBEN.Tables[dtdataset.TableName].Clear();
                }
                else
                {
                    Utils.dsRCBEN.Tables.Add(dtdataset.TableName);
                    Utils.dsRCBEN.Tables[dtdataset.TableName].Columns.Add("ObjectReferrence");
                    Utils.dsRCBEN.Tables[dtdataset.TableName].Columns.Add("Node");
                }
                for (int i = 0; i < dtdataset.Rows.Count; i++)
                {
                    row112 = Utils.dsRCBEN.Tables[dtdataset.TableName].NewRow();
                    Utils.dsRCBEN.Tables[dtdataset.TableName].NewRow();
                    for (int j = 0; j < dtdataset.Columns.Count; j++)
                    {
                        Index112 = dtdataset.Rows[i][j].ToString();
                        row112[j] = Index112.ToString();
                    }
                    Utils.dsRCBEN.Tables[dtdataset.TableName].Rows.Add(row112);
                }
                Utils.dsRCBData = Utils.dsRCBEN;
                Utils.dsRCBData.Merge(Utils.dsRCBAI, false, MissingSchemaAction.Add);
                Utils.dsRCBData.Merge(Utils.dsRCBAO, false, MissingSchemaAction.Add);
                Utils.dsRCBData.Merge(Utils.dsRCBDI, false, MissingSchemaAction.Add);
                Utils.dsRCBData.Merge(Utils.dsRCBDO, false, MissingSchemaAction.Add);
            }
            #endregion Fill Address to Datatable for RCBConfiguration 
            if (mode == Mode.ADD)
            {
                //Namrata:29/6/2017
                int intStart = Convert.ToInt32(enData[10].Value); // AINo
                int intRange = Convert.ToInt32(enData[4].Value); //AutoMapRange
                int intENIndex = Convert.ToInt32(enData[12].Value); // AIIndex

                //Ajay: 23/11/2017
                if (intRange > getMaxENs())
                {
                    MessageBox.Show("Maximum " + getMaxENs() + " EN's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    //Ajay: 21/11/2017
                    for (int i = intStart; i <= intStart + intRange - 1; i++)
                    {

                        ENNo = Globals.ENNo;
                        ENNo += 1;
                        ENINdex = intENIndex++;
                        if (masterType == MasterTypes.ADR)
                        {
                            ENDescription = ucen.txtDescription.Text;
                        }
                        else if (masterType == MasterTypes.IEC101)
                        {
                            ENDescription = ucen.txtDescription.Text;
                        }
                        else if (masterType == MasterTypes.IEC103)
                        {
                            ENDescription = ucen.txtDescription.Text;
                        }
                        else if (masterType == MasterTypes.MODBUS)
                        {
                            ENDescription = ucen.txtDescription.Text;
                        }
                        //Namrata: 10/02/2018
                        else if (masterType == MasterTypes.IEC61850Client)
                        {
                            ENDescription = ucen.txtDescription.Text;
                        }
                        EN NewEI = new EN("EN", enData, null, masterType, masterNo, IEDNo);
                        NewEI.ENNo = ENNo.ToString();
                        //Ajay: 11/06/2018
                        //NewEI.Index = ENINdex.ToString();
                        NewEI.Index = ENINdex.ToString();
                        if ((ucen.cmbDataType.Text == "UnsignedInt32_LSB_MSB") 
                            || (ucen.cmbDataType.Text == "SignedInt32_LSB_MSB") || (ucen.cmbDataType.Text == "UnsignedInt32_MSB_LSB") 
                            || (ucen.cmbDataType.Text == "SignedInt32_MSB_LSB") || (ucen.cmbDataType.Text == "Float_LSB_MSB") 
                            || (ucen.cmbDataType.Text == "Float_MSB_LSB") || (ucen.cmbDataType.Text == "UnsignedLong32_Bit_MSWord_LSWord") 
                            || (ucen.cmbDataType.Text == "UnsignedLong32_Bit_LSWord_MSWord") || (ucen.cmbDataType.Text == "SignedLong32_Bit_MSWord_LSWord") 
                            || (ucen.cmbDataType.Text == "SignedLong32_Bit_LSWord_MSWord") || (ucen.cmbDataType.Text == "Float_MSWord_LSWord") 
                            || (ucen.cmbDataType.Text == "Float_LSWord_MSWord") || (ucen.cmbDataType.Text == "UnsignedInt24_LSB_MSB") 
                            || (ucen.cmbDataType.Text == "SignedInt24_LSB_MSB") || (ucen.cmbDataType.Text == "UnsignedInt24_MSB_LSB") 
                            || (ucen.cmbDataType.Text == "SignedInt24_MSB_LSB"))
                        {
                            intENIndex++;
                        }

                        NewEI.Description = ENDescription;
                        NewEI.IEDName = ucen.cmbIEDName.Text.ToString();
                        NewEI.IEC61850Index = ucen.cmb61850Index.Text.ToString();
                        NewEI.IEC61850ResponseType = ucen.cmb61850ResponseType.Text.ToString();
                        //Namrata: 10/04/2018
                        if (masterType == MasterTypes.IEC61850Client)
                        {
                            if ((ucen.cmb61850Index.Text.ToString() == "") || (ucen.cmb61850ResponseType.Text.ToString() == ""))
                            {
                                MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        enList.Add(NewEI);

                    }
                }
            }
            else if (mode == Mode.EDIT)
            {
                enList[editIndex].updateAttributes(enData);
                //Namrata: 10/04/2018
                if (masterType == MasterTypes.IEC61850Client)
                {
                    if ((ucen.cmb61850Index.Text.ToString() == "") || (ucen.cmb61850ResponseType.Text.ToString() == ""))
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
            //Namrata: 27/7/2017
            if (sender != null && e != null)
            {
                ucen.grpEN.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucen btnCancel_Click clicked in class!!!");
            ucen.grpEN.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
            Utils.resetValues(ucen.grpEN);
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucen btnFirst_Click clicked in class!!!");
            if (ucen.lvENlist.Items.Count <= 0) return;
            if (enList.ElementAt(0).IsNodeComment) return;
            editIndex = 0;
            loadValues();
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            //Namrata: 27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucen btnPrev_Click clicked in class!!!");
            if (editIndex - 1 < 0) return;
            if (enList.ElementAt(editIndex - 1).IsNodeComment) return;
            editIndex--;
            loadValues();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //Namrata: 27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucen btnNext_Click clicked in class!!!");
            if (editIndex + 1 >= ucen.lvENlist.Items.Count) return;
            if (enList.ElementAt(editIndex + 1).IsNodeComment) return;
            editIndex++;
            loadValues();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucen btnLast_Click clicked in class!!!");
            if (ucen.lvENlist.Items.Count <= 0) return;
            if (enList.ElementAt(enList.Count - 1).IsNodeComment) return;
            editIndex = enList.Count - 1;
            loadValues();
        }
        private void lvENlist_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucen lvENlist_DoubleClick clicked in class!!!");
            //Namrata: 10/09/2017
            ucen.txtAutoMapNumber.Text = "0";
            ucen.txtAutoMapNumber.Enabled = false;
            ucen.lblAutomapNumber.Enabled = false;
            //if (ucen.lvENlist.SelectedItems.Count <= 0) return;
            //ListViewItem lvi = ucen.lvENlist.SelectedItems[0];

            //Namrata: 07/03/2018
            int ListIndex = ucen.lvENlist.FocusedItem.Index;
            ListViewItem lvi = ucen.lvENlist.Items[ListIndex];

            Utils.UncheckOthers(ucen.lvENlist, lvi.Index);
            if (enList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.IEC61850Client) || (masterType == MasterTypes.MODBUS))
            {
                ucen.lblAutomapNumber.Visible = false;
                ucen.txtAutoMapNumber.Visible = false;
                ucen.btnDone.Location = new Point(121, 235);
                ucen.btnCancel.Location = new Point(218, 235);
                ucen.btnFirst.Location = new Point(15, 265);
                ucen.btnNext.Location = new Point(89, 265);
                ucen.btnPrev.Location = new Point(163, 265);
                ucen.btnLast.Location = new Point(237, 265);
                ucen.grpEN.Size = new Size(314, 295);
            }
            if (masterType == MasterTypes.IEC61850Client)
            {
                EnableEventsIEC61850onDoublClick();
                FetchComboboxData();

                //Namrata: 04/04/2018
                ucen.cmbIEDName.SelectedIndex = ucen.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                ucen.txtFC.Text = ((DataRowView)ucen.cmb61850Index.SelectedItem).Row[2].ToString();

                if ((ucen.cmb61850Index.Text.ToString() == "") || (ucen.cmb61850ResponseType.Text.ToString() == ""))
                {
                    MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            ucen.grpEN.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            Utils.showNavigation(ucen.grpEN, true);
            loadValues();
            ucen.cmbResponseType.Focus();
        }
        private void lvENlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "lvENlist_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    Console.WriteLine("*** selected DI: {0}", diIndex);
                    //Namrata: 27/7/2017
                    ucen.lvENMapItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENMap_ItemSelectionChanged);
                    ucen.lvENMap.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucen.lvENMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucen.lvENMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucen.lvENMap);
                    //Namrata: 27/7/2017
                    ucen.lvENMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucen.lvENlist.SelectedItems.Clear();
                    ucen.lvENlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucen.lvENlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucen.lvENlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucen.lvENMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENMap_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //if (e.IsSelected)
            //{
            //    string enIndex = e.Item.Text;
            //    Console.WriteLine("*** selected EN: {0}", enIndex);
            //    //Namrata: 27/7/2017
            //    ucen.lvENMapItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENMap_ItemSelectionChanged);
            //    //Remove selection from ENMap...
            //    ucen.lvENMap.SelectedItems.Clear();
            //    Utils.highlightListviewItem(enIndex, ucen.lvENMap);
            //    //Namrata: 27/7/2017
            //    ucen.lvENMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENMap_ItemSelectionChanged);
            //}
        }

        private void lvENMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "lvDOMap_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    Console.WriteLine("*** selected DI: {0}", diIndex);
                    //Namrata: 27/7/2017
                    ucen.lvENlistItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENlist_ItemSelectionChanged);
                    ucen.lvENlist.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucen.lvENlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucen.lvENlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucen.lvENlist);
                    //Namrata:lvAIlist 27/7/2017
                    ucen.lvENlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucen.lvENMap.SelectedItems.Clear();
                    ucen.lvENMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucen.lvENMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucen.lvENMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucen.lvENlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENlist_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //if (e.IsSelected)
            //{
            //    string enIndex = e.Item.Text;
            //    Console.WriteLine("*** selected EN: {0}", enIndex);
            //    //Namrata: 27/7/2017
            //    ucen.lvENlistItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENlist_ItemSelectionChanged);
            //    //Remove selection from ENMap...
            //    ucen.lvENlist.SelectedItems.Clear();
            //    Utils.highlightListviewItem(enIndex, ucen.lvENlist);
            //    //Namrata: 27/7/2017
            //    ucen.lvENlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvENlist_ItemSelectionChanged);
            //}
        }
        private void loadDefaults()
        {
            ucen.txtAutoMapNumber.Text = "1";
            ucen.txtENNo.Text = (Globals.ENNo + 1).ToString();
            ucen.txtSubIndex.Text = "1";
            ucen.txtMultiplier.Text = "1";
            ucen.txtConstant.Text = "0";
            ucen.txtIndex.Text = "1";
            if (masterType == MasterTypes.ADR)
            {
                if (ucen.lvENlist.Items.Count - 1 >= 0)
                {
                    ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 1);
                }
                ucen.cmbResponseType.SelectedIndex = ucen.cmbResponseType.FindStringExact("ADR_EN");
                ucen.txtDescription.Text = "ADR_EN";// + (Globals.ENNo + 1).ToString();
            }
            else if (masterType == MasterTypes.IEC101)
            {
                if (ucen.lvENlist.Items.Count - 1 >= 0)
                {
                    ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 1);
                }
                ucen.cmbResponseType.SelectedIndex = ucen.cmbResponseType.FindStringExact("IntegratedTotals");
                ucen.txtDescription.Text = "IEC101_EN";// + (Globals.ENNo + 1).ToString();
            }
            else if (masterType == MasterTypes.IEC103)
            {
                if (ucen.lvENlist.Items.Count - 1 >= 0)
                {
                    ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 1);
                }
                ucen.cmbResponseType.SelectedIndex = ucen.cmbResponseType.FindStringExact("Measurand_III");
                ucen.txtDescription.Text = "IEC103_EN";/// + (Globals.ENNo + 1).ToString();
            }
            else if (masterType == MasterTypes.MODBUS)
            {
                if (ucen.lvENlist.Items.Count - 1 >= 0)
                {
                    //Ajay: 11/06/2018
                    //ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 1);
                    switch (enList[enList.Count - 1].DataType)
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
                            ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 2);
                            break;
                        default:
                            ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 1);
                            break;
                    }
                }
                ucen.cmbResponseType.SelectedIndex = ucen.cmbResponseType.FindStringExact("ReadInputRegister");
                ucen.txtDescription.Text = "MODBUS_EN";// + (Globals.ENNo + 1).ToString();
            }
            else if (masterType == MasterTypes.IEC61850Client)
            {
                if (ucen.lvENlist.Items.Count - 1 >= 0)
                {
                    ucen.txtIndex.Text = Convert.ToString(Convert.ToInt32(enList[enList.Count - 1].Index) + 1);
                }
                ucen.cmbResponseType.SelectedIndex = ucen.cmbResponseType.FindStringExact("ReadInputRegister");
                ucen.txtDescription.Text = "IEC61850_EN";// + (Globals.ENNo + 1).ToString();
            }
        }
        private void loadValues()
        {
            EN en = enList.ElementAt(editIndex);
            if (en != null)
            {
                ucen.txtENNo.Text = en.ENNo;
                ucen.cmbResponseType.SelectedIndex = ucen.cmbResponseType.FindStringExact(en.ResponseType);
                ucen.txtIndex.Text = en.Index;
                ucen.txtSubIndex.Text = en.SubIndex;
                ucen.cmbDataType.SelectedIndex = ucen.cmbDataType.FindStringExact(en.DataType);
                ucen.txtMultiplier.Text = en.Multiplier;
                ucen.txtConstant.Text = en.Constant;
                ucen.txtDescription.Text = en.Description;
                ucen.cmbIEDName.SelectedIndex = ucen.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                //ucai.cmbIEDName.SelectedIndex = ucai.cmbIEDName.FindStringExact(ai.IEDName);
                ucen.cmb61850ResponseType.SelectedIndex = ucen.cmb61850ResponseType.FindStringExact(en.IEC61850ResponseType);
                ucen.cmb61850Index.SelectedIndex = ucen.cmb61850Index.FindStringExact(en.IEC61850Index);

            }
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucen.grpEN))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }

        private void refreshList()
        {
            int cnt = 0;
            ucen.lvENlist.Items.Clear();
            Utils.ENlistforDescription.Clear();

            if((masterType == MasterTypes.ADR)|| (masterType == MasterTypes.IEC101)|| (masterType == MasterTypes.IEC103) ||(masterType == MasterTypes.MODBUS)|| (masterType == MasterTypes.Virtual))
            {
                foreach (EN en in enList)
                {
                    string[] row = new string[8];
                    if (en.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = en.ENNo;
                        row[1] = en.ResponseType;
                        row[2] = en.Index;
                        row[3] = en.SubIndex;
                        row[4] = en.DataType;
                        row[5] = en.Multiplier;
                        row[6] = en.Constant;
                        row[7] = en.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucen.lvENlist.Items.Add(lvItem);
                }
            }
            if(masterType == MasterTypes.IEC61850Client)
            {
                foreach (EN en in enList)
                {
                    string[] row = new string[9];
                    if (en.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = en.ENNo;
                        row[1] = en.IEDName;
                        row[2] = en.IEC61850ResponseType;
                        row[3] = en.IEC61850Index;
                        row[4] = en.FC;
                        row[5] = en.SubIndex;
                        row[6] = en.Multiplier;
                        row[7] = en.Constant;
                        row[8] = en.Constant;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucen.lvENlist.Items.Add(lvItem);
                }
            }
          
            ucen.lblENRecords.Text = enList.Count.ToString();
            Utils.EnlistRegenerateIndex.AddRange(enList);
            Utils.ENlistforDescription.AddRange(enList);
        }
        private int getMaxENs()
        {
            if (masterType == MasterTypes.IEC103) return Globals.MaxIEC103EN;
            else if (masterType == MasterTypes.MODBUS) return Globals.MaxMODBUSEN;
            //Namarta:13/7/2107
            else if (masterType == MasterTypes.IEC101) return Globals.MaxIEC101EN;
            else if (masterType == MasterTypes.ADR) return Globals.MaxADREN;
            else if (masterType == MasterTypes.IEC61850Client) return Globals.Max61850EN;
            else return 0;
        }
        /* ============================================= Below this, EN Map logic... ============================================= */
        private void CreateNewSlave(string slaveNum, string slaveID, XmlNode enmNode)
        {
            List<ENMap> senmList = new List<ENMap>();
            slavesENMapList.Add(slaveID, senmList);
            if (enmNode != null)
            {
                foreach (XmlNode node in enmNode)
                {
                    //Console.WriteLine("***** node type: {0}", node.NodeType);
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    senmList.Add(new ENMap(node, Utils.getSlaveTypes(slaveID)));
                }
            }
            AddMap2SlaveButton(Int32.Parse(slaveNum), slaveID);
            //Namrata: 24/02/2018
            //If Description attribute not exist in XML 
            senmList.AsEnumerable().ToList().ForEach(item =>
            {
                string strDONo = item.ENNo;
                item.Description = Utils.ENlistforDescription.AsEnumerable().Where(x => x.ENNo == strDONo).Select(x => x.Description).FirstOrDefault();
            });
            refreshMapList(senmList);
            currentSlave = slaveID;
        }
        private void DeleteSlave(string slaveID)
        {
            Console.WriteLine("*** Deleting slave {0}", slaveID);
            slavesENMapList.Remove(slaveID);
            RadioButton rb = null;
            foreach (Control ctrl in ucen.flpMap2Slave.Controls)
            {
                if (ctrl.Tag.ToString() == slaveID)
                {
                    rb = (RadioButton)ctrl;
                    break;
                }
            }
            if (rb != null) ucen.flpMap2Slave.Controls.Remove(rb);
        }
        //Namrata:13/7/2017
        private void CheckIEC101SlaveStatusChanges()
        {
            Console.WriteLine("*** CheckIEC101SlaveStatusChanges");
            //Check for slave addition...
            foreach (IEC101Slave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
            {
                string slaveID = "IEC101Slave_" + slvMB.SlaveNum;
                bool slaveAdded = true;
                foreach (KeyValuePair<string, List<ENMap>> sn in slavesENMapList)
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
            foreach (KeyValuePair<string, List<ENMap>> sain in slavesENMapList)//Loop thru slaves...
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
                if (ucen.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucen.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucen.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucen.lvENMap.Items.Clear();
                    currentSlave = "";
                }
            }

            fillMapOptions(Utils.getSlaveTypes(currentSlave));
        }
        private void CheckIEC104SlaveStatusChanges()
        {
            Console.WriteLine("*** CheckIEC104SlaveStatusChanges");
            //Check for slave addition...
            foreach (IEC104Slave slv104 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC104Group().getIEC104Slaves())//Loop thru slaves...
            {
                string slaveID = "IEC104_" + slv104.SlaveNum;
                bool slaveAdded = true;
                foreach (KeyValuePair<string, List<ENMap>> sn in slavesENMapList)
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
            foreach (KeyValuePair<string, List<ENMap>> senn in slavesENMapList)//Loop thru slaves...
            {
                string slaveID = senn.Key;
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
                if (ucen.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucen.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucen.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucen.lvENMap.Items.Clear();
                    currentSlave = "";
                }
            }

            fillMapOptions(Utils.getSlaveTypes(currentSlave));
        }

        private void CheckMODBUSSlaveStatusChanges()
        {
            Console.WriteLine("*** CheckMODBUSSlaveStatusChanges");
            //Check for slave addition...
            foreach (MODBUSSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())//Loop thru slaves...
            {
                string slaveID = "MODBUSSlave_" + slvMB.SlaveNum;
                bool slaveAdded = true;
                foreach (KeyValuePair<string, List<ENMap>> sn in slavesENMapList)
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
            foreach (KeyValuePair<string, List<ENMap>> senn in slavesENMapList)//Loop thru slaves...
            {
                string slaveID = senn.Key;
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
                if (ucen.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucen.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucen.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucen.lvENMap.Items.Clear();
                    currentSlave = "";
                }
            }

            fillMapOptions(Utils.getSlaveTypes(currentSlave));
        }
        private void CheckIEC61850SlaveStatusChanges()
        {
            Console.WriteLine("*** CheckMODBUSSlaveStatusChanges");
            //Check for slave addition...
            foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())//Loop thru slaves...
            {
                string slaveID = "IEC61850Server_" + slvMB.SlaveNum; //61850ServerSlaveGroup_
                bool slaveAdded = true;
                foreach (KeyValuePair<string, List<ENMap>> sn in slavesENMapList)
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
            foreach (KeyValuePair<string, List<ENMap>> senn in slavesENMapList)//Loop thru slaves...
            {
                string slaveID = senn.Key;
                bool slaveDeleted = true;
                if (Utils.getSlaveTypes(slaveID) != SlaveTypes.IEC61850Server) continue;
                foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())//Loop thru slaves...
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
                if (ucen.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucen.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucen.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucen.lvENMap.Items.Clear();
                    currentSlave = "";
                }
            }

            fillMapOptions(Utils.getSlaveTypes(currentSlave));
        }
        private void deleteENFromMaps(string enNo)
        {
            Console.WriteLine("*** Deleting element no. {0}", enNo);
            foreach (KeyValuePair<string, List<ENMap>> senn in slavesENMapList)//Loop thru slaves...
            {
                List<ENMap> delEntry = senn.Value;
                foreach (ENMap enmn in delEntry)
                {
                    if (enmn.ENNo == enNo)
                    {
                        delEntry.Remove(enmn);
                        break;
                    }
                }
            }
        }

        private void AddMap2SlaveButton(int slaveNum, string slaveID)
        {
            RadioButton rb = new RadioButton();
            rb.Name = slaveID;
            rb.Tag = slaveID;//Ex. 'IEC104_1'/'MODBUSSlave_1'
            if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC104)
                rb.Text = "IEC104 " + slaveNum;
            else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.MODBUSSLAVE)
                rb.Text = "MODBUS " + slaveNum;
            else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC61850Server)
                rb.Text = "IEC61850 " + slaveNum;
            //Namrata:7/7/17
            //--------------------------//
            else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC101SLAVE)
                rb.Text = "IEC101 " + slaveNum;
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
            //rb.Padding = new Padding(0, 0, 0, 0);
            rb.TextAlign = ContentAlignment.TopCenter;
            rb.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            rb.Appearance = Appearance.Button;
            rb.AutoSize = true;
            rb.Image = Properties.Resources.SlaveRadioButton;
            rb.Click += rbGrpMap2Slave_Click;

            ucen.flpMap2Slave.Controls.Add(rb);
            rb.Checked = true;
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
                if (ucen.lvENlist.SelectedItems.Count > 0)
                {
                    //If possible highlight the map for new slave selected...
                    //Remove selection from ENMap...
                    ucen.lvENMap.SelectedItems.Clear();
                    Utils.highlightListviewItem(ucen.lvENlist.SelectedItems[0].Text, ucen.lvENMap);
                }
            }

            ShowHideSlaveColumns();

            if (rbChanged && ucen.lvENlist.CheckedItems.Count <= 0) return;//We supported map listing only

            EN mapEN = null;

            if (ucen.lvENlist.CheckedItems.Count != 1)
            {
                MessageBox.Show("Select single EN element to map!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i < ucen.lvENlist.Items.Count; i++)
            {
                if (ucen.lvENlist.Items[i].Checked)
                {
                    Console.WriteLine("*** Mapping index: {0}", i);
                    mapEN = enList.ElementAt(i);
                    ucen.lvENlist.Items[i].Checked = false;//Now we can uncheck in listview...
                    break;
                }
            }
            if (mapEN == null) return;

            //Search if already mapped for current slave...
            bool alreadyMapped = false;
            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(currentSlave, out slaveENMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Slave entry doesnot exist!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                //Do it from elsewhere...
                //slaveENMapList = new List<ENMap>();
                //slavesENMapList.Add(currentSlave, slaveENMapList);
            }
            else
            {
                Console.WriteLine("##### Slave entries exists");
            }
            //Namrata Commented: 28/04/2018
            //foreach (ENMap senm in slaveENMapList)
            //{
            //    if (senm.ENNo == mapEN.ENNo)
            //    {
            //        Console.WriteLine("##### Hoorray, already mapped...");
            //        MessageBox.Show("EN already mapped!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        alreadyMapped = true;
            //        break;
            //    }
            //}
            if (!alreadyMapped)
            {
                mapMode = Mode.ADD;
                mapEditIndex = -1;
                Utils.resetValues(ucen.grpENMap);
                ucen.txtENMNo.Text = mapEN.ENNo;
                ucen.txtMapDescription.Text = getDescription(ucen.lvENlist, mapEN.ENNo.ToString());
                //Namrata:1/7/2017
                ucen.txtENAutoMapNumber.Text = "1";
                loadMapDefaults();
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE))
                {
                    ucen.grpENMap.Size = new Size(301, 307);
                    ucen.grpENMap.Location = new Point(583, 310);
                    ucen.lblAutoMapM.Visible = true;
                    ucen.txtENAutoMapNumber.Visible = true;
                    ucen.btnENMDone.Location = new Point(116, 267);
                    ucen.btnENMCancel.Location = new Point(203, 267);
                }
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) ucen.cmbENMCommandType.Enabled = true;
                else ucen.cmbENMCommandType.Enabled = false;
                //Namrata:28/7/ 2017
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) ucen.cmbENMDataType.SelectedIndex = ucen.cmbENMDataType.FindStringExact("IntegratedTotals");
                ucen.grpENMap.Visible = true;
                ucen.txtENMReportingIndex.Focus();
            }
        }

        private void btnENMDelete_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucen btnENMDelete_Click clicked in class!!!");
            DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                return;
            }

            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(currentSlave, out slaveENMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error deleting EN map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = ucen.lvENMap.Items.Count - 1; i >= 0; i--)
            {
                if (ucen.lvENMap.Items[i].Checked)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    slaveENMapList.RemoveAt(i);
                    ucen.lvENMap.Items[i].Remove();
                }
            }
            Console.WriteLine("*** slaveENMapList count: {0} lv count: {1}", slaveENMapList.Count, ucen.lvENMap.Items.Count);
            refreshMapList(slaveENMapList);
        }
        List<ENMap> slaveENMapList;

        private void btnENMDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAIMDone_Click";
            try
            {
                List<KeyValuePair<string, string>> enmData = Utils.getKeyValueAttributes(ucen.grpENMap);
                int intStart = Convert.ToInt32(enmData[7].Value); // AINo
                int intRange = Convert.ToInt32(enmData[1].Value); //AutoMapRange
                int intAIIndex = Convert.ToInt32(enmData[8].Value); // AIReportingIndex

                //Namrata:24/7/2017
                //For Modbus Slave
                int intAIIndex1 = Convert.ToInt32(enmData[8].Value); // For MODBUSSlave AI Index Incremented by 1
                int intDupAIIndex = 0;
                int AINumber = 0, AIINdex = 0;
                int ReportingIndex = 0;
                string AIDescription = "";

                //Namrata:8/7/2017
                ListViewItem item = ucen.lvENlist.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Text == intStart.ToString()); //Find Index Of ListView
                string ind = ucen.lvENlist.Items.IndexOf(item).ToString(); //Find Index Of ListView

                //Namrata:31/7/2017
                ListViewItem ExistAIMap = ucen.lvENMap.FindItemWithText(ucen.txtENMNo.Text); //Eliminate Duplicate Record From lvAIMAp List

                if (!ValidateMap()) return;
                Console.WriteLine("*** ucai btnAIMDone_Click clicked in class!!!");

                if (!slavesENMapList.TryGetValue(currentSlave, out slaveENMapList))
                {
                    Console.WriteLine("##### Slave entries does not exists");
                    MessageBox.Show("Error adding AI map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (mapMode == Mode.ADD)
                {
                    if (enList.Count >= 0)
                    {

                        //Namrata: 31 / 7 / 2017
                        //if (ExistAIMap != null)
                        //{
                        //    MessageBox.Show("Map Entry Already Exist In " + currentSlave.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    return;
                        //}
                        //else
                        //{
                        if ((intRange + Convert.ToInt16(ind)) > ucen.lvENlist.Items.Count)
                        {
                            MessageBox.Show("Slave Entry Does Not Exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            for (int i = intStart; i <= intStart + intRange - 1; i++)
                            {
                                if (enList.Select(x => x.ENNo).ToList().Contains(i.ToString()))
                                {

                                    AINumber = i;
                                    AIINdex = intAIIndex++;
                                    ucen.txtMapDescription.Text = getDescription(ucen.lvENlist, AINumber.ToString());
                                    AIDescription = ucen.txtMapDescription.Text;

                                    ENMap NewENMap = new ENMap("EN", enmData, Utils.getSlaveTypes(currentSlave));

                                    NewENMap.ENNo = AINumber.ToString();
                                    NewENMap.Description = AIDescription.ToString();
                                    NewENMap.ReportingIndex = (AIINdex).ToString();

                                    slaveENMapList.Add(NewENMap);
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
                    slaveENMapList[mapEditIndex].updateAttributes(enmData);
                }
                refreshMapList(slaveENMapList);
                ucen.grpENMap.Visible = false;
                mapMode = Mode.NONE;
                mapEditIndex = -1;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EnableEvents()
        {
            ucen.lblAutoMapM.Visible = false;
            ucen.txtENAutoMapNumber.Visible = false;
            ucen.btnENMDone.Location = new Point(116, 245);
            ucen.btnENMCancel.Location = new Point(203, 245);
            ucen.grpENMap.Location = new Point(450, 330);
            ucen.grpENMap.Height = 288;
        }
        private void btnENMCancel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucen btnENMCancel_Click clicked in class!!!");
            ucen.grpENMap.Visible = false;
            mapMode = Mode.NONE;
            mapEditIndex = -1;
            Utils.resetValues(ucen.grpENMap);
        }

        private void lvENMap_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucen lvENMap_DoubleClick clicked in class!!!");
            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(currentSlave, out slaveENMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error editing EN map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //if (ucen.lvENMap.SelectedItems.Count <= 0) return;
            //ListViewItem lvi = ucen.lvENMap.SelectedItems[0];

            //Namrata: 07/03/2018
            int ListIndex = ucen.lvENMap.FocusedItem.Index;
            ListViewItem lvi = ucen.lvENMap.Items[ListIndex];

            Utils.UncheckOthers(ucen.lvENMap, lvi.Index);
            if (slaveENMapList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ucen.txtENAutoMapNumber.Text = "0";
            if((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE)|| (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE))
            {
                EnableEvents();
            }
            ucen.grpENMap.Visible = true;
            mapMode = Mode.EDIT;
            mapEditIndex = lvi.Index;
            loadMapValues();
            ucen.txtENMReportingIndex.Focus();
        }

        private void loadMapDefaults()
        {
            ucen.txtENMReportingIndex.Text = (Globals.ENReportingIndex + 1).ToString();
            ucen.txtENMDeadBand.Text = "1";
            ucen.txtENMMultiplier.Text = "1";
            ucen.txtENMConstant.Text = "0";
            //if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
            //{
            //    ucen.txtMapDescription.Text = "MODBUSSLAVE_EN_" + Globals.ENNo.ToString();
            //}
            //if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE)
            //{
            //    ucen.txtMapDescription.Text = "IEC101SLAVE_EN_" + Globals.ENNo.ToString();
            //}
            //if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104)
            //{
            //    ucen.txtMapDescription.Text = "IEC104_EN_" + Globals.ENNo.ToString();
            //}
            //if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
            //{
            //    ucen.txtMapDescription.Text = "IEC61850Server_EN_" + Globals.ENNo.ToString();
            //}
            //ucen.cmbENMDataType.SelectedIndex = ucen.cmbENMDataType.FindStringExact("IntegratedTotals");
        }

        private void loadMapValues()
        {
            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(currentSlave, out slaveENMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error loading EN map data for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ENMap enmn = slaveENMapList.ElementAt(mapEditIndex);
            if (enmn != null)
            {
                ucen.txtENMNo.Text = enmn.ENNo;
                ucen.txtENMReportingIndex.Text = enmn.ReportingIndex;
                ucen.cmbENMDataType.SelectedIndex = ucen.cmbENMDataType.FindStringExact(enmn.DataType);
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                {
                    ucen.cmbENMCommandType.SelectedIndex = ucen.cmbENMCommandType.FindStringExact(enmn.CommandType);
                    ucen.cmbENMCommandType.Enabled = true;
                }
                else
                {
                    ucen.cmbENMCommandType.Enabled = false;
                }
                //Namrata: 18/11/2017
                ucen.txtMapDescription.Text = enmn.Description;
                ucen.txtENMDeadBand.Text = enmn.Deadband;
                ucen.txtENMMultiplier.Text = enmn.Multiplier;
                ucen.txtENMConstant.Text = enmn.Constant;
            }
        }

        private bool ValidateMap()
        {
            bool status = true;

            //Check empty field's
            if (Utils.IsEmptyFields(ucen.grpENMap))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return status;
        }

        private void refreshMapList(List<ENMap> tmpList)
        {
            int cnt = 0;
            ucen.lvENMap.Items.Clear();
            //addListHeaders();

            ucen.lblENMRecords.Text = "0";
            if (tmpList == null) return;

            foreach (ENMap enmp in tmpList)
            {
                string[] row = new string[8];
                if (enmp.IsNodeComment)
                {
                    row[0] = "Comment...";
                }
                else
                {
                    row[0] = enmp.ENNo;
                    row[1] = enmp.ReportingIndex;
                    row[2] = enmp.DataType;
                    row[3] = enmp.CommandType;
                    row[4] = enmp.Deadband;
                    row[5] = enmp.Multiplier;
                    row[6] = enmp.Constant;
                    row[7] = enmp.Description;
                }

                ListViewItem lvItem = new ListViewItem(row);
                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucen.lvENMap.Items.Add(lvItem);
            }
            ucen.lblENMRecords.Text = tmpList.Count.ToString();
        }

        private void refreshCurrentMapList()
        {
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
            //List<ENMap> senmList; //Ajay: 08/08/2018 commented
            //Ajay: 08/08/2018 if condition commented
            //Namrata:02/05/2018
            //if (slaveENMapList != null)
            //{
            //    slaveENMapList = slaveENMapList.OrderBy(x => Convert.ToInt32(x.ENNo)).ToList();
            //}
            //if (!slavesENMapList.TryGetValue(currentSlave, out senmList)) //Ajay: 08/08/2018 commented
            if (!slavesENMapList.TryGetValue(currentSlave, out slaveENMapList))  //Ajay: 08/08/2018
            {
                refreshMapList(null);
            }
            else
            {
                //if (senmList.Count > 0 && slaveENMapList != null) //Ajay: 08/08/2018 if condition commented
                if (slavesENMapList != null && slavesENMapList.Count > 0)
                {
                    //senmList = slaveENMapList.OrderBy(x => Convert.ToInt32(x.ENNo)).ToList(); //Ajay: 08/08/2018 commented
                    //refreshMapList(senmList); //Ajay: 08/08/2018 commented
                    slaveENMapList = slaveENMapList.OrderBy(x => Convert.ToInt32(x.ENNo)).ToList(); //Ajay: 08/08/2018
                }
                refreshMapList(slaveENMapList); //Ajay: 08/08/2018
            }
        }
        /* ============================================= Above this, EN Map logic... ============================================= */

        private void fillOptions()
        {
            DataColumn dcAddressColumn = dtdataset.Columns.Add("Address", typeof(string));
            dtdataset.Columns.Add("IED", typeof(string));

            //Fill IED Name
            ucen.cmbIEDName.Items.Clear();
            //Namrata: 31/10/2017
            ucen.cmbIEDName.DataSource = Utils.dsIED.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//Utils.DtIEDName;
            ucen.cmbIEDName.DisplayMember = "IEDName";
            if (Utils.Iec61850IEDname != "")
            {
                ucen.cmbIEDName.Text = Utils.Iec61850IEDname;
            }
            //Fill ResponseType For IEC61850Client
            ucen.cmb61850ResponseType.Items.Clear();
            ucen.cmb61850ResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//Utils.DtAddress;
            ucen.cmb61850ResponseType.DisplayMember = "Address";

            //Fill Response Type...
            if (masterType == MasterTypes.IEC61850Client)
            {
                ucen.cmbResponseType.Items.Clear();
            }
            else
            {
                ucen.cmbResponseType.Items.Clear();
                foreach (String rt in EN.getResponseTypes(masterType))
                {
                    ucen.cmbResponseType.Items.Add(rt.ToString());
                }
                ucen.cmbResponseType.SelectedIndex = 0;
            }

            //Fill Data Type...
            if(masterType == MasterTypes.IEC61850Client)
            {
                ucen.cmbDataType.Items.Clear();
            }
            else
            {
                foreach (String dt in EN.getDataTypes(masterType))
                {
                    ucen.cmbDataType.Items.Add(dt.ToString());
                }
                ucen.cmbDataType.SelectedIndex = 0;
            }
        }

        private void fillMapOptions(SlaveTypes sType)
        {
            /***** Fill Map details related combobox ******/

            try
            {
                //Fill Data Type...
                ucen.cmbENMDataType.Items.Clear();
                foreach (String dt in ENMap.getDataTypes(sType))
                {
                    ucen.cmbENMDataType.Items.Add(dt.ToString());
                }
                if (ucen.cmbENMDataType.Items.Count > 0) ucen.cmbENMDataType.SelectedIndex = 0;
            }
            catch (System.NullReferenceException)
            {
                Utils.WriteLine(VerboseLevel.ERROR, "EN Map DataType does not exist for Slave Type: {0}", sType.ToString());
            }

            try
            {
                //Fill Command Type...
                ucen.cmbENMCommandType.Items.Clear();
                foreach (String ct in ENMap.getCommandTypes(sType))
                {
                    ucen.cmbENMCommandType.Items.Add(ct.ToString());
                }
                if (ucen.cmbENMCommandType.Items.Count > 0) ucen.cmbENMCommandType.SelectedIndex = 0;
            }
            catch (System.NullReferenceException)
            {
                Utils.WriteLine(VerboseLevel.ERROR, "EN Map CommandType does not exist for Slave Type: {0}", sType.ToString());
            }
        }

        private void addListHeaders()
        {
            if(masterType == MasterTypes.ADR)
            {
                ucen.lvENlist.Columns.Add("No.", "No.", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Response Type", "Response Type", 220, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Index", "Index", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Sub Index", "Sub Index", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Data Type", "Data Type", 200, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Constant", "Constant", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
            }
            if (masterType == MasterTypes.IEC101)
            {
                ucen.lvENlist.Columns.Add("No.", "No.", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Response Type", "Response Type", 220, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Index", "Index", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Sub Index", "Sub Index", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Data Type", "Data Type", 200, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Constant", "Constant", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
            }
            if (masterType == MasterTypes.IEC103)
            {
                ucen.lvENlist.Columns.Add("No.", "No.", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Response Type", "Response Type", 220, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Index", "Index", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Sub Index", "Sub Index", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Data Type", "Data Type", 200, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Constant", "Constant", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
            }
            if (masterType == MasterTypes.MODBUS)
            {
                ucen.lvENlist.Columns.Add("No.", "No.", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Response Type", "Response Type", 220, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Index", "Index", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Sub Index", "Sub Index", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Data Type", "Data Type", 200, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Constant", "Constant", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
            }
            if (masterType == MasterTypes.Virtual)
            {
                ucen.lvENlist.Columns.Add("No.", "No.", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Response Type", "Response Type", 220, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Index", "Index", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Sub Index", "Sub Index", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Data Type", "Data Type", 200, HorizontalAlignment.Left, 0);
                ucen.lvENlist.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Constant", "Constant", 70, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
            }
                if (masterType == MasterTypes.IEC61850Client)
                {
                    ucen.lvENlist.Columns.Add("No.", "No.", 60, HorizontalAlignment.Left, 1);
                ucen.lvENlist.Columns.Add("IEDName", 50, HorizontalAlignment.Left);
                ucen.lvENlist.Columns.Add("Response Type", "Response Type", 220, HorizontalAlignment.Left, 0);
                    ucen.lvENlist.Columns.Add("Index", "Index", 250, HorizontalAlignment.Left, 1);
                    ucen.lvENlist.Columns.Add("FC", "FC", 40, HorizontalAlignment.Left, 1);
                    ucen.lvENlist.Columns.Add("Sub Index", "Sub Index", 70, HorizontalAlignment.Left, 1);
                    ucen.lvENlist.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
                    ucen.lvENlist.Columns.Add("Constant", "Constant", 70, HorizontalAlignment.Left, 1);
                    ucen.lvENlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
                //Namrata: 15/9/2017
                //Hide IED Name
                ucen.lvENlist.Columns[1].Width = 0;
            }
            


            //Add EN map headers..
            ucen.lvENMap.Columns.Add("No.", "No.", 60, HorizontalAlignment.Left, 1);
            ucen.lvENMap.Columns.Add("Reporting Index", "Reporting Index", 130, HorizontalAlignment.Left, 1);
            ucen.lvENMap.Columns.Add("Data Type", "Data Type", 130, HorizontalAlignment.Left, 0);
            ucen.lvENMap.Columns.Add("Command Type", "Command Type", 0, HorizontalAlignment.Left, 0);
            ucen.lvENMap.Columns.Add("Deadband", "Deadband", 70, HorizontalAlignment.Left, 0);
            ucen.lvENMap.Columns.Add("Multiplier", "Multiplier", 70, HorizontalAlignment.Left, 1);
            ucen.lvENMap.Columns.Add("Constant", "Constant", -2, HorizontalAlignment.Left, 1);
            ucen.lvENMap.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, 0);
        }
        public void EnableEventsIEC61850onDoublClick()
        {
            ucen.grpEN.Location = new Point(172, 52);
            ucen.lblRT.Visible = false;
            ucen.lblIdx.Visible = false;
            ucen.lblDT.Visible = false;
            ucen.cmbResponseType.Visible = false;
            ucen.cmbResponseType.Visible = false;
            ucen.txtIndex.Visible = false;
            ucen.txtIndex.Visible = false;
            ucen.cmbDataType.Visible = false;
            ucen.cmbDataType.Visible = false;
            ucen.cmbIEDName.Visible = true;
            ucen.lblIEDName.Visible = true;
            ucen.LblIndex61850.Visible = true;
            ucen.cmb61850Index.Visible = true;
            ucen.LblRespType.Visible = true;
            ucen.cmb61850ResponseType.Visible = true;
            ucen.lblfc.Visible = true;
            ucen.txtFC.Visible = true;

            ucen.lblEN.Location = new Point(14, 30);
            ucen.lblIEDName.Location = new Point(14, 55);
            ucen.LblRespType.Location = new Point(14, 81);
            ucen.LblIndex61850.Location = new Point(14, 106);
            ucen.lblfc.Location = new Point(14, 131);
            ucen.lblSIdx.Location = new Point(14, 157);
            ucen.lblM.Location = new Point(14, 182);
            ucen.lblC.Location = new Point(14, 207);
            ucen.lblDesc.Location = new Point(14, 232);
            ucen.lblAutomapNumber.Visible = false;
            ucen.txtENNo.Location = new Point(120, 27);
            ucen.cmbIEDName.Location = new Point(120, 52);
            ucen.cmb61850ResponseType.Location = new Point(120, 78);
            ucen.cmb61850Index.Location = new Point(120, 103);
            ucen.txtFC.Location = new Point(120, 128);
            ucen.txtSubIndex.Location = new Point(120, 154);
            ucen.txtMultiplier.Location = new Point(120, 179);
            ucen.txtConstant.Location = new Point(120, 204);
            ucen.txtDescription.Location = new Point(120, 229);
            ucen.txtAutoMapNumber.Visible = false;

            ucen.txtENNo.Size = new Size(300, 20);
            ucen.txtSubIndex.Size = new Size(300, 20);
            ucen.cmbIEDName.Size = new Size(300, 20);
            ucen.cmb61850ResponseType.Size = new Size(300, 20);
            ucen.cmb61850Index.Size = new Size(300, 20);
            ucen.txtFC.Size = new Size(300, 20);
            ucen.txtMultiplier.Size = new Size(300, 20);
            ucen.txtConstant.Size = new Size(300, 20);
            ucen.txtDescription.Size = new Size(300, 20);
            ucen.txtAutoMapNumber.Size = new Size(300, 20);

            ucen.btnDone.Location = new Point(170, 258);
            ucen.btnCancel.Location = new Point(260, 258);
            ucen.btnFirst.Location = new Point(90, 287);
            ucen.btnPrev.Location = new Point(180, 287);
            ucen.btnNext.Location = new Point(270, 287);
            ucen.btnLast.Location = new Point(360, 287);
            //ucen.btnDone.Location = new Point(170, 282);
            //ucen.btnCancel.Location = new Point(260, 282);
            ucen.grpEN.Size = new Size(440, 315);
            ucen.pbHdr.Width = 440;
        }
        public void EnableEventsIEC61850onLoad()
        {
            ucen.grpEN.Location = new Point(172, 52);
            ucen.lblRT.Visible = false;
            ucen.lblIdx.Visible = false;
            ucen.lblDT.Visible = false;
            ucen.cmbResponseType.Visible = false;
            ucen.cmbResponseType.Visible = false;
            ucen.txtIndex.Visible = false;
            ucen.txtIndex.Visible = false;
            ucen.cmbDataType.Visible = false;
            ucen.cmbDataType.Visible = false;
            ucen.cmbIEDName.Visible = true;
            ucen.lblIEDName.Visible = true;
            ucen.LblIndex61850.Visible = true;
            ucen.cmb61850Index.Visible = true;
            ucen.LblRespType.Visible = true;
            ucen.cmb61850ResponseType.Visible = true;
            ucen.lblfc.Visible = true;
            ucen.txtFC.Visible = true;

            ucen.lblEN.Location = new Point(14, 30);
            ucen.lblIEDName.Location = new Point(14, 55);
            ucen.LblRespType.Location = new Point(14, 81);
            ucen.LblIndex61850.Location = new Point(14, 106);
            ucen.lblfc.Location = new Point(14, 131);
            ucen.lblSIdx.Location = new Point(14, 157);
            ucen.lblM.Location = new Point(14, 182);
            ucen.lblC.Location = new Point(14, 207);
            ucen.lblDesc.Location = new Point(14, 232);
            ucen.lblAutomapNumber.Location = new Point(14, 257);


            ucen.txtENNo.Location = new Point(120, 27);
            ucen.cmbIEDName.Location = new Point(120, 52);
            ucen.cmb61850ResponseType.Location = new Point(120, 78);
            ucen.cmb61850Index.Location = new Point(120, 103);
            ucen.txtFC.Location = new Point(120, 128);
            ucen.txtSubIndex.Location = new Point(120, 154);
            ucen.txtMultiplier.Location = new Point(120, 179);
            ucen.txtConstant.Location = new Point(120, 204);
            ucen.txtDescription.Location = new Point(120, 229);
            ucen.txtAutoMapNumber.Location = new Point(120, 254);
            ucen.lblAutomapNumber.Visible = true;
            ucen.txtAutoMapNumber.Visible = true;
            ucen.lblAutomapNumber.Enabled = true;
            ucen.txtAutoMapNumber.Enabled = true;
            ucen.txtENNo.Size = new Size(300,20);
            ucen.txtSubIndex.Size = new Size(300, 20);
            ucen.cmbIEDName.Size = new Size(300, 20);
            ucen.cmb61850ResponseType.Size = new Size(300, 20);
            ucen.cmb61850Index.Size = new Size(300, 20);
            ucen.txtFC.Size = new Size(300, 20);
            ucen.txtMultiplier.Size = new Size(300, 20);
            ucen.txtConstant.Size = new Size(300, 20);
            ucen.txtDescription.Size = new Size(300, 20);
            ucen.txtAutoMapNumber.Size = new Size(300, 20);
            ucen.btnDone.Location = new Point(170, 282);
            ucen.btnCancel.Location = new Point(260, 282);
            ucen.grpEN.Size = new Size(440, 320);
            ucen.pbHdr.Width = 440;
        }
        public void EnableEventsonLoad()
        {
            ucen.grpEN.Location = new Point(172,52);
            ucen.cmbIEDName.Visible = false;
            ucen.lblIEDName.Visible = false;
            ucen.LblIndex61850.Visible = false;
            ucen.cmb61850Index.Visible = false;
            ucen.LblRespType.Visible = false;
            ucen.cmb61850ResponseType.Visible = false;
            ucen.lblfc.Visible = false;
            ucen.txtFC.Visible = false;

            ucen.lblEN.Location = new Point(14, 30);
            ucen.lblRT.Location = new Point(14, 55);
            ucen.lblIdx.Location = new Point(14, 81);
            ucen.lblSIdx.Location = new Point(14, 106);
            ucen.lblDT.Location = new Point(14, 131);
            ucen.lblM.Location = new Point(14, 157);
            ucen.lblC.Location = new Point(14, 182);
            ucen.lblDesc.Location = new Point(14, 207);
            ucen.lblAutomapNumber.Location = new Point(14, 232);

            ucen.txtENNo.Location = new Point(120, 27);
            ucen.txtENNo.Size = new Size(174, 20);
            ucen.cmbResponseType.Location = new Point(120, 52);
            ucen.cmbResponseType.Size = new Size(174, 20);
            ucen.txtIndex.Location = new Point(120, 78);
            ucen.txtIndex.Size = new Size(174, 20);
            ucen.txtSubIndex.Location = new Point(120, 103);
            ucen.txtSubIndex.Size = new Size(174, 20);
            ucen.cmbDataType.Location = new Point(120, 128);
            ucen.cmbDataType.Size = new Size(174, 20);
            ucen.txtMultiplier.Location = new Point(120, 154);
            ucen.txtMultiplier.Size = new Size(174, 20);
            ucen.txtConstant.Location = new Point(120, 179);
            ucen.txtConstant.Size = new Size(174, 20);
            ucen.txtDescription.Location = new Point(120, 204);
            ucen.txtDescription.Size = new Size(174, 20);
            ucen.txtAutoMapNumber.Location = new Point(120, 229);
            ucen.txtAutoMapNumber.Size = new Size(174, 20);
            ucen.btnDone.Location = new Point(120, 260);
            ucen.btnCancel.Location = new Point(217, 260);
            ucen.grpEN.Size = new Size(313, 300);
        }
           
        private void ShowHideSlaveColumns()
        {
            if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) Utils.getColumnHeader(ucen.lvENMap, "Command Type").Width = COL_CMD_TYPE_WIDTH;
            else Utils.getColumnHeader(ucen.lvENMap, "Command Type").Width = 0;//Hide...
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

            rootNode = xmlDoc.CreateElement("ENConfiguration");
            xmlDoc.AppendChild(rootNode);

            foreach (EN en in enList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(en.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }

            return rootNode;
        }

        public string exportXML()
        {
            XmlNode xmlNode = exportXMLnode();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 2; //default is 1.
            xmlNode.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();

            return stringWriter.ToString();
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

            rootNode = xmlDoc.CreateElement("ENMap");
            xmlDoc.AppendChild(rootNode);

            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(slaveID, out slaveENMapList))
            {
                Console.WriteLine("##### Slave entries for {0} does not exists", slaveID);
                return rootNode;
            }
            //Namarta:15/05/2018
            List<ENMap> sdimList = slaveENMapList.OrderBy(x => Convert.ToInt32(x.ENNo)).ToList();
            slaveENMapList = sdimList;
            foreach (ENMap enmn in slaveENMapList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(enmn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }

            return rootNode;
        }

        public string exportMapXML(String slaveID)
        {
            XmlNode xmlNode = exportMapXMLnode(slaveID);
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

            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(slaveID, out slaveENMapList))
            {
                Console.WriteLine("EN INI: ##### Slave entries for {0} does not exists", slaveID);
                return iniData;
            }
            //Ajay: 26/12/2018
            //if (slaveENMapList != null && slaveENMapList.Count > 0)
            //{
            //    try
            //    {
            //        var hash = new HashSet<int>();
            //        List<string> duplicateRIList = slaveENMapList.Select(x => x.ReportingIndex).ToList().Where(y => !hash.Add(Convert.ToInt32(y))).ToList();
            //        if (duplicateRIList != null && duplicateRIList.Count > 0)
            //        {
            //            MessageBox.Show("EN: Duplicate of Reporting Index (" + string.Join(",", duplicateRIList) + ") found!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //    }
            //    catch { }
            //}

            if (element == "DeadBandEN")
            {
                foreach (ENMap enmn in slaveENMapList)
                {
                    iniData += "DeadBand_" + ctr++ + "=" + Utils.GetDataTypeShortNotation(enmn.DataType) + "," + enmn.ReportingIndex + "," + enmn.Deadband + Environment.NewLine;
                }
            }
            else if (element == "EN")
            {
                foreach (ENMap enmn in slaveENMapList)
                {
                    int ri;
                    try
                    {
                        ri = Int32.Parse(enmn.ReportingIndex);
                    }
                    catch (System.FormatException)
                    {
                        ri = 0;
                    }
                    if (slaveENMapList.Where(x => x.ReportingIndex == ri.ToString()).Select(x => x).Count() > 1) //Ajay: 28/12/2018
                    {
                        MessageBox.Show("Duplicate Reporting Index (" + enmn.ReportingIndex + ") found of EN No " + enmn.ENNo, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        iniData += "AI_" + ctr++ + "=" + Utils.GenerateIndex("AI", Utils.GetDataTypeIndex(enmn.DataType), ri).ToString() + Environment.NewLine;//EN data returned as AI...
                    }
                }
            }

            return iniData;
        }
        //Ajay: 28/12/2018
        public string exportINI_DeadBandEN(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";

            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(slaveID, out slaveENMapList))
            {
                Console.WriteLine("EN INI: ##### Slave entries for {0} does not exists", slaveID);
                return iniData;
            }
            
            //Ajay: 26/12/2018
            //Ajay: 28/12/2018 Commented
            //if (slaveENMapList != null && slaveENMapList.Count > 0)
            //{
            //    try
            //    {
            //        var hash = new HashSet<int>();
            //        List<string> duplicateRIList = slaveENMapList.Select(x => x.ReportingIndex).ToList().Where(y => !hash.Add(Convert.ToInt32(y))).ToList();
            //        if (duplicateRIList != null && duplicateRIList.Count > 0)
            //        {
            //            MessageBox.Show("EN: Duplicate of Reporting Index (" + string.Join(",", duplicateRIList) + ") found!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //    }
            //    catch { }
            //}

            if (element == "DeadBandEN")
            {
                foreach (ENMap enmn in slaveENMapList)
                {
                    iniData += "DeadBand_" + ctr++ + "=" + Utils.GetDataTypeShortNotation(enmn.DataType) + "," + enmn.ReportingIndex + "," + enmn.Deadband + Environment.NewLine;
                }
            }
            else if (element == "EN")
            {
                foreach (ENMap enmn in slaveENMapList)
                {
                    int ri;
                    try
                    {
                        ri = Int32.Parse(enmn.ReportingIndex);
                    }
                    catch (System.FormatException)
                    {
                        ri = 0;
                    }
                    iniData += "AI_" + ctr++ + "=" + Utils.GenerateIndex("AI", Utils.GetDataTypeIndex(enmn.DataType), ri).ToString() + Environment.NewLine;//EN data returned as AI...
                }
            }

            return iniData;
        }
        public void changeIEC104Sequence(int oSlaveNo, int nSlaveNo)
        {
            if (oSlaveNo == nSlaveNo) return;
            Utils.ChangeKey(slavesENMapList, "IEC104_" + oSlaveNo, "IEC104_" + nSlaveNo);
            //Change radio button Tag n Text...
            foreach (Control ctrl in ucen.flpMap2Slave.Controls)
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
            Utils.ChangeKey(slavesENMapList, "MODBUSSlave_" + oSlaveNo, "MODBUSSlave_" + nSlaveNo);
            //Change radio button Tag n Text...
            foreach (Control ctrl in ucen.flpMap2Slave.Controls)
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
        public void regenerateENSequence()
        {
            string strRoutineName = "regenerateENSequence";
            try
            {
                foreach (EN ain in enList)
                {
                    int oENNo = Int32.Parse(ain.ENNo);
                    int nENNo = Globals.ENNo++;
                    ain.ENNo = nENNo.ToString();
                    List<string> ReIndexedList = new List<string>();
                    //Now Change in Map...
                    foreach (KeyValuePair<string, List<ENMap>> maps in slavesENMapList)
                    {
                        List<ENMap> sdimList = maps.Value;

                        foreach (ENMap aim in sdimList)
                        {
                            if (aim.ENNo == oENNo.ToString() && !aim.IsReindexed)
                            {
                                //Ajay: 08/08/2018 if same AO mapped again it should take same AO no on reindex. 
                                //aim.ENNo = nAINo.ToString();
                                //aim.IsReindexed = true;
                                sdimList.Where(x => x.ENNo == oENNo.ToString()).ToList().ForEach(x => { x.ENNo = nENNo.ToString(); x.IsReindexed = true; });
                                break;
                            }
                        }
                    }
                    //Now change in Parameter Load nodes...
                    Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeAISequence(oENNo, nENNo);
                }
                //Reset reindex status, for next use...
                foreach (KeyValuePair<string, List<ENMap>> maps in slavesENMapList)
                {
                    List<ENMap> saimList = maps.Value;
                    foreach (ENMap aim in saimList)
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

            List<ENMap> slaveENMapList;
            if (!slavesENMapList.TryGetValue(slaveID, out slaveENMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                return ret;
            }

            foreach (ENMap enm in slaveENMapList)
            {
                if (enm.ENNo == value.ToString()) return Int32.Parse(enm.ReportingIndex);
            }

            return ret;
        }

        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("EN_"))
            {
                //If a IEC104 slave added/deleted, reflect in UI as well as objects.
                CheckIEC104SlaveStatusChanges();
                //If a MODBUS slave added/deleted, reflect in UI as well as objects.
                CheckMODBUSSlaveStatusChanges();
                CheckIEC61850SlaveStatusChanges();
                //Namarta:13/7/2017
                CheckIEC101SlaveStatusChanges();
                ShowHideSlaveColumns();

                return ucen;
            }

            return null;
        }

        public void parseENCNode(XmlNode encNode, bool imported)
        {
            if (encNode == null)
            {
                rnName = "ENConfiguration";
                return;
            }

            //First set root node name...
            rnName = encNode.Name;

            if (encNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = encNode.Value;
            }

            foreach (XmlNode node in encNode)
            {
                //Console.WriteLine("***** node type: {0}", node.NodeType);
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                enList.Add(new EN(node, masterType, masterNo, IEDNo, imported));
            }
            refreshList();
        }

        public void parseENMNode(string slaveNum, string slaveID, XmlNode enmNode)
        {
            //SlaveID Ex. 'IEC104_1'/'MODBUSSlave_1'
            CreateNewSlave(slaveNum, slaveID, enmNode);
        }

        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }

        public int getCount()
        {
            int ctr = 0;
            foreach (EN enNode in enList)
            {
                if (enNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }

        public List<EN> getENs()
        {
            return enList;
        }
        //Namrata:27/7/2017
        public int getENMapCount()
        {
            int ctr = 0;
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
            List<ENMap> senmList;
            if (!slavesENMapList.TryGetValue(currentSlave, out senmList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                refreshMapList(null);
            }
            else
            {
                refreshMapList(senmList);
            }
            if (senmList == null)
            {
                return 0;
            }
            else
            {
                foreach (ENMap asaa in senmList)
                {
                    if (asaa.IsNodeComment) continue;
                    ctr++;
                }
            }
            return ctr;
        }
        public List<ENMap> getSlaveENMaps(string slaveID)
        {
            List<ENMap> slaveENMapList;
            slavesENMapList.TryGetValue(slaveID, out slaveENMapList);
            return slaveENMapList;
        }
        public class ListViewItemComparer : IComparer
        {
            private int col;
            private SortOrder order;
            private int dataType = 0;
            public ListViewItemComparer()
            {
                col = 0;
                order = SortOrder.Ascending;
            }
            public ListViewItemComparer(int column, SortOrder order, int ColType)
            {
                col = column;
                this.order = order;
                dataType = ColType;
            }
            public int Compare(object x, object y)
            {
                int returnVal = -1;
                if (dataType == 0)
                {
                    //string
                    returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                }
                else if (dataType == 1)
                {
                    //int
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
    }
}


