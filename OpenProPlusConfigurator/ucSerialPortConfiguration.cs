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
    * \brief     <b>ucSerialPortConfiguration</b> is a user interface to display all SerialInterface's
    * \details   This is a user interface to display all SerialInterface's. The user can only modify it's parameters.
    * 
    * 
    */
    public partial class ucSerialPortConfiguration : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event EventHandler lvSPortsDoubleClick;
        public event EventHandler btnEditClick;
        public event EventHandler cmbFlowControlSelectedIndexChanged;
        public event ItemCheckEventHandler lvSPortsItemCheck;
        public event KeyPressEventHandler txtTcpPortKeyPress;
        public ucSerialPortConfiguration()
        {
            InitializeComponent();
            txtPortNo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtPortName.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtTcpPort.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtRTSPreTime.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtRTSPostTime.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
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
        private void lvSPorts_DoubleClick(object sender, EventArgs e)
        {
            if (lvSPortsDoubleClick != null)
                lvSPortsDoubleClick(sender, e);
        }
        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpSI, sender, e);
        }
        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpSI, sender, e);
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
        private void cmbFlowControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFlowControlSelectedIndexChanged != null)
                cmbFlowControlSelectedIndexChanged(sender, e);
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEditClick != null)
                btnEditClick(sender, e);
        }
  
        private void lvSPorts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvSPortsItemCheck != null)
                lvSPortsItemCheck(sender, e);
        }

        private void txtTcpPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtTcpPortKeyPress != null)
                txtTcpPortKeyPress(sender, e);
        }
    }
}
