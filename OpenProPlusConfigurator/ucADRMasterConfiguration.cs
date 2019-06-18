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
    public partial class ucADRMasterConfiguration : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnExportIEDClick;
        public event EventHandler btnImportIEDClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler lvIEDListDoubleClick;
        public ucADRMasterConfiguration()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblDesc_Click(object sender, EventArgs e)
        {

        }

        private void lblAFV_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblRI_Click(object sender, EventArgs e)
        {

        }

        private void lblCSI_Click(object sender, EventArgs e)
        {

        }

        private void lblDB_Click(object sender, EventArgs e)
        {

        }

        private void lblPN_Click(object sender, EventArgs e)
        {

        }

        private void lblMN_Click(object sender, EventArgs e)
        {

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

        private void btnExportIED_Click(object sender, EventArgs e)
        {
            if (btnExportIEDClick != null)
                btnExportIEDClick(sender, e);
        }

        private void btnImportIED_Click(object sender, EventArgs e)
        {
            if (btnImportIEDClick != null)
                btnImportIEDClick(sender, e);
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


        private void lvIEDList_DoubleClick(object sender, EventArgs e)
        {
            if (lvIEDListDoubleClick != null)
                lvIEDListDoubleClick(sender, e);
        }

        private void ucADRMasterConfiguration_Load(object sender, EventArgs e)
        {

        }

        private void chkDR_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpIED, sender, e);
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void label2_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpIED, sender, e);
        }

        private void lvIEDList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grpIEC103_Enter(object sender, EventArgs e)
        {

        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SpecialCharacter_Validation(e);
        }
    }
}
