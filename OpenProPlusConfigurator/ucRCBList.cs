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
    public partial class ucRCBList : UserControl
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
        public event EventHandler ucRCBListLoad;
        public event EventHandler btnRefreshClick;
        public event EventHandler checkedListBox1Click;
        public event KeyEventHandler comboBox1KeyUp;
        public event CancelEventHandler comboBox1Validating;
        public ucRCBList()
        {
            InitializeComponent();
        }
        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox1Validating != null)
                comboBox1Validating(sender, e);
        }
        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (comboBox1KeyUp != null)
                comboBox1KeyUp(sender, e);
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (btnRefreshClick != null)
                btnRefreshClick(sender, e);
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

        private void lvAIlist_DoubleClick(object sender, EventArgs e)
        {
            if (lvAIlistDoubleClick != null)
                lvAIlistDoubleClick(sender, e);
        }

        public void ucRCBList_Load(object sender, EventArgs e)
        {
            if (ucRCBListLoad != null)
                ucRCBListLoad(sender, e);            
        }

        private void checkedListBox1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1Click != null)
                checkedListBox1Click(sender, e);
        }

        private void lvAIlist_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
        }
        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpAO, sender, e);
        }
        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }
        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpAO, sender, e);
        }
    }
}
