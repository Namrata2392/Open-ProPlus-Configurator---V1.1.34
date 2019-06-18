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
    * \brief     <b>ucMasterVirtual</b> is a user interface to display IED & Virtual master parameters
    * \details   This is a user interface to display IED & Virtual master parameters. It supports
    * only single IED.
    * 
    * 
    */
    public partial class ucMasterVirtual : UserControl
    {
        public ucMasterVirtual()
        {
            InitializeComponent();
            txtMasterNo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
        }
    }
}
