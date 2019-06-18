using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace OpenProPlusConfigurator
{
    public partial class ucGroup61850ServerSlave : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnExportINIClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event EventHandler lv61850ServerSlaveDoubleClick;
        public event EventHandler cmbProtocolTypeSelectedIndexChanged;
        public event EventHandler btnimporticdClick;
        public event EventHandler BtnICDClick;
        public event EventHandler ucGroup61850ServerSlaveLoad;
        public event ItemCheckEventHandler lv61850ServerSlaveItemCheck;
        public event EventHandler PCDFileClick;
        public event EventHandler CmbPortNameSelectedIndexChanged;
        public ucGroup61850ServerSlave()
        {
            InitializeComponent();
            txtSlaveNum.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtTCPPort.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtRemoteIP.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            //txtEventQSize.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtFirmwareVersion.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
        }
        private void CmbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbPortNameSelectedIndexChanged != null)
                CmbPortNameSelectedIndexChanged(sender, e);
        }
        private void BtnICD_Click(object sender, EventArgs e)
        {
            if (BtnICDClick != null)
                BtnICDClick(sender, e);
        }
        private void btnimporticd_Click(object sender, EventArgs e)
        {
            if (btnimporticdClick != null)
                btnimporticdClick(sender, e);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAddClick != null)
                btnAddClick(sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDeleteClick != null)
                btnDeleteClick(sender, e);
        }

        private void btnExportINI_Click(object sender, EventArgs e)
        {
            if (btnExportINIClick != null)
                btnExportINIClick(sender, e);

        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (btnDoneClick != null)
                btnDoneClick(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancelClick != null)
                btnCancelClick(sender, e);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {

        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void btnLast_Click(object sender, EventArgs e)
        {

        }

        private void lv61850ServerSlave_DoubleClick(object sender, EventArgs e)
        {
            if (lv61850ServerSlaveDoubleClick != null)
                lv61850ServerSlaveDoubleClick(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpIEC61850Slave, sender, e);
        }

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpIEC61850Slave, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void ucGroup61850ServerSlave_Load(object sender, EventArgs e)
        {
            if (ucGroup61850ServerSlaveLoad != null)
                ucGroup61850ServerSlaveLoad(sender, e);
        }

        private void pbHdr_MouseDown_1(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove_1(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpIEC61850Slave, sender, e);
        }
      
        private void lv61850ServerSlave_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lv61850ServerSlaveItemCheck != null)
                lv61850ServerSlaveItemCheck(sender, e);
        }

        private void PCDFile_Click(object sender, EventArgs e)
        {
            if (PCDFileClick != null)
                PCDFileClick(sender, e);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cmbLocalIP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
