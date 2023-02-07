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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtOverrideDesc1 = new System.Windows.Forms.TextBox();
            this.txtOverrideDesc2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtOverrideSubtext1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numSpellId2 = new System.Windows.Forms.NumericUpDown();
            this.txtOverrideSubtext2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtOverrideName1 = new System.Windows.Forms.TextBox();
            this.numSpellId1 = new System.Windows.Forms.NumericUpDown();
            this.lblSpellName2 = new System.Windows.Forms.Label();
            this.txtOverrideName2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSpellName1 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblVisibleSpellName1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numOverrideSpellId2 = new System.Windows.Forms.NumericUpDown();
            this.lblVisibleSpellName2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numOverrideSpellId1 = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.numVisibleSpellId1 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.lblOverrideSpellName2 = new System.Windows.Forms.Label();
            this.numVisibleSpellId2 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.lblOverrideSpellName1 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblTraitId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.grpNodeInfo.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpellId2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpellId1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOverrideSpellId2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOverrideSpellId1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVisibleSpellId1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVisibleSpellId2)).BeginInit();
            this.tabPage2.SuspendLayout();
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
            this.grpNodeInfo.Controls.Add(this.tabControl1);
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
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 81);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(300, 1167);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.txtOverrideDesc1);
            this.tabPage1.Controls.Add(this.txtOverrideDesc2);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label24);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.txtOverrideSubtext1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.numSpellId2);
            this.tabPage1.Controls.Add(this.txtOverrideSubtext2);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label23);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.txtOverrideName1);
            this.tabPage1.Controls.Add(this.numSpellId1);
            this.tabPage1.Controls.Add(this.lblSpellName2);
            this.tabPage1.Controls.Add(this.txtOverrideName2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.lblSpellName1);
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.lblVisibleSpellName1);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.numOverrideSpellId2);
            this.tabPage1.Controls.Add(this.lblVisibleSpellName2);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.numOverrideSpellId1);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.numVisibleSpellId1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.lblOverrideSpellName2);
            this.tabPage1.Controls.Add(this.numVisibleSpellId2);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.lblOverrideSpellName1);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(292, 1139);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Entries";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(114, 434);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 21);
            this.label12.TabIndex = 42;
            this.label12.Text = "Entry 2";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(114, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 21);
            this.label11.TabIndex = 23;
            this.label11.Text = "Entry 1";
            // 
            // txtOverrideDesc1
            // 
            this.txtOverrideDesc1.Location = new System.Drawing.Point(82, 287);
            this.txtOverrideDesc1.Multiline = true;
            this.txtOverrideDesc1.Name = "txtOverrideDesc1";
            this.txtOverrideDesc1.Size = new System.Drawing.Size(202, 132);
            this.txtOverrideDesc1.TabIndex = 22;
            // 
            // txtOverrideDesc2
            // 
            this.txtOverrideDesc2.Location = new System.Drawing.Point(82, 715);
            this.txtOverrideDesc2.Multiline = true;
            this.txtOverrideDesc2.Name = "txtOverrideDesc2";
            this.txtOverrideDesc2.Size = new System.Drawing.Size(202, 132);
            this.txtOverrideDesc2.TabIndex = 41;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 283);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 30);
            this.label10.TabIndex = 21;
            this.label10.Text = "Override\r\nDescription:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 480);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(49, 15);
            this.label24.TabIndex = 24;
            this.label24.Text = "Spell ID:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 711);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 30);
            this.label13.TabIndex = 40;
            this.label13.Text = "Override\r\nDescription:";
            // 
            // txtOverrideSubtext1
            // 
            this.txtOverrideSubtext1.Location = new System.Drawing.Point(64, 253);
            this.txtOverrideSubtext1.Name = "txtOverrideSubtext1";
            this.txtOverrideSubtext1.Size = new System.Drawing.Size(220, 23);
            this.txtOverrideSubtext1.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Spell:";
            // 
            // numSpellId2
            // 
            this.numSpellId2.Location = new System.Drawing.Point(64, 476);
            this.numSpellId2.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numSpellId2.Name = "numSpellId2";
            this.numSpellId2.Size = new System.Drawing.Size(220, 23);
            this.numSpellId2.TabIndex = 25;
            // 
            // txtOverrideSubtext2
            // 
            this.txtOverrideSubtext2.Location = new System.Drawing.Point(64, 681);
            this.txtOverrideSubtext2.Name = "txtOverrideSubtext2";
            this.txtOverrideSubtext2.Size = new System.Drawing.Size(220, 23);
            this.txtOverrideSubtext2.TabIndex = 39;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 249);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 30);
            this.label9.TabIndex = 19;
            this.label9.Text = "Override\r\nSubtext:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Spell ID:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 460);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(35, 15);
            this.label23.TabIndex = 26;
            this.label23.Text = "Spell:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 677);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 30);
            this.label14.TabIndex = 38;
            this.label14.Text = "Override\r\nSubtext:";
            // 
            // txtOverrideName1
            // 
            this.txtOverrideName1.Location = new System.Drawing.Point(64, 219);
            this.txtOverrideName1.Name = "txtOverrideName1";
            this.txtOverrideName1.Size = new System.Drawing.Size(220, 23);
            this.txtOverrideName1.TabIndex = 18;
            // 
            // numSpellId1
            // 
            this.numSpellId1.Location = new System.Drawing.Point(64, 48);
            this.numSpellId1.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numSpellId1.Name = "numSpellId1";
            this.numSpellId1.Size = new System.Drawing.Size(220, 23);
            this.numSpellId1.TabIndex = 6;
            // 
            // lblSpellName2
            // 
            this.lblSpellName2.AutoSize = true;
            this.lblSpellName2.Location = new System.Drawing.Point(47, 460);
            this.lblSpellName2.Name = "lblSpellName2";
            this.lblSpellName2.Size = new System.Drawing.Size(0, 15);
            this.lblSpellName2.TabIndex = 27;
            // 
            // txtOverrideName2
            // 
            this.txtOverrideName2.Location = new System.Drawing.Point(64, 647);
            this.txtOverrideName2.Name = "txtOverrideName2";
            this.txtOverrideName2.Size = new System.Drawing.Size(220, 23);
            this.txtOverrideName2.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 215);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 30);
            this.label4.TabIndex = 17;
            this.label4.Text = "Override\r\nName:";
            // 
            // lblSpellName1
            // 
            this.lblSpellName1.AutoSize = true;
            this.lblSpellName1.Location = new System.Drawing.Point(47, 32);
            this.lblSpellName1.Name = "lblSpellName1";
            this.lblSpellName1.Size = new System.Drawing.Size(0, 15);
            this.lblSpellName1.TabIndex = 8;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 538);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(52, 30);
            this.label21.TabIndex = 28;
            this.label21.Text = "Override\r\nSpell ID:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 643);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 30);
            this.label15.TabIndex = 36;
            this.label15.Text = "Override\r\nName:";
            // 
            // lblVisibleSpellName1
            // 
            this.lblVisibleSpellName1.AutoSize = true;
            this.lblVisibleSpellName1.Location = new System.Drawing.Point(53, 156);
            this.lblVisibleSpellName1.Name = "lblVisibleSpellName1";
            this.lblVisibleSpellName1.Size = new System.Drawing.Size(0, 15);
            this.lblVisibleSpellName1.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 30);
            this.label6.TabIndex = 9;
            this.label6.Text = "Override\r\nSpell ID:";
            // 
            // numOverrideSpellId2
            // 
            this.numOverrideSpellId2.Location = new System.Drawing.Point(64, 542);
            this.numOverrideSpellId2.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numOverrideSpellId2.Name = "numOverrideSpellId2";
            this.numOverrideSpellId2.Size = new System.Drawing.Size(220, 23);
            this.numOverrideSpellId2.TabIndex = 29;
            // 
            // lblVisibleSpellName2
            // 
            this.lblVisibleSpellName2.AutoSize = true;
            this.lblVisibleSpellName2.Location = new System.Drawing.Point(53, 584);
            this.lblVisibleSpellName2.Name = "lblVisibleSpellName2";
            this.lblVisibleSpellName2.Size = new System.Drawing.Size(0, 15);
            this.lblVisibleSpellName2.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 30);
            this.label7.TabIndex = 15;
            this.label7.Text = "Visible\r\nSpell:";
            // 
            // numOverrideSpellId1
            // 
            this.numOverrideSpellId1.Location = new System.Drawing.Point(64, 114);
            this.numOverrideSpellId1.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numOverrideSpellId1.Name = "numOverrideSpellId1";
            this.numOverrideSpellId1.Size = new System.Drawing.Size(220, 23);
            this.numOverrideSpellId1.TabIndex = 10;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 506);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 30);
            this.label20.TabIndex = 30;
            this.label20.Text = "Override\r\nSpell:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 575);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 30);
            this.label17.TabIndex = 34;
            this.label17.Text = "Visible\r\nSpell:";
            // 
            // numVisibleSpellId1
            // 
            this.numVisibleSpellId1.Location = new System.Drawing.Point(64, 184);
            this.numVisibleSpellId1.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numVisibleSpellId1.Name = "numVisibleSpellId1";
            this.numVisibleSpellId1.Size = new System.Drawing.Size(220, 23);
            this.numVisibleSpellId1.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 30);
            this.label5.TabIndex = 11;
            this.label5.Text = "Override\r\nSpell:";
            // 
            // lblOverrideSpellName2
            // 
            this.lblOverrideSpellName2.AutoSize = true;
            this.lblOverrideSpellName2.Location = new System.Drawing.Point(64, 515);
            this.lblOverrideSpellName2.Name = "lblOverrideSpellName2";
            this.lblOverrideSpellName2.Size = new System.Drawing.Size(0, 15);
            this.lblOverrideSpellName2.TabIndex = 31;
            // 
            // numVisibleSpellId2
            // 
            this.numVisibleSpellId2.Location = new System.Drawing.Point(64, 612);
            this.numVisibleSpellId2.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numVisibleSpellId2.Name = "numVisibleSpellId2";
            this.numVisibleSpellId2.Size = new System.Drawing.Size(220, 23);
            this.numVisibleSpellId2.TabIndex = 33;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 180);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 30);
            this.label8.TabIndex = 13;
            this.label8.Text = "Visible\r\nSpell ID:";
            // 
            // lblOverrideSpellName1
            // 
            this.lblOverrideSpellName1.AutoSize = true;
            this.lblOverrideSpellName1.Location = new System.Drawing.Point(64, 87);
            this.lblOverrideSpellName1.Name = "lblOverrideSpellName1";
            this.lblOverrideSpellName1.Size = new System.Drawing.Size(0, 15);
            this.lblOverrideSpellName1.TabIndex = 12;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 608);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 30);
            this.label18.TabIndex = 32;
            this.label18.Text = "Visible\r\nSpell ID:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(292, 1139);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Currency/Cost";
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
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 13);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 15);
            this.label16.TabIndex = 0;
            this.label16.Text = "Currency:";
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpellId2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpellId1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOverrideSpellId2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOverrideSpellId1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVisibleSpellId1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVisibleSpellId2)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
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
        private NumericUpDown numSpellId1;
        private Label label2;
        private Label lblSpellName1;
        private Label label3;
        private Label lblOverrideSpellName1;
        private Label label5;
        private NumericUpDown numOverrideSpellId1;
        private Label label6;
        private Label lblVisibleSpellName1;
        private Label label7;
        private NumericUpDown numVisibleSpellId1;
        private Label label8;
        private TextBox txtOverrideDesc1;
        private Label label10;
        private TextBox txtOverrideSubtext1;
        private Label label9;
        private TextBox txtOverrideName1;
        private Label label4;
        private Label label12;
        private TextBox txtOverrideDesc2;
        private Label label13;
        private TextBox txtOverrideSubtext2;
        private Label label14;
        private TextBox txtOverrideName2;
        private Label label15;
        private Label lblVisibleSpellName2;
        private Label label17;
        private NumericUpDown numVisibleSpellId2;
        private Label label18;
        private Label lblOverrideSpellName2;
        private Label label20;
        private NumericUpDown numOverrideSpellId2;
        private Label label21;
        private Label lblSpellName2;
        private Label label23;
        private NumericUpDown numSpellId2;
        private Label label24;
        private Label label11;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label16;
    }
}