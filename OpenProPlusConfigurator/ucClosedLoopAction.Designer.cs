namespace OpenProPlusConfigurator
{
    partial class ucClosedLoopAction
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
            this.lvCLA = new System.Windows.Forms.ListView();
            this.lblCLA = new System.Windows.Forms.Label();
            this.grpCLA = new System.Windows.Forms.GroupBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtDONo = new System.Windows.Forms.TextBox();
            this.txtAINo2 = new System.Windows.Forms.TextBox();
            this.txtAINo1 = new System.Windows.Forms.TextBox();
            this.lblDO = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.txtLow = new System.Windows.Forms.TextBox();
            this.lblLow = new System.Windows.Forms.Label();
            this.txtCLAIndex = new System.Windows.Forms.TextBox();
            this.lblCI = new System.Windows.Forms.Label();
            this.txtDelay = new System.Windows.Forms.TextBox();
            this.lblDS = new System.Windows.Forms.Label();
            this.txtHigh = new System.Windows.Forms.TextBox();
            this.lblHigh = new System.Windows.Forms.Label();
            this.lblAI2 = new System.Windows.Forms.Label();
            this.lblAI1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpCLA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvCLA
            // 
            this.lvCLA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvCLA.CheckBoxes = true;
            this.lvCLA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCLA.FullRowSelect = true;
            this.lvCLA.Location = new System.Drawing.Point(0, 30);
            this.lvCLA.MultiSelect = false;
            this.lvCLA.Name = "lvCLA";
            this.lvCLA.Size = new System.Drawing.Size(1050, 620);
            this.lvCLA.TabIndex = 9;
            this.lvCLA.UseCompatibleStateImageBehavior = false;
            this.lvCLA.View = System.Windows.Forms.View.Details;
            this.lvCLA.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvCLA_ItemCheck);
            this.lvCLA.DoubleClick += new System.EventHandler(this.lvCLA_DoubleClick);
            // 
            // lblCLA
            // 
            this.lblCLA.AutoSize = true;
            this.lblCLA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCLA.Location = new System.Drawing.Point(3, 0);
            this.lblCLA.Name = "lblCLA";
            this.lblCLA.Size = new System.Drawing.Size(130, 15);
            this.lblCLA.TabIndex = 8;
            this.lblCLA.Text = "Closed Loop Action";
            // 
            // grpCLA
            // 
            this.grpCLA.BackColor = System.Drawing.SystemColors.Control;
            this.grpCLA.Controls.Add(this.btnLast);
            this.grpCLA.Controls.Add(this.btnNext);
            this.grpCLA.Controls.Add(this.btnPrev);
            this.grpCLA.Controls.Add(this.btnFirst);
            this.grpCLA.Controls.Add(this.txtDONo);
            this.grpCLA.Controls.Add(this.txtAINo2);
            this.grpCLA.Controls.Add(this.txtAINo1);
            this.grpCLA.Controls.Add(this.lblDO);
            this.grpCLA.Controls.Add(this.lblHdrText);
            this.grpCLA.Controls.Add(this.pbHdr);
            this.grpCLA.Controls.Add(this.btnCancel);
            this.grpCLA.Controls.Add(this.btnDone);
            this.grpCLA.Controls.Add(this.txtLow);
            this.grpCLA.Controls.Add(this.lblLow);
            this.grpCLA.Controls.Add(this.txtCLAIndex);
            this.grpCLA.Controls.Add(this.lblCI);
            this.grpCLA.Controls.Add(this.txtDelay);
            this.grpCLA.Controls.Add(this.lblDS);
            this.grpCLA.Controls.Add(this.txtHigh);
            this.grpCLA.Controls.Add(this.lblHigh);
            this.grpCLA.Controls.Add(this.lblAI2);
            this.grpCLA.Controls.Add(this.lblAI1);
            this.grpCLA.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpCLA.Location = new System.Drawing.Point(366, 141);
            this.grpCLA.Name = "grpCLA";
            this.grpCLA.Size = new System.Drawing.Size(285, 262);
            this.grpCLA.TabIndex = 17;
            this.grpCLA.TabStop = false;
            this.grpCLA.Visible = false;
            this.grpCLA.Enter += new System.EventHandler(this.grpCLA_Enter);
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(213, 232);
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
            this.btnNext.Location = new System.Drawing.Point(147, 232);
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
            this.btnPrev.Location = new System.Drawing.Point(81, 232);
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
            this.btnFirst.Location = new System.Drawing.Point(15, 232);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 111;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtDONo
            // 
            this.txtDONo.Location = new System.Drawing.Point(107, 99);
            this.txtDONo.Name = "txtDONo";
            this.txtDONo.Size = new System.Drawing.Size(165, 20);
            this.txtDONo.TabIndex = 35;
            this.txtDONo.Tag = "DONo";
            this.txtDONo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDONo_KeyPress);
            // 
            // txtAINo2
            // 
            this.txtAINo2.Location = new System.Drawing.Point(107, 75);
            this.txtAINo2.Name = "txtAINo2";
            this.txtAINo2.Size = new System.Drawing.Size(165, 20);
            this.txtAINo2.TabIndex = 33;
            this.txtAINo2.Tag = "AINo2";
            this.txtAINo2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAINo2_KeyPress);
            // 
            // txtAINo1
            // 
            this.txtAINo1.Location = new System.Drawing.Point(107, 51);
            this.txtAINo1.Name = "txtAINo1";
            this.txtAINo1.Size = new System.Drawing.Size(165, 20);
            this.txtAINo1.TabIndex = 31;
            this.txtAINo1.Tag = "AINo1";
            this.txtAINo1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAINo1_KeyPress);
            // 
            // lblDO
            // 
            this.lblDO.AutoSize = true;
            this.lblDO.Location = new System.Drawing.Point(15, 102);
            this.lblDO.Name = "lblDO";
            this.lblDO.Size = new System.Drawing.Size(43, 13);
            this.lblDO.TabIndex = 34;
            this.lblDO.Text = "DO No.";
            // 
            // lblHdrText
            // 
            this.lblHdrText.AutoSize = true;
            this.lblHdrText.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblHdrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHdrText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHdrText.Location = new System.Drawing.Point(10, 4);
            this.lblHdrText.Name = "lblHdrText";
            this.lblHdrText.Size = new System.Drawing.Size(30, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "CLA";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(285, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(204, 198);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Location = new System.Drawing.Point(107, 198);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 44;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // txtLow
            // 
            this.txtLow.Location = new System.Drawing.Point(107, 147);
            this.txtLow.Name = "txtLow";
            this.txtLow.Size = new System.Drawing.Size(165, 20);
            this.txtLow.TabIndex = 41;
            this.txtLow.Tag = "Low";
            this.txtLow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLow_KeyPress);
            // 
            // lblLow
            // 
            this.lblLow.AutoSize = true;
            this.lblLow.Location = new System.Drawing.Point(15, 150);
            this.lblLow.Name = "lblLow";
            this.lblLow.Size = new System.Drawing.Size(27, 13);
            this.lblLow.TabIndex = 40;
            this.lblLow.Text = "Low";
            // 
            // txtCLAIndex
            // 
            this.txtCLAIndex.Enabled = false;
            this.txtCLAIndex.Location = new System.Drawing.Point(107, 27);
            this.txtCLAIndex.Name = "txtCLAIndex";
            this.txtCLAIndex.Size = new System.Drawing.Size(165, 20);
            this.txtCLAIndex.TabIndex = 29;
            this.txtCLAIndex.Tag = "CLAIndex";
            // 
            // lblCI
            // 
            this.lblCI.AutoSize = true;
            this.lblCI.Location = new System.Drawing.Point(15, 30);
            this.lblCI.Name = "lblCI";
            this.lblCI.Size = new System.Drawing.Size(56, 13);
            this.lblCI.TabIndex = 28;
            this.lblCI.Text = "CLA Index";
            // 
            // txtDelay
            // 
            this.txtDelay.Location = new System.Drawing.Point(107, 171);
            this.txtDelay.Name = "txtDelay";
            this.txtDelay.Size = new System.Drawing.Size(165, 20);
            this.txtDelay.TabIndex = 43;
            this.txtDelay.Tag = "DelaySec";
            this.txtDelay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDelay_KeyPress);
            // 
            // lblDS
            // 
            this.lblDS.AutoSize = true;
            this.lblDS.Location = new System.Drawing.Point(15, 174);
            this.lblDS.Name = "lblDS";
            this.lblDS.Size = new System.Drawing.Size(71, 13);
            this.lblDS.TabIndex = 42;
            this.lblDS.Text = "Delay (in sec)";
            // 
            // txtHigh
            // 
            this.txtHigh.Location = new System.Drawing.Point(107, 123);
            this.txtHigh.Name = "txtHigh";
            this.txtHigh.Size = new System.Drawing.Size(165, 20);
            this.txtHigh.TabIndex = 39;
            this.txtHigh.Tag = "High";
            this.txtHigh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHigh_KeyPress);
            // 
            // lblHigh
            // 
            this.lblHigh.AutoSize = true;
            this.lblHigh.Location = new System.Drawing.Point(15, 126);
            this.lblHigh.Name = "lblHigh";
            this.lblHigh.Size = new System.Drawing.Size(29, 13);
            this.lblHigh.TabIndex = 38;
            this.lblHigh.Text = "High";
            // 
            // lblAI2
            // 
            this.lblAI2.AutoSize = true;
            this.lblAI2.Location = new System.Drawing.Point(15, 78);
            this.lblAI2.Name = "lblAI2";
            this.lblAI2.Size = new System.Drawing.Size(46, 13);
            this.lblAI2.TabIndex = 32;
            this.lblAI2.Text = "AI No. 2";
            // 
            // lblAI1
            // 
            this.lblAI1.AutoSize = true;
            this.lblAI1.Location = new System.Drawing.Point(15, 54);
            this.lblAI1.Name = "lblAI1";
            this.lblAI1.Size = new System.Drawing.Size(46, 13);
            this.lblAI1.TabIndex = 30;
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
            this.panel1.Controls.Add(this.lblCLA);
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
            // ucClosedLoopAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpCLA);
            this.Controls.Add(this.lvCLA);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "ucClosedLoopAction";
            this.Size = new System.Drawing.Size(1050, 700);
            this.grpCLA.ResumeLayout(false);
            this.grpCLA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lvCLA;
        private System.Windows.Forms.Label lblCLA;
        public System.Windows.Forms.GroupBox grpCLA;
        private System.Windows.Forms.Label lblDO;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.TextBox txtLow;
        private System.Windows.Forms.Label lblLow;
        public System.Windows.Forms.TextBox txtCLAIndex;
        private System.Windows.Forms.Label lblCI;
        public System.Windows.Forms.TextBox txtDelay;
        private System.Windows.Forms.Label lblDS;
        public System.Windows.Forms.TextBox txtHigh;
        private System.Windows.Forms.Label lblHigh;
        private System.Windows.Forms.Label lblAI2;
        private System.Windows.Forms.Label lblAI1;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.TextBox txtDONo;
        public System.Windows.Forms.TextBox txtAINo2;
        public System.Windows.Forms.TextBox txtAINo1;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
