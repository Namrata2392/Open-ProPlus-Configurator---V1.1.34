using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>MasterTypes</b> is a enumeration to store all supported master types
    * \details   This is a enumeration to store all supported master types.
    * 
    */
    public enum MasterTypes
    {
        UNKNOWN,
        ADR,
        IEC101,
        IEC103,
        MODBUS,
        Virtual,
        IEC61850Client,
        PLU,
    }
    /**
    * \brief     <b>SlaveTypes</b> is a enumeration to store all supported slave types
    * \details   This is a enumeration to store all supported slave types.
    * 
    */
    public enum SlaveTypes
    {
        UNKNOWN,
        IEC104,
        MODBUSSLAVE,
        IEC61850Server,
        IEC101SLAVE,
    }
    /**
    * \brief     <b>VerboseLevel</b> is a enumeration to store all supported verbose levels
    * \details   This is a enumeration to store all supported verbose levels.
    * 
    */
    public enum VerboseLevel
    {
        BOMBARD = 0,
        DEBUG = 1,
        WARNING = 2,
        ERROR = 3
    }
    /**
    * \brief     <b>ResetUniqueNos</b> is a enumeration to specify the type of element to reset
    * \details   This is a enumeration to specify the type of element whose indexing will be reset to 0.
    * 
    */
    public enum ResetUniqueNos
    {
        ALL,
        AI,
        AO,
        DI,
        DO,
        EN,
        IEC104SLAVE,
        MODBUSSLAVE,
        IEC103MASTER,
        IEC101SLAVE,
        //Namrata:3/6/2017;
        IEC101MASTER,
        ADRMaster,
        MODBUSMASTER,
        _61850MASTER,
        CLA,
        PR,
        MD,
        DP,
        DD
    }
    /**
    * \brief     <b>Globals</b> is a class to store global vars
    * \details   This class stores global vars which are same across entire system.
    * 
    */
    public class Globals
    {
        #region Declarations
        private enum MODE
        {
            TEST,
            RELEASE
        }
        private static MODE mode = MODE.TEST;
        public static string PROJECT_ICON = "OpenProPlus_Config";
        public static string rowColour = "#C2DFFF";//"#C0FFC0";//C0FFC0
        public const VerboseLevel Level = VerboseLevel.ERROR;
        public const int INI_XML_VERSION = 4;
        public const string XSD_FILENAME = "openproplus_config.xsd";
        public const string XSD_DATATYPE_FILENAME = "common_config.xsd";
        public const string PROMPT_DELETE_ENTRY = "Are you sure you want to delete?";
        //Namrata: 31/08/2017
        public const string TIME_ZONE_LIST = "zone.txt";
        //Namrata:19/05/2017
        public static string resources_path = Application.StartupPath + @"\" + "resources" + @"\";
        public static string test_resources_path = Application.StartupPath + @"\" + "resources" + @"\";
        //Namrata: 31/08/2017
        public static string Zoneresources_path = Application.StartupPath + @"\" + "resources" + @"\";
        public static string testZoneresources_path = Application.StartupPath + @"\" + "resources" + @"\";
        #region All Default Values
        //All Default Values...
        public const string XML_VERSION = "4";
        public const string DEVICE_TYPE = "APCG-42";
        public const string HW_VERSION = "1.0";
        public const string SW_VERSION = "1.0";
        public const string FIRMWARE_VERSION = "1.0";
        public const string DEVICE_DESC = "OpenPro+";
        public const int LOG_SERVER_PORT = 514;
        public const int MAX_DATA_POINTS = 4000;
        public const int MAX_DEBUG_LEVEL = 7;
        public const int MAX_DESCRIPTION_LEN = 19;
        #endregion All Default Values
        #region About Information
        public const string version = "1.1.29";
        //public string version = Utils.AssemblyVersion();
        public const string copyright = "Copyright (c) 2016, Ashida Electronics Pvt.Ltd";
        public const string officeName = "Ashida House";
        public const string address1 = "Plot No. A-308";
        public const string address2 = "Road No. – 21";
        public const string address3 = "Wagle Industrial Estate";
        public const string city = "Thane";
        public const string state = "Maharashtra"; 
        public const string country = "INDIA";
        public const string pincode = "400 604";
        public const string tel = "+91–22–61299124/25";
        public const string fax = "+91–22–25804262";
        public const string email = "support@ashidaelectronics.com";
        public const string website = "www.ashidaelectronics.com";
        #endregion About Information
        #endregion Declarations
        public static string RESOURCES_PATH
        {
            get
            {
                if (mode == MODE.TEST) return test_resources_path;
                else return resources_path;
            }
        }

        #region ZonePath
        //Namrata: 31/08/2017
        public static string ZONE_RESOURCES_PATH
        {
            get
            {
                if (mode == MODE.TEST) return Zoneresources_path;
                else return testZoneresources_path;
            }
        }
        #endregion ZonePath

        #region Define Max Values Of Slave
        //Define Max Values Of Slave
        private static int gMaxIEC104Slave = 4;
        private static int gMaxMODBUSSlave = 4;
        private static int gMaxIEC101Slave = 4;
        private static int gMaxSlaves = 4;
        public static int MaxIEC104Slave
        {
            get { return gMaxIEC104Slave; }
            set { gMaxIEC104Slave = value; }
        }
        public static int MaxMODBUSSlave
        {
            get { return gMaxMODBUSSlave; }
            set { gMaxMODBUSSlave = value; }
        }
        public static int MaxIEC101Slave
        {
            get { return gMaxIEC101Slave; }
            set { gMaxIEC101Slave = value; }
        }
        public static int MaxSlaves
        {
            get { return gMaxSlaves; }
            set { gMaxSlaves = value; }
        }
        #endregion Define Max Values Of Slave

        #region Define Max Values of PLU
        private static int gMaxPLUAI = 50;
        private static int gMaxPLUDI = 50;
        public static int MaxPLUAI
        {
            get { return gMaxPLUAI; }
            set { gMaxPLUAI = value; }
        }
        public static int MaxPLUDI
        {
            get { return gMaxPLUDI; }
            set { gMaxPLUDI = value; }
        }
        #endregion Define max Values of PLU

        #region Define Max Values of ADR
        //Namrata:3/6/2017
        private static int gMaxADRMaster = 1;
        private static int gMaxADRIED = 1;
        private static int gMaxADRAI = 50;
        private static int gMaxADRDI = 200;
        private static int gMaxADRDO = 50;
        private static int gMaxADREN = 50;
        private static int gMaxADREN1 = 50;
        public static int MaxADRMaster
        {
            get { return gMaxADRMaster; }
            set { gMaxADRMaster = value; }
        }
        public static int MaxADRIED
        {
            get { return gMaxADRIED; }
            set { gMaxADRIED = value; }
        }
        public static int MaxADRAI
        {
            get { return gMaxADRAI; }
            set { gMaxADRAI = value; }
        }
        public static int MaxADRDI
        {
            get { return gMaxADRDI; }
            set { gMaxADRDI = value; }
        }
        public static int MaxADRDO
        {
            get { return gMaxADRDO; }
            set { gMaxADRDO = value; }
        }
        public static int MaxADREN
        {
            get { return gMaxADREN; }
            set { gMaxADREN = value; }
        }

        public static int Max61850EN
        {
            get { return gMaxADREN1; }
            set { gMaxADREN1 = value; }
        }
        #endregion Define Max Values of ADR

        #region Define Max Values of IEC101
        //Define Max Values of IEC101Master
        private static int gMaxIEC101Master = 4;
        private static int gMaxIEC101IED = 50;
        private static int gMaxIEC101AI = 500;
        private static int gMaxIEC101DI = 2000;
        private static int gMaxIEC101DI1 = 2000;
        private static int gMaxIEC101DO = 50;
        private static int gMaxIEC101DO1 = 500;
        private static int gMaxIEC101EN = 500;
        public static int MaxIEC101Master
        {
            get { return gMaxIEC101Master; }
            set { gMaxIEC101Master = value; }
        }
        public static int MaxIEC61850DI
        {
            get { return gMaxIEC101DI1; }
            set { gMaxIEC101DI1 = value; }
        }
        public static int MaxIEC101IED
        {
            get { return gMaxIEC101IED; }
            set { gMaxIEC101IED = value; }
        }
        public static int MaxIEC101AI
        {
            get { return gMaxIEC101AI; }
            set { gMaxIEC101AI = value; }
        }
        public static int MaxIEC101DI
        {
            get { return gMaxIEC101DI; }
            set { gMaxIEC101DI = value; }
        }
        public static int MaxIEC101DO
        {
            get { return gMaxIEC101DO; }
            set { gMaxIEC101DO = value; }
        }
        public static int MaxIEC101DO1
        {
            get { return gMaxIEC101DO1; }
            set { gMaxIEC101DO1 = value; }
        }
        public static int MaxIEC101EN
        {
            get { return gMaxIEC101EN; }
            set { gMaxIEC101EN = value; }
        }
        #endregion Define Max Values of IEC101

        #region Define max Values of IEC103
        //Define max Values of IEC103Master
        private static int gMaxIEC103Master = 4;
        private static int gMaxIEC103IED = 50;
        private static int gMaxIEC103AI = 500;
        private static int gMaxIEC103DI = 2000;
        private static int gMaxIEC103DO = 500;
        private static int gMaxIEC103EN = 500;
        public static int MaxIEC103Master
        {
            get { return gMaxIEC103Master; }
            set { gMaxIEC103Master = value; }
        }
        public static int MaxIEC103IED
        {
            get { return gMaxIEC103IED; }
            set { gMaxIEC103IED = value; }
        }
        public static int MaxIEC103AI
        {
            get { return gMaxIEC103AI; }
            set { gMaxIEC103AI = value; }
        }
        public static int MaxIEC103DI
        {
            get { return gMaxIEC103DI; }
            set { gMaxIEC103DI = value; }
        }
        public static int MaxIEC103DO
        {
            get { return gMaxIEC103DO; }
            set { gMaxIEC103DO = value; }
        }
        public static int MaxIEC103EN
        {
            get { return gMaxIEC103EN; }
            set { gMaxIEC103EN = value; }
        }
        #endregion Define max Values of IEC103
       
        #region Define Max Values Of MODBUS
        //Define Max Values Of MODBUSMaster
        private static int gMaxMODBUSMaster = 100;
        private static int gMaxMODBUSIED = 200;
        private static int gMaxMODBUSIED1 = 200;
        private static int gMaxMODBUSAI = 500;
        private static int gMaxMODBUSAI1 = 2000;
        private static int gMaxMODBUSDI = 2000;
        private static int gMaxMODBUSDO = 500;
        private static int gMaxMODBUSEN = 500;
        public static int MaxMODBUSMaster
        {
            get { return gMaxMODBUSMaster; }
            set { gMaxMODBUSMaster = value; }
        }
        public static int MaxMODBUSIED
        {
            get { return gMaxMODBUSIED; }
            set { gMaxMODBUSIED = value; }
        }

        public static int MaIEC61850IED
        {
            get { return gMaxMODBUSIED1; }
            set { gMaxMODBUSIED1 = value; }
        }
        public static int MaxMODBUSAI
        {
            get { return gMaxMODBUSAI; }
            set { gMaxMODBUSAI = value; }
        }

        public static int MaxMODBUSAI1
        {
            get { return gMaxMODBUSAI1; }
            set { gMaxMODBUSAI1 = value; }
        }
        public static int MaxMODBUSDI
        {
            get { return gMaxMODBUSDI; }
            set { gMaxMODBUSDI = value; }
        }
        public static int MaxMODBUSDO
        {
            get { return gMaxMODBUSDO; }
            set { gMaxMODBUSDO = value; }
        }
        public static int MaxMODBUSEN
        {
            get { return gMaxMODBUSEN; }
            set { gMaxMODBUSEN = value; }
        }
        #endregion Define Max Values Of MODBUS

        #region Define Max Values Of Close Loop Action
        //Define Max Values Of Close Loop Action
        private static int gMaxCLA = 256;
        private static int gMaxProfile = 256;
        private static int gMaxMDCalc = 256;
        private static int gMaxDerivedParam = 100;
        private static int gMaxDerivedDI = 100;
        public static int MaxCLA
        {
            get { return gMaxCLA; }
            set { gMaxCLA = value; }
        }
        public static int MaxProfile
        {
            get { return gMaxProfile; }
            set { gMaxProfile = value; }
        }
        public static int MaxMDCalc
        {
            get { return gMaxMDCalc; }
            set { gMaxMDCalc = value; }
        }
        public static int MaxDerivedParam
        {
            get { return gMaxDerivedParam; }
            set { gMaxDerivedParam = value; }
        }
        public static int MaxDerivedDI
        {
            get { return gMaxDerivedDI; }
            set { gMaxDerivedDI = value; }
        }
        #endregion Define Max Values Of Close Loop Action
        //Namrata: 11/07/2017
        #region Unique Global No. Slave
        //Unique Global No. Slave
        private static int gSlaveNo = 0;
        //private static int gIEC104SlaveNo = 0;
        //private static int gMODBUSSlaveNo = 0;
        //private static int gIEC101SlaveNo = 0;
        public static int SlaveNo
        {
            get { return gSlaveNo; }
            set { if (value > gSlaveNo) gSlaveNo = value; }
        }
        #endregion Unique Global No. Slave

        #region Unique Global No. Master
        //Unique Global No. Master
        private static int gMasterNo = 0;
        //private static int gADRMasterNo = 0;
        //private static int gIEC101MasterNo = 0;
        //private static int gIEC103MasterNo = 0;
        //private static int gMODBUSMasterNo = 0;
        public static int MasterNo
        {
            get { return gMasterNo; }
            set { if (value > gMasterNo) gMasterNo = value; }
        }
        #endregion Unique Global No. Master

        #region Unique Global No. AI, DI, DO, EN 
        //Unique Global No. AI, DI, DO, EN 
        private static int gAINo = 0;
        private static int gAONo = 0;
        private static int gDINo = 0;
        private static int gDONo = 0;
        private static int gENNo = 0;
        public static int AINo
        {
            get { return gAINo; }
            set { if (value > gAINo) gAINo = value; }
        }
        public static int AONo
        {
            get { return gAONo; }
            set { if (value > gAONo) gAONo = value; }
        }
        public static int DINo
        {
            get { return gDINo; }
            set { if (value > gDINo) gDINo = value; }
        }
        public static int DONo
        {
            get { return gDONo; }
            set { if (value > gDONo) gDONo = value; }
        }
        public static int ENNo
        {
            get { return gENNo; }
            set { if (value > gENNo) gENNo = value; }
        }
        #endregion Unique Global No. AI, DI, DO, EN 

        #region Unique Global No. AIIndex, DIIndex, DOIndex, EN 
        //Unique Global No. AIIndex, DIIndex, DOIndex, EN 
        private static int gAIIndex = 0;
        private static int gAOIndex = 0;
        private static int gDIIndex = 0;
        private static int gDOIndex = 0;
        private static int gENIndex = 0;
        public static int AIIndex
        {
            get { return gAIIndex; }
            set { if (value > gAIIndex) gAIIndex = value; }
        }
        public static int AOIndex
        {
            get { return gAOIndex; }
            set { if (value > gAOIndex) gAOIndex = value; }
        }
        public static int DIIndex
        {
            get { return gDIIndex; }
            set { if (value > gDIIndex) gDIIndex = value; }
        }
        public static int DOIndex
        {
            get { return gDOIndex; }
            set { if (value > gDOIndex) gDOIndex = value; }
        }
        public static int ENIndex
        {
            get { return gENIndex; }
            set { if (value > gENIndex) gENIndex = value; }
        }
        #endregion Unique Global No. AIIndex, DIIndex, DOIndex, EN 

        #region Unique Global No. AIReportingIndex, DIReportingIndex, DOReportingIndex, ENReportingIndex 
        //Unique Global No. AIReportingIndex, DIReportingIndex, DOReportingIndex, ENReportingIndex 
        private static int gAIReportingIndex = 0;
        private static int gAOReportingIndex = 0;
        private static int gDIReportingIndex = 0;
        private static int gDOReportingIndex = 0;
        private static int gENReportingIndex = 0;
        public static int AIReportingIndex
        {
            get { return gAIReportingIndex; }
            set { if (value > gAIReportingIndex) gAIReportingIndex = value; }
        }
        public static int AOReportingIndex
        {
            get { return gAOReportingIndex; }
            set { if (value > gAOReportingIndex) gAOReportingIndex = value; }
        }
        public static int DIReportingIndex
        {
            get { return gDIReportingIndex; }
            set { if (value > gDIReportingIndex) gDIReportingIndex = value; }
        }
        public static int DOReportingIndex
        {
            get { return gDOReportingIndex; }
            set { if (value > gDOReportingIndex) gDOReportingIndex = value; }
        }
        public static int ENReportingIndex
        {
            get { return gENReportingIndex; }
            set { if (value > gENReportingIndex) gENReportingIndex = value; }
        }
        #endregion Unique Global No. AIReportingIndex, DIReportingIndex, DOReportingIndex, ENReportingIndex 

        #region  Unique Global No. Close Loop Action    
        //Unique Global No. Close Loop Action  
        private static int gCLANo = 0;
        private static int gProfileNo = 0;
        private static int gMDNo = 0;
        private static int gDPNo = 0;
        private static int gDDNo = 0;
        public static int CLANo
        {
            get { return gCLANo; }
            set { if (value > gCLANo) gCLANo = value; }
        }
        public static int ProfileNo
        {
            get { return gProfileNo; }
            set { if (value > gProfileNo) gProfileNo = value; }
        }
        public static int MDNo
        {
            get { return gMDNo; }
            set { if (value > gMDNo) gMDNo = value; }
        }
        public static int DPNo
        {
            get { return gDPNo; }
            set { if (value > gDPNo) gDPNo = value; }
        }
        public static int DDNo
        {
            get { return gDDNo; }
            set { if (value > gDDNo) gDDNo = value; }
        }


        #endregion Unique Global No. Close Loop Action  

        #region Get ADDRIEDNo ,IEC101sIEDNo,IEC101ASDUAddress ,IEC103IEDNo ,IEC103ASDUAddress ,MODBUSsIEDNo
        //Namrata:3/6/2017
        private static Dictionary<int, int> gADRsIEDNo = new Dictionary<int, int>();
        private static Dictionary<int, int> gIEC101sIEDNo = new Dictionary<int, int>();
        private static Dictionary<int, int> gIEC101sASDUAddress = new Dictionary<int, int>();
        private static Dictionary<int, int> gIEC103sIEDNo = new Dictionary<int, int>();
        private static Dictionary<int, int> gIEC103sASDUAddress = new Dictionary<int, int>();
        private static Dictionary<int, int> gMODBUSsIEDNo = new Dictionary<int, int>();
        private static Dictionary<int, int> g61850sIEDNo = new Dictionary<int, int>();
       
        public static int getIEC103IEDNo(int IEC103MasterNo)
        {
            int IEC103IEDNo;
            if (!gIEC103sIEDNo.TryGetValue(IEC103MasterNo, out IEC103IEDNo))
            {
                Utils.WriteLine(VerboseLevel.WARNING, "##### IEC103 master entry does not exists");
                gIEC103sIEDNo.Add(IEC103MasterNo, 0);
                return 0;
            }
            else
            {
                return IEC103IEDNo;
            }
        }
        public static void setIEC103IEDNo(int IEC103MasterNo, int value)
        {
            int IEC103IEDNo;
            if (!gIEC103sIEDNo.TryGetValue(IEC103MasterNo, out IEC103IEDNo))
            {
                Utils.WriteLine(VerboseLevel.WARNING, "##### IEC103 master entry does not exists");
                gIEC103sIEDNo.Add(IEC103MasterNo, value);
            }
            else
            {
                if (value > IEC103IEDNo) gIEC103sIEDNo[IEC103MasterNo] = value;
            }
        }

        public static int get61850IEDNo(int S61850MasterNo)
        {
            int IEC61850IEDNo;
            if (!g61850sIEDNo.TryGetValue(S61850MasterNo, out IEC61850IEDNo))
            {
                Utils.WriteLine(VerboseLevel.WARNING, "##### IEC103 master entry does not exists");
                g61850sIEDNo.Add(S61850MasterNo, 0);
                return 0;
            }
            else
            {
                return IEC61850IEDNo;
            }
        }

        public static void set61850IEDNo(int S61850MasterNo, int value)
        {
            int IEC61850IEDNo;
            if (!g61850sIEDNo.TryGetValue(S61850MasterNo, out IEC61850IEDNo))
            {
                Utils.WriteLine(VerboseLevel.WARNING, "##### IEC103 master entry does not exists");
                g61850sIEDNo.Add(S61850MasterNo, value);
            }
            else
            {
                if (value > IEC61850IEDNo) g61850sIEDNo[S61850MasterNo] = value;
            }
        }
        public static int getIEC101IEDNo(int IEC101MasterNo)
        {
            int IEC101IEDNo;
            if (!gIEC101sIEDNo.TryGetValue(IEC101MasterNo, out IEC101IEDNo))
            {
                Utils.WriteLine(VerboseLevel.WARNING, "##### IEC101 master entry does not exists");
                gIEC101sIEDNo.Add(IEC101MasterNo, 0);
                return 0;
            }
            else
            {
                return IEC101IEDNo;
            }
        }
        public static int getADRIEDNo(int ADRMasterNo)
        {
            int ADRIEDNo;
            if (!gADRsIEDNo.TryGetValue(ADRMasterNo, out ADRIEDNo))
            {
                Utils.WriteLine(VerboseLevel.WARNING, "##### IEC103 master entry does not exists");
                gADRsIEDNo.Add(ADRMasterNo, 0);
                return 0;
            }
            else
            {
                return ADRIEDNo;
            }
        }
        public static int getIEC103ASDUAddress(int IEC103MasterNo)
        {
            int IEC103ASDUAddress;
            if (!gIEC103sASDUAddress.TryGetValue(IEC103MasterNo, out IEC103ASDUAddress))
            {
                Utils.WriteLine(VerboseLevel.WARNING, "##### IEC103 master entry does not exists");
                gIEC103sASDUAddress.Add(IEC103MasterNo, 0);
                return 0;
            }
            else
            {
                return IEC103ASDUAddress;
            }
        }
        public static int getIEC101ASDUAddress(int IEC101MasterNo)
        {
            int IEC101ASDUAddress;
            if (!gIEC101sASDUAddress.TryGetValue(IEC101MasterNo, out IEC101ASDUAddress))
            {
                Utils.WriteLine(VerboseLevel.WARNING, "##### IEC101 master entry does not exists");
                gIEC101sASDUAddress.Add(IEC101MasterNo, 0);
                return 0;
            }
            else
            {
                return IEC101ASDUAddress;
            }
        }
       
       
        public static void setIEC101IEDNo(int IEC101MasterNo, int value)
        {
            int IEC101IEDNo;
            if (!gIEC101sIEDNo.TryGetValue(IEC101MasterNo, out IEC101IEDNo))
            {
                Utils.WriteLine(VerboseLevel.WARNING, "##### IEC101 master entry does not exists");
                gIEC101sIEDNo.Add(IEC101MasterNo, value);
            }
            else
            {
                if (value > IEC101IEDNo) gIEC101sIEDNo[IEC101MasterNo] = value;
            }
        }
        public static void setIEC103ASDUAddress(int IEC103MasterNo, int value)
        {
            int IEC103ASDUAddress;
            if (!gIEC103sASDUAddress.TryGetValue(IEC103MasterNo, out IEC103ASDUAddress))
            {
                Utils.WriteLine(VerboseLevel.WARNING, "##### IEC103 master entry does not exists");
                gIEC103sASDUAddress.Add(IEC103MasterNo, value);
            }
            else
            {
                if (value > IEC103ASDUAddress) gIEC103sASDUAddress[IEC103MasterNo] = value;
            }
        }
        public static void setIEC101ASDUAddress(int IEC101MasterNo, int value)
        {
            int IEC101ASDUAddress;
            if (!gIEC101sASDUAddress.TryGetValue(IEC101MasterNo, out IEC101ASDUAddress))
            {
                Utils.WriteLine(VerboseLevel.WARNING, "##### IEC101 master entry does not exists");
                gIEC101sASDUAddress.Add(IEC101MasterNo, value);
            }
            else
            {
                if (value > IEC101ASDUAddress) gIEC101sASDUAddress[IEC101MasterNo] = value;
            }
        }
        public static int getMODBUSIEDNo(int MODBUSMasterNo)
        {
            int MODBUSIEDNo;
            if (!gMODBUSsIEDNo.TryGetValue(MODBUSMasterNo, out MODBUSIEDNo))
            {
                Utils.WriteLine(VerboseLevel.WARNING, "##### MODBUS master entry does not exists");
                gMODBUSsIEDNo.Add(MODBUSMasterNo, 0);

                return 0;
            }
            else
            {
                return MODBUSIEDNo;
            }
        }
      
        public static void setMODBUSIEDNo(int MODBUSMasterNo, int value)
        {
            int MODBUSIEDNo;
            if (!gMODBUSsIEDNo.TryGetValue(MODBUSMasterNo, out MODBUSIEDNo))
            {
                Utils.WriteLine(VerboseLevel.WARNING, "##### MODBUS master entry does not exists");
                gMODBUSsIEDNo.Add(MODBUSMasterNo, value);
            }
            else
            {
                if (value > MODBUSIEDNo) gMODBUSsIEDNo[MODBUSMasterNo] = value;
            }
        }
        #endregion Get ADDRIEDNo ,IEC101sIEDNo,IEC101ASDUAddress ,IEC103IEDNo ,IEC103ASDUAddress ,MODBUSsIEDNo

        #region Reset Unique No.
        public static void resetUniqueNos(ResetUniqueNos rType)
        {
            switch (rType)
            {
                case ResetUniqueNos.IEC104SLAVE:
                    //gIEC104SlaveNo = 0;
                    SlaveNo = 0;
                    break;

                case ResetUniqueNos.MODBUSSLAVE:
                    //gMODBUSSlaveNo = 0;
                    SlaveNo = 0;
                    break;

                case ResetUniqueNos.IEC101SLAVE:
                    //gIEC101SlaveNo = 0;
                    SlaveNo = 0;
                    break;

                case ResetUniqueNos.IEC103MASTER:
                    gMasterNo = 0;
                   // gIEC103MasterNo = 0;
                    break;

                //Namrata:10/7/2017
                case ResetUniqueNos.IEC101MASTER:
                    gMasterNo = 0;
                    //gIEC101MasterNo = 0;
                    break;

                case ResetUniqueNos.MODBUSMASTER:
                    gMasterNo = 0;
                    //gMODBUSMasterNo = 0;
                    break;

                case ResetUniqueNos.CLA:
                    gCLANo = 0;
                    break;

                case ResetUniqueNos.PR:
                    gProfileNo = 0;
                    break;

                case ResetUniqueNos.MD:
                    gMDNo = 0;
                    break;

                case ResetUniqueNos.DP:
                    gDPNo = 0;
                    break;

                case ResetUniqueNos.DD:
                    gDDNo = 0;
                    break;

                case ResetUniqueNos.AI:
                    gAINo = 0;
                    break;

                case ResetUniqueNos.AO:
                    gAONo = 0;
                    break;

                case ResetUniqueNos.DI:
                    gDINo = 0;
                    break;

                case ResetUniqueNos.DO:
                    gDONo = 0;
                    break;

                case ResetUniqueNos.EN:
                    gENNo = 0;
                    break;  

                case ResetUniqueNos.ADRMaster:
                    gMasterNo = 0;
                    //gADRMasterNo = 0;
                    break;
                case ResetUniqueNos._61850MASTER:
                    gMasterNo = 0;
                    //gADRMasterNo = 0;
                    break;
                case ResetUniqueNos.ALL:
                    gSlaveNo = 0;
                    gMasterNo = 0;
                    gAINo = 0;
                    gDINo = 0;
                    gDONo = 0;
                    gENNo = 0;
                    gAOIndex = 0;
                    gAIIndex = 0;
                    gDIIndex = 0;
                    gDOIndex = 0;
                    gENIndex = 0;
                    gAIReportingIndex = 0;
                    gDIReportingIndex = 0;
                    gDOReportingIndex = 0;
                    gENReportingIndex = 0;
                    gCLANo = 0;
                    gProfileNo = 0;
                    gMDNo = 0;
                    gDPNo = 0;
                    gDDNo = 0;
                    gIEC103sIEDNo = null;
                    gIEC103sIEDNo = new Dictionary<int, int>();
                    gIEC103sASDUAddress = null;
                    gIEC103sASDUAddress = new Dictionary<int, int>();
                    gMODBUSsIEDNo = null;
                    gMODBUSsIEDNo = new Dictionary<int, int>();
                    gADRsIEDNo = null;
                    gADRsIEDNo = new Dictionary<int, int>();
                    g61850sIEDNo = null;
                    g61850sIEDNo = new Dictionary<int, int>();

                    break;
            }
        }
        #endregion Reset Unique No.

        #region UnusedCode
        //public static int IEC104SlaveNo
        //{
        //    get { return gIEC104SlaveNo; }
        //    set { if(value>gIEC104SlaveNo) gIEC104SlaveNo = value; }
        //}
        //public static int MODBUSSlaveNo
        //{
        //    get { return gMODBUSSlaveNo; }
        //    set { if (value > gMODBUSSlaveNo) gMODBUSSlaveNo = value; }
        //}
        //public static int IEC101SlaveNo
        //{
        //    get { return gIEC101SlaveNo; }
        //    set { if (value > gIEC101SlaveNo) gIEC101SlaveNo = value; }
        //}
        //public static int IEC103MasterNo
        //{
        //    get { return gIEC103MasterNo; }
        //    set { if (value > gIEC103MasterNo) gIEC103MasterNo = value; }
        //}

        //public static int IEC101MasterNo
        //{
        //    get { return gIEC101MasterNo; }
        //    set { if (value > gIEC101MasterNo) gIEC101MasterNo = value; }
        //}
        //public static int ADRMasterNo
        //{
        //    get { return gADRMasterNo; }
        //    set { if (value > gADRMasterNo) gADRMasterNo = value; }
        //}

        //public static int MODBUSMasterNo
        //{
        //    get { return gMODBUSMasterNo; }
        //    set { if (value > gMODBUSMasterNo) gMODBUSMasterNo = value; }
        //}

        #endregion
    }
}
