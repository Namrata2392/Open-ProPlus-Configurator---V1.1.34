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
    * \brief     <b>ucSystemConfig</b> is a user interface to display system configurations
    * \details   This is a user interface to display system related configurations. The user can only modify it's parameters.
    */
    public partial class ucSystemConfig : UserControl
    {
        public event EventHandler cmbTimeSyncSourceSelectedIndexChanged;
        public event EventHandler cmbRedundancyModeSelectedIndexChanged;
        public event EventHandler cmbRedundancyModeSelectedValueChanged;
        public event EventHandler CmbTimeZoneTextChanged;
        public event EventHandler CmbTimeZoneSelectedValueChanged;
        public event EventHandler CmbTimeZoneTextUpdate;
        public event EventHandler CmbTimeZoneValidated;
        public event System.ComponentModel.CancelEventHandler CmbTimeZoneValidating;
        public ucSystemConfig()
        {
            InitializeComponent();
            txtLogServerPort.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            cmbLogProtocol.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtMaxDataPoints.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
        }

        private void cmbTimeSyncSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTimeSyncSourceSelectedIndexChanged != null)
                cmbTimeSyncSourceSelectedIndexChanged(sender, e);
        }
        private void txtNTPServer1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowIP(sender, e);
        }
        private void txtNTPServer2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowIP(sender, e);
        }
        private void txtLogServerIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowIP(sender, e);
        }
        private void txtRedundantSystemIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowIP(sender, e);
        }
        private void ucSystemConfig_Load(object sender, EventArgs e)
        {

        }
        private void cmbRedundancyMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRedundancyModeSelectedIndexChanged != null)
                cmbRedundancyModeSelectedIndexChanged(sender, e);
        }
        private void cmbRedundancyMode_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbRedundancyModeSelectedValueChanged != null)
                cmbRedundancyModeSelectedValueChanged(sender, e);
        }
        private void CmbTimeZone_TextChanged(object sender, EventArgs e)
        {
            if (CmbTimeZoneTextChanged != null)
                CmbTimeZoneTextChanged(sender, e);
        }
        private void CmbTimeZone_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbTimeZoneSelectedValueChanged != null)
                CmbTimeZoneSelectedValueChanged(sender, e);
        }
        private void CmbTimeZone_TextUpdate(object sender, EventArgs e)
        {
            if (CmbTimeZoneTextUpdate != null)
                CmbTimeZoneTextUpdate(sender, e);
        }
        private void CmbTimeZone_Validated(object sender, EventArgs e)
        {
            if (CmbTimeZoneValidated != null)
                CmbTimeZoneValidated(sender, e);
        }
        private void CmbTimeZone_Validating(object sender, CancelEventArgs e)
        {
            if (CmbTimeZoneValidating != null)
                CmbTimeZoneValidating(sender, e);
        }
    }
}
