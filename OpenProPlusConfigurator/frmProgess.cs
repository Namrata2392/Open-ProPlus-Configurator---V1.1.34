using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>frmProgess</b> is the user interface to display progress message
    * \details   This is the user interface to display progress message.
    * 
    */
    public partial class frmProgess : Form
    {
        public frmProgess()
        {
            InitializeComponent();
        }

        private void frmProgess_Load(object sender, EventArgs e)
        {
            Utils.SetFormIcon(this);
        }
    }
}
