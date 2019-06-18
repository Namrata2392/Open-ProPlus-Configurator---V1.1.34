namespace OpenProPlusConfigurator
{
    partial class frmAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.label2 = new System.Windows.Forms.Label();
            this.lblBuilddate = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblASCSubstation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(412, 58);
            this.label2.TabIndex = 25;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // lblBuilddate
            // 
            this.lblBuilddate.AutoSize = true;
            this.lblBuilddate.Location = new System.Drawing.Point(11, 87);
            this.lblBuilddate.Name = "lblBuilddate";
            this.lblBuilddate.Size = new System.Drawing.Size(62, 13);
            this.lblBuilddate.TabIndex = 24;
            this.lblBuilddate.Text = "Build Date: ";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(11, 63);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 22;
            this.lblVersion.Text = "Version";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(334, 87);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblASCSubstation
            // 
            this.lblASCSubstation.AutoSize = true;
            this.lblASCSubstation.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblASCSubstation.Location = new System.Drawing.Point(9, 18);
            this.lblASCSubstation.Name = "lblASCSubstation";
            this.lblASCSubstation.Size = new System.Drawing.Size(271, 28);
            this.lblASCSubstation.TabIndex = 35;
            this.lblASCSubstation.Text = "OpenPro+ Configurator";
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 197);
            this.Controls.Add(this.lblASCSubstation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblBuilddate);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About...";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblBuilddate;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblASCSubstation;
    }
}
