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
    * \brief     <b>
    Master</b> is a class to store IED & Virtual master parameters.
    * \details   This class stores info for IED & Virtual master parameters. It supports
    * only single IED. It also exports the XML node related to this object.
    * 
    */
    public class VirtualMaster
    {
        enum masterType
        {
            Virtual
        };
        private bool isNodeComment = false;
        private string comment = "";
        private masterType mType = masterType.Virtual;
        private int masterNum = -1;
        private int debug = 3;
        List<IED> iedList = new List<IED>();
        ucMasterVirtual ucvm = new ucMasterVirtual();
        private TreeNode VirtualMasterTreeNode;
        private string[] arrAttributes = { "MasterNum", "DEBUG" };

        public VirtualMaster(int mn, TreeNode tn)
        {
            VirtualMasterTreeNode = tn;//Save local copy so we can use it later...
            addListHeaders();
            fillOptions();
            MasterNum = mn.ToString();
            DEBUG = (debug).ToString();

            tn.Nodes.Clear();
            //Special case, add only single IED node...
            TreeNode tmp = tn.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
            iedList.Add(new IED(iedList.Count, "Virtual_IED", tmp, MasterTypes.Virtual, Int32.Parse(MasterNum)));

            refreshList();
            return;
        }

        public VirtualMaster(XmlNode mNode, TreeNode tn)
        {
            VirtualMasterTreeNode = tn;//Save local copy so we can use it later...
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

            tn.Nodes.Clear();
            foreach (XmlNode node in mNode)
            {
                //Utils.WriteLine("***** node type: {0}", node.NodeType);
                if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                TreeNode tmp = tn.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                iedList.Add(new IED(node, tmp, MasterTypes.Virtual, Int32.Parse(MasterNum), false));
            }
            refreshList();
        }

        public void parseVMNode(XmlNode vmNode)
        {
            //Parse n store values...
            Utils.WriteLine(VerboseLevel.DEBUG, "vmNode name: '{0}'", vmNode.Name);
            if (vmNode.Attributes != null)
            {
                //First set the root element value...
                try
                {
                    mType = (masterType)Enum.Parse(typeof(masterType), vmNode.Name);
                }
                catch (System.ArgumentException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Enum argument {0} not supported!!!", vmNode.Name);
                }

                foreach (XmlAttribute item in vmNode.Attributes)
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
            else if (vmNode.NodeType == XmlNodeType.Comment)
            {
                isNodeComment = true;
                comment = vmNode.Value;
            }

            foreach (XmlNode node in vmNode)
            {
                //Utils.WriteLine("***** node type: {0}", node.NodeType);
                if (iedList.Count == 1)//IMP: Since only one node should be there for 'IED'
                {
                    //Already added...
                    iedList[0].parseIEDNode(node);
                }
                else
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    TreeNode tmp = VirtualMasterTreeNode.Nodes.Add("IED_" + Utils.GenerateShortUniqueKey(), "IED", "IED", "IED");
                    iedList.Add(new IED(node, tmp, MasterTypes.Virtual, Int32.Parse(MasterNum), false));
                }
            }
            refreshList();
        }

        private void fillOptions()
        {
            //Fill Debug level...
            ucvm.cmbDebug.Items.Clear();
            for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
            {
                ucvm.cmbDebug.Items.Add(i.ToString());
            }
            ucvm.cmbDebug.SelectedIndex = 0;
        }

        private void addListHeaders()
        {
            ucvm.lvIEDList.Columns.Add("No.", 40, HorizontalAlignment.Left);
            ucvm.lvIEDList.Columns.Add("Device", 110, HorizontalAlignment.Left);
            ucvm.lvIEDList.Columns.Add("Description",150, HorizontalAlignment.Left);
        }

        private void refreshList()
        {
            int cnt = 0;
            ucvm.lvIEDList.Items.Clear();
            //addListHeaders();

            foreach (IED ied in iedList)
            {
                if (ied.IsNodeComment) continue;

                string[] row = { ied.UnitID, ied.Device, ied.Description };
                ListViewItem lvItem = new ListViewItem(row);
                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucvm.lvIEDList.Items.Add(lvItem);
            }
        }

        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("Virtual_")) { refreshList(); return ucvm; }

            kpArr.RemoveAt(0);
            Utils.WriteLine(VerboseLevel.DEBUG, "$$$$$ elem: {0}", kpArr.ElementAt(0));
            if (kpArr.ElementAt(0).Contains("IED_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Utils.WriteLine(VerboseLevel.DEBUG, "$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length-1]);
                if (iedList.Count <= 0) return null;
                return iedList[idx].getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "***** VirtualMaster: View for element: {0} not supported!!!", kpArr.ElementAt(0));
            }

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

        public string getMasterID
        {
            get { return "Virtual_" + MasterNum; }
        }

        public bool IsNodeComment
        {
            get { return isNodeComment; }
        }

        public string MasterNum
        {
            get {
                try
                {
                    masterNum = Int32.Parse(ucvm.txtMasterNo.Text);
                }
                catch (System.FormatException)
                {
                    masterNum = -1;
                    ucvm.txtMasterNo.Text = masterNum.ToString();
                }
                return masterNum.ToString();
            }
            set { masterNum = Int32.Parse(value); ucvm.txtMasterNo.Text = value; }
        }

        public string DEBUG
        {
            get { debug = Int32.Parse(ucvm.cmbDebug.Text); return debug.ToString(); }
            set { debug = Int32.Parse(value); ucvm.cmbDebug.SelectedIndex = ucvm.cmbDebug.FindStringExact(value); }
        }
    }
}
