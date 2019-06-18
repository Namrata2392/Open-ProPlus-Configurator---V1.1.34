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
    * \brief     <b>ucAIlist</b> is a user interface to display all AI's and there corresponding mapping infos.
    * \details   This is a user interface to display all AI's and there corresponding mapping's for various slaves. It provides interface
    * to add multiple AI's. The user can map this AI's to various slaves. The user can also modify mapping parameters.
    * 
    * 
    */
    public partial class ucAIlist : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event EventHandler lvAIlistDoubleClick;
        public event ListViewItemSelectionChangedEventHandler lvAIlistItemSelectionChanged;
        public event ListViewItemSelectionChangedEventHandler lvAIMapItemSelectionChanged;
       
        public event EventHandler cmbIEDnoSelectedIndexChanged;
        //public event EventHandler btnMapClick;
        public event EventHandler btnAIMDeleteClick;
        public event EventHandler btnAIMDoneClick;
        public event EventHandler btnAIMCancelClick;
        public event EventHandler lvAIMapDoubleClick;
       
        public event EventHandler button1Click; 
        public event EventHandler button3Click; 
        public event EventHandler linkLabel1Click;
        public event EventHandler LinkDeleteConfigueClick;
        public event EventHandler cmb61850IndexSelectedIndexChanged;
        public event EventHandler cmb61850ResponseTypeSelectedIndexChanged;
        public event EventHandler cmbIEDNameSelectedIndexChanged;
        public event EventHandler ucAIlistLoad;
        public event EventHandler cmbDataTypeSelectedIndexChanged;
        public event EventHandler cmbAIMDataTypeSelectedIndexChanged;
        public event DrawListViewItemEventHandler lvAIlistDrawItem;
        public event EventHandler lvAIMapSelectedIndexChanged;
        public event EventHandler lvAIlistSelectedIndexChanged;
        public ucAIlist()
        {
            InitializeComponent();
            //Utils.createPBTitleBar(pbHdr, lblHdrText, this.PointToScreen(lblHdrText.Location));
            //Utils.createPBTitleBar(pbAIMHdr, lblAIMHdrText, this.PointToScreen(lblAIMHdrText.Location));
            txtAINo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtAIMNo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            cmbAIMCommandType.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...

            txtDescription.MaxLength = Globals.MAX_DESCRIPTION_LEN;
        }
        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDataTypeSelectedIndexChanged != null)
                cmbDataTypeSelectedIndexChanged(sender, e);
        }

        public void btnAdd_Click(object sender, EventArgs e)
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

        private void lvAIlist_DoubleClick(object sender, EventArgs e)
        {
            if (lvAIlistDoubleClick != null)
                lvAIlistDoubleClick(sender, e);
        }

        private void lvAIlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvAIlistItemSelectionChanged != null)
                lvAIlistItemSelectionChanged(sender, e);
            Console.WriteLine("***** lvAIlist ItemSelectionChanged!!! index: {0}", e.ItemIndex);
        }
        private void lvAIMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAIMapSelectedIndexChanged != null)
                lvAIMapSelectedIndexChanged(sender, e);
        }
        private void lvAIlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAIlistSelectedIndexChanged != null)
                lvAIlistSelectedIndexChanged(sender, e);
            //Console.WriteLine("***** lvAIMap ItemSelectionChanged!!! index: {0}", e.ItemIndex);
        }
        //Namrata:10/6/2017
        private void lvAIMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvAIMapItemSelectionChanged != null)
                lvAIMapItemSelectionChanged(sender, e);
            Console.WriteLine("***** lvAIMap ItemSelectionChanged!!! index: {0}", e.ItemIndex);

        }
        private void cmbIEDno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIEDnoSelectedIndexChanged != null)
                cmbIEDnoSelectedIndexChanged(sender, e);
        }
        private void cmbAIMDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAIMDataTypeSelectedIndexChanged != null)
                cmbAIMDataTypeSelectedIndexChanged(sender, e);
        }
        private void cmb61850Index_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb61850IndexSelectedIndexChanged != null)
                cmb61850IndexSelectedIndexChanged(sender, e);
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
        private void linkLabel1_Click(object sender, EventArgs e)
        {
            if (linkLabel1Click != null)
                linkLabel1Click(sender, e);
        }

        private void LinkDeleteConfigue_Click(object sender, EventArgs e)
        {
            if (LinkDeleteConfigueClick != null)
                LinkDeleteConfigueClick(sender, e);
        }
        private void lvAIMap_DoubleClick(object sender, EventArgs e)
        {
            if (lvAIMapDoubleClick != null)
                lvAIMapDoubleClick(sender, e);
        }

        private void btnAIMDone_Click(object sender, EventArgs e)
        {
            if (btnAIMDoneClick != null)
                btnAIMDoneClick(sender, e);
        }

        private void btnAIMCancel_Click(object sender, EventArgs e)
        {
            if (btnAIMCancelClick != null)
                btnAIMCancelClick(sender, e);
        }

        private void btnAIMDelete_Click(object sender, EventArgs e)
        {
            if (btnAIMDeleteClick != null)
                btnAIMDeleteClick(sender, e);
        }

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpAI, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpAI, sender, e);
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

        private void txtAIMReportingIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtAIMMultiplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, true);
        }

        private void txtAIMConstant_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, true);
        }

        private void txtAIMDeadBand_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, true);
        }

        private void pbAIMHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbAIMHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpAIMap, sender, e);
        }

        private void lblAIMHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblAIMHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpAIMap, sender, e);
        }

        public void ucAIlist_Load(object sender, EventArgs e)
        {
            if (ucAIlistLoad != null)
                ucAIlistLoad(sender, e);
        }
        private void cmbMasterType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (btnPrevClick != null)
                btnPrevClick(sender, e);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (btnFirstClick != null)
                btnFirstClick(sender, e);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (btnFirstClick != null)
                btnFirstClick(sender, e);
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

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            //if (btnDeleteAllClick != null)
            //    btnDeleteAllClick(sender, e);
        }

       
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void LinkDeleteConfigue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void txtAIAutoMapRange_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblHdrText_Click(object sender, EventArgs e)
        {

        }

        private void txtAIAutoMapRange_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }
        public event ItemCheckEventHandler lvAIlistItemCheck;
        private void lvAIlist_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvAIlistItemCheck != null)
                lvAIlistItemCheck(sender, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lvAIlist_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (lvAIlistDrawItem != null)
                lvAIlistDrawItem(sender, e);
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
