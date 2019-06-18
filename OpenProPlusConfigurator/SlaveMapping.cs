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
    * \brief     <b>SlaveMapping</b> is a class to store mapping data for different types of slaves.
    * \details   This class stores mapping data for different types of slaves like IEC104, MODBUS Slave, etc.
    * It also exports the XML node related to this object.
    * 
    */
    public class SlaveMapping
    {
        private string rnName = "SlaveMapping";
        AIConfiguration aicNode;
        AOConfiguration aocNode;
        DIConfiguration dicNode;
        DOConfiguration docNode;
        ENConfiguration encNode;
        public SlaveMapping(AIConfiguration aic, AOConfiguration aoc,DIConfiguration dic, DOConfiguration doc, ENConfiguration enc)
        {
            string strRoutineName = "SlaveMapping";
            try
            {
                aicNode = aic;
                aocNode = aoc;
                dicNode = dic;
                docNode = doc;
                encNode = enc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool slaveExist(XmlNode chkSlave)
        {

            if (chkSlave == null || chkSlave.Attributes == null) return false;

            XmlAttribute item = chkSlave.Attributes[0];//Get 'SlaveNum'

            string slaveID = chkSlave.Name + "_" + item.Value;

            if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC104)
            {
                //Loop thru slaves... 
                foreach (IEC104Slave slv104 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC104Group().getIEC104Slaves())
                {
                    if (slaveID == "IEC104_" + slv104.SlaveNum) return true;
                }
            }
            else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.MODBUSSLAVE)
            {
                //Loop thru slaves...
                foreach (MODBUSSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())
                {
                    if (slaveID == "MODBUSSlave_" + slvMB.SlaveNum) return true;
                }
            }
            else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC101SLAVE)
            {
                //Loop thru slaves...
                foreach (IEC101Slave slv101 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
                {
                    if (slaveID == "IEC101Slave_" + slv101.SlaveNum) return true;
                }
            }
            else if (Utils.getSlaveTypes(slaveID) == SlaveTypes.IEC61850Server)
            {
                //Loop thru slaves...
                foreach (IEC61850ServerSlave slv101 in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())
                {
                    if (slaveID == "IEC61850Server_" + slv101.SlaveNum) return true;
                }
            }
            Console.WriteLine("*** Danger!!! slave {0} doesnot exist. Ignoring it's mapping.", slaveID);
            return false;
        }

        public void parseSMNode(XmlNode smNode)
        {
            string strRoutineName = "parseSMNode";
            try
            {
                foreach (XmlNode node in smNode)       //We get 'IEC104'/'MODBUSSlave' slave map's
                {
                    if (!slaveExist(node)) continue;  //Since slave entry is missing, we will ignore it's value in SlaveMapping...
                    if (node.Attributes != null)
                    {
                        XmlAttribute item = node.Attributes[0];  //Get 'SlaveNum'
                        string slaveID = node.Name + "_" + item.Value;  //Ex. 'IEC104_1'
                        foreach (XmlNode mapNode in node)
                        {
                            if (mapNode.Name == "AIMap")
                            {
                                aicNode.parseAIMNode(item.Value, slaveID, mapNode);
                            }
                            else if (mapNode.Name == "AOMap")
                            {
                                aocNode.parseAIMNode(item.Value, slaveID, mapNode);
                            }
                            else if (mapNode.Name == "DIMap")
                            {
                                dicNode.parseDIMNode(item.Value, slaveID, mapNode);
                            }
                            else if (mapNode.Name == "DOMap")
                            {
                                docNode.parseDOMNode(item.Value, slaveID, mapNode);
                            }
                            else if (mapNode.Name == "ENMap")
                            {
                                encNode.parseENMNode(item.Value, slaveID, mapNode);
                            }
                        }
                    }
                }
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
            exportXMLnodeIEC104(xmlDoc, rootNode);
            exportXMLnodeMODBUS(xmlDoc, rootNode);
            exportXMLnodeIEC101(xmlDoc, rootNode);
            exportXMLnodeIEC61850(xmlDoc, rootNode);
            return rootNode;
        }
        private void exportXMLnodeIEC104(XmlDocument xmlDoc, XmlNode rootNode)
        {
            string strRoutineName = "exportXMLnodeIEC104";
            try
            {
                foreach (IEC104Slave slv104 in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC104Group().getIEC104Slaves())
                {//Loop thru slaves...
                    Console.WriteLine("@@@@@@@ Get mapping details for IEC104 slave no.: {0}", slv104.SlaveNum);
                    XmlNode slaveNode = xmlDoc.CreateElement("IEC104");
                    rootNode.AppendChild(slaveNode);
                    XmlAttribute attrSlaveNode = xmlDoc.CreateAttribute("SlaveNum");
                    attrSlaveNode.Value = slv104.SlaveNum;
                    slaveNode.Attributes.Append(attrSlaveNode);

                    if (aicNode != null)
                    {
                        try
                        {
                            XmlNode importSMAINode = slaveNode.OwnerDocument.ImportNode(aicNode.exportMapXMLnode("IEC104_" + slv104.SlaveNum), true);
                            slaveNode.AppendChild(importSMAINode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }
                    if (aocNode != null)
                    {
                        try
                        {
                            XmlNode importSMAONode = slaveNode.OwnerDocument.ImportNode(aocNode.exportMapXMLnode("IEC104_" + slv104.SlaveNum), true);
                            slaveNode.AppendChild(importSMAONode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }
                    if (dicNode != null)
                    {
                        try
                        {
                            XmlNode importSMDINode = slaveNode.OwnerDocument.ImportNode(dicNode.exportMapXMLnode("IEC104_" + slv104.SlaveNum), true);
                            slaveNode.AppendChild(importSMDINode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }

                    if (docNode != null)
                    {
                        try
                        {
                            XmlNode importSMDONode = slaveNode.OwnerDocument.ImportNode(docNode.exportMapXMLnode("IEC104_" + slv104.SlaveNum), true);
                            slaveNode.AppendChild(importSMDONode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }

                    if (encNode != null)
                    {
                        try
                        {
                            XmlNode importSMENNode = slaveNode.OwnerDocument.ImportNode(encNode.exportMapXMLnode("IEC104_" + slv104.SlaveNum), true);
                            slaveNode.AppendChild(importSMENNode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void exportXMLnodeMODBUS(XmlDocument xmlDoc, XmlNode rootNode)
        {
            string strRoutineName = "exportXMLnodeMODBUS";
            try
            {
                foreach (MODBUSSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())
                {//Loop thru slaves...
                    Console.WriteLine("@@@@@@@ Get mapping details for MODBUSSlave slave no.: {0}", slvMB.SlaveNum);
                    XmlNode slaveNode = xmlDoc.CreateElement("MODBUSSlave");
                    rootNode.AppendChild(slaveNode);
                    XmlAttribute attrSlaveNode = xmlDoc.CreateAttribute("SlaveNum");
                    attrSlaveNode.Value = slvMB.SlaveNum;
                    slaveNode.Attributes.Append(attrSlaveNode);

                    if (aicNode != null)
                    {
                        try
                        {
                            XmlNode importSMAINode = slaveNode.OwnerDocument.ImportNode(aicNode.exportMapXMLnode("MODBUSSlave_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMAINode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }
                    if (aocNode != null)
                    {
                        try
                        {
                            XmlNode importSMAINode = slaveNode.OwnerDocument.ImportNode(aocNode.exportMapXMLnode("MODBUSSlave_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMAINode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }
                    if (dicNode != null)
                    {
                        try
                        {
                            XmlNode importSMDINode = slaveNode.OwnerDocument.ImportNode(dicNode.exportMapXMLnode("MODBUSSlave_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMDINode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }

                    if (docNode != null)
                    {
                        try
                        {
                            XmlNode importSMDONode = slaveNode.OwnerDocument.ImportNode(docNode.exportMapXMLnode("MODBUSSlave_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMDONode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }

                    if (encNode != null)
                    {
                        try
                        {
                            XmlNode importSMENNode = slaveNode.OwnerDocument.ImportNode(encNode.exportMapXMLnode("MODBUSSlave_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMENNode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void exportXMLnodeIEC101(XmlDocument xmlDoc, XmlNode rootNode)
        {
            string strRoutineName = "exportXMLnodeIEC101";
            try
            {
                foreach (IEC101Slave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
                {//Loop thru slaves...
                    Console.WriteLine("@@@@@@@ Get mapping details for IEC101Slave slave no.: {0}", slvMB.SlaveNum);
                    XmlNode slaveNode = xmlDoc.CreateElement("IEC101Slave");
                    rootNode.AppendChild(slaveNode);
                    XmlAttribute attrSlaveNode = xmlDoc.CreateAttribute("SlaveNum");
                    attrSlaveNode.Value = slvMB.SlaveNum;
                    slaveNode.Attributes.Append(attrSlaveNode);

                    if (aicNode != null)
                    {
                        try
                        {
                            XmlNode importSMAINode = slaveNode.OwnerDocument.ImportNode(aicNode.exportMapXMLnode("IEC101Slave_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMAINode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }
                    if (aocNode != null)
                    {
                        try
                        {
                            XmlNode importSMAINode = slaveNode.OwnerDocument.ImportNode(aocNode.exportMapXMLnode("IEC101Slave_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMAINode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }

                    if (dicNode != null)
                    {
                        try
                        {
                            XmlNode importSMDINode = slaveNode.OwnerDocument.ImportNode(dicNode.exportMapXMLnode("IEC101Slave_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMDINode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }

                    if (docNode != null)
                    {
                        try
                        {
                            XmlNode importSMDONode = slaveNode.OwnerDocument.ImportNode(docNode.exportMapXMLnode("IEC101Slave_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMDONode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }

                    if (encNode != null)
                    {
                        try
                        {
                            XmlNode importSMENNode = slaveNode.OwnerDocument.ImportNode(encNode.exportMapXMLnode("IEC101Slave_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMENNode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void exportXMLnodeIEC61850(XmlDocument xmlDoc, XmlNode rootNode)
        {
            string strRoutineName = "exportXMLnodeIEC61850";
            try
            {
                foreach (IEC61850ServerSlave slvMB in Utils.getOpenProPlusHandle().getSlaveConfiguration().get61850SlaveGroup().getMODBUSSlaves())
                {//Loop thru slaves...
                    Console.WriteLine("@@@@@@@ Get mapping details for IEC61850ServerGroup slave no.: {0}", slvMB.SlaveNum);
                    XmlNode slaveNode = xmlDoc.CreateElement("IEC61850Server");
                    rootNode.AppendChild(slaveNode);
                    XmlAttribute attrSlaveNode = xmlDoc.CreateAttribute("SlaveNum");
                    attrSlaveNode.Value = slvMB.SlaveNum;
                    slaveNode.Attributes.Append(attrSlaveNode);
                    if (aicNode != null)
                    {
                        try
                        {
                            XmlNode importSMAINode = slaveNode.OwnerDocument.ImportNode(aicNode.exportMapXMLnode("IEC61850Server_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMAINode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }
                    if (aocNode != null)
                    {
                        try
                        {
                            XmlNode importSMAINode = slaveNode.OwnerDocument.ImportNode(aocNode.exportMapXMLnode("IEC61850Server_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMAINode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }
                    if (dicNode != null)
                    {
                        try
                        {
                            XmlNode importSMDINode = slaveNode.OwnerDocument.ImportNode(dicNode.exportMapXMLnode("IEC61850Server_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMDINode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }
                    if (docNode != null)
                    {
                        try
                        {
                            XmlNode importSMDONode = slaveNode.OwnerDocument.ImportNode(docNode.exportMapXMLnode("IEC61850Server_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMDONode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }
                    if (encNode != null)
                    {
                        try
                        {
                            XmlNode importSMENNode = slaveNode.OwnerDocument.ImportNode(encNode.exportMapXMLnode("IEC61850Server_" + slvMB.SlaveNum), true);
                            slaveNode.AppendChild(importSMENNode);
                        }
                        catch (System.InvalidOperationException)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
