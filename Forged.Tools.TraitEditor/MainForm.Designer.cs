namespace Forged.Tools.TraitEditor
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
            this.grpNodeInfo = new System.Windows.Forms.GroupBox();
            this.numSpellId = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblTraitId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.grpNodeInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpellId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // listTrees
            // 
            this.listTrees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listTrees.DisplayMember = "Description";
            this.listTrees.FormattingEnabled = true;
            this.listTrees.ItemHeight = 15;
            this.listTrees.Location = new System.Drawing.Point(12, 12);
            this.listTrees.Name = "listTrees";
            this.listTrees.Size = new System.Drawing.Size(250, 1249);
            this.listTrees.TabIndex = 2;
            this.listTrees.ValueMember = "Description";
            // 
            // grpNodeInfo
            // 
            this.grpNodeInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpNodeInfo.BackColor = System.Drawing.SystemColors.Desktop;
            this.grpNodeInfo.Controls.Add(this.numSpellId);
            this.grpNodeInfo.Controls.Add(this.label2);
            this.grpNodeInfo.Controls.Add(this.button3);
            this.grpNodeInfo.Controls.Add(this.button2);
            this.grpNodeInfo.Controls.Add(this.button1);
            this.grpNodeInfo.Controls.Add(this.lblTraitId);
            this.grpNodeInfo.Controls.Add(this.label1);
            this.grpNodeInfo.ForeColor = System.Drawing.SystemColors.Control;
            this.grpNodeInfo.Location = new System.Drawing.Point(268, 12);
            this.grpNodeInfo.Name = "grpNodeInfo";
            this.grpNodeInfo.Size = new System.Drawing.Size(300, 1248);
            this.grpNodeInfo.TabIndex = 3;
            this.grpNodeInfo.TabStop = false;
            this.grpNodeInfo.Text = "Node Info";
            // 
            // numSpellId
            // 
            this.numSpellId.Location = new System.Drawing.Point(61, 81);
            this.numSpellId.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numSpellId.Name = "numSpellId";
            this.numSpellId.Size = new System.Drawing.Size(120, 23);
            this.numSpellId.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Spell ID:";
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button3.Location = new System.Drawing.Point(209, 22);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Location = new System.Drawing.Point(111, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(15, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lblTraitId
            // 
            this.lblTraitId.AutoSize = true;
            this.lblTraitId.Location = new System.Drawing.Point(33, 56);
            this.lblTraitId.Name = "lblTraitId";
            this.lblTraitId.Size = new System.Drawing.Size(0, 15);
            this.lblTraitId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(2088, 1272);
            this.Controls.Add(this.grpNodeInfo);
            this.Controls.Add(this.listTrees);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Name = "MainForm";
            this.Text = "Trait Editor";
            this.grpNodeInfo.ResumeLayout(false);
            this.grpNodeInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpellId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox listTrees;
        private GroupBox grpNodeInfo;
        private Label lblTraitId;
        private Label label1;
        private Button button3;
        private Button button2;
        private Button button1;
        private NumericUpDown numSpellId;
        private Label label2;
        private FileSystemWatcher fileSystemWatcher1;
    }
}