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
            this.digitsButton = new System.Windows.Forms.Button();
            this.bugsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // digitsButton
            // 
            this.digitsButton.Location = new System.Drawing.Point(12, 12);
            this.digitsButton.Name = "digitsButton";
            this.digitsButton.Size = new System.Drawing.Size(75, 23);
            this.digitsButton.TabIndex = 0;
            this.digitsButton.Text = "Digits";
            this.digitsButton.UseVisualStyleBackColor = true;
            this.digitsButton.Click += new System.EventHandler(this.DigitsButton_Click);
            // 
            // bugsButton
            // 
            this.bugsButton.Location = new System.Drawing.Point(12, 41);
            this.bugsButton.Name = "bugsButton";
            this.bugsButton.Size = new System.Drawing.Size(75, 23);
            this.bugsButton.TabIndex = 1;
            this.bugsButton.Text = "Bugs";
            this.bugsButton.UseVisualStyleBackColor = true;
            this.bugsButton.Click += new System.EventHandler(this.BugsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 73);
            this.Controls.Add(this.bugsButton);
            this.Controls.Add(this.digitsButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button digitsButton;
        private System.Windows.Forms.Button bugsButton;
    }
}