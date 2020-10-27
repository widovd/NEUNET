namespace Neunet.Images.Charts3D
{
    partial class WireframeToolStrip
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.defaultResolutionButton = new System.Windows.Forms.ToolStripButton();
            this.decreaseResolutionButton = new System.Windows.Forms.ToolStripButton();
            this.increaseResolutionButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sliceNoneButton = new System.Windows.Forms.ToolStripButton();
            this.sliceUButton = new System.Windows.Forms.ToolStripButton();
            this.sliceVButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sliceValueDownButton = new System.Windows.Forms.ToolStripButton();
            this.sliceValueDefaultButton = new System.Windows.Forms.ToolStripButton();
            this.sliceValueUpButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.viewRaysButton = new System.Windows.Forms.ToolStripButton();
            this.viewNormalsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultResolutionButton,
            this.decreaseResolutionButton,
            this.increaseResolutionButton,
            this.toolStripSeparator1,
            this.sliceNoneButton,
            this.sliceUButton,
            this.sliceVButton,
            this.toolStripSeparator2,
            this.sliceValueDownButton,
            this.sliceValueDefaultButton,
            this.sliceValueUpButton,
            this.toolStripSeparator3,
            this.toolStripSeparator4,
            this.viewRaysButton,
            this.viewNormalsButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(470, 25);
            this.toolStrip.TabIndex = 9;
            // 
            // defaultResolutionButton
            // 
            this.defaultResolutionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.defaultResolutionButton.Image = global::Neunet.Properties.Resources.WireframeResolutionDefault;
            this.defaultResolutionButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.defaultResolutionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.defaultResolutionButton.Name = "defaultResolutionButton";
            this.defaultResolutionButton.Size = new System.Drawing.Size(23, 22);
            this.defaultResolutionButton.Text = "Maximum resolution";
            this.defaultResolutionButton.Click += new System.EventHandler(this.DefaultResolutionButton_Click);
            // 
            // decreaseResolutionButton
            // 
            this.decreaseResolutionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.decreaseResolutionButton.Image = global::Neunet.Properties.Resources.WireframeResolutionDown;
            this.decreaseResolutionButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.decreaseResolutionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.decreaseResolutionButton.Name = "decreaseResolutionButton";
            this.decreaseResolutionButton.Size = new System.Drawing.Size(23, 22);
            this.decreaseResolutionButton.Text = "Decrease resolution";
            this.decreaseResolutionButton.Click += new System.EventHandler(this.DecreaseResolutionButton_Click);
            // 
            // increaseResolutionButton
            // 
            this.increaseResolutionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.increaseResolutionButton.Image = global::Neunet.Properties.Resources.WireframeResolutionUp;
            this.increaseResolutionButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.increaseResolutionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.increaseResolutionButton.Name = "increaseResolutionButton";
            this.increaseResolutionButton.Size = new System.Drawing.Size(23, 22);
            this.increaseResolutionButton.Text = "Increase resolution";
            this.increaseResolutionButton.Click += new System.EventHandler(this.IncreaseResolutionButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // sliceNoneButton
            // 
            this.sliceNoneButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sliceNoneButton.Image = global::Neunet.Properties.Resources.ViewSliceNone;
            this.sliceNoneButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sliceNoneButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sliceNoneButton.Name = "sliceNoneButton";
            this.sliceNoneButton.Size = new System.Drawing.Size(23, 22);
            this.sliceNoneButton.Text = "Show all";
            this.sliceNoneButton.Click += new System.EventHandler(this.SliceNoneButton_Click);
            // 
            // sliceUButton
            // 
            this.sliceUButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sliceUButton.Image = global::Neunet.Properties.Resources.ViewSliceU;
            this.sliceUButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sliceUButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sliceUButton.Name = "sliceUButton";
            this.sliceUButton.Size = new System.Drawing.Size(23, 22);
            this.sliceUButton.Text = "Slice U";
            this.sliceUButton.Click += new System.EventHandler(this.SliceUButton_Click);
            // 
            // sliceVButton
            // 
            this.sliceVButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sliceVButton.Image = global::Neunet.Properties.Resources.ViewSliceV;
            this.sliceVButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sliceVButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sliceVButton.Name = "sliceVButton";
            this.sliceVButton.Size = new System.Drawing.Size(23, 22);
            this.sliceVButton.Text = "Slice V";
            this.sliceVButton.Click += new System.EventHandler(this.SliceVButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // sliceValueDownButton
            // 
            this.sliceValueDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sliceValueDownButton.Image = global::Neunet.Properties.Resources.ViewSliceDown;
            this.sliceValueDownButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sliceValueDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sliceValueDownButton.Name = "sliceValueDownButton";
            this.sliceValueDownButton.Size = new System.Drawing.Size(23, 22);
            this.sliceValueDownButton.Text = "Slice value down";
            this.sliceValueDownButton.Click += new System.EventHandler(this.SliceValueDownButton_Click);
            // 
            // sliceValueDefaultButton
            // 
            this.sliceValueDefaultButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sliceValueDefaultButton.Image = global::Neunet.Properties.Resources.ViewSliceDefault;
            this.sliceValueDefaultButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sliceValueDefaultButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sliceValueDefaultButton.Name = "sliceValueDefaultButton";
            this.sliceValueDefaultButton.Size = new System.Drawing.Size(23, 22);
            this.sliceValueDefaultButton.Text = "Slice value default";
            this.sliceValueDefaultButton.Click += new System.EventHandler(this.SliceValueDefaultButton_Click);
            // 
            // sliceValueUpButton
            // 
            this.sliceValueUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sliceValueUpButton.Image = global::Neunet.Properties.Resources.ViewSliceUp;
            this.sliceValueUpButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sliceValueUpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sliceValueUpButton.Name = "sliceValueUpButton";
            this.sliceValueUpButton.Size = new System.Drawing.Size(23, 22);
            this.sliceValueUpButton.Text = "Slice value up";
            this.sliceValueUpButton.Click += new System.EventHandler(this.SliceValueUpButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // viewRaysButton
            // 
            this.viewRaysButton.CheckOnClick = true;
            this.viewRaysButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.viewRaysButton.Image = global::Neunet.Properties.Resources.ViewRays;
            this.viewRaysButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.viewRaysButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewRaysButton.Name = "viewRaysButton";
            this.viewRaysButton.Size = new System.Drawing.Size(23, 22);
            this.viewRaysButton.Text = "View rays";
            this.viewRaysButton.CheckedChanged += new System.EventHandler(this.ViewRays_CheckedChanged);
            // 
            // viewNormalsButton
            // 
            this.viewNormalsButton.CheckOnClick = true;
            this.viewNormalsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.viewNormalsButton.Image = global::Neunet.Properties.Resources.ViewNormalVectors;
            this.viewNormalsButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.viewNormalsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewNormalsButton.Name = "viewNormalsButton";
            this.viewNormalsButton.Size = new System.Drawing.Size(23, 22);
            this.viewNormalsButton.Text = "View normal vectors";
            this.viewNormalsButton.CheckedChanged += new System.EventHandler(this.ViewNormals_CheckedChanged);
            // 
            // WireframeToolStrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip);
            this.Name = "WireframeToolStrip";
            this.Size = new System.Drawing.Size(470, 25);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton defaultResolutionButton;
        private System.Windows.Forms.ToolStripButton decreaseResolutionButton;
        private System.Windows.Forms.ToolStripButton increaseResolutionButton;
        private System.Windows.Forms.ToolStripButton sliceNoneButton;
        private System.Windows.Forms.ToolStripButton sliceUButton;
        private System.Windows.Forms.ToolStripButton sliceVButton;
        private System.Windows.Forms.ToolStripButton sliceValueDownButton;
        private System.Windows.Forms.ToolStripButton sliceValueDefaultButton;
        private System.Windows.Forms.ToolStripButton sliceValueUpButton;
        private System.Windows.Forms.ToolStripButton viewRaysButton;
        private System.Windows.Forms.ToolStripButton viewNormalsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}
