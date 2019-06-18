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
    public partial class ucPLU : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler lvPLUDoubleClick;
        public event EventHandler lvMODBUSmasterDoubleClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event DrawListViewItemEventHandler lvMODBUSmasterDrawItem; 
        public event DrawListViewColumnHeaderEventHandler lvMODBUSmasterDrawColumnHeader;//lvMODBUSmasterDrawSubItem
        public event DrawListViewSubItemEventHandler lvMODBUSmasterDrawSubItem;
        public ucPLU()
        {
            InitializeComponent();
        }
        private void lvMODBUSmaster_DoubleClick(object sender, EventArgs e)
        {
            if (lvMODBUSmasterDoubleClick != null)
                lvMODBUSmasterDoubleClick(sender, e);
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
        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void lvMODBUSmaster_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpPLU, sender, e);
        }
        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpPLU, sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lvMODBUSmaster_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            //e.DrawText();
            if (lvMODBUSmasterDrawItem != null)
                lvMODBUSmasterDrawItem(sender, e);
        }

        private void lvMODBUSmaster_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
          if(lvMODBUSmasterDrawColumnHeader != null)
                lvMODBUSmasterDrawColumnHeader(sender, e);
        }

        private void lvMODBUSmaster_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (lvMODBUSmasterDrawSubItem != null)
                lvMODBUSmasterDrawSubItem(sender, e);
        }

        private void ucPLU_Load(object sender, EventArgs e)
        {

        }

        private void lvPLU_DoubleClick(object sender, EventArgs e)
        {
            if (lvPLUDoubleClick != null)
                lvPLUDoubleClick(sender, e);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {

        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void btnLast_Click(object sender, EventArgs e)
        {

        }
    }
}
