

//            ucPLU.lvMODBUSmasterDoubleClick += new System.EventHandler(this.lvMODBUSmaster_DoubleClick);
//            //ucPLU.lvMODBUSmasterDrawItem += new DrawListViewItemEventHandler(this.lvMODBUSmaster_DrawItem);
//            //ucPLU.lvMODBUSmasterDrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvMODBUSmaster_DrawSubItem);
//            //ucPLU.lvMODBUSmasterDrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvMODBUSmaster_DrawColumnHeader);
//        }

//        private void lvMODBUSmaster_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
//        {
//            e.DrawDefault = true;
//        }
//        private void lvMODBUSmaster_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
//        {
//            e.Graphics.FillRectangle(Brushes.Black, e.Bounds);
//            e.DrawText();
//            using (StringFormat sf = new StringFormat())
//            {
//                e.DrawBackground();
//                using (Font headerFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold)) //Font size!!!!
//                {
//                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Navy, e.Bounds, sf);
//                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1), e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width - 2, e.Bounds.Height - 2);
//                }
//            }
//        }
//        private void lvMODBUSmaster_DrawItem(object sender, DrawListViewItemEventArgs e)
//        {
//            e.DrawDefault = true;
//        }
//        private void addListHeaders()
//        {
//            //ucPLU.lvMODBUSmaster.OwnerDraw = true;
//            ucPLU.lvMODBUSmaster.Columns.Add("No.", 80, HorizontalAlignment.Left);
//            ucPLU.lvMODBUSmaster.Columns.Add("Run", 80, HorizontalAlignment.Left);
//        }
//        private void refreshList()
//        {
//            int cnt = 0;
//            ucPLU.lvMODBUSmaster.Items.Clear();
//            foreach (PLUMaster sl in s104List)
//            {
//                string[] row = new string[2];
//                if (sl.IsNodeComment)
//                {
//                    row[0] = "Comment...";
//                }
//                else
//                {
//                    row[0] = sl.MasterNum;
//                    row[1] = sl.Run;
//                }
//                ListViewItem lvItem = new ListViewItem(row);
//                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
//                ucPLU.lvMODBUSmaster.Items.Add(lvItem);
//            }
//            Utils.VirtualPLU.AddRange(s104List); //Namrata: 26/10/2017
//        }
//        public Control getView(List<string> kpArr)
//        {
//            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("PLUGroup_"))
//            {
//                return ucPLU;
//            }
//            kpArr.RemoveAt(0);
//            if (kpArr.ElementAt(0).Contains("PLU_"))
//            {
//                int idx = -1;
//                string[] elems = kpArr.ElementAt(0).Split('_');
//                Utils.WriteLine(VerboseLevel.DEBUG, "$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
//                idx = Int32.Parse(elems[elems.Length - 1]);
//                if (s104List.Count <= 0) return null;
//                return s104List[idx].getView(kpArr);
//            }
//            else
//            {
//                Utils.WriteLine(VerboseLevel.WARNING, "***** View for element: {0} not supported!!!", kpArr.ElementAt(0));
//            }
//            return null;
//        }
//        public XmlNode exportXMLnode()
//        {
//            XmlDocument xmlDoc = new XmlDocument();
//            StringWriter stringWriter = new StringWriter();
//            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
//            XmlNode rootNode = xmlDoc.CreateElement(rnName);
//            xmlDoc.AppendChild(rootNode);
//            foreach (PLUMaster sn in s104List)
//            {
//                XmlNode importNode = rootNode.OwnerDocument.ImportNode(sn.exportXMLnode(), true);
//                rootNode.AppendChild(importNode);
//            }
//            return rootNode;
//        }
//        public void parseMBGNode(XmlNode mbgNode, TreeNode tn)
//        {
//            rnName = mbgNode.Name;
//            tn.Nodes.Clear();
//            IEC104GroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
//            foreach (XmlNode node in mbgNode)
//            {
//                TreeNode tmp = null;
//                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
//                s104List.Add(new PLUMaster(node, tmp));
//            }
//            refreshList();
//        }
//        public int getCount()
//        {
//            int ctr = 0;
//            foreach (PLUMaster s104Node in s104List)
//            {
//                if (s104Node.IsNodeComment) continue;
//                ctr++;
//            }
//            return ctr;
//        }
//        public List<PLUMaster> getIEC104Slaves()
//        {
//            return s104List;
//        }
//    }
//}
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
    public class PLUGroup
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "PLUGroup";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        List<PLUMaster> PLUList = new List<PLUMaster>();
        ucPLU ucnc = new ucPLU();
        private TreeNode PLUGroupTreeNode;
        public PLUGroup(TreeNode tn)
        {
            tn.Nodes.Clear();
            PLUGroupTreeNode = tn;
            ucnc.btnAddClick += new System.EventHandler(this.btnAdd_Click);
            ucnc.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
            ucnc.btnDoneClick += new System.EventHandler(this.btnDone_Click);
            ucnc.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
            ucnc.lvPLUDoubleClick += new System.EventHandler(this.lvPLU_DoubleClick);
            addListHeaders();
            //fillOptions();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Utils.WriteLine(VerboseLevel.DEBUG, "*** s104List count: {0} lv count: {1}", PLUList.Count, ucnc.lvPLU.Items.Count);
            DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                return;
            }
            for (int i = ucnc.lvPLU.Items.Count - 1; i >= 0; i--)
            {
                if (ucnc.lvPLU.Items[i].Checked)
                {
                    Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", i);
                    PLUList.RemoveAt(i);
                    ucnc.lvPLU.Items[i].Remove();
                }
            }
            Utils.WriteLine(VerboseLevel.DEBUG, "*** s104List count: {0} lv count: {1}", PLUList.Count, ucnc.lvPLU.Items.Count);
            refreshList();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Utils.IntIEC104Modbus = Utils.IntIEC104Modbus - 1;
            ucnc.grpPLU.Visible = false;
            mode = Mode.NONE;
            editIndex = -1;
        }
        private void lvPLU_DoubleClick(object sender, EventArgs e)
        {
            if (ucnc.lvPLU.SelectedItems.Count <= 0) return;
            ListViewItem lvi = ucnc.lvPLU.SelectedItems[0];
            Utils.UncheckOthers(ucnc.lvPLU, lvi.Index);
            if (PLUList.ElementAt(lvi.Index).IsNodeComment)
            {
                MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ucnc.grpPLU.Visible = true;
            mode = Mode.EDIT;
            editIndex = lvi.Index;
            Utils.showNavigation(ucnc.grpPLU, true);
            loadValues();
            
        }
        private void loadValues()
        {
            PLUMaster ni = PLUList.ElementAt(editIndex);
            if (ni != null)
            {
                ucnc.txtMasterNum.Text = ni.MasterNum;
                if (ni.Run.ToLower() == "yes") ucnc.chkRun.Checked = true;
                else ucnc.chkRun.Checked = false;
            }
        }
        private void addListHeaders()
        {
            ucnc.lvPLU.Columns.Add("No.", 60, HorizontalAlignment.Left);
            ucnc.lvPLU.Columns.Add("Run", 60, HorizontalAlignment.Left);

        }
        public List<PLUMaster> getPLUInterfaces()
        {
            return PLUList;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            mode = Mode.ADD;
            editIndex = -1;
            Utils.resetValues(ucnc.grpPLU);
            Utils.showNavigation(ucnc.grpPLU, false);
            loadDefaults();
            ucnc.txtMasterNum.Text = (Globals.MasterNo + 1).ToString();
            ucnc.grpPLU.Visible = true;
        }
        private void loadDefaults()
        {
            ucnc.chkRun.Checked = true;
        }
        public void refreshList()
        {
            int cnt = 0;
            ucnc.lvPLU.Items.Clear();
            foreach (PLUMaster ni in PLUList)
            {
                string[] row = new string[2];//Namrata:30/5/2017
                if (ni.IsNodeComment)
                {
                    row[0] = "Comment...";
                }
                else
                {
                    row[0] = ni.MasterNum;
                    row[1] = ni.Run;
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucnc.lvPLU.Items.Add(lvItem);
                }
            }
            Utils.VirtualPLU.AddRange(PLUList); //Namrata: 26/10/2017
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDone_Click";
            try
            {
                List<KeyValuePair<string, string>> niData = Utils.getKeyValueAttributes(ucnc.grpPLU);

                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    PLUList.Add(new PLUMaster("PLU", niData,tmp));
                }
                else if (mode == Mode.EDIT)
                {
                    PLUList[editIndex].updateAttributes(niData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    ucnc.grpPLU.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("PLUGroup_"))
            {
                return ucnc;
            }
            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("PLU_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Utils.WriteLine(VerboseLevel.DEBUG, "$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (PLUList.Count <= 0) return null;
                return PLUList[idx].getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "***** View for element: {0} not supported!!!", kpArr.ElementAt(0));
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
            foreach (PLUMaster sn in PLUList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(sn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public void parseMBGNode(XmlNode mbgNode, TreeNode tn)
        {
            rnName = mbgNode.Name;
            tn.Nodes.Clear();
            PLUGroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
            foreach (XmlNode node in mbgNode)
            {
                TreeNode tmp = null;
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                PLUList.Add(new PLUMaster(node, tmp));
            }
            refreshList();
        }
        public int getCount()
        {
            int ctr = 0;
            foreach (PLUMaster s104Node in PLUList)
            {
                if (s104Node.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<PLUMaster> getIEC104Slaves()
        {
            return PLUList;
        }
    }
}