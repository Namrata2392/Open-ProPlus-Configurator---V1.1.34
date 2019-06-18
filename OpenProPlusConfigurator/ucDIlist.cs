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
    * \brief     <b>ucDIlist</b> is a user interface to display all DI's and there corresponding mapping infos.
    * \details   This is a user interface to display all DI's and there corresponding mapping's for various slaves. It provides interface
    * to add multiple DI's. The user can map this DI's to various slaves. The user can also modify mapping parameters.
    * 
    * 
    */
    public partial class ucDIlist : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event EventHandler lvDIlistDoubleClick;
        public event ListViewItemSelectionChangedEventHandler lvDIlistItemSelectionChanged;
        public event ListViewItemSelectionChangedEventHandler lvDIMapItemSelectionChanged;
        public event EventHandler btnDIMDeleteClick;
        public event EventHandler btnDIMDoneClick;
        public event EventHandler btnDIMCancelClick;
        public event EventHandler lvDIMapDoubleClick; 
        public event EventHandler button1Click; 
        public event EventHandler button2Click;
        public event EventHandler linkDOClick; 
        public event EventHandler linkLabel1Click;
        public event EventHandler ucDIlistLoad;
        public event EventHandler cmbIEDNameSelectedIndexChanged;
        public event EventHandler cmb61850DIResponseTypeSelectedIndexChanged;
        public event EventHandler cmb61850DIIndexSelectedIndexChanged;
        public event EventHandler CmbReportingIndexDropDown;
        public event EventHandler lvDIMapSelectedIndexChanged;
        public event EventHandler lvDIlistSelectedIndexChanged;
        public ucDIlist()
        {
            InitializeComponent();
            //Utils.createPBTitleBar(pbHdr, lblHdrText, this.PointToScreen(lblHdrText.Location));
            //Utils.createPBTitleBar(pbDIMHdr, lblDIMHdrText, this.PointToScreen(lblDIMHdrText.Location));
            txtDINo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtDIMNo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            cmbDIMCommandType.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            //flpMap2Slave.BackColor = ColorTranslator.FromHtml(Globals.rowColour); //System.Drawing.SystemColors.Window;

            txtDescription.MaxLength = Globals.MAX_DESCRIPTION_LEN;
        }

        private void lvDIlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDIlistSelectedIndexChanged != null)
                lvDIlistSelectedIndexChanged(sender, e);
        }
        private void lvDIMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDIMapSelectedIndexChanged != null)
                lvDIMapSelectedIndexChanged(sender, e);
        }
    
        public void ucDIlist_Load(object sender, EventArgs e)
        {
            if (ucDIlistLoad != null)
                ucDIlistLoad(sender, e);
        }
        private void CmbReportingIndex_DropDown(object sender, EventArgs e)
        {
            if (CmbReportingIndexDropDown != null)
                CmbReportingIndexDropDown(sender, e);
        }
        private void cmbIEDName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIEDNameSelectedIndexChanged != null)
                cmbIEDNameSelectedIndexChanged(sender, e);
        }
        private void cmb61850DIResponseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb61850DIResponseTypeSelectedIndexChanged != null)
                cmb61850DIResponseTypeSelectedIndexChanged(sender, e);
        }

        private void cmb61850DIIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb61850DIIndexSelectedIndexChanged != null)
                cmb61850DIIndexSelectedIndexChanged(sender, e);
        }
        private void linkDO_Click(object sender, EventArgs e)
        {
            if (linkDOClick != null)
                linkDOClick(sender, e);
        }
        private void linkLabel1_Click(object sender, EventArgs e)
        {
            if (linkLabel1Click != null)
                linkLabel1Click(sender, e);
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
        
        private void lvDIlist_DoubleClick(object sender, EventArgs e)
        {
            if (lvDIlistDoubleClick != null)
                lvDIlistDoubleClick(sender, e);
        }

        private void lvDIlist_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvDIlistItemSelectionChanged != null)
                lvDIlistItemSelectionChanged(sender, e);
            Console.WriteLine("***** lvDIlist ItemSelectionChanged!!! index: {0}", e.ItemIndex);
        }

        private void lvDIMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvDIMapItemSelectionChanged != null)
                lvDIMapItemSelectionChanged(sender, e);
            Console.WriteLine("***** lvDIMap ItemSelectionChanged!!! index: {0}", e.ItemIndex);
        }
        /*
        private void btnMap_Click(object sender, EventArgs e)
        {
            if (btnMapClick != null)
                btnMapClick(sender, e);
        }
         */

        private void lvDIMap_DoubleClick(object sender, EventArgs e)
        {
            if (lvDIMapDoubleClick != null)
                lvDIMapDoubleClick(sender, e);
        }

        private void btnDIMDone_Click(object sender, EventArgs e)
        {
            if (btnDIMDoneClick != null)
                btnDIMDoneClick(sender, e);
        }

        private void btnDIMCancel_Click(object sender, EventArgs e)
        {
            if (btnDIMCancelClick != null)
                btnDIMCancelClick(sender, e);
        }

        private void btnDIMDelete_Click(object sender, EventArgs e)
        {
            if (btnDIMDeleteClick != null)
                btnDIMDeleteClick(sender, e);
        }

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDI, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDI, sender, e);
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
        private void txtIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtSubIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtDIMReportingIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtDIMBitPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void pbDIMHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbDIMHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDIMap, sender, e);
        }

        private void lblDIMHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblDIMHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpDIMap, sender, e);
        }
        private void txtAutpMapNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
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
