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
    * \brief     <b>ucNetworkConfiguration</b> is a user interface to display all NetworkInterface's
    * \details   This is a user interface to display all NetworkInterface's. The user can only modify it's parameters.
    * 
    * 
    */
    public partial class ucNetworkConfiguration : UserControl
    {
        public event EventHandler Load;
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event EventHandler lvNPortsDoubleClick;
        public event DrawListViewColumnHeaderEventHandler lvNPortsDrawColumnHeader;
        public event EventHandler btnEditClick;
        public event EventHandler lvNPortsSelectedIndexChanged;
        public event ItemCheckEventHandler lvNPortsItemCheck;
        public event CancelEventHandler txtIPValidating;
        public ucNetworkConfiguration()
        {
            InitializeComponent();
            //Utils.createPBTitleBar(pbHdr, lblHdrText, this.PointToScreen(lblHdrText.Location));
            txtPortNo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtPortName.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
        }
        private void lvNPorts_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;

                using (var headerFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold))
                {
                    e.Graphics.FillRectangle(Brushes.Pink, e.Bounds);
                    e.Graphics.DrawString(e.Header.Text, headerFont,
                        Brushes.Black, e.Bounds, sf);
                }
            }
        }
        private void ucNetworkConfiguration_Load(object sender, EventArgs e)
        {
            Console.WriteLine("######### In uc, ucnc_Load");
            if (Load != null)
            {
                Load(sender, e);
            }
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

        private void lvNPorts_DoubleClick(object sender, EventArgs e)
        {
            if (lvNPortsDoubleClick != null)
                lvNPortsDoubleClick(sender, e);
        }

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpNI, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpNI, sender, e);
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

        private void txtIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowIP(sender, e);
        }

        private void txtSubnetMask_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowIP(sender, e);
        }

        private void txtGateway_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowIP(sender, e);
        }

        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEditClick != null)
                btnEditClick(sender, e);
        }

        private void lvNPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvNPortsSelectedIndexChanged != null)
                lvNPortsSelectedIndexChanged(sender, e);
        }
    
        private void lvNPorts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvNPortsItemCheck != null)
                lvNPortsItemCheck(sender, e);
        }

        private void txtIP_Validating(object sender, CancelEventArgs e)
        {
            if (txtIPValidating != null)
                txtIPValidating(sender, e);
        }
    }
}
