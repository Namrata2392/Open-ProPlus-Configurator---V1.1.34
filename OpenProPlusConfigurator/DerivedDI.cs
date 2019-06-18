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
    * \brief     <b>DerivedDI</b> is a class to store all the DD's
    * \details   This class stores info related to all DD's. It allows
    * user to add multiple DD's. Whenever a DD is added, it creates
    * a entry in Virtual Master for virtual parameter. It also exports the XML node related to this object.
    * 
    */
    public class DerivedDI
    {
        #region Declaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "DerivedDI";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private bool isNodeComment = false;
        private string comment = "";
        List<DD> ddList = new List<DD>();
        ucDerivedDI ucdd = new ucDerivedDI();
        #endregion Declaration
        public DerivedDI()
        {
            string strRoutineName = "DerivedDI";
            try
            {
                ucdd.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucdd.lvDDItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvDD_ItemCheck);
                ucdd.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucdd.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucdd.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucdd.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucdd.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucdd.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucdd.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucdd.lvDDDoubleClick += new System.EventHandler(this.lvDD_DoubleClick);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvDD_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lvDD_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucdd.lvDD.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAdd_Click";
            try
            {
                if (ddList.Count >= Globals.MaxDerivedDI)
                {
                    MessageBox.Show("Maximum " + Globals.MaxDerivedDI + " Derived DI's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucdd.grpDD);
                Utils.showNavigation(ucdd.grpDD, false);
                loadDefaults();
                ucdd.txtDDIndex.Text = (Globals.DDNo + 1).ToString();
                ucdd.grpDD.Visible = true;
                ucdd.txtDINo1.Focus();
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
                //Utils.WriteLine(VerboseLevel.DEBUG, "*** mbsList count: {0} lv count: {1}", mbsList.Count, ucmbs.lvMODBUSSlave.Items.Count);
                if (ucdd.lvDD.Items.Count == 0)
                {
                    MessageBox.Show("Please add Derived DI ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucdd.lvDD.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Derived DI" + ucdd.lvDD.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucdd.lvDD.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            DeleteDD(iIndex);

                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast Derived DI ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    //Utils.WriteLine(VerboseLevel.DEBUG, "*** mbsList count: {0} lv count: {1}", mbsList.Count, ucmbs.lvMODBUSSlave.Items.Count);
                    refreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DeleteDD(int arrIndex)
        {
            string strRoutineName = "DeleteDD";
            try
            {
                //Ajay: 25/09/2018
                //Utils.RemoveDI4DD(Int32.Parse(ddList[arrIndex].DDIndex));
                bool IsAllow = Utils.RemoveDI4DD(Int32.Parse(ddList[arrIndex].DDIndex));
                if (IsAllow) //Ajay: 25/09/2018
                {
                    ddList.RemoveAt(arrIndex);
                    ucdd.lvDD.Items[arrIndex].Remove();
                }
                else { }
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

                List<KeyValuePair<string, string>> ddData = Utils.getKeyValueAttributes(ucdd.grpDD);
                if (mode == Mode.ADD)
                {
                    ddList.Add(new DD("DD", ddData));
                    Utils.CreateDI4DD(Int32.Parse(ddList[ddList.Count - 1].DDIndex));
                }
                else if (mode == Mode.EDIT)
                {
                    ddList[editIndex].updateAttributes(ddData);
                }

                refreshList();
                //Namrata: 09/08/2017
                if (sender != null && e != null)
                {
                    ucdd.grpDD.Visible = false;
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
                ucdd.grpDD.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucdd.grpDD);
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
                Console.WriteLine("*** ucdd btnFirst_Click clicked in class!!!");
                if (ucdd.lvDD.Items.Count <= 0) return;
                if (ddList.ElementAt(0).IsNodeComment) return;
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
                Console.WriteLine("*** ucdd btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (ddList.ElementAt(editIndex - 1).IsNodeComment) return;
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
                Console.WriteLine("*** ucdd btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= ucdd.lvDD.Items.Count) return;
                if (ddList.ElementAt(editIndex + 1).IsNodeComment) return;
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
                Console.WriteLine("*** ucdd btnLast_Click clicked in class!!!");
                if (ucdd.lvDD.Items.Count <= 0) return;
                if (ddList.ElementAt(ddList.Count - 1).IsNodeComment) return;
                editIndex = ddList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvDD_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvDD_DoubleClick";
            try
            {
                if (ucdd.lvDD.SelectedItems.Count <= 0) return;

                ListViewItem lvi = ucdd.lvDD.SelectedItems[0];
                Utils.UncheckOthers(ucdd.lvDD, lvi.Index);
                if (ddList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucdd.grpDD.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucdd.grpDD, true);
                loadValues();
                ucdd.txtDINo1.Focus();
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
                ucdd.txtDINo1.Text = "0";
                ucdd.txtDINo2.Text = "0";
                ucdd.cmbOperation.SelectedIndex = ucdd.cmbOperation.FindStringExact("OR");
                ucdd.txtDelayMS.Text = "10";
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
                DD dd = ddList.ElementAt(editIndex);
                if (dd != null)
                {
                    ucdd.txtDDIndex.Text = dd.DDIndex;
                    ucdd.txtDINo1.Text = dd.DINo1;
                    ucdd.txtDINo2.Text = dd.DINo2;
                    ucdd.cmbOperation.SelectedIndex = ucdd.cmbOperation.FindStringExact(dd.Operation);
                    ucdd.txtDelayMS.Text = dd.DelayMS;
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
            if (Utils.IsEmptyFields(ucdd.grpDD))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check DI no. 1
            if (!Utils.IsGreaterThanZero(ucdd.txtDINo1.Text))
            {
                MessageBox.Show("DI No. 1 should be greater than zero.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!Utils.IsValidDI(ucdd.txtDINo1.Text))
            {
                MessageBox.Show("DI No. 1 is not a valid DI.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check DI no. 2
            if (Utils.IsLessThanZero(ucdd.txtDINo2.Text))
            {
                MessageBox.Show("DI No. 2 cannot be less than zero.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (Utils.IsGreaterThanZero(ucdd.txtDINo2.Text) && !Utils.IsValidDI(ucdd.txtDINo2.Text))
            {
                MessageBox.Show("DI No. 2 does not exists.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check Delay...
            if (Utils.IsLessThanZero(ucdd.txtDelayMS.Text))
            {
                MessageBox.Show("Delay cannot be less than zero.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return status;
        }
        private void fillOptions()
        {
            string strRoutineName = "fillOptions";
            try
            {
                //Fill Operation...
                ucdd.cmbOperation.Items.Clear();
                foreach (String op in DD.getOperations())
                {
                    ucdd.cmbOperation.Items.Add(op.ToString());
                }
                ucdd.cmbOperation.SelectedIndex = 0;
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
                ucdd.lvDD.Columns.Add("No.", 40, HorizontalAlignment.Left);
                ucdd.lvDD.Columns.Add("DI No. 1", 60, HorizontalAlignment.Left);
                ucdd.lvDD.Columns.Add("DI No. 2", 60, HorizontalAlignment.Left);
                ucdd.lvDD.Columns.Add("Operation", 80, HorizontalAlignment.Left);
                ucdd.lvDD.Columns.Add("Delay (ms)", -2, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void refreshList()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                int cnt = 0;
                ucdd.lvDD.Items.Clear();
                foreach (DD ddNode in ddList)
                {
                    string[] row = new string[5];
                    int rNo = 0;
                    if (ddNode.IsNodeComment)
                    {
                        row[rNo] = "Comment...";
                    }
                    else
                    {
                        row[rNo++] = ddNode.DDIndex;
                        row[rNo++] = ddNode.DINo1;
                        row[rNo++] = ddNode.DINo2;
                        row[rNo++] = ddNode.Operation;
                        row[rNo++] = ddNode.DelayMS;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucdd.lvDD.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("DerivedDI_")) return ucdd;
            return null;
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
            rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            foreach (DD ddn in ddList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(ddn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public void regenerateSequence()
        {
            string strRoutineName = "regenerateSequence";
            try
            {
                int oDDNo = -1;
                int nDDNo = -1;
                //Reset DD no.
                Globals.resetUniqueNos(ResetUniqueNos.DD);
                Globals.DDNo++;//Start from 1...
                foreach (DD dd in ddList)
                {
                    oDDNo = Int32.Parse(dd.DDIndex);
                    nDDNo = Globals.DDNo++;
                    dd.DDIndex = nDDNo.ToString();
                    //Replace in Virtual master
                    foreach (VirtualMaster vm in Utils.getOpenProPlusHandle().getMasterConfiguration().getVirtualGroup().getVirtualMasters())
                    {
                        foreach (IED ied in vm.getIEDs())
                        {
                            ied.changeDDSequence(oDDNo, nDDNo);
                        }
                    }
                }
                Utils.getOpenProPlusHandle().getMasterConfiguration().getVirtualGroup().refreshList();
                Utils.getOpenProPlusHandle().getParameterLoadConfiguration().getDerivedDI().refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void resetReindexFlags()
        {
            string strRoutineName = "resetReindexFlags";
            try
            {
                foreach (DD dd in ddList)
                {
                    dd.IsReindexedDINo1 = false;
                    dd.IsReindexedDINo2 = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ChangeDISequence(int oDINo, int nDINo)
        {
            string strRoutineName = "ChangeDISequence";
            try
            {
                //Do not break as there can be one DI referred in multiple DD's...
                foreach (DD dd in ddList)
                {
                    if (dd.DINo1 == oDINo.ToString() && !dd.IsReindexedDINo1)
                    {
                        dd.DINo1 = nDINo.ToString();
                        dd.IsReindexedDINo1 = true;
                    }
                    if (dd.DINo2 == oDINo.ToString() && !dd.IsReindexedDINo2)
                    {
                        dd.DINo2 = nDINo.ToString();
                        dd.IsReindexedDINo2 = true;
                    }
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void parseDDGNode(XmlNode ddgNode, TreeNode tn)
        {
            string strRoutineName = "parseDDGNode";
            try
            {
                //First set root node name...
                rnName = ddgNode.Name;
                if (ddgNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = ddgNode.Value;
                }
                if (tn != null) tn.Nodes.Clear();
                foreach (XmlNode node in ddgNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    ddList.Add(new DD(node));
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }

        public int getCount()
        {
            int ctr = 0;
            foreach (DD ddNode in ddList)
            {
                if (ddNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }

        public List<DD> getDDs()
        {
            return ddList;
        }
    }
}
