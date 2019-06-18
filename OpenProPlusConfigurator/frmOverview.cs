using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Xml.Schema;
using System.IO;
namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>frmOverview</b> is the user interface displaying <b>overview</b> of the system
    * \details   This is the user interface which displays the overview of the complete system. It is 
    * supported by various filters like data point selection, master selection, slave selection, etc.
    * The user can use this module to filter out unmapped data points.
    * 
    */
    public partial class frmOverview : Form
    {
        OpenProPlus_Config opcHandle;
        public const int COLS_B4_MULTISLAVE = 13;
        public const int TOTAL_MAP_PARAMS = 7;
        public const int FILTER_PANEL_HEIGHT = 50;
        private int sortColumn = -1;
        private bool singleSlave = false;
        DataGridView dgv = new DataGridView();
        DataGridView DgvList = new DataGridView();
        DataTable DtGrid = new DataTable();
        public List<AI> AIList = new List<AI>();
        string[] row = new string[0];
        DataTable dt = new DataTable();
        DataTable dtZeitplan = new DataTable();
        public frmOverview()
        {
            InitializeComponent();
        }
        public frmOverview(OpenProPlus_Config lopc)
        {
            InitializeComponent();
            opcHandle = lopc;
        }
        public void setOpenProPlusHandle(OpenProPlus_Config lopc)
        {
            opcHandle = lopc;
        }
        private void grpMappingView_Resize(object sender, EventArgs e)
        {
            //Namrata: 05/08/2017
            groupBox1.Width = grpMappingView.Width - 30;
            pbHdr.Width = grpMappingView.Width - 30;
            lblMasterType.Location = new Point(6, 40); // Master Typoe
            cmbMasterType.Location = new Point(89, 36);
            lblMaster.Location = new Point(215, 40); //Master
            cmbMaster.Location = new Point(266, 36);
            lblIED.Location = new Point(390, 40);
            cmbIED.Location = new Point(428, 36); // IED
            groupBox2.Location = new Point(555, 31);
            groupBox2.Size = new Size(224, 50);
            lblStatus.Location = new Point(790, 40);
            cmbStatus.Location = new Point(840, 36);
            lblSlaves.Location = new Point(960, 40);
            cmbSlaves.Location = new Point(1013, 36);
            btnApply.Location = new Point(1150, 36);
            btnReset.Location = new Point(1240, 36);
            btnExportToExcel.Location = new Point(1180, 68);
            btnExportToExcel.Size = new Size(107, 26);
            lvMappingView.Width = grpMappingView.Width - 30;
            lvMappingView.Height = grpMappingView.Height - FILTER_PANEL_HEIGHT - 120;
        }
        private void addItemAOs(IED ied, string masterName)
        {
            #region AllSlaves
            if (cmbSlaves.GetItemText(cmbSlaves.SelectedItem).ToLower() == "all")
            {
                int slaveOffset = 0; bool isMapped = false;
                int arrHdrSize12 = 0;
                arrHdrSize12 = COLS_B4_MULTISLAVE;
                //IEC104Slave
                int nIEC104Slaves = getIEC104SlavesCount();
                if (nIEC104Slaves == 1) arrHdrSize12 = arrHdrSize12 + nIEC104Slaves + TOTAL_MAP_PARAMS;//COLS_B4_MULTISLAVE
                else arrHdrSize12 = arrHdrSize12 + nIEC104Slaves;//COLS_B4_MULTISLAVE
                Array.Resize(ref row, arrHdrSize12);

                //MODBUSSlave
                int nMODBUSSlaves = getMODBUSSlavesCount();
                if (nMODBUSSlaves == 1) arrHdrSize12 = arrHdrSize12 + nMODBUSSlaves + TOTAL_MAP_PARAMS;//COLS_B4_MULTISLAVE
                else arrHdrSize12 = arrHdrSize12 + +nMODBUSSlaves;//COLS_B4_MULTISLAVE 
                Array.Resize(ref row, arrHdrSize12);

                //IEC101Slave
                int nIEC101Slaves = getIEC101SlavesCount();
                if (nIEC101Slaves == 1) arrHdrSize12 = arrHdrSize12 + nIEC101Slaves + TOTAL_MAP_PARAMS;
                else arrHdrSize12 = arrHdrSize12 + nIEC101Slaves;//+COLS_B4_MULTISLAVE
                Array.Resize(ref row, arrHdrSize12);

                //---------------------------Default Row values--------------------------------------------//
                row[0] = masterName; row[1] = ied.UnitID;
                foreach (AO ain in ied.getAOConfiguration().getAOs())
                {
                    if (ain.IsNodeComment) continue;
                    #region Default row[] values 
                    row[2] = "AO";
                    row[3] = ain.AONo;
                    row[4] = ain.Description;
                    row[5] = ain.Index;
                    row[6] = ain.SubIndex;
                    row[7] = ain.ResponseType;
                    row[8] = ain.DataType;
                    row[9] = ain.Multiplier;
                    row[10] = ain.Constant;
                    row[11] = "";
                    row[12] = "";
                    #endregion Default row[] values

                    #region IEC104Slave
                    //Reset map info cols...
                    if (nIEC104Slaves == 1)
                        for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    slaveOffset = 0;
                    isMapped = false;
                    foreach (IEC104Slave iecs in getIEC104Slaves())//-------------Total Slaves
                    {
                        List<AOMap> aims = ied.getAOConfiguration().getSlaveAIMaps(iecs.getSlaveID);//----Slave and its map
                        if (aims == null) { row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg"; }
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (AOMap aim in aims)
                            {
                                if (aim.AONo == ain.AONo)////Check maped and configuration
                                {
                                    isMapped = true;
                                    row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    //If single slave, show it's map parameters as well...
                                    //Namarta: 07/08/2017
                                    if (nIEC104Slaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }
                    #endregion IEC104Slave

                    #region MODBUS Slave
                    //Reset map info cols...
                    if (nMODBUSSlaves == 1)
                        for (int i = slaveOffset; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    isMapped = false;
                    foreach (MODBUSSlave modbus in getMODBUSSlaves())//-------------Total Slaves
                    {
                        List<AOMap> aims = ied.getAOConfiguration().getSlaveAIMaps(modbus.getSlaveID);//----Slave and its map
                        if (aims == null) { row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg"; }
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (AOMap aim in aims)
                            {
                                if (aim.AONo == ain.AONo)////Check maped and configuration
                                {
                                    isMapped = true;
                                    //If single slave, show it's map parameters as well...
                                    if (nMODBUSSlaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }
                    #endregion MODBUS Slave

                    #region IEC101 Slave
                    //Reset map info cols...
                    if (nIEC101Slaves == 1)
                        for (int i = slaveOffset; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    isMapped = false;
                    foreach (IEC101Slave iec101 in getIEC101Slaves())//-------------Total Slaves
                    {
                        List<AOMap> aims = ied.getAOConfiguration().getSlaveAIMaps(iec101.getSlaveID);//----Slave and its map
                        if (aims == null) { row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg"; }
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (AOMap aim in aims)
                            {
                                if (aim.AONo == ain.AONo)////Check maped and configuration
                                {
                                    isMapped = true;
                                    //If single slave, show it's map parameters as well...
                                    if (nIEC101Slaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }
                    #endregion IEC101 Slave

                    //Show item depending on Status = Mapped / UnMapped AND whether it's a single slave...
                    if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                    {
                        ListViewItem lvItem = new ListViewItem(row);
                        lvMappingView.Items.Add(lvItem);
                    }
                    else
                    {
                        if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                    }
                }
            }
            #endregion AllSlaves

            #region IEC104Slave
            foreach (IEC104Slave iecs1 in opcHandle.getSlaveConfiguration().getIEC104Group().getIEC104Slaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "IEC104 " + iecs1.SlaveNum)
                {
                    int nIEC104Slaves = getIEC104SlavesCount();
                    int arrHdrSize12 = 0;
                    if (nIEC104Slaves == 1) arrHdrSize12 = COLS_B4_MULTISLAVE + nIEC104Slaves + TOTAL_MAP_PARAMS;
                    else arrHdrSize12 = COLS_B4_MULTISLAVE + nIEC104Slaves;
                    string[] row = new string[arrHdrSize12];
                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (AO ain in ied.getAOConfiguration().getAOs())
                    {
                        if (ain.IsNodeComment) continue;
                        row[2] = "AO";
                        row[3] = ain.AONo;
                        row[4] = ain.Description;
                        row[5] = ain.Index;
                        row[6] = ain.SubIndex;
                        row[7] = ain.ResponseType;
                        row[8] = ain.DataType;
                        row[9] = ain.Multiplier;
                        row[10] = ain.Constant;
                        row[11] = "";
                        row[12] = "";
                        //Reset map info cols...
                        if (nIEC104Slaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";
                        int slaveOffset = 0;
                        bool isMapped = false;
                        foreach (IEC104Slave iecs in getIEC104Slaves())
                        {
                            List<AOMap> aims = ied.getAOConfiguration().getSlaveAIMaps(iecs.getSlaveID);
                            if (aims == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            else
                            {
                                foreach (AOMap aim in aims)
                                {
                                    row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                    if (aim.AONo == ain.AONo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nIEC104Slaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 1] = aim.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 2] = aim.DataType;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 3] = aim.Deadband;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 4] = aim.Multiplier;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 5] = aim.Constant;
                                        }
                                        break;
                                    }
                                }
                            }
                            slaveOffset++;
                        }
                        //Show item depending on Status=Mapped/UnMapped AND whether it's a single slave...
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            //if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            #endregion IEC104Slave

            #region MODBUSSlave
            foreach (MODBUSSlave ModbusSlave in opcHandle.getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "MODBUS " + ModbusSlave.SlaveNum)
                {
                    int nMODBUSSlaves = getMODBUSSlavesCount();
                    int arrHdrSize1 = 0;
                    if (nMODBUSSlaves == 1) arrHdrSize1 = COLS_B4_MULTISLAVE + nMODBUSSlaves + TOTAL_MAP_PARAMS;
                    else arrHdrSize1 = COLS_B4_MULTISLAVE + nMODBUSSlaves;
                    string[] row = new string[arrHdrSize1];

                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (AO ain in ied.getAOConfiguration().getAOs())
                    {
                        if (ain.IsNodeComment) continue;

                        row[2] = "AO";
                        row[3] = ain.AONo;
                        row[4] = ain.Description;
                        row[5] = ain.Index;
                        row[6] = ain.SubIndex;
                        row[7] = ain.ResponseType;
                        row[8] = ain.DataType;
                        row[9] = ain.Multiplier;
                        row[10] = ain.Constant;
                        row[11] = "";
                        row[12] = "";
                        //Reset map info cols...
                        if (nMODBUSSlaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";

                        int slaveOffset = 0;
                        bool isMapped = false;
                        foreach (MODBUSSlave iecs in getMODBUSSlaves())
                        {
                            List<AOMap> aims = ied.getAOConfiguration().getSlaveAIMaps(iecs.getSlaveID);
                            if (aims == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            else
                            {
                                foreach (AOMap aim in aims)
                                {
                                    row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                    if (aim.AONo == ain.AONo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nMODBUSSlaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 1] = aim.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 2] = aim.DataType;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 3] = aim.Deadband;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 4] = aim.Multiplier;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 5] = aim.Constant;
                                        }
                                        break;
                                    }
                                }
                            }
                            slaveOffset++;
                        }
                        //Show item depending on Status=Mapped/UnMapped AND whether it's a single slave...
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            #endregion MODBUSSlave

            #region IEC101Slave
            foreach (IEC101Slave iec101Slave in opcHandle.getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "IEC101 " + iec101Slave.SlaveNum)
                {
                    int nIEC101Slaves = getIEC101SlavesCount();
                    int arrHdrSize1 = 0;
                    if (nIEC101Slaves == 1) arrHdrSize1 = COLS_B4_MULTISLAVE + nIEC101Slaves + TOTAL_MAP_PARAMS;
                    else arrHdrSize1 = COLS_B4_MULTISLAVE + nIEC101Slaves;
                    string[] row = new string[arrHdrSize1];
                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (AO ain in ied.getAOConfiguration().getAOs())
                    {
                        if (ain.IsNodeComment) continue;

                        row[2] = "AO";
                        row[3] = ain.AONo;
                        row[4] = ain.Description;
                        row[5] = ain.Index;
                        row[6] = ain.SubIndex;
                        row[7] = ain.ResponseType;
                        row[8] = ain.DataType;
                        row[9] = ain.Multiplier;
                        row[10] = ain.Constant;
                        row[11] = "";
                        row[12] = "";
                        //Reset map info cols...
                        if (nIEC101Slaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";
                        int slaveOffset = 0;
                        bool isMapped = false;
                        foreach (IEC101Slave iecs in getIEC101Slaves())
                        {
                            List<AOMap> aims = ied.getAOConfiguration().getSlaveAIMaps(iecs.getSlaveID);
                            if (aims == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            else
                            {
                                foreach (AOMap aim in aims)
                                {
                                    row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                    if (aim.AONo == ain.AONo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nIEC101Slaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 1] = aim.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 2] = aim.DataType;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 3] = aim.Deadband;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 4] = aim.Multiplier;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 5] = aim.Constant;
                                        }
                                        break;
                                    }
                                }
                            }
                            slaveOffset++;
                        }
                        //Show item depending on Status=Mapped/UnMapped AND whether it's a single slave...
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            #endregion IEC101Slave
        }
        private void addItemAIs(IED ied, string masterName)
        {
            #region AllSlaves
            if (cmbSlaves.GetItemText(cmbSlaves.SelectedItem).ToLower() == "all")
            {
                int slaveOffset = 0; bool isMapped = false;
                int arrHdrSize12 = 0;
                arrHdrSize12 = COLS_B4_MULTISLAVE;
                //IEC104Slave
                int nIEC104Slaves = getIEC104SlavesCount();
                if (nIEC104Slaves == 1) arrHdrSize12 = arrHdrSize12 + nIEC104Slaves + TOTAL_MAP_PARAMS;//COLS_B4_MULTISLAVE
                else arrHdrSize12 = arrHdrSize12 + nIEC104Slaves;//COLS_B4_MULTISLAVE
                Array.Resize(ref row, arrHdrSize12);

                //MODBUSSlave
                int nMODBUSSlaves = getMODBUSSlavesCount();
                if (nMODBUSSlaves == 1) arrHdrSize12 = arrHdrSize12 + nMODBUSSlaves + TOTAL_MAP_PARAMS;//COLS_B4_MULTISLAVE
                else arrHdrSize12 = arrHdrSize12 + +nMODBUSSlaves;//COLS_B4_MULTISLAVE 
                Array.Resize(ref row, arrHdrSize12);

                //IEC101Slave
                int nIEC101Slaves = getIEC101SlavesCount();
                if (nIEC101Slaves == 1) arrHdrSize12 = arrHdrSize12 + nIEC101Slaves + TOTAL_MAP_PARAMS;
                else arrHdrSize12 = arrHdrSize12 + nIEC101Slaves;//+COLS_B4_MULTISLAVE
                Array.Resize(ref row, arrHdrSize12);

                //---------------------------Default Row values--------------------------------------------//
                row[0] = masterName; row[1] = ied.UnitID;
                foreach (AI ain in ied.getAIConfiguration().getAIs())
                {
                    if (ain.IsNodeComment) continue;
                    #region Default row[] values 
                    row[2] = "AI";
                    row[3] = ain.AINo;
                    row[4] = ain.Description;
                    row[5] = ain.Index;
                    row[6] = ain.SubIndex;
                    row[7] = ain.ResponseType;
                    row[8] = ain.DataType;
                    row[9] = ain.Multiplier;
                    row[10] = ain.Constant;
                    row[11] = "";
                    row[12] = "";
                    #endregion Default row[] values

                    #region IEC104Slave
                    //Reset map info cols...
                    if (nIEC104Slaves == 1)
                        for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    slaveOffset = 0;
                    isMapped = false;
                    foreach (IEC104Slave iecs in getIEC104Slaves())//-------------Total Slaves
                    {
                        List<AIMap> aims = ied.getAIConfiguration().getSlaveAIMaps(iecs.getSlaveID);//----Slave and its map
                        if (aims == null) { row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg"; }
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (AIMap aim in aims)
                            {
                                if (aim.AINo == ain.AINo)////Check maped and configuration
                                {
                                    isMapped = true;
                                    row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    //If single slave, show it's map parameters as well...
                                    //Namarta: 07/08/2017
                                    if (nIEC104Slaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }
                    #endregion IEC104Slave

                    #region MODBUS Slave
                    //Reset map info cols...
                    if (nMODBUSSlaves == 1)
                        for (int i = slaveOffset; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    isMapped = false;
                    foreach (MODBUSSlave modbus in getMODBUSSlaves())//-------------Total Slaves
                    {
                        List<AIMap> aims = ied.getAIConfiguration().getSlaveAIMaps(modbus.getSlaveID);//----Slave and its map
                        if (aims == null) { row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg"; }
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (AIMap aim in aims)
                            {
                                if (aim.AINo == ain.AINo)////Check maped and configuration
                                {
                                    isMapped = true;
                                    //If single slave, show it's map parameters as well...
                                    if (nMODBUSSlaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }
                    #endregion MODBUS Slave

                    #region IEC101 Slave
                    //Reset map info cols...
                    if (nIEC101Slaves == 1)
                        for (int i = slaveOffset; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    isMapped = false;
                    foreach (IEC101Slave iec101 in getIEC101Slaves())//-------------Total Slaves
                    {
                        List<AIMap> aims = ied.getAIConfiguration().getSlaveAIMaps(iec101.getSlaveID);//----Slave and its map
                        if (aims == null) { row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg"; }
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (AIMap aim in aims)
                            {
                                if (aim.AINo == ain.AINo)////Check maped and configuration
                                {
                                    isMapped = true;
                                    //If single slave, show it's map parameters as well...
                                    if (nIEC101Slaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }
                    #endregion IEC101 Slave

                    //Show item depending on Status = Mapped / UnMapped AND whether it's a single slave...
                    if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                    {
                        ListViewItem lvItem = new ListViewItem(row);
                        lvMappingView.Items.Add(lvItem);
                    }
                    else
                    {
                        if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                    }
                }
            }
            #endregion AllSlaves

            #region IEC104Slave
            foreach (IEC104Slave iecs1 in opcHandle.getSlaveConfiguration().getIEC104Group().getIEC104Slaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "IEC104 " + iecs1.SlaveNum)
                {
                    int nIEC104Slaves = getIEC104SlavesCount();
                    int arrHdrSize12 = 0;
                    if (nIEC104Slaves == 1) arrHdrSize12 = COLS_B4_MULTISLAVE + nIEC104Slaves + TOTAL_MAP_PARAMS;
                    else arrHdrSize12 = COLS_B4_MULTISLAVE + nIEC104Slaves;
                    string[] row = new string[arrHdrSize12];
                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (AI ain in ied.getAIConfiguration().getAIs())
                    {
                        if (ain.IsNodeComment) continue;
                        row[2] = "AI";
                        row[3] = ain.AINo;
                        row[4] = ain.Description;
                        row[5] = ain.Index;
                        row[6] = ain.SubIndex;
                        row[7] = ain.ResponseType;
                        row[8] = ain.DataType;
                        row[9] = ain.Multiplier;
                        row[10] = ain.Constant;
                        row[11] = "";
                        row[12] = "";
                        //Reset map info cols...
                        if (nIEC104Slaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";
                        int slaveOffset = 0;
                        bool isMapped = false;
                        foreach (IEC104Slave iecs in getIEC104Slaves())
                        {
                            List<AIMap> aims = ied.getAIConfiguration().getSlaveAIMaps(iecs.getSlaveID);
                            if (aims == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            else
                            {
                                foreach (AIMap aim in aims)
                                {
                                    row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                    if (aim.AINo == ain.AINo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nIEC104Slaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 1] = aim.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 2] = aim.DataType;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 3] = aim.Deadband;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 4] = aim.Multiplier;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 5] = aim.Constant;
                                        }
                                        break;
                                    }
                                }
                            }
                            slaveOffset++;
                        }
                        //Show item depending on Status=Mapped/UnMapped AND whether it's a single slave...
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            //if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            #endregion IEC104Slave

            #region MODBUSSlave
            foreach (MODBUSSlave ModbusSlave in opcHandle.getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "MODBUS " + ModbusSlave.SlaveNum)
                {
                    int nMODBUSSlaves = getMODBUSSlavesCount();
                    int arrHdrSize1 = 0;
                    if (nMODBUSSlaves == 1) arrHdrSize1 = COLS_B4_MULTISLAVE + nMODBUSSlaves + TOTAL_MAP_PARAMS;
                    else arrHdrSize1 = COLS_B4_MULTISLAVE + nMODBUSSlaves;
                    string[] row = new string[arrHdrSize1];

                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (AI ain in ied.getAIConfiguration().getAIs())
                    {
                        if (ain.IsNodeComment) continue;

                        row[2] = "AI";
                        row[3] = ain.AINo;
                        row[4] = ain.Description;
                        row[5] = ain.Index;
                        row[6] = ain.SubIndex;
                        row[7] = ain.ResponseType;
                        row[8] = ain.DataType;
                        row[9] = ain.Multiplier;
                        row[10] = ain.Constant;
                        row[11] = "";
                        row[12] = "";
                        //Reset map info cols...
                        if (nMODBUSSlaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";

                        int slaveOffset = 0;
                        bool isMapped = false;
                        foreach (MODBUSSlave iecs in getMODBUSSlaves())
                        {
                            List<AIMap> aims = ied.getAIConfiguration().getSlaveAIMaps(iecs.getSlaveID);
                            if (aims == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            else
                            {
                                foreach (AIMap aim in aims)
                                {
                                    row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                    if (aim.AINo == ain.AINo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nMODBUSSlaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 1] = aim.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 2] = aim.DataType;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 3] = aim.Deadband;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 4] = aim.Multiplier;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 5] = aim.Constant;
                                        }
                                        break;
                                    }
                                }
                            }
                            slaveOffset++;
                        }
                        //Show item depending on Status=Mapped/UnMapped AND whether it's a single slave...
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            #endregion MODBUSSlave

            #region IEC101Slave
            foreach (IEC101Slave iec101Slave in opcHandle.getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "IEC101 " + iec101Slave.SlaveNum)
                {
                    int nIEC101Slaves = getIEC101SlavesCount();
                    int arrHdrSize1 = 0;
                    if (nIEC101Slaves == 1) arrHdrSize1 = COLS_B4_MULTISLAVE + nIEC101Slaves + TOTAL_MAP_PARAMS;
                    else arrHdrSize1 = COLS_B4_MULTISLAVE + nIEC101Slaves;
                    string[] row = new string[arrHdrSize1];
                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (AI ain in ied.getAIConfiguration().getAIs())
                    {
                        if (ain.IsNodeComment) continue;

                        row[2] = "AI";
                        row[3] = ain.AINo;
                        row[4] = ain.Description;
                        row[5] = ain.Index;
                        row[6] = ain.SubIndex;
                        row[7] = ain.ResponseType;
                        row[8] = ain.DataType;
                        row[9] = ain.Multiplier;
                        row[10] = ain.Constant;
                        row[11] = "";
                        row[12] = "";
                        //Reset map info cols...
                        if (nIEC101Slaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";
                        int slaveOffset = 0;
                        bool isMapped = false;
                        foreach (IEC101Slave iecs in getIEC101Slaves())
                        {
                            List<AIMap> aims = ied.getAIConfiguration().getSlaveAIMaps(iecs.getSlaveID);
                            if (aims == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            else
                            {
                                foreach (AIMap aim in aims)
                                {
                                    row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                    if (aim.AINo == ain.AINo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nIEC101Slaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 1] = aim.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 2] = aim.DataType;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 3] = aim.Deadband;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 4] = aim.Multiplier;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 5] = aim.Constant;
                                        }
                                        break;
                                    }
                                }
                            }
                            slaveOffset++;
                        }
                        //Show item depending on Status=Mapped/UnMapped AND whether it's a single slave...
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            #endregion IEC101Slave
        }
        private void addItemDIs(IED ied, string masterName)
        {
            #region AllSlavesDI
            if (cmbSlaves.GetItemText(cmbSlaves.SelectedItem).ToLower() == "all")
            {
                int slaveOffset = 0; bool isMapped = false;
                int arrHdrSize12 = 0;
                arrHdrSize12 = COLS_B4_MULTISLAVE;

                //IEC104Slave
                int nIEC104Slaves = getIEC104SlavesCount();
                if (nIEC104Slaves == 1) arrHdrSize12 = arrHdrSize12 + nIEC104Slaves + TOTAL_MAP_PARAMS;//COLS_B4_MULTISLAVE
                else arrHdrSize12 = arrHdrSize12 + nIEC104Slaves;//COLS_B4_MULTISLAVE
                Array.Resize(ref row, arrHdrSize12);

                //MODBUSSlave
                int nMODBUSSlaves = getMODBUSSlavesCount();
                if (nMODBUSSlaves == 1) arrHdrSize12 = arrHdrSize12 + nMODBUSSlaves + TOTAL_MAP_PARAMS;//COLS_B4_MULTISLAVE
                else arrHdrSize12 = arrHdrSize12 + +nMODBUSSlaves;//COLS_B4_MULTISLAVE 
                Array.Resize(ref row, arrHdrSize12);

                //IEC101Slave
                int nIEC101Slaves = getIEC101SlavesCount();
                if (nIEC101Slaves == 1) arrHdrSize12 = arrHdrSize12 + nIEC101Slaves + TOTAL_MAP_PARAMS;
                else arrHdrSize12 = arrHdrSize12 + nIEC101Slaves;//+COLS_B4_MULTISLAVE
                Array.Resize(ref row, arrHdrSize12);

                //---------------------------Default Row values--------------------------------------------//
                row[0] = masterName; row[1] = ied.UnitID;
                foreach (DI din in ied.getDIConfiguration().getDIs())
                {
                    if (din.IsNodeComment) continue;

                    row[2] = "DI";
                    row[3] = din.DINo;
                    row[4] = din.Description;
                    row[5] = din.Index;
                    row[6] = din.SubIndex;
                    row[7] = din.ResponseType;
                    row[8] = "";
                    row[9] = "";
                    row[10] = "";
                    row[11] = "";
                    row[12] = "";

                    #region IEC104Slave
                    //Reset map info cols...
                    if (nIEC104Slaves == 1)
                        for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    slaveOffset = 0;
                    isMapped = false;

                    foreach (IEC104Slave iecs in getIEC104Slaves())
                    {
                        List<DIMap> dims = ied.getDIConfiguration().getSlaveDIMaps(iecs.getSlaveID);
                        if (dims == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (DIMap dim in dims)
                            {
                                if (dim.DINo == din.DINo)
                                {
                                    isMapped = true;
                                    //If single slave, show it's map parameters as well...
                                    if (nIEC104Slaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }
                    #endregion IEC104Slave

                    #region MODBUS Slave
                    //Reset map info cols...
                    if (nMODBUSSlaves == 1)
                        for (int i = slaveOffset; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    isMapped = false;

                    foreach (MODBUSSlave iecs in getMODBUSSlaves())
                    {
                        List<DIMap> dims = ied.getDIConfiguration().getSlaveDIMaps(iecs.getSlaveID);
                        if (dims == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (DIMap dim in dims)
                            {
                                if (dim.DINo == din.DINo)
                                {
                                    isMapped = true;
                                    //If single slave, show it's map parameters as well...
                                    if (nMODBUSSlaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }

                    #endregion MODBUS Slave

                    #region IEC101 Slave
                    //Reset map info cols...
                    if (nIEC101Slaves == 1)
                        for (int i = slaveOffset; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    isMapped = false;

                    foreach (IEC101Slave iecs in getIEC101Slaves())
                    {
                        List<DIMap> dims = ied.getDIConfiguration().getSlaveDIMaps(iecs.getSlaveID);
                        if (dims == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (DIMap dim in dims)
                            {
                                if (dim.DINo == din.DINo)
                                {
                                    isMapped = true;
                                    //If single slave, show it's map parameters as well...
                                    if (nIEC101Slaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }

                    #endregion IEC101 Slave

                    #region WriteRows
                    //Show item depending on Status = Mapped / UnMapped AND whether it's a single slave...
                    if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                    {
                        ListViewItem lvItem = new ListViewItem(row);
                        lvMappingView.Items.Add(lvItem);
                    }
                    else
                    {
                        if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                    }
                    #endregion WriteRows
                }
            }

            #endregion AllSlavesDI

            #region IEC104SlaveDI
            foreach (IEC104Slave iecs1 in opcHandle.getSlaveConfiguration().getIEC104Group().getIEC104Slaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "IEC104 " + iecs1.SlaveNum)
                {
                    int nIEC104Slaves = getIEC104SlavesCount();
                    int arrHdrSize = 0;
                    if (nIEC104Slaves == 1) arrHdrSize = COLS_B4_MULTISLAVE + nIEC104Slaves + TOTAL_MAP_PARAMS;
                    else arrHdrSize = COLS_B4_MULTISLAVE + nIEC104Slaves;
                    string[] row = new string[arrHdrSize];

                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (DI din in ied.getDIConfiguration().getDIs())
                    {
                        if (din.IsNodeComment) continue;

                        row[2] = "DI";
                        row[3] = din.DINo;
                        row[4] = din.Description;
                        row[5] = din.Index;
                        row[6] = din.SubIndex;
                        row[7] = din.ResponseType;
                        row[8] = "";
                        row[9] = "";
                        row[10] = "";
                        row[11] = "";
                        row[12] = "";

                        //Reset map info cols...
                        if (nIEC104Slaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";

                        int slaveOffset = 0;
                        bool isMapped = false;
                        foreach (IEC104Slave iecs in getIEC104Slaves())
                        {
                            List<DIMap> dims = ied.getDIConfiguration().getSlaveDIMaps(iecs.getSlaveID);
                            if (dims == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            else
                            {
                                foreach (DIMap dim in dims)
                                {
                                    row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                    if (dim.DINo == din.DINo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nIEC104Slaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 1] = dim.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 2] = dim.DataType;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 6] = dim.BitPos;
                                        }
                                        break;
                                    }
                                }
                            }
                            slaveOffset++;
                        }

                        //Show item depending on Status=Mapped/UnMapped AND whether it's a single slave...
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }

            #endregion IEC104SlaveDI

            #region MODBUSSlaveDI
            foreach (MODBUSSlave ModbusSlave in opcHandle.getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "MODBUS " + ModbusSlave.SlaveNum)
                {
                    int nMODBUSSlaves = getMODBUSSlavesCount();
                    int arrHdrSizeMODBUS = 0;
                    if (nMODBUSSlaves == 1) arrHdrSizeMODBUS = COLS_B4_MULTISLAVE + nMODBUSSlaves + TOTAL_MAP_PARAMS;
                    else arrHdrSizeMODBUS = COLS_B4_MULTISLAVE + nMODBUSSlaves;
                    string[] row = new string[arrHdrSizeMODBUS];

                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (DI din in ied.getDIConfiguration().getDIs())
                    {
                        if (din.IsNodeComment) continue;

                        row[2] = "DI";
                        row[3] = din.DINo;
                        row[4] = din.Description;
                        row[5] = din.Index;
                        row[6] = din.SubIndex;
                        row[7] = din.ResponseType;
                        row[8] = "";
                        row[9] = "";
                        row[10] = "";
                        row[11] = "";
                        row[12] = "";

                        //Reset map info cols...
                        if (nMODBUSSlaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";

                        int MODBUSslaveOffset = 0;
                        bool isMapped = false;
                        foreach (MODBUSSlave iecs in getMODBUSSlaves())
                        {
                            List<DIMap> dims = ied.getDIConfiguration().getSlaveDIMaps(iecs.getSlaveID);
                            if (dims == null) row[COLS_B4_MULTISLAVE + MODBUSslaveOffset] = "transparentimg";
                            else
                            {
                                foreach (DIMap dim in dims)
                                {
                                    row[COLS_B4_MULTISLAVE + MODBUSslaveOffset] = "transparentimg";
                                    if (dim.DINo == din.DINo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + MODBUSslaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nMODBUSSlaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + MODBUSslaveOffset + 1] = dim.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + MODBUSslaveOffset + 2] = dim.DataType;
                                            row[COLS_B4_MULTISLAVE + MODBUSslaveOffset + 6] = dim.BitPos;
                                        }
                                        break;
                                    }
                                }
                            }
                            MODBUSslaveOffset++;
                        }

                        //Show item depending on Status=Mapped/UnMapped AND whether it's a single slave...
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            #endregion MODBUSSlaveDI

            #region IEC101Slave DI
            foreach (IEC101Slave iec101 in opcHandle.getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "IEC101 " + iec101.SlaveNum)
                {
                    int nIec101Slaves = getIEC101SlavesCount();
                    int arrHdrSizeMODBUS = 0;
                    if (nIec101Slaves == 1) arrHdrSizeMODBUS = COLS_B4_MULTISLAVE + nIec101Slaves + TOTAL_MAP_PARAMS;
                    else arrHdrSizeMODBUS = COLS_B4_MULTISLAVE + nIec101Slaves;
                    string[] row = new string[arrHdrSizeMODBUS];

                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (DI din in ied.getDIConfiguration().getDIs())
                    {
                        if (din.IsNodeComment) continue;

                        row[2] = "DI";
                        row[3] = din.DINo;
                        row[4] = din.Description;
                        row[5] = din.Index;
                        row[6] = din.SubIndex;
                        row[7] = din.ResponseType;
                        row[8] = "";
                        row[9] = "";
                        row[10] = "";
                        row[11] = "";
                        row[12] = "";

                        //Reset map info cols...
                        if (nIec101Slaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";

                        int slaveOffset = 0;
                        bool isMapped = false;
                        foreach (IEC101Slave iecs in getIEC101Slaves())
                        {
                            List<DIMap> dims = ied.getDIConfiguration().getSlaveDIMaps(iecs.getSlaveID);
                            if (dims == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            else
                            {
                                foreach (DIMap dim in dims)
                                {
                                    row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                    if (dim.DINo == din.DINo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nIec101Slaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 1] = dim.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 2] = dim.DataType;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 6] = dim.BitPos;
                                        }
                                        break;
                                    }
                                }
                            }
                            slaveOffset++;
                        }

                        //Show item depending on Status=Mapped/UnMapped AND whether it's a single slave...
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            #endregion MODBUSSlaveDI
        }
        private void addItemDOs(IED ied, string masterName)
        {
            #region AllSlavesDO
            if (cmbSlaves.GetItemText(cmbSlaves.SelectedItem).ToLower() == "all")
            {
                int slaveOffset = 0; bool isMapped = false;
                int arrHdrSize12 = 0;
                arrHdrSize12 = COLS_B4_MULTISLAVE;

                //IEC104Slave
                int nIEC104Slaves = getIEC104SlavesCount();
                if (nIEC104Slaves == 1) arrHdrSize12 = arrHdrSize12 + nIEC104Slaves + TOTAL_MAP_PARAMS;//COLS_B4_MULTISLAVE
                else arrHdrSize12 = arrHdrSize12 + nIEC104Slaves;//COLS_B4_MULTISLAVE
                Array.Resize(ref row, arrHdrSize12);

                //MODBUSSlave
                int nMODBUSSlaves = getMODBUSSlavesCount();
                if (nMODBUSSlaves == 1) arrHdrSize12 = arrHdrSize12 + nMODBUSSlaves + TOTAL_MAP_PARAMS;//COLS_B4_MULTISLAVE
                else arrHdrSize12 = arrHdrSize12 + +nMODBUSSlaves;//COLS_B4_MULTISLAVE 
                Array.Resize(ref row, arrHdrSize12);

                //IEC101Slave
                int nIEC101Slaves = getIEC101SlavesCount();
                if (nIEC101Slaves == 1) arrHdrSize12 = arrHdrSize12 + nIEC101Slaves + TOTAL_MAP_PARAMS;
                else arrHdrSize12 = arrHdrSize12 + nIEC101Slaves;//+COLS_B4_MULTISLAVE
                Array.Resize(ref row, arrHdrSize12);

                //---------------------------Default Row values--------------------------------------------//
                row[0] = masterName; row[1] = ied.UnitID;

                foreach (DO don in ied.getDOConfiguration().getDOs())
                {
                    if (don.IsNodeComment) continue;

                    row[2] = "DO";
                    row[3] = don.DONo;
                    row[4] = don.Description;
                    row[5] = don.Index;
                    row[6] = don.SubIndex;
                    row[7] = don.ResponseType;
                    row[8] = "";
                    row[9] = "";
                    row[10] = "";
                    row[11] = don.ControlType;
                    row[12] = don.PulseDurationMS;

                    #region IEC104Slave
                    //Reset map info cols...
                    if (nIEC104Slaves == 1)
                        for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    slaveOffset = 0;
                    isMapped = false;

                    foreach (IEC104Slave iecs in getIEC104Slaves())
                    {
                        List<DOMap> doms = ied.getDOConfiguration().getSlaveDOMaps(iecs.getSlaveID);
                        if (doms == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (DOMap dom in doms)
                            {
                                if (dom.DONo == don.DONo)
                                {
                                    isMapped = true;
                                    //If single slave, show it's map parameters as well...
                                    if (nIEC104Slaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }

                    #endregion IEC104Slave

                    #region MODBUS Slave
                    //Reset map info cols...
                    if (nMODBUSSlaves == 1)
                        for (int i = slaveOffset; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    isMapped = false;

                    foreach (MODBUSSlave iecs in getMODBUSSlaves())
                    {
                        List<DOMap> doms = ied.getDOConfiguration().getSlaveDOMaps(iecs.getSlaveID);
                        if (doms == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (DOMap dom in doms)
                            {

                                if (dom.DONo == don.DONo)
                                {
                                    isMapped = true;
                                    //If single slave, show it's map parameters as well...
                                    if (nMODBUSSlaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }
                    #endregion MODBUS Slave

                    #region IEC101 Slave
                    //Reset map info cols...
                    if (nIEC101Slaves == 1)
                        for (int i = slaveOffset; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    isMapped = false;

                    foreach (IEC101Slave iecs in getIEC101Slaves())
                    {
                        List<DOMap> doms = ied.getDOConfiguration().getSlaveDOMaps(iecs.getSlaveID);
                        if (doms == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (DOMap dom in doms)
                            {

                                if (dom.DONo == don.DONo)
                                {
                                    isMapped = true;
                                    row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    //If single slave, show it's map parameters as well...
                                    if (nIEC101Slaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }

                    #endregion IEC101 Slave

                    #region WriteRows
                    //Show item depending on Status = Mapped / UnMapped AND whether it's a single slave...
                    if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                    {
                        ListViewItem lvItem = new ListViewItem(row);
                        lvMappingView.Items.Add(lvItem);
                    }
                    else
                    {
                        if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                    }
                    #endregion WriteRows
                }
            }

            #endregion AllSlavesD)

            #region IEC104
            foreach (IEC104Slave iecs1 in opcHandle.getSlaveConfiguration().getIEC104Group().getIEC104Slaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "IEC104 " + iecs1.SlaveNum)
                {
                    int nIEC104Slaves = getIEC104SlavesCount();
                    int arrHdrSize = 0;
                    if (nIEC104Slaves == 1) arrHdrSize = COLS_B4_MULTISLAVE + nIEC104Slaves + TOTAL_MAP_PARAMS;
                    else arrHdrSize = COLS_B4_MULTISLAVE + nIEC104Slaves;
                    string[] row = new string[arrHdrSize];

                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (DO don in ied.getDOConfiguration().getDOs())
                    {
                        if (don.IsNodeComment) continue;

                        row[2] = "DO";
                        row[3] = don.DONo;
                        row[4] = don.Description;
                        row[5] = don.Index;
                        row[6] = don.SubIndex;
                        row[7] = don.ResponseType;
                        row[8] = "";
                        row[9] = "";
                        row[10] = "";
                        row[11] = don.ControlType;
                        row[12] = don.PulseDurationMS;

                        //Reset map info cols...
                        if (nIEC104Slaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";

                        int slaveOffset = 0;
                        bool isMapped = false;
                        foreach (IEC104Slave iecs in getIEC104Slaves())
                        {
                            List<DOMap> doms = ied.getDOConfiguration().getSlaveDOMaps(iecs.getSlaveID);
                            if (doms == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            else
                            {
                                foreach (DOMap dom in doms)
                                {
                                    row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                    if (dom.DONo == don.DONo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nIEC104Slaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 1] = dom.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 2] = dom.DataType;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 6] = dom.BitPos;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 7] = dom.Select;
                                        }
                                        break;
                                    }
                                }
                            }
                            slaveOffset++;
                        }

                        //Show item depending on Status=Mapped/UnMapped AND whether it's a single slave...
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            #endregion IEC104

            #region MODBUSSlave
            foreach (MODBUSSlave iecs1 in opcHandle.getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "MODBUS " + iecs1.SlaveNum)
                {
                    int nMODBUSSlaves = getMODBUSSlavesCount();
                    int arrHdrMODBUSSize = 0;
                    if (nMODBUSSlaves == 1) arrHdrMODBUSSize = COLS_B4_MULTISLAVE + nMODBUSSlaves + TOTAL_MAP_PARAMS;
                    else arrHdrMODBUSSize = COLS_B4_MULTISLAVE + nMODBUSSlaves;
                    string[] row = new string[arrHdrMODBUSSize];

                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (DO don in ied.getDOConfiguration().getDOs())
                    {
                        if (don.IsNodeComment) continue;

                        row[2] = "DO";
                        row[3] = don.DONo;
                        row[4] = don.Description;
                        row[5] = don.Index;
                        row[6] = don.SubIndex;
                        row[7] = don.ResponseType;
                        row[8] = "";
                        row[9] = "";
                        row[10] = "";
                        row[11] = don.ControlType;
                        row[12] = don.PulseDurationMS;

                        //Reset map info cols...
                        if (nMODBUSSlaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";

                        int ModbusslaveOffset = 0;
                        bool isMapped = false;
                        foreach (MODBUSSlave iecs in getMODBUSSlaves())
                        {
                            List<DOMap> doms = ied.getDOConfiguration().getSlaveDOMaps(iecs.getSlaveID);
                            if (doms == null) row[COLS_B4_MULTISLAVE + ModbusslaveOffset] = "transparentimg";
                            else
                            {
                                foreach (DOMap dom in doms)
                                {
                                    row[COLS_B4_MULTISLAVE + ModbusslaveOffset] = "transparentimg";
                                    if (dom.DONo == don.DONo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + ModbusslaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nMODBUSSlaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 1] = dom.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 2] = dom.DataType;
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 6] = dom.BitPos;
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 7] = dom.Select;
                                        }
                                        break;
                                    }
                                }
                            }
                            ModbusslaveOffset++;
                        }

                        //Show item depending on Status = Mapped / UnMapped AND whether it's a single slave...
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            #endregion MODBUSSLave

            #region IEC101Slave DO
            foreach (IEC101Slave iecs101 in opcHandle.getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "IEC101 " + iecs101.SlaveNum)
                {
                    int nIEC101Slaves = getIEC101SlavesCount();
                    int arrHdrMODBUSSize = 0;
                    if (nIEC101Slaves == 1) arrHdrMODBUSSize = COLS_B4_MULTISLAVE + nIEC101Slaves + TOTAL_MAP_PARAMS;
                    else arrHdrMODBUSSize = COLS_B4_MULTISLAVE + nIEC101Slaves;
                    string[] row = new string[arrHdrMODBUSSize];

                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (DO don in ied.getDOConfiguration().getDOs())
                    {
                        if (don.IsNodeComment) continue;

                        row[2] = "DO";
                        row[3] = don.DONo;
                        row[4] = don.Description;
                        row[5] = don.Index;
                        row[6] = don.SubIndex;
                        row[7] = don.ResponseType;
                        row[8] = "";
                        row[9] = "";
                        row[10] = "";
                        row[11] = don.ControlType;
                        row[12] = don.PulseDurationMS;

                        //Reset map info cols...
                        if (nIEC101Slaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";

                        int ModbusslaveOffset = 0;
                        bool isMapped = false;
                        foreach (IEC101Slave iecs in getIEC101Slaves())
                        {
                            List<DOMap> doms = ied.getDOConfiguration().getSlaveDOMaps(iecs.getSlaveID);
                            if (doms == null) row[COLS_B4_MULTISLAVE + ModbusslaveOffset] = "transparentimg";
                            else
                            {
                                foreach (DOMap dom in doms)
                                {
                                    row[COLS_B4_MULTISLAVE + ModbusslaveOffset] = "transparentimg";
                                    if (dom.DONo == don.DONo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + ModbusslaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nIEC101Slaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 1] = dom.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 2] = dom.DataType;
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 6] = dom.BitPos;
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 7] = dom.Select;
                                        }
                                        break;
                                    }
                                }
                            }
                            ModbusslaveOffset++;
                        }

                        //Show item depending on Status = Mapped / UnMapped AND whether it's a single slave...
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            #endregion IEC101Slave DO
        }
        private void addItemENs(IED ied, string masterName)
        {
            #region AllSlavesEN
            if (cmbSlaves.GetItemText(cmbSlaves.SelectedItem).ToLower() == "all")
            {
                int slaveOffset = 0; bool isMapped = false;
                int arrHdrSize12 = 0;
                arrHdrSize12 = COLS_B4_MULTISLAVE;

                //IEC104Slave
                int nIEC104Slaves = getIEC104SlavesCount();
                if (nIEC104Slaves == 1) arrHdrSize12 = arrHdrSize12 + nIEC104Slaves + TOTAL_MAP_PARAMS;//COLS_B4_MULTISLAVE
                else arrHdrSize12 = arrHdrSize12 + nIEC104Slaves;//COLS_B4_MULTISLAVE
                Array.Resize(ref row, arrHdrSize12);

                //MODBUSSlave
                int nMODBUSSlaves = getMODBUSSlavesCount();
                if (nMODBUSSlaves == 1) arrHdrSize12 = arrHdrSize12 + nMODBUSSlaves + TOTAL_MAP_PARAMS;//COLS_B4_MULTISLAVE
                else arrHdrSize12 = arrHdrSize12 + +nMODBUSSlaves;//COLS_B4_MULTISLAVE 
                Array.Resize(ref row, arrHdrSize12);

                //IEC101Slave
                int nIEC101Slaves = getIEC101SlavesCount();
                if (nIEC101Slaves == 1) arrHdrSize12 = arrHdrSize12 + nIEC101Slaves + TOTAL_MAP_PARAMS;
                else arrHdrSize12 = arrHdrSize12 + nIEC101Slaves;//+COLS_B4_MULTISLAVE
                Array.Resize(ref row, arrHdrSize12);

                //---------------------------Default Row values--------------------------------------------//
                row[0] = masterName; row[1] = ied.UnitID;

                foreach (EN enn in ied.getENConfiguration().getENs())
                {
                    if (enn.IsNodeComment) continue;

                    row[2] = "EN";
                    row[3] = enn.ENNo;
                    row[4] = enn.Description;
                    row[5] = enn.Index;
                    row[6] = enn.SubIndex;
                    row[7] = enn.ResponseType;
                    row[8] = enn.DataType;
                    row[9] = enn.Multiplier;
                    row[10] = enn.Constant;
                    row[11] = "";
                    row[12] = "";

                    #region IEC104Slave
                    //Reset map info cols...
                    if (nIEC104Slaves == 1)
                        for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    slaveOffset = 0;
                    isMapped = false;

                    foreach (IEC104Slave iecs in getIEC104Slaves())
                    {
                        List<ENMap> enms = ied.getENConfiguration().getSlaveENMaps(iecs.getSlaveID);
                        if (enms == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (ENMap enm in enms)
                            {
                                if (enm.ENNo == enn.ENNo)
                                {
                                    isMapped = true;
                                    //If single slave, show it's map parameters as well...
                                    if (nIEC104Slaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }

                    #endregion IEC104Slave

                    #region MODBUS Slave
                    //Reset map info cols...
                    if (nMODBUSSlaves == 1)
                        for (int i = slaveOffset; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    isMapped = false;

                    foreach (MODBUSSlave iecs in getMODBUSSlaves())
                    {
                        List<ENMap> enms = ied.getENConfiguration().getSlaveENMaps(iecs.getSlaveID);
                        if (enms == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (ENMap enm in enms)
                            {
                                if (enm.ENNo == enn.ENNo)
                                {
                                    isMapped = true;

                                    //If single slave, show it's map parameters as well...
                                    if (nMODBUSSlaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }
                    #endregion MODBUS Slave

                    #region IEC101 Slave
                    //Reset map info cols...
                    if (nIEC101Slaves == 1)
                        for (int i = slaveOffset; i <= TOTAL_MAP_PARAMS; i++)
                            row[COLS_B4_MULTISLAVE + i] = "";
                    isMapped = false;

                    foreach (IEC101Slave iecs in getIEC101Slaves())
                    {
                        List<ENMap> enms = ied.getENConfiguration().getSlaveENMaps(iecs.getSlaveID);
                        if (enms == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                        else
                        {
                            row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            foreach (ENMap enm in enms)
                            {
                                if (enm.ENNo == enn.ENNo)
                                {
                                    isMapped = true;

                                    //If single slave, show it's map parameters as well...
                                    if (nIEC101Slaves >= 1)
                                    {
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                    }
                                    break;
                                }
                            }
                        }
                        slaveOffset++;
                    }
                    #endregion IEC101 Slave

                    #region WriteRows
                    //Show item depending on Status = Mapped / UnMapped AND whether it's a single slave...
                    if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                    {
                        ListViewItem lvItem = new ListViewItem(row);
                        lvMappingView.Items.Add(lvItem);
                    }
                    else
                    {
                        if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                    }
                    #endregion WriteRows
                }
            }

            #endregion AllSlavesEN

            #region IEC104Slave
            foreach (IEC104Slave iecs1 in opcHandle.getSlaveConfiguration().getIEC104Group().getIEC104Slaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "IEC104 " + iecs1.SlaveNum)
                {
                    int nIEC104Slaves = getIEC104SlavesCount();
                    int arrHdrSize = 0;
                    if (nIEC104Slaves == 1) arrHdrSize = COLS_B4_MULTISLAVE + nIEC104Slaves + TOTAL_MAP_PARAMS;
                    else arrHdrSize = COLS_B4_MULTISLAVE + nIEC104Slaves;
                    string[] row = new string[arrHdrSize];

                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (EN enn in ied.getENConfiguration().getENs())
                    {
                        if (enn.IsNodeComment) continue;

                        row[2] = "EN";
                        row[3] = enn.ENNo;
                        row[4] = enn.Description;
                        row[5] = enn.Index;
                        row[6] = enn.SubIndex;
                        row[7] = enn.ResponseType;
                        row[8] = enn.DataType;
                        row[9] = enn.Multiplier;
                        row[10] = enn.Constant;
                        row[11] = "";
                        row[12] = "";

                        //Reset map info cols...
                        if (nIEC104Slaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";

                        int slaveOffset = 0;//condn slave=0,1,>1 respect filters
                        bool isMapped = false;
                        foreach (IEC104Slave iecs in getIEC104Slaves())
                        {
                            List<ENMap> enms = ied.getENConfiguration().getSlaveENMaps(iecs.getSlaveID);
                            if (enms == null) row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                            else
                            {
                                foreach (ENMap enm in enms)
                                {
                                    row[COLS_B4_MULTISLAVE + slaveOffset] = "transparentimg";
                                    if (enm.ENNo == enn.ENNo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + slaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nIEC104Slaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 1] = enm.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 2] = enm.DataType;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 3] = enm.Deadband;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 4] = enm.Multiplier;
                                            row[COLS_B4_MULTISLAVE + slaveOffset + 5] = enm.Constant;
                                        }
                                        break;
                                    }
                                }
                            }
                            slaveOffset++;
                        }
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            #endregion IEC104Slave

            #region MODBUSSlave
            foreach (MODBUSSlave iecs1 in opcHandle.getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "MODBUS " + iecs1.SlaveNum)
                {
                    int nMODBUSSLaves = getMODBUSSlavesCount();
                    int arrHdrModbusSize = 0;
                    if (nMODBUSSLaves == 1) arrHdrModbusSize = COLS_B4_MULTISLAVE + nMODBUSSLaves + TOTAL_MAP_PARAMS;
                    else arrHdrModbusSize = COLS_B4_MULTISLAVE + nMODBUSSLaves;
                    string[] row = new string[arrHdrModbusSize];

                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (EN enn in ied.getENConfiguration().getENs())
                    {
                        if (enn.IsNodeComment) continue;

                        row[2] = "EN";
                        row[3] = enn.ENNo;
                        row[4] = enn.Description;
                        row[5] = enn.Index;
                        row[6] = enn.SubIndex;
                        row[7] = enn.ResponseType;
                        row[8] = enn.DataType;
                        row[9] = enn.Multiplier;
                        row[10] = enn.Constant;
                        row[11] = "";
                        row[12] = "";
                        //Reset map info cols...
                        if (nMODBUSSLaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";

                        int ModbusslaveOffset = 0;//condn slave=0,1,>1 respect filters
                        bool isMapped = false;
                        foreach (MODBUSSlave iecs in getMODBUSSlaves())
                        {
                            List<ENMap> enms = ied.getENConfiguration().getSlaveENMaps(iecs.getSlaveID);
                            if (enms == null) row[COLS_B4_MULTISLAVE + ModbusslaveOffset] = "transparentimg";
                            else
                            {
                                foreach (ENMap enm in enms)
                                {
                                    row[COLS_B4_MULTISLAVE + ModbusslaveOffset] = "transparentimg";
                                    if (enm.ENNo == enn.ENNo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + ModbusslaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nMODBUSSLaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 1] = enm.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 2] = enm.DataType;
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 3] = enm.Deadband;
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 4] = enm.Multiplier;
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 5] = enm.Constant;
                                        }
                                        break;
                                    }
                                }
                            }
                            ModbusslaveOffset++;
                        }
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            #endregion MODBUSSlave

            #region IEC101Slave EN
            foreach (IEC101Slave iecs1 in opcHandle.getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
            {
                if (cmbSlaves.SelectedItem.ToString() == "IEC101 " + iecs1.SlaveNum)
                {
                    int nIEC101SLaves = getIEC101SlavesCount();
                    int arrHdrModbusSize = 0;
                    if (nIEC101SLaves == 1) arrHdrModbusSize = COLS_B4_MULTISLAVE + nIEC101SLaves + TOTAL_MAP_PARAMS;
                    else arrHdrModbusSize = COLS_B4_MULTISLAVE + nIEC101SLaves;
                    string[] row = new string[arrHdrModbusSize];

                    row[0] = masterName; row[1] = ied.UnitID;
                    foreach (EN enn in ied.getENConfiguration().getENs())
                    {
                        if (enn.IsNodeComment) continue;

                        row[2] = "EN";
                        row[3] = enn.ENNo;
                        row[4] = enn.Description;
                        row[5] = enn.Index;
                        row[6] = enn.SubIndex;
                        row[7] = enn.ResponseType;
                        row[8] = enn.DataType;
                        row[9] = enn.Multiplier;
                        row[10] = enn.Constant;
                        row[11] = "";
                        row[12] = "";
                        //Reset map info cols...
                        if (nIEC101SLaves == 1)
                            for (int i = 1; i <= TOTAL_MAP_PARAMS; i++)
                                row[COLS_B4_MULTISLAVE + i] = "";
                        int ModbusslaveOffset = 0;//condn slave=0,1,>1 respect filters
                        bool isMapped = false;
                        foreach (IEC101Slave iecs in getIEC101Slaves())
                        {
                            List<ENMap> enms = ied.getENConfiguration().getSlaveENMaps(iecs.getSlaveID);
                            if (enms == null) row[COLS_B4_MULTISLAVE + ModbusslaveOffset] = "transparentimg";
                            else
                            {
                                foreach (ENMap enm in enms)
                                {
                                    row[COLS_B4_MULTISLAVE + ModbusslaveOffset] = "transparentimg";
                                    if (enm.ENNo == enn.ENNo)
                                    {
                                        isMapped = true;
                                        row[COLS_B4_MULTISLAVE + ModbusslaveOffset] = "yes";
                                        //If single slave, show it's map parameters as well...
                                        if (nIEC101SLaves == 1)
                                        {
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 1] = enm.ReportingIndex;
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 2] = enm.DataType;
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 3] = enm.Deadband;
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 4] = enm.Multiplier;
                                            row[COLS_B4_MULTISLAVE + ModbusslaveOffset + 5] = enm.Constant;
                                        }
                                        break;
                                    }
                                }
                            }
                            ModbusslaveOffset++;
                        }
                        if (!singleSlave || cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "all")//Only then can we respect Mapped/UnMapped...
                        {
                            ListViewItem lvItem = new ListViewItem(row);
                            lvMappingView.Items.Add(lvItem);
                        }
                        else
                        {
                            if (isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "mapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                            else if (!isMapped && cmbStatus.GetItemText(cmbStatus.SelectedItem).ToLower() == "unmapped")
                            {
                                ListViewItem lvItem = new ListViewItem(row);
                                lvMappingView.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
            #endregion MODBUSSlave
        }
        private int getIEC104SlavesCount()
        {
            if (singleSlave) return 1;
            else return opcHandle.getSlaveConfiguration().getIEC104Group().getCount();
        }
        private List<IEC104Slave> getIEC104Slaves()
        {
            return opcHandle.getSlaveConfiguration().getIEC104Group().getIEC104SlavesByFilter(((SlaveItem)cmbSlaves.SelectedItem).SlaveID);
        }
        private List<IEC103Master> getIEC103Masters()
        {
            return opcHandle.getMasterConfiguration().getIEC103Group().getIEC103MastersByFilter(((MasterItem)cmbMaster.SelectedItem).MasterID);
        }
        private List<MODBUSMaster> getMODBUSMasters()
        {
            return opcHandle.getMasterConfiguration().getMODBUSGroup().getMODBUSMastersByFilter(((MasterItem)cmbMaster.SelectedItem).MasterID);
        }
        private List<VirtualMaster> getVirtualMasters()
        {
            return opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMastersByFilter(((MasterItem)cmbMaster.SelectedItem).MasterID);
        }
        //Namrata:6/7/2017
        private List<IEC101Master> getIEC101Masters()
        {
            return opcHandle.getMasterConfiguration().getIEC101Group().getIEC101MastersByFilter(((MasterItem)cmbMaster.SelectedItem).MasterID);
        }
        private List<ADRMaster> getADRMasters()
        {
            return opcHandle.getMasterConfiguration().getADRMasterGroup().getADRMastersByFilter(((MasterItem)cmbMaster.SelectedItem).MasterID);
        }
        private List<MODBUSSlave> getMODBUSSlaves()
        {
            return opcHandle.getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlavesByFilter(((SlaveItem)cmbSlaves.SelectedItem).SlaveID);
        }
        private List<IEC101Slave> getIEC101Slaves()
        {
            return opcHandle.getSlaveConfiguration().getIEC101SlaveGroup().getIEC101SlavesByFilter(((SlaveItem)cmbSlaves.SelectedItem).SlaveID);
        }
        private int getIEC101SlavesCount()
        {
            if (singleSlave) return 1;
            else return opcHandle.getSlaveConfiguration().getIEC101SlaveGroup().getCount();
        }
        private int getMODBUSSlavesCount()
        {
            if (singleSlave) return 1;
            else return opcHandle.getSlaveConfiguration().getMODBUSSlaveGroup().getCount();
        }
        private List<IED> getIEDs(MasterTypes masterType, string masterID)
        {
            List<IED> iedList = new List<IED>();
            if (MasterTypes.UNKNOWN == masterType || MasterTypes.IEC101 == masterType)
            {
                foreach (IED ied in opcHandle.getMasterConfiguration().getIEC101Group().getIEC101IEDsByFilter(masterID))
                {
                    iedList.Add(ied);
                }
            }
            if (MasterTypes.UNKNOWN == masterType || MasterTypes.ADR == masterType)
            {
                foreach (IED ied in opcHandle.getMasterConfiguration().getADRMasterGroup().getIEC103IEDsByFilter(masterID))
                {
                    iedList.Add(ied);
                }
            }
            if (MasterTypes.UNKNOWN == masterType || MasterTypes.IEC103 == masterType)
            {
                foreach (IED ied in opcHandle.getMasterConfiguration().getIEC103Group().getIEC103IEDsByFilter(masterID))
                {
                    iedList.Add(ied);
                }
            }
            if (MasterTypes.UNKNOWN == masterType || MasterTypes.MODBUS == masterType)
            {
                foreach (IED ied in opcHandle.getMasterConfiguration().getMODBUSGroup().getMODBUSIEDsByFilter(masterID))
                {
                    iedList.Add(ied);
                }
            }
            if (MasterTypes.UNKNOWN == masterType || MasterTypes.Virtual == masterType)
            {
                foreach (IED ied in opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualIEDsByFilter(masterID))
                {
                    iedList.Add(ied);
                }
            }
            return iedList;
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            Console.WriteLine("****** Refresh btn");
            singleSlave = false;
            lvMappingView.Clear();
            addMappingViewHeaders();
            btnExportToExcel.Enabled = true;
            #region IEC101Master
            //Namrata:6/7/2017
            //Load IEC101 Masters info...
            if (cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "all" || cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "iec101")
            {
                foreach (IEC101Master mbm in getIEC101Masters())
                {
                    if (mbm.IsNodeComment) continue;
                    foreach (IED ied in mbm.getIEDsByFilter(((IEDItem)cmbIED.SelectedItem).IEDID))
                    {
                        if (ied.IsNodeComment) continue;
                        if (chkAI.Checked) addItemAIs(ied, "IEC101 " + mbm.MasterNum);
                        //Namrata: 24/11/2017
                        if (chkAO.Checked) addItemAOs(ied, "IEC101 " + mbm.MasterNum);
                        if (chkDI.Checked) addItemDIs(ied, "IEC101 " + mbm.MasterNum);
                        if (chkDO.Checked) addItemDOs(ied, "IEC101 " + mbm.MasterNum);
                        if (chkEN.Checked) addItemENs(ied, "IEC101 " + mbm.MasterNum);
                    }
                }
            }
            #endregion IEC101Master

            #region ADRMaster
            if (cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "all" || cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "adr")
            {
                foreach (ADRMaster mbm in getADRMasters())
                {
                    if (mbm.IsNodeComment) continue;
                    foreach (IED ied in mbm.getIEDsByFilter(((IEDItem)cmbIED.SelectedItem).IEDID))
                    {
                        if (ied.IsNodeComment) continue;
                        if (chkAI.Checked) addItemAIs(ied, "ADR " + mbm.MasterNum);
                        //Namrata: 24/11/2017
                        if (chkAO.Checked) addItemAOs(ied, "ADR " + mbm.MasterNum);
                        if (chkDI.Checked) addItemDIs(ied, "ADR " + mbm.MasterNum);
                        if (chkDO.Checked) addItemDOs(ied, "ADR " + mbm.MasterNum);
                        if (chkEN.Checked) addItemENs(ied, "ADR " + mbm.MasterNum);
                    }
                }
            }
            #endregion ADRMaster

            #region  IEC103Master
            //Load IEC103 Masters info...
            if (cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "all" || cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "iec103")
            {
                foreach (IEC103Master iecm in getIEC103Masters())
                {
                    if (iecm.IsNodeComment) continue;
                    foreach (IED ied in iecm.getIEDsByFilter(((IEDItem)cmbIED.SelectedItem).IEDID))
                    {
                        if (ied.IsNodeComment) continue;
                        if (chkAI.Checked) addItemAIs(ied, "IEC103 " + iecm.MasterNum);
                        //Namrata: 24/11/2017
                        if (chkAO.Checked) addItemAOs(ied, "IEC103 " + iecm.MasterNum);
                        if (chkDI.Checked) addItemDIs(ied, "IEC103 " + iecm.MasterNum);
                        if (chkDO.Checked) addItemDOs(ied, "IEC103 " + iecm.MasterNum);
                        if (chkEN.Checked) addItemENs(ied, "IEC103 " + iecm.MasterNum);
                    }
                }
            }
            #endregion  IEC103Master

            #region MODBUSMaster
            //Load MODBUS Masters info...
            if (cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "all" || cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "modbus")
            {
                foreach (MODBUSMaster mbm in getMODBUSMasters())
                {
                    if (mbm.IsNodeComment) continue;
                    foreach (IED ied in mbm.getIEDsByFilter(((IEDItem)cmbIED.SelectedItem).IEDID))
                    {
                        if (ied.IsNodeComment) continue;
                        if (chkAI.Checked) addItemAIs(ied, "MODBUS " + mbm.MasterNum);
                        //Namrata: 24/11/2017
                        if (chkAO.Checked) addItemAOs(ied, "MODBUS " + mbm.MasterNum);
                        if (chkDI.Checked) addItemDIs(ied, "MODBUS " + mbm.MasterNum);
                        if (chkDO.Checked) addItemDOs(ied, "MODBUS " + mbm.MasterNum);
                        if (chkEN.Checked) addItemENs(ied, "MODBUS " + mbm.MasterNum);
                    }
                }
            }
            #endregion MODBUSMaster

            #region VirtualMaster
            //Load Virtual Masters info...
            if (cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "all" || cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "virtual")
            {
                foreach (VirtualMaster vm in getVirtualMasters())
                {
                    if (vm.IsNodeComment) continue;
                    foreach (IED ied in vm.getIEDsByFilter(((IEDItem)cmbIED.SelectedItem).IEDID))
                    {
                        if (ied.IsNodeComment) continue;
                        if (chkAI.Checked) addItemAIs(ied, "Virtual " + vm.MasterNum);
                        //Namrata: 24/11/2017
                        if (chkAO.Checked) addItemAOs(ied, "Virtual " + vm.MasterNum);
                        if (chkDI.Checked) addItemDIs(ied, "Virtual " + vm.MasterNum);
                        if (chkDO.Checked) addItemDOs(ied, "Virtual " + vm.MasterNum);
                        if (chkEN.Checked) addItemENs(ied, "Virtual " + vm.MasterNum);
                    }
                }
            }
            #endregion VirtualMaster

            //Namrata: 04/12/2017
            tspCount.Visible = true;
            TspLabelCount.Visible = true;
            tspCount.Text = lvMappingView.Items.Count.ToString();
        }
        private void addMappingViewHeaders()
        {
            #region ColumnNames
            lvMappingView.Columns.Add("Master", 80, HorizontalAlignment.Center);
            lvMappingView.Columns.Add("IED", 45, HorizontalAlignment.Center);
            lvMappingView.Columns.Add("Point", 48, HorizontalAlignment.Center);
            lvMappingView.Columns.Add("No.", 45, HorizontalAlignment.Center);
            lvMappingView.Columns[lvMappingView.Columns.Count - 1].Tag = "int";
            lvMappingView.Columns.Add("Description", 110, HorizontalAlignment.Center);
            lvMappingView.Columns.Add("Index", 70, HorizontalAlignment.Center);
            lvMappingView.Columns[lvMappingView.Columns.Count - 1].Tag = "int";
            lvMappingView.Columns.Add("SubIdx", 70, HorizontalAlignment.Center);
            lvMappingView.Columns[lvMappingView.Columns.Count - 1].Tag = "int";
            lvMappingView.Columns.Add("Response Type", 120, HorizontalAlignment.Center);
            lvMappingView.Columns.Add("Data Type", 190, HorizontalAlignment.Center);
            lvMappingView.Columns.Add("Multiplier", 80, HorizontalAlignment.Center);
            lvMappingView.Columns[lvMappingView.Columns.Count - 1].Tag = "int";
            lvMappingView.Columns.Add("Constant", 80, HorizontalAlignment.Center);
            lvMappingView.Columns[lvMappingView.Columns.Count - 1].Tag = "int";
            lvMappingView.Columns.Add("Control", 70, HorizontalAlignment.Center);
            lvMappingView.Columns.Add("Pulse", 70, HorizontalAlignment.Center);
            lvMappingView.Columns[lvMappingView.Columns.Count - 1].Tag = "int";
            #endregion ColumnNames

            #region AllSlaves
            if (cmbSlaves.GetItemText(cmbSlaves.SelectedItem).ToLower() == "all")
            {
                foreach (IEC104Slave iecs in opcHandle.getSlaveConfiguration().getIEC104Group().getIEC104Slaves())
                {
                    lvMappingView.Columns.Add("IEC104 " + iecs.SlaveNum, 80, HorizontalAlignment.Center);
                }
                //if (opcHandle.getSlaveConfiguration().getIEC104Group().getCount() == 1) singleSlave = true;
                foreach (MODBUSSlave iecs1 in opcHandle.getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())
                {
                    lvMappingView.Columns.Add("MODBUS " + iecs1.SlaveNum, 80, HorizontalAlignment.Center);
                }
                //if (opcHandle.getSlaveConfiguration().getMODBUSSlaveGroup().getCount() == 1) singleSlave = true;
                foreach (IEC101Slave iec in opcHandle.getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
                {
                    lvMappingView.Columns.Add("IEC101 " + iec.SlaveNum, 80, HorizontalAlignment.Center);
                }
                //if (opcHandle.getSlaveConfiguration().getIEC101SlaveGroup().getCount() == 1) singleSlave = true;
            }
            else
            {
                lvMappingView.Columns.Add(cmbSlaves.GetItemText(cmbSlaves.SelectedItem), 80, HorizontalAlignment.Center);
                singleSlave = true;
            }
            #endregion AllSlaves
            //If single slave, show map parameters...
            if (singleSlave)
            {
                lvMappingView.Columns.Add("ReportingIndex", 110, HorizontalAlignment.Center);
                lvMappingView.Columns[lvMappingView.Columns.Count - 1].Tag = "int";
                lvMappingView.Columns.Add("DataType", 180, HorizontalAlignment.Center);
                lvMappingView.Columns.Add("Deadband", 90, HorizontalAlignment.Center);
                lvMappingView.Columns.Add("Slave_Multiplier", 150, HorizontalAlignment.Center);
                lvMappingView.Columns.Add("Slave_Constant", 150, HorizontalAlignment.Center);
                lvMappingView.Columns.Add("Bit", 60, HorizontalAlignment.Center);
                lvMappingView.Columns.Add("Select", 80, HorizontalAlignment.Center);
            }
        }
        private void lvMappingView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Black, e.Bounds);
            e.DrawText();
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                e.DrawBackground();
                using (Font headerFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold)) //Font size!!!!
                {
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Navy, e.Bounds, sf);
                }
            }
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 2), 0, 23, 2500, e.Bounds.Height - 2);
        }
        private void lvMappingView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex >= COLS_B4_MULTISLAVE && e.SubItem.Text == "yes")
            {
                e.DrawDefault = false;
                e.DrawBackground();
                Rectangle tPos = new Rectangle(e.SubItem.Bounds.X + ((e.SubItem.Bounds.Width - e.SubItem.Bounds.Height) / 2), e.SubItem.Bounds.Y, e.SubItem.Bounds.Height, e.SubItem.Bounds.Height);
                e.Graphics.DrawImage((Image)Properties.Resources.ResourceManager.GetObject("greenindicator"), tPos);
            }
            else if (e.SubItem.Text == "transparentimg")
            {
                e.DrawDefault = false;
                e.DrawBackground();
                Rectangle tPos = new Rectangle(e.SubItem.Bounds.X + ((e.SubItem.Bounds.Width - e.SubItem.Bounds.Height) / 2), e.SubItem.Bounds.Y, e.SubItem.Bounds.Height, e.SubItem.Bounds.Height);
                e.Graphics.DrawImage((Image)Properties.Resources.ResourceManager.GetObject("transparent"), tPos);
            }
            else
            {
                e.DrawDefault = true;
            }
        }
        private void lvMappingView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn)
            {
                sortColumn = e.Column;
                lvMappingView.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (lvMappingView.Sorting == SortOrder.Ascending)
                    lvMappingView.Sorting = SortOrder.Descending;
                else
                    lvMappingView.Sorting = SortOrder.Ascending;
            }
            // Call the sort method to manually sort.
            lvMappingView.Sort();
            lvMappingView.ListViewItemSorter = new ListViewItemComparer(e.Column, lvMappingView.Sorting, lvMappingView.Columns[e.Column].Tag);
        }
        // Implements the manual sorting of items by column.
        class ListViewItemComparer : IComparer
        {
            private int col;
            private SortOrder order;
            private int dataType = 0;//0->string, 1->int
            public ListViewItemComparer()
            {
                col = 0;
                order = SortOrder.Ascending;
            }
            public ListViewItemComparer(int column, SortOrder order, object objType)
            {
                col = column;
                this.order = order;
                //IMP: Default should be 'string' as we can use Tag to store other info as well...
                if (objType != null && objType.ToString() == "int") dataType = 1;
            }
            public int Compare(object x, object y)
            {
                int returnVal = -1;
                if (dataType == 0)
                {
                    //string
                    returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                }
                else if (dataType == 1)
                {
                    //int
                    string ix = "0", iy = "0";
                    if (((ListViewItem)x).SubItems[col].Text == "") ix = "0";
                    else ix = ((ListViewItem)x).SubItems[col].Text;
                    if (((ListViewItem)y).SubItems[col].Text == "") iy = "0";
                    else iy = ((ListViewItem)y).SubItems[col].Text;
                    returnVal = CompareInt(ix, iy);
                }
                // Determine whether the sort order is descending.
                // Invert the value returned by String.Compare.
                if (order == SortOrder.Descending) returnVal *= -1;
                return returnVal;
            }

            private int CompareInt(string x, string y)
            {
                int ix, iy;
                try
                {
                    ix = Int32.Parse(x);
                }
                catch (System.FormatException)
                {
                    ix = 0;
                }
                try
                {
                    iy = Int32.Parse(y);
                }
                catch (System.FormatException)
                {
                    iy = 0;
                }
                return ix - iy;
            }
        }
        private void FillOptions()
        {
            Console.WriteLine("*** filling options");
            cmbMasterType.Items.Add(new MasterTypeItem("ALL", MasterTypes.UNKNOWN));
            foreach (MasterTypes mt in Enum.GetValues(typeof(MasterTypes)))
            {
                if (mt.ToString().ToLower() == "unknown") continue;
                cmbMasterType.Items.Add(new MasterTypeItem(mt.ToString(), mt));
            }
            //Fill status combo
            cmbStatus.Items.Add("ALL");
            cmbStatus.Items.Add("Mapped");
            cmbStatus.Items.Add("UnMapped");
        }
        private void ResetFilters()
        {
            //IMP: Don't change sequence for combobox...
            //Clear All Items .
            //Namrata: 10/08/2017
            lvMappingView.Columns.Clear();
            lvMappingView.Items.Clear();
            cmbIED.Items.Clear();
            cmbIED.Items.Add(new IEDItem("ALL", "ALL", MasterTypes.UNKNOWN));
            //FIXME: Add ALL + actual IED
            cmbMaster.Items.Clear();
            cmbMaster.Items.Add(new MasterItem("ALL", "ALL", MasterTypes.UNKNOWN));
            //FIXME: Add ALL + actual masters
            Console.WriteLine("*** Resetting filters...");
            cmbMasterType.SelectedIndex = cmbMasterType.FindStringExact("ALL");
            cmbMaster.SelectedIndex = cmbMaster.FindStringExact("ALL");
            cmbIED.SelectedIndex = cmbIED.FindStringExact("ALL");
            chkAI.Checked = true;
            chkAO.Checked = true;
            chkDI.Checked = true;
            chkDO.Checked = true;
            chkEN.Checked = true;
            cmbStatus.SelectedIndex = cmbStatus.FindStringExact("ALL");
            cmbSlaves.Items.Clear();
            cmbSlaves.Items.Add(new SlaveItem("ALL", "ALL"));

            #region Add Slaves In ComboBox
            foreach (IEC104Slave iec104 in opcHandle.getSlaveConfiguration().getIEC104Group().getIEC104Slaves())
            {
                if (iec104.IsNodeComment) continue;
                cmbSlaves.Items.Add(new SlaveItem("IEC104 " + iec104.SlaveNum, iec104.getSlaveID));
            }
            foreach (MODBUSSlave modbus in opcHandle.getSlaveConfiguration().getMODBUSSlaveGroup().getMODBUSSlaves())
            {
                if (modbus.IsNodeComment) continue;
                cmbSlaves.Items.Add(new SlaveItem("MODBUS " + modbus.SlaveNum, modbus.getSlaveID));
            }
            foreach (IEC101Slave iec in opcHandle.getSlaveConfiguration().getIEC101SlaveGroup().getIEC101Slaves())
            {
                if (iec.IsNodeComment) continue;
                cmbSlaves.Items.Add(new SlaveItem("IEC101 " + iec.SlaveNum, iec.getSlaveID));
            }
            cmbSlaves.SelectedIndex = cmbSlaves.FindStringExact("ALL");
            #endregion Add Slaves In ComboBox
        }
        private void frmOverview_Load(object sender, EventArgs e)
        {
            Utils.SetFormIcon(this, "Overview1");
            //Namrata: 04/12/2017
            TspLabelCount.Visible = false;
            tspCount.Visible = false;
            FillOptions();
            ResetFilters();
            btnExportToExcel.Enabled = false;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetFilters();
        }
        private void cmbMasterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("*** Index changed: {0}", cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower());
            cmbMaster.Items.Clear();
            cmbMaster.Items.Add(new MasterItem("ALL", "ALL", MasterTypes.UNKNOWN));

            #region ADRMaster
            //Namrata:6/7/2017
            if (cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "all" || cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "adr")
            {
                foreach (ADRMaster adr in opcHandle.getMasterConfiguration().getADRMasterGroup().getADRMasters())
                {
                    if (adr.IsNodeComment) continue;
                    cmbMaster.Items.Add(new MasterItem("ADR " + adr.MasterNum, adr.getMasterID, MasterTypes.ADR));
                }
            }
            #endregion ADRMaster

            #region IEC101Master
            //Namrata:6/7/2017
            if (cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "all" || cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "iec101")
            {
                foreach (IEC101Master iec101 in opcHandle.getMasterConfiguration().getIEC101Group().getIEC101Masters())
                {
                    if (iec101.IsNodeComment) continue;
                    cmbMaster.Items.Add(new MasterItem("IEC101 " + iec101.MasterNum, iec101.getMasterID, MasterTypes.IEC101));
                }
            }
            #endregion IEC101Master

            #region IEC103Master
            if (cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "all" || cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "iec103")
            {
                foreach (IEC103Master iec103 in opcHandle.getMasterConfiguration().getIEC103Group().getIEC103Masters())
                {
                    if (iec103.IsNodeComment) continue;
                    cmbMaster.Items.Add(new MasterItem("IEC103 " + iec103.MasterNum, iec103.getMasterID, MasterTypes.IEC103));
                }
            }
            #endregion IEC103Master

            //#region MODBUSMaster
            if (cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "all" || cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "modbus")
            {
                foreach (MODBUSMaster mbm in opcHandle.getMasterConfiguration().getMODBUSGroup().getMODBUSMasters())
                {
                    if (mbm.IsNodeComment) continue;
                    cmbMaster.Items.Add(new MasterItem("MODBUS " + mbm.MasterNum, mbm.getMasterID, MasterTypes.MODBUS));
                }
            }
            //#endregion MODBUSMaster
            #region MODBUSMaster
            ////if (cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "all" || cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "modbus")
            ////{
            ////    foreach (MODBUSMaster mbm in opcHandle.getMasterConfiguration().getMODBUSGroup().getMODBUSMasters())
            ////    {
            ////        if (mbm.IsNodeComment) continue;
            ////        cmbMaster.Items.Add(new MasterItem("MODBUS_" + mbm.MasterNum, mbm.getMasterID, MasterTypes.MODBUS));
            ////    }
            ////}
            #endregion MODBUSMaster

            #region VirtualMaster
            if (cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "all" || cmbMasterType.GetItemText(cmbMasterType.SelectedItem).ToLower() == "virtual")
            {
                foreach (VirtualMaster vm in opcHandle.getMasterConfiguration().getVirtualGroup().getVirtualMasters())
                {
                    if (vm.IsNodeComment) continue;
                    cmbMaster.Items.Add(new MasterItem("Virtual " + vm.MasterNum, vm.getMasterID, MasterTypes.Virtual));
                }
            }
            #endregion VirtualMaster
            cmbMaster.SelectedIndex = cmbMaster.FindStringExact("ALL");
        }
        private void cmbMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIED.Items.Clear();
            cmbIED.Items.Add(new IEDItem("ALL", "ALL", MasterTypes.UNKNOWN));

            foreach (IED ied in getIEDs(((MasterTypeItem)cmbMasterType.SelectedItem).MasterType, ((MasterItem)cmbMaster.SelectedItem).MasterID))
            {
                cmbIED.Items.Add(new IEDItem("IED " + ied.UnitID, ied.getIEDID, MasterTypes.UNKNOWN));//FIXME: IED should return it's master Type in property
            }
            cmbIED.SelectedIndex = cmbIED.FindStringExact("ALL");
        }
        //Inner classes defined here...
        private class SlaveItem : object
        {
            protected string sName;
            protected string sID;

            public SlaveItem(string slaveName, string slaveID)
            {
                sName = slaveName;
                sID = slaveID;
            }

            public override string ToString()
            {
                return sName;
            }

            public string SlaveID
            {
                get { return sID; }
            }
        }
        private class MasterTypeItem : object
        {
            protected string mtName;
            protected MasterTypes mt;

            public MasterTypeItem(string masterTypeName, MasterTypes mType)
            {
                mtName = masterTypeName;
                mt = mType;
            }

            public override string ToString()
            {
                return mtName;
            }

            public MasterTypes MasterType
            {
                get { return mt; }
            }
        }
        private class MasterItem : object
        {
            protected string mName;
            protected string mID;
            protected MasterTypes mt;

            public MasterItem(string masterName, string masterID, MasterTypes mType)
            {
                mName = masterName;
                mID = masterID;
                mt = mType;
            }

            public override string ToString()
            {
                return mName;
            }

            public string MasterID
            {
                get { return mID; }
            }

            public MasterTypes MasterType
            {
                get { return mt; }
            }
        }
        private class IEDItem : object
        {
            protected string iName;
            protected string iID;
            protected MasterTypes mt;

            public IEDItem(string iedName, string iedID, MasterTypes mType)
            {
                iName = iedName;
                iID = iedID;
                mt = mType;
            }

            public override string ToString()
            {
                return iName;
            }

            public string IEDID
            {
                get { return iID; }
            }

            public MasterTypes MasterType
            {
                get { return mt; }
            }
        }
        private void cmbSlaves_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSlaves.GetItemText(cmbSlaves.SelectedItem).ToLower() == "all")
            {
                cmbStatus.SelectedItem = "ALL";
                cmbStatus.Enabled = false;
            }
            else
            {
                cmbStatus.Enabled = true;
            }
        }
        private void DrawPictureboxBox(PictureBox box, Graphics g, Color textColor, Color borderColor)
        {
            if (box != null)
            {
                Brush textBrush = new SolidBrush(textColor);
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                SizeF strSize = g.MeasureString(box.Text, box.Font);
                Rectangle rect = new Rectangle(box.ClientRectangle.X,
                                               box.ClientRectangle.Y + (int)(strSize.Height / 2),
                                               box.ClientRectangle.Width - 1,
                                               box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

                // Clear text and border
                //g.Clear(this.BackColor);

                // Draw text
                g.DrawString(box.Text, box.Font, textBrush, box.Padding.Left, 0);

                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }
        private void DrawGroupBox(GroupBox box, Graphics g, Color textColor, Color borderColor)
        {
            if (box != null)
            {
                Brush textBrush = new SolidBrush(textColor);
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                SizeF strSize = g.MeasureString(box.Text, box.Font);
                Rectangle rect = new Rectangle(box.ClientRectangle.X,
                                               box.ClientRectangle.Y + (int)(strSize.Height / 2),
                                               box.ClientRectangle.Width - 1,
                                               box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

                // Clear text and border
                g.Clear(this.BackColor);

                // Draw text
                g.DrawString(box.Text, box.Font, textBrush, box.Padding.Left, 0);

                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            GroupBox box = sender as GroupBox;
            DrawGroupBox(box, e.Graphics, Color.Red, Color.Black);
        }
        private void pbHdr_Paint(object sender, PaintEventArgs e)
        {
            PictureBox box = sender as PictureBox;
            DrawPictureboxBox(box, e.Graphics, Color.Red, Color.Black);
        }
        public void GetDatainDatatable()
        {
            DtGrid.Rows.Clear();
            DtGrid.Columns.Clear();
            var imageConverter = new ImageConverter();
            if (!singleSlave || cmbSlaves.GetItemText(cmbSlaves.SelectedItem).ToLower() == "all")
            {
                int iPulseIndex = 0;
                List<int> AList = new List<int>();
                if (lvMappingView.Columns.Count == 0)
                {
                    MessageBox.Show("Please add Data Points", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    for (int i = 0; i <= lvMappingView.Columns.Count; i++)
                    {
                        if (lvMappingView.Columns[i].Text == "Pulse")
                        {
                            iPulseIndex = i;
                            break;
                        }
                        iPulseIndex += 1;
                    }
                    foreach (ColumnHeader column in lvMappingView.Columns)
                    {
                        DtGrid.Columns.Add(column.Text);
                    }
                    foreach (ListViewItem item in lvMappingView.Items)
                    {
                        DataRow row = DtGrid.NewRow();
                        for (int i = 0; i < DtGrid.Columns.Count; i++)
                        {
                            if (item.SubItems.ToString() != "")
                            {
                                if (item.SubItems[i].Text == "yes")
                                {
                                    row[i] = "Yes";
                                }
                                else if (item.SubItems[i].Text == "transparentimg")
                                {
                                    row[i] = "No";
                                }
                                else
                                {
                                    row[i] = item.SubItems[i].Text;
                                }
                            }
                        }
                        DtGrid.Rows.Add(row);
                    }
                    DgvList.DataSource = DtGrid;
                }
            }
            else
            {
                try
                {
                    if (lvMappingView.Columns.Count == 0)
                    {
                        MessageBox.Show("Please add Data Points", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        foreach (ColumnHeader column in lvMappingView.Columns)
                        {
                            DtGrid.Columns.Add(column.Text);
                        }
                        foreach (ListViewItem item in lvMappingView.Items)
                        {
                            DataRow row = DtGrid.NewRow();
                            for (int i = 0; i < DtGrid.Columns.Count; i++)
                            {
                                if (item.SubItems.ToString() != "")
                                {
                                    if (item.SubItems[i].Text == "yes")
                                    {
                                        row[i] = "Yes";
                                    }
                                    else if (item.SubItems[i].Text == "transparentimg")
                                    {
                                        row[i] = "No";
                                    }
                                    else
                                    {
                                        row[i] = item.SubItems[i].Text;
                                    }
                                }
                            }
                            DtGrid.Rows.Add(row);
                        }
                        DgvList.DataSource = DtGrid;
                    }
                }
                catch (System.Data.DuplicateNameException)
                {

                }
            }
        }
        public DataTable GetTable()
        {
            DataTable table = new DataTable();
            table = DtGrid;
            return table;
        }
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            //Namrata: 09/12/2017
            String filename = "";
            GetDatainDatatable();
            if (DtGrid != null && DtGrid.Rows.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
                saveFileDialog.FileName = "Overview";
                saveFileDialog.Title = "Save an Excel";
                saveFileDialog.ShowDialog();
                // If the file name is not an empty string open it for saving.
                if (saveFileDialog.FileName != "")
                {
                    filename = saveFileDialog.FileName;
                }
                else
                {
                    return;
                }
                OpenProPlusConfigurator.ExcelUtlity obj = new OpenProPlusConfigurator.ExcelUtlity();
                obj.WriteDataTableToExcel(GetTable(), "Overview Details", filename, "Overview Details");
            }
        }

        private void cmbIED_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
