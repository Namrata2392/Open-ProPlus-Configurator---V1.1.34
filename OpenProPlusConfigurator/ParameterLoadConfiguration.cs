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
    * \brief     <b>ParameterLoadConfiguration</b> is a class to store different types of conditional parameters.
    * \details   This class store different types of conditional parameters. It also exports the XML node related to this object.
    * 
    */
    public class ParameterLoadConfiguration
    {
        private string rnName = "ParameterLoadConfiguration";
        ClosedLoopAction claGrp = new ClosedLoopAction();
        ProfileRecord prGrp = new ProfileRecord();
        MDCalculation mdcGrp = new MDCalculation();
        DerivedParam dpGrp = new DerivedParam();
        DerivedDI ddGrp = new DerivedDI();

        ucParameterLoadConfiguration ucplc = new ucParameterLoadConfiguration();

        public ParameterLoadConfiguration()
        {
            addListHeaders();
            refreshList();
        }

        private void addListHeaders()
        {
            ucplc.lvParameterLoadConfiguration.Columns.Add("No.", 60, HorizontalAlignment.Left);
            ucplc.lvParameterLoadConfiguration.Columns.Add("Description", 170, HorizontalAlignment.Left);
            ucplc.lvParameterLoadConfiguration.Columns.Add("Total",100, HorizontalAlignment.Left);
            //ucplc.lvParameterLoadConfiguration.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void refreshList()
        {
            int cnt;
            int rowCnt = 0;

            //Parameter Load Configuration...
            cnt = 0;
            ucplc.lvParameterLoadConfiguration.Items.Clear();
            //addListHeaders();

            string[] row1 = { "1", "Closed Loop Action", claGrp.getCount().ToString() };
            ListViewItem lvItem1 = new ListViewItem(row1);
            if (rowCnt++ % 2 == 0) lvItem1.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
            ucplc.lvParameterLoadConfiguration.Items.Add(lvItem1);
            
            string[] row2 = { "2", "Profile Record", prGrp.getCount().ToString() };
            ListViewItem lvItem2 = new ListViewItem(row2);
            if (rowCnt++ % 2 == 0) lvItem2.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
            ucplc.lvParameterLoadConfiguration.Items.Add(lvItem2);
            
            string[] row3 = { "3", "MD Calculation", mdcGrp.getCount().ToString() };
            ListViewItem lvItem3 = new ListViewItem(row3);
            if (rowCnt++ % 2 == 0) lvItem3.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
            ucplc.lvParameterLoadConfiguration.Items.Add(lvItem3);

            string[] row4 = { "4", "Derived Parameters", dpGrp.getCount().ToString() };
            ListViewItem lvItem4 = new ListViewItem(row4);
            if (rowCnt++ % 2 == 0) lvItem4.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
            ucplc.lvParameterLoadConfiguration.Items.Add(lvItem4);

            string[] row5 = { "5", "Derived DI", ddGrp.getCount().ToString() };
            ListViewItem lvItem5 = new ListViewItem(row5);
            if (rowCnt++ % 2 == 0) lvItem5.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
            ucplc.lvParameterLoadConfiguration.Items.Add(lvItem5);
        }

        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("ParameterLoadConfiguration_")) { refreshList(); return ucplc; }

            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("ClosedLoopAction_"))
            {
                if (claGrp == null) return null;
                return claGrp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("ProfileRecord_"))
            {
                if (prGrp == null) return null;
                return prGrp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("DerivedParam_"))
            {
                if (dpGrp == null) return null;
                return dpGrp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("DerivedDI_"))
            {
                if (ddGrp == null) return null;
                return ddGrp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("MDCalculation_"))
            {
                if (mdcGrp == null) return null;
                return mdcGrp.getView(kpArr);
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
            if (claGrp != null)
            {
                XmlNode importCLAGNode = rootNode.OwnerDocument.ImportNode(claGrp.exportXMLnode(), true);
                rootNode.AppendChild(importCLAGNode);
            }
            if (prGrp != null)
            {
                XmlNode importPRGNode = rootNode.OwnerDocument.ImportNode(prGrp.exportXMLnode(), true);
                rootNode.AppendChild(importPRGNode);
            }

            if (dpGrp != null)
            {
                XmlNode importDPNode = rootNode.OwnerDocument.ImportNode(dpGrp.exportXMLnode(), true);
                rootNode.AppendChild(importDPNode);
            }

            if (ddGrp != null)
            {
                XmlNode importDDNode = rootNode.OwnerDocument.ImportNode(ddGrp.exportXMLnode(), true);
                rootNode.AppendChild(importDDNode);
            }

            if (mdcGrp != null)
            {
                XmlNode importMDCNode = rootNode.OwnerDocument.ImportNode(mdcGrp.exportXMLnode(), true);
                rootNode.AppendChild(importMDCNode);
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

        public string exportINI(string slaveNum, string slaveID)
        {
            string finalINIdata = "";
            int ctr = 1;

            string iniData = "";
            string element = "ClosedLoopAction";
            if (claGrp != null)
            {
                string tmp = claGrp.exportINI(slaveNum, slaveID, element, ref ctr);
                iniData += tmp;
            }

            finalINIdata += "Num" + element + "=" + (ctr - 1).ToString() + Environment.NewLine;
            finalINIdata += iniData;

            return finalINIdata;
        }

        public void ChangeAISequence(int oAINo, int nAINo)
        {
            if (oAINo == nAINo) return;
            claGrp.ChangeAISequence(oAINo, nAINo);
            prGrp.ChangeAISequence(oAINo, nAINo);
            mdcGrp.ChangeAISequence(oAINo, nAINo);
            dpGrp.ChangeAISequence(oAINo, nAINo);
        }

        public void ChangeDISequence(int oDINo, int nDINo)
        {
            if (oDINo == nDINo) return;
            ddGrp.ChangeDISequence(oDINo, nDINo);
        }

        public void ChangeDOSequence(int oDONo, int nDONo)
        {
            if (oDONo == nDONo) return;
            claGrp.ChangeDOSequence(oDONo, nDONo);
        }

        public void ChangeENSequence(int oENNo, int nENNo)
        {
            if (oENNo == nENNo) return;
            //We can add logic here for any EN No. in PLC nodes...
        }

        public void resetReindexFlags()
        {
            claGrp.resetReindexFlags();
            prGrp.resetReindexFlags();
            mdcGrp.resetReindexFlags();
            dpGrp.resetReindexFlags();
            ddGrp.resetReindexFlags();
        }

        public void parsePLCNode(XmlNode plcNode, Dictionary<string, TreeNode> treeDicts)
        {
            //First set root node name...
            rnName = plcNode.Name;

            //tn.Nodes.Clear();
            foreach (XmlNode node in plcNode)
            {
                //Utils.WriteLine("***** node type: {0}", node.NodeType);
                if (node.Name == "ClosedLoopAction")
                {
                    //TreeNode tmp = tn.Nodes.Add("ClosedLoopAction", "Closed Loop Action", "ClosedLoopAction", "ClosedLoopAction");
                    claGrp.parseCLAGNode(node, treeDicts["ClosedLoopAction"]);
                }
                else if (node.Name == "ProfileRecord")
                {
                    //TreeNode tmp = tn.Nodes.Add("ProfileRecord", "Profile Record", "ProfileRecord", "ProfileRecord");
                    prGrp.parsePRGNode(node, treeDicts["ProfileRecord"]);
                }
                else if (node.Name == "DerivedParam")
                {
                    //TreeNode tmp = tn.Nodes.Add("DerivedParam", "Derived Parameter", "DerivedParam", "DerivedParam");
                    dpGrp.parseDPGNode(node, treeDicts["DerivedParam"]);
                }
                else if (node.Name == "DerivedDI")
                {
                    //TreeNode tmp = tn.Nodes.Add("DerivedDI", "Derived DI", "DerivedDI", "DerivedDI");
                    ddGrp.parseDDGNode(node, treeDicts["DerivedDI"]);
                }
                else if (node.Name == "MDCalculation")
                {
                    //TreeNode tmp = tn.Nodes.Add("MDCalculation", "MD Calculation", "MDCalculation", "MDCalculation");
                    mdcGrp.parseMDCGNode(node, treeDicts["MDCalculation"]);
                }
                else
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "***** ParameterLoadConfiguration: Node '{0}' not supported!!!", node.Name);
                }
            }

            refreshList();
        }

        public ClosedLoopAction getClosedLoopAction()
        {
            return claGrp;
        }

        public ProfileRecord getProfileRecord()
        {
            return prGrp;
        }

        public MDCalculation getMDCalculation()
        {
            return mdcGrp;
        }

        public DerivedParam getDerivedParam()
        {
            return dpGrp;
        }

        public DerivedDI getDerivedDI()
        {
            return ddGrp;
        }
    }
}
