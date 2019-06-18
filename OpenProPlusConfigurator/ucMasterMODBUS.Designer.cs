namespace OpenProPlusConfigurator
{
    partial class ucMasterMODBUS
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
            this.lvIEDList = new System.Windows.Forms.ListView();
            this.lblIED = new System.Windows.Forms.Label();
            this.grpMB = new System.Windows.Forms.GroupBox();
            this.txtRefreshInterval = new System.Windows.Forms.TextBox();
            this.lblRI = new System.Windows.Forms.Label();
            this.txtMBDescription = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtFirmwareVer = new System.Windows.Forms.TextBox();
            this.lblAFV = new System.Windows.Forms.Label();
            this.txtPortNo = new System.Windows.Forms.TextBox();
            this.lblPortNo = new System.Windows.Forms.Label();
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.lblDebug = new System.Windows.Forms.Label();
            this.txtClockSyncInterval = new System.Windows.Forms.TextBox();
            this.txtPollingInterval = new System.Windows.Forms.TextBox();
            this.lblPI = new System.Windows.Forms.Label();
            this.lblPTS = new System.Windows.Forms.Label();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.cmbProtocolType = new System.Windows.Forms.ComboBox();
            this.lblPT = new System.Windows.Forms.Label();
            this.txtMasterNo = new System.Windows.Forms.TextBox();
            this.lblMN = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpIED = new System.Windows.Forms.GroupBox();
            this.txtTCPPort = new System.Windows.Forms.TextBox();
            this.lblTP = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDS = new System.Windows.Forms.Label();
            this.chkDR = new System.Windows.Forms.CheckBox();
            this.txtUnitID = new System.Windows.Forms.TextBox();
            this.lblUI = new System.Windows.Forms.Label();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.lblTO = new System.Windows.Forms.Label();
            this.txtRetries = new System.Windows.Forms.TextBox();
            this.lblRetries = new System.Windows.Forms.Label();
            this.txtDevice = new System.Windows.Forms.TextBox();
            this.lblDevice = new System.Windows.Forms.Label();
            this.txtRemoteIP = new System.Windows.Forms.TextBox();
            this.lblRIP = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnExportIED = new System.Windows.Forms.Button();
            this.btnImportIED = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpMB.SuspendLayout();
            this.grpIED.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.lvIEDList.Size = new System.Drawing.Size(1050, 380);
            this.lvIEDList.TabIndex = 11;
            this.lvIEDList.UseCompatibleStateImageBehavior = false;
            this.lvIEDList.View = System.Windows.Forms.View.Details;
            this.lvIEDList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEDList_ItemCheck);
            this.lvIEDList.DoubleClick += new System.EventHandler(this.lvIEDList_DoubleClick);
            // 
            // lblIED
            // 
            this.lblIED.AutoSize = true;
            this.lblIED.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblIED.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIED.Location = new System.Drawing.Point(0, 0);
            this.lblIED.Name = "lblIED";
            this.lblIED.Size = new System.Drawing.Size(52, 13);
            this.lblIED.TabIndex = 10;
            this.lblIED.Text = "IED List";
            // 
            // grpMB
            // 
            this.grpMB.Controls.Add(this.txtRefreshInterval);
            this.grpMB.Controls.Add(this.lblRI);
            this.grpMB.Controls.Add(this.txtMBDescription);
            this.grpMB.Controls.Add(this.lblDesc);
            this.grpMB.Controls.Add(this.txtFirmwareVer);
            this.grpMB.Controls.Add(this.lblAFV);
            this.grpMB.Controls.Add(this.txtPortNo);
            this.grpMB.Controls.Add(this.lblPortNo);
            this.grpMB.Controls.Add(this.txtDebug);
            this.grpMB.Controls.Add(this.lblDebug);
            this.grpMB.Controls.Add(this.txtClockSyncInterval);
            this.grpMB.Controls.Add(this.txtPollingInterval);
            this.grpMB.Controls.Add(this.lblPI);
            this.grpMB.Controls.Add(this.lblPTS);
            this.grpMB.Controls.Add(this.chkRun);
            this.grpMB.Controls.Add(this.cmbProtocolType);
            this.grpMB.Controls.Add(this.lblPT);
            this.grpMB.Controls.Add(this.txtMasterNo);
            this.grpMB.Controls.Add(this.lblMN);
            this.grpMB.Location = new System.Drawing.Point(3, 0);
            this.grpMB.Name = "grpMB";
            this.grpMB.Size = new System.Drawing.Size(315, 256);
            this.grpMB.TabIndex = 14;
            this.grpMB.TabStop = false;
            this.grpMB.Text = "MODBUS Master";
            // 
            // txtRefreshInterval
            // 
            this.txtRefreshInterval.Enabled = false;
            this.txtRefreshInterval.Location = new System.Drawing.Point(152, 113);
            this.txtRefreshInterval.Name = "txtRefreshInterval";
            this.txtRefreshInterval.Size = new System.Drawing.Size(139, 20);
            this.txtRefreshInterval.TabIndex = 40;
            // 
            // lblRI
            // 
            this.lblRI.AutoSize = true;
            this.lblRI.Location = new System.Drawing.Point(14, 116);
            this.lblRI.Name = "lblRI";
            this.lblRI.Size = new System.Drawing.Size(108, 13);
            this.lblRI.TabIndex = 39;
            this.lblRI.Text = "Refresh Interval (sec)";
            // 
            // txtMBDescription
            // 
            this.txtMBDescription.Enabled = false;
            this.txtMBDescription.Location = new System.Drawing.Point(152, 213);
            this.txtMBDescription.Name = "txtMBDescription";
            this.txtMBDescription.Size = new System.Drawing.Size(139, 20);
            this.txtMBDescription.TabIndex = 36;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(14, 216);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(60, 13);
            this.lblDesc.TabIndex = 35;
            this.lblDesc.Text = "Description";
            // 
            // txtFirmwareVer
            // 
            this.txtFirmwareVer.Enabled = false;
            this.txtFirmwareVer.Location = new System.Drawing.Point(152, 188);
            this.txtFirmwareVer.Name = "txtFirmwareVer";
            this.txtFirmwareVer.Size = new System.Drawing.Size(139, 20);
            this.txtFirmwareVer.TabIndex = 34;
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(14, 191);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(71, 13);
            this.lblAFV.TabIndex = 33;
            this.lblAFV.Text = "Firmware Ver.";
            // 
            // txtPortNo
            // 
            this.txtPortNo.Enabled = false;
            this.txtPortNo.Location = new System.Drawing.Point(152, 163);
            this.txtPortNo.Name = "txtPortNo";
            this.txtPortNo.Size = new System.Drawing.Size(139, 20);
            this.txtPortNo.TabIndex = 32;
            // 
            // lblPortNo
            // 
            this.lblPortNo.AutoSize = true;
            this.lblPortNo.Location = new System.Drawing.Point(14, 166);
            this.lblPortNo.Name = "lblPortNo";
            this.lblPortNo.Size = new System.Drawing.Size(46, 13);
            this.lblPortNo.TabIndex = 31;
            this.lblPortNo.Text = "Port No.";
            // 
            // txtDebug
            // 
            this.txtDebug.Enabled = false;
            this.txtDebug.Location = new System.Drawing.Point(152, 138);
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.Size = new System.Drawing.Size(139, 20);
            this.txtDebug.TabIndex = 30;
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(14, 141);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(39, 13);
            this.lblDebug.TabIndex = 29;
            this.lblDebug.Text = "Debug";
            // 
            // txtClockSyncInterval
            // 
            this.txtClockSyncInterval.Enabled = false;
            this.txtClockSyncInterval.Location = new System.Drawing.Point(152, 88);
            this.txtClockSyncInterval.Name = "txtClockSyncInterval";
            this.txtClockSyncInterval.Size = new System.Drawing.Size(139, 20);
            this.txtClockSyncInterval.TabIndex = 28;
            // 
            // txtPollingInterval
            // 
            this.txtPollingInterval.Enabled = false;
            this.txtPollingInterval.Location = new System.Drawing.Point(152, 63);
            this.txtPollingInterval.Name = "txtPollingInterval";
            this.txtPollingInterval.Size = new System.Drawing.Size(139, 20);
            this.txtPollingInterval.TabIndex = 27;
            // 
            // lblPI
            // 
            this.lblPI.AutoSize = true;
            this.lblPI.Location = new System.Drawing.Point(14, 66);
            this.lblPI.Name = "lblPI";
            this.lblPI.Size = new System.Drawing.Size(98, 13);
            this.lblPI.TabIndex = 26;
            this.lblPI.Text = "Polling Interval (ms)";
            // 
            // lblPTS
            // 
            this.lblPTS.AutoSize = true;
            this.lblPTS.Location = new System.Drawing.Point(14, 91);
            this.lblPTS.Name = "lblPTS";
            this.lblPTS.Size = new System.Drawing.Size(125, 13);
            this.lblPTS.TabIndex = 25;
            this.lblPTS.Text = "Clock Sync Interval (sec)";
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Enabled = false;
            this.chkRun.Location = new System.Drawing.Point(17, 237);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 17);
            this.chkRun.TabIndex = 18;
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // cmbProtocolType
            // 
            this.cmbProtocolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProtocolType.Enabled = false;
            this.cmbProtocolType.FormattingEnabled = true;
            this.cmbProtocolType.Location = new System.Drawing.Point(151, 37);
            this.cmbProtocolType.Name = "cmbProtocolType";
            this.cmbProtocolType.Size = new System.Drawing.Size(139, 21);
            this.cmbProtocolType.TabIndex = 14;
            // 
            // lblPT
            // 
            this.lblPT.AutoSize = true;
            this.lblPT.Location = new System.Drawing.Point(14, 40);
            this.lblPT.Name = "lblPT";
            this.lblPT.Size = new System.Drawing.Size(73, 13);
            this.lblPT.TabIndex = 13;
            this.lblPT.Text = "Protocol Type";
            // 
            // txtMasterNo
            // 
            this.txtMasterNo.Enabled = false;
            this.txtMasterNo.Location = new System.Drawing.Point(151, 12);
            this.txtMasterNo.Name = "txtMasterNo";
            this.txtMasterNo.Size = new System.Drawing.Size(139, 20);
            this.txtMasterNo.TabIndex = 1;
            // 
            // lblMN
            // 
            this.lblMN.AutoSize = true;
            this.lblMN.Location = new System.Drawing.Point(14, 15);
            this.lblMN.Name = "lblMN";
            this.lblMN.Size = new System.Drawing.Size(59, 13);
            this.lblMN.TabIndex = 0;
            this.lblMN.Text = "Master No.";
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(84, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 28);
            this.btnDelete.TabIndex = 29;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(4, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 28);
            this.btnAdd.TabIndex = 28;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grpIED
            // 
            this.grpIED.BackColor = System.Drawing.SystemColors.Control;
            this.grpIED.Controls.Add(this.txtTCPPort);
            this.grpIED.Controls.Add(this.lblTP);
            this.grpIED.Controls.Add(this.btnLast);
            this.grpIED.Controls.Add(this.btnNext);
            this.grpIED.Controls.Add(this.btnPrev);
            this.grpIED.Controls.Add(this.btnFirst);
            this.grpIED.Controls.Add(this.txtDescription);
            this.grpIED.Controls.Add(this.lblDS);
            this.grpIED.Controls.Add(this.chkDR);
            this.grpIED.Controls.Add(this.txtUnitID);
            this.grpIED.Controls.Add(this.lblUI);
            this.grpIED.Controls.Add(this.txtTimeOut);
            this.grpIED.Controls.Add(this.lblTO);
            this.grpIED.Controls.Add(this.txtRetries);
            this.grpIED.Controls.Add(this.lblRetries);
            this.grpIED.Controls.Add(this.txtDevice);
            this.grpIED.Controls.Add(this.lblDevice);
            this.grpIED.Controls.Add(this.txtRemoteIP);
            this.grpIED.Controls.Add(this.lblRIP);
            this.grpIED.Controls.Add(this.lblHdrText);
            this.grpIED.Controls.Add(this.pbHdr);
            this.grpIED.Controls.Add(this.btnCancel);
            this.grpIED.Controls.Add(this.btnDone);
            this.grpIED.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpIED.Location = new System.Drawing.Point(272, 39);
            this.grpIED.Name = "grpIED";
            this.grpIED.Size = new System.Drawing.Size(286, 266);
            this.grpIED.TabIndex = 27;
            this.grpIED.TabStop = false;
            this.grpIED.Visible = false;
            // 
            // txtTCPPort
            // 
            this.txtTCPPort.Location = new System.Drawing.Point(108, 102);
            this.txtTCPPort.Name = "txtTCPPort";
            this.txtTCPPort.Size = new System.Drawing.Size(155, 20);
            this.txtTCPPort.TabIndex = 89;
            this.txtTCPPort.Tag = "TCPPort";
            this.txtTCPPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTCPPort_KeyPress);
            // 
            // lblTP
            // 
            this.lblTP.AutoSize = true;
            this.lblTP.Location = new System.Drawing.Point(12, 105);
            this.lblTP.Name = "lblTP";
            this.lblTP.Size = new System.Drawing.Size(50, 13);
            this.lblTP.TabIndex = 88;
            this.lblTP.Text = "TCP Port";
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(204, 235);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(58, 22);
            this.btnLast.TabIndex = 102;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(140, 235);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(58, 22);
            this.btnNext.TabIndex = 101;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(76, 235);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(58, 22);
            this.btnPrev.TabIndex = 100;
            this.btnPrev.Text = "<Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(12, 235);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(58, 22);
            this.btnFirst.TabIndex = 99;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(108, 174);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(155, 20);
            this.txtDescription.TabIndex = 95;
            this.txtDescription.Tag = "Description";
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            // 
            // lblDS
            // 
            this.lblDS.AutoSize = true;
            this.lblDS.Location = new System.Drawing.Point(12, 177);
            this.lblDS.Name = "lblDS";
            this.lblDS.Size = new System.Drawing.Size(60, 13);
            this.lblDS.TabIndex = 94;
            this.lblDS.Text = "Description";
            // 
            // chkDR
            // 
            this.chkDR.AutoSize = true;
            this.chkDR.Location = new System.Drawing.Point(12, 209);
            this.chkDR.Name = "chkDR";
            this.chkDR.Size = new System.Drawing.Size(42, 17);
            this.chkDR.TabIndex = 96;
            this.chkDR.Tag = "DR";
            this.chkDR.Text = "DR";
            this.chkDR.UseVisualStyleBackColor = true;
            // 
            // txtUnitID
            // 
            this.txtUnitID.Location = new System.Drawing.Point(108, 30);
            this.txtUnitID.Name = "txtUnitID";
            this.txtUnitID.Size = new System.Drawing.Size(155, 20);
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
            this.txtTimeOut.Location = new System.Drawing.Point(108, 150);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(155, 20);
            this.txtTimeOut.TabIndex = 93;
            this.txtTimeOut.Tag = "TimeOutMS";
            this.txtTimeOut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimeOut_KeyPress);
            // 
            // lblTO
            // 
            this.lblTO.AutoSize = true;
            this.lblTO.Location = new System.Drawing.Point(12, 153);
            this.lblTO.Name = "lblTO";
            this.lblTO.Size = new System.Drawing.Size(82, 13);
            this.lblTO.TabIndex = 92;
            this.lblTO.Text = "Time out (msec)";
            // 
            // txtRetries
            // 
            this.txtRetries.Location = new System.Drawing.Point(108, 126);
            this.txtRetries.Name = "txtRetries";
            this.txtRetries.Size = new System.Drawing.Size(155, 20);
            this.txtRetries.TabIndex = 91;
            this.txtRetries.Tag = "Retries";
            this.txtRetries.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRetries_KeyPress);
            // 
            // lblRetries
            // 
            this.lblRetries.AutoSize = true;
            this.lblRetries.Location = new System.Drawing.Point(12, 129);
            this.lblRetries.Name = "lblRetries";
            this.lblRetries.Size = new System.Drawing.Size(40, 13);
            this.lblRetries.TabIndex = 90;
            this.lblRetries.Text = "Retries";
            // 
            // txtDevice
            // 
            this.txtDevice.Location = new System.Drawing.Point(108, 54);
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.Size = new System.Drawing.Size(155, 20);
            this.txtDevice.TabIndex = 85;
            this.txtDevice.Tag = "Device";
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Location = new System.Drawing.Point(12, 57);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(41, 13);
            this.lblDevice.TabIndex = 84;
            this.lblDevice.Text = "Device";
            // 
            // txtRemoteIP
            // 
            this.txtRemoteIP.Location = new System.Drawing.Point(108, 78);
            this.txtRemoteIP.Name = "txtRemoteIP";
            this.txtRemoteIP.Size = new System.Drawing.Size(155, 20);
            this.txtRemoteIP.TabIndex = 87;
            this.txtRemoteIP.Tag = "RemoteIP";
            this.txtRemoteIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemoteIP_KeyPress);
            // 
            // lblRIP
            // 
            this.lblRIP.AutoSize = true;
            this.lblRIP.Location = new System.Drawing.Point(12, 81);
            this.lblRIP.Name = "lblRIP";
            this.lblRIP.Size = new System.Drawing.Size(57, 13);
            this.lblRIP.TabIndex = 86;
            this.lblRIP.Text = "Remote IP";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(28, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "IED";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(287, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(195, 202);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 98;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(108, 202);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 97;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnExportIED
            // 
            this.btnExportIED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExportIED.FlatAppearance.BorderSize = 0;
            this.btnExportIED.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportIED.Location = new System.Drawing.Point(160, 3);
            this.btnExportIED.Name = "btnExportIED";
            this.btnExportIED.Size = new System.Drawing.Size(68, 28);
            this.btnExportIED.TabIndex = 30;
            this.btnExportIED.Text = "E&xport IED";
            this.btnExportIED.UseVisualStyleBackColor = true;
            this.btnExportIED.Click += new System.EventHandler(this.btnExportIED_Click);
            // 
            // btnImportIED
            // 
            this.btnImportIED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImportIED.FlatAppearance.BorderSize = 0;
            this.btnImportIED.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnImportIED.Location = new System.Drawing.Point(240, 3);
            this.btnImportIED.Name = "btnImportIED";
            this.btnImportIED.Size = new System.Drawing.Size(68, 28);
            this.btnImportIED.TabIndex = 31;
            this.btnImportIED.Text = "&Import IED";
            this.btnImportIED.UseVisualStyleBackColor = true;
            this.btnImportIED.Click += new System.EventHandler(this.btnImportIED_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.grpMB);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpIED);
            this.splitContainer1.Panel2.Controls.Add(this.lvIEDList);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.lblIED);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 700);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 32;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnExportIED);
            this.panel1.Controls.Add(this.btnImportIED);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 393);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 45);
            this.panel1.TabIndex = 28;
            // 
            // ucMasterMODBUS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucMasterMODBUS";
            this.Size = new System.Drawing.Size(1050, 700);
            this.grpMB.ResumeLayout(false);
            this.grpMB.PerformLayout();
            this.grpIED.ResumeLayout(false);
            this.grpIED.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lvIEDList;
        public System.Windows.Forms.Label lblIED;
        private System.Windows.Forms.GroupBox grpMB;
        public System.Windows.Forms.TextBox txtMasterNo;
        private System.Windows.Forms.Label lblMN;
        public System.Windows.Forms.ComboBox cmbProtocolType;
        private System.Windows.Forms.Label lblPT;
        public System.Windows.Forms.CheckBox chkRun;
        public System.Windows.Forms.TextBox txtClockSyncInterval;
        public System.Windows.Forms.TextBox txtPollingInterval;
        private System.Windows.Forms.Label lblPI;
        private System.Windows.Forms.Label lblPTS;
        public System.Windows.Forms.TextBox txtFirmwareVer;
        private System.Windows.Forms.Label lblAFV;
        public System.Windows.Forms.TextBox txtPortNo;
        private System.Windows.Forms.Label lblPortNo;
        public System.Windows.Forms.TextBox txtDebug;
        private System.Windows.Forms.Label lblDebug;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.GroupBox grpIED;
        public System.Windows.Forms.CheckBox chkDR;
        public System.Windows.Forms.TextBox txtUnitID;
        private System.Windows.Forms.Label lblUI;
        public System.Windows.Forms.TextBox txtTimeOut;
        private System.Windows.Forms.Label lblTO;
        public System.Windows.Forms.TextBox txtRetries;
        private System.Windows.Forms.Label lblRetries;
        public System.Windows.Forms.TextBox txtDevice;
        private System.Windows.Forms.Label lblDevice;
        public System.Windows.Forms.TextBox txtRemoteIP;
        private System.Windows.Forms.Label lblRIP;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDS;
        public System.Windows.Forms.Button btnExportIED;
        public System.Windows.Forms.Button btnImportIED;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.TextBox txtMBDescription;
        private System.Windows.Forms.Label lblDesc;
        public System.Windows.Forms.TextBox txtRefreshInterval;
        private System.Windows.Forms.Label lblRI;
        public System.Windows.Forms.TextBox txtTCPPort;
        private System.Windows.Forms.Label lblTP;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
    }
}
