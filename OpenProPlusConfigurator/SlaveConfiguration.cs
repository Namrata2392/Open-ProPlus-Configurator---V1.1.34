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
    * \brief     <b>SlaveConfiguration</b> is a class to store different types of slaves.
    * \details   This class stores information about various slave types like IEC104, MODBUS Slave, etc.
    * It also exports the XML node related to this object.
    * 
    */
    public class SlaveConfiguration
    {
        private string rnName = "SlaveConfiguration";
        private IEC104Group iec104Grp = new IEC104Group();
        private MODBUSSlaveGroup mbSlaveGrp = new MODBUSSlaveGroup();
        private IEC101SlaveGroup iec101Grp = new IEC101SlaveGroup();
        private IEC61850ServerSlaveGroup server61850Slave = new IEC61850ServerSlaveGroup();
        ucSlaveConfiguration ucsc = new ucSlaveConfiguration();

        public SlaveConfiguration()
        {
            string strRoutineName = "SlaveConfiguration";
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

        private void addListHeaders()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                ucsc.lvSlaveConfiguration.Columns.Add("No.", 60, HorizontalAlignment.Left);
                ucsc.lvSlaveConfiguration.Columns.Add("Description", 150, HorizontalAlignment.Left);
                ucsc.lvSlaveConfiguration.Columns.Add("Total", 100, HorizontalAlignment.Left);
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
                int cnt;
                int rowCnt = 0;
                //Slave Configuration...
                cnt = 0;
                ucsc.lvSlaveConfiguration.Items.Clear();
                string[] row1 = { "1", "IEC104", iec104Grp.getCount().ToString() };
                ListViewItem lvItem1 = new ListViewItem(row1);
                if (rowCnt++ % 2 == 0) lvItem1.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucsc.lvSlaveConfiguration.Items.Add(lvItem1);
                string[] row2 = { "2", "MODBUS", mbSlaveGrp.getCount().ToString() };
                ListViewItem lvItem2 = new ListViewItem(row2);
                if (rowCnt++ % 2 == 0) lvItem2.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucsc.lvSlaveConfiguration.Items.Add(lvItem2);
                string[] row3 = { "3", "IEC101", iec101Grp.getCount().ToString() };
                ListViewItem lvItem3 = new ListViewItem(row3);
                if (rowCnt++ % 2 == 0) lvItem3.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucsc.lvSlaveConfiguration.Items.Add(lvItem3);
                string[] row4 = { "4", "IEC61850 Server", server61850Slave.getCount().ToString() };
                ListViewItem lvItem4 = new ListViewItem(row4);
                if (rowCnt++ % 2 == 0) lvItem4.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucsc.lvSlaveConfiguration.Items.Add(lvItem4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void parseSCNode(XmlNode sNode, Dictionary<string, TreeNode> treeDicts)
        {
            string strRoutineName = "parseSCNode";
            try
            {
                //First set root node name...
                rnName = sNode.Name;
                //tn.Nodes.Clear();
                foreach (XmlNode node in sNode)
                {
                    if (node.Name == "IEC104Group")
                    {
                        iec104Grp.parseIECGNode(node, treeDicts["IEC104Group"]);
                    }
                    else if (node.Name == "MODBUSSlaveGroup")
                    {
                        mbSlaveGrp.parseMBSGNode(node, treeDicts["MODBUSSlaveGroup"]);
                    }
                    else if (node.Name == "IEC101SlaveGroup")
                    {
                        iec101Grp.parseIECGNode(node, treeDicts["IEC101SlaveGroup"]);
                    }
                    else if (node.Name == "IEC61850ServerGroup") //IEC61850ServerGroup
                    {
                        server61850Slave.parse61850ServerSlaveGNode(node, treeDicts["IEC61850ServerGroup"]);//IEC61850ServerSlaveGroup
                    }
                    else
                    {
                        Console.WriteLine("***** SlaveConfiguration: Node '{0}' not supported!!!", node.Name);
                    }
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("SlaveConfiguration_")) { refreshList(); return ucsc; }

            kpArr.RemoveAt(0);

            if (kpArr.ElementAt(0).Contains("IEC104Group_"))
            {
                if (iec104Grp == null) return null;
                return iec104Grp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("MODBUSSlaveGroup_"))
            {
                if (mbSlaveGrp == null) return null;
                return mbSlaveGrp.getView(kpArr);
            }

            else if (kpArr.ElementAt(0).Contains("IEC101SlaveGroup_"))
            {
                if (iec101Grp == null) return null;
                return iec101Grp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("IEC61850ServerGroup_"))//IEC61850ServerSlaveGroup
            {
                if (server61850Slave == null) return null;
                return server61850Slave.getView(kpArr);
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
            //Maintain Sequence Of XML
            if (iec104Grp != null)
            {
                XmlNode importIECGNode = rootNode.OwnerDocument.ImportNode(iec104Grp.exportXMLnode(), true);
                rootNode.AppendChild(importIECGNode);
            }

            if (mbSlaveGrp != null)
            {
                XmlNode importMBSGNode = rootNode.OwnerDocument.ImportNode(mbSlaveGrp.exportXMLnode(), true);
                rootNode.AppendChild(importMBSGNode);
            }
            //Namrata:7/7/2017
            if (iec101Grp != null)
            {
                XmlNode importIECGNode = rootNode.OwnerDocument.ImportNode(iec101Grp.exportXMLnode(), true);
                rootNode.AppendChild(importIECGNode);
            }
            //Namrata:7/7/2017
            if (server61850Slave != null)
            {
                XmlNode importIECGNode = rootNode.OwnerDocument.ImportNode(server61850Slave.exportXMLnode(), true);
                rootNode.AppendChild(importIECGNode);
            }
            return rootNode;
        }
        public IEC104Group getIEC104Group()
        {
            return iec104Grp;
        }

        public IEC101SlaveGroup getIEC101SlaveGroup()
        {
            return iec101Grp;
        }
        public MODBUSSlaveGroup getMODBUSSlaveGroup()
        {
            return mbSlaveGrp;
        }
        public IEC61850ServerSlaveGroup get61850SlaveGroup()
        {
            return server61850Slave;
        }
    }
}
