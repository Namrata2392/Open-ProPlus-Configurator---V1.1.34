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
    public partial class ucAOList : UserControl
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
        //public event EventHandler btnMapClick;
        public event EventHandler btnAIMDeleteClick;
        public event EventHandler btnAIMDoneClick;
        public event EventHandler btnAIMCancelClick;
        public event EventHandler lvAIMapDoubleClick;
        public event EventHandler lvAIMapSelectedIndexChanged;
        public event EventHandler lvAIlistSelectedIndexChanged;
        public event EventHandler cmb61850IndexSelectedIndexChanged;
        public event EventHandler cmb61850ResponseTypeSelectedIndexChanged;
        public event EventHandler cmbIEDNameSelectedIndexChanged;
        public ucAOList()
        {
            InitializeComponent();
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

        private void btnAIMDelete_Click(object sender, EventArgs e)
        {
            if (btnAIMDeleteClick != null)
                btnAIMDeleteClick(sender, e);
        }

        private void btnAIMCancel_Click(object sender, EventArgs e)
        {
            if (btnAIMCancelClick != null)
                btnAIMCancelClick(sender, e);
        }

        private void btnAIMDone_Click(object sender, EventArgs e)
        {
            if (btnAIMDoneClick != null)
                btnAIMDoneClick(sender, e);
        }

        private void lvAIMap_DoubleClick(object sender, EventArgs e)
        {
            if (lvAIMapDoubleClick != null)
                lvAIMapDoubleClick(sender, e);
        }

        private void lvAIMap_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvAIMapItemSelectionChanged != null)
                lvAIMapItemSelectionChanged(sender, e);
            Console.WriteLine("***** lvAIlist ItemSelectionChanged!!! index: {0}", e.ItemIndex);
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
        private void lblAIMHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void lblAIMHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpAI, sender, e);
        }
        private void pbAIMHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void pbAIMHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpAIMap, sender, e);
        }
        private void lvAIlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAIlistSelectedIndexChanged != null)
                lvAIlistSelectedIndexChanged(sender, e);
        }
        private void lvAIMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAIMapSelectedIndexChanged != null)
                lvAIMapSelectedIndexChanged(sender, e);
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
