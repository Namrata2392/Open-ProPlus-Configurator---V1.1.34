namespace OpenProPlusConfigurator
{
    partial class ucGroupMODBUS
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
            this.lvMODBUSmaster = new System.Windows.Forms.ListView();
            this.lblMMC = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpMODBUS = new System.Windows.Forms.GroupBox();
            this.cmbDebug = new System.Windows.Forms.ComboBox();
            this.lblDB = new System.Windows.Forms.Label();
            this.txtRefreshInterval = new System.Windows.Forms.TextBox();
            this.lblRI = new System.Windows.Forms.Label();
            this.cmbPortNo = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.cmbProtocolType = new System.Windows.Forms.ComboBox();
            this.lblPT = new System.Windows.Forms.Label();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.txtMasterNo = new System.Windows.Forms.TextBox();
            this.lblMN = new System.Windows.Forms.Label();
            this.txtFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lblAFV = new System.Windows.Forms.Label();
            this.txtPollingInterval = new System.Windows.Forms.TextBox();
            this.lblPI = new System.Windows.Forms.Label();
            this.txtClockSyncInterval = new System.Windows.Forms.TextBox();
            this.lblCSI = new System.Windows.Forms.Label();
            this.lblPN = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpMODBUS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMODBUSmaster
            // 
            this.lvMODBUSmaster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvMODBUSmaster.CheckBoxes = true;
            this.lvMODBUSmaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMODBUSmaster.FullRowSelect = true;
            this.lvMODBUSmaster.Location = new System.Drawing.Point(0, 30);
            this.lvMODBUSmaster.MultiSelect = false;
            this.lvMODBUSmaster.Name = "lvMODBUSmaster";
            this.lvMODBUSmaster.Size = new System.Drawing.Size(1050, 620);
            this.lvMODBUSmaster.TabIndex = 9;
            this.lvMODBUSmaster.UseCompatibleStateImageBehavior = false;
            this.lvMODBUSmaster.View = System.Windows.Forms.View.Details;
            this.lvMODBUSmaster.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvMODBUSmaster_ItemCheck);
            this.lvMODBUSmaster.DoubleClick += new System.EventHandler(this.lvMODBUSmaster_DoubleClick);
            // 
            // lblMMC
            // 
            this.lblMMC.AutoSize = true;
            this.lblMMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMMC.Location = new System.Drawing.Point(3, 0);
            this.lblMMC.Name = "lblMMC";
            this.lblMMC.Size = new System.Drawing.Size(209, 15);
            this.lblMMC.TabIndex = 8;
            this.lblMMC.Text = "MODBUS Master Configuration:";
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(80, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 28);
            this.btnDelete.TabIndex = 26;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(3, 9);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 28);
            this.btnAdd.TabIndex = 25;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grpMODBUS
            // 
            this.grpMODBUS.BackColor = System.Drawing.SystemColors.Control;
            this.grpMODBUS.Controls.Add(this.cmbDebug);
            this.grpMODBUS.Controls.Add(this.lblDB);
            this.grpMODBUS.Controls.Add(this.txtRefreshInterval);
            this.grpMODBUS.Controls.Add(this.lblRI);
            this.grpMODBUS.Controls.Add(this.cmbPortNo);
            this.grpMODBUS.Controls.Add(this.txtDescription);
            this.grpMODBUS.Controls.Add(this.lblDesc);
            this.grpMODBUS.Controls.Add(this.btnLast);
            this.grpMODBUS.Controls.Add(this.btnNext);
            this.grpMODBUS.Controls.Add(this.btnPrev);
            this.grpMODBUS.Controls.Add(this.btnFirst);
            this.grpMODBUS.Controls.Add(this.cmbProtocolType);
            this.grpMODBUS.Controls.Add(this.lblPT);
            this.grpMODBUS.Controls.Add(this.chkRun);
            this.grpMODBUS.Controls.Add(this.txtMasterNo);
            this.grpMODBUS.Controls.Add(this.lblMN);
            this.grpMODBUS.Controls.Add(this.txtFirmwareVersion);
            this.grpMODBUS.Controls.Add(this.lblAFV);
            this.grpMODBUS.Controls.Add(this.txtPollingInterval);
            this.grpMODBUS.Controls.Add(this.lblPI);
            this.grpMODBUS.Controls.Add(this.txtClockSyncInterval);
            this.grpMODBUS.Controls.Add(this.lblCSI);
            this.grpMODBUS.Controls.Add(this.lblPN);
            this.grpMODBUS.Controls.Add(this.lblHdrText);
            this.grpMODBUS.Controls.Add(this.pbHdr);
            this.grpMODBUS.Controls.Add(this.btnCancel);
            this.grpMODBUS.Controls.Add(this.btnDone);
            this.grpMODBUS.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpMODBUS.Location = new System.Drawing.Point(365, 100);
            this.grpMODBUS.Name = "grpMODBUS";
            this.grpMODBUS.Size = new System.Drawing.Size(315, 334);
            this.grpMODBUS.TabIndex = 24;
            this.grpMODBUS.TabStop = false;
            this.grpMODBUS.Visible = false;
            // 
            // cmbDebug
            // 
            this.cmbDebug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDebug.FormattingEnabled = true;
            this.cmbDebug.Location = new System.Drawing.Point(142, 214);
            this.cmbDebug.Name = "cmbDebug";
            this.cmbDebug.Size = new System.Drawing.Size(156, 21);
            this.cmbDebug.TabIndex = 108;
            this.cmbDebug.Tag = "DEBUG";
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Location = new System.Drawing.Point(12, 217);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(68, 13);
            this.lblDB.TabIndex = 107;
            this.lblDB.Text = "Debug Level";
            // 
            // txtRefreshInterval
            // 
            this.txtRefreshInterval.Location = new System.Drawing.Point(142, 162);
            this.txtRefreshInterval.Name = "txtRefreshInterval";
            this.txtRefreshInterval.Size = new System.Drawing.Size(156, 20);
            this.txtRefreshInterval.TabIndex = 93;
            this.txtRefreshInterval.Tag = "RefreshInterval";
            // 
            // lblRI
            // 
            this.lblRI.AutoSize = true;
            this.lblRI.Location = new System.Drawing.Point(12, 165);
            this.lblRI.Name = "lblRI";
            this.lblRI.Size = new System.Drawing.Size(108, 13);
            this.lblRI.TabIndex = 92;
            this.lblRI.Text = "Refresh Interval (sec)";
            // 
            // cmbPortNo
            // 
            this.cmbPortNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortNo.FormattingEnabled = true;
            this.cmbPortNo.Location = new System.Drawing.Point(142, 83);
            this.cmbPortNo.Name = "cmbPortNo";
            this.cmbPortNo.Size = new System.Drawing.Size(156, 21);
            this.cmbPortNo.TabIndex = 87;
            this.cmbPortNo.Tag = "PortNum";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(142, 241);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(156, 20);
            this.txtDescription.TabIndex = 99;
            this.txtDescription.Tag = "Description";
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(12, 244);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(60, 13);
            this.lblDesc.TabIndex = 98;
            this.lblDesc.Text = "Description";
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(240, 303);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(58, 22);
            this.btnLast.TabIndex = 106;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(164, 303);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(58, 22);
            this.btnNext.TabIndex = 105;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(88, 303);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(58, 22);
            this.btnPrev.TabIndex = 104;
            this.btnPrev.Text = "<Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(12, 303);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(58, 22);
            this.btnFirst.TabIndex = 103;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // cmbProtocolType
            // 
            this.cmbProtocolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProtocolType.FormattingEnabled = true;
            this.cmbProtocolType.Location = new System.Drawing.Point(142, 56);
            this.cmbProtocolType.Name = "cmbProtocolType";
            this.cmbProtocolType.Size = new System.Drawing.Size(156, 21);
            this.cmbProtocolType.TabIndex = 85;
            this.cmbProtocolType.Tag = "ProtocolType";
            this.cmbProtocolType.SelectedIndexChanged += new System.EventHandler(this.cmbProtocolType_SelectedIndexChanged);
            // 
            // lblPT
            // 
            this.lblPT.AutoSize = true;
            this.lblPT.Location = new System.Drawing.Point(12, 59);
            this.lblPT.Name = "lblPT";
            this.lblPT.Size = new System.Drawing.Size(73, 13);
            this.lblPT.TabIndex = 84;
            this.lblPT.Text = "Protocol Type";
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Location = new System.Drawing.Point(12, 276);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 17);
            this.chkRun.TabIndex = 100;
            this.chkRun.Tag = "Run_YES_NO";
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // txtMasterNo
            // 
            this.txtMasterNo.Enabled = false;
            this.txtMasterNo.Location = new System.Drawing.Point(142, 30);
            this.txtMasterNo.Name = "txtMasterNo";
            this.txtMasterNo.Size = new System.Drawing.Size(156, 20);
            this.txtMasterNo.TabIndex = 83;
            this.txtMasterNo.Tag = "MasterNum";
            // 
            // lblMN
            // 
            this.lblMN.AutoSize = true;
            this.lblMN.Location = new System.Drawing.Point(12, 33);
            this.lblMN.Name = "lblMN";
            this.lblMN.Size = new System.Drawing.Size(59, 13);
            this.lblMN.TabIndex = 82;
            this.lblMN.Text = "Master No.";
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.Location = new System.Drawing.Point(142, 188);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.Size = new System.Drawing.Size(156, 20);
            this.txtFirmwareVersion.TabIndex = 97;
            this.txtFirmwareVersion.Tag = "AppFirmwareVersion";
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(12, 191);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(87, 13);
            this.lblAFV.TabIndex = 96;
            this.lblAFV.Text = "Firmware Version";
            // 
            // txtPollingInterval
            // 
            this.txtPollingInterval.Location = new System.Drawing.Point(142, 136);
            this.txtPollingInterval.Name = "txtPollingInterval";
            this.txtPollingInterval.Size = new System.Drawing.Size(156, 20);
            this.txtPollingInterval.TabIndex = 91;
            this.txtPollingInterval.Tag = "PollingIntervalmSec";
            this.txtPollingInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPollingInterval_KeyPress);
            // 
            // lblPI
            // 
            this.lblPI.AutoSize = true;
            this.lblPI.Location = new System.Drawing.Point(12, 139);
            this.lblPI.Name = "lblPI";
            this.lblPI.Size = new System.Drawing.Size(110, 13);
            this.lblPI.TabIndex = 90;
            this.lblPI.Text = "Polling Interval (msec)";
            // 
            // txtClockSyncInterval
            // 
            this.txtClockSyncInterval.Location = new System.Drawing.Point(142, 110);
            this.txtClockSyncInterval.Name = "txtClockSyncInterval";
            this.txtClockSyncInterval.Size = new System.Drawing.Size(156, 20);
            this.txtClockSyncInterval.TabIndex = 89;
            this.txtClockSyncInterval.Tag = "PortTimesyncSec";
            this.txtClockSyncInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClockSyncInterval_KeyPress);
            // 
            // lblCSI
            // 
            this.lblCSI.AutoSize = true;
            this.lblCSI.Location = new System.Drawing.Point(12, 113);
            this.lblCSI.Name = "lblCSI";
            this.lblCSI.Size = new System.Drawing.Size(125, 13);
            this.lblCSI.TabIndex = 88;
            this.lblCSI.Text = "Clock Sync Interval (sec)";
            // 
            // lblPN
            // 
            this.lblPN.AutoSize = true;
            this.lblPN.Location = new System.Drawing.Point(12, 86);
            this.lblPN.Name = "lblPN";
            this.lblPN.Size = new System.Drawing.Size(46, 13);
            this.lblPN.TabIndex = 86;
            this.lblPN.Text = "Port No.";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(102, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "MODBUS Master";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(315, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(230, 269);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 102;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(142, 269);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 101;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMMC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 30);
            this.panel1.TabIndex = 27;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 650);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1050, 50);
            this.panel2.TabIndex = 28;
            // 
            // ucGroupMODBUS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMODBUS);
            this.Controls.Add(this.lvMODBUSmaster);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucGroupMODBUS";
            this.Size = new System.Drawing.Size(1050, 700);
            this.grpMODBUS.ResumeLayout(false);
            this.grpMODBUS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lvMODBUSmaster;
        private System.Windows.Forms.Label lblMMC;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.GroupBox grpMODBUS;
        public System.Windows.Forms.CheckBox chkRun;
        public System.Windows.Forms.TextBox txtMasterNo;
        private System.Windows.Forms.Label lblMN;
        public System.Windows.Forms.TextBox txtFirmwareVersion;
        private System.Windows.Forms.Label lblAFV;
        public System.Windows.Forms.TextBox txtPollingInterval;
        private System.Windows.Forms.Label lblPI;
        public System.Windows.Forms.TextBox txtClockSyncInterval;
        private System.Windows.Forms.Label lblCSI;
        public System.Windows.Forms.Label lblPN;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.ComboBox cmbProtocolType;
        private System.Windows.Forms.Label lblPT;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDesc;
        public System.Windows.Forms.ComboBox cmbPortNo;
        public System.Windows.Forms.TextBox txtRefreshInterval;
        private System.Windows.Forms.Label lblRI;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.ComboBox cmbDebug;
        private System.Windows.Forms.Label lblDB;
    }
}
