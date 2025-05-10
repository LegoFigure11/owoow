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
            L_ExtraTimeLoadProfile = new Label();
            TB_ExtraTimeLoadProfile = new TextBox();
            L_ExtraTimeLoadGame = new Label();
            TB_ExtraTimeLoadGame = new TextBox();
            label1 = new Label();
            L_EnableWebhooks = new Label();
            CB_EnableWebhooks = new CheckBox();
            L_WebhookMessage = new Label();
            TB_WebhookMessage = new TextBox();
            TB_ResultURLs = new TextBox();
            L_ResultURLs = new Label();
            TB_ErrorURLs = new TextBox();
            L_ErrorURLs = new Label();
            B_TestWebhooks = new Button();
            TB_WebhookErrorMessage = new TextBox();
            L_WebhookErrorMessage = new Label();
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
            L_ExtraTimeCloseGame.TabIndex = 14;
            L_ExtraTimeCloseGame.Text = "Extra time to close the game:";
            // 
            // L_ExtraTimeReturnHome
            // 
            L_ExtraTimeReturnHome.AutoSize = true;
            L_ExtraTimeReturnHome.Location = new Point(12, 34);
            L_ExtraTimeReturnHome.Name = "L_ExtraTimeReturnHome";
            L_ExtraTimeReturnHome.Size = new Size(182, 15);
            L_ExtraTimeReturnHome.TabIndex = 13;
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
            TB_ExtraTimeCloseGame.TabIndex = 2;
            TB_ExtraTimeCloseGame.Text = "99999";
            TB_ExtraTimeCloseGame.TextChanged += TB_ExtraTimeCloseGame_TextChanged;
            TB_ExtraTimeCloseGame.KeyPress += KeyPress_AllowOnlyNumerical;
            TB_ExtraTimeCloseGame.Leave += TB_Leave;
            // 
            // TB_ExtraTimeReturnHome
            // 
            TB_ExtraTimeReturnHome.CharacterCasing = CharacterCasing.Upper;
            TB_ExtraTimeReturnHome.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_ExtraTimeReturnHome.Location = new Point(238, 32);
            TB_ExtraTimeReturnHome.MaxLength = 5;
            TB_ExtraTimeReturnHome.Name = "TB_ExtraTimeReturnHome";
            TB_ExtraTimeReturnHome.Size = new Size(42, 22);
            TB_ExtraTimeReturnHome.TabIndex = 1;
            TB_ExtraTimeReturnHome.Text = "99999";
            TB_ExtraTimeReturnHome.TextChanged += TB_ExtraTimeReturnHome_TextChanged;
            TB_ExtraTimeReturnHome.KeyPress += KeyPress_AllowOnlyNumerical;
            TB_ExtraTimeReturnHome.Leave += TB_Leave;
            // 
            // L_ExtraTimeLoadProfile
            // 
            L_ExtraTimeLoadProfile.AutoSize = true;
            L_ExtraTimeLoadProfile.Location = new Point(12, 86);
            L_ExtraTimeLoadProfile.Name = "L_ExtraTimeLoadProfile";
            L_ExtraTimeLoadProfile.Size = new Size(175, 15);
            L_ExtraTimeLoadProfile.TabIndex = 15;
            L_ExtraTimeLoadProfile.Text = "Extra time to load player profile:";
            // 
            // TB_ExtraTimeLoadProfile
            // 
            TB_ExtraTimeLoadProfile.CharacterCasing = CharacterCasing.Upper;
            TB_ExtraTimeLoadProfile.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_ExtraTimeLoadProfile.Location = new Point(238, 84);
            TB_ExtraTimeLoadProfile.MaxLength = 16;
            TB_ExtraTimeLoadProfile.Name = "TB_ExtraTimeLoadProfile";
            TB_ExtraTimeLoadProfile.Size = new Size(42, 22);
            TB_ExtraTimeLoadProfile.TabIndex = 3;
            TB_ExtraTimeLoadProfile.Text = "99999";
            TB_ExtraTimeLoadProfile.TextChanged += TB_ExtraTimeLoadProfile_TextChanged;
            TB_ExtraTimeLoadProfile.KeyPress += KeyPress_AllowOnlyNumerical;
            TB_ExtraTimeLoadProfile.Leave += TB_Leave;
            // 
            // L_ExtraTimeLoadGame
            // 
            L_ExtraTimeLoadGame.AutoSize = true;
            L_ExtraTimeLoadGame.Location = new Point(12, 110);
            L_ExtraTimeLoadGame.Name = "L_ExtraTimeLoadGame";
            L_ExtraTimeLoadGame.Size = new Size(156, 15);
            L_ExtraTimeLoadGame.TabIndex = 17;
            L_ExtraTimeLoadGame.Text = "Extra time to load the game:";
            // 
            // TB_ExtraTimeLoadGame
            // 
            TB_ExtraTimeLoadGame.CharacterCasing = CharacterCasing.Upper;
            TB_ExtraTimeLoadGame.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_ExtraTimeLoadGame.Location = new Point(238, 108);
            TB_ExtraTimeLoadGame.MaxLength = 16;
            TB_ExtraTimeLoadGame.Name = "TB_ExtraTimeLoadGame";
            TB_ExtraTimeLoadGame.Size = new Size(42, 22);
            TB_ExtraTimeLoadGame.TabIndex = 5;
            TB_ExtraTimeLoadGame.Text = "99999";
            TB_ExtraTimeLoadGame.TextChanged += TB_ExtraTimeLoadGame_TextChanged;
            TB_ExtraTimeLoadGame.KeyPress += KeyPress_AllowOnlyNumerical;
            TB_ExtraTimeLoadGame.Leave += TB_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 10);
            label1.Name = "label1";
            label1.Size = new Size(125, 15);
            label1.TabIndex = 12;
            label1.Text = "Avoid System Update?";
            // 
            // L_EnableWebhooks
            // 
            L_EnableWebhooks.AutoSize = true;
            L_EnableWebhooks.Location = new Point(12, 143);
            L_EnableWebhooks.Name = "L_EnableWebhooks";
            L_EnableWebhooks.Size = new Size(149, 15);
            L_EnableWebhooks.TabIndex = 18;
            L_EnableWebhooks.Text = "Enable Discord Webhooks?";
            // 
            // CB_EnableWebhooks
            // 
            CB_EnableWebhooks.AutoSize = true;
            CB_EnableWebhooks.Location = new Point(253, 144);
            CB_EnableWebhooks.Name = "CB_EnableWebhooks";
            CB_EnableWebhooks.Size = new Size(15, 14);
            CB_EnableWebhooks.TabIndex = 6;
            CB_EnableWebhooks.UseVisualStyleBackColor = true;
            CB_EnableWebhooks.CheckedChanged += CB_EnableWebhooks_CheckedChanged;
            // 
            // L_WebhookMessage
            // 
            L_WebhookMessage.AutoSize = true;
            L_WebhookMessage.Location = new Point(12, 164);
            L_WebhookMessage.Name = "L_WebhookMessage";
            L_WebhookMessage.Size = new Size(182, 15);
            L_WebhookMessage.TabIndex = 19;
            L_WebhookMessage.Text = "Result Found Webhook Message:";
            // 
            // TB_WebhookMessage
            // 
            TB_WebhookMessage.Location = new Point(12, 182);
            TB_WebhookMessage.Name = "TB_WebhookMessage";
            TB_WebhookMessage.Size = new Size(268, 23);
            TB_WebhookMessage.TabIndex = 7;
            TB_WebhookMessage.TextChanged += TB_WebhookMessage_TextChanged;
            // 
            // TB_ResultURLs
            // 
            TB_ResultURLs.Location = new Point(12, 270);
            TB_ResultURLs.Name = "TB_ResultURLs";
            TB_ResultURLs.Size = new Size(268, 23);
            TB_ResultURLs.TabIndex = 9;
            TB_ResultURLs.TextChanged += TB_ResultURLs_TextChanged;
            // 
            // L_ResultURLs
            // 
            L_ResultURLs.AutoSize = true;
            L_ResultURLs.Location = new Point(12, 252);
            L_ResultURLs.Name = "L_ResultURLs";
            L_ResultURLs.Size = new Size(120, 15);
            L_ResultURLs.TabIndex = 21;
            L_ResultURLs.Text = "Result Message URLs:";
            // 
            // TB_ErrorURLs
            // 
            TB_ErrorURLs.Location = new Point(12, 314);
            TB_ErrorURLs.Name = "TB_ErrorURLs";
            TB_ErrorURLs.Size = new Size(268, 23);
            TB_ErrorURLs.TabIndex = 10;
            TB_ErrorURLs.TextChanged += TB_ErrorURLs_TextChanged;
            // 
            // L_ErrorURLs
            // 
            L_ErrorURLs.AutoSize = true;
            L_ErrorURLs.Location = new Point(12, 296);
            L_ErrorURLs.Name = "L_ErrorURLs";
            L_ErrorURLs.Size = new Size(113, 15);
            L_ErrorURLs.TabIndex = 22;
            L_ErrorURLs.Text = "Error Message URLs:";
            // 
            // B_TestWebhooks
            // 
            B_TestWebhooks.Location = new Point(12, 339);
            B_TestWebhooks.Name = "B_TestWebhooks";
            B_TestWebhooks.Size = new Size(268, 25);
            B_TestWebhooks.TabIndex = 11;
            B_TestWebhooks.Text = "Test Webhooks";
            B_TestWebhooks.UseVisualStyleBackColor = true;
            B_TestWebhooks.Click += B_TestWebhooks_Click;
            // 
            // TB_WebhookErrorMessage
            // 
            TB_WebhookErrorMessage.Location = new Point(12, 226);
            TB_WebhookErrorMessage.Name = "TB_WebhookErrorMessage";
            TB_WebhookErrorMessage.Size = new Size(268, 23);
            TB_WebhookErrorMessage.TabIndex = 8;
            TB_WebhookErrorMessage.TextChanged += TB_WebhookErrorMessage_TextChanged;
            // 
            // L_WebhookErrorMessage
            // 
            L_WebhookErrorMessage.AutoSize = true;
            L_WebhookErrorMessage.Location = new Point(12, 208);
            L_WebhookErrorMessage.Name = "L_WebhookErrorMessage";
            L_WebhookErrorMessage.Size = new Size(138, 15);
            L_WebhookErrorMessage.TabIndex = 20;
            L_WebhookErrorMessage.Text = "Error Webhook Message:";
            // 
            // SeedResetSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(286, 368);
            Controls.Add(TB_WebhookErrorMessage);
            Controls.Add(L_WebhookErrorMessage);
            Controls.Add(B_TestWebhooks);
            Controls.Add(TB_ErrorURLs);
            Controls.Add(L_ErrorURLs);
            Controls.Add(TB_ResultURLs);
            Controls.Add(L_ResultURLs);
            Controls.Add(TB_WebhookMessage);
            Controls.Add(L_WebhookMessage);
            Controls.Add(L_EnableWebhooks);
            Controls.Add(CB_EnableWebhooks);
            Controls.Add(label1);
            Controls.Add(L_ExtraTimeLoadGame);
            Controls.Add(TB_ExtraTimeLoadGame);
            Controls.Add(L_ExtraTimeLoadProfile);
            Controls.Add(TB_ExtraTimeLoadProfile);
            Controls.Add(L_ExtraTimeCloseGame);
            Controls.Add(L_ExtraTimeReturnHome);
            Controls.Add(TB_ExtraTimeCloseGame);
            Controls.Add(TB_ExtraTimeReturnHome);
            Controls.Add(CB_AvoidSystemUpdate);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SeedResetSettings";
            Text = "Seed Reset Settings";
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
        private Label L_ExtraTimeLoadProfile;
        public TextBox TB_ExtraTimeLoadProfile;
        private Label L_ExtraTimeLoadGame;
        public TextBox TB_ExtraTimeLoadGame;
        private Label label1;
        private Label L_EnableWebhooks;
        private CheckBox CB_EnableWebhooks;
        private Label L_WebhookMessage;
        private TextBox TB_WebhookMessage;
        private TextBox TB_ResultURLs;
        private Label L_ResultURLs;
        private TextBox TB_ErrorURLs;
        private Label L_ErrorURLs;
        private Button B_TestWebhooks;
        private TextBox TB_WebhookErrorMessage;
        private Label L_WebhookErrorMessage;
    }
}