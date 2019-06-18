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
    public class ADRMaster
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        enum masterType
        {
            ADR
        };
        private int clockSyncInterval = -1;
        private bool isNodeComment = false;
        private string comment = "";
        private masterType mType = masterType.ADR;
        private int masterNum = -1;
        private bool run = true;
        private int debug = -1;
        private int pollingintervalmSec = 300;
        private int porttimesyncSec = -1;
        private int refreshInterval = 120;
        private string appFirmwareVersion;
        private string desc = "";
        //Namrata:15/6/2017
        private int SelectedIndex;
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        List<IED> iedList = new List<IED>();
        ucADRMasterConfiguration ucadr = new ucADRMasterConfiguration();
        private TreeNode ADRMasterTreeNode;
        private OpenFileDialog ofdXMLFile = new OpenFileDialog();
        private string[] arrAttributes = { "MasterNum", "ClockSyncInterval", "Run", "DEBUG", "PollingIntervalmSec", "PortTimesyncSec", "RefreshInterval", "AppFirmwareVersion", "Description" };
        #endregion Declaration
        public ADRMaster(string ADRName, List<KeyValuePair<string, string>> ADRData, TreeNode tn)
        {
            string strRoutineName = "ADRMaster";
            try
            {
                ucadr.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
                ucadr.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
                ucadr.btnExportIED.Click += new System.EventHandler(this.btnExportIED_Click);
                ucadr.btnImportIED.Click += new System.EventHandler(this.btnImportIED_Click);
                ucadr.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucadr.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucadr.lvIEDList.DoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
                addListHeaders();
                fillOptions();
                //First set the root element value...
                try
                {
                    mType = (masterType)Enum.Parse(typeof(masterType), ADRName);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", ADRName);
                }
                //Parse n store values...
                if (ADRData != null && ADRData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> ADRkp in ADRData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", ADRkp.Key, ADRkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(ADRkp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(ADRkp.Key).SetValue(this, ADRkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", ADRkp.Key, ADRkp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                if (tn != null) tn.Nodes.Clear();
                ADRMasterTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
                if (tn != null) tn.Text = "ADR " + this.Description;   //Now since we have parsed, we can name the treenode...
                refreshList();
                ucadr.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public ADRMaster(XmlNode mNode, TreeNode tn)
        {
            string strRoutineName = "ADRMaster";
            try
            {
                ucadr.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucadr.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucadr.btnExportIEDClick += new System.EventHandler(this.btnExportIED_Click);
                ucadr.btnImportIEDClick += new System.EventHandler(this.btnImportIED_Click);
                ucadr.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucadr.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucadr.lvIEDListDoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
                addListHeaders();
                fillOptions();
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
                ADRMasterTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
                                       //Now since we have parsed, we can name the treenode...
                tn.Text = "ADR " + this.Description;
                foreach (XmlNode node in mNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    TreeNode tmp = tn.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                    iedList.Add(new IED(node, tmp, MasterTypes.ADR, Int32.Parse(MasterNum), false));
                }
                refreshList();
                ucadr.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateAttributes(List<KeyValuePair<string, string>> ADRData)
        {
            string strRoutineName = "updateAttributes";
            try
            {
                if (ADRData != null && ADRData.Count > 0)
                {
                    foreach (KeyValuePair<string, string> ADRkp in ADRData)
                    {
                        Utils.Write(VerboseLevel.DEBUG, "{0} {1} ", ADRkp.Key, ADRkp.Value);
                        try
                        {
                            if (this.GetType().GetProperty(ADRkp.Key) != null) //Ajay: 10/08/2018
                            {
                                this.GetType().GetProperty(ADRkp.Key).SetValue(this, ADRkp.Value);
                            }
                        }
                        catch (System.NullReferenceException)
                        {
                            Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!! key: {0} value: {1}", ADRkp.Key, ADRkp.Value);
                        }
                    }
                    Utils.Write(VerboseLevel.DEBUG, "\n");
                }
                ucadr.lblIED.Text = "IED List (Master No: " + this.MasterNum + ")";
                if (ADRMasterTreeNode != null) ADRMasterTreeNode.Text = "ADR " + this.Description;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAdd_Click";
            try
            {
                if (iedList.Count >= Globals.MaxADRIED)
                {
                    MessageBox.Show("Maximum " + Globals.MaxADRIED + " IED's in ADR Masters are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucadr.grpIED);
                Utils.showNavigation(ucadr.grpIED, false);
                loadDefaults();
                //Namrata:3/6/2017
                ucadr.txtUnitID.Text = (Globals.getADRIEDNo(Int32.Parse(MasterNum)) + 1).ToString();
                ucadr.grpIED.Visible = true;
                ucadr.txtUnitID.Focus();
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
                Utils.WriteLine(VerboseLevel.DEBUG, "*** iedList count: {0} lv count: {1}", iedList.Count, ucadr.lvIEDList.Items.Count);
                DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    return;
                }
                for (int i = ucadr.lvIEDList.Items.Count - 1; i >= 0; i--)
                {
                    if (ucadr.lvIEDList.Items[i].Checked)
                    {
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", i);
                        ADRMasterTreeNode.Nodes.Remove(iedList.ElementAt(i).getTreeNode());
                        Utils.RemoveDI4IED(MasterTypes.ADR, Int32.Parse(MasterNum), Int32.Parse(iedList[i].UnitID));
                        iedList.RemoveAt(i);
                        ucadr.lvIEDList.Items[i].Remove();
                    }
                }
                Utils.WriteLine(VerboseLevel.DEBUG, "*** iedList count: {0} lv count: {1}", iedList.Count, ucadr.lvIEDList.Items.Count);
                refreshList();
            }

            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExportIED_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnExportIED_Click";
            try
            {
                if (ucadr.lvIEDList.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Select Single IED For Export!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ListViewItem lvi = ucadr.lvIEDList.CheckedItems[0];
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
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnImportIED_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnImportIED_Click";
            try
            {
                if (ucadr.lvIEDList.Items.Count >= 1)
                {
                    MessageBox.Show("Only 1 IED required", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
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
                                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                                TreeNode tmp = ADRMasterTreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                                iedList.Add(new IED(node, tmp, MasterTypes.ADR, Int32.Parse(MasterNum), true));
                                Utils.CreateDI4IED(MasterTypes.ADR, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
                                tmp.Expand();
                            }
                        }
                        refreshList();
                        MessageBox.Show("IED imported successfully!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }//File selected thru open dialog...
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDone_Click";
            try
            {
                if (!Validate()) return;
                List<KeyValuePair<string, string>> iedData = Utils.getKeyValueAttributes(ucadr.grpIED);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = ADRMasterTreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                    iedList.Add(new IED("IED", iedData, tmp, MasterTypes.ADR, Int32.Parse(MasterNum)));
                    //Namrata:11/7/2017
                    Utils.CreateDI4IED(MasterTypes.ADR, Int32.Parse(MasterNum), Int32.Parse(iedList[iedList.Count - 1].UnitID), iedList[iedList.Count - 1].Device);
                }
                else if (mode == Mode.EDIT)
                {
                    iedList[editIndex].updateAttributes(iedData);
                    if (ucadr.lvIEDList.SelectedIndices.Count > 0)
                    {
                        SelectedIndex = Convert.ToInt16(ucadr.lvIEDList.FocusedItem.Index);
                        Utils.IEDUnitID = Convert.ToInt16(ucadr.lvIEDList.Items[SelectedIndex].Text);
                    }
                    Utils.UpdateDI4IED(MasterTypes.ADR, Int32.Parse(MasterNum), Utils.IEDUnitID, Int32.Parse(iedList[SelectedIndex].UnitID), iedList[iedList.Count - 1].Device);
                }
                refreshList();
                ucadr.grpIED.Visible = false;
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
                ucadr.grpIED.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucadr.grpIED);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvIEDList_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvIEDList_DoubleClick";
            try
            {
                if (ucadr.lvIEDList.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucadr.lvIEDList.SelectedItems[0];
                Utils.UncheckOthers(ucadr.lvIEDList, lvi.Index);
                if (iedList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucadr.grpIED.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucadr.grpIED, true);
                loadValues();
                ucadr.txtUnitID.Focus();
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
                ucadr.txtUnitID.Text = "1";
                ucadr.txtDevice.Text = "ADR_" + (Globals.getADRIEDNo(Int32.Parse(MasterNum)) + 1).ToString();
                ucadr.txtRetries.Text = "3";
                ucadr.txtTimeOut.Text = "100";
                ucadr.txtDescription.Text = "ADR_IED_" + (Globals.getADRIEDNo(Int32.Parse(MasterNum)) + 1).ToString();
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
                IED eied = iedList.ElementAt(editIndex);
                if (eied != null)
                {
                    ucadr.txtUnitID.Text = eied.UnitID;
                    ucadr.txtDevice.Text = eied.Device;
                    ucadr.txtRetries.Text = eied.Retries;
                    ucadr.txtTimeOut.Text = eied.TimeOutMS;
                    ucadr.txtDescription.Text = eied.Description;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsUnitIDUnique(string unitID)
        {
            string strRoutineName = "IsUnitIDUnique";
            try
            {
                for (int i = 0; i < iedList.Count; i++)
                {
                    IED ied = iedList.ElementAt(i);
                    if (ied.UnitID == unitID && (mode == Mode.ADD || editIndex != i)) return false;
                }

                return true;
            }
            catch (Exception Ex)
            {
                throw Ex;
                //MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Validate()
        {
            try
            {
                bool status = true;
                //Check empty field's
                if (Utils.IsEmptyFields(ucadr.grpIED))
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
            //fill combobox options, if any...
        }
        private void addListHeaders()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                ucadr.lvIEDList.Columns.Add("Unit ID", 60, HorizontalAlignment.Left);
                ucadr.lvIEDList.Columns.Add("Device", 90, HorizontalAlignment.Left);
                ucadr.lvIEDList.Columns.Add("Retires", 60, HorizontalAlignment.Left);
                ucadr.lvIEDList.Columns.Add("Timeout (msec)", 110, HorizontalAlignment.Left);
                ucadr.lvIEDList.Columns.Add("Description", 110, HorizontalAlignment.Left);
                //ucadr.lvIEDList.Columns.Add("DR", -2, HorizontalAlignment.Left);
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
                ucadr.lvIEDList.Items.Clear();
                //Namrata: 25/11/2017
                Utils.ADRMasteriedList.Clear();
                foreach (IED ied in iedList)
                {
                    string[] row = new string[5];
                    if (ied.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = ied.UnitID;
                        row[1] = ied.Device;
                        row[2] = ied.Retries;
                        row[3] = ied.TimeOutMS;
                        row[4] = ied.Description;
                        //row[5] = ied.DR;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucadr.lvIEDList.Items.Add(lvItem);
                }
                //Namrata: 20/11/2017
                Utils.ADRMasteriedList.AddRange(iedList);
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
                if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("ADR_")) return ucadr;
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
            catch (Exception Ex)
            {
                throw Ex;
                //MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public TreeNode getTreeNode()
        {
            return ADRMasterTreeNode;
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
            try
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
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<IED> getIEDs()
        {
            return iedList;
        }
        public List<IED> getIEDsByFilter(string iedID)
        {
            try
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
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public string getMasterID
        {
            get { return "ADR_" + MasterNum; }
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string Run
        {
            get { run = ucadr.chkRun.Checked; return (run == true ? "YES" : "NO"); }
            set
            {
                run = (value.ToLower() == "yes") ? true : false;
                if (run == true) ucadr.chkRun.Checked = true;
                else ucadr.chkRun.Checked = false;
            }
        }

        public string ClockSyncInterval
        {
            get { clockSyncInterval = Int32.Parse(ucadr.txtClockSyncInterval.Text); return clockSyncInterval.ToString(); }
            set { clockSyncInterval = Int32.Parse(value); ucadr.txtClockSyncInterval.Text = value; }
        }
        public string DEBUG
        {
            get { debug = Int32.Parse(ucadr.txtDebug.Text); return debug.ToString(); }
            set { debug = Int32.Parse(value); ucadr.txtDebug.Text = value; }
        }
        public string MasterNum
        {
            get { masterNum = Int32.Parse(ucadr.txtMasterNo.Text); return masterNum.ToString(); }
            set { masterNum = Int32.Parse(value); ucadr.txtMasterNo.Text = value; Globals.MasterNo = Int32.Parse(value); }
        }
        public string PollingIntervalmSec
        {
            get
            {
                try
                {
                    pollingintervalmSec = Int32.Parse(ucadr.txtPollingInterval.Text);
                }
                catch (System.FormatException)
                {
                    pollingintervalmSec = 120;
                    ucadr.txtPollingInterval.Text = pollingintervalmSec.ToString();
                }
                return refreshInterval.ToString();
            }
            set { pollingintervalmSec = Int32.Parse(value); ucadr.txtPollingInterval.Text = value; }
        }
        public string PortTimesyncSec
        {
            get
            {
                try
                {
                    porttimesyncSec = Int32.Parse(ucadr.txtPortTimesync.Text);
                }
                catch (System.FormatException)
                {
                    porttimesyncSec = 120;
                    ucadr.txtPortTimesync.Text = porttimesyncSec.ToString();
                }
                return refreshInterval.ToString();
            }
            set { porttimesyncSec = Int32.Parse(value); ucadr.txtPortTimesync.Text = value; }
        }
        public string RefreshInterval
        {
            get
            {
                try
                {
                    refreshInterval = Int32.Parse(ucadr.txtRefreshInterval.Text);
                }
                catch (System.FormatException)
                {
                    refreshInterval = 120;
                    ucadr.txtRefreshInterval.Text = refreshInterval.ToString();
                }
                return refreshInterval.ToString();
            }
            set { refreshInterval = Int32.Parse(value); ucadr.txtRefreshInterval.Text = value; }
        }
        public string AppFirmwareVersion
        {
            get { return appFirmwareVersion = ucadr.txtFirmwareVer.Text; }
            set { appFirmwareVersion = ucadr.txtFirmwareVer.Text = value; }
        }
        public string Description
        {
            get { return desc = ucadr.txtIECDesc.Text; }
            set { desc = ucadr.txtIECDesc.Text = value; }
        }
    }
}
