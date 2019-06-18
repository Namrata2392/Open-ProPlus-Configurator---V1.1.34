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
    * \brief     <b>ProfileRecord</b> is a class to store all the Profile's
    * \details   This class stores info related to all Profile's. It allows
    * user to add multiple Profile's. Whenever a Profile is added, it creates
    * a entry in Virtual Master for virtual parameter. It also exports the XML node related to this object.
    * 
    */
    public class ProfileRecord
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "ProfileRecord";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        private bool isNodeComment = false;
        private string comment = "";
        private int profileInterval = 10;
        List<Profile> pList = new List<Profile>();
        ucProfileRecord ucpr = new ucProfileRecord();
        private string[] arrAttributes = { "ProfileInterval" };
        public ProfileRecord()
        {
            ucpr.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucpr.lvProfileItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvProfile_ItemCheck);
            ucpr.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucpr.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucpr.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucpr.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
            ucpr.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
            ucpr.btnNextClick += new System.EventHandler(this.btnNext_Click);
            ucpr.btnLastClick += new System.EventHandler(this.btnLast_Click);
            ucpr.lvProfileDoubleClick += new System.EventHandler(this.lvProfile_DoubleClick);
            addListHeaders();
            fillOptions();
            ucpr.txtProfileInterval.Text = profileInterval.ToString();
        }
        private void lvProfile_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucpr.lvProfile.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
            if (pList.Count >= Globals.MaxProfile)
            {
                MessageBox.Show("Maximum " + Globals.MaxProfile + " Profile's are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            mode = Mode.ADD;
            editIndex = -1;
            Utils.resetValues(ucpr.grpP);
            Utils.showNavigation(ucpr.grpP, false);
            loadDefaults();
            ucpr.txtProfileIndex.Text = (Globals.ProfileNo + 1).ToString();
            ucpr.grpP.Visible = true;
            ucpr.txtAINo.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Utils.WriteLine(VerboseLevel.DEBUG, "*** mbsList count: {0} lv count: {1}", mbsList.Count, ucmbs.lvMODBUSSlave.Items.Count);
            if (ucpr.lvProfile.Items.Count == 0)
            {
                MessageBox.Show("Please add Profile Record ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (ucpr.lvProfile.CheckedItems.Count == 1)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete Profile Record " + ucpr.lvProfile.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    int iIndex = ucpr.lvProfile.CheckedItems[0].Index;
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
                    MessageBox.Show("Please select atleast one Profile Record ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //Utils.WriteLine(VerboseLevel.DEBUG, "*** mbsList count: {0} lv count: {1}", mbsList.Count, ucmbs.lvMODBUSSlave.Items.Count);
                refreshList();
            }
            //Utils.WriteLine(VerboseLevel.DEBUG, "*** pList count: {0} lv count: {1}", pList.Count, ucpr.lvProfile.Items.Count);
            //DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            //if (result == DialogResult.No)
            //{
            //    return;
            //}
            //for (int i = ucpr.lvProfile.Items.Count - 1; i >= 0; i--)
            //{
            //    if (ucpr.lvProfile.Items[i].Checked)
            //    {
            //        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", i);
            //        DeleteProfile(i);
            //    }
            //}
            //Utils.WriteLine(VerboseLevel.DEBUG, "*** pList count: {0} lv count: {1}", pList.Count, ucpr.lvProfile.Items.Count);
            //refreshList();
        }
        public void DeleteProfile(int arrIndex)
        {
            //Ajay: 25/09/2018
            //Utils.RemoveDI4Profile(Int32.Parse(pList[arrIndex].ProfileIndex));
            bool IsAllow = Utils.RemoveDI4Profile(Int32.Parse(pList[arrIndex].ProfileIndex));
            if (IsAllow) //Ajay: 25/09/2018
            {
                pList.RemoveAt(arrIndex);
                ucpr.lvProfile.Items[arrIndex].Remove();
            }
            else { }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;

            List<KeyValuePair<string, string>> pData = Utils.getKeyValueAttributes(ucpr.grpP);
            if (mode == Mode.ADD)
            {
                pList.Add(new Profile("Profile", pData));
                Utils.CreateDI4Profile(Int32.Parse(pList[pList.Count - 1].ProfileIndex));
            }
            else if (mode == Mode.EDIT)
            {
                pList[editIndex].updateAttributes(pData);
            }

            refreshList();
            //Namrata: 09/08/2017
            if (sender != null && e != null)
            {
                ucpr.grpP.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ucpr.grpP.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
            Utils.resetValues(ucpr.grpP);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucpr btnFirst_Click clicked in class!!!");
            if (ucpr.lvProfile.Items.Count <= 0) return;
            if (pList.ElementAt(0).IsNodeComment) return;
            editIndex = 0;
            loadValues();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            //Namrata:27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucpr btnPrev_Click clicked in class!!!");
            if (editIndex - 1 < 0) return;
            if (pList.ElementAt(editIndex - 1).IsNodeComment) return;
            editIndex--;
            loadValues();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //Namrata:27/7/2017
            btnDone_Click(null, null);
            Console.WriteLine("*** ucpr btnNext_Click clicked in class!!!");
            if (editIndex + 1 >= ucpr.lvProfile.Items.Count) return;
            if (pList.ElementAt(editIndex + 1).IsNodeComment) return;
            editIndex++;
            loadValues();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucpr btnLast_Click clicked in class!!!");
            if (ucpr.lvProfile.Items.Count <= 0) return;
            if (pList.ElementAt(pList.Count - 1).IsNodeComment) return;
            editIndex = pList.Count - 1;
            loadValues();
        }

        private void lvProfile_DoubleClick(object sender, EventArgs e)
        {
            if (ucpr.lvProfile.SelectedItems.Count <= 0) return;

            ListViewItem lvi = ucpr.lvProfile.SelectedItems[0];
            Utils.UncheckOthers(ucpr.lvProfile, lvi.Index);
            if (pList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ucpr.grpP.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            Utils.showNavigation(ucpr.grpP, true);
            loadValues();
            ucpr.txtAINo.Focus();
        }

        private void loadDefaults()
        {
            ucpr.txtHigh.Text = "0";
            ucpr.txtLow.Text = "0";
            ucpr.txtDelay.Text = "5";
        }

        private void loadValues()
        {
            Profile prof = pList.ElementAt(editIndex);
            if (prof != null)
            {
                ucpr.txtProfileIndex.Text = prof.ProfileIndex;
                ucpr.txtAINo.Text = prof.AINo;
                ucpr.txtHigh.Text = prof.High;
                ucpr.txtLow.Text = prof.Low;
                ucpr.txtDelay.Text = prof.DelaySec;
            }
        }

        private bool Validate()
        {
            bool status = true;

            //Check empty field's
            if (Utils.IsEmptyFields(ucpr.grpP))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check AI no.
            if (!Utils.IsGreaterThanZero(ucpr.txtAINo.Text))
            {
                MessageBox.Show("AI No. should be greater than zero.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!Utils.IsValidAI(ucpr.txtAINo.Text))
            {
                MessageBox.Show("AI No. is not a valid AI.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check atleast one, high or low...
            if (Utils.IsLessThanEqual2Zero(ucpr.txtHigh.Text) && Utils.IsLessThanEqual2Zero(ucpr.txtLow.Text))
            {
                MessageBox.Show("Either High or Low values should be specified.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check High/Low should not be < 0...
            if (Utils.IsLessThanZero(ucpr.txtHigh.Text) || Utils.IsLessThanZero(ucpr.txtLow.Text))
            {
                MessageBox.Show("High/Low cannot be less than zero.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check Delay...
            if (Utils.IsLessThanZero(ucpr.txtDelay.Text))
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
            ucpr.lvProfile.Columns.Add("No.", 40, HorizontalAlignment.Left);
            ucpr.lvProfile.Columns.Add("AI No.", 60, HorizontalAlignment.Left);
            ucpr.lvProfile.Columns.Add("High", 70, HorizontalAlignment.Left);
            ucpr.lvProfile.Columns.Add("Low", 70, HorizontalAlignment.Left);
            ucpr.lvProfile.Columns.Add("Delay(sec)", -2, HorizontalAlignment.Left);
        }

        public void refreshList()
        {
            int cnt = 0;
            ucpr.lvProfile.Items.Clear();
            
            foreach (Profile pNode in pList)
            {
                string[] row = new string[5];
                int rNo = 0;
                if (pNode.IsNodeComment)
                {
                    row[rNo] = "Comment...";
                }
                else
                {
                    row[rNo++] = pNode.ProfileIndex;
                    row[rNo++] = pNode.AINo;
                    row[rNo++] = pNode.High;
                    row[rNo++] = pNode.Low;
                    row[rNo++] = pNode.DelaySec;
                }
                ListViewItem lvItem = new ListViewItem(row);
                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucpr.lvProfile.Items.Add(lvItem);
            }
        }

        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("ProfileRecord_")) return ucpr;
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
            foreach (Profile prof in pList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(prof.exportXMLnode(), true);
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
            int oProfileNo = -1;
            int nProfileNo = -1;

            //Reset Profile no.
            Globals.resetUniqueNos(ResetUniqueNos.PR);
            Globals.ProfileNo++;//Start from 1...

            foreach (Profile pr in pList)
            {
                oProfileNo = Int32.Parse(pr.ProfileIndex);
                nProfileNo = Globals.ProfileNo++;
                pr.ProfileIndex = nProfileNo.ToString();
                //Replace in Virtual master
                foreach (VirtualMaster vm in Utils.getOpenProPlusHandle().getMasterConfiguration().getVirtualGroup().getVirtualMasters())
                {
                    foreach (IED ied in vm.getIEDs())
                    {
                        ied.changeProfileSequence(oProfileNo, nProfileNo);
                    }
                }
            }
            Utils.getOpenProPlusHandle().getMasterConfiguration().getVirtualGroup().refreshList();
            Utils.getOpenProPlusHandle().getParameterLoadConfiguration().getProfileRecord().refreshList();
        }

        public void resetReindexFlags()
        {
            foreach (Profile pr in pList)
            {
                pr.IsReindexedAINo = false;
            }
        }
        public void ChangeAISequence(int oAINo, int nAINo)
        {
            //Do not break as there can be one AI referred in multiple Profile's...
            foreach (Profile pr in pList)
            {
                if (pr.AINo == oAINo.ToString() && !pr.IsReindexedAINo)
                {
                    pr.AINo = nAINo.ToString();
                    pr.IsReindexedAINo = true;
                }
            }
            refreshList();
        }
        public void parsePRGNode(XmlNode prgNode, TreeNode tn)
        {
            //First set root node name...
            rnName = prgNode.Name;

            if (prgNode.Attributes != null)
            {
                foreach (XmlAttribute item in prgNode.Attributes)
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
            else if (prgNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = prgNode.Value;
            }
            if (tn != null) tn.Nodes.Clear();
            foreach (XmlNode node in prgNode)
            {
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                pList.Add(new Profile(node));
            }
            refreshList();
        }
        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }
        public string ProfileInterval
        {
            get {
                try
                {
                    profileInterval = Int32.Parse(ucpr.txtProfileInterval.Text);
                }
                catch (System.FormatException)
                {
                    profileInterval = -1;
                    ucpr.txtProfileInterval.Text = profileInterval.ToString();
                }
                return profileInterval.ToString();
            }
            set { profileInterval = Int32.Parse(value); ucpr.txtProfileInterval.Text = value; }
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (Profile pNode in pList)
            {
                if (pNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }

        public List<Profile> getProfiles()
        {
            return pList;
        }
    }
}
