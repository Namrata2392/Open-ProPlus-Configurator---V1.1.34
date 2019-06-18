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
    public class IEC101Master
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
            IEC101
        };
        ucAIlist ucai = new ucAIlist();
        private bool isNodeComment = false;
        private string comment = "";
        private masterType mType = masterType.IEC101;
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
        //Namarta:7/7/2017
        private string asSize = "";
        private string ioaSz = "";
        private string cSize = "";
        private string linkAddressSize = "";
        List<IED> iedList = new List<IED>();
        ucMasterIEC101 uciec101 = new ucMasterIEC101();
        private TreeNode IEC101TreeNode;
        private OpenFileDialog ofdXMLFile = new OpenFileDialog();
        private string[] arrAttributes = { "MasterNum", "PortNum", "Run", "DEBUG", "GiTime", "ClockSyncInterval", "RefreshInterval", "AppFirmwareVersion", "Description" };
        #endregion Declarations
        public IEC101Master(string m101Name, List<KeyValuePair<string, string>> m101Data, TreeNode tn)
        {
            string strRoutineName = "IEC101Master";
            try
            {
                uciec101.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                uciec101.lvIEDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
                uciec101.BtnDeleteAllClick += new System.EventHandler(this.BtnDeleteAll_Click);
                uciec101.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                uciec101.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
                uciec101.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
                uciec101.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                uciec101.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                uciec101.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                uciec101.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                uciec101.btnNextClick += new System.EventHandler(this.btnNext_Click);
                uciec101.btnLastClick += new System.EventHandler(this.btnLast_Click);
                uciec101.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
                addListHeaders();
                fillOptions();
                try
                {
                    mType = (masterType)Enum.Parse(typeof(masterType), m101Name);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", m101Name);
                }
                if (m101Data != null && m101Data.Count > 0)
                {
                    foreach (KeyValuePair<string, string> m101kp in m101Data)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", m101kp.Key, m101kp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(m101kp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(m101kp.Key).SetValue(this, m101kp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", m101kp.Key, m101kp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                if (tn != null) tn.Nodes.Clear();
                IEC101TreeNode = tn;
                if (tn != null) tn.Text = "IEC101 " + this.Description;
                refreshList();
                uciec101.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            string strRoutineName = "BtnDeleteAll_Click";
            try
            {
                uciec101.lvIEDListItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
                foreach (ListViewItem listItem in uciec101.lvIEDList.Items)
                {
                    listItem.Checked = true;
                }
                DialogResult result = MessageBox.Show("Do You Want To Delete All Records ? ", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    foreach (ListViewItem listItem in uciec101.lvIEDList.Items)
                    {
                        listItem.Checked = false;
                    }
                    return;
                }
                for (int i = uciec101.lvIEDList.Items.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine("*** removing indices: {0}", i);
                    IEC101TreeNode.Nodes.Remove(iedList.ElementAt(i).getTreeNode());
                    Utils.RemoveDI4IED(MasterTypes.IEC101, Int32.Parse(MasterNum), Int32.Parse(iedList[i].UnitID));
                    iedList.RemoveAt(i);
                    uciec101.lvIEDList.Items[i].Remove();
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public IEC101Master(XmlNode mNode, TreeNode tn)
        {
            string strRoutineName = "IEC101Master";
            try
            {
                uciec101.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                uciec101.lvIEDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
                uciec101.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                uciec101.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
                uciec101.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
                uciec101.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                uciec101.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                uciec101.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                uciec101.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                uciec101.btnNextClick += new System.EventHandler(this.btnNext_Click);
                uciec101.btnLastClick += new System.EventHandler(this.btnLast_Click);
                uciec101.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
                addListHeaders();
                fillOptions();
                Utils.WriteLine(VerboseLevel.DEBUG, "mNode name: '{0}'", mNode.Name);
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
                else if (mNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = mNode.Value;
                }
                if (tn != null) tn.Nodes.Clear();
                IEC101TreeNode = tn;
                tn.Text = "IEC101 " + this.Description;
                foreach (XmlNode node in mNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    TreeNode tmp = tn.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                    iedList.Add(new IED(node, tmp, MasterTypes.IEC101, Int32.Parse(MasterNum), false));
                }
                refreshList();
                uciec101.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
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
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    uciec101.lvIEDList.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
        public void updateAttributes(List<KeyValuePair<string, string>> m101Data)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (m101Data != null && m101Data.Count > 0)
                {
                    foreach (KeyValuePair<string, string> m101kp in m101Data)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", m101kp.Key, m101kp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(m101kp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(m101kp.Key).SetValue(this, m101kp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", m101kp.Key, m101kp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                uciec101.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
                if (IEC101TreeNode != null) IEC101TreeNode.Text = "IEC101 " + this.Description;
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
                if (iedList.Count >= Globals.MaxIEC101IED)
                {
                    MessageBox.Show("Maximum " + Globals.MaxIEC101IED + " IED's in IEC101 Masters are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(uciec101.grpIED);
                Utils.showNavigation(uciec101.grpIED, false);
                loadDefaults();
                uciec101.txtUnitID.Text = (Globals.getIEC101IEDNo(Int32.Parse(MasterNum)) + 1).ToString();
                uciec101.txtASDUaddress.Text = (Globals.getIEC101ASDUAddress(Int32.Parse(MasterNum)) + 1).ToString();
                uciec101.grpIED.Visible = true;
                uciec101.txtUnitID.Focus();
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
                if (uciec101.lvIEDList.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one IED ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (uciec101.lvIEDList.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete IED " + uciec101.lvIEDList.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = uciec101.lvIEDList.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            IEC101TreeNode.Nodes.Remove(iedList.ElementAt(iIndex).getTreeNode());
                            Utils.RemoveDI4IED(MasterTypes.IEC101, Int32.Parse(MasterNum), Int32.Parse(iedList[iIndex].UnitID));
                            iedList.RemoveAt(iIndex);
                            uciec101.lvIEDList.Items[iIndex].Remove();
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
                if (uciec101.lvIEDList.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Select single IED for export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ListViewItem lvi = uciec101.lvIEDList.CheckedItems[0];
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
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnFirst_Click";
            try
            {
                Console.WriteLine("*** uciec btnFirst_Click clicked in class!!!");
                if (uciec101.lvIEDList.Items.Count <= 0) return;
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
                if (editIndex + 1 >= uciec101.lvIEDList.Items.Count) return;
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
                if (uciec101.lvIEDList.Items.Count <= 0) return;
                if (iedList.ElementAt(iedList.Count - 1).IsNodeComment) return;
                editIndex = iedList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnImportIED_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnImportIED_Click";
            try
            {
                if (ofdXMLFile.ShowDialog() == DialogResult.OK)
                {
                    Utils.WriteLine(VerboseLevel.DEBUG, "*** Opening file: {0}", ofdXMLFile.FileName);
                    if (!Utils.IsXMLWellFormed(ofdXMLFile.FileName))
                    {
                        MessageBox.Show("Selected file is not a valid XML!!!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            TreeNode tmp = IEC101TreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                            iedList.Add(new IED(node, tmp, MasterTypes.IEC101, Int32.Parse(MasterNum), true));
                            Utils.CreateDI4IED(MasterTypes.IEC101, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
                            tmp.Expand();
                        }
                    }
                    refreshList();
                    MessageBox.Show("IED imported successfully!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!Validate()) return;
                List<KeyValuePair<string, string>> iedData = Utils.getKeyValueAttributes(uciec101.grpIED);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = IEC101TreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                    iedList.Add(new IED("IED", iedData, tmp, MasterTypes.IEC101, Int32.Parse(MasterNum)));
                    Utils.CreateDI4IED(MasterTypes.IEC101, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
                }
                else if (mode == Mode.EDIT)
                {
                    iedList[editIndex].updateAttributes(iedData);
                    if (uciec101.lvIEDList.SelectedIndices.Count > 0)
                    {
                        SelectedIndex = Convert.ToInt16(uciec101.lvIEDList.FocusedItem.Index);
                        Utils.IEDUnitID = Convert.ToInt16(uciec101.lvIEDList.Items[SelectedIndex].Text);
                    }
                    Utils.UpdateDI4IED(MasterTypes.IEC101, Int32.Parse(MasterNum), Utils.IEDUnitID, Int32.Parse(iedList[SelectedIndex].UnitID), iedList[iedList.Count - 1].Device);
                }
                refreshList();
                //Namrata: 09/08/2017
                if (sender != null && e != null)
                {
                    uciec101.grpIED.Visible = false;
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
                uciec101.grpIED.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(uciec101.grpIED);
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
                if (uciec101.lvIEDList.SelectedItems.Count <= 0) return;
                ListViewItem lvi = uciec101.lvIEDList.SelectedItems[0];
                Utils.UncheckOthers(uciec101.lvIEDList, lvi.Index);
                if (iedList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                uciec101.grpIED.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(uciec101.grpIED, true);
                loadValues();
                uciec101.txtUnitID.Focus();
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
                uciec101.txtDevice.Text = "IEC101_" + (Globals.getIEC101IEDNo(Int32.Parse(MasterNum)) + 1).ToString();
                uciec101.txtRetries.Text = "3";
                uciec101.txtTimeOut.Text = "100";
                uciec101.txtDescription.Text = "IEC101_IED_" + (Globals.getIEC101IEDNo(Int32.Parse(MasterNum)) + 1).ToString();
                uciec101.cmbASDU.SelectedIndex = uciec101.cmbASDU.FindStringExact("2");
                uciec101.CmbIOA.SelectedIndex = uciec101.CmbIOA.FindStringExact("2");
                uciec101.CmbCOT.SelectedIndex = uciec101.CmbCOT.FindStringExact("1");
                uciec101.CmbLinkAddressS.SelectedIndex = uciec101.CmbLinkAddressS.FindStringExact("1");
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
                    uciec101.txtUnitID.Text = eied.UnitID;
                    uciec101.txtASDUaddress.Text = eied.ASDUAddr;
                    uciec101.txtDevice.Text = eied.Device;
                    uciec101.txtRetries.Text = eied.Retries;
                    uciec101.txtTimeOut.Text = eied.TimeOutMS;
                    uciec101.txtDescription.Text = eied.Description;
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
            if (Utils.IsEmptyFields(uciec101.grpIED))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //IED-UnitID should be unique...
            if (!IsUnitIDUnique(uciec101.txtUnitID.Text))
            {
                MessageBox.Show("IED Unit ID must be unique!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            string strRoutineName = "fillOptions";
            try
            {
                //Fill ASDU size...
                uciec101.cmbASDU.Items.Clear();
                foreach (String br in IED.getASDUsizes())
                {
                    uciec101.cmbASDU.Items.Add(br.ToString());
                }
                uciec101.cmbASDU.SelectedIndex = 0;

                //Fill IOA size...
                uciec101.CmbIOA.Items.Clear();
                foreach (String br in IED.getIOAsizes())
                {
                    uciec101.CmbIOA.Items.Add(br.ToString());
                }
                uciec101.CmbIOA.SelectedIndex = 0;

                //Fill COT size...
                uciec101.CmbCOT.Items.Clear();
                foreach (String br in IED.getCOTsizes())
                {
                    uciec101.CmbCOT.Items.Add(br.ToString());
                }
                uciec101.CmbCOT.SelectedIndex = 0;

                //Fill LinkAddress size...
                uciec101.CmbLinkAddressS.Items.Clear();
                foreach (String br in IED.getLinkAddresssizes())
                {
                    uciec101.CmbLinkAddressS.Items.Add(br.ToString());
                }
                uciec101.CmbLinkAddressS.SelectedIndex = 0;
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
                uciec101.lvIEDList.Columns.Add("Unit ID", 80, HorizontalAlignment.Left);
                uciec101.lvIEDList.Columns.Add("ASDU Address", 130, HorizontalAlignment.Left);
                uciec101.lvIEDList.Columns.Add("Device", 100, HorizontalAlignment.Left);
                uciec101.lvIEDList.Columns.Add("Retires", 60, HorizontalAlignment.Left);
                uciec101.lvIEDList.Columns.Add("Timeout (msec)", 85, HorizontalAlignment.Left);
                uciec101.lvIEDList.Columns.Add("Link Address Size", 100, HorizontalAlignment.Left);
                uciec101.lvIEDList.Columns.Add("ASDU Size", 100, HorizontalAlignment.Left);
                uciec101.lvIEDList.Columns.Add("IOA Size", 100, HorizontalAlignment.Left);
                uciec101.lvIEDList.Columns.Add("COT Size", 100, HorizontalAlignment.Left);
                uciec101.lvIEDList.Columns.Add("Description", 150, HorizontalAlignment.Left);
                uciec101.lvIEDList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
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
                uciec101.lvIEDList.Items.Clear();
                //Namrata: 25/11/2017
                Utils.IEC101MasteriedList.Clear();
                foreach (IED ied in iedList)
                {
                    string[] row = new string[10];
                    if (ied.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = ied.UnitID;
                        row[1] = ied.ASDUAddr;
                        row[2] = ied.Device;
                        row[3] = ied.Retries;
                        row[4] = ied.TimeOutMS;
                        row[5] = ied.LinkAddressSize;
                        row[6] = ied.ASDUSize;
                        row[7] = ied.IOASize;
                        row[8] = ied.COTSize;
                        row[9] = ied.Description;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    uciec101.lvIEDList.Items.Add(lvItem);
                }
                //Namrata: 20/11/2017
                Utils.IEC101MasteriedList.AddRange(iedList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IEC101_")) return uciec101;
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
            return IEC101TreeNode;
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
            get { return "IEC101_" + MasterNum; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string MasterNum
        {
            get { masterNum = Int32.Parse(uciec101.txtMasterNo.Text); return masterNum.ToString(); }
            set { masterNum = Int32.Parse(value); uciec101.txtMasterNo.Text = value; Globals.MasterNo = Int32.Parse(value); }
        }
        public string PortNum
        {
            get { portNum = Int32.Parse(uciec101.txtPortNo.Text); return portNum.ToString(); }
            set { portNum = Int32.Parse(value); uciec101.txtPortNo.Text = value; }
        }
        public string Run
        {
            get { run = uciec101.chkRun.Checked; return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
                if (run == true) uciec101.chkRun.Checked = true;
                else uciec101.chkRun.Checked = false;
            }
        }
        public string DEBUG
        {
            get { debug = Int32.Parse(uciec101.txtDebug.Text); return debug.ToString(); }
            set { debug = Int32.Parse(value); uciec101.txtDebug.Text = value; }
        }
        public string GiTime
        {
            get { giTime = Int32.Parse(uciec101.txtGiTime.Text); return giTime.ToString(); }
            set { giTime = Int32.Parse(value); uciec101.txtGiTime.Text = value; }
        }
        public string ClockSyncInterval
        {
            get { clockSyncInterval = Int32.Parse(uciec101.txtClockSyncInterval.Text); return clockSyncInterval.ToString(); }
            set { clockSyncInterval = Int32.Parse(value); uciec101.txtClockSyncInterval.Text = value; }
        }
        public string RefreshInterval
        {
            get
            {
                try
                {
                    refreshInterval = Int32.Parse(uciec101.txtRefreshInterval.Text);
                }
                catch (System.FormatException)
                {
                    refreshInterval = 120;
                    uciec101.txtRefreshInterval.Text = refreshInterval.ToString();
                }
                return refreshInterval.ToString();
            }
            set { refreshInterval = Int32.Parse(value); uciec101.txtRefreshInterval.Text = value; }
        }
        public string AppFirmwareVersion
        {
            get { return appFirmwareVersion = uciec101.txtFirmwareVer.Text; }
            set { appFirmwareVersion = uciec101.txtFirmwareVer.Text = value; }
        }
        public string Description
        {
            get { return desc = uciec101.txtIECDesc.Text; }
            set { desc = uciec101.txtIECDesc.Text = value; }
        }
        public string ASDUSize
        {
            get { return asSize; }
            set { asSize = value; }
        }
        public string IOASize
        {
            get
            {
                return ioaSz;
            }
            set
            {
                ioaSz = value;
            }
        }
        public string COTSize
        {
            get { return cSize; }
            set { cSize = value; }
        }
    }
}
