using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace OpenProPlusConfigurator
{
    public partial class frmOpenPro_UI : Form
    {
        
        public frmOpenPro_UI()
        {
            InitializeComponent();
        }
        
        public List<NetworkInterface> NetworkIPLIst1 = new List<NetworkInterface>();
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://" + cmbIP.Text +"/");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            
        }
        private void frmOpenPro_UI_Load(object sender, EventArgs e)
        {
            cmbIP.Items.Clear();
            var myDistinctList = Utils.NetworkIPLIst.GroupBy(i => i.IP).Select(g => g.First()).ToList();
            foreach (var L in myDistinctList)
            {
                var UID = L.IP;
                {
                    cmbIP.Items.Add(L.IP);
                }
            }
        }

    }
}
