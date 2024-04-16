namespace owoow.WinForms
{
    partial class MainWindow
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
            GB_SeedControlsContainer = new GroupBox();
            GB_Seed = new GroupBox();
            L_Seed1 = new Label();
            L_Seed0 = new Label();
            TB_Seed1 = new TextBox();
            TB_Seed0 = new TextBox();
            groupBox1 = new GroupBox();
            B_CopyToInitial = new Button();
            label1 = new Label();
            TB_Status = new TextBox();
            L_CurrentS0 = new Label();
            TB_CurrentS1 = new TextBox();
            L_Status = new Label();
            TB_CurrentS0 = new TextBox();
            TB_CurrentAdvances = new TextBox();
            L_CurrentAdvances = new Label();
            B_Disconnect = new Button();
            B_Connect = new Button();
            L_SwitchIP = new Label();
            TB_SwitchIP = new TextBox();
            GB_SAVInfo = new GroupBox();
            L_MarkCharm = new Label();
            L_ShinyCharm = new Label();
            CB_MarkCharm = new CheckBox();
            L_SID = new Label();
            L_TID = new Label();
            TB_SID = new TextBox();
            CB_ShinyCharm = new CheckBox();
            TB_TID = new TextBox();
            GB_SeedControlsContainer.SuspendLayout();
            GB_Seed.SuspendLayout();
            groupBox1.SuspendLayout();
            GB_SAVInfo.SuspendLayout();
            SuspendLayout();
            // 
            // GB_SeedControlsContainer
            // 
            GB_SeedControlsContainer.Controls.Add(GB_Seed);
            GB_SeedControlsContainer.Controls.Add(groupBox1);
            GB_SeedControlsContainer.Controls.Add(GB_SAVInfo);
            GB_SeedControlsContainer.Location = new Point(0, 5);
            GB_SeedControlsContainer.Name = "GB_SeedControlsContainer";
            GB_SeedControlsContainer.RightToLeft = RightToLeft.No;
            GB_SeedControlsContainer.Size = new Size(788, 295);
            GB_SeedControlsContainer.TabIndex = 11;
            GB_SeedControlsContainer.TabStop = false;
            // 
            // GB_Seed
            // 
            GB_Seed.Controls.Add(L_Seed1);
            GB_Seed.Controls.Add(L_Seed0);
            GB_Seed.Controls.Add(TB_Seed1);
            GB_Seed.Controls.Add(TB_Seed0);
            GB_Seed.Location = new Point(0, 0);
            GB_Seed.Name = "GB_Seed";
            GB_Seed.RightToLeft = RightToLeft.No;
            GB_Seed.Size = new Size(200, 60);
            GB_Seed.TabIndex = 0;
            GB_Seed.TabStop = false;
            // 
            // L_Seed1
            // 
            L_Seed1.AutoSize = true;
            L_Seed1.Location = new Point(11, 35);
            L_Seed1.Name = "L_Seed1";
            L_Seed1.Size = new Size(49, 15);
            L_Seed1.TabIndex = 7;
            L_Seed1.Text = "Seed[1]:";
            // 
            // L_Seed0
            // 
            L_Seed0.AutoSize = true;
            L_Seed0.Location = new Point(11, 11);
            L_Seed0.Name = "L_Seed0";
            L_Seed0.Size = new Size(49, 15);
            L_Seed0.TabIndex = 6;
            L_Seed0.Text = "Seed[0]:";
            // 
            // TB_Seed1
            // 
            TB_Seed1.CharacterCasing = CharacterCasing.Upper;
            TB_Seed1.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Seed1.Location = new Point(71, 33);
            TB_Seed1.MaxLength = 16;
            TB_Seed1.Name = "TB_Seed1";
            TB_Seed1.Size = new Size(118, 22);
            TB_Seed1.TabIndex = 5;
            TB_Seed1.Text = "0123456789ABCDEF";
            TB_Seed1.KeyPress += KeyPress_AllowOnlyHex;
            // 
            // TB_Seed0
            // 
            TB_Seed0.CharacterCasing = CharacterCasing.Upper;
            TB_Seed0.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Seed0.Location = new Point(71, 9);
            TB_Seed0.MaxLength = 16;
            TB_Seed0.Name = "TB_Seed0";
            TB_Seed0.Size = new Size(118, 22);
            TB_Seed0.TabIndex = 4;
            TB_Seed0.Text = "0123456789ABCDEF";
            TB_Seed0.KeyPress += KeyPress_AllowOnlyHex;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(B_CopyToInitial);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(TB_Status);
            groupBox1.Controls.Add(L_CurrentS0);
            groupBox1.Controls.Add(TB_CurrentS1);
            groupBox1.Controls.Add(L_Status);
            groupBox1.Controls.Add(TB_CurrentS0);
            groupBox1.Controls.Add(TB_CurrentAdvances);
            groupBox1.Controls.Add(L_CurrentAdvances);
            groupBox1.Controls.Add(B_Disconnect);
            groupBox1.Controls.Add(B_Connect);
            groupBox1.Controls.Add(L_SwitchIP);
            groupBox1.Controls.Add(TB_SwitchIP);
            groupBox1.Location = new Point(0, 49);
            groupBox1.Margin = new Padding(3, 0, 3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.RightToLeft = RightToLeft.No;
            groupBox1.Size = new Size(200, 184);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            // 
            // B_CopyToInitial
            // 
            B_CopyToInitial.Enabled = false;
            B_CopyToInitial.Location = new Point(11, 154);
            B_CopyToInitial.Name = "B_CopyToInitial";
            B_CopyToInitial.Size = new Size(178, 25);
            B_CopyToInitial.TabIndex = 19;
            B_CopyToInitial.Text = "Copy to Initial";
            B_CopyToInitial.UseVisualStyleBackColor = true;
            B_CopyToInitial.Click += B_CopyToInitial_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 132);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 11;
            label1.Text = "Seed[1]:";
            // 
            // TB_Status
            // 
            TB_Status.BackColor = SystemColors.Control;
            TB_Status.BorderStyle = BorderStyle.None;
            TB_Status.Location = new Point(56, 64);
            TB_Status.Name = "TB_Status";
            TB_Status.ReadOnly = true;
            TB_Status.RightToLeft = RightToLeft.No;
            TB_Status.Size = new Size(132, 16);
            TB_Status.TabIndex = 18;
            TB_Status.TabStop = false;
            TB_Status.Text = "wwwwwwwwwwwwww";
            TB_Status.TextAlign = HorizontalAlignment.Right;
            // 
            // L_CurrentS0
            // 
            L_CurrentS0.AutoSize = true;
            L_CurrentS0.Location = new Point(11, 108);
            L_CurrentS0.Name = "L_CurrentS0";
            L_CurrentS0.Size = new Size(49, 15);
            L_CurrentS0.TabIndex = 10;
            L_CurrentS0.Text = "Seed[0]:";
            // 
            // TB_CurrentS1
            // 
            TB_CurrentS1.CharacterCasing = CharacterCasing.Upper;
            TB_CurrentS1.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_CurrentS1.Location = new Point(71, 130);
            TB_CurrentS1.MaxLength = 16;
            TB_CurrentS1.Name = "TB_CurrentS1";
            TB_CurrentS1.ReadOnly = true;
            TB_CurrentS1.Size = new Size(118, 22);
            TB_CurrentS1.TabIndex = 9;
            TB_CurrentS1.Text = "0123456789ABCDEF";
            // 
            // L_Status
            // 
            L_Status.AutoSize = true;
            L_Status.Location = new Point(11, 64);
            L_Status.Name = "L_Status";
            L_Status.Size = new Size(42, 15);
            L_Status.TabIndex = 17;
            L_Status.Text = "Status:";
            // 
            // TB_CurrentS0
            // 
            TB_CurrentS0.CharacterCasing = CharacterCasing.Upper;
            TB_CurrentS0.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_CurrentS0.Location = new Point(71, 106);
            TB_CurrentS0.MaxLength = 16;
            TB_CurrentS0.Name = "TB_CurrentS0";
            TB_CurrentS0.ReadOnly = true;
            TB_CurrentS0.Size = new Size(118, 22);
            TB_CurrentS0.TabIndex = 8;
            TB_CurrentS0.Text = "0123456789ABCDEF";
            // 
            // TB_CurrentAdvances
            // 
            TB_CurrentAdvances.CharacterCasing = CharacterCasing.Lower;
            TB_CurrentAdvances.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_CurrentAdvances.Location = new Point(78, 82);
            TB_CurrentAdvances.MaxLength = 15;
            TB_CurrentAdvances.Name = "TB_CurrentAdvances";
            TB_CurrentAdvances.ReadOnly = true;
            TB_CurrentAdvances.Size = new Size(111, 22);
            TB_CurrentAdvances.TabIndex = 16;
            TB_CurrentAdvances.TabStop = false;
            TB_CurrentAdvances.Text = "123,456,789,012";
            TB_CurrentAdvances.TextAlign = HorizontalAlignment.Right;
            // 
            // L_CurrentAdvances
            // 
            L_CurrentAdvances.AutoSize = true;
            L_CurrentAdvances.Location = new Point(11, 87);
            L_CurrentAdvances.Name = "L_CurrentAdvances";
            L_CurrentAdvances.Size = new Size(61, 15);
            L_CurrentAdvances.TabIndex = 15;
            L_CurrentAdvances.Text = "Advances:";
            // 
            // B_Disconnect
            // 
            B_Disconnect.Enabled = false;
            B_Disconnect.Location = new Point(100, 36);
            B_Disconnect.Name = "B_Disconnect";
            B_Disconnect.Size = new Size(89, 25);
            B_Disconnect.TabIndex = 14;
            B_Disconnect.Text = "Disconnect";
            B_Disconnect.UseVisualStyleBackColor = true;
            B_Disconnect.Click += B_Disconnect_Click;
            // 
            // B_Connect
            // 
            B_Connect.Location = new Point(11, 36);
            B_Connect.Name = "B_Connect";
            B_Connect.Size = new Size(89, 25);
            B_Connect.TabIndex = 13;
            B_Connect.Text = "Connect";
            B_Connect.UseVisualStyleBackColor = true;
            B_Connect.Click += B_Connect_Click;
            // 
            // L_SwitchIP
            // 
            L_SwitchIP.AutoSize = true;
            L_SwitchIP.Location = new Point(11, 17);
            L_SwitchIP.Name = "L_SwitchIP";
            L_SwitchIP.Size = new Size(58, 15);
            L_SwitchIP.TabIndex = 12;
            L_SwitchIP.Text = "Switch IP:";
            // 
            // TB_SwitchIP
            // 
            TB_SwitchIP.CharacterCasing = CharacterCasing.Lower;
            TB_SwitchIP.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_SwitchIP.Location = new Point(78, 12);
            TB_SwitchIP.MaxLength = 15;
            TB_SwitchIP.Name = "TB_SwitchIP";
            TB_SwitchIP.Size = new Size(111, 22);
            TB_SwitchIP.TabIndex = 11;
            TB_SwitchIP.Text = "123.123.123.123";
            // 
            // GB_SAVInfo
            // 
            GB_SAVInfo.Controls.Add(L_MarkCharm);
            GB_SAVInfo.Controls.Add(L_ShinyCharm);
            GB_SAVInfo.Controls.Add(CB_MarkCharm);
            GB_SAVInfo.Controls.Add(L_SID);
            GB_SAVInfo.Controls.Add(L_TID);
            GB_SAVInfo.Controls.Add(TB_SID);
            GB_SAVInfo.Controls.Add(CB_ShinyCharm);
            GB_SAVInfo.Controls.Add(TB_TID);
            GB_SAVInfo.Location = new Point(0, 223);
            GB_SAVInfo.Name = "GB_SAVInfo";
            GB_SAVInfo.Size = new Size(200, 72);
            GB_SAVInfo.TabIndex = 2;
            GB_SAVInfo.TabStop = false;
            // 
            // L_MarkCharm
            // 
            L_MarkCharm.AutoSize = true;
            L_MarkCharm.Location = new Point(11, 42);
            L_MarkCharm.Name = "L_MarkCharm";
            L_MarkCharm.Size = new Size(78, 15);
            L_MarkCharm.TabIndex = 22;
            L_MarkCharm.Text = "Mark Charm?";
            // 
            // L_ShinyCharm
            // 
            L_ShinyCharm.AutoSize = true;
            L_ShinyCharm.Location = new Point(11, 17);
            L_ShinyCharm.Name = "L_ShinyCharm";
            L_ShinyCharm.Size = new Size(80, 15);
            L_ShinyCharm.TabIndex = 21;
            L_ShinyCharm.Text = "Shiny Charm?";
            // 
            // CB_MarkCharm
            // 
            CB_MarkCharm.AutoSize = true;
            CB_MarkCharm.Location = new Point(94, 43);
            CB_MarkCharm.Name = "CB_MarkCharm";
            CB_MarkCharm.Size = new Size(15, 14);
            CB_MarkCharm.TabIndex = 10;
            CB_MarkCharm.UseVisualStyleBackColor = true;
            // 
            // L_SID
            // 
            L_SID.AutoSize = true;
            L_SID.Location = new Point(115, 42);
            L_SID.Name = "L_SID";
            L_SID.Size = new Size(27, 15);
            L_SID.TabIndex = 20;
            L_SID.Text = "SID:";
            // 
            // L_TID
            // 
            L_TID.AutoSize = true;
            L_TID.Location = new Point(115, 17);
            L_TID.Name = "L_TID";
            L_TID.Size = new Size(27, 15);
            L_TID.TabIndex = 19;
            L_TID.Text = "TID:";
            // 
            // TB_SID
            // 
            TB_SID.CharacterCasing = CharacterCasing.Upper;
            TB_SID.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_SID.Location = new Point(148, 40);
            TB_SID.MaxLength = 5;
            TB_SID.Name = "TB_SID";
            TB_SID.Size = new Size(41, 22);
            TB_SID.TabIndex = 9;
            TB_SID.Text = "54321";
            // 
            // CB_ShinyCharm
            // 
            CB_ShinyCharm.AutoSize = true;
            CB_ShinyCharm.Location = new Point(94, 18);
            CB_ShinyCharm.Name = "CB_ShinyCharm";
            CB_ShinyCharm.Size = new Size(15, 14);
            CB_ShinyCharm.TabIndex = 2;
            CB_ShinyCharm.UseVisualStyleBackColor = true;
            // 
            // TB_TID
            // 
            TB_TID.CharacterCasing = CharacterCasing.Upper;
            TB_TID.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_TID.Location = new Point(148, 15);
            TB_TID.MaxLength = 5;
            TB_TID.Name = "TB_TID";
            TB_TID.Size = new Size(41, 22);
            TB_TID.TabIndex = 8;
            TB_TID.Text = "12345";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(GB_SeedControlsContainer);
            Name = "MainWindow";
            RightToLeft = RightToLeft.No;
            Text = "owoow (´・ω・`)";
            Load += MainWindow_Load;
            GB_SeedControlsContainer.ResumeLayout(false);
            GB_Seed.ResumeLayout(false);
            GB_Seed.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            GB_SAVInfo.ResumeLayout(false);
            GB_SAVInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private GroupBox GB_SeedControlsContainer;
        private GroupBox GB_Seed;
        private Label L_Seed1;
        private Label L_Seed0;
        private TextBox TB_Seed1;
        private TextBox TB_Seed0;
        private GroupBox groupBox1;
        private TextBox TB_CurrentAdvances;
        private Label L_CurrentAdvances;
        private Button B_Disconnect;
        private Button B_Connect;
        private Label L_SwitchIP;
        private TextBox TB_SwitchIP;
        private Label L_Status;
        private TextBox TB_Status;
        private CheckBox CB_ShinyCharm;
        private GroupBox GB_SAVInfo;
        private TextBox TB_TID;
        private Label L_SID;
        private Label L_TID;
        private CheckBox CB_MarkCharm;
        private TextBox TB_SID;
        private Label L_MarkCharm;
        private Label L_ShinyCharm;
        private Button B_CopyToInitial;
        private Label label1;
        private Label L_CurrentS0;
        private TextBox TB_CurrentS1;
        private TextBox TB_CurrentS0;
    }
}
