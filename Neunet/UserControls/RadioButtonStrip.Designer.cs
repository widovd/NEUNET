namespace Neunet.UserControls
{
    partial class RadioButtonStrip
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
            this.RadioToolStrip = new System.Windows.Forms.ToolStrip();
            this.SuspendLayout();
            // 
            // RadioToolStrip
            // 
            this.RadioToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.RadioToolStrip.Location = new System.Drawing.Point(0, 0);
            this.RadioToolStrip.Name = "RadioToolStrip";
            this.RadioToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.RadioToolStrip.Size = new System.Drawing.Size(338, 25);
            this.RadioToolStrip.TabIndex = 0;
            this.RadioToolStrip.Text = "RadioToolStrip";
            // 
            // RadioButtonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.RadioToolStrip);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "RadioButtonControl";
            this.Size = new System.Drawing.Size(338, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip RadioToolStrip;
    }
}
