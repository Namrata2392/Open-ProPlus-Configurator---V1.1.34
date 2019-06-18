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
    * \brief     <b>DOConfiguration</b> is a class to store info about all DO's and there corresponding mapping infos.
    * \details   This class stores info related to all DO's and there corresponding mapping's for various slaves. It allows
    * user to add multiple DO's. The user can map this DO's to various slaves created in the system, along with the mapping parameters
    * for those slave types. It also exports the XML node related to this object.
    * 
    */
    public class DOConfiguration
    {
        #region Declaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private RCBConfiguration RCBNode = null;
        private string rnName = "";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private Mode mapMode = Mode.NONE;
        private int mapEditIndex = -1;
        private bool isNodeComment = false;
        private string comment = "";
        private string currentSlave = "";
        Dictionary<string, List<DOMap>> slavesDOMapList = new Dictionary<string, List<DOMap>>();
        private MasterTypes masterType = MasterTypes.UNKNOWN;
        private int masterNo = -1;
        private int IEDNo = -1;
        List<DO> doList = new List<DO>();
        ucDOlist ucdo = new ucDOlist();
        private const int COL_CMD_TYPE_WIDTH = 130;
        private const int COL_SELECT_TYPE_WIDTH = 80;
        //Namrata: 11/09/2017
        //Fill RessponseType in All Configuration . 
        public DataGridView dataGridViewDataSet = new DataGridView();
        public DataTable dtdataset = new DataTable();
        DataRow datasetRow;
        private string Response = "";
        private string ied = "";
        #endregion Declaration
        public DOConfiguration(MasterTypes mType, int mNo, int iNo)
        {
            masterType = mType;
            masterNo = mNo;
            IEDNo = iNo;
            ucdo.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucdo.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucdo.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucdo.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucdo.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            ucdo.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            ucdo.btnNextClick += new System.EventHandler(this.btnNext_Click);
            ucdo.btnLastClick += new System.EventHandler(this.btnLast_Click);
            ucdo.LinkDeleteConfigueClick += new System.EventHandler(this.LinkDeleteConfigue_Click);
            ucdo.linkDoMapClick += new System.EventHandler(this.linkDoMap_Click);
            ucdo.cmbIEDName.SelectedIndexChanged += new System.EventHandler(this.cmbIEDName_SelectedIndexChanged);
            ucdo.cmb61850DIResponseType.SelectedIndexChanged += new System.EventHandler(this.cmb61850DIResponseType_SelectedIndexChanged);
            ucdo.cmb61850DIIndex.SelectedIndexChanged += new System.EventHandler(this.cmb61850DIIndex_SelectedIndexChanged);
            //ucdo.lvDOlistSelectedIndexChanged += new System.EventHandler(this.lvDOlist_SelectedIndexChanged); //---For Alternate Colour
            //ucdo.lvDOMapSelectedIndexChanged += new System.EventHandler(this.lvDOMap_SelectedIndexChanged); //---For Alternate Colour
            ucdo.lvDOlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOlist_ItemSelectionChanged);
            //Namarta : 27/7/2017
            ucdo.lvDOMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOMap_ItemSelectionChanged);
            if (mType == MasterTypes.Virtual)//Disable add/edit/delete/dblclick n remove checkboxes...
            {
                EnableEventsVirtualOnLoad();
            }
            else
            {
                ucdo.lvDOlistDoubleClick += new System.EventHandler(this.lvDOlist_DoubleClick);
            }
            if (mType == MasterTypes.IEC61850Client)
            {
                EnableEventsIEC61850OnLoad();
            }
            if (mType == MasterTypes.IEC103)
            {
                EnableEventsADRMODBUS101103OnLoad();
            }
            if (mType == MasterTypes.MODBUS)
            {
                EnableEventsADRMODBUS101103OnLoad();
            }
            if (mType == MasterTypes.ADR)
            {
                EnableEventsADRMODBUS101103OnLoad();
            }
            if (mType == MasterTypes.IEC101)
            {
                EnableEventsADRMODBUS101103OnLoad();
            }
            ucdo.btnDOMDeleteClick += new System.EventHandler(this.btnDOMDelete_Click);
            ucdo.btnDOMDoneClick += new System.EventHandler(this.btnDOMDone_Click);
            ucdo.btnDOMCancelClick += new System.EventHandler(this.btnDOMCancel_Click);
            ucdo.lvDOMapDoubleClick += new System.EventHandler(this.lvDOMap_DoubleClick);
            addListHeaders();
            fillOptions();
        }
        public void EnableEventsVirtualOnLoad()
        {
            ucdo.btnAdd.Enabled = false;
            ucdo.btnDelete.Enabled = false;
            //Namrata: 12/09/2017
            ucdo.lblIEDName.Visible = false;
            ucdo.LblRespType.Visible = false;
            ucdo.LblIndex61850.Visible = false;
            ucdo.cmbIEDName.Visible = false;
            ucdo.cmb61850DIIndex.Visible = false;
            ucdo.cmb61850DIResponseType.Visible = false;
            //Namrata: 12/09/2017
            ucdo.lblIEDName.Visible = false;
            ucdo.LblRespType.Visible = false;
            ucdo.LblIndex61850.Visible = false;
            ucdo.cmbIEDName.Visible = false;
            ucdo.cmb61850DIIndex.Visible = false;
            ucdo.cmb61850DIResponseType.Visible = false;
            ucdo.lblAutoMapNumber.Location = new Point(11, 211);
            ucdo.txtAutoMapNumber.Location = new Point(121, 213);
            ucdo.btnDone.Location = new Point(102, 279);
            ucdo.btnCancel.Location = new Point(192, 279);
            ucdo.btnFirst.Location = new Point(10, 312);
            ucdo.btnPrev.Location = new Point(81, 312);
            ucdo.btnNext.Location = new Point(160, 312);
            ucdo.btnLast.Location = new Point(231, 312);
            ucdo.grpDO.Location = new Point(172, 52);
        }
        public void EnableEventsADRMODBUS101103OnLoad()
        {
            ucdo.lblAutoMapNumber.Visible = true;
            ucdo.txtAutoMapNumber.Visible = true;
            ucdo.lblAutoMapNumber.Enabled = true;
            ucdo.txtAutoMapNumber.Enabled = true;
            ucdo.lblIEDName.Visible = false;
            ucdo.LblRespType.Visible = false;
            ucdo.LblIndex61850.Visible = false;
            ucdo.cmbIEDName.Visible = false;
            ucdo.cmb61850DIIndex.Visible = false;
            ucdo.cmb61850DIResponseType.Visible = false;
            ucdo.lblDescription.Location = new Point(12, 188);
            ucdo.txtDescription.Location = new Point(123, 185);
            ucdo.lblEnableDI.Location = new Point(12, 214);
            ucdo.txtEnableDI.Location = new Point(123, 211);
            ucdo.lblAutoMapNumber.Location = new Point(12, 240);
            ucdo.txtAutoMapNumber.Location = new Point(123, 237);
            ucdo.btnDone.Location = new Point(123, 265);
            ucdo.btnCancel.Location = new Point(208, 265);
            ucdo.grpDO.Location = new Point(172, 52); ;
            ucdo.grpDO.Height = 305;
        }
        public void EnableEventsOnDoubleClick()
        {
            ucdo.lblIEDName.Visible = false;
            ucdo.LblRespType.Visible = false;
            ucdo.LblIndex61850.Visible = false;
            ucdo.cmbIEDName.Visible = false;
            ucdo.cmb61850DIIndex.Visible = false;
            ucdo.cmb61850DIResponseType.Visible = false;
            ucdo.lblDescription.Location = new Point(12, 188);
            ucdo.txtDescription.Location = new Point(123, 185);
            ucdo.lblEnableDI.Location = new Point(12, 214);
            ucdo.txtEnableDI.Location = new Point(123, 211);
            //ucdo.lblDescription.Location = new Point(12, 191);
            //ucdo.txtDescription.Location = new Point(122, 191);
            ucdo.lblAutoMapNumber.Visible = false;
            ucdo.txtAutoMapNumber.Visible = false;
            //ucdo.lblEnableDI.Location = new Point(12, 218);
            //ucdo.txtEnableDI.Location = new Point(122, 218);
            //ucdo.lblEnableDI2.Location = new Point(12, 245);
            //ucdo.txtEnableDI2.Location = new Point(122, 245);
            ucdo.btnDone.Location = new Point(123, 238);
            ucdo.btnCancel.Location = new Point(208, 238);
            ucdo.btnFirst.Location = new Point(12, 270);
            ucdo.btnPrev.Location = new Point(82, 270);
            ucdo.btnNext.Location = new Point(152, 270);
            ucdo.btnLast.Location = new Point(222, 270);
            ucdo.grpDO.Location = new Point(172, 52);
            ucdo.grpDO.Height = 300;
        }
        public void EnableEventsIEC61850OnLoad()
        {
            ucdo.lblAutoMapNumber.Visible = true;
            ucdo.txtAutoMapNumber.Visible = true;
            ucdo.lblAutoMapNumber.Enabled = true;
            ucdo.txtAutoMapNumber.Enabled = true;

            //Lable: DONo and TxtDONO
            ucdo.lblDON.Location = new Point(12, 36);
            ucdo.txtDONo.Size = new Size(300, 21);
            ucdo.txtDONo.Location = new Point(123, 33);

            ucdo.lblIEDName.Location = new Point(12, 61);
            ucdo.cmbIEDName.Location = new Point(123, 58);
            ucdo.cmbIEDName.Size = new Size(300, 21);

            ucdo.LblRespType.Location = new Point(12, 87);
            ucdo.cmb61850DIResponseType.Location = new Point(123, 84);
            ucdo.cmb61850DIResponseType.Size = new Size(300, 21);

            ucdo.LblIndex61850.Location = new Point(12, 112);
            ucdo.cmb61850DIIndex.Location = new Point(123, 109);
            ucdo.cmb61850DIIndex.Size = new Size(300, 21);

            ucdo.lblfc.Location = new Point(12, 137);
            ucdo.txtFC.Location = new Point(123, 134);
            ucdo.txtFC.Size = new Size(300, 21);
            ucdo.txtFC.Enabled = false;

            ucdo.lblSIdx.Location = new Point(12, 163);
            ucdo.txtSubIndex.Size = new Size(300, 21);
            ucdo.txtSubIndex.Location = new Point(123, 160);

            ucdo.lblCT.Location = new Point(12, 188);
            ucdo.cmbControlType.Size = new Size(300, 21);
            ucdo.cmbControlType.Location = new Point(123, 185);


            ucdo.lblPD.Location = new Point(12, 214);
            ucdo.txtPulseDuration.Location = new Point(123, 211);
            ucdo.txtPulseDuration.Size = new Size(300, 21);

            ucdo.lblEnableDI.Location = new Point(12, 240);
            ucdo.txtEnableDI.Location = new Point(123, 237);
            ucdo.txtEnableDI.Size = new Size(300, 21);

            ucdo.lblDescription.Location = new Point(12, 240);
            ucdo.txtDescription.Location = new Point(123, 237);
            ucdo.txtDescription.Size = new Size(300, 21);

            ucdo.lblDescription.Location = new Point(12, 266);
            ucdo.txtDescription.Location = new Point(123, 263);
            ucdo.txtDescription.Size = new Size(300, 21);

            ucdo.lblAutoMapNumber.Location = new Point(12, 291);
            ucdo.txtAutoMapNumber.Location = new Point(123, 288);
            ucdo.txtAutoMapNumber.Size = new Size(300, 21);

            ucdo.lblRT.Visible = false;
            ucdo.cmbResponseType.Visible = false;
            ucdo.lblIdx.Visible = false;
            ucdo.txtIndex.Visible = false;

            ucdo.btnDone.Location = new Point(190, 315);
            ucdo.btnCancel.Location = new Point(280, 315);
           
            ucdo.grpDO.Size = new Size(530, 355);
            ucdo.grpDO.Width = 440;
            ucdo.grpDO.Location = new Point(172, 52);
            ucdo.pbHdr.Width = 530;
        }
        public void Event61850OnDoubleClick()
        {
            ucdo.lblAutoMapNumber.Visible = true;
            ucdo.txtAutoMapNumber.Visible = true;
            ucdo.lblAutoMapNumber.Enabled = true;
            ucdo.txtAutoMapNumber.Enabled = true;

            ////Lable: DONo and TxtDONO
            //ucdo.lblDON.Location = new Point(12, 36);
            //ucdo.txtDONo.Size = new Size(300, 21);
            //ucdo.txtDONo.Location = new Point(123, 33);

            //ucdo.lblSIdx.Location = new Point(12, 61);
            //ucdo.txtSubIndex.Location = new Point(123, 58);
            //ucdo.txtSubIndex.Size = new Size(300, 21);

            //ucdo.lblCT.Location = new Point(12, 87);
            //ucdo.cmbControlType.Location = new Point(123, 84);
            //ucdo.cmbControlType.Size = new Size(300, 21);

            //ucdo.lblPD.Location = new Point(12, 112);
            //ucdo.txtPulseDuration.Location = new Point(123, 109);
            //ucdo.txtPulseDuration.Size = new Size(300, 21);

            //ucdo.lblIEDName.Location = new Point(12, 137);
            //ucdo.cmbIEDName.Location = new Point(123, 134);
            //ucdo.cmbIEDName.Size = new Size(300, 21);

            //ucdo.LblRespType.Location = new Point(12, 163);
            //ucdo.cmb61850DIResponseType.Size = new Size(300, 21);
            //ucdo.cmb61850DIResponseType.Location = new Point(123, 160);

            //ucdo.LblIndex61850.Location = new Point(12, 188);
            //ucdo.cmb61850DIIndex.Size = new Size(300, 21);
            //ucdo.cmb61850DIIndex.Location = new Point(123, 185);

            //ucdo.lblfc.Location = new Point(12, 214);
            //ucdo.txtFC.Location = new Point(123, 211);
            //ucdo.txtFC.Size = new Size(300, 21);
            //ucdo.txtFC.Enabled = false;

            //ucdo.lblDescription.Location = new Point(12, 240);
            //ucdo.txtDescription.Location = new Point(123, 237);
            //ucdo.txtDescription.Size = new Size(300, 21);

            //ucdo.lblEnableDI.Location = new Point(12, 266);
            //ucdo.txtEnableDI.Location = new Point(123, 263);
            //ucdo.txtEnableDI.Size = new Size(300, 21);
            //Lable: DONo and TxtDONO
            ucdo.lblDON.Location = new Point(12, 36);
            ucdo.txtDONo.Size = new Size(300, 21);
            ucdo.txtDONo.Location = new Point(123, 33);

            ucdo.lblIEDName.Location = new Point(12, 61);
            ucdo.cmbIEDName.Location = new Point(123, 58);
            ucdo.cmbIEDName.Size = new Size(300, 21);

            ucdo.LblRespType.Location = new Point(12, 87);
            ucdo.cmb61850DIResponseType.Location = new Point(123, 84);
            ucdo.cmb61850DIResponseType.Size = new Size(300, 21);

            ucdo.LblIndex61850.Location = new Point(12, 112);
            ucdo.cmb61850DIIndex.Location = new Point(123, 109);
            ucdo.cmb61850DIIndex.Size = new Size(300, 21);

            ucdo.lblfc.Location = new Point(12, 137);
            ucdo.txtFC.Location = new Point(123, 134);
            ucdo.txtFC.Size = new Size(300, 21);
            ucdo.txtFC.Enabled = false;

            ucdo.lblSIdx.Location = new Point(12, 163);
            ucdo.txtSubIndex.Size = new Size(300, 21);
            ucdo.txtSubIndex.Location = new Point(123, 160);

            ucdo.lblCT.Location = new Point(12, 188);
            ucdo.cmbControlType.Size = new Size(300, 21);
            ucdo.cmbControlType.Location = new Point(123, 185);


            ucdo.lblPD.Location = new Point(12, 214);
            ucdo.txtPulseDuration.Location = new Point(123, 211);
            ucdo.txtPulseDuration.Size = new Size(300, 21);

            ucdo.lblEnableDI.Location = new Point(12, 240);
            ucdo.txtEnableDI.Location = new Point(123, 237);
            ucdo.txtEnableDI.Size = new Size(300, 21);

            ucdo.lblDescription.Location = new Point(12, 240);
            ucdo.txtDescription.Location = new Point(123, 237);
            ucdo.txtDescription.Size = new Size(300, 21);

            ucdo.lblDescription.Location = new Point(12, 266);
            ucdo.txtDescription.Location = new Point(123, 263);
            ucdo.txtDescription.Size = new Size(300, 21);

            ucdo.lblAutoMapNumber.Visible = false;
            ucdo.txtAutoMapNumber.Visible = false;

            ucdo.lblRT.Visible = false;
            ucdo.cmbResponseType.Visible = false;
            ucdo.lblIdx.Visible = false;
            ucdo.txtIndex.Visible = false;

            ucdo.btnDone.Location = new Point(190, 290);
            ucdo.btnCancel.Location = new Point(280, 290);
            ucdo.btnFirst.Location = new Point(90, 320);
            ucdo.btnPrev.Location = new Point(180, 320);
            ucdo.btnNext.Location = new Point(270, 320);
            ucdo.btnLast.Location = new Point(360, 320);
            ucdo.grpDO.Size = new Size(530, 345); ucdo.grpDO.Width = 440;
            ucdo.grpDO.Location = new Point(172, 52);
            ucdo.pbHdr.Width = 530;
        }
        private int SelectedIndex;
        private void lvDOlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color GreenColour = Color.FromArgb(82, 208, 23);
            if (ucdo.lvDOlist.SelectedIndices.Count > 0)
            {
                SelectedIndex = Convert.ToInt32(ucdo.lvDOlist.SelectedItems[0].Text);
                ucdo.lvDOMap.SelectedItems.Clear();
                ucdo.lvDOMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucdo.lvDOMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucdo.lvDOMap.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                ucdo.lvDOlist.SelectedItems.Clear();
                ucdo.lvDOlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucdo.lvDOlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucdo.lvDOlist.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
            }
        }

        private void lvDOMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color GreenColour = Color.FromArgb(82, 208, 23);
            if (ucdo.lvDOMap.SelectedIndices.Count > 0)
            {
                SelectedIndex = Convert.ToInt32(ucdo.lvDOMap.SelectedItems[0].Text);
                ucdo.lvDOlist.SelectedItems.Clear();
                ucdo.lvDOlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucdo.lvDOlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucdo.lvDOlist.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);

                ucdo.lvDOMap.SelectedItems.Clear();
                ucdo.lvDOMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                ucdo.lvDOMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                ucdo.lvDOMap.Items.Cast<ListViewItem>().Where(x => x.Text == SelectedIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
            }
        }
        private void FetchComboboxData()
        {
            //Namrata: 13/03/2018
            ucdo.cmbIEDName.DataSource = null;
            List<string> tblNameList = Utils.dsIED.Tables.OfType<DataTable>().Select(tbl => tbl.TableName).ToList();
            string tblName = tblNameList.Where(x => x.Contains(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client)).Select(x => x).FirstOrDefault();
            //Namrata: 26/04/2018
            if (tblName != null)
            {
                ucdo.cmbIEDName.DataSource = Utils.dsIED.Tables[tblName];
                ucdo.cmbIEDName.DisplayMember = "IEDName";
                //Namrata: 21/03/2018
                ucdo.cmb61850DIResponseType.DataSource = Utils.dsResponseType.Tables[tblName];
                ucdo.cmb61850DIResponseType.DisplayMember = "Address";
                //Namrata: 29/03/2018
                ucdo.cmb61850DIIndex.DataSource = Utils.DsAllConfigurationData.Tables[tblName + "_On Request"];
                ucdo.cmb61850DIIndex.DisplayMember = "ObjectReferrence";
                ucdo.cmb61850DIIndex.ValueMember = "Node";
            }
            else
            {
                //MessageBox.Show("ICD File Missing !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }
        }
            
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (doList.Count >= getMaxDOs())
            {
                MessageBox.Show("Maximum " + getMaxDOs() + " DO's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Console.WriteLine("*** ucdo btnAdd_Click clicked in class!!!");
            mode = Mode.ADD;
            editIndex = -1;
            if (masterType == MasterTypes.IEC61850Client)
            {
                EnableEventsIEC61850OnLoad();
                FetchComboboxData();
            }
            else if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS))
            {
                EnableEventsADRMODBUS101103OnLoad();
            }
            else if (masterType == MasterTypes.Virtual)
            {
                EnableEventsVirtualOnLoad();
            }
            Utils.resetValues(ucdo.grpDO);
            Utils.showNavigation(ucdo.grpDO, false);
            loadDefaults();
            ucdo.grpDO.Visible = true;
            ucdo.cmbResponseType.Focus();
            //Namrata: 04/04/2018
            if (masterType == MasterTypes.IEC61850Client)
            {
                if (ucdo.cmbIEDName.SelectedIndex != -1)
                {
                    ucdo.cmbIEDName.SelectedIndex = ucdo.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                    ucdo.txtFC.Text = ((DataRowView)ucdo.cmb61850DIIndex.SelectedItem).Row[2].ToString();
                    //Namrata: 10/04/2018
                    if ((ucdo.cmb61850DIIndex.Text.ToString() == "") || (ucdo.cmb61850DIResponseType.Text.ToString() == ""))
                    {
                        MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    ucdo.cmbIEDName.Visible = false;
                    MessageBox.Show("ICD File Missing !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        private void LinkDeleteConfigue_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listItem in ucdo.lvDOlist.Items)
            {
                listItem.Checked = true;
            }
            Console.WriteLine("*** ucdo btnDelete_Click clicked in class!!!");
            Console.WriteLine("*** doList count: {0} lv count: {1}", doList.Count, ucdo.lvDOlist.Items.Count);
            DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                foreach (ListViewItem listItem in ucdo.lvDOlist.Items)
                {
                    listItem.Checked = false;
                }
                return;
            }
            for (int i = ucdo.lvDOlist.Items.Count - 1; i >= 0; i--)
            {
                Console.WriteLine("*** removing indices: {0}", i);
                if (Utils.IsExistDOinPLC(doList.ElementAt(i).DONo))
                {
                    DialogResult ask = MessageBox.Show("DO " + doList.ElementAt(i).DONo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (ask == DialogResult.No)
                    {
                        continue;
                    }
                    Utils.DeleteDOFromPLC(doList.ElementAt(i).DONo);
                }
                deleteDOFromMaps(doList.ElementAt(i).DONo);
                doList.RemoveAt(i);
                ucdo.lvDOlist.Items.Clear();
            }
            Console.WriteLine("*** doList count: {0} lv count: {1}", doList.Count, ucdo.lvDOlist.Items.Count);
            refreshList();
            //Refresh map listview...
            refreshCurrentMapList();
        }
        private void linkDoMap_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucdo btnDOMDelete_Click clicked in class!!!");
            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(currentSlave, out slaveDOMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error deleting DO map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (ListViewItem listItem in ucdo.lvDOMap.Items)
            {
                listItem.Checked = true;
            }
            DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                foreach (ListViewItem listItem in ucdo.lvDOMap.Items)
                {
                    listItem.Checked = false;
                }
                return;
            }
            for (int i = ucdo.lvDOMap.Items.Count - 1; i >= 0; i--)
            {

                Console.WriteLine("*** removing indices: {0}", i);
                slaveDOMapList.RemoveAt(i);
                ucdo.lvDOMap.Items.Clear();
            }
            Console.WriteLine("*** slaveDOMapList count: {0} lv count: {1}", slaveDOMapList.Count, ucdo.lvDOMap.Items.Count);
            refreshMapList(slaveDOMapList);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucdo btnDelete_Click clicked in class!!!");
            Console.WriteLine("*** doList count: {0} lv count: {1}", doList.Count, ucdo.lvDOlist.Items.Count);
            DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                return;
            }

            for (int i = ucdo.lvDOlist.Items.Count - 1; i >= 0; i--)
            {
                if (ucdo.lvDOlist.Items[i].Checked)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    if (Utils.IsExistDOinPLC(doList.ElementAt(i).DONo))
                    {
                        DialogResult ask = MessageBox.Show("DO " + doList.ElementAt(i).DONo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (ask == DialogResult.No)
                        {
                            continue;
                        }
                        Utils.DeleteDOFromPLC(doList.ElementAt(i).DONo);
                    }
                    deleteDOFromMaps(doList.ElementAt(i).DONo);
                    doList.RemoveAt(i);
                    ucdo.lvDOlist.Items[i].Remove();
                }
            }
            Console.WriteLine("*** doList count: {0} lv count: {1}", doList.Count, ucdo.lvDOlist.Items.Count);
            refreshList();
            //Refresh map listview...
            refreshCurrentMapList();


            //var LitsItemsChecked = new List<KeyValuePair<int, int>>();
            //int arraycnt = 0;
            //string SubitemText;
            //int finIndex;
            //Console.WriteLine("*** ucdo btnDelete_Click clicked in class!!!");
            //Console.WriteLine("*** doList count: {0} lv count: {1}", doList.Count, ucdo.lvDOlist.Items.Count);
            //DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            //if (result == DialogResult.No)
            //{
            //    return;
            //}
            //foreach (ListViewItem lvi in ucdo.lvDOlist.Items)
            //{
            //    SubitemText = lvi.SubItems[2].Text;
            //    finIndex = lvi.Index;
            //    if (lvi.Checked == true)
            //    {
            //        arraycnt = arraycnt + 1;
            //        //Key count,Value Repotingindex
            //        LitsItemsChecked.Add(new KeyValuePair<int, int>(Convert.ToInt32(lvi.Text), Convert.ToInt32(SubitemText)));
            //        if (Utils.IsExistDIinPLC(doList.ElementAt(finIndex).DONo))
            //        {
            //            DialogResult ask = MessageBox.Show("DO " + doList.ElementAt(finIndex).DONo + " is referred in ParameterLoadConfiguration and all the references will also be deleted." + Environment.NewLine + "Do you want to continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //            if (ask == DialogResult.No)
            //            {
            //                continue;
            //            }
            //            Utils.DeleteDIFromPLC(doList.ElementAt(finIndex).DONo);
            //        }
            //        deleteDOFromMaps(doList.ElementAt(finIndex).DONo);
            //        doList.RemoveAt(finIndex);
            //        ucdo.lvDOlist.Items[finIndex].Remove();
            //    }
            //}
            //refreshList1(doList, LitsItemsChecked[0].Key, LitsItemsChecked[0].Value);
            //refreshList();
            //refreshCurrentMapList();//Refresh map listview...
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            int EnableDI = 0;
            //Namrata:30/6/2017
            int txtno = int.Parse(ucdo.txtAutoMapNumber.Text);
            //if (!Validate()) return;
            Console.WriteLine("*** ucai btnDone_Click clicked in class!!!");
            List<KeyValuePair<string, string>> doData = Utils.getKeyValueAttributes(ucdo.grpDO);

            //Namrata: 27/09/2017
            //fill Address to Datatable for RCBConfiguration 
            if (masterType == MasterTypes.IEC61850Client)
            {
                //Response = ucdo.cmb61850DIResponseType.Text;
                //datasetRow = dtdataset.NewRow();
                //datasetRow["Address"] = Response.ToString(); datasetRow["IED"] = IEDNo.ToString();
                //dtdataset.Rows.Add(datasetRow);
                //Utils.dtDataSet = dtdataset;
                //Utils.dtDORCB = dtdataset;
                ////Namrata: 23/10/2017
                //Utils.dtDataSet.Merge(Utils.dtAIRCB, false, MissingSchemaAction.Add);
                //Utils.dtDataSet.Merge(Utils.dtDIRCB, false, MissingSchemaAction.Add);

                Response = ucdo.cmb61850DIResponseType.Text;
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
                //Utils.dtDataSet = dtdataset;

                //Namrata: 15/03/2018
                dataGridViewDataSet.DataSource = dtdataset;
                dtdataset.TableName = Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname;
                string Index112 = "";
                string[] arr112 = new string[dtdataset.Rows.Count];
                string[] arrCo1l12 = new string[dtdataset.Rows.Count];
                DataRow row112;
                if (Utils.dsRCBDO.Tables.Contains(dtdataset.TableName))
                {
                    Utils.dsRCBDO.Tables[dtdataset.TableName].Clear();
                }
                else
                {
                    Utils.dsRCBDO.Tables.Add(dtdataset.TableName);
                    Utils.dsRCBDO.Tables[dtdataset.TableName].Columns.Add("ObjectReferrence");
                    Utils.dsRCBDO.Tables[dtdataset.TableName].Columns.Add("Node");
                }
                for (int i = 0; i < dtdataset.Rows.Count; i++)
                {
                    row112 = Utils.dsRCBDO.Tables[dtdataset.TableName].NewRow();
                    Utils.dsRCBDO.Tables[dtdataset.TableName].NewRow();
                    for (int j = 0; j < dtdataset.Columns.Count; j++)
                    {
                        Index112 = dtdataset.Rows[i][j].ToString();
                        row112[j] = Index112.ToString();
                    }
                    Utils.dsRCBDO.Tables[dtdataset.TableName].Rows.Add(row112);
                }
                Utils.dsRCBData = Utils.dsRCBDO;
                Utils.dsRCBData.Merge(Utils.dsRCBAI, false, MissingSchemaAction.Add);
                Utils.dsRCBData.Merge(Utils.dsRCBAO, false, MissingSchemaAction.Add);
                Utils.dsRCBData.Merge(Utils.dsRCBDI, false, MissingSchemaAction.Add);
                Utils.dsRCBData.Merge(Utils.dsRCBEN, false, MissingSchemaAction.Add);
            }

            if (mode == Mode.ADD)
            {
                int intStart = Convert.ToInt32(doData[10].Value); // DONo
                int intRange = Convert.ToInt32(doData[6].Value); //AutoMapRange
                int intDOIndex = Convert.ToInt32(doData[12].Value); // DOIndex
                int DONumber1 = 0, DOINdex1 = 0;

                string DIDescription = "";
                //Ajay: 23/11/2017
                if (intRange > getMaxDOs())
                {
                    MessageBox.Show("Maximum " + getMaxDOs() + " DO's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    for (int i = intStart; i <= intStart + intRange - 1; i++)
                    {
                        DONumber1 = Globals.DONo;
                        DONumber1 += 1;
                        DOINdex1 = intDOIndex++;
                        EnableDI = Convert.ToInt32(ucdo.txtEnableDI.Text);
                        if (EnableDI == 0)
                        {
                            EnableDI = Convert.ToInt32(ucdo.txtEnableDI.Text);
                        }
                        else if (!Utils.EnableDI.Where(x => x.DINo == EnableDI.ToString()).Select(x => x.DINo).Any()) //.ToList().Contains(EnableDI.ToString()))
                        {
                            MessageBox.Show("DI entry does not exist !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (masterType == MasterTypes.ADR)
                        {
                            DIDescription = ucdo.txtDescription.Text;
                        }
                        else if (masterType == MasterTypes.IEC101)
                        {
                            DIDescription = ucdo.txtDescription.Text;
                        }
                        if (masterType == MasterTypes.IEC103)
                        {
                            DIDescription = ucdo.txtDescription.Text;
                        }
                        else if (masterType == MasterTypes.MODBUS)
                        {
                            DIDescription = ucdo.txtDescription.Text;
                        }
                        else if (masterType == MasterTypes.IEC61850Client)
                        {
                            DIDescription = ucdo.txtDescription.Text;
                        }
                        DO NewDO = new DO("DO", doData, null, masterType, masterNo, IEDNo);
                        NewDO.DONo = DONumber1.ToString();
                        NewDO.Index = DOINdex1.ToString();
                        NewDO.IEDName = ucdo.cmbIEDName.Text.ToString();
                        NewDO.IEC61850ResponseType = ucdo.cmb61850DIResponseType.Text.ToString();
                        NewDO.IEC61850Index = ucdo.cmb61850DIIndex.Text.ToString();
                        NewDO.EnableDI = EnableDI.ToString();
                        //Namrata: 10/04/2018
                        if (masterType == MasterTypes.IEC61850Client)
                        {
                            if ((ucdo.cmb61850DIIndex.Text.ToString() == "") || (ucdo.cmb61850DIResponseType.Text.ToString() == ""))
                            {
                                MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        doList.Add(NewDO);
                    }
                }
            }
            else if (mode == Mode.EDIT)
            {
                doList[editIndex].updateAttributes(doData);
                EnableDI = Convert.ToInt32(ucdo.txtEnableDI.Text);

                if (EnableDI == 0)
                {
                    EnableDI = Convert.ToInt32(ucdo.txtEnableDI.Text);
                }
                else if (!Utils.EnableDI.Where(x => x.DINo == EnableDI.ToString()).Select(x => x.DINo).Any()) //.ToList().Contains(EnableDI.ToString()))
                {
                    MessageBox.Show("DI entry does not exist !!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } //Namrata: 10/04/2018
                if (masterType == MasterTypes.IEC61850Client)
                {
                    if ((ucdo.cmb61850DIIndex.Text.ToString() == "") || (ucdo.cmb61850DIResponseType.Text.ToString() == ""))
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
                ucdo.grpDO.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucdo btnCancel_Click clicked in class!!!");
            ucdo.grpDO.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
            Utils.resetValues(ucdo.grpDO);
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucdo btnFirst_Click clicked in class!!!");
            if (ucdo.lvDOlist.Items.Count <= 0) return;
            if (doList.ElementAt(0).IsNodeComment) return;
            editIndex = 0;
            loadValues();
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            //Namrata:27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucdo btnPrev_Click clicked in class!!!");
            if (editIndex - 1 < 0) return;
            if (doList.ElementAt(editIndex - 1).IsNodeComment) return;
            editIndex--;
            loadValues();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            //Namrata:27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucdo btnNext_Click clicked in class!!!");
            if (editIndex + 1 >= ucdo.lvDOlist.Items.Count) return;
            if (doList.ElementAt(editIndex + 1).IsNodeComment) return;
            editIndex++;
            loadValues();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucdo btnLast_Click clicked in class!!!");
            if (ucdo.lvDOlist.Items.Count <= 0) return;
            if (doList.ElementAt(doList.Count - 1).IsNodeComment) return;
            editIndex = doList.Count - 1;
            loadValues();
        }
        private void lvDOlist_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucdo lvDOlist_DoubleClick clicked in class!!!");
            //Namrata: 10/09/2017
            ucdo.txtAutoMapNumber.Text = "0";
            ucdo.txtAutoMapNumber.Enabled = false;
            ucdo.lblAutoMapNumber.Enabled = false;

            //Namrata: 07/03/2018
            int ListIndex = ucdo.lvDOlist.FocusedItem.Index;
            ListViewItem lvi = ucdo.lvDOlist.Items[ListIndex];

            //if (ucdo.lvDOlist.SelectedItems.Count <= 0) return;
            //ListViewItem lvi = ucdo.lvDOlist.SelectedItems[0];
            Utils.UncheckOthers(ucdo.lvDOlist, lvi.Index);
            if (doList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (masterType == MasterTypes.IEC61850Client)
            {
                Event61850OnDoubleClick();
                FetchComboboxData();
                //Namrata: 04/04/2018
                ucdo.cmbIEDName.SelectedIndex = ucdo.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
                ucdo.txtFC.Text = ((DataRowView)ucdo.cmb61850DIIndex.SelectedItem).Row[2].ToString();

                if ((ucdo.cmb61850DIIndex.Text.ToString() == "") || (ucdo.cmb61850DIResponseType.Text.ToString() == ""))
                {
                    MessageBox.Show("Fields cannot empty", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS))
            {
                EnableEventsOnDoubleClick();
            }
            ucdo.grpDO.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            Utils.showNavigation(ucdo.grpDO, true);
            loadValues();
            ucdo.cmbResponseType.Focus();
        }
        private void lvDOlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string strRoutineName = "lvDOlist_ItemSelectionChanged";
            try
            {
                if (e.IsSelected)
                {
                    Color GreenColour = Color.FromArgb(34, 217, 0);
                    string diIndex = e.Item.Text;
                    Console.WriteLine("*** selected DI: {0}", diIndex);
                    //Namrata: 27/7/2017
                    ucdo.lvDOMapItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOMap_ItemSelectionChanged);
                    ucdo.lvDOMap.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucdo.lvDOMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucdo.lvDOMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucdo.lvDOMap);
                    //Namrata: 27/7/2017
                    ucdo.lvDOMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucdo.lvDOlist.SelectedItems.Clear();
                    ucdo.lvDOlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucdo.lvDOlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucdo.lvDOlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucdo.lvDOMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOMap_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //if (e.IsSelected)
            //{
            //    string doIndex = e.Item.Text;
            //    Console.WriteLine("*** selected DO: {0}", doIndex);
            //    //Namrata : 27/7/2017
            //    ucdo.lvDOMapItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOMap_ItemSelectionChanged);
            //    //Remove selection from DOMap...
            //    ucdo.lvDOMap.SelectedItems.Clear();
            //    Utils.highlightListviewItem(doIndex, ucdo.lvDOMap);
            //    //Namrata : 27/7/2017
            //    ucdo.lvDOMapItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOMap_ItemSelectionChanged);
            //}
        }
        //Namrata: 27/7/2017
        private void lvDOMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
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
                    ucdo.lvDOlistItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOlist_ItemSelectionChanged);
                    ucdo.lvDOlist.SelectedItems.Clear();   //Remove selection from DIMap...
                    ucdo.lvDOlist.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window); //Namrata: 07/04/2018
                    ucdo.lvDOlist.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour)); //Namrata: 07/04/2018
                    Utils.highlightListviewItem(diIndex, ucdo.lvDOlist);
                    //Namrata:lvAIlist 27/7/2017
                    ucdo.lvDOlist.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);//Namrata: 07/04/2018
                    ucdo.lvDOMap.SelectedItems.Clear();
                    ucdo.lvDOMap.Items.Cast<ListViewItem>().ToList().ForEach(x => x.BackColor = SystemColors.Window);
                    ucdo.lvDOMap.Items.Cast<ListViewItem>().Where(s => s.Index % 2 == 0).ToList().ForEach(x => x.BackColor = ColorTranslator.FromHtml(Globals.rowColour));
                    ucdo.lvDOMap.Items.Cast<ListViewItem>().Where(x => x.Text == diIndex.ToString()).ToList().ForEach(item => item.BackColor = GreenColour);
                    ucdo.lvDOlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOlist_ItemSelectionChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ":" + "Error:" + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //if (e.IsSelected)
            //{
            //    string doIndex = e.Item.Text;
            //    Console.WriteLine("*** selected DO: {0}", doIndex);
            //    //Namrata : 27/7/2017
            //    ucdo.lvDOlistItemSelectionChanged -= new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOlist_ItemSelectionChanged);
            //    //Remove selection from DOMap...
            //    ucdo.lvDOlist.SelectedItems.Clear();
            //    Utils.highlightListviewMapItem(doIndex, ucdo.lvDOlist);
            //    //Namrata : 27/7/2017
            //    ucdo.lvDOlistItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDOlist_ItemSelectionChanged);
            //}
        }
        private void loadDefaults()
        {
            ucdo.txtAutoMapNumber.Text = "1";
            ucdo.txtDONo.Text = (Globals.DONo + 1).ToString();
            ucdo.txtIndex.Text = (Globals.DOIndex + 1).ToString();
            ucdo.txtIndex.Text = "1";
            ucdo.txtSubIndex.Text = "0";
            ucdo.txtEnableDI.Text = "0";

            ////Namrata:24/5/2017
            ucdo.txtPulseDuration.Text = "100";
            if (masterType == MasterTypes.ADR)
            {
                if (ucdo.lvDOlist.Items.Count - 1 >= 0)
                {
                    ucdo.txtIndex.Text = Convert.ToString(Convert.ToInt32(doList[doList.Count - 1].Index) + 1);
                }
                ucdo.cmbResponseType.SelectedIndex = ucdo.cmbResponseType.FindStringExact("ADR_DO_AI");
                ucdo.txtDescription.Text = "ADR_DO";// + (Globals.DONo + 1).ToString();
            }
            else if (masterType == MasterTypes.IEC101)
            {
                if (ucdo.lvDOlist.Items.Count - 1 >= 0)
                {
                    ucdo.txtIndex.Text = Convert.ToString(Convert.ToInt32(doList[doList.Count - 1].Index) + 1);
                }
                ucdo.cmbResponseType.SelectedIndex = ucdo.cmbResponseType.FindStringExact("DoubleCommand");
                ucdo.txtDescription.Text = "IEC101_DO";// + (Globals.DONo + 1).ToString();
            }
            else if (masterType == MasterTypes.IEC103)
            {
                if (ucdo.lvDOlist.Items.Count - 1 >= 0)
                {
                    ucdo.txtIndex.Text = Convert.ToString(Convert.ToInt32(doList[doList.Count - 1].Index) + 1);
                }
                ucdo.cmbResponseType.SelectedIndex = ucdo.cmbResponseType.FindStringExact("GeneralCommand");
                ucdo.txtDescription.Text = "IEC103_DO";// + (Globals.DONo + 1).ToString();
            }

            else if (masterType == MasterTypes.MODBUS)
            {
                if (ucdo.lvDOlist.Items.Count - 1 >= 0)
                {
                    ucdo.txtIndex.Text = Convert.ToString(Convert.ToInt32(doList[doList.Count - 1].Index) + 1);
                }
                ucdo.cmbResponseType.SelectedIndex = ucdo.cmbResponseType.FindStringExact("WriteSingleCoil");
                ucdo.txtDescription.Text = "MODBUS_DO";// + (Globals.DONo + 1).ToString();
            }
            else if (masterType == MasterTypes.IEC61850Client)
            {
                if (ucdo.lvDOlist.Items.Count - 1 >= 0)
                {
                    ucdo.txtIndex.Text = Convert.ToString(Convert.ToInt32(doList[doList.Count - 1].Index) + 1);
                }
                ucdo.cmbResponseType.SelectedIndex = ucdo.cmbResponseType.FindStringExact("WriteSingleCoil");
                ucdo.txtDescription.Text = "IEC61850Client_DO";// + (Globals.DONo + 1).ToString();
            }
        }
        private void loadValues()
        {
            DO doe = doList.ElementAt(editIndex);
            if (doe != null)
            {
                ucdo.txtDONo.Text = doe.DONo;
                ucdo.cmbResponseType.SelectedIndex = ucdo.cmbResponseType.FindStringExact(doe.ResponseType);
                ucdo.txtIndex.Text = doe.Index;
                ucdo.txtSubIndex.Text = doe.SubIndex;
                ucdo.cmbControlType.SelectedIndex = ucdo.cmbControlType.FindStringExact(doe.ControlType);
                ucdo.txtPulseDuration.Text = doe.PulseDurationMS;
                ucdo.txtDescription.Text = doe.Description;
                ucdo.txtEnableDI.Text = doe.EnableDI;
                ucdo.txtFC.Text = doe.FC;
                //Namrata: 16/03/2018
                ucdo.cmbIEDName.SelectedIndex = ucdo.cmbIEDName.FindStringExact(Utils.Iec61850IEDname);
            }
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucdo.grpDO))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void refreshList()
        {
            int cnt = 0;
            ucdo.lvDOlist.Items.Clear();
            Utils.DolistforDescription.Clear();
            if ((masterType == MasterTypes.ADR) || (masterType == MasterTypes.IEC101) || (masterType == MasterTypes.IEC103) || (masterType == MasterTypes.MODBUS) || (masterType == MasterTypes.Virtual))
            {
                foreach (DO don in doList)
                {
                    string[] row = new string[8];
                    if (don.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = don.DONo;
                        row[1] = don.ResponseType;
                        row[2] = don.Index;
                        row[3] = don.SubIndex;
                        row[4] = don.ControlType;
                        row[5] = don.PulseDurationMS;
                        row[6] = don.EnableDI;
                        row[7] = don.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucdo.lvDOlist.Items.Add(lvItem);
                }
            }
            if (masterType == MasterTypes.IEC61850Client)
            {
                foreach (DO don in doList)
                {
                    string[] row = new string[10];
                    if (don.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = don.DONo;
                        row[1] = don.IEDName;
                        row[2] = don.IEC61850ResponseType;
                        row[3] = don.IEC61850Index;
                        row[4] = don.FC;
                        row[5] = don.SubIndex;
                        row[6] = don.ControlType;
                        row[7] = don.PulseDurationMS;
                        row[8] = don.EnableDI;
                        row[9] = don.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucdo.lvDOlist.Items.Add(lvItem);
                }
            }
            ucdo.lblDORecords.Text = doList.Count.ToString();
            Utils.DolistRegenerateIndex.AddRange(doList);
            Utils.DolistforDescription.AddRange(doList);
        }
        private int getMaxDOs()
        {
            if (masterType == MasterTypes.IEC103) return Globals.MaxIEC103DO;
            else if (masterType == MasterTypes.MODBUS) return Globals.MaxMODBUSDO;
            //Namrata:13/7/2017
            else if (masterType == MasterTypes.ADR) return Globals.MaxADRDO;
            else if (masterType == MasterTypes.IEC101) return Globals.MaxIEC101DO;
            else if (masterType == MasterTypes.IEC61850Client) return Globals.MaxIEC101DO1;
            else return 0;
        }
        /* ============================================= Below this, DO Map logic... ============================================= */

        private void CreateNewSlave(string slaveNum, string slaveID, XmlNode domNode)
        {
            List<DOMap> sdomList = new List<DOMap>();
            slavesDOMapList.Add(slaveID, sdomList);
            if (domNode != null)
            {
                foreach (XmlNode node in domNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    sdomList.Add(new DOMap(node, Utils.getSlaveTypes(slaveID)));
                }
            }
            AddMap2SlaveButton(Int32.Parse(slaveNum), slaveID);

            //Namrata: 24/02/2018
            //If Description attribute not exist in XML 
            sdomList.AsEnumerable().ToList().ForEach(item =>
            {
                string strDONo = item.DONo;
                item.Description = Utils.DolistforDescription.AsEnumerable().Where(x => x.DONo == strDONo).Select(x => x.Description).FirstOrDefault();
            });
            refreshMapList(sdomList);
            currentSlave = slaveID;
        }
        private void CreateNewSlave1(string slaveNum, string slaveID, XmlNode domNode)
        {
            List<DOMap> sdomList = new List<DOMap>();
            slavesDOMapList.Add(slaveID, sdomList);
            if (domNode != null)
            {
                foreach (XmlNode node in domNode)
                {


                    if (node.Attributes.OfType<XmlAttribute>().Where(x => x.Name == "Description").Any())
                    {
                        node.Attributes.OfType<XmlAttribute>().Where(x => x.Name == "Description").ToList().ForEach(x =>
                        {
                            for (int i = 1; i <= Utils.DolistRegenerateIndex.Count; i++)
                            {
                                if (doList.Select(x1 => x1.DONo).ToList().Contains(i.ToString()))
                                {
                                    if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                                    {

                                        x.Value = doList[i].Description.ToString(); //"MODBUS_DO";
                                    }
                                }
                                ucdo.txtMapDescription.Text = doList[i].Description.ToString();
                            }

                        });

                    }


                    //Namrata: 23/02/2018
                    //if (node.Attributes.OfType<XmlAttribute>().Where(x => x.Name == "Description").Any())
                    //{
                    //    node.Attributes.OfType<XmlAttribute>().Where(x => x.Name == "Description").ToList().ForEach(x =>
                    //    {
                    //        if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                    //        {
                    //            x.Value = "MODBUS_DO";
                    //        }
                    //    });
                    //    ucdo.txtMapDescription.Text = "MODBUS_DO";
                    //}


                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    sdomList.Add(new DOMap(node, Utils.getSlaveTypes(slaveID)));
                }
            }
            AddMap2SlaveButton(Int32.Parse(slaveNum), slaveID);
            //Namrata: 23/02/2018




            //for (int i = 0; i < sdomList.Count; i++)
            //{
            //    if (sdomList[i].Description == "")
            //    {
            //        sdomList[i].Description = "MODBUS_DO";
            //        ucdo.txtMapDescription.Text = "MODBUS_DO";
            //    }
            //}
            refreshMapList(sdomList);
            currentSlave = slaveID;
        }
        private void DeleteSlave(string slaveID)
        {
            Console.WriteLine("*** Deleting slave {0}", slaveID);
            slavesDOMapList.Remove(slaveID);
            RadioButton rb = null;
            foreach (Control ctrl in ucdo.flpMap2Slave.Controls)
            {
                if (ctrl.Tag.ToString() == slaveID)
                {
                    rb = (RadioButton)ctrl;
                    break;
                }
            }
            if (rb != null) ucdo.flpMap2Slave.Controls.Remove(rb);
        }
        private void CheckIEC104SlaveStatusChanges()
        {
            Console.WriteLine("*** CheckIEC104SlaveStatusChanges");
            //Check for slave addition...
            foreach (IEC104Slave slv104 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC104Group().getIEC104Slaves())//Loop thru slaves...
            {
                string slaveID = "IEC104_" + slv104.SlaveNum;
                bool slaveAdded = true;
                foreach (KeyValuePair<string, List<DOMap>> sn in slavesDOMapList)
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
            foreach (KeyValuePair<string, List<DOMap>> sdon in slavesDOMapList)//Loop thru slaves...
            {
                string slaveID = sdon.Key;
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
                if (ucdo.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucdo.lvDOMap.Items.Clear();
                    currentSlave = "";
                }
            }
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
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
                foreach (KeyValuePair<string, List<DOMap>> sn in slavesDOMapList)
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
            foreach (KeyValuePair<string, List<DOMap>> sain in slavesDOMapList)//Loop thru slaves...
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
                if (ucdo.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucdo.lvDOMap.Items.Clear();
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
                foreach (KeyValuePair<string, List<DOMap>> sn in slavesDOMapList)
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
            foreach (KeyValuePair<string, List<DOMap>> sdon in slavesDOMapList)//Loop thru slaves...
            {
                string slaveID = sdon.Key;
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
                if (ucdo.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucdo.lvDOMap.Items.Clear();
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
                foreach (KeyValuePair<string, List<DOMap>> sn in slavesDOMapList)
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
            foreach (KeyValuePair<string, List<DOMap>> sdon in slavesDOMapList)//Loop thru slaves...
            {
                string slaveID = sdon.Key;
                bool slaveDeleted = true;
                if (Utils.getSlaveTypes(slaveID) != SlaveTypes.IEC61850Server) continue;
                foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())//Loop thru slaves...
                {
                    slaveID = "IEC61850Server_" + slvMB.SlaveNum; //61850ServerSlaveGroup_
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
                if (ucdo.flpMap2Slave.Controls.Count > 0)
                {
                    ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Checked = true;
                    currentSlave = ((RadioButton)ucdo.flpMap2Slave.Controls[0]).Tag.ToString();
                    refreshCurrentMapList();
                }
                else
                {
                    ucdo.lvDOMap.Items.Clear();
                    currentSlave = "";
                }
            }

            fillMapOptions(Utils.getSlaveTypes(currentSlave));
        }
        private void deleteDOFromMaps(string doNo)
        {
            Console.WriteLine("*** Deleting element no. {0}", doNo);
            foreach (KeyValuePair<string, List<DOMap>> sdon in slavesDOMapList)//Loop thru slaves...
            {
                List<DOMap> delEntry = sdon.Value;
                foreach (DOMap domn in delEntry)
                {
                    if (domn.DONo == doNo)
                    {
                        delEntry.Remove(domn);
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
            //Add IEC101 Button On Mapping Side
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

            rb.TextAlign = ContentAlignment.TopCenter;
            rb.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            rb.Appearance = Appearance.Button;
            rb.AutoSize = true;
            rb.Image = Properties.Resources.SlaveRadioButton;
            rb.Click += rbGrpMap2Slave_Click;

            ucdo.flpMap2Slave.Controls.Add(rb);
            rb.Checked = true;
        }
        private string getDescription(ListView lstview, string ainno)
        {
            int iColIndex = ucdo.lvDOlist.Columns["Description"].Index;
            var query = lstview.Items
                    .Cast<ListViewItem>()
                    .Where(item => item.SubItems[0].Text == ainno).Select(s => s.SubItems[iColIndex].Text).Single();
            return query.ToString();
        }
        public void IEC62850MappingEvents()
        {
            ucdo.cmbDOMCommandType.Enabled = false;
            ucdo.lblAutoMapMapping.Visible = true;
            ucdo.txtDOAutoMap.Visible = true;
            ucdo.pbDOMHdr.Size = new Size(450, 22);
            ucdo.grpDOMap.Size = new Size(450, 270);
            ucdo.grpDOMap.Location = new Point(400, 335);
            ucdo.lblDNM.Location = new Point(12, 30);
            ucdo.txtDOMNo.Location = new Point(122, 30);
            ucdo.txtDOMNo.Size = new Size(300, 21);
            ucdo.lblDMRI.Visible = false;
            ucdo.txtDOMReportingIndex.Visible = false;
            ucdo.lbl61850reportingindex.Visible = true;
            ucdo.CmbReportingIndex.Visible = true;
            ucdo.lbl61850reportingindex.Location = new Point(12, 55);
            ucdo.CmbReportingIndex.Location = new Point(122, 55);
            ucdo.CmbReportingIndex.Size = new Size(300, 21);
            ucdo.lblDMDT.Location = new Point(12, 82);
            ucdo.cmbDOMDataType.Location = new Point(122, 82);
            ucdo.cmbDOMDataType.Size = new Size(300, 21);
            ucdo.lblDMCT.Location = new Point(12, 108);
            ucdo.cmbDOMCommandType.Location = new Point(122, 108);
            ucdo.cmbDOMCommandType.Size = new Size(300, 21);
            ucdo.lblDMBP.Location = new Point(12, 135);
            ucdo.txtDOMBitPos.Location = new Point(122, 135);
            ucdo.txtDOMBitPos.Size = new Size(300, 21);
            ucdo.lblmapdesc.Location = new Point(12, 162);
            ucdo.txtMapDescription.Location = new Point(122, 162);
            ucdo.txtMapDescription.Size = new Size(300, 21);
            ucdo.lblAutoMapMapping.Location = new Point(12, 188);
            ucdo.txtDOAutoMap.Location = new Point(122, 188);
            ucdo.txtDOAutoMap.Size = new Size(300, 21);
            ucdo.chkDOMSelect.Location = new Point(12, 215);
            ucdo.btnDOMDone.Location = new Point(210, 217);
            ucdo.btnDOMCancel.Location = new Point(300, 217);
            ucdo.grpDOMap.Height = 255;
            ucdo.grpDOMap.Location = new Point(400, 354);
        }
        public void IEC61850MappingEvents1()
        {
            ucdo.cmbDOMCommandType.Enabled = false;
            ucdo.lblAutoMapMapping.Visible = false;
            ucdo.txtDOAutoMap.Visible = false;
            ucdo.lblDNM.Location = new Point(12, 30);
            ucdo.txtDOMNo.Location = new Point(122, 30);
            ucdo.txtDOMNo.Size = new Size(300, 21);
            ucdo.lblDMRI.Visible = false;
            ucdo.txtDOMReportingIndex.Visible = false;
            ucdo.lbl61850reportingindex.Visible = true;
            ucdo.CmbReportingIndex.Visible = true;
            ucdo.lbl61850reportingindex.Location = new Point(12, 55);
            ucdo.CmbReportingIndex.Location = new Point(122, 55);
            ucdo.CmbReportingIndex.Size = new Size(300, 21);
            ucdo.lblDMDT.Location = new Point(12, 82);
            ucdo.cmbDOMDataType.Location = new Point(122, 82);
            ucdo.cmbDOMDataType.Size = new Size(300, 21);
            ucdo.lblDMCT.Location = new Point(12, 108);
            ucdo.cmbDOMCommandType.Location = new Point(122, 108);
            ucdo.cmbDOMCommandType.Size = new Size(300, 21);
            ucdo.lblDMBP.Location = new Point(12, 135);
            ucdo.txtDOMBitPos.Location = new Point(122, 135);
            ucdo.txtDOMBitPos.Size = new Size(300, 21);
            ucdo.lblmapdesc.Location = new Point(12, 162);
            ucdo.txtMapDescription.Location = new Point(122, 162);
            ucdo.txtMapDescription.Size = new Size(300, 21);
            ucdo.chkDOMSelect.Location = new Point(12, 188);
            ucdo.btnDOMDone.Location = new Point(210, 190);
            ucdo.btnDOMCancel.Location = new Point(300, 190);
            ucdo.pbDOMHdr.Size = new Size(450, 22);
            ucdo.grpDOMap.Size = new Size(450, 230);
            ucdo.grpDOMap.Location = new Point(450, 325);
        }
        public void EnableMappingEvents()
        {
            ucdo.pbDOMHdr.Size = new Size(299, 22);
            ucdo.grpDOMap.Size = new Size(299, 260);
            ucdo.grpDOMap.Location = new Point(450, 363);

            ucdo.txtDOMNo.Size = new Size(151, 20);

            ucdo.lblDMRI.Visible = true;
            ucdo.txtDOMReportingIndex.Visible = true;
            ucdo.txtDOMReportingIndex.Size = new Size(151, 20);

            ucdo.lbl61850reportingindex.Visible = false;
            ucdo.CmbReportingIndex.Visible = false;

            ucdo.lblDMDT.Location = new Point(12, 82);
            ucdo.cmbDOMDataType.Location = new Point(122, 82);
            ucdo.cmbDOMDataType.Size = new Size(151, 20);

            ucdo.lblDMCT.Location = new Point(12, 108);
            ucdo.cmbDOMCommandType.Location = new Point(122, 108);
            ucdo.cmbDOMCommandType.Size = new Size(151, 20);

            ucdo.lblDMBP.Location = new Point(12, 135);
            ucdo.txtDOMBitPos.Location = new Point(122, 135);
            ucdo.txtDOMBitPos.Size = new Size(151, 20);

            ucdo.lblmapdesc.Location = new Point(12, 162);
            ucdo.txtMapDescription.Location = new Point(122, 162);
            ucdo.txtMapDescription.Size = new Size(151, 20);

            ucdo.lblAutoMapMapping.Visible = true;
            ucdo.txtDOAutoMap.Visible = true;
            ucdo.lblAutoMapMapping.Location = new Point(12, 188);
            ucdo.txtDOAutoMap.Location = new Point(122, 188);
            ucdo.txtDOAutoMap.Size = new Size(151, 20);

            ucdo.chkDOMSelect.Location = new Point(12, 215);

            ucdo.btnDOMDone.Location = new Point(120, 217);
            ucdo.btnDOMCancel.Location = new Point(200, 217);
        }
        public void EnableMappingEventsOnDoubleClick()
        {
            ucdo.lblAutoMapMapping.Visible = false;
            ucdo.txtDOAutoMap.Visible = false;
            ucdo.txtDOMNo.Size = new Size(149, 20);
            ucdo.lblDMRI.Visible = true;
            ucdo.txtDOMReportingIndex.Visible = true;
            ucdo.txtDOMNo.Size = new Size(149, 20);
            ucdo.lbl61850reportingindex.Visible = false;
            ucdo.CmbReportingIndex.Visible = false;
            ucdo.lblDMDT.Location = new Point(12, 82);
            ucdo.cmbDOMDataType.Location = new Point(122, 82);
            ucdo.cmbDOMDataType.Size = new Size(149, 20);
            ucdo.lblDMCT.Location = new Point(12, 108);
            ucdo.cmbDOMCommandType.Location = new Point(122, 108);
            ucdo.cmbDOMCommandType.Size = new Size(149, 20);
            ucdo.lblDMBP.Location = new Point(12, 135);
            ucdo.txtDOMBitPos.Location = new Point(122, 135);
            ucdo.txtDOMBitPos.Size = new Size(149, 20);
            ucdo.lblmapdesc.Location = new Point(12, 162);
            ucdo.txtMapDescription.Location = new Point(122, 162);
            ucdo.txtMapDescription.Size = new Size(149, 20);
            ucdo.chkDOMSelect.Location = new Point(12, 188);
            ucdo.btnDOMDone.Location = new Point(120, 190);
            ucdo.btnDOMCancel.Location = new Point(200, 190);
            ucdo.pbDOMHdr.Size = new Size(299, 22);
            ucdo.grpDOMap.Size = new Size(299, 220);
            ucdo.grpDOMap.Location = new Point(450, 370);
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
                if (ucdo.lvDOlist.SelectedItems.Count > 0)
                {
                    //If possible highlight the map for new slave selected...
                    //Remove selection from DOMap...
                    ucdo.lvDOMap.SelectedItems.Clear();
                    Utils.highlightListviewItem(ucdo.lvDOlist.SelectedItems[0].Text, ucdo.lvDOMap);
                }
            }
            ShowHideSlaveColumns();
            ShowHideMODBUSSlaveColumns();
            ShowHideIEC61850ServerSlaveColumns();
            ShowHideMODBUSSlaveColumns();

            if (rbChanged && ucdo.lvDOlist.CheckedItems.Count <= 0) return;//We supported map listing only

            DO mapDO = null;

            if (ucdo.lvDOlist.CheckedItems.Count != 1)
            {
                MessageBox.Show("Select single DO element to map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i < ucdo.lvDOlist.Items.Count; i++)
            {
                if (ucdo.lvDOlist.Items[i].Checked)
                {
                    Console.WriteLine("*** Mapping index: {0}", i);
                    mapDO = doList.ElementAt(i);
                    ucdo.lvDOlist.Items[i].Checked = false;//Now we can uncheck in listview...
                    break;
                }
            }
            if (mapDO == null) return;

            //Search if already mapped for current slave...
            bool alreadyMapped = false;
            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(currentSlave, out slaveDOMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Slave entry doesnot exist!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                //Do it from elsewhere...
                //slaveDOMapList = new List<DOMap>();
                //slavesDOMapList.Add(currentSlave, slaveDOMapList);
            }
            else
            {
                Console.WriteLine("##### Slave entries exists");
            }
            //Namrata Commented: 28/04/2018
            //foreach (DOMap sdom in slaveDOMapList)
            //{
            //    if (sdom.DONo == mapDO.DONo)
            //    {
            //        Console.WriteLine("##### Hoorray, already mapped...");
            //        MessageBox.Show("DO already mapped!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        alreadyMapped = true;
            //        break;
            //    }
            //}
            if (!alreadyMapped)
            {
                mapMode = Mode.ADD;
                mapEditIndex = -1;
                Utils.resetValues(ucdo.grpDOMap);
                ucdo.txtDOMNo.Text = mapDO.DONo;
                string str = getDescription(ucdo.lvDOlist, mapDO.DONo);
                ucdo.txtMapDescription.Text = str;
                //Namrata:1/7/2017
                ucdo.txtDOAutoMap.Text = "1";
                loadMapDefaults();
                //Namrata: 4/11/2017
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
                {
                    //Namrata: 4/11/2017
                    Utils.CurrentSlaveFinal = currentSlave;
                    ucdo.CmbReportingIndex.DataSource = null;
                    ucdo.CmbReportingIndex.Items.Clear();
                    ucdo.CmbReportingIndex.DataSource = Utils.DSDOSlave.Tables[Utils.CurrentSlaveFinal];
                    ucdo.CmbReportingIndex.DisplayMember = "ObjectReferrence";
                    //Namrata: 26/03/2018
                    ucdo.chkDOMSelect.Enabled = false;
                    IEC62850MappingEvents();
                }
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) 
                {
                    EnableMappingEvents();
                    //Namrata: 19/04/2018
                    ucdo.chkDOMSelect.Enabled = false;
                    ucdo.cmbDOMCommandType.Enabled = true;

                }
                if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
                {
                    EnableMappingEvents();
                    ucdo.chkDOMSelect.Enabled = true;
                    ucdo.cmbDOMCommandType.Enabled = false;
                }
                //if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) ucdo.cmbDOMCommandType.Enabled = true;
                //else ucdo.cmbDOMCommandType.Enabled = false;
                //Namrata:21/7/2017
                //if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) ucdo.chkDOMSelect.Enabled = false;
                //else ucdo.chkDOMSelect.Enabled = true;

                //if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server) ucdo.cmbDOMCommandType.Enabled = true;
                //else
                //    ucdo.cmbDOMCommandType.Enabled = false;
                //Namrata:21 / 7 / 2017
                //if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server) ucdo.chkDOMSelect.Enabled = false;
                //else ucdo.chkDOMSelect.Enabled = false;

                ucdo.grpDOMap.Visible = true;
                ucdo.txtDOMReportingIndex.Focus();
            }
        }
        private void btnDOMDelete_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucdo btnDOMDelete_Click clicked in class!!!");
            DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                return;
            }

            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(currentSlave, out slaveDOMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error deleting DO map!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = ucdo.lvDOMap.Items.Count - 1; i >= 0; i--)
            {
                if (ucdo.lvDOMap.Items[i].Checked)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    slaveDOMapList.RemoveAt(i);
                    ucdo.lvDOMap.Items[i].Remove();
                }
            }
            Console.WriteLine("*** slaveDOMapList count: {0} lv count: {1}", slaveDOMapList.Count, ucdo.lvDOMap.Items.Count);
            refreshMapList(slaveDOMapList);


            //Console.WriteLine("*** ucdi btnDIMDelete_Click clicked in class!!!");
            //DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            //if (result == DialogResult.No)
            //{
            //    return;
            //}
            //List<DOMap> slaveDOMapList;
            //if (!slavesDOMapList.TryGetValue(currentSlave, out slaveDOMapList))
            //{
            //    Console.WriteLine("##### Slave entries does not exists");
            //    MessageBox.Show("Error deleting DI map!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //var LitsItemsChecked = new List<KeyValuePair<int, int>>();
            //int arraycnt = 0;
            //string SubitemText;
            //int finIndex;
            //foreach (ListViewItem lvi in ucdo.lvDOMap.Items)
            //{
            //    SubitemText = lvi.SubItems[1].Text;
            //    finIndex = lvi.Index;

            //    if (lvi.Checked == true)
            //    {
            //        arraycnt = arraycnt + 1;
            //        //Key count,Value Repotingindex
            //        LitsItemsChecked.Add(new KeyValuePair<int, int>(Convert.ToInt32(lvi.Text), Convert.ToInt32(SubitemText)));
            //        slaveDOMapList.RemoveAt(finIndex);
            //        ucdo.lvDOMap.Items[finIndex].Remove();
            //    }
            //}
            //refreshMapList1(slaveDOMapList, LitsItemsChecked[0].Key, LitsItemsChecked[0].Value);
            //Console.WriteLine("*** slaveDIMapList count: {0} lv count: {1}", slaveDOMapList.Count, ucdo.lvDOMap.Items.Count);
        }
        private void refreshMapList1(List<DOMap> tmpList, int changeof, int changefrom)
        {
            int cnt = 0;
            ucdo.lvDOMap.Items.Clear();
            //ucdi.lblDIMRecords.Text = "0";
            if (tmpList == null) return;
            int ReportingIndexStart = 0;
            //ReportingIndexStart = changefrom;
            int intDINO = changeof + 1;
            int tmpListCnt = 0;
            foreach (DOMap domp in tmpList)
            {
                string[] row = new string[6];
                if (domp.IsNodeComment)
                {
                    row[0] = "Comment...";
                }
                else
                {
                    tmpListCnt++;
                    row[0] = domp.DONo;
                    if (domp.DONo == intDINO.ToString())
                    {
                        ReportingIndexStart = changefrom;// Convert.ToInt32(dimp.ReportingIndex);
                        domp.ReportingIndex = ReportingIndexStart.ToString();
                        row[1] = domp.ReportingIndex;
                    }
                    else
                    {
                        if (Convert.ToInt32(domp.DONo) < intDINO)
                        {
                            row[1] = domp.ReportingIndex;
                        }
                        else
                        {
                            ReportingIndexStart = ReportingIndexStart + 1;
                            domp.ReportingIndex = ReportingIndexStart.ToString();
                            row[1] = domp.ReportingIndex;
                        }
                    }
                    row[2] = domp.DataType;
                    row[3] = domp.CommandType;
                    row[4] = domp.BitPos;
                    row[5] = domp.Select;
                }
                ListViewItem lvItem = new ListViewItem(row);
                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucdo.lvDOMap.Items.Add(lvItem);
            }
            ucdo.lblDOMRecords.Text = tmpList.Count.ToString();

        }
        List<DOMap> slaveDOMapList;
        private void btnDOMDone_Click(object sender, EventArgs e)
        {
            //if (!ValidateMap()) return;

            Console.WriteLine("*** ucdo btnDOMDone_Click clicked in class!!!");
            //List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(currentSlave, out slaveDOMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error adding DO map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<KeyValuePair<string, string>> domData = Utils.getKeyValueAttributes(ucdo.grpDOMap);
            if (mapMode == Mode.ADD)
            {
                int intStart = Convert.ToInt32(domData[6].Value); // DONo
                int intRange = Convert.ToInt32(domData[2].Value); //AutoMapRange
                int intDOIndex = Convert.ToInt32(domData[8].Value); // DOReportingIndex
                string dodes = "";
                int DONumber1 = 0, DOINdex1 = 0;
                //Namrata:8/7/2017
                //Find Index Of ListView
                ListViewItem item = ucdo.lvDOlist.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Text == intStart.ToString());
                string ind = ucdo.lvDOlist.Items.IndexOf(item).ToString();
                //Namrata:31/7/2017
                //Eliminate Duplicate Record From lvAIMAp List
                //ListViewItem ExistDOMap = ucdo.lvDOMap.FindItemWithText(ucdo.txtDOMNo.Text);
                ListViewItem ExistDOMap = ucdo.lvDOMap.FindItemWithText(ucdo.txtDONo.Text);
                if (doList.Count >= 0)
                {
                    //Namrata:31/7/2017
                    //Check If LvDOMapList Is Not Empty
                    //if (ExistDOMap != null)
                    //{
                    //    MessageBox.Show("Map Entry Already Exist In " + currentSlave.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    //else
                    //{
                    if ((intRange + Convert.ToInt16(ind)) > ucdo.lvDOlist.Items.Count)
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
                            if (doList.Select(x => x.DONo).ToList().Contains(i.ToString()))
                            {
                                DONumber1 = i;
                                DOINdex1 = intDOIndex++;
                                ucdo.txtMapDescription.Text = getDescription(ucdo.lvDOlist, DONumber1.ToString());
                                dodes = ucdo.txtMapDescription.Text;
                                DOMap NewDOMap = new DOMap("DO", domData, Utils.getSlaveTypes(currentSlave));
                                NewDOMap.DONo = DONumber1.ToString();
                                NewDOMap.ReportingIndex = DOINdex1.ToString();
                                NewDOMap.Description = dodes.ToString();
                                slaveDOMapList.Add(NewDOMap);
                            }
                            else
                            {
                                intRange++;
                            }
                        }
                    }
                }
            }
            else if (mapMode == Mode.EDIT)
            {
                slaveDOMapList[mapEditIndex].updateAttributes(domData);
            }
            refreshMapList(slaveDOMapList);
            ucdo.grpDOMap.Visible = false;
            mapMode = Mode.NONE;
            mapEditIndex = -1;
        }
        private void btnDOMCancel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucdo btnDOMCancel_Click clicked in class!!!");
            ucdo.grpDOMap.Visible = false;
            mapMode = Mode.NONE;
            mapEditIndex = -1;
            Utils.resetValues(ucdo.grpDOMap);
        }
        private void lvDOMap_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucdo lvDOMap_DoubleClick clicked in class!!!");
            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(currentSlave, out slaveDOMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error editing DO map for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Namrata: 07/03/2018
            int ListIndex = ucdo.lvDOMap.FocusedItem.Index;
            ListViewItem lvi = ucdo.lvDOMap.Items[ListIndex];

            //if (ucdo.lvDOMap.SelectedItems.Count <= 0) return;
            //ListViewItem lvi = ucdo.lvDOMap.SelectedItems[0];
            Utils.UncheckOthers(ucdo.lvDOMap, lvi.Index);
            if (slaveDOMapList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104))
            {
                EnableMappingEventsOnDoubleClick();
            }
            if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
            {
                IEC61850MappingEvents1();
            }
            ucdo.txtDOAutoMap.Text = "0";
            ucdo.grpDOMap.Visible = true;
            mapMode = Mode.EDIT;
            mapEditIndex = lvi.Index;
            loadMapValues();
            ucdo.txtDOMReportingIndex.Focus();
        }
        private void loadMapDefaults()
        {
            ucdo.txtDOMReportingIndex.Text = (Globals.DOReportingIndex + 1).ToString();
            ucdo.txtDOMBitPos.Text = "0";
        }
        private void loadMapValues()
        {
            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(currentSlave, out slaveDOMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                MessageBox.Show("Error loading DO map data for slave!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DOMap domn = slaveDOMapList.ElementAt(mapEditIndex);
            if (domn != null)
            {
                ucdo.txtDOMNo.Text = domn.DONo;
                ucdo.txtDOMReportingIndex.Text = domn.ReportingIndex;
                ucdo.cmbDOMDataType.SelectedIndex = ucdo.cmbDOMDataType.FindStringExact(domn.DataType);
                if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE)
                {
                    ucdo.cmbDOMCommandType.SelectedIndex = ucdo.cmbDOMCommandType.FindStringExact(domn.CommandType);
                    ucdo.cmbDOMCommandType.Enabled = true;
                    //Namrata:21/7/2017
                    ucdo.chkDOMSelect.Enabled = false;
                }
                else
                {
                    ucdo.cmbDOMCommandType.Enabled = false;
                    ucdo.chkDOMSelect.Enabled = true;   //Namrata:21/7/2017
                }
                ucdo.txtDOMBitPos.Text = domn.BitPos;
                //Namrata: 18/11/2017
                ucdo.txtMapDescription.Text = domn.Description;
                if (domn.Select.ToLower() == "enable") ucdo.chkDOMSelect.Checked = true;
                else ucdo.chkDOMSelect.Checked = false;
            }
        }
        private bool ValidateMap()
        {
            bool status = true;

            //Check empty field's
            if (Utils.IsEmptyFields(ucdo.grpDOMap))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return status;
        }
        private void refreshMapList(List<DOMap> tmpList)
        {
            int cnt = 0;
            ucdo.lvDOMap.Items.Clear();
            //addListHeaders();

            ucdo.lblDOMRecords.Text = "0";
            if (tmpList == null) return;
            if ((Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC101SLAVE) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC104) || (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE))
            {
                foreach (DOMap domp in tmpList)
                {
                    string[] row = new string[7];
                    if (domp.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = domp.DONo;
                        row[1] = domp.ReportingIndex;
                        row[2] = domp.DataType;
                        row[3] = domp.CommandType;
                        row[4] = domp.BitPos;
                        row[5] = domp.Select;
                        row[6] = domp.Description;
                    }

                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucdo.lvDOMap.Items.Add(lvItem);
                }
            }
            if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server)
            {
                foreach (DOMap domp in tmpList)
                {
                    string[] row = new string[7];
                    if (domp.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = domp.DONo;
                        row[1] = domp.IEC61850ReportingIndex;
                        row[2] = domp.DataType;
                        row[3] = domp.CommandType;
                        row[4] = domp.BitPos;
                        row[5] = domp.Select;
                        row[6] = domp.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucdo.lvDOMap.Items.Add(lvItem);
                }
            }
            ucdo.lblDOMRecords.Text = tmpList.Count.ToString();
        }
        private void refreshCurrentMapList()
        {
            fillMapOptions(Utils.getSlaveTypes(currentSlave));
            //List<DOMap> sdomList; //Ajay: 08/08/2018 commented
            //Ajay: 08/08/2018 if condition commented
            //Namrata:02/05/2018
            //if (slaveDOMapList != null)
            //{
            //    slaveDOMapList = slaveDOMapList.OrderBy(x => Convert.ToInt32(x.DONo)).ToList();
            //}
            //if (!slavesDOMapList.TryGetValue(currentSlave, out sdomList)) //Ajay: 08/08/2018 commented
            if (!slavesDOMapList.TryGetValue(currentSlave, out slaveDOMapList))  //Ajay: 08/08/2018
            {
                refreshMapList(null);
            }
            else
            {
                //Namrata:02/05/2018
                //if(sdomList.Count > 0 && slaveDOMapList != null) //Ajay: 08/08/2018 if condition commented
                if (slaveDOMapList != null && slaveDOMapList.Count > 0)
                {
                    //sdomList = slaveDOMapList.OrderBy(x => Convert.ToInt32(x.DONo)).ToList(); //Ajay: 08/08/2018 commented
                    //slaveDOMapList = sdomList; //Namrata:15/05/2018 //Ajay: 08/08/2018 commented
                    //sdomList = slaveDOMapList.OrderBy(x => Convert.ToInt32(x.DONo)).ToList();
                    slaveDOMapList = slaveDOMapList.OrderBy(x => Convert.ToInt32(x.DONo)).ToList(); //Ajay: 08/08/2018
                    //refreshMapList(sdomList);
                }
                //refreshMapList(sdomList); //Ajay: 08/08/2018 commented
                refreshMapList(slaveDOMapList); //Ajay: 08/08/2018
            }
        }
        /* ============================================= Above this, DO Map logic... ============================================= */

        private void cmbIEDName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "DO : cmbIEDName_SelectedIndexChanged";
            try
            {
                //Namrata: 04/04/2018
                if (ucdo.cmbIEDName.Focused == false)
                {

                }
                else
                {
                    Utils.Iec61850IEDname = ucdo.cmbIEDName.Text;
                    List<DataTable> dtList = Utils.dsResponseType.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                    if (dtList.Count == 0)
                    {
                        ucdo.cmb61850DIResponseType.DataSource = null;
                        ucdo.cmb61850DIIndex.DataSource = null;
                        ucdo.cmb61850DIResponseType.Enabled = false;
                        ucdo.cmb61850DIIndex.Enabled = false;
                        ucdo.txtFC.Text = "";
                    }
                    else
                    {
                        ucdo.cmb61850DIResponseType.Enabled = true;
                        ucdo.cmb61850DIIndex.Enabled = true;
                        ucdo.cmb61850DIResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];//[Utils.strFrmOpenproplusTreeNode + "/" + "Undefined" + "/" + Utils.Iec61850IEDname];
                        ucdo.cmb61850DIResponseType.DisplayMember = "Address";
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
            string strRoutineName = "DO : cmb61850DIIndex_SelectedIndexChanged";
            try
            {
                if (ucdo.cmb61850DIIndex.Items.Count > 0)
                {
                    if (ucdo.cmb61850DIIndex.SelectedIndex != -1)
                    {
                        ucdo.txtFC.Text = ((DataRowView)ucdo.cmb61850DIIndex.SelectedItem).Row[2].ToString();
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
            string strRoutineName = "DO: cmb61850DIResponseType_SelectedIndexChanged";
            try
            {
                if (ucdo.cmb61850DIResponseType.Items.Count > 1)
                {
                    if ((ucdo.cmb61850DIResponseType.SelectedIndex != -1))
                    {
                        //Namrata: 04/04/2018
                        Utils.Iec61850IEDname = ucdo.cmbIEDName.Text;
                        List<DataTable> dtList = Utils.DsAllConfigurationData.Tables.OfType<DataTable>().Where(tbl => tbl.TableName.StartsWith(Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname)).ToList();
                        DataSet dsdummy = new DataSet();
                        dtList.ForEach(tbl => { DataTable dt = tbl.Copy(); dsdummy.Tables.Add(dt); });
                        ucdo.cmb61850DIIndex.DataSource = dsdummy.Tables[ucdo.cmb61850DIResponseType.SelectedIndex];
                        ucdo.cmb61850DIIndex.DisplayMember = "ObjectReferrence";
                        ucdo.cmb61850DIIndex.ValueMember = "Node";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void fillOptions()
        {
            string strRoutineName = "DO: fillOptions";
            try
            {
                //Namrata: 28/09/2017
                //For RCB Configuration 
                dataGridViewDataSet.DataSource = null;
                dtdataset.Clear();
                dataGridViewDataSet.Rows.Clear();
                dataGridViewDataSet.Visible = false;
                dtdataset.Rows.Clear(); dtdataset.Columns.Clear();
                DataColumn dcAddressColumn = dtdataset.Columns.Add("Address", typeof(string));
                dtdataset.Columns.Add("IED", typeof(string));

                //Fill IED Name
                ucdo.cmbIEDName.Items.Clear();
                //Namrata: 31/10/2017
                ucdo.cmbIEDName.DataSource = Utils.dsIED.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];// Utils.DtIEDName;
                ucdo.cmbIEDName.DisplayMember = "IEDName";
                //Namrata: 04/04/2018
                if (Utils.Iec61850IEDname != "")
                {
                    ucdo.cmbIEDName.Text = Utils.Iec61850IEDname;
                }
                //Namrata: 15/9/2017
                //Fill ResponseType For IEC61850Client
                ucdo.cmb61850DIResponseType.Items.Clear();
                //Namrata: 31/10/2017
                ucdo.cmb61850DIResponseType.DataSource = Utils.dsResponseType.Tables[Utils.strFrmOpenproplusTreeNode + "_" + Utils.UnitIDForIEC61850Client + "_" + Utils.Iec61850IEDname];// Utils.DtAddress;
                ucdo.cmb61850DIResponseType.DisplayMember = "Address";

                //Fill Response Type...
                if (masterType == MasterTypes.IEC61850Client)
                {
                    ucdo.cmbResponseType.Items.Clear();
                }
                else
                {
                    ucdo.cmbResponseType.Items.Clear();
                    foreach (String rt in DO.getResponseTypes(masterType))
                    {
                        ucdo.cmbResponseType.Items.Add(rt.ToString());
                    }
                    ucdo.cmbResponseType.SelectedIndex = 0;
                }

                //Fill Control Type...
                //if (masterType == MasterTypes.IEC61850Client)
                //{
                //    ucdo.cmbControlType.Items.Clear();
                //}
                //else
                //{

                    foreach (String ct in DO.getControlTypes(masterType))
                    {
                        ucdo.cmbControlType.Items.Add(ct.ToString());
                    }
                    ucdo.cmbControlType.SelectedIndex = 0;
                //}
            }

            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fillMapOptions(SlaveTypes sType)
        {
            /***** Fill Map details related combobox ******/
            try
            {
                //Fill Data Type...
                ucdo.cmbDOMDataType.Items.Clear();
                foreach (String dt in DOMap.getDataTypes(sType))
                {
                    ucdo.cmbDOMDataType.Items.Add(dt.ToString());
                }
                if (ucdo.cmbDOMDataType.Items.Count > 0) ucdo.cmbDOMDataType.SelectedIndex = 0;
            }
            catch (System.NullReferenceException)
            {
                Utils.WriteLine(VerboseLevel.ERROR, "DO Map DataType does not exist for Slave Type: {0}", sType.ToString());
            }

            try
            {
                //Fill Command Type...
                ucdo.cmbDOMCommandType.Items.Clear();
                foreach (String ct in DOMap.getCommandTypes(sType))
                {
                    ucdo.cmbDOMCommandType.Items.Add(ct.ToString());
                }
                if (ucdo.cmbDOMCommandType.Items.Count > 0) ucdo.cmbDOMCommandType.SelectedIndex = 0;
            }
            catch (System.NullReferenceException)
            {
                Utils.WriteLine(VerboseLevel.ERROR, "DO Map CommandType does not exist for Slave Type: {0}", sType.ToString());
            }
        }
        private void addListHeaders()
        {
            if (masterType == MasterTypes.MODBUS)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Type", 110, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Pulse Duration(ms)", -2, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Enable DI", "Enable DI", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                //ucdo.lvDOlist.Columns.Add("Description", 100, HorizontalAlignment.Left);
            }
            else if (masterType == MasterTypes.ADR)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Type", 110, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Pulse Duration(ms)", -2, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Enable DI", "Enable DI", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                //ucdo.lvDOlist.Columns.Add("Description", 100, HorizontalAlignment.Left);
            }
            else if (masterType == MasterTypes.IEC101)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Type", 110, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Pulse Duration(ms)", -2, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Enable DI", "Enable DI", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Description", "Description", 100, HorizontalAlignment.Left, String.Empty);
            }
            else if (masterType == MasterTypes.IEC103)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Type", 110, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Pulse Duration(ms)", -2, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Enable DI", "Enable DI", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                //ucdo.lvDOlist.Columns.Add("Description", 100, HorizontalAlignment.Left);
            }
            else if (masterType == MasterTypes.Virtual)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Response Type", 220, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Index", 60, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Type", 110, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Pulse Duration(ms)", -2, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Enable DI", "Enable DI", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                //ucdo.lvDOlist.Columns.Add("Description", 100, HorizontalAlignment.Left);
            }
            else if (masterType == MasterTypes.IEC61850Client)
            {
                ucdo.lvDOlist.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("IED Name", 110, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Response Type", 210, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Index", 310, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("FC", 50, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Sub Index", 70, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Control Type", 90, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Pulse Duration(ms)", -2, HorizontalAlignment.Left);
                ucdo.lvDOlist.Columns.Add("Enable DI", "Enable DI", 60, HorizontalAlignment.Left, String.Empty);
                ucdo.lvDOlist.Columns.Add("Description", "Description", 150, HorizontalAlignment.Left, String.Empty);
                //ucdo.lvDOlist.Columns.Add("Description", 100, HorizontalAlignment.Left);
                //Namrata: 15/9/2017
                //Hide IED Name
                ucdo.lvDOlist.Columns[1].Width = 0;
            }

            //Add DO map headers...
            ucdo.lvDOMap.Columns.Add("DO No.", 70, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Reporting Index", 130, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Data Type", 130, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Command Type", 0, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Bit Position", 80, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Select", 150, HorizontalAlignment.Left);
            ucdo.lvDOMap.Columns.Add("Description", 150, HorizontalAlignment.Left);
        }
        private void ShowHideSlaveColumns()
        {
            if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) Utils.getColumnHeader(ucdo.lvDOMap, "Command Type").Width = COL_CMD_TYPE_WIDTH;
            else Utils.getColumnHeader(ucdo.lvDOMap, "Command Type").Width = 0;//Hide...
        }
        private void ShowHideMODBUSSlaveColumns()
        {
            if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.MODBUSSLAVE) Utils.getColumnHeader(ucdo.lvDOMap, "Select").Width = 0;
            else Utils.getColumnHeader(ucdo.lvDOMap, "Select").Width = COL_SELECT_TYPE_WIDTH;//Hide...
        }
        private void ShowHideIEC61850ServerSlaveColumns()
        {
            if (Utils.getSlaveTypes(currentSlave) == SlaveTypes.IEC61850Server) Utils.getColumnHeader(ucdo.lvDOMap, "Select").Width = 0;
            else Utils.getColumnHeader(ucdo.lvDOMap, "Select").Width = COL_SELECT_TYPE_WIDTH;//Hide...
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
            rootNode = xmlDoc.CreateElement("DOConfiguration");
            xmlDoc.AppendChild(rootNode);
            foreach (DO don in doList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(don.exportXMLnode(), true);
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
            rootNode = xmlDoc.CreateElement("DOMap");
            xmlDoc.AppendChild(rootNode);

            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(slaveID, out slaveDOMapList))
            {
                return rootNode;
            }
            //Namarta:15/05/2018
            List<DOMap> sdimList = slaveDOMapList.OrderBy(x => Convert.ToInt32(x.DONo)).ToList();
            slaveDOMapList = sdimList;
            foreach (DOMap domn in slaveDOMapList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(domn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(slaveID, out slaveDOMapList))
            {
                Console.WriteLine("DO INI: ##### Slave entries for {0} does not exists", slaveID);
                return iniData;
            }
            //IMP: If "DoubleCommand", create only single IOA in .INI file...
            Dictionary<string, string> riList = new Dictionary<string, string>();
            foreach (DOMap domn in slaveDOMapList)
            {
                int ri;
                try
                {
                    ri = Int32.Parse(domn.ReportingIndex);
                }
                catch (System.FormatException)
                {
                    ri = 0;
                }

                if (domn.DataType == "DoubleCommand" && IsRIinINI(riList, domn.ReportingIndex)) continue;
                if (!riList.ContainsKey(domn.ReportingIndex)) //Ajay: 26/12/2018
                {
                    iniData += "DO_" + ctr++ + "=" + Utils.GenerateIndex("DO", Utils.GetDataTypeIndex(domn.DataType), ri).ToString() + Environment.NewLine;
                    riList.Add(domn.ReportingIndex, domn.DataType);
                }
                //Ajay: 26/12/2018
                else
                {
                    MessageBox.Show("Duplicate Reporting Index (" + domn.ReportingIndex + ") found of DO No " + domn.DONo, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //iniData += "DO_" + ctr++ + "=" + Utils.GenerateIndex("DO", Utils.GetDataTypeIndex(domn.DataType), ri).ToString() + Environment.NewLine;
            }
            return iniData;
        }
        private bool IsRIinINI(Dictionary<string, string> ril, string ri)
        {
            string tmp;
            if (!ril.TryGetValue(ri, out tmp)) return false;
            else if (tmp != "DoubleCommand")
            {//Got ri, check if DoubleCommand
                return false;
            }
            else return true;
        }
        public void changeIEC104Sequence(int oSlaveNo, int nSlaveNo)
        {
            if (oSlaveNo == nSlaveNo) return;
            Utils.ChangeKey(slavesDOMapList, "IEC104_" + oSlaveNo, "IEC104_" + nSlaveNo);
            //Change radio button Tag n Text...
            foreach (Control ctrl in ucdo.flpMap2Slave.Controls)
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
        //Namrata:13/7/2017
        public void changeIEC101laveSequence(int oSlaveNo, int nSlaveNo)
        {
            if (oSlaveNo == nSlaveNo) return;
            Utils.ChangeKey(slavesDOMapList, "IEC101_" + oSlaveNo, "IEC101_" + nSlaveNo);
            //Change radio button Tag n Text...
            foreach (Control ctrl in ucdo.flpMap2Slave.Controls)
            {
                RadioButton rb = (RadioButton)ctrl;
                if (rb.Tag.ToString() == "IEC101_" + oSlaveNo)
                {
                    rb.Tag = "IEC101_" + nSlaveNo;//Ex. 'IEC101_'
                    rb.Text = "IEC101 " + nSlaveNo;
                    break;
                }
            }
            //Check currentSlave var...
            if (currentSlave == "IEC101_" + oSlaveNo) currentSlave = "IEC101_" + nSlaveNo;
        }
        public void changeMODBUSSlaveSequence(int oSlaveNo, int nSlaveNo)
        {
            if (oSlaveNo == nSlaveNo) return;
            Utils.ChangeKey(slavesDOMapList, "MODBUSSlave_" + oSlaveNo, "MODBUSSlave_" + nSlaveNo);
            //Change radio button Tag n Text...
            foreach (Control ctrl in ucdo.flpMap2Slave.Controls)
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
        public void regenerateDOSequenceold()
        {
            foreach (DO don in doList)
            {
                int oDONo = Int32.Parse(don.DONo);
                //Namrata: 30/10/2017
                //int nDONo = oDONo;
                int nDONo = Globals.DONo++;
                don.DONo = nDONo.ToString();

                //Now change in map...
                foreach (KeyValuePair<string, List<DOMap>> maps in slavesDOMapList)
                {
                    List<DOMap> sdomList = maps.Value;
                    foreach (DOMap dom in sdomList)
                    {
                        if (dom.DONo == oDONo.ToString() && !dom.IsReindexed)
                        {
                            dom.DONo = nDONo.ToString();
                            dom.IsReindexed = true;
                            break;
                        }
                    }
                }

                //Now change in Parameter Load nodes...
                Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeDOSequence(oDONo, nDONo);
            }
            //Reset reindexing status, for next use...
            foreach (KeyValuePair<string, List<DOMap>> maps in slavesDOMapList)
            {
                List<DOMap> sdomList = maps.Value;
                foreach (DOMap dom in sdomList)
                {
                    dom.IsReindexed = false;
                }
            }
            refreshList();
            refreshCurrentMapList();
        }

        public void regenerateDOSequence()
        {
            foreach (DO din in doList)
            {
                int oDONo = Int32.Parse(din.DONo);
                int nDONo = Globals.DONo++;
                din.DONo = nDONo.ToString();

                //Now change in map...
                foreach (KeyValuePair<string, List<DOMap>> maps in slavesDOMapList)
                {
                    List<DOMap> sdimList = maps.Value;
                    foreach (DOMap dim in sdimList)
                    {
                        if (dim.DONo == oDONo.ToString() && !dim.IsReindexed)
                        {
                            //Ajay: 08/08/2018 if same AO mapped again it should take same AO no on reindex.
                            //dim.DONo = nDONo.ToString();
                            //dim.IsReindexed = true;
                            sdimList.Where(x => x.DONo == oDONo.ToString()).ToList().ForEach(x => { x.DONo = nDONo.ToString(); x.IsReindexed = true; });
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
                Utils.getOpenProPlusHandle().getParameterLoadConfiguration().ChangeDISequence(oDONo, nDONo);
            }
            //Reset reindexing status, for next use...
            foreach (KeyValuePair<string, List<DOMap>> maps in slavesDOMapList)
            {
                List<DOMap> sdimList = maps.Value;
                foreach (DOMap dim in sdimList)
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
            List<DOMap> slaveDOMapList;
            if (!slavesDOMapList.TryGetValue(slaveID, out slaveDOMapList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                return ret;
            }
            foreach (DOMap dom in slaveDOMapList)
            {
                if (dom.DONo == value.ToString()) return Int32.Parse(dom.ReportingIndex);
            }
            return ret;
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("DO_"))
            {
                //If a IEC104 slave added/deleted, reflect in UI as well as objects.
                CheckIEC104SlaveStatusChanges();
                //If a MODBUS slave added/deleted, reflect in UI as well as objects.
                CheckMODBUSSlaveStatusChanges();
                CheckIEC61850SlaveStatusChanges();
                //Namrata:13/7/2017
                CheckIEC101SlaveStatusChanges();
                ShowHideSlaveColumns();
                ShowHideMODBUSSlaveColumns();
                ShowHideIEC61850ServerSlaveColumns();
                //Namrata:21/7/2017
                //ShowHideMODBUSSlaveColumns();
                return ucdo;
            }
            return null;
        }

        public void parseDOCNode(XmlNode docNode, bool imported)
        {
            if (docNode == null)
            {
                rnName = "DOConfiguration";
                return;
            }
            //First set root node name...
            rnName = docNode.Name;
            if (docNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = docNode.Value;
            }
            foreach (XmlNode node in docNode)
            {
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                doList.Add(new DO(node, masterType, masterNo, IEDNo, imported));
            }
            for (int i = 0; i < doList.Count; i++)
            {
                if (doList[i].EnableDI == "")
                {
                    doList[i].EnableDI = "0";
                    ucdo.txtEnableDI.Text = "0";
                }
            }
            refreshList();
        }
        public void parseDOMNode(string slaveNum, string slaveID, XmlNode domNode)
        {
            //SlaveID Ex. 'IEC104_1'/'MODBUSSlave_1'
            CreateNewSlave(slaveNum, slaveID, domNode);
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (DO doNode in doList)
            {
                if (doNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<DO> getDOs()
        {
            return doList;
        }
        //Namrata:27/7/2017
        public int getDOMapCount()
        {
            int ctr = 0;
            fillMapOptions(Utils.getSlaveTypes(currentSlave));

            List<DOMap> sdomList;
            if (!slavesDOMapList.TryGetValue(currentSlave, out sdomList))
            {
                Console.WriteLine("##### Slave entries does not exists");
                refreshMapList(null);
            }
            else
            {
                refreshMapList(sdomList);
            }
            if (sdomList == null)
            {
                return 0;
            }
            else
            {
                foreach (DOMap asaa in sdomList)
                {
                    if (asaa.IsNodeComment) continue;
                    ctr++;
                }
            }

            return ctr;
        }
        public List<DOMap> getSlaveDOMaps(string slaveID)
        {
            List<DOMap> slaveDOMapList;
            slavesDOMapList.TryGetValue(slaveID, out slaveDOMapList);
            return slaveDOMapList;
        }
    }
}
