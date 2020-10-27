namespace Neunet.Forms
{
    partial class BaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.idleTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // idleTimer
            // 
            this.idleTimer.Enabled = true;
            this.idleTimer.Tick += new System.EventHandler(this.IdleTimer_Tick);
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 207);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaseForm";
            this.Text = "BaseForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BaseForm_FormClosed);
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer idleTimer;
    }
}