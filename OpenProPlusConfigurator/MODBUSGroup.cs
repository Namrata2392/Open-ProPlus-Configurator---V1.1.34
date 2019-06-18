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
    * \brief     <b>MODBUSGroup</b> is a class to store all the MODBUSMaster's
    * \details   This class stores info related to all MODBUSMaster's. It allows
    * user to add multiple MODBUSMaster's. It also exports the XML node related to this object.
    * 
    */
    public class MODBUSGroup
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "MODBUSGroup";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        List<MODBUSMaster> mbList = new List<MODBUSMaster>();
        ucGroupMODBUS ucmb = new ucGroupMODBUS();
        private TreeNode MODBUSGroupTreeNode;
        public MODBUSGroup(TreeNode tn)
        {
            tn.Nodes.Clear();
            MODBUSGroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
            ucmb.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucmb.lvMODBUSmasterItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvMODBUSmaster_ItemCheck);
            ucmb.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucmb.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucmb.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucmb.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            ucmb.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            ucmb.btnNextClick += new System.EventHandler(this.btnNext_Click);
            ucmb.btnLastClick += new System.EventHandler(this.btnLast_Click);
            ucmb.lvMODBUSmasterDoubleClick += new System.EventHandler(this.lvMODBUSmaster_DoubleClick);
            ucmb.cmbProtocolTypeSelectedIndexChanged += new System.EventHandler(this.cmbProtocolType_SelectedIndexChanged);
            addListHeaders();
            fillOptions();
        }
        private void lvMODBUSmaster_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucmb.lvMODBUSmaster.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
            if (mbList.Count >= Globals.MaxMODBUSMaster)
            {
                MessageBox.Show("Maximum " + Globals.MaxMODBUSMaster + " MODBUS Masters are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Console.WriteLine("*** ucmb btnAdd_Click clicked in class!!!");
            mode = Mode.ADD;
            editIndex = -1;
            Utils.resetValues(ucmb.grpMODBUS);
            Utils.showNavigation(ucmb.grpMODBUS, false);
            loadDefaults();
            ucmb.txtMasterNo.Text = (Globals.MasterNo + 1).ToString();
            ucmb.grpMODBUS.Visible = true;
            ucmb.cmbProtocolType.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Utils.OPPCCModbusList.Clear();
                if (ucmb.lvMODBUSmaster.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one Master ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucmb.lvMODBUSmaster.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Master " + ucmb.lvMODBUSmaster.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucmb.lvMODBUSmaster.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            MODBUSGroupTreeNode.Nodes.Remove(mbList.ElementAt(iIndex).getTreeNode());
                            mbList.RemoveAt(iIndex);
                            ucmb.lvMODBUSmaster.Items[iIndex].Remove();
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
                MessageBox.Show(ex.Message.ToString());
            }
            //Utils.OPPCCModbusList.Clear();
            //Console.WriteLine("*** ucmb btnDelete_Click clicked in class!!!");
            //Console.WriteLine("*** mbList count: {0} lv count: {1}", mbList.Count, ucmb.lvMODBUSmaster.Items.Count);
            //DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            //if (result == DialogResult.No)
            //{
            //    return;
            //}
            //for (int i = ucmb.lvMODBUSmaster.Items.Count - 1; i >= 0; i--)
            //{
            //    if (ucmb.lvMODBUSmaster.Items[i].Checked)
            //    {
            //        Console.WriteLine("*** removing indices: {0}", i);
            //        MODBUSGroupTreeNode.Nodes.Remove(mbList.ElementAt(i).getTreeNode());
            //        mbList.RemoveAt(i);
            //        ucmb.lvMODBUSmaster.Items[i].Remove();
            //        //Namrata: 27/7/2017
            //        Utils.OPPCCModbusList.AddRange(mbList);
            //    }
            //}
            //Console.WriteLine("*** mbList count: {0} lv count: {1}", mbList.Count, ucmb.lvMODBUSmaster.Items.Count);
            //refreshList();
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            Utils.OPPCCModbusList.Clear();
            if (!Validate()) return;
            Console.WriteLine("*** ucmb btnDone_Click clicked in class!!!");
            List<KeyValuePair<string, string>> mbData = Utils.getKeyValueAttributes(ucmb.grpMODBUS);
            if (mode == Mode.ADD)
            {
                TreeNode tmp = MODBUSGroupTreeNode.Nodes.Add("MODBUS_" + Utils.GenerateShortUniqueKey(), "MODBUS", "ModbusMaster", "ModbusMaster");
                mbList.Add(new MODBUSMaster("MODBUS", mbData, tmp));
                //Namrata:4/7/2017
                //Utils.OPPCCModbusList.AddRange(mbList);
            }
            else if (mode == Mode.EDIT)
            {
                mbList[editIndex].updateAttributes(mbData);
            }
            refreshList();
            ucmb.grpMODBUS.Visible = false;
            //Globals.MODBUSMasterNo += IEC103Group;
            mode = Mode.NONE;
            editIndex = -1;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucmb btnCancel_Click clicked in class!!!");
            //Utils.IntIEC103Modbus = Utils.IntIEC103Modbus - 1;
            ucmb.grpMODBUS.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
            Utils.resetValues(ucmb.grpMODBUS);
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucmb btnFirst_Click clicked in class!!!");
            if (ucmb.lvMODBUSmaster.Items.Count <= 0) return;
            if (mbList.ElementAt(0).IsNodeComment) return;
            editIndex = 0;
            loadValues();
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucmb btnPrev_Click clicked in class!!!");
            if (editIndex - 1 < 0) return;
            if (mbList.ElementAt(editIndex - 1).IsNodeComment) return;
            editIndex--;
            loadValues();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucmb btnNext_Click clicked in class!!!");
            if (editIndex + 1 >= ucmb.lvMODBUSmaster.Items.Count) return;
            if (mbList.ElementAt(editIndex + 1).IsNodeComment) return;
            editIndex++;
            loadValues();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucmb btnLast_Click clicked in class!!!");
            if (ucmb.lvMODBUSmaster.Items.Count <= 0) return;
            if (mbList.ElementAt(mbList.Count - 1).IsNodeComment) return;
            editIndex = mbList.Count - 1;
            loadValues();
        }
        private void lvMODBUSmaster_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucmb lvMODBUSmaster_DoubleClick clicked in class!!!");
            if (ucmb.lvMODBUSmaster.SelectedItems.Count <= 0) return;

            ListViewItem lvi = ucmb.lvMODBUSmaster.SelectedItems[0];
            Utils.UncheckOthers(ucmb.lvMODBUSmaster, lvi.Index);
            if (mbList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ucmb.grpMODBUS.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            Utils.showNavigation(ucmb.grpMODBUS, true);
            loadValues();
            ucmb.cmbProtocolType.Focus();
        }
        private void cmbProtocolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillPortNos();
            if (ucmb.cmbProtocolType.GetItemText(ucmb.cmbProtocolType.SelectedItem).ToLower() == "tcp")
            {
                ucmb.lblPN.Text = "Network No.";
            }
            else
            {
                ucmb.lblPN.Text = "UART No.";
            }
        }
        private void loadDefaults()
        {
            ucmb.cmbProtocolType.FindStringExact("RTU");
            ucmb.txtClockSyncInterval.Text = "300";
            ucmb.txtPollingInterval.Text = "10";
            ucmb.txtRefreshInterval.Text = "120";
            ucmb.cmbDebug.SelectedIndex = ucmb.cmbDebug.FindStringExact("3");
            ucmb.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION;
            ucmb.txtDescription.Text = "MODBUS_"+(Globals.MasterNo + 1).ToString();
        }
        private void loadValues()
        {
            MODBUSMaster mbm = mbList.ElementAt(editIndex);
            if (mbm != null)
            {
                ucmb.txtMasterNo.Text = mbm.MasterNum;
                ucmb.cmbProtocolType.SelectedIndex = ucmb.cmbProtocolType.FindStringExact(mbm.ProtocolType);
                ucmb.cmbPortNo.SelectedIndex = ucmb.cmbPortNo.FindStringExact(mbm.PortNum);
              
                ucmb.txtClockSyncInterval.Text = mbm.PollingIntervalmSec;
                ucmb.txtPollingInterval.Text = mbm.PollingIntervalmSec;
                ucmb.txtRefreshInterval.Text = mbm.RefreshInterval;
                ucmb.cmbDebug.SelectedIndex = ucmb.cmbDebug.FindStringExact(mbm.DEBUG);
                ucmb.txtFirmwareVersion.Text = mbm.AppFirmwareVersion;
                if (mbm.Run.ToLower() == "yes") ucmb.chkRun.Checked = true;
                else ucmb.chkRun.Checked = false;
                ucmb.txtDescription.Text = mbm.Description;

            }
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucmb.grpMODBUS))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            //Fill Protocol Type's...
            ucmb.cmbProtocolType.Items.Clear();
            foreach (String pt in MODBUSMaster.getProtocolTypes())
            {
                ucmb.cmbProtocolType.Items.Add(pt.ToString());
            }
            ucmb.cmbProtocolType.SelectedIndex = 0;
            //Fill Debug levels...
            ucmb.cmbDebug.Items.Clear();
            for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
            {
                ucmb.cmbDebug.Items.Add(i.ToString());
            }
            ucmb.cmbDebug.SelectedIndex = 0;
        }
        private void fillPortNos()
        {
            ucmb.cmbPortNo.Items.Clear();
            if (ucmb.cmbProtocolType.GetItemText(ucmb.cmbProtocolType.SelectedItem).ToLower() == "tcp")
            {
                foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                {
                    ucmb.cmbPortNo.Items.Add(ni.PortNum);
                }
            }
            else//assume serial interfaces...
            {
                foreach (SerialInterface si in Utils.getOpenProPlusHandle().getSerialPortConfiguration().getSerialInterfaces())
                {
                    ucmb.cmbPortNo.Items.Add(si.PortNum);
                }
            }
            if (ucmb.cmbPortNo.Items.Count > 0) ucmb.cmbPortNo.SelectedIndex = 0;
        }
        private void addListHeaders()
        {
            ucmb.lvMODBUSmaster.Columns.Add("Master No.", 80, HorizontalAlignment.Left);
            ucmb.lvMODBUSmaster.Columns.Add("Protocol Type", 110, HorizontalAlignment.Left);
            ucmb.lvMODBUSmaster.Columns.Add("Port No.", 70, HorizontalAlignment.Left);
            ucmb.lvMODBUSmaster.Columns.Add("Clock Sync Interval (sec)", 130, HorizontalAlignment.Left);
            ucmb.lvMODBUSmaster.Columns.Add("Polling Interval(msec)", 120, HorizontalAlignment.Left);
            ucmb.lvMODBUSmaster.Columns.Add("Refresh Interval", 110, HorizontalAlignment.Left);
            ucmb.lvMODBUSmaster.Columns.Add("Firmware Version", 130, HorizontalAlignment.Left);
            ucmb.lvMODBUSmaster.Columns.Add("Debug Level", 90, HorizontalAlignment.Left);
            ucmb.lvMODBUSmaster.Columns.Add("Description", 170, HorizontalAlignment.Left);
            ucmb.lvMODBUSmaster.Columns.Add("Run", 70, HorizontalAlignment.Left);
            ucmb.lvMODBUSmaster.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
        }
        private void refreshList()
        {
            Utils.OPPCCModbusList.Clear();
            int cnt = 0;
            ucmb.lvMODBUSmaster.Items.Clear();
            foreach (MODBUSMaster mt in mbList)
            {
                string[] row = new string[10];
                if (mt.IsNodeComment)
                {
                    row[0] = "Comment...";
                }
                else
                {
                    row[0] = mt.MasterNum;
                    row[1] = mt.ProtocolType;
                    row[2] = mt.PortNum;
                    row[3] = mt.PortTimesyncSec;
                    row[4] = mt.PollingIntervalmSec;
                    row[5] = mt.RefreshInterval;
                    row[6] = mt.AppFirmwareVersion;
                    row[7] = mt.DEBUG;
                    row[8] = mt.Description;
                    row[9] = mt.Run;
                }
                ListViewItem lvItem = new ListViewItem(row);
                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucmb.lvMODBUSmaster.Items.Add(lvItem);
            }
            //Namrata:4/7/2017
            Utils.OPPCCModbusList.AddRange(mbList);
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("MODBUSGroup_"))
            {
                //Fill Port No.
                fillPortNos();
                return ucmb;
            }
            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("MODBUS_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Console.WriteLine("$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (mbList.Count <= 0) return null;
                return mbList[idx].getView(kpArr);
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
            foreach (MODBUSMaster mn in mbList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(mn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }

        public string exportXML()
        {
            XmlNode xmlNode = exportXMLnode();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.Indentation = 2; //default is 1.
            xmlNode.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();
            return stringWriter.ToString();
        }

        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            foreach (MODBUSMaster mbm in mbList)
            {
                iniData += mbm.exportINI(slaveNum, slaveID, element, ref ctr);
            }
            return iniData;
        }

        public void regenerateSequence()
        {
            int oMasterNo = -1;
            int nMasterNo = -1;
            //Reset MODBUS master no.
            Globals.resetUniqueNos(ResetUniqueNos.MODBUSMASTER);
            Globals.MasterNo++;//Start from 1...
            foreach (MODBUSMaster mt in mbList)
            {
                oMasterNo = Int32.Parse(mt.MasterNum);
                nMasterNo = Globals.MasterNo++;
                mt.MasterNum = nMasterNo.ToString();
            }
            Utils.getOpenProPlusHandle().getMasterConfiguration().getMODBUSGroup().refreshList();
        }

        public void regenerateAISequence()
        {
            foreach (MODBUSMaster mbm in mbList)
            {
                foreach (IED ied in mbm.getIEDs())
                {
                    ied.regenerateAISequence();
                }
            }
        }
        public void regenerateAOSequence()
        {
            foreach (MODBUSMaster mbm in mbList)
            {
                foreach (IED ied in mbm.getIEDs())
                {
                    ied.regenerateAOSequence();
                }
            }
        }
        public void regenerateDISequence()
        {
            foreach (MODBUSMaster mbm in mbList)
            {
                foreach (IED ied in mbm.getIEDs())
                {
                    ied.regenerateDISequence();
                }
            }
        }

        public void regenerateDOSequence()
        {
            foreach (MODBUSMaster mbm in mbList)
            {
                foreach (IED ied in mbm.getIEDs())
                {
                    ied.regenerateDOSequence();
                }
            }
        }

        public void regenerateENSequence()
        {
            foreach (MODBUSMaster mbm in mbList)
            {
                foreach (IED ied in mbm.getIEDs())
                {
                    ied.regenerateENSequence();
                }
            }
        }

        public void parseMBGNode(XmlNode mbgNode, TreeNode tn)
        {
            //First set root node name...
            rnName = mbgNode.Name;
            tn.Nodes.Clear();
            MODBUSGroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
            foreach (XmlNode node in mbgNode)
            {
                //Console.WriteLine("***** node type: {0}", node.NodeType);
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                TreeNode tmp = tn.Nodes.Add("MODBUS_" + Utils.GenerateShortUniqueKey(), "MODBUS", "ModbusMaster", "ModbusMaster");
                mbList.Add(new MODBUSMaster(node, tmp));
            }
            refreshList();
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (MODBUSMaster mbNode in mbList)
            {
                if (mbNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<MODBUSMaster> getMODBUSMasters()
        {
            return mbList;
        }
        public List<MODBUSMaster> getMODBUSMastersByFilter(string masterID)
        {
            List<MODBUSMaster> mList = new List<MODBUSMaster>();
            if (masterID.ToLower() == "all") return mbList;
            else
                foreach (MODBUSMaster m in mbList)
                {
                    if (m.getMasterID == masterID)
                    {
                        mList.Add(m);
                        break;
                    }
                }

            return mList;
        }
        public List<IED> getMODBUSIEDsByFilter(string masterID)
        {
            List<IED> iLst = new List<IED>();

            if (masterID.ToLower() == "all")
            {
                foreach (MODBUSMaster m in mbList)
                {
                    foreach (IED ied in m.getIEDs())
                    {
                        iLst.Add(ied);
                    }
                }
            }
            else
            {
                foreach (MODBUSMaster m in mbList)
                {
                    if (m.getMasterID == masterID)
                    {
                        foreach (IED ied in m.getIEDs())
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
