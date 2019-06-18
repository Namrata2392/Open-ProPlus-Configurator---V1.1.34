namespace OpenProPlusConfigurator
{
    partial class ucSlaveConfiguration
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
            this.lblSC = new System.Windows.Forms.Label();
            this.lvSlaveConfiguration = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSC
            // 
            this.lblSC.AutoSize = true;
            this.lblSC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSC.Location = new System.Drawing.Point(2, 0);
            this.lblSC.Name = "lblSC";
            this.lblSC.Size = new System.Drawing.Size(136, 15);
            this.lblSC.TabIndex = 13;
            this.lblSC.Text = "Slave Configuration:";
            // 
            // lvSlaveConfiguration
            // 
            this.lvSlaveConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvSlaveConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSlaveConfiguration.FullRowSelect = true;
            this.lvSlaveConfiguration.Location = new System.Drawing.Point(0, 30);
            this.lvSlaveConfiguration.MultiSelect = false;
            this.lvSlaveConfiguration.Name = "lvSlaveConfiguration";
            this.lvSlaveConfiguration.Size = new System.Drawing.Size(846, 346);
            this.lvSlaveConfiguration.TabIndex = 12;
            this.lvSlaveConfiguration.UseCompatibleStateImageBehavior = false;
            this.lvSlaveConfiguration.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(846, 30);
            this.panel1.TabIndex = 14;
            // 
            // ucSlaveConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvSlaveConfiguration);
            this.Controls.Add(this.panel1);
            this.Name = "ucSlaveConfiguration";
            this.Size = new System.Drawing.Size(846, 376);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSC;
        public System.Windows.Forms.ListView lvSlaveConfiguration;
        private System.Windows.Forms.Panel panel1;
    }
}
