using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>IEC103Master</b> is a class to store all the IED's & IEC103 master parameters.
    * \details   This class stores info for all IED's & IEC103 master parameters. It allows
    * user to add multiple IED's. It also exports the XML node related to this object.
    * 
    */
    public class IEC103Master
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
            IEC103
        };
        ucAIlist ucai = new ucAIlist();
        private bool isNodeComment = false;
        private string comment = "";
        private masterType mType = masterType.IEC103;
        private int masterNum = -1;
        private int portNum = -1;
        private bool run = true;
        private int debug = -1;
        private int giTime = 300;
        private int clockSyncInterval = -1;
        private int refreshInterval = 120;
        private string appFirmwareVersion;
        private string desc = "";
        //Namrata:15/6/2017
        private int SelectedIndex;
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        public static int UnitID;
        List<IED> iedList = new List<IED>();
        ucMasterIEC103 uciec = new ucMasterIEC103();
        private TreeNode IEC103TreeNode;
        private OpenFileDialog ofdXMLFile = new OpenFileDialog();
        private string[] arrAttributes = { "MasterNum", "PortNum", "Run", "DEBUG", "GiTime", "ClockSyncInterval", "RefreshInterval", "AppFirmwareVersion", "Description" };
        #endregion Declarations
        public IEC103Master(string m103Name, List<KeyValuePair<string, string>> m103Data, TreeNode tn)
        {
            string strRoutineName = "IEC103Master";
            try
            {
                uciec.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                uciec.lvIEDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
                uciec.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                uciec.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
                uciec.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
                uciec.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                uciec.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                uciec.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                uciec.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                uciec.btnNextClick += new System.EventHandler(this.btnNext_Click);
                uciec.btnLastClick += new System.EventHandler(this.btnLast_Click);
                uciec.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
                uciec.btnExportXlsClick += new System.EventHandler(this.btnExportXls_Click);
                addListHeaders();
                fillOptions();
                //First set the root element value...
                try
                {
                    mType = (masterType)Enum.Parse(typeof(masterType), m103Name);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", m103Name);
                }
                //Parse n store values...
                if (m103Data != null && m103Data.Count > 0)
                {
                    foreach (KeyValuePair<string, string> m103kp in m103Data)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", m103kp.Key, m103kp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(m103kp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(m103kp.Key).SetValue(this, m103kp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", m103kp.Key, m103kp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                if (tn != null) tn.Nodes.Clear();
                IEC103TreeNode = tn;
                if (tn != null) tn.Text = "IEC103 " + this.Description;
                refreshList();
                uciec.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public IEC103Master(XmlNode mNode, TreeNode tn)
        {
            string strRoutineName = "IEC103Master";
            try
            {
                uciec.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                uciec.lvIEDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
                uciec.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                uciec.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
                uciec.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
                uciec.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                uciec.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                uciec.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                uciec.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                uciec.btnNextClick += new System.EventHandler(this.btnNext_Click);
                uciec.btnLastClick += new System.EventHandler(this.btnLast_Click);
                uciec.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
                addListHeaders();
                fillOptions();

                //Parse n store values...
                Utils.WriteLine(VerboseLevel.DEBUG, "mNode name: '{0}'", mNode.Name);
                if (mNode.Attributes != null)
                {
                    //First set the root element value...
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
                else if (mNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = mNode.Value;
                }

                if (tn != null) tn.Nodes.Clear();
                IEC103TreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...

                //Now since we have parsed, we can name the treenode...
                tn.Text = "IEC103 " + this.Description;

                foreach (XmlNode node in mNode)
                {
                    //Utils.WriteLine("***** node type: {0}", node.NodeType);
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    TreeNode tmp = tn.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                    iedList.Add(new IED(node, tmp, MasterTypes.IEC103, Int32.Parse(MasterNum), false));
                }
                refreshList();
                uciec.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> m103Data)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (m103Data != null && m103Data.Count > 0)
                {
                    foreach (KeyValuePair<string, string> m103kp in m103Data)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", m103kp.Key, m103kp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(m103kp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(m103kp.Key).SetValue(this, m103kp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", m103kp.Key, m103kp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }

                uciec.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
                if (IEC103TreeNode != null) IEC103TreeNode.Text = "IEC103 " + this.Description;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvIEDList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lvIEDList_ItemCheck";
            try
            {
                try
                {
                    var listView = sender as ListView;
                    if (listView != null)
                    {
                        int index = e.Index;
                        uciec.lvIEDList.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
                if (iedList.Count >= Globals.MaxIEC103IED)
                {
                    MessageBox.Show("Maximum " + Globals.MaxIEC103IED + " IED's in IEC103 Masters are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(uciec.grpIED);
                Utils.showNavigation(uciec.grpIED, false);
                loadDefaults();
                uciec.txtUnitID.Text = (Globals.getIEC103IEDNo(Int32.Parse(MasterNum)) + 1).ToString();
                uciec.txtASDUaddress.Text = (Globals.getIEC103ASDUAddress(Int32.Parse(MasterNum)) + 1).ToString();
                uciec.grpIED.Visible = true;
                uciec.txtUnitID.Focus();
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
                if (uciec.lvIEDList.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one IED ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (uciec.lvIEDList.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete IED " + uciec.lvIEDList.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = uciec.lvIEDList.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            IEC103TreeNode.Nodes.Remove(iedList.ElementAt(iIndex).getTreeNode());
                            Utils.RemoveDI4IED(MasterTypes.IEC103, Int32.Parse(MasterNum), Int32.Parse(iedList[iIndex].UnitID));
                            iedList.RemoveAt(iIndex);
                            uciec.lvIEDList.Items[iIndex].Remove();
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
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportIED_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnExportIED_Click";
            try
            {
                if (uciec.lvIEDList.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Select single IED for export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ListViewItem lvi = uciec.lvIEDList.CheckedItems[0];
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
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportXls_Click(object sender, EventArgs e)
        {
            if (uciec.lvIEDList.CheckedItems.Count != 1)
            {
                MessageBox.Show("Select single IED for export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem lvi = uciec.lvIEDList.CheckedItems[0];
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
            Utils.SaveIEDExcelFile(expObj.exportIED());

        }
        private void btnImportIED_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnExportIED_Click";
            try
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
                                    MessageBox.Show("Invalid Master Type (" + item.Value + ") to import!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            TreeNode tmp = IEC103TreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                            iedList.Add(new IED(node, tmp, MasterTypes.IEC103, Int32.Parse(MasterNum), true));
                            Utils.CreateDI4IED(MasterTypes.IEC103, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);

                            tmp.Expand();
                        }
                    }
                    refreshList();

                    MessageBox.Show("IED imported successfully!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }//File selected thru open dialog...}
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
                List<KeyValuePair<string, string>> iedData = Utils.getKeyValueAttributes(uciec.grpIED);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = IEC103TreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                    iedList.Add(new IED("IED", iedData, tmp, MasterTypes.IEC103, Int32.Parse(MasterNum)));
                    Utils.CreateDI4IED(MasterTypes.IEC103, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
                }
                else if (mode == Mode.EDIT)
                {
                    iedList[editIndex].updateAttributes(iedData);
                    if (uciec.lvIEDList.SelectedIndices.Count > 0)
                    {
                        SelectedIndex = Convert.ToInt16(uciec.lvIEDList.FocusedItem.Index);
                        Utils.IEDUnitID = Convert.ToInt16(uciec.lvIEDList.Items[SelectedIndex].Text);
                    }
                    Utils.UpdateDI4IED(MasterTypes.IEC103, Int32.Parse(MasterNum), Utils.IEDUnitID, Int32.Parse(iedList[SelectedIndex].UnitID), iedList[iedList.Count - 1].Device);
                }
                refreshList();
                //Namrata: 09/08/2017
                if (sender != null && e != null)
                {
                    uciec.grpIED.Visible = false;
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
                uciec.grpIED.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(uciec.grpIED);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnFirst_Click";
            try
            {
                Console.WriteLine("*** uciec btnFirst_Click clicked in class!!!");
                if (uciec.lvIEDList.Items.Count <= 0) return;
                if (iedList.ElementAt(0).IsNodeComment) return;
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
                Console.WriteLine("*** uciec btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (iedList.ElementAt(editIndex - 1).IsNodeComment) return;
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
            Console.WriteLine("*** uciec btnNext_Click clicked in class!!!");
            if (editIndex + 1 >= uciec.lvIEDList.Items.Count) return;
            if (iedList.ElementAt(editIndex + 1).IsNodeComment) return;
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
                Console.WriteLine("*** uciec btnLast_Click clicked in class!!!");
            if (uciec.lvIEDList.Items.Count <= 0) return;
            if (iedList.ElementAt(iedList.Count - 1).IsNodeComment) return;
            editIndex = iedList.Count - 1;
            loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvIEDList_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvIEDList_DoubleClick";
            try
            {
                if (uciec.lvIEDList.SelectedItems.Count <= 0) return;

            ListViewItem lvi = uciec.lvIEDList.SelectedItems[0];
            Utils.UncheckOthers(uciec.lvIEDList, lvi.Index);
            if (iedList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            uciec.grpIED.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            Utils.showNavigation(uciec.grpIED, true);
            loadValues();
            uciec.txtUnitID.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadDefaults()
        {
            string strRoutineName = "loadDefaults";
            try
            {
                uciec.txtDevice.Text = "IEC103_" + (Globals.getIEC103IEDNo(Int32.Parse(MasterNum)) + 1).ToString();
            uciec.txtRetries.Text = "3";
            uciec.txtTimeOut.Text = "100";
            uciec.txtDescription.Text = "IEC103_IED_" + (Globals.getIEC103IEDNo(Int32.Parse(MasterNum)) + 1).ToString();
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
                IED eied = iedList.ElementAt(editIndex);
            if (eied != null)
            {
                uciec.txtUnitID.Text = eied.UnitID;
                uciec.txtASDUaddress.Text = eied.ASDUAddr;
                uciec.txtDevice.Text = eied.Device;
                uciec.txtRetries.Text = eied.Retries;
                uciec.txtTimeOut.Text = eied.TimeOutMS;
                if (eied.DR.ToLower() == "enable") uciec.chkDR.Checked = true;
                else uciec.chkDR.Checked = false;
                uciec.txtDescription.Text = eied.Description;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (Utils.IsEmptyFields(uciec.grpIED))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //IED-UnitID should be unique...
            if (!IsUnitIDUnique(uciec.txtUnitID.Text))
            {
                MessageBox.Show("IED Unit ID must be unique!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return status;
        }

        private void fillOptions()
        {
            //fill combobox options, if any...
        }

        private void addListHeaders()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                uciec.lvIEDList.Columns.Add("Unit ID", 60, HorizontalAlignment.Left);
            uciec.lvIEDList.Columns.Add("ASDU Address", 90, HorizontalAlignment.Left);
            uciec.lvIEDList.Columns.Add("Device", 90, HorizontalAlignment.Left);
            uciec.lvIEDList.Columns.Add("Retires", 60, HorizontalAlignment.Left);
            uciec.lvIEDList.Columns.Add("Timeout(msec)", 110, HorizontalAlignment.Left);
            uciec.lvIEDList.Columns.Add("Description", 110, HorizontalAlignment.Left);
            uciec.lvIEDList.Columns.Add("DR Applicable", -2, HorizontalAlignment.Left);
            uciec.lvIEDList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
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
            uciec.lvIEDList.Items.Clear();
            //Namrata: 25/11/2017
            Utils.IEC103MasteriedList.Clear();
            foreach (IED ied in iedList)
            {
                string[] row = new string[7];

                if (ied.IsNodeComment)
                {
                    row[0] = "Comment...";
                }
                else
                {
                    row[0] = ied.UnitID;
                    Array.Resize(ref Utils.GetIEDNoList, Utils.GetIEDNoList.Length + 1);
                    Utils.GetIEDNoList[cnt] = ied.UnitID[0].ToString();
                    row[1] = ied.ASDUAddr;
                    row[2] = ied.Device;
                    row[3] = ied.Retries;
                    row[4] = ied.TimeOutMS;
                    row[5] = ied.Description;
                    row[6] = ied.DR;
                }

                ListViewItem lvItem = new ListViewItem(row);
                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                uciec.lvIEDList.Items.Add(lvItem);
            }
            //Namrata: 20/11/2017
            Utils.IEC103MasteriedList.AddRange(iedList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IEC103_")) return uciec;
            kpArr.RemoveAt(0);
            Utils.WriteLine(VerboseLevel.DEBUG, "$$$$$ elem: {0}", kpArr.ElementAt(0));
            if (kpArr.ElementAt(0).Contains("IED_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Utils.WriteLine(VerboseLevel.DEBUG, "$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (iedList.Count <= 0) return null;
                return iedList[idx].getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "***** IEC103Master: View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }
            return null;
        }
        public TreeNode getTreeNode()
        {
            return IEC103TreeNode;
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
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(iedn.exportXMLnode(), true);
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
            get { return "IEC103_" + MasterNum; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string MasterNum
        {
            get { masterNum = Int32.Parse(uciec.txtMasterNo.Text); return masterNum.ToString(); }
            set { masterNum = Int32.Parse(value); uciec.txtMasterNo.Text = value; Globals.MasterNo = Int32.Parse(value); }
        }
        public string PortNum
        {
            get { portNum = Int32.Parse(uciec.txtPortNo.Text); return portNum.ToString(); }
            set { portNum = Int32.Parse(value); uciec.txtPortNo.Text = value; }
        }
        public string Run
        {
            get { run = uciec.chkRun.Checked; return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
                if (run == true) uciec.chkRun.Checked = true;
                else uciec.chkRun.Checked = false;
            }
        }
        public string DEBUG
        {
            get { debug = Int32.Parse(uciec.txtDebug.Text); return debug.ToString(); }
            set { debug = Int32.Parse(value); uciec.txtDebug.Text = value; }
        }
        public string GiTime
        {
            get { giTime = Int32.Parse(uciec.txtGiTime.Text); return giTime.ToString(); }
            set { giTime = Int32.Parse(value); uciec.txtGiTime.Text = value; }
        }
        public string ClockSyncInterval
        {
            get { clockSyncInterval = Int32.Parse(uciec.txtClockSyncInterval.Text); return clockSyncInterval.ToString(); }
            set { clockSyncInterval = Int32.Parse(value); uciec.txtClockSyncInterval.Text = value; }
        }
        public string RefreshInterval
        {
            get
            {
                try
                {
                    refreshInterval = Int32.Parse(uciec.txtRefreshInterval.Text);
                }
                catch (System.FormatException)
                {
                    refreshInterval = 120;
                    uciec.txtRefreshInterval.Text = refreshInterval.ToString();
                }
                return refreshInterval.ToString();
            }
            set { refreshInterval = Int32.Parse(value); uciec.txtRefreshInterval.Text = value; }
        }
        public string AppFirmwareVersion
        {
            get { return appFirmwareVersion = uciec.txtFirmwareVer.Text; }
            set { appFirmwareVersion = uciec.txtFirmwareVer.Text = value; }
        }
        public string Description
        {
            get { return desc = uciec.txtIECDesc.Text; }
            set { desc = uciec.txtIECDesc.Text = value; }
        }
    }
}
