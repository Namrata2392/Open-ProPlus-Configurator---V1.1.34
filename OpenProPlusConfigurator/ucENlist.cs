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
    * \brief     <b>ucENlist</b> is a user interface to display all EN's and there corresponding mapping infos.
    * \details   This is a user interface to display all EN's and there corresponding mapping's for various slaves. It provides interface
    * to add multiple EN's. The user can map this EN's to various slaves. The user can also modify mapping parameters.
    * 
    * 
    */
    public partial class ucENlist : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event EventHandler lvENlistDoubleClick;
        public event ListViewItemSelectionChangedEventHandler lvENlistItemSelectionChanged;
        public event ListViewItemSelectionChangedEventHandler lvENMapItemSelectionChanged;
        //public event EventHandler btnMapClick;
        public event EventHandler btnENMDeleteClick;
        public event EventHandler btnENMDoneClick;
        public event EventHandler btnENMCancelClick;
        public event EventHandler lvENMapDoubleClick;
        public event EventHandler button1Click;
        public event EventHandler button2Click;
        public event EventHandler LinkDeleteConfigueClick; 
        public event EventHandler lnkENMapClick;
        public event EventHandler lvENlistSelectedIndexChanged;
        public event EventHandler lvENMapSelectedIndexChanged;
        public event EventHandler cmbIEDNameSelectedIndexChanged;
        public event EventHandler cmb61850ResponseTypeSelectedIndexChanged;
        public event EventHandler cmb61850IndexSelectedIndexChanged;
        public ucENlist()
        {
            InitializeComponent();
            //Utils.createPBTitleBar(pbHdr, lblHdrText, this.PointToScreen(lblHdrText.Location));
            //Utils.createPBTitleBar(pbENMHdr, lblENMHdrText, this.PointToScreen(lblENMHdrText.Location));
            txtENNo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtENMNo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            cmbENMCommandType.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
              
        txtDescription.MaxLength = Globals.MAX_DESCRIPTION_LEN;
        }
        private void lvENlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvENlistSelectedIndexChanged != null)
                lvENlistSelectedIndexChanged(sender, e);
        }

        private void lvENMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvENMapSelectedIndexChanged != null)
                lvENMapSelectedIndexChanged(sender, e);
        }
        private void LinkDeleteConfigue_Click(object sender, EventArgs e)
        {
            if (LinkDeleteConfigueClick != null)
                LinkDeleteConfigueClick(sender, e);
        }
        private void lnkENMap_Click(object sender, EventArgs e)
        {
            if (lnkENMapClick != null)
                lnkENMapClick(sender, e);
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

        private void lvENlist_DoubleClick(object sender, EventArgs e)
        {
            if (lvENlistDoubleClick != null)
                lvENlistDoubleClick(sender, e);
        }

        private void lvENlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvENlistItemSelectionChanged != null)
                lvENlistItemSelectionChanged(sender, e);
            Console.WriteLine("***** lvENlist ItemSelectionChanged!!! index: {0}", e.ItemIndex);
        }

        private void lvENMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvENMapItemSelectionChanged != null)
                lvENMapItemSelectionChanged(sender, e);
            Console.WriteLine("***** lvENMap ItemSelectionChanged!!! index: {0}", e.ItemIndex);
        }
        /*
        private void btnMap_Click(object sender, EventArgs e)
        {
            if (btnMapClick != null)
                btnMapClick(sender, e);
        }
         */

        private void lvENMap_DoubleClick(object sender, EventArgs e)
        {
            if (lvENMapDoubleClick != null)
                lvENMapDoubleClick(sender, e);
        }

        private void btnENMDone_Click(object sender, EventArgs e)
        {
            if (btnENMDoneClick != null)
                btnENMDoneClick(sender, e);
        }

        private void btnENMCancel_Click(object sender, EventArgs e)
        {
            if (btnENMCancelClick != null)
                btnENMCancelClick(sender, e);
        }

        private void btnENMDelete_Click(object sender, EventArgs e)
        {
            if (btnENMDeleteClick != null)
                btnENMDeleteClick(sender, e);
        }

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpEN, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpEN, sender, e);
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1Click != null)
                button1Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2Click != null)
                button2Click(sender, e);
        }


        private void btnLast_Click(object sender, EventArgs e)
        {
            if (btnLastClick != null)
                btnLastClick(sender, e);
        }

        private void txtIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtSubIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtMultiplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, true);
        }

        private void txtConstant_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, true);
        }

        private void txtENMReportingIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtENMMultiplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, true);
        }

        private void txtENMConstant_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, true);
        }

        private void txtENMDeadBand_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, true);
        }

        private void pbENMHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbENMHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpENMap, sender, e);
        }

        private void lblENMHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblENMHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpENMap, sender, e);
        }

        private void ucENlist_Load(object sender, EventArgs e)
        {

        }

        private void btnPrevClick_Click(object sender, EventArgs e)
        {

        }

        private void txtAutoMapNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtENAutoMapNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void grpEN_Enter(object sender, EventArgs e)
        {

        }

        private void lblAutomapNumber_Click(object sender, EventArgs e)
        {

        }
      
        private void cmbIEDName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIEDNameSelectedIndexChanged != null)
                cmbIEDNameSelectedIndexChanged(sender, e);
        }

        private void cmb61850ResponseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb61850ResponseTypeSelectedIndexChanged != null)
                cmb61850ResponseTypeSelectedIndexChanged(sender, e);
        }

        private void cmb61850Index_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb61850IndexSelectedIndexChanged != null)
                cmb61850IndexSelectedIndexChanged(sender, e);
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SpecialCharacter_Validation(e);
        }

        private void txtMapDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SpecialCharacter_Validation(e);
        }
    }
}
