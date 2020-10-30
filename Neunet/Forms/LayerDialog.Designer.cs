namespace Neunet.Forms
{
    partial class LayerDialog
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
            this.neuronsTextBox = new Neunet.UserControls.IntegerTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // neuronsTextBox
            // 
            this.neuronsTextBox.Format = "G";
            this.neuronsTextBox.Location = new System.Drawing.Point(12, 32);
            this.neuronsTextBox.MaxValue = 2147483647;
            this.neuronsTextBox.MinValue = -2147483648;
            this.neuronsTextBox.Name = "neuronsTextBox";
            this.neuronsTextBox.Size = new System.Drawing.Size(100, 20);
            this.neuronsTextBox.TabIndex = 26;
            this.neuronsTextBox.Text = "0";
            this.neuronsTextBox.Value = 0;
            this.neuronsTextBox.ValueChanged += new System.EventHandler(this.NeuronsTextBox_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Neurons";
            // 
            // LayerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(286, 101);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.neuronsTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "LayerDialog";
            this.Text = "Layer editor";
            this.Controls.SetChildIndex(this.neuronsTextBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.IntegerTextBox neuronsTextBox;
        private System.Windows.Forms.Label label1;
    }
}
