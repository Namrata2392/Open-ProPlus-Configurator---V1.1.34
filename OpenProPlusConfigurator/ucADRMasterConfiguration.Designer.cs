namespace OpenProPlusConfigurator
{
    partial class ucADRMasterConfiguration
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
            this.grpIEC103 = new System.Windows.Forms.GroupBox();
            this.txtClockSyncInterval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRefreshInterval = new System.Windows.Forms.TextBox();
            this.txtIECDesc = new System.Windows.Forms.TextBox();
            this.txtFirmwareVer = new System.Windows.Forms.TextBox();
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.lblRefreshInterval = new System.Windows.Forms.Label();
            this.txtPortTimesync = new System.Windows.Forms.TextBox();
            this.lblPortTimeSync = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.txtMasterNo = new System.Windows.Forms.TextBox();
            this.lblMN = new System.Windows.Forms.Label();
            this.lblAFV = new System.Windows.Forms.Label();
            this.txtPollingInterval = new System.Windows.Forms.TextBox();
            this.lblpollingInterval = new System.Windows.Forms.Label();
            this.lblDB = new System.Windows.Forms.Label();
            this.lblIED = new System.Windows.Forms.Label();
            this.lvIEDList = new System.Windows.Forms.ListView();
            this.grpIED = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDS = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtUnitID = new System.Windows.Forms.TextBox();
            this.lblUI = new System.Windows.Forms.Label();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.lblTO = new System.Windows.Forms.Label();
            this.txtRetries = new System.Windows.Forms.TextBox();
            this.lblRetries = new System.Windows.Forms.Label();
            this.txtDevice = new System.Windows.Forms.TextBox();
            this.lblDevice = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnImportIED = new System.Windows.Forms.Button();
            this.btnExportIED = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpIEC103.SuspendLayout();
            this.grpIED.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpIEC103
            // 
            this.grpIEC103.BackColor = System.Drawing.SystemColors.Control;
            this.grpIEC103.Controls.Add(this.txtClockSyncInterval);
            this.grpIEC103.Controls.Add(this.label3);
            this.grpIEC103.Controls.Add(this.txtRefreshInterval);
            this.grpIEC103.Controls.Add(this.txtIECDesc);
            this.grpIEC103.Controls.Add(this.txtFirmwareVer);
            this.grpIEC103.Controls.Add(this.txtDebug);
            this.grpIEC103.Controls.Add(this.lblRefreshInterval);
            this.grpIEC103.Controls.Add(this.txtPortTimesync);
            this.grpIEC103.Controls.Add(this.lblPortTimeSync);
            this.grpIEC103.Controls.Add(this.lblDesc);
            this.grpIEC103.Controls.Add(this.chkRun);
            this.grpIEC103.Controls.Add(this.txtMasterNo);
            this.grpIEC103.Controls.Add(this.lblMN);
            this.grpIEC103.Controls.Add(this.lblAFV);
            this.grpIEC103.Controls.Add(this.txtPollingInterval);
            this.grpIEC103.Controls.Add(this.lblpollingInterval);
            this.grpIEC103.Controls.Add(this.lblDB);
            this.grpIEC103.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpIEC103.Location = new System.Drawing.Point(3, 3);
            this.grpIEC103.Name = "grpIEC103";
            this.grpIEC103.Size = new System.Drawing.Size(295, 231);
            this.grpIEC103.TabIndex = 22;
            this.grpIEC103.TabStop = false;
            this.grpIEC103.Text = "ADR Master";
            this.grpIEC103.Enter += new System.EventHandler(this.grpIEC103_Enter);
            // 
            // txtClockSyncInterval
            // 
            this.txtClockSyncInterval.Enabled = false;
            this.txtClockSyncInterval.Location = new System.Drawing.Point(146, 39);
            this.txtClockSyncInterval.Name = "txtClockSyncInterval";
            this.txtClockSyncInterval.Size = new System.Drawing.Size(137, 20);
            this.txtClockSyncInterval.TabIndex = 113;
            this.txtClockSyncInterval.Tag = "MasterNum";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 112;
            this.label3.Text = "Clock Sync Interval (sec)";
            // 
            // txtRefreshInterval
            // 
            this.txtRefreshInterval.Enabled = false;
            this.txtRefreshInterval.Location = new System.Drawing.Point(146, 135);
            this.txtRefreshInterval.Name = "txtRefreshInterval";
            this.txtRefreshInterval.Size = new System.Drawing.Size(137, 20);
            this.txtRefreshInterval.TabIndex = 111;
            // 
            // txtIECDesc
            // 
            this.txtIECDesc.Enabled = false;
            this.txtIECDesc.Location = new System.Drawing.Point(146, 183);
            this.txtIECDesc.Name = "txtIECDesc";
            this.txtIECDesc.Size = new System.Drawing.Size(137, 20);
            this.txtIECDesc.TabIndex = 110;
            // 
            // txtFirmwareVer
            // 
            this.txtFirmwareVer.Enabled = false;
            this.txtFirmwareVer.Location = new System.Drawing.Point(146, 159);
            this.txtFirmwareVer.Name = "txtFirmwareVer";
            this.txtFirmwareVer.Size = new System.Drawing.Size(137, 20);
            this.txtFirmwareVer.TabIndex = 109;
            // 
            // txtDebug
            // 
            this.txtDebug.Enabled = false;
            this.txtDebug.Location = new System.Drawing.Point(146, 63);
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.Size = new System.Drawing.Size(137, 20);
            this.txtDebug.TabIndex = 108;
            // 
            // lblRefreshInterval
            // 
            this.lblRefreshInterval.AutoSize = true;
            this.lblRefreshInterval.Location = new System.Drawing.Point(14, 138);
            this.lblRefreshInterval.Name = "lblRefreshInterval";
            this.lblRefreshInterval.Size = new System.Drawing.Size(108, 13);
            this.lblRefreshInterval.TabIndex = 105;
            this.lblRefreshInterval.Text = "Refresh Interval (sec)";
            this.lblRefreshInterval.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtPortTimesync
            // 
            this.txtPortTimesync.Enabled = false;
            this.txtPortTimesync.Location = new System.Drawing.Point(146, 111);
            this.txtPortTimesync.Name = "txtPortTimesync";
            this.txtPortTimesync.Size = new System.Drawing.Size(137, 20);
            this.txtPortTimesync.TabIndex = 93;
            this.txtPortTimesync.Tag = "RefreshInterval";
            // 
            // lblPortTimeSync
            // 
            this.lblPortTimeSync.AutoSize = true;
            this.lblPortTimeSync.Location = new System.Drawing.Point(14, 114);
            this.lblPortTimeSync.Name = "lblPortTimeSync";
            this.lblPortTimeSync.Size = new System.Drawing.Size(105, 13);
            this.lblPortTimeSync.TabIndex = 92;
            this.lblPortTimeSync.Text = "Port Time Sync (sec)";
            this.lblPortTimeSync.Click += new System.EventHandler(this.lblRI_Click);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(14, 186);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(60, 13);
            this.lblDesc.TabIndex = 96;
            this.lblDesc.Text = "Description";
            this.lblDesc.Click += new System.EventHandler(this.lblDesc_Click);
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Enabled = false;
            this.chkRun.Location = new System.Drawing.Point(18, 206);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 17);
            this.chkRun.TabIndex = 98;
            this.chkRun.Tag = "Run_YES_NO";
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // txtMasterNo
            // 
            this.txtMasterNo.Enabled = false;
            this.txtMasterNo.Location = new System.Drawing.Point(146, 15);
            this.txtMasterNo.Name = "txtMasterNo";
            this.txtMasterNo.Size = new System.Drawing.Size(137, 20);
            this.txtMasterNo.TabIndex = 83;
            this.txtMasterNo.Tag = "MasterNum";
            // 
            // lblMN
            // 
            this.lblMN.AutoSize = true;
            this.lblMN.Location = new System.Drawing.Point(14, 18);
            this.lblMN.Name = "lblMN";
            this.lblMN.Size = new System.Drawing.Size(59, 13);
            this.lblMN.TabIndex = 82;
            this.lblMN.Text = "Master No.";
            this.lblMN.Click += new System.EventHandler(this.lblMN_Click);
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(14, 162);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(71, 13);
            this.lblAFV.TabIndex = 94;
            this.lblAFV.Text = "Firmware Ver.";
            this.lblAFV.Click += new System.EventHandler(this.lblAFV_Click);
            // 
            // txtPollingInterval
            // 
            this.txtPollingInterval.Enabled = false;
            this.txtPollingInterval.Location = new System.Drawing.Point(146, 87);
            this.txtPollingInterval.Name = "txtPollingInterval";
            this.txtPollingInterval.Size = new System.Drawing.Size(137, 20);
            this.txtPollingInterval.TabIndex = 91;
            this.txtPollingInterval.Tag = "ClockSyncInterval";
            // 
            // lblpollingInterval
            // 
            this.lblpollingInterval.AutoSize = true;
            this.lblpollingInterval.Location = new System.Drawing.Point(14, 90);
            this.lblpollingInterval.Name = "lblpollingInterval";
            this.lblpollingInterval.Size = new System.Drawing.Size(98, 13);
            this.lblpollingInterval.TabIndex = 90;
            this.lblpollingInterval.Text = "Polling Interval (ms)";
            this.lblpollingInterval.Click += new System.EventHandler(this.lblCSI_Click);
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Location = new System.Drawing.Point(14, 66);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(68, 13);
            this.lblDB.TabIndex = 86;
            this.lblDB.Text = "Debug Level";
            this.lblDB.Click += new System.EventHandler(this.lblDB_Click);
            // 
            // lblIED
            // 
            this.lblIED.AutoSize = true;
            this.lblIED.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblIED.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIED.Location = new System.Drawing.Point(0, 0);
            this.lblIED.Name = "lblIED";
            this.lblIED.Size = new System.Drawing.Size(52, 13);
            this.lblIED.TabIndex = 23;
            this.lblIED.Text = "IED List";
            // 
            // lvIEDList
            // 
            this.lvIEDList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvIEDList.CheckBoxes = true;
            this.lvIEDList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvIEDList.FullRowSelect = true;
            this.lvIEDList.Location = new System.Drawing.Point(0, 13);
            this.lvIEDList.MultiSelect = false;
            this.lvIEDList.Name = "lvIEDList";
            this.lvIEDList.Size = new System.Drawing.Size(1050, 401);
            this.lvIEDList.TabIndex = 24;
            this.lvIEDList.UseCompatibleStateImageBehavior = false;
            this.lvIEDList.View = System.Windows.Forms.View.Details;
            this.lvIEDList.SelectedIndexChanged += new System.EventHandler(this.lvIEDList_SelectedIndexChanged);
            this.lvIEDList.DoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
            // 
            // grpIED
            // 
            this.grpIED.BackColor = System.Drawing.SystemColors.Control;
            this.grpIED.Controls.Add(this.label2);
            this.grpIED.Controls.Add(this.txtDescription);
            this.grpIED.Controls.Add(this.lblDS);
            this.grpIED.Controls.Add(this.pictureBox1);
            this.grpIED.Controls.Add(this.txtUnitID);
            this.grpIED.Controls.Add(this.lblUI);
            this.grpIED.Controls.Add(this.txtTimeOut);
            this.grpIED.Controls.Add(this.lblTO);
            this.grpIED.Controls.Add(this.txtRetries);
            this.grpIED.Controls.Add(this.lblRetries);
            this.grpIED.Controls.Add(this.txtDevice);
            this.grpIED.Controls.Add(this.lblDevice);
            this.grpIED.Controls.Add(this.label1);
            this.grpIED.Controls.Add(this.btnCancel);
            this.grpIED.Controls.Add(this.btnDone);
            this.grpIED.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpIED.Location = new System.Drawing.Point(320, 105);
            this.grpIED.Name = "grpIED";
            this.grpIED.Size = new System.Drawing.Size(278, 200);
            this.grpIED.TabIndex = 25;
            this.grpIED.TabStop = false;
            this.grpIED.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 15);
            this.label2.TabIndex = 115;
            this.label2.Text = "IED";
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label2_MouseDown);
            this.label2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label2_MouseMove);
            this.label2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label2_MouseUp);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(102, 130);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(159, 20);
            this.txtDescription.TabIndex = 93;
            this.txtDescription.Tag = "Description";
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            // 
            // lblDS
            // 
            this.lblDS.AutoSize = true;
            this.lblDS.Location = new System.Drawing.Point(12, 133);
            this.lblDS.Name = "lblDS";
            this.lblDS.Size = new System.Drawing.Size(60, 13);
            this.lblDS.TabIndex = 92;
            this.lblDS.Text = "Description";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(278, 22);
            this.pictureBox1.TabIndex = 39;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // txtUnitID
            // 
            this.txtUnitID.Location = new System.Drawing.Point(102, 30);
            this.txtUnitID.Name = "txtUnitID";
            this.txtUnitID.Size = new System.Drawing.Size(159, 20);
            this.txtUnitID.TabIndex = 83;
            this.txtUnitID.Tag = "UnitID";
            // 
            // lblUI
            // 
            this.lblUI.AutoSize = true;
            this.lblUI.Location = new System.Drawing.Point(12, 33);
            this.lblUI.Name = "lblUI";
            this.lblUI.Size = new System.Drawing.Size(40, 13);
            this.lblUI.TabIndex = 82;
            this.lblUI.Text = "Unit ID";
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.Location = new System.Drawing.Point(102, 105);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(159, 20);
            this.txtTimeOut.TabIndex = 91;
            this.txtTimeOut.Tag = "TimeOutMS";
            // 
            // lblTO
            // 
            this.lblTO.AutoSize = true;
            this.lblTO.Location = new System.Drawing.Point(12, 108);
            this.lblTO.Name = "lblTO";
            this.lblTO.Size = new System.Drawing.Size(79, 13);
            this.lblTO.TabIndex = 90;
            this.lblTO.Text = "Timeout (msec)";
            // 
            // txtRetries
            // 
            this.txtRetries.Location = new System.Drawing.Point(102, 80);
            this.txtRetries.Name = "txtRetries";
            this.txtRetries.Size = new System.Drawing.Size(159, 20);
            this.txtRetries.TabIndex = 89;
            this.txtRetries.Tag = "Retries";
            // 
            // lblRetries
            // 
            this.lblRetries.AutoSize = true;
            this.lblRetries.Location = new System.Drawing.Point(12, 83);
            this.lblRetries.Name = "lblRetries";
            this.lblRetries.Size = new System.Drawing.Size(40, 13);
            this.lblRetries.TabIndex = 88;
            this.lblRetries.Text = "Retries";
            // 
            // txtDevice
            // 
            this.txtDevice.Location = new System.Drawing.Point(102, 55);
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.Size = new System.Drawing.Size(159, 20);
            this.txtDevice.TabIndex = 87;
            this.txtDevice.Tag = "Device";
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Location = new System.Drawing.Point(12, 58);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(41, 13);
            this.lblDevice.TabIndex = 86;
            this.lblDevice.Text = "Device";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(10, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 40;
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(193, 160);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 96;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(102, 160);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 95;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnImportIED
            // 
            this.btnImportIED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImportIED.FlatAppearance.BorderSize = 0;
            this.btnImportIED.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnImportIED.Location = new System.Drawing.Point(240, 3);
            this.btnImportIED.Name = "btnImportIED";
            this.btnImportIED.Size = new System.Drawing.Size(68, 32);
            this.btnImportIED.TabIndex = 33;
            this.btnImportIED.TabStop = false;
            this.btnImportIED.Text = "&Import IED";
            this.btnImportIED.UseVisualStyleBackColor = true;
            this.btnImportIED.Click += new System.EventHandler(this.btnImportIED_Click);
            // 
            // btnExportIED
            // 
            this.btnExportIED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExportIED.FlatAppearance.BorderSize = 0;
            this.btnExportIED.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportIED.Location = new System.Drawing.Point(160, 3);
            this.btnExportIED.Name = "btnExportIED";
            this.btnExportIED.Size = new System.Drawing.Size(68, 32);
            this.btnExportIED.TabIndex = 32;
            this.btnExportIED.TabStop = false;
            this.btnExportIED.Text = "E&xport IED";
            this.btnExportIED.UseVisualStyleBackColor = true;
            this.btnExportIED.Click += new System.EventHandler(this.btnExportIED_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(82, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 32);
            this.btnDelete.TabIndex = 31;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 32);
            this.btnAdd.TabIndex = 30;
            this.btnAdd.TabStop = false;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpIEC103);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpIED);
            this.splitContainer1.Panel2.Controls.Add(this.lvIEDList);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.lblIED);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 700);
            this.splitContainer1.SplitterDistance = 237;
            this.splitContainer1.TabIndex = 34;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnExportIED);
            this.panel1.Controls.Add(this.btnImportIED);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 414);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 45);
            this.panel1.TabIndex = 34;
            // 
            // ucADRMasterConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucADRMasterConfiguration";
            this.Size = new System.Drawing.Size(1050, 700);
            this.Load += new System.EventHandler(this.ucADRMasterConfiguration_Load);
            this.grpIEC103.ResumeLayout(false);
            this.grpIEC103.PerformLayout();
            this.grpIED.ResumeLayout(false);
            this.grpIED.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.GroupBox grpIEC103;
        public System.Windows.Forms.TextBox txtPortTimesync;
        private System.Windows.Forms.Label lblPortTimeSync;
        private System.Windows.Forms.Label lblDesc;
        public System.Windows.Forms.CheckBox chkRun;
        public System.Windows.Forms.TextBox txtMasterNo;
        private System.Windows.Forms.Label lblMN;
        private System.Windows.Forms.Label lblAFV;
        public System.Windows.Forms.TextBox txtPollingInterval;
        private System.Windows.Forms.Label lblpollingInterval;
        private System.Windows.Forms.Label lblDB;
        private System.Windows.Forms.Label lblRefreshInterval;
        public System.Windows.Forms.Label lblIED;
        public System.Windows.Forms.ListView lvIEDList;
        public System.Windows.Forms.GroupBox grpIED;
        public System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDS;
        public System.Windows.Forms.TextBox txtTimeOut;
        private System.Windows.Forms.Label lblTO;
        public System.Windows.Forms.TextBox txtRetries;
        private System.Windows.Forms.Label lblRetries;
        public System.Windows.Forms.TextBox txtDevice;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.Button btnImportIED;
        public System.Windows.Forms.Button btnExportIED;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.TextBox txtDebug;
        public System.Windows.Forms.TextBox txtFirmwareVer;
        public System.Windows.Forms.TextBox txtIECDesc;
        public System.Windows.Forms.TextBox txtRefreshInterval;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtUnitID;
        private System.Windows.Forms.Label lblUI;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox txtClockSyncInterval;
        private System.Windows.Forms.Label label3;
    }
}
