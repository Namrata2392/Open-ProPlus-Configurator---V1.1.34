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
    * \brief     <b>MasterConfiguration</b> is a class to store different types of masters.
    * \details   This class stores information about various master types like IEC103, MODBUS, etc.
    * It also exports the XML node related to this object.
    * 
    */
    public class MasterConfiguration
    {
        #region Declaration
        private string rnName = "MasterConfiguration";
        IEC103Group iecGrp;
        IEC101Group iec101Grp;
        MODBUSGroup mbGrp;
        IEC61850ServerGroup server61850;
        VirtualGroup vGrp;
        ADRGroup adrgroup;
        PLUGroup plugroup;
        RCBConfiguration RCB;
        ucMasterConfiguration ucmc = new ucMasterConfiguration();
        #endregion Declaration
        public MasterConfiguration(Dictionary<string, TreeNode> treeDicts)
        {
            string strRoutineName = "MasterConfiguration";
            try
            {
                foreach (KeyValuePair<string, TreeNode> mckp in treeDicts)
                {
                    if (mckp.Key == "ADRGroup")
                    {
                        adrgroup = new ADRGroup(treeDicts["ADRGroup"]);
                    }
                    //Namrata:6/7/2017
                    if (mckp.Key == "IEC101Group")
                    {
                        iec101Grp = new IEC101Group(treeDicts["IEC101Group"]);
                    }
                    if (mckp.Key == "IEC103Group")
                    {
                        iecGrp = new IEC103Group(treeDicts["IEC103Group"]);
                    }
                    else if (mckp.Key == "MODBUSGroup")
                    {
                        mbGrp = new MODBUSGroup(treeDicts["MODBUSGroup"]);
                    }
                    else if (mckp.Key == "IEC61850ClientGroup")//61850Group
                    {
                        server61850 = new IEC61850ServerGroup(treeDicts["IEC61850ClientGroup"]); //61850Group
                    }
                    //Namrata:25/10/2017
                    else if (mckp.Key == "PLUGroup")//61850Group
                    {
                        plugroup = new PLUGroup(treeDicts["PLUGroup"]); //61850Group
                    }
                    else if (mckp.Key == "VirtualGroup")
                    {
                        vGrp = new VirtualGroup(treeDicts["VirtualGroup"]);
                    }
                    else
                    {
                        Console.WriteLine("***** MasterConfiguration: Node '{0}' not supported!!!", mckp.Key);
                    }
                }
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
            string strRoutineName = "txtTcpPortKeyPress";
            try
            {
                ucmc.lvMasterConfiguration.Columns.Add("No.", 60, HorizontalAlignment.Left);
                ucmc.lvMasterConfiguration.Columns.Add("Description", 150, HorizontalAlignment.Left);
                ucmc.lvMasterConfiguration.Columns.Add("Total", 150, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshList()
        {
            string strRoutineName = "txtTcpPortKeyPress";
            try
            {
                int rowCnt = 0;
                ucmc.lvMasterConfiguration.Items.Clear();
                string[] row1 = { "1", "ADR", adrgroup.getCount().ToString() };
                ListViewItem lvItem1 = new ListViewItem(row1);
                if (rowCnt++ % 2 == 0) lvItem1.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucmc.lvMasterConfiguration.Items.Add(lvItem1);
                string[] row2 = { "2", "IEC101", iec101Grp.getCount().ToString() };
                ListViewItem lvItem2 = new ListViewItem(row2);
                if (rowCnt++ % 2 == 0) lvItem2.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucmc.lvMasterConfiguration.Items.Add(lvItem2);
                string[] row3 = { "3", "IEC103", iecGrp.getCount().ToString() };
                ListViewItem lvItem3 = new ListViewItem(row3);
                if (rowCnt++ % 2 == 0) lvItem3.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucmc.lvMasterConfiguration.Items.Add(lvItem3);
                string[] row5 = { "4", "MODBUS", mbGrp.getCount().ToString() };
                ListViewItem lvItem5 = new ListViewItem(row5);
                if (rowCnt++ % 2 == 0) lvItem5.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucmc.lvMasterConfiguration.Items.Add(lvItem5);
                string[] row6 = { "5", "Virtual", vGrp.getCount().ToString() };
                ListViewItem lvItem6 = new ListViewItem(row6);
                if (rowCnt++ % 2 == 0) lvItem6.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                ucmc.lvMasterConfiguration.Items.Add(lvItem6);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("MasterConfiguration_")) { refreshList(); return ucmc; }

            kpArr.RemoveAt(0);
            //Namarta: 10 / 5 / 2017
            if (kpArr.ElementAt(0).Contains("ADRGroup_"))
            {
                if (adrgroup == null) return null;
                return adrgroup.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("IEC101Group_"))
            {
                if (iec101Grp == null) return null;
                return iec101Grp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("IEC103Group_"))
            {
                if (iecGrp == null) return null;
                return iecGrp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("MODBUSGroup_"))
            {
                if (mbGrp == null) return null;
                return mbGrp.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("IEC61850ClientGroup_"))// 61850Group_
            {
                if (server61850 == null) return null;
                return server61850.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("PLUGroup_"))// 61850Group_
            {
                if (plugroup == null) return null;
                return plugroup.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("VirtualGroup_"))
            {
                if (vGrp == null) return null;
                return vGrp.getView(kpArr);
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
            //Namrata:31/5/2017
            if (adrgroup != null)
            {
                XmlNode importADRMasterNode = rootNode.OwnerDocument.ImportNode(adrgroup.exportXMLnode(), true);
                rootNode.AppendChild(importADRMasterNode);
            }
            if (iec101Grp != null)
            {
                XmlNode import101Node = rootNode.OwnerDocument.ImportNode(iec101Grp.exportXMLnode(), true);
                rootNode.AppendChild(import101Node);
            }
            if (iecGrp != null)
            {
                XmlNode importIECGNode = rootNode.OwnerDocument.ImportNode(iecGrp.exportXMLnode(), true);
                rootNode.AppendChild(importIECGNode);
            }
            if (mbGrp != null)
            {
                XmlNode importMBGNode = rootNode.OwnerDocument.ImportNode(mbGrp.exportXMLnode(), true);
                rootNode.AppendChild(importMBGNode);
            }

            if (server61850 != null)
            {
                XmlNode importMBGNode = rootNode.OwnerDocument.ImportNode(server61850.exportXMLnode(), true);
                rootNode.AppendChild(importMBGNode);
                //XmlNode importRCBNode = rootNode.OwnerDocument.ImportNode(RCB.exportXMLnode(), true);
                //rootNode.AppendChild(importRCBNode);
            }
            if (vGrp != null)
            {
                XmlNode importVGNode = rootNode.OwnerDocument.ImportNode(vGrp.exportXMLnode(), true);
                rootNode.AppendChild(importVGNode);
            }
            //Namrata: 25/10/2017
            if (plugroup != null)
            {
                XmlNode importMBGNode = rootNode.OwnerDocument.ImportNode(plugroup.exportXMLnode(), true);
                rootNode.AppendChild(importMBGNode);
            }
            return rootNode;
        }
        public string exportINI(string slaveNum, string slaveID)
        {
            //string[] arrINIelements = { "AI", "EN","AO", "DI", "DO", "IED", "DeadBandAI", "DeadBandEN" };//Don't change order...
            string[] arrINIelements = { "AI", "AO", "DI", "DO", "EN", "IED", "DeadBandAI", "DeadBandEN" };//Don't change order... //Ajay: 28/12/2018
            string finalINIdata = "";
            string storeINIdata = "";
            int ctr = 1;
            for (int i = 0; i < arrINIelements.Length; i++)
            {
                string iniData = "";
                if (arrINIelements[i] != "EN" && arrINIelements[i] != "DeadBandEN") ctr = 1;//Since EN treated as AI...
                //Namrata:31/5/2017
                if (adrgroup != null)
                {
                    string tmp = adrgroup.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                }
                if (iec101Grp != null)
                {
                    string tmp = iec101Grp.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                }
                if (iecGrp != null)
                {
                    string tmp = iecGrp.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                }
                if (mbGrp != null)
                {
                    string tmp = mbGrp.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                }
                if (server61850 != null)
                {
                    string tmp = server61850.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                }
                if (vGrp != null)
                {
                    string tmp = vGrp.exportINI(slaveNum, slaveID, arrINIelements[i], ref ctr);
                    iniData += tmp;
                }
                if (arrINIelements[i] == "AI" || arrINIelements[i] == "DeadBandAI")
                {
                    storeINIdata += iniData;
                }
                else
                {
                    string postFix = "";
                    if (arrINIelements[i] == "EN") postFix = "AI";
                    else postFix = arrINIelements[i];

                    if (arrINIelements[i] != "DeadBandEN") finalINIdata += "Num" + postFix + "=" + (ctr - 1).ToString() + Environment.NewLine;
                    if (storeINIdata.Length > 0) finalINIdata += storeINIdata;
                    storeINIdata = "";
                    finalINIdata += iniData;
                }
                if (arrINIelements[i] == "IED")
                {
                    //Hardcoded entry as asked by Naina...
                    finalINIdata += "IEDClockSyncIntervalSec=10" + Environment.NewLine;
                }
            }

            return finalINIdata;
        }
        public void regenerateAOSequence()
        {
            string strRoutineName = "regenerateAOSequence";
            try
            {
                //Reset AI no.
                Globals.resetUniqueNos(ResetUniqueNos.AO);
                Globals.AONo++;
                //Handle IEC101 masters...
                iec101Grp.regenerateAOSequence();

                //Handle IEC103 masters...
                iecGrp.regenerateAOSequence();

                //Handle MODBUS masters...
                mbGrp.regenerateAOSequence();

                //server61850.regenerateAISequence();

                //Handle Virtual masters...
                vGrp.regenerateAOSequence();

                //Namrata:31/5/2017
                //Handle ADR masters...
                //adrgroup.regenerateAOSequence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateAISequence()
        {
            string strRoutineName = "regenerateAISequence";
            try
            {
                //Reset AI no.
                Globals.resetUniqueNos(ResetUniqueNos.AI);
                Globals.AINo++;
                //Handle IEC101 masters...
                iec101Grp.regenerateAISequence();
                //Handle IEC103 masters...
                iecGrp.regenerateAISequence();
                //Handle MODBUS masters...
                mbGrp.regenerateAISequence();
                server61850.regenerateAISequence();
                //Handle Virtual masters...
                vGrp.regenerateAISequence();
                //Namrata:31/5/2017
                //Handle ADR masters...
                adrgroup.regenerateAISequence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateDISequence()
        {
            string strRoutineName = "regenerateDISequence";
            try
            {
                //Reset DI no.
                Globals.resetUniqueNos(ResetUniqueNos.DI);
                Globals.DINo++;//Start from 1...

                //Handle IEC101 masters...
                iec101Grp.regenerateDISequence();

                //Handle IEC103 masters...
                iecGrp.regenerateDISequence();

                //Handle MODBUS masters...
                mbGrp.regenerateDISequence();

                //Handle IEC61850 masters...
                server61850.regenerateDISequence();

                //Handle Virtual masters...
                vGrp.regenerateDISequence();

                //Namrata:31/5/2017
                //Handle ADR masters...
                adrgroup.regenerateDISequence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void regenerateDOSequence()
        {
            string strRoutineName = "regenerateDOSequence";
            try
            {
                //Reset DO no.
                Globals.resetUniqueNos(ResetUniqueNos.DO);
                Globals.DONo++;//Start from 1...
                               //Globals.DOIndex++;//Start from 1...
                               //Globals.DOReportingIndex++;//Start from 1...
                               //Handle IEC101 masters...
                iec101Grp.regenerateDOSequence();
                //Handle IEC103 masters...
                iecGrp.regenerateDOSequence();
                //Handle MODBUS masters...
                mbGrp.regenerateDOSequence();
                server61850.regenerateDOSequence();
                //Handle Virtual masters...
                vGrp.regenerateDOSequence();
                //Namrata:31/5/2017
                //Handle ADR masters...
                adrgroup.regenerateDOSequence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void regenerateENSequence()
        {
            string strRoutineName = "regenerateENSequence";
            try
            {
                //Reset EN no.
                Globals.resetUniqueNos(ResetUniqueNos.EN);
                Globals.ENNo++;//Start from 1...
                               //Globals.ENIndex++;//Start from 1...
                               //Globals.ENReportingIndex++;//Start from 1...
                               //Handle IEC101 masters...
                iec101Grp.regenerateENSequence();
                //Handle IEC103 masters...
                iecGrp.regenerateENSequence();
                //Handle MODBUS masters...
                mbGrp.regenerateENSequence();
                server61850.regenerateENSequence();
                //Handle Virtual masters...
                vGrp.regenerateENSequence();
                //Namrata:31/5/2017
                //Handle ADR masters...
                adrgroup.regenerateENSequence();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void parseMCNode(XmlNode mNode, Dictionary<string, TreeNode> treeDicts)
        {
            string strRoutineName = "parseMCNode";
            try
            {
                //First set root node name...
                rnName = mNode.Name;

                //tn.Nodes.Clear();
                foreach (XmlNode node in mNode)
                {
                    //Namrata:31/5/2017
                    if (node.Name == "ADRGroup")
                    {
                        adrgroup.parseIECGNode(node, treeDicts["ADRGroup"]);
                    }
                    if (node.Name == "IEC101Group")
                    {
                        iec101Grp.parseIECGNode(node, treeDicts["IEC101Group"]);
                    }
                    else if (node.Name == "IEC103Group")
                    {
                        iecGrp.parseIECGNode(node, treeDicts["IEC103Group"]);
                    }
                    else if (node.Name == "MODBUSGroup")
                    {
                        mbGrp.parseMBGNode(node, treeDicts["MODBUSGroup"]);
                    }
                    else if (node.Name == "IEC61850ClientGroup")
                    {
                        server61850.parseMBGNode(node, treeDicts["IEC61850ClientGroup"]); //61850Group
                    }
                    else if (node.Name == "PLUGroup")
                    {
                        plugroup.parseMBGNode(node, treeDicts["PLUGroup"]); //61850Group
                    }
                    else if (node.Name == "VirtualGroup")
                    {
                        vGrp.parseVGNode(node/*, treeDicts["VirtualGroup"] */);//TreeNode not needed as already sent...
                    }
                    else
                    {
                        Console.WriteLine("***** MasterConfiguration: Node '{0}' not supported!!!", node.Name);
                    }
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public IEC103Group getIEC103Group()
        {
            return iecGrp;
        }
        public IEC101Group getIEC101Group()
        {
            return iec101Grp;
        }
        //Namarta:31/5/2017
        public ADRGroup getADRMasterGroup()
        {
            return adrgroup;
        }
        public MODBUSGroup getMODBUSGroup()
        {
            return mbGrp;
        }
        public VirtualGroup getVirtualGroup()
        {
            return vGrp;
        }
        public IEC61850ServerGroup get61850Group()
        {
            return server61850;
        }
    }
}
