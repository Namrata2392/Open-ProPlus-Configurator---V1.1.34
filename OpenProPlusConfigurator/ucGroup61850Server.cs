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
    public partial class ucGroup61850Server : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event EventHandler lvMODBUSmasterDoubleClick;
        public event EventHandler cmbProtocolTypeSelectedIndexChanged;
        public ucGroup61850Server()
        {
            InitializeComponent();
            txtMasterNo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...

            txtDescription.MaxLength = Globals.MAX_DESCRIPTION_LEN;
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

        private void lvMODBUSmaster_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvMODBUSmaster_DoubleClick(object sender, EventArgs e)
        {
            if (lvMODBUSmasterDoubleClick != null)
                lvMODBUSmasterDoubleClick(sender, e);
        }

        private void cmbProtocolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProtocolTypeSelectedIndexChanged != null)
                cmbProtocolTypeSelectedIndexChanged(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpIEC61850, sender, e);
        }

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpIEC61850, sender, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public event ItemCheckEventHandler lvMODBUSmasterItemCheck;
        private void lvMODBUSmaster_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvMODBUSmasterItemCheck != null)
                lvMODBUSmasterItemCheck(sender, e);
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SpecialCharacter_Validation(e);
        }
    }
}
