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
    /**
    * \brief     <b>ucGroupMODBUSSlave</b> is a user interface to display all MODBUSSlave's
    * \details   This is a user interface to display all MODBUSSlave's. It provides interface
    * to add multiple MODBUSSlave's. The user can also modify it's parameters.
    * 
    * 
    */
    public partial class ucGroupMODBUSSlave : UserControl
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
        public event EventHandler lvMODBUSSlaveDoubleClick;
        public event EventHandler cmbProtocolTypeSelectedIndexChanged;
        public event EventHandler cmbLocalIPSelectedIndexChanged;
        public event EventHandler cmbLocalIPSelectedValueChanged;
        public event EventHandler cmbPortNoSelectedIndexChanged;
        public event DrawItemEventHandler cmbLocalIPDrawItem;
        public event ItemCheckEventHandler lvMODBUSSlaveItemCheck;
        public event EventHandler CmbPortNameSelectedIndexChanged;
        public ucGroupMODBUSSlave()
        {
            InitializeComponent();
            //Utils.createPBTitleBar(pbHdr, lblHdrText, this.PointToScreen(lblHdrText.Location));
            txtSlaveNum.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtTCPPort.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtRemoteIP.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtEventQSize.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtFirmwareVersion.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
        }
        private void CmbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbPortNameSelectedIndexChanged != null)
                CmbPortNameSelectedIndexChanged(sender, e);
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

        private void lvMODBUSSlave_DoubleClick(object sender, EventArgs e)
        {
            if (lvMODBUSSlaveDoubleClick != null)
                lvMODBUSSlaveDoubleClick(sender, e);
        }

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpMODBUSSlave, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpMODBUSSlave, sender, e);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (btnFirstClick != null)
                btnFirstClick(sender, e);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (btnPrevClick != null)
                btnPrevClick(sender, e);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (btnNextClick != null)
                btnNextClick(sender, e);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (btnLastClick != null)
                btnLastClick(sender, e);
        }

        private void txtTCPPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtASDUaddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtT0_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtT1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtT2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtT3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtW_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtK_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtCyclicInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtEventQSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtRemoteIP_KeyPress(object sender, KeyPressEventArgs e)

        {
            Utils.allowIP(sender, e);
        }

        private void txtUnitID_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void cmbProtocolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProtocolTypeSelectedIndexChanged != null)
                cmbProtocolTypeSelectedIndexChanged(sender, e);
        }

        private void ucGroupMODBUSSlave_Load(object sender, EventArgs e)
        {

        }

       
        private void cmbLocalIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLocalIPSelectedIndexChanged != null)
                cmbLocalIPSelectedIndexChanged(sender, e);
        }

        private void cmbLocalIP_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbLocalIPSelectedValueChanged != null)
                cmbLocalIPSelectedValueChanged(sender, e);
        }

        private void cmbPortNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPortNoSelectedIndexChanged != null)
                cmbPortNoSelectedIndexChanged(sender, e);
        }

        private void cmbLocalIP_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (cmbLocalIPDrawItem != null)
                cmbLocalIPDrawItem(sender, e);
        }
     
        private void lvMODBUSSlave_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvMODBUSSlaveItemCheck != null)
                lvMODBUSSlaveItemCheck(sender, e);
        }

        private void grpMODBUSSlave_Enter(object sender, EventArgs e)
        {

        }

        
    }
}
