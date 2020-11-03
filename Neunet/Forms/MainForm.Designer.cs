namespace Neunet.Forms
{
    partial class MainForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.samplesImage = new Neunet.Images.Charts2D.SamplesImage();
            this.coefficientsImage = new Neunet.Images.CoefficientsImage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.samplesLabel = new System.Windows.Forms.Label();
            this.derivativesImage = new Neunet.Images.CoefficientsImage();
            this.coefficientsLabel = new System.Windows.Forms.Label();
            this.derivativesLabel = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.messageStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveReminderLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.historyImage = new Neunet.Images.HistoryImage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.randomizeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.learnToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.verifyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.stopToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.settingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openTrainingSetImageFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTrainingSetLabelFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.learnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationDataFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.commonAppDataFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.openTestSetImageFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTestSetLabelFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.samplesImage, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.coefficientsImage, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.derivativesImage, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.coefficientsLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.derivativesLabel, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(837, 308);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // samplesImage
            // 
            this.samplesImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.samplesImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.samplesImage.Location = new System.Drawing.Point(3, 26);
            this.samplesImage.Margin = new System.Windows.Forms.Padding(0);
            this.samplesImage.Name = "samplesImage";
            this.samplesImage.Size = new System.Drawing.Size(831, 181);
            this.samplesImage.TabIndex = 0;
            // 
            // coefficientsImage
            // 
            this.coefficientsImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.coefficientsImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coefficientsImage.Location = new System.Drawing.Point(3, 233);
            this.coefficientsImage.Margin = new System.Windows.Forms.Padding(0);
            this.coefficientsImage.Name = "coefficientsImage";
            this.coefficientsImage.Size = new System.Drawing.Size(831, 23);
            this.coefficientsImage.TabIndex = 8;
            this.coefficientsImage.Values = null;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.samplesLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(831, 20);
            this.panel1.TabIndex = 1;
            // 
            // samplesLabel
            // 
            this.samplesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.samplesLabel.Location = new System.Drawing.Point(0, 0);
            this.samplesLabel.Name = "samplesLabel";
            this.samplesLabel.Size = new System.Drawing.Size(831, 20);
            this.samplesLabel.TabIndex = 7;
            this.samplesLabel.Text = "Samples";
            this.samplesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // derivativesImage
            // 
            this.derivativesImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.derivativesImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.derivativesImage.Location = new System.Drawing.Point(3, 282);
            this.derivativesImage.Margin = new System.Windows.Forms.Padding(0);
            this.derivativesImage.Name = "derivativesImage";
            this.derivativesImage.Size = new System.Drawing.Size(831, 23);
            this.derivativesImage.TabIndex = 12;
            this.derivativesImage.Values = null;
            // 
            // coefficientsLabel
            // 
            this.coefficientsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coefficientsLabel.Location = new System.Drawing.Point(6, 210);
            this.coefficientsLabel.Name = "coefficientsLabel";
            this.coefficientsLabel.Size = new System.Drawing.Size(825, 20);
            this.coefficientsLabel.TabIndex = 13;
            this.coefficientsLabel.Text = "Coefficients";
            this.coefficientsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // derivativesLabel
            // 
            this.derivativesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.derivativesLabel.Location = new System.Drawing.Point(6, 259);
            this.derivativesLabel.Name = "derivativesLabel";
            this.derivativesLabel.Size = new System.Drawing.Size(825, 20);
            this.derivativesLabel.TabIndex = 14;
            this.derivativesLabel.Text = "Derivatives";
            this.derivativesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.messageStatusLabel,
            this.saveReminderLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 536);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(837, 22);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // messageStatusLabel
            // 
            this.messageStatusLabel.Name = "messageStatusLabel";
            this.messageStatusLabel.Size = new System.Drawing.Size(56, 17);
            this.messageStatusLabel.Text = "Message.";
            // 
            // saveReminderLabel
            // 
            this.saveReminderLabel.Name = "saveReminderLabel";
            this.saveReminderLabel.Size = new System.Drawing.Size(145, 17);
            this.saveReminderLabel.Text = "The network has changed.";
            this.saveReminderLabel.Visible = false;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.historyImage);
            this.mainPanel.Controls.Add(this.toolStrip1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(837, 200);
            this.mainPanel.TabIndex = 11;
            // 
            // historyImage
            // 
            this.historyImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.historyImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyImage.InnerConnection = Neunet.Images.ConnectionTypeEnum.Line;
            this.historyImage.Location = new System.Drawing.Point(0, 25);
            this.historyImage.Magnification = 1;
            this.historyImage.Margin = new System.Windows.Forms.Padding(0);
            this.historyImage.Name = "historyImage";
            this.historyImage.OuterConnection = Neunet.Images.ConnectionTypeEnum.Line;
            this.historyImage.PointSize = 14F;
            this.historyImage.Size = new System.Drawing.Size(837, 175);
            this.historyImage.TabIndex = 10;
            this.historyImage.VAxisText = "Cost value";
            this.historyImage.ViewPoints = false;
            this.historyImage.Zoom = 0.95F;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator3,
            this.editToolStripButton,
            this.toolStripSeparator5,
            this.randomizeToolStripButton,
            this.learnToolStripButton,
            this.verifyToolStripButton,
            this.stopToolStripButton,
            this.settingsToolStripButton,
            this.toolStripSeparator6});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(837, 25);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = global::Neunet.Properties.Resources.New;
            this.newToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "New";
            this.newToolStripButton.ToolTipText = "New network";
            this.newToolStripButton.Click += new System.EventHandler(this.NewMenuItem_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::Neunet.Properties.Resources.Open;
            this.openToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "Open";
            this.openToolStripButton.ToolTipText = "Open network from file";
            this.openToolStripButton.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = global::Neunet.Properties.Resources.Save;
            this.saveToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "Save";
            this.saveToolStripButton.ToolTipText = "Save network to file";
            this.saveToolStripButton.Click += new System.EventHandler(this.SaveMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // editToolStripButton
            // 
            this.editToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editToolStripButton.Image = global::Neunet.Properties.Resources.Edit;
            this.editToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolStripButton.Name = "editToolStripButton";
            this.editToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.editToolStripButton.Text = "Edit";
            this.editToolStripButton.ToolTipText = "Edit the network";
            this.editToolStripButton.Click += new System.EventHandler(this.Edit_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // randomizeToolStripButton
            // 
            this.randomizeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.randomizeToolStripButton.Image = global::Neunet.Properties.Resources.Dices;
            this.randomizeToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.randomizeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.randomizeToolStripButton.Name = "randomizeToolStripButton";
            this.randomizeToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.randomizeToolStripButton.Text = "Randomize";
            this.randomizeToolStripButton.ToolTipText = "Randomize the network";
            this.randomizeToolStripButton.Click += new System.EventHandler(this.RandomizeMenuItem_Click);
            // 
            // learnToolStripButton
            // 
            this.learnToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.learnToolStripButton.Image = global::Neunet.Properties.Resources.Learn;
            this.learnToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.learnToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.learnToolStripButton.Name = "learnToolStripButton";
            this.learnToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.learnToolStripButton.Text = "Learn";
            this.learnToolStripButton.ToolTipText = "Learn the network";
            this.learnToolStripButton.Click += new System.EventHandler(this.LearnButton_Click);
            // 
            // verifyToolStripButton
            // 
            this.verifyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.verifyToolStripButton.Image = global::Neunet.Properties.Resources.Verify;
            this.verifyToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.verifyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.verifyToolStripButton.Name = "verifyToolStripButton";
            this.verifyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.verifyToolStripButton.Text = "Verify";
            this.verifyToolStripButton.ToolTipText = "Verify the network";
            this.verifyToolStripButton.Click += new System.EventHandler(this.VerifyButton_Click);
            // 
            // stopToolStripButton
            // 
            this.stopToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopToolStripButton.Image = global::Neunet.Properties.Resources.Stop;
            this.stopToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stopToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopToolStripButton.Name = "stopToolStripButton";
            this.stopToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.stopToolStripButton.Text = "Stop";
            this.stopToolStripButton.ToolTipText = "Stop the operation";
            this.stopToolStripButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // settingsToolStripButton
            // 
            this.settingsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsToolStripButton.Image = global::Neunet.Properties.Resources.Settings;
            this.settingsToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.settingsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsToolStripButton.Name = "settingsToolStripButton";
            this.settingsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.settingsToolStripButton.Text = "Settings";
            this.settingsToolStripButton.ToolTipText = "Calculation settings";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenuItem,
            this.calculateToolStripMenuItem,
            this.toolsMenu,
            this.helpMenu});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(837, 24);
            this.mainMenuStrip.TabIndex = 12;
            this.mainMenuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMenuItem,
            this.toolStripSeparator2,
            this.openMenuItem,
            this.saveMenuItem,
            this.saveAsMenuItem,
            this.toolStripSeparator1,
            this.openTrainingSetImageFileMenuItem,
            this.openTrainingSetLabelFileMenuItem,
            this.openTestSetImageFileMenuItem,
            this.openTestSetLabelFileMenuItem,
            this.toolStripSeparator4,
            this.exitMenuItem});
            this.fileMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            // 
            // newMenuItem
            // 
            this.newMenuItem.Image = global::Neunet.Properties.Resources.New;
            this.newMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newMenuItem.Name = "newMenuItem";
            this.newMenuItem.Size = new System.Drawing.Size(220, 22);
            this.newMenuItem.Text = "&New";
            this.newMenuItem.Click += new System.EventHandler(this.NewMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(217, 6);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Image = global::Neunet.Properties.Resources.Open;
            this.openMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMenuItem.Size = new System.Drawing.Size(220, 22);
            this.openMenuItem.Text = "&Open";
            this.openMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Image = global::Neunet.Properties.Resources.Save;
            this.saveMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMenuItem.Size = new System.Drawing.Size(220, 22);
            this.saveMenuItem.Text = "&Save";
            this.saveMenuItem.Click += new System.EventHandler(this.SaveMenuItem_Click);
            // 
            // saveAsMenuItem
            // 
            this.saveAsMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveAsMenuItem.Name = "saveAsMenuItem";
            this.saveAsMenuItem.Size = new System.Drawing.Size(220, 22);
            this.saveAsMenuItem.Text = "Save &As";
            this.saveAsMenuItem.Click += new System.EventHandler(this.SaveAsMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // openTrainingSetImageFileMenuItem
            // 
            this.openTrainingSetImageFileMenuItem.Name = "openTrainingSetImageFileMenuItem";
            this.openTrainingSetImageFileMenuItem.Size = new System.Drawing.Size(220, 22);
            this.openTrainingSetImageFileMenuItem.Text = "Open training set image file";
            this.openTrainingSetImageFileMenuItem.Click += new System.EventHandler(this.OpenTrainingSetImageFileMenuItem_Click);
            // 
            // openTrainingSetLabelFileMenuItem
            // 
            this.openTrainingSetLabelFileMenuItem.Name = "openTrainingSetLabelFileMenuItem";
            this.openTrainingSetLabelFileMenuItem.Size = new System.Drawing.Size(220, 22);
            this.openTrainingSetLabelFileMenuItem.Text = "Open training set label file";
            this.openTrainingSetLabelFileMenuItem.Click += new System.EventHandler(this.OpenTrainingSetLabelFileMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(217, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(220, 22);
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // editMenuItem
            // 
            this.editMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem1});
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editMenuItem.Text = "Edit";
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(94, 22);
            this.editToolStripMenuItem1.Text = "Edit";
            this.editToolStripMenuItem1.Click += new System.EventHandler(this.Edit_Click);
            // 
            // calculateToolStripMenuItem
            // 
            this.calculateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.randomizeToolStripMenuItem,
            this.learnToolStripMenuItem,
            this.verifyToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripMenuItem1,
            this.settingsToolStripMenuItem});
            this.calculateToolStripMenuItem.Name = "calculateToolStripMenuItem";
            this.calculateToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.calculateToolStripMenuItem.Text = "Run";
            // 
            // randomizeToolStripMenuItem
            // 
            this.randomizeToolStripMenuItem.Image = global::Neunet.Properties.Resources.Dices;
            this.randomizeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.randomizeToolStripMenuItem.Name = "randomizeToolStripMenuItem";
            this.randomizeToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.randomizeToolStripMenuItem.Text = "Randomize";
            this.randomizeToolStripMenuItem.Click += new System.EventHandler(this.RandomizeMenuItem_Click);
            // 
            // learnToolStripMenuItem
            // 
            this.learnToolStripMenuItem.Image = global::Neunet.Properties.Resources.Learn;
            this.learnToolStripMenuItem.Name = "learnToolStripMenuItem";
            this.learnToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.learnToolStripMenuItem.Text = "Learn";
            this.learnToolStripMenuItem.Click += new System.EventHandler(this.LearnButton_Click);
            // 
            // verifyToolStripMenuItem
            // 
            this.verifyToolStripMenuItem.Image = global::Neunet.Properties.Resources.Verify;
            this.verifyToolStripMenuItem.Name = "verifyToolStripMenuItem";
            this.verifyToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.verifyToolStripMenuItem.Text = "Verify";
            this.verifyToolStripMenuItem.Click += new System.EventHandler(this.VerifyButton_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Image = global::Neunet.Properties.Resources.Stop;
            this.stopToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(130, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::Neunet.Properties.Resources.Settings;
            this.settingsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.CalculationSettingsToolStripMenuItem_Click);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationDataFolder,
            this.commonAppDataFolderToolStripMenuItem,
            this.programFolderToolStripMenuItem});
            this.toolsMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(46, 20);
            this.toolsMenu.Text = "&Tools";
            // 
            // applicationDataFolder
            // 
            this.applicationDataFolder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.applicationDataFolder.Name = "applicationDataFolder";
            this.applicationDataFolder.Size = new System.Drawing.Size(205, 22);
            this.applicationDataFolder.Text = "AppData folder";
            this.applicationDataFolder.ToolTipText = "folder: C:\\Users\\<user>\\AppData\\Roaming\\FRESH3";
            this.applicationDataFolder.Click += new System.EventHandler(this.ApplicationDataFolder_Click);
            // 
            // commonAppDataFolderToolStripMenuItem
            // 
            this.commonAppDataFolderToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.commonAppDataFolderToolStripMenuItem.Name = "commonAppDataFolderToolStripMenuItem";
            this.commonAppDataFolderToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.commonAppDataFolderToolStripMenuItem.Text = "CommonAppData folder";
            this.commonAppDataFolderToolStripMenuItem.Click += new System.EventHandler(this.CommonAppDataFolderToolStripMenuItem_Click);
            // 
            // programFolderToolStripMenuItem
            // 
            this.programFolderToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.programFolderToolStripMenuItem.Name = "programFolderToolStripMenuItem";
            this.programFolderToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.programFolderToolStripMenuItem.Text = "Program folder";
            this.programFolderToolStripMenuItem.Click += new System.EventHandler(this.ProgramFolderToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.mainPanel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer.Size = new System.Drawing.Size(837, 512);
            this.splitContainer.SplitterDistance = 200;
            this.splitContainer.TabIndex = 13;
            // 
            // openTestSetImageFileMenuItem
            // 
            this.openTestSetImageFileMenuItem.Name = "openTestSetImageFileMenuItem";
            this.openTestSetImageFileMenuItem.Size = new System.Drawing.Size(220, 22);
            this.openTestSetImageFileMenuItem.Text = "Open test set image file";
            this.openTestSetImageFileMenuItem.Click += new System.EventHandler(this.OpenTestSetImageFileMenuItem_Click);
            // 
            // openTestSetLabelFileMenuItem
            // 
            this.openTestSetLabelFileMenuItem.Name = "openTestSetLabelFileMenuItem";
            this.openTestSetLabelFileMenuItem.Size = new System.Drawing.Size(220, 22);
            this.openTestSetLabelFileMenuItem.Text = "Open test set label file";
            this.openTestSetLabelFileMenuItem.Click += new System.EventHandler(this.OpenTestSetLabelFileMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 558);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Images.Charts2D.SamplesImage samplesImage;
        private System.Windows.Forms.Label samplesLabel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel messageStatusLabel;
        private Images.CoefficientsImage coefficientsImage;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem newMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem applicationDataFolder;
        private System.Windows.Forms.ToolStripMenuItem commonAppDataFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem openTrainingSetImageFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTrainingSetLabelFileMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel saveReminderLabel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panel1;
        private Images.HistoryImage historyImage;
        private Images.CoefficientsImage derivativesImage;
        private System.Windows.Forms.Label coefficientsLabel;
        private System.Windows.Forms.Label derivativesLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton randomizeToolStripButton;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton learnToolStripButton;
        private System.Windows.Forms.ToolStripButton stopToolStripButton;
        private System.Windows.Forms.ToolStripButton verifyToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem learnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton settingsToolStripButton;
        private System.Windows.Forms.ToolStripButton editToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openTestSetImageFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTestSetLabelFileMenuItem;
    }
}

