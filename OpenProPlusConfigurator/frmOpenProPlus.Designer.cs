namespace OpenProPlusConfigurator
{
    partial class frmOpenProPlus
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOpenProPlus));
            this.mnuParser = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.showOverviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.openProUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsParser = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMappingView = new System.Windows.Forms.ToolStripButton();
            this.tsbReindexData = new System.Windows.Forms.ToolStripButton();
            this.tspImportICD = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.tspManual = new System.Windows.Forms.ToolStripButton();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.ssParser = new System.Windows.Forms.StatusStrip();
            this.sslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.filename = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.scParser = new System.Windows.Forms.SplitContainer();
            this.tvItems = new System.Windows.Forms.TreeView();
            this.cmsAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sujitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpMappingView = new System.Windows.Forms.GroupBox();
            this.lvMappingView = new System.Windows.Forms.ListView();
            this.sfdXMLFile = new System.Windows.Forms.SaveFileDialog();
            this.ofdXMLFile = new System.Windows.Forms.OpenFileDialog();
            this.lblFileName = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.pnlValidationMessages = new System.Windows.Forms.Panel();
            this.lvValidationMessages = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.mnuParser.SuspendLayout();
            this.tsParser.SuspendLayout();
            this.ssParser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scParser)).BeginInit();
            this.scParser.Panel1.SuspendLayout();
            this.scParser.SuspendLayout();
            this.cmsAdd.SuspendLayout();
            this.grpMappingView.SuspendLayout();
            this.pnlValidationMessages.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuParser
            // 
            this.mnuParser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mnuParser.Location = new System.Drawing.Point(0, 0);
            this.mnuParser.Name = "mnuParser";
            this.mnuParser.Size = new System.Drawing.Size(856, 24);
            this.mnuParser.TabIndex = 2;
            this.mnuParser.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator4,
            this.recentFilesToolStripMenuItem,
            this.toolStripSeparator6,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::OpenProPlusConfigurator.Properties.Resources.newfile;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::OpenProPlusConfigurator.Properties.Resources.openfolder;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::OpenProPlusConfigurator.Properties.Resources.Save__5_;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::OpenProPlusConfigurator.Properties.Resources.Save__9_1;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // recentFilesToolStripMenuItem
            // 
            this.recentFilesToolStripMenuItem.Image = global::OpenProPlusConfigurator.Properties.Resources.imageedit_7_6956011015;
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.recentFilesToolStripMenuItem.Text = "Recent File";
            this.recentFilesToolStripMenuItem.Click += new System.EventHandler(this.recentFilesToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::OpenProPlusConfigurator.Properties.Resources.cross_mark_on_a_black_circle_background__1_;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbarToolStripMenuItem,
            this.statusBarToolStripMenuItem,
            this.toolStripSeparator7,
            this.showOverviewToolStripMenuItem,
            this.toolStripSeparator8,
            this.openProUIToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // toolbarToolStripMenuItem
            // 
            this.toolbarToolStripMenuItem.Checked = true;
            this.toolbarToolStripMenuItem.CheckOnClick = true;
            this.toolbarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolbarToolStripMenuItem.Name = "toolbarToolStripMenuItem";
            this.toolbarToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.toolbarToolStripMenuItem.Text = "&Toolbar";
            this.toolbarToolStripMenuItem.Click += new System.EventHandler(this.toolbarToolStripMenuItem_Click);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckOnClick = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.statusBarToolStripMenuItem.Text = "&Status Bar";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.statusBarToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(152, 6);
            // 
            // showOverviewToolStripMenuItem
            // 
            this.showOverviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showOverviewToolStripMenuItem.Image")));
            this.showOverviewToolStripMenuItem.Name = "showOverviewToolStripMenuItem";
            this.showOverviewToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.showOverviewToolStripMenuItem.Text = "Show Overview";
            this.showOverviewToolStripMenuItem.Click += new System.EventHandler(this.showOverviewToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(152, 6);
            // 
            // openProUIToolStripMenuItem
            // 
            this.openProUIToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openProUIToolStripMenuItem.Image")));
            this.openProUIToolStripMenuItem.Name = "openProUIToolStripMenuItem";
            this.openProUIToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.openProUIToolStripMenuItem.Text = "OpenPro+ UI";
            this.openProUIToolStripMenuItem.Click += new System.EventHandler(this.openProUIToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shelpToolStripMenuItem,
            this.manualToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // shelpToolStripMenuItem
            // 
            this.shelpToolStripMenuItem.Image = global::OpenProPlusConfigurator.Properties.Resources.question;
            this.shelpToolStripMenuItem.Name = "shelpToolStripMenuItem";
            this.shelpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.shelpToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.shelpToolStripMenuItem.Text = "&Help";
            this.shelpToolStripMenuItem.Click += new System.EventHandler(this.shelpToolStripMenuItem_Click);
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("manualToolStripMenuItem.Image")));
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.manualToolStripMenuItem.Text = "Manual";
            this.manualToolStripMenuItem.Click += new System.EventHandler(this.manualToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(142, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::OpenProPlusConfigurator.Properties.Resources.info__1_;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tsParser
            // 
            this.tsParser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbSave,
            this.toolStripSeparator1,
            this.tsbMappingView,
            this.tsbReindexData,
            this.tspImportICD,
            this.toolStripSeparator3,
            this.tsbHelp,
            this.tspManual,
            this.tsbAbout,
            this.toolStripSeparator5,
            this.tsbClose});
            this.tsParser.Location = new System.Drawing.Point(0, 24);
            this.tsParser.Name = "tsParser";
            this.tsParser.Size = new System.Drawing.Size(856, 25);
            this.tsParser.TabIndex = 4;
            this.tsParser.Text = "toolStrip1";
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = global::OpenProPlusConfigurator.Properties.Resources.newfile;
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(23, 22);
            this.tsbNew.Text = "toolStripButton1";
            this.tsbNew.ToolTipText = "New";
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = global::OpenProPlusConfigurator.Properties.Resources.openfolder;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "toolStripButton2";
            this.tsbOpen.ToolTipText = "Open";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::OpenProPlusConfigurator.Properties.Resources.Save__5_;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "toolStripButton3";
            this.tsbSave.ToolTipText = "Save";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbMappingView
            // 
            this.tsbMappingView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMappingView.Image = ((System.Drawing.Image)(resources.GetObject("tsbMappingView.Image")));
            this.tsbMappingView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMappingView.Name = "tsbMappingView";
            this.tsbMappingView.Size = new System.Drawing.Size(23, 22);
            this.tsbMappingView.Text = "Overview";
            this.tsbMappingView.Click += new System.EventHandler(this.tsbMappingView_Click);
            // 
            // tsbReindexData
            // 
            this.tsbReindexData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbReindexData.Image = global::OpenProPlusConfigurator.Properties.Resources.sorting;
            this.tsbReindexData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReindexData.Name = "tsbReindexData";
            this.tsbReindexData.Size = new System.Drawing.Size(23, 22);
            this.tsbReindexData.Text = "Reindex Data";
            this.tsbReindexData.Click += new System.EventHandler(this.tsbReindexData_Click);
            // 
            // tspImportICD
            // 
            this.tspImportICD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspImportICD.Image = ((System.Drawing.Image)(resources.GetObject("tspImportICD.Image")));
            this.tspImportICD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspImportICD.Name = "tspImportICD";
            this.tspImportICD.Size = new System.Drawing.Size(23, 22);
            this.tspImportICD.ToolTipText = "OpenPro+ UI";
            this.tspImportICD.Click += new System.EventHandler(this.tspImportICD_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = global::OpenProPlusConfigurator.Properties.Resources.question;
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 22);
            this.tsbHelp.Text = "toolStripButton4";
            this.tsbHelp.ToolTipText = "Help";
            this.tsbHelp.Click += new System.EventHandler(this.tsbHelp_Click);
            // 
            // tspManual
            // 
            this.tspManual.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspManual.Image = global::OpenProPlusConfigurator.Properties.Resources.imageedit_7_6752810522;
            this.tspManual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspManual.Name = "tspManual";
            this.tspManual.Size = new System.Drawing.Size(23, 22);
            this.tspManual.Text = "toolStripButton1";
            this.tspManual.ToolTipText = "Manual";
            this.tspManual.Click += new System.EventHandler(this.tspManual_Click);
            // 
            // tsbAbout
            // 
            this.tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAbout.Image = global::OpenProPlusConfigurator.Properties.Resources.info__1_;
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(23, 22);
            this.tsbAbout.Text = "toolStripButton1";
            this.tsbAbout.ToolTipText = "About";
            this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = global::OpenProPlusConfigurator.Properties.Resources.cross_mark_on_a_black_circle_background__1_;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(23, 22);
            this.tsbClose.Text = "Close";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // ssParser
            // 
            this.ssParser.BackColor = System.Drawing.Color.White;
            this.ssParser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslStatus,
            this.toolStripStatusLabel1,
            this.tsFileName,
            this.filename,
            this.tspFileName});
            this.ssParser.Location = new System.Drawing.Point(0, 464);
            this.ssParser.Name = "ssParser";
            this.ssParser.Size = new System.Drawing.Size(856, 22);
            this.ssParser.TabIndex = 5;
            this.ssParser.Text = "OpenPro+ Configurator";
            this.ssParser.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ssParser_ItemClicked);
            // 
            // sslStatus
            // 
            this.sslStatus.Name = "sslStatus";
            this.sslStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(70, 17);
            this.toolStripStatusLabel1.Text = "File Name :";
            this.toolStripStatusLabel1.Visible = false;
            // 
            // tsFileName
            // 
            this.tsFileName.ActiveLinkColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tsFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsFileName.Name = "tsFileName";
            this.tsFileName.Size = new System.Drawing.Size(0, 17);
            // 
            // filename
            // 
            this.filename.Name = "filename";
            this.filename.Size = new System.Drawing.Size(0, 17);
            // 
            // tspFileName
            // 
            this.tspFileName.ActiveLinkColor = System.Drawing.Color.Blue;
            this.tspFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspFileName.ForeColor = System.Drawing.Color.Black;
            this.tspFileName.Name = "tspFileName";
            this.tspFileName.Size = new System.Drawing.Size(0, 17);
            // 
            // scParser
            // 
            this.scParser.BackColor = System.Drawing.SystemColors.Control;
            this.scParser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scParser.Location = new System.Drawing.Point(0, 49);
            this.scParser.Name = "scParser";
            // 
            // scParser.Panel1
            // 
            this.scParser.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.scParser.Panel1.Controls.Add(this.tvItems);
            // 
            // scParser.Panel2
            // 
            this.scParser.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.scParser.Panel2.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.scParser_Panel2_ControlAdded);
            this.scParser.Panel2.Resize += new System.EventHandler(this.scParser_Panel2_Resize);
            this.scParser.Size = new System.Drawing.Size(856, 415);
            this.scParser.SplitterDistance = 169;
            this.scParser.TabIndex = 6;
            // 
            // tvItems
            // 
            this.tvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvItems.HideSelection = false;
            this.tvItems.ItemHeight = 20;
            this.tvItems.Location = new System.Drawing.Point(0, 0);
            this.tvItems.Name = "tvItems";
            this.tvItems.Size = new System.Drawing.Size(169, 415);
            this.tvItems.TabIndex = 0;
            this.tvItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvItems_AfterSelect);
            this.tvItems.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvItems_NodeMouseClick);
            // 
            // cmsAdd
            // 
            this.cmsAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sujitToolStripMenuItem});
            this.cmsAdd.Name = "cmsAdd";
            this.cmsAdd.Size = new System.Drawing.Size(98, 26);
            // 
            // sujitToolStripMenuItem
            // 
            this.sujitToolStripMenuItem.Name = "sujitToolStripMenuItem";
            this.sujitToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.sujitToolStripMenuItem.Text = "Sujit";
            // 
            // grpMappingView
            // 
            this.grpMappingView.BackColor = System.Drawing.SystemColors.Control;
            this.grpMappingView.Controls.Add(this.lvMappingView);
            this.grpMappingView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMappingView.Location = new System.Drawing.Point(0, 49);
            this.grpMappingView.Name = "grpMappingView";
            this.grpMappingView.Size = new System.Drawing.Size(856, 415);
            this.grpMappingView.TabIndex = 7;
            this.grpMappingView.TabStop = false;
            this.grpMappingView.Resize += new System.EventHandler(this.grpMappingView_Resize);
            // 
            // lvMappingView
            // 
            this.lvMappingView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvMappingView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMappingView.FullRowSelect = true;
            this.lvMappingView.HideSelection = false;
            this.lvMappingView.Location = new System.Drawing.Point(5, 71);
            this.lvMappingView.MultiSelect = false;
            this.lvMappingView.Name = "lvMappingView";
            this.lvMappingView.OwnerDraw = true;
            this.lvMappingView.Size = new System.Drawing.Size(826, 335);
            this.lvMappingView.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lvMappingView.TabIndex = 10;
            this.lvMappingView.UseCompatibleStateImageBehavior = false;
            this.lvMappingView.View = System.Windows.Forms.View.Details;
            this.lvMappingView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMappingView_ColumnClick);
            this.lvMappingView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvMappingView_DrawColumnHeader);
            this.lvMappingView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvMappingView_DrawSubItem);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.Location = new System.Drawing.Point(329, 24);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(0, 15);
            this.lblFileName.TabIndex = 9;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // pnlValidationMessages
            // 
            this.pnlValidationMessages.BackColor = System.Drawing.SystemColors.Control;
            this.pnlValidationMessages.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlValidationMessages.Controls.Add(this.lvValidationMessages);
            this.pnlValidationMessages.Controls.Add(this.panel1);
            this.pnlValidationMessages.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlValidationMessages.Location = new System.Drawing.Point(0, 347);
            this.pnlValidationMessages.Name = "pnlValidationMessages";
            this.pnlValidationMessages.Size = new System.Drawing.Size(856, 117);
            this.pnlValidationMessages.TabIndex = 9;
            this.pnlValidationMessages.Visible = false;
            // 
            // lvValidationMessages
            // 
            this.lvValidationMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvValidationMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvValidationMessages.Location = new System.Drawing.Point(0, 22);
            this.lvValidationMessages.Name = "lvValidationMessages";
            this.lvValidationMessages.Size = new System.Drawing.Size(852, 91);
            this.lvValidationMessages.TabIndex = 0;
            this.lvValidationMessages.UseCompatibleStateImageBehavior = false;
            this.lvValidationMessages.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 22);
            this.panel1.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(825, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(27, 22);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmOpenProPlus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(856, 486);
            this.Controls.Add(this.pnlValidationMessages);
            this.Controls.Add(this.scParser);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.grpMappingView);
            this.Controls.Add(this.ssParser);
            this.Controls.Add(this.tsParser);
            this.Controls.Add(this.mnuParser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuParser;
            this.Name = "frmOpenProPlus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpenPro+ Configurator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmParser_FormClosing);
            this.Load += new System.EventHandler(this.frmParser_Load);
            this.Resize += new System.EventHandler(this.frmParser_Resize);
            this.mnuParser.ResumeLayout(false);
            this.mnuParser.PerformLayout();
            this.tsParser.ResumeLayout(false);
            this.tsParser.PerformLayout();
            this.ssParser.ResumeLayout(false);
            this.ssParser.PerformLayout();
            this.scParser.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scParser)).EndInit();
            this.scParser.ResumeLayout(false);
            this.cmsAdd.ResumeLayout(false);
            this.grpMappingView.ResumeLayout(false);
            this.pnlValidationMessages.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuParser;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolbarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip tsParser;
        private System.Windows.Forms.ToolStripStatusLabel sslStatus;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SplitContainer scParser;
        private System.Windows.Forms.TreeView tvItems;
        private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.ContextMenuStrip cmsAdd;
        private System.Windows.Forms.ToolStripMenuItem sujitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbMappingView;
        private System.Windows.Forms.GroupBox grpMappingView;
        private System.Windows.Forms.SaveFileDialog sfdXMLFile;
        private System.Windows.Forms.OpenFileDialog ofdXMLFile;
        public System.Windows.Forms.ListView lvMappingView;
        private System.Windows.Forms.ToolStripMenuItem showOverviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbReindexData;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripMenuItem openProUIToolStripMenuItem;
        public System.Windows.Forms.StatusStrip ssParser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsFileName;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.ToolStripStatusLabel filename;
        private System.Windows.Forms.ToolStripStatusLabel tspFileName;
        public System.Windows.Forms.ToolStripButton tspImportICD;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.Panel pnlValidationMessages;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListView lvValidationMessages;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton tspManual;
    }
}

