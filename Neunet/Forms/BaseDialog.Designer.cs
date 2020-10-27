namespace Neunet.Forms
{
    partial class BaseDialog
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.bottomTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.bottomFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.bottomTableLayoutPanel.SuspendLayout();
            this.bottomFlowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Image = global::Neunet.Properties.Resources.Cancel;
            this.cancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cancelButton.Location = new System.Drawing.Point(84, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 24);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Image = global::Neunet.Properties.Resources.Ok;
            this.okButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.okButton.Location = new System.Drawing.Point(3, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 24);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Ok";
            this.okButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // bottomTableLayoutPanel
            // 
            this.bottomTableLayoutPanel.ColumnCount = 1;
            this.bottomTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.bottomTableLayoutPanel.Controls.Add(this.bottomFlowLayoutPanel, 0, 0);
            this.bottomTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomTableLayoutPanel.Location = new System.Drawing.Point(0, 173);
            this.bottomTableLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
            this.bottomTableLayoutPanel.Name = "bottomTableLayoutPanel";
            this.bottomTableLayoutPanel.RowCount = 1;
            this.bottomTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.bottomTableLayoutPanel.Size = new System.Drawing.Size(286, 34);
            this.bottomTableLayoutPanel.TabIndex = 25;
            // 
            // bottomFlowLayoutPanel
            // 
            this.bottomFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.bottomFlowLayoutPanel.AutoSize = true;
            this.bottomFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bottomFlowLayoutPanel.Controls.Add(this.okButton);
            this.bottomFlowLayoutPanel.Controls.Add(this.cancelButton);
            this.bottomFlowLayoutPanel.Location = new System.Drawing.Point(62, 2);
            this.bottomFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
            this.bottomFlowLayoutPanel.Name = "bottomFlowLayoutPanel";
            this.bottomFlowLayoutPanel.Size = new System.Drawing.Size(162, 30);
            this.bottomFlowLayoutPanel.TabIndex = 0;
            // 
            // BaseDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(286, 207);
            this.Controls.Add(this.bottomTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseDialog";
            this.Text = "DialogForm";
            this.bottomTableLayoutPanel.ResumeLayout(false);
            this.bottomTableLayoutPanel.PerformLayout();
            this.bottomFlowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.Button okButton;
        protected System.Windows.Forms.Button cancelButton;
        protected System.Windows.Forms.TableLayoutPanel bottomTableLayoutPanel;
        protected System.Windows.Forms.FlowLayoutPanel bottomFlowLayoutPanel;
    }
}
