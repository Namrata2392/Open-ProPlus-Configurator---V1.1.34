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
using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;
namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>NetWorkConfiguration</b> is a class to store all the NetworkInterface's
    * \details   This class stores info related to all NetworkInterface's. It only allows
    * user to modify the parameters. It also exports the XML node related to this object.
    * 
    */
    public class NetWorkConfiguration
    {
        #region Declaration
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "NetWorkConfiguration";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;
        List<NetworkInterface> niList = new List<NetworkInterface>();
        ucNetworkConfiguration ucnc = new ucNetworkConfiguration();
        #endregion Declaration
        public NetWorkConfiguration()
        {
            string strRoutineName = "NetWorkConfiguration";
            try
            {
                ucnc.Load += new System.EventHandler(this.ucnc_Load);
                ucnc.txtIPValidating += new System.ComponentModel.CancelEventHandler(this.txtIP_Validating);
                ucnc.lvNPortsItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvNPorts_ItemCheck);
                ucnc.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucnc.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucnc.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucnc.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucnc.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucnc.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucnc.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucnc.lvNPortsDoubleClick += new System.EventHandler(this.lvNPorts_DoubleClick);
                //Namrata: 28/10/2017
                ucnc.btnEditClick += new System.EventHandler(this.btnEdit_Click);
                //ucnc.lvNPortsDrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvNPorts_DrawColumnHeader);
                addListHeaders();
                fillOptions();
                ucnc.txtVirtualIP.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtIP_Validating(object sender, CancelEventArgs e)
        {

        }
        private void lvNPorts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lvNPorts_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucnc.lvNPorts.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
        //Namrata: 28/10/2017
        NetworkInterface mapAI = null;
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnEdit_Click";
            try
            {
                ucnc.lvNPorts.SelectedItems.Clear();
                if (ucnc.lvNPorts.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Select Single Element To Update Network Configuration !!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    for (int i = 0; i < ucnc.lvNPorts.Items.Count; i++)
                    {
                        if (ucnc.lvNPorts.Items[i].Checked)
                        {
                            mapAI = niList.ElementAt(i);
                            ListViewItem lvi = ucnc.lvNPorts.CheckedItems[0];
                            Utils.UncheckOthers(ucnc.lvNPorts, lvi.Index);
                            ucnc.grpNI.Visible = true;
                            mode = Mode.EDIT;
                            editIndex = i;
                            Utils.showNavigation(ucnc.grpNI, true);
                            loadValues();
                            ucnc.cmbAddressType.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public List<NetworkInterface> getNetworkInterfaces()
        {
            return niList;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDelete_Click";
            try
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "*** niList count: {0} lv count: {1}", niList.Count, ucnc.lvNPorts.Items.Count);
                DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    return;
                }
                for (int i = ucnc.lvNPorts.Items.Count - 1; i >= 0; i--)
                {
                    if (ucnc.lvNPorts.Items[i].Checked)
                    {
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", i);
                        niList.RemoveAt(i);
                        ucnc.lvNPorts.Items[i].Remove();
                    }
                }
                Utils.WriteLine(VerboseLevel.DEBUG, "*** niList count: {0} lv count: {1}", niList.Count, ucnc.lvNPorts.Items.Count);
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDone_Click";
            try
            {
                if (!Validate()) return;
                List<KeyValuePair<string, string>> niData = Utils.getKeyValueAttributes(ucnc.grpNI);

                if (mode == Mode.ADD)
                {
                    niList.Add(new NetworkInterface("Lan", niData));
                }
                else if (mode == Mode.EDIT)
                {
                    niList[editIndex].updateAttributes(niData);
                    Utils.NetworkIPLIst.AddRange(niList);

                }
                for (int i = 0; i < niList.Count; i++)
                {
                    #region bond0
                    if (niList[i].PortName == "bond0")
                    {
                        if (niList[i].Enable == "YES")
                        {
                            if (Utils.DummyDI.Select(x => x.Index).ToList().Contains(niList[0].PortNum))
                            {

                            }
                            else
                            {
                                Utils.CreateDI4NetworkInterface(Convert.ToInt32(niList[0].PortNum));
                            }
                            if (Utils.DummyDI.Select(x => x.Index).ToList().Contains(niList[1].PortNum))
                            {

                            }
                            else
                            {
                                Utils.CreateDI4NetworkInterface(Convert.ToInt32(niList[1].PortNum));
                            }
                            if (Utils.DummyDI.Select(x => x.Index).ToList().Contains(niList[2].PortNum))
                            {

                            }
                            else
                            {
                                Utils.CreateDI4NetworkInterface(Convert.ToInt32(niList[2].PortNum));
                            }
                        }
                        else
                        {
                            if (niList[i].Enable == "NO")
                            {
                                if (Utils.DummyDI.Select(x => x.Index).ToList().Contains(niList[i].PortNum))
                                {
                                    if (niList[3].Enable == "YES")
                                    {
                                        Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[2].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[2].PortNum);
                                    }
                                    else if (niList[2].Enable == "NO")
                                    {
                                        Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[0].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[0].PortNum);
                                        Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[1].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[1].PortNum);
                                        Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[2].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[2].PortNum);
                                    }
                                }
                            }
                        }
                    }

                    #endregion bond0
                    #region br0
                    if (niList[i].PortName == "br0")
                    {
                        if (niList[i].Enable == "YES")
                        {
                            if (Utils.DummyDI.Select(x => x.Index).ToList().Contains(niList[0].PortNum))
                            {

                            }
                            else
                            {
                                Utils.CreateDI4NetworkInterface(Convert.ToInt32(niList[0].PortNum));
                            }
                            if (Utils.DummyDI.Select(x => x.Index).ToList().Contains(niList[1].PortNum))
                            {

                            }
                            else
                            {
                                Utils.CreateDI4NetworkInterface(Convert.ToInt32(niList[1].PortNum)); ;
                            }
                            if (Utils.DummyDI.Select(x => x.Index).ToList().Contains(niList[3].PortNum))
                            {

                            }
                            else
                            {
                                Utils.CreateDI4NetworkInterface(Convert.ToInt32(niList[3].PortNum));
                            }
                        }
                        else
                        {
                            if (niList[i].Enable == "NO")
                            {
                                if (Utils.DummyDI.Select(x => x.Index).ToList().Contains(niList[i].PortNum))
                                {
                                    if (niList[2].Enable == "YES")
                                    {
                                        Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[3].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[3].PortNum);
                                    }
                                    else if (niList[2].Enable == "NO")
                                    {
                                        Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[0].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[0].PortNum);
                                        Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[1].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[1].PortNum);
                                        Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[3].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[3].PortNum);
                                    }
                                }
                            }
                        }
                    }
                    #endregion br0
                    #region eth0
                    if ((niList[i].PortName == "eth0"))
                    {
                        if (niList[i].Enable == "NO")
                        {
                            if ((niList[2].Enable == "YES") || (niList[3].Enable == "YES"))
                            {
                                if (Utils.DummyDI.Select(x => x.Index).ToList().Contains(niList[i].PortNum))
                                {

                                }
                                else
                                {
                                    Utils.CreateDI4NetworkInterface(Convert.ToInt32(niList[0].PortNum));
                                }
                            }
                            else if ((niList[2].Enable == "NO") || (niList[3].Enable == "NO"))
                            {
                                Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[0].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[0].PortNum);
                            }
                        }
                        if (niList[i].Enable == "YES")
                        {
                            if (Utils.DummyDI.Select(x => x.Index).ToList().Contains(niList[i].PortNum))
                            {

                            }
                            else
                            {
                                Utils.CreateDI4NetworkInterface(Convert.ToInt32(niList[0].PortNum));
                            }
                        }
                    }
                    #endregion eth0
                    #region eth1
                    if ((niList[i].PortName == "eth1"))
                    {
                        if (niList[i].Enable == "NO")
                        {
                            if ((niList[2].Enable == "YES") || (niList[3].Enable == "YES"))
                            {
                                if (Utils.DummyDI.Select(x => x.Index).ToList().Contains(niList[i].PortNum))
                                {

                                }
                                else
                                {
                                    Utils.CreateDI4NetworkInterface(Convert.ToInt32(niList[1].PortNum));
                                }
                            }
                            else if ((niList[2].Enable == "NO") || (niList[3].Enable == "NO"))
                            {
                                Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[1].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[1].PortNum);
                            }
                        }
                        if (niList[i].Enable == "YES")
                        {
                            if (Utils.DummyDI.Select(x => x.Index).ToList().Contains(niList[i].PortNum))
                            {
                                //Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[0].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[0].PortNum);
                            }
                            else
                            {
                                Utils.CreateDI4NetworkInterface(Convert.ToInt32(niList[1].PortNum));
                            }
                        }
                    }
                    #endregion eth1
                }
                //Namrata: 06/02/2018
                if (niList[0].Enable == "YES" && niList[1].Enable == "YES")
                {
                    niList[2].Enable = niList[3].Enable = "NO";
                    Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[2].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[2].PortNum);
                    Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[3].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[3].PortNum);
                }
                if (niList[2].Enable == "YES" && niList[3].Enable == "YES")
                {
                    niList[0].Enable = niList[1].Enable = "NO";
                    Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[0].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[0].PortNum);
                    Utils.RemoveDI4IEDNetwork("NetworkConfiguration", Int32.Parse(niList[1].PortNum), 0, Int32.Parse((Globals.DINo).ToString()), "LANHealth_" + niList[1].PortNum);
                }
                #region Valild IPaddress and VirtualIP with respective SubnetMask
                //Namrata: 08/02/2018
                if (ucnc.txtVirtualIP.Text != "0.0.0.0")
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(ucnc.txtIP.Text)) && !string.IsNullOrEmpty(Convert.ToString(ucnc.txtVirtualIP.Text)) && !string.IsNullOrEmpty(Convert.ToString(ucnc.txtSubnetMask.Text)))
                    {
                        var SubnetMask = IPAddress.Parse(ucnc.txtSubnetMask.Text);//Get Subnet Mask
                        var ipAddress = IPAddress.Parse(ucnc.txtIP.Text);//Get IP Address
                        IPAddress networkAddres1 = IPAddressExtensions.GetNetworkAddress(ipAddress, SubnetMask);
                        var VirtualIPAddress = IPAddress.Parse(ucnc.txtVirtualIP.Text);//Get VirtualIP Address
                        IPAddress networkAddres2 = IPAddressExtensions.GetNetworkAddress(VirtualIPAddress, SubnetMask);
                        bool inSameNet = networkAddres1.IsInSameSubnet(networkAddres2, SubnetMask);


                        //var ipAddress = IPAddress.Parse(Convert.ToString(ucnc.txtIP.Text));//Get IP Address
                        //var VirtualIPAddress = IPAddress.Parse(Convert.ToString(ucnc.txtVirtualIP.Text));//Get VirtualIP Address
                        //var SubnetMask = IPAddress.Parse(Convert.ToString(ucnc.txtSubnetMask.Text));//Get Subnet Mask
                        //bool inSameNet = ipAddress.IsInSameSubnet(VirtualIPAddress, SubnetMask);

                        if (inSameNet == true)
                        {

                        }
                        else
                        {
                            MessageBox.Show("IP Address and VirtualIP Address are not on the same network", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                #endregion Valild IPaddress and VirtualIP with respective SubnetMask
                //Namrata:31/01/2019
                if (ucnc.txtVirtualIP.Text == "0.0.0.0")
                {
                    if (Regex.IsMatch(ucnc.txtSubnetMask.Text, @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"))
                    {

                    }
                    else
                    {
                        MessageBox.Show("Valid Subnetmask ranges are between 0.0.0.0 - 255.255.255.255." , Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //IPValid.ValidteSubnetMask(ucnc.txtSubnetMask.Text);
                }
                refreshList();
                if (sender != null && e != null)
                {
                    ucnc.grpNI.Visible = false;
                    mode = Mode.NONE;
                    editIndex = -1;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnCancel_Click";
            try
            {
                //Namrata: 28/10/2017
                foreach (ListViewItem i in ucnc.lvNPorts.CheckedItems)
                {
                    i.Checked = false;
                }
                ucnc.grpNI.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucnc.grpNI);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnFirst_Click";
            try
            {
                Console.WriteLine("*** ucnc btnFirst_Click clicked in class!!!");
                if (ucnc.lvNPorts.Items.Count <= 0) return;
                if (niList.ElementAt(0).IsNodeComment) return;
                editIndex = 0;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnPrev_Click";
            try
            {
                //Namrata:09/08/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** ucnc btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (niList.ElementAt(editIndex - 1).IsNodeComment) return;
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
            string strRoutineName = "btnPrev_Click";
            try
            {
                //Namrata:09/08/2017
                btnDone_Click(null, null);
                Console.WriteLine("*** ucnc btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= ucnc.lvNPorts.Items.Count) return;
                if (niList.ElementAt(editIndex + 1).IsNodeComment) return;
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
            string strRoutineName = "btnLast_Click";
            try
            {
                Console.WriteLine("*** ucnc btnLast_Click clicked in class!!!");
                if (ucnc.lvNPorts.Items.Count <= 0) return;
                if (niList.ElementAt(niList.Count - 1).IsNodeComment) return;
                editIndex = niList.Count - 1;
                loadValues();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvNPorts_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvNPorts_DoubleClick";
            try
            {
                if (ucnc.lvNPorts.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucnc.lvNPorts.SelectedItems[0];
                if (niList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Namrata: 06-02-2018
                if (Utils.RedundancyMode == "Primary" || Utils.RedundancyMode == "Secondary")
                {
                    ucnc.txtVirtualIP.Enabled = true;
                }
                else if (Utils.RedundancyMode == "None")
                {
                    ucnc.txtVirtualIP.Enabled = false;
                }
                ucnc.chkEnable.Enabled = true;
                if (niList[0].Enable == "YES" && niList[1].Enable == "YES")
                {
                    niList[2].Enable = niList[3].Enable = "NO";
                    if (lvi.Index == 0 || lvi.Index == 1)
                    {
                        ucnc.chkEnable.Enabled = true;
                    }
                    else
                    {
                        ucnc.chkEnable.Enabled = false;
                    }
                }
                if (niList[2].Enable == "YES" && niList[3].Enable == "YES")
                {
                    niList[0].Enable = niList[1].Enable = "NO";
                    if (lvi.Index == 2 || lvi.Index == 3)
                    {
                        ucnc.chkEnable.Enabled = true;
                    }
                    else
                    {
                        ucnc.chkEnable.Enabled = false;
                    }
                }
                ucnc.grpNI.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucnc.grpNI, true);
                loadValues();
                ucnc.cmbAddressType.Focus();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            string strRoutineName = "lvNPorts_DoubleClick";
            try
            {
                NetworkInterface ni = niList.ElementAt(editIndex);
                if (ni != null)
                {
                    ucnc.cmbAddressType.SelectedIndex = ucnc.cmbAddressType.FindStringExact(ni.AddressType);
                    ucnc.cmbConnectionType.SelectedIndex = ucnc.cmbConnectionType.FindStringExact(ni.ConnectionType);
                    ucnc.txtIP.Text = ni.IP;
                    ucnc.txtVirtualIP.Text = ni.VirtualIP;
                    ucnc.txtSubnetMask.Text = ni.SubNetMask;
                    ucnc.txtGateway.Text = ni.GateWay;
                    ucnc.txtPortNo.Text = ni.PortNum;
                    ucnc.txtPortName.Text = ni.PortName;
                    //Namrata:30/5/2017
                    ucnc.cmbPortName.SelectedIndex = ucnc.cmbPortName.FindStringExact(ni.PrimaryDevice);
                    if (ni.Enable.ToLower() == "yes") ucnc.chkEnable.Checked = true;
                    else ucnc.chkEnable.Checked = false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Validate()
        {
            bool status = true;
            //Check empty field's
            if (Utils.IsEmptyFields(ucnc.grpNI))
            {
                MessageBox.Show("Fields cannot be empty!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check IP...
            if (!Utils.IsValidIPv4(ucnc.txtIP.Text))
            {
                MessageBox.Show("Invalid IP.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check IP...
            if (!Utils.IsValidIPv4(ucnc.txtVirtualIP.Text))
            {
                MessageBox.Show("Invalid VirtualIP.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Check subnet mask
            //if (!Utils.IsValidSubnet(ucnc.txtSubnetMask.Text))
            //{
            //    MessageBox.Show("Invalid Subnet mask.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            //Check gateway
            if (!Utils.IsValidIPv4(ucnc.txtGateway.Text))
            {
                MessageBox.Show("Invalid Gateway.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return status;
        }
        private void fillOptions()
        {
            string strRoutineName = "btnFirst_Click";
            try
            {
                //Fill Address Type...
                ucnc.cmbAddressType.Items.Clear();
                foreach (String at in NetworkInterface.getAddressTypes())
                {
                    ucnc.cmbAddressType.Items.Add(at.ToString());
                }
                ucnc.cmbAddressType.SelectedIndex = 0;

                //Fill Connection Type...
                ucnc.cmbConnectionType.Items.Clear();
                foreach (String ct in NetworkInterface.getConnectionTypes())
                {
                    ucnc.cmbConnectionType.Items.Add(ct.ToString());
                }
                ucnc.cmbConnectionType.SelectedIndex = 0;

                //Fill Port name
                ucnc.cmbPortName.Items.Clear();
                foreach (String ct1 in NetworkInterface.getPortName())
                {
                    ucnc.cmbPortName.Items.Add(ct1.ToString());
                }
                ucnc.cmbPortName.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ucnc_Load(object sender, EventArgs e)
        {
            Utils.WriteLine(VerboseLevel.BOMBARD, "######### ucnc_Load");
        }
        public Control getView(List<string> kpArr)
        {
            return ucnc;
        }
        public XmlNode exportXMLnode()
        {
            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            XmlNode rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);
            foreach (NetworkInterface ni in niList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(ni.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
        }
        public void parseNCNode(XmlNode ncNode)
        {
            string strRoutineName = "parseNCNode";
            try
            {
                //Namrata: 06/03/2018
                //Check if 4th entry exist in NetworkConfiguration while Import XML File .
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<Lan AddressType='STATIC' IP='10.0.2.106' VirtualIP='0.0.0.0' GateWay='10.0.0.50' SubNetMask='255.0.0.0' PortNum='4' PortName='br0' PrimaryDevice='eth0' Enable='NO' ConnectionType='Bond' Mode='ActiveBackup' BondPort0='1' BondPort1='2' LinkUpDelay='100' LinkDownDelay='100' />");
                
                XmlDocument doc3 = new XmlDocument();
                doc3.Load(Utils.XMLFileName);
                rnName = ncNode.Name;
                if (doc3.DocumentElement.ChildNodes[1].Name == "NetWorkConfiguration")
                {
                    if (doc3.DocumentElement.ChildNodes[1].HasChildNodes)
                    {
                        if (doc3.DocumentElement.ChildNodes[1].ChildNodes.Count != 4)
                        {
                            XmlNode ncNode1 = doc3.ImportNode(doc.DocumentElement, true);
                            doc3.DocumentElement.ChildNodes[1].AppendChild(ncNode1);
                            doc3.Save(Utils.XMLFileName);
                            ncNode.AppendChild(ncNode.OwnerDocument.ImportNode(ncNode1, true));
                        }
                    }
                }
                foreach (XmlNode node in ncNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    niList.Add(new NetworkInterface(node));
                }
                refreshList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addListHeaders()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                ucnc.lvNPorts.Columns.Add("Port No.", 75, HorizontalAlignment.Left);
                ucnc.lvNPorts.Columns.Add("Port Name", 90, HorizontalAlignment.Left);
                ucnc.lvNPorts.Columns.Add("Connection Type", 100, HorizontalAlignment.Left);
                ucnc.lvNPorts.Columns.Add("Address Type", 120, HorizontalAlignment.Left);
                ucnc.lvNPorts.Columns.Add("IP Address", 130, HorizontalAlignment.Left);
                ucnc.lvNPorts.Columns.Add("Virtual IP Address", 130, HorizontalAlignment.Left);
                ucnc.lvNPorts.Columns.Add("Subnet Mask", 120, HorizontalAlignment.Left);
                ucnc.lvNPorts.Columns.Add("Gateway", 100, HorizontalAlignment.Left);
                //Namrata:30/5/2017
                ucnc.lvNPorts.Columns.Add("Primary Device", 100, HorizontalAlignment.Left);
                ucnc.lvNPorts.Columns.Add("Enable", 100, HorizontalAlignment.Left);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        DataTable dt = new DataTable();
        public void refreshList()
        {
            string strRoutineName = "addListHeaders";
            try
            {
                int cnt = 0;
                Utils.NetworkIPLIstDummy.Clear();
                ucnc.lvNPorts.Items.Clear();
                foreach (NetworkInterface ni in niList)
                {
                    string[] row = new string[10];//Namrata:30/5/2017
                    if (ni.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = ni.PortNum;
                        row[1] = ni.PortName;
                        row[2] = ni.ConnectionType;
                        row[3] = ni.AddressType;
                        row[4] = ni.IP;
                        row[5] = ni.VirtualIP;
                        row[6] = ni.SubNetMask;
                        row[7] = ni.GateWay;
                        row[8] = ni.PrimaryDevice;
                        row[9] = ni.Enable;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucnc.lvNPorts.Items.Add(lvItem);
                }
                //Namrata: 27/7/2017
                Utils.NetworkIPLIst.AddRange(niList);
                Utils.NetworkIPLIstDummy.AddRange(niList);
                ListToDataTable(niList);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + Ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ListToDataTable<NetworkInterface>(IList<NetworkInterface> varlist)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (typeof(NetworkInterface).IsValueType || typeof(NetworkInterface).Equals(typeof(string)))
            {
                DataColumn dc = new DataColumn("Values");
                dt.Columns.Add(dc);
                foreach (NetworkInterface item in varlist)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item;
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                PropertyInfo[] propT = typeof(NetworkInterface).GetProperties(); //find all the public properties of this Type using reflection;
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
            Utils.dtNetworkConfig = ds;
            Utils.dtNetwork = dt;
            Utils.DtNetworkConfiguration = dt;
            //return dt;
        }
    }
}
