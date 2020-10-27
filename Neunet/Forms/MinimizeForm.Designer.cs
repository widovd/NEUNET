namespace Neunet.Forms
{
    partial class MinimizeForm
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
            this.testSteepestDescentButton = new System.Windows.Forms.Button();
            this.testConjugateGradientButton = new System.Windows.Forms.Button();
            this.randomizePointButton = new System.Windows.Forms.Button();
            this.randomizeFunctionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // testSteepestDescentButton
            // 
            this.testSteepestDescentButton.Location = new System.Drawing.Point(12, 70);
            this.testSteepestDescentButton.Name = "testSteepestDescentButton";
            this.testSteepestDescentButton.Size = new System.Drawing.Size(125, 23);
            this.testSteepestDescentButton.TabIndex = 0;
            this.testSteepestDescentButton.Text = "Steepest descent";
            this.testSteepestDescentButton.UseVisualStyleBackColor = true;
            this.testSteepestDescentButton.Click += new System.EventHandler(this.TestSteepestDescentButton_Click);
            // 
            // testConjugateGradientButton
            // 
            this.testConjugateGradientButton.Location = new System.Drawing.Point(12, 99);
            this.testConjugateGradientButton.Name = "testConjugateGradientButton";
            this.testConjugateGradientButton.Size = new System.Drawing.Size(125, 23);
            this.testConjugateGradientButton.TabIndex = 1;
            this.testConjugateGradientButton.Text = "Conjugate gradient";
            this.testConjugateGradientButton.UseVisualStyleBackColor = true;
            this.testConjugateGradientButton.Click += new System.EventHandler(this.TestConjugateGradientButton_Click);
            // 
            // randomizePointButton
            // 
            this.randomizePointButton.Location = new System.Drawing.Point(12, 41);
            this.randomizePointButton.Name = "randomizePointButton";
            this.randomizePointButton.Size = new System.Drawing.Size(125, 23);
            this.randomizePointButton.TabIndex = 2;
            this.randomizePointButton.Text = "Randomize point";
            this.randomizePointButton.UseVisualStyleBackColor = true;
            this.randomizePointButton.Click += new System.EventHandler(this.RandomizePointButton_Click);
            // 
            // randomizeFunctionButton
            // 
            this.randomizeFunctionButton.Location = new System.Drawing.Point(12, 12);
            this.randomizeFunctionButton.Name = "randomizeFunctionButton";
            this.randomizeFunctionButton.Size = new System.Drawing.Size(125, 23);
            this.randomizeFunctionButton.TabIndex = 3;
            this.randomizeFunctionButton.Text = "Randomize function";
            this.randomizeFunctionButton.UseVisualStyleBackColor = true;
            this.randomizeFunctionButton.Click += new System.EventHandler(this.RandomizeFunctionButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 168);
            this.Controls.Add(this.randomizeFunctionButton);
            this.Controls.Add(this.randomizePointButton);
            this.Controls.Add(this.testConjugateGradientButton);
            this.Controls.Add(this.testSteepestDescentButton);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button testSteepestDescentButton;
        private System.Windows.Forms.Button testConjugateGradientButton;
        private System.Windows.Forms.Button randomizePointButton;
        private System.Windows.Forms.Button randomizeFunctionButton;
    }
}

