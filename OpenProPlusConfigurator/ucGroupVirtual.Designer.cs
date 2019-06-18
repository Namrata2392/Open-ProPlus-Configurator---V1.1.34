namespace OpenProPlusConfigurator
{
    partial class ucGroupVirtual
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
            this.lvVirtualMaster = new System.Windows.Forms.ListView();
            this.lblVMC = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvVirtualMaster
            // 
            this.lvVirtualMaster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvVirtualMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvVirtualMaster.FullRowSelect = true;
            this.lvVirtualMaster.Location = new System.Drawing.Point(0, 30);
            this.lvVirtualMaster.MultiSelect = false;
            this.lvVirtualMaster.Name = "lvVirtualMaster";
            this.lvVirtualMaster.Size = new System.Drawing.Size(620, 386);
            this.lvVirtualMaster.TabIndex = 11;
            this.lvVirtualMaster.UseCompatibleStateImageBehavior = false;
            this.lvVirtualMaster.View = System.Windows.Forms.View.Details;
            // 
            // lblVMC
            // 
            this.lblVMC.AutoSize = true;
            this.lblVMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVMC.Location = new System.Drawing.Point(2, 0);
            this.lblVMC.Name = "lblVMC";
            this.lblVMC.Size = new System.Drawing.Size(190, 15);
            this.lblVMC.TabIndex = 10;
            this.lblVMC.Text = "Virtual Master Configuration:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblVMC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 30);
            this.panel1.TabIndex = 12;
            // 
            // ucGroupVirtual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvVirtualMaster);
            this.Controls.Add(this.panel1);
            this.Name = "ucGroupVirtual";
            this.Size = new System.Drawing.Size(620, 416);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lvVirtualMaster;
        private System.Windows.Forms.Label lblVMC;
        private System.Windows.Forms.Panel panel1;
    }
}
