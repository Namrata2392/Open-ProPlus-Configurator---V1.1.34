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
    * \brief     <b>ucGroupIEC104</b> is a user interface to display all IEC104Slave's
    * \details   This is a user interface to display all IEC104Slave's. It provides interface
    * to add multiple IEC104Slave's. The user can also modify it's parameters.
    * 
    * 
    */
    public partial class ucGroupIEC104 : UserControl
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
        public event EventHandler lvIEC104SlaveDoubleClick;
        public event EventHandler cmbLocalIPSelectedIndexChanged;
        public event DrawListViewItemEventHandler lvIEC104SlaveDrawItem;
        public event DrawListViewColumnHeaderEventHandler lvIEC104SlaveDrawColumnHeader;//lvMODBUSmasterDrawSubItem
        public event DrawListViewSubItemEventHandler lvIEC104SlaveDrawSubItem;
        public event ItemCheckEventHandler lvIEC104SlaveItemCheck;
        public event ItemCheckedEventHandler lvIEC104SlaveItemChecked;
        public event EventHandler CmbPortNameSelectedIndexChanged;
        //Ajay: 28/12/2018
        public bool INIExported = false;
        public ucGroupIEC104()
        {
            InitializeComponent();
            //Utils.createPBTitleBar(pbHdr, lblHdrText, this.PointToScreen(lblHdrText.Location));
            txtSlaveNum.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtEventQSize.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtFirmwareVersion.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
        }

        private void CmbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbPortNameSelectedIndexChanged != null)
                CmbPortNameSelectedIndexChanged(sender, e);
        }
        private void lvIEC104Slave_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (lvIEC104SlaveDrawItem != null)
                lvIEC104SlaveDrawItem(sender, e);
        }
        private void lvIEC104Slave_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (lvIEC104SlaveDrawSubItem != null)
                lvIEC104SlaveDrawSubItem(sender, e);
        }
        private void lvIEC104Slave_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (lvIEC104SlaveDrawColumnHeader != null)
                lvIEC104SlaveDrawColumnHeader(sender, e);
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
            INIExported = false; //Ajay: 28/12/2018
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

        private void lvIEC104Slave_DoubleClick(object sender, EventArgs e)
        {
            if (lvIEC104SlaveDoubleClick != null)
                lvIEC104SlaveDoubleClick(sender, e);
        }

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpIEC104, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpIEC104, sender, e);
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

        private void ucGroupIEC104_Load(object sender, EventArgs e)
        {

        }

        private void pbHdr_Click(object sender, EventArgs e)
        {

        }

        private void cmbLocalIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLocalIPSelectedIndexChanged != null)
                cmbLocalIPSelectedIndexChanged(sender, e);
        }

        private void lvIEC104Slave_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvIEC104SlaveItemCheck != null)
                lvIEC104SlaveItemCheck(sender, e);
        }

        private void lvIEC104Slave_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lvIEC104SlaveItemChecked != null)
                lvIEC104SlaveItemChecked(sender, e);
        }

        private void grpIEC104_Enter(object sender, EventArgs e)
        {

        }

        private void lblIOA_Click(object sender, EventArgs e)
        {

        }
    }
}
