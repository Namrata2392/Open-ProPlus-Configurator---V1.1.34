namespace OpenProPlusConfigurator
{
    partial class ucSerialPortConfiguration
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
            this.lvSPorts = new System.Windows.Forms.ListView();
            this.lblSPC = new System.Windows.Forms.Label();
            this.grpSI = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.lblPR = new System.Windows.Forms.Label();
            this.cmbFlowControl = new System.Windows.Forms.ComboBox();
            this.lblFC = new System.Windows.Forms.Label();
            this.cmbStopbits = new System.Windows.Forms.ComboBox();
            this.lblSB = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.txtPortName = new System.Windows.Forms.TextBox();
            this.lblPN = new System.Windows.Forms.Label();
            this.txtPortNo = new System.Windows.Forms.TextBox();
            this.lblPortNo = new System.Windows.Forms.Label();
            this.txtTcpPort = new System.Windows.Forms.TextBox();
            this.lblTP = new System.Windows.Forms.Label();
            this.txtRTSPostTime = new System.Windows.Forms.TextBox();
            this.txtRTSPreTime = new System.Windows.Forms.TextBox();
            this.lblRPT = new System.Windows.Forms.Label();
            this.lblRPsT = new System.Windows.Forms.Label();
            this.cmbDatabits = new System.Windows.Forms.ComboBox();
            this.lblDB = new System.Windows.Forms.Label();
            this.lblBR = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpSI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvSPorts
            // 
            this.lvSPorts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvSPorts.CheckBoxes = true;
            this.lvSPorts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSPorts.FullRowSelect = true;
            this.lvSPorts.Location = new System.Drawing.Point(0, 30);
            this.lvSPorts.MultiSelect = false;
            this.lvSPorts.Name = "lvSPorts";
            this.lvSPorts.Size = new System.Drawing.Size(1050, 670);
            this.lvSPorts.TabIndex = 3;
            this.lvSPorts.UseCompatibleStateImageBehavior = false;
            this.lvSPorts.View = System.Windows.Forms.View.Details;
            this.lvSPorts.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvSPorts_ItemCheck);
            this.lvSPorts.DoubleClick += new System.EventHandler(this.lvSPorts_DoubleClick);
            // 
            // lblSPC
            // 
            this.lblSPC.AutoSize = true;
            this.lblSPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSPC.Location = new System.Drawing.Point(2, 0);
            this.lblSPC.Name = "lblSPC";
            this.lblSPC.Size = new System.Drawing.Size(169, 15);
            this.lblSPC.TabIndex = 2;
            this.lblSPC.Text = "Serial Port Configuration:";
            // 
            // grpSI
            // 
            this.grpSI.BackColor = System.Drawing.SystemColors.Control;
            this.grpSI.Controls.Add(this.label1);
            this.grpSI.Controls.Add(this.chkEnable);
            this.grpSI.Controls.Add(this.btnLast);
            this.grpSI.Controls.Add(this.btnNext);
            this.grpSI.Controls.Add(this.btnPrev);
            this.grpSI.Controls.Add(this.btnFirst);
            this.grpSI.Controls.Add(this.cmbParity);
            this.grpSI.Controls.Add(this.lblPR);
            this.grpSI.Controls.Add(this.cmbFlowControl);
            this.grpSI.Controls.Add(this.lblFC);
            this.grpSI.Controls.Add(this.cmbStopbits);
            this.grpSI.Controls.Add(this.lblSB);
            this.grpSI.Controls.Add(this.lblHdrText);
            this.grpSI.Controls.Add(this.pbHdr);
            this.grpSI.Controls.Add(this.cmbBaudRate);
            this.grpSI.Controls.Add(this.btnCancel);
            this.grpSI.Controls.Add(this.btnDone);
            this.grpSI.Controls.Add(this.txtPortName);
            this.grpSI.Controls.Add(this.lblPN);
            this.grpSI.Controls.Add(this.txtPortNo);
            this.grpSI.Controls.Add(this.lblPortNo);
            this.grpSI.Controls.Add(this.txtTcpPort);
            this.grpSI.Controls.Add(this.lblTP);
            this.grpSI.Controls.Add(this.txtRTSPostTime);
            this.grpSI.Controls.Add(this.txtRTSPreTime);
            this.grpSI.Controls.Add(this.lblRPT);
            this.grpSI.Controls.Add(this.lblRPsT);
            this.grpSI.Controls.Add(this.cmbDatabits);
            this.grpSI.Controls.Add(this.lblDB);
            this.grpSI.Controls.Add(this.lblBR);
            this.grpSI.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpSI.Location = new System.Drawing.Point(199, 102);
            this.grpSI.Name = "grpSI";
            this.grpSI.Size = new System.Drawing.Size(318, 360);
            this.grpSI.TabIndex = 16;
            this.grpSI.TabStop = false;
            this.grpSI.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 118;
            this.label1.Text = "Stop Bits";
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Location = new System.Drawing.Point(11, 307);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(59, 17);
            this.chkEnable.TabIndex = 117;
            this.chkEnable.Tag = "Enable_YES_NO";
            this.chkEnable.Text = "Enable";
            this.chkEnable.UseVisualStyleBackColor = true;
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(236, 333);
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
            this.btnNext.Location = new System.Drawing.Point(161, 333);
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
            this.btnPrev.Location = new System.Drawing.Point(86, 333);
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
            this.btnFirst.Location = new System.Drawing.Point(11, 333);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 111;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // cmbParity
            // 
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Location = new System.Drawing.Point(127, 164);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(168, 21);
            this.cmbParity.TabIndex = 38;
            this.cmbParity.Tag = "Parity";
            // 
            // lblPR
            // 
            this.lblPR.AutoSize = true;
            this.lblPR.Location = new System.Drawing.Point(11, 167);
            this.lblPR.Name = "lblPR";
            this.lblPR.Size = new System.Drawing.Size(33, 13);
            this.lblPR.TabIndex = 37;
            this.lblPR.Text = "Parity";
            // 
            // cmbFlowControl
            // 
            this.cmbFlowControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFlowControl.FormattingEnabled = true;
            this.cmbFlowControl.Location = new System.Drawing.Point(127, 137);
            this.cmbFlowControl.Name = "cmbFlowControl";
            this.cmbFlowControl.Size = new System.Drawing.Size(168, 21);
            this.cmbFlowControl.TabIndex = 36;
            this.cmbFlowControl.Tag = "FlowControl";
            this.cmbFlowControl.SelectedIndexChanged += new System.EventHandler(this.cmbFlowControl_SelectedIndexChanged);
            // 
            // lblFC
            // 
            this.lblFC.AutoSize = true;
            this.lblFC.Location = new System.Drawing.Point(11, 140);
            this.lblFC.Name = "lblFC";
            this.lblFC.Size = new System.Drawing.Size(65, 13);
            this.lblFC.TabIndex = 35;
            this.lblFC.Text = "Flow Control";
            // 
            // cmbStopbits
            // 
            this.cmbStopbits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopbits.FormattingEnabled = true;
            this.cmbStopbits.Location = new System.Drawing.Point(127, 110);
            this.cmbStopbits.Name = "cmbStopbits";
            this.cmbStopbits.Size = new System.Drawing.Size(168, 21);
            this.cmbStopbits.TabIndex = 34;
            this.cmbStopbits.Tag = "Stopbits";
            // 
            // lblSB
            // 
            this.lblSB.AutoSize = true;
            this.lblSB.Location = new System.Drawing.Point(12, 110);
            this.lblSB.Name = "lblSB";
            this.lblSB.Size = new System.Drawing.Size(0, 13);
            this.lblSB.TabIndex = 33;
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(121, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "Serial Port Interface";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(331, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Location = new System.Drawing.Point(127, 56);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(168, 21);
            this.cmbBaudRate.TabIndex = 30;
            this.cmbBaudRate.Tag = "BaudRate";
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(227, 300);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 48;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(127, 300);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 47;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // txtPortName
            // 
            this.txtPortName.Enabled = false;
            this.txtPortName.Location = new System.Drawing.Point(127, 243);
            this.txtPortName.Name = "txtPortName";
            this.txtPortName.Size = new System.Drawing.Size(168, 20);
            this.txtPortName.TabIndex = 44;
            this.txtPortName.Tag = "PortName";
            // 
            // lblPN
            // 
            this.lblPN.AutoSize = true;
            this.lblPN.Location = new System.Drawing.Point(11, 246);
            this.lblPN.Name = "lblPN";
            this.lblPN.Size = new System.Drawing.Size(57, 13);
            this.lblPN.TabIndex = 43;
            this.lblPN.Text = "Port Name";
            // 
            // txtPortNo
            // 
            this.txtPortNo.Enabled = false;
            this.txtPortNo.Location = new System.Drawing.Point(127, 30);
            this.txtPortNo.Name = "txtPortNo";
            this.txtPortNo.Size = new System.Drawing.Size(168, 20);
            this.txtPortNo.TabIndex = 28;
            this.txtPortNo.Tag = "PortNum";
            // 
            // lblPortNo
            // 
            this.lblPortNo.AutoSize = true;
            this.lblPortNo.Location = new System.Drawing.Point(11, 33);
            this.lblPortNo.Name = "lblPortNo";
            this.lblPortNo.Size = new System.Drawing.Size(46, 13);
            this.lblPortNo.TabIndex = 27;
            this.lblPortNo.Text = "Port No.";
            // 
            // txtTcpPort
            // 
            this.txtTcpPort.Location = new System.Drawing.Point(127, 269);
            this.txtTcpPort.Name = "txtTcpPort";
            this.txtTcpPort.Size = new System.Drawing.Size(168, 20);
            this.txtTcpPort.TabIndex = 46;
            this.txtTcpPort.Tag = "TcpPort";
            this.txtTcpPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTcpPort_KeyPress);
            // 
            // lblTP
            // 
            this.lblTP.AutoSize = true;
            this.lblTP.Location = new System.Drawing.Point(11, 272);
            this.lblTP.Name = "lblTP";
            this.lblTP.Size = new System.Drawing.Size(50, 13);
            this.lblTP.TabIndex = 45;
            this.lblTP.Text = "TCP Port";
            // 
            // txtRTSPostTime
            // 
            this.txtRTSPostTime.Enabled = false;
            this.txtRTSPostTime.Location = new System.Drawing.Point(127, 217);
            this.txtRTSPostTime.Name = "txtRTSPostTime";
            this.txtRTSPostTime.Size = new System.Drawing.Size(168, 20);
            this.txtRTSPostTime.TabIndex = 42;
            this.txtRTSPostTime.Tag = "RtsPostTime";
            // 
            // txtRTSPreTime
            // 
            this.txtRTSPreTime.Enabled = false;
            this.txtRTSPreTime.Location = new System.Drawing.Point(127, 191);
            this.txtRTSPreTime.Name = "txtRTSPreTime";
            this.txtRTSPreTime.Size = new System.Drawing.Size(168, 20);
            this.txtRTSPreTime.TabIndex = 40;
            this.txtRTSPreTime.Tag = "RtsPreTime";
            // 
            // lblRPT
            // 
            this.lblRPT.AutoSize = true;
            this.lblRPT.Location = new System.Drawing.Point(11, 194);
            this.lblRPT.Name = "lblRPT";
            this.lblRPT.Size = new System.Drawing.Size(108, 13);
            this.lblRPT.TabIndex = 39;
            this.lblRPT.Text = "RTS Pre Time (msec)";
            // 
            // lblRPsT
            // 
            this.lblRPsT.AutoSize = true;
            this.lblRPsT.Location = new System.Drawing.Point(11, 220);
            this.lblRPsT.Name = "lblRPsT";
            this.lblRPsT.Size = new System.Drawing.Size(113, 13);
            this.lblRPsT.TabIndex = 41;
            this.lblRPsT.Text = "RTS Post Time (msec)";
            // 
            // cmbDatabits
            // 
            this.cmbDatabits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatabits.FormattingEnabled = true;
            this.cmbDatabits.Location = new System.Drawing.Point(127, 83);
            this.cmbDatabits.Name = "cmbDatabits";
            this.cmbDatabits.Size = new System.Drawing.Size(168, 21);
            this.cmbDatabits.TabIndex = 32;
            this.cmbDatabits.Tag = "Databits";
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Location = new System.Drawing.Point(11, 86);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(50, 13);
            this.lblDB.TabIndex = 31;
            this.lblDB.Text = "Data Bits";
            // 
            // lblBR
            // 
            this.lblBR.AutoSize = true;
            this.lblBR.Location = new System.Drawing.Point(11, 59);
            this.lblBR.Name = "lblBR";
            this.lblBR.Size = new System.Drawing.Size(58, 13);
            this.lblBR.TabIndex = 29;
            this.lblBR.Text = "Baud Rate";
            // 
            // btnEdit
            // 
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.Location = new System.Drawing.Point(181, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(76, 21);
            this.btnEdit.TabIndex = 19;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSPC);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 30);
            this.panel1.TabIndex = 20;
            // 
            // ucSerialPortConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpSI);
            this.Controls.Add(this.lvSPorts);
            this.Controls.Add(this.panel1);
            this.Name = "ucSerialPortConfiguration";
            this.Size = new System.Drawing.Size(1050, 700);
            this.grpSI.ResumeLayout(false);
            this.grpSI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lvSPorts;
        private System.Windows.Forms.Label lblSPC;
        public System.Windows.Forms.GroupBox grpSI;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        public System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.TextBox txtPortName;
        private System.Windows.Forms.Label lblPN;
        public System.Windows.Forms.TextBox txtPortNo;
        private System.Windows.Forms.Label lblPortNo;
        public System.Windows.Forms.TextBox txtTcpPort;
        private System.Windows.Forms.Label lblTP;
        public System.Windows.Forms.TextBox txtRTSPostTime;
        public System.Windows.Forms.TextBox txtRTSPreTime;
        private System.Windows.Forms.Label lblRPT;
        private System.Windows.Forms.Label lblRPsT;
        public System.Windows.Forms.ComboBox cmbDatabits;
        private System.Windows.Forms.Label lblDB;
        private System.Windows.Forms.Label lblBR;
        public System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.Label lblPR;
        public System.Windows.Forms.ComboBox cmbFlowControl;
        private System.Windows.Forms.Label lblFC;
        public System.Windows.Forms.ComboBox cmbStopbits;
        private System.Windows.Forms.Label lblSB;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.CheckBox chkEnable;
        public System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}
