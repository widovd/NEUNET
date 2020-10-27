namespace Neunet.UserControls
{
    partial class RunToolStrip
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.resetButton = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.stepButton = new System.Windows.Forms.ToolStripButton();
            this.runButton = new System.Windows.Forms.ToolStripButton();
            this.stopButton = new System.Windows.Forms.ToolStripButton();
            this.settingsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetButton,
            this.deleteButton,
            this.stepButton,
            this.runButton,
            this.stopButton,
            this.toolStripSeparator1,
            this.settingsButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(432, 25);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "DistributionToolStrip";
            // 
            // resetButton
            // 
            this.resetButton.Image = global::Neunet.Properties.Resources.Reset;
            this.resetButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(55, 22);
            this.resetButton.Text = "Reset";
            this.resetButton.ToolTipText = "Delete all iterations";
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Image = global::Neunet.Properties.Resources.DeleteLast;
            this.deleteButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(60, 22);
            this.deleteButton.Text = "Delete";
            this.deleteButton.ToolTipText = "Delete the last iteration";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // stepButton
            // 
            this.stepButton.Image = global::Neunet.Properties.Resources.Step;
            this.stepButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stepButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(50, 22);
            this.stepButton.Text = "Step";
            this.stepButton.ToolTipText = "Perform one iteration step";
            this.stepButton.Click += new System.EventHandler(this.StepButton_Click);
            // 
            // runButton
            // 
            this.runButton.Image = global::Neunet.Properties.Resources.Run;
            this.runButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.runButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(48, 22);
            this.runButton.Text = "Run";
            this.runButton.ToolTipText = "Perform multiple iteration steps until the end criterium is met";
            this.runButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Image = global::Neunet.Properties.Resources.Stop;
            this.stopButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(51, 22);
            this.stopButton.Text = "Stop";
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Image = global::Neunet.Properties.Resources.Settings;
            this.settingsButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.settingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(69, 22);
            this.settingsButton.Text = "Settings";
            this.settingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // RunToolStrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip);
            this.Name = "RunToolStrip";
            this.Size = new System.Drawing.Size(432, 25);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton resetButton;
        private System.Windows.Forms.ToolStripButton deleteButton;
        private System.Windows.Forms.ToolStripButton stepButton;
        private System.Windows.Forms.ToolStripButton runButton;
        private System.Windows.Forms.ToolStripButton stopButton;
        private System.Windows.Forms.ToolStripButton settingsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
