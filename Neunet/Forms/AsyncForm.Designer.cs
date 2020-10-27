namespace Neunet.Forms
{
    partial class AsyncForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.messageLabel = new System.Windows.Forms.Label();
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.FlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.stopButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TableLayoutPanel.SuspendLayout();
            this.FlowLayoutPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // messageLabel
            // 
            this.messageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLabel.Location = new System.Drawing.Point(0, 0);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(477, 42);
            this.messageLabel.TabIndex = 30;
            this.messageLabel.Text = "Calculating ...";
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TableLayoutPanel
            // 
            this.TableLayoutPanel.ColumnCount = 1;
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel.Controls.Add(this.FlowLayoutPanel, 0, 0);
            this.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TableLayoutPanel.Location = new System.Drawing.Point(0, 42);
            this.TableLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            this.TableLayoutPanel.RowCount = 1;
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel.Size = new System.Drawing.Size(477, 34);
            this.TableLayoutPanel.TabIndex = 29;
            // 
            // FlowLayoutPanel
            // 
            this.FlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.FlowLayoutPanel.AutoSize = true;
            this.FlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FlowLayoutPanel.Controls.Add(this.stopButton);
            this.FlowLayoutPanel.Location = new System.Drawing.Point(198, 2);
            this.FlowLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
            this.FlowLayoutPanel.Name = "FlowLayoutPanel";
            this.FlowLayoutPanel.Size = new System.Drawing.Size(81, 30);
            this.FlowLayoutPanel.TabIndex = 0;
            // 
            // stopButton
            // 
            this.stopButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.stopButton.Location = new System.Drawing.Point(3, 3);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 24);
            this.stopButton.TabIndex = 3;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 76);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(477, 22);
            this.statusStrip1.TabIndex = 31;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(66, 17);
            this.statusLabel.Text = "statusLabel";
            // 
            // AsyncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(477, 98);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.TableLayoutPanel);
            this.Controls.Add(this.statusStrip1);
            this.Name = "AsyncForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "";
            this.Load += new System.EventHandler(this.AsyncForm_Load);
            this.TableLayoutPanel.ResumeLayout(false);
            this.TableLayoutPanel.PerformLayout();
            this.FlowLayoutPanel.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}
