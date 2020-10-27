namespace Neunet.Forms
{
    partial class ExceptionDialog
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
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.FlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.Ok1Button = new System.Windows.Forms.Button();
            this.copyButton = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.exceptionListView = new System.Windows.Forms.ListView();
            this.levelColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorCodeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.messageColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorLabel = new System.Windows.Forms.Label();
            this.stackTraceTextBox = new System.Windows.Forms.TextBox();
            this.stackTraceLabel = new System.Windows.Forms.Label();
            this.TableLayoutPanel.SuspendLayout();
            this.FlowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanel
            // 
            this.TableLayoutPanel.ColumnCount = 1;
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel.Controls.Add(this.FlowLayoutPanel, 0, 0);
            this.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TableLayoutPanel.Location = new System.Drawing.Point(0, 337);
            this.TableLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            this.TableLayoutPanel.RowCount = 1;
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel.Size = new System.Drawing.Size(657, 34);
            this.TableLayoutPanel.TabIndex = 28;
            // 
            // FlowLayoutPanel
            // 
            this.FlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.FlowLayoutPanel.AutoSize = true;
            this.FlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FlowLayoutPanel.Controls.Add(this.Ok1Button);
            this.FlowLayoutPanel.Controls.Add(this.copyButton);
            this.FlowLayoutPanel.Location = new System.Drawing.Point(247, 2);
            this.FlowLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
            this.FlowLayoutPanel.Name = "FlowLayoutPanel";
            this.FlowLayoutPanel.Size = new System.Drawing.Size(162, 30);
            this.FlowLayoutPanel.TabIndex = 0;
            // 
            // Ok1Button
            // 
            this.Ok1Button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ok1Button.Image = global::Neunet.Properties.Resources.Ok;
            this.Ok1Button.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Ok1Button.Location = new System.Drawing.Point(3, 3);
            this.Ok1Button.Name = "Ok1Button";
            this.Ok1Button.Size = new System.Drawing.Size(75, 23);
            this.Ok1Button.TabIndex = 2;
            this.Ok1Button.Text = "Ok";
            this.Ok1Button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Ok1Button.UseVisualStyleBackColor = true;
            // 
            // copyButton
            // 
            this.copyButton.Location = new System.Drawing.Point(84, 3);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(75, 23);
            this.copyButton.TabIndex = 32;
            this.copyButton.Text = "Copy";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.exceptionListView);
            this.splitContainer.Panel1.Controls.Add(this.errorLabel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.stackTraceTextBox);
            this.splitContainer.Panel2.Controls.Add(this.stackTraceLabel);
            this.splitContainer.Size = new System.Drawing.Size(657, 337);
            this.splitContainer.SplitterDistance = 119;
            this.splitContainer.TabIndex = 31;
            // 
            // exceptionListView
            // 
            this.exceptionListView.AutoArrange = false;
            this.exceptionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.levelColumnHeader,
            this.typeColumnHeader,
            this.errorCodeColumnHeader,
            this.messageColumnHeader});
            this.exceptionListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exceptionListView.FullRowSelect = true;
            this.exceptionListView.HideSelection = false;
            this.exceptionListView.Location = new System.Drawing.Point(0, 23);
            this.exceptionListView.MultiSelect = false;
            this.exceptionListView.Name = "exceptionListView";
            this.exceptionListView.Size = new System.Drawing.Size(657, 96);
            this.exceptionListView.TabIndex = 0;
            this.exceptionListView.UseCompatibleStateImageBehavior = false;
            this.exceptionListView.View = System.Windows.Forms.View.Details;
            this.exceptionListView.SelectedIndexChanged += new System.EventHandler(this.ExceptionListView_SelectedIndexChanged);
            // 
            // levelColumnHeader
            // 
            this.levelColumnHeader.Text = "Level";
            this.levelColumnHeader.Width = 40;
            // 
            // typeColumnHeader
            // 
            this.typeColumnHeader.Text = "Type";
            this.typeColumnHeader.Width = 120;
            // 
            // errorCodeColumnHeader
            // 
            this.errorCodeColumnHeader.Text = "Error code";
            this.errorCodeColumnHeader.Width = 80;
            // 
            // messageColumnHeader
            // 
            this.messageColumnHeader.Text = "Message";
            this.messageColumnHeader.Width = 800;
            // 
            // errorLabel
            // 
            this.errorLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.errorLabel.Location = new System.Drawing.Point(0, 0);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(657, 23);
            this.errorLabel.TabIndex = 1;
            this.errorLabel.Text = "Error:";
            this.errorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stackTraceTextBox
            // 
            this.stackTraceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stackTraceTextBox.Location = new System.Drawing.Point(0, 23);
            this.stackTraceTextBox.Multiline = true;
            this.stackTraceTextBox.Name = "stackTraceTextBox";
            this.stackTraceTextBox.Size = new System.Drawing.Size(657, 191);
            this.stackTraceTextBox.TabIndex = 0;
            // 
            // stackTraceLabel
            // 
            this.stackTraceLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.stackTraceLabel.Location = new System.Drawing.Point(0, 0);
            this.stackTraceLabel.Name = "stackTraceLabel";
            this.stackTraceLabel.Size = new System.Drawing.Size(657, 23);
            this.stackTraceLabel.TabIndex = 2;
            this.stackTraceLabel.Text = "Stack trace:";
            this.stackTraceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ExceptionDialog
            // 
            this.AcceptButton = this.Ok1Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(657, 371);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.TableLayoutPanel);
            this.Name = "ExceptionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exception";
            this.Load += new System.EventHandler(this.ExceptionDialog_Load);
            this.TableLayoutPanel.ResumeLayout(false);
            this.TableLayoutPanel.PerformLayout();
            this.FlowLayoutPanel.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        protected System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel;
        protected System.Windows.Forms.Button Ok1Button;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TextBox stackTraceTextBox;
        private System.Windows.Forms.ListView exceptionListView;
        private System.Windows.Forms.ColumnHeader typeColumnHeader;
        private System.Windows.Forms.ColumnHeader errorCodeColumnHeader;
        private System.Windows.Forms.ColumnHeader messageColumnHeader;
        private System.Windows.Forms.ColumnHeader levelColumnHeader;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Label stackTraceLabel;
    }
}
