namespace Neunet.Images.Charts2D
{
    partial class GradientToolStrip
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
            this.resetToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.inverseGradientToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.colorGradientDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripButton,
            this.toolStripSeparator5,
            this.inverseGradientToolStripButton,
            this.colorGradientDropDownButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(83, 25);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "DistributionToolStrip";
            // 
            // resetToolStripButton
            // 
            this.resetToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.resetToolStripButton.Image = global::Neunet.Properties.Resources.ResetOrigin;
            this.resetToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetToolStripButton.Name = "resetToolStripButton";
            this.resetToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.resetToolStripButton.Text = "Reset origin and zoom";
            this.resetToolStripButton.ToolTipText = "Reset origin and zoom";
            this.resetToolStripButton.Click += new System.EventHandler(this.ResetImageToolStripButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // inverseGradientToolStripButton
            // 
            this.inverseGradientToolStripButton.CheckOnClick = true;
            this.inverseGradientToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.inverseGradientToolStripButton.Image = global::Neunet.Properties.Resources.InverseGradient;
            this.inverseGradientToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.inverseGradientToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.inverseGradientToolStripButton.Name = "inverseGradientToolStripButton";
            this.inverseGradientToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.inverseGradientToolStripButton.Text = "Inverse gradient";
            this.inverseGradientToolStripButton.CheckedChanged += new System.EventHandler(this.InverseGradientToolStripButton_Click);
            // 
            // colorGradientDropDownButton
            // 
            this.colorGradientDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.colorGradientDropDownButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.colorGradientDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.colorGradientDropDownButton.Name = "colorGradientDropDownButton";
            this.colorGradientDropDownButton.Size = new System.Drawing.Size(29, 20);
            this.colorGradientDropDownButton.Text = "Color gradient type";
            // 
            // GradientToolStrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip);
            this.Name = "GradientToolStrip";
            this.Size = new System.Drawing.Size(83, 25);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton resetToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton inverseGradientToolStripButton;
        private System.Windows.Forms.ToolStripDropDownButton colorGradientDropDownButton;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}
