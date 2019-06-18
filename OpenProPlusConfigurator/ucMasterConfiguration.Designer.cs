namespace OpenProPlusConfigurator
{
    partial class ucMasterConfiguration
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvMasterConfiguration = new System.Windows.Forms.ListView();
            this.lblMC = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMasterConfiguration
            // 
            this.lvMasterConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvMasterConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMasterConfiguration.FullRowSelect = true;
            this.lvMasterConfiguration.Location = new System.Drawing.Point(0, 30);
            this.lvMasterConfiguration.MultiSelect = false;
            this.lvMasterConfiguration.Name = "lvMasterConfiguration";
            this.lvMasterConfiguration.Size = new System.Drawing.Size(1029, 657);
            this.lvMasterConfiguration.TabIndex = 10;
            this.lvMasterConfiguration.UseCompatibleStateImageBehavior = false;
            this.lvMasterConfiguration.View = System.Windows.Forms.View.Details;
            // 
            // lblMC
            // 
            this.lblMC.AutoSize = true;
            this.lblMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMC.Location = new System.Drawing.Point(2, 0);
            this.lblMC.Name = "lblMC";
            this.lblMC.Size = new System.Drawing.Size(145, 15);
            this.lblMC.TabIndex = 11;
            this.lblMC.Text = "Master Configuration:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1029, 30);
            this.panel1.TabIndex = 12;
            // 
            // ucMasterConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvMasterConfiguration);
            this.Controls.Add(this.panel1);
            this.Name = "ucMasterConfiguration";
            this.Size = new System.Drawing.Size(1029, 687);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lvMasterConfiguration;
        private System.Windows.Forms.Label lblMC;
        private System.Windows.Forms.Panel panel1;
    }
}
