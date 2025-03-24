namespace owoow.WinForms.Subforms
{
    partial class OverworldScanner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverworldScanner));
            L_PokemonPresent = new Label();
            CB_ViewSelect = new ComboBox();
            L_ViewSelect = new Label();
            L_PID = new Label();
            L_EC = new Label();
            TB_PID = new TextBox();
            TB_EC = new TextBox();
            TB_IVs = new TextBox();
            label1 = new Label();
            TB_Nature = new TextBox();
            label2 = new Label();
            TB_Gender = new TextBox();
            label3 = new Label();
            TB_Ability = new TextBox();
            label4 = new Label();
            L_Seed = new Label();
            TB_Seed = new TextBox();
            PB_MarkSprite = new PictureBox();
            PB_PokemonSprite = new PictureBox();
            TB_Height = new TextBox();
            label5 = new Label();
            L_Y = new Label();
            L_Z = new Label();
            L_X = new Label();
            L_Map = new Label();
            ((System.ComponentModel.ISupportInitialize)PB_MarkSprite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB_PokemonSprite).BeginInit();
            SuspendLayout();
            // 
            // L_PokemonPresent
            // 
            L_PokemonPresent.AutoSize = true;
            L_PokemonPresent.Location = new Point(8, 154);
            L_PokemonPresent.Name = "L_PokemonPresent";
            L_PokemonPresent.Size = new Size(115, 15);
            L_PokemonPresent.TabIndex = 2;
            L_PokemonPresent.Text = "Pokémon Present: w";
            // 
            // CB_ViewSelect
            // 
            CB_ViewSelect.FormattingEnabled = true;
            CB_ViewSelect.Location = new Point(87, 8);
            CB_ViewSelect.Name = "CB_ViewSelect";
            CB_ViewSelect.Size = new Size(148, 23);
            CB_ViewSelect.TabIndex = 0;
            CB_ViewSelect.Text = "wwwwwwwwwwww-10";
            CB_ViewSelect.SelectedIndexChanged += CB_ViewSelect_SelectedIndexChanged;
            // 
            // L_ViewSelect
            // 
            L_ViewSelect.AutoSize = true;
            L_ViewSelect.Location = new Point(8, 11);
            L_ViewSelect.Name = "L_ViewSelect";
            L_ViewSelect.Size = new Size(73, 15);
            L_ViewSelect.TabIndex = 1;
            L_ViewSelect.Text = "View Details:";
            // 
            // L_PID
            // 
            L_PID.AutoSize = true;
            L_PID.Location = new Point(265, 34);
            L_PID.Name = "L_PID";
            L_PID.Size = new Size(28, 15);
            L_PID.TabIndex = 5;
            L_PID.Text = "PID:";
            // 
            // L_EC
            // 
            L_EC.AutoSize = true;
            L_EC.Location = new Point(269, 58);
            L_EC.Name = "L_EC";
            L_EC.Size = new Size(24, 15);
            L_EC.TabIndex = 7;
            L_EC.Text = "EC:";
            // 
            // TB_PID
            // 
            TB_PID.CharacterCasing = CharacterCasing.Upper;
            TB_PID.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_PID.Location = new Point(299, 32);
            TB_PID.MaxLength = 16;
            TB_PID.Name = "TB_PID";
            TB_PID.ReadOnly = true;
            TB_PID.Size = new Size(62, 22);
            TB_PID.TabIndex = 6;
            TB_PID.TabStop = false;
            TB_PID.Text = "01234567";
            // 
            // TB_EC
            // 
            TB_EC.CharacterCasing = CharacterCasing.Upper;
            TB_EC.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_EC.Location = new Point(299, 56);
            TB_EC.MaxLength = 16;
            TB_EC.Name = "TB_EC";
            TB_EC.ReadOnly = true;
            TB_EC.Size = new Size(62, 22);
            TB_EC.TabIndex = 8;
            TB_EC.TabStop = false;
            TB_EC.Text = "01234567";
            // 
            // TB_IVs
            // 
            TB_IVs.CharacterCasing = CharacterCasing.Upper;
            TB_IVs.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_IVs.Location = new Point(299, 128);
            TB_IVs.MaxLength = 16;
            TB_IVs.Name = "TB_IVs";
            TB_IVs.ReadOnly = true;
            TB_IVs.Size = new Size(191, 22);
            TB_IVs.TabIndex = 16;
            TB_IVs.TabStop = false;
            TB_IVs.Text = "31/31/31/31/31/31";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(268, 130);
            label1.Name = "label1";
            label1.Size = new Size(25, 15);
            label1.TabIndex = 15;
            label1.Text = "IVs:";
            // 
            // TB_Nature
            // 
            TB_Nature.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Nature.Location = new Point(428, 82);
            TB_Nature.MaxLength = 16;
            TB_Nature.Name = "TB_Nature";
            TB_Nature.ReadOnly = true;
            TB_Nature.Size = new Size(62, 22);
            TB_Nature.TabIndex = 12;
            TB_Nature.TabStop = false;
            TB_Nature.Text = "Adamant";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(376, 85);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 11;
            label2.Text = "Nature:";
            // 
            // TB_Gender
            // 
            TB_Gender.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Gender.Location = new Point(299, 80);
            TB_Gender.MaxLength = 16;
            TB_Gender.Name = "TB_Gender";
            TB_Gender.ReadOnly = true;
            TB_Gender.Size = new Size(62, 22);
            TB_Gender.TabIndex = 10;
            TB_Gender.TabStop = false;
            TB_Gender.Text = "F";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(245, 82);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 9;
            label3.Text = "Gender:";
            // 
            // TB_Ability
            // 
            TB_Ability.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Ability.Location = new Point(299, 104);
            TB_Ability.MaxLength = 16;
            TB_Ability.Name = "TB_Ability";
            TB_Ability.ReadOnly = true;
            TB_Ability.Size = new Size(191, 22);
            TB_Ability.TabIndex = 14;
            TB_Ability.TabStop = false;
            TB_Ability.Text = "Compoundeyes";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(249, 106);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 13;
            label4.Text = "Ability:";
            // 
            // L_Seed
            // 
            L_Seed.AutoSize = true;
            L_Seed.Location = new Point(258, 11);
            L_Seed.Name = "L_Seed";
            L_Seed.Size = new Size(35, 15);
            L_Seed.TabIndex = 3;
            L_Seed.Text = "Seed:";
            // 
            // TB_Seed
            // 
            TB_Seed.CharacterCasing = CharacterCasing.Upper;
            TB_Seed.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Seed.Location = new Point(299, 8);
            TB_Seed.MaxLength = 16;
            TB_Seed.Name = "TB_Seed";
            TB_Seed.ReadOnly = true;
            TB_Seed.Size = new Size(62, 22);
            TB_Seed.TabIndex = 4;
            TB_Seed.TabStop = false;
            TB_Seed.Text = "01234567";
            // 
            // PB_MarkSprite
            // 
            PB_MarkSprite.Location = new Point(442, 24);
            PB_MarkSprite.Name = "PB_MarkSprite";
            PB_MarkSprite.Size = new Size(48, 48);
            PB_MarkSprite.SizeMode = PictureBoxSizeMode.CenterImage;
            PB_MarkSprite.TabIndex = 24;
            PB_MarkSprite.TabStop = false;
            // 
            // PB_PokemonSprite
            // 
            PB_PokemonSprite.Location = new Point(379, 8);
            PB_PokemonSprite.Name = "PB_PokemonSprite";
            PB_PokemonSprite.Size = new Size(64, 64);
            PB_PokemonSprite.SizeMode = PictureBoxSizeMode.CenterImage;
            PB_PokemonSprite.TabIndex = 23;
            PB_PokemonSprite.TabStop = false;
            // 
            // TB_Height
            // 
            TB_Height.CharacterCasing = CharacterCasing.Upper;
            TB_Height.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Height.Location = new Point(299, 152);
            TB_Height.MaxLength = 16;
            TB_Height.Name = "TB_Height";
            TB_Height.ReadOnly = true;
            TB_Height.Size = new Size(191, 22);
            TB_Height.TabIndex = 18;
            TB_Height.TabStop = false;
            TB_Height.Text = "255 (XXXL)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(247, 154);
            label5.Name = "label5";
            label5.Size = new Size(46, 15);
            label5.TabIndex = 17;
            label5.Text = "Height:";
            // 
            // L_Y
            // 
            L_Y.AutoSize = true;
            L_Y.Location = new Point(8, 66);
            L_Y.Name = "L_Y";
            L_Y.Size = new Size(29, 15);
            L_Y.TabIndex = 25;
            L_Y.Text = "Y: w";
            // 
            // L_Z
            // 
            L_Z.AutoSize = true;
            L_Z.Location = new Point(8, 82);
            L_Z.Name = "L_Z";
            L_Z.Size = new Size(29, 15);
            L_Z.TabIndex = 26;
            L_Z.Text = "Z: w";
            // 
            // L_X
            // 
            L_X.AutoSize = true;
            L_X.Location = new Point(8, 50);
            L_X.Name = "L_X";
            L_X.Size = new Size(29, 15);
            L_X.TabIndex = 27;
            L_X.Text = "X: w";
            // 
            // L_Map
            // 
            L_Map.AutoSize = true;
            L_Map.Location = new Point(8, 34);
            L_Map.Name = "L_Map";
            L_Map.Size = new Size(60, 15);
            L_Map.TabIndex = 28;
            L_Map.Text = "Map ID: w";
            // 
            // OverworldScanner
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(498, 183);
            Controls.Add(L_Map);
            Controls.Add(L_X);
            Controls.Add(L_Z);
            Controls.Add(L_Y);
            Controls.Add(label5);
            Controls.Add(TB_Height);
            Controls.Add(PB_MarkSprite);
            Controls.Add(PB_PokemonSprite);
            Controls.Add(TB_Ability);
            Controls.Add(label4);
            Controls.Add(TB_Gender);
            Controls.Add(label3);
            Controls.Add(TB_Nature);
            Controls.Add(label2);
            Controls.Add(TB_IVs);
            Controls.Add(label1);
            Controls.Add(TB_EC);
            Controls.Add(TB_PID);
            Controls.Add(TB_Seed);
            Controls.Add(L_EC);
            Controls.Add(L_PID);
            Controls.Add(L_Seed);
            Controls.Add(L_ViewSelect);
            Controls.Add(CB_ViewSelect);
            Controls.Add(L_PokemonPresent);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "OverworldScanner";
            Text = "OverworldScanner";
            FormClosing += OverworldScanner_FormClosing;
            ((System.ComponentModel.ISupportInitialize)PB_MarkSprite).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB_PokemonSprite).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label L_PokemonPresent;
        private ComboBox CB_ViewSelect;
        private Label L_ViewSelect;
        private Label L_PID;
        private Label L_EC;
        private TextBox TB_PID;
        private TextBox TB_EC;
        private TextBox TB_IVs;
        private Label label1;
        private TextBox TB_Nature;
        private Label label2;
        private TextBox TB_Gender;
        private Label label3;
        private TextBox TB_Ability;
        private Label label4;
        private Label L_Seed;
        private TextBox TB_Seed;
        private PictureBox PB_MarkSprite;
        private PictureBox PB_PokemonSprite;
        private TextBox TB_Height;
        private Label label5;
        private Label L_Y;
        private Label L_Z;
        private Label L_X;
        private Label L_Map;
    }
}
