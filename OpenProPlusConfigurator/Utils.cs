using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Net;
using System.Reflection;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>Utils</b> is a class used to define utility functions
    * \details   This class is used to define utility functions which can be used across system.
    * 
    */
    public class Utils
    {
        #region Decalrations
        //Namrata: 27/04/2018
        public static string DirectoryNAME = "";
        public static string XMLFileNAME = "";
        public static string CreateXMLFolder = "";
        public static string XMLFileFullPath = "";
        public static string XMLFileNameWithoutExtension = "";
        public static bool IsAI = false;

        //nAMARTA:26/04/208
        public static string finalxmlfilename = "";
        public static string DName = "";
        public static string a = "";
        public static string Directory = "";
        public static string[] dsNameTemp;
        private static OpenProPlus_Config opcHandle;
        public static SaveFileDialog sfdFile = new SaveFileDialog();
        private static Point MouseDownLocation;
        //Namrata:19/6/2017

        public static string ExtractedFileName = "";
        public static string XMLFilecopy = "";
        public static string ZipFilePath = "";
        public static string ICDFiles = "";
        public static string XMLFileIEC61850Client = "";
        public static string XMLFilePath = "";
        public static string FilenameWithoutExtension = "";
        public static string AppPath = "";
        public static string[] GetIEDNoList = new string[0];
        public static Int32 IntIEC104Modbus = 0;
        public static Int32 IntIEC103Modbus = 0;
        public static string IEDno = "";
        public static int IEDUnitID;
        public static string ailistcount = "";
        public static string ICDFilePath = "";
        public static string MasterNum = "";
        public static string UnitIDfor61850Client = "";
        //Namrata: 13/10/2017
        public static string strFrmOpenproplusTreeNode = "";
        public static string strFrmOpenproplusIEdname = "";
        public static string Filename = "";
        //Namrata: 24/10/2017
        public static string Manufacture = "";
        public static string IEDName = "";
        public static string ICDName = "";
        public static string LogicalDeviceName = "";
        public static string MODBUSSlaveEnable = "";
        //Namrata: 02/11/2017
        public static string IEC61850ServerICDName = "";
        public static string UpdatedIEC61850ServerICDName = "";
        public static string IEC61850ServerSlaveNo = "";
        public static string IEC61850ServerSlaveNode = "";
        public static string CurrentSlave = "";
        public static string CurrentSlaveFinal = "";
        //Namrata: 21/01/2018
        public static string XMLFileName = "";
        public static bool IsOpen = true;
        public static string XMLUpdatedFileName = "";
        public static string XMLOldFileName = "";
        public static string RedundancyMode = "";
        public static string RemoteIPAddress = "";
        public static string FinalIEDName = "";
        public static string SCLFileName = "";
        public static string DirectoryPath = "";
        public static List<AI> DummyslaveAIMapList1 = new List<AI>();
        public static List<AIMap> AIMap = new List<AIMap>();
        public static List<NetworkInterface> NetworkIPLIst = new List<NetworkInterface>();
        public static List<NetworkInterface> NetworkIPLIstDummy = new List<NetworkInterface>();
        public static List<SerialInterface> DummysiList = new List<SerialInterface>();
        //Namrata:19/09/2017
        public static DataSet dtNetworkConfig = new DataSet();
        public static DataTable dtNetwork = new DataTable();
        //Namrata:19/09/2017
        public static DataSet dtSerialConfig = new DataSet();
        public static DataTable dtSerial = new DataTable();
        //Namrata:25/09/2017
        public static DataTable dtDataSet = new DataTable();
        public static List<NetworkInterface> NetworkInterface = new List<NetworkInterface>();
        //Namrata: 26/10/2017
        public static List<PLUMaster> VirtualPLU = new List<PLUMaster>();
        //Namrata: 16/11/2017
        public static List<DI> DilistRegenerateIndex = new List<DI>();
        public static List<AI> AilistRegenerateIndex = new List<AI>();
        public static List<AO> AolistRegenerateIndex = new List<AO>();
        public static List<DO> DolistRegenerateIndex = new List<DO>();
        public static List<EN> EnlistRegenerateIndex = new List<EN>();
        public static int MapIndex;
        public static List<IED> MODBUSMasteriedList = new List<IED>();
        public static List<IED> IEC101MasteriedList = new List<IED>();
        public static List<IED> IEC103MasteriedList = new List<IED>();
        public static List<IED> IEC61850MasteriedList = new List<IED>();
        public static List<IED> ADRMasteriedList = new List<IED>();
        //Namrata: 02/11/2017
        public static DataTable dtAISlave = new DataTable();
        public static DataSet dsAISlave = new DataSet();
       
        public static DataSet Dumyds = new DataSet();
        public static DataSet DumydsFinal = new DataSet();
        //Namrata: 07/11/2017
        public static DataTable dtDISlave = new DataTable();
        public static DataTable dtDOSlave = new DataTable();
        public static DataSet DSDISlave = new DataSet();
        public static DataSet DSDOSlave = new DataSet();
        public static DataSet DumydsDI = new DataSet();
        public static DataSet DumydsDO = new DataSet();
        public static List<DI> diList = new List<DI>();
        public static List<DI> DiListForVirualDIImport = new List<DI>();
     
        //Namrata:19/09/2017
        public static DataSet dsIEC104Slave = new DataSet();
        public static DataTable dtIEC104Slave = new DataTable();
        public static DataTable DtNetworkConfiguration = new DataTable();

        public static DataSet DsMODBUSSlave = new DataSet();
        public static DataTable DtMODBUSSlave = new DataTable();
        public static DataSet DsIEC61850ServerSlave = new DataSet();
        public static DataTable DtIEC61850ServerSlave = new DataTable();
        public static string UnitIDForIEC61850Client = "";
        public static string MasterNumForIEC61850Client = "";

        public static List<AI> AIlistforDescription = new List<AI>();
        public static List<AO> AOlistforDescription = new List<AO>();
        public static List<DI> DIlistforDescription = new List<DI>();
        public static List<DO> DolistforDescription = new List<DO>();
        public static List<EN> ENlistforDescription = new List<EN>();

        public static List<DI> diList1 = new List<DI>();
        public static List<DI> DummyDI = new List<DI>();
        public static List<DI> EnableDI = new List<DI>();

        public static List<IED> IEC61850MasteriedListGetIEDNo = new List<IED>();
        //Namrata: 09/04/2018
        public static DataTable OnReqData = new DataTable(); // On Request Data
        public static DataTable DtRCBdata = new DataTable();
        public static DataSet DsRCBData = new DataSet();
        public static DataSet DsRCB = new DataSet();
        public static DataSet DsRCBDS = new DataSet(); // Fill Dataset in Combobox
        public static DataSet DsRCBDataset = new DataSet(); // Fill Dataset in Combobox
        public static DataSet dsRCBData = new DataSet();
        public static DataSet DsResponseType = new DataSet(); // Fill Combobox ResponseType in AI,DI,DO,EN Configuraton
        public static DataSet dsResponseType = new DataSet(); // Fill Combobox ResponseType in AI,DI,DO,EN Configuraton
        public static DataSet dsIEDName = new DataSet(); // Fill Combobox IED
        public static DataSet dsIED = new DataSet(); // Fill Combobox IED
        public static DataSet DsAllConfigureData = new DataSet(); // Fill Configuration Data
        public static DataSet DsAllConfigurationData = new DataSet(); // Fill Configuration Data
        public static DataSet dsRCBAI = new DataSet();
        public static DataSet dsRCBAO = new DataSet();
        public static DataSet dsRCBDI = new DataSet();
        public static DataSet dsRCBDO = new DataSet();
        public static DataSet dsRCBEN = new DataSet();
        public static List<MODBUSMaster> OPPCCModbusList = new List<MODBUSMaster>(); //Genrate CSV File
        public static List<IEC61850ServerMaster> IEC61850ServerList = new List<IEC61850ServerMaster>(); //Get MasterNumber on Treenode Click
        public static List<RCB> RCB = new List<RCB>();
        public static string Iec61850IEDname = ""; //Get IEDName For IEC61850Client
        public static string XMLPath = "";//Get XML Path foe IEC61850Client
        //Namrata:26/04/2018
        public static string XMLFolderPath = "";
        public static List<AIMap> AIMapReindex = new List<AIMap>();
        #endregion Decalrations

        public static void SpecialCharacter_Validation(KeyPressEventArgs e)
        {
            var regex = new Regex(@"[<>]"); //(@"[^a-zA-Z0-9\s]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        public static void setOpenProPlusHandle(OpenProPlus_Config lopc)
        {
            opcHandle = lopc;
        }
        public static OpenProPlus_Config getOpenProPlusHandle()
        {
            return opcHandle;
        }
        public static void SetFormIcon(Form frmObj)
        {
            frmObj.Icon = (Icon)Properties.Resources.ResourceManager.GetObject(Globals.PROJECT_ICON);
        }
        public static void SetFormIcon(Form frmObj, string resourceName)
        {
            frmObj.Icon = (Icon)Properties.Resources.ResourceManager.GetObject(resourceName);
        }
        public static List<NetworkInterface> niList = new List<NetworkInterface>();
        public static List<SerialInterface> siList = new List<SerialInterface>();
        public static void CreateDefaultEntries()
        {
            //Create DI entry for 'DeviceMode'
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            diData.Add(new KeyValuePair<string, string>("DINo", (Globals.DINo + 1).ToString()));
            diData.Add(new KeyValuePair<string, string>("ResponseType", "DeviceMode"));
            diData.Add(new KeyValuePair<string, string>("Index", "0"));
            diData.Add(new KeyValuePair<string, string>("SubIndex", "0"));
            diData.Add(new KeyValuePair<string, string>("Description", "Hot/Standby"));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().getDIs().Add(new DI("DI", diData, null, MasterTypes.Virtual, 1, 0));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();

            //Create Network port entries...
            for (int i = 0; i < 4; i++)
            {
                List<KeyValuePair<string, string>> niData = new List<KeyValuePair<string, string>>();
                niData.Add(new KeyValuePair<string, string>("PortNum", (i + 1).ToString()));
                if (i == 3)
                {
                    niData.Add(new KeyValuePair<string, string>("PortName", "br0"));
                    niData.Add(new KeyValuePair<string, string>("ConnectionType", "Wired"));
                    niData.Add(new KeyValuePair<string, string>("IP", "10.0.2.106"));
                    niData.Add(new KeyValuePair<string, string>("Enable", "NO"));
                }
                else if (i == 2)
                {
                    niData.Add(new KeyValuePair<string, string>("PortName", "bond0"));
                    niData.Add(new KeyValuePair<string, string>("ConnectionType", "Bond"));
                    niData.Add(new KeyValuePair<string, string>("IP", "10.0.2.106"));
                    niData.Add(new KeyValuePair<string, string>("Enable", "YES"));
                }
                else
                {
                    niData.Add(new KeyValuePair<string, string>("PortName", "eth" + i));
                    niData.Add(new KeyValuePair<string, string>("ConnectionType", "Wired"));
                    niData.Add(new KeyValuePair<string, string>("IP", "10.0.2.110"));
                    niData.Add(new KeyValuePair<string, string>("Enable", "NO"));
                }
                niData.Add(new KeyValuePair<string, string>("VirtualIP", "0.0.0.0"));
                niData.Add(new KeyValuePair<string, string>("AddressType", "STATIC"));
                niData.Add(new KeyValuePair<string, string>("SubNetMask", "255.0.0.0"));
                niData.Add(new KeyValuePair<string, string>("GateWay", "10.0.0.50"));
                opcHandle.getNetworkConfiguration().getNetworkInterfaces().Add(new NetworkInterface("Lan", niData));

                NetworkInterface NewAI = new NetworkInterface("NetworkInterface", niData);
                niList.Add(NewAI);
            }

            for (int j = 0; j < niList.Count; j++)
            {
                string strLANHlth = "LANHealth_";
                {
                    if ((niList[j].PortName == "bond0") && (niList[j].Enable == "YES"))
                    {
                        if (niList[0].Enable == "NO")
                        {
                            if (!Utils.DummyDI.Select(x => x.Description.TrimEnd()).ToList().Contains(strLANHlth + niList[0].PortNum))
                            {
                                Utils.CreateDI4NetworkInterface(Convert.ToInt32(niList[0].PortNum));
                            }
                        }
                        if (niList[1].Enable == "NO")
                        {
                            if (!Utils.DummyDI.Select(x => x.Description.TrimEnd()).ToList().Contains(strLANHlth + niList[1].PortNum))
                            {
                                Utils.CreateDI4NetworkInterface(Convert.ToInt32(niList[1].PortNum));
                            }
                        }
                        if (!Utils.DummyDI.Select(x => x.Description.TrimEnd()).ToList().Contains(strLANHlth + niList[j].PortNum))
                        {
                            CreateDI4NetworkInterface(Convert.ToInt32(niList[j].PortNum));
                        }
                    }
                }
            }
            opcHandle.getNetworkConfiguration().refreshList();
            //Create Serial port entries...
            int baseTCPport = 2201;
            for (int i = 0; i < 4; i++)
            {
                List<KeyValuePair<string, string>> siData = new List<KeyValuePair<string, string>>();
                siData.Add(new KeyValuePair<string, string>("PortNum", (i + 1).ToString()));
                siData.Add(new KeyValuePair<string, string>("BaudRate", "57600"));
                siData.Add(new KeyValuePair<string, string>("Databits", "8"));
                siData.Add(new KeyValuePair<string, string>("Stopbits", "1"));
                siData.Add(new KeyValuePair<string, string>("FlowControl", "NONE"));
                siData.Add(new KeyValuePair<string, string>("Parity", "NONE"));
                siData.Add(new KeyValuePair<string, string>("RtsPreTime", "50"));
                siData.Add(new KeyValuePair<string, string>("RtsPostTime", "100"));
                siData.Add(new KeyValuePair<string, string>("PortName", "ttyO"));
                siData.Add(new KeyValuePair<string, string>("TcpPort", (baseTCPport + i).ToString()));
                //Namrata: 14/09/2017
                siData.Add(new KeyValuePair<string, string>("Enable", "NO"));
                opcHandle.getSerialPortConfiguration().getSerialInterfaces().Add(new SerialInterface("Port", siData));
                //Namrata: 14/09/2017
                //Show DI entry in Virtual DI if Enable Mode is "YES".
                if (siData[10].Value == "YES")
                {
                    CreateDI4SerialPort(i + 1);
                }
            }
            opcHandle.getSerialPortConfiguration().refreshList();
        }
        public static void CreateDI4NetworkInterface(int portNo)
        {
            Utils.WriteLine(VerboseLevel.DEBUG, "*** Into CreateDI4NetworkInterface");
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            diData.Add(new KeyValuePair<string, string>("DINo", (Globals.DINo + 1).ToString()));
            diData.Add(new KeyValuePair<string, string>("ResponseType", "LANHealth"));
            diData.Add(new KeyValuePair<string, string>("Index", portNo.ToString()));
            diData.Add(new KeyValuePair<string, string>("SubIndex", "0"));
            diData.Add(new KeyValuePair<string, string>("Description", "LANHealth_" + portNo.ToString()));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().getDIs().Add(new DI("DI", diData, null, MasterTypes.Virtual, 1, 0));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        //Namrata: 27/09/2017
        //Remove Entry From Virtual DI when Active is "NO".
        public static void RemoveDI4IEDNetwork(string mt, int masterNo, int UnitIDOLD, int unitID, string device)
        {
            string responsetype = "LANHealth";
            Utils.WriteLine(VerboseLevel.DEBUG, "*** Into CreateDI4IED");
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDINetwork(responsetype, masterNo, UnitIDOLD, unitID, diData);
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        //Namrata: 27/09/2017
        //Remove Entry From Virtual DI when Active is "NO".
        public static void RemoveDI4IEDserialConfiguratiuon(string mt, int masterNo, int UnitIDOLD, int unitID, string device)
        {
            string responsetype = "UARTHealth";
            Utils.WriteLine(VerboseLevel.DEBUG, "*** Into CreateDI4IED");
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDINetwork(responsetype, masterNo, UnitIDOLD, unitID, diData);
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        public static void CreateDI4SerialPort(int portNo)
        {
            Utils.WriteLine(VerboseLevel.DEBUG, "*** Into CreateDI4SerialPort");
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            diData.Add(new KeyValuePair<string, string>("DINo", (Globals.DINo + 1).ToString()));
            diData.Add(new KeyValuePair<string, string>("ResponseType", "UARTHealth"));
            diData.Add(new KeyValuePair<string, string>("Index", portNo.ToString()));
            diData.Add(new KeyValuePair<string, string>("SubIndex", "0"));
            diData.Add(new KeyValuePair<string, string>("Description", "UARTHealth_" + portNo.ToString()));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().getDIs().Add(new DI("DI", diData, null, MasterTypes.Virtual,1,0));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }

        public static string DINoForPrimaryDevice = "";
        public static int DIIncrementForPrimaryDevice = 1;
        public static string DINoForSecDevice = "";
        public static int DIIncrementForSecDevice = 1;
        public static void RemoveDI4IEDystemPortForPrimaryDevice(string mt, int masterNo, int UnitIDOLD, int unitID, string device)
        {
            string responsetype = "PrimaryDevice";
            Utils.WriteLine(VerboseLevel.DEBUG, "*** Into CreateDI4IED");
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDINetwork(responsetype, masterNo, UnitIDOLD, unitID, diData);
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        public static void CreateDI4SystemPortForPrimaryDevice()
        {
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            diData.Add(new KeyValuePair<string, string>("DINo", (Globals.DINo + 1).ToString()));
            diData.Add(new KeyValuePair<string, string>("ResponseType", "PrimaryDevice"));
            diData.Add(new KeyValuePair<string, string>("Index", "0"));
            diData.Add(new KeyValuePair<string, string>("SubIndex", "0"));
            diData.Add(new KeyValuePair<string, string>("Description", "PrimaryDevice"));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().getDIs().Add(new DI("DI", diData, null, MasterTypes.Virtual, 1, 0));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        public static void CreateDI4SystemPortForPrimaryDeviceAfterXMLImport1()
        {
            var DIListDistictItems = Utils.DiListForVirualDIImport.GroupBy(i => new { i.Index, i.SubIndex, i.ResponseType }).Select(g => g.First()).ToList();

            var ListByAscending = from s in DIListDistictItems
                                orderby s.DINo
                                select s;
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            DINoForPrimaryDevice = ListByAscending.Select(x => x.DINo).LastOrDefault();
            int FinalDINo = (Convert.ToInt32(DINoForPrimaryDevice) + DIIncrementForPrimaryDevice);
            diData.Add(new KeyValuePair<string, string>("DINo", (FinalDINo).ToString()));
            diData.Add(new KeyValuePair<string, string>("ResponseType", "PrimaryDevice"));
            diData.Add(new KeyValuePair<string, string>("Index", "0"));
            diData.Add(new KeyValuePair<string, string>("SubIndex", "0"));
            diData.Add(new KeyValuePair<string, string>("Description", "PrimaryDevice"));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().getDIs().Add(new DI("DI", diData, null, MasterTypes.Virtual, 1, 0));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        public static void CreateDI4SystemPortForSecondaryDeviceAfterXMLImport1()
        {
            var DIListDistictItems = Utils.DiListForVirualDIImport.GroupBy(i => new { i.Index, i.SubIndex, i.ResponseType }).Select(g => g.First()).ToList();

            var ListByAscending = from list in DIListDistictItems orderby list.DINo select list;
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            DINoForPrimaryDevice = ListByAscending.Select(x => x.DINo).LastOrDefault();
            int FinalDINo = (Convert.ToInt32(DINoForPrimaryDevice) + DIIncrementForPrimaryDevice);
            diData.Add(new KeyValuePair<string, string>("DINo", (FinalDINo).ToString()));
            diData.Add(new KeyValuePair<string, string>("ResponseType", "SecDevice"));
            diData.Add(new KeyValuePair<string, string>("Index", "0"));
            diData.Add(new KeyValuePair<string, string>("SubIndex", "0"));
            diData.Add(new KeyValuePair<string, string>("Description", "SecondaryDevice"));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().getDIs().Add(new DI("DI", diData, null, MasterTypes.Virtual, 1, 0));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        //Namrata: 06/09/2017
        //Create DI for Secondary Device in Virtual Master .
        public static void CreateDI4SystemPortForSecondaryDevice()
        {
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            diData.Add(new KeyValuePair<string, string>("DINo", (Globals.DINo + 1).ToString()));
            diData.Add(new KeyValuePair<string, string>("ResponseType", "SecDevice"));
            diData.Add(new KeyValuePair<string, string>("Index", "0"));
            diData.Add(new KeyValuePair<string, string>("SubIndex", "0"));
            diData.Add(new KeyValuePair<string, string>("Description", "SecondaryDevice"));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().getDIs().Add(new DI("DI", diData, null, MasterTypes.Virtual, 1, 0));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        public static void RemoveDI4IEDystemPortForSecondaryDevice(string mt, int masterNo, int UnitIDOLD, int unitID, string device)
        {
            string responsetype = "SecDevice";
            Utils.WriteLine(VerboseLevel.DEBUG, "*** Into CreateDI4IED");
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDINetwork(responsetype, masterNo, UnitIDOLD, unitID, diData);
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        public static void CreateDI4IED(MasterTypes mt, int masterNo, int unitID, string device)
        {
            //IEDUnitID = unitID;
            Utils.WriteLine(VerboseLevel.DEBUG, "*** Into CreateDI4IED");
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            diData.Add(new KeyValuePair<string, string>("DINo", (Globals.DINo + 1).ToString()));
            if (MasterTypes.IEC103 == mt)
                diData.Add(new KeyValuePair<string, string>("ResponseType", "IEDHealthIEC103"));
            else if (MasterTypes.MODBUS == mt)
                diData.Add(new KeyValuePair<string, string>("ResponseType", "IEDHealthMODBUS"));
            //Namarta:11/7/2017
            else if (MasterTypes.ADR == mt)
                diData.Add(new KeyValuePair<string, string>("ResponseType", "IEDHealthADR"));
            else if (MasterTypes.IEC101 == mt)
                diData.Add(new KeyValuePair<string, string>("ResponseType", "IEDHealthIEC101"));
            else if (MasterTypes.IEC61850Client == mt)
                diData.Add(new KeyValuePair<string, string>("ResponseType", "IEDHealthIEC61850"));
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "*** Cannot create entry in Virtual DI as master not supported!!!");
                return;
            }
            IEDUnitID = unitID;
            diData.Add(new KeyValuePair<string, string>("Index", masterNo.ToString()));
            diData.Add(new KeyValuePair<string, string>("SubIndex", unitID.ToString()));
            diData.Add(new KeyValuePair<string, string>("Description", device + "_" + (unitID).ToString()));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().getDIs().Add(new DI("DI", diData, null, MasterTypes.Virtual, 1, 0));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        public static void RemoveDI4IED(MasterTypes mt, int masterNo, int unitID)
        {
            string responseType = "";
            if (MasterTypes.IEC103 == mt)
                responseType = "IEDHealthIEC103";
            else if (MasterTypes.MODBUS == mt)
                responseType = "IEDHealthMODBUS";
            //Namrata:11/7/2017
            else if (MasterTypes.ADR == mt)
                responseType = "IEDHealthADR";
            else if (MasterTypes.IEC101 == mt)
                responseType = "IEDHealthIEC101";
            else if (MasterTypes.Virtual == mt)
                responseType = "IEDHealthIEC61850";
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "*** Cannot remove entry in Virtual DI as master not supported!!!");
                return;
            }
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDI(responseType, masterNo, unitID);
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        public static void CreateDI4MD(int mdIndex)
        {
            Utils.WriteLine(VerboseLevel.DEBUG, "*** Into CreateDI4MD");
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            diData.Add(new KeyValuePair<string, string>("DINo", (Globals.DINo + 1).ToString()));
            diData.Add(new KeyValuePair<string, string>("ResponseType", "MDAlarm"));
            diData.Add(new KeyValuePair<string, string>("Index", mdIndex.ToString()));
            diData.Add(new KeyValuePair<string, string>("SubIndex", "0"));
            diData.Add(new KeyValuePair<string, string>("Description", "MDAlarm_" + mdIndex.ToString()));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().getDIs().Add(new DI("DI", diData, null, MasterTypes.Virtual, 1, 0));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        public static void RemoveDI4MD(int mdIndex)
        {
            string responseType = "MDAlarm";
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDI(responseType, mdIndex, 0);
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        public static void CreateAI4MD(int mdIndex, float multiplier)
        {
            Utils.WriteLine(VerboseLevel.DEBUG, "*** Into CreateAI4MD");
            string[] responseTypes = { "MDSlindingWindow", "MDWindow" };

            foreach (string rt in responseTypes)
            {
                List<KeyValuePair<string, string>> aiData = new List<KeyValuePair<string, string>>();
                aiData.Add(new KeyValuePair<string, string>("AINo", (Globals.AINo + 1).ToString()));
                aiData.Add(new KeyValuePair<string, string>("ResponseType", rt));
                aiData.Add(new KeyValuePair<string, string>("Index", mdIndex.ToString()));
                aiData.Add(new KeyValuePair<string, string>("SubIndex", "0"));
                aiData.Add(new KeyValuePair<string, string>("DataType", "Float"));
                aiData.Add(new KeyValuePair<string, string>("Multiplier", multiplier.ToString()));
                aiData.Add(new KeyValuePair<string, string>("Constant", "0"));
                aiData.Add(new KeyValuePair<string, string>("Description", rt + "_" + mdIndex.ToString()));

                opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().getAIs().Add(new AI("AI", aiData, null, MasterTypes.Virtual, 1, 0));
            }
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().refreshList();
        }
        public static void RemoveAI4MD(int mdIndex)
        {
            string[] responseTypes = { "MDSlindingWindow", "MDWindow" };

            foreach (string rt in responseTypes)
            {
                opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().removeAI(rt, mdIndex, 0);
            }

            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().refreshList();
        }
        public static void CreateDI4CLA(int claIndex)
        {
            Utils.WriteLine(VerboseLevel.DEBUG, "*** Into CreateDI4CLA");

            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            diData.Add(new KeyValuePair<string, string>("DINo", (Globals.DINo + 1).ToString()));
            diData.Add(new KeyValuePair<string, string>("ResponseType", "CLA"));
            diData.Add(new KeyValuePair<string, string>("Index", claIndex.ToString()));
            diData.Add(new KeyValuePair<string, string>("SubIndex", "0"));
            diData.Add(new KeyValuePair<string, string>("Description", "CLA_" + claIndex.ToString()));

            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().getDIs().Add(new DI("DI", diData, null, MasterTypes.Virtual, 1, 0));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        //Ajay: 25/09/2018 commented
        //public static void RemoveDI4CLA(int claIndex)
        //{
        //    string responseType = "CLA";
        //    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDI(responseType, claIndex, 0);
        //    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        //}
        
        //Ajay: 25/09/2018
        public static bool RemoveDI4CLA(int claIndex)
        {
            bool IsAllow = true;
            string responseType = "CLA";
            if (!opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().IsDIExist(responseType, claIndex))
            {
                DialogResult dlg = MessageBox.Show("Associated entry does not exist in Virtual DI. " + Environment.NewLine + "Do you still want to continue deleting Closed loop Action ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dlg.ToString().ToLower() == "yes")
                {
                    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDI(responseType, claIndex, 0);
                    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();

                }
                else { IsAllow = false; }
            }
            else
            {
                opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDI(responseType, claIndex, 0);
                opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
            }
            return IsAllow;
        }
        public static void CreateDI4Profile(int pIndex)
        {
            Utils.WriteLine(VerboseLevel.DEBUG, "*** Into CreateDI4Profile");

            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            diData.Add(new KeyValuePair<string, string>("DINo", (Globals.DINo + 1).ToString()));
            diData.Add(new KeyValuePair<string, string>("ResponseType", "Profile"));
            diData.Add(new KeyValuePair<string, string>("Index", pIndex.ToString()));
            diData.Add(new KeyValuePair<string, string>("SubIndex", "0"));
            diData.Add(new KeyValuePair<string, string>("Description", "Profile_" + pIndex.ToString()));

            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().getDIs().Add(new DI("DI", diData, null, MasterTypes.Virtual, 1, 0));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        //Ajay: 25/09/2018 Commented
        //public static void RemoveDI4Profile(int pIndex)
        //{
        //    string responseType = "Profile";
        //    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDI(responseType, pIndex, 0);
        //    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        //}

        //Ajay: 25/09/2018
        public static bool RemoveDI4Profile(int pIndex)
        {
            bool IsAllow = true;
            string responseType = "Profile";
            if (!opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().IsDIExist(responseType, pIndex))
            {
                DialogResult dlg = MessageBox.Show("Associated entry does not exist in Virtual DI. " + Environment.NewLine + "Do you still want to continue deleting Profile Record?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dlg.ToString().ToLower() == "yes")
                {
                    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDI(responseType, pIndex, 0);
                    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();

                }
                else { IsAllow = false; }
            }
            else
            {
                opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDI(responseType, pIndex, 0);
                opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
            }
            return IsAllow;
        }
        //Ajay: 25/09/2018
        public static bool RemoveDI4MDCalculation(int pIndex)
        {
            bool IsAllow = true;
            string responseType = "";
            DialogResult dlg;
            responseType = "MDAlarm";
            if (!opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().IsDIExist(responseType, pIndex))
            {
                dlg = MessageBox.Show("Associated entry does not exist in Virtual DI. " + Environment.NewLine + "Do you still want to continue deleting MD Calculation?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dlg.ToString().ToLower() == "yes")
                {
                    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDI(responseType, pIndex, 0);
                    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
                    
                }
                else { IsAllow = false; }
            }
            else
            {
                opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDI(responseType, pIndex, 0);
                opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
            }
            if (IsAllow)
            {
                responseType = "MDSlindingWindow";
                if (!opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().IsAIExist(responseType, pIndex))
                {
                    dlg = MessageBox.Show("Associated entry does not exist in Virtual AI. " + Environment.NewLine + "Do you still want to continue deleting MD Calculation?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dlg.ToString().ToLower() == "yes")
                    {
                        opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().removeAI(responseType, pIndex, 0);
                        opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().refreshList();
                    }
                    else { IsAllow = false; }
                }
                else
                {
                    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().removeAI(responseType, pIndex, 0);
                    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().refreshList();
                }
            }
            if (IsAllow)
            {
                responseType = "MDWindow";
                if (!opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().IsAIExist(responseType, pIndex))
                {
                    dlg = MessageBox.Show("Associated entry does not exist in Virtual AI. " + Environment.NewLine + "Do you still want to continue deleting MD Calculation?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dlg.ToString().ToLower() == "yes")
                    {
                        opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().removeAI(responseType, pIndex, 0);
                        opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().refreshList();
                    }
                    else { IsAllow = false; }
                }
                else
                {
                    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().removeAI(responseType, pIndex, 0);
                    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().refreshList();
                }
            }
            return IsAllow;
            //responseType = "MDSlindingWindow";
            //if (!opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().IsAIExist(responseType, pIndex))
            //{
            //    dlg = MessageBox.Show("Corresponding entry does not exist in Virtual AI. " + Environment.NewLine + "Do you still want to continue deleting MD Calculation?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            //    if (dlg.ToString().ToLower() == "yes")
            //    {
            //        responseType = "MDSlindingWindow";
            //        opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().removeAI(responseType, pIndex, 0);
            //        //opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().refreshList();
            //        responseType = "MDWindow";
            //        opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().removeAI(responseType, pIndex, 0);
            //        opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().refreshList();
            //    }
            //    else { }
            //}
        }
        public static void CreateDI4DD(int ddIndex)
        {
            Utils.WriteLine(VerboseLevel.DEBUG, "*** Into CreateDI4DD");
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            diData.Add(new KeyValuePair<string, string>("DINo", (Globals.DINo + 1).ToString()));
            diData.Add(new KeyValuePair<string, string>("ResponseType", "DerivedDI"));
            diData.Add(new KeyValuePair<string, string>("Index", ddIndex.ToString()));
            diData.Add(new KeyValuePair<string, string>("SubIndex", "0"));
            diData.Add(new KeyValuePair<string, string>("Description", "DerivedDI_" + ddIndex.ToString()));

            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().getDIs().Add(new DI("DI", diData, null, MasterTypes.Virtual, 1, 0));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        //Ajay: 25/09/2018 commented
        //public static void RemoveDI4DD(int ddIndex)
        //{
        //    string responseType = "DerivedDI";
        //    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDI(responseType, ddIndex, 0);
        //    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        //}
        //Ajay: 25/09/2018
        public static bool RemoveDI4DD(int ddIndex)
        {
            string responseType = "DerivedDI";
            //opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDI(responseType, ddIndex, 0);
            //opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
            bool IsAllow = true;
            if (!opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().IsDIExist(responseType, ddIndex))
            {
                DialogResult dlg = MessageBox.Show("Associated entry does not exist in Virtual DI. " + Environment.NewLine + "Do you still want to continue deleting Derived DI?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dlg.ToString().ToLower() == "yes")
                {
                    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDI(responseType, ddIndex, 0);
                    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();

                }
                else { IsAllow = false; }
            }
            else
            {
                opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().removeDI(responseType, ddIndex, 0);
                opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
            }
            return IsAllow;
        }
        //Namrata: 15/6/2017
        //update Attributes in Virual DI 
        public static void UpdateDI4IED(MasterTypes mt, int masterNo, int UnitIDOLD, int unitID, string device)
        {
            string responsetype;
            Utils.WriteLine(VerboseLevel.DEBUG, "*** Into CreateDI4IED");
            List<KeyValuePair<string, string>> diData = new List<KeyValuePair<string, string>>();
            if (MasterTypes.IEC103 == mt)
            {
                responsetype = "IEDHealthIEC103";
                diData.Add(new KeyValuePair<string, string>("ResponseType", responsetype));
            }
            else if (MasterTypes.MODBUS == mt)
            {
                responsetype = "IEDHealthMODBUS";
                diData.Add(new KeyValuePair<string, string>("ResponseType", responsetype));
            }
            //Namrata:15/6/2017
            else if (MasterTypes.ADR == mt)
            {
                responsetype = "IEDHealthADR";
                diData.Add(new KeyValuePair<string, string>("ResponseType", responsetype));
            }
            //Namrata:14/7/2017
            else if (MasterTypes.IEC101 == mt)
            {
                responsetype = "IEDHealthIEC101";
                diData.Add(new KeyValuePair<string, string>("ResponseType", responsetype));
            }
            else if (MasterTypes.IEC61850Client == mt)
            {
                responsetype = "IEDHealthIEC61850";
                diData.Add(new KeyValuePair<string, string>("ResponseType", responsetype));
            }
            else
            {
                Utils.WriteLine(VerboseLevel.WARNING, "*** Cannot create entry in Virtual DI as master not supported!!!");
                return;
            }
            diData.Add(new KeyValuePair<string, string>("Index", masterNo.ToString()));
            diData.Add(new KeyValuePair<string, string>("SubIndex", unitID.ToString()));
            diData.Add(new KeyValuePair<string, string>("Description", device + "_" + (unitID).ToString()));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().UpdateDI(responsetype, masterNo, UnitIDOLD, unitID, diData);
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getDIConfiguration().refreshList();
        }
        public static void CreateAI4DP(int dpIndex)
        {
            Utils.WriteLine(VerboseLevel.DEBUG, "*** Into CreateAI4DP");
            List<KeyValuePair<string, string>> aiData = new List<KeyValuePair<string, string>>();
            aiData.Add(new KeyValuePair<string, string>("AINo", (Globals.AINo + 1).ToString()));
            aiData.Add(new KeyValuePair<string, string>("ResponseType", "DerivedParam"));
            aiData.Add(new KeyValuePair<string, string>("Index", dpIndex.ToString()));
            aiData.Add(new KeyValuePair<string, string>("SubIndex", "0"));
            aiData.Add(new KeyValuePair<string, string>("DataType", "Float"));
            aiData.Add(new KeyValuePair<string, string>("Multiplier", "1"));
            aiData.Add(new KeyValuePair<string, string>("Constant", "0"));
            aiData.Add(new KeyValuePair<string, string>("Description", "DerivedParam_" + dpIndex.ToString()));

            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().getAIs().Add(new AI("AI", aiData, null, MasterTypes.Virtual, 1, 0));
            opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().refreshList();
        }
        //Ajay: 25/09/2018 commented
        //public static void RemoveAI4DP(int dpIndex)
        //{
        //    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().removeAI("DerivedParam", dpIndex, 0);
        //    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().refreshList();
        //}
        //Ajay: 25/09/2018
        public static bool RemoveAI4DP(int dpIndex)
        {
            //opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().removeAI("DerivedParam", dpIndex, 0);
            //opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().refreshList();
            bool IsAllow = true;
            string responseType = "DerivedParam";
            if (!opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().IsAIExist(responseType, dpIndex))
            {
                DialogResult dlg = MessageBox.Show("Associated entry does not exist in Virtual AI. " + Environment.NewLine + "Do you still want to continue deleting Derived Parameter?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dlg.ToString().ToLower() == "yes")
                {
                    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().removeAI(responseType, dpIndex, 0);
                    opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().refreshList();

                }
                else { IsAllow = false; }
            }
            else
            {
                opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().removeAI(responseType, dpIndex, 0);
                opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters()[0].getIEDs()[0].getAIConfiguration().refreshList();
            }
            return IsAllow;
        }
        public static void allowNumbersOnly(object sender, KeyPressEventArgs e, bool supportNegative, bool supportDecimal)
        {
            int position = (sender as TextBox).SelectionStart;

            if (!(char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back
                || (e.KeyChar == '-' && supportNegative) || (e.KeyChar == '.' && supportDecimal)))
                e.Handled = true;

            //Do not accept any character b4 '-'sign...
            if (supportNegative && (sender as TextBox).Text.IndexOf('-') > -1 && position == 0) e.Handled = true;

            //Only one -ve sign, at start ONLY and when negative value is supported...
            if (e.KeyChar == '-' && (!supportNegative || position != 0 || (sender as TextBox).Text.IndexOf('-') > -1)) e.Handled = true;

            //Only allow one decimal point
            if (supportDecimal && e.KeyChar == '.' && ((sender as TextBox).Text.IndexOf('.') > -1)) e.Handled = true;
        }
        public static void allowIP(object sender, KeyPressEventArgs e)
        {
            int position = (sender as TextBox).SelectionStart;

            if (!(char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back
                || e.KeyChar == '.'))
                e.Handled = true;

            //First char cannot be '.'
            if (e.KeyChar == '.' && position == 0) e.Handled = true;

            //There cannot consecutive '.', so start verifying from second char...
            if (e.KeyChar == '.' && position > 0 && (sender as TextBox).Text.ElementAt(position - 1) == '.') e.Handled = true;

            //Only allow three decimal points
            if (e.KeyChar == '.' && getOccurences((sender as TextBox).Text, '.') >= 3) e.Handled = true;
        }
        public static int getOccurences(string str, char srch)
        {
            if (str == null || str.Length <= 0) return 0;

            string[] splitValues = str.Split(srch);
            return splitValues.Length - 1;
        }
        public static bool IsValidIPv4(string ipString)
        {
            try
            {
                if (ipString == null || ipString.Length == 0)
                {
                    return false;
                }

                String[] parts = ipString.Split(new[] { "." }, StringSplitOptions.None);
                if (parts.Length != 4)
                {
                    return false;
                }

                foreach (String s in parts)
                {
                    int i = Int32.Parse(s);
                    if ((i < 0) || (i > 255))
                    {
                        return false;
                    }
                }
                if (ipString.EndsWith("."))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            //if (String.IsNullOrWhiteSpace(ipString))
            //{
            //    return false;
            //}

            //string[] splitValues = ipString.Split('.');
            //if (splitValues.Length != 4)
            //{
            //    return false;
            //}

            //byte tempForParsing;

            //return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }
        public static bool IsValidTCPPort(int tcpPort)
        {
            if (tcpPort <= 0) return false;

            return true;
        }
        public static bool IsValidSubnet(string snmStr)
        {
            // checks if the address is all leading ones followed by only zeroes
            IPAddress snm;
            if (!IPAddress.TryParse(snmStr, out snm))
            {
                return false;
            }

            byte[] ipOctets = snm.GetAddressBytes();
            bool restAreOnes = false;
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    bool bitValue = (ipOctets[i] >> j & 1) == 1;
                    if (restAreOnes && !bitValue)
                        return false;
                    restAreOnes = bitValue;
                }
            }
            return true;
        }
        public static void UncheckOthers(ListView lv, int ignoreIndex)
        {
            if (ignoreIndex < 0 || lv.Items.Count <= 0) return;

            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (i == ignoreIndex) continue;
                if (lv.Items[i].Checked) lv.Items[i].Checked = false;
            }
        }
        public static ColumnHeader getColumnHeader(ListView lv, string hdrName)
        {
            if (String.IsNullOrWhiteSpace(hdrName) || lv == null || lv.Columns.Count <= 0) return null;
            foreach (ColumnHeader ch in lv.Columns)
            {
                if (ch.Text.ToLower() == hdrName.ToLower()) return ch;
            }
            return null;
        }
        public static SlaveTypes getSlaveTypes(string slaveID)
        {
            //Namrata:6/7/2017
            if (slaveID.Contains("IEC104_")) return SlaveTypes.IEC104;
            else if (slaveID.Contains("MODBUSSlave_")) return SlaveTypes.MODBUSSLAVE;
            else if (slaveID.Contains("IEC101Slave_")) return SlaveTypes.IEC101SLAVE;
            else if (slaveID.Contains("IEC61850Server_")) return SlaveTypes.IEC61850Server;
            //else if (slaveID.Contains("IEC61850ServerGroup_")) return SlaveTypes.IEC61850Server;

            else return SlaveTypes.UNKNOWN;
        }
        public static string GetDataTypeShortNotation(string dType)
        {
            string sNotation = "UK";//Say, Unknown
            if (dType == "ShortFloatingPoint") sNotation = "MF";
            else if (dType == "IntegratedTotals") sNotation = "IT";
            else if (dType == "SinglePoint") sNotation = "SP";
            else if (dType == "DoublePoint") sNotation = "DP";
            else if (dType == "SingleCommand") sNotation = "SC";
            else if (dType == "DoubleCommand") sNotation = "DC";
            //else sNotation = "MF";

            return sNotation;
        }
        public static int GetDataTypeIndex(string dType)
        {
            int iIndex = -1;//Say, Unknown
            if (dType == "ShortFloatingPoint") iIndex = 2;
            else if (dType == "IntegratedTotals") iIndex = 3;
            else if (dType == "SinglePoint") iIndex = 0;
            else if (dType == "DoublePoint") iIndex = 1;
            else if (dType == "SingleCommand") iIndex = 0;
            else if (dType == "DoubleCommand") iIndex = 1;
            return iIndex;
        }
        public static bool ChangeKey<TKey, TValue>(IDictionary<TKey, TValue> dict, TKey oldKey, TKey newKey)
        {
            TValue value;
            if (!dict.TryGetValue(oldKey, out value))
                return false;
            dict.Remove(oldKey);  // do not change order
            dict[newKey] = value;  // or dict.Add(newKey, value) depending on ur comfort
            return true;
        }
        public static void handleMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }
        public static void handleMouseMove(Control ctrl, object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ctrl.Left = e.X + ctrl.Left - MouseDownLocation.X;
                ctrl.Top = e.Y + ctrl.Top - MouseDownLocation.Y;
            }
        }
        public static void SaveIEDFile(string iedData)
        {
            sfdFile.Filter = "XML Files|*.xml";
            if (sfdFile.ShowDialog() == DialogResult.OK)
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "*** Saving to file: {0}", sfdFile.FileName);
                writeIEDFile(sfdFile.FileName, iedData);
                //Ajay: 21/11/2017 Show message box with file path  
                MessageBox.Show("\"" + sfdFile.FileName + "\" saved successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //Namrata:02/06/2017
        public static void SaveIEDExcelFile(string ieddatat1)
        {
            sfdFile.Filter = "XLS Files |*.xls";
            if (sfdFile.ShowDialog() == DialogResult.OK)
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "*** Saving to file: {0}", sfdFile.FileName);
                writeIEDFile(sfdFile.FileName, ieddatat1);
                //Ajay: 21/11/2017 Show message box with file path
                MessageBox.Show("\"" + sfdFile.FileName + "\" saved successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public static void writeIEDFile1(string wFile1, string ieddatat1)
        {
            File.WriteAllText(wFile1, ieddatat1);
        }
        public static void writeIEDFile(string wFile, string iedData)
        {
            File.WriteAllText(wFile, iedData);
        }
        public static void WriteLine(VerboseLevel level, string format, params object[] parameters)
        {
            if (level >= Globals.Level)
                Console.WriteLine(format, parameters);
        }
        public static void Write(VerboseLevel level, string format, params object[] parameters)
        {
            if (level >= Globals.Level)
                Console.Write(format, parameters);
        }
        public static void resetValues(Control ctrlgrp)
        {
            foreach (Control ctrl in ctrlgrp.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    Console.WriteLine("*** Got textbox: {0} tag {1}", ctrl.Name, ctrl.Tag);
                    ((TextBox)ctrl).Text = "";
                }
                else if (ctrl.GetType() == typeof(ComboBox))
                {
                    Console.WriteLine("*** Got ComboBox: {0} tag {1}", ctrl.Name, ctrl.Tag);
                    if (((ComboBox)ctrl).Items.Count > 0) ((ComboBox)ctrl).SelectedIndex = 0;
                }
                else if (ctrl.GetType() == typeof(CheckBox))
                {
                    Console.WriteLine("*** Got CheckBox: {0} tag {1}", ctrl.Name, ctrl.Tag);
                    ((CheckBox)ctrl).Checked = false;
                }
                else
                {
                    Console.WriteLine("*** Control Type {0} not supported!!!", ctrl.GetType());
                }
            }
        }
        public static void showNavigation(Control ctrlgrp, bool show)
        {
            if (ctrlgrp.Controls["btnFirst"] != null) ((Button)ctrlgrp.Controls["btnFirst"]).Visible = show;
            if (ctrlgrp.Controls["btnPrev"] != null) ((Button)ctrlgrp.Controls["btnPrev"]).Visible = show;
            if (ctrlgrp.Controls["btnNext"] != null) ((Button)ctrlgrp.Controls["btnNext"]).Visible = show;
            if (ctrlgrp.Controls["btnLast"] != null) ((Button)ctrlgrp.Controls["btnLast"]).Visible = show;
        }
        public static bool IsEmptyFields(Control ctrlgrp)
        {
            foreach (Control ctrl in ctrlgrp.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    if (((TextBox)ctrl).Text.Trim() == "") return true;
                }
            }
            return false;
        }
        public static List<KeyValuePair<string, string>> getKeyValueAttributes(Control ctrlgrp)
        {
            List<KeyValuePair<string, string>> kvpData = new List<KeyValuePair<string, string>>();
            foreach (Control ctrl in ctrlgrp.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    Console.WriteLine("*** Got textbox: {0} tag {1}", ctrl.Name, ctrl.Tag);
                    if (ctrl.Tag != null && ctrl.Tag.ToString() != "") kvpData.Add(new KeyValuePair<string, string>(((TextBox)ctrl).Tag.ToString(), ((TextBox)ctrl).Text));
                }
                else if (ctrl.GetType() == typeof(ComboBox))
                {
                    Console.WriteLine("*** Got ComboBox: {0} tag {1}", ctrl.Name, ctrl.Tag);
                    if (ctrl.Tag != null && ctrl.Tag.ToString() != "") kvpData.Add(new KeyValuePair<string, string>(((ComboBox)ctrl).Tag.ToString(), ((ComboBox)ctrl).GetItemText(((ComboBox)ctrl).SelectedItem)));
                }
                else if (ctrl.GetType() == typeof(CheckBox))
                {
                    Console.WriteLine("*** Got CheckBox: {0} tag {1}", ctrl.Name, ctrl.Tag);
                    //Handle special case where for some we need 'YES/NO' values n for some 'ENABLE/DISABLE'
                    if (ctrl.Tag != null && ctrl.Tag.ToString() != "")
                    {
                        string[] elems = ctrl.Tag.ToString().Split('_');
                        if (elems.Length == 3)
                        {
                            kvpData.Add(new KeyValuePair<string, string>(elems[0], (((CheckBox)ctrl).Checked == true ? elems[1] : elems[2])));
                        }
                        else
                        {
                            kvpData.Add(new KeyValuePair<string, string>(elems[0], (((CheckBox)ctrl).Checked == true ? "ENABLE" : "DISABLE")));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("*** Control Type {0} not supported!!!", ctrl.GetType());
                }
            }
            return kvpData;
        }
        public static List<string> getKeyPathArray(TreeNode tn)
        {
            TreeNode tmp = tn;
            List<string> kp = new List<string>();

            while (tmp != null)
            {
                Console.WriteLine("*** Treenode index: {0}", tmp.Index);
                kp.Add(tmp.Name + "_" + tmp.Index);
                tmp = tmp.Parent;
            }

            kp.Reverse();
            Console.WriteLine("########### key path size: {0}", kp.Count);
            foreach (string kps in kp)
            {
                int i = 0;
                Console.WriteLine("### i: {0} val: '{1}'", i++, kps);
            }

            return kp;
        }
        public static List<string> getKeyPathArray1(string tn)
        {
            string tmp = tn;
            List<string> kp = new List<string>();

            while (tmp != null)
            {
                Console.WriteLine("*** Treenode index: {0}", tmp.ToString());
                kp.Add(tmp.ToString());// + "_" + tmp.Index);
                tmp = tmp.ToString();
            }

            kp.Reverse();
            Console.WriteLine("########### key path size: {0}", kp.Count);
            foreach (string kps in kp)
            {
                int i = 0;
                Console.WriteLine("### i: {0} val: '{1}'", i++, kps);
            }

            return kp;
        }
        public static void createPBTitleBar(PictureBox pbHdr, Label lblHdrText, Point ptTitleLoc)
        {
            ptTitleLoc = pbHdr.PointToClient(ptTitleLoc);
            lblHdrText.Parent = pbHdr;
            lblHdrText.Location = ptTitleLoc;
            lblHdrText.BackColor = Color.Transparent;

            //Set Gradiency to picture box...
            Bitmap bm = new Bitmap(pbHdr.Width, pbHdr.Height);
            LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new PointF(0, 0), new PointF(pbHdr.Width, 0), System.Drawing.Color.Blue, System.Drawing.Color.LightSteelBlue);
            Graphics gr = Graphics.FromImage(bm);
            gr.FillRectangle(brush, new RectangleF(0, 0, pbHdr.Width, pbHdr.Height));
            pbHdr.BackgroundImage = bm;
        }
        public static void highlightListviewItem(string key, ListView lvdm)
        {
            for (int i = 0; i < lvdm.Items.Count; i++)
            {

                if (key == lvdm.Items[i].Text)
                {
                    lvdm.HideSelection = true;
                    lvdm.Focus();
                    lvdm.FocusedItem = lvdm.Items[i];
                    lvdm.Items[i].Selected = true;

                    //lvdm.HideSelection = false;
                    //lvdm.Items[i].BackColor = SystemColors.HighlightText;
                    //lvdm.Items[i].BackColor = Color.Yellow;
                    lvdm.Items[i].EnsureVisible();

                    /* Don't use .selected above as it will be overriden by OS. 
                     * Now question is how to remove backcolor ;-) as in case of 'selected' property
                     * we call .clear()
                    lvdm.Items[i].BackColor = Color.Red;
                     */
                    break;
                }
            }
        }
        //Namrata:10/6/2017
        public static void highlightListviewMapItem(string Mapkey, ListView lvmap)
        {
            for (int i = 0; i < lvmap.Items.Count; i++)
            {
                if (Mapkey == lvmap.Items[i].Text)
                {
                    lvmap.HideSelection = true;
                    lvmap.Focus();
                    lvmap.FocusedItem = lvmap.Items[i];
                    lvmap.Items[i].Selected = true;
                    lvmap.Items[i].EnsureVisible();
                    break;
                }
            }
        }
        public static int GenerateIndex(string element, int dataTypeIndex, int reportingIndex)
        {
            int retVal = 0;
            if (dataTypeIndex > 7)
            {
                Console.WriteLine("*** dataTypeIndex cannot be > 7, as we have only 3 bits.");
                return retVal;
            }

            byte idxByte = (byte)((dataTypeIndex << 5) & 0xFF);
            //Console.WriteLine("*** idxByte: {0}", idxByte.ToString("X2"));

            retVal = reportingIndex;
            //Console.WriteLine("*** retVal: {0}", retVal.ToString("X8"));
            retVal = (idxByte << 24) | retVal;
            Console.WriteLine("*** retVal: {0} {1}", retVal.ToString("X8"), retVal);
            return retVal;
        }
        public static int GetReportingIndex(string slaveNum, string slaveID, string dataPoint, int value)
        {
            int ret = 0;
            if (value <= 0) return ret;
            //Loop thru IEC101...
            foreach (IEC101Master iec101 in opcHandle.getMasterConfiguration().getIEC101Group().getIEC101Masters())
            {
                foreach (IED ied in iec101.getIEDs())
                {
                    if (dataPoint == "AI")
                        ret = ied.getAIConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    //Namrata: 24/11/2017
                    else if (dataPoint == "AO")
                        ret = ied.getAOConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "DI")
                        ret = ied.getDIConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "DO")
                        ret = ied.getDOConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "EN")
                        ret = ied.getENConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    if (ret > 0) return ret;
                }
            }

            //Loop thru ADR...
            foreach (ADRMaster adrmaster in opcHandle.getMasterConfiguration().getADRMasterGroup().getADRMasters())
            {
                foreach (IED ied in adrmaster.getIEDs())
                {
                    if (dataPoint == "AI")
                        ret = ied.getAIConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    //Namrata: 24/11/2017
                    else if (dataPoint == "AO")
                        ret = ied.getAOConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "DI")
                        ret = ied.getDIConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "DO")
                        ret = ied.getDOConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "EN")
                        ret = ied.getENConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    if (ret > 0) return ret;
                }
            }
            //Loop thru IEC103...
            foreach (IEC103Master iec103 in opcHandle.getMasterConfiguration().getIEC103Group().getIEC103Masters())
            {
                foreach (IED ied in iec103.getIEDs())
                {
                    if (dataPoint == "AI")
                        ret = ied.getAIConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    //Namrata: 24/11/2017
                    else if (dataPoint == "AO")
                        ret = ied.getAOConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "DI")
                        ret = ied.getDIConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "DO")
                        ret = ied.getDOConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "EN")
                        ret = ied.getENConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    if (ret > 0) return ret;
                }
            }

            //Loop thru MODBUS...
            foreach (MODBUSMaster mbm in opcHandle.getMasterConfiguration().getMODBUSGroup().getMODBUSMasters())
            {
                foreach (IED ied in mbm.getIEDs())
                {
                    if (dataPoint == "AI")
                        ret = ied.getAIConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    //Namrata: 24/11/2017
                    else if (dataPoint == "AO")
                        ret = ied.getAOConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "DI")
                        ret = ied.getDIConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "DO")
                        ret = ied.getDOConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "EN")
                        ret = ied.getENConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    if (ret > 0) return ret;
                }
            }

            //Loop thru Virtual...
            foreach (VirtualMaster vm in opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters())
            {
                foreach (IED ied in vm.getIEDs())
                {
                    if (dataPoint == "AI")
                        ret = ied.getAIConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    //Namrata: 24/11/2017
                    else if (dataPoint == "AO")
                        ret = ied.getAOConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "DI")
                        ret = ied.getDIConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "DO")
                        ret = ied.getDOConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    else if (dataPoint == "EN")
                        ret = ied.getENConfiguration().GetReportingIndex(slaveNum, slaveID, value);
                    if (ret > 0) return ret;
                }
            }
            return ret;
        }
        public static string GenerateShortUniqueKey()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString.Replace("/", "");
            return GuidString;
        }
        public static bool IsXMLWellFormed(string xmlFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(xmlFile);
            }
            catch (XmlException)
            {
                return false;//Not Well-formed XML...
            }
            catch (ArgumentNullException)
            {
                return false;//Empty filename...
            }
            return true;
        }
        public static bool IsXMLValid(string xmlFile, string xsdFile, ValidationEventHandler vehFunc)
        {
            XmlSchemaSet schemaSet = new XmlSchemaSet();  //Load the XmlSchemaSet.
            try
            {
                //Namrata:10/7/ 2017
                schemaSet.Add(null, xsdFile);
                //schemaSet.Add(Globals.PROJECT_ICON, xsdFile);
            }
            catch (XmlSchemaException e)
            {
                Console.WriteLine("*** XmlSchemaException: Msg: {0}\nSource: {1}", e.Message, e.Source);
                return false;
            }
            XmlSchema compiledSchema = null;
            foreach (XmlSchema schema in schemaSet.Schemas())
            {
                compiledSchema = schema;
            }
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(compiledSchema);
            settings.ValidationEventHandler += new ValidationEventHandler(vehFunc);
            settings.ValidationType = ValidationType.Schema;
            XmlReader vreader = XmlReader.Create(xmlFile, settings);    //Create the schema validating reader.
            while (vreader.Read()) { }
            vreader.Close();  //Close the reader.
            return true;
        }
        public static bool IsGreaterThanZero(string val)
        {
            bool status = true;
            try
            {
                if (val == null || val == "" || Int32.Parse(val) <= 0)
                {
                    status = false;
                }
            }
            catch (System.FormatException)
            {
                status = false;
            }
            return status;
        }
        public static bool IsLessThanZero(string val)
        {
            bool status = true;
            try
            {
                if (val == null || val == "" || Int32.Parse(val) >= 0)
                {
                    status = false;
                }
            }
            catch (System.FormatException)
            {
                status = false;
            }
            return status;
        }
        public static bool IsLessThanEqual2Zero(string val)
        {
            bool status = true;
            try
            {
                if (val == null || val == "" || Int32.Parse(val) > 0)
                {
                    status = false;
                }
            }
            catch (System.FormatException)
            {
                status = false;
            }
            return status;
        }
        public static bool IsValidAO(string ao)
        {
            if (ao == null || ao == "" || IsLessThanEqual2Zero(ao)) return false;
            //Loop thru IEC101...
            foreach (IEC101Master iec101 in opcHandle.getMasterConfiguration().getIEC101Group().getIEC101Masters())
            {
                foreach (IED ied in iec101.getIEDs())
                {
                    foreach (AO ain in ied.getAOConfiguration().getAOs())
                    {
                        if (ain.AONo == ao)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "AO found in IEC101...");
                            Console.WriteLine("AO found in IEC101...");
                            return true;
                        }
                    }
                }
            }
            //Loop thru ADR...
            foreach (ADRMaster iec101 in opcHandle.getMasterConfiguration().getADRMasterGroup().getADRMasters())
            {
                foreach (IED ied in iec101.getIEDs())
                {
                    foreach (AO ain in ied.getAOConfiguration().getAOs())
                    {
                        if (ain.AONo == ao)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "AO found in ADR...");
                            Console.WriteLine("AO found in ADR...");
                            return true;
                        }
                    }
                }
            }
            //Loop thru IEC103...
            foreach (IEC103Master iec103 in opcHandle.getMasterConfiguration().getIEC103Group().getIEC103Masters())
            {
                foreach (IED ied in iec103.getIEDs())
                {
                    foreach (AO ain in ied.getAOConfiguration().getAOs())
                    {
                        if (ain.AONo == ao)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "AO found in IEC103...");
                            Console.WriteLine("AO found in IEC103...");
                            return true;
                        }
                    }
                }
            }
            //Loop thru MODBUS...
            foreach (MODBUSMaster mbm in opcHandle.getMasterConfiguration().getMODBUSGroup().getMODBUSMasters())
            {
                foreach (IED ied in mbm.getIEDs())
                {
                    foreach (AO ain in ied.getAOConfiguration().getAOs())
                    {
                        if (ain.AONo == ao)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "AO found in MODBUS...");
                            Console.WriteLine("AO found in MODBUS...");
                            return true;
                        }
                    }
                }
            }

            //Loop thru Virtual...
            foreach (VirtualMaster vm in opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters())
            {
                foreach (IED ied in vm.getIEDs())
                {
                    foreach (AO ain in ied.getAOConfiguration().getAOs())
                    {
                        if (ain.AONo == ao)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "AO found in Virtual...");
                            Console.WriteLine("AO found in Virtual...");
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool IsValidAI(string ai)
        {
            if (ai == null || ai == "" || IsLessThanEqual2Zero(ai)) return false;
            //Loop thru IEC101...
            foreach (IEC101Master iec101 in opcHandle.getMasterConfiguration().getIEC101Group().getIEC101Masters())
            {
                foreach (IED ied in iec101.getIEDs())
                {
                    foreach (AI ain in ied.getAIConfiguration().getAIs())
                    {
                        if (ain.AINo == ai)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "AI found in IEC101...");
                            Console.WriteLine("AI found in IEC101...");
                            return true;
                        }
                    }
                }
            }
            //Loop thru ADR...
            foreach (ADRMaster iec101 in opcHandle.getMasterConfiguration().getADRMasterGroup().getADRMasters())
            {
                foreach (IED ied in iec101.getIEDs())
                {
                    foreach (AI ain in ied.getAIConfiguration().getAIs())
                    {
                        if (ain.AINo == ai)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "AI found in ADR...");
                            Console.WriteLine("AI found in ADR...");
                            return true;
                        }
                    }
                }
            }
            //Loop thru IEC103...
            foreach (IEC103Master iec103 in opcHandle.getMasterConfiguration().getIEC103Group().getIEC103Masters())
            {
                foreach (IED ied in iec103.getIEDs())
                {
                    foreach (AI ain in ied.getAIConfiguration().getAIs())
                    {
                        if (ain.AINo == ai)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "AI found in IEC103...");
                            Console.WriteLine("AI found in IEC103...");
                            return true;
                        }
                    }
                }
            }
            //Loop thru MODBUS...
            foreach (MODBUSMaster mbm in opcHandle.getMasterConfiguration().getMODBUSGroup().getMODBUSMasters())
            {
                foreach (IED ied in mbm.getIEDs())
                {
                    foreach (AI ain in ied.getAIConfiguration().getAIs())
                    {
                        if (ain.AINo == ai)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "AI found in MODBUS...");
                            Console.WriteLine("AI found in MODBUS...");
                            return true;
                        }
                    }
                }
            }

            //Loop thru Virtual...
            foreach (VirtualMaster vm in opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters())
            {
                foreach (IED ied in vm.getIEDs())
                {
                    foreach (AI ain in ied.getAIConfiguration().getAIs())
                    {
                        if (ain.AINo == ai)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "AI found in Virtual...");
                            Console.WriteLine("AI found in Virtual...");
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static bool IsValidDI(string di)
        {
            if (di == null || di == "" || IsLessThanEqual2Zero(di)) return false;
            //Loop thru IEC101...
            foreach (IEC101Master iec101 in opcHandle.getMasterConfiguration().getIEC101Group().getIEC101Masters())
            {
                foreach (IED ied in iec101.getIEDs())
                {
                    foreach (DI din in ied.getDIConfiguration().getDIs())
                    {
                        if (din.DINo == di)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "DI found in IEC103...");
                            Console.WriteLine("DI found in IEC103...");
                            return true;
                        }
                    }
                }
            }
            //Loop thru ADR...
            foreach (ADRMaster iec103 in opcHandle.getMasterConfiguration().getADRMasterGroup().getADRMasters())
            {
                foreach (IED ied in iec103.getIEDs())
                {
                    foreach (DI din in ied.getDIConfiguration().getDIs())
                    {
                        if (din.DINo == di)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "DI found in IEC103...");
                            Console.WriteLine("DI found in IEC103...");
                            return true;
                        }
                    }
                }
            }
            //Loop thru IEC103...
            foreach (IEC103Master iec103 in opcHandle.getMasterConfiguration().getIEC103Group().getIEC103Masters())
            {
                foreach (IED ied in iec103.getIEDs())
                {
                    foreach (DI din in ied.getDIConfiguration().getDIs())
                    {
                        if (din.DINo == di)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "DI found in IEC103...");
                            Console.WriteLine("DI found in IEC103...");
                            return true;
                        }
                    }
                }
            }

            //Loop thru MODBUS...
            foreach (MODBUSMaster mbm in opcHandle.getMasterConfiguration().getMODBUSGroup().getMODBUSMasters())
            {
                foreach (IED ied in mbm.getIEDs())
                {
                    foreach (DI din in ied.getDIConfiguration().getDIs())
                    {
                        if (din.DINo == di)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "DI found in MODBUS...");
                            Console.WriteLine("DI found in MODBUS...");
                            return true;
                        }
                    }
                }
            }

            //Loop thru Virtual...
            foreach (VirtualMaster vm in opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters())
            {
                foreach (IED ied in vm.getIEDs())
                {
                    foreach (DI din in ied.getDIConfiguration().getDIs())
                    {
                        if (din.DINo == di)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "DI found in Virtual...");
                            Console.WriteLine("DI found in Virtual...");
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public static bool IsValidDO(string don)
        {
            if (don == null || don == "" || IsLessThanEqual2Zero(don)) return false;
            //Loop thru IEC101...
            foreach (IEC101Master iec101 in opcHandle.getMasterConfiguration().getIEC101Group().getIEC101Masters())
            {
                foreach (IED ied in iec101.getIEDs())
                {
                    foreach (DO ldon in ied.getDOConfiguration().getDOs())
                    {
                        if (ldon.DONo == don)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "DO found in IEC101...");
                            Console.WriteLine("DO found in IEC101...");
                            return true;
                        }
                    }
                }
            }
            //Loop thru ADR...
            foreach (ADRMaster Adrmaster in opcHandle.getMasterConfiguration().getADRMasterGroup().getADRMasters())
            {
                foreach (IED ied in Adrmaster.getIEDs())
                {
                    foreach (DO ldon in ied.getDOConfiguration().getDOs())
                    {
                        if (ldon.DONo == don)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "DO found in Adrmaster...");
                            Console.WriteLine("DO found in Adrmaster...");
                            return true;
                        }
                    }
                }
            }
            //Loop thru IEC103...
            foreach (IEC103Master iec103 in opcHandle.getMasterConfiguration().getIEC103Group().getIEC103Masters())
            {
                foreach (IED ied in iec103.getIEDs())
                {
                    foreach (DO ldon in ied.getDOConfiguration().getDOs())
                    {
                        if (ldon.DONo == don)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "DO found in IEC103...");
                            Console.WriteLine("DO found in IEC103...");
                            return true;
                        }
                    }
                }
            }

            //Loop thru MODBUS...
            foreach (MODBUSMaster mbm in opcHandle.getMasterConfiguration().getMODBUSGroup().getMODBUSMasters())
            {
                foreach (IED ied in mbm.getIEDs())
                {
                    foreach (DO ldon in ied.getDOConfiguration().getDOs())
                    {
                        if (ldon.DONo == don)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "DO found in MODBUS...");
                            Console.WriteLine("DO found in MODBUS...");
                            return true;
                        }
                    }
                }
            }

            //Loop thru Virtual...
            foreach (VirtualMaster vm in opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters())
            {
                foreach (IED ied in vm.getIEDs())
                {
                    foreach (DO ldon in ied.getDOConfiguration().getDOs())
                    {
                        if (ldon.DONo == don)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "DO found in Virtual...");
                            Console.WriteLine("DO found in Virtual...");
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public static bool IsValidEN(string en)
        {
            if (en == null || en == "" || IsLessThanEqual2Zero(en)) return false;
            //Loop thru IEC101...
            foreach (IEC101Master iec101 in opcHandle.getMasterConfiguration().getIEC101Group().getIEC101Masters())
            {
                foreach (IED ied in iec101.getIEDs())
                {
                    foreach (EN ein in ied.getENConfiguration().getENs())
                    {
                        if (ein.ENNo == en)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "EN found in IEC101...");
                            Console.WriteLine("EN found in IEC101...");
                            return true;
                        }
                    }
                }
            }
            //Loop thru ADR...
            foreach (ADRMaster adrmaster in opcHandle.getMasterConfiguration().getADRMasterGroup().getADRMasters())
            {
                foreach (IED ied in adrmaster.getIEDs())
                {
                    foreach (EN ein in ied.getENConfiguration().getENs())
                    {
                        if (ein.ENNo == en)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "EN found in ADR...");
                            Console.WriteLine("EN found in ADR...");
                            return true;
                        }
                    }
                }
            }
            //Loop thru IEC103...
            foreach (IEC103Master iec103 in opcHandle.getMasterConfiguration().getIEC103Group().getIEC103Masters())
            {
                foreach (IED ied in iec103.getIEDs())
                {
                    foreach (EN ein in ied.getENConfiguration().getENs())
                    {
                        if (ein.ENNo == en)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "EN found in IEC103...");
                            Console.WriteLine("EN found in IEC103...");
                            return true;
                        }
                    }
                }
            }

            //Loop thru MODBUS...
            foreach (MODBUSMaster mbm in opcHandle.getMasterConfiguration().getMODBUSGroup().getMODBUSMasters())
            {
                foreach (IED ied in mbm.getIEDs())
                {
                    foreach (EN ein in ied.getENConfiguration().getENs())
                    {
                        if (ein.ENNo == en)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "EN found in MODBUS...");
                            Console.WriteLine("EN found in MODBUS...");
                            return true;
                        }
                    }
                }
            }

            //Loop thru Virtual...
            foreach (VirtualMaster vm in opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters())
            {
                foreach (IED ied in vm.getIEDs())
                {
                    foreach (EN ein in ied.getENConfiguration().getENs())
                    {
                        if (ein.ENNo == en)
                        {
                            Utils.WriteLine(VerboseLevel.DEBUG, "EN found in Virtual...");
                            Console.WriteLine("EN found in Virtual...");
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public static bool IsExistAIinPLC(string ai)
        {
            //Loop thru CLA...
            foreach (CLA cla in opcHandle.getParameterLoadConfiguration().getClosedLoopAction().getCLAs())
            {
                if (cla.AINo1 == ai || cla.AINo2 == ai) return true;
            }
            //Loop thru profile...
            foreach (Profile pr in opcHandle.getParameterLoadConfiguration().getProfileRecord().getProfiles())
            {
                if (pr.AINo == ai) return true;
            }
            //Loop thru MD...
            foreach (MD md in opcHandle.getParameterLoadConfiguration().getMDCalculation().getMDs())
            {
                if (md.V_AINo == ai || md.I1_AINo == ai || md.I2_AINo == ai || md.I3_AINo == ai) return true;
            }
            //Loop thru DP...
            foreach (DP dp in opcHandle.getParameterLoadConfiguration().getDerivedParam().getDPs())
            {
                if (dp.AINo1 == ai || dp.AINo2 == ai) return true;
            }
            return false;
        }
        public static void DeleteAIFromPLC(string ai)
        {
            ClosedLoopAction claMain = opcHandle.getParameterLoadConfiguration().getClosedLoopAction(); //Loop thru CLA...
            for (int i = opcHandle.getParameterLoadConfiguration().getClosedLoopAction().getCLAs().Count - 1; i >= 0; i--)
            {
                CLA cla = claMain.getCLAs().ElementAt(i);
                if (cla.AINo1 == ai || cla.AINo2 == ai) claMain.DeleteCLA(i);
            }
            claMain.refreshList();

            ProfileRecord prMain = opcHandle.getParameterLoadConfiguration().getProfileRecord();   //Loop thru profile...
            for (int i = opcHandle.getParameterLoadConfiguration().getProfileRecord().getProfiles().Count - 1; i >= 0; i--)
            {
                Profile pr = prMain.getProfiles().ElementAt(i);
                if (pr.AINo == ai) prMain.DeleteProfile(i);
            }
            prMain.refreshList();

            MDCalculation mdMain = opcHandle.getParameterLoadConfiguration().getMDCalculation();  //Loop thru MD...
            for (int i = opcHandle.getParameterLoadConfiguration().getMDCalculation().getMDs().Count - 1; i >= 1; i--)//IMP: MD=0 ignored...
            {
                MD md = mdMain.getMDs().ElementAt(i);
                if (md.V_AINo == ai || md.I1_AINo == ai || md.I2_AINo == ai || md.I3_AINo == ai) mdMain.DeleteMD(i, true);
            }
            mdMain.refreshList();

            DerivedParam dpMain = opcHandle.getParameterLoadConfiguration().getDerivedParam(); //Loop thru DP...
            for (int i = opcHandle.getParameterLoadConfiguration().getDerivedParam().getDPs().Count - 1; i >= 0; i--)
            {
                DP dp = dpMain.getDPs().ElementAt(i);
                if (dp.AINo1 == ai || dp.AINo2 == ai) dpMain.DeleteDP(i);
            }
            dpMain.refreshList();
        }

        public static bool IsExistDIinPLC(string di)
        {
            foreach (DD dd in opcHandle.getParameterLoadConfiguration().getDerivedDI().getDDs())//Loop thru DD...
            {
                if (dd.DINo1 == di || dd.DINo2 == di) return true;
            }
            return false;
        }

        public static void DeleteDIFromPLC(string di)
        {
            DerivedDI ddMain = opcHandle.getParameterLoadConfiguration().getDerivedDI();  //Loop thru DD...
            for (int i = opcHandle.getParameterLoadConfiguration().getDerivedDI().getDDs().Count - 1; i >= 0; i--)
            {
                DD dd = ddMain.getDDs().ElementAt(i);
                if (dd.DINo1 == di || dd.DINo2 == di) ddMain.DeleteDD(i);
            }
            ddMain.refreshList();
        }

        public static bool IsExistDOinPLC(string don)
        {
            foreach (CLA cla in opcHandle.getParameterLoadConfiguration().getClosedLoopAction().getCLAs()) //Loop thru CLA...
            {
                if (cla.DONo == don) return true;
            }
            return false;
        }

        public static void DeleteDOFromPLC(string don)
        {
            //Loop thru CLA...
            ClosedLoopAction claMain = opcHandle.getParameterLoadConfiguration().getClosedLoopAction();
            for (int i = opcHandle.getParameterLoadConfiguration().getClosedLoopAction().getCLAs().Count - 1; i >= 0; i--)
            {
                CLA cla = claMain.getCLAs().ElementAt(i);
                if (cla.DONo == don) claMain.DeleteCLA(i);
            }
            claMain.refreshList();
        }

        #region Assembly Attribute Accessors

        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        public static string BuildDate
        {
            get
            {
                DateTime buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
                return buildDate.ToShortDateString();
            }
        }

        public static string CompanyAddress
        {
            get
            {
                string address = @"{0},{1}{2},{3}{4},{5}{6},{7}{8},{9}{10},{11}{12},{13}{14}{15}";

                return string.Format(address, Globals.officeName, Environment.NewLine, Globals.address1, Environment.NewLine, Globals.address2, Environment.NewLine, Globals.address3, Environment.NewLine, Globals.city, Environment.NewLine, Globals.state, Environment.NewLine, Globals.country, Environment.NewLine, Globals.pincode, Environment.NewLine);
            }
        }
        #endregion
    }
}
