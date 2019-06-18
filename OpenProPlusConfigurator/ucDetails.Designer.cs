namespace OpenProPlusConfigurator
{
    partial class ucDetails
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
            this.lblDet = new System.Windows.Forms.Label();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtSwVersion = new System.Windows.Forms.TextBox();
            this.txtHwVersion = new System.Windows.Forms.TextBox();
            this.txtDeviceType = new System.Windows.Forms.TextBox();
            this.txtXMLVersion = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblSwVersion = new System.Windows.Forms.Label();
            this.lblHwVersion = new System.Windows.Forms.Label();
            this.lblDeviceType = new System.Windows.Forms.Label();
            this.lblXMLVersion = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.lvDetails = new System.Windows.Forms.ListView();
            this.btnEdit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDet
            // 
            this.lblDet.AutoSize = true;
            this.lblDet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDet.Location = new System.Drawing.Point(2, 0);
            this.lblDet.Name = "lblDet";
            this.lblDet.Size = new System.Drawing.Size(103, 15);
            this.lblDet.TabIndex = 10;
            this.lblDet.Text = "Device Details:";
            // 
            // grpDetails
            // 
            this.grpDetails.BackColor = System.Drawing.SystemColors.Control;
            this.grpDetails.Controls.Add(this.txtDescription);
            this.grpDetails.Controls.Add(this.txtSwVersion);
            this.grpDetails.Controls.Add(this.txtHwVersion);
            this.grpDetails.Controls.Add(this.txtDeviceType);
            this.grpDetails.Controls.Add(this.txtXMLVersion);
            this.grpDetails.Controls.Add(this.lblDescription);
            this.grpDetails.Controls.Add(this.lblSwVersion);
            this.grpDetails.Controls.Add(this.lblHwVersion);
            this.grpDetails.Controls.Add(this.lblDeviceType);
            this.grpDetails.Controls.Add(this.lblXMLVersion);
            this.grpDetails.Controls.Add(this.lblHdrText);
            this.grpDetails.Controls.Add(this.pbHdr);
            this.grpDetails.Controls.Add(this.btnCancel);
            this.grpDetails.Controls.Add(this.btnDone);
            this.grpDetails.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpDetails.Location = new System.Drawing.Point(272, 106);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(273, 207);
            this.grpDetails.TabIndex = 25;
            this.grpDetails.TabStop = false;
            this.grpDetails.Visible = false;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(113, 134);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(140, 20);
            this.txtDescription.TabIndex = 116;
            this.txtDescription.Tag = "DeviceDescription";
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            // 
            // txtSwVersion
            // 
            this.txtSwVersion.Location = new System.Drawing.Point(113, 108);
            this.txtSwVersion.Name = "txtSwVersion";
            this.txtSwVersion.Size = new System.Drawing.Size(140, 20);
            this.txtSwVersion.TabIndex = 115;
            this.txtSwVersion.Tag = "SwVer";
            // 
            // txtHwVersion
            // 
            this.txtHwVersion.Location = new System.Drawing.Point(113, 82);
            this.txtHwVersion.Name = "txtHwVersion";
            this.txtHwVersion.Size = new System.Drawing.Size(140, 20);
            this.txtHwVersion.TabIndex = 114;
            this.txtHwVersion.Tag = "HwVersion";
            // 
            // txtDeviceType
            // 
            this.txtDeviceType.Location = new System.Drawing.Point(113, 56);
            this.txtDeviceType.Name = "txtDeviceType";
            this.txtDeviceType.Size = new System.Drawing.Size(140, 20);
            this.txtDeviceType.TabIndex = 113;
            this.txtDeviceType.Tag = "DeviceType";
            // 
            // txtXMLVersion
            // 
            this.txtXMLVersion.Location = new System.Drawing.Point(113, 30);
            this.txtXMLVersion.Name = "txtXMLVersion";
            this.txtXMLVersion.Size = new System.Drawing.Size(140, 20);
            this.txtXMLVersion.TabIndex = 112;
            this.txtXMLVersion.Tag = "XMLVersion";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 134);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(97, 13);
            this.lblDescription.TabIndex = 111;
            this.lblDescription.Text = "Device Description";
            // 
            // lblSwVersion
            // 
            this.lblSwVersion.AutoSize = true;
            this.lblSwVersion.Location = new System.Drawing.Point(12, 108);
            this.lblSwVersion.Name = "lblSwVersion";
            this.lblSwVersion.Size = new System.Drawing.Size(60, 13);
            this.lblSwVersion.TabIndex = 110;
            this.lblSwVersion.Text = "Sw Version";
            // 
            // lblHwVersion
            // 
            this.lblHwVersion.AutoSize = true;
            this.lblHwVersion.Location = new System.Drawing.Point(12, 82);
            this.lblHwVersion.Name = "lblHwVersion";
            this.lblHwVersion.Size = new System.Drawing.Size(61, 13);
            this.lblHwVersion.TabIndex = 109;
            this.lblHwVersion.Text = "Hw Version";
            this.lblHwVersion.Click += new System.EventHandler(this.lblHwVersion_Click);
            // 
            // lblDeviceType
            // 
            this.lblDeviceType.AutoSize = true;
            this.lblDeviceType.Location = new System.Drawing.Point(12, 56);
            this.lblDeviceType.Name = "lblDeviceType";
            this.lblDeviceType.Size = new System.Drawing.Size(68, 13);
            this.lblDeviceType.TabIndex = 108;
            this.lblDeviceType.Text = "Device Type";
            // 
            // lblXMLVersion
            // 
            this.lblXMLVersion.AutoSize = true;
            this.lblXMLVersion.Location = new System.Drawing.Point(12, 30);
            this.lblXMLVersion.Name = "lblXMLVersion";
            this.lblXMLVersion.Size = new System.Drawing.Size(67, 13);
            this.lblXMLVersion.TabIndex = 107;
            this.lblXMLVersion.Text = "XML Version";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(46, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "Details";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(276, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(185, 164);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 106;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(111, 164);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 105;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // lvDetails
            // 
            this.lvDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDetails.FullRowSelect = true;
            this.lvDetails.Location = new System.Drawing.Point(0, 30);
            this.lvDetails.MultiSelect = false;
            this.lvDetails.Name = "lvDetails";
            this.lvDetails.Size = new System.Drawing.Size(813, 377);
            this.lvDetails.TabIndex = 26;
            this.lvDetails.UseCompatibleStateImageBehavior = false;
            this.lvDetails.View = System.Windows.Forms.View.Details;
            this.lvDetails.SizeChanged += new System.EventHandler(this.lvDetails_SizeChanged);
            this.lvDetails.DoubleClick += new System.EventHandler(this.lvDetails_DoubleClick);
            // 
            // btnEdit
            // 
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.Location = new System.Drawing.Point(134, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(68, 27);
            this.btnEdit.TabIndex = 27;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDet);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(813, 30);
            this.panel1.TabIndex = 28;
            // 
            // ucDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.lvDetails);
            this.Controls.Add(this.panel1);
            this.Name = "ucDetails";
            this.Size = new System.Drawing.Size(813, 407);
            this.Load += new System.EventHandler(this.ucDetails_Load);
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        //FIXME: Expose as property, instead of making public...
        private System.Windows.Forms.Label lblDet;
        public System.Windows.Forms.GroupBox grpDetails;
        public System.Windows.Forms.TextBox txtDescription;
        public System.Windows.Forms.TextBox txtSwVersion;
        public System.Windows.Forms.TextBox txtHwVersion;
        public System.Windows.Forms.TextBox txtDeviceType;
        public System.Windows.Forms.TextBox txtXMLVersion;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.ListView lvDetails;
        public System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblDescription;
        public System.Windows.Forms.Label lblSwVersion;
        public System.Windows.Forms.Label lblHwVersion;
        public System.Windows.Forms.Label lblDeviceType;
        public System.Windows.Forms.Label lblXMLVersion;
    }
}
