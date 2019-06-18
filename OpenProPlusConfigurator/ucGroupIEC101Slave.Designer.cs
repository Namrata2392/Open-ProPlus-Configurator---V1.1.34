namespace OpenProPlusConfigurator
{
    partial class ucGroupIEC101Slave
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
            this.lblISC = new System.Windows.Forms.Label();
            this.lvIEC101Slave = new System.Windows.Forms.ListView();
            this.grpIEC101 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CmbLinkAddSize = new System.Windows.Forms.ComboBox();
            this.txtLinkAdd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.cmbDebug = new System.Windows.Forms.ComboBox();
            this.cmbIOASize = new System.Windows.Forms.ComboBox();
            this.cmbPortNo = new System.Windows.Forms.ComboBox();
            this.txtSlaveNum = new System.Windows.Forms.TextBox();
            this.lblSN = new System.Windows.Forms.Label();
            this.lblDebug = new System.Windows.Forms.Label();
            this.txtFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lblAFV = new System.Windows.Forms.Label();
            this.txtEventQSize = new System.Windows.Forms.TextBox();
            this.lblEQS = new System.Windows.Forms.Label();
            this.txtCyclicInterval = new System.Windows.Forms.TextBox();
            this.lblCI = new System.Windows.Forms.Label();
            this.lblCOT = new System.Windows.Forms.Label();
            this.cmbCOTsize = new System.Windows.Forms.ComboBox();
            this.lblIOA = new System.Windows.Forms.Label();
            this.lblASDUsize = new System.Windows.Forms.Label();
            this.cmbASDUsize = new System.Windows.Forms.ComboBox();
            this.txtASDUaddress = new System.Windows.Forms.TextBox();
            this.lblASDU = new System.Windows.Forms.Label();
            this.lblLIP = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnexportIEC101INI = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpIEC101.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblISC
            // 
            this.lblISC.AutoSize = true;
            this.lblISC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblISC.Location = new System.Drawing.Point(2, 0);
            this.lblISC.Name = "lblISC";
            this.lblISC.Size = new System.Drawing.Size(186, 15);
            this.lblISC.TabIndex = 5;
            this.lblISC.Text = "IEC101 Slave Configuration:";
            // 
            // lvIEC101Slave
            // 
            this.lvIEC101Slave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvIEC101Slave.CheckBoxes = true;
            this.lvIEC101Slave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvIEC101Slave.FullRowSelect = true;
            this.lvIEC101Slave.Location = new System.Drawing.Point(0, 30);
            this.lvIEC101Slave.MultiSelect = false;
            this.lvIEC101Slave.Name = "lvIEC101Slave";
            this.lvIEC101Slave.Size = new System.Drawing.Size(1050, 624);
            this.lvIEC101Slave.TabIndex = 6;
            this.lvIEC101Slave.UseCompatibleStateImageBehavior = false;
            this.lvIEC101Slave.View = System.Windows.Forms.View.Details;
            this.lvIEC101Slave.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvIEC101Slave_ItemCheck);
            this.lvIEC101Slave.DoubleClick += new System.EventHandler(this.lvIEC101Slave_DoubleClick);
            // 
            // grpIEC101
            // 
            this.grpIEC101.BackColor = System.Drawing.SystemColors.Control;
            this.grpIEC101.Controls.Add(this.label2);
            this.grpIEC101.Controls.Add(this.CmbLinkAddSize);
            this.grpIEC101.Controls.Add(this.txtLinkAdd);
            this.grpIEC101.Controls.Add(this.label1);
            this.grpIEC101.Controls.Add(this.chkRun);
            this.grpIEC101.Controls.Add(this.btnLast);
            this.grpIEC101.Controls.Add(this.btnNext);
            this.grpIEC101.Controls.Add(this.btnPrev);
            this.grpIEC101.Controls.Add(this.btnFirst);
            this.grpIEC101.Controls.Add(this.cmbDebug);
            this.grpIEC101.Controls.Add(this.cmbIOASize);
            this.grpIEC101.Controls.Add(this.cmbPortNo);
            this.grpIEC101.Controls.Add(this.txtSlaveNum);
            this.grpIEC101.Controls.Add(this.lblSN);
            this.grpIEC101.Controls.Add(this.lblDebug);
            this.grpIEC101.Controls.Add(this.txtFirmwareVersion);
            this.grpIEC101.Controls.Add(this.lblAFV);
            this.grpIEC101.Controls.Add(this.txtEventQSize);
            this.grpIEC101.Controls.Add(this.lblEQS);
            this.grpIEC101.Controls.Add(this.txtCyclicInterval);
            this.grpIEC101.Controls.Add(this.lblCI);
            this.grpIEC101.Controls.Add(this.lblCOT);
            this.grpIEC101.Controls.Add(this.cmbCOTsize);
            this.grpIEC101.Controls.Add(this.lblIOA);
            this.grpIEC101.Controls.Add(this.lblASDUsize);
            this.grpIEC101.Controls.Add(this.cmbASDUsize);
            this.grpIEC101.Controls.Add(this.txtASDUaddress);
            this.grpIEC101.Controls.Add(this.lblASDU);
            this.grpIEC101.Controls.Add(this.lblLIP);
            this.grpIEC101.Controls.Add(this.lblHdrText);
            this.grpIEC101.Controls.Add(this.pbHdr);
            this.grpIEC101.Controls.Add(this.btnCancel);
            this.grpIEC101.Controls.Add(this.btnDone);
            this.grpIEC101.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpIEC101.Location = new System.Drawing.Point(352, 94);
            this.grpIEC101.Name = "grpIEC101";
            this.grpIEC101.Size = new System.Drawing.Size(286, 393);
            this.grpIEC101.TabIndex = 18;
            this.grpIEC101.TabStop = false;
            this.grpIEC101.Visible = false;
            this.grpIEC101.Enter += new System.EventHandler(this.grpIEC104_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 123;
            this.label2.Text = "Link Address  Size";
            // 
            // CmbLinkAddSize
            // 
            this.CmbLinkAddSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbLinkAddSize.FormattingEnabled = true;
            this.CmbLinkAddSize.Location = new System.Drawing.Point(121, 127);
            this.CmbLinkAddSize.Name = "CmbLinkAddSize";
            this.CmbLinkAddSize.Size = new System.Drawing.Size(148, 21);
            this.CmbLinkAddSize.TabIndex = 124;
            this.CmbLinkAddSize.Tag = "LinkAddressSize";
            // 
            // txtLinkAdd
            // 
            this.txtLinkAdd.Location = new System.Drawing.Point(121, 79);
            this.txtLinkAdd.Name = "txtLinkAdd";
            this.txtLinkAdd.Size = new System.Drawing.Size(148, 20);
            this.txtLinkAdd.TabIndex = 122;
            this.txtLinkAdd.Tag = "LinkAddress";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 121;
            this.label1.Text = "Link Address";
            // 
            // chkRun
            // 
            this.chkRun.AutoSize = true;
            this.chkRun.Location = new System.Drawing.Point(9, 338);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(46, 17);
            this.chkRun.TabIndex = 120;
            this.chkRun.Tag = "Run_YES_NO";
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(210, 364);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(59, 22);
            this.btnLast.TabIndex = 119;
            this.btnLast.Text = "Last>>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(138, 364);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(59, 22);
            this.btnNext.TabIndex = 118;
            this.btnNext.Text = "Next>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(72, 364);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(59, 22);
            this.btnPrev.TabIndex = 117;
            this.btnPrev.Text = "<Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(9, 364);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 116;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // cmbDebug
            // 
            this.cmbDebug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDebug.FormattingEnabled = true;
            this.cmbDebug.Location = new System.Drawing.Point(121, 299);
            this.cmbDebug.Name = "cmbDebug";
            this.cmbDebug.Size = new System.Drawing.Size(148, 21);
            this.cmbDebug.TabIndex = 91;
            this.cmbDebug.Tag = "DEBUG";
            // 
            // cmbIOASize
            // 
            this.cmbIOASize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIOASize.FormattingEnabled = true;
            this.cmbIOASize.Location = new System.Drawing.Point(121, 177);
            this.cmbIOASize.Name = "cmbIOASize";
            this.cmbIOASize.Size = new System.Drawing.Size(148, 21);
            this.cmbIOASize.TabIndex = 69;
            this.cmbIOASize.Tag = "IOASize";
            // 
            // cmbPortNo
            // 
            this.cmbPortNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortNo.FormattingEnabled = true;
            this.cmbPortNo.Location = new System.Drawing.Point(121, 54);
            this.cmbPortNo.Name = "cmbPortNo";
            this.cmbPortNo.Size = new System.Drawing.Size(148, 21);
            this.cmbPortNo.TabIndex = 115;
            this.cmbPortNo.Tag = "PortNum";
            // 
            // txtSlaveNum
            // 
            this.txtSlaveNum.Enabled = false;
            this.txtSlaveNum.Location = new System.Drawing.Point(121, 30);
            this.txtSlaveNum.Name = "txtSlaveNum";
            this.txtSlaveNum.Size = new System.Drawing.Size(148, 20);
            this.txtSlaveNum.TabIndex = 57;
            this.txtSlaveNum.Tag = "SlaveNum";
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Location = new System.Drawing.Point(9, 33);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(54, 13);
            this.lblSN.TabIndex = 56;
            this.lblSN.Text = "Slave No.";
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(9, 302);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(68, 13);
            this.lblDebug.TabIndex = 90;
            this.lblDebug.Text = "Debug Level";
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.Enabled = false;
            this.txtFirmwareVersion.Location = new System.Drawing.Point(121, 275);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.Size = new System.Drawing.Size(148, 20);
            this.txtFirmwareVersion.TabIndex = 89;
            this.txtFirmwareVersion.Tag = "AppFirmwareVersion";
            // 
            // lblAFV
            // 
            this.lblAFV.AutoSize = true;
            this.lblAFV.Location = new System.Drawing.Point(9, 278);
            this.lblAFV.Name = "lblAFV";
            this.lblAFV.Size = new System.Drawing.Size(87, 13);
            this.lblAFV.TabIndex = 88;
            this.lblAFV.Text = "Firmware Version";
            // 
            // txtEventQSize
            // 
            this.txtEventQSize.Enabled = false;
            this.txtEventQSize.Location = new System.Drawing.Point(121, 251);
            this.txtEventQSize.Name = "txtEventQSize";
            this.txtEventQSize.Size = new System.Drawing.Size(148, 20);
            this.txtEventQSize.TabIndex = 87;
            this.txtEventQSize.Tag = "EventQSize";
            // 
            // lblEQS
            // 
            this.lblEQS.AutoSize = true;
            this.lblEQS.Location = new System.Drawing.Point(9, 254);
            this.lblEQS.Name = "lblEQS";
            this.lblEQS.Size = new System.Drawing.Size(93, 13);
            this.lblEQS.TabIndex = 86;
            this.lblEQS.Text = "Event Queue Size";
            // 
            // txtCyclicInterval
            // 
            this.txtCyclicInterval.Location = new System.Drawing.Point(121, 227);
            this.txtCyclicInterval.Name = "txtCyclicInterval";
            this.txtCyclicInterval.Size = new System.Drawing.Size(148, 20);
            this.txtCyclicInterval.TabIndex = 85;
            this.txtCyclicInterval.Tag = "CyclicInterval";
            // 
            // lblCI
            // 
            this.lblCI.AutoSize = true;
            this.lblCI.Location = new System.Drawing.Point(9, 230);
            this.lblCI.Name = "lblCI";
            this.lblCI.Size = new System.Drawing.Size(99, 13);
            this.lblCI.TabIndex = 84;
            this.lblCI.Text = "Cyclic Interval (sec)";
            // 
            // lblCOT
            // 
            this.lblCOT.AutoSize = true;
            this.lblCOT.Location = new System.Drawing.Point(9, 205);
            this.lblCOT.Name = "lblCOT";
            this.lblCOT.Size = new System.Drawing.Size(52, 13);
            this.lblCOT.TabIndex = 70;
            this.lblCOT.Text = "COT Size";
            // 
            // cmbCOTsize
            // 
            this.cmbCOTsize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCOTsize.FormattingEnabled = true;
            this.cmbCOTsize.Location = new System.Drawing.Point(121, 202);
            this.cmbCOTsize.Name = "cmbCOTsize";
            this.cmbCOTsize.Size = new System.Drawing.Size(148, 21);
            this.cmbCOTsize.TabIndex = 71;
            this.cmbCOTsize.Tag = "COTSize";
            // 
            // lblIOA
            // 
            this.lblIOA.AutoSize = true;
            this.lblIOA.Location = new System.Drawing.Point(9, 180);
            this.lblIOA.Name = "lblIOA";
            this.lblIOA.Size = new System.Drawing.Size(48, 13);
            this.lblIOA.TabIndex = 68;
            this.lblIOA.Text = "IOA Size";
            // 
            // lblASDUsize
            // 
            this.lblASDUsize.AutoSize = true;
            this.lblASDUsize.Location = new System.Drawing.Point(9, 157);
            this.lblASDUsize.Name = "lblASDUsize";
            this.lblASDUsize.Size = new System.Drawing.Size(60, 13);
            this.lblASDUsize.TabIndex = 66;
            this.lblASDUsize.Text = "ASDU Size";
            // 
            // cmbASDUsize
            // 
            this.cmbASDUsize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbASDUsize.FormattingEnabled = true;
            this.cmbASDUsize.Location = new System.Drawing.Point(121, 152);
            this.cmbASDUsize.Name = "cmbASDUsize";
            this.cmbASDUsize.Size = new System.Drawing.Size(148, 21);
            this.cmbASDUsize.TabIndex = 67;
            this.cmbASDUsize.Tag = "ASDUSize";
            // 
            // txtASDUaddress
            // 
            this.txtASDUaddress.Location = new System.Drawing.Point(121, 103);
            this.txtASDUaddress.Name = "txtASDUaddress";
            this.txtASDUaddress.Size = new System.Drawing.Size(148, 20);
            this.txtASDUaddress.TabIndex = 65;
            this.txtASDUaddress.Tag = "ASDUAddress";
            // 
            // lblASDU
            // 
            this.lblASDU.AutoSize = true;
            this.lblASDU.Location = new System.Drawing.Point(9, 106);
            this.lblASDU.Name = "lblASDU";
            this.lblASDU.Size = new System.Drawing.Size(78, 13);
            this.lblASDU.TabIndex = 64;
            this.lblASDU.Text = "ASDU Address";
            // 
            // lblLIP
            // 
            this.lblLIP.AutoSize = true;
            this.lblLIP.Location = new System.Drawing.Point(9, 57);
            this.lblLIP.Name = "lblLIP";
            this.lblLIP.Size = new System.Drawing.Size(46, 13);
            this.lblLIP.TabIndex = 58;
            this.lblLIP.Text = "Port No.";
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
            this.lblHdrText.Text = "IEC101 Slave";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(307, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(201, 331);
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
            this.btnDone.Location = new System.Drawing.Point(121, 331);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 93;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(80, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 28);
            this.btnDelete.TabIndex = 24;
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
            this.btnAdd.TabIndex = 23;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnexportIEC101INI
            // 
            this.btnexportIEC101INI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnexportIEC101INI.FlatAppearance.BorderSize = 0;
            this.btnexportIEC101INI.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnexportIEC101INI.Location = new System.Drawing.Point(160, 9);
            this.btnexportIEC101INI.Name = "btnexportIEC101INI";
            this.btnexportIEC101INI.Size = new System.Drawing.Size(68, 28);
            this.btnexportIEC101INI.TabIndex = 25;
            this.btnexportIEC101INI.Text = "&Export INI";
            this.btnexportIEC101INI.UseVisualStyleBackColor = true;
            this.btnexportIEC101INI.Click += new System.EventHandler(this.btnexportIEC101INI_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblISC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 30);
            this.panel1.TabIndex = 26;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnexportIEC101INI);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 654);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1050, 46);
            this.panel2.TabIndex = 27;
            // 
            // ucGroupIEC101Slave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpIEC101);
            this.Controls.Add(this.lvIEC101Slave);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucGroupIEC101Slave";
            this.Size = new System.Drawing.Size(1050, 700);
            this.grpIEC101.ResumeLayout(false);
            this.grpIEC101.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblISC;
        public System.Windows.Forms.ListView lvIEC101Slave;
        public System.Windows.Forms.GroupBox grpIEC101;
        public System.Windows.Forms.ComboBox cmbDebug;
        public System.Windows.Forms.ComboBox cmbIOASize;
        public System.Windows.Forms.ComboBox cmbPortNo;
        public System.Windows.Forms.TextBox txtSlaveNum;
        private System.Windows.Forms.Label lblSN;
        private System.Windows.Forms.Label lblDebug;
        public System.Windows.Forms.TextBox txtFirmwareVersion;
        private System.Windows.Forms.Label lblAFV;
        public System.Windows.Forms.TextBox txtEventQSize;
        private System.Windows.Forms.Label lblEQS;
        public System.Windows.Forms.TextBox txtCyclicInterval;
        private System.Windows.Forms.Label lblCI;
        private System.Windows.Forms.Label lblCOT;
        public System.Windows.Forms.ComboBox cmbCOTsize;
        private System.Windows.Forms.Label lblIOA;
        private System.Windows.Forms.Label lblASDUsize;
        public System.Windows.Forms.ComboBox cmbASDUsize;
        public System.Windows.Forms.TextBox txtASDUaddress;
        private System.Windows.Forms.Label lblASDU;
        private System.Windows.Forms.Label lblLIP;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Button btnexportIEC101INI;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.CheckBox chkRun;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.TextBox txtLinkAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox CmbLinkAddSize;
    }
}
