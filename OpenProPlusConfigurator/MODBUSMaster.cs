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
    * \brief     <b>MODBUSMaster</b> is a class to store all the IED's & MODBUS master parameters.
    * \details   This class stores info for all IED's & MODBUS master parameters. It allows
    * user to add multiple IED's. It also exports the XML node related to this object.
    * 
    */
    public class MODBUSMaster
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
            MODBUS
        };

        enum protocolType
        {
            RTU,
            ASCII,
            TCP
        };
        private int clockSyncInterval = -1;
        private bool isNodeComment = false;
        private string comment = "";
        private masterType mType = masterType.MODBUS;
        private protocolType pt = protocolType.RTU;
        private int masterNum = -1;
        private int portNum = -1;
        private bool run = true;
        private int debug = -1;
        private int pollingIntervalmSec = -1;
        private int portTimesyncSec = -1;
        private int refreshInterval = 120;
        private string appFirmwareVersion;
        private string desc = "";
        //Namrata:15/6/2017
        private int SelectedIndex;
        private Mode mode = Mode.NONE;
        private int editIndex = -1;

        List<IED> iedList = new List<IED>();
        ucMasterMODBUS ucmod = new ucMasterMODBUS();
        private TreeNode MODBUSTreeNode;
        private OpenFileDialog ofdXMLFile = new OpenFileDialog();

        private string[] arrAttributes = { "ProtocolType", "MasterNum", "PortNum", "Run", "DEBUG", "PollingIntervalmSec", "PortTimesyncSec", "RefreshInterval", "AppFirmwareVersion", "Description" };
        #endregion Declarations
        public static List<string> getProtocolTypes()
        {
            List<string> lpt = new List<string>();
            foreach (protocolType ptv in Enum.GetValues(typeof(protocolType)))
            {
                lpt.Add(ptv.ToString());
            }
            return lpt;
        }

        public MODBUSMaster(string mbName, List<KeyValuePair<string, string>> mbData, TreeNode tn)
        {
            ucmod.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucmod.lvIEDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
            ucmod.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucmod.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
            ucmod.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
            ucmod.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucmod.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucmod.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            ucmod.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            ucmod.btnNextClick += new System.EventHandler(this.btnNext_Click);
            ucmod.btnLastClick += new System.EventHandler(this.btnLast_Click);
            ucmod.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
            addListHeaders();
            this.fillOptions();

            //First set the root element value...
            try
            {
                mType = (masterType)Enum.Parse(typeof(masterType), mbName);
            }
            catch (System.ArgumentException)
            {
                Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", mbName);
            }

            //Parse n store values...
            if (mbData != null && mbData.Count > 0)
            {
                foreach (KeyValuePair<string, string> mbkp in mbData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", mbkp.Key, mbkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(mbkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(mbkp.Key).SetValue(this, mbkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", mbkp.Key, mbkp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }

            if (tn != null) tn.Nodes.Clear();
            MODBUSTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...

            //Now since we have parsed, we can name the treenode...
            //if (tn != null) tn.Text = "MODBUS " + this.Description;
            if (tn != null) tn.Text = "MODBUS " + this.Description;
            /* While manually adding, no IED initially...
            foreach (XmlNode node in mNode)
            {
                //Utils.WriteLine("***** node type: {0}", node.NodeType);
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                TreeNode tmp = tn.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                iedList.Add(new IED(node, tmp));
            }
             */
            refreshList();
            ucmod.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
        }

        public MODBUSMaster(XmlNode mNode, TreeNode tn)
        {
            ucmod.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucmod.lvIEDListItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
            ucmod.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucmod.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
            ucmod.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
            ucmod.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucmod.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucmod.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            ucmod.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            ucmod.btnNextClick += new System.EventHandler(this.btnNext_Click);
            ucmod.btnLastClick += new System.EventHandler(this.btnLast_Click);
            ucmod.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
            addListHeaders();
            this.fillOptions();

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
            MODBUSTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...

            //Now since we have parsed, we can name the treenode...
            if (tn != null) tn.Text = "MODBUS " + this.Description;

            foreach (XmlNode node in mNode)
            {
                //Utils.WriteLine("***** node type: {0}", node.NodeType);
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                TreeNode tmp = tn.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                iedList.Add(new IED(node, tmp, MasterTypes.MODBUS, Int32.Parse(MasterNum), false));
            }
            refreshList();
            ucmod.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
        }

        public void updateAttributes(List<KeyValuePair<string, string>> mbData)
        {
            if (mbData != null && mbData.Count > 0)
            {
                foreach (KeyValuePair<string, string> mbkp in mbData)
                {
                    Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", mbkp.Key, mbkp.Value);
                    try
                    {
                        if (this.GetType().GetProperty(mbkp.Key) != null) //Ajay: 10/08/2018
                        {
                            this.GetType().GetProperty(mbkp.Key).SetValue(this, mbkp.Value);
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", mbkp.Key, mbkp.Value);
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }

            ucmod.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
            if (MODBUSTreeNode != null) MODBUSTreeNode.Text = "MODBUS " + this.Description;
        }
        private void lvIEDList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucmod.lvIEDList.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
            //if (ProtocolType.ToLower() != "tcp")
            //{
            //    if (String.IsNullOrWhiteSpace(ucmod.txtRemoteIP.Text) || !Utils.IsValidIPv4(ucmod.txtRemoteIP.Text)) ucmod.txtRemoteIP.Text = "0.0.0.0";
            //    if (String.IsNullOrWhiteSpace(ucmod.txtTCPPort.Text)) ucmod.txtTCPPort.Text = "502";
            //}

            if (iedList.Count >= Globals.MaxMODBUSIED)
            {
                MessageBox.Show("Maximum " + Globals.MaxMODBUSIED + " IED's in MODBUS Masters are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            mode = Mode.ADD;
            editIndex = -1;
            Utils.resetValues(ucmod.grpIED);
            Utils.showNavigation(ucmod.grpIED, false);
            loadDefaults();
           
            ucmod.grpIED.Visible = true;
            ucmod.txtUnitID.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucmod.lvIEDList.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one IED ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucmod.lvIEDList.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete IED " + ucmod.lvIEDList.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucmod.lvIEDList.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            MODBUSTreeNode.Nodes.Remove(iedList.ElementAt(iIndex).getTreeNode());
                            Utils.RemoveDI4IED(MasterTypes.MODBUS, Int32.Parse(MasterNum), Int32.Parse(iedList[iIndex].UnitID));
                            iedList.RemoveAt(iIndex);
                            ucmod.lvIEDList.Items[iIndex].Remove();
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
                MessageBox.Show(ex.Message.ToString());
            }
            //Utils.WriteLine(VerboseLevel.DEBUG, "*** iedList count: {0} lv count: {1}", iedList.Count, ucmod.lvIEDList.Items.Count);
            //DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            //if (result == DialogResult.No)
            //{
            //    return;
            //}

            //for (int i = ucmod.lvIEDList.Items.Count - 1; i >= 0; i--)
            //{
            //    if (ucmod.lvIEDList.Items[i].Checked)
            //    {
            //        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", i);
            //        MODBUSTreeNode.Nodes.Remove(iedList.ElementAt(i).getTreeNode());
            //        Utils.RemoveDI4IED(MasterTypes.MODBUS, Int32.Parse(MasterNum), Int32.Parse(iedList[i].UnitID));
            //        iedList.RemoveAt(i);
            //        ucmod.lvIEDList.Items[i].Remove();
            //    }
            //}
            //Utils.WriteLine(VerboseLevel.DEBUG, "*** iedList count: {0} lv count: {1}", iedList.Count, ucmod.lvIEDList.Items.Count);
            //refreshList();
        }

        private void btnExportIED_Click(object sender, EventArgs e)
        {
            if (ucmod.lvIEDList.CheckedItems.Count != 1)
            {
                MessageBox.Show("Select single IED for export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem lvi = ucmod.lvIEDList.CheckedItems[0];
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
            Utils.SaveIEDFile(expObj.exportIED());// exportIED
        }
        //private void btnImportIED_Click(object sender, EventArgs e)
        //{
        //    if (ofdXMLFile.ShowDialog() == DialogResult.OK)
        //    {
        //        Utils.WriteLine(VerboseLevel.DEBUG, "*** Opening file: {0}", ofdXMLFile.FileName);
        //        if (!Utils.IsXMLWellFormed(ofdXMLFile.FileName))
        //        {
        //            MessageBox.Show("Selected file is not a valid XML!!!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.Load(ofdXMLFile.FileName);
        //        //XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
        //        //ns.AddNamespace("oppc", Globals.NAMESPACE_OPEN_PRO_PLUS);
        //        XmlNodeList nodeList = xmlDoc.SelectNodes("IEDexport");
        //        Utils.WriteLine(VerboseLevel.BOMBARD, "nodeList count: {0}", nodeList.Count);

        //        if (nodeList.Count <= 0)
        //        {
        //            MessageBox.Show("Selected file is not an IED exported node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        XmlNode rootNode = nodeList.Item(0);
        //        Utils.WriteLine(VerboseLevel.DEBUG, "*** Exported IED Node name: {0}", rootNode.Name);

        //        if (rootNode.Attributes != null)
        //        {
        //            foreach (XmlAttribute item in rootNode.Attributes)
        //            {
        //                Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", item.Name, item.Value);
        //                if (item.Name == "MasterType")
        //                {
        //                    if (item.Value != mType.ToString())
        //                    {
        //                        MessageBox.Show("Invalid Master Type (" + item.Value + ") to import!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                        return;
        //                    }
        //                }
        //            }
        //        }

        //        foreach (XmlNode node in rootNode)
        //        {
        //            Utils.WriteLine(VerboseLevel.BOMBARD, "node value: '{0}' child count {1}", node.Name, node.ChildNodes.Count);
        //            if (node.Name == "IED")
        //            {
        //                //Utils.WriteLine("***** node type: {0}", node.NodeType);
        //                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
        //                TreeNode tmp = MODBUSTreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
        //                iedList.Add(new IED(node, tmp, MasterTypes.MODBUS, Int32.Parse(MasterNum), true));
        //                Utils.CreateDI4IED(MasterTypes.MODBUS, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
        //                tmp.Expand();
        //            }
        //        }
        //        refreshList();

        //        MessageBox.Show("IED imported successfully!!!", "IED Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }//File selected thru open dialog...
        //}
        private void btnImportIED_Click(object sender, EventArgs e)
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
                        //Utils.WriteLine("***** node type: {0}", node.NodeType);
                        if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                        TreeNode tmp = MODBUSTreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                        iedList.Add(new IED(node, tmp, MasterTypes.MODBUS, Int32.Parse(MasterNum), true));
                        Utils.CreateDI4IED(MasterTypes.MODBUS, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
                        tmp.Expand();
                    }
                }
                refreshList();
                MessageBox.Show("IED imported successfully!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }//File selected thru open dialog...
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;
            List<KeyValuePair<string, string>> iedData = Utils.getKeyValueAttributes(ucmod.grpIED);
            if (mode == Mode.ADD)
            {
                TreeNode tmp = MODBUSTreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                //iedList.Add(new IED("IED",iedData, "MODBUS_IED", tmp, MasterTypes.MODBUS, Int32.Parse(MasterNum)));
                iedList.Add(new IED("IED", iedData, tmp, MasterTypes.MODBUS, Int32.Parse(MasterNum)));
                Utils.CreateDI4IED(MasterTypes.MODBUS, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
            }
            else if (mode == Mode.EDIT)
            {
                iedList[editIndex].updateAttributes(iedData);
                //Namrata:14/06/2017
                if (ucmod.lvIEDList.SelectedIndices.Count > 0)
                {
                    SelectedIndex = Convert.ToInt16(ucmod.lvIEDList.FocusedItem.Index);
                    Utils.IEDUnitID = Convert.ToInt16(ucmod.lvIEDList.Items[SelectedIndex].Text);
                }
                //Namrata:14/06/2017
                Utils.UpdateDI4IED(MasterTypes.MODBUS, Int32.Parse(MasterNum), Utils.IEDUnitID, Int32.Parse(iedList[SelectedIndex].UnitID), iedList[iedList.Count - 1].Device);
            }
            refreshList();
            //Namrata: 09/08/2017
            if (sender != null && e != null)
            {
                ucmod.grpIED.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ucmod.grpIED.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
            Utils.resetValues(ucmod.grpIED);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucmod btnFirst_Click clicked in class!!!");
            if (ucmod.lvIEDList.Items.Count <= 0) return;
            if (iedList.ElementAt(0).IsNodeComment) return;
            editIndex = 0;
            loadValues();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            //Namrata:27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucmod btnPrev_Click clicked in class!!!");
            if (editIndex - 1 < 0) return;
            if (iedList.ElementAt(editIndex - 1).IsNodeComment) return;
            editIndex--;
            loadValues();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //Namrata:27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucmod btnNext_Click clicked in class!!!");
            if (editIndex + 1 >= ucmod.lvIEDList.Items.Count) return;
            if (iedList.ElementAt(editIndex + 1).IsNodeComment) return;
            editIndex++;
            loadValues();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucmod btnLast_Click clicked in class!!!");
            if (ucmod.lvIEDList.Items.Count <= 0) return;
            if (iedList.ElementAt(iedList.Count - 1).IsNodeComment) return;
            editIndex = iedList.Count - 1;
            loadValues();
        }

        private void lvIEDList_DoubleClick(object sender, EventArgs e)
        {
            if (ucmod.lvIEDList.SelectedItems.Count <= 0) return;

            ListViewItem lvi = ucmod.lvIEDList.SelectedItems[0];
            Utils.UncheckOthers(ucmod.lvIEDList, lvi.Index);
            if (iedList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ucmod.grpIED.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            Utils.showNavigation(ucmod.grpIED, true);
            loadValues();
            ucmod.txtUnitID.Focus();
        }

        private void loadDefaults()
        {
            ucmod.txtUnitID.Text = (Globals.getMODBUSIEDNo(Int32.Parse(MasterNum)) + 1).ToString();
            ucmod.txtDevice.Text = "MODBUS_" + (Globals.getMODBUSIEDNo(Int32.Parse(MasterNum)) + 1).ToString();
            ucmod.txtDescription.Text = "MODBUS_IED_" + (Globals.getMODBUSIEDNo(Int32.Parse(MasterNum)) + 1).ToString();
            //ucmod.txtRemoteIP.Text = "0.0.0.0";
            if (ucmod.lvIEDList.Items.Count - 1 >= 0)
            {
                //ucmod.txtUnitID.Text = Convert.ToString(Convert.ToInt32(iedList[iedList.Count - 1].UnitID) + 1);
                ucmod.txtDevice.Text = "MODBUS_" + (Globals.getMODBUSIEDNo(Int32.Parse(MasterNum)) + 1).ToString();
                ucmod.txtDescription.Text = "MODBUS_IED_" + (Globals.getMODBUSIEDNo(Int32.Parse(MasterNum)) + 1).ToString();
            }
            ucmod.txtTCPPort.Text = "502";
            ucmod.txtRetries.Text = "3";
            ucmod.txtTimeOut.Text = "100";
        }

        private void loadValues()
        {
            IED eied = iedList.ElementAt(editIndex);
            if (eied != null)
            {
                ucmod.txtUnitID.Text = eied.UnitID;
                ucmod.txtRemoteIP.Text = eied.RemoteIP;
                ucmod.txtTCPPort.Text = eied.TCPPort;
                ucmod.txtDevice.Text = eied.Device;
                ucmod.txtRetries.Text = eied.Retries;
                ucmod.txtTimeOut.Text = eied.TimeOutMS;
                if (eied.DR.ToLower() == "enable") ucmod.chkDR.Checked = true;
                else ucmod.chkDR.Checked = false;
                ucmod.txtDescription.Text = eied.Description;
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

            if (ProtocolType.ToLower() != "tcp")
            {
                if (String.IsNullOrWhiteSpace(ucmod.txtRemoteIP.Text) || !Utils.IsValidIPv4(ucmod.txtRemoteIP.Text)) ucmod.txtRemoteIP.Text = "0.0.0.0";
                if (String.IsNullOrWhiteSpace(ucmod.txtTCPPort.Text)) ucmod.txtTCPPort.Text = "502";
            }

            //Check empty field's
            if (Utils.IsEmptyFields(ucmod.grpIED))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //IED-UnitID should be unique...
            if (!IsUnitIDUnique(ucmod.txtUnitID.Text))
            {
                MessageBox.Show("IED Unit ID must be unique!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check Remote IP...
            if (!Utils.IsValidIPv4(ucmod.txtRemoteIP.Text))
            {
                MessageBox.Show("Invalid Remote IP.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check TCP Port...
            if (!Utils.IsValidTCPPort(Int32.Parse(ucmod.txtTCPPort.Text)))
            {
                MessageBox.Show("Invalid TCP Port.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return status;
        }

        private void fillOptions()
        {
            //Fill Protocol Type...
            ucmod.cmbProtocolType.Items.Clear();
            foreach (protocolType ptv in Enum.GetValues(typeof(protocolType)))
            {
                ucmod.cmbProtocolType.Items.Add(ptv.ToString());
            }
            ucmod.cmbProtocolType.SelectedIndex = 0;
        }

        private void addListHeaders()
        {
            ucmod.lvIEDList.Columns.Add("Unit ID", 60, HorizontalAlignment.Left);
            //ucmod.lvIEDList.Columns.Add("ASDUAddr", 90, HorizontalAlignment.Left);
            ucmod.lvIEDList.Columns.Add("Device", 90, HorizontalAlignment.Left);
            ucmod.lvIEDList.Columns.Add("Remote IP Address", 150, HorizontalAlignment.Left);
            ucmod.lvIEDList.Columns.Add("TCP Port", 90, HorizontalAlignment.Left);
            ucmod.lvIEDList.Columns.Add("Retires", 60, HorizontalAlignment.Left);
            ucmod.lvIEDList.Columns.Add("Timeout (msec)", 110, HorizontalAlignment.Left);
            ucmod.lvIEDList.Columns.Add("Description", -2, HorizontalAlignment.Left);
            ucmod.lvIEDList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
        }

        private void refreshList()
        {
            int cnt = 0;
            ucmod.lvIEDList.Items.Clear();
            //addListHeaders();
            //Namrata: 25/11/2017
            Utils.MODBUSMasteriedList.Clear();
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
                    row[1] = ied.Device;
                    if (ProtocolType.ToLower() == "tcp")
                    {
                        row[2] = ied.RemoteIP;
                        row[3] = ied.TCPPort;
                    }
                    else
                    {
                        row[2] = "NA";
                        row[3] = "NA";
                    }
                    row[4] = ied.Retries;
                    row[5] = ied.TimeOutMS;
                    row[6] = ied.Description;
                }

                ListViewItem lvItem = new ListViewItem(row);
                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucmod.lvIEDList.Items.Add(lvItem);
            }
            //Namrata: 20/11/2017
            Utils.MODBUSMasteriedList.AddRange(iedList);
        }

        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("MODBUS_")) return ucmod;

            kpArr.RemoveAt(0);
            Utils.WriteLine(VerboseLevel.DEBUG, "$$$$$ elem: {0}", kpArr.ElementAt(0));
            if (kpArr.ElementAt(0).Contains("IED_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Utils.WriteLine(VerboseLevel.DEBUG, "$$$$ After split elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (iedList.Count <= 0) return null;
                return iedList[idx].getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "***** MODBUSMaster: View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }

            return null;
        }

        public TreeNode getTreeNode()
        {
            return MODBUSTreeNode;
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
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(iedn.exportXMLMODBUSnode(), true);
                //XmlNode importNode = rootNode.OwnerDocument.ImportNode(iedn.exportXMLnode(), true);
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
        public string ClockSyncInterval
        {
            get { clockSyncInterval = Int32.Parse(ucmod.txtClockSyncInterval.Text); return clockSyncInterval.ToString(); }
            set { clockSyncInterval = Int32.Parse(value); ucmod.txtClockSyncInterval.Text = value; }
        }
        public string getMasterID
        {
            get { return "MODBUS_" + MasterNum; }
        }

        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }

        public string ProtocolType
        {
            get { pt = (protocolType)Enum.Parse(typeof(protocolType), ucmod.cmbProtocolType.GetItemText(ucmod.cmbProtocolType.SelectedItem)); return pt.ToString(); }
            set
            {
                try
                {
                    pt = (protocolType)Enum.Parse(typeof(protocolType), value);
                    ucmod.cmbProtocolType.SelectedIndex = ucmod.cmbProtocolType.FindStringExact(value);
                    if (pt.ToString().ToLower() == "tcp")
                    {
                        ucmod.txtRemoteIP.Enabled = true;
                        ucmod.txtTCPPort.Enabled = true;
                    }
                    else
                    {
                        ucmod.txtRemoteIP.Enabled = false;
                        ucmod.txtTCPPort.Enabled = false;
                    }
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", value);
                }
            }
        }

        public string MasterNum
        {
            get { masterNum = Int32.Parse(ucmod.txtMasterNo.Text); return masterNum.ToString(); }
            set { masterNum = Int32.Parse(value); ucmod.txtMasterNo.Text = value; Globals.MasterNo = Int32.Parse(value); }
        }

        public string PortNum
        {
            get { portNum = Int32.Parse(ucmod.txtPortNo.Text); return portNum.ToString(); }
            set { portNum = Int32.Parse(value); ucmod.txtPortNo.Text = value; }
        }

        public string Run
        {
            get { run = ucmod.chkRun.Checked; return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
                if (run == true) ucmod.chkRun.Checked = true;
                else ucmod.chkRun.Checked = false;
            }
        }

        public string DEBUG
        {
            get { debug = Int32.Parse(ucmod.txtDebug.Text); return debug.ToString(); }
            set { debug = Int32.Parse(value); ucmod.txtDebug.Text = value; }
        }

        public string PollingIntervalmSec
        {
            get { pollingIntervalmSec = Int32.Parse(ucmod.txtPollingInterval.Text); return pollingIntervalmSec.ToString(); }
            set { pollingIntervalmSec = Int32.Parse(value); ucmod.txtPollingInterval.Text = value; }
        }

        public string PortTimesyncSec
        {
            get { portTimesyncSec = Int32.Parse(ucmod.txtClockSyncInterval.Text); return portTimesyncSec.ToString(); }
            set { portTimesyncSec = Int32.Parse(value); ucmod.txtClockSyncInterval.Text = value; }
        }

        public string RefreshInterval
        {
            get
            {
                try
                {
                    refreshInterval = Int32.Parse(ucmod.txtRefreshInterval.Text);
                }
                catch (System.FormatException)
                {
                    refreshInterval = 120;
                    ucmod.txtRefreshInterval.Text = refreshInterval.ToString();
                }
                return refreshInterval.ToString();
            }
            set { refreshInterval = Int32.Parse(value); ucmod.txtRefreshInterval.Text = value; }
        }

        public string AppFirmwareVersion
        {
            get { return appFirmwareVersion = ucmod.txtFirmwareVer.Text; }
            set { appFirmwareVersion = ucmod.txtFirmwareVer.Text = value; }
        }

        public string Description
        {
            get { return desc = ucmod.txtMBDescription.Text; }
            set { desc = ucmod.txtMBDescription.Text = value; }
        }
    }
}
