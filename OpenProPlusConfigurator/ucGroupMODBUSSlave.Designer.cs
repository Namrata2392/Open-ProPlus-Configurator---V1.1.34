namespace OpenProPlusConfigurator
{
    partial class ucGroupMODBUSSlave
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
            this.lvMODBUSSlave = new System.Windows.Forms.ListView();
            this.lblMSC = new System.Windows.Forms.Label();
            this.grpMODBUSSlave = new System.Windows.Forms.GroupBox();
            this.CmbPortName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PbOff = new System.Windows.Forms.PictureBox();
            this.PbOn = new System.Windows.Forms.PictureBox();
            this.txtSecRemoteIP = new System.Windows.Forms.TextBox();
            this.lblSecRemoteIP = new System.Windows.Forms.Label();
            this.lblLIP = new System.Windows.Forms.Label();
            this.cmbLocalIP = new System.Windows.Forms.ComboBox();
            this.txtUnitID = new System.Windows.Forms.TextBox();
            this.lblUI = new System.Windows.Forms.Label();
            this.cmbPortNo = new System.Windows.Forms.ComboBox();
            this.lblPN = new System.Windows.Forms.Label();
            this.cmbDebug = new System.Windows.Forms.ComboBox();
            this.cmbProtocolType = new System.Windows.Forms.ComboBox();
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
            this.txtRemoteIP = new System.Windows.Forms.TextBox();
            this.lblRIP = new System.Windows.Forms.Label();
            this.txtTCPPort = new System.Windows.Forms.TextBox();
            this.lblTPort = new System.Windows.Forms.Label();
            this.lblPT = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.cmbEnable = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExportINI = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TpBtn = new System.Windows.Forms.ToolTip(this.components);
            this.grpMODBUSSlave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbOff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMODBUSSlave
            // 
            this.lvMODBUSSlave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvMODBUSSlave.CheckBoxes = true;
            this.lvMODBUSSlave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMODBUSSlave.FullRowSelect = true;
            this.lvMODBUSSlave.Location = new System.Drawing.Point(0, 30);
            this.lvMODBUSSlave.MultiSelect = false;
            this.lvMODBUSSlave.Name = "lvMODBUSSlave";
            this.lvMODBUSSlave.Size = new System.Drawing.Size(1050, 620);
            this.lvMODBUSSlave.TabIndex = 5;
            this.lvMODBUSSlave.UseCompatibleStateImageBehavior = false;
            this.lvMODBUSSlave.View = System.Windows.Forms.View.Details;
            this.lvMODBUSSlave.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvMODBUSSlave_ItemCheck);
            this.lvMODBUSSlave.DoubleClick += new System.EventHandler(this.lvMODBUSSlave_DoubleClick);
            // 
            // lblMSC
            // 
            this.lblMSC.AutoSize = true;
            this.lblMSC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMSC.Location = new System.Drawing.Point(3, 3);
            this.lblMSC.Name = "lblMSC";
            this.lblMSC.Size = new System.Drawing.Size(200, 15);
            this.lblMSC.TabIndex = 4;
            this.lblMSC.Text = "MODBUS Slave Configuration:";
            // 
            // grpMODBUSSlave
            // 
            this.grpMODBUSSlave.BackColor = System.Drawing.SystemColors.Control;
            this.grpMODBUSSlave.Controls.Add(this.CmbPortName);
            this.grpMODBUSSlave.Controls.Add(this.label1);
            this.grpMODBUSSlave.Controls.Add(this.PbOff);
            this.grpMODBUSSlave.Controls.Add(this.PbOn);
            this.grpMODBUSSlave.Controls.Add(this.txtSecRemoteIP);
            this.grpMODBUSSlave.Controls.Add(this.lblSecRemoteIP);
            this.grpMODBUSSlave.Controls.Add(this.lblLIP);
            this.grpMODBUSSlave.Controls.Add(this.cmbLocalIP);
            this.grpMODBUSSlave.Controls.Add(this.txtUnitID);
            this.grpMODBUSSlave.Controls.Add(this.lblUI);
            this.grpMODBUSSlave.Controls.Add(this.cmbPortNo);
            this.grpMODBUSSlave.Controls.Add(this.lblPN);
            this.grpMODBUSSlave.Controls.Add(this.cmbDebug);
            this.grpMODBUSSlave.Controls.Add(this.cmbProtocolType);
            this.grpMODBUSSlave.Controls.Add(this.btnLast);
            this.grpMODBUSSlave.Controls.Add(this.btnNext);
            this.grpMODBUSSlave.Controls.Add(this.btnPrev);
            this.grpMODBUSSlave.Controls.Add(this.btnFirst);
            this.grpMODBUSSlave.Controls.Add(this.chkRun);
            this.grpMODBUSSlave.Controls.Add(this.txtSlaveNum);
            this.grpMODBUSSlave.Controls.Add(this.lblSN);
            this.grpMODBUSSlave.Controls.Add(this.lblDebug);
            this.grpMODBUSSlave.Controls.Add(this.txtFirmwareVersion);
            this.grpMODBUSSlave.Controls.Add(this.lblAFV);
            this.grpMODBUSSlave.Controls.Add(this.txtEventQSize);
            this.grpMODBUSSlave.Controls.Add(this.lblEQS);
            this.grpMODBUSSlave.Controls.Add(this.txtRemoteIP);
            this.grpMODBUSSlave.Controls.Add(this.lblRIP);
            this.grpMODBUSSlave.Controls.Add(this.txtTCPPort);
            this.grpMODBUSSlave.Controls.Add(this.lblTPort);
            this.grpMODBUSSlave.Controls.Add(this.lblPT);
            this.grpMODBUSSlave.Controls.Add(this.lblHdrText);
            this.grpMODBUSSlave.Controls.Add(this.pbHdr);
            this.grpMODBUSSlave.Controls.Add(this.btnCancel);
            this.grpMODBUSSlave.Controls.Add(this.btnDone);
            this.grpMODBUSSlave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpMODBUSSlave.Location = new System.Drawing.Point(309, 78);
            this.grpMODBUSSlave.Name = "grpMODBUSSlave";
            this.grpMODBUSSlave.Size = new System.Drawing.Size(343, 373);
            this.grpMODBUSSlave.TabIndex = 17;
            this.grpMODBUSSlave.TabStop = false;
            this.grpMODBUSSlave.Visible = false;
            this.grpMODBUSSlave.Enter += new System.EventHandler(this.grpMODBUSSlave_Enter);
            // 
            // CmbPortName
            // 
            this.CmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPortName.FormattingEnabled = true;
            this.CmbPortName.Location = new System.Drawing.Point(165, 105);
            this.CmbPortName.Name = "CmbPortName";
            this.CmbPortName.Size = new System.Drawing.Size(133, 21);
            this.CmbPortName.TabIndex = 136;
            this.CmbPortName.Tag = "PortName";
            this.CmbPortName.SelectedIndexChanged += new System.EventHandler(this.CmbPortName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 135;
            this.label1.Text = "Port Name";
            // 
            // PbOff
            // 
            this.PbOff.AccessibleDescription = " ";
            this.PbOff.Cursor = System.Windows.Forms.Cursors.No;
            this.PbOff.Image = global::OpenProPlusConfigurator.Properties.Resources.switch__8_;
            this.PbOff.Location = new System.Drawing.Point(306, 107);
            this.PbOff.Name = "PbOff";
            this.PbOff.Size = new System.Drawing.Size(18, 15);
            this.PbOff.TabIndex = 134;
            this.PbOff.TabStop = false;
            this.TpBtn.SetToolTip(this.PbOff, "Enable: No");
            // 
            // PbOn
            // 
            this.PbOn.AccessibleDescription = " ";
            this.PbOn.Cursor = System.Windows.Forms.Cursors.No;
            this.PbOn.Image = global::OpenProPlusConfigurator.Properties.Resources._switch;
            this.PbOn.Location = new System.Drawing.Point(306, 107);
            this.PbOn.Name = "PbOn";
            this.PbOn.Size = new System.Drawing.Size(18, 15);
            this.PbOn.TabIndex = 133;
            this.PbOn.TabStop = false;
            this.TpBtn.SetToolTip(this.PbOn, "Enable: Yes");
            // 
            // txtSecRemoteIP
            // 
            this.txtSecRemoteIP.Location = new System.Drawing.Point(165, 156);
            this.txtSecRemoteIP.Name = "txtSecRemoteIP";
            this.txtSecRemoteIP.Size = new System.Drawing.Size(159, 20);
            this.txtSecRemoteIP.TabIndex = 124;
            this.txtSecRemoteIP.Tag = "SecRemoteIP";
            // 
            // lblSecRemoteIP
            // 
            this.lblSecRemoteIP.AutoSize = true;
            this.lblSecRemoteIP.Location = new System.Drawing.Point(12, 159);
            this.lblSecRemoteIP.Name = "lblSecRemoteIP";
            this.lblSecRemoteIP.Size = new System.Drawing.Size(152, 13);
            this.lblSecRemoteIP.TabIndex = 123;
            this.lblSecRemoteIP.Text = "Secondary Remote IP Address";
            // 
            // lblLIP
            // 
            this.lblLIP.AutoSize = true;
            this.lblLIP.Location = new System.Drawing.Point(12, 110);
            this.lblLIP.Name = "lblLIP";
            this.lblLIP.Size = new System.Drawing.Size(87, 13);
            this.lblLIP.TabIndex = 117;
            this.lblLIP.Text = "Local IP Address";
            this.lblLIP.Visible = false;
            // 
            // cmbLocalIP
            // 
            this.cmbLocalIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocalIP.FormattingEnabled = true;
            this.cmbLocalIP.Location = new System.Drawing.Point(367, 80);
            this.cmbLocalIP.Name = "cmbLocalIP";
            this.cmbLocalIP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbLocalIP.Size = new System.Drawing.Size(159, 21);
            this.cmbLocalIP.TabIndex = 116;
            this.cmbLocalIP.Tag = "LocalIP";
            this.cmbLocalIP.Visible = false;
            this.cmbLocalIP.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbLocalIP_DrawItem);
            this.cmbLocalIP.SelectedIndexChanged += new System.EventHandler(this.cmbLocalIP_SelectedIndexChanged);
            this.cmbLocalIP.SelectedValueChanged += new System.EventHandler(this.cmbLocalIP_SelectedValueChanged);
            // 
            // txtUnitID
            // 
            this.txtUnitID.Location = new System.Drawing.Point(165, 206);
            this.txtUnitID.Name = "txtUnitID";
            this.txtUnitID.Size = new System.Drawing.Size(159, 20);
            this.txtUnitID.TabIndex = 67;
            this.txtUnitID.Tag = "UnitID";
            this.txtUnitID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnitID_KeyPress);
            // 
            // lblUI
            // 
            this.lblUI.AutoSize = true;
            this.lblUI.Location = new System.Drawing.Point(12, 209);
            this.lblUI.Name = "lblUI";
            this.lblUI.Size = new System.Drawing.Size(40, 13);
            this.lblUI.TabIndex = 66;
            this.lblUI.Text = "Unit ID";
            // 
            // cmbPortNo
            // 
            this.cmbPortNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortNo.FormattingEnabled = true;
            this.cmbPortNo.Location = new System.Drawing.Point(165, 79);
            this.cmbPortNo.Name = "cmbPortNo";
            this.cmbPortNo.Size = new System.Drawing.Size(159, 21);
            this.cmbPortNo.TabIndex = 61;
            this.cmbPortNo.Tag = "PortNum";
            this.cmbPortNo.SelectedIndexChanged += new System.EventHandler(this.cmbPortNo_SelectedIndexChanged);
            // 
            // lblPN
            // 
            this.lblPN.AutoSize = true;
            this.lblPN.Location = new System.Drawing.Point(12, 80);
            this.lblPN.Name = "lblPN";
            this.lblPN.Size = new System.Drawing.Size(46, 13);
            this.lblPN.TabIndex = 60;
            this.lblPN.Text = "Port No.";
            // 
            // cmbDebug
            // 
            this.cmbDebug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDebug.FormattingEnabled = true;
            this.cmbDebug.Location = new System.Drawing.Point(165, 281);
            this.cmbDebug.Name = "cmbDebug";
            this.cmbDebug.Size = new System.Drawing.Size(159, 21);
            this.cmbDebug.TabIndex = 73;
            this.cmbDebug.Tag = "DEBUG";
            // 
            // cmbProtocolType
            // 
            this.cmbProtocolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProtocolType.FormattingEnabled = true;
            this.cmbProtocolType.Location = new System.Drawing.Point(165, 53);
            this.cmbProtocolType.Name = "cmbProtocolType";
            this.cmbProtocolType.Size = new System.Drawing.Size(159, 21);
            this.cmbProtocolType.TabIndex = 59;
            this.cmbProtocolType.Tag = "ProtocolType";
            this.cmbProtocolType.SelectedIndexChanged += new System.EventHandler(this.cmbProtocolType_SelectedIndexChanged);
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(264, 345);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(59, 22);
            this.btnLast.TabIndex = 80;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(180, 345);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(59, 22);
            this.btnNext.TabIndex = 79;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(96, 345);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(59, 22);
            this.btnPrev.TabIndex = 78;
            this.btnPrev.Text = "<Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(12, 345);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 77;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Location = new System.Drawing.Point(12, 318);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 17);
            this.chkRun.TabIndex = 74;
            this.chkRun.Tag = "Run_YES_NO";
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // txtSlaveNum
            // 
            this.txtSlaveNum.Enabled = false;
            this.txtSlaveNum.Location = new System.Drawing.Point(165, 28);
            this.txtSlaveNum.Name = "txtSlaveNum";
            this.txtSlaveNum.Size = new System.Drawing.Size(159, 20);
            this.txtSlaveNum.TabIndex = 57;
            this.txtSlaveNum.Tag = "SlaveNum";
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Location = new System.Drawing.Point(12, 31);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(54, 13);
            this.lblSN.TabIndex = 56;
            this.lblSN.Text = "Slave No.";
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(12, 284);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(68, 13);
            this.lblDebug.TabIndex = 72;
            this.lblDebug.Text = "Debug Level";
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.Enabled = false;
            this.txtFirmwareVersion.Location = new System.Drawing.Point(165, 256);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.Size = new System.Drawing.Size(159, 20);
            this.txtFirmwareVersion.TabIndex = 71;
            this.txtFirmwareVersion.Tag = "AppFirmwareVersion";
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(12, 259);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(87, 13);
            this.lblAFV.TabIndex = 70;
            this.lblAFV.Text = "Firmware Version";
            // 
            // txtEventQSize
            // 
            this.txtEventQSize.Enabled = false;
            this.txtEventQSize.Location = new System.Drawing.Point(165, 231);
            this.txtEventQSize.Name = "txtEventQSize";
            this.txtEventQSize.Size = new System.Drawing.Size(159, 20);
            this.txtEventQSize.TabIndex = 69;
            this.txtEventQSize.Tag = "EventQSize";
            this.txtEventQSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEventQSize_KeyPress);
            // 
            // lblEQS
            // 
            this.lblEQS.AutoSize = true;
            this.lblEQS.Location = new System.Drawing.Point(12, 234);
            this.lblEQS.Name = "lblEQS";
            this.lblEQS.Size = new System.Drawing.Size(93, 13);
            this.lblEQS.TabIndex = 68;
            this.lblEQS.Text = "Event Queue Size";
            // 
            // txtRemoteIP
            // 
            this.txtRemoteIP.Location = new System.Drawing.Point(165, 131);
            this.txtRemoteIP.Name = "txtRemoteIP";
            this.txtRemoteIP.Size = new System.Drawing.Size(159, 20);
            this.txtRemoteIP.TabIndex = 65;
            this.txtRemoteIP.Tag = "RemoteIP";
            this.txtRemoteIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemoteIP_KeyPress);
            // 
            // lblRIP
            // 
            this.lblRIP.AutoSize = true;
            this.lblRIP.Location = new System.Drawing.Point(12, 134);
            this.lblRIP.Name = "lblRIP";
            this.lblRIP.Size = new System.Drawing.Size(135, 13);
            this.lblRIP.TabIndex = 64;
            this.lblRIP.Text = "Primary Remote IP Address";
            // 
            // txtTCPPort
            // 
            this.txtTCPPort.Location = new System.Drawing.Point(165, 181);
            this.txtTCPPort.Name = "txtTCPPort";
            this.txtTCPPort.Size = new System.Drawing.Size(159, 20);
            this.txtTCPPort.TabIndex = 63;
            this.txtTCPPort.Tag = "TcpPort";
            this.txtTCPPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTCPPort_KeyPress);
            // 
            // lblTPort
            // 
            this.lblTPort.AutoSize = true;
            this.lblTPort.Location = new System.Drawing.Point(12, 184);
            this.lblTPort.Name = "lblTPort";
            this.lblTPort.Size = new System.Drawing.Size(50, 13);
            this.lblTPort.TabIndex = 62;
            this.lblTPort.Text = "TCP Port";
            // 
            // lblPT
            // 
            this.lblPT.AutoSize = true;
            this.lblPT.Location = new System.Drawing.Point(12, 56);
            this.lblPT.Name = "lblPT";
            this.lblPT.Size = new System.Drawing.Size(73, 13);
            this.lblPT.TabIndex = 58;
            this.lblPT.Text = "Protocol Type";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(96, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "MODBUS Slave";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(349, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(256, 312);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 76;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(165, 312);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 75;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // cmbEnable
            // 
            this.cmbEnable.BackColor = System.Drawing.SystemColors.Control;
            this.cmbEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbEnable.Enabled = false;
            this.cmbEnable.FormattingEnabled = true;
            this.cmbEnable.Location = new System.Drawing.Point(639, 183);
            this.cmbEnable.Name = "cmbEnable";
            this.cmbEnable.Size = new System.Drawing.Size(38, 23);
            this.cmbEnable.TabIndex = 119;
            this.cmbEnable.Tag = "Enable";
            this.cmbEnable.Visible = false;
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
            this.btnExportINI.Location = new System.Drawing.Point(160, 9);
            this.btnExportINI.Name = "btnExportINI";
            this.btnExportINI.Size = new System.Drawing.Size(68, 28);
            this.btnExportINI.TabIndex = 21;
            this.btnExportINI.Text = "E&xport INI";
            this.btnExportINI.UseVisualStyleBackColor = true;
            this.btnExportINI.Visible = false;
            this.btnExportINI.Click += new System.EventHandler(this.btnExportINI_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMSC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 30);
            this.panel1.TabIndex = 22;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExportINI);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 650);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1050, 50);
            this.panel2.TabIndex = 23;
            // 
            // ucGroupMODBUSSlave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMODBUSSlave);
            this.Controls.Add(this.lvMODBUSSlave);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbEnable);
            this.Name = "ucGroupMODBUSSlave";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Size = new System.Drawing.Size(1050, 700);
            this.Load += new System.EventHandler(this.ucGroupMODBUSSlave_Load);
            this.grpMODBUSSlave.ResumeLayout(false);
            this.grpMODBUSSlave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbOff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lvMODBUSSlave;
        private System.Windows.Forms.Label lblMSC;
        public System.Windows.Forms.GroupBox grpMODBUSSlave;
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
        public System.Windows.Forms.TextBox txtRemoteIP;
        private System.Windows.Forms.Label lblRIP;
        public System.Windows.Forms.TextBox txtTCPPort;
        private System.Windows.Forms.Label lblTPort;
        private System.Windows.Forms.Label lblPT;
        public System.Windows.Forms.TextBox txtSlaveNum;
        private System.Windows.Forms.Label lblSN;
        public System.Windows.Forms.Button btnExportINI;
        public System.Windows.Forms.CheckBox chkRun;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.ComboBox cmbProtocolType;
        public System.Windows.Forms.ComboBox cmbDebug;
        public System.Windows.Forms.ComboBox cmbPortNo;
        public System.Windows.Forms.Label lblPN;
        public System.Windows.Forms.TextBox txtUnitID;
        private System.Windows.Forms.Label lblUI;
        public System.Windows.Forms.ComboBox cmbLocalIP;
        private System.Windows.Forms.Label lblLIP;
        public System.Windows.Forms.ComboBox cmbEnable;
        public System.Windows.Forms.TextBox txtSecRemoteIP;
        private System.Windows.Forms.Label lblSecRemoteIP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.PictureBox PbOn;
        public System.Windows.Forms.PictureBox PbOff;
        public System.Windows.Forms.ComboBox CmbPortName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip TpBtn;
    }
}
