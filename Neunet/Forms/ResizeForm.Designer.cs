namespace Neunet.Forms
{
    partial class ResizeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResizeForm));
            this.WidthLabel = new System.Windows.Forms.Label();
            this.HeightLabel = new System.Windows.Forms.Label();
            this.WidthTextBox = new System.Windows.Forms.TextBox();
            this.HeightTextBox = new System.Windows.Forms.TextBox();
            this.OKButton1 = new System.Windows.Forms.Button();
            this.CancelButton1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // WidthLabel
            // 
            this.WidthLabel.AutoSize = true;
            this.WidthLabel.Location = new System.Drawing.Point(9, 9);
            this.WidthLabel.Name = "WidthLabel";
            this.WidthLabel.Size = new System.Drawing.Size(35, 13);
            this.WidthLabel.TabIndex = 0;
            this.WidthLabel.Text = "Width";
            // 
            // HeightLabel
            // 
            this.HeightLabel.AutoSize = true;
            this.HeightLabel.Location = new System.Drawing.Point(90, 9);
            this.HeightLabel.Name = "HeightLabel";
            this.HeightLabel.Size = new System.Drawing.Size(38, 13);
            this.HeightLabel.TabIndex = 1;
            this.HeightLabel.Text = "Height";
            // 
            // WidthTextBox
            // 
            this.WidthTextBox.Location = new System.Drawing.Point(12, 25);
            this.WidthTextBox.Name = "WidthTextBox";
            this.WidthTextBox.Size = new System.Drawing.Size(75, 20);
            this.WidthTextBox.TabIndex = 2;
            // 
            // HeightTextBox
            // 
            this.HeightTextBox.Location = new System.Drawing.Point(93, 25);
            this.HeightTextBox.Name = "HeightTextBox";
            this.HeightTextBox.Size = new System.Drawing.Size(75, 20);
            this.HeightTextBox.TabIndex = 3;
            // 
            // OKButton1
            // 
            this.OKButton1.Location = new System.Drawing.Point(12, 62);
            this.OKButton1.Name = "OKButton1";
            this.OKButton1.Size = new System.Drawing.Size(75, 23);
            this.OKButton1.TabIndex = 4;
            this.OKButton1.Text = "OK";
            this.OKButton1.UseVisualStyleBackColor = true;
            this.OKButton1.Click += new System.EventHandler(this.OKButton1_Click);
            // 
            // CancelButton1
            // 
            this.CancelButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton1.Location = new System.Drawing.Point(93, 62);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(75, 23);
            this.CancelButton1.TabIndex = 5;
            this.CancelButton1.Text = "Cancel";
            this.CancelButton1.UseVisualStyleBackColor = true;
            // 
            // cResizeForm
            // 
            this.AcceptButton = this.OKButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelButton1;
            this.ClientSize = new System.Drawing.Size(180, 99);
            this.Controls.Add(this.CancelButton1);
            this.Controls.Add(this.OKButton1);
            this.Controls.Add(this.HeightTextBox);
            this.Controls.Add(this.WidthTextBox);
            this.Controls.Add(this.HeightLabel);
            this.Controls.Add(this.WidthLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cResizeForm";
            this.Text = "Resize";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WidthLabel;
        private System.Windows.Forms.Label HeightLabel;
        private System.Windows.Forms.TextBox WidthTextBox;
        private System.Windows.Forms.TextBox HeightTextBox;
        private System.Windows.Forms.Button OKButton1;
        private System.Windows.Forms.Button CancelButton1;
    }
}