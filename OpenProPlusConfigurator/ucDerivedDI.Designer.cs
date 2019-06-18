namespace OpenProPlusConfigurator
{
    partial class ucDerivedDI
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
            this.lvDD = new System.Windows.Forms.ListView();
            this.lblDD = new System.Windows.Forms.Label();
            this.grpDD = new System.Windows.Forms.GroupBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtDelayMS = new System.Windows.Forms.TextBox();
            this.lblDM = new System.Windows.Forms.Label();
            this.cmbOperation = new System.Windows.Forms.ComboBox();
            this.txtDINo2 = new System.Windows.Forms.TextBox();
            this.txtDINo1 = new System.Windows.Forms.TextBox();
            this.lblOpr = new System.Windows.Forms.Label();
            this.lblHdrText = new System.Windows.Forms.Label();
            this.pbHdr = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.txtDDIndex = new System.Windows.Forms.TextBox();
            this.lblDDI = new System.Windows.Forms.Label();
            this.lblDI2 = new System.Windows.Forms.Label();
            this.lblDI1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpDD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvDD
            // 
            this.lvDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvDD.CheckBoxes = true;
            this.lvDD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDD.FullRowSelect = true;
            this.lvDD.Location = new System.Drawing.Point(0, 30);
            this.lvDD.MultiSelect = false;
            this.lvDD.Name = "lvDD";
            this.lvDD.Size = new System.Drawing.Size(1050, 620);
            this.lvDD.TabIndex = 9;
            this.lvDD.UseCompatibleStateImageBehavior = false;
            this.lvDD.View = System.Windows.Forms.View.Details;
            this.lvDD.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvDD_ItemCheck);
            this.lvDD.DoubleClick += new System.EventHandler(this.lvDD_DoubleClick);
            // 
            // lblDD
            // 
            this.lblDD.AutoSize = true;
            this.lblDD.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDD.Location = new System.Drawing.Point(0, 0);
            this.lblDD.Name = "lblDD";
            this.lblDD.Size = new System.Drawing.Size(74, 15);
            this.lblDD.TabIndex = 8;
            this.lblDD.Text = "Derived DI";
            // 
            // grpDD
            // 
            this.grpDD.BackColor = System.Drawing.SystemColors.Control;
            this.grpDD.Controls.Add(this.btnLast);
            this.grpDD.Controls.Add(this.btnNext);
            this.grpDD.Controls.Add(this.btnPrev);
            this.grpDD.Controls.Add(this.btnFirst);
            this.grpDD.Controls.Add(this.txtDelayMS);
            this.grpDD.Controls.Add(this.lblDM);
            this.grpDD.Controls.Add(this.cmbOperation);
            this.grpDD.Controls.Add(this.txtDINo2);
            this.grpDD.Controls.Add(this.txtDINo1);
            this.grpDD.Controls.Add(this.lblOpr);
            this.grpDD.Controls.Add(this.lblHdrText);
            this.grpDD.Controls.Add(this.pbHdr);
            this.grpDD.Controls.Add(this.btnCancel);
            this.grpDD.Controls.Add(this.btnDone);
            this.grpDD.Controls.Add(this.txtDDIndex);
            this.grpDD.Controls.Add(this.lblDDI);
            this.grpDD.Controls.Add(this.lblDI2);
            this.grpDD.Controls.Add(this.lblDI1);
            this.grpDD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpDD.Location = new System.Drawing.Point(359, 118);
            this.grpDD.Name = "grpDD";
            this.grpDD.Size = new System.Drawing.Size(267, 212);
            this.grpDD.TabIndex = 17;
            this.grpDD.TabStop = false;
            this.grpDD.Visible = false;
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(192, 182);
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
            this.btnNext.Location = new System.Drawing.Point(132, 182);
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
            this.btnPrev.Location = new System.Drawing.Point(72, 182);
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
            this.btnFirst.Location = new System.Drawing.Point(12, 182);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(59, 22);
            this.btnFirst.TabIndex = 111;
            this.btnFirst.Text = "<<First";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtDelayMS
            // 
            this.txtDelayMS.Location = new System.Drawing.Point(87, 123);
            this.txtDelayMS.Name = "txtDelayMS";
            this.txtDelayMS.Size = new System.Drawing.Size(166, 20);
            this.txtDelayMS.TabIndex = 46;
            this.txtDelayMS.Tag = "DelayMS";
            this.txtDelayMS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDelayMS_KeyPress);
            // 
            // lblDM
            // 
            this.lblDM.AutoSize = true;
            this.lblDM.Location = new System.Drawing.Point(12, 126);
            this.lblDM.Name = "lblDM";
            this.lblDM.Size = new System.Drawing.Size(56, 13);
            this.lblDM.TabIndex = 45;
            this.lblDM.Text = "Delay (ms)";
            // 
            // cmbOperation
            // 
            this.cmbOperation.BackColor = System.Drawing.SystemColors.Window;
            this.cmbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperation.FormattingEnabled = true;
            this.cmbOperation.Location = new System.Drawing.Point(86, 98);
            this.cmbOperation.Name = "cmbOperation";
            this.cmbOperation.Size = new System.Drawing.Size(166, 21);
            this.cmbOperation.TabIndex = 42;
            this.cmbOperation.Tag = "Operation";
            // 
            // txtDINo2
            // 
            this.txtDINo2.Location = new System.Drawing.Point(87, 74);
            this.txtDINo2.Name = "txtDINo2";
            this.txtDINo2.Size = new System.Drawing.Size(166, 20);
            this.txtDINo2.TabIndex = 40;
            this.txtDINo2.Tag = "DINo2";
            this.txtDINo2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDINo2_KeyPress);
            // 
            // txtDINo1
            // 
            this.txtDINo1.Location = new System.Drawing.Point(86, 50);
            this.txtDINo1.Name = "txtDINo1";
            this.txtDINo1.Size = new System.Drawing.Size(166, 20);
            this.txtDINo1.TabIndex = 38;
            this.txtDINo1.Tag = "DINo1";
            this.txtDINo1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDINo1_KeyPress);
            // 
            // lblOpr
            // 
            this.lblOpr.AutoSize = true;
            this.lblOpr.Location = new System.Drawing.Point(12, 101);
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
            this.lblHdrText.Size = new System.Drawing.Size(25, 13);
            this.lblHdrText.TabIndex = 40;
            this.lblHdrText.Text = "DD";
            this.lblHdrText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseDown);
            this.lblHdrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblHdrText_MouseMove);
            // 
            // pbHdr
            // 
            this.pbHdr.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pbHdr.Location = new System.Drawing.Point(0, 0);
            this.pbHdr.Name = "pbHdr";
            this.pbHdr.Size = new System.Drawing.Size(267, 22);
            this.pbHdr.TabIndex = 39;
            this.pbHdr.TabStop = false;
            this.pbHdr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseDown);
            this.pbHdr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbHdr_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(185, 149);
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
            this.btnDone.Location = new System.Drawing.Point(87, 149);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 28);
            this.btnDone.TabIndex = 47;
            this.btnDone.Text = "&Update";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // txtDDIndex
            // 
            this.txtDDIndex.Enabled = false;
            this.txtDDIndex.Location = new System.Drawing.Point(86, 26);
            this.txtDDIndex.Name = "txtDDIndex";
            this.txtDDIndex.Size = new System.Drawing.Size(166, 20);
            this.txtDDIndex.TabIndex = 36;
            this.txtDDIndex.Tag = "DDIndex";
            // 
            // lblDDI
            // 
            this.lblDDI.AutoSize = true;
            this.lblDDI.Location = new System.Drawing.Point(12, 29);
            this.lblDDI.Name = "lblDDI";
            this.lblDDI.Size = new System.Drawing.Size(52, 13);
            this.lblDDI.TabIndex = 35;
            this.lblDDI.Text = "DD Index";
            // 
            // lblDI2
            // 
            this.lblDI2.AutoSize = true;
            this.lblDI2.Location = new System.Drawing.Point(12, 77);
            this.lblDI2.Name = "lblDI2";
            this.lblDI2.Size = new System.Drawing.Size(47, 13);
            this.lblDI2.TabIndex = 39;
            this.lblDI2.Text = "DI No. 2";
            // 
            // lblDI1
            // 
            this.lblDI1.AutoSize = true;
            this.lblDI1.Location = new System.Drawing.Point(12, 53);
            this.lblDI1.Name = "lblDI1";
            this.lblDI1.Size = new System.Drawing.Size(47, 13);
            this.lblDI1.TabIndex = 37;
            this.lblDI1.Text = "DI No. 1";
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
            this.panel1.Controls.Add(this.lblDD);
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
            // ucDerivedDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpDD);
            this.Controls.Add(this.lvDD);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "ucDerivedDI";
            this.Size = new System.Drawing.Size(1050, 700);
            this.grpDD.ResumeLayout(false);
            this.grpDD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHdr)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lvDD;
        private System.Windows.Forms.Label lblDD;
        public System.Windows.Forms.GroupBox grpDD;
        private System.Windows.Forms.Label lblOpr;
        private System.Windows.Forms.Label lblHdrText;
        private System.Windows.Forms.PictureBox pbHdr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        public System.Windows.Forms.TextBox txtDDIndex;
        private System.Windows.Forms.Label lblDDI;
        private System.Windows.Forms.Label lblDI2;
        private System.Windows.Forms.Label lblDI1;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.TextBox txtDINo2;
        public System.Windows.Forms.TextBox txtDINo1;
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
