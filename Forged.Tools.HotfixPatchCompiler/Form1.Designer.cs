namespace Forged.Tools.HotfixPatchCompiler
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.numVersion = new System.Windows.Forms.NumericUpDown();
            this.txtRanges = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numVersion)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "New Version Number";
            // 
            // numVersion
            // 
            this.numVersion.Location = new System.Drawing.Point(137, 7);
            this.numVersion.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numVersion.Name = "numVersion";
            this.numVersion.Size = new System.Drawing.Size(169, 23);
            this.numVersion.TabIndex = 1;
            // 
            // txtRanges
            // 
            this.txtRanges.Location = new System.Drawing.Point(137, 36);
            this.txtRanges.Multiline = true;
            this.txtRanges.Name = "txtRanges";
            this.txtRanges.Size = new System.Drawing.Size(169, 181);
            this.txtRanges.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "VerifiedBuild Ranges";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 60);
            this.label3.TabIndex = 4;
            this.label3.Text = "Examples:\r\n100001\r\n110000-120000\r\n150000+";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(121, 223);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 256);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRanges);
            this.Controls.Add(this.numVersion);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Patch Builder";
            ((System.ComponentModel.ISupportInitialize)(this.numVersion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private NumericUpDown numVersion;
        private TextBox txtRanges;
        private Label label2;
        private Label label3;
        private Button button1;
    }
}