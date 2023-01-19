namespace Trait_Editor
{
    partial class MainForm
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
            this.listTrees = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listTrees
            // 
            this.listTrees.FormattingEnabled = true;
            this.listTrees.ItemHeight = 15;
            this.listTrees.Location = new System.Drawing.Point(12, 12);
            this.listTrees.Name = "listTrees";
            this.listTrees.Size = new System.Drawing.Size(250, 1234);
            this.listTrees.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 1261);
            this.Controls.Add(this.listTrees);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox listTrees;
    }
}