namespace Neunet.Images.Charts2D
{
    partial class DistributionToolStrip
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DistributionToolStrip));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.distributionVisibleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.parametricGridVisibleButton = new System.Windows.Forms.ToolStripButton();
            this.parametricGridPenColorButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.parametricPointsVisible = new System.Windows.Forms.ToolStripButton();
            this.pointsPenButton = new System.Windows.Forms.ToolStripButton();
            this.pointsBrushButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.intersectionsVisibleButton = new System.Windows.Forms.ToolStripButton();
            this.intersectionsPenButton = new System.Windows.Forms.ToolStripButton();
            this.intersectionsBrushButton = new System.Windows.Forms.ToolStripButton();
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
            this.distributionVisibleButton,
            this.toolStripSeparator6,
            this.parametricGridVisibleButton,
            this.parametricGridPenColorButton,
            this.toolStripSeparator1,
            this.parametricPointsVisible,
            this.pointsPenButton,
            this.pointsBrushButton,
            this.toolStripSeparator2,
            this.intersectionsVisibleButton,
            this.intersectionsPenButton,
            this.intersectionsBrushButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(335, 25);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "DistributionToolStrip";
            // 
            // distributionVisibleButton
            // 
            this.distributionVisibleButton.CheckOnClick = true;
            this.distributionVisibleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.distributionVisibleButton.Image = global::Neunet.Properties.Resources.GradientInterpolated;
            this.distributionVisibleButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.distributionVisibleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.distributionVisibleButton.Name = "distributionVisibleButton";
            this.distributionVisibleButton.Size = new System.Drawing.Size(23, 22);
            this.distributionVisibleButton.Text = "Show distribution";
            this.distributionVisibleButton.CheckedChanged += new System.EventHandler(this.DistributionVisibleButton_CheckedChanged);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // linesVisibleButton
            // 
            this.parametricGridVisibleButton.CheckOnClick = true;
            this.parametricGridVisibleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.parametricGridVisibleButton.Image = ((System.Drawing.Image)(resources.GetObject("linesVisibleButton.Image")));
            this.parametricGridVisibleButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.parametricGridVisibleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.parametricGridVisibleButton.Name = "linesVisibleButton";
            this.parametricGridVisibleButton.Size = new System.Drawing.Size(23, 22);
            this.parametricGridVisibleButton.Text = "Show parametric grid";
            this.parametricGridVisibleButton.CheckedChanged += new System.EventHandler(this.ParametricGridVisible_CheckedChanged);
            // 
            // parametricGridPenColorButton
            // 
            this.parametricGridPenColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.parametricGridPenColorButton.Image = global::Neunet.Properties.Resources.ParamLinesColor;
            this.parametricGridPenColorButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.parametricGridPenColorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.parametricGridPenColorButton.Name = "parametricGridPenColorButton";
            this.parametricGridPenColorButton.Size = new System.Drawing.Size(23, 22);
            this.parametricGridPenColorButton.Text = "Parametric grid pen color";
            this.parametricGridPenColorButton.Click += new System.EventHandler(this.ParametricGridPenColorButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // pointsVisibleButton
            // 
            this.parametricPointsVisible.CheckOnClick = true;
            this.parametricPointsVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.parametricPointsVisible.Image = global::Neunet.Properties.Resources.ParamPoints;
            this.parametricPointsVisible.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.parametricPointsVisible.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.parametricPointsVisible.Name = "pointsVisibleButton";
            this.parametricPointsVisible.Size = new System.Drawing.Size(23, 22);
            this.parametricPointsVisible.Text = "Show parametric points";
            this.parametricPointsVisible.CheckedChanged += new System.EventHandler(this.PointsVisibleButton_CheckedChanged);
            // 
            // pointsPenButton
            // 
            this.pointsPenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pointsPenButton.Image = global::Neunet.Properties.Resources.ParamPointsPenColor;
            this.pointsPenButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pointsPenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pointsPenButton.Name = "pointsPenButton";
            this.pointsPenButton.Size = new System.Drawing.Size(23, 22);
            this.pointsPenButton.Text = "Parametric points pen color";
            this.pointsPenButton.Click += new System.EventHandler(this.ParametricPointsPenColorButton_Click);
            // 
            // pointsBrushButton
            // 
            this.pointsBrushButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pointsBrushButton.Image = global::Neunet.Properties.Resources.ParamPointsBrushColor;
            this.pointsBrushButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pointsBrushButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pointsBrushButton.Name = "pointsBrushButton";
            this.pointsBrushButton.Size = new System.Drawing.Size(23, 22);
            this.pointsBrushButton.Text = "Parametric points brush color";
            this.pointsBrushButton.Click += new System.EventHandler(this.ParametricPointsBrushColorButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // intersectionsVisibleButton
            // 
            this.intersectionsVisibleButton.CheckOnClick = true;
            this.intersectionsVisibleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.intersectionsVisibleButton.Image = global::Neunet.Properties.Resources.ParamIntersections;
            this.intersectionsVisibleButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.intersectionsVisibleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.intersectionsVisibleButton.Name = "intersectionsVisibleButton";
            this.intersectionsVisibleButton.Size = new System.Drawing.Size(23, 22);
            this.intersectionsVisibleButton.Text = "Show self-intersections";
            this.intersectionsVisibleButton.CheckedChanged += new System.EventHandler(this.IntersectionsVisibleButton_CheckedChanged);
            // 
            // intersectionsPenButton
            // 
            this.intersectionsPenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.intersectionsPenButton.Image = global::Neunet.Properties.Resources.ParamInterPenColor;
            this.intersectionsPenButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.intersectionsPenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.intersectionsPenButton.Name = "intersectionsPenButton";
            this.intersectionsPenButton.Size = new System.Drawing.Size(23, 22);
            this.intersectionsPenButton.Text = "Self-intersection pen color";
            this.intersectionsPenButton.Click += new System.EventHandler(this.IntersectionsPenColorButton_Click);
            // 
            // intersectionsBrushButton
            // 
            this.intersectionsBrushButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.intersectionsBrushButton.Image = global::Neunet.Properties.Resources.ParamInterBrushColor;
            this.intersectionsBrushButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.intersectionsBrushButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.intersectionsBrushButton.Name = "intersectionsBrushButton";
            this.intersectionsBrushButton.Size = new System.Drawing.Size(23, 22);
            this.intersectionsBrushButton.Text = "Self-intersection brush color";
            this.intersectionsBrushButton.Click += new System.EventHandler(this.IntersectionsBrushColorButton_Click);
            // 
            // DistributionToolStrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip);
            this.Name = "DistributionToolStrip";
            this.Size = new System.Drawing.Size(335, 25);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton distributionVisibleButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton parametricGridVisibleButton;
        private System.Windows.Forms.ToolStripButton parametricGridPenColorButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton parametricPointsVisible;
        private System.Windows.Forms.ToolStripButton pointsPenButton;
        private System.Windows.Forms.ToolStripButton pointsBrushButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton intersectionsVisibleButton;
        private System.Windows.Forms.ToolStripButton intersectionsPenButton;
        private System.Windows.Forms.ToolStripButton intersectionsBrushButton;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}
