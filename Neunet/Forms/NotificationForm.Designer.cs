namespace Neunet.Forms
{
    partial class NotificationForm
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
            this.components = new System.ComponentModel.Container();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notificationLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // notificationLabel
            // 
            this.notificationLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notificationLabel.Location = new System.Drawing.Point(0, 0);
            this.notificationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.notificationLabel.Name = "notificationLabel";
            this.notificationLabel.Size = new System.Drawing.Size(347, 52);
            this.notificationLabel.TabIndex = 0;
            this.notificationLabel.Text = "Notification";
            this.notificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Yellow;
            this.ClientSize = new System.Drawing.Size(347, 52);
            this.Controls.Add(this.notificationLabel);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Red;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotificationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notification";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.NotificationForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label notificationLabel;
    }
}