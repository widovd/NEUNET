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
            this.testButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.matrixImage1 = new Neunet.Images.Charts2D.MatrixImage();
            this.matrixImage2 = new Neunet.Images.Charts2D.MatrixImage();
            this.matrixImage3 = new Neunet.Images.Charts2D.MatrixImage();
            this.matrixImage4 = new Neunet.Images.Charts2D.MatrixImage();
            this.matrixImage5 = new Neunet.Images.Charts2D.MatrixImage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.left2Button = new System.Windows.Forms.Button();
            this.left1Button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.right2Button = new System.Windows.Forms.Button();
            this.right1Button = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.randomIndexButton = new System.Windows.Forms.Button();
            this.learnButton = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
            this.ysLabel = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.coefficientsImage = new Neunet.Images.CoefficientsImage();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(12, 41);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 0;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel1.Controls.Add(this.matrixImage1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.matrixImage2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.matrixImage3, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.matrixImage4, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.matrixImage5, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 263);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(837, 142);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // matrixImage1
            // 
            this.matrixImage1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.matrixImage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.matrixImage1.Location = new System.Drawing.Point(70, 35);
            this.matrixImage1.Margin = new System.Windows.Forms.Padding(0);
            this.matrixImage1.Name = "matrixImage1";
            this.matrixImage1.Size = new System.Drawing.Size(130, 104);
            this.matrixImage1.TabIndex = 0;
            // 
            // matrixImage2
            // 
            this.matrixImage2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.matrixImage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.matrixImage2.Location = new System.Drawing.Point(203, 35);
            this.matrixImage2.Margin = new System.Windows.Forms.Padding(0);
            this.matrixImage2.Name = "matrixImage2";
            this.matrixImage2.Size = new System.Drawing.Size(130, 104);
            this.matrixImage2.TabIndex = 1;
            // 
            // matrixImage3
            // 
            this.matrixImage3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.matrixImage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.matrixImage3.Location = new System.Drawing.Point(336, 35);
            this.matrixImage3.Margin = new System.Windows.Forms.Padding(0);
            this.matrixImage3.Name = "matrixImage3";
            this.matrixImage3.Size = new System.Drawing.Size(130, 104);
            this.matrixImage3.TabIndex = 2;
            // 
            // matrixImage4
            // 
            this.matrixImage4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.matrixImage4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.matrixImage4.Location = new System.Drawing.Point(469, 35);
            this.matrixImage4.Margin = new System.Windows.Forms.Padding(0);
            this.matrixImage4.Name = "matrixImage4";
            this.matrixImage4.Size = new System.Drawing.Size(130, 104);
            this.matrixImage4.TabIndex = 3;
            // 
            // matrixImage5
            // 
            this.matrixImage5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.matrixImage5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.matrixImage5.Location = new System.Drawing.Point(602, 35);
            this.matrixImage5.Margin = new System.Windows.Forms.Padding(0);
            this.matrixImage5.Name = "matrixImage5";
            this.matrixImage5.Size = new System.Drawing.Size(130, 104);
            this.matrixImage5.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(73, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(206, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 29);
            this.label2.TabIndex = 8;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(339, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 29);
            this.label3.TabIndex = 9;
            this.label3.Text = "label3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(472, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 29);
            this.label4.TabIndex = 10;
            this.label4.Text = "label4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(605, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 29);
            this.label5.TabIndex = 11;
            this.label5.Text = "label5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.left2Button);
            this.panel1.Controls.Add(this.left1Button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(6, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(58, 98);
            this.panel1.TabIndex = 12;
            // 
            // left2Button
            // 
            this.left2Button.Location = new System.Drawing.Point(0, 22);
            this.left2Button.Name = "left2Button";
            this.left2Button.Size = new System.Drawing.Size(58, 23);
            this.left2Button.TabIndex = 6;
            this.left2Button.Text = "<<";
            this.left2Button.UseVisualStyleBackColor = true;
            this.left2Button.Click += new System.EventHandler(this.Left2Button_Click);
            // 
            // left1Button
            // 
            this.left1Button.Location = new System.Drawing.Point(0, 0);
            this.left1Button.Name = "left1Button";
            this.left1Button.Size = new System.Drawing.Size(58, 23);
            this.left1Button.TabIndex = 5;
            this.left1Button.Text = "<";
            this.left1Button.UseVisualStyleBackColor = true;
            this.left1Button.Click += new System.EventHandler(this.Left1Button_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.right2Button);
            this.panel2.Controls.Add(this.right1Button);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(738, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(93, 98);
            this.panel2.TabIndex = 13;
            // 
            // right2Button
            // 
            this.right2Button.Location = new System.Drawing.Point(0, 22);
            this.right2Button.Name = "right2Button";
            this.right2Button.Size = new System.Drawing.Size(58, 23);
            this.right2Button.TabIndex = 7;
            this.right2Button.Text = ">>";
            this.right2Button.UseVisualStyleBackColor = true;
            this.right2Button.Click += new System.EventHandler(this.Right2Button_Click);
            // 
            // right1Button
            // 
            this.right1Button.Location = new System.Drawing.Point(0, 0);
            this.right1Button.Name = "right1Button";
            this.right1Button.Size = new System.Drawing.Size(58, 23);
            this.right1Button.TabIndex = 6;
            this.right1Button.Text = ">";
            this.right1Button.UseVisualStyleBackColor = true;
            this.right1Button.Click += new System.EventHandler(this.Right1Button_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.randomIndexButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(6, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(58, 23);
            this.panel3.TabIndex = 14;
            // 
            // randomIndexButton
            // 
            this.randomIndexButton.Location = new System.Drawing.Point(0, 0);
            this.randomIndexButton.Name = "randomIndexButton";
            this.randomIndexButton.Size = new System.Drawing.Size(58, 23);
            this.randomIndexButton.TabIndex = 0;
            this.randomIndexButton.Text = "Random";
            this.randomIndexButton.UseVisualStyleBackColor = true;
            this.randomIndexButton.Click += new System.EventHandler(this.RandomIndexButton_Click);
            // 
            // learnButton
            // 
            this.learnButton.Location = new System.Drawing.Point(12, 70);
            this.learnButton.Name = "learnButton";
            this.learnButton.Size = new System.Drawing.Size(75, 23);
            this.learnButton.TabIndex = 2;
            this.learnButton.Text = "Learn";
            this.learnButton.UseVisualStyleBackColor = true;
            this.learnButton.Click += new System.EventHandler(this.LearnButton_Click);
            // 
            // newButton
            // 
            this.newButton.Location = new System.Drawing.Point(12, 12);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(75, 23);
            this.newButton.TabIndex = 3;
            this.newButton.Text = "New";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // ysLabel
            // 
            this.ysLabel.AutoSize = true;
            this.ysLabel.Location = new System.Drawing.Point(122, 12);
            this.ysLabel.Name = "ysLabel";
            this.ysLabel.Size = new System.Drawing.Size(17, 13);
            this.ysLabel.TabIndex = 5;
            this.ysLabel.Text = "ys";
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(12, 99);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 6;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 405);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(837, 22);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(66, 17);
            this.statusLabel.Text = "statusLabel";
            // 
            // coefficientsImage
            // 
            this.coefficientsImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.coefficientsImage.Coefficients = null;
            this.coefficientsImage.Location = new System.Drawing.Point(125, 41);
            this.coefficientsImage.Margin = new System.Windows.Forms.Padding(0);
            this.coefficientsImage.Name = "coefficientsImage";
            this.coefficientsImage.Size = new System.Drawing.Size(676, 23);
            this.coefficientsImage.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(122, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "label6";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 427);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.coefficientsImage);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.ysLabel);
            this.Controls.Add(this.newButton);
            this.Controls.Add(this.learnButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestForm_FormClosed);
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Images.Charts2D.MatrixImage matrixImage1;
        private Images.Charts2D.MatrixImage matrixImage2;
        private Images.Charts2D.MatrixImage matrixImage3;
        private Images.Charts2D.MatrixImage matrixImage4;
        private Images.Charts2D.MatrixImage matrixImage5;
        private System.Windows.Forms.Button left1Button;
        private System.Windows.Forms.Button right1Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button learnButton;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button left2Button;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button right2Button;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button randomIndexButton;
        private System.Windows.Forms.Label ysLabel;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private Images.CoefficientsImage coefficientsImage;
        private System.Windows.Forms.Label label6;
    }
}

