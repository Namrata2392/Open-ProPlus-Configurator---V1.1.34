namespace OpenProPlusConfigurator
{
    partial class ucSystemConfiguration
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
            this.lvSConfiguration = new System.Windows.Forms.ListView();
            this.lblSC = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvSConfiguration
            // 
            this.lvSConfiguration.BackgroundImageTiled = true;
            this.lvSConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvSConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSConfiguration.FullRowSelect = true;
            this.lvSConfiguration.Location = new System.Drawing.Point(0, 29);
            this.lvSConfiguration.MultiSelect = false;
            this.lvSConfiguration.Name = "lvSConfiguration";
            this.lvSConfiguration.Size = new System.Drawing.Size(649, 315);
            this.lvSConfiguration.TabIndex = 3;
            this.lvSConfiguration.UseCompatibleStateImageBehavior = false;
            this.lvSConfiguration.View = System.Windows.Forms.View.Details;
            // 
            // lblSC
            // 
            this.lblSC.AutoSize = true;
            this.lblSC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSC.Location = new System.Drawing.Point(2, 0);
            this.lblSC.Name = "lblSC";
            this.lblSC.Size = new System.Drawing.Size(147, 15);
            this.lblSC.TabIndex = 2;
            this.lblSC.Text = "System Configuration:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(649, 29);
            this.panel1.TabIndex = 4;
            // 
            // ucSystemConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvSConfiguration);
            this.Controls.Add(this.panel1);
            this.Name = "ucSystemConfiguration";
            this.Size = new System.Drawing.Size(649, 344);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lvSConfiguration;
        private System.Windows.Forms.Label lblSC;
        private System.Windows.Forms.Panel panel1;
    }
}
