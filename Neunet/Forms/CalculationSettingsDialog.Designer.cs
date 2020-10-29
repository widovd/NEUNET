namespace Neunet.Forms
{
    partial class CalculationSettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalculationSettingsDialog));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.defaultButton = new System.Windows.Forms.ToolStripButton();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(387, 25);
            this.toolStrip1.TabIndex = 26;
            this.toolStrip1.Text = "toolStrip";
            // 
            // defaultButton
            // 
            this.defaultButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.defaultButton.Image = ((System.Drawing.Image)(resources.GetObject("defaultButton.Image")));
            this.defaultButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.defaultButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.defaultButton.Name = "defaultButton";
            this.defaultButton.Size = new System.Drawing.Size(49, 22);
            this.defaultButton.Text = "Default";
            this.defaultButton.Click += new System.EventHandler(this.DefaultButton_Click);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 25);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(387, 434);
            this.propertyGrid.TabIndex = 27;
            // 
            // CalculationSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(387, 493);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "CalculationSettingsDialog";
            this.Text = "Calculation settings";
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.propertyGrid, 0);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton defaultButton;
        private System.Windows.Forms.PropertyGrid propertyGrid;
    }
}
