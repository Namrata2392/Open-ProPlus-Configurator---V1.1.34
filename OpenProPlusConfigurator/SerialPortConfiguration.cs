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
    * \brief     <b>SerialPortConfiguration</b> is a class to store all the SerialInterface's
    * \details   This class stores info related to all SerialInterface's. It only allows
    * user to modify the parameters. It also exports the XML node related to this object.
    * 
    */
    public class SerialPortConfiguration
    {
        private enum Mode
        {
            NONE,
            ADD,
            EDIT
        }
        private string rnName = "SerialPortConfiguration";
        private Mode mode = Mode.NONE;
        private int editIndex = -1;

        List<SerialInterface> siList = new List<SerialInterface>();
        ucSerialPortConfiguration ucspc = new ucSerialPortConfiguration();

        public SerialPortConfiguration()
        {
            string strRoutineName = "txtTcpPortKeyPress";
            try
            {
                ucspc.txtTcpPortKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTcpPort_KeyPress);
                //ucspc.btnAddClick += new System.EventHandler(this.btnAdd_Click);
                ucspc.lvSPortsItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvSPorts_ItemCheck);
                ucspc.btnDeleteClick += new System.EventHandler(this.btnDelete_Click);
                ucspc.btnDoneClick += new System.EventHandler(this.btnDone_Click);
                ucspc.btnCancelClick += new System.EventHandler(this.btnCancel_Click);
                ucspc.btnFirstClick += new System.EventHandler(this.btnFirst_Click);
                ucspc.btnPrevClick += new System.EventHandler(this.btnPrev_Click);
                ucspc.btnNextClick += new System.EventHandler(this.btnNext_Click);
                ucspc.btnLastClick += new System.EventHandler(this.btnLast_Click);
                ucspc.btnEditClick += new System.EventHandler(this.btnEdit_Click);
                ucspc.lvSPortsDoubleClick += new System.EventHandler(this.lvSPorts_DoubleClick);
                ucspc.cmbFlowControlSelectedIndexChanged += new System.EventHandler(this.cmbFlowControl_SelectedIndexChanged);
                addListHeaders();
                fillOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        SerialInterface serial = null;
        private void txtTcpPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strRoutineName = "txtTcpPortKeyPress";
            try
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvSPorts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string strRoutineName = "lvSPorts_ItemCheck";
            try
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    int index = e.Index;
                    ucspc.lvSPorts.CheckedItems.OfType<ListViewItem>().Where(x => x.Index != index).ToList().ForEach(item =>
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
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnEdit_Click";
            try
            {
                ucspc.lvSPorts.SelectedItems.Clear();
                if (ucspc.lvSPorts.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Select Single Element To Update Serial Configuration !!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    for (int i = 0; i < ucspc.lvSPorts.Items.Count; i++)
                    {
                        if (ucspc.lvSPorts.Items[i].Checked)
                        {
                            serial = siList.ElementAt(i);
                            ListViewItem lvi = ucspc.lvSPorts.CheckedItems[0];
                            Utils.UncheckOthers(ucspc.lvSPorts, lvi.Index);
                            ucspc.grpSI.Visible = true;
                            mode = Mode.EDIT;
                            editIndex = i;
                            Utils.showNavigation(ucspc.grpSI, true);
                            loadValues();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void cmbFlowControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoutineName = "cmbFlowControl_SelectedIndexChanged";
            try
            {
                Console.WriteLine("*** Caught FlowControl change!!! text: {0}", ucspc.cmbFlowControl.GetItemText(ucspc.cmbFlowControl.SelectedItem));
                if (ucspc.cmbFlowControl.GetItemText(ucspc.cmbFlowControl.SelectedItem) == "RTS/CTS")
                {
                    ucspc.txtRTSPreTime.Enabled = true;
                    ucspc.txtRTSPostTime.Enabled = true;
                }
                else
                {
                    ucspc.txtRTSPreTime.Enabled = false;
                    ucspc.txtRTSPostTime.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public List<SerialInterface> getSerialInterfaces()
        {
            return siList;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            mode = Mode.ADD;
            editIndex = -1;
            Utils.resetValues(ucspc.grpSI);
            Utils.showNavigation(ucspc.grpSI, false);
            ucspc.grpSI.Visible = true;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strRoutineName = "btnDelete_Click";
            try
            {
                Utils.WriteLine(VerboseLevel.DEBUG, "*** siList count: {0} lv count: {1}", siList.Count, ucspc.lvSPorts.Items.Count);
                DialogResult result = MessageBox.Show(Globals.PROMPT_DELETE_ENTRY, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    return;
                }

                for (int i = ucspc.lvSPorts.Items.Count - 1; i >= 0; i--)
                {
                    if (ucspc.lvSPorts.Items[i].Checked)
                    {
                        Utils.WriteLine(VerboseLevel.DEBUG, "*** removing indices: {0}", i);
                        siList.RemoveAt(i);
                        ucspc.lvSPorts.Items[i].Remove();
                    }
                }
                Utils.WriteLine(VerboseLevel.DEBUG, "*** siList count: {0} lv count: {1}", siList.Count, ucspc.lvSPorts.Items.Count);
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
                List<KeyValuePair<string, string>> siData = Utils.getKeyValueAttributes(ucspc.grpSI);
                if (mode == Mode.ADD)
                {
                    siList.Add(new SerialInterface("Port", siData));
                }
                else if (mode == Mode.EDIT)
                {
                    siList[editIndex].updateAttributes(siData);
                }

                refreshList();
                //Namrata: 14/09/2017
                //Create DI Entry in Virtual DI
                if (siData[0].Value == "YES")
                {
                    Utils.CreateDI4SerialPort(Convert.ToInt32(siData[6].Value));
                }

                //Namrata: 27/09/2017
                //Remove Entry From Virtual DI when Active is "NO".
                else if (siData[0].Value == "NO")
                {
                    Utils.RemoveDI4IEDserialConfiguratiuon("SerialPortConfiguration", Int32.Parse(siData[6].Value), 0, Int32.Parse((Globals.DINo).ToString()), "UARTHealth_" + siData[6].Value);
                }
                //Namrata: 09/08/2017
                if (sender != null && e != null)
                {
                    ucspc.grpSI.Visible = false;
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
                //Namrata: 28/10/2017
                foreach (ListViewItem Listitem in ucspc.lvSPorts.CheckedItems)
                {
                    Listitem.Checked = false;
                }
                ucspc.grpSI.Visible = false;
                mode = Mode.NONE;
                editIndex = -1;
                Utils.resetValues(ucspc.grpSI);
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
                Console.WriteLine("*** ucspc btnFirst_Click clicked in class!!!");
                if (ucspc.lvSPorts.Items.Count <= 0) return;
                if (siList.ElementAt(0).IsNodeComment) return;
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
                Console.WriteLine("*** ucspc btnPrev_Click clicked in class!!!");
                if (editIndex - 1 < 0) return;
                if (siList.ElementAt(editIndex - 1).IsNodeComment) return;
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
                Console.WriteLine("*** ucspc btnNext_Click clicked in class!!!");
                if (editIndex + 1 >= ucspc.lvSPorts.Items.Count) return;
                if (siList.ElementAt(editIndex + 1).IsNodeComment) return;
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
                Console.WriteLine("*** ucspc btnLast_Click clicked in class!!!");
                if (ucspc.lvSPorts.Items.Count <= 0) return;
                if (siList.ElementAt(siList.Count - 1).IsNodeComment) return;
                editIndex = siList.Count - 1;
                loadValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lvSPorts_DoubleClick(object sender, EventArgs e)
        {
            string strRoutineName = "lvSPorts_DoubleClick";
            try
            {
                if (ucspc.lvSPorts.SelectedItems.Count <= 0) return;
                ListViewItem lvi = ucspc.lvSPorts.SelectedItems[0];
                Utils.UncheckOthers(ucspc.lvSPorts, lvi.Index);
                if (siList.ElementAt(lvi.Index).IsNodeComment)
                {
                    MessageBox.Show("Comments cannot be edited!!!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ucspc.grpSI.Visible = true;
                mode = Mode.EDIT;
                editIndex = lvi.Index;
                Utils.showNavigation(ucspc.grpSI, true);
                loadValues();
                ucspc.cmbBaudRate.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            string strRoutineName = "loadValues";
            try
            {
                SerialInterface si = siList.ElementAt(editIndex);
                if (si != null)
                {
                    ucspc.txtPortNo.Text = si.PortNum;
                    ucspc.cmbBaudRate.SelectedIndex = ucspc.cmbBaudRate.FindStringExact(si.BaudRate);
                    ucspc.cmbDatabits.SelectedIndex = ucspc.cmbDatabits.FindStringExact(si.Databits);
                    ucspc.cmbStopbits.SelectedIndex = ucspc.cmbStopbits.FindStringExact(si.Stopbits);
                    ucspc.cmbFlowControl.SelectedIndex = ucspc.cmbFlowControl.FindStringExact(si.FlowControl);
                    ucspc.cmbParity.SelectedIndex = ucspc.cmbParity.FindStringExact(si.Parity);
                    ucspc.txtRTSPreTime.Text = si.RtsPreTime;
                    ucspc.txtRTSPostTime.Text = si.RtsPostTime;
                    ucspc.txtPortName.Text = si.PortName;
                    ucspc.txtTcpPort.Text = si.TcpPort;
                    //Namrata: 14/09/2017
                    if (si.Enable.ToLower() == "yes") ucspc.chkEnable.Checked = true;
                    else ucspc.chkEnable.Checked = false;
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
            return status;
        }
        private void fillOptions()
        {
            string strRoutineName = "fillOptions";
            try
            {
                //Fill Baudrate...
                ucspc.cmbBaudRate.Items.Clear();
                foreach (String br in SerialInterface.getBaudRates())
                {
                    ucspc.cmbBaudRate.Items.Add(br.ToString());
                }
                ucspc.cmbBaudRate.SelectedIndex = 0;

                //Fill Databits...
                ucspc.cmbDatabits.Items.Clear();
                foreach (String db in SerialInterface.getDataBits())
                {
                    ucspc.cmbDatabits.Items.Add(db.ToString());
                }
                ucspc.cmbDatabits.SelectedIndex = 0;

                //Fill Stopbits...
                ucspc.cmbStopbits.Items.Clear();
                foreach (String sb in SerialInterface.getStopBits())
                {
                    ucspc.cmbStopbits.Items.Add(sb.ToString());
                }
                ucspc.cmbStopbits.SelectedIndex = 0;

                //Fill FlowControl...
                ucspc.cmbFlowControl.Items.Clear();
                foreach (String fc in SerialInterface.getFlowControls())
                {
                    ucspc.cmbFlowControl.Items.Add(fc.ToString());
                }
                ucspc.cmbFlowControl.SelectedIndex = 0;

                //Fill Parity...
                ucspc.cmbParity.Items.Clear();
                foreach (String pr in SerialInterface.getParities())
                {
                    ucspc.cmbParity.Items.Add(pr.ToString());
                }
                ucspc.cmbParity.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Control getView(List<string> kpArr)
        {
            return ucspc;
        }
        public XmlNode exportXMLnode()
        {
            string strRoutineName = "exportXMLnode";
            //try
            //{
            XmlDocument xmlDoc = new XmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            XmlNode rootNode = xmlDoc.CreateElement(rnName);
            xmlDoc.AppendChild(rootNode);

            foreach (SerialInterface si in siList)
            {
                XmlNode importNode = rootNode.OwnerDocument.ImportNode(si.exportXMLnode(), true);
                rootNode.AppendChild(importNode);
            }
            return rootNode;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return rootNode;
            //}
        }
        public void parseSPCNode(XmlNode spcNode)
        {
            string strRoutineName = "parseSPCNode";
            try
            {
                rnName = spcNode.Name; //First set root node name...

                foreach (XmlNode node in spcNode)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;//IMP: Ignore comments in file...
                    siList.Add(new SerialInterface(node));
                }
                refreshList();
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
                ucspc.lvSPorts.Columns.Add("Port No.", 70, HorizontalAlignment.Left);
                ucspc.lvSPorts.Columns.Add("Baud Rate", 100, HorizontalAlignment.Left);
                ucspc.lvSPorts.Columns.Add("Data Bits", 95, HorizontalAlignment.Left);
                ucspc.lvSPorts.Columns.Add("Stop Bits", 95, HorizontalAlignment.Left);
                ucspc.lvSPorts.Columns.Add("Flow Control", 100, HorizontalAlignment.Left);
                ucspc.lvSPorts.Columns.Add("Parity", 120, HorizontalAlignment.Left);
                ucspc.lvSPorts.Columns.Add("RTS Pre-time", 100, HorizontalAlignment.Left);
                ucspc.lvSPorts.Columns.Add("RTS Post-time", 100, HorizontalAlignment.Left);
                ucspc.lvSPorts.Columns.Add("Port Name", 100, HorizontalAlignment.Left);
                ucspc.lvSPorts.Columns.Add("TCP Port", 90, HorizontalAlignment.Left);
                ucspc.lvSPorts.Columns.Add("Enable", 100, HorizontalAlignment.Left);
            }
            //ucspc.lvSPorts.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void refreshList()
        {
            string strRoutineName = "refreshList";
            try
            {
                int cnt = 0;
                ucspc.lvSPorts.Items.Clear();
                foreach (SerialInterface si in siList)
                {
                    string[] row = new string[11];
                    if (si.IsNodeComment)
                    {
                        row[0] = "Comment...";
                    }
                    else
                    {
                        row[0] = si.PortNum;
                        row[1] = si.BaudRate;
                        row[2] = si.Databits;
                        row[3] = si.Stopbits;
                        row[4] = si.FlowControl;
                        row[5] = si.Parity;
                        row[6] = si.RtsPreTime;
                        row[7] = si.RtsPostTime;
                        row[8] = si.PortName;
                        row[9] = si.TcpPort;
                        //Namrata: 14/09/2017
                        row[10] = si.Enable;
                    }
                    ListViewItem lvItem = new ListViewItem(row);
                    if (cnt++ % 2 == 0) lvItem.BackColor = ColorTranslator.FromHtml(Globals.rowColour);
                    ucspc.lvSPorts.Items.Add(lvItem);
                }
                //Namrata: 27/7/2017
                Utils.DummysiList.AddRange(siList);
                ListToDataTable(siList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DataTable ListToDataTable<NetworkInterface>(IList<NetworkInterface> varlist)
        {
            string strRoutineName = "ListToDataTable";
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
            //for reference types other than  string
            // Used PropertyInfo class of System.Reflection
            else
            {
                PropertyInfo[] propT = typeof(NetworkInterface).GetProperties();//find all the public properties of this Type using reflection
                foreach (PropertyInfo pi in propT)
                {
                    DataColumn dc = new DataColumn(pi.Name, pi.PropertyType); //create a datacolumn for each property
                    dt.Columns.Add(dc);
                }
                //now we iterate through all the items , take the corresponding values and add a new row in dt
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
            Utils.dtSerialConfig = ds;
            Utils.dtSerial = dt;
            return dt;
            // catch (Exception ex)
            //{
            //    MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}
