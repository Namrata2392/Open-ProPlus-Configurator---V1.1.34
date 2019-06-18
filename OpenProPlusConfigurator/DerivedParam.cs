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
    * \brief     <b>DerivedParam</b> is a class to store all the DP's
    * \details   This class stores info related to all DP's. It allows
    * user to add multiple DP's. Whenever a DP is added, it creates
    * a entry in Virtual Master for virtual parameter. It also exports the XML node related to this object.
    * 
    */
    public class DerivedParam
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "DerivedParam";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private bool isNodeComment = false;
        private string comment = "";
        List<DP> dpList = new List<DP>();
        ucDerivedParam ucdp = new ucDerivedParam();
        public DerivedParam()
        {
            string strRoutineName = "DerivedParam";
            try
            {
                ucdp.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucdp.lvDPItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvDP_ItemCheck);
                ucdp.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucdp.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucdp.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucdp.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucdp.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucdp.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucdp.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucdp.lvDPDoubleClick += new System.EventHandler(this.lvDP_DoubleClick);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvDP_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lvDP_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucdp.lvDP.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
                if (dpList.Count >= Globals.MaxDerivedParam)
                {
                    MessageBox.Show("Maximum " + Globals.MaxDerivedParam + " Derived Param's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucdp.grpDP);
                Utils.showNavigation(ucdp.grpDP, false);
                loadDefaults();
                ucdp.txtDPIndex.Text = (Globals.DPNo + 1).ToString();
                ucdp.grpDP.Visible = true;
                ucdp.txtAINo1.Focus();
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
                if (ucdp.lvDP.Items.Count == 0)
                {
                    MessageBox.Show("Please add Derived Parameter ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucdp.lvDP.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete  Derived Parameter " + ucdp.lvDP.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucdp.lvDP.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            DeleteDP(iIndex);

                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast  Derived Parameter ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        public void DeleteDP(int arrIndex)
        {
            string strRoutineName = "DeleteDP";
            try
            {
                //Ajay: 25/09/2018
                //Utils.RemoveAI4DP(Int32.Parse(dpList[arrIndex].DPIndex));
                bool IsAllow = Utils.RemoveAI4DP(Int32.Parse(dpList[arrIndex].DPIndex));
                if (IsAllow) //Ajay: 25/09/2018
                {
                    dpList.RemoveAt(arrIndex);
                    ucdp.lvDP.Items[arrIndex].Remove();
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

                List<KeyValuePair<string, string>> dpData = Utils.getKeyValueAttributes(ucdp.grpDP);
                if (mode == Mode.ADD)
                {
                    dpList.Add(new DP("DP", dpData));
                    Utils.CreateAI4DP(Int32.Parse(dpList[dpList.Count - 1].DPIndex));
                }
                else if (mode == Mode.EDIT)
                {
                    dpList[editIndex].updateAttributes(dpData);
                }

                refreshList();
                //Namrata: 09/08/2017
                if (sender != null && e != null)
                {
                    ucdp.grpDP.Visible = false;
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
                ucdp.grpDP.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucdp.grpDP);
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
                Console.WriteLine("*** ucdp btnFirst_Click clicked in class!!!");
                if (ucdp.lvDP.Items.Count <= 0) return;
                if (dpList.ElementAt(0).IsNodeComment) return;
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
                Console.WriteLine("*** ucdp btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (dpList.ElementAt(editIndex - 1).IsNodeComment) return;
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
                Console.WriteLine("*** ucdp btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= ucdp.lvDP.Items.Count) return;
                if (dpList.ElementAt(editIndex + 1).IsNodeComment) return;
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
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** ucdp btnLast_Click clicked in class!!!");
                if (ucdp.lvDP.Items.Count <= 0) return;
                if (dpList.ElementAt(dpList.Count - 1).IsNodeComment) return;
                editIndex = dpList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvDP_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvDP_DoubleClick";
            try
            {
                if (ucdp.lvDP.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucdp.lvDP.SelectedItems[0];
                Utils.UncheckOthers(ucdp.lvDP, lvi.Index);
                if (dpList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucdp.grpDP.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucdp.grpDP, true);
                loadValues();
                ucdp.txtAINo1.Focus();
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
            if (Utils.IsEmptyFields(ucdp.grpDP))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check AI no. 1
            if (!Utils.IsGreaterThanZero(ucdp.txtAINo1.Text))
            {
                MessageBox.Show("AI No. 1 should be greater than zero.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!Utils.IsValidAI(ucdp.txtAINo1.Text))
            {
                MessageBox.Show("AI No. 1 is not a valid AI.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check AI no. 2
            if (Utils.IsLessThanZero(ucdp.txtAINo2.Text))
            {
                MessageBox.Show("AI No. 2 cannot be less than zero.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (Utils.IsGreaterThanZero(ucdp.txtAINo2.Text) && !Utils.IsValidAI(ucdp.txtAINo2.Text))
            {
                MessageBox.Show("AI No. 2 does not exists.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check Delay...
            if (Utils.IsLessThanZero(ucdp.txtDelayMS.Text))
            {
                MessageBox.Show("Delay cannot be less than zero.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void loadDefaults()
        {
            string strRoutineName = "loadDefaults";
            try
            {
                ucdp.txtAINo1.Text = "0";
                ucdp.txtAINo2.Text = "0";
                ucdp.cmbOperation.SelectedIndex = ucdp.cmbOperation.FindStringExact("ADD");
                ucdp.txtDelayMS.Text = "10";
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
                DP dp = dpList.ElementAt(editIndex);
                if (dp != null)
                {
                    ucdp.txtDPIndex.Text = dp.DPIndex;
                    ucdp.txtAINo1.Text = dp.AINo1;
                    ucdp.txtAINo2.Text = dp.AINo2;
                    ucdp.cmbOperation.SelectedIndex = ucdp.cmbOperation.FindStringExact(dp.Operation);
                    ucdp.txtDelayMS.Text = dp.DelayMS;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fillOptions()
        {
            string strRoutineName = "loadValues";
            try
            {
                //Fill Operation...
                ucdp.cmbOperation.Items.Clear();
                foreach (String op in DP.getOperations())
                {
                    ucdp.cmbOperation.Items.Add(op.ToString());
                }
                ucdp.cmbOperation.SelectedIndex = 0;
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
                ucdp.lvDP.Columns.Add("No.", 40, HorizontalAlignment.Left);
                ucdp.lvDP.Columns.Add("AI No. 1", 60, HorizontalAlignment.Left);
                ucdp.lvDP.Columns.Add("AI No. 2", 60, HorizontalAlignment.Left);
                ucdp.lvDP.Columns.Add("Operation", 80, HorizontalAlignment.Left);
                ucdp.lvDP.Columns.Add("Delay (ms)", -2, HorizontalAlignment.Left);
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
                ucdp.lvDP.Items.Clear();
                foreach (DP dpNode in dpList)
                {
                    string[] row = new string[5];
                    int rNo = 0;
                    if (dpNode.IsNodeComment)
                    {
                        row[rNo] = "Comment...";
                    }
                    else
                    {
                        row[rNo++] = dpNode.DPIndex;
                        row[rNo++] = dpNode.AINo1;
                        row[rNo++] = dpNode.AINo2;
                        row[rNo++] = dpNode.Operation;
                        row[rNo++] = dpNode.DelayMS;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucdp.lvDP.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("DerivedParam_")) return ucdp;
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
            foreach (DP dpn in dpList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(dpn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public void regenerateSequence()
        {
            string strRoutineName = "regenerateSequence";
            try
            {
                int oDPNo = -1;
                int nDPNo = -1;
                //Reset DP no.
                Globals.resetUniqueNos(ResetUniqueNos.DP);
                Globals.DPNo++;//Start from 1...
                foreach (DP dp in dpList)
                {
                    oDPNo = Int32.Parse(dp.DPIndex);
                    nDPNo = Globals.DPNo++;
                    dp.DPIndex = nDPNo.ToString();
                    //Replace in Virtual master
                    foreach (VirtualMaster vm in Utils.getOpenProPlusHandle().getMasterConfiguration().getVirtualGroup().getVirtualMasters())
                    {
                        foreach (IED ied in vm.getIEDs())
                        {
                            ied.changeDPSequence(oDPNo, nDPNo);
                        }
                    }
                }
                Utils.getOpenProPlusHandle().getMasterConfiguration().getVirtualGroup().refreshList();
                Utils.getOpenProPlusHandle().getParameterLoadConfiguration().getDerivedParam().refreshList();
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
                foreach (DP dp in dpList)
                {
                    dp.IsReindexedAINo1 = false;
                    dp.IsReindexedAINo2 = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ChangeAISequence(int oAINo, int nAINo)
        {
            string strRoutineName = "ChangeAISequence";
            try
            {
                //Do not break as there can be one AI referred in multiple DP's...
                foreach (DP dp in dpList)
                {
                    if (dp.AINo1 == oAINo.ToString() && !dp.IsReindexedAINo1)
                    {
                        dp.AINo1 = nAINo.ToString();
                        dp.IsReindexedAINo1 = true;
                    }
                    if (dp.AINo2 == oAINo.ToString() && !dp.IsReindexedAINo2)
                    {
                        dp.AINo2 = nAINo.ToString();
                        dp.IsReindexedAINo2 = true;
                    }
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void parseDPGNode(XmlNode dpgNode, TreeNode tn)
        {
            string strRoutineName = "ChangeAISequence";
            try
            {
                //First set root node name...
                rnName = dpgNode.Name;
                if (dpgNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = dpgNode.Value;
                }
                if (tn != null) tn.Nodes.Clear();
                foreach (XmlNode node in dpgNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;
                    dpList.Add(new DP(node));
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
            foreach (DP dpNode in dpList)
            {
                if (dpNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<DP> getDPs()
        {
            return dpList;
        }
    }
}
