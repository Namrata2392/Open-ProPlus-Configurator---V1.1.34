namespace OpenProPlusConfigurator
{
    partial class ucIED
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
            this.lvIEDDetails = new System.Windows.Forms.ListView();
            this.grpIED = new System.Windows.Forms.GroupBox();
            this.txtTCPPort = new System.Windows.Forms.TextBox();
            this.lblTCPPort = new System.Windows.Forms.Label();
            this.txtRemoteIP = new System.Windows.Forms.TextBox();
            this.lblRIP = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDevice = new System.Windows.Forms.TextBox();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.lblTO = new System.Windows.Forms.Label();
            this.txtRetries = new System.Windows.Forms.TextBox();
            this.txtASDUaddress = new System.Windows.Forms.TextBox();
            this.lblASDU = new System.Windows.Forms.Label();
            this.lblRetries = new System.Windows.Forms.Label();
            this.chkDR = new System.Windows.Forms.CheckBox();
            this.lblDevice = new System.Windows.Forms.Label();
            this.txtUnitID = new System.Windows.Forms.TextBox();
            this.lblUI = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblIED = new System.Windows.Forms.Label();
            this.txtLinkAddressSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpIED.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvIEDDetails
            // 
            this.lvIEDDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvIEDDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvIEDDetails.FullRowSelect = true;
            this.lvIEDDetails.Location = new System.Drawing.Point(0, 30);
            this.lvIEDDetails.MultiSelect = false;
            this.lvIEDDetails.Name = "lvIEDDetails";
            this.lvIEDDetails.Size = new System.Drawing.Size(1050, 385);
            this.lvIEDDetails.TabIndex = 35;
            this.lvIEDDetails.UseCompatibleStateImageBehavior = false;
            this.lvIEDDetails.View = System.Windows.Forms.View.Details;
            // 
            // grpIED
            // 
            this.grpIED.Controls.Add(this.txtLinkAddressSize);
            this.grpIED.Controls.Add(this.label1);
            this.grpIED.Controls.Add(this.txtTCPPort);
            this.grpIED.Controls.Add(this.lblTCPPort);
            this.grpIED.Controls.Add(this.txtRemoteIP);
            this.grpIED.Controls.Add(this.lblRIP);
            this.grpIED.Controls.Add(this.txtDescription);
            this.grpIED.Controls.Add(this.lblDesc);
            this.grpIED.Controls.Add(this.txtDevice);
            this.grpIED.Controls.Add(this.txtTimeOut);
            this.grpIED.Controls.Add(this.lblTO);
            this.grpIED.Controls.Add(this.txtRetries);
            this.grpIED.Controls.Add(this.txtASDUaddress);
            this.grpIED.Controls.Add(this.lblASDU);
            this.grpIED.Controls.Add(this.lblRetries);
            this.grpIED.Controls.Add(this.chkDR);
            this.grpIED.Controls.Add(this.lblDevice);
            this.grpIED.Controls.Add(this.txtUnitID);
            this.grpIED.Controls.Add(this.lblUI);
            this.grpIED.Location = new System.Drawing.Point(3, 0);
            this.grpIED.Name = "grpIED";
            this.grpIED.Size = new System.Drawing.Size(265, 266);
            this.grpIED.TabIndex = 15;
            this.grpIED.TabStop = false;
            this.grpIED.Text = "IED Params";
            // 
            // txtTCPPort
            // 
            this.txtTCPPort.Enabled = false;
            this.txtTCPPort.Location = new System.Drawing.Point(123, 140);
            this.txtTCPPort.Name = "txtTCPPort";
            this.txtTCPPort.Size = new System.Drawing.Size(120, 20);
            this.txtTCPPort.TabIndex = 37;
            // 
            // lblTCPPort
            // 
            this.lblTCPPort.AutoSize = true;
            this.lblTCPPort.Location = new System.Drawing.Point(11, 143);
            this.lblTCPPort.Name = "lblTCPPort";
            this.lblTCPPort.Size = new System.Drawing.Size(50, 13);
            this.lblTCPPort.TabIndex = 36;
            this.lblTCPPort.Text = "TCP Port";
            // 
            // txtRemoteIP
            // 
            this.txtRemoteIP.Enabled = false;
            this.txtRemoteIP.Location = new System.Drawing.Point(123, 115);
            this.txtRemoteIP.Name = "txtRemoteIP";
            this.txtRemoteIP.Size = new System.Drawing.Size(120, 20);
            this.txtRemoteIP.TabIndex = 35;
            // 
            // lblRIP
            // 
            this.lblRIP.AutoSize = true;
            this.lblRIP.Location = new System.Drawing.Point(11, 118);
            this.lblRIP.Name = "lblRIP";
            this.lblRIP.Size = new System.Drawing.Size(57, 13);
            this.lblRIP.TabIndex = 34;
            this.lblRIP.Text = "Remote IP";
            // 
            // txtDescription
            // 
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(123, 215);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(120, 20);
            this.txtDescription.TabIndex = 32;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(11, 218);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(60, 13);
            this.lblDesc.TabIndex = 31;
            this.lblDesc.Text = "Description";
            // 
            // txtDevice
            // 
            this.txtDevice.Enabled = false;
            this.txtDevice.Location = new System.Drawing.Point(123, 90);
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.Size = new System.Drawing.Size(120, 20);
            this.txtDevice.TabIndex = 26;
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.Enabled = false;
            this.txtTimeOut.Location = new System.Drawing.Point(123, 190);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(120, 20);
            this.txtTimeOut.TabIndex = 30;
            // 
            // lblTO
            // 
            this.lblTO.AutoSize = true;
            this.lblTO.Location = new System.Drawing.Point(11, 193);
            this.lblTO.Name = "lblTO";
            this.lblTO.Size = new System.Drawing.Size(70, 13);
            this.lblTO.TabIndex = 29;
            this.lblTO.Text = "Time out (ms)";
            // 
            // txtRetries
            // 
            this.txtRetries.Enabled = false;
            this.txtRetries.Location = new System.Drawing.Point(123, 165);
            this.txtRetries.Name = "txtRetries";
            this.txtRetries.Size = new System.Drawing.Size(120, 20);
            this.txtRetries.TabIndex = 28;
            // 
            // txtASDUaddress
            // 
            this.txtASDUaddress.Enabled = false;
            this.txtASDUaddress.Location = new System.Drawing.Point(123, 40);
            this.txtASDUaddress.Name = "txtASDUaddress";
            this.txtASDUaddress.Size = new System.Drawing.Size(120, 20);
            this.txtASDUaddress.TabIndex = 24;
            // 
            // lblASDU
            // 
            this.lblASDU.AutoSize = true;
            this.lblASDU.Location = new System.Drawing.Point(11, 43);
            this.lblASDU.Name = "lblASDU";
            this.lblASDU.Size = new System.Drawing.Size(78, 13);
            this.lblASDU.TabIndex = 23;
            this.lblASDU.Text = "ASDU Address";
            // 
            // lblRetries
            // 
            this.lblRetries.AutoSize = true;
            this.lblRetries.Location = new System.Drawing.Point(11, 168);
            this.lblRetries.Name = "lblRetries";
            this.lblRetries.Size = new System.Drawing.Size(40, 13);
            this.lblRetries.TabIndex = 27;
            this.lblRetries.Text = "Retries";
            // 
            // chkDR
            // 
            this.chkDR.AutoSize = true;
            this.chkDR.Enabled = false;
            this.chkDR.Location = new System.Drawing.Point(11, 241);
            this.chkDR.Name = "chkDR";
            this.chkDR.Size = new System.Drawing.Size(42, 17);
            this.chkDR.TabIndex = 33;
            this.chkDR.Text = "DR";
            this.chkDR.UseVisualStyleBackColor = true;
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Location = new System.Drawing.Point(11, 93);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(41, 13);
            this.lblDevice.TabIndex = 25;
            this.lblDevice.Text = "Device";
            // 
            // txtUnitID
            // 
            this.txtUnitID.Enabled = false;
            this.txtUnitID.Location = new System.Drawing.Point(124, 15);
            this.txtUnitID.Name = "txtUnitID";
            this.txtUnitID.Size = new System.Drawing.Size(120, 20);
            this.txtUnitID.TabIndex = 22;
            // 
            // lblUI
            // 
            this.lblUI.AutoSize = true;
            this.lblUI.Location = new System.Drawing.Point(11, 18);
            this.lblUI.Name = "lblUI";
            this.lblUI.Size = new System.Drawing.Size(40, 13);
            this.lblUI.TabIndex = 21;
            this.lblUI.Text = "Unit ID";
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
            this.splitContainer1.Panel1.Controls.Add(this.grpIED);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvIEDDetails);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 700);
            this.splitContainer1.SplitterDistance = 281;
            this.splitContainer1.TabIndex = 36;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblIED);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 30);
            this.panel1.TabIndex = 36;
            // 
            // lblIED
            // 
            this.lblIED.AutoSize = true;
            this.lblIED.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIED.Location = new System.Drawing.Point(2, 5);
            this.lblIED.Name = "lblIED";
            this.lblIED.Size = new System.Drawing.Size(61, 15);
            this.lblIED.TabIndex = 0;
            this.lblIED.Text = "IED List:";
            // 
            // txtLinkAddressSize
            // 
            this.txtLinkAddressSize.Enabled = false;
            this.txtLinkAddressSize.Location = new System.Drawing.Point(123, 65);
            this.txtLinkAddressSize.Name = "txtLinkAddressSize";
            this.txtLinkAddressSize.Size = new System.Drawing.Size(120, 20);
            this.txtLinkAddressSize.TabIndex = 39;
            this.txtLinkAddressSize.Tag = "LinkAddressSize";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Link Address Size";
            // 
            // ucIED
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucIED";
            this.Size = new System.Drawing.Size(1050, 700);
            this.Load += new System.EventHandler(this.ucIED_Load);
            this.grpIED.ResumeLayout(false);
            this.grpIED.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ListView lvIEDDetails;
        private System.Windows.Forms.GroupBox grpIED;
        public System.Windows.Forms.TextBox txtTimeOut;
        private System.Windows.Forms.Label lblTO;
        public System.Windows.Forms.TextBox txtRetries;
        public System.Windows.Forms.TextBox txtASDUaddress;
        private System.Windows.Forms.Label lblASDU;
        private System.Windows.Forms.Label lblRetries;
        public System.Windows.Forms.CheckBox chkDR;
        private System.Windows.Forms.Label lblDevice;
        public System.Windows.Forms.TextBox txtUnitID;
        private System.Windows.Forms.Label lblUI;
        public System.Windows.Forms.TextBox txtDevice;
        public System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDesc;
        public System.Windows.Forms.TextBox txtTCPPort;
        private System.Windows.Forms.Label lblTCPPort;
        public System.Windows.Forms.TextBox txtRemoteIP;
        private System.Windows.Forms.Label lblRIP;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblIED;
        public System.Windows.Forms.TextBox txtLinkAddressSize;
        private System.Windows.Forms.Label label1;
    }
}
