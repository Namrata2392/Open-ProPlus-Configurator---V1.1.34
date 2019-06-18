namespace OpenProPlusConfigurator
{
    partial class ucADRGroup
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
            this.lblIMC = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDB = new System.Windows.Forms.Label();
            this.lblGT = new System.Windows.Forms.Label();
            this.txtPollingInterval = new System.Windows.Forms.TextBox();
            this.lblCSI = new System.Windows.Forms.Label();
            this.txtPoolingTime = new System.Windows.Forms.TextBox();
            this.lblAFV = new System.Windows.Forms.Label();
            this.txtFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lblMN = new System.Windows.Forms.Label();
            this.txtMasterNo = new System.Windows.Forms.TextBox();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cmbDebug = new System.Windows.Forms.ComboBox();
            this.lblRI = new System.Windows.Forms.Label();
            this.txtRefreshInterval = new System.Windows.Forms.TextBox();
            this.grpADR = new System.Windows.Forms.GroupBox();
            this.txtClockSync = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.lvIADRMaster = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpADR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblIMC
            // 
            this.lblIMC.AutoSize = true;
            this.lblIMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIMC.Location = new System.Drawing.Point(3, 0);
            this.lblIMC.Name = "lblIMC";
            this.lblIMC.Size = new System.Drawing.Size(177, 15);
            this.lblIMC.TabIndex = 7;
            this.lblIMC.Text = "ADR Master Configuration:";
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(80, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 28);
            this.btnDelete.TabIndex = 106;
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
            this.btnAdd.TabIndex = 105;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDone
            // 
            this.btnDone.BackColor = System.Drawing.SystemColors.Control;
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(146, 227);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 99;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = false;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(234, 227);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 100;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Location = new System.Drawing.Point(13, 175);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(68, 13);
            this.lblDB.TabIndex = 86;
            this.lblDB.Text = "Debug Level";
            // 
            // lblGT
            // 
            this.lblGT.AutoSize = true;
            this.lblGT.Location = new System.Drawing.Point(13, 79);
            this.lblGT.Name = "lblGT";
            this.lblGT.Size = new System.Drawing.Size(110, 13);
            this.lblGT.TabIndex = 88;
            this.lblGT.Text = "Polling Interval (msec)";
            // 
            // txtPollingInterval
            // 
            this.txtPollingInterval.Location = new System.Drawing.Point(148, 76);
            this.txtPollingInterval.Name = "txtPollingInterval";
            this.txtPollingInterval.Size = new System.Drawing.Size(159, 20);
            this.txtPollingInterval.TabIndex = 89;
            this.txtPollingInterval.Tag = "PollingIntervalmSec";
            // 
            // lblCSI
            // 
            this.lblCSI.AutoSize = true;
            this.lblCSI.Location = new System.Drawing.Point(13, 103);
            this.lblCSI.Name = "lblCSI";
            this.lblCSI.Size = new System.Drawing.Size(117, 13);
            this.lblCSI.TabIndex = 90;
            this.lblCSI.Text = "Polling Time Sync (sec)";
            // 
            // txtPoolingTime
            // 
            this.txtPoolingTime.Location = new System.Drawing.Point(148, 100);
            this.txtPoolingTime.Name = "txtPoolingTime";
            this.txtPoolingTime.Size = new System.Drawing.Size(159, 20);
            this.txtPoolingTime.TabIndex = 91;
            this.txtPoolingTime.Tag = "PortTimesyncSec";
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(13, 151);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(87, 13);
            this.lblAFV.TabIndex = 94;
            this.lblAFV.Text = "Firmware Version";
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.Enabled = false;
            this.txtFirmwareVersion.Location = new System.Drawing.Point(148, 148);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.Size = new System.Drawing.Size(159, 20);
            this.txtFirmwareVersion.TabIndex = 95;
            this.txtFirmwareVersion.Tag = "AppFirmwareVersion";
            // 
            // lblMN
            // 
            this.lblMN.AutoSize = true;
            this.lblMN.Location = new System.Drawing.Point(13, 31);
            this.lblMN.Name = "lblMN";
            this.lblMN.Size = new System.Drawing.Size(59, 13);
            this.lblMN.TabIndex = 82;
            this.lblMN.Text = "Master No.";
            // 
            // txtMasterNo
            // 
            this.txtMasterNo.Enabled = false;
            this.txtMasterNo.Location = new System.Drawing.Point(148, 28);
            this.txtMasterNo.Name = "txtMasterNo";
            this.txtMasterNo.Size = new System.Drawing.Size(159, 20);
            this.txtMasterNo.TabIndex = 83;
            this.txtMasterNo.Tag = "MasterNum";
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Location = new System.Drawing.Point(13, 234);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 17);
            this.chkRun.TabIndex = 98;
            this.chkRun.Tag = "Run_YES_NO";
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(13, 200);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(60, 13);
            this.lblDesc.TabIndex = 96;
            this.lblDesc.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(148, 197);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(159, 20);
            this.txtDescription.TabIndex = 97;
            this.txtDescription.Tag = "Description";
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            // 
            // cmbDebug
            // 
            this.cmbDebug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDebug.FormattingEnabled = true;
            this.cmbDebug.Location = new System.Drawing.Point(148, 172);
            this.cmbDebug.Name = "cmbDebug";
            this.cmbDebug.Size = new System.Drawing.Size(159, 21);
            this.cmbDebug.TabIndex = 87;
            this.cmbDebug.Tag = "DEBUG";
            // 
            // lblRI
            // 
            this.lblRI.AutoSize = true;
            this.lblRI.Location = new System.Drawing.Point(13, 127);
            this.lblRI.Name = "lblRI";
            this.lblRI.Size = new System.Drawing.Size(108, 13);
            this.lblRI.TabIndex = 92;
            this.lblRI.Text = "Refresh Interval (sec)";
            // 
            // txtRefreshInterval
            // 
            this.txtRefreshInterval.Location = new System.Drawing.Point(148, 124);
            this.txtRefreshInterval.Name = "txtRefreshInterval";
            this.txtRefreshInterval.Size = new System.Drawing.Size(159, 20);
            this.txtRefreshInterval.TabIndex = 93;
            this.txtRefreshInterval.Tag = "RefreshInterval";
            // 
            // grpADR
            // 
            this.grpADR.BackColor = System.Drawing.SystemColors.Control;
            this.grpADR.Controls.Add(this.txtClockSync);
            this.grpADR.Controls.Add(this.label1);
            this.grpADR.Controls.Add(this.txtRefreshInterval);
            this.grpADR.Controls.Add(this.lblRI);
            this.grpADR.Controls.Add(this.cmbDebug);
            this.grpADR.Controls.Add(this.txtDescription);
            this.grpADR.Controls.Add(this.lblDesc);
            this.grpADR.Controls.Add(this.chkRun);
            this.grpADR.Controls.Add(this.txtMasterNo);
            this.grpADR.Controls.Add(this.lblMN);
            this.grpADR.Controls.Add(this.txtFirmwareVersion);
            this.grpADR.Controls.Add(this.lblAFV);
            this.grpADR.Controls.Add(this.txtPoolingTime);
            this.grpADR.Controls.Add(this.lblCSI);
            this.grpADR.Controls.Add(this.txtPollingInterval);
            this.grpADR.Controls.Add(this.lblGT);
            this.grpADR.Controls.Add(this.lblDB);
            this.grpADR.Controls.Add(this.lblHdrText);
            this.grpADR.Controls.Add(this.pbHdr);
            this.grpADR.Controls.Add(this.btnCancel);
            this.grpADR.Controls.Add(this.btnDone);
            this.grpADR.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpADR.Location = new System.Drawing.Point(282, 125);
            this.grpADR.Name = "grpADR";
            this.grpADR.Size = new System.Drawing.Size(318, 271);
            this.grpADR.TabIndex = 22;
            this.grpADR.TabStop = false;
            this.grpADR.Visible = false;
            // 
            // txtClockSync
            // 
            this.txtClockSync.Location = new System.Drawing.Point(148, 52);
            this.txtClockSync.Name = "txtClockSync";
            this.txtClockSync.Size = new System.Drawing.Size(159, 20);
            this.txtClockSync.TabIndex = 102;
            this.txtClockSync.Tag = "ClockSyncInterval";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 101;
            this.label1.Text = "Clock Sync Interval (sec)";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(75, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "ADR Master";
            this.lblHdrText.Click += new System.EventHandler(this.lblHdrText_Click);
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(318, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // lvIADRMaster
            // 
            this.lvIADRMaster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvIADRMaster.CheckBoxes = true;
            this.lvIADRMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvIADRMaster.FullRowSelect = true;
            this.lvIADRMaster.Location = new System.Drawing.Point(0, 30);
            this.lvIADRMaster.MultiSelect = false;
            this.lvIADRMaster.Name = "lvIADRMaster";
            this.lvIADRMaster.Size = new System.Drawing.Size(1050, 620);
            this.lvIADRMaster.TabIndex = 8;
            this.lvIADRMaster.UseCompatibleStateImageBehavior = false;
            this.lvIADRMaster.View = System.Windows.Forms.View.Details;
            this.lvIADRMaster.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIADRMaster_ItemCheck);
            this.lvIADRMaster.DoubleClick += new System.EventHandler(this.lvIADRMaster_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblIMC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 30);
            this.panel1.TabIndex = 107;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 650);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1050, 50);
            this.panel2.TabIndex = 108;
            // 
            // ucADRGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpADR);
            this.Controls.Add(this.lvIADRMaster);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "ucADRGroup";
            this.Size = new System.Drawing.Size(1050, 700);
            this.grpADR.ResumeLayout(false);
            this.grpADR.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblIMC;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDB;
        private System.Windows.Forms.Label lblGT;
        public System.Windows.Forms.TextBox txtPollingInterval;
        private System.Windows.Forms.Label lblCSI;
        public System.Windows.Forms.TextBox txtPoolingTime;
        private System.Windows.Forms.Label lblAFV;
        public System.Windows.Forms.TextBox txtFirmwareVersion;
        private System.Windows.Forms.Label lblMN;
        public System.Windows.Forms.TextBox txtMasterNo;
        public System.Windows.Forms.CheckBox chkRun;
        private System.Windows.Forms.Label lblDesc;
        public System.Windows.Forms.TextBox txtDescription;
        public System.Windows.Forms.ComboBox cmbDebug;
        private System.Windows.Forms.Label lblRI;
        public System.Windows.Forms.TextBox txtRefreshInterval;
        public System.Windows.Forms.GroupBox grpADR;
        public System.Windows.Forms.ListView lvIADRMaster;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        public System.Windows.Forms.TextBox txtClockSync;
        private System.Windows.Forms.Label label1;
    }
}
