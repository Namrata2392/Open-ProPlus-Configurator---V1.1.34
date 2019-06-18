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
    * \brief     <b>ClosedLoopAction</b> is a class to store all the CLA's
    * \details   This class stores info related to all CLA's. It allows
    * user to add multiple CLA's. Whenever a CLA is added, it creates
    * a entry in Virtual Master for virtual parameter. It also exports the XML node related to this object.
    * 
    */
    public class ClosedLoopAction
    {
        #region Declaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "ClosedLoopAction";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private bool isNodeComment = false;
        private string comment = "";
        List<CLA> claList = new List<CLA>();
        ucClosedLoopAction uccla = new ucClosedLoopAction();
        #endregion Declaration
        public ClosedLoopAction()
        {
            string strRoutineName = "ClosedLoopAction";
            try
            {
                uccla.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                uccla.lvCLAItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvCLA_ItemCheck);
                uccla.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                uccla.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                uccla.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                uccla.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                uccla.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                uccla.btnNextClick += new System.EventHandler(this.btnNext_Click);
                uccla.btnLastClick += new System.EventHandler(this.btnLast_Click);
                uccla.lvCLADoubleClick += new System.EventHandler(this.lvCLA_DoubleClick);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvCLA_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lvCLA_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    uccla.lvCLA.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
                if (claList.Count >= Globals.MaxCLA)
                {
                    MessageBox.Show("Maximum " + Globals.MaxCLA + " Closed Loop Action's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(uccla.grpCLA);
                Utils.showNavigation(uccla.grpCLA, false);
                loadDefaults();
                uccla.txtCLAIndex.Text = (Globals.CLANo + 1).ToString();
                uccla.grpCLA.Visible = true;
                uccla.txtAINo1.Focus();
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
                if (uccla.lvCLA.Items.Count == 0)
                {
                    MessageBox.Show("Please add Closed loop Action ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (uccla.lvCLA.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Closed loop Action " + uccla.lvCLA.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = uccla.lvCLA.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            DeleteCLA(iIndex);

                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast Closed loop Action ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        public void DeleteCLA(int arrIndex)
        {
            string strRoutineName = "DeleteCLA";
            try
            {
                //Ajay: 25/09/2018
                //Utils.RemoveDI4CLA(Int32.Parse(claList[arrIndex].CLAIndex));
                bool IsAllow = Utils.RemoveDI4CLA(Int32.Parse(claList[arrIndex].CLAIndex));
                if (IsAllow) //Ajay: 25/09/2018
                {
                    claList.RemoveAt(arrIndex);
                    uccla.lvCLA.Items[arrIndex].Remove();
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
                List<KeyValuePair<string, string>> claData = Utils.getKeyValueAttributes(uccla.grpCLA);
                if (mode == Mode.ADD)
                {
                    claList.Add(new CLA("CLA", claData));
                    Utils.CreateDI4CLA(Int32.Parse(claList[claList.Count - 1].CLAIndex));
                }
                else if (mode == Mode.EDIT)
                {
                    claList[editIndex].updateAttributes(claData);
                }
                refreshList();
                //Namrata: 09/08/2017
                if (sender != null && e != null)
                {
                    uccla.grpCLA.Visible = false;
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
                uccla.grpCLA.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(uccla.grpCLA);
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
                Console.WriteLine("*** uccla btnFirst_Click clicked in class!!!");
                if (uccla.lvCLA.Items.Count <= 0) return;
                if (claList.ElementAt(0).IsNodeComment) return;
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
                Console.WriteLine("*** uccla btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (claList.ElementAt(editIndex - 1).IsNodeComment) return;
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
                Console.WriteLine("*** uccla btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= uccla.lvCLA.Items.Count) return;
                if (claList.ElementAt(editIndex + 1).IsNodeComment) return;
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
                Console.WriteLine("*** uccla btnLast_Click clicked in class!!!");
                if (uccla.lvCLA.Items.Count <= 0) return;
                if (claList.ElementAt(claList.Count - 1).IsNodeComment) return;
                editIndex = claList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvCLA_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvCLA_DoubleClick";
            try
            {
                if (uccla.lvCLA.SelectedItems.Count <= 0) return;
                ListViewItem lvi = uccla.lvCLA.SelectedItems[0];
                Utils.UncheckOthers(uccla.lvCLA, lvi.Index);
                if (claList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                uccla.grpCLA.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(uccla.grpCLA, true);
                loadValues();
                uccla.txtAINo1.Focus();
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
                uccla.txtAINo1.Text = "0";
                uccla.txtAINo2.Text = "0";
                uccla.txtDONo.Text = "0";
                uccla.txtHigh.Text = "0";
                uccla.txtLow.Text = "0";
                uccla.txtDelay.Text = "5";
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
                CLA cla = claList.ElementAt(editIndex);
                if (cla != null)
                {
                    uccla.txtCLAIndex.Text = cla.CLAIndex;
                    uccla.txtAINo1.Text = cla.AINo1;
                    uccla.txtAINo2.Text = cla.AINo2;
                    uccla.txtDONo.Text = cla.DONo;
                    uccla.txtHigh.Text = cla.High;
                    uccla.txtLow.Text = cla.Low;
                    uccla.txtDelay.Text = cla.DelaySec;
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
            if (Utils.IsEmptyFields(uccla.grpCLA))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check AI no. 1
            if (!Utils.IsGreaterThanZero(uccla.txtAINo1.Text))
            {
                MessageBox.Show("AI No. 1 should be greater than zero.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!Utils.IsValidAI(uccla.txtAINo1.Text))
            {
                MessageBox.Show("AI No. 1 is not a valid AI.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check AI no. 2
            if (Utils.IsLessThanZero(uccla.txtAINo2.Text))
            {
                MessageBox.Show("AI No. 2 cannot be less than zero.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (Utils.IsGreaterThanZero(uccla.txtAINo2.Text) && !Utils.IsValidAI(uccla.txtAINo2.Text))
            {
                MessageBox.Show("AI No. 2 does not exists.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check DO no.
            if (!Utils.IsGreaterThanZero(uccla.txtDONo.Text))
            {
                MessageBox.Show("DO No. should be greater than zero.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!Utils.IsValidDO(uccla.txtDONo.Text))
            {
                MessageBox.Show("DO No. does not exists.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check atleast one, high or low...
            if (Utils.IsLessThanEqual2Zero(uccla.txtHigh.Text) && Utils.IsLessThanEqual2Zero(uccla.txtLow.Text))
            {
                MessageBox.Show("Either High or Low values should be specified.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check High/Low should not be < 0...
            if (Utils.IsLessThanZero(uccla.txtHigh.Text) || Utils.IsLessThanZero(uccla.txtLow.Text))
            {
                MessageBox.Show("High/Low cannot be less than zero.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check Delay...
            if (Utils.IsLessThanZero(uccla.txtDelay.Text))
            {
                MessageBox.Show("Delay cannot be less than zero.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            //Fill combobox values, if any...
        }
        private void addListHeaders()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                uccla.lvCLA.Columns.Add("No.", 40, HorizontalAlignment.Left);
                uccla.lvCLA.Columns.Add("AI No. 1", 60, HorizontalAlignment.Left);
                uccla.lvCLA.Columns.Add("AI No. 2", 60, HorizontalAlignment.Left);
                uccla.lvCLA.Columns.Add("DO No.", 60, HorizontalAlignment.Left);
                uccla.lvCLA.Columns.Add("High", 70, HorizontalAlignment.Left);
                uccla.lvCLA.Columns.Add("Low", 70, HorizontalAlignment.Left);
                uccla.lvCLA.Columns.Add("Delay(sec)", -2, HorizontalAlignment.Left);
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
                uccla.lvCLA.Items.Clear();
                foreach (CLA claNode in claList)
                {
                    string[] row = new string[7];
                    int rNo = 0;
                    if (claNode.IsNodeComment)
                    {
                        row[rNo] = "Comment...";
                    }
                    else
                    {
                        row[rNo++] = claNode.CLAIndex;
                        row[rNo++] = claNode.AINo1;
                        row[rNo++] = claNode.AINo2;
                        row[rNo++] = claNode.DONo;
                        row[rNo++] = claNode.High;
                        row[rNo++] = claNode.Low;
                        row[rNo++] = claNode.DelaySec;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    uccla.lvCLA.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("ClosedLoopAction_")) return uccla;
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
            foreach (CLA clan in claList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(clan.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            foreach (CLA clan in claList)
            {
                iniData += "CLA_" + ctr++ + "=" + Utils.GetReportingIndex(slaveNum, slaveID, "AI", Int32.Parse(clan.AINo1)) + "," + Utils.GetReportingIndex(slaveNum, slaveID, "AI", Int32.Parse(clan.AINo2)) + "," + Utils.GetReportingIndex(slaveNum, slaveID, "DO", Int32.Parse(clan.DONo)) + "," + clan.High + "," + clan.Low + "," + clan.DelaySec;
            }
            return iniData;
        }
        public void regenerateSequence()
        {
            string strRoutineName = "regenerateSequence";
            try
            {
                int oCLANo = -1;
                int nCLANo = -1;
                //Reset CLA no.
                Globals.resetUniqueNos(ResetUniqueNos.CLA);
                Globals.CLANo++;//Start from 1...
                foreach (CLA cl in claList)
                {
                    oCLANo = Int32.Parse(cl.CLAIndex);
                    //nCLANo = Globals.IEC104SlaveNo++;
                    nCLANo = Globals.SlaveNo++;
                    cl.CLAIndex = nCLANo.ToString();

                    //Replace in Virtual master
                    foreach (VirtualMaster vm in Utils.getOpenProPlusHandle().getMasterConfiguration().getVirtualGroup().getVirtualMasters())
                    {
                        foreach (IED ied in vm.getIEDs())
                        {
                            ied.changeCLASequence(oCLANo, nCLANo);
                        }
                    }
                }
                Utils.getOpenProPlusHandle().getMasterConfiguration().getVirtualGroup().refreshList();
                Utils.getOpenProPlusHandle().getParameterLoadConfiguration().getClosedLoopAction().refreshList();
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
                foreach (CLA cla in claList)
                {
                    cla.IsReindexedAINo1 = false;
                    cla.IsReindexedAINo2 = false;
                    cla.IsReindexedDONo = false;
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
                //Do not break as there can be one AI referred in multiple CLA's...
                foreach (CLA cla in claList)
                {
                    if (cla.AINo1 == oAINo.ToString() && !cla.IsReindexedAINo1)
                    {
                        cla.AINo1 = nAINo.ToString();
                        cla.IsReindexedAINo1 = true;
                    }
                    if (cla.AINo2 == oAINo.ToString() && !cla.IsReindexedAINo2)
                    {
                        cla.AINo2 = nAINo.ToString();
                        cla.IsReindexedAINo2 = true;
                    }
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ChangeDOSequence(int oDONo, int nDONo)
        {
            string strRoutineName = "ChangeDOSequence";
            try
            {
                //Do not break as there can be one DO referred in multiple CLA's...
                foreach (CLA cla in claList)
                {
                    if (cla.DONo == oDONo.ToString() && !cla.IsReindexedDONo)
                    {
                        cla.DONo = nDONo.ToString();
                        cla.IsReindexedDONo = true;
                    }
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void parseCLAGNode(XmlNode clagNode, TreeNode tn)
        {
            string strRoutineName = "parseCLAGNode";
            try
            {
                //First set root node name...
                rnName = clagNode.Name;
                if (clagNode.NodeType == XmlNodeType.Comment)
                {
                    isNodeComment = true;
                    comment = clagNode.Value;
                }
                if (tn != null) tn.Nodes.Clear();
                foreach (XmlNode node in clagNode)
                {
                    //Utils.WriteLine("***** node type: {0}", node.NodeType);
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                                                                       //No need: TreeNode tmp = tn.Nodes.Add("CLA_"+Utils.GenerateShortUniqueKey(), "CLA", "CLA", "CLA");
                    claList.Add(new CLA(node));
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
            foreach (CLA claNode in claList)
            {
                if (claNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<CLA> getCLAs()
        {
            return claList;
        }
    }
}
