﻿namespace Neunet.Forms
{
    partial class BugForm
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.messageStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveReminderLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
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
            this.openTestSetImageFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTestSetLabelFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationDataFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.commonAppDataFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.randomizeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.runToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.stopToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.settingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.bugWorldImage = new Neunet.Images.WorldImage();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
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
            this.calculateToolStripMenuItem.Name = "calculateToolStripMenuItem";
            this.calculateToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.calculateToolStripMenuItem.Text = "Run";
            // 
            // randomizeToolStripMenuItem
            // 
            this.randomizeToolStripMenuItem.Image = global::Neunet.Properties.Resources.Dices;
            this.randomizeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.randomizeToolStripMenuItem.Name = "randomizeToolStripMenuItem";
            this.randomizeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.randomizeToolStripMenuItem.Text = "Randomize";
            this.randomizeToolStripMenuItem.Click += new System.EventHandler(this.RandomizeMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Image = global::Neunet.Properties.Resources.Stop;
            this.stopToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::Neunet.Properties.Resources.Settings;
            this.settingsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
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
            // toolStrip1
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator3,
            this.editToolStripButton,
            this.toolStripSeparator5,
            this.randomizeToolStripButton,
            this.runToolStripButton,
            this.stopToolStripButton,
            this.settingsToolStripButton,
            this.toolStripSeparator6});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip1";
            this.toolStrip.Size = new System.Drawing.Size(837, 25);
            this.toolStrip.TabIndex = 12;
            this.toolStrip.Text = "toolStrip";
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
            // runToolStripButton
            // 
            this.runToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.runToolStripButton.Image = global::Neunet.Properties.Resources.Run;
            this.runToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.runToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runToolStripButton.Name = "runToolStripButton";
            this.runToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.runToolStripButton.Text = "Run";
            this.runToolStripButton.ToolTipText = "Verify the network";
            this.runToolStripButton.Click += new System.EventHandler(this.RunButton_Click);
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
            // bugWorldImage
            // 
            this.bugWorldImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bugWorldImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bugWorldImage.Location = new System.Drawing.Point(0, 49);
            this.bugWorldImage.Margin = new System.Windows.Forms.Padding(0);
            this.bugWorldImage.Name = "bugWorldImage";
            this.bugWorldImage.Size = new System.Drawing.Size(837, 487);
            this.bugWorldImage.TabIndex = 13;
            // 
            // updateTimer
            // 
            this.updateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Image = global::Neunet.Properties.Resources.Run;
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // BugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 558);
            this.Controls.Add(this.bugWorldImage);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.statusStrip);
            this.Name = "BugForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Bugs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel messageStatusLabel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
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
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openTestSetImageFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTestSetLabelFileMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton editToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton randomizeToolStripButton;
        private System.Windows.Forms.ToolStripButton runToolStripButton;
        private System.Windows.Forms.ToolStripButton stopToolStripButton;
        private System.Windows.Forms.ToolStripButton settingsToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private Images.WorldImage bugWorldImage;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
    }
}

