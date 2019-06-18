namespace OpenProPlusConfigurator
{
    partial class ucNetworkConfiguration
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
            this.label1 = new System.Windows.Forms.Label();
            this.lvNPorts = new System.Windows.Forms.ListView();
            this.grpNI = new System.Windows.Forms.GroupBox();
            this.txtVirtualIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.lblPrimaryDevice = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.cmbAddressType = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.txtPortName = new System.Windows.Forms.TextBox();
            this.lblPN = new System.Windows.Forms.Label();
            this.txtPortNo = new System.Windows.Forms.TextBox();
            this.lblPortNo = new System.Windows.Forms.Label();
            this.txtGateway = new System.Windows.Forms.TextBox();
            this.lblGW = new System.Windows.Forms.Label();
            this.txtSubnetMask = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblSM = new System.Windows.Forms.Label();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.cmbConnectionType = new System.Windows.Forms.ComboBox();
            this.lblCT = new System.Windows.Forms.Label();
            this.lblAT = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpNI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Network Configuration:";
            // 
            // lvNPorts
            // 
            this.lvNPorts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvNPorts.CheckBoxes = true;
            this.lvNPorts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvNPorts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvNPorts.FullRowSelect = true;
            this.lvNPorts.Location = new System.Drawing.Point(0, 30);
            this.lvNPorts.MultiSelect = false;
            this.lvNPorts.Name = "lvNPorts";
            this.lvNPorts.Size = new System.Drawing.Size(1050, 670);
            this.lvNPorts.TabIndex = 1;
            this.lvNPorts.UseCompatibleStateImageBehavior = false;
            this.lvNPorts.View = System.Windows.Forms.View.Details;
            this.lvNPorts.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvNPorts_DrawColumnHeader);
            this.lvNPorts.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvNPorts_ItemCheck);
            this.lvNPorts.SelectedIndexChanged += new System.EventHandler(this.lvNPorts_SelectedIndexChanged);
            this.lvNPorts.DoubleClick += new System.EventHandler(this.lvNPorts_DoubleClick);
            // 
            // grpNI
            // 
            this.grpNI.BackColor = System.Drawing.SystemColors.Control;
            this.grpNI.Controls.Add(this.txtVirtualIP);
            this.grpNI.Controls.Add(this.label2);
            this.grpNI.Controls.Add(this.cmbPortName);
            this.grpNI.Controls.Add(this.lblPrimaryDevice);
            this.grpNI.Controls.Add(this.btnLast);
            this.grpNI.Controls.Add(this.btnNext);
            this.grpNI.Controls.Add(this.btnPrev);
            this.grpNI.Controls.Add(this.btnFirst);
            this.grpNI.Controls.Add(this.lblHdrText);
            this.grpNI.Controls.Add(this.pbHdr);
            this.grpNI.Controls.Add(this.cmbAddressType);
            this.grpNI.Controls.Add(this.btnCancel);
            this.grpNI.Controls.Add(this.btnDone);
            this.grpNI.Controls.Add(this.txtPortName);
            this.grpNI.Controls.Add(this.lblPN);
            this.grpNI.Controls.Add(this.txtPortNo);
            this.grpNI.Controls.Add(this.lblPortNo);
            this.grpNI.Controls.Add(this.txtGateway);
            this.grpNI.Controls.Add(this.lblGW);
            this.grpNI.Controls.Add(this.txtSubnetMask);
            this.grpNI.Controls.Add(this.txtIP);
            this.grpNI.Controls.Add(this.lblIP);
            this.grpNI.Controls.Add(this.lblSM);
            this.grpNI.Controls.Add(this.chkEnable);
            this.grpNI.Controls.Add(this.cmbConnectionType);
            this.grpNI.Controls.Add(this.lblCT);
            this.grpNI.Controls.Add(this.lblAT);
            this.grpNI.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpNI.Location = new System.Drawing.Point(315, 128);
            this.grpNI.Name = "grpNI";
            this.grpNI.Size = new System.Drawing.Size(299, 319);
            this.grpNI.TabIndex = 0;
            this.grpNI.TabStop = false;
            this.grpNI.Visible = false;
            // 
            // txtVirtualIP
            // 
            this.txtVirtualIP.Location = new System.Drawing.Point(114, 154);
            this.txtVirtualIP.Name = "txtVirtualIP";
            this.txtVirtualIP.Size = new System.Drawing.Size(160, 20);
            this.txtVirtualIP.TabIndex = 41;
            this.txtVirtualIP.Tag = "VirtualIP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Virtual IP Address";
            // 
            // cmbPortName
            // 
            this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Location = new System.Drawing.Point(114, 229);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(160, 21);
            this.cmbPortName.TabIndex = 16;
            this.cmbPortName.Tag = "PrimaryDevice";
            // 
            // lblPrimaryDevice
            // 
            this.lblPrimaryDevice.AutoSize = true;
            this.lblPrimaryDevice.Location = new System.Drawing.Point(12, 232);
            this.lblPrimaryDevice.Name = "lblPrimaryDevice";
            this.lblPrimaryDevice.Size = new System.Drawing.Size(78, 13);
            this.lblPrimaryDevice.TabIndex = 15;
            this.lblPrimaryDevice.Text = "Primary Device";
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(213, 290);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(59, 22);
            this.btnLast.TabIndex = 23;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(146, 290);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(59, 22);
            this.btnNext.TabIndex = 22;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(79, 290);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(59, 22);
            this.btnPrev.TabIndex = 21;
            this.btnPrev.Text = "<Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnFirst.Location = new System.Drawing.Point(12, 290);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 20;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(109, 13);
            this.lblHdrText.TabIndex = 0;
            this.lblHdrText.Text = "Network Interface";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(299, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // cmbAddressType
            // 
            this.cmbAddressType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAddressType.FormattingEnabled = true;
            this.cmbAddressType.Location = new System.Drawing.Point(114, 103);
            this.cmbAddressType.Name = "cmbAddressType";
            this.cmbAddressType.Size = new System.Drawing.Size(160, 21);
            this.cmbAddressType.TabIndex = 8;
            this.cmbAddressType.Tag = "AddressType";
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(206, 257);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(114, 257);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 18;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // txtPortName
            // 
            this.txtPortName.Enabled = false;
            this.txtPortName.Location = new System.Drawing.Point(114, 52);
            this.txtPortName.Name = "txtPortName";
            this.txtPortName.Size = new System.Drawing.Size(160, 20);
            this.txtPortName.TabIndex = 4;
            this.txtPortName.Tag = "PortName";
            // 
            // lblPN
            // 
            this.lblPN.AutoSize = true;
            this.lblPN.Location = new System.Drawing.Point(12, 55);
            this.lblPN.Name = "lblPN";
            this.lblPN.Size = new System.Drawing.Size(57, 13);
            this.lblPN.TabIndex = 3;
            this.lblPN.Text = "Port Name";
            // 
            // txtPortNo
            // 
            this.txtPortNo.Enabled = false;
            this.txtPortNo.Location = new System.Drawing.Point(114, 27);
            this.txtPortNo.Name = "txtPortNo";
            this.txtPortNo.Size = new System.Drawing.Size(160, 20);
            this.txtPortNo.TabIndex = 2;
            this.txtPortNo.Tag = "PortNum";
            // 
            // lblPortNo
            // 
            this.lblPortNo.AutoSize = true;
            this.lblPortNo.Location = new System.Drawing.Point(12, 30);
            this.lblPortNo.Name = "lblPortNo";
            this.lblPortNo.Size = new System.Drawing.Size(46, 13);
            this.lblPortNo.TabIndex = 1;
            this.lblPortNo.Text = "Port No.";
            // 
            // txtGateway
            // 
            this.txtGateway.Location = new System.Drawing.Point(114, 204);
            this.txtGateway.Name = "txtGateway";
            this.txtGateway.Size = new System.Drawing.Size(160, 20);
            this.txtGateway.TabIndex = 14;
            this.txtGateway.Tag = "GateWay";
            this.txtGateway.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGateway_KeyPress);
            // 
            // lblGW
            // 
            this.lblGW.AutoSize = true;
            this.lblGW.Location = new System.Drawing.Point(12, 207);
            this.lblGW.Name = "lblGW";
            this.lblGW.Size = new System.Drawing.Size(49, 13);
            this.lblGW.TabIndex = 13;
            this.lblGW.Text = "Gateway";
            // 
            // txtSubnetMask
            // 
            this.txtSubnetMask.Location = new System.Drawing.Point(114, 179);
            this.txtSubnetMask.Name = "txtSubnetMask";
            this.txtSubnetMask.Size = new System.Drawing.Size(160, 20);
            this.txtSubnetMask.TabIndex = 12;
            this.txtSubnetMask.Tag = "SubNetMask";
            this.txtSubnetMask.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSubnetMask_KeyPress);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(114, 129);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(160, 20);
            this.txtIP.TabIndex = 10;
            this.txtIP.Tag = "IP";
            this.txtIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIP_KeyPress);
            this.txtIP.Validating += new System.ComponentModel.CancelEventHandler(this.txtIP_Validating);
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(12, 132);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(58, 13);
            this.lblIP.TabIndex = 9;
            this.lblIP.Text = "IP Address";
            // 
            // lblSM
            // 
            this.lblSM.AutoSize = true;
            this.lblSM.Location = new System.Drawing.Point(12, 182);
            this.lblSM.Name = "lblSM";
            this.lblSM.Size = new System.Drawing.Size(70, 13);
            this.lblSM.TabIndex = 11;
            this.lblSM.Text = "Subnet Mask";
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Location = new System.Drawing.Point(15, 264);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(59, 17);
            this.chkEnable.TabIndex = 17;
            this.chkEnable.Tag = "Enable_YES_NO";
            this.chkEnable.Text = "Enable";
            this.chkEnable.UseVisualStyleBackColor = true;
            this.chkEnable.CheckedChanged += new System.EventHandler(this.chkEnable_CheckedChanged);
            // 
            // cmbConnectionType
            // 
            this.cmbConnectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnectionType.FormattingEnabled = true;
            this.cmbConnectionType.Location = new System.Drawing.Point(114, 77);
            this.cmbConnectionType.Name = "cmbConnectionType";
            this.cmbConnectionType.Size = new System.Drawing.Size(160, 21);
            this.cmbConnectionType.TabIndex = 6;
            this.cmbConnectionType.Tag = "ConnectionType";
            // 
            // lblCT
            // 
            this.lblCT.AutoSize = true;
            this.lblCT.Location = new System.Drawing.Point(12, 80);
            this.lblCT.Name = "lblCT";
            this.lblCT.Size = new System.Drawing.Size(88, 13);
            this.lblCT.TabIndex = 5;
            this.lblCT.Text = "Connection Type";
            // 
            // lblAT
            // 
            this.lblAT.AutoSize = true;
            this.lblAT.Location = new System.Drawing.Point(12, 106);
            this.lblAT.Name = "lblAT";
            this.lblAT.Size = new System.Drawing.Size(72, 13);
            this.lblAT.TabIndex = 7;
            this.lblAT.Text = "Address Type";
            // 
            // btnEdit
            // 
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.Location = new System.Drawing.Point(162, 4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(68, 10);
            this.btnEdit.TabIndex = 16;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 30);
            this.panel1.TabIndex = 17;
            // 
            // ucNetworkConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.grpNI);
            this.Controls.Add(this.lvNPorts);
            this.Controls.Add(this.panel1);
            this.Name = "ucNetworkConfiguration";
            this.Size = new System.Drawing.Size(1050, 700);
            this.Load += new System.EventHandler(this.ucNetworkConfiguration_Load);
            this.grpNI.ResumeLayout(false);
            this.grpNI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListView lvNPorts;
        public System.Windows.Forms.GroupBox grpNI;
        public System.Windows.Forms.TextBox txtPortName;
        private System.Windows.Forms.Label lblPN;
        public System.Windows.Forms.TextBox txtPortNo;
        private System.Windows.Forms.Label lblPortNo;
        public System.Windows.Forms.TextBox txtGateway;
        private System.Windows.Forms.Label lblGW;
        public System.Windows.Forms.TextBox txtSubnetMask;
        public System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblSM;
        public System.Windows.Forms.CheckBox chkEnable;
        public System.Windows.Forms.ComboBox cmbConnectionType;
        private System.Windows.Forms.Label lblCT;
        private System.Windows.Forms.Label lblAT;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.ComboBox cmbAddressType;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Label lblPrimaryDevice;
        public System.Windows.Forms.ComboBox cmbPortName;
        public System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox txtVirtualIP;
        private System.Windows.Forms.Label label2;
    }
}
