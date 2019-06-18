using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>ucDetails</b> is a user interface to display device details
    * \details   This is a user interface to display device details. The user can only modify it's parameters.
    * 
    * 
    */
    public partial class ucDetails : UserControl
    {
        public event EventHandler btnEditClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler lvDetailsDoubleClick;
        public event EventHandler lvDetailsSizeChanged;
        public event EventHandler ucDetailsLoad;
        public ucDetails()
        {
            InitializeComponent();
            //Utils.createPBTitleBar(pbHdr, lblHdrText, this.PointToScreen(lblHdrText.Location));
            txtDescription.MaxLength = Globals.MAX_DESCRIPTION_LEN;
        }

        private void txtXMLVersion_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEditClick != null)
                btnEditClick(sender, e);
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

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDetails, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDetails, sender, e);
        }

        private void lblHwVersion_Click(object sender, EventArgs e)
        {

        }

        private void lvDetails_DoubleClick(object sender, EventArgs e)
        {
            if (lvDetailsDoubleClick != null)
                lvDetailsDoubleClick(sender, e);
        }

        private void lvDetails_SizeChanged(object sender, EventArgs e)
        {
            if (lvDetailsSizeChanged != null)
                lvDetailsSizeChanged(sender, e);
        }
        private void ucDetails_Load(object sender, EventArgs e)
        {
            if (ucDetailsLoad != null)
                ucDetailsLoad(sender, e);
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SpecialCharacter_Validation(e);
        }
    }
}
