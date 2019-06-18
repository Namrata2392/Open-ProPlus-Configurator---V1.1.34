using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.IO;

namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>frmAbout</b> is a user interface to display product details
    * \details   This is a user interface to display product details. It displays 
    * information like Copyright, support URL's, product version, etc.
    * 
    * 
    */
    partial class frmAbout : Form
    {
        public frmAbout()
        {
            //Namrata:30/6/2017
            InitializeComponent();

            //this.lblVersion.Text = String.Format("Version {0}", Utils.AssemblyVersion);
            //Ajay: 02/07/2018 
            //this.lblVersion.Text = String.Format("Version {0}", Globals.version);
            this.lblVersion.Text = "Version " + Utils.AssemblyVersion;
            this.lblBuilddate.Text = "Date: " + Utils.BuildDate;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {

        }
    }
}
