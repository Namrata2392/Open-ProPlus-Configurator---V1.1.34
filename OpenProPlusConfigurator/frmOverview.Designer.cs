namespace OpenProPlusConfigurator
{
    partial class frmOverview
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpMappingView = new System.Windows.Forms.GroupBox();
            this.StatusCount = new System.Windows.Forms.StatusStrip();
            this.TspLabelCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkAO = new System.Windows.Forms.CheckBox();
            this.chkEN = new System.Windows.Forms.CheckBox();
            this.chkAI = new System.Windows.Forms.CheckBox();
            this.chkDO = new System.Windows.Forms.CheckBox();
            this.chkDI = new System.Windows.Forms.CheckBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.cmbSlaves = new System.Windows.Forms.ComboBox();
            this.cmbIED = new System.Windows.Forms.ComboBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.lblIED = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.lblSlaves = new System.Windows.Forms.Label();
            this.cmbMaster = new System.Windows.Forms.ComboBox();
            this.lblMasterType = new System.Windows.Forms.Label();
            this.lblMaster = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbMasterType = new System.Windows.Forms.ComboBox();
            this.lvMappingView = new System.Windows.Forms.ListView();
            this.grpMappingView.SuspendLayout();
            this.StatusCount.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMappingView
            // 
            this.grpMappingView.BackColor = System.Drawing.SystemColors.Control;
            this.grpMappingView.Controls.Add(this.StatusCount);
            this.grpMappingView.Controls.Add(this.groupBox1);
            this.grpMappingView.Controls.Add(this.lvMappingView);
            this.grpMappingView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMappingView.Location = new System.Drawing.Point(0, 0);
            this.grpMappingView.Name = "grpMappingView";
            this.grpMappingView.Size = new System.Drawing.Size(872, 504);
            this.grpMappingView.TabIndex = 8;
            this.grpMappingView.TabStop = false;
            this.grpMappingView.Resize += new System.EventHandler(this.grpMappingView_Resize);
            // 
            // StatusCount
            // 
            this.StatusCount.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TspLabelCount,
            this.tspCount});
            this.StatusCount.Location = new System.Drawing.Point(3, 479);
            this.StatusCount.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.StatusCount.Name = "StatusCount";
            this.StatusCount.Size = new System.Drawing.Size(866, 22);
            this.StatusCount.Stretch = false;
            this.StatusCount.TabIndex = 29;
            this.StatusCount.Text = "statusStrip1";
            // 
            // TspLabelCount
            // 
            this.TspLabelCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TspLabelCount.Margin = new System.Windows.Forms.Padding(8, 3, 0, 2);
            this.TspLabelCount.Name = "TspLabelCount";
            this.TspLabelCount.Size = new System.Drawing.Size(89, 17);
            this.TspLabelCount.Text = "Total Records :";
            // 
            // tspCount
            // 
            this.tspCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspCount.Name = "tspCount";
            this.tspCount.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.btnExportToExcel);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.cmbSlaves);
            this.groupBox1.Controls.Add(this.cmbIED);
            this.groupBox1.Controls.Add(this.cmbStatus);
            this.groupBox1.Controls.Add(this.lblHdrText);
            this.groupBox1.Controls.Add(this.lblIED);
            this.groupBox1.Controls.Add(this.pbHdr);
            this.groupBox1.Controls.Add(this.lblSlaves);
            this.groupBox1.Controls.Add(this.cmbMaster);
            this.groupBox1.Controls.Add(this.lblMasterType);
            this.groupBox1.Controls.Add(this.lblMaster);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.cmbMasterType);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(12, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(826, 106);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.BackColor = System.Drawing.SystemColors.Control;
            this.btnExportToExcel.Location = new System.Drawing.Point(767, 35);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(53, 47);
            this.btnExportToExcel.TabIndex = 12;
            this.btnExportToExcel.Text = "Export To Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = false;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.SystemColors.Control;
            this.btnReset.Location = new System.Drawing.Point(98, 62);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(86, 27);
            this.btnReset.TabIndex = 11;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.chkAO);
            this.groupBox2.Controls.Add(this.chkEN);
            this.groupBox2.Controls.Add(this.chkAI);
            this.groupBox2.Controls.Add(this.chkDO);
            this.groupBox2.Controls.Add(this.chkDI);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(372, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(225, 50);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Points";
            // 
            // chkAO
            // 
            this.chkAO.AutoSize = true;
            this.chkAO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAO.Location = new System.Drawing.Point(48, 19);
            this.chkAO.Name = "chkAO";
            this.chkAO.Size = new System.Drawing.Size(41, 17);
            this.chkAO.TabIndex = 32;
            this.chkAO.Text = "AO";
            this.chkAO.UseVisualStyleBackColor = true;
            // 
            // chkEN
            // 
            this.chkEN.AutoSize = true;
            this.chkEN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEN.Location = new System.Drawing.Point(175, 19);
            this.chkEN.Name = "chkEN";
            this.chkEN.Size = new System.Drawing.Size(41, 17);
            this.chkEN.TabIndex = 31;
            this.chkEN.Text = "EN";
            this.chkEN.UseVisualStyleBackColor = true;
            // 
            // chkAI
            // 
            this.chkAI.AutoSize = true;
            this.chkAI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAI.Location = new System.Drawing.Point(6, 19);
            this.chkAI.Name = "chkAI";
            this.chkAI.Size = new System.Drawing.Size(36, 17);
            this.chkAI.TabIndex = 28;
            this.chkAI.Text = "AI";
            this.chkAI.UseVisualStyleBackColor = true;
            // 
            // chkDO
            // 
            this.chkDO.AutoSize = true;
            this.chkDO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDO.Location = new System.Drawing.Point(131, 19);
            this.chkDO.Name = "chkDO";
            this.chkDO.Size = new System.Drawing.Size(42, 17);
            this.chkDO.TabIndex = 30;
            this.chkDO.Text = "DO";
            this.chkDO.UseVisualStyleBackColor = true;
            // 
            // chkDI
            // 
            this.chkDI.AutoSize = true;
            this.chkDI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDI.Location = new System.Drawing.Point(91, 19);
            this.chkDI.Name = "chkDI";
            this.chkDI.Size = new System.Drawing.Size(37, 17);
            this.chkDI.TabIndex = 29;
            this.chkDI.Text = "DI";
            this.chkDI.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.SystemColors.Control;
            this.btnApply.Location = new System.Drawing.Point(6, 64);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(86, 27);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // cmbSlaves
            // 
            this.cmbSlaves.FormattingEnabled = true;
            this.cmbSlaves.Location = new System.Drawing.Point(652, 64);
            this.cmbSlaves.Name = "cmbSlaves";
            this.cmbSlaves.Size = new System.Drawing.Size(110, 21);
            this.cmbSlaves.TabIndex = 20;
            this.cmbSlaves.SelectedIndexChanged += new System.EventHandler(this.cmbSlaves_SelectedIndexChanged);
            // 
            // cmbIED
            // 
            this.cmbIED.FormattingEnabled = true;
            this.cmbIED.Location = new System.Drawing.Point(256, 32);
            this.cmbIED.Name = "cmbIED";
            this.cmbIED.Size = new System.Drawing.Size(110, 21);
            this.cmbIED.TabIndex = 26;
            this.cmbIED.SelectedIndexChanged += new System.EventHandler(this.cmbIED_SelectedIndexChanged);
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(652, 34);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(110, 21);
            this.cmbStatus.TabIndex = 19;
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(65, 13);
            this.lblHdrText.TabIndex = 42;
            this.lblHdrText.Text = "OverView ";
            // 
            // lblIED
            // 
            this.lblIED.AutoSize = true;
            this.lblIED.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIED.Location = new System.Drawing.Point(222, 35);
            this.lblIED.Name = "lblIED";
            this.lblIED.Size = new System.Drawing.Size(28, 13);
            this.lblIED.TabIndex = 25;
            this.lblIED.Text = "IED";
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(826, 22);
            this.pbHdr.TabIndex = 41;
            this.pbHdr.TabStop = false;
            this.pbHdr.Paint += new System.Windows.Forms.PaintEventHandler(this.pbHdr_Paint);
            // 
            // lblSlaves
            // 
            this.lblSlaves.AutoSize = true;
            this.lblSlaves.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlaves.Location = new System.Drawing.Point(603, 69);
            this.lblSlaves.Name = "lblSlaves";
            this.lblSlaves.Size = new System.Drawing.Size(45, 13);
            this.lblSlaves.TabIndex = 14;
            this.lblSlaves.Text = "Slaves";
            // 
            // cmbMaster
            // 
            this.cmbMaster.FormattingEnabled = true;
            this.cmbMaster.Location = new System.Drawing.Point(256, 66);
            this.cmbMaster.Name = "cmbMaster";
            this.cmbMaster.Size = new System.Drawing.Size(110, 21);
            this.cmbMaster.TabIndex = 24;
            this.cmbMaster.SelectedIndexChanged += new System.EventHandler(this.cmbMaster_SelectedIndexChanged);
            // 
            // lblMasterType
            // 
            this.lblMasterType.AutoSize = true;
            this.lblMasterType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMasterType.Location = new System.Drawing.Point(10, 35);
            this.lblMasterType.Name = "lblMasterType";
            this.lblMasterType.Size = new System.Drawing.Size(77, 13);
            this.lblMasterType.TabIndex = 21;
            this.lblMasterType.Text = "Master Type";
            // 
            // lblMaster
            // 
            this.lblMaster.AutoSize = true;
            this.lblMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaster.Location = new System.Drawing.Point(205, 69);
            this.lblMaster.Name = "lblMaster";
            this.lblMaster.Size = new System.Drawing.Size(45, 13);
            this.lblMaster.TabIndex = 23;
            this.lblMaster.Text = "Master";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(603, 35);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(43, 13);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Status";
            // 
            // cmbMasterType
            // 
            this.cmbMasterType.BackColor = System.Drawing.SystemColors.Window;
            this.cmbMasterType.FormattingEnabled = true;
            this.cmbMasterType.Location = new System.Drawing.Point(93, 32);
            this.cmbMasterType.Name = "cmbMasterType";
            this.cmbMasterType.Size = new System.Drawing.Size(110, 21);
            this.cmbMasterType.TabIndex = 22;
            this.cmbMasterType.SelectedIndexChanged += new System.EventHandler(this.cmbMasterType_SelectedIndexChanged);
            // 
            // lvMappingView
            // 
            this.lvMappingView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvMappingView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMappingView.FullRowSelect = true;
            this.lvMappingView.HideSelection = false;
            this.lvMappingView.Location = new System.Drawing.Point(12, 131);
            this.lvMappingView.MultiSelect = false;
            this.lvMappingView.Name = "lvMappingView";
            this.lvMappingView.OwnerDraw = true;
            this.lvMappingView.Size = new System.Drawing.Size(826, 339);
            this.lvMappingView.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lvMappingView.TabIndex = 10;
            this.lvMappingView.UseCompatibleStateImageBehavior = false;
            this.lvMappingView.View = System.Windows.Forms.View.Details;
            this.lvMappingView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMappingView_ColumnClick);
            this.lvMappingView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvMappingView_DrawColumnHeader);
            this.lvMappingView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvMappingView_DrawSubItem);
            // 
            // frmOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 504);
            this.Controls.Add(this.grpMappingView);
            this.Name = "frmOverview";
            this.Text = "Overview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmOverview_Load);
            this.grpMappingView.ResumeLayout(false);
            this.grpMappingView.PerformLayout();
            this.StatusCount.ResumeLayout(false);
            this.StatusCount.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMappingView;
        public System.Windows.Forms.ListView lvMappingView;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblSlaves;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbSlaves;
        private System.Windows.Forms.ComboBox cmbMasterType;
        private System.Windows.Forms.Label lblMasterType;
        private System.Windows.Forms.ComboBox cmbIED;
        private System.Windows.Forms.Label lblIED;
        private System.Windows.Forms.ComboBox cmbMaster;
        private System.Windows.Forms.Label lblMaster;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.CheckBox chkEN;
        private System.Windows.Forms.CheckBox chkAI;
        private System.Windows.Forms.CheckBox chkDO;
        private System.Windows.Forms.CheckBox chkDI;
        private System.Windows.Forms.CheckBox chkAO;
        private System.Windows.Forms.StatusStrip StatusCount;
        private System.Windows.Forms.ToolStripStatusLabel TspLabelCount;
        private System.Windows.Forms.ToolStripStatusLabel tspCount;
        private System.Windows.Forms.Button btnExportToExcel;
    }
}