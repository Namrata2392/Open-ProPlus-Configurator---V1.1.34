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
    public class IEC101Group
    {
        OpenProPlus_Config opcHandle;
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "IEC101Group";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        public List<IEC101Master> m101List = new List<IEC101Master>();
        ucIEC101Group ucm101 = new ucIEC101Group();
        ucAIlist ucai = new ucAIlist();
        private TreeNode IEC101GroupTreeNode;

        public IEC101Group(TreeNode tn)
        {
            tn.Nodes.Clear();
            IEC101GroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
            ucm101.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucm101.lvIEC101MasterItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEC101Master_ItemCheck);
            ucm101.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucm101.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucm101.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucm101.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            ucm101.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            ucm101.btnNextClick += new System.EventHandler(this.btnNext_Click);
            ucm101.btnLastClick += new System.EventHandler(this.btnLast_Click);
            ucm101.lvIEC101MasterDoubleClick += new System.EventHandler(this.lvIEC101Master_DoubleClick);
            addListHeaders();
            fillOptions();
        }
        private void lvIEC101Master_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucm101.lvIEC101Master.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (m101List.Count >= Globals.MaxIEC101Master)
            {
                MessageBox.Show("Maximum " + Globals.MaxIEC101Master + " IEC101 Masters are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Console.WriteLine("*** ucm103 btnAdd_Click clicked in class!!!");
            mode = Mode.ADD;
            editIndex = -1;
            Utils.resetValues(ucm101.grpIEC101);
            Utils.showNavigation(ucm101.grpIEC101, false);
            //Namrata: 3/7/2017
            loadDefaults();
            ucm101.txtMasterNo.Text = (Globals.MasterNo + 1).ToString();
            ucm101.grpIEC101.Visible = true;
            ucm101.cmbPortNo.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ucm101.lvIEC101Master.Items.Count == 0)
            {
                MessageBox.Show("Please add atleast one master ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (ucm101.lvIEC101Master.CheckedItems.Count == 1)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete Master " + ucm101.lvIEC101Master.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    int iIndex = ucm101.lvIEC101Master.CheckedItems[0].Index;
                    Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                    if (result == DialogResult.Yes)
                    {
                        IEC101GroupTreeNode.Nodes.Remove(m101List.ElementAt(iIndex).getTreeNode());
                        m101List.RemoveAt(iIndex);
                        ucm101.lvIEC101Master.Items[iIndex].Remove();
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
            //Console.WriteLine("*** ucm103 btnDelete_Click clicked in class!!!");
            //Console.WriteLine("*** m103List count: {0} lv count: {1}", m101List.Count, ucm101.lvIEC101Master.Items.Count);
            //DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            //if (result == DialogResult.No)
            //{
            //    return;
            //}
            //for (int i = ucm101.lvIEC101Master.Items.Count - 1; i >= 0; i--)
            //{
            //    if (ucm101.lvIEC101Master.Items[i].Checked)
            //    {
            //        Console.WriteLine("*** removing indices: {0}", i);
            //        IEC101GroupTreeNode.Nodes.Remove(m101List.ElementAt(i).getTreeNode());
            //        m101List.RemoveAt(i);
            //        ucm101.lvIEC101Master.Items[i].Remove();
            //    }
            //}
            //Console.WriteLine("*** m103List count: {0} lv count: {1}", m101List.Count, ucm101.lvIEC101Master.Items.Count);
            //refreshList();
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;
            Console.WriteLine("*** ucm103 btnDone_Click clicked in class!!!");
            List<KeyValuePair<string, string>> m101Data = Utils.getKeyValueAttributes(ucm101.grpIEC101);
            if (mode == Mode.ADD)
            {
                TreeNode tmp = IEC101GroupTreeNode.Nodes.Add("IEC101_" + Utils.GenerateShortUniqueKey(), "IEC101", "IEC101Master", "IEC101Master");
                m101List.Add(new IEC101Master("IEC101", m101Data, tmp));
            }
            else if (mode == Mode.EDIT)
            {
                m101List[editIndex].updateAttributes(m101Data);
            }
            refreshList();
            //Namrata: 09/08/2017
            if (sender != null && e != null)
            {
                ucm101.grpIEC101.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucm103 btnCancel_Click clicked in class!!!");
            ucm101.grpIEC101.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
            Utils.resetValues(ucm101.grpIEC101);
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucm103 btnFirst_Click clicked in class!!!");
            if (ucm101.lvIEC101Master.Items.Count <= 0) return;
            if (m101List.ElementAt(0).IsNodeComment) return;
            editIndex = 0;
            loadValues();
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            //Namrata:27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucm103 btnPrev_Click clicked in class!!!");
            if (editIndex - 1 < 0) return;
            if (m101List.ElementAt(editIndex - 1).IsNodeComment) return;
            editIndex--;
            loadValues();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            //Namrata:27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucm103 btnNext_Click clicked in class!!!");
            if (editIndex + 1 >= ucm101.lvIEC101Master.Items.Count) return;
            if (m101List.ElementAt(editIndex + 1).IsNodeComment) return;
            editIndex++;
            loadValues();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucm103 btnLast_Click clicked in class!!!");
            if (ucm101.lvIEC101Master.Items.Count <= 0) return;
            if (m101List.ElementAt(m101List.Count - 1).IsNodeComment) return;
            editIndex = m101List.Count - 1;
            loadValues();
        }
        private void lvIEC101Master_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucm103 lvIEC103Master_DoubleClick clicked in class!!!");
            if (ucm101.lvIEC101Master.SelectedItems.Count <= 0) return;
            ListViewItem lvi = ucm101.lvIEC101Master.SelectedItems[0];
            Utils.UncheckOthers(ucm101.lvIEC101Master, lvi.Index);
            if (m101List.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ucm101.grpIEC101.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            Utils.showNavigation(ucm101.grpIEC101, true);
            loadValues();
            ucm101.cmbPortNo.Focus();
        }
        private void loadDefaults()
        {
            ucm101.cmbDebug.SelectedIndex = ucm101.cmbDebug.FindStringExact("3");
            ucm101.txtGiTime.Text = "600";
            ucm101.txtClockSyncInterval.Text = "300";
            ucm101.txtRefreshInterval.Text = "120";
            ucm101.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION;
            ucm101.txtDescription.Text = "IEC101_" + (Globals.MasterNo + 1).ToString();
        }
        private void loadValues()
        {
            IEC101Master m101 = m101List.ElementAt(editIndex);
            if (m101 != null)
            {
                ucm101.txtMasterNo.Text = m101.MasterNum;
                ucm101.cmbPortNo.SelectedIndex = ucm101.cmbPortNo.FindStringExact(m101.PortNum);
                ucm101.cmbDebug.SelectedIndex = ucm101.cmbDebug.FindStringExact(m101.DEBUG);
                ucm101.txtGiTime.Text = m101.GiTime;
                ucm101.txtClockSyncInterval.Text = m101.ClockSyncInterval;
                ucm101.txtRefreshInterval.Text = m101.RefreshInterval;
                ucm101.txtFirmwareVersion.Text = m101.AppFirmwareVersion;
                if (m101.Run.ToLower() == "yes") ucm101.chkRun.Checked = true;
                else ucm101.chkRun.Checked = false;
                ucm101.txtDescription.Text = m101.Description;
            }
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucm101.grpIEC101))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            //Fill Debug levels...
            ucm101.cmbDebug.Items.Clear();
            for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
            {
                ucm101.cmbDebug.Items.Add(i.ToString());
            }
            ucm101.cmbDebug.SelectedIndex = 0;
        }
        private void addListHeaders()
        {
            ucm101.lvIEC101Master.Columns.Add("Master No.", 80, HorizontalAlignment.Left);
            ucm101.lvIEC101Master.Columns.Add("Port No.", 80, HorizontalAlignment.Left);
            ucm101.lvIEC101Master.Columns.Add("GI Time (sec)", 80, HorizontalAlignment.Left);
            ucm101.lvIEC101Master.Columns.Add("Clock Sync Interval (sec)", 150, HorizontalAlignment.Left);
            ucm101.lvIEC101Master.Columns.Add("Refresh Interval (sec)", 130, HorizontalAlignment.Left);
            ucm101.lvIEC101Master.Columns.Add("Firmware Version", 140, HorizontalAlignment.Left);
            ucm101.lvIEC101Master.Columns.Add("Debug Level", 110, HorizontalAlignment.Left);
            ucm101.lvIEC101Master.Columns.Add("Description", 150, HorizontalAlignment.Left);
            ucm101.lvIEC101Master.Columns.Add("Run", 80, HorizontalAlignment.Left);
            ucm101.lvIEC101Master.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private void refreshList()
        {
            int cnt = 0;
            ucm101.lvIEC101Master.Items.Clear();
            //addListHeaders();

            foreach (IEC101Master mt in m101List)
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
                ucm101.lvIEC101Master.Items.Add(lvItem);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IEC101Group_"))
            {
                //Fill serial ports...
                ucm101.cmbPortNo.Items.Clear();
                foreach (SerialInterface si in Utils.getOpenProPlusHandle().getSerialPortConfiguration().getSerialInterfaces())
                {
                    ucm101.cmbPortNo.Items.Add(si.PortNum);
                }
                if (ucm101.cmbPortNo.Items.Count > 0) ucm101.cmbPortNo.SelectedIndex = 0;
                return ucm101;
            }
            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("IEC101_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Console.WriteLine("$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (m101List.Count <= 0) return null;
                return m101List[idx].getView(kpArr);
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
            foreach (IEC101Master mn in m101List)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(mn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            foreach (IEC101Master iec101 in m101List)
            {
                iniData += iec101.exportINI(slaveNum, slaveID, element, ref ctr);
            }
            return iniData;
        }
        public void regenerateAISequence()
        {
            foreach (IEC101Master i101 in m101List)
            {
                foreach (IED ied in i101.getIEDs())
                {
                    ied.regenerateAISequence();
                }
            }
        }
        public void regenerateAOSequence()
        {
            foreach (IEC101Master i101 in m101List)
            {
                foreach (IED ied in i101.getIEDs())
                {
                    ied.regenerateAOSequence();
                }
            }
        }
        public void regenerateDISequence()
        {
            foreach (IEC101Master i101 in m101List)
            {
                foreach (IED ied in i101.getIEDs())
                {
                    ied.regenerateDISequence();
                }
            }
        }
        public void regenerateDOSequence()
        {
            foreach (IEC101Master i101 in m101List)
            {
                foreach (IED ied in i101.getIEDs())
                {
                    ied.regenerateDOSequence();
                }
            }
        }
        public void regenerateENSequence()
        {
            foreach (IEC101Master i101 in m101List)
            {
                foreach (IED ied in i101.getIEDs())
                {
                    ied.regenerateENSequence();
                }
            }
        }
        public void parseIECGNode(XmlNode iecgNode, TreeNode tn)
        {
            rnName = iecgNode.Name;
            tn.Nodes.Clear();
            IEC101GroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
            foreach (XmlNode node in iecgNode)
            {
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                TreeNode tmp = tn.Nodes.Add("IEC101_" + Utils.GenerateShortUniqueKey(), "IEC101", "IEC101Master", "IEC101Master");
                m101List.Add(new IEC101Master(node, tmp));
            }
            refreshList();
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (IEC101Master iecNode in m101List)
            {
                if (iecNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<IEC101Master> getIEC101Masters()
        {
            return m101List;
        }
        public List<IEC101Master> getIEC101MastersByFilter(string masterID)
        {
            List<IEC101Master> mList = new List<IEC101Master>();
            if (masterID.ToLower() == "all") return m101List;
            else
                foreach (IEC101Master iec in m101List)
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
        public List<IED> getIEC101IEDsByFilter(string masterID)
        {
            List<IED> iLst = new List<IED>();
            if (masterID.ToLower() == "all")
            {
                foreach (IEC101Master iec in m101List)
                {
                    foreach (IED ied in iec.getIEDs())
                    {
                        iLst.Add(ied);
                    }
                }
            }
            else
            {
                foreach (IEC101Master iec in m101List)
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
        //Namrata:19/6/2017
        public List<IED> getIEC101IEDsByFilter(string masterID, string iedID)
        {
            List<IED> iLst = new List<IED>();

            foreach (IEC101Master iec in m101List)
            {
                if (iec.getMasterID == masterID)
                {
                    if (iedID.ToLower() == "all")
                    {
                        return iec.getIEDs();
                    }
                    else
                    {
                        foreach (IED ied in iec.getIEDs())
                        {
                            if (ied.getIEDID == iedID)
                            {
                                iLst.Add(ied);
                                break;
                            }
                        }
                    }
                    break;
                }
            }
            return iLst;
        }
    }
}
