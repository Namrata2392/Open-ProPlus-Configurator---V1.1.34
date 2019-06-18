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
    #region Declaration
    public class ADRGroup
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "ADRGroup";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        List<ADRMaster> ADRlist = new List<ADRMaster>();
        ucADRGroup ucadrgroup = new ucADRGroup();
        private TreeNode ADRGroupTreeNode;
        #endregion Declaration
        public ADRGroup(TreeNode tn)
        {
            string strRoutineName = "ADRGroup";
            try
            {
                tn.Nodes.Clear();
            ADRGroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
            ucadrgroup.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucadrgroup.lvIADRMasterItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIADRMaster_ItemCheck);
            ucadrgroup.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucadrgroup.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucadrgroup.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucadrgroup.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            ucadrgroup.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            ucadrgroup.btnNextClick += new System.EventHandler(this.btnNext_Click);
            ucadrgroup.btnLastClick += new System.EventHandler(this.btnLast_Click);
            ucadrgroup.lvIADRMasterDoubleClick += new System.EventHandler(this.lvIADRMaster_DoubleClick);
            addListHeaders();
            fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvIADRMaster_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lvIADRMaster_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucadrgroup.lvIADRMaster.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
                if (ADRlist.Count >= Globals.MaxADRMaster)
                {
                    MessageBox.Show("Maximum one ADR master is supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Console.WriteLine("*** ucm103 btnAdd_Click clicked in class!!!");
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucadrgroup.grpADR);
                Utils.showNavigation(ucadrgroup.grpADR, false);
                loadDefaults();
                ucadrgroup.txtMasterNo.Text = (Globals.MasterNo + 1).ToString();
                ucadrgroup.grpADR.Visible = true;
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
                if (ucadrgroup.lvIADRMaster.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one master ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucadrgroup.lvIADRMaster.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Master " + ucadrgroup.lvIADRMaster.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucadrgroup.lvIADRMaster.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            ADRGroupTreeNode.Nodes.Remove(ADRlist.ElementAt(iIndex).getTreeNode());
                            ADRlist.RemoveAt(iIndex);
                            ucadrgroup.lvIADRMaster.Items[iIndex].Remove();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one Master ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    refreshList();
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

                Console.WriteLine("*** ucm103 btnDone_Click clicked in class!!!");
                List<KeyValuePair<string, string>> adrdata = Utils.getKeyValueAttributes(ucadrgroup.grpADR);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = ADRGroupTreeNode.Nodes.Add("ADR_" + Utils.GenerateShortUniqueKey(), "ADR", "ADRMaster", "ADRMaster");
                    ADRlist.Add(new ADRMaster("ADR", adrdata, tmp));
                }
                else if (mode == Mode.EDIT)
                {
                    ADRlist[editIndex].updateAttributes(adrdata);
                }
                refreshList();
                ucadrgroup.grpADR.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnCancel_Click";
            try
            {
                Console.WriteLine("*** ucm103 btnCancel_Click clicked in class!!!");
                ucadrgroup.grpADR.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucadrgroup.grpADR);
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
                Console.WriteLine("*** ucm103 btnFirst_Click clicked in class!!!");
                if (ucadrgroup.lvIADRMaster.Items.Count <= 0) return;
                if (ADRlist.ElementAt(0).IsNodeComment) return;
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
                Console.WriteLine("*** ucm103 btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (ADRlist.ElementAt(editIndex - 1).IsNodeComment) return;
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
                Console.WriteLine("*** ucm103 btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= ucadrgroup.lvIADRMaster.Items.Count) return;
                if (ADRlist.ElementAt(editIndex + 1).IsNodeComment) return;
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
                Console.WriteLine("*** ucm103 btnLast_Click clicked in class!!!");
                if (ucadrgroup.lvIADRMaster.Items.Count <= 0) return;
                if (ADRlist.ElementAt(ADRlist.Count - 1).IsNodeComment) return;
                editIndex = ADRlist.Count - 1;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvIADRMaster_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvIADRMaster_DoubleClick";
            try
            {
                Console.WriteLine("*** ucm103 lvIEC103Master_DoubleClick clicked in class!!!");
                if (ucadrgroup.lvIADRMaster.SelectedItems.Count <= 0) return;

                ListViewItem lvi = ucadrgroup.lvIADRMaster.SelectedItems[0];
                Utils.UncheckOthers(ucadrgroup.lvIADRMaster, lvi.Index);
                if (ADRlist.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucadrgroup.grpADR.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucadrgroup.grpADR, true);
                loadValues();
            }

            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "loadDefaults";
            try
            {
                ucadrgroup.cmbDebug.SelectedIndex = ucadrgroup.cmbDebug.FindStringExact("3");
                ucadrgroup.txtClockSync.Text = "60";
                ucadrgroup.txtPollingInterval.Text = "5";
                ucadrgroup.txtPoolingTime.Text = "300";
                ucadrgroup.txtRefreshInterval.Text = "120";
                ucadrgroup.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION;
                ucadrgroup.txtDescription.Text = "ADR_" + (Globals.MasterNo + 1).ToString();
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
                ADRMaster M103 = ADRlist.ElementAt(editIndex);
                if (M103 != null)
                {
                    ucadrgroup.txtMasterNo.Text = M103.MasterNum;
                    ucadrgroup.txtClockSync.Text = M103.ClockSyncInterval;
                    ucadrgroup.cmbDebug.SelectedIndex = ucadrgroup.cmbDebug.FindStringExact(M103.DEBUG);
                    ucadrgroup.txtPollingInterval.Text = M103.PollingIntervalmSec;
                    ucadrgroup.txtPoolingTime.Text = M103.PortTimesyncSec;
                    ucadrgroup.txtRefreshInterval.Text = M103.RefreshInterval;
                    ucadrgroup.txtFirmwareVersion.Text = M103.AppFirmwareVersion;
                    if (M103.Run.ToLower() == "yes") ucadrgroup.chkRun.Checked = true;
                    else ucadrgroup.chkRun.Checked = false;
                    ucadrgroup.txtDescription.Text = M103.Description;
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
                if (Utils.IsEmptyFields(ucadrgroup.grpADR))
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
        private void fillOptions()
        {
            string strRoutineName = "fillOptions";
            try
            {
                //Fill Debug levels...
                ucadrgroup.cmbDebug.Items.Clear();
                for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
                {
                    ucadrgroup.cmbDebug.Items.Add(i.ToString());
                }
                ucadrgroup.cmbDebug.SelectedIndex = 0;
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
                ucadrgroup.lvIADRMaster.Columns.Add("Master No.", 90, HorizontalAlignment.Left);
                ucadrgroup.lvIADRMaster.Columns.Add("Clock Sync Interval (sec)", 130, HorizontalAlignment.Left);
                ucadrgroup.lvIADRMaster.Columns.Add("Polling Interval (msec)", 120, HorizontalAlignment.Left);
                ucadrgroup.lvIADRMaster.Columns.Add("Port Time Sync (sec)", 120, HorizontalAlignment.Left);
                ucadrgroup.lvIADRMaster.Columns.Add("Refresh Interval (sec)", 120, HorizontalAlignment.Left);
                ucadrgroup.lvIADRMaster.Columns.Add("Firmware Version", 130, HorizontalAlignment.Left);
                ucadrgroup.lvIADRMaster.Columns.Add("Debug Level", 110, HorizontalAlignment.Left);
                ucadrgroup.lvIADRMaster.Columns.Add("Description",180, HorizontalAlignment.Left);
                ucadrgroup.lvIADRMaster.Columns.Add("Run", 90, HorizontalAlignment.Left);
                ucadrgroup.lvIADRMaster.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "refreshList";
            try
            {
                int cnt = 0;
                ucadrgroup.lvIADRMaster.Items.Clear();

                foreach (ADRMaster mt in ADRlist)
                {
                    string[] row = new string[10];
                    if (mt.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = mt.MasterNum;
                        row[1] = mt.ClockSyncInterval;
                        row[2] = mt.PollingIntervalmSec;
                        row[3] = mt.PortTimesyncSec;
                        row[4] = mt.RefreshInterval;
                        row[5] = mt.AppFirmwareVersion;
                        row[6] = mt.DEBUG;
                        row[7] = mt.Description;
                        row[8] = mt.Run;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucadrgroup.lvIADRMaster.Items.Add(lvItem);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            try
            {
                if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("ADRGroup_"))
                {
                    return ucadrgroup;
                }
                kpArr.RemoveAt(0);
                if (kpArr.ElementAt(0).Contains("ADR_"))
                {
                    int idx = -1;
                    string[] elems = kpArr.ElementAt(0).Split('_');
                    Console.WriteLine("$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                    idx = Int32.Parse(elems[elems.Length - 1]);
                    if (ADRlist.Count <= 0) return null;
                    return ADRlist[idx].getView(kpArr);
                }
                else
                {
                    Console.WriteLine("***** View for element: {0} not supported!!!", kpArr.ElementAt(0));
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public XmlNode exportXMLnode()
        {
            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            XmlNode rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            foreach (ADRMaster mn in ADRlist)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(mn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            try
            {
                string iniData = "";
                foreach (ADRMaster adr in ADRlist)
                {
                    iniData += adr.exportINI(slaveNum, slaveID, element, ref ctr);
                }
                return iniData;
            }
            catch (Exception Ex)
            {
                throw Ex;
                //MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void regenerateAISequence()
        {
            string strRoutineName = "regenerateAISequence";
            try
            {
                foreach (ADRMaster adr in ADRlist)
                {
                    foreach (IED ied in adr.getIEDs())
                    {
                        ied.regenerateAISequence();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateDISequence()
        {
            string strRoutineName = "regenerateDISequence";
            try
            {
                foreach (ADRMaster adr in ADRlist)
                {
                    foreach (IED ied in adr.getIEDs())
                    {
                        ied.regenerateDISequence();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateDOSequence()
        {
            string strRoutineName = "regenerateDOSequence";
            try
            {
                foreach (ADRMaster adr in ADRlist)
                {
                    foreach (IED ied in adr.getIEDs())
                    {
                        ied.regenerateDOSequence();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateENSequence()
        {
            string strRoutineName = "regenerateENSequence";
            try
            {
                foreach (ADRMaster adr in ADRlist)
                {
                    foreach (IED ied in adr.getIEDs())
                    {
                        ied.regenerateENSequence();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void parseIECGNode(XmlNode iecgNode, TreeNode tn)
        {
            string strRoutineName = "parseIECGNode";
            try
            {
                rnName = iecgNode.Name;
                tn.Nodes.Clear();
                ADRGroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
                foreach (XmlNode node in iecgNode)
                {
                    XmlElement xParentEle = node.ParentNode as XmlElement;
                    //Namarta: 06/03/2018
                    //Check if XML Attribute exist in XML Node
                    if ((xParentEle != null) && !xParentEle.HasAttribute("ClockSyncInterval"))
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(Utils.XMLFileName);
                        XmlAttribute attrName = xmlDoc.CreateAttribute("ClockSyncInterval");
                        attrName.Value = "60";
                        node.Attributes.SetNamedItem(attrName);
                    }
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    TreeNode tmp = tn.Nodes.Add("ADR_" + Utils.GenerateShortUniqueKey(), "ADR", "ADRMaster", "ADRMaster");
                    ADRlist.Add(new ADRMaster(node, tmp));
                }

                for (int i = 0; i < ADRlist.Count; i++)
                {
                    if (ADRlist[i].ClockSyncInterval == "")
                    {
                        ADRlist[i].ClockSyncInterval = "60";
                        ucadrgroup.txtClockSync.Text = "60";
                    }
                }
                refreshList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public int getCount()
        {
            try
            {
                int ctr = 0;
                foreach (ADRMaster adr in ADRlist)
                {
                    if (adr.IsNodeComment) continue;
                    ctr++;
                }
                return ctr;
            }
            catch (Exception Ex)
            {
                throw Ex;
                //MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public List<ADRMaster> getADRMasters()
        {
            return ADRlist;
        }
        public List<ADRMaster> getADRMastersByFilter(string masterID)
        {
            try
            {
                List<ADRMaster> mList = new List<ADRMaster>();
                if (masterID.ToLower() == "all") return ADRlist;
                else
                    foreach (ADRMaster iec in ADRlist)
                    {
                        if (iec.getMasterID == masterID)
                        {
                            mList.Add(iec);
                            break;
                        }
                    }

                return mList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<IED> getIEC103IEDsByFilter(string masterID)
        {
            try
            {
                List<IED> iLst = new List<IED>();
                if (masterID.ToLower() == "all")
                {
                    foreach (ADRMaster adr in ADRlist)
                    {
                        foreach (IED ied in adr.getIEDs())
                        {
                            iLst.Add(ied);
                        }
                    }
                }
                else
                {
                    foreach (ADRMaster adr in ADRlist)
                    {
                        if (adr.getMasterID == masterID)
                        {
                            foreach (IED ied in adr.getIEDs())
                            {
                                iLst.Add(ied);
                            }
                            break;
                        }
                    }
                }

                return iLst;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
