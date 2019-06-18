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
    * \brief     <b>SystemConfiguration</b> is a class to store different types of system configurations.
    * \details   This class stores information about different types of system configurations.
    * It also exports the XML node related to this object.
    * 
    */
    public class SystemConfiguration
    {
        private string rnName = "SystemConfiguration";
        SystemConfig mNode = new SystemConfig();
        ucSystemConfiguration ucsc = new ucSystemConfiguration();
        public SystemConfiguration()
        {
            string strRoutineName = "SystemConfiguration";
            try
            {
                addListHeaders();
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            //string strRoutineName = "SystemConfiguration";
            //try
            //{
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("SystemConfiguration_")) { refreshList(); return ucsc; }
            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("SystemConfig_"))
            {
                if (mNode == null) return null;
                return mNode.getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "***** SystemConfiguration: Node key '{0}' not supported for view!!!", kpArr.ElementAt(0));
            }
            return null;
        }
        private void addListHeaders()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                ucsc.lvSConfiguration.Columns.Add("No.", 100, HorizontalAlignment.Left);
                ucsc.lvSConfiguration.Columns.Add("Description", 250, HorizontalAlignment.Left);
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
                ucsc.lvSConfiguration.Items.Clear();
                if (mNode != null)
                {
                    string[] row = { "1", "System Config" };
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucsc.lvSConfiguration.Items.Add(lvItem);
                }
                Utils.WriteLine(VerboseLevel.DEBUG, "########## ucsc row count: {0}", ucsc.lvSConfiguration.Items.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void parseSCNode(XmlNode scNode, Dictionary<string, TreeNode> treeDicts)
        {
            string strRoutineName = "parseSCNode";
            try
            {
                //First set root node name...
                rnName = scNode.Name;
                //tn.Nodes.Clear();
                foreach (XmlNode node in scNode)
                {
                    //Utils.WriteLine("***** node type: {0}", node.NodeType);
                    if (node.Name == "SystemConfig")
                    {
                        mNode.parseSystemConfigNode(node);//FIXME: Try to use string to generate object.
                    }
                    else
                    {
                        Utils.WriteLine(VerboseLevel.DEBUG, "***** SystemConfiguration->{0} not supported", node.Name);
                    }
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void parseSCNodeToCheckDeviceExist(XmlNode scNode, Dictionary<string, TreeNode> treeDicts)
        {
            string strRoutineName = "parseSCNodeToCheckDeviceExist";
            try
            {
                //First set root node name...
                rnName = scNode.Name;
                //tn.Nodes.Clear();
                foreach (XmlNode node in scNode)
                {
                    //Utils.WriteLine("***** node type: {0}", node.NodeType);
                    if (node.Name == "SystemConfig")
                    {
                        mNode.parseSystemConfigNodeForprimarySecDevice(node);//FIXME: Try to use string to generate object.
                    }
                    else
                    {
                        Utils.WriteLine(VerboseLevel.DEBUG, "***** SystemConfiguration->{0} not supported", node.Name);
                    }
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
            if (mNode != null)
            {
                XmlNode importMCNode = rootNode.OwnerDocument.ImportNode(mNode.exportXMLnode(), true);
                rootNode.AppendChild(importMCNode);
            }
            return rootNode;
        }
    }
}
