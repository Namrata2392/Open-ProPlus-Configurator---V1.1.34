namespace OpenProPlusConfigurator
{
    partial class ucDerivedParam
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
            this.lvDP = new System.Windows.Forms.ListView();
            this.lblDP = new System.Windows.Forms.Label();
            this.grpDP = new System.Windows.Forms.GroupBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtDelayMS = new System.Windows.Forms.TextBox();
            this.lblDM = new System.Windows.Forms.Label();
            this.cmbOperation = new System.Windows.Forms.ComboBox();
            this.txtAINo2 = new System.Windows.Forms.TextBox();
            this.txtAINo1 = new System.Windows.Forms.TextBox();
            this.lblOpr = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.txtDPIndex = new System.Windows.Forms.TextBox();
            this.lblDPI = new System.Windows.Forms.Label();
            this.lblAI2 = new System.Windows.Forms.Label();
            this.lblAI1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpDP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvDP
            // 
            this.lvDP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvDP.CheckBoxes = true;
            this.lvDP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDP.FullRowSelect = true;
            this.lvDP.Location = new System.Drawing.Point(0, 30);
            this.lvDP.MultiSelect = false;
            this.lvDP.Name = "lvDP";
            this.lvDP.Size = new System.Drawing.Size(1050, 620);
            this.lvDP.TabIndex = 9;
            this.lvDP.UseCompatibleStateImageBehavior = false;
            this.lvDP.View = System.Windows.Forms.View.Details;
            this.lvDP.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvDP_ItemCheck);
            this.lvDP.DoubleClick += new System.EventHandler(this.lvDP_DoubleClick);
            // 
            // lblDP
            // 
            this.lblDP.AutoSize = true;
            this.lblDP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDP.Location = new System.Drawing.Point(3, 8);
            this.lblDP.Name = "lblDP";
            this.lblDP.Size = new System.Drawing.Size(118, 13);
            this.lblDP.TabIndex = 8;
            this.lblDP.Text = "Derived Parameters";
            // 
            // grpDP
            // 
            this.grpDP.BackColor = System.Drawing.SystemColors.Control;
            this.grpDP.Controls.Add(this.btnLast);
            this.grpDP.Controls.Add(this.btnNext);
            this.grpDP.Controls.Add(this.btnPrev);
            this.grpDP.Controls.Add(this.btnFirst);
            this.grpDP.Controls.Add(this.txtDelayMS);
            this.grpDP.Controls.Add(this.lblDM);
            this.grpDP.Controls.Add(this.cmbOperation);
            this.grpDP.Controls.Add(this.txtAINo2);
            this.grpDP.Controls.Add(this.txtAINo1);
            this.grpDP.Controls.Add(this.lblOpr);
            this.grpDP.Controls.Add(this.lblHdrText);
            this.grpDP.Controls.Add(this.pbHdr);
            this.grpDP.Controls.Add(this.btnCancel);
            this.grpDP.Controls.Add(this.btnDone);
            this.grpDP.Controls.Add(this.txtDPIndex);
            this.grpDP.Controls.Add(this.lblDPI);
            this.grpDP.Controls.Add(this.lblAI2);
            this.grpDP.Controls.Add(this.lblAI1);
            this.grpDP.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpDP.Location = new System.Drawing.Point(331, 95);
            this.grpDP.Name = "grpDP";
            this.grpDP.Size = new System.Drawing.Size(277, 217);
            this.grpDP.TabIndex = 17;
            this.grpDP.TabStop = false;
            this.grpDP.Visible = false;
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(204, 189);
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
            this.btnNext.Location = new System.Drawing.Point(140, 189);
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
            this.btnPrev.Location = new System.Drawing.Point(76, 189);
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
            this.btnFirst.Location = new System.Drawing.Point(12, 189);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 111;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtDelayMS
            // 
            this.txtDelayMS.Location = new System.Drawing.Point(97, 130);
            this.txtDelayMS.Name = "txtDelayMS";
            this.txtDelayMS.Size = new System.Drawing.Size(167, 20);
            this.txtDelayMS.TabIndex = 46;
            this.txtDelayMS.Tag = "DelayMS";
            this.txtDelayMS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDelayMS_KeyPress);
            // 
            // lblDM
            // 
            this.lblDM.AutoSize = true;
            this.lblDM.Location = new System.Drawing.Point(12, 133);
            this.lblDM.Name = "lblDM";
            this.lblDM.Size = new System.Drawing.Size(56, 13);
            this.lblDM.TabIndex = 45;
            this.lblDM.Text = "Delay (ms)";
            // 
            // cmbOperation
            // 
            this.cmbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperation.FormattingEnabled = true;
            this.cmbOperation.Location = new System.Drawing.Point(96, 104);
            this.cmbOperation.Name = "cmbOperation";
            this.cmbOperation.Size = new System.Drawing.Size(167, 21);
            this.cmbOperation.TabIndex = 42;
            this.cmbOperation.Tag = "Operation";
            // 
            // txtAINo2
            // 
            this.txtAINo2.Location = new System.Drawing.Point(97, 79);
            this.txtAINo2.Name = "txtAINo2";
            this.txtAINo2.Size = new System.Drawing.Size(167, 20);
            this.txtAINo2.TabIndex = 40;
            this.txtAINo2.Tag = "AINo2";
            this.txtAINo2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAINo2_KeyPress);
            // 
            // txtAINo1
            // 
            this.txtAINo1.Location = new System.Drawing.Point(97, 54);
            this.txtAINo1.Name = "txtAINo1";
            this.txtAINo1.Size = new System.Drawing.Size(167, 20);
            this.txtAINo1.TabIndex = 38;
            this.txtAINo1.Tag = "AINo1";
            this.txtAINo1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAINo1_KeyPress);
            // 
            // lblOpr
            // 
            this.lblOpr.AutoSize = true;
            this.lblOpr.Location = new System.Drawing.Point(12, 107);
            this.lblOpr.Name = "lblOpr";
            this.lblOpr.Size = new System.Drawing.Size(53, 13);
            this.lblOpr.TabIndex = 41;
            this.lblOpr.Text = "Operation";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(24, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "DP";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(277, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(196, 156);
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
            this.btnDone.Location = new System.Drawing.Point(97, 156);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 47;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // txtDPIndex
            // 
            this.txtDPIndex.Enabled = false;
            this.txtDPIndex.Location = new System.Drawing.Point(97, 29);
            this.txtDPIndex.Name = "txtDPIndex";
            this.txtDPIndex.Size = new System.Drawing.Size(167, 20);
            this.txtDPIndex.TabIndex = 36;
            this.txtDPIndex.Tag = "DPIndex";
            // 
            // lblDPI
            // 
            this.lblDPI.AutoSize = true;
            this.lblDPI.Location = new System.Drawing.Point(12, 32);
            this.lblDPI.Name = "lblDPI";
            this.lblDPI.Size = new System.Drawing.Size(51, 13);
            this.lblDPI.TabIndex = 35;
            this.lblDPI.Text = "DP Index";
            // 
            // lblAI2
            // 
            this.lblAI2.AutoSize = true;
            this.lblAI2.Location = new System.Drawing.Point(12, 82);
            this.lblAI2.Name = "lblAI2";
            this.lblAI2.Size = new System.Drawing.Size(46, 13);
            this.lblAI2.TabIndex = 39;
            this.lblAI2.Text = "AI No. 2";
            // 
            // lblAI1
            // 
            this.lblAI1.AutoSize = true;
            this.lblAI1.Location = new System.Drawing.Point(12, 57);
            this.lblAI1.Name = "lblAI1";
            this.lblAI1.Size = new System.Drawing.Size(46, 13);
            this.lblAI1.TabIndex = 37;
            this.lblAI1.Text = "AI No. 1";
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
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDP);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 30);
            this.panel1.TabIndex = 21;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 650);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1050, 50);
            this.panel2.TabIndex = 22;
            // 
            // ucDerivedParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpDP);
            this.Controls.Add(this.lvDP);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucDerivedParam";
            this.Size = new System.Drawing.Size(1050, 700);
            this.grpDP.ResumeLayout(false);
            this.grpDP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lvDP;
        private System.Windows.Forms.Label lblDP;
        public System.Windows.Forms.GroupBox grpDP;
        private System.Windows.Forms.Label lblOpr;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.TextBox txtDPIndex;
        private System.Windows.Forms.Label lblDPI;
        private System.Windows.Forms.Label lblAI2;
        private System.Windows.Forms.Label lblAI1;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.TextBox txtAINo2;
        public System.Windows.Forms.TextBox txtAINo1;
        public System.Windows.Forms.ComboBox cmbOperation;
        public System.Windows.Forms.TextBox txtDelayMS;
        private System.Windows.Forms.Label lblDM;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
