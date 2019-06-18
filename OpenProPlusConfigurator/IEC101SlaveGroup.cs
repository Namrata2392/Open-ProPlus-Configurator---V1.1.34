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
    public class IEC101SlaveGroup
    {
        #region Decaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "IEC101SlaveGroup";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private List<IEC101Slave> s101List = new List<IEC101Slave>();//Expose the list to others for slave mapping thru func()...
        ucGroupIEC101Slave ucs101 = new ucGroupIEC101Slave();
        private TreeNode IEC101GroupTreeNode;
        #endregion Decaration
        public IEC101SlaveGroup()
        {
            string strRoutineName = "IEC101SlaveGroup";
            try
            {
                ucs101.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucs101.lvIEC101SlaveItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEC101Slave_ItemCheck);
                ucs101.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucs101.btnexportIEC101INIClick += new System.EventHandler(this.btnexportIEC101INI_Click);
                ucs101.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucs101.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucs101.lvIEC101SlaveDoubleClick += new System.EventHandler(this.lvIEC101Slave_DoubleClick);
                ucs101.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucs101.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucs101.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucs101.btnLastClick += new System.EventHandler(this.btnLast_Click);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private SlaveTypes slave = SlaveTypes.UNKNOWN;
        private void lvIEC101Slave_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lvIEC101Slave_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucs101.lvIEC101Slave.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
                if (s101List.Count >= Globals.MaxIEC101Slave)
                {
                    MessageBox.Show("Maximum " + Globals.MaxIEC101Slave + " IEC101 Slaves are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucs101.grpIEC101);
                Utils.showNavigation(ucs101.grpIEC101, false);
                loadDefaults();
                ucs101.txtSlaveNum.Text = (Globals.SlaveNo + 1).ToString();
                ucs101.grpIEC101.Visible = true;
                ucs101.txtSlaveNum.Focus();
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
                ucs101.grpIEC101.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucs101.grpIEC101);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnexportIEC101INI_Click(object sender, EventArgs e)
        {

        }
        private void loadDefaults()
        {
            string strRoutineName = "loadDefaults";
            try
            {
                ucs101.cmbPortNo.SelectedIndex = ucs101.cmbPortNo.FindStringExact("1");
                ucs101.txtASDUaddress.Text = "1";
                ucs101.cmbASDUsize.SelectedIndex = ucs101.cmbASDUsize.FindStringExact("2");
                ucs101.cmbIOASize.SelectedIndex = ucs101.cmbIOASize.FindStringExact("2");
                ucs101.cmbCOTsize.SelectedIndex = ucs101.cmbCOTsize.FindStringExact("1");
                ucs101.txtCyclicInterval.Text = "600";
                ucs101.txtEventQSize.Text = "1000";
                ucs101.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION;
                ucs101.cmbDebug.SelectedIndex = ucs101.cmbDebug.FindStringExact("3");
                ucs101.chkRun.Checked = true;
                //Namrata: 20/1/2017
                ucs101.CmbLinkAddSize.SelectedIndex = ucs101.CmbLinkAddSize.FindStringExact("2");
                ucs101.txtLinkAdd.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvIEC101Slave_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvIEC101Slave_DoubleClick";
            try
            {
                if (ucs101.lvIEC101Slave.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucs101.lvIEC101Slave.SelectedItems[0];
                Utils.UncheckOthers(ucs101.lvIEC101Slave, lvi.Index);
                if (s101List.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucs101.grpIEC101.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucs101.grpIEC101, true);
                loadValues();
                ucs101.txtSlaveNum.Focus();
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
                Utils.WriteLine(VerboseLevel.DEBUG, "*** mbsList count: {0} lv count: {1}", s101List.Count, ucs101.lvIEC101Slave.Items.Count);
                if (ucs101.lvIEC101Slave.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one slave ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucs101.lvIEC101Slave.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Slave No." + ucs101.lvIEC101Slave.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucs101.lvIEC101Slave.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            s101List.RemoveAt(iIndex);
                            ucs101.lvIEC101Slave.Items[iIndex].Remove();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one Slave ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    Utils.WriteLine(VerboseLevel.DEBUG, "*** mbsList count: {0} lv count: {1}", s101List.Count, ucs101.lvIEC101Slave.Items.Count);
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

                List<KeyValuePair<string, string>> s101Data = Utils.getKeyValueAttributes(ucs101.grpIEC101);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    //No need to create subnodes: tmp = IEC104GroupTreeNode.Nodes.Add("IEC104_" + Utils.GenerateShortUniqueKey(), "IEC104", "IEC104", "IEC104");
                    s101List.Add(new IEC101Slave("IEC101Slave", s101Data, tmp));
                }
                else if (mode == Mode.EDIT)
                {
                    s101List[editIndex].updateAttributes(s101Data);
                }
                refreshList();
                //Namrata: 09/08/2017
                if (sender != null && e != null)
                {
                    ucs101.grpIEC101.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
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
            if (Utils.IsEmptyFields(ucs101.grpIEC101))
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
                ucs101.cmbPortNo.Items.Clear();
                foreach (String br in IEC101Slave.getPortNo())
                {
                    ucs101.cmbPortNo.Items.Add(br.ToString());
                }
                ucs101.cmbPortNo.SelectedIndex = 0;

                //Fill ASDU size...
                ucs101.cmbASDUsize.Items.Clear();
                foreach (String br in IEC101Slave.getASDUsizes())
                {
                    ucs101.cmbASDUsize.Items.Add(br.ToString());
                }
                ucs101.cmbASDUsize.SelectedIndex = 0;

                //Fill IOA size...
                ucs101.cmbIOASize.Items.Clear();
                foreach (String ioa in IEC101Slave.getIOAsizes())
                {
                    ucs101.cmbIOASize.Items.Add(ioa.ToString());
                }
                ucs101.cmbIOASize.SelectedIndex = 0;

                //Fill COT size...
                ucs101.cmbCOTsize.Items.Clear();
                foreach (String db in IEC101Slave.getCOTsizes())
                {
                    ucs101.cmbCOTsize.Items.Add(db.ToString());
                }
                ucs101.cmbCOTsize.SelectedIndex = 0;

                //Fill Debug levels...
                ucs101.cmbDebug.Items.Clear();
                for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
                {
                    ucs101.cmbDebug.Items.Add(i.ToString());
                }
                ucs101.cmbDebug.SelectedIndex = 0;

                //Fill Link Address Sizes
                ucs101.CmbLinkAddSize.Items.Clear();
                foreach (String br in IEC101Slave.getLinkAddresssizes())
                {
                    ucs101.CmbLinkAddSize.Items.Add(br.ToString());
                }
                ucs101.CmbLinkAddSize.SelectedIndex = 0;
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
                IEC101Slave s101 = s101List.ElementAt(editIndex);
                if (s101 != null)
                {
                    ucs101.txtSlaveNum.Text = s101.SlaveNum;
                    ucs101.cmbPortNo.SelectedIndex = ucs101.cmbPortNo.FindStringExact(s101.PortNum);
                    ucs101.txtASDUaddress.Text = s101.ASDUAddress;
                    ucs101.cmbASDUsize.SelectedIndex = ucs101.cmbASDUsize.FindStringExact(s101.ASDUSize);
                    ucs101.cmbIOASize.SelectedIndex = ucs101.cmbIOASize.FindStringExact(s101.IOASize);
                    ucs101.cmbCOTsize.SelectedIndex = ucs101.cmbCOTsize.FindStringExact(s101.COTSize);
                    ucs101.txtCyclicInterval.Text = s101.CyclicInterval;
                    ucs101.txtEventQSize.Text = s101.EventQSize;
                    ucs101.txtFirmwareVersion.Text = s101.AppFirmwareVersion;
                    ucs101.cmbDebug.SelectedIndex = ucs101.cmbDebug.FindStringExact(s101.DEBUG);
                    ucs101.txtLinkAdd.Text = s101.LinkAddress;
                    ucs101.CmbLinkAddSize.SelectedIndex = ucs101.CmbLinkAddSize.FindStringExact(s101.LinkAddressSize);
                }
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
                ucs101.lvIEC101Slave.Columns.Add("Slave No.", 70, HorizontalAlignment.Left);
                ucs101.lvIEC101Slave.Columns.Add("Port No.", 90, HorizontalAlignment.Left);
                ucs101.lvIEC101Slave.Columns.Add("Link Address", 90, HorizontalAlignment.Left);
                ucs101.lvIEC101Slave.Columns.Add("ASDU Address", 100, HorizontalAlignment.Left);
                ucs101.lvIEC101Slave.Columns.Add("Link Address Size", 90, HorizontalAlignment.Left);
                ucs101.lvIEC101Slave.Columns.Add("ASDU Size", 90, HorizontalAlignment.Left);
                ucs101.lvIEC101Slave.Columns.Add("IOA Size", 90, HorizontalAlignment.Left);
                ucs101.lvIEC101Slave.Columns.Add("COT Size", 90, HorizontalAlignment.Left);
                ucs101.lvIEC101Slave.Columns.Add("Cyclic Interval", 110, HorizontalAlignment.Left);
                ucs101.lvIEC101Slave.Columns.Add("Event Queue Size", 80, HorizontalAlignment.Left);
                ucs101.lvIEC101Slave.Columns.Add("Firmware Version", 100, HorizontalAlignment.Left);
                ucs101.lvIEC101Slave.Columns.Add("Debug Level", 90, HorizontalAlignment.Left);
                ucs101.lvIEC101Slave.Columns.Add("Run", -2, HorizontalAlignment.Left);
                ucs101.lvIEC101Slave.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
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
                ucs101.lvIEC101Slave.Items.Clear();
                //addListHeaders();
                foreach (IEC101Slave sl in s101List)
                {
                    string[] row = new string[19];
                    if (sl.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = sl.SlaveNum;
                        row[1] = sl.PortNum;
                        row[2] = sl.LinkAddress;
                        row[3] = sl.ASDUAddress;
                        row[4] = sl.LinkAddressSize;
                        row[5] = sl.ASDUSize;
                        row[6] = sl.IOASize;
                        row[7] = sl.COTSize;
                        row[8] = sl.CyclicInterval;
                        row[9] = sl.EventQSize;
                        row[10] = sl.AppFirmwareVersion;
                        row[11] = sl.DEBUG;
                        row[12] = sl.Run;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucs101.lvIEC101Slave.Items.Add(lvItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IEC101SlaveGroup_"))
            {
                //Fill Port No.
                //fillPortNos();
                return ucs101;
            }

            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("IEC101Slave_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Utils.WriteLine(VerboseLevel.DEBUG, "$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (s101List.Count <= 0) return null;
                return s101List[idx].getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "***** View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }

            return null;
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnFirst_Click";
            try
            {
                Console.WriteLine("*** ucs101 btnFirst_Click clicked in class!!!");
                if (ucs101.lvIEC101Slave.Items.Count <= 0) return;
                if (s101List.ElementAt(0).IsNodeComment) return;
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
                Console.WriteLine("*** ucs104 btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (s101List.ElementAt(editIndex - 1).IsNodeComment) return;
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
                Console.WriteLine("*** ucs104 btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= ucs101.lvIEC101Slave.Items.Count) return;
                if (s101List.ElementAt(editIndex + 1).IsNodeComment) return;
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
                Console.WriteLine("*** ucs104 btnLast_Click clicked in class!!!");
                if (ucs101.lvIEC101Slave.Items.Count <= 0) return;
                if (s101List.ElementAt(s101List.Count - 1).IsNodeComment) return;
                editIndex = s101List.Count - 1;
                loadValues();
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
                rnName = iecgNode.Name;
                tn.Nodes.Clear();
                IEC101GroupTreeNode = tn;
                foreach (XmlNode node in iecgNode)
                {
                    TreeNode tmp = null;
                    if (node.NodeType == XmlNodeType.Comment) continue;
                    s101List.Add(new IEC101Slave(node, tmp));
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public XmlNode exportXMLnode()
        {
            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            XmlNode rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            foreach (IEC101Slave sn in s101List)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(sn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (IEC101Slave s101Node in s101List)
            {
                if (s101Node.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<IEC101Slave> getIEC101Slaves()
        {
            return s101List;
        }
        public List<IEC101Slave> getIEC101SlavesByFilter(string slaveID)
        {
            List<IEC101Slave> iec101List = new List<IEC101Slave>();
            if (slaveID.ToLower() == "all") return s101List;
            else
                foreach (IEC101Slave iec in s101List)
                {
                    if (iec.getSlaveID == slaveID)
                    {
                        iec101List.Add(iec);
                        break;
                    }
                }

            return iec101List;
        }
    }
}
