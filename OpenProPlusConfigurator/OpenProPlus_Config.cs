using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.IO.Compression;

namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>oppcValidationMessages</b> is a class which stores event data
    * \details   This class stores event data which is returned while validating the
    * XML file against a schema file. It stores info like the severity of conflict,
    * line no. at which conflict detected, error message, etc. It also validates the
    * XML against a schema before opening it.
    * 
    */
    public class oppcValidationMessages
    {
        private XmlSeverityType severity;
        private int lineno = -1;
        private string message = "";
        public oppcValidationMessages(XmlSeverityType st, int ln, string msg)
        {
            severity = st;
            lineno = ln;
            message = msg;
        }
        public XmlSeverityType Severity { get { return severity; } }
        public int LineNo { get { return lineno; } }
        public string Message { get { return message; } }
    }

    /**
    * \brief     <b>OpenProPlus_Config</b> is the main class.
    * \details   This is the main class. It holds all other sub classes like master configuration,
    * slave configuration, master types, slave types, IED's, etc. It also deals with creating default nodes,
    * exporting to XML, exporting to INI, saving file, reseting context, etc.
    * 
    */
    public class OpenProPlus_Config
    {
        //Ajay: 21/11/2017
        public bool IsFileOpen = true;
        private string xmlFile = null;
        private bool gXMLValid = true;
        private string rnName = "OpenProPlus_Config";
        private bool isNodeComment = false;
        private string comment = "";
        private XmlDocument schemaDataType = null;
        private XmlDocument schemaMain = null;
        private XmlNamespaceManager nsSchema = null;
        private Details det = new Details();
        private NetWorkConfiguration nc = new NetWorkConfiguration();
        private SerialPortConfiguration spc;// = new SerialPortConfiguration();
        private SystemConfiguration sc = new SystemConfiguration();
        private SlaveConfiguration slc;// = new SlaveConfiguration();//Expose it to others for slave mapping...
        private MasterConfiguration mtc;//new MasterConfiguration();
        private ParameterLoadConfiguration plc;// = new ParameterLoadConfiguration();
        public event EventHandler<oppcValidationMessages> ShowValidationMessages;
        ucOpenProPlus_Config ucoppc = new ucOpenProPlus_Config();

        //Namrata:16/03/2018
        private RCBConfiguration rcb;//new MasterConfiguration();
        //Namrata:04/7/2017
        DataTable dt = new DataTable(); //Datatable For Genrating CSV File
        DataGridView datagrid = new DataGridView();
        public List<MODBUSMaster> MODBUSList = new List<MODBUSMaster>();

        #region MainNodes
        KeyValuePair<string, string>[] mainNodes = new KeyValuePair<string, string>[]
                    {
                        new KeyValuePair<string, string>("Details", "Details"),
                        new KeyValuePair<string, string>("NetWorkConfiguration", "Network Configuration"),
                        new KeyValuePair<string, string>("SerialPortConfiguration", "Serial Configuration"),
                        new KeyValuePair<string, string>("SystemConfiguration", "System Configuration"),
                        new KeyValuePair<string, string>("SlaveConfiguration", "Slave Configuration"),
                        new KeyValuePair<string, string>("MasterConfiguration", "Master Configuration"),
                        new KeyValuePair<string, string>("ParameterLoadConfiguration", "Parameter Load Configuration")
                    };
        #endregion MainNodes

        #region CommonNodes
        KeyValuePair<string, string>[] commonNodes = new KeyValuePair<string, string>[]
                    {
                        new KeyValuePair<string, string>("ADRMaster", "ADRMaster"),
                        new KeyValuePair<string, string>("IEC101Master", "IEC101Master"),
                        new KeyValuePair<string, string>("IEC103Master", "IEC103Master"),
                        new KeyValuePair<string, string>("ModbusMaster", "ModbusMaster"),
                        new KeyValuePair<string, string>("IEC61850ServerMaster", "IEC61850ServerMaster"),
                        new KeyValuePair<string, string>("VirtualMaster", "VirtualMaster"),
                        //new KeyValuePair<string, string>("PLUMaster", "PLUMaster"),
                        new KeyValuePair<string, string>("IED", "IED"),
                        new KeyValuePair<string, string>("RCB", "RCB"),
                        new KeyValuePair<string, string>("AI", "AI"),
                        new KeyValuePair<string, string>("AO", "AO"),
                        new KeyValuePair<string, string>("DI", "DI"),
                        new KeyValuePair<string, string>("DO", "DO"),
                        new KeyValuePair<string, string>("EN", "EN")
                    };
        #endregion CommonNodes

        #region SystemConfig
        KeyValuePair<string, string>[] sysConfiguration = new KeyValuePair<string, string>[]
                    {
                        new KeyValuePair<string, string>("SystemConfig", "System Config"),
                    };
        #endregion SystemConfig

        #region Default tree nodes for 'MasterConfiguration' , 'SlaveConfiguration' , 'ParameterLoadConfiguration'

        Dictionary<string, TreeNode> defSCnodes = new Dictionary<string, TreeNode>();
        Dictionary<string, TreeNode> defSLCnodes = new Dictionary<string, TreeNode>();
        Dictionary<string, TreeNode> defMCnodes = new Dictionary<string, TreeNode>();
        Dictionary<string, TreeNode> defPLCnodes = new Dictionary<string, TreeNode>();

        #endregion Default tree nodes for 'MasterConfiguration' , 'SlaveConfiguration' , 'ParameterLoadConfiguration'

        #region SlaveConfiguration

        KeyValuePair<string, string>[] slaveConfiguration = new KeyValuePair<string, string>[]
                    {
                        new KeyValuePair<string, string>("IEC104Group", "IEC104 Group"),
                        new KeyValuePair<string, string>("MODBUSSlaveGroup", "MODBUS Group"),
                        new KeyValuePair<string, string>("IEC101SlaveGroup", "IEC101 Group"),
                        new KeyValuePair<string, string>("IEC61850ServerGroup", "IEC61850 Server Group"),
                    };

        #endregion SlaveConfiguration

        #region MasterConfiguration
        KeyValuePair<string, string>[] masterConfiguration = new KeyValuePair<string, string>[]
                    {
                        new KeyValuePair<string, string>("ADRGroup",    "ADR Group"),
                        new KeyValuePair<string, string>("IEC101Group", "IEC101 Group"),
                        new KeyValuePair<string, string>("IEC103Group", "IEC103 Group"),
                        new KeyValuePair<string, string>("MODBUSGroup", "MODBUS Group"),
                        //Namrata: 01/09/2017
                        new KeyValuePair<string, string>("IEC61850ClientGroup", "IEC61850 Group"),
                         //new KeyValuePair<string, string>("PLUGroup", "PLU Group"),//IEC61850Group
                        new KeyValuePair<string, string>("VirtualGroup", "Virtual Group"),
                    };

        #endregion  masterConfiguration

        #region ParameterLoadConfiguration
        KeyValuePair<string, string>[] paraLoadConfiguration = new KeyValuePair<string, string>[]
                    {
                        new KeyValuePair<string, string>("ClosedLoopAction", "Closed Loop Action"),
                        new KeyValuePair<string, string>("ProfileRecord", "Profile Record"),
                        new KeyValuePair<string, string>("MDCalculation", "MD Calculation"),
                        new KeyValuePair<string, string>("DerivedParam", "Derived Parameter"),
                        new KeyValuePair<string, string>("DerivedDI", "Derived DI"),
                    };
        #endregion ParameterLoadConfiguration

        public OpenProPlus_Config()
        {
            Utils.setOpenProPlusHandle(this);
            loadSchema(out schemaDataType, Globals.RESOURCES_PATH + Globals.XSD_DATATYPE_FILENAME);//Do asap since we will b loading info from schema for various objects like SerialPortConfiguration, etc.
            loadSchema(out schemaMain, Globals.RESOURCES_PATH + Globals.XSD_FILENAME);//Do asap since we will b loading info from schema for various objects like maxOccurs for IED, etc...
            setMaxValues();
            Console.WriteLine("### Reading maxOccurs: {0}", getMaxNetworkInterfaces());
            spc = new SerialPortConfiguration();
            slc = new SlaveConfiguration();
            plc = new ParameterLoadConfiguration();
            ucoppc.Click += new System.EventHandler(this.button1_Click);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Utils.WriteLine(VerboseLevel.BOMBARD, "*** ucoppc btn clicked, handled in class.");
        }

        private void mnuItem_Click(object sender, EventArgs e)
        {
            Utils.WriteLine(VerboseLevel.BOMBARD, "*** ucoppc mnuItem clicked, handled in class.");
            dt.Columns.Clear();
            dt.Rows.Clear();
            //Create Datatable
            dt.Columns.Add("Binary Name", typeof(string));
            dt.Columns.Add("MasterNum", typeof(string));
            dt.Columns.Add("Debug", typeof(string));
            dt.Columns.Add("Run", typeof(string));
            MODBUSList.Clear();
            MODBUSList.AddRange(Utils.OPPCCModbusList);
            foreach (var L in Utils.OPPCCModbusList)
            {
                DataRow newRow = dt.NewRow();
                newRow["Binary Name"] = (MasterTypes.MODBUS).ToString();
                newRow["MasterNum"] = L.MasterNum;
                newRow["Debug"] = L.DEBUG;
                newRow["Run"] = L.Run;
                dt.Rows.Add(newRow);
                datagrid.DataSource = dt;
            }
            ExportToExcel();
        }
        public void ExportToExcel()
        {
            string strRoutineName = "ExportToExcel";
            try
            {
                DataTable dt1 = new DataTable();
                String filename = "";
                dt1 = (DataTable)datagrid.DataSource;
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
                    saveFileDialog1.FileName = "MODBUS File"; ;
                    saveFileDialog1.Title = "Save an CSV";
                    saveFileDialog1.ShowDialog();
                    // If the file name is not an empty string open it for saving.
                    if (saveFileDialog1.FileName != "")
                    {
                        filename = saveFileDialog1.FileName;
                    }
                    else
                    {
                        return;
                    }
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false);

                    foreach (DataRow dr in dt1.Rows)
                    {
                        for (int i = 0; i < dt1.Columns.Count; i++)
                        {
                            if (!Convert.IsDBNull(dr[i]))
                            {
                                string value = dr[i].ToString();
                                if (value.Contains(','))
                                {
                                    value = String.Format("\"{0}\"", value);
                                    sw.Write(value);
                                }
                                else
                                {
                                    sw.Write(dr[i].ToString());
                                }
                            }
                            if (i < dt1.Columns.Count - 1)
                            {
                                sw.Write(",");
                            }
                        }
                        sw.Write(sw.NewLine);
                    }
                    MessageBox.Show("File Exported Successfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("OpenProPlus_Config:" + strRoutineName + ": " + Application.ProductName + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
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

            XmlNode importDetNode = rootNode.OwnerDocument.ImportNode(det.exportXMLnode(), true);
            rootNode.AppendChild(importDetNode);

            XmlNode importNCNode = rootNode.OwnerDocument.ImportNode(nc.exportXMLnode(), true);
            rootNode.AppendChild(importNCNode);

            XmlNode importSPCNode = rootNode.OwnerDocument.ImportNode(spc.exportXMLnode(), true);
            rootNode.AppendChild(importSPCNode);

            XmlNode importSCNode = rootNode.OwnerDocument.ImportNode(sc.exportXMLnode(), true);
            rootNode.AppendChild(importSCNode);

            XmlNode importSLCNode = rootNode.OwnerDocument.ImportNode(slc.exportXMLnode(), true);
            rootNode.AppendChild(importSLCNode);

            XmlNode importMTCNode = rootNode.OwnerDocument.ImportNode(mtc.exportXMLnode(), true);
            rootNode.AppendChild(importMTCNode);

            XmlNode importPLCNode = rootNode.OwnerDocument.ImportNode(plc.exportXMLnode(), true);
            rootNode.AppendChild(importPLCNode);
            return rootNode;
        }
        static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                file.MoveTo(Path.Combine(destination.FullName, file.Name));
            }
            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                // Get destination directory.
                string destinationDir = Path.Combine(destination.FullName, dir.Name);
                // Call CopyDirectory() recursively.
                CopyDirectory(dir, new DirectoryInfo(destinationDir));
            }
        }
        static IEnumerable<string> GetFileSearchPaths(string fileName)
        {
            yield return fileName;
            yield return Path.Combine(Directory.GetParent(Path.GetDirectoryName(fileName)).FullName, Path.GetFileName(fileName));
        }
        static bool FileExists(string fileName)
        {
            return GetFileSearchPaths(fileName).Any(File.Exists);
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
            string iniData = "";
            iniData += mtc.exportINI(slaveNum, slaveID);
            //Now handle ParameterLoadConfiguration...
            iniData += plc.exportINI(slaveNum, slaveID);
            return iniData;
        }
        public void regenerateSequence()
        {
            plc.getClosedLoopAction().regenerateSequence();  //Handle Closed Loop Action...
            plc.getProfileRecord().regenerateSequence();//Handle Profile Record...
            plc.getMDCalculation().regenerateSequence(); //Handle MD Calculation...
            plc.getDerivedParam().regenerateSequence();  //Handle Derived Param...
            plc.getDerivedDI().regenerateSequence();   //Handle Derived DI...

            if (Utils.AilistRegenerateIndex.Count > 0) // For AI Reindexing
            {
                DialogResult result = MessageBox.Show("Do you want to reindex AI data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    mtc.regenerateAISequence();
                    //MessageBox.Show(" AI Reindexing Done!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == DialogResult.No)
                {

                }
            }
            if (Utils.AolistRegenerateIndex.Count > 0) // for AO Reindexing
            {
                DialogResult AOresult = MessageBox.Show("Do you want to reindex AO data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (AOresult == DialogResult.Yes)
                {
                    mtc.regenerateAOSequence();
                    //MessageBox.Show(" AO Reindexing Done!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (AOresult == DialogResult.No)
                {

                }
            }
            if (Utils.DilistRegenerateIndex.Count > 0) // For DI Reindexing
            {
                DialogResult DIresult = MessageBox.Show("Do you want to reindex DI data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (DIresult == DialogResult.Yes)
                {
                    mtc.regenerateDISequence();
                    //MessageBox.Show(" DI Reindexing Done!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (DIresult == DialogResult.No)
                {

                }
            }

            if (Utils.DolistRegenerateIndex.Count > 0)
            {
                DialogResult DOResult = MessageBox.Show("Do you want to reindex DO data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (DOResult == DialogResult.Yes)
                {
                    mtc.regenerateDOSequence();
                    //MessageBox.Show(" DO Reindexing Done!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (DOResult == DialogResult.No)
                {

                }
            }
            if (Utils.EnlistRegenerateIndex.Count > 0)
            {
                DialogResult ENresult = MessageBox.Show("Do you want to reindex EN data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (ENresult == DialogResult.Yes)
                {
                    mtc.regenerateENSequence();
                    //MessageBox.Show(" EN Reindexing Done!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (ENresult == DialogResult.No)
                {

                }
            }

            //Reset reindexing flags for PLC nodes for next use...
            plc.resetReindexFlags();
        }
        //Display any warnings or errors.
        private void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            gXMLValid = false;
            Console.WriteLine("\tValidation error oppc object: Line No. {0}: {1}", e.Exception.LineNumber, e.Message);
            oppcValidationMessages ovm = new oppcValidationMessages(e.Severity, e.Exception.LineNumber, e.Message);
            if (ShowValidationMessages != null)
                ShowValidationMessages(sender, ovm);
        }

        public bool loadSchema(out XmlDocument schDoc, string schemaFile)
        {
            schDoc = null;
            if (schemaFile == null || schemaFile == "" || !File.Exists(schemaFile)) return false;

            schDoc = new XmlDocument();
            try
            {
                schDoc.Load(schemaFile);
            }
            catch (XmlException)
            {
                return false;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            nsSchema = new XmlNamespaceManager(schDoc.NameTable);
            nsSchema.AddNamespace("xx", "http://www.w3.org/2001/XMLSchema");

            XmlNodeList nodeList = schDoc.DocumentElement.SelectNodes("//xx:schema", nsSchema);
            if (nodeList.Count <= 0)
            {
                return false;
            }

            return true;
        }

        public List<string> getDataTypeValues(string dataTypeName)
        {
            if (String.IsNullOrWhiteSpace(dataTypeName)) return null;
            if (schemaDataType == null) return null;
            if (schemaDataType.DocumentElement.SelectNodes("//xx:schema/xx:simpleType[@name='" + dataTypeName + "'][1]/xx:restriction/xx:enumeration", nsSchema).Count <= 0) return null;

            List<string> dtValues = new List<string>();
            foreach (XmlElement el in schemaDataType.DocumentElement.SelectNodes("//xx:schema/xx:simpleType[@name='" + dataTypeName + "'][1]/xx:restriction/xx:enumeration", nsSchema))
            {
                //Console.WriteLine(el.GetAttribute("value"));
                dtValues.Add(el.GetAttribute("value"));
            }

            return dtValues;
        }
        public void setMaxValues()
        {
            string selectPath;
            int tmp;
            if (schemaMain == null) return;

            KeyValuePair<string, string>[] allMaxVars = new KeyValuePair<string, string>[]
                    {
                        new KeyValuePair<string, string>("MaxIEC104Slave", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='SlaveConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='IEC104Group'][1]/xx:complexType/xx:sequence/xx:element[@name='IEC104'][1]"),
                        new KeyValuePair<string, string>("MaxIEC103Master", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='MasterConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='IEC103Group'][1]/xx:complexType/xx:sequence/xx:element[@name='IEC103'][1]"),
                        new KeyValuePair<string, string>("MaxIEC103IED", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='MasterConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='IEC103Group'][1]/xx:complexType/xx:sequence/xx:element[@name='IEC103'][1]/xx:complexType/xx:sequence/xx:element[@name='IED'][1]"),
                        new KeyValuePair<string, string>("MaxIEC103AI", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='MasterConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='IEC103Group'][1]/xx:complexType/xx:sequence/xx:element[@name='IEC103'][1]/xx:complexType/xx:sequence/xx:element[@name='IED'][1]/xx:complexType/xx:sequence/xx:element[@name='AIConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='AI'][1]"),
                        new KeyValuePair<string, string>("MaxIEC103DI", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='MasterConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='IEC103Group'][1]/xx:complexType/xx:sequence/xx:element[@name='IEC103'][1]/xx:complexType/xx:sequence/xx:element[@name='IED'][1]/xx:complexType/xx:sequence/xx:element[@name='DIConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='DI'][1]"),
                        new KeyValuePair<string, string>("MaxIEC103DO", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='MasterConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='IEC103Group'][1]/xx:complexType/xx:sequence/xx:element[@name='IEC103'][1]/xx:complexType/xx:sequence/xx:element[@name='IED'][1]/xx:complexType/xx:sequence/xx:element[@name='DOConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='DO'][1]"),
                        new KeyValuePair<string, string>("MaxIEC103EN", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='MasterConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='IEC103Group'][1]/xx:complexType/xx:sequence/xx:element[@name='IEC103'][1]/xx:complexType/xx:sequence/xx:element[@name='IED'][1]/xx:complexType/xx:sequence/xx:element[@name='ENConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='EN'][1]"),
                        new KeyValuePair<string, string>("MaxMODBUSMaster", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='MasterConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='MODBUSGroup'][1]/xx:complexType/xx:sequence/xx:element[@name='MODBUS'][1]"),
                        new KeyValuePair<string, string>("MaxMODBUSIED", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='MasterConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='MODBUSGroup'][1]/xx:complexType/xx:sequence/xx:element[@name='MODBUS'][1]/xx:complexType/xx:sequence/xx:element[@name='IED'][1]"),
                        new KeyValuePair<string, string>("MaxMODBUSAI", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='MasterConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='MODBUSGroup'][1]/xx:complexType/xx:sequence/xx:element[@name='MODBUS'][1]/xx:complexType/xx:sequence/xx:element[@name='IED'][1]/xx:complexType/xx:sequence/xx:element[@name='AIConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='AI'][1]"),
                        new KeyValuePair<string, string>("MaxMODBUSDI", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='MasterConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='MODBUSGroup'][1]/xx:complexType/xx:sequence/xx:element[@name='MODBUS'][1]/xx:complexType/xx:sequence/xx:element[@name='IED'][1]/xx:complexType/xx:sequence/xx:element[@name='DIConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='DI'][1]"),
                        new KeyValuePair<string, string>("MaxMODBUSDO", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='MasterConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='MODBUSGroup'][1]/xx:complexType/xx:sequence/xx:element[@name='MODBUS'][1]/xx:complexType/xx:sequence/xx:element[@name='IED'][1]/xx:complexType/xx:sequence/xx:element[@name='DOConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='DO'][1]"),
                        new KeyValuePair<string, string>("MaxMODBUSEN", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='MasterConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='MODBUSGroup'][1]/xx:complexType/xx:sequence/xx:element[@name='MODBUS'][1]/xx:complexType/xx:sequence/xx:element[@name='IED'][1]/xx:complexType/xx:sequence/xx:element[@name='ENConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='EN'][1]"),
                        new KeyValuePair<string, string>("MaxCLA", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='ParameterLoadConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='ClosedLoopAction'][1]/xx:complexType/xx:sequence/xx:element[@name='CLA'][1]"),
                        new KeyValuePair<string, string>("MaxProfile", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='ParameterLoadConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='ProfileRecord'][1]/xx:complexType/xx:sequence/xx:element[@name='Profile'][1]"),
                        new KeyValuePair<string, string>("MaxMDCalc", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='ParameterLoadConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='MDCalculation'][1]/xx:complexType/xx:sequence/xx:element[@name='MD'][1]"),
                        new KeyValuePair<string, string>("MaxDerivedParam", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='ParameterLoadConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='DerivedParam'][1]/xx:complexType/xx:sequence/xx:element[@name='DP'][1]"),
                        new KeyValuePair<string, string>("MaxDerivedDI", "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='ParameterLoadConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='DerivedDI'][1]/xx:complexType/xx:sequence/xx:element[@name='DD'][1]")
                    };

            foreach (KeyValuePair<string, string> maxVar in allMaxVars)
            {
                selectPath = maxVar.Value;
                if (schemaMain.DocumentElement.SelectNodes(selectPath, nsSchema).Count > 0)
                {
                    XmlElement el = (XmlElement)schemaMain.DocumentElement.SelectNodes(selectPath, nsSchema)[0];
                    try
                    {
                        tmp = Int32.Parse(el.GetAttribute("maxOccurs"));
                        typeof(Globals).GetProperty(maxVar.Key).SetValue(typeof(Globals), tmp);
                        Console.WriteLine("Got max. values for {0}: {1}", maxVar.Key, tmp);
                    }
                    catch (System.FormatException)
                    {
                        tmp = -1;
                    }
                    catch (System.NullReferenceException)
                    {
                        tmp = -1;
                    }
                }
            }
        }
        public int getMaxNetworkInterfaces()
        {
            int maxVal = -1;
            string selectPath = "//xx:schema/xx:element/xx:complexType/xx:sequence/xx:element[@name='NetWorkConfiguration'][1]/xx:complexType/xx:sequence/xx:element[@name='Lan'][1]";
            if (schemaMain == null) return maxVal;
            if (schemaMain.DocumentElement.SelectNodes(selectPath, nsSchema).Count <= 0) return -1;

            XmlElement el = (XmlElement)schemaMain.DocumentElement.SelectNodes(selectPath, nsSchema)[0];

            try
            {
                maxVal = Int32.Parse(el.GetAttribute("maxOccurs"));
            }
            catch (System.FormatException)
            {
                maxVal = -1;
            }

            Console.WriteLine("Voww, MaxNetworkInterfaces got it!!! {0}", maxVal);
            return maxVal;
        }
        public int loadXML(string filename, TreeView tvItems, out bool IsXmlValid)
        {
            IsXmlValid = false;
            if (filename == null || filename == "" || !File.Exists(filename)) return -1;

            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(filename);

            }
            catch (XmlException)
            {
                return -2;//Not Well-formed XML...
            }
            catch (ArgumentNullException)
            {
                return -1;
            }

            //Utils.DummyDI.Clear();

            gXMLValid = true;//IMP: Reset global var...
            xmlFile = filename;//Assign only after loading file...
            //Namrata: 24/02/2018
            Utils.XMLFileName = filename;
            XmlNodeList nodeList = xmlDoc.SelectNodes("OpenProPlus_Config");
            XmlNode rootNode = nodeList.Item(0);
            rnName = rootNode.Name;
            foreach (XmlNode node in rootNode)
            {
                Utils.WriteLine(VerboseLevel.BOMBARD, "node value: '{0}' child count {1}", node.Name, node.ChildNodes.Count);
                if (node.Name == "Details")
                {
                    det.parseDetailsNode(node);
                }
                else if (node.Name == "NetWorkConfiguration")
                {
                    nc.parseNCNode(node);
                }
                else if (node.Name == "SerialPortConfiguration")
                {
                    spc.parseSPCNode(node);
                }
                else if (node.Name == "SystemConfiguration")
                {
                    sc.parseSCNode(node, defSCnodes);
                }
                else if (node.Name == "SlaveConfiguration")
                {
                    slc.parseSCNode(node, defSLCnodes);
                }
                else if (node.Name == "MasterConfiguration")
                {
                    mtc.parseMCNode(node, defMCnodes);
                }
                else if (node.Name == "ParameterLoadConfiguration")
                {
                    plc.parsePLCNode(node, defPLCnodes);
                }
                else
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "***** Node '{0}' not supported!!!", node.Name);
                }
            }

            #region If User edit "SystemConfig" "RedundancyMode" "None" to "Primary/Secondary" it creates automatic enteris in VirtualDI 
            //Namrata: 03/03/2018
            if (rootNode.ChildNodes[3].Name == "SystemConfiguration")
            {
                sc.parseSCNodeToCheckDeviceExist(rootNode.ChildNodes[3], defSCnodes);
            }
            else
            {

            }
            #endregion If User edit "SystemConfig" "RedundancyMode" "None" to "Primary/Secondary" it creates automatic enteris in VirtualDI 
            //Namrata: 21/1/2017
            xmlDoc.Save(filename);
            xmlDoc.Save(Utils.XMLFileName);
            if (!Utils.IsXMLValid(filename, Globals.RESOURCES_PATH + Globals.XSD_FILENAME, ValidationCallBack))
            {
                Utils.WriteLine(VerboseLevel.ERROR, "*** Error parsing XSD file!!!");
                MessageBox.Show("filename: " + filename + ";" + "Globals.RESOURCES_PATH: " + Globals.RESOURCES_PATH + "Globals.XSD_FILENAME: " + Globals.XSD_FILENAME);
                return -3;
            }
            else
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "*** Atleast XSD file parsed successfully!!!");
                if (!gXMLValid)
                {
                    Utils.WriteLine(VerboseLevel.ERROR, "oooohhh: XML file is not valid as per schema!!!");
                    return -4;
                }
                else
                {
                    //Namrata: 19/11/2017
                    Utils.WriteLine(VerboseLevel.DEBUG, "*** hoooorray, XML valid as per schema.");
                    IsXmlValid = true;
                }
            }
            Utils.WriteLine(VerboseLevel.DEBUG, "Loading XMLfile: {0}", xmlFile);
            tvItems.ExpandAll();
            return 0;
        }
        public string getXMLData()
        {
            return this.exportXML();
        }
        public string getINIData(string filename, string slaveNum, string slaveID)
        {
            string iniData = "[" + slaveNum + "]" + Environment.NewLine;
            iniData += "xmlFile=" + filename + Environment.NewLine;
            iniData += "xmlVersion=" + Globals.INI_XML_VERSION + Environment.NewLine;//FIXME:
            iniData += "Master=" + slaveNum + Environment.NewLine;
            return iniData += this.exportINI(slaveNum, slaveID);
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("OpenProPlus_Config_")) return ucoppc;

            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("Details_"))
            {
                return det.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("NetWorkConfiguration_"))
            {
                return nc.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("SerialPortConfiguration_"))
            {
                return spc.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("SystemConfiguration_"))
            {
                return sc.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("SlaveConfiguration_"))
            {
                return slc.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("MasterConfiguration_"))
            {
                return mtc.getView(kpArr);
            }
            else if (kpArr.ElementAt(0).Contains("ParameterLoadConfiguration_"))
            {
                return plc.getView(kpArr);
            }
            else
            {
                Utils.WriteLine(VerboseLevel.ERROR, "***** OpenProPlus_Config: Node key '{0}' not supported for view!!!", kpArr.ElementAt(0));
            }
            return null;
        }
        public ContextMenuStrip getContextMenu()
        {
            //Namrata: 15/07/2017
            ContextMenuStrip tcms = new ContextMenuStrip();
            ImageList ilTvItems = new ImageList();
            ilTvItems.ImageSize = new Size(14, 14);
            ilTvItems.ColorDepth = ColorDepth.Depth32Bit;
            tcms.ImageList = ilTvItems;
            ToolStripMenuItem itmAdd = new ToolStripMenuItem("Genrate CSV File", null, new System.EventHandler(this.mnuItem_Click));
            tcms.Items.Add(itmAdd);
            return tcms;
        }
       
        public ContextMenuStrip GetToolstripMenu()
        {
            ContextMenuStrip tcms = new ContextMenuStrip();
            ToolStripMenuItem itmAdd = new ToolStripMenuItem();
            itmAdd = new ToolStripMenuItem("Export ICD", null, new System.EventHandler(this.ExportICD_Click));
            //itmAdd.Image = Image.FromFile(@"C:\Users\namrata\Desktop\add.png");
            tcms.Items.Add(itmAdd);
            return tcms;
        }
      
        //Namrata: 03/04/2018
        private void ExportICD_Click(object sender, EventArgs e)
        {
            Utils.ICDFiles = Application.StartupPath + @"\" + "IEC61850_Client" + @"\" + "ProtocolConfiguration";
            SaveFileDialog SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.FileName = Utils.SCLFileName;
            SaveFileDialog.DefaultExt = "icd";
            SaveFileDialog.Filter = "SCL files|*.icd;*.cid;*.iid;*.ssd;*.scd|IED Capability Description|*.icd|Instantiated IED Description|*.iid|System Specification Description|*.ssd|Substation Configuration Description|*.scd|Configured IED Description|*.cid|All files|*.*";
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(Utils.ICDFiles))
                {
                    if (FileExists(Utils.ICDFiles + @"\" + Utils.SCLFileName))
                    {
                        if(File.Exists(SaveFileDialog.FileName))
                        {
                            File.Delete(SaveFileDialog.FileName);
                        }
                        File.Copy(Utils.ICDFiles + @"\" + Utils.SCLFileName, SaveFileDialog.FileName);
                        MessageBox.Show("File save successfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("File does not exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        public void loadDefaultItems(TreeView tvItems)
        {
            ImageList ilTvItems = new ImageList();
            ilTvItems.ImageSize = new Size(19, 19);
            ilTvItems.ColorDepth = ColorDepth.Depth32Bit;
            tvItems.ImageList = ilTvItems;
            //Add images for main Nodes, by key...
            ilTvItems.Images.Add("OpenProPlus_Config", (Icon)Properties.Resources.ResourceManager.GetObject("OpenProPlus_Config"));
            foreach (KeyValuePair<string, string> mn in mainNodes)
            {
                try
                {
                    ilTvItems.Images.Add(mn.Key, (Image)Properties.Resources.ResourceManager.GetObject(mn.Key));
                }
                catch (System.InvalidOperationException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Failed to load image with key: {0}", mn.Key);
                }
            }
            //Add images for Common Nodes, by key...
            foreach (KeyValuePair<string, string> cnkp in commonNodes)
            {
                try
                {
                    if (cnkp.Key == "AI" || cnkp.Key == "DI" || cnkp.Key == "DO" || cnkp.Key == "EN" || cnkp.Key == "IED" || cnkp.Key == "RCB" || cnkp.Key == "IEC61850ServerMaster")
                        ilTvItems.Images.Add(cnkp.Key, (Icon)Properties.Resources.ResourceManager.GetObject(cnkp.Key));
                    else
                        ilTvItems.Images.Add(cnkp.Key, (Image)Properties.Resources.ResourceManager.GetObject(cnkp.Key));
                }
                catch (System.InvalidOperationException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Failed to load image with key: {0}", cnkp.Key);
                }
            }
            //Add images for SystemConfiguration Nodes, by key...
            foreach (KeyValuePair<string, string> sckp in sysConfiguration)
            {
                try
                {
                    ilTvItems.Images.Add(sckp.Key, (Image)Properties.Resources.ResourceManager.GetObject(sckp.Key));
                }
                catch (System.InvalidOperationException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Failed to load image with key: {0}", sckp.Key);
                }
            }
            //Add images for SlaveConfiguration Nodes, by key...
            foreach (KeyValuePair<string, string> slckp in slaveConfiguration)
            {
                try
                {
                    if (slckp.Key == "IEC104Group" || slckp.Key == "MODBUSSlaveGroup" || slckp.Key == "IEC101SlaveGroup" || slckp.Key == "IEC61850ServerGroup")
                        ilTvItems.Images.Add(slckp.Key, (Icon)Properties.Resources.ResourceManager.GetObject(slckp.Key));
                    else
                        ilTvItems.Images.Add(slckp.Key, (Image)Properties.Resources.ResourceManager.GetObject(slckp.Key));
                }
                catch (System.InvalidOperationException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Failed to load image with key: {0}", slckp.Key);
                }
            }
            //Add images for MasterConfiguration Nodes, by key...
            foreach (KeyValuePair<string, string> mckp in masterConfiguration)
            {
                try
                {
                    if (mckp.Key == "MODBUSGroup" || mckp.Key == "IEC101Group" || mckp.Key == "IEC61850ClientGroup")
                        ilTvItems.Images.Add(mckp.Key, (Icon)Properties.Resources.ResourceManager.GetObject(mckp.Key));
                    else
                        ilTvItems.Images.Add(mckp.Key, (Image)Properties.Resources.ResourceManager.GetObject(mckp.Key));
                }
                catch (System.InvalidOperationException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Failed to load image with key: {0}", mckp.Key);
                }
            }
            //Add images for ParameterLoadConfiguration Nodes, by key...
            foreach (KeyValuePair<string, string> plckp in paraLoadConfiguration)
            {
                try
                {
                    if (plckp.Key == "ClosedLoopAction" || plckp.Key == "DerivedDI" || plckp.Key == "DerivedParam")
                        ilTvItems.Images.Add(plckp.Key, (Icon)Properties.Resources.ResourceManager.GetObject(plckp.Key));
                    else
                        ilTvItems.Images.Add(plckp.Key, (Image)Properties.Resources.ResourceManager.GetObject(plckp.Key));
                }
                catch (System.InvalidOperationException)
                {
                    Utils.WriteLine(VerboseLevel.WARNING, "Failed to load image with key: {0}", plckp.Key);
                }
            }
            //Add nodes...
            tvItems.Nodes.Clear();
            tvItems.Nodes.Add("OpenProPlus_Config", "OpenProPlus Configuration", "OpenProPlus_Config", "OpenProPlus_Config");
            foreach (KeyValuePair<string, string> mn in mainNodes)
            {
                tvItems.Nodes[0].Nodes.Add(mn.Key, mn.Value, mn.Key, mn.Key);
            }
            //Add default treenodes for 'SystemConfiguration'
            foreach (KeyValuePair<string, string> sc in sysConfiguration)
            {
                TreeNode tmp = tvItems.Nodes[0].Nodes["SystemConfiguration"].Nodes.Add(sc.Key, sc.Value, sc.Key, sc.Key);
                defSCnodes.Add(sc.Key, tmp);
            }
            //Add default treenodes for 'SlaveConfiguration'
            foreach (KeyValuePair<string, string> slckp in slaveConfiguration)
            {
                TreeNode tmp = tvItems.Nodes[0].Nodes["SlaveConfiguration"].Nodes.Add(slckp.Key, slckp.Value, slckp.Key, slckp.Key);
                defSLCnodes.Add(slckp.Key, tmp);
            }
            //Add default treenodes for 'MasterConfiguration'
            foreach (KeyValuePair<string, string> mc in masterConfiguration)
            {
                TreeNode tmp = tvItems.Nodes[0].Nodes["MasterConfiguration"].Nodes.Add(mc.Key, mc.Value, mc.Key, mc.Key);
                defMCnodes.Add(mc.Key, tmp);
            }
            mtc = new MasterConfiguration(defMCnodes);
            //Add default treenodes for 'ParameterLoadConfiguration'
            foreach (KeyValuePair<string, string> plc in paraLoadConfiguration)
            {
                TreeNode tmp = tvItems.Nodes[0].Nodes["ParameterLoadConfiguration"].Nodes.Add(plc.Key, plc.Value, plc.Key, plc.Key);
                defPLCnodes.Add(plc.Key, tmp);
            }
            tvItems.ExpandAll();
        }
        public NetWorkConfiguration getNetworkConfiguration()
        {
            return nc;
        }
        public SerialPortConfiguration getSerialPortConfiguration()
        {
            return spc;
        }
        public MasterConfiguration getMasterConfiguration()
        {
            return mtc;
        }
        public SlaveConfiguration getSlaveConfiguration()
        {
            return slc;
        }
        public ParameterLoadConfiguration getParameterLoadConfiguration()
        {
            return plc;
        }
    }
}
