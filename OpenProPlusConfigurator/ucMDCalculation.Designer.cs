namespace OpenProPlusConfigurator
{
    partial class ucMDCalculation
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
            this.lvMD = new System.Windows.Forms.ListView();
            this.lblMD = new System.Windows.Forms.Label();
            this.grpMDC = new System.Windows.Forms.GroupBox();
            this.txtSlidingWindowTime = new System.Windows.Forms.TextBox();
            this.lblSWT = new System.Windows.Forms.Label();
            this.txtWindowTime = new System.Windows.Forms.TextBox();
            this.lblWT = new System.Windows.Forms.Label();
            this.grpMD = new System.Windows.Forms.GroupBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtI2AIno = new System.Windows.Forms.TextBox();
            this.txtI1AIno = new System.Windows.Forms.TextBox();
            this.txtVAIno = new System.Windows.Forms.TextBox();
            this.lblI2AI = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.txtHigh = new System.Windows.Forms.TextBox();
            this.lblHigh = new System.Windows.Forms.Label();
            this.txtMDIndex = new System.Windows.Forms.TextBox();
            this.lblMI = new System.Windows.Forms.Label();
            this.txtMultiplier = new System.Windows.Forms.TextBox();
            this.txtI3AIno = new System.Windows.Forms.TextBox();
            this.lblI3AI = new System.Windows.Forms.Label();
            this.lblMult = new System.Windows.Forms.Label();
            this.lblI1AI = new System.Windows.Forms.Label();
            this.lblVAI = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpMDC.SuspendLayout();
            this.grpMD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMD
            // 
            this.lvMD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvMD.CheckBoxes = true;
            this.lvMD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMD.FullRowSelect = true;
            this.lvMD.Location = new System.Drawing.Point(0, 13);
            this.lvMD.MultiSelect = false;
            this.lvMD.Name = "lvMD";
            this.lvMD.Size = new System.Drawing.Size(1050, 550);
            this.lvMD.TabIndex = 11;
            this.lvMD.UseCompatibleStateImageBehavior = false;
            this.lvMD.View = System.Windows.Forms.View.Details;
            this.lvMD.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvMD_ItemCheck);
            this.lvMD.DoubleClick += new System.EventHandler(this.lvMD_DoubleClick);
            // 
            // lblMD
            // 
            this.lblMD.AutoSize = true;
            this.lblMD.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMD.Location = new System.Drawing.Point(0, 0);
            this.lblMD.Name = "lblMD";
            this.lblMD.Size = new System.Drawing.Size(50, 13);
            this.lblMD.TabIndex = 10;
            this.lblMD.Text = "MD List";
            // 
            // grpMDC
            // 
            this.grpMDC.Controls.Add(this.txtSlidingWindowTime);
            this.grpMDC.Controls.Add(this.lblSWT);
            this.grpMDC.Controls.Add(this.txtWindowTime);
            this.grpMDC.Controls.Add(this.lblWT);
            this.grpMDC.Location = new System.Drawing.Point(3, 3);
            this.grpMDC.Name = "grpMDC";
            this.grpMDC.Size = new System.Drawing.Size(502, 82);
            this.grpMDC.TabIndex = 13;
            this.grpMDC.TabStop = false;
            this.grpMDC.Text = "MD Calculation";
            // 
            // txtSlidingWindowTime
            // 
            this.txtSlidingWindowTime.Location = new System.Drawing.Point(391, 34);
            this.txtSlidingWindowTime.Name = "txtSlidingWindowTime";
            this.txtSlidingWindowTime.Size = new System.Drawing.Size(94, 20);
            this.txtSlidingWindowTime.TabIndex = 3;
            // 
            // lblSWT
            // 
            this.lblSWT.AutoSize = true;
            this.lblSWT.Location = new System.Drawing.Point(251, 37);
            this.lblSWT.Name = "lblSWT";
            this.lblSWT.Size = new System.Drawing.Size(131, 13);
            this.lblSWT.TabIndex = 2;
            this.lblSWT.Text = "Sliding Window Time (min)";
            // 
            // txtWindowTime
            // 
            this.txtWindowTime.Location = new System.Drawing.Point(147, 34);
            this.txtWindowTime.Name = "txtWindowTime";
            this.txtWindowTime.Size = new System.Drawing.Size(94, 20);
            this.txtWindowTime.TabIndex = 1;
            // 
            // lblWT
            // 
            this.lblWT.AutoSize = true;
            this.lblWT.Location = new System.Drawing.Point(10, 37);
            this.lblWT.Name = "lblWT";
            this.lblWT.Size = new System.Drawing.Size(129, 13);
            this.lblWT.TabIndex = 0;
            this.lblWT.Text = "Window/Block Time (min)";
            // 
            // grpMD
            // 
            this.grpMD.BackColor = System.Drawing.SystemColors.Control;
            this.grpMD.Controls.Add(this.btnLast);
            this.grpMD.Controls.Add(this.btnNext);
            this.grpMD.Controls.Add(this.btnPrev);
            this.grpMD.Controls.Add(this.btnFirst);
            this.grpMD.Controls.Add(this.txtI2AIno);
            this.grpMD.Controls.Add(this.txtI1AIno);
            this.grpMD.Controls.Add(this.txtVAIno);
            this.grpMD.Controls.Add(this.lblI2AI);
            this.grpMD.Controls.Add(this.lblHdrText);
            this.grpMD.Controls.Add(this.pbHdr);
            this.grpMD.Controls.Add(this.btnCancel);
            this.grpMD.Controls.Add(this.btnDone);
            this.grpMD.Controls.Add(this.txtHigh);
            this.grpMD.Controls.Add(this.lblHigh);
            this.grpMD.Controls.Add(this.txtMDIndex);
            this.grpMD.Controls.Add(this.lblMI);
            this.grpMD.Controls.Add(this.txtMultiplier);
            this.grpMD.Controls.Add(this.txtI3AIno);
            this.grpMD.Controls.Add(this.lblI3AI);
            this.grpMD.Controls.Add(this.lblMult);
            this.grpMD.Controls.Add(this.lblI1AI);
            this.grpMD.Controls.Add(this.lblVAI);
            this.grpMD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpMD.Location = new System.Drawing.Point(374, 110);
            this.grpMD.Name = "grpMD";
            this.grpMD.Size = new System.Drawing.Size(284, 265);
            this.grpMD.TabIndex = 18;
            this.grpMD.TabStop = false;
            this.grpMD.Visible = false;
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(212, 240);
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
            this.btnNext.Location = new System.Drawing.Point(148, 240);
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
            this.btnPrev.Location = new System.Drawing.Point(84, 240);
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
            this.btnFirst.Location = new System.Drawing.Point(20, 240);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 111;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtI2AIno
            // 
            this.txtI2AIno.Location = new System.Drawing.Point(99, 105);
            this.txtI2AIno.Name = "txtI2AIno";
            this.txtI2AIno.Size = new System.Drawing.Size(166, 20);
            this.txtI2AIno.TabIndex = 38;
            this.txtI2AIno.Tag = "I2_AINo";
            this.txtI2AIno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtI2AIno_KeyPress);
            // 
            // txtI1AIno
            // 
            this.txtI1AIno.Location = new System.Drawing.Point(99, 80);
            this.txtI1AIno.Name = "txtI1AIno";
            this.txtI1AIno.Size = new System.Drawing.Size(166, 20);
            this.txtI1AIno.TabIndex = 36;
            this.txtI1AIno.Tag = "I1_AINo";
            this.txtI1AIno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtI1AIno_KeyPress);
            // 
            // txtVAIno
            // 
            this.txtVAIno.Location = new System.Drawing.Point(99, 55);
            this.txtVAIno.Name = "txtVAIno";
            this.txtVAIno.Size = new System.Drawing.Size(166, 20);
            this.txtVAIno.TabIndex = 34;
            this.txtVAIno.Tag = "V_AINo";
            this.txtVAIno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVAIno_KeyPress);
            // 
            // lblI2AI
            // 
            this.lblI2AI.AutoSize = true;
            this.lblI2AI.Location = new System.Drawing.Point(12, 108);
            this.lblI2AI.Name = "lblI2AI";
            this.lblI2AI.Size = new System.Drawing.Size(49, 13);
            this.lblI2AI.TabIndex = 37;
            this.lblI2AI.Text = "I2 AI No.";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(26, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "MD";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(289, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(197, 206);
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
            this.btnDone.Location = new System.Drawing.Point(99, 206);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 47;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // txtHigh
            // 
            this.txtHigh.Location = new System.Drawing.Point(99, 180);
            this.txtHigh.Name = "txtHigh";
            this.txtHigh.Size = new System.Drawing.Size(166, 20);
            this.txtHigh.TabIndex = 44;
            this.txtHigh.Tag = "High";
            this.txtHigh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHigh_KeyPress);
            // 
            // lblHigh
            // 
            this.lblHigh.AutoSize = true;
            this.lblHigh.Location = new System.Drawing.Point(12, 183);
            this.lblHigh.Name = "lblHigh";
            this.lblHigh.Size = new System.Drawing.Size(29, 13);
            this.lblHigh.TabIndex = 43;
            this.lblHigh.Text = "High";
            // 
            // txtMDIndex
            // 
            this.txtMDIndex.Enabled = false;
            this.txtMDIndex.Location = new System.Drawing.Point(99, 30);
            this.txtMDIndex.Name = "txtMDIndex";
            this.txtMDIndex.Size = new System.Drawing.Size(166, 20);
            this.txtMDIndex.TabIndex = 32;
            this.txtMDIndex.Tag = "MDIndex";
            // 
            // lblMI
            // 
            this.lblMI.AutoSize = true;
            this.lblMI.Location = new System.Drawing.Point(12, 33);
            this.lblMI.Name = "lblMI";
            this.lblMI.Size = new System.Drawing.Size(53, 13);
            this.lblMI.TabIndex = 31;
            this.lblMI.Text = "MD Index";
            // 
            // txtMultiplier
            // 
            this.txtMultiplier.Location = new System.Drawing.Point(99, 155);
            this.txtMultiplier.Name = "txtMultiplier";
            this.txtMultiplier.Size = new System.Drawing.Size(166, 20);
            this.txtMultiplier.TabIndex = 42;
            this.txtMultiplier.Tag = "Multiplier";
            this.txtMultiplier.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMultiplier_KeyPress);
            // 
            // txtI3AIno
            // 
            this.txtI3AIno.Location = new System.Drawing.Point(99, 130);
            this.txtI3AIno.Name = "txtI3AIno";
            this.txtI3AIno.Size = new System.Drawing.Size(166, 20);
            this.txtI3AIno.TabIndex = 40;
            this.txtI3AIno.Tag = "I3_AINo";
            this.txtI3AIno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtI3AIno_KeyPress);
            // 
            // lblI3AI
            // 
            this.lblI3AI.AutoSize = true;
            this.lblI3AI.Location = new System.Drawing.Point(12, 133);
            this.lblI3AI.Name = "lblI3AI";
            this.lblI3AI.Size = new System.Drawing.Size(49, 13);
            this.lblI3AI.TabIndex = 39;
            this.lblI3AI.Text = "I3 AI No.";
            // 
            // lblMult
            // 
            this.lblMult.AutoSize = true;
            this.lblMult.Location = new System.Drawing.Point(12, 158);
            this.lblMult.Name = "lblMult";
            this.lblMult.Size = new System.Drawing.Size(48, 13);
            this.lblMult.TabIndex = 41;
            this.lblMult.Text = "Multiplier";
            this.lblMult.Click += new System.EventHandler(this.lblMult_Click);
            // 
            // lblI1AI
            // 
            this.lblI1AI.AutoSize = true;
            this.lblI1AI.Location = new System.Drawing.Point(12, 83);
            this.lblI1AI.Name = "lblI1AI";
            this.lblI1AI.Size = new System.Drawing.Size(49, 13);
            this.lblI1AI.TabIndex = 35;
            this.lblI1AI.Text = "I1 AI No.";
            // 
            // lblVAI
            // 
            this.lblVAI.AutoSize = true;
            this.lblVAI.Location = new System.Drawing.Point(12, 58);
            this.lblVAI.Name = "lblVAI";
            this.lblVAI.Size = new System.Drawing.Size(47, 13);
            this.lblVAI.TabIndex = 33;
            this.lblVAI.Text = "V AI No.";
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(80, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 28);
            this.btnDelete.TabIndex = 22;
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
            this.btnAdd.TabIndex = 21;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.grpMDC);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpMD);
            this.splitContainer1.Panel2.Controls.Add(this.lvMD);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.lblMD);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 700);
            this.splitContainer1.SplitterDistance = 88;
            this.splitContainer1.TabIndex = 23;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 563);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 45);
            this.panel1.TabIndex = 19;
            // 
            // ucMDCalculation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucMDCalculation";
            this.Size = new System.Drawing.Size(1050, 700);
            this.grpMDC.ResumeLayout(false);
            this.grpMDC.PerformLayout();
            this.grpMD.ResumeLayout(false);
            this.grpMD.PerformLayout();
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

        public System.Windows.Forms.ListView lvMD;
        private System.Windows.Forms.Label lblMD;
        private System.Windows.Forms.GroupBox grpMDC;
        public System.Windows.Forms.TextBox txtWindowTime;
        private System.Windows.Forms.Label lblWT;
        public System.Windows.Forms.TextBox txtSlidingWindowTime;
        private System.Windows.Forms.Label lblSWT;
        public System.Windows.Forms.GroupBox grpMD;
        public System.Windows.Forms.TextBox txtI2AIno;
        public System.Windows.Forms.TextBox txtI1AIno;
        public System.Windows.Forms.TextBox txtVAIno;
        private System.Windows.Forms.Label lblI2AI;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.TextBox txtHigh;
        private System.Windows.Forms.Label lblHigh;
        public System.Windows.Forms.TextBox txtMDIndex;
        private System.Windows.Forms.Label lblMI;
        public System.Windows.Forms.TextBox txtMultiplier;
        public System.Windows.Forms.TextBox txtI3AIno;
        private System.Windows.Forms.Label lblI3AI;
        private System.Windows.Forms.Label lblMult;
        private System.Windows.Forms.Label lblI1AI;
        private System.Windows.Forms.Label lblVAI;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
    }
}
