namespace Neunet.Images
{
    partial class BaseImage
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.imageContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.imageContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(400, 300);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.SizeChanged += new System.EventHandler(this.PictureBox_SizeChanged);
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox_Paint);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseUp);
            // 
            // imageContextMenuStrip
            // 
            this.imageContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.resizeToolStripMenuItem});
            this.imageContextMenuStrip.Name = "ImageContextMenuStrip";
            this.imageContextMenuStrip.Size = new System.Drawing.Size(170, 70);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.copyToolStripMenuItem.Text = "Copy to clipboard";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.Copy_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAs_Click);
            // 
            // resizeToolStripMenuItem
            // 
            this.resizeToolStripMenuItem.Name = "resizeToolStripMenuItem";
            this.resizeToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.resizeToolStripMenuItem.Text = "Resize image";
            this.resizeToolStripMenuItem.Click += new System.EventHandler(this.Resize_Click);
            // 
            // BaseImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.pictureBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "BaseImage";
            this.Size = new System.Drawing.Size(400, 300);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.imageContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ContextMenuStrip imageContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeToolStripMenuItem;
    }
}
