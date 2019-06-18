namespace OpenProPlusConfigurator
{
    partial class ucGroupIEC104
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
            this.components = new System.ComponentModel.Container();
            this.lvIEC104Slave = new System.Windows.Forms.ListView();
            this.grpIEC104 = new System.Windows.Forms.GroupBox();
            this.chkEventWithoutTime = new System.Windows.Forms.CheckBox();
            this.PbOn = new System.Windows.Forms.PictureBox();
            this.PbOff = new System.Windows.Forms.PictureBox();
            this.CmbPortName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSecRemote = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbEnable = new System.Windows.Forms.ComboBox();
            this.cmbDebug = new System.Windows.Forms.ComboBox();
            this.cmbIOASize = new System.Windows.Forms.ComboBox();
            this.cmbLocalIP = new System.Windows.Forms.ComboBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.txtSlaveNum = new System.Windows.Forms.TextBox();
            this.lblSN = new System.Windows.Forms.Label();
            this.lblDebug = new System.Windows.Forms.Label();
            this.txtFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lblAFV = new System.Windows.Forms.Label();
            this.txtEventQSize = new System.Windows.Forms.TextBox();
            this.lblEQS = new System.Windows.Forms.Label();
            this.txtK = new System.Windows.Forms.TextBox();
            this.lblK = new System.Windows.Forms.Label();
            this.txtW = new System.Windows.Forms.TextBox();
            this.lblW = new System.Windows.Forms.Label();
            this.txtT3 = new System.Windows.Forms.TextBox();
            this.lblT3 = new System.Windows.Forms.Label();
            this.txtT2 = new System.Windows.Forms.TextBox();
            this.lblT2 = new System.Windows.Forms.Label();
            this.txtT1 = new System.Windows.Forms.TextBox();
            this.lblT1 = new System.Windows.Forms.Label();
            this.txtT0 = new System.Windows.Forms.TextBox();
            this.lblT0 = new System.Windows.Forms.Label();
            this.txtCyclicInterval = new System.Windows.Forms.TextBox();
            this.lblCI = new System.Windows.Forms.Label();
            this.lblCOT = new System.Windows.Forms.Label();
            this.cmbCOTsize = new System.Windows.Forms.ComboBox();
            this.lblIOA = new System.Windows.Forms.Label();
            this.lblASDUsize = new System.Windows.Forms.Label();
            this.cmbASDUsize = new System.Windows.Forms.ComboBox();
            this.txtASDUaddress = new System.Windows.Forms.TextBox();
            this.lblASDU = new System.Windows.Forms.Label();
            this.txtRemoteIP = new System.Windows.Forms.TextBox();
            this.lblRIP = new System.Windows.Forms.Label();
            this.txtTCPPort = new System.Windows.Forms.TextBox();
            this.lblTPort = new System.Windows.Forms.Label();
            this.lblLIP = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.lblEnable = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExportINI = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.TpBtn = new System.Windows.Forms.ToolTip(this.components);
            this.grpIEC104.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbOff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvIEC104Slave
            // 
            this.lvIEC104Slave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvIEC104Slave.CheckBoxes = true;
            this.lvIEC104Slave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvIEC104Slave.FullRowSelect = true;
            this.lvIEC104Slave.Location = new System.Drawing.Point(0, 30);
            this.lvIEC104Slave.MultiSelect = false;
            this.lvIEC104Slave.Name = "lvIEC104Slave";
            this.lvIEC104Slave.Size = new System.Drawing.Size(1050, 620);
            this.lvIEC104Slave.TabIndex = 5;
            this.lvIEC104Slave.UseCompatibleStateImageBehavior = false;
            this.lvIEC104Slave.View = System.Windows.Forms.View.Details;
            this.lvIEC104Slave.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvIEC104Slave_DrawColumnHeader);
            this.lvIEC104Slave.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvIEC104Slave_DrawItem);
            this.lvIEC104Slave.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvIEC104Slave_DrawSubItem);
            this.lvIEC104Slave.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEC104Slave_ItemCheck);
            this.lvIEC104Slave.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvIEC104Slave_ItemChecked);
            this.lvIEC104Slave.DoubleClick += new System.EventHandler(this.lvIEC104Slave_DoubleClick);
            // 
            // grpIEC104
            // 
            this.grpIEC104.BackColor = System.Drawing.SystemColors.Control;
            this.grpIEC104.Controls.Add(this.chkEventWithoutTime);
            this.grpIEC104.Controls.Add(this.PbOn);
            this.grpIEC104.Controls.Add(this.PbOff);
            this.grpIEC104.Controls.Add(this.CmbPortName);
            this.grpIEC104.Controls.Add(this.label1);
            this.grpIEC104.Controls.Add(this.txtSecRemote);
            this.grpIEC104.Controls.Add(this.label3);
            this.grpIEC104.Controls.Add(this.cmbEnable);
            this.grpIEC104.Controls.Add(this.cmbDebug);
            this.grpIEC104.Controls.Add(this.cmbIOASize);
            this.grpIEC104.Controls.Add(this.cmbLocalIP);
            this.grpIEC104.Controls.Add(this.btnLast);
            this.grpIEC104.Controls.Add(this.btnNext);
            this.grpIEC104.Controls.Add(this.btnPrev);
            this.grpIEC104.Controls.Add(this.btnFirst);
            this.grpIEC104.Controls.Add(this.chkRun);
            this.grpIEC104.Controls.Add(this.txtSlaveNum);
            this.grpIEC104.Controls.Add(this.lblSN);
            this.grpIEC104.Controls.Add(this.lblDebug);
            this.grpIEC104.Controls.Add(this.txtFirmwareVersion);
            this.grpIEC104.Controls.Add(this.lblAFV);
            this.grpIEC104.Controls.Add(this.txtEventQSize);
            this.grpIEC104.Controls.Add(this.lblEQS);
            this.grpIEC104.Controls.Add(this.txtK);
            this.grpIEC104.Controls.Add(this.lblK);
            this.grpIEC104.Controls.Add(this.txtW);
            this.grpIEC104.Controls.Add(this.lblW);
            this.grpIEC104.Controls.Add(this.txtT3);
            this.grpIEC104.Controls.Add(this.lblT3);
            this.grpIEC104.Controls.Add(this.txtT2);
            this.grpIEC104.Controls.Add(this.lblT2);
            this.grpIEC104.Controls.Add(this.txtT1);
            this.grpIEC104.Controls.Add(this.lblT1);
            this.grpIEC104.Controls.Add(this.txtT0);
            this.grpIEC104.Controls.Add(this.lblT0);
            this.grpIEC104.Controls.Add(this.txtCyclicInterval);
            this.grpIEC104.Controls.Add(this.lblCI);
            this.grpIEC104.Controls.Add(this.lblCOT);
            this.grpIEC104.Controls.Add(this.cmbCOTsize);
            this.grpIEC104.Controls.Add(this.lblIOA);
            this.grpIEC104.Controls.Add(this.lblASDUsize);
            this.grpIEC104.Controls.Add(this.cmbASDUsize);
            this.grpIEC104.Controls.Add(this.txtASDUaddress);
            this.grpIEC104.Controls.Add(this.lblASDU);
            this.grpIEC104.Controls.Add(this.txtRemoteIP);
            this.grpIEC104.Controls.Add(this.lblRIP);
            this.grpIEC104.Controls.Add(this.txtTCPPort);
            this.grpIEC104.Controls.Add(this.lblTPort);
            this.grpIEC104.Controls.Add(this.lblLIP);
            this.grpIEC104.Controls.Add(this.lblHdrText);
            this.grpIEC104.Controls.Add(this.pbHdr);
            this.grpIEC104.Controls.Add(this.btnCancel);
            this.grpIEC104.Controls.Add(this.btnDone);
            this.grpIEC104.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpIEC104.Location = new System.Drawing.Point(317, 52);
            this.grpIEC104.Name = "grpIEC104";
            this.grpIEC104.Size = new System.Drawing.Size(319, 544);
            this.grpIEC104.TabIndex = 17;
            this.grpIEC104.TabStop = false;
            this.grpIEC104.Visible = false;
            this.grpIEC104.Enter += new System.EventHandler(this.grpIEC104_Enter);
            // 
            // chkEventWithoutTime
            // 
            this.chkEventWithoutTime.AutoSize = true;
            this.chkEventWithoutTime.Location = new System.Drawing.Point(168, 454);
            this.chkEventWithoutTime.Name = "chkEventWithoutTime";
            this.chkEventWithoutTime.Size = new System.Drawing.Size(120, 17);
            this.chkEventWithoutTime.TabIndex = 122;
            this.chkEventWithoutTime.Tag = "EventWithoutTime_YES_NO";
            this.chkEventWithoutTime.Text = "Event Without Time";
            this.chkEventWithoutTime.UseVisualStyleBackColor = true;
            // 
            // PbOn
            // 
            this.PbOn.AccessibleDescription = " ";
            this.PbOn.Cursor = System.Windows.Forms.Cursors.No;
            this.PbOn.Image = global::OpenProPlusConfigurator.Properties.Resources._switch;
            this.PbOn.Location = new System.Drawing.Point(280, 60);
            this.PbOn.Name = "PbOn";
            this.PbOn.Size = new System.Drawing.Size(18, 15);
            this.PbOn.TabIndex = 132;
            this.PbOn.TabStop = false;
            this.TpBtn.SetToolTip(this.PbOn, "Enable: Yes");
            // 
            // PbOff
            // 
            this.PbOff.AccessibleDescription = " ";
            this.PbOff.Cursor = System.Windows.Forms.Cursors.No;
            this.PbOff.Image = global::OpenProPlusConfigurator.Properties.Resources.switch__8_;
            this.PbOff.Location = new System.Drawing.Point(280, 60);
            this.PbOff.Name = "PbOff";
            this.PbOff.Size = new System.Drawing.Size(18, 15);
            this.PbOff.TabIndex = 131;
            this.PbOff.TabStop = false;
            this.TpBtn.SetToolTip(this.PbOff, "Enable: No");
            // 
            // CmbPortName
            // 
            this.CmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPortName.FormattingEnabled = true;
            this.CmbPortName.Location = new System.Drawing.Point(168, 57);
            this.CmbPortName.Name = "CmbPortName";
            this.CmbPortName.Size = new System.Drawing.Size(102, 21);
            this.CmbPortName.TabIndex = 128;
            this.CmbPortName.Tag = "PortName";
            this.CmbPortName.SelectedIndexChanged += new System.EventHandler(this.CmbPortName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 127;
            this.label1.Text = "Port Name";
            // 
            // txtSecRemote
            // 
            this.txtSecRemote.Location = new System.Drawing.Point(168, 130);
            this.txtSecRemote.Name = "txtSecRemote";
            this.txtSecRemote.Size = new System.Drawing.Size(130, 20);
            this.txtSecRemote.TabIndex = 126;
            this.txtSecRemote.Tag = "SecRemoteIP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 13);
            this.label3.TabIndex = 124;
            this.label3.Text = "Secondary Remote IP Address";
            // 
            // cmbEnable
            // 
            this.cmbEnable.BackColor = System.Drawing.SystemColors.Control;
            this.cmbEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbEnable.Enabled = false;
            this.cmbEnable.FormattingEnabled = true;
            this.cmbEnable.Location = new System.Drawing.Point(13, 477);
            this.cmbEnable.Name = "cmbEnable";
            this.cmbEnable.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbEnable.Size = new System.Drawing.Size(10, 15);
            this.cmbEnable.TabIndex = 123;
            this.cmbEnable.Tag = "Enable";
            this.cmbEnable.Visible = false;
            // 
            // cmbDebug
            // 
            this.cmbDebug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDebug.FormattingEnabled = true;
            this.cmbDebug.Location = new System.Drawing.Point(168, 425);
            this.cmbDebug.Name = "cmbDebug";
            this.cmbDebug.Size = new System.Drawing.Size(130, 21);
            this.cmbDebug.TabIndex = 91;
            this.cmbDebug.Tag = "DEBUG";
            // 
            // cmbIOASize
            // 
            this.cmbIOASize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIOASize.FormattingEnabled = true;
            this.cmbIOASize.Location = new System.Drawing.Point(168, 203);
            this.cmbIOASize.Name = "cmbIOASize";
            this.cmbIOASize.Size = new System.Drawing.Size(130, 21);
            this.cmbIOASize.TabIndex = 69;
            this.cmbIOASize.Tag = "IOASize";
            // 
            // cmbLocalIP
            // 
            this.cmbLocalIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocalIP.FormattingEnabled = true;
            this.cmbLocalIP.Location = new System.Drawing.Point(292, 30);
            this.cmbLocalIP.Name = "cmbLocalIP";
            this.cmbLocalIP.Size = new System.Drawing.Size(13, 21);
            this.cmbLocalIP.TabIndex = 115;
            this.cmbLocalIP.Tag = "LocalIP";
            this.cmbLocalIP.Visible = false;
            this.cmbLocalIP.SelectedIndexChanged += new System.EventHandler(this.cmbLocalIP_SelectedIndexChanged);
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(239, 513);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(59, 22);
            this.btnLast.TabIndex = 114;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(164, 513);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(59, 22);
            this.btnNext.TabIndex = 113;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(89, 513);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(59, 22);
            this.btnPrev.TabIndex = 112;
            this.btnPrev.Text = "<Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(14, 513);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 111;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Location = new System.Drawing.Point(14, 454);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 17);
            this.chkRun.TabIndex = 92;
            this.chkRun.Tag = "Run_YES_NO";
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // txtSlaveNum
            // 
            this.txtSlaveNum.Enabled = false;
            this.txtSlaveNum.Location = new System.Drawing.Point(168, 33);
            this.txtSlaveNum.Name = "txtSlaveNum";
            this.txtSlaveNum.Size = new System.Drawing.Size(130, 20);
            this.txtSlaveNum.TabIndex = 57;
            this.txtSlaveNum.Tag = "SlaveNum";
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Location = new System.Drawing.Point(14, 36);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(54, 13);
            this.lblSN.TabIndex = 56;
            this.lblSN.Text = "Slave No.";
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(14, 428);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(68, 13);
            this.lblDebug.TabIndex = 90;
            this.lblDebug.Text = "Debug Level";
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.Enabled = false;
            this.txtFirmwareVersion.Location = new System.Drawing.Point(168, 401);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.Size = new System.Drawing.Size(130, 20);
            this.txtFirmwareVersion.TabIndex = 89;
            this.txtFirmwareVersion.Tag = "AppFirmwareVersion";
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(14, 404);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(87, 13);
            this.lblAFV.TabIndex = 88;
            this.lblAFV.Text = "Firmware Version";
            // 
            // txtEventQSize
            // 
            this.txtEventQSize.Enabled = false;
            this.txtEventQSize.Location = new System.Drawing.Point(168, 377);
            this.txtEventQSize.Name = "txtEventQSize";
            this.txtEventQSize.Size = new System.Drawing.Size(130, 20);
            this.txtEventQSize.TabIndex = 87;
            this.txtEventQSize.Tag = "EventQSize";
            this.txtEventQSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEventQSize_KeyPress);
            // 
            // lblEQS
            // 
            this.lblEQS.AutoSize = true;
            this.lblEQS.Location = new System.Drawing.Point(14, 380);
            this.lblEQS.Name = "lblEQS";
            this.lblEQS.Size = new System.Drawing.Size(93, 13);
            this.lblEQS.TabIndex = 86;
            this.lblEQS.Text = "Event Queue Size";
            // 
            // txtK
            // 
            this.txtK.Location = new System.Drawing.Point(168, 329);
            this.txtK.Name = "txtK";
            this.txtK.Size = new System.Drawing.Size(130, 20);
            this.txtK.TabIndex = 83;
            this.txtK.Tag = "K";
            this.txtK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtK_KeyPress);
            // 
            // lblK
            // 
            this.lblK.AutoSize = true;
            this.lblK.Location = new System.Drawing.Point(14, 332);
            this.lblK.Name = "lblK";
            this.lblK.Size = new System.Drawing.Size(14, 13);
            this.lblK.TabIndex = 82;
            this.lblK.Text = "K";
            // 
            // txtW
            // 
            this.txtW.Location = new System.Drawing.Point(168, 305);
            this.txtW.Name = "txtW";
            this.txtW.Size = new System.Drawing.Size(130, 20);
            this.txtW.TabIndex = 81;
            this.txtW.Tag = "W";
            this.txtW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtW_KeyPress);
            // 
            // lblW
            // 
            this.lblW.AutoSize = true;
            this.lblW.Location = new System.Drawing.Point(14, 308);
            this.lblW.Name = "lblW";
            this.lblW.Size = new System.Drawing.Size(18, 13);
            this.lblW.TabIndex = 80;
            this.lblW.Text = "W";
            // 
            // txtT3
            // 
            this.txtT3.Location = new System.Drawing.Point(251, 278);
            this.txtT3.Name = "txtT3";
            this.txtT3.Size = new System.Drawing.Size(45, 20);
            this.txtT3.TabIndex = 79;
            this.txtT3.Tag = "T3";
            this.txtT3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtT3_KeyPress);
            // 
            // lblT3
            // 
            this.lblT3.AutoSize = true;
            this.lblT3.Location = new System.Drawing.Point(251, 259);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(46, 13);
            this.lblT3.TabIndex = 78;
            this.lblT3.Text = "T3 (sec)";
            // 
            // txtT2
            // 
            this.txtT2.Location = new System.Drawing.Point(172, 278);
            this.txtT2.Name = "txtT2";
            this.txtT2.Size = new System.Drawing.Size(45, 20);
            this.txtT2.TabIndex = 77;
            this.txtT2.Tag = "T2";
            this.txtT2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtT2_KeyPress);
            // 
            // lblT2
            // 
            this.lblT2.AutoSize = true;
            this.lblT2.Location = new System.Drawing.Point(172, 259);
            this.lblT2.Name = "lblT2";
            this.lblT2.Size = new System.Drawing.Size(46, 13);
            this.lblT2.TabIndex = 76;
            this.lblT2.Text = "T2 (sec)";
            // 
            // txtT1
            // 
            this.txtT1.Location = new System.Drawing.Point(93, 278);
            this.txtT1.Name = "txtT1";
            this.txtT1.Size = new System.Drawing.Size(45, 20);
            this.txtT1.TabIndex = 75;
            this.txtT1.Tag = "T1";
            this.txtT1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtT1_KeyPress);
            // 
            // lblT1
            // 
            this.lblT1.AutoSize = true;
            this.lblT1.Location = new System.Drawing.Point(93, 259);
            this.lblT1.Name = "lblT1";
            this.lblT1.Size = new System.Drawing.Size(46, 13);
            this.lblT1.TabIndex = 74;
            this.lblT1.Text = "T1 (sec)";
            // 
            // txtT0
            // 
            this.txtT0.Location = new System.Drawing.Point(14, 278);
            this.txtT0.Name = "txtT0";
            this.txtT0.Size = new System.Drawing.Size(45, 20);
            this.txtT0.TabIndex = 73;
            this.txtT0.Tag = "T0";
            this.txtT0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtT0_KeyPress);
            // 
            // lblT0
            // 
            this.lblT0.AutoSize = true;
            this.lblT0.Location = new System.Drawing.Point(14, 259);
            this.lblT0.Name = "lblT0";
            this.lblT0.Size = new System.Drawing.Size(46, 13);
            this.lblT0.TabIndex = 72;
            this.lblT0.Text = "T0 (sec)";
            // 
            // txtCyclicInterval
            // 
            this.txtCyclicInterval.Location = new System.Drawing.Point(168, 353);
            this.txtCyclicInterval.Name = "txtCyclicInterval";
            this.txtCyclicInterval.Size = new System.Drawing.Size(130, 20);
            this.txtCyclicInterval.TabIndex = 85;
            this.txtCyclicInterval.Tag = "CyclicInterval";
            this.txtCyclicInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCyclicInterval_KeyPress);
            // 
            // lblCI
            // 
            this.lblCI.AutoSize = true;
            this.lblCI.Location = new System.Drawing.Point(14, 356);
            this.lblCI.Name = "lblCI";
            this.lblCI.Size = new System.Drawing.Size(99, 13);
            this.lblCI.TabIndex = 84;
            this.lblCI.Text = "Cyclic Interval (sec)";
            // 
            // lblCOT
            // 
            this.lblCOT.AutoSize = true;
            this.lblCOT.Location = new System.Drawing.Point(14, 231);
            this.lblCOT.Name = "lblCOT";
            this.lblCOT.Size = new System.Drawing.Size(52, 13);
            this.lblCOT.TabIndex = 70;
            this.lblCOT.Text = "COT Size";
            // 
            // cmbCOTsize
            // 
            this.cmbCOTsize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCOTsize.FormattingEnabled = true;
            this.cmbCOTsize.Location = new System.Drawing.Point(168, 228);
            this.cmbCOTsize.Name = "cmbCOTsize";
            this.cmbCOTsize.Size = new System.Drawing.Size(130, 21);
            this.cmbCOTsize.TabIndex = 71;
            this.cmbCOTsize.Tag = "COTSize";
            // 
            // lblIOA
            // 
            this.lblIOA.AutoSize = true;
            this.lblIOA.Location = new System.Drawing.Point(14, 206);
            this.lblIOA.Name = "lblIOA";
            this.lblIOA.Size = new System.Drawing.Size(48, 13);
            this.lblIOA.TabIndex = 68;
            this.lblIOA.Text = "IOA Size";
            this.lblIOA.Click += new System.EventHandler(this.lblIOA_Click);
            // 
            // lblASDUsize
            // 
            this.lblASDUsize.AutoSize = true;
            this.lblASDUsize.Location = new System.Drawing.Point(14, 181);
            this.lblASDUsize.Name = "lblASDUsize";
            this.lblASDUsize.Size = new System.Drawing.Size(60, 13);
            this.lblASDUsize.TabIndex = 66;
            this.lblASDUsize.Text = "ASDU Size";
            // 
            // cmbASDUsize
            // 
            this.cmbASDUsize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbASDUsize.FormattingEnabled = true;
            this.cmbASDUsize.Location = new System.Drawing.Point(168, 178);
            this.cmbASDUsize.Name = "cmbASDUsize";
            this.cmbASDUsize.Size = new System.Drawing.Size(130, 21);
            this.cmbASDUsize.TabIndex = 67;
            this.cmbASDUsize.Tag = "ASDUSize";
            // 
            // txtASDUaddress
            // 
            this.txtASDUaddress.Location = new System.Drawing.Point(168, 154);
            this.txtASDUaddress.Name = "txtASDUaddress";
            this.txtASDUaddress.Size = new System.Drawing.Size(130, 20);
            this.txtASDUaddress.TabIndex = 65;
            this.txtASDUaddress.Tag = "ASDUAddress";
            this.txtASDUaddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtASDUaddress_KeyPress);
            // 
            // lblASDU
            // 
            this.lblASDU.AutoSize = true;
            this.lblASDU.Location = new System.Drawing.Point(14, 157);
            this.lblASDU.Name = "lblASDU";
            this.lblASDU.Size = new System.Drawing.Size(78, 13);
            this.lblASDU.TabIndex = 64;
            this.lblASDU.Text = "ASDU Address";
            // 
            // txtRemoteIP
            // 
            this.txtRemoteIP.Location = new System.Drawing.Point(168, 106);
            this.txtRemoteIP.Name = "txtRemoteIP";
            this.txtRemoteIP.Size = new System.Drawing.Size(130, 20);
            this.txtRemoteIP.TabIndex = 63;
            this.txtRemoteIP.Tag = "RemoteIP";
            this.txtRemoteIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemoteIP_KeyPress);
            // 
            // lblRIP
            // 
            this.lblRIP.AutoSize = true;
            this.lblRIP.Location = new System.Drawing.Point(14, 109);
            this.lblRIP.Name = "lblRIP";
            this.lblRIP.Size = new System.Drawing.Size(135, 13);
            this.lblRIP.TabIndex = 62;
            this.lblRIP.Text = "Primary Remote IP Address";
            // 
            // txtTCPPort
            // 
            this.txtTCPPort.Location = new System.Drawing.Point(168, 82);
            this.txtTCPPort.Name = "txtTCPPort";
            this.txtTCPPort.Size = new System.Drawing.Size(130, 20);
            this.txtTCPPort.TabIndex = 61;
            this.txtTCPPort.Tag = "TcpPort";
            this.txtTCPPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTCPPort_KeyPress);
            // 
            // lblTPort
            // 
            this.lblTPort.AutoSize = true;
            this.lblTPort.Location = new System.Drawing.Point(14, 85);
            this.lblTPort.Name = "lblTPort";
            this.lblTPort.Size = new System.Drawing.Size(50, 13);
            this.lblTPort.TabIndex = 60;
            this.lblTPort.Text = "TCP Port";
            // 
            // lblLIP
            // 
            this.lblLIP.AutoSize = true;
            this.lblLIP.Location = new System.Drawing.Point(14, 56);
            this.lblLIP.Name = "lblLIP";
            this.lblLIP.Size = new System.Drawing.Size(46, 13);
            this.lblLIP.TabIndex = 58;
            this.lblLIP.Text = "Local IP";
            this.lblLIP.Visible = false;
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(84, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "IEC104 Slave";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(319, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(230, 477);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 94;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(151, 477);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 93;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // lblEnable
            // 
            this.lblEnable.AutoSize = true;
            this.lblEnable.Location = new System.Drawing.Point(593, 118);
            this.lblEnable.Name = "lblEnable";
            this.lblEnable.Size = new System.Drawing.Size(43, 13);
            this.lblEnable.TabIndex = 121;
            this.lblEnable.Text = "Enable:";
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(80, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 28);
            this.btnDelete.TabIndex = 20;
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
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExportINI
            // 
            this.btnExportINI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExportINI.FlatAppearance.BorderSize = 0;
            this.btnExportINI.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportINI.Location = new System.Drawing.Point(158, 9);
            this.btnExportINI.Name = "btnExportINI";
            this.btnExportINI.Size = new System.Drawing.Size(68, 28);
            this.btnExportINI.TabIndex = 21;
            this.btnExportINI.Text = "E&xport INI";
            this.btnExportINI.UseVisualStyleBackColor = true;
            this.btnExportINI.Click += new System.EventHandler(this.btnExportINI_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExportINI);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 650);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 50);
            this.panel1.TabIndex = 22;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1050, 30);
            this.panel2.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "IEC104 Slave Configuration :";
            // 
            // ucGroupIEC104
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpIEC104);
            this.Controls.Add(this.lvIEC104Slave);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblEnable);
            this.Name = "ucGroupIEC104";
            this.Size = new System.Drawing.Size(1050, 700);
            this.Load += new System.EventHandler(this.ucGroupIEC104_Load);
            this.grpIEC104.ResumeLayout(false);
            this.grpIEC104.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbOff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListView lvIEC104Slave;
        public System.Windows.Forms.GroupBox grpIEC104;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblDebug;
        public System.Windows.Forms.TextBox txtFirmwareVersion;
        private System.Windows.Forms.Label lblAFV;
        public System.Windows.Forms.TextBox txtEventQSize;
        private System.Windows.Forms.Label lblEQS;
        public System.Windows.Forms.TextBox txtK;
        private System.Windows.Forms.Label lblK;
        public System.Windows.Forms.TextBox txtW;
        private System.Windows.Forms.Label lblW;
        public System.Windows.Forms.TextBox txtT3;
        private System.Windows.Forms.Label lblT3;
        public System.Windows.Forms.TextBox txtT2;
        private System.Windows.Forms.Label lblT2;
        public System.Windows.Forms.TextBox txtT1;
        private System.Windows.Forms.Label lblT1;
        public System.Windows.Forms.TextBox txtT0;
        private System.Windows.Forms.Label lblT0;
        public System.Windows.Forms.TextBox txtCyclicInterval;
        private System.Windows.Forms.Label lblCI;
        private System.Windows.Forms.Label lblCOT;
        public System.Windows.Forms.ComboBox cmbCOTsize;
        private System.Windows.Forms.Label lblIOA;
        private System.Windows.Forms.Label lblASDUsize;
        public System.Windows.Forms.ComboBox cmbASDUsize;
        public System.Windows.Forms.TextBox txtASDUaddress;
        private System.Windows.Forms.Label lblASDU;
        public System.Windows.Forms.TextBox txtRemoteIP;
        private System.Windows.Forms.Label lblRIP;
        public System.Windows.Forms.TextBox txtTCPPort;
        private System.Windows.Forms.Label lblTPort;
        private System.Windows.Forms.Label lblLIP;
        public System.Windows.Forms.TextBox txtSlaveNum;
        private System.Windows.Forms.Label lblSN;
        public System.Windows.Forms.Button btnExportINI;
        public System.Windows.Forms.CheckBox chkRun;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.ComboBox cmbLocalIP;
        public System.Windows.Forms.ComboBox cmbDebug;
        public System.Windows.Forms.ComboBox cmbIOASize;
        public System.Windows.Forms.Label lblEnable;
        public System.Windows.Forms.ComboBox cmbEnable;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtSecRemote;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox CmbPortName;
        private System.Windows.Forms.ToolTip TpBtn;
        public System.Windows.Forms.PictureBox PbOff;
        public System.Windows.Forms.PictureBox PbOn;
        public System.Windows.Forms.CheckBox chkEventWithoutTime;
    }
}
