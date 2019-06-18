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
    * \brief     <b>ucDOlist</b> is a user interface to display all DO's and there corresponding mapping infos.
    * \details   This is a user interface to display all DO's and there corresponding mapping's for various slaves. It provides interface
    * to add multiple DO's. The user can map this DO's to various slaves. The user can also modify mapping parameters.
    * 
    * 
    */
    public partial class ucDOlist : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event EventHandler lvDOlistDoubleClick;
        public event ListViewItemSelectionChangedEventHandler lvDOlistItemSelectionChanged;
        public event ListViewItemSelectionChangedEventHandler lvDOMapItemSelectionChanged;
        //public event EventHandler btnMapClick;
        public event EventHandler btnDOMDeleteClick;
        public event EventHandler btnDOMDoneClick;
        public event EventHandler btnDOMCancelClick;
        public event EventHandler lvDOMapDoubleClick; 
        public event EventHandler button2Click;
        public event EventHandler button1Click;
        public event EventHandler LinkDeleteConfigueClick; 
        public event EventHandler linkDoMapClick;
       
        public event EventHandler lvDOMapSelectedIndexChanged;
        public event EventHandler lvDOlistSelectedIndexChanged;
        public event EventHandler cmbIEDNameSelectedIndexChanged;
        public event EventHandler cmb61850DIIndexSelectedIndexChanged;
        public event EventHandler cmb61850DIResponseTypeSelectedIndexChanged;
        public ucDOlist()
        {
            InitializeComponent();
           // Utils.createPBTitleBar(pbHdr, lblHdrText, this.PointToScreen(lblHdrText.Location));
            //Utils.createPBTitleBar(pbDOMHdr, lblDOMHdrText, this.PointToScreen(lblDOMHdrText.Location));
            txtDONo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtDOMNo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            cmbDOMCommandType.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
        }
        private void lvDOlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDOlistSelectedIndexChanged != null)
                lvDOlistSelectedIndexChanged(sender, e);
        }
        private void cmbIEDName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIEDNameSelectedIndexChanged != null)
                cmbIEDNameSelectedIndexChanged(sender, e);
        }
        private void cmb61850DIIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb61850DIIndexSelectedIndexChanged != null)
                cmb61850DIIndexSelectedIndexChanged(sender, e);
        }
        private void lvDOMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDOMapSelectedIndexChanged != null)
                lvDOMapSelectedIndexChanged(sender, e);
        }
        private void cmb61850DIResponseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb61850DIResponseTypeSelectedIndexChanged != null)
                cmb61850DIResponseTypeSelectedIndexChanged(sender, e);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAddClick != null)
                btnAddClick(sender, e);
        }

        private void LinkDeleteConfigue_Click(object sender, EventArgs e)
        {
              if (LinkDeleteConfigueClick != null)
                LinkDeleteConfigueClick(sender, e);
        }
        private void linkDoMap_Click(object sender, EventArgs e)
        {
            if (linkDoMapClick != null)
                linkDoMapClick(sender, e);
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

        private void lvDOlist_DoubleClick(object sender, EventArgs e)
        {
            if (lvDOlistDoubleClick != null)
                lvDOlistDoubleClick(sender, e);
        }

        private void lvDOlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvDOlistItemSelectionChanged != null)
                lvDOlistItemSelectionChanged(sender, e);
            Console.WriteLine("***** lvDOlist ItemSelectionChanged!!! index: {0}", e.ItemIndex);
        }

        private void lvDOMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvDOMapItemSelectionChanged != null)
                lvDOMapItemSelectionChanged(sender, e);
            Console.WriteLine("***** lvDOMap ItemSelectionChanged!!! index: {0}", e.ItemIndex);
        }
        /*
        private void btnMap_Click(object sender, EventArgs e)
        {
            if (btnMapClick != null)
                btnMapClick(sender, e);
        }
         */

        private void lvDOMap_DoubleClick(object sender, EventArgs e)
        {
            if (lvDOMapDoubleClick != null)
                lvDOMapDoubleClick(sender, e);
        }

        private void btnDOMDone_Click(object sender, EventArgs e)
        {
            if (btnDOMDoneClick != null)
                btnDOMDoneClick(sender, e);
        }

        private void btnDOMCancel_Click(object sender, EventArgs e)
        {
            if (btnDOMCancelClick != null)
                btnDOMCancelClick(sender, e);
        }

        private void btnDOMDelete_Click(object sender, EventArgs e)
        {
            if (btnDOMDeleteClick != null)
                btnDOMDeleteClick(sender, e);
        }

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDO, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDO, sender, e);
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

        private void txtIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtSubIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtPulseDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtDOMReportingIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtDOMBitPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void pbDOMHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbDOMHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDOMap, sender, e);
        }

        private void lblDOMHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblDOMHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDOMap, sender, e);
        }

        private void ucDOlist_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

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

        private void lblAutoMapNumber_Click(object sender, EventArgs e)
        {

        }

        private void txtAutoMapNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ucDOlist_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtDONo_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

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
