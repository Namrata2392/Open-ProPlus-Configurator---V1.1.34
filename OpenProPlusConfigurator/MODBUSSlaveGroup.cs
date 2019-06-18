using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Reflection;

namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>MODBUSSlaveGroup</b> is a class to store all the MODBUSSlave's
    * \details   This class stores info related to all MODBUSSlave's. It allows
    * user to add multiple MODBUSSlave's. It also exports the XML node related to this object.
    * 
    */
    public class MODBUSSlaveGroup
    {
        #region Declaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        enum protocolType
        {
            RTU,
            ASCII,
            TCP
        };
        ucGroupIEC104 ucGroupIEC104 = new ucGroupIEC104();
        string NetworkActive = "";
        string SerialActive = "";
        private string rnName = "MODBUSSlaveGroup";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        public List<MODBUSSlave> mbsList = new List<MODBUSSlave>();//Expose the list to others for slave mapping...
        ucGroupMODBUSSlave ucmbs = new ucGroupMODBUSSlave();
        private TreeNode MODBUSSlaveGroupTreeNode;
        #endregion Declaration
        public MODBUSSlaveGroup()
        {
            string strRoutineName = "MODBUSSlaveGroup";
            try
            {
                ucmbs.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucmbs.lvMODBUSSlaveItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvMODBUSSlave_ItemCheck);
                ucmbs.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucmbs.btnExportINIClick += new System.EventHandler(this.btnExportINI_Click);
                ucmbs.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucmbs.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucmbs.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucmbs.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucmbs.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucmbs.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucmbs.lvMODBUSSlaveDoubleClick += new System.EventHandler(this.lvMODBUSSlave_DoubleClick);
                ucmbs.cmbProtocolTypeSelectedIndexChanged += new System.EventHandler(this.cmbProtocolType_SelectedIndexChanged);
                ////Namrata:22 / 09 / 2017
                ucmbs.cmbLocalIPSelectedIndexChanged += new System.EventHandler(this.cmbLocalIP_SelectedIndexChanged);
                ucmbs.cmbPortNoSelectedIndexChanged += new System.EventHandler(this.cmbPortNo_SelectedIndexChanged);
                ucmbs.CmbPortNameSelectedIndexChanged += new System.EventHandler(this.CmbPortName_SelectedIndexChanged);
                addListHeaders();
                //ucmbs.label2.Text = "";
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CmbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "CmbPortName_SelectedIndexChanged";
            try
            {
                foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                {
                    if (ucmbs.CmbPortName.Text == ni.PortName)
                    {
                        ucmbs.cmbLocalIP.Text = ni.IP;
                        if (ni.Enable.ToUpper() == "YES")
                        {
                            ucmbs.PbOff.Visible = false;
                            ucmbs.PbOn.Visible = true;
                            ucmbs.PbOn.BringToFront();
                        }
                        else
                        {
                            ucmbs.PbOn.Visible = false;
                            ucmbs.PbOff.Visible = true;
                            ucmbs.PbOff.BringToFront();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvMODBUSSlave_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lvMODBUSSlave_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucmbs.lvMODBUSSlave.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
                    {
                        item.Checked = false;
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbLocalIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "cmbLocalIP_SelectedIndexChanged";
            try
            {
                //Namrata: 21/09/2017
                if (ucmbs.cmbProtocolType.GetItemText(ucmbs.cmbProtocolType.SelectedItem).ToLower() == "tcp")
                {
                    ucmbs.cmbPortNo.DataSource = Utils.dtNetworkConfig.Tables[0];
                    ucmbs.cmbPortNo.DisplayMember = "PortNum";
                    ucmbs.cmbPortNo.ValueMember = "IP";
                    ucmbs.cmbPortNo.SelectedIndex = ucmbs.cmbLocalIP.SelectedIndex;
                }
                //Namrata: 21/09/2017
                else
                {
                    ucmbs.cmbPortNo.DataSource = Utils.dtNetworkConfig.Tables[0];
                    ucmbs.cmbPortNo.DisplayMember = "PortNum";
                    ucmbs.cmbPortNo.ValueMember = "IP";
                    ucmbs.cmbPortNo.SelectedIndex = ucmbs.cmbLocalIP.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbPortNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "cmbPortNo_SelectedIndexChanged";
            try
            {
                //Namrata: 22/09/2017
                if (ucmbs.cmbProtocolType.GetItemText(ucmbs.cmbProtocolType.SelectedItem).ToLower() == "tcp")
                {
                    NetworkActive = "";
                    ucmbs.cmbEnable.DataSource = Utils.dtNetworkConfig.Tables[0]; //ucmbs.cmbLocalIP.SelectedIndex
                    ucmbs.cmbEnable.DisplayMember = "Enable";
                    ucmbs.cmbEnable.ValueMember = "PortNum";
                    ucmbs.cmbEnable.SelectedIndex = ucmbs.cmbPortNo.SelectedIndex;
                    NetworkActive = ucmbs.cmbEnable.Text;
                    //ucmbs.label2.Text = NetworkActive;
                    //Utils.MODBUSSlaveEnable = ucmbs.label2.Text;
                }
                else if (ucmbs.cmbProtocolType.GetItemText(ucmbs.cmbProtocolType.SelectedItem).ToLower() == "rtu")
                {
                    SerialActive = "";
                    ucmbs.cmbEnable.DataSource = Utils.dtSerialConfig.Tables[0];//ucmbs.cmbLocalIP.SelectedIndex
                    ucmbs.cmbEnable.DisplayMember = "Enable";
                    ucmbs.cmbEnable.ValueMember = "PortNum";
                    ucmbs.cmbEnable.SelectedIndex = ucmbs.cmbPortNo.SelectedIndex;
                    SerialActive = ucmbs.cmbEnable.Text;
                    //ucmbs.label2.Text = SerialActive;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnAdd_Click";
            try
            {
                if (mbsList.Count >= Globals.MaxMODBUSSlave)
                {
                    MessageBox.Show("Maximum " + Globals.MaxMODBUSSlave + " MODBUS Slaves are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mode = Mode.ADD;
                editIndex = -1;
                Utils.resetValues(ucmbs.grpMODBUSSlave);
                Utils.showNavigation(ucmbs.grpMODBUSSlave, false);
                loadDefaults();
                ucmbs.txtSlaveNum.Text = (Globals.SlaveNo + 1).ToString();
                ucmbs.grpMODBUSSlave.Visible = true;
                ucmbs.cmbLocalIP.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDelete_Click";
            try
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "*** mbsList count: {0} lv count: {1}", mbsList.Count, ucmbs.lvMODBUSSlave.Items.Count);
                if (ucmbs.lvMODBUSSlave.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one slave ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucmbs.lvMODBUSSlave.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Slave No." + ucmbs.lvMODBUSSlave.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucmbs.lvMODBUSSlave.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            mbsList.RemoveAt(iIndex);
                            ucmbs.lvMODBUSSlave.Items[iIndex].Remove();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one Slave ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    Utils.WriteLine(VerboseLevel.DEBUG, "*** mbsList count: {0} lv count: {1}", mbsList.Count, ucmbs.lvMODBUSSlave.Items.Count);
                    refreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExportINI_Click(object sender, EventArgs e)
        {
            //FIXME: Check if the 'xmlFile' exists, meaning the filename exist...
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDone_Click";
            try
            {
                if (!Validate()) return;
                List<KeyValuePair<string, string>> mbsData = Utils.getKeyValueAttributes(ucmbs.grpMODBUSSlave);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    mbsList.Add(new MODBUSSlave("MODBUSSlave", mbsData, tmp));
                }
                else if (mode == Mode.EDIT)
                {
                    mbsList[editIndex].updateAttributes(mbsData);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    ucmbs.grpMODBUSSlave.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnCancel_Click";
            try
            {
                Utils.IntIEC104Modbus = Utils.IntIEC104Modbus - 1;
                ucmbs.grpMODBUSSlave.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                //Utils.resetValues(ucmbs.grpMODBUSSlave);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnFirst_Click";
            try
            {
                Console.WriteLine("*** ucmbs btnFirst_Click clicked in class!!!");
                if (ucmbs.lvMODBUSSlave.Items.Count <= 0) return;
                if (mbsList.ElementAt(0).IsNodeComment) return;
                editIndex = 0;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnPrev_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** ucmbs btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (mbsList.ElementAt(editIndex - 1).IsNodeComment) return;
                editIndex--;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnNext_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** ucmbs btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= ucmbs.lvMODBUSSlave.Items.Count) return;
                if (mbsList.ElementAt(editIndex + 1).IsNodeComment) return;
                editIndex++;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnLast_Click";
            try
            {
                Console.WriteLine("*** ucmbs btnLast_Click clicked in class!!!");
                if (ucmbs.lvMODBUSSlave.Items.Count <= 0) return;
                if (mbsList.ElementAt(mbsList.Count - 1).IsNodeComment) return;
                editIndex = mbsList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvMODBUSSlave_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvMODBUSSlave_DoubleClick";
            try
            {
                if (ucmbs.lvMODBUSSlave.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucmbs.lvMODBUSSlave.SelectedItems[0];
                Utils.UncheckOthers(ucmbs.lvMODBUSSlave, lvi.Index);
                if (mbsList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucmbs.grpMODBUSSlave.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucmbs.grpMODBUSSlave, true);
                loadValues();
                ucmbs.cmbProtocolType.Focus();
                //namrata:10/5/2017
                ucmbs.cmbLocalIP.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbProtocolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "cmbProtocolType_SelectedIndexChanged";
            try
            {
                //fillPortNos();
                fillEnable();
                if (ucmbs.cmbProtocolType.GetItemText(ucmbs.cmbProtocolType.SelectedItem).ToLower() == "tcp")
                {
                    //Namrata: 19/09/2017
                    ucmbs.lblPN.Text = "Port No.";//ucmbs.lblPN.Text = "Network No.";
                    ucmbs.txtTCPPort.Enabled = true;
                    ucmbs.txtRemoteIP.Enabled = true;
                    ucmbs.cmbPortNo.Enabled = false;
                    //Namrata: 21/09/2017
                    ucmbs.cmbLocalIP.Enabled = true;
                    ucmbs.CmbPortName.Enabled = true;
                    ucmbs.txtSecRemoteIP.Enabled = true;
                }
                else
                {
                    ucmbs.lblPN.Text = "UART No.";
                    ucmbs.txtTCPPort.Enabled = false;
                    ucmbs.txtRemoteIP.Enabled = false;
                    //Namrata: 21/09/2017
                    ucmbs.cmbLocalIP.Enabled = false;
                    ucmbs.cmbPortNo.Enabled = true;
                    ucmbs.CmbPortName.Enabled = false;
                    ucmbs.txtSecRemoteIP.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadDefaults()
        {
            string strRoutineName = "loadDefaults";
            try
            {
                ucmbs.cmbProtocolType.SelectedIndex = ucmbs.cmbProtocolType.FindStringExact("TCP");
                if (ucmbs.cmbPortNo.Items.Count > 0) ucmbs.cmbPortNo.SelectedIndex = 0;
                ucmbs.txtTCPPort.Text = "502";
                ucmbs.txtEventQSize.Text = "1001";
                ucmbs.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION;
                ucmbs.cmbDebug.SelectedIndex = ucmbs.cmbDebug.FindStringExact("3");
                ucmbs.chkRun.Checked = true;
                ucmbs.txtRemoteIP.Text = "192.168.1.1";
                //Namrata:8/12/2017
                ucmbs.txtSecRemoteIP.Text = "192.168.1.10";
                ucmbs.txtUnitID.Text = "1";
                //Namrata: 20/9/2017
                ucmbs.cmbEnable.SelectedIndex = ucmbs.cmbEnable.FindStringExact("NO");
                //ucmbs.label2.Text = ucmbs.cmbEnable.Text;
                if (ucmbs.cmbLocalIP.Items.Count > 0) ucmbs.cmbLocalIP.SelectedIndex = 0;
                foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                {
                    if (ni.Enable == "YES")
                    {
                        ucmbs.CmbPortName.Text = ni.PortName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        int a = 0;
        private void loadValues()
        {
            string strRoutineName = "loadValues";
            try
            {
                MODBUSSlave mbs = mbsList.ElementAt(editIndex);
                if (mbs != null)
                {
                    ucmbs.txtSlaveNum.Text = mbs.SlaveNum;
                    ucmbs.cmbProtocolType.SelectedIndex = ucmbs.cmbProtocolType.FindStringExact(mbs.ProtocolType);
                    ucmbs.cmbLocalIP.SelectedIndex = ucmbs.cmbLocalIP.FindStringExact(mbs.LocalIP);
                    ucmbs.cmbPortNo.SelectedIndex = ucmbs.cmbPortNo.FindStringExact(mbs.PortNum);
                    //ucmbs.cmbPortNoSelectedIndexChanged += new System.EventHandler(this.cmbPortNo_SelectedIndexChanged);
                    a = Convert.ToInt32((mbs.PortNum));
                    ucmbs.cmbEnable.DataSource = Utils.dtNetworkConfig.Tables[0];//ucmbs.cmbLocalIP.SelectedIndex
                    ucmbs.cmbEnable.DisplayMember = "Enable";
                    ucmbs.cmbEnable.ValueMember = "PortNum";
                    ucmbs.txtTCPPort.Text = mbs.TcpPort;
                    ucmbs.txtRemoteIP.Text = mbs.RemoteIP;
                    ucmbs.txtSecRemoteIP.Text = mbs.SecRemoteIP;
                    ucmbs.txtUnitID.Text = mbs.UnitID;
                    ucmbs.txtEventQSize.Text = mbs.EventQSize;
                    ucmbs.txtFirmwareVersion.Text = mbs.AppFirmwareVersion;
                    ucmbs.cmbDebug.SelectedIndex = ucmbs.cmbDebug.FindStringExact(mbs.DEBUG);
                    if (mbs.Run.ToLower() == "yes") ucmbs.chkRun.Checked = true;
                    else ucmbs.chkRun.Checked = false;
                    foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                    {
                        if (ni.IP == ucmbs.cmbLocalIP.Text && ni.Enable == "YES")
                        {
                            ucmbs.CmbPortName.Text = ni.PortName;
                            ucmbs.CmbPortName.SelectedIndex = ucmbs.cmbDebug.FindStringExact(mbs.PortName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Validate()
        {
            bool status = true;
            if (ucmbs.cmbProtocolType.GetItemText(ucmbs.cmbProtocolType.SelectedItem).ToLower() != "tcp")
            {
                if (String.IsNullOrWhiteSpace(ucmbs.txtRemoteIP.Text) || !Utils.IsValidIPv4(ucmbs.txtRemoteIP.Text)) ucmbs.txtRemoteIP.Text = "0.0.0.0";
                if (String.IsNullOrWhiteSpace(ucmbs.txtTCPPort.Text)) ucmbs.txtTCPPort.Text = "502";
            }
            //Check empty field's
            if (Utils.IsEmptyFields(ucmbs.grpMODBUSSlave))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check Remote IP...
            if (!Utils.IsValidIPv4(ucmbs.txtRemoteIP.Text))
            {
                MessageBox.Show("Invalid Remote IP.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check Remote IP...
            if (!Utils.IsValidIPv4(ucmbs.txtSecRemoteIP.Text))
            {
                MessageBox.Show("Invalid Secondary Remote IP.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check LocalIP ..
            //Namrata:10/5/2017
            if (!Utils.IsValidIPv4(ucmbs.cmbLocalIP.Text))
            {
                MessageBox.Show("Invalid Local IP", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check TCP Port...
            if (!Utils.IsValidTCPPort(Int32.Parse(ucmbs.txtTCPPort.Text)))
            {
                MessageBox.Show("Invalid TCP Port.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            string strRoutineName = "fillOptions";
            try
            {
                //Fill Protocol Types...
                ucmbs.cmbProtocolType.Items.Clear();
                foreach (protocolType ptv in Enum.GetValues(typeof(protocolType)))
                {
                    ucmbs.cmbProtocolType.Items.Add(ptv.ToString());
                }
                if (ucmbs.cmbProtocolType.Items.Count > 0) ucmbs.cmbProtocolType.SelectedIndex = 0;

                //Fill Debug levels...
                ucmbs.cmbDebug.Items.Clear();
                for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
                {
                    ucmbs.cmbDebug.Items.Add(i.ToString());
                }
                ucmbs.cmbDebug.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Namrata: 22/09/2017
        private void fillEnable()
        {
            string strRoutineName = "fillEnable";
            try
            {
                ucmbs.cmbEnable.DataSource = null;
                ucmbs.cmbEnable.Items.Clear();
                ucmbs.cmbEnable.Items.Add("YES");
                ucmbs.cmbEnable.Items.Add("NO");
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fillPortNos()
        {
            string strRoutineName = "fillPortNos";
            try
            {
                //Namrata: 16/10/2017
                ucmbs.cmbPortNo.DataSource = null;
                ucmbs.cmbPortNo.Items.Clear();
                if (ucmbs.cmbProtocolType.GetItemText(ucmbs.cmbProtocolType.SelectedItem).ToLower() == "tcp")
                {
                    foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                    {
                        ucmbs.cmbPortNo.Items.Add(ni.PortNum);
                    }
                }
                else//assume serial interfaces...
                {
                    foreach (SerialInterface si in Utils.getOpenProPlusHandle().getSerialPortConfiguration().getSerialInterfaces())
                    {
                        ucmbs.cmbPortNo.Items.Add(si.PortNum);
                    }
                }
                if (ucmbs.cmbPortNo.Items.Count > 0) ucmbs.cmbPortNo.SelectedIndex = 0;
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
                ucmbs.lvMODBUSSlave.Columns.Add("Slave No.", 60, HorizontalAlignment.Left);
                ucmbs.lvMODBUSSlave.Columns.Add("Protocol Type", 90, HorizontalAlignment.Left);
                //Namarta: 10/05/2017
                ucmbs.lvMODBUSSlave.Columns.Add("Port No.", 70, HorizontalAlignment.Left);
                ucmbs.lvMODBUSSlave.Columns.Add("Local IP Address", 120, HorizontalAlignment.Left);
                ucmbs.lvMODBUSSlave.Columns.Add("TCP Port", 90, HorizontalAlignment.Left);
                ucmbs.lvMODBUSSlave.Columns.Add("Primary Remote IP Address", 120, HorizontalAlignment.Left);
                ucmbs.lvMODBUSSlave.Columns.Add("Secondary Remote IP Address", 150, HorizontalAlignment.Left);
                ucmbs.lvMODBUSSlave.Columns.Add("Unit ID", 50, HorizontalAlignment.Left);
                ucmbs.lvMODBUSSlave.Columns.Add("Event Q Size", 80, HorizontalAlignment.Left);
                ucmbs.lvMODBUSSlave.Columns.Add("Firmware Version", 100, HorizontalAlignment.Left);
                ucmbs.lvMODBUSSlave.Columns.Add("Debug Level", 80, HorizontalAlignment.Left);
                ucmbs.lvMODBUSSlave.Columns.Add("Run", 70, HorizontalAlignment.Left);
                ucmbs.lvMODBUSSlave.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
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
                ucmbs.lvMODBUSSlave.Items.Clear();
                foreach (MODBUSSlave sl in mbsList)
                {
                    string[] row = new string[21]; //string[] row = new string[19];
                    int rNo = 0;
                    if (sl.IsNodeComment)
                    {
                        row[rNo] = "Comment...";
                    }
                    else
                    {
                        row[rNo++] = sl.SlaveNum;
                        row[rNo++] = sl.ProtocolType;
                        row[rNo++] = sl.PortNum;
                        if (sl.ProtocolType.ToLower() == "tcp")
                        {
                            //Namrata: 21/09/2017
                            row[rNo++] = sl.LocalIP;
                            row[rNo++] = sl.TcpPort;
                            row[rNo++] = sl.RemoteIP;
                            row[rNo++] = sl.SecRemoteIP;
                        }
                        else
                        {
                            ////Namrata: 21/09/2017
                            row[rNo++] = "NA";
                            row[rNo++] = "NA";
                            row[rNo++] = "NA";
                        }
                        row[rNo++] = sl.UnitID;
                        row[rNo++] = sl.EventQSize;
                        row[rNo++] = sl.AppFirmwareVersion;
                        row[rNo++] = sl.DEBUG;
                        row[rNo++] = sl.Run;
                        row[rNo++] = sl.PortName;
                    }

                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucmbs.lvMODBUSSlave.Items.Add(lvItem);
                    ListToDataTable(mbsList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable ListToDataTable<MODBUSSlave>(IList<MODBUSSlave> varlist)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (typeof(MODBUSSlave).IsValueType || typeof(MODBUSSlave).Equals(typeof(string)))
            {
                DataColumn dc = new DataColumn("Values");
                dt.Columns.Add(dc);
                foreach (MODBUSSlave item in varlist)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item;
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                PropertyInfo[] propT = typeof(MODBUSSlave).GetProperties(); //find all the public properties of this Type using reflection;
                foreach (PropertyInfo pi in propT)
                {
                    DataColumn dc = new DataColumn(pi.Name, pi.PropertyType);
                    dt.Columns.Add(dc);
                }
                for (int item = 0; item < varlist.Count(); item++)
                {
                    DataRow dr = dt.NewRow();
                    for (int property = 0; property < propT.Length; property++)
                    {
                        dr[property] = propT[property].GetValue(varlist[item], null);
                    }
                    dt.Rows.Add(dr);
                }
            }
            dt.AcceptChanges();
            ds.Tables.Add(dt);
            Utils.DsMODBUSSlave = ds;
            Utils.DtMODBUSSlave = dt;
            return dt;
        }
        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("MODBUSSlaveGroup_"))
            {
                ucmbs.cmbLocalIP.Items.Clear();
                ucmbs.CmbPortName.Items.Clear();
                foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                {
                    if (ni.Enable == "YES")
                    {
                        //Namrata:20/1/2018
                        ucmbs.CmbPortName.Items.Add(ni.PortName);
                        if (ni.VirtualIP != "0.0.0.0")
                        {
                            ucmbs.cmbLocalIP.Items.Add(ni.VirtualIP);
                        }
                        else
                        {
                            ucmbs.cmbLocalIP.Items.Add(ni.IP);
                        }
                    }
                    //ucmbs.cmbLocalIP.Items.Add(ni.IP);
                    //ucmbs.CmbPortName.Items.Add(ni.PortName);
                }
                if (ucmbs.cmbLocalIP.Items.Count > 0) ucmbs.cmbLocalIP.SelectedIndex = 0;
                //Fill Port No.
                fillPortNos();
                fillEnable();
                return ucmbs;
            }
            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("MODBUSSlave_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Utils.WriteLine(VerboseLevel.DEBUG, "$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (mbsList.Count <= 0) return null;
                return mbsList[idx].getView(kpArr);
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

            foreach (MODBUSSlave sn in mbsList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(sn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }

            return rootNode;
        }
        public void parseMBSGNode(XmlNode mbsgNode, TreeNode tn)
        {
            string strRoutineName = "parseMBSGNode";
            try
            {
                rnName = mbsgNode.Name;
                tn.Nodes.Clear();
                MODBUSSlaveGroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
                foreach (XmlNode node in mbsgNode)
                {
                    TreeNode tmp = null;
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                                                                       //No need to create subnodes: tmp = tn.Nodes.Add("MODBUSSlave_" + Utils.GenerateShortUniqueKey(), "MODBUSSlave", "MODBUSSlave", "MODBUSSlave");
                    mbsList.Add(new MODBUSSlave(node, tmp));
                }
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int getCount()
        {
            int ctr = 0;
            foreach (MODBUSSlave s104Node in mbsList)
            {
                if (s104Node.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }

        public List<MODBUSSlave> getMODBUSSlaves()
        {
            return mbsList;
        }

        public List<MODBUSSlave> getMODBUSSlavesByFilter(string slaveID)
        {
            List<MODBUSSlave> mbsList1 = new List<MODBUSSlave>();
            if (slaveID.ToLower() == "all") return mbsList;
            else
                foreach (MODBUSSlave mbs in mbsList)
                {
                    if (mbs.getSlaveID == slaveID)
                    {
                        mbsList1.Add(mbs);
                        break;
                    }
                }

            return mbsList1;
        }
    }
}