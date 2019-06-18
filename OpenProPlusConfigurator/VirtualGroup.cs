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
    * \brief     <b>VirtualGroup</b> is a class to store all the VirtualMaster's
    * \details   This class stores info related to all VirtualMaster's. It allows
    * user to add multiple VirtualMaster's. It also exports the XML node related to this object.
    * 
    */
    public class VirtualGroup
    {
        private string rnName = "VirtualGroup";
        List<VirtualMaster> vmList = new List<VirtualMaster>();
        ucGroupVirtual ucvm = new ucGroupVirtual();
        private TreeNode VirtualGroupTreeNode;
        public VirtualGroup(TreeNode tn)
        {
            tn.Nodes.Clear();
            VirtualGroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
            //Special case, add only single virtual node...
            TreeNode tmp = tn.Nodes.Add("Virtual_" + Utils.GenerateShortUniqueKey(), "Virtual", "VirtualMaster", "VirtualMaster");
            vmList.Add(new VirtualMaster(vmList.Count + 1, tmp));
            Utils.WriteLine(VerboseLevel.DEBUG, "*** boooooom VirtualGroup: vmList count: {0}", vmList.Count);
            addListHeaders();
            refreshList();
        }
        private void addListHeaders()
        {
            ucvm.lvVirtualMaster.Columns.Add("No.", 60, HorizontalAlignment.Left);
            ucvm.lvVirtualMaster.Columns.Add("Debug",100, HorizontalAlignment.Left);
        }
        public void refreshList()
        {
            int cnt = 0;
            ucvm.lvVirtualMaster.Items.Clear();
            foreach (VirtualMaster mt in vmList)
            {
                if (mt.IsNodeComment) continue;
                string[] row = { mt.MasterNum, mt.DEBUG };
                ListViewItem lvItem = new ListViewItem(row);
                if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucvm.lvVirtualMaster.Items.Add(lvItem);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("VirtualGroup_")) { refreshList(); return ucvm; }
            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("Virtual_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Utils.WriteLine(VerboseLevel.DEBUG, "$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length-1]);
                if (vmList.Count <= 0) return null;
                return vmList[idx].getView(kpArr);
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
            foreach (VirtualMaster mn in vmList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(mn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            Utils.WriteLine(VerboseLevel.DEBUG, "*** boooooom exportXMLNode: vmList count: {0}", vmList.Count);
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID, string element, ref int ctr)
        {
            string iniData = "";
            foreach (VirtualMaster vm in vmList)
            {
                iniData += vm.exportINI(slaveNum, slaveID, element, ref ctr);
            }
            return iniData;
        }
        public void regenerateAISequence()
        {
            foreach (VirtualMaster vm in vmList)
            {
                foreach (IED ied in vm.getIEDs())
                {
                    ied.regenerateAISequence();
                }
            }
        }
        public void regenerateAOSequence()
        {
            foreach (VirtualMaster vm in vmList)
            {
                foreach (IED ied in vm.getIEDs())
                {
                    ied.regenerateAOSequence();
                }
            }
        }
        public void regenerateDISequence()
        {
            foreach (VirtualMaster vm in vmList)
            {
                foreach (IED ied in vm.getIEDs())
                {
                    ied.regenerateDISequence();
                }
            }
        }
        public void regenerateDOSequence()
        {
            foreach (VirtualMaster vm in vmList)
            {
                foreach (IED ied in vm.getIEDs())
                {
                    ied.regenerateDOSequence();
                }
            }
        }
        public void regenerateENSequence()
        {
            foreach (VirtualMaster vm in vmList)
            {
                foreach (IED ied in vm.getIEDs())
                {
                    ied.regenerateENSequence();
                }
            }
        }
        public void parseVGNode(XmlNode vgNode/*, TreeNode tn*/)
        {
            rnName = vgNode.Name;//First set root node name...
            foreach (XmlNode node in vgNode)
            {
                if (vmList.Count == 1)//IMP: Since only one node should be there for 'VirtualMaster'
                {
                    vmList[0].parseVMNode(node);//Already added...
                }
                else
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    TreeNode tmp = VirtualGroupTreeNode.Nodes.Add("Virtual_" + Utils.GenerateShortUniqueKey(), "Virtual", "Virtual", "Virtual");
                    vmList.Add(new VirtualMaster(node, tmp));
                }
                Utils.WriteLine(VerboseLevel.DEBUG, "*** boooooom parseVGNode: vmList count: {0}", vmList.Count);
            }
            refreshList();
        }

        public int getCount()
        {
            int ctr = 0;
            foreach (VirtualMaster vmNode in vmList)
            {
                if (vmNode.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<VirtualMaster> getVirtualMasters()
        {
            return vmList;
        }
        public List<VirtualMaster> getVirtualMastersByFilter(string masterID)
        {
            List<VirtualMaster> mList = new List<VirtualMaster>();
            if (masterID.ToLower() == "all") return vmList;
            else
                foreach (VirtualMaster m in vmList)
                {
                    if (m.getMasterID == masterID)
                    {
                        mList.Add(m);
                        break;
                    }
                }
            return mList;
        }
        public List<IED> getVirtualIEDsByFilter(string masterID)
        {
            List<IED> iLst = new List<IED>();

            if (masterID.ToLower() == "all")
            {
                foreach (VirtualMaster vm in vmList)
                {
                    foreach (IED ied in vm.getIEDs())
                    {
                        iLst.Add(ied);
                    }
                }
            }
            else
            {
                foreach (VirtualMaster vm in vmList)
                {
                    if (vm.getMasterID == masterID)
                    {
                        foreach (IED ied in vm.getIEDs())
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
