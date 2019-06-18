namespace OpenProPlusConfigurator
{
    partial class ucProfileRecord
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
            this.lvProfile = new System.Windows.Forms.ListView();
            this.lblProfile = new System.Windows.Forms.Label();
            this.grpPR = new System.Windows.Forms.GroupBox();
            this.txtProfileInterval = new System.Windows.Forms.TextBox();
            this.lblPI = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpP = new System.Windows.Forms.GroupBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtAINo = new System.Windows.Forms.TextBox();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.txtLow = new System.Windows.Forms.TextBox();
            this.lblLow = new System.Windows.Forms.Label();
            this.txtProfileIndex = new System.Windows.Forms.TextBox();
            this.lblPIndex = new System.Windows.Forms.Label();
            this.txtDelay = new System.Windows.Forms.TextBox();
            this.lblDS = new System.Windows.Forms.Label();
            this.txtHigh = new System.Windows.Forms.TextBox();
            this.lblHigh = new System.Windows.Forms.Label();
            this.lblAI = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpPR.SuspendLayout();
            this.grpP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvProfile
            // 
            this.lvProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvProfile.CheckBoxes = true;
            this.lvProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvProfile.FullRowSelect = true;
            this.lvProfile.Location = new System.Drawing.Point(0, 13);
            this.lvProfile.MultiSelect = false;
            this.lvProfile.Name = "lvProfile";
            this.lvProfile.Size = new System.Drawing.Size(1050, 567);
            this.lvProfile.TabIndex = 11;
            this.lvProfile.UseCompatibleStateImageBehavior = false;
            this.lvProfile.View = System.Windows.Forms.View.Details;
            this.lvProfile.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvProfile_ItemCheck);
            this.lvProfile.DoubleClick += new System.EventHandler(this.lvProfile_DoubleClick);
            // 
            // lblProfile
            // 
            this.lblProfile.AutoSize = true;
            this.lblProfile.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfile.Location = new System.Drawing.Point(0, 0);
            this.lblProfile.Name = "lblProfile";
            this.lblProfile.Size = new System.Drawing.Size(67, 13);
            this.lblProfile.TabIndex = 10;
            this.lblProfile.Text = "Profile List";
            // 
            // grpPR
            // 
            this.grpPR.Controls.Add(this.txtProfileInterval);
            this.grpPR.Controls.Add(this.lblPI);
            this.grpPR.Location = new System.Drawing.Point(3, 2);
            this.grpPR.Name = "grpPR";
            this.grpPR.Size = new System.Drawing.Size(242, 66);
            this.grpPR.TabIndex = 12;
            this.grpPR.TabStop = false;
            this.grpPR.Text = "Profile Record";
            // 
            // txtProfileInterval
            // 
            this.txtProfileInterval.Location = new System.Drawing.Point(119, 24);
            this.txtProfileInterval.Name = "txtProfileInterval";
            this.txtProfileInterval.Size = new System.Drawing.Size(113, 20);
            this.txtProfileInterval.TabIndex = 1;
            this.txtProfileInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProfileInterval_KeyPress);
            // 
            // lblPI
            // 
            this.lblPI.AutoSize = true;
            this.lblPI.Location = new System.Drawing.Point(6, 27);
            this.lblPI.Name = "lblPI";
            this.lblPI.Size = new System.Drawing.Size(100, 13);
            this.lblPI.TabIndex = 0;
            this.lblPI.Text = "Profile Interval (sec)";
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
            // grpP
            // 
            this.grpP.BackColor = System.Drawing.SystemColors.Control;
            this.grpP.Controls.Add(this.btnLast);
            this.grpP.Controls.Add(this.btnNext);
            this.grpP.Controls.Add(this.btnPrev);
            this.grpP.Controls.Add(this.btnFirst);
            this.grpP.Controls.Add(this.txtAINo);
            this.grpP.Controls.Add(this.lblHdrText);
            this.grpP.Controls.Add(this.pbHdr);
            this.grpP.Controls.Add(this.btnCancel);
            this.grpP.Controls.Add(this.btnDone);
            this.grpP.Controls.Add(this.txtLow);
            this.grpP.Controls.Add(this.lblLow);
            this.grpP.Controls.Add(this.txtProfileIndex);
            this.grpP.Controls.Add(this.lblPIndex);
            this.grpP.Controls.Add(this.txtDelay);
            this.grpP.Controls.Add(this.lblDS);
            this.grpP.Controls.Add(this.txtHigh);
            this.grpP.Controls.Add(this.lblHigh);
            this.grpP.Controls.Add(this.lblAI);
            this.grpP.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpP.Location = new System.Drawing.Point(190, 75);
            this.grpP.Name = "grpP";
            this.grpP.Size = new System.Drawing.Size(278, 214);
            this.grpP.TabIndex = 21;
            this.grpP.TabStop = false;
            this.grpP.Visible = false;
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(205, 187);
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
            this.btnNext.Location = new System.Drawing.Point(142, 187);
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
            this.btnPrev.Location = new System.Drawing.Point(79, 187);
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
            this.btnFirst.Location = new System.Drawing.Point(16, 187);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 111;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtAINo
            // 
            this.txtAINo.Location = new System.Drawing.Point(111, 54);
            this.txtAINo.Name = "txtAINo";
            this.txtAINo.Size = new System.Drawing.Size(155, 20);
            this.txtAINo.TabIndex = 31;
            this.txtAINo.Tag = "AINo";
            this.txtAINo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAINo_KeyPress);
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(43, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "Profile";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(293, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(198, 154);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(111, 154);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 40;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // txtLow
            // 
            this.txtLow.Location = new System.Drawing.Point(111, 102);
            this.txtLow.Name = "txtLow";
            this.txtLow.Size = new System.Drawing.Size(155, 20);
            this.txtLow.TabIndex = 37;
            this.txtLow.Tag = "Low";
            this.txtLow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLow_KeyPress);
            // 
            // lblLow
            // 
            this.lblLow.AutoSize = true;
            this.lblLow.Location = new System.Drawing.Point(16, 105);
            this.lblLow.Name = "lblLow";
            this.lblLow.Size = new System.Drawing.Size(27, 13);
            this.lblLow.TabIndex = 36;
            this.lblLow.Text = "Low";
            // 
            // txtProfileIndex
            // 
            this.txtProfileIndex.Enabled = false;
            this.txtProfileIndex.Location = new System.Drawing.Point(111, 30);
            this.txtProfileIndex.Name = "txtProfileIndex";
            this.txtProfileIndex.Size = new System.Drawing.Size(155, 20);
            this.txtProfileIndex.TabIndex = 29;
            this.txtProfileIndex.Tag = "ProfileIndex";
            // 
            // lblPIndex
            // 
            this.lblPIndex.AutoSize = true;
            this.lblPIndex.Location = new System.Drawing.Point(16, 33);
            this.lblPIndex.Name = "lblPIndex";
            this.lblPIndex.Size = new System.Drawing.Size(65, 13);
            this.lblPIndex.TabIndex = 28;
            this.lblPIndex.Text = "Profile Index";
            // 
            // txtDelay
            // 
            this.txtDelay.Location = new System.Drawing.Point(111, 126);
            this.txtDelay.Name = "txtDelay";
            this.txtDelay.Size = new System.Drawing.Size(155, 20);
            this.txtDelay.TabIndex = 39;
            this.txtDelay.Tag = "DelaySec";
            this.txtDelay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDelay_KeyPress);
            // 
            // lblDS
            // 
            this.lblDS.AutoSize = true;
            this.lblDS.Location = new System.Drawing.Point(16, 129);
            this.lblDS.Name = "lblDS";
            this.lblDS.Size = new System.Drawing.Size(71, 13);
            this.lblDS.TabIndex = 38;
            this.lblDS.Text = "Delay (in sec)";
            // 
            // txtHigh
            // 
            this.txtHigh.Location = new System.Drawing.Point(111, 78);
            this.txtHigh.Name = "txtHigh";
            this.txtHigh.Size = new System.Drawing.Size(155, 20);
            this.txtHigh.TabIndex = 35;
            this.txtHigh.Tag = "High";
            this.txtHigh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHigh_KeyPress);
            // 
            // lblHigh
            // 
            this.lblHigh.AutoSize = true;
            this.lblHigh.Location = new System.Drawing.Point(16, 81);
            this.lblHigh.Name = "lblHigh";
            this.lblHigh.Size = new System.Drawing.Size(29, 13);
            this.lblHigh.TabIndex = 34;
            this.lblHigh.Text = "High";
            // 
            // lblAI
            // 
            this.lblAI.AutoSize = true;
            this.lblAI.Location = new System.Drawing.Point(16, 57);
            this.lblAI.Name = "lblAI";
            this.lblAI.Size = new System.Drawing.Size(37, 13);
            this.lblAI.TabIndex = 30;
            this.lblAI.Text = "AI No.";
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
            this.splitContainer1.Panel1.Controls.Add(this.grpPR);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpP);
            this.splitContainer1.Panel2.Controls.Add(this.lvProfile);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.lblProfile);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 700);
            this.splitContainer1.SplitterDistance = 71;
            this.splitContainer1.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 580);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 45);
            this.panel1.TabIndex = 22;
            // 
            // ucProfileRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucProfileRecord";
            this.Size = new System.Drawing.Size(1050, 700);
            this.grpPR.ResumeLayout(false);
            this.grpPR.PerformLayout();
            this.grpP.ResumeLayout(false);
            this.grpP.PerformLayout();
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

        public System.Windows.Forms.ListView lvProfile;
        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.GroupBox grpPR;
        public System.Windows.Forms.TextBox txtProfileInterval;
        private System.Windows.Forms.Label lblPI;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.GroupBox grpP;
        public System.Windows.Forms.TextBox txtAINo;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.TextBox txtLow;
        private System.Windows.Forms.Label lblLow;
        public System.Windows.Forms.TextBox txtProfileIndex;
        private System.Windows.Forms.Label lblPIndex;
        public System.Windows.Forms.TextBox txtDelay;
        private System.Windows.Forms.Label lblDS;
        public System.Windows.Forms.TextBox txtHigh;
        private System.Windows.Forms.Label lblHigh;
        private System.Windows.Forms.Label lblAI;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
    }
}
