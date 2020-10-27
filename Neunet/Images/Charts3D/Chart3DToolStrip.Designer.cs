namespace Neunet.Images.Charts3D
{
    partial class Chart3DToolStrip
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
            this.viewGridButton = new System.Windows.Forms.ToolStripButton();
            this.viewAxesButton = new System.Windows.Forms.ToolStripButton();
            this.viewOriginButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.projectionToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.projectionDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.XY_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.YZ_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ZX_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.YX_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ZY_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XZ_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.swapLeftRightButton = new System.Windows.Forms.ToolStripButton();
            this.swapFrontRearButton = new System.Windows.Forms.ToolStripButton();
            this.swapTopBottomButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.swapAxesButton = new System.Windows.Forms.ToolStripButton();
            this.resetZoomButton = new System.Windows.Forms.ToolStripButton();
            this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.zoomInButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.resetPerspectiveButton = new System.Windows.Forms.ToolStripButton();
            this.decreasePerspectiveButton = new System.Windows.Forms.ToolStripButton();
            this.increasePerspectiveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewGridButton,
            this.viewAxesButton,
            this.viewOriginButton,
            this.toolStripSeparator1,
            this.projectionToolStripButton,
            this.projectionDropDownButton,
            this.toolStripSeparator2,
            this.swapLeftRightButton,
            this.swapFrontRearButton,
            this.swapTopBottomButton,
            this.toolStripSeparator3,
            this.swapAxesButton,
            this.resetZoomButton,
            this.zoomOutButton,
            this.zoomInButton,
            this.toolStripSeparator4,
            this.resetPerspectiveButton,
            this.decreasePerspectiveButton,
            this.increasePerspectiveButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(377, 25);
            this.toolStrip.TabIndex = 12;
            // 
            // viewGridButton
            // 
            this.viewGridButton.CheckOnClick = true;
            this.viewGridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.viewGridButton.Image = global::Neunet.Properties.Resources.ViewGrid;
            this.viewGridButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.viewGridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewGridButton.Name = "viewGridButton";
            this.viewGridButton.Size = new System.Drawing.Size(23, 22);
            this.viewGridButton.Text = "View grid";
            this.viewGridButton.CheckedChanged += new System.EventHandler(this.ViewGridButton_CheckedChanged);
            // 
            // viewAxesButton
            // 
            this.viewAxesButton.CheckOnClick = true;
            this.viewAxesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.viewAxesButton.Image = global::Neunet.Properties.Resources.ViewAxes;
            this.viewAxesButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.viewAxesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewAxesButton.Name = "viewAxesButton";
            this.viewAxesButton.Size = new System.Drawing.Size(23, 22);
            this.viewAxesButton.Text = "View axes";
            this.viewAxesButton.CheckedChanged += new System.EventHandler(this.ViewAxesButton_CheckedChanged);
            // 
            // viewOriginButton
            // 
            this.viewOriginButton.CheckOnClick = true;
            this.viewOriginButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.viewOriginButton.Image = global::Neunet.Properties.Resources.Origin;
            this.viewOriginButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.viewOriginButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewOriginButton.Name = "viewOriginButton";
            this.viewOriginButton.Size = new System.Drawing.Size(23, 22);
            this.viewOriginButton.Text = "toolStripButton1";
            this.viewOriginButton.CheckedChanged += new System.EventHandler(this.ViewOriginButton_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // projectionToolStripButton
            // 
            this.projectionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.projectionToolStripButton.Image = global::Neunet.Properties.Resources.xzProjection;
            this.projectionToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.projectionToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.projectionToolStripButton.Name = "projectionToolStripButton";
            this.projectionToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.projectionToolStripButton.Text = "projectionToolStripButton";
            this.projectionToolStripButton.Click += new System.EventHandler(this.RepeatOrientation_Click);
            // 
            // projectionDropDownButton
            // 
            this.projectionDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.projectionDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.XY_toolStripMenuItem,
            this.YZ_toolStripMenuItem,
            this.ZX_toolStripMenuItem,
            this.toolStripSeparator8,
            this.YX_toolStripMenuItem,
            this.ZY_toolStripMenuItem,
            this.XZ_toolStripMenuItem});
            this.projectionDropDownButton.Image = global::Neunet.Properties.Resources.Projection;
            this.projectionDropDownButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.projectionDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.projectionDropDownButton.Name = "projectionDropDownButton";
            this.projectionDropDownButton.Size = new System.Drawing.Size(29, 22);
            this.projectionDropDownButton.Text = "projectionDropDownButton";
            // 
            // XY_toolStripMenuItem
            // 
            this.XY_toolStripMenuItem.Image = global::Neunet.Properties.Resources.yxProjection;
            this.XY_toolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.XY_toolStripMenuItem.Name = "XY_toolStripMenuItem";
            this.XY_toolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.XY_toolStripMenuItem.Text = "X/Y";
            this.XY_toolStripMenuItem.Click += new System.EventHandler(this.XY_Orientation_Click);
            // 
            // YZ_toolStripMenuItem
            // 
            this.YZ_toolStripMenuItem.Image = global::Neunet.Properties.Resources.zyProjection;
            this.YZ_toolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.YZ_toolStripMenuItem.Name = "YZ_toolStripMenuItem";
            this.YZ_toolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.YZ_toolStripMenuItem.Text = "Y/Z";
            this.YZ_toolStripMenuItem.Click += new System.EventHandler(this.YZ_Orientation_Click);
            // 
            // ZX_toolStripMenuItem
            // 
            this.ZX_toolStripMenuItem.Image = global::Neunet.Properties.Resources.xzProjection;
            this.ZX_toolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ZX_toolStripMenuItem.Name = "ZX_toolStripMenuItem";
            this.ZX_toolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.ZX_toolStripMenuItem.Text = "Z/X";
            this.ZX_toolStripMenuItem.Click += new System.EventHandler(this.ZX_Orientation_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(90, 6);
            // 
            // YX_toolStripMenuItem
            // 
            this.YX_toolStripMenuItem.Image = global::Neunet.Properties.Resources.xyProjection;
            this.YX_toolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.YX_toolStripMenuItem.Name = "YX_toolStripMenuItem";
            this.YX_toolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.YX_toolStripMenuItem.Text = "Y/X";
            this.YX_toolStripMenuItem.Click += new System.EventHandler(this.YX_Orientation_Click);
            // 
            // ZY_toolStripMenuItem
            // 
            this.ZY_toolStripMenuItem.Image = global::Neunet.Properties.Resources.yzProjection;
            this.ZY_toolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ZY_toolStripMenuItem.Name = "ZY_toolStripMenuItem";
            this.ZY_toolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.ZY_toolStripMenuItem.Text = "Z/Y";
            this.ZY_toolStripMenuItem.Click += new System.EventHandler(this.ZY_Orientation_Click);
            // 
            // XZ_toolStripMenuItem
            // 
            this.XZ_toolStripMenuItem.Image = global::Neunet.Properties.Resources.zxProjection;
            this.XZ_toolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.XZ_toolStripMenuItem.Name = "XZ_toolStripMenuItem";
            this.XZ_toolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.XZ_toolStripMenuItem.Text = "X/Z";
            this.XZ_toolStripMenuItem.Click += new System.EventHandler(this.XZ_Orientation_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // swapLeftRightButton
            // 
            this.swapLeftRightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.swapLeftRightButton.Image = global::Neunet.Properties.Resources.SwapLeftRight;
            this.swapLeftRightButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.swapLeftRightButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.swapLeftRightButton.Name = "swapLeftRightButton";
            this.swapLeftRightButton.Size = new System.Drawing.Size(23, 22);
            this.swapLeftRightButton.Text = "Left ↔ Right";
            this.swapLeftRightButton.Click += new System.EventHandler(this.SwapLeftRightButton_Click);
            // 
            // swapFrontRearButton
            // 
            this.swapFrontRearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.swapFrontRearButton.Image = global::Neunet.Properties.Resources.SwapFrontRear;
            this.swapFrontRearButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.swapFrontRearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.swapFrontRearButton.Name = "swapFrontRearButton";
            this.swapFrontRearButton.Size = new System.Drawing.Size(23, 22);
            this.swapFrontRearButton.Text = "Front ↔ Rear";
            this.swapFrontRearButton.Click += new System.EventHandler(this.SwapFrontRearButton_Click);
            // 
            // swapTopBottomButton
            // 
            this.swapTopBottomButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.swapTopBottomButton.Image = global::Neunet.Properties.Resources.SwapTopBottom;
            this.swapTopBottomButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.swapTopBottomButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.swapTopBottomButton.Name = "swapTopBottomButton";
            this.swapTopBottomButton.Size = new System.Drawing.Size(23, 22);
            this.swapTopBottomButton.Text = "Top ↔ Bottom";
            this.swapTopBottomButton.Click += new System.EventHandler(this.SwapTopBottomButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // swapAxesButton
            // 
            this.swapAxesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.swapAxesButton.Image = global::Neunet.Properties.Resources.SwapAxes;
            this.swapAxesButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.swapAxesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.swapAxesButton.Name = "swapAxesButton";
            this.swapAxesButton.Size = new System.Drawing.Size(23, 22);
            this.swapAxesButton.Text = "Swap Axes";
            this.swapAxesButton.Click += new System.EventHandler(this.SwapAxesButton_Click);
            // 
            // resetZoomButton
            // 
            this.resetZoomButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.resetZoomButton.Image = global::Neunet.Properties.Resources.ResetZoom;
            this.resetZoomButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetZoomButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetZoomButton.Name = "resetZoomButton";
            this.resetZoomButton.Size = new System.Drawing.Size(23, 22);
            this.resetZoomButton.Text = "Reset zoom";
            this.resetZoomButton.Click += new System.EventHandler(this.ResetZoomButton_Click);
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutButton.Image = global::Neunet.Properties.Resources.ZoomOut;
            this.zoomOutButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(23, 22);
            this.zoomOutButton.Text = "Zoom out";
            this.zoomOutButton.Click += new System.EventHandler(this.ZoomOutButton_Click);
            // 
            // zoomInButton
            // 
            this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInButton.Image = global::Neunet.Properties.Resources.ZoomIn;
            this.zoomInButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(23, 22);
            this.zoomInButton.Text = "Zoom in";
            this.zoomInButton.Click += new System.EventHandler(this.ZoomInButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // resetPerspectiveButton
            // 
            this.resetPerspectiveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.resetPerspectiveButton.Image = global::Neunet.Properties.Resources.ResetPerspective;
            this.resetPerspectiveButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetPerspectiveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetPerspectiveButton.Name = "resetPerspectiveButton";
            this.resetPerspectiveButton.Size = new System.Drawing.Size(23, 22);
            this.resetPerspectiveButton.Text = "Reset perspective";
            this.resetPerspectiveButton.Click += new System.EventHandler(this.ResetPerspectiveButton_Click);
            // 
            // decreasePerspectiveButton
            // 
            this.decreasePerspectiveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.decreasePerspectiveButton.Image = global::Neunet.Properties.Resources.DecreasePerspective;
            this.decreasePerspectiveButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.decreasePerspectiveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.decreasePerspectiveButton.Name = "decreasePerspectiveButton";
            this.decreasePerspectiveButton.Size = new System.Drawing.Size(23, 22);
            this.decreasePerspectiveButton.Text = "Decrease perspective";
            this.decreasePerspectiveButton.Click += new System.EventHandler(this.DecreasePerspectiveButton_Click);
            // 
            // increasePerspectiveButton
            // 
            this.increasePerspectiveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.increasePerspectiveButton.Image = global::Neunet.Properties.Resources.IncreasePerspective;
            this.increasePerspectiveButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.increasePerspectiveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.increasePerspectiveButton.Name = "increasePerspectiveButton";
            this.increasePerspectiveButton.Size = new System.Drawing.Size(23, 20);
            this.increasePerspectiveButton.Text = "Increase perspective";
            this.increasePerspectiveButton.Click += new System.EventHandler(this.IncreasePerspectiveButton_Click);
            // 
            // Chart3DToolStrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip);
            this.Name = "Chart3DToolStrip";
            this.Size = new System.Drawing.Size(377, 25);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton viewGridButton;
        private System.Windows.Forms.ToolStripButton viewAxesButton;
        private System.Windows.Forms.ToolStripButton viewOriginButton;
        private System.Windows.Forms.ToolStripButton projectionToolStripButton;
        private System.Windows.Forms.ToolStripDropDownButton projectionDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem XY_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem YZ_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ZX_toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem YX_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ZY_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem XZ_toolStripMenuItem;
        private System.Windows.Forms.ToolStripButton swapLeftRightButton;
        private System.Windows.Forms.ToolStripButton swapFrontRearButton;
        private System.Windows.Forms.ToolStripButton swapTopBottomButton;
        private System.Windows.Forms.ToolStripButton swapAxesButton;
        private System.Windows.Forms.ToolStripButton resetZoomButton;
        private System.Windows.Forms.ToolStripButton zoomOutButton;
        private System.Windows.Forms.ToolStripButton zoomInButton;
        private System.Windows.Forms.ToolStripButton resetPerspectiveButton;
        private System.Windows.Forms.ToolStripButton decreasePerspectiveButton;
        private System.Windows.Forms.ToolStripButton increasePerspectiveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}
