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
    * \brief     <b>ucIED</b> is a user interface to display count of all data points
    * \details   This is a user interface to display count of all data points. It also
    * displays the IED parameters like Retries, Remote IP, TCP port, etc depending on 
    * the master type it belongs to.
    * 
    * 
    */
    public partial class ucIED : UserControl
    {
        public ucIED()
        {
            InitializeComponent();
            txtUnitID.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtASDUaddress.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtDevice.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtRemoteIP.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtTCPPort.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtRetries.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtTimeOut.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtDescription.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
        }

        private void ucIED_Load(object sender, EventArgs e)
        {

        }
    }
}
