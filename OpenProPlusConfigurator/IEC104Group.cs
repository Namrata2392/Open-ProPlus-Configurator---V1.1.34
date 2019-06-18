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
    * \brief     <b>IEC104Group</b> is a class to store all the IEC104Slave's
    * \details   This class stores info related to all IEC104Slave's. It allows
    * user to add multiple IEC104Slave's. It also exports the XML node related to this object.
    * 
    */
    public class IEC104Group
    {
        #region Decalration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "IEC104Group";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        string NetworkActive = "";
        private List<IEC104Slave> s104List = new List<IEC104Slave>();//Expose the list to others for slave mapping thru func()...
        ucGroupIEC104 ucs104 = new ucGroupIEC104();
        private TreeNode IEC104GroupTreeNode;
        ucGroupMODBUSSlave ucGroupMODBUSSlave = new ucGroupMODBUSSlave();
        #endregion Decalration
        public IEC104Group()
        {
            string strRoutineName = "IEC104Group";
            try
            {
                ucs104.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucs104.lvIEC104SlaveItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEC104Slave_ItemCheck);
                ucs104.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucs104.btnExportINIClick += new System.EventHandler(this.btnExportINI_Click);
                ucs104.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucs104.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucs104.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucs104.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucs104.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucs104.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucs104.lvIEC104SlaveDoubleClick += new System.EventHandler(this.lvIEC104Slave_DoubleClick);
                ucs104.cmbLocalIPSelectedIndexChanged += new System.EventHandler(this.cmbLocalIP_SelectedIndexChanged);
                ucs104.CmbPortNameSelectedIndexChanged += new System.EventHandler(this.CmbPortName_SelectedIndexChanged);
                addListHeaders();
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
                    if (ucs104.CmbPortName.Text == ni.PortName)
                    {
                        ucs104.cmbLocalIP.Text = ni.IP;
                        if (ni.Enable.ToUpper() == "YES")
                        {
                            ucs104.PbOff.Visible = false;
                            ucs104.PbOn.Visible = true;
                            ucs104.PbOn.BringToFront();
                        }
                        else
                        {
                            ucs104.PbOn.Visible = false;
                            ucs104.PbOff.Visible = true;
                            ucs104.PbOff.BringToFront();
                        }
                    }
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
                NetworkActive = "";
                ucs104.cmbEnable.DataSource = Utils.dtNetworkConfig.Tables[0];//ucmbs.cmbLocalIP.SelectedIndex
                ucs104.cmbEnable.DisplayMember = "Enable";
                ucs104.cmbEnable.ValueMember = "PortNum";
                ucs104.cmbEnable.SelectedIndex = ucs104.cmbLocalIP.SelectedIndex;
                NetworkActive = ucs104.cmbEnable.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //ucs104.lblYesNo.Text = NetworkActive;
        }
        //Namrata: 22/09/2017
        private void fillEnable()
        {
            string strRoutineName = "fillEnable";
            try
            {
                ucs104.cmbEnable.DataSource = null;
                ucs104.cmbEnable.Items.Clear();
                ucs104.cmbEnable.Items.Add("YES");
                ucs104.cmbEnable.Items.Add("NO");
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
                if (s104List.Count >= Globals.MaxIEC104Slave)
                {
                    MessageBox.Show("Maximum " + Globals.MaxIEC104Slave + " IEC104 Slaves are supported.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                mode = Mode.ADD;
                editIndex = -1;
                ucs104.grpIEC104.Size = new Size(319, 517);
                Utils.resetValues(ucs104.grpIEC104);
                Utils.showNavigation(ucs104.grpIEC104, false);
                loadDefaults();
                ucs104.txtSlaveNum.Text = (Globals.SlaveNo + 1).ToString();
                ucs104.grpIEC104.Visible = true;
                ucs104.cmbLocalIP.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvIEC104Slave_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lvIEC104Slave_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucs104.lvIEC104Slave.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDelete_Click";
            try
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "*** s104List count: {0} lv count: {1}", s104List.Count, ucs104.lvIEC104Slave.Items.Count);
                if (ucs104.lvIEC104Slave.Items.Count == 0)
                {
                    MessageBox.Show("Please add atleast one slave ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ucs104.lvIEC104Slave.CheckedItems.Count == 1)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete Slave No." + ucs104.lvIEC104Slave.CheckedItems[0].Text + " ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        int iIndex = ucs104.lvIEC104Slave.CheckedItems[0].Index;
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", iIndex);
                        if (result == DialogResult.Yes)
                        {
                            s104List.RemoveAt(iIndex);
                            ucs104.lvIEC104Slave.Items[iIndex].Remove();
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
                    Utils.WriteLine(VerboseLevel.DEBUG, "*** s104List count: {0} lv count: {1}", s104List.Count, ucs104.lvIEC104Slave.Items.Count);
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
                List<KeyValuePair<string, string>> s104Data = Utils.getKeyValueAttributes(ucs104.grpIEC104);
                if (mode == Mode.ADD)
                {
                    TreeNode tmp = null;
                    s104List.Add(new IEC104Slave("IEC104", s104Data, tmp));
                }
                else if (mode == Mode.EDIT)
                {
                    s104List[editIndex].updateAttributes(s104Data);
                }
                refreshList();
                //Namrata: 09/08/2017
                if (sender != null && e != null)
                {
                    ucs104.grpIEC104.Visible = false;
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
                ucs104.grpIEC104.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                //Utils.resetValues(ucs104.grpIEC104);
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
                Console.WriteLine("*** ucs104 btnFirst_Click clicked in class!!!");
                if (ucs104.lvIEC104Slave.Items.Count <= 0) return;
                if (s104List.ElementAt(0).IsNodeComment) return;
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
                Console.WriteLine("*** ucs104 btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (s104List.ElementAt(editIndex - 1).IsNodeComment) return;
                editIndex--;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnNext_Click";
            try
            {
                //Namrata:27/7/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** ucs104 btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= ucs104.lvIEC104Slave.Items.Count) return;
                if (s104List.ElementAt(editIndex + 1).IsNodeComment) return;
                editIndex++;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*** ucs104 btnLast_Click clicked in class!!!");
            if (ucs104.lvIEC104Slave.Items.Count <= 0) return;
            if (s104List.ElementAt(s104List.Count - 1).IsNodeComment) return;
            editIndex = s104List.Count - 1;
            loadValues();
        }
        private void lvIEC104Slave_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvIEC104Slave_DoubleClick";
            try
            {
                if (ucs104.lvIEC104Slave.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucs104.lvIEC104Slave.SelectedItems[0];
                Utils.UncheckOthers(ucs104.lvIEC104Slave, lvi.Index);
                if (s104List.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucs104.grpIEC104.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucs104.grpIEC104, true);
                loadValues();
                ucs104.cmbLocalIP.Focus();
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
                ucs104.txtTCPPort.Text = "2404";
                ucs104.txtASDUaddress.Text = "1";
                ucs104.cmbASDUsize.SelectedIndex = ucs104.cmbASDUsize.FindStringExact("2");
                ucs104.cmbIOASize.SelectedIndex = ucs104.cmbIOASize.FindStringExact("3");
                ucs104.cmbCOTsize.SelectedIndex = ucs104.cmbCOTsize.FindStringExact("2");
                ucs104.txtT0.Text = "30";
                ucs104.txtT1.Text = "15";
                ucs104.txtT2.Text = "10";
                ucs104.txtT3.Text = "20";
                ucs104.txtW.Text = "8";
                ucs104.txtK.Text = "12";
                ucs104.txtCyclicInterval.Text = "600";
                ucs104.txtEventQSize.Text = "1000";
                ucs104.txtRemoteIP.Text = "10.0.2.207";
                //Namrata: 08/12/2017
                ucs104.txtSecRemote.Text = "10.0.2.208";
                ucs104.txtFirmwareVersion.Text = Globals.FIRMWARE_VERSION;
                ucs104.cmbDebug.SelectedIndex = ucs104.cmbDebug.FindStringExact("3");
                //Namrata: 20/9/2017
                ucs104.cmbEnable.SelectedIndex = ucs104.cmbEnable.FindStringExact("NO");
                foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                {
                    if (ni.Enable == "YES")
                    {
                        ucs104.CmbPortName.Text = ni.PortName;
                    }
                }
                ucs104.chkRun.Checked = true;
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
                IEC104Slave s104 = s104List.ElementAt(editIndex);
                if (s104 != null)
                {
                    ucs104.txtSlaveNum.Text = s104.SlaveNum;
                    ucs104.cmbLocalIP.SelectedIndex = ucs104.cmbLocalIP.FindStringExact(s104.LocalIP);
                    ucs104.txtTCPPort.Text = s104.TcpPort;
                    ucs104.txtRemoteIP.Text = s104.RemoteIP;
                    //Namrata: 08/12/2017
                    ucs104.txtSecRemote.Text = s104.SecRemoteIP;
                    ucs104.txtASDUaddress.Text = s104.ASDUAddress;
                    ucs104.cmbASDUsize.SelectedIndex = ucs104.cmbASDUsize.FindStringExact(s104.ASDUSize);
                    ucs104.cmbIOASize.SelectedIndex = ucs104.cmbIOASize.FindStringExact(s104.IOASize);
                    ucs104.cmbCOTsize.SelectedIndex = ucs104.cmbCOTsize.FindStringExact(s104.COTSize);
                    ucs104.txtT0.Text = s104.T0;
                    ucs104.txtT1.Text = s104.T1;
                    ucs104.txtT2.Text = s104.T2;
                    ucs104.txtT3.Text = s104.T3;
                    ucs104.txtW.Text = s104.W;
                    ucs104.txtK.Text = s104.K;
                    ucs104.txtCyclicInterval.Text = s104.CyclicInterval;
                    ucs104.txtEventQSize.Text = s104.EventQSize;
                    ucs104.txtFirmwareVersion.Text = s104.AppFirmwareVersion;
                    ucs104.cmbDebug.SelectedIndex = ucs104.cmbDebug.FindStringExact(s104.DEBUG);
                    ucs104.cmbEnable.DataSource = Utils.dtNetworkConfig.Tables[0];//ucmbs.cmbLocalIP.SelectedIndex
                    ucs104.cmbEnable.DisplayMember = "Enable";
                    ucs104.cmbEnable.ValueMember = "PortNum";
                    foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                    {
                        if (ni.IP == ucs104.cmbLocalIP.Text && ni.Enable == "YES")
                        {
                            ucs104.CmbPortName.Text = ni.PortName;
                            ucs104.CmbPortName.SelectedIndex = ucs104.cmbDebug.FindStringExact(s104.PortName);
                        }
                    }
                    if (s104.EventWithoutTime.ToLower() == "yes") ucs104.chkEventWithoutTime.Checked = true;
                    else ucs104.chkEventWithoutTime.Checked = false;
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
            //Check empty field's
            if (Utils.IsEmptyFields(ucs104.grpIEC104))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check Remote IP...
            if (!Utils.IsValidIPv4(ucs104.txtRemoteIP.Text))
            {
                MessageBox.Show("Invalid Remote IP.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check Remote IP...
            if (!Utils.IsValidIPv4(ucs104.txtSecRemote.Text))
            {
                MessageBox.Show("Invalid Remote IP.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            string strRoutineName = "fillOptions";
            try
            {
                //Fill ASDU size...
                ucs104.cmbASDUsize.Items.Clear();
                foreach (String br in IEC104Slave.getASDUsizes())
                {
                    ucs104.cmbASDUsize.Items.Add(br.ToString());
                }
                ucs104.cmbASDUsize.SelectedIndex = 0;
                //Fill IOA size...
                ucs104.cmbIOASize.Items.Clear();
                foreach (String ioa in IEC104Slave.getIOAsizes())
                {
                    ucs104.cmbIOASize.Items.Add(ioa.ToString());
                }
                ucs104.cmbIOASize.SelectedIndex = 0;
                //Fill COT size...
                ucs104.cmbCOTsize.Items.Clear();
                foreach (String db in IEC104Slave.getCOTsizes())
                {
                    ucs104.cmbCOTsize.Items.Add(db.ToString());
                }
                ucs104.cmbCOTsize.SelectedIndex = 0;

                //Fill Debug levels...
                ucs104.cmbDebug.Items.Clear();
                for (int i = 1; i <= Globals.MAX_DEBUG_LEVEL; i++)
                {
                    ucs104.cmbDebug.Items.Add(i.ToString());
                }
                ucs104.cmbDebug.SelectedIndex = 0;
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
                //ucs104.lvIEC104Slave.OwnerDraw = true;
                ucs104.lvIEC104Slave.Columns.Add("Slave No.", 60, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("Local IP Address", 80, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("TCP Port", 50, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("Primary Remote IP Address", 140, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("Secondary Remote IP Address", 140, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("ASDU Address", 80, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("ASDU Size", 60, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("IOA Size", 60, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("COT Size", 60, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("T0", 35, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("T1", 35, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("T2", 35, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("T3", 35, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("W", 35, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("K", 35, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("Cyclic Interval (sec)", 80, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("Event Queue Size", 70, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("Firmware Version", 90, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("Debug Level", 70, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("Run ", 40, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.Columns.Add("Event Without Time ", 150, HorizontalAlignment.Left);
                ucs104.lvIEC104Slave.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
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
                ucs104.lvIEC104Slave.Items.Clear();

                foreach (IEC104Slave sl in s104List)
                {
                    string[] row = new string[22];//string[] row = new string[19];
                    if (sl.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = sl.SlaveNum;
                        row[1] = sl.LocalIP;
                        row[2] = sl.TcpPort;
                        row[3] = sl.RemoteIP;
                        row[4] = sl.SecRemoteIP;
                        row[5] = sl.ASDUAddress;
                        row[6] = sl.ASDUSize;
                        row[7] = sl.IOASize;
                        row[8] = sl.COTSize;
                        row[9] = sl.T0;
                        row[10] = sl.T1;
                        row[11] = sl.T2;
                        row[12] = sl.T3;
                        row[13] = sl.W;
                        row[14] = sl.K;
                        row[15] = sl.CyclicInterval;
                        row[16] = sl.EventQSize;
                        row[17] = sl.AppFirmwareVersion;
                        row[18] = sl.DEBUG;
                        row[19] = sl.Run;
                        row[20] = sl.EventWithoutTime;
                        row[21] = sl.PortName;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucs104.lvIEC104Slave.Items.Add(lvItem);
                }
                ListToDataTable(s104List);
            }

            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Control getView(List<string> kpArr)
        {
            if (kpArr.Count == 1 && kpArr.ElementAt(0).Contains("IEC104Group_"))
            {
                ucs104.cmbLocalIP.Items.Clear();
                ucs104.CmbPortName.Items.Clear();
                foreach (NetworkInterface ni in Utils.getOpenProPlusHandle().getNetworkConfiguration().getNetworkInterfaces())
                {
                    if (ni.Enable == "YES")
                    {
                        //Namrata:20/1/2018
                        ucs104.CmbPortName.Items.Add(ni.PortName);
                        if (ni.VirtualIP != "0.0.0.0")
                        {
                            ucs104.cmbLocalIP.Items.Add(ni.VirtualIP);
                        }
                        else
                        {
                            ucs104.cmbLocalIP.Items.Add(ni.IP);
                        }
                    }
                }
                if (ucs104.cmbLocalIP.Items.Count > 0) ucs104.cmbLocalIP.SelectedIndex = 0;
                fillEnable();
                return ucs104;
            }

            kpArr.RemoveAt(0);
            if (kpArr.ElementAt(0).Contains("IEC104_"))
            {
                int idx = -1;
                string[] elems = kpArr.ElementAt(0).Split('_');
                Utils.WriteLine(VerboseLevel.DEBUG, "$$$$ elem0: {0} elem1: {1}", elems[0], elems[elems.Length - 1]);
                idx = Int32.Parse(elems[elems.Length - 1]);
                if (s104List.Count <= 0) return null;
                return s104List[idx].getView(kpArr);
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
            foreach (IEC104Slave sn in s104List)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(sn.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public void parseIECGNode(XmlNode iecgNode, TreeNode tn)
        {
            string strRoutineName = "parseIECGNode";
            try
            {
                //First set root node name...
                rnName = iecgNode.Name;
                tn.Nodes.Clear();
                IEC104GroupTreeNode = tn;//Save local copy so we can use it to manually add nodes in above constructor...
                foreach (XmlNode node in iecgNode)
                {
                    TreeNode tmp = null;

                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    s104List.Add(new IEC104Slave(node, tmp));
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
            foreach (IEC104Slave s104Node in s104List)
            {
                if (s104Node.IsNodeComment) continue;
                ctr++;
            }
            return ctr;
        }
        public List<IEC104Slave> getIEC104Slaves()
        {
            return s104List;
        }
        public List<IEC104Slave> getIEC104SlavesByFilter(string slaveID)
        {
            List<IEC104Slave> iec104List = new List<IEC104Slave>();
            if (slaveID.ToLower() == "all") return s104List;
            else
                foreach (IEC104Slave iec in s104List)
                {
                    if (iec.getSlaveID == slaveID)
                    {
                        iec104List.Add(iec);
                        break;
                    }
                }

            return iec104List;
        }
        public DataTable ListToDataTable<IEC104Slave>(IList<IEC104Slave> varlist)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (typeof(IEC104Slave).IsValueType || typeof(IEC104Slave).Equals(typeof(string)))
            {
                DataColumn dc = new DataColumn("Values");
                dt.Columns.Add(dc);
                foreach (IEC104Slave item in varlist)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item;
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                PropertyInfo[] propT = typeof(IEC104Slave).GetProperties(); //find all the public properties of this Type using reflection;
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
            Utils.dsIEC104Slave = ds;
            Utils.dtIEC104Slave = dt;
            return dt;
        }
    }
}
