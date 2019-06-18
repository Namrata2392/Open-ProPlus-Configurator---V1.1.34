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
    * \brief     <b>IEC103Group</b> is a class to store all the 
    's
    * \details   This class stores info related to all IEC103Master's. It allows
    * user to add multiple IEC103Master's. It also exports the XML node related to this object.
    * 
    */
    public class IEC103Group
    {
        #region Declaration
        OpenProPlus_Config opcHandle;
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "IEC103Group";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        public List<IEC103Master> m103List = new List<IEC103Master>();
        ucGroupIEC103 ucm103 = new ucGroupIEC103();
        ucAIlist ucai = new ucAIlist();
        private TreeNode IEC103GroupTreeNode;
        #endregion Declaration
        public IEC103Group(TreeNode tn)
        {
            string strRoutineName = "IEC103Group";
            try
            {
                tn.Nodes.Clear();
                IEC103GroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
                ucm103.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucm103.lvIEC103MasterItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEC103Master_ItemCheck);
                ucm103.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucm103.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucm103.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucm103.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucm103.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucm103.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucm103.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucm103.lvIEC103MasterDoubleClick += new System.EventHandler(this.lvIEC103Master_DoubleClick);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvIEC103Master_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lvIEC103Master_ItemCheck";
            try
            {
                try
                {
                    var listView = sender as ListView;
                    if (listView != null)
                    {
                        int index = e.Index;
                        ucm103.lvIEC103Master.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
                if (m103List.Count >= Globals.MaxIEC103Master)
                {
                    MessageBox.Show("Maximum " + Globals.MaxIEC103Master + " IEC103 Masters are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Console.WriteLine("*** ucm103 btnAdd_Click clicked in class!!!");
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucm103.grpIEC103);
                Utils.showNavigation(ucm103.grpIEC103, false);
                loadDefaults();
                //Namrata:29/7/2017
                ucm103.txtMasterNo.Text = (Globals.MasterNo + 1).ToString();
                ucm103.grpIEC103.Visible = true;
                ucm103.cmbPortNo.Focus();
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
                if (ucm103.lvIEC103Master.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one Master ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucm103.lvIEC103Master.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Master " + ucm103.lvIEC103Master.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucm103.lvIEC103Master.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            IEC103GroupTreeNode.Nodes.Remove(m103List.ElementAt(iIndex).getTreeNode());
                            m103List.RemoveAt(iIndex);
                            ucm103.lvIEC103Master.Items[iIndex].Remove();
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
                List<KeyValuePair<string, string>> m103Data = Utils.getKeyValueAttributes(ucm103.grpIEC103);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = IEC103GroupTreeNode.Nodes.Add("IEC103_" + Utils.GenerateShortUniqueKey(), "IEC103", "IEC103Master", "IEC103Master");
                    m103List.Add(new IEC103Master("IEC103", m103Data, tmp));
                }
                else if (mode == Mode.EDIT)
                {
                    m103List[editIndex].updateAttributes(m103Data);
                }
                refreshList();
                //Namrata: 09/08/2017
                if (sender != null && e != null)
                {
                    ucm103.grpIEC103.Visible = false;
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
                Console.WriteLine("*** ucm103 btnCancel_Click clicked in class!!!");
                Utils.IntIEC103Modbus = Utils.IntIEC103Modbus - 1;
                ucm103.grpIEC103.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucm103.grpIEC103);
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
                Console.WriteLine("*** ucm103 btnFirst_Click clicked in class!!!");
                if (ucm103.lvIEC103Master.Items.Count <= 0) return;
                if (m103List.ElementAt(0).IsNodeComment) return;
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
                Console.WriteLine("*** ucm103 btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (m103List.ElementAt(editIndex - 1).IsNodeComment) return;
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
                Console.WriteLine("*** ucm103 btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= ucm103.lvIEC103Master.Items.Count) return;
                if (m103List.ElementAt(editIndex + 1).IsNodeComment) return;
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
                Console.WriteLine("*** ucm103 btnLast_Click clicked in class!!!");
                if (ucm103.lvIEC103Master.Items.Count <= 0) return;
                if (m103List.ElementAt(m103List.Count - 1).IsNodeComment) return;
                editIndex = m103List.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvIEC103Master_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvIEC103Master_DoubleClick";
            try
            {
                Console.WriteLine("*** ucm103 lvIEC103Master_DoubleClick clicked in class!!!");
                if (ucm103.lvIEC103Master.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucm103.lvIEC103Master.SelectedItems[0];
                Utils.UncheckOthers(ucm103.lvIEC103Master, lvi.Index);
                if (m103List.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucm103.grpIEC103.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucm103.grpIEC103, true);
                loadValues();
                ucm103.cmbPortNo.Focus();
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
                ucm103.cmbDebug.SelectedIndex = ucm103.cmbDebug.FindStringExact("3");
                ucm103.txtGiTime.Text = "600";
                ucm103.txtClockSyncInterval.Text = "300";
                ucm103.txtRefreshInterval.Text = "120";
                ucm103.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION;
                ucm103.txtDescription.Text = "IEC103_" + (Globals.MasterNo + 1).ToString();
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
                IEC103Master m103 = m103List.ElementAt(editIndex);
                if (m103 != null)
                {
                    ucm103.txtMasterNo.Text = m103.MasterNum;
                    ucm103.cmbPortNo.SelectedIndex = ucm103.cmbPortNo.FindStringExact(m103.PortNum);
                    ucm103.cmbDebug.SelectedIndex = ucm103.cmbDebug.FindStringExact(m103.DEBUG);
                    ucm103.txtGiTime.Text = m103.GiTime;
                    ucm103.txtClockSyncInterval.Text = m103.ClockSyncInterval;
                    ucm103.txtRefreshInterval.Text = m103.RefreshInterval;
                    ucm103.txtFirmwareVersion.Text = m103.AppFirmwareVersion;
                    if (m103.Run.ToLower() == "yes") ucm103.chkRun.Checked = true;
                    else ucm103.chkRun.Checked = false;
                    ucm103.txtDescription.Text = m103.Description;
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
            if (Utils.IsEmptyFields(ucm103.grpIEC103))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            string strRoutineName = "fillOptions";
            try
            {
                //Fill Debug levels...
                ucm103.cmbDebug.Items.Clear();
                for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
                {
                    ucm103.cmbDebug.Items.Add(i.ToString());
                }
                ucm103.cmbDebug.SelectedIndex = 0;
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
                ucm103.lvIEC103Master.Columns.Add("Master No.", 80, HorizontalAlignment.Left);
                ucm103.lvIEC103Master.Columns.Add("Port No.", 70, HorizontalAlignment.Left);
                ucm103.lvIEC103Master.Columns.Add("GI Time (sec)", 90, HorizontalAlignment.Left);
                ucm103.lvIEC103Master.Columns.Add("Clock Sync Interval (sec)", 130, HorizontalAlignment.Left);
                ucm103.lvIEC103Master.Columns.Add("Refresh Interval (sec)", 130, HorizontalAlignment.Left);
                ucm103.lvIEC103Master.Columns.Add("Firmware Version", 120, HorizontalAlignment.Left);
                ucm103.lvIEC103Master.Columns.Add("Debug Level", 100, HorizontalAlignment.Left);
                ucm103.lvIEC103Master.Columns.Add("Description", 150, HorizontalAlignment.Left);
                ucm103.lvIEC103Master.Columns.Add("Run", 80, HorizontalAlignment.Left);
                ucm103.lvIEC103Master.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
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
                ucm103.lvIEC103Master.Items.Clear();
                foreach (IEC103Master mt in m103List)
                {
                    string[] row = new string[9];
                    if (mt.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = mt.MasterNum;
                        row[1] = mt.PortNum;
                        row[2] = mt.GiTime;
                        row[3] = mt.ClockSyncInterval;
                        row[4] = mt.RefreshInterval;
                        row[5] = mt.AppFirmwareVersion;
                        row[6] = mt.DEBUG;
                        row[7] = mt.Description;
                        row[8] = mt.Run;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucm103.lvIEC103Master.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IEC103Group_"))
            {
                //Fill serial ports...
                ucm103.cmbPortNo.Items.Clear();
                foreach (SerialInterface si in Utils.getOpenProPlusHandle().getSerialPortConfiguration().getSerialInterfaces())
                {
                    ucm103.cmbPortNo.Items.Add(si.PortNum);
                }
                if (ucm103.cmbPortNo.Items.Count > 0) ucm103.cmbPortNo.SelectedIndex = 0;
                return ucm103;
            }
            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("IEC103_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Console.WriteLine("$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (m103List.Count <= 0) return null;
                return m103List[idx].getView(kpArr);
            }
            else
            {
                Console.WriteLine("***** View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }
            return null;
        }
        public XmlNode exportXMLnode()
        {
            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            XmlNode rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            foreach (IEC103Master mn in m103List)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(mn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            foreach (IEC103Master iec103 in m103List)
            {
                iniData += iec103.exportINI(slaveNum, slaveID, element, ref ctr);
            }
            return iniData;
        }
        public void regenerateAISequence()
        {
            string strRoutineName = "regenerateAISequence";
            try
            {
                foreach (IEC103Master i103 in m103List)
                {
                    foreach (IED ied in i103.getIEDs())
                    {
                        ied.regenerateAISequence();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void regenerateAOSequence()
        {
            string strRoutineName = "regenerateAOSequence";
            try
            {
                foreach (IEC103Master i103 in m103List)
                {
                    foreach (IED ied in i103.getIEDs())
                    {
                        ied.regenerateAOSequence();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateDISequence()
        {
            string strRoutineName = "regenerateDISequence";
            try
            {
                foreach (IEC103Master i103 in m103List)
                {
                    foreach (IED ied in i103.getIEDs())
                    {
                        ied.regenerateDISequence();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateDOSequence()
        {
            string strRoutineName = "regenerateDOSequence";
            try
            {
                foreach (IEC103Master i103 in m103List)
                {
                    foreach (IED ied in i103.getIEDs())
                    {
                        ied.regenerateDOSequence();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateENSequence()
        {
            string strRoutineName = "regenerateENSequence";
            try
            {
                foreach (IEC103Master i103 in m103List)
            {
                foreach (IED ied in i103.getIEDs())
                {
                    ied.regenerateENSequence();
                }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void parseIECGNode(XmlNode iecgNode, TreeNode tn)
        {
            string strRoutineName = "parseIECGNode";
            try
            {
                //First set root node name...
                rnName = iecgNode.Name;
            tn.Nodes.Clear();
            IEC103GroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
            foreach (XmlNode node in iecgNode)
            {
                //Console.WriteLine("***** node type: {0}", node.NodeType);
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                TreeNode tmp = tn.Nodes.Add("IEC103_" + Utils.GenerateShortUniqueKey(), "IEC103", "IEC103Master", "IEC103Master");
                m103List.Add(new IEC103Master(node, tmp));
            }
            refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (IEC103Master iecNode in m103List)
            {
                if (iecNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<IEC103Master> getIEC103Masters()
        {
            return m103List;
        }
        public List<IEC103Master> getIEC103MastersByFilter(string masterID)
        {
            List<IEC103Master> mList = new List<IEC103Master>();
            if (masterID.ToLower() == "all") return m103List;
            else
                foreach (IEC103Master iec in m103List)
                {
                    if (iec.getMasterID == masterID)
                    {
                        mList.Add(iec);
                        break;
                    }
                }
            return mList;
        }
        //Namrata:22/6/2017
        public List<IED> getIEC103IEDsByFilter(string masterID)
        {
            List<IED> iLst = new List<IED>();
            if (masterID.ToLower() == "all")
            {
                foreach (IEC103Master iec in m103List)
                {
                    foreach (IED ied in iec.getIEDs())
                    {
                        iLst.Add(ied);
                    }
                }
            }
            else
            {
                foreach (IEC103Master iec in m103List)
                {
                    if (iec.getMasterID == masterID)
                    {
                        foreach (IED ied in iec.getIEDs())
                        {
                            iLst.Add(ied);
                        }
                        break;
                    }
                }
            }

            return iLst;
        }
    }
}
