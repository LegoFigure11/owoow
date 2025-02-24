namespace owoow.WinForms.Subforms
{
    partial class SeedResetSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeedResetSettings));
            CB_AvoidSystemUpdate = new CheckBox();
            L_ExtraTimeCloseGame = new Label();
            L_ExtraTimeReturnHome = new Label();
            TB_ExtraTimeCloseGame = new TextBox();
            TB_ExtraTimeReturnHome = new TextBox();
            L_ExtraTimeCheckDLC = new Label();
            L_ExtraTimeLoadProfile = new Label();
            TB_ExtraTimeCheckDLC = new TextBox();
            TB_ExtraTimeLoadProfile = new TextBox();
            L_ExtraTimeLoadGame = new Label();
            TB_ExtraTimeLoadGame = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // CB_AvoidSystemUpdate
            // 
            CB_AvoidSystemUpdate.AutoSize = true;
            CB_AvoidSystemUpdate.Location = new Point(253, 11);
            CB_AvoidSystemUpdate.Name = "CB_AvoidSystemUpdate";
            CB_AvoidSystemUpdate.Size = new Size(15, 14);
            CB_AvoidSystemUpdate.TabIndex = 0;
            CB_AvoidSystemUpdate.UseVisualStyleBackColor = true;
            CB_AvoidSystemUpdate.CheckedChanged += CB_AvoidSystemUpdate_CheckedChanged;
            // 
            // L_ExtraTimeCloseGame
            // 
            L_ExtraTimeCloseGame.AutoSize = true;
            L_ExtraTimeCloseGame.Location = new Point(12, 58);
            L_ExtraTimeCloseGame.Name = "L_ExtraTimeCloseGame";
            L_ExtraTimeCloseGame.Size = new Size(160, 15);
            L_ExtraTimeCloseGame.TabIndex = 11;
            L_ExtraTimeCloseGame.Text = "Extra time to close the game:";
            // 
            // L_ExtraTimeReturnHome
            // 
            L_ExtraTimeReturnHome.AutoSize = true;
            L_ExtraTimeReturnHome.Location = new Point(12, 34);
            L_ExtraTimeReturnHome.Name = "L_ExtraTimeReturnHome";
            L_ExtraTimeReturnHome.Size = new Size(182, 15);
            L_ExtraTimeReturnHome.TabIndex = 10;
            L_ExtraTimeReturnHome.Text = "Extra time to open HOME Menu: ";
            // 
            // TB_ExtraTimeCloseGame
            // 
            TB_ExtraTimeCloseGame.CharacterCasing = CharacterCasing.Upper;
            TB_ExtraTimeCloseGame.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_ExtraTimeCloseGame.Location = new Point(238, 56);
            TB_ExtraTimeCloseGame.MaxLength = 16;
            TB_ExtraTimeCloseGame.Name = "TB_ExtraTimeCloseGame";
            TB_ExtraTimeCloseGame.Size = new Size(42, 22);
            TB_ExtraTimeCloseGame.TabIndex = 9;
            TB_ExtraTimeCloseGame.Text = "99999";
            TB_ExtraTimeCloseGame.TextChanged += TB_ExtraTimeCloseGame_TextChanged;
            TB_ExtraTimeCloseGame.KeyPress += KeyPress_AllowOnlyNumerical;
            // 
            // TB_ExtraTimeReturnHome
            // 
            TB_ExtraTimeReturnHome.CharacterCasing = CharacterCasing.Upper;
            TB_ExtraTimeReturnHome.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_ExtraTimeReturnHome.Location = new Point(238, 32);
            TB_ExtraTimeReturnHome.MaxLength = 5;
            TB_ExtraTimeReturnHome.Name = "TB_ExtraTimeReturnHome";
            TB_ExtraTimeReturnHome.Size = new Size(42, 22);
            TB_ExtraTimeReturnHome.TabIndex = 8;
            TB_ExtraTimeReturnHome.Text = "99999";
            TB_ExtraTimeReturnHome.TextChanged += TB_ExtraTimeReturnHome_TextChanged;
            TB_ExtraTimeReturnHome.KeyPress += KeyPress_AllowOnlyNumerical;
            // 
            // L_ExtraTimeCheckDLC
            // 
            L_ExtraTimeCheckDLC.AutoSize = true;
            L_ExtraTimeCheckDLC.Location = new Point(12, 110);
            L_ExtraTimeCheckDLC.Name = "L_ExtraTimeCheckDLC";
            L_ExtraTimeCheckDLC.Size = new Size(206, 15);
            L_ExtraTimeCheckDLC.TabIndex = 15;
            L_ExtraTimeCheckDLC.Text = "Extra time to check if DLC is available:";
            // 
            // L_ExtraTimeLoadProfile
            // 
            L_ExtraTimeLoadProfile.AutoSize = true;
            L_ExtraTimeLoadProfile.Location = new Point(12, 86);
            L_ExtraTimeLoadProfile.Name = "L_ExtraTimeLoadProfile";
            L_ExtraTimeLoadProfile.Size = new Size(175, 15);
            L_ExtraTimeLoadProfile.TabIndex = 14;
            L_ExtraTimeLoadProfile.Text = "Extra time to load player profile:";
            // 
            // TB_ExtraTimeCheckDLC
            // 
            TB_ExtraTimeCheckDLC.CharacterCasing = CharacterCasing.Upper;
            TB_ExtraTimeCheckDLC.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_ExtraTimeCheckDLC.Location = new Point(238, 108);
            TB_ExtraTimeCheckDLC.MaxLength = 16;
            TB_ExtraTimeCheckDLC.Name = "TB_ExtraTimeCheckDLC";
            TB_ExtraTimeCheckDLC.Size = new Size(42, 22);
            TB_ExtraTimeCheckDLC.TabIndex = 13;
            TB_ExtraTimeCheckDLC.Text = "99999";
            TB_ExtraTimeCheckDLC.TextChanged += TB_ExtraTimeCheckDLC_TextChanged;
            TB_ExtraTimeCheckDLC.KeyPress += KeyPress_AllowOnlyNumerical;
            // 
            // TB_ExtraTimeLoadProfile
            // 
            TB_ExtraTimeLoadProfile.CharacterCasing = CharacterCasing.Upper;
            TB_ExtraTimeLoadProfile.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_ExtraTimeLoadProfile.Location = new Point(238, 84);
            TB_ExtraTimeLoadProfile.MaxLength = 16;
            TB_ExtraTimeLoadProfile.Name = "TB_ExtraTimeLoadProfile";
            TB_ExtraTimeLoadProfile.Size = new Size(42, 22);
            TB_ExtraTimeLoadProfile.TabIndex = 12;
            TB_ExtraTimeLoadProfile.Text = "99999";
            TB_ExtraTimeLoadProfile.TextChanged += TB_ExtraTimeLoadProfile_TextChanged;
            TB_ExtraTimeLoadProfile.KeyPress += KeyPress_AllowOnlyNumerical;
            // 
            // L_ExtraTimeLoadGame
            // 
            L_ExtraTimeLoadGame.AutoSize = true;
            L_ExtraTimeLoadGame.Location = new Point(12, 134);
            L_ExtraTimeLoadGame.Name = "L_ExtraTimeLoadGame";
            L_ExtraTimeLoadGame.Size = new Size(156, 15);
            L_ExtraTimeLoadGame.TabIndex = 17;
            L_ExtraTimeLoadGame.Text = "Extra time to load the game:";
            // 
            // TB_ExtraTimeLoadGame
            // 
            TB_ExtraTimeLoadGame.CharacterCasing = CharacterCasing.Upper;
            TB_ExtraTimeLoadGame.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_ExtraTimeLoadGame.Location = new Point(238, 132);
            TB_ExtraTimeLoadGame.MaxLength = 16;
            TB_ExtraTimeLoadGame.Name = "TB_ExtraTimeLoadGame";
            TB_ExtraTimeLoadGame.Size = new Size(42, 22);
            TB_ExtraTimeLoadGame.TabIndex = 16;
            TB_ExtraTimeLoadGame.Text = "99999";
            TB_ExtraTimeLoadGame.TextChanged += TB_ExtraTimeLoadGame_TextChanged;
            TB_ExtraTimeLoadGame.KeyPress += KeyPress_AllowOnlyNumerical;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 10);
            label1.Name = "label1";
            label1.Size = new Size(125, 15);
            label1.TabIndex = 18;
            label1.Text = "Avoid System Update?";
            // 
            // SeedResetSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(286, 160);
            Controls.Add(label1);
            Controls.Add(L_ExtraTimeLoadGame);
            Controls.Add(TB_ExtraTimeLoadGame);
            Controls.Add(L_ExtraTimeCheckDLC);
            Controls.Add(L_ExtraTimeLoadProfile);
            Controls.Add(TB_ExtraTimeCheckDLC);
            Controls.Add(TB_ExtraTimeLoadProfile);
            Controls.Add(L_ExtraTimeCloseGame);
            Controls.Add(L_ExtraTimeReturnHome);
            Controls.Add(TB_ExtraTimeCloseGame);
            Controls.Add(TB_ExtraTimeReturnHome);
            Controls.Add(CB_AvoidSystemUpdate);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SeedResetSettings";
            Text = "SeedResetSettings";
            FormClosing += SeedResetSettings_FormClosing;
            Load += SeedResetSettings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox CB_AvoidSystemUpdate;
        private Label L_ExtraTimeCloseGame;
        private Label L_ExtraTimeReturnHome;
        public TextBox TB_ExtraTimeCloseGame;
        public TextBox TB_ExtraTimeReturnHome;
        private Label L_ExtraTimeCheckDLC;
        private Label L_ExtraTimeLoadProfile;
        public TextBox TB_ExtraTimeCheckDLC;
        public TextBox TB_ExtraTimeLoadProfile;
        private Label L_ExtraTimeLoadGame;
        public TextBox TB_ExtraTimeLoadGame;
        private Label label1;
    }
}