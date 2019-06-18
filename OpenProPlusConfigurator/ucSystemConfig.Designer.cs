namespace OpenProPlusConfigurator
{
    partial class ucSystemConfig
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
            this.lblSC = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbRedundancyMode = new System.Windows.Forms.ComboBox();
            this.lblRM = new System.Windows.Forms.Label();
            this.txtMaxDataPoints = new System.Windows.Forms.TextBox();
            this.lblTimeZone = new System.Windows.Forms.Label();
            this.lblDP = new System.Windows.Forms.Label();
            this.cmbTimeSyncSource = new System.Windows.Forms.ComboBox();
            this.lblTSS = new System.Windows.Forms.Label();
            this.txtRedundantSystemIP = new System.Windows.Forms.TextBox();
            this.lblRSIP = new System.Windows.Forms.Label();
            this.grpLog = new System.Windows.Forms.GroupBox();
            this.txtLogServerIP = new System.Windows.Forms.TextBox();
            this.lblLP = new System.Windows.Forms.Label();
            this.txtLogServerPort = new System.Windows.Forms.TextBox();
            this.lblLSP = new System.Windows.Forms.Label();
            this.lblLSIP = new System.Windows.Forms.Label();
            this.chkEdit = new System.Windows.Forms.CheckBox();
            this.cmbLogProtocol = new System.Windows.Forms.ComboBox();
            this.chkLogRemote = new System.Windows.Forms.CheckBox();
            this.chkLogLocal = new System.Windows.Forms.CheckBox();
            this.grpNTP = new System.Windows.Forms.GroupBox();
            this.chkNTP = new System.Windows.Forms.CheckBox();
            this.txtNTPServer2 = new System.Windows.Forms.TextBox();
            this.txtNTPServer1 = new System.Windows.Forms.TextBox();
            this.lblNTPS1 = new System.Windows.Forms.Label();
            this.lblNTPS2 = new System.Windows.Forms.Label();
            this.CmbTimeZone = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkDBSync = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.grpLog.SuspendLayout();
            this.grpNTP.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSC
            // 
            this.lblSC.AutoSize = true;
            this.lblSC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSC.Location = new System.Drawing.Point(2, 0);
            this.lblSC.Name = "lblSC";
            this.lblSC.Size = new System.Drawing.Size(102, 15);
            this.lblSC.TabIndex = 0;
            this.lblSC.Text = "System Config:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(792, 30);
            this.panel1.TabIndex = 37;
            // 
            // cmbRedundancyMode
            // 
            this.cmbRedundancyMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRedundancyMode.FormattingEnabled = true;
            this.cmbRedundancyMode.Location = new System.Drawing.Point(178, 9);
            this.cmbRedundancyMode.Name = "cmbRedundancyMode";
            this.cmbRedundancyMode.Size = new System.Drawing.Size(198, 21);
            this.cmbRedundancyMode.TabIndex = 40;
            this.cmbRedundancyMode.SelectedIndexChanged += new System.EventHandler(this.cmbRedundancyMode_SelectedIndexChanged);
            this.cmbRedundancyMode.SelectedValueChanged += new System.EventHandler(this.cmbRedundancyMode_SelectedValueChanged);
            // 
            // lblRM
            // 
            this.lblRM.AutoSize = true;
            this.lblRM.Location = new System.Drawing.Point(9, 12);
            this.lblRM.Name = "lblRM";
            this.lblRM.Size = new System.Drawing.Size(98, 13);
            this.lblRM.TabIndex = 39;
            this.lblRM.Text = "Redundancy Mode";
            // 
            // txtMaxDataPoints
            // 
            this.txtMaxDataPoints.Enabled = false;
            this.txtMaxDataPoints.Location = new System.Drawing.Point(181, 384);
            this.txtMaxDataPoints.Name = "txtMaxDataPoints";
            this.txtMaxDataPoints.Size = new System.Drawing.Size(195, 20);
            this.txtMaxDataPoints.TabIndex = 48;
            // 
            // lblTimeZone
            // 
            this.lblTimeZone.AutoSize = true;
            this.lblTimeZone.Location = new System.Drawing.Point(9, 92);
            this.lblTimeZone.Name = "lblTimeZone";
            this.lblTimeZone.Size = new System.Drawing.Size(58, 13);
            this.lblTimeZone.TabIndex = 49;
            this.lblTimeZone.Text = "Time Zone";
            // 
            // lblDP
            // 
            this.lblDP.AutoSize = true;
            this.lblDP.Location = new System.Drawing.Point(15, 384);
            this.lblDP.Name = "lblDP";
            this.lblDP.Size = new System.Drawing.Size(109, 13);
            this.lblDP.TabIndex = 47;
            this.lblDP.Text = "Maximum Data Points";
            // 
            // cmbTimeSyncSource
            // 
            this.cmbTimeSyncSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeSyncSource.FormattingEnabled = true;
            this.cmbTimeSyncSource.Location = new System.Drawing.Point(178, 62);
            this.cmbTimeSyncSource.Name = "cmbTimeSyncSource";
            this.cmbTimeSyncSource.Size = new System.Drawing.Size(198, 21);
            this.cmbTimeSyncSource.TabIndex = 44;
            this.cmbTimeSyncSource.SelectedIndexChanged += new System.EventHandler(this.cmbTimeSyncSource_SelectedIndexChanged);
            // 
            // lblTSS
            // 
            this.lblTSS.AutoSize = true;
            this.lblTSS.Location = new System.Drawing.Point(9, 63);
            this.lblTSS.Name = "lblTSS";
            this.lblTSS.Size = new System.Drawing.Size(94, 13);
            this.lblTSS.TabIndex = 43;
            this.lblTSS.Text = "Time Sync Source";
            // 
            // txtRedundantSystemIP
            // 
            this.txtRedundantSystemIP.Location = new System.Drawing.Point(178, 36);
            this.txtRedundantSystemIP.Name = "txtRedundantSystemIP";
            this.txtRedundantSystemIP.Size = new System.Drawing.Size(198, 20);
            this.txtRedundantSystemIP.TabIndex = 42;
            // 
            // lblRSIP
            // 
            this.lblRSIP.AutoSize = true;
            this.lblRSIP.Location = new System.Drawing.Point(9, 37);
            this.lblRSIP.Name = "lblRSIP";
            this.lblRSIP.Size = new System.Drawing.Size(151, 13);
            this.lblRSIP.TabIndex = 41;
            this.lblRSIP.Text = "Redundant System IP Address";
            // 
            // grpLog
            // 
            this.grpLog.Controls.Add(this.txtLogServerIP);
            this.grpLog.Controls.Add(this.lblLP);
            this.grpLog.Controls.Add(this.txtLogServerPort);
            this.grpLog.Controls.Add(this.lblLSP);
            this.grpLog.Controls.Add(this.lblLSIP);
            this.grpLog.Controls.Add(this.chkEdit);
            this.grpLog.Controls.Add(this.cmbLogProtocol);
            this.grpLog.Controls.Add(this.chkLogRemote);
            this.grpLog.Controls.Add(this.chkLogLocal);
            this.grpLog.Location = new System.Drawing.Point(12, 234);
            this.grpLog.Name = "grpLog";
            this.grpLog.Size = new System.Drawing.Size(363, 140);
            this.grpLog.TabIndex = 46;
            this.grpLog.TabStop = false;
            this.grpLog.Text = "Log Settings";
            // 
            // txtLogServerIP
            // 
            this.txtLogServerIP.Location = new System.Drawing.Point(166, 53);
            this.txtLogServerIP.Name = "txtLogServerIP";
            this.txtLogServerIP.Size = new System.Drawing.Size(190, 20);
            this.txtLogServerIP.TabIndex = 27;
            // 
            // lblLP
            // 
            this.lblLP.AutoSize = true;
            this.lblLP.Location = new System.Drawing.Point(8, 109);
            this.lblLP.Name = "lblLP";
            this.lblLP.Size = new System.Drawing.Size(67, 13);
            this.lblLP.TabIndex = 30;
            this.lblLP.Text = "Log Protocol";
            // 
            // txtLogServerPort
            // 
            this.txtLogServerPort.Enabled = false;
            this.txtLogServerPort.Location = new System.Drawing.Point(166, 79);
            this.txtLogServerPort.Name = "txtLogServerPort";
            this.txtLogServerPort.Size = new System.Drawing.Size(189, 20);
            this.txtLogServerPort.TabIndex = 29;
            // 
            // lblLSP
            // 
            this.lblLSP.AutoSize = true;
            this.lblLSP.Location = new System.Drawing.Point(8, 82);
            this.lblLSP.Name = "lblLSP";
            this.lblLSP.Size = new System.Drawing.Size(81, 13);
            this.lblLSP.TabIndex = 28;
            this.lblLSP.Text = "Log Server Port";
            // 
            // lblLSIP
            // 
            this.lblLSIP.AutoSize = true;
            this.lblLSIP.Location = new System.Drawing.Point(8, 56);
            this.lblLSIP.Name = "lblLSIP";
            this.lblLSIP.Size = new System.Drawing.Size(113, 13);
            this.lblLSIP.TabIndex = 26;
            this.lblLSIP.Text = "Log Server IP Address";
            // 
            // chkEdit
            // 
            this.chkEdit.AutoSize = true;
            this.chkEdit.Location = new System.Drawing.Point(8, 141);
            this.chkEdit.Name = "chkEdit";
            this.chkEdit.Size = new System.Drawing.Size(64, 17);
            this.chkEdit.TabIndex = 32;
            this.chkEdit.Text = "Editable";
            this.chkEdit.UseVisualStyleBackColor = true;
            this.chkEdit.Visible = false;
            // 
            // cmbLogProtocol
            // 
            this.cmbLogProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogProtocol.Enabled = false;
            this.cmbLogProtocol.FormattingEnabled = true;
            this.cmbLogProtocol.Location = new System.Drawing.Point(166, 106);
            this.cmbLogProtocol.Name = "cmbLogProtocol";
            this.cmbLogProtocol.Size = new System.Drawing.Size(189, 21);
            this.cmbLogProtocol.TabIndex = 31;
            // 
            // chkLogRemote
            // 
            this.chkLogRemote.AutoSize = true;
            this.chkLogRemote.Location = new System.Drawing.Point(166, 30);
            this.chkLogRemote.Name = "chkLogRemote";
            this.chkLogRemote.Size = new System.Drawing.Size(84, 17);
            this.chkLogRemote.TabIndex = 25;
            this.chkLogRemote.Text = "Log Remote";
            this.chkLogRemote.UseVisualStyleBackColor = true;
            // 
            // chkLogLocal
            // 
            this.chkLogLocal.AutoSize = true;
            this.chkLogLocal.Location = new System.Drawing.Point(13, 30);
            this.chkLogLocal.Name = "chkLogLocal";
            this.chkLogLocal.Size = new System.Drawing.Size(73, 17);
            this.chkLogLocal.TabIndex = 24;
            this.chkLogLocal.Text = "Log Local";
            this.chkLogLocal.UseVisualStyleBackColor = true;
            // 
            // grpNTP
            // 
            this.grpNTP.Controls.Add(this.chkNTP);
            this.grpNTP.Controls.Add(this.txtNTPServer2);
            this.grpNTP.Controls.Add(this.txtNTPServer1);
            this.grpNTP.Controls.Add(this.lblNTPS1);
            this.grpNTP.Controls.Add(this.lblNTPS2);
            this.grpNTP.Location = new System.Drawing.Point(12, 116);
            this.grpNTP.Name = "grpNTP";
            this.grpNTP.Size = new System.Drawing.Size(364, 112);
            this.grpNTP.TabIndex = 45;
            this.grpNTP.TabStop = false;
            this.grpNTP.Text = "NTP Settings";
            // 
            // chkNTP
            // 
            this.chkNTP.AutoSize = true;
            this.chkNTP.Location = new System.Drawing.Point(13, 81);
            this.chkNTP.Name = "chkNTP";
            this.chkNTP.Size = new System.Drawing.Size(70, 17);
            this.chkNTP.TabIndex = 22;
            this.chkNTP.Text = "Use NTP";
            this.chkNTP.UseVisualStyleBackColor = true;
            // 
            // txtNTPServer2
            // 
            this.txtNTPServer2.Location = new System.Drawing.Point(166, 51);
            this.txtNTPServer2.Name = "txtNTPServer2";
            this.txtNTPServer2.Size = new System.Drawing.Size(191, 20);
            this.txtNTPServer2.TabIndex = 21;
            // 
            // txtNTPServer1
            // 
            this.txtNTPServer1.Location = new System.Drawing.Point(166, 25);
            this.txtNTPServer1.Name = "txtNTPServer1";
            this.txtNTPServer1.Size = new System.Drawing.Size(191, 20);
            this.txtNTPServer1.TabIndex = 19;
            // 
            // lblNTPS1
            // 
            this.lblNTPS1.AutoSize = true;
            this.lblNTPS1.Location = new System.Drawing.Point(8, 28);
            this.lblNTPS1.Name = "lblNTPS1";
            this.lblNTPS1.Size = new System.Drawing.Size(113, 13);
            this.lblNTPS1.TabIndex = 18;
            this.lblNTPS1.Text = "NTP Server 1 Address";
            // 
            // lblNTPS2
            // 
            this.lblNTPS2.AutoSize = true;
            this.lblNTPS2.Location = new System.Drawing.Point(8, 50);
            this.lblNTPS2.Name = "lblNTPS2";
            this.lblNTPS2.Size = new System.Drawing.Size(113, 13);
            this.lblNTPS2.TabIndex = 20;
            this.lblNTPS2.Text = "NTP Server 2 Address";
            // 
            // CmbTimeZone
            // 
            this.CmbTimeZone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CmbTimeZone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTimeZone.FormattingEnabled = true;
            this.CmbTimeZone.Location = new System.Drawing.Point(178, 89);
            this.CmbTimeZone.Name = "CmbTimeZone";
            this.CmbTimeZone.Size = new System.Drawing.Size(198, 21);
            this.CmbTimeZone.TabIndex = 50;
            this.CmbTimeZone.TextUpdate += new System.EventHandler(this.CmbTimeZone_TextUpdate);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkDBSync);
            this.panel3.Controls.Add(this.cmbRedundancyMode);
            this.panel3.Controls.Add(this.lblRM);
            this.panel3.Controls.Add(this.CmbTimeZone);
            this.panel3.Controls.Add(this.txtMaxDataPoints);
            this.panel3.Controls.Add(this.grpNTP);
            this.panel3.Controls.Add(this.lblTimeZone);
            this.panel3.Controls.Add(this.grpLog);
            this.panel3.Controls.Add(this.lblDP);
            this.panel3.Controls.Add(this.lblRSIP);
            this.panel3.Controls.Add(this.cmbTimeSyncSource);
            this.panel3.Controls.Add(this.txtRedundantSystemIP);
            this.panel3.Controls.Add(this.lblTSS);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 30);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(792, 492);
            this.panel3.TabIndex = 0;
            // 
            // chkDBSync
            // 
            this.chkDBSync.AutoSize = true;
            this.chkDBSync.Location = new System.Drawing.Point(23, 410);
            this.chkDBSync.Name = "chkDBSync";
            this.chkDBSync.Size = new System.Drawing.Size(68, 17);
            this.chkDBSync.TabIndex = 51;
            this.chkDBSync.Tag = "DBSync";
            this.chkDBSync.Text = "DB Sync";
            this.chkDBSync.UseVisualStyleBackColor = true;
            // 
            // ucSystemConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "ucSystemConfig";
            this.Size = new System.Drawing.Size(792, 522);
            this.Load += new System.EventHandler(this.ucSystemConfig_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpLog.ResumeLayout(false);
            this.grpLog.PerformLayout();
            this.grpNTP.ResumeLayout(false);
            this.grpNTP.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSC;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox cmbRedundancyMode;
        private System.Windows.Forms.Label lblRM;
        public System.Windows.Forms.TextBox txtMaxDataPoints;
        private System.Windows.Forms.Label lblTimeZone;
        private System.Windows.Forms.Label lblDP;
        public System.Windows.Forms.ComboBox cmbTimeSyncSource;
        private System.Windows.Forms.Label lblTSS;
        public System.Windows.Forms.TextBox txtRedundantSystemIP;
        private System.Windows.Forms.Label lblRSIP;
        public System.Windows.Forms.GroupBox grpLog;
        public System.Windows.Forms.TextBox txtLogServerIP;
        private System.Windows.Forms.Label lblLP;
        public System.Windows.Forms.TextBox txtLogServerPort;
        private System.Windows.Forms.Label lblLSP;
        private System.Windows.Forms.Label lblLSIP;
        public System.Windows.Forms.CheckBox chkEdit;
        public System.Windows.Forms.ComboBox cmbLogProtocol;
        public System.Windows.Forms.CheckBox chkLogRemote;
        public System.Windows.Forms.CheckBox chkLogLocal;
        public System.Windows.Forms.GroupBox grpNTP;
        public System.Windows.Forms.CheckBox chkNTP;
        public System.Windows.Forms.TextBox txtNTPServer2;
        public System.Windows.Forms.TextBox txtNTPServer1;
        private System.Windows.Forms.Label lblNTPS1;
        private System.Windows.Forms.Label lblNTPS2;
        public System.Windows.Forms.ComboBox CmbTimeZone;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.CheckBox chkDBSync;
    }
}
