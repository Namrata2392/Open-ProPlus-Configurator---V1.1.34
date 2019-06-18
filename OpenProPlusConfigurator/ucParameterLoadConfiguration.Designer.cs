namespace OpenProPlusConfigurator
{
    partial class ucParameterLoadConfiguration
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
            this.lblPLC = new System.Windows.Forms.Label();
            this.lvParameterLoadConfiguration = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPLC
            // 
            this.lblPLC.AutoSize = true;
            this.lblPLC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPLC.Location = new System.Drawing.Point(2, 0);
            this.lblPLC.Name = "lblPLC";
            this.lblPLC.Size = new System.Drawing.Size(200, 15);
            this.lblPLC.TabIndex = 13;
            this.lblPLC.Text = "Parameter Load Configuration";
            // 
            // lvParameterLoadConfiguration
            // 
            this.lvParameterLoadConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvParameterLoadConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvParameterLoadConfiguration.FullRowSelect = true;
            this.lvParameterLoadConfiguration.Location = new System.Drawing.Point(0, 30);
            this.lvParameterLoadConfiguration.MultiSelect = false;
            this.lvParameterLoadConfiguration.Name = "lvParameterLoadConfiguration";
            this.lvParameterLoadConfiguration.Size = new System.Drawing.Size(781, 350);
            this.lvParameterLoadConfiguration.TabIndex = 12;
            this.lvParameterLoadConfiguration.UseCompatibleStateImageBehavior = false;
            this.lvParameterLoadConfiguration.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblPLC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(781, 30);
            this.panel1.TabIndex = 14;
            // 
            // ucParameterLoadConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvParameterLoadConfiguration);
            this.Controls.Add(this.panel1);
            this.Name = "ucParameterLoadConfiguration";
            this.Size = new System.Drawing.Size(781, 380);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPLC;
        public System.Windows.Forms.ListView lvParameterLoadConfiguration;
        private System.Windows.Forms.Panel panel1;
    }
}
