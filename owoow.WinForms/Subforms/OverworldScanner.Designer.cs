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
            L_MonX = new Label();
            TB_MonX = new TextBox();
            L_MonY = new Label();
            TB_MonY = new TextBox();
            L_MonZ = new Label();
            TB_MonZ = new TextBox();
            L_Z = new Label();
            TB_Z = new TextBox();
            L_Y = new Label();
            TB_Y = new TextBox();
            L_X = new Label();
            TB_X = new TextBox();
            L_Map = new Label();
            TB_Map = new TextBox();
            TB_Distance = new TextBox();
            L_Distance = new Label();
            ((System.ComponentModel.ISupportInitialize)PB_MarkSprite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB_PokemonSprite).BeginInit();
            SuspendLayout();
            // 
            // L_PokemonPresent
            // 
            L_PokemonPresent.AutoSize = true;
            L_PokemonPresent.Location = new Point(8, 34);
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
            L_PID.Location = new Point(278, 34);
            L_PID.Name = "L_PID";
            L_PID.Size = new Size(28, 15);
            L_PID.TabIndex = 5;
            L_PID.Text = "PID:";
            // 
            // L_EC
            // 
            L_EC.AutoSize = true;
            L_EC.Location = new Point(282, 58);
            L_EC.Name = "L_EC";
            L_EC.Size = new Size(24, 15);
            L_EC.TabIndex = 7;
            L_EC.Text = "EC:";
            // 
            // TB_PID
            // 
            TB_PID.CharacterCasing = CharacterCasing.Upper;
            TB_PID.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_PID.Location = new Point(312, 32);
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
            TB_EC.Location = new Point(312, 56);
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
            TB_IVs.Location = new Point(312, 128);
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
            label1.Location = new Point(281, 130);
            label1.Name = "label1";
            label1.Size = new Size(25, 15);
            label1.TabIndex = 15;
            label1.Text = "IVs:";
            // 
            // TB_Nature
            // 
            TB_Nature.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Nature.Location = new Point(441, 82);
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
            label2.Location = new Point(389, 85);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 11;
            label2.Text = "Nature:";
            // 
            // TB_Gender
            // 
            TB_Gender.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Gender.Location = new Point(312, 80);
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
            label3.Location = new Point(258, 82);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 9;
            label3.Text = "Gender:";
            // 
            // TB_Ability
            // 
            TB_Ability.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Ability.Location = new Point(312, 104);
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
            label4.Location = new Point(262, 106);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 13;
            label4.Text = "Ability:";
            // 
            // L_Seed
            // 
            L_Seed.AutoSize = true;
            L_Seed.Location = new Point(271, 11);
            L_Seed.Name = "L_Seed";
            L_Seed.Size = new Size(35, 15);
            L_Seed.TabIndex = 3;
            L_Seed.Text = "Seed:";
            // 
            // TB_Seed
            // 
            TB_Seed.CharacterCasing = CharacterCasing.Upper;
            TB_Seed.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Seed.Location = new Point(312, 8);
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
            PB_MarkSprite.Location = new Point(455, 24);
            PB_MarkSprite.Name = "PB_MarkSprite";
            PB_MarkSprite.Size = new Size(48, 48);
            PB_MarkSprite.SizeMode = PictureBoxSizeMode.CenterImage;
            PB_MarkSprite.TabIndex = 24;
            PB_MarkSprite.TabStop = false;
            // 
            // PB_PokemonSprite
            // 
            PB_PokemonSprite.Location = new Point(392, 8);
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
            TB_Height.Location = new Point(312, 152);
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
            label5.Location = new Point(260, 154);
            label5.Name = "label5";
            label5.Size = new Size(46, 15);
            label5.TabIndex = 17;
            label5.Text = "Height:";
            // 
            // L_MonX
            // 
            L_MonX.AutoSize = true;
            L_MonX.Location = new Point(289, 178);
            L_MonX.Name = "L_MonX";
            L_MonX.Size = new Size(17, 15);
            L_MonX.TabIndex = 29;
            L_MonX.Text = "X:";
            // 
            // TB_MonX
            // 
            TB_MonX.CharacterCasing = CharacterCasing.Upper;
            TB_MonX.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_MonX.Location = new Point(312, 176);
            TB_MonX.MaxLength = 16;
            TB_MonX.Name = "TB_MonX";
            TB_MonX.ReadOnly = true;
            TB_MonX.Size = new Size(191, 22);
            TB_MonX.TabIndex = 30;
            TB_MonX.TabStop = false;
            TB_MonX.Text = "12345.678901234567890";
            // 
            // L_MonY
            // 
            L_MonY.AutoSize = true;
            L_MonY.Location = new Point(289, 202);
            L_MonY.Name = "L_MonY";
            L_MonY.Size = new Size(17, 15);
            L_MonY.TabIndex = 31;
            L_MonY.Text = "Y:";
            // 
            // TB_MonY
            // 
            TB_MonY.CharacterCasing = CharacterCasing.Upper;
            TB_MonY.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_MonY.Location = new Point(312, 200);
            TB_MonY.MaxLength = 16;
            TB_MonY.Name = "TB_MonY";
            TB_MonY.ReadOnly = true;
            TB_MonY.Size = new Size(191, 22);
            TB_MonY.TabIndex = 32;
            TB_MonY.TabStop = false;
            TB_MonY.Text = "12345.678901234567890";
            // 
            // L_MonZ
            // 
            L_MonZ.AutoSize = true;
            L_MonZ.Location = new Point(289, 226);
            L_MonZ.Name = "L_MonZ";
            L_MonZ.Size = new Size(17, 15);
            L_MonZ.TabIndex = 33;
            L_MonZ.Text = "Z:";
            // 
            // TB_MonZ
            // 
            TB_MonZ.CharacterCasing = CharacterCasing.Upper;
            TB_MonZ.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_MonZ.Location = new Point(312, 224);
            TB_MonZ.MaxLength = 16;
            TB_MonZ.Name = "TB_MonZ";
            TB_MonZ.ReadOnly = true;
            TB_MonZ.Size = new Size(191, 22);
            TB_MonZ.TabIndex = 34;
            TB_MonZ.TabStop = false;
            TB_MonZ.Text = "12345.678901234567890";
            // 
            // L_Z
            // 
            L_Z.AutoSize = true;
            L_Z.Location = new Point(4, 226);
            L_Z.Name = "L_Z";
            L_Z.Size = new Size(52, 15);
            L_Z.TabIndex = 41;
            L_Z.Text = "Player Z:";
            // 
            // TB_Z
            // 
            TB_Z.CharacterCasing = CharacterCasing.Upper;
            TB_Z.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Z.Location = new Point(62, 224);
            TB_Z.MaxLength = 16;
            TB_Z.Name = "TB_Z";
            TB_Z.ReadOnly = true;
            TB_Z.Size = new Size(173, 22);
            TB_Z.TabIndex = 42;
            TB_Z.TabStop = false;
            TB_Z.Text = "123456789";
            // 
            // L_Y
            // 
            L_Y.AutoSize = true;
            L_Y.Location = new Point(4, 202);
            L_Y.Name = "L_Y";
            L_Y.Size = new Size(52, 15);
            L_Y.TabIndex = 39;
            L_Y.Text = "Player Y:";
            // 
            // TB_Y
            // 
            TB_Y.CharacterCasing = CharacterCasing.Upper;
            TB_Y.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Y.Location = new Point(62, 200);
            TB_Y.MaxLength = 16;
            TB_Y.Name = "TB_Y";
            TB_Y.ReadOnly = true;
            TB_Y.Size = new Size(173, 22);
            TB_Y.TabIndex = 40;
            TB_Y.TabStop = false;
            TB_Y.Text = "123456789";
            // 
            // L_X
            // 
            L_X.AutoSize = true;
            L_X.Location = new Point(4, 178);
            L_X.Name = "L_X";
            L_X.Size = new Size(52, 15);
            L_X.TabIndex = 37;
            L_X.Text = "Player X:";
            // 
            // TB_X
            // 
            TB_X.CharacterCasing = CharacterCasing.Upper;
            TB_X.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_X.Location = new Point(62, 176);
            TB_X.MaxLength = 16;
            TB_X.Name = "TB_X";
            TB_X.ReadOnly = true;
            TB_X.Size = new Size(173, 22);
            TB_X.TabIndex = 38;
            TB_X.TabStop = false;
            TB_X.Text = "123456789";
            // 
            // L_Map
            // 
            L_Map.AutoSize = true;
            L_Map.Location = new Point(22, 130);
            L_Map.Name = "L_Map";
            L_Map.Size = new Size(34, 15);
            L_Map.TabIndex = 35;
            L_Map.Text = "Map:";
            // 
            // TB_Map
            // 
            TB_Map.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Map.Location = new Point(62, 128);
            TB_Map.MaxLength = 16;
            TB_Map.Name = "TB_Map";
            TB_Map.ReadOnly = true;
            TB_Map.Size = new Size(173, 22);
            TB_Map.TabIndex = 43;
            TB_Map.TabStop = false;
            TB_Map.Text = "0123456789ABCDEF";
            // 
            // TB_Distance
            // 
            TB_Distance.CharacterCasing = CharacterCasing.Upper;
            TB_Distance.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Distance.Location = new Point(62, 152);
            TB_Distance.MaxLength = 16;
            TB_Distance.Name = "TB_Distance";
            TB_Distance.ReadOnly = true;
            TB_Distance.Size = new Size(173, 22);
            TB_Distance.TabIndex = 44;
            TB_Distance.TabStop = false;
            TB_Distance.Text = "0123456789ABCDEF";
            // 
            // L_Distance
            // 
            L_Distance.AutoSize = true;
            L_Distance.Location = new Point(1, 154);
            L_Distance.Name = "L_Distance";
            L_Distance.Size = new Size(55, 15);
            L_Distance.TabIndex = 45;
            L_Distance.Text = "Distance:";
            // 
            // OverworldScanner
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(510, 252);
            Controls.Add(L_Distance);
            Controls.Add(TB_Distance);
            Controls.Add(TB_Map);
            Controls.Add(L_Z);
            Controls.Add(TB_Z);
            Controls.Add(L_Y);
            Controls.Add(TB_Y);
            Controls.Add(L_X);
            Controls.Add(TB_X);
            Controls.Add(L_Map);
            Controls.Add(L_MonZ);
            Controls.Add(TB_MonZ);
            Controls.Add(L_MonY);
            Controls.Add(TB_MonY);
            Controls.Add(L_MonX);
            Controls.Add(TB_MonX);
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
            Text = "Overworld Scanner";
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
        private Label L_MonX;
        private TextBox TB_MonX;
        private Label L_MonY;
        private TextBox TB_MonY;
        private Label L_MonZ;
        private TextBox TB_MonZ;
        private Label L_Z;
        private TextBox TB_Z;
        private Label L_Y;
        private TextBox TB_Y;
        private Label L_X;
        private TextBox TB_X;
        private Label L_Map;
        private TextBox TB_Map;
        private TextBox TB_Distance;
        private Label L_Distance;
    }
}
