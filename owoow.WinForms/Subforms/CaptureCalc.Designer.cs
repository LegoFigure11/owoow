namespace owoow.WinForms.Subforms
{
    partial class CaptureCalc
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaptureCalc));
            L_Seed1 = new Label();
            L_Seed0 = new Label();
            TB_Seed1 = new TextBox();
            TB_Seed0 = new TextBox();
            TB_AdvancesIncrease = new TextBox();
            B_CopyToInitial = new Button();
            L_CurrentS1 = new Label();
            L_CurrentS0 = new Label();
            TB_CurrentS1 = new TextBox();
            TB_CurrentS0 = new TextBox();
            TB_CurrentAdvances = new TextBox();
            L_CurrentAdvances = new Label();
            GB_MyPokemon = new GroupBox();
            L_Active_Level = new Label();
            NUD_Active_Level = new NumericUpDown();
            L_Active_Gender = new Label();
            CB_Active_Gender = new ComboBox();
            L_Active_Species = new Label();
            CB_Active_Species = new ComboBox();
            GB_WildPokemon = new GroupBox();
            L_Wild_Form = new Label();
            NUD_Wild_Form = new NumericUpDown();
            L_Wild_Weight = new Label();
            NUD_Wild_Weight = new NumericUpDown();
            L_Wild_Rate = new Label();
            NUD_Wild_Rate = new NumericUpDown();
            L_Wild_Level = new Label();
            NUD_Wild_Level = new NumericUpDown();
            L_Wild_Status = new Label();
            CB_Wild_Status = new ComboBox();
            L_Wild_CurHP = new Label();
            NUD_Wild_CurHP = new NumericUpDown();
            L_Wild_MaxHP = new Label();
            NUD_Wild_MaxHP = new NumericUpDown();
            L_Wild_Gender = new Label();
            CB_Wild_Gender = new ComboBox();
            L_Wild_Species = new Label();
            CB_Wild_Species = new ComboBox();
            GB_Modifiers = new GroupBox();
            CB_8thBadge = new CheckBox();
            CB_Charm = new CheckBox();
            L_ZukanCaught = new Label();
            L_Turns = new Label();
            L_Ball = new Label();
            NUD_ZukanCaught = new NumericUpDown();
            CB_Ball = new ComboBox();
            NUD_Turns = new NumericUpDown();
            CB_First = new CheckBox();
            CB_Dusk = new CheckBox();
            CB_Registered = new CheckBox();
            CB_Surfing = new CheckBox();
            CB_Fishing = new CheckBox();
            B_ReadParty = new Button();
            button2 = new Button();
            NUD_PartySlot = new NumericUpDown();
            B_Search = new Button();
            L_TargetSuccess = new Label();
            CB_TargetSuccess = new ComboBox();
            L_Capture_Plus = new Label();
            L_Capture_Initial = new Label();
            TB_Capture_Advances = new TextBox();
            TB_Capture_Initial = new TextBox();
            DGV_Results = new DataGridView();
            advancesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            successDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            criticalDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            shakesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            seed0DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            seed1DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            CaptureResultsSource = new BindingSource(components);
            L_TargetCrit = new Label();
            CB_TargetCrit = new ComboBox();
            L_ShakesMin = new Label();
            NUD_ShakesMax = new NumericUpDown();
            NUD_ShakesMin = new NumericUpDown();
            L_ShakesMax = new Label();
            GB_MyPokemon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_Active_Level).BeginInit();
            GB_WildPokemon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_Wild_Form).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Wild_Weight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Wild_Rate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Wild_Level).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Wild_CurHP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Wild_MaxHP).BeginInit();
            GB_Modifiers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_ZukanCaught).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Turns).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_PartySlot).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CaptureResultsSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_ShakesMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_ShakesMin).BeginInit();
            SuspendLayout();
            // 
            // L_Seed1
            // 
            L_Seed1.AutoSize = true;
            L_Seed1.Location = new Point(12, 33);
            L_Seed1.Name = "L_Seed1";
            L_Seed1.Size = new Size(49, 15);
            L_Seed1.TabIndex = 11;
            L_Seed1.Text = "Seed[1]:";
            // 
            // L_Seed0
            // 
            L_Seed0.AutoSize = true;
            L_Seed0.Location = new Point(12, 9);
            L_Seed0.Name = "L_Seed0";
            L_Seed0.Size = new Size(49, 15);
            L_Seed0.TabIndex = 10;
            L_Seed0.Text = "Seed[0]:";
            // 
            // TB_Seed1
            // 
            TB_Seed1.CharacterCasing = CharacterCasing.Upper;
            TB_Seed1.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Seed1.Location = new Point(89, 31);
            TB_Seed1.MaxLength = 16;
            TB_Seed1.Name = "TB_Seed1";
            TB_Seed1.Size = new Size(118, 22);
            TB_Seed1.TabIndex = 9;
            TB_Seed1.Text = "0123456789ABCDEF";
            // 
            // TB_Seed0
            // 
            TB_Seed0.CharacterCasing = CharacterCasing.Upper;
            TB_Seed0.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Seed0.Location = new Point(89, 7);
            TB_Seed0.MaxLength = 16;
            TB_Seed0.Name = "TB_Seed0";
            TB_Seed0.Size = new Size(118, 22);
            TB_Seed0.TabIndex = 8;
            TB_Seed0.Text = "0123456789ABCDEF";
            // 
            // TB_AdvancesIncrease
            // 
            TB_AdvancesIncrease.CharacterCasing = CharacterCasing.Lower;
            TB_AdvancesIncrease.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_AdvancesIncrease.Location = new Point(160, 59);
            TB_AdvancesIncrease.MaxLength = 15;
            TB_AdvancesIncrease.Name = "TB_AdvancesIncrease";
            TB_AdvancesIncrease.ReadOnly = true;
            TB_AdvancesIncrease.Size = new Size(47, 22);
            TB_AdvancesIncrease.TabIndex = 29;
            TB_AdvancesIncrease.TabStop = false;
            TB_AdvancesIncrease.Text = "12,345";
            TB_AdvancesIncrease.TextAlign = HorizontalAlignment.Right;
            // 
            // B_CopyToInitial
            // 
            B_CopyToInitial.Location = new Point(12, 131);
            B_CopyToInitial.Name = "B_CopyToInitial";
            B_CopyToInitial.Size = new Size(196, 25);
            B_CopyToInitial.TabIndex = 22;
            B_CopyToInitial.Text = "Update Seeds";
            B_CopyToInitial.UseVisualStyleBackColor = true;
            B_CopyToInitial.Click += B_CopyToInitial_Click;
            // 
            // L_CurrentS1
            // 
            L_CurrentS1.AutoSize = true;
            L_CurrentS1.Location = new Point(12, 109);
            L_CurrentS1.Name = "L_CurrentS1";
            L_CurrentS1.Size = new Size(49, 15);
            L_CurrentS1.TabIndex = 26;
            L_CurrentS1.Text = "Seed[1]:";
            // 
            // L_CurrentS0
            // 
            L_CurrentS0.AutoSize = true;
            L_CurrentS0.Location = new Point(12, 85);
            L_CurrentS0.Name = "L_CurrentS0";
            L_CurrentS0.Size = new Size(49, 15);
            L_CurrentS0.TabIndex = 25;
            L_CurrentS0.Text = "Seed[0]:";
            // 
            // TB_CurrentS1
            // 
            TB_CurrentS1.CharacterCasing = CharacterCasing.Upper;
            TB_CurrentS1.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_CurrentS1.Location = new Point(89, 107);
            TB_CurrentS1.MaxLength = 16;
            TB_CurrentS1.Name = "TB_CurrentS1";
            TB_CurrentS1.ReadOnly = true;
            TB_CurrentS1.Size = new Size(118, 22);
            TB_CurrentS1.TabIndex = 24;
            TB_CurrentS1.TabStop = false;
            TB_CurrentS1.Text = "0123456789ABCDEF";
            // 
            // TB_CurrentS0
            // 
            TB_CurrentS0.CharacterCasing = CharacterCasing.Upper;
            TB_CurrentS0.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_CurrentS0.Location = new Point(89, 83);
            TB_CurrentS0.MaxLength = 16;
            TB_CurrentS0.Name = "TB_CurrentS0";
            TB_CurrentS0.ReadOnly = true;
            TB_CurrentS0.Size = new Size(118, 22);
            TB_CurrentS0.TabIndex = 23;
            TB_CurrentS0.TabStop = false;
            TB_CurrentS0.Text = "0123456789ABCDEF";
            // 
            // TB_CurrentAdvances
            // 
            TB_CurrentAdvances.CharacterCasing = CharacterCasing.Lower;
            TB_CurrentAdvances.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_CurrentAdvances.Location = new Point(52, 59);
            TB_CurrentAdvances.MaxLength = 15;
            TB_CurrentAdvances.Name = "TB_CurrentAdvances";
            TB_CurrentAdvances.ReadOnly = true;
            TB_CurrentAdvances.Size = new Size(106, 22);
            TB_CurrentAdvances.TabIndex = 28;
            TB_CurrentAdvances.TabStop = false;
            TB_CurrentAdvances.Text = "12,345,678,901";
            TB_CurrentAdvances.TextAlign = HorizontalAlignment.Right;
            // 
            // L_CurrentAdvances
            // 
            L_CurrentAdvances.AutoSize = true;
            L_CurrentAdvances.Location = new Point(12, 64);
            L_CurrentAdvances.Name = "L_CurrentAdvances";
            L_CurrentAdvances.Size = new Size(34, 15);
            L_CurrentAdvances.TabIndex = 27;
            L_CurrentAdvances.Text = "Adv.:";
            // 
            // GB_MyPokemon
            // 
            GB_MyPokemon.Controls.Add(L_Active_Level);
            GB_MyPokemon.Controls.Add(NUD_Active_Level);
            GB_MyPokemon.Controls.Add(L_Active_Gender);
            GB_MyPokemon.Controls.Add(CB_Active_Gender);
            GB_MyPokemon.Controls.Add(L_Active_Species);
            GB_MyPokemon.Controls.Add(CB_Active_Species);
            GB_MyPokemon.Location = new Point(213, 5);
            GB_MyPokemon.Name = "GB_MyPokemon";
            GB_MyPokemon.Size = new Size(200, 179);
            GB_MyPokemon.TabIndex = 30;
            GB_MyPokemon.TabStop = false;
            GB_MyPokemon.Text = "My Pokémon";
            // 
            // L_Active_Level
            // 
            L_Active_Level.AutoSize = true;
            L_Active_Level.Location = new Point(129, 127);
            L_Active_Level.Name = "L_Active_Level";
            L_Active_Level.Size = new Size(24, 15);
            L_Active_Level.TabIndex = 39;
            L_Active_Level.Text = "Lv.:";
            // 
            // NUD_Active_Level
            // 
            NUD_Active_Level.Location = new Point(155, 125);
            NUD_Active_Level.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NUD_Active_Level.Name = "NUD_Active_Level";
            NUD_Active_Level.Size = new Size(39, 23);
            NUD_Active_Level.TabIndex = 38;
            NUD_Active_Level.TextAlign = HorizontalAlignment.Right;
            NUD_Active_Level.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // L_Active_Gender
            // 
            L_Active_Gender.AutoSize = true;
            L_Active_Gender.Location = new Point(2, 52);
            L_Active_Gender.Name = "L_Active_Gender";
            L_Active_Gender.Size = new Size(48, 15);
            L_Active_Gender.TabIndex = 37;
            L_Active_Gender.Text = "Gender:";
            // 
            // CB_Active_Gender
            // 
            CB_Active_Gender.FormattingEnabled = true;
            CB_Active_Gender.Items.AddRange(new object[] { "Male", "Female", "Unknown" });
            CB_Active_Gender.Location = new Point(56, 49);
            CB_Active_Gender.Name = "CB_Active_Gender";
            CB_Active_Gender.Size = new Size(138, 23);
            CB_Active_Gender.TabIndex = 36;
            CB_Active_Gender.Text = "wwwwwwwwwwww";
            // 
            // L_Active_Species
            // 
            L_Active_Species.AutoSize = true;
            L_Active_Species.Location = new Point(2, 25);
            L_Active_Species.Name = "L_Active_Species";
            L_Active_Species.Size = new Size(49, 15);
            L_Active_Species.TabIndex = 35;
            L_Active_Species.Text = "Species:";
            // 
            // CB_Active_Species
            // 
            CB_Active_Species.FormattingEnabled = true;
            CB_Active_Species.Location = new Point(56, 22);
            CB_Active_Species.Name = "CB_Active_Species";
            CB_Active_Species.Size = new Size(138, 23);
            CB_Active_Species.TabIndex = 34;
            CB_Active_Species.Text = "wwwwwwwwwwww";
            // 
            // GB_WildPokemon
            // 
            GB_WildPokemon.Controls.Add(L_Wild_Form);
            GB_WildPokemon.Controls.Add(NUD_Wild_Form);
            GB_WildPokemon.Controls.Add(L_Wild_Weight);
            GB_WildPokemon.Controls.Add(NUD_Wild_Weight);
            GB_WildPokemon.Controls.Add(L_Wild_Rate);
            GB_WildPokemon.Controls.Add(NUD_Wild_Rate);
            GB_WildPokemon.Controls.Add(L_Wild_Level);
            GB_WildPokemon.Controls.Add(NUD_Wild_Level);
            GB_WildPokemon.Controls.Add(L_Wild_Status);
            GB_WildPokemon.Controls.Add(CB_Wild_Status);
            GB_WildPokemon.Controls.Add(L_Wild_CurHP);
            GB_WildPokemon.Controls.Add(NUD_Wild_CurHP);
            GB_WildPokemon.Controls.Add(L_Wild_MaxHP);
            GB_WildPokemon.Controls.Add(NUD_Wild_MaxHP);
            GB_WildPokemon.Controls.Add(L_Wild_Gender);
            GB_WildPokemon.Controls.Add(CB_Wild_Gender);
            GB_WildPokemon.Controls.Add(L_Wild_Species);
            GB_WildPokemon.Controls.Add(CB_Wild_Species);
            GB_WildPokemon.Location = new Point(419, 5);
            GB_WildPokemon.Name = "GB_WildPokemon";
            GB_WildPokemon.Size = new Size(200, 179);
            GB_WildPokemon.TabIndex = 31;
            GB_WildPokemon.TabStop = false;
            GB_WildPokemon.Text = "Wild Pokémon";
            // 
            // L_Wild_Form
            // 
            L_Wild_Form.AutoSize = true;
            L_Wild_Form.Location = new Point(115, 52);
            L_Wild_Form.Name = "L_Wild_Form";
            L_Wild_Form.Size = new Size(38, 15);
            L_Wild_Form.TabIndex = 69;
            L_Wild_Form.Text = "Form:";
            // 
            // NUD_Wild_Form
            // 
            NUD_Wild_Form.Location = new Point(155, 49);
            NUD_Wild_Form.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            NUD_Wild_Form.Name = "NUD_Wild_Form";
            NUD_Wild_Form.Size = new Size(39, 23);
            NUD_Wild_Form.TabIndex = 68;
            NUD_Wild_Form.TextAlign = HorizontalAlignment.Right;
            NUD_Wild_Form.ValueChanged += NUD_Wild_Form_ValueChanged;
            // 
            // L_Wild_Weight
            // 
            L_Wild_Weight.AutoSize = true;
            L_Wild_Weight.Enabled = false;
            L_Wild_Weight.Location = new Point(2, 152);
            L_Wild_Weight.Name = "L_Wild_Weight";
            L_Wild_Weight.Size = new Size(48, 15);
            L_Wild_Weight.TabIndex = 38;
            L_Wild_Weight.Text = "Weight:";
            // 
            // NUD_Wild_Weight
            // 
            NUD_Wild_Weight.Enabled = false;
            NUD_Wild_Weight.Location = new Point(56, 150);
            NUD_Wild_Weight.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            NUD_Wild_Weight.Name = "NUD_Wild_Weight";
            NUD_Wild_Weight.Size = new Size(46, 23);
            NUD_Wild_Weight.TabIndex = 37;
            NUD_Wild_Weight.TextAlign = HorizontalAlignment.Right;
            NUD_Wild_Weight.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // L_Wild_Rate
            // 
            L_Wild_Rate.AutoSize = true;
            L_Wild_Rate.Enabled = false;
            L_Wild_Rate.Location = new Point(120, 152);
            L_Wild_Rate.Name = "L_Wild_Rate";
            L_Wild_Rate.Size = new Size(33, 15);
            L_Wild_Rate.TabIndex = 36;
            L_Wild_Rate.Text = "Rate:";
            // 
            // NUD_Wild_Rate
            // 
            NUD_Wild_Rate.Enabled = false;
            NUD_Wild_Rate.Location = new Point(155, 150);
            NUD_Wild_Rate.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            NUD_Wild_Rate.Name = "NUD_Wild_Rate";
            NUD_Wild_Rate.Size = new Size(39, 23);
            NUD_Wild_Rate.TabIndex = 35;
            NUD_Wild_Rate.TextAlign = HorizontalAlignment.Right;
            NUD_Wild_Rate.Value = new decimal(new int[] { 200, 0, 0, 0 });
            // 
            // L_Wild_Level
            // 
            L_Wild_Level.AutoSize = true;
            L_Wild_Level.Location = new Point(129, 127);
            L_Wild_Level.Name = "L_Wild_Level";
            L_Wild_Level.Size = new Size(24, 15);
            L_Wild_Level.TabIndex = 33;
            L_Wild_Level.Text = "Lv.:";
            // 
            // NUD_Wild_Level
            // 
            NUD_Wild_Level.Location = new Point(155, 125);
            NUD_Wild_Level.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NUD_Wild_Level.Name = "NUD_Wild_Level";
            NUD_Wild_Level.Size = new Size(39, 23);
            NUD_Wild_Level.TabIndex = 32;
            NUD_Wild_Level.TextAlign = HorizontalAlignment.Right;
            NUD_Wild_Level.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // L_Wild_Status
            // 
            L_Wild_Status.AutoSize = true;
            L_Wild_Status.Location = new Point(2, 100);
            L_Wild_Status.Name = "L_Wild_Status";
            L_Wild_Status.Size = new Size(42, 15);
            L_Wild_Status.TabIndex = 31;
            L_Wild_Status.Text = "Status:";
            // 
            // CB_Wild_Status
            // 
            CB_Wild_Status.FormattingEnabled = true;
            CB_Wild_Status.Items.AddRange(new object[] { "(None)", "Asleep", "Burned", "Frozen", "Paralyzed", "Poisoned" });
            CB_Wild_Status.Location = new Point(56, 99);
            CB_Wild_Status.Name = "CB_Wild_Status";
            CB_Wild_Status.Size = new Size(138, 23);
            CB_Wild_Status.TabIndex = 30;
            CB_Wild_Status.Text = "wwwwwwwwwwww";
            // 
            // L_Wild_CurHP
            // 
            L_Wild_CurHP.AutoSize = true;
            L_Wild_CurHP.Location = new Point(2, 76);
            L_Wild_CurHP.Name = "L_Wild_CurHP";
            L_Wild_CurHP.Size = new Size(51, 15);
            L_Wild_CurHP.TabIndex = 29;
            L_Wild_CurHP.Text = "Cur. HP:";
            // 
            // NUD_Wild_CurHP
            // 
            NUD_Wild_CurHP.Location = new Point(56, 74);
            NUD_Wild_CurHP.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            NUD_Wild_CurHP.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NUD_Wild_CurHP.Name = "NUD_Wild_CurHP";
            NUD_Wild_CurHP.Size = new Size(39, 23);
            NUD_Wild_CurHP.TabIndex = 28;
            NUD_Wild_CurHP.TextAlign = HorizontalAlignment.Right;
            NUD_Wild_CurHP.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // L_Wild_MaxHP
            // 
            L_Wild_MaxHP.AutoSize = true;
            L_Wild_MaxHP.Location = new Point(101, 76);
            L_Wild_MaxHP.Name = "L_Wild_MaxHP";
            L_Wild_MaxHP.Size = new Size(52, 15);
            L_Wild_MaxHP.TabIndex = 27;
            L_Wild_MaxHP.Text = "Max HP:";
            // 
            // NUD_Wild_MaxHP
            // 
            NUD_Wild_MaxHP.Location = new Point(155, 74);
            NUD_Wild_MaxHP.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            NUD_Wild_MaxHP.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NUD_Wild_MaxHP.Name = "NUD_Wild_MaxHP";
            NUD_Wild_MaxHP.Size = new Size(39, 23);
            NUD_Wild_MaxHP.TabIndex = 26;
            NUD_Wild_MaxHP.TextAlign = HorizontalAlignment.Right;
            NUD_Wild_MaxHP.Value = new decimal(new int[] { 200, 0, 0, 0 });
            // 
            // L_Wild_Gender
            // 
            L_Wild_Gender.AutoSize = true;
            L_Wild_Gender.Location = new Point(2, 52);
            L_Wild_Gender.Name = "L_Wild_Gender";
            L_Wild_Gender.Size = new Size(48, 15);
            L_Wild_Gender.TabIndex = 25;
            L_Wild_Gender.Text = "Gender:";
            // 
            // CB_Wild_Gender
            // 
            CB_Wild_Gender.FormattingEnabled = true;
            CB_Wild_Gender.Items.AddRange(new object[] { "M", "F", "-" });
            CB_Wild_Gender.Location = new Point(56, 49);
            CB_Wild_Gender.Name = "CB_Wild_Gender";
            CB_Wild_Gender.Size = new Size(39, 23);
            CB_Wild_Gender.TabIndex = 24;
            CB_Wild_Gender.Text = "wwwwwwwwwwww";
            // 
            // L_Wild_Species
            // 
            L_Wild_Species.AutoSize = true;
            L_Wild_Species.Location = new Point(2, 25);
            L_Wild_Species.Name = "L_Wild_Species";
            L_Wild_Species.Size = new Size(49, 15);
            L_Wild_Species.TabIndex = 10;
            L_Wild_Species.Text = "Species:";
            // 
            // CB_Wild_Species
            // 
            CB_Wild_Species.FormattingEnabled = true;
            CB_Wild_Species.Location = new Point(56, 22);
            CB_Wild_Species.Name = "CB_Wild_Species";
            CB_Wild_Species.Size = new Size(138, 23);
            CB_Wild_Species.TabIndex = 9;
            CB_Wild_Species.Text = "wwwwwwwwwwww";
            CB_Wild_Species.SelectedIndexChanged += CB_Wild_Species_SelectedIndexChanged;
            // 
            // GB_Modifiers
            // 
            GB_Modifiers.Controls.Add(CB_8thBadge);
            GB_Modifiers.Controls.Add(CB_Charm);
            GB_Modifiers.Controls.Add(L_ZukanCaught);
            GB_Modifiers.Controls.Add(L_Turns);
            GB_Modifiers.Controls.Add(L_Ball);
            GB_Modifiers.Controls.Add(NUD_ZukanCaught);
            GB_Modifiers.Controls.Add(CB_Ball);
            GB_Modifiers.Controls.Add(NUD_Turns);
            GB_Modifiers.Controls.Add(CB_First);
            GB_Modifiers.Controls.Add(CB_Dusk);
            GB_Modifiers.Controls.Add(CB_Registered);
            GB_Modifiers.Controls.Add(CB_Surfing);
            GB_Modifiers.Controls.Add(CB_Fishing);
            GB_Modifiers.Location = new Point(625, 5);
            GB_Modifiers.Name = "GB_Modifiers";
            GB_Modifiers.Size = new Size(200, 208);
            GB_Modifiers.TabIndex = 32;
            GB_Modifiers.TabStop = false;
            GB_Modifiers.Text = "Modifiers";
            // 
            // CB_8thBadge
            // 
            CB_8thBadge.AutoSize = true;
            CB_8thBadge.Location = new Point(100, 152);
            CB_8thBadge.Name = "CB_8thBadge";
            CB_8thBadge.Size = new Size(79, 19);
            CB_8thBadge.TabIndex = 12;
            CB_8thBadge.Text = "8th Badge";
            CB_8thBadge.UseVisualStyleBackColor = true;
            CB_8thBadge.CheckedChanged += CB_8thBadge_CheckedChanged;
            // 
            // CB_Charm
            // 
            CB_Charm.AutoSize = true;
            CB_Charm.Location = new Point(6, 151);
            CB_Charm.Name = "CB_Charm";
            CB_Charm.Size = new Size(96, 19);
            CB_Charm.TabIndex = 11;
            CB_Charm.Text = "Catch Charm";
            CB_Charm.UseVisualStyleBackColor = true;
            CB_Charm.CheckedChanged += CB_Charm_CheckedChanged;
            // 
            // L_ZukanCaught
            // 
            L_ZukanCaught.AutoSize = true;
            L_ZukanCaught.Location = new Point(6, 175);
            L_ZukanCaught.Name = "L_ZukanCaught";
            L_ZukanCaught.Size = new Size(127, 15);
            L_ZukanCaught.TabIndex = 10;
            L_ZukanCaught.Text = "Species Caught in Dex:";
            // 
            // L_Turns
            // 
            L_Turns.AutoSize = true;
            L_Turns.Location = new Point(100, 127);
            L_Turns.Name = "L_Turns";
            L_Turns.Size = new Size(39, 15);
            L_Turns.TabIndex = 9;
            L_Turns.Text = "Turns:";
            // 
            // L_Ball
            // 
            L_Ball.AutoSize = true;
            L_Ball.Location = new Point(6, 25);
            L_Ball.Name = "L_Ball";
            L_Ball.Size = new Size(29, 15);
            L_Ball.TabIndex = 8;
            L_Ball.Text = "Ball:";
            // 
            // NUD_ZukanCaught
            // 
            NUD_ZukanCaught.Location = new Point(155, 173);
            NUD_ZukanCaught.Maximum = new decimal(new int[] { 584, 0, 0, 0 });
            NUD_ZukanCaught.Name = "NUD_ZukanCaught";
            NUD_ZukanCaught.Size = new Size(39, 23);
            NUD_ZukanCaught.TabIndex = 7;
            NUD_ZukanCaught.TextAlign = HorizontalAlignment.Right;
            NUD_ZukanCaught.Value = new decimal(new int[] { 584, 0, 0, 0 });
            NUD_ZukanCaught.ValueChanged += NUD_ZukanCaught_ValueChanged;
            // 
            // CB_Ball
            // 
            CB_Ball.FormattingEnabled = true;
            CB_Ball.Items.AddRange(new object[] { "Beast Ball", "Dive Ball", "Dream Ball", "Dusk Ball", "Fast Ball", "Friend Ball", "Great Ball", "Heal Ball", "Heavy Ball", "Level Ball", "Love Ball", "Lure Ball", "Luxury Ball", "Moon Ball", "Nest Ball", "Net Ball", "Poké Ball", "Premier Ball", "Quick Ball", "Repeat Ball", "Safari Ball", "Sport Ball", "Timer Ball", "Ultra Ball" });
            CB_Ball.Location = new Point(41, 22);
            CB_Ball.Name = "CB_Ball";
            CB_Ball.Size = new Size(153, 23);
            CB_Ball.TabIndex = 6;
            CB_Ball.Text = "wwwwwwwwwwww";
            CB_Ball.SelectedIndexChanged += CB_Ball_SelectedIndexChanged;
            // 
            // NUD_Turns
            // 
            NUD_Turns.Location = new Point(155, 125);
            NUD_Turns.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NUD_Turns.Name = "NUD_Turns";
            NUD_Turns.Size = new Size(39, 23);
            NUD_Turns.TabIndex = 5;
            NUD_Turns.TextAlign = HorizontalAlignment.Right;
            NUD_Turns.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // CB_First
            // 
            CB_First.AutoSize = true;
            CB_First.Location = new Point(6, 126);
            CB_First.Name = "CB_First";
            CB_First.Size = new Size(75, 19);
            CB_First.TabIndex = 4;
            CB_First.Text = "First Turn";
            CB_First.UseVisualStyleBackColor = true;
            // 
            // CB_Dusk
            // 
            CB_Dusk.AutoSize = true;
            CB_Dusk.Location = new Point(6, 101);
            CB_Dusk.Name = "CB_Dusk";
            CB_Dusk.Size = new Size(87, 19);
            CB_Dusk.TabIndex = 3;
            CB_Dusk.Text = "Cave/Night";
            CB_Dusk.UseVisualStyleBackColor = true;
            // 
            // CB_Registered
            // 
            CB_Registered.AutoSize = true;
            CB_Registered.Location = new Point(6, 76);
            CB_Registered.Name = "CB_Registered";
            CB_Registered.Size = new Size(167, 19);
            CB_Registered.TabIndex = 2;
            CB_Registered.Text = "Registered in Dex (Caught)";
            CB_Registered.UseVisualStyleBackColor = true;
            // 
            // CB_Surfing
            // 
            CB_Surfing.AutoSize = true;
            CB_Surfing.Location = new Point(100, 51);
            CB_Surfing.Name = "CB_Surfing";
            CB_Surfing.Size = new Size(64, 19);
            CB_Surfing.TabIndex = 1;
            CB_Surfing.Text = "Surfing";
            CB_Surfing.UseVisualStyleBackColor = true;
            // 
            // CB_Fishing
            // 
            CB_Fishing.AutoSize = true;
            CB_Fishing.Location = new Point(6, 51);
            CB_Fishing.Name = "CB_Fishing";
            CB_Fishing.Size = new Size(64, 19);
            CB_Fishing.TabIndex = 0;
            CB_Fishing.Text = "Fishing";
            CB_Fishing.UseVisualStyleBackColor = true;
            // 
            // B_ReadParty
            // 
            B_ReadParty.Location = new Point(221, 190);
            B_ReadParty.Name = "B_ReadParty";
            B_ReadParty.Size = new Size(141, 25);
            B_ReadParty.TabIndex = 33;
            B_ReadParty.Text = "Read My Pokémon";
            B_ReadParty.UseVisualStyleBackColor = true;
            B_ReadParty.Click += B_ReadParty_Click;
            // 
            // button2
            // 
            button2.Location = new Point(421, 190);
            button2.Name = "button2";
            button2.Size = new Size(192, 25);
            button2.TabIndex = 34;
            button2.Text = "Read Wild Pokémon";
            button2.UseVisualStyleBackColor = true;
            button2.Click += B_ReadWild_Click;
            // 
            // NUD_PartySlot
            // 
            NUD_PartySlot.Location = new Point(368, 190);
            NUD_PartySlot.Maximum = new decimal(new int[] { 6, 0, 0, 0 });
            NUD_PartySlot.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NUD_PartySlot.Name = "NUD_PartySlot";
            NUD_PartySlot.Size = new Size(39, 23);
            NUD_PartySlot.TabIndex = 40;
            NUD_PartySlot.TextAlign = HorizontalAlignment.Right;
            NUD_PartySlot.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // B_Search
            // 
            B_Search.Location = new Point(12, 288);
            B_Search.Name = "B_Search";
            B_Search.Size = new Size(195, 25);
            B_Search.TabIndex = 41;
            B_Search.Text = "Search";
            B_Search.UseVisualStyleBackColor = true;
            B_Search.Click += B_Search_Click;
            // 
            // L_TargetSuccess
            // 
            L_TargetSuccess.AutoSize = true;
            L_TargetSuccess.Location = new Point(32, 212);
            L_TargetSuccess.Name = "L_TargetSuccess";
            L_TargetSuccess.Size = new Size(51, 15);
            L_TargetSuccess.TabIndex = 63;
            L_TargetSuccess.Text = "Success:";
            // 
            // CB_TargetSuccess
            // 
            CB_TargetSuccess.FormattingEnabled = true;
            CB_TargetSuccess.Items.AddRange(new object[] { "(Ignore)", "Yes", "No" });
            CB_TargetSuccess.Location = new Point(89, 209);
            CB_TargetSuccess.Name = "CB_TargetSuccess";
            CB_TargetSuccess.Size = new Size(118, 23);
            CB_TargetSuccess.TabIndex = 60;
            CB_TargetSuccess.Text = "None";
            // 
            // L_Capture_Plus
            // 
            L_Capture_Plus.AutoSize = true;
            L_Capture_Plus.Location = new Point(68, 187);
            L_Capture_Plus.Name = "L_Capture_Plus";
            L_Capture_Plus.Size = new Size(15, 15);
            L_Capture_Plus.TabIndex = 62;
            L_Capture_Plus.Text = "+";
            // 
            // L_Capture_Initial
            // 
            L_Capture_Initial.AutoSize = true;
            L_Capture_Initial.Location = new Point(20, 164);
            L_Capture_Initial.Name = "L_Capture_Initial";
            L_Capture_Initial.Size = new Size(63, 15);
            L_Capture_Initial.TabIndex = 61;
            L_Capture_Initial.Text = "Initial Adv.";
            // 
            // TB_Capture_Advances
            // 
            TB_Capture_Advances.CharacterCasing = CharacterCasing.Upper;
            TB_Capture_Advances.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Capture_Advances.Location = new Point(89, 185);
            TB_Capture_Advances.MaxLength = 16;
            TB_Capture_Advances.Name = "TB_Capture_Advances";
            TB_Capture_Advances.Size = new Size(118, 22);
            TB_Capture_Advances.TabIndex = 59;
            TB_Capture_Advances.Text = "5000";
            TB_Capture_Advances.TextAlign = HorizontalAlignment.Right;
            // 
            // TB_Capture_Initial
            // 
            TB_Capture_Initial.CharacterCasing = CharacterCasing.Upper;
            TB_Capture_Initial.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Capture_Initial.Location = new Point(89, 162);
            TB_Capture_Initial.MaxLength = 16;
            TB_Capture_Initial.Name = "TB_Capture_Initial";
            TB_Capture_Initial.Size = new Size(118, 22);
            TB_Capture_Initial.TabIndex = 58;
            TB_Capture_Initial.Text = "0";
            TB_Capture_Initial.TextAlign = HorizontalAlignment.Right;
            // 
            // DGV_Results
            // 
            DGV_Results.AllowUserToAddRows = false;
            DGV_Results.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = Color.Black;
            DGV_Results.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DGV_Results.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DGV_Results.AutoGenerateColumns = false;
            DGV_Results.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DGV_Results.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGV_Results.Columns.AddRange(new DataGridViewColumn[] { advancesDataGridViewTextBoxColumn, successDataGridViewCheckBoxColumn, criticalDataGridViewCheckBoxColumn, shakesDataGridViewTextBoxColumn, seed0DataGridViewTextBoxColumn, seed1DataGridViewTextBoxColumn });
            DGV_Results.DataSource = CaptureResultsSource;
            DGV_Results.Location = new Point(215, 219);
            DGV_Results.Name = "DGV_Results";
            DGV_Results.ReadOnly = true;
            DGV_Results.RowHeadersVisible = false;
            DGV_Results.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV_Results.Size = new Size(610, 347);
            DGV_Results.TabIndex = 64;
            // 
            // advancesDataGridViewTextBoxColumn
            // 
            advancesDataGridViewTextBoxColumn.DataPropertyName = "Advances";
            advancesDataGridViewTextBoxColumn.HeaderText = "Advances";
            advancesDataGridViewTextBoxColumn.Name = "advancesDataGridViewTextBoxColumn";
            advancesDataGridViewTextBoxColumn.ReadOnly = true;
            advancesDataGridViewTextBoxColumn.Width = 83;
            // 
            // successDataGridViewCheckBoxColumn
            // 
            successDataGridViewCheckBoxColumn.DataPropertyName = "Success";
            successDataGridViewCheckBoxColumn.HeaderText = "Success";
            successDataGridViewCheckBoxColumn.Name = "successDataGridViewCheckBoxColumn";
            successDataGridViewCheckBoxColumn.ReadOnly = true;
            successDataGridViewCheckBoxColumn.Width = 54;
            // 
            // criticalDataGridViewCheckBoxColumn
            // 
            criticalDataGridViewCheckBoxColumn.DataPropertyName = "Critical";
            criticalDataGridViewCheckBoxColumn.HeaderText = "Critical";
            criticalDataGridViewCheckBoxColumn.Name = "criticalDataGridViewCheckBoxColumn";
            criticalDataGridViewCheckBoxColumn.ReadOnly = true;
            criticalDataGridViewCheckBoxColumn.Width = 50;
            // 
            // shakesDataGridViewTextBoxColumn
            // 
            shakesDataGridViewTextBoxColumn.DataPropertyName = "Shakes";
            shakesDataGridViewTextBoxColumn.HeaderText = "Shakes";
            shakesDataGridViewTextBoxColumn.Name = "shakesDataGridViewTextBoxColumn";
            shakesDataGridViewTextBoxColumn.ReadOnly = true;
            shakesDataGridViewTextBoxColumn.Width = 68;
            // 
            // seed0DataGridViewTextBoxColumn
            // 
            seed0DataGridViewTextBoxColumn.DataPropertyName = "Seed0";
            seed0DataGridViewTextBoxColumn.HeaderText = "Seed0";
            seed0DataGridViewTextBoxColumn.Name = "seed0DataGridViewTextBoxColumn";
            seed0DataGridViewTextBoxColumn.ReadOnly = true;
            seed0DataGridViewTextBoxColumn.Width = 63;
            // 
            // seed1DataGridViewTextBoxColumn
            // 
            seed1DataGridViewTextBoxColumn.DataPropertyName = "Seed1";
            seed1DataGridViewTextBoxColumn.HeaderText = "Seed1";
            seed1DataGridViewTextBoxColumn.Name = "seed1DataGridViewTextBoxColumn";
            seed1DataGridViewTextBoxColumn.ReadOnly = true;
            seed1DataGridViewTextBoxColumn.Width = 63;
            // 
            // CaptureResultsSource
            // 
            CaptureResultsSource.DataSource = typeof(Core.Interfaces.CaptureFrame);
            // 
            // L_TargetCrit
            // 
            L_TargetCrit.AutoSize = true;
            L_TargetCrit.Location = new Point(36, 237);
            L_TargetCrit.Name = "L_TargetCrit";
            L_TargetCrit.Size = new Size(47, 15);
            L_TargetCrit.TabIndex = 66;
            L_TargetCrit.Text = "Critical:";
            // 
            // CB_TargetCrit
            // 
            CB_TargetCrit.FormattingEnabled = true;
            CB_TargetCrit.Items.AddRange(new object[] { "(Ignore)", "Yes", "No" });
            CB_TargetCrit.Location = new Point(89, 234);
            CB_TargetCrit.Name = "CB_TargetCrit";
            CB_TargetCrit.Size = new Size(118, 23);
            CB_TargetCrit.TabIndex = 65;
            CB_TargetCrit.Text = "None";
            // 
            // L_ShakesMin
            // 
            L_ShakesMin.AutoSize = true;
            L_ShakesMin.Location = new Point(10, 261);
            L_ShakesMin.Name = "L_ShakesMin";
            L_ShakesMin.Size = new Size(73, 15);
            L_ShakesMin.TabIndex = 68;
            L_ShakesMin.Text = "Shakes Min.:";
            // 
            // NUD_ShakesMax
            // 
            NUD_ShakesMax.Location = new Point(172, 259);
            NUD_ShakesMax.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            NUD_ShakesMax.Name = "NUD_ShakesMax";
            NUD_ShakesMax.Size = new Size(35, 23);
            NUD_ShakesMax.TabIndex = 67;
            NUD_ShakesMax.TextAlign = HorizontalAlignment.Right;
            NUD_ShakesMax.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // NUD_ShakesMin
            // 
            NUD_ShakesMin.Location = new Point(89, 259);
            NUD_ShakesMin.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            NUD_ShakesMin.Name = "NUD_ShakesMin";
            NUD_ShakesMin.Size = new Size(35, 23);
            NUD_ShakesMin.TabIndex = 69;
            NUD_ShakesMin.TextAlign = HorizontalAlignment.Right;
            // 
            // L_ShakesMax
            // 
            L_ShakesMax.AutoSize = true;
            L_ShakesMax.Location = new Point(130, 261);
            L_ShakesMax.Name = "L_ShakesMax";
            L_ShakesMax.Size = new Size(36, 15);
            L_ShakesMax.TabIndex = 70;
            L_ShakesMax.Text = "Max.:";
            // 
            // CaptureCalc
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(835, 578);
            Controls.Add(L_ShakesMax);
            Controls.Add(NUD_ShakesMin);
            Controls.Add(L_ShakesMin);
            Controls.Add(NUD_ShakesMax);
            Controls.Add(L_TargetCrit);
            Controls.Add(CB_TargetCrit);
            Controls.Add(DGV_Results);
            Controls.Add(L_TargetSuccess);
            Controls.Add(CB_TargetSuccess);
            Controls.Add(L_Capture_Plus);
            Controls.Add(L_Capture_Initial);
            Controls.Add(TB_Capture_Advances);
            Controls.Add(TB_Capture_Initial);
            Controls.Add(B_Search);
            Controls.Add(NUD_PartySlot);
            Controls.Add(button2);
            Controls.Add(B_ReadParty);
            Controls.Add(GB_Modifiers);
            Controls.Add(GB_WildPokemon);
            Controls.Add(GB_MyPokemon);
            Controls.Add(TB_AdvancesIncrease);
            Controls.Add(B_CopyToInitial);
            Controls.Add(L_CurrentS1);
            Controls.Add(L_CurrentS0);
            Controls.Add(TB_CurrentS1);
            Controls.Add(TB_CurrentS0);
            Controls.Add(TB_CurrentAdvances);
            Controls.Add(L_CurrentAdvances);
            Controls.Add(L_Seed1);
            Controls.Add(L_Seed0);
            Controls.Add(TB_Seed1);
            Controls.Add(TB_Seed0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "CaptureCalc";
            Text = "Capture RNG";
            FormClosing += DexRecSearcher_FormClosing;
            GB_MyPokemon.ResumeLayout(false);
            GB_MyPokemon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_Active_Level).EndInit();
            GB_WildPokemon.ResumeLayout(false);
            GB_WildPokemon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_Wild_Form).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Wild_Weight).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Wild_Rate).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Wild_Level).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Wild_CurHP).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Wild_MaxHP).EndInit();
            GB_Modifiers.ResumeLayout(false);
            GB_Modifiers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_ZukanCaught).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Turns).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_PartySlot).EndInit();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).EndInit();
            ((System.ComponentModel.ISupportInitialize)CaptureResultsSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_ShakesMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_ShakesMin).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label L_Seed1;
        private Label L_Seed0;
        public TextBox TB_Seed1;
        public TextBox TB_Seed0;
        private TextBox TB_AdvancesIncrease;
        private Button B_CopyToInitial;
        private Label L_CurrentS1;
        private Label L_CurrentS0;
        private TextBox TB_CurrentS1;
        private TextBox TB_CurrentS0;
        public TextBox TB_CurrentAdvances;
        private Label L_CurrentAdvances;
        private GroupBox GB_MyPokemon;
        private GroupBox GB_WildPokemon;
        private GroupBox GB_Modifiers;
        private CheckBox CB_Surfing;
        private CheckBox CB_Fishing;
        private CheckBox CB_Dusk;
        private CheckBox CB_Registered;
        private ComboBox CB_Ball;
        private NumericUpDown NUD_Turns;
        private CheckBox CB_First;
        private NumericUpDown NUD_ZukanCaught;
        private Label L_ZukanCaught;
        private Label L_Turns;
        private Label L_Ball;
        private Label L_Wild_Species;
        private ComboBox CB_Wild_Species;
        private ComboBox CB_Wild_Gender;
        private Label L_Wild_Gender;
        private Label L_Wild_CurHP;
        private NumericUpDown NUD_Wild_CurHP;
        private Label L_Wild_MaxHP;
        private NumericUpDown NUD_Wild_MaxHP;
        private Label L_Wild_Status;
        private ComboBox CB_Wild_Status;
        private Label L_Wild_Level;
        private NumericUpDown NUD_Wild_Level;
        private Label L_Wild_Weight;
        private NumericUpDown NUD_Wild_Weight;
        private Label L_Wild_Rate;
        private NumericUpDown NUD_Wild_Rate;
        private Label L_Active_Level;
        private NumericUpDown NUD_Active_Level;
        private Label L_Active_Gender;
        private ComboBox CB_Active_Gender;
        private Label L_Active_Species;
        private ComboBox CB_Active_Species;
        private Button B_ReadParty;
        private Button button2;
        private CheckBox CB_Charm;
        private NumericUpDown NUD_PartySlot;
        private CheckBox CB_8thBadge;
        private Button B_Search;
        private Label L_TargetSuccess;
        private ComboBox CB_TargetSuccess;
        private Label L_Capture_Plus;
        private Label L_Capture_Initial;
        private TextBox TB_Capture_Advances;
        private TextBox TB_Capture_Initial;
        private DataGridView DGV_Results;
        private BindingSource CaptureResultsSource;
        private DataGridViewTextBoxColumn advancesDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn successDataGridViewCheckBoxColumn;
        private DataGridViewCheckBoxColumn criticalDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn shakesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn seed0DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn seed1DataGridViewTextBoxColumn;
        private Label L_TargetCrit;
        private ComboBox CB_TargetCrit;
        private Label L_ShakesMin;
        private NumericUpDown NUD_ShakesMax;
        private NumericUpDown NUD_ShakesMin;
        private Label L_ShakesMax;
        private NumericUpDown NUD_Wild_Form;
        private Label L_Wild_Form;
    }
}
