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
    * \brief     <b>MDCalculation</b> is a class to store all the MD's
    * \details   This class stores info related to all MD's. It allows
    * user to add multiple MD's. Whenever a MD is added, it creates
    * a entry in Virtual Master for virtual parameter. It also exports the XML node related to this object.
    * 
    */
    public class MDCalculation
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "MDCalculation";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;

        private bool isNodeComment = false;
        private string comment = "";
        private int windowTime = 15;
        private int slidingWindowTime = 1;
        List<MD> mdList = new List<MD>();
        ucMDCalculation ucmdc = new ucMDCalculation();
        private string[] arrAttributes = { "WindowTime", "SlidingWindowTime" };

        public MDCalculation()
        {
            ucmdc.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucmdc.lvMDItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvMD_ItemCheck);
            ucmdc.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucmdc.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucmdc.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucmdc.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            ucmdc.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            ucmdc.btnNextClick += new System.EventHandler(this.btnNext_Click);
            ucmdc.btnLastClick += new System.EventHandler(this.btnLast_Click);
            ucmdc.lvMDDoubleClick += new System.EventHandler(this.lvMD_DoubleClick);
            addListHeaders();
            fillOptions();
            ucmdc.txtWindowTime.Text = windowTime.ToString();
            ucmdc.txtSlidingWindowTime.Text = slidingWindowTime.ToString();
        }
        private void lvMD_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucmdc.lvMD.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
            if (mdList.Count >= Globals.MaxMDCalc)
            {
                MessageBox.Show("Maximum " + Globals.MaxMDCalc + " MD's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Console.WriteLine("*** ucmdc btnAdd_Click clicked in class!!!");
            mode = Mode.ADD;
            editIndex = -1;
            Utils.resetValues(ucmdc.grpMD);
            Utils.showNavigation(ucmdc.grpMD, false);
            loadDefaults();
            ucmdc.txtMDIndex.Text = (Globals.MDNo + 1).ToString();
            ucmdc.grpMD.Visible = true;
            ucmdc.txtVAIno.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Utils.WriteLine(VerboseLevel.DEBUG, "*** mbsList count: {0} lv count: {1}", mbsList.Count, ucmbs.lvMODBUSSlave.Items.Count);
            if (ucmdc.lvMD.Items.Count == 0)
            {
                MessageBox.Show("Please add MD Calculation", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (ucmdc.lvMD.CheckedItems.Count == 1)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete MD Calculation " + ucmdc.lvMD.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    int iIndex = ucmdc.lvMD.CheckedItems[0].Index;
                    Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                    if (result == DialogResult.Yes)
                    {
                        DeleteProfile(iIndex);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select atleast MD Calculation ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //Utils.WriteLine(VerboseLevel.DEBUG, "*** mbsList count: {0} lv count: {1}", mbsList.Count, ucmbs.lvMODBUSSlave.Items.Count);
                refreshList();
            }
            ////Namrata:24/5/2017
            // Utils.WriteLine(VerboseLevel.DEBUG, "*** mdList count: {0} lv count: {1}", mdList.Count, ucmdc.lvMD.Items.Count);
            // DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            // if (result == DialogResult.No)
            // {
            //     return;
            // }

            // for (int i = ucmdc.lvMD.Items.Count - 1; i >= 0; i--)
            // {
            //       if (ucmdc.lvMD.Items[i].Checked)
            //         {
            //         Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", i);
            //         DeleteProfile(i);
            //     }
            // }
            // Utils.WriteLine(VerboseLevel.DEBUG, "*** mdList count: {0} lv count: {1}", mdList.Count, ucmdc.lvMD.Items.Count);
            // refreshList();
        }
        //Namrata:24/5/2017
        public void DeleteProfile(int arrIndex)
        {
            //Ajay: 25/09/2018
            //Utils.RemoveDI4Profile(Int32.Parse(mdList[arrIndex].MDIndex));
            bool IsAllow = Utils.RemoveDI4MDCalculation(Int32.Parse(mdList[arrIndex].MDIndex));   //Ajay: 25/09/2018
            if (IsAllow)
            {
                mdList.RemoveAt(arrIndex);
                ucmdc.lvMD.Items[arrIndex].Remove();
            }
            else { }
        }
        public void DeleteMD(int arrIndex, bool handleMD0)
        {
            Utils.RemoveDI4MD(Int32.Parse(mdList[arrIndex].MDIndex));
            Utils.RemoveAI4MD(Int32.Parse(mdList[arrIndex].MDIndex));
            mdList.RemoveAt(arrIndex);
            ucmdc.lvMD.Items[arrIndex].Remove();

            if (handleMD0)
            {
                //Special case...
                if (mdList.Count == 1)
                {
                    //Delete MD0 entry...
                    mdList.RemoveAt(0);
                }
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;

            List<KeyValuePair<string, string>> mdData = Utils.getKeyValueAttributes(ucmdc.grpMD);
            if (mode == Mode.ADD)
            {
                if (mdList.Count <= 0)
                {
                    CreateMD0Entry();
                }
                mdList.Add(new MD("MD", mdData));
                Utils.CreateDI4MD(Int32.Parse(mdList[mdList.Count-1].MDIndex));
                Utils.CreateAI4MD(Int32.Parse(mdList[mdList.Count - 1].MDIndex), Int32.Parse(mdList[mdList.Count - 1].Multiplier));
            }
            else if (mode == Mode.EDIT)
            {
                mdList[editIndex].updateAttributes(mdData);
            }
            refreshList();
            //Namrata: 09/08/2017
            if (sender != null && e != null)
            {
                ucmdc.grpMD.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ucmdc.grpMD.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
            Utils.resetValues(ucmdc.grpMD);
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucmdc btnFirst_Click clicked in class!!!");
            if (ucmdc.lvMD.Items.Count <= 0) return;
            if (mdList.ElementAt(0).IsNodeComment) return;
            editIndex = 0;
            if (mdList.ElementAt(editIndex).MDIndex == "0") EnableControls(false);
            else EnableControls(true);
            loadValues();
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            //Namrata:27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucmdc btnPrev_Click clicked in class!!!");
            if (editIndex - 1 < 0) return;
            if (mdList.ElementAt(editIndex - 1).IsNodeComment) return;
            editIndex--;
            if (mdList.ElementAt(editIndex).MDIndex == "0") EnableControls(false);
            else EnableControls(true);
            loadValues();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            //Namrata:27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucmdc btnNext_Click clicked in class!!!");
            if (editIndex + 1 >= ucmdc.lvMD.Items.Count) return;
            if (mdList.ElementAt(editIndex + 1).IsNodeComment) return;
            editIndex++;
            if (mdList.ElementAt(editIndex).MDIndex == "0") EnableControls(false);
            else EnableControls(true);
            loadValues();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucmdc btnLast_Click clicked in class!!!");
            if (ucmdc.lvMD.Items.Count <= 0) return;
            if (mdList.ElementAt(mdList.Count - 1).IsNodeComment) return;
            editIndex = mdList.Count - 1;
            if (mdList.ElementAt(editIndex).MDIndex == "0") EnableControls(false);
            else EnableControls(true);
            loadValues();
        }
        private void lvMD_DoubleClick(object sender, EventArgs e)
        {
            if (ucmdc.lvMD.SelectedItems.Count <= 0) return;

            ListViewItem lvi = ucmdc.lvMD.SelectedItems[0];
            Utils.UncheckOthers(ucmdc.lvMD, lvi.Index);
            if (mdList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (lvi.Index==0)
            {
                EnableControls(false);//For MD 0, do not allow to edit few entries...
            }
            else
            {
                EnableControls(true);
            }
            ucmdc.grpMD.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            Utils.showNavigation(ucmdc.grpMD, true);
            loadValues();
            ucmdc.txtVAIno.Focus();
        }
        private void loadDefaults()
        {
            ucmdc.txtVAIno.Text = "0";
            ucmdc.txtI1AIno.Text = "0";
            ucmdc.txtI2AIno.Text = "0";
            ucmdc.txtI3AIno.Text = "0";
            ucmdc.txtMultiplier.Text = "1";
            ucmdc.txtHigh.Text = "1";
        }
        private void loadValues()
        {
            MD md = mdList.ElementAt(editIndex);
            if (md != null)
            {
                ucmdc.txtMDIndex.Text = md.MDIndex;
                ucmdc.txtVAIno.Text = md.V_AINo;
                ucmdc.txtI1AIno.Text = md.I1_AINo;
                ucmdc.txtI2AIno.Text = md.I2_AINo;
                ucmdc.txtI3AIno.Text = md.I3_AINo;
                ucmdc.txtMultiplier.Text = md.Multiplier;
                ucmdc.txtHigh.Text = md.High;
            }
        }
        private bool Validate()
        {
            bool status = true;

            //Check empty field's
            if (Utils.IsEmptyFields(ucmdc.grpMD))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check invalid values...
            if (Utils.IsLessThanZero(ucmdc.txtVAIno.Text) || Utils.IsLessThanZero(ucmdc.txtI1AIno.Text) || Utils.IsLessThanZero(ucmdc.txtI2AIno.Text)
                || Utils.IsLessThanZero(ucmdc.txtI3AIno.Text) || Utils.IsLessThanZero(ucmdc.txtMultiplier.Text) || Utils.IsLessThanZero(ucmdc.txtHigh.Text))
            {
                MessageBox.Show("Fields cannot be less than zero.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Utils.IsGreaterThanZero(ucmdc.txtVAIno.Text) && !Utils.IsValidAI(ucmdc.txtVAIno.Text))
            {
                MessageBox.Show("V AI No. is not a valid AI.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Utils.IsGreaterThanZero(ucmdc.txtI1AIno.Text) && !Utils.IsValidAI(ucmdc.txtI1AIno.Text))
            {
                MessageBox.Show("I1 AI No. is not a valid AI.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Utils.IsGreaterThanZero(ucmdc.txtI2AIno.Text) && !Utils.IsValidAI(ucmdc.txtI2AIno.Text))
            {
                MessageBox.Show("I2 AI No. is not a valid AI.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Utils.IsGreaterThanZero(ucmdc.txtI3AIno.Text) && !Utils.IsValidAI(ucmdc.txtI3AIno.Text))
            {
                MessageBox.Show("I3 AI No. is not a valid AI.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void CreateMD0Entry()
        {
            List<KeyValuePair<string, string>> mdzData = new List<KeyValuePair<string, string>>();
            mdzData.Add(new KeyValuePair<string, string>("MDIndex", "0"));
            mdzData.Add(new KeyValuePair<string, string>("V_AINo", "0"));
            mdzData.Add(new KeyValuePair<string, string>("I1_AINo", "0"));
            mdzData.Add(new KeyValuePair<string, string>("I2_AINo", "0"));
            mdzData.Add(new KeyValuePair<string, string>("I3_AINo", "0"));
            mdzData.Add(new KeyValuePair<string, string>("Multiplier", "1"));
            mdzData.Add(new KeyValuePair<string, string>("High", "0"));
            mdList.Add(new MD("MD", mdzData));
        }
        private void EnableControls(bool enable)
        {
            ucmdc.txtVAIno.Enabled = enable;
            ucmdc.txtI1AIno.Enabled = enable;
            ucmdc.txtI2AIno.Enabled = enable;
            ucmdc.txtI3AIno.Enabled = enable;
            ucmdc.txtMultiplier.Enabled = enable;
        }
        private void fillOptions()
        {
            //Fill combobox values, if any...
        }
        //Special Case For MD Calculation
        //Add Defauylt Values In Md Calculation
        private void addListHeaders()
        {
            ucmdc.lvMD.Columns.Add("No.", 40, HorizontalAlignment.Left);
            ucmdc.lvMD.Columns.Add("V AI No.", 60, HorizontalAlignment.Left);
            ucmdc.lvMD.Columns.Add("I1 AI No.", 60, HorizontalAlignment.Left);
            ucmdc.lvMD.Columns.Add("I2 AI No.", 60, HorizontalAlignment.Left);
            ucmdc.lvMD.Columns.Add("I3 AI No.", 60, HorizontalAlignment.Left);
            ucmdc.lvMD.Columns.Add("Multiplier", 60, HorizontalAlignment.Left);
            ucmdc.lvMD.Columns.Add("High", -2, HorizontalAlignment.Left);
        }
        public void refreshList()
        {
            int cnt = 0;
            ucmdc.lvMD.Items.Clear();
            //addListHeaders();
            foreach (MD mdNode in mdList)
            {
                string[] row = new string[7];
                int rNo = 0;
                if (mdNode.IsNodeComment)
                {
                    row[rNo] = "Comment...";
                }
                else
                {
                    row[rNo++] = mdNode.MDIndex;
                    row[rNo++] = mdNode.V_AINo;
                    row[rNo++] = mdNode.I1_AINo;
                    row[rNo++] = mdNode.I2_AINo;
                    row[rNo++] = mdNode.I3_AINo;
                    row[rNo++] = mdNode.Multiplier;
                    row[rNo++] = mdNode.High;
                    //row[rNo++] = mdNode.DI;
                }
                ListViewItem lvItem = new ListViewItem(row);
                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucmdc.lvMD.Items.Add(lvItem);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("MDCalculation_")) return ucmdc;
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

            //Needed To Comment As Per Requirement of Shilpa
            //Namrata:13/7/2017
            foreach (string attr in arrAttributes)
            {
                XmlAttribute attrName = xmlDoc.CreateAttribute(attr);
                attrName.Value = (string)this.GetType().GetProperty(attr).GetValue(this);
                rootNode.Attributes.Append(attrName);
            }
            foreach (MD mdn in mdList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(mdn.exportXMLnode(), true);
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
        public void regenerateSequence()
        {
            int oMDNo = -1;
            int nMDNo = -1;
            //Reset MD no.
            Globals.resetUniqueNos(ResetUniqueNos.MD);
            Globals.MDNo++;//Start from 1...
            foreach (MD md in mdList)
            {
                if (md.MDIndex == "0") continue;//IMP: Ignore 'MD=0' value.
                oMDNo = Int32.Parse(md.MDIndex);
                nMDNo = Globals.MDNo++;
                md.MDIndex = nMDNo.ToString();

                //Replace in Virtual master
                foreach (VirtualMaster vm in Utils.getOpenProPlusHandle().getMasterConfiguration().getVirtualGroup().getVirtualMasters())
                {
                    foreach (IED ied in vm.getIEDs())
                    {
                        ied.changeMDSequence(oMDNo, nMDNo);
                    }
                }
            }
            Utils.getOpenProPlusHandle().getMasterConfiguration().getVirtualGroup().refreshList();
            Utils.getOpenProPlusHandle().getParameterLoadConfiguration().getMDCalculation().refreshList();
        }
        public void resetReindexFlags()
        {
            foreach (MD md in mdList)
            {
                md.IsReindexedV_AINo = false;
                md.IsReindexedI1_AINo = false;
                md.IsReindexedI2_AINo = false;
                md.IsReindexedI3_AINo = false;
            }
        }
        public void ChangeAISequence(int oAINo, int nAINo)
        {
            //Do not break as there can be one AI referred in multiple MD's...
            foreach (MD md in mdList)
            {
                if (md.V_AINo == oAINo.ToString() && !md.IsReindexedV_AINo)
                {
                    md.V_AINo = nAINo.ToString();
                    md.IsReindexedV_AINo = true;
                }
                if (md.I1_AINo == oAINo.ToString() && !md.IsReindexedI1_AINo)
                {
                    md.I1_AINo = nAINo.ToString();
                    md.IsReindexedI1_AINo = true;
                }
                if (md.I2_AINo == oAINo.ToString() && !md.IsReindexedI2_AINo)
                {
                    md.I2_AINo = nAINo.ToString();
                    md.IsReindexedI2_AINo = true;
                }
                if (md.I3_AINo == oAINo.ToString() && !md.IsReindexedI3_AINo)
                {
                    md.I3_AINo = nAINo.ToString();
                    md.IsReindexedI3_AINo = true;
                }
            }
            refreshList();
        }
        public void parseMDCGNode(XmlNode mdcgNode, TreeNode tn)
        {
            //First set root node name...
            rnName = mdcgNode.Name;
            if (mdcgNode.Attributes != null)
            {
                foreach (XmlAttribute item in mdcgNode.Attributes)
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
                        Utils.WriteLine(VerboseLevel.WARNING, "Field doesn't exist. XML and class fields mismatch!!!");
                    }
                }
                Utils.Write(VerboseLevel.DEBUG, "\n");
            }
            else if (mdcgNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = mdcgNode.Value;
            }

            if (tn != null) tn.Nodes.Clear();
            foreach (XmlNode node in mdcgNode)
            {
                //Utils.WriteLine("***** node type: {0}", node.NodeType);
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                //No need: TreeNode tmp = tn.Nodes.Add("IEC103_"+Utils.GenerateShortUniqueKey(), "IEC103", "IEC103", "IEC103");
                mdList.Add(new MD(node));
            }
            refreshList();
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string WindowTime
        {
            get
            {
                try
                {
                    windowTime = Int32.Parse(ucmdc.txtWindowTime.Text);
                }
                catch (System.FormatException)
                {
                    windowTime = -1;
                    ucmdc.txtWindowTime.Text = windowTime.ToString();
                }
                return windowTime.ToString();
            }
            set { windowTime = Int32.Parse(value); ucmdc.txtWindowTime.Text = value; }
        }
        public string SlidingWindowTime
        {
            get {
                try
                {
                    slidingWindowTime = Int32.Parse(ucmdc.txtSlidingWindowTime.Text);
                }
                catch (System.FormatException)
                {
                    slidingWindowTime = -1;
                    ucmdc.txtSlidingWindowTime.Text = slidingWindowTime.ToString();
                }
                return slidingWindowTime.ToString();
            }
            set { slidingWindowTime = Int32.Parse(value); ucmdc.txtSlidingWindowTime.Text = value; }
        }

        public int getCount()
        {
            int ctr = 0;
            foreach (MD mdNode in mdList)
            {
                if (mdNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }

        public List<MD> getMDs()
        {
            return mdList;
        }
    }
}
