namespace owoow.WinForms.Subforms
{
    partial class Cramomatic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cramomatic));
            GB_Seed = new GroupBox();
            L_Seed1 = new Label();
            L_Seed0 = new Label();
            TB_Seed1 = new TextBox();
            TB_Seed0 = new TextBox();
            GB_SearchSettings = new GroupBox();
            L_Item4 = new Label();
            CB_Item4 = new ComboBox();
            L_Item3 = new Label();
            CB_Item3 = new ComboBox();
            L_Item2 = new Label();
            CB_Item2 = new ComboBox();
            CB_BonusOnly = new CheckBox();
            L_Item1 = new Label();
            CB_Item1 = new ComboBox();
            CB_Cramomatic_MenuClose = new CheckBox();
            L_Target = new Label();
            CB_Target = new ComboBox();
            CB_Cramomatic_MenuClose_Direction = new CheckBox();
            L_Cramomatic_NPCs = new Label();
            TB_Cramomatic_NPCs = new TextBox();
            L_Timeline_Plus = new Label();
            B_Cramomatic_Search = new Button();
            L_Timeline_Initial = new Label();
            TB_Cramomatic_Advances = new TextBox();
            TB_Cramomatic_Initial = new TextBox();
            DGV_Results = new DataGridView();
            advancesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            jumpDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            animationDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            prizeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            bonusDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            seed0DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            seed1DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            CramomaticResultsSource = new BindingSource(components);
            GB_Seed.SuspendLayout();
            GB_SearchSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CramomaticResultsSource).BeginInit();
            SuspendLayout();
            // 
            // GB_Seed
            // 
            GB_Seed.Controls.Add(L_Seed1);
            GB_Seed.Controls.Add(L_Seed0);
            GB_Seed.Controls.Add(TB_Seed1);
            GB_Seed.Controls.Add(TB_Seed0);
            GB_Seed.Location = new Point(0, 2);
            GB_Seed.Name = "GB_Seed";
            GB_Seed.RightToLeft = RightToLeft.No;
            GB_Seed.Size = new Size(212, 60);
            GB_Seed.TabIndex = 1;
            GB_Seed.TabStop = false;
            // 
            // L_Seed1
            // 
            L_Seed1.AutoSize = true;
            L_Seed1.Location = new Point(33, 35);
            L_Seed1.Name = "L_Seed1";
            L_Seed1.Size = new Size(49, 15);
            L_Seed1.TabIndex = 7;
            L_Seed1.Text = "Seed[1]:";
            // 
            // L_Seed0
            // 
            L_Seed0.AutoSize = true;
            L_Seed0.Location = new Point(33, 11);
            L_Seed0.Name = "L_Seed0";
            L_Seed0.Size = new Size(49, 15);
            L_Seed0.TabIndex = 6;
            L_Seed0.Text = "Seed[0]:";
            // 
            // TB_Seed1
            // 
            TB_Seed1.CharacterCasing = CharacterCasing.Upper;
            TB_Seed1.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Seed1.Location = new Point(88, 33);
            TB_Seed1.MaxLength = 16;
            TB_Seed1.Name = "TB_Seed1";
            TB_Seed1.Size = new Size(118, 22);
            TB_Seed1.TabIndex = 1;
            TB_Seed1.Text = "0123456789ABCDEF";
            // 
            // TB_Seed0
            // 
            TB_Seed0.CharacterCasing = CharacterCasing.Upper;
            TB_Seed0.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Seed0.Location = new Point(88, 9);
            TB_Seed0.MaxLength = 16;
            TB_Seed0.Name = "TB_Seed0";
            TB_Seed0.Size = new Size(118, 22);
            TB_Seed0.TabIndex = 0;
            TB_Seed0.Text = "0123456789ABCDEF";
            // 
            // GB_SearchSettings
            // 
            GB_SearchSettings.Controls.Add(L_Item4);
            GB_SearchSettings.Controls.Add(CB_Item4);
            GB_SearchSettings.Controls.Add(L_Item3);
            GB_SearchSettings.Controls.Add(CB_Item3);
            GB_SearchSettings.Controls.Add(L_Item2);
            GB_SearchSettings.Controls.Add(CB_Item2);
            GB_SearchSettings.Controls.Add(CB_BonusOnly);
            GB_SearchSettings.Controls.Add(L_Item1);
            GB_SearchSettings.Controls.Add(CB_Item1);
            GB_SearchSettings.Controls.Add(CB_Cramomatic_MenuClose);
            GB_SearchSettings.Controls.Add(L_Target);
            GB_SearchSettings.Controls.Add(CB_Target);
            GB_SearchSettings.Controls.Add(CB_Cramomatic_MenuClose_Direction);
            GB_SearchSettings.Controls.Add(L_Cramomatic_NPCs);
            GB_SearchSettings.Controls.Add(TB_Cramomatic_NPCs);
            GB_SearchSettings.Controls.Add(L_Timeline_Plus);
            GB_SearchSettings.Controls.Add(B_Cramomatic_Search);
            GB_SearchSettings.Controls.Add(L_Timeline_Initial);
            GB_SearchSettings.Controls.Add(TB_Cramomatic_Advances);
            GB_SearchSettings.Controls.Add(TB_Cramomatic_Initial);
            GB_SearchSettings.Location = new Point(0, 53);
            GB_SearchSettings.Name = "GB_SearchSettings";
            GB_SearchSettings.Size = new Size(212, 289);
            GB_SearchSettings.TabIndex = 3;
            GB_SearchSettings.TabStop = false;
            // 
            // L_Item4
            // 
            L_Item4.AutoSize = true;
            L_Item4.Location = new Point(39, 188);
            L_Item4.Name = "L_Item4";
            L_Item4.Size = new Size(43, 15);
            L_Item4.TabIndex = 76;
            L_Item4.Text = "Item 4:";
            // 
            // CB_Item4
            // 
            CB_Item4.FormattingEnabled = true;
            CB_Item4.Items.AddRange(new object[] { "Black Apricorn", "Blue Apricorn", "Green Apricorn", "Pink Apricorn", "Red Apricorn", "White Apricorn", "Yellow Apricorn", "Sweet Ingredient" });
            CB_Item4.Location = new Point(88, 185);
            CB_Item4.Name = "CB_Item4";
            CB_Item4.Size = new Size(118, 23);
            CB_Item4.TabIndex = 7;
            CB_Item4.Text = "None";
            CB_Item4.Leave += CB_Leave;
            // 
            // L_Item3
            // 
            L_Item3.AutoSize = true;
            L_Item3.Location = new Point(39, 163);
            L_Item3.Name = "L_Item3";
            L_Item3.Size = new Size(43, 15);
            L_Item3.TabIndex = 74;
            L_Item3.Text = "Item 3:";
            // 
            // CB_Item3
            // 
            CB_Item3.FormattingEnabled = true;
            CB_Item3.Items.AddRange(new object[] { "Black Apricorn", "Blue Apricorn", "Green Apricorn", "Pink Apricorn", "Red Apricorn", "White Apricorn", "Yellow Apricorn", "Sweet Ingredient" });
            CB_Item3.Location = new Point(88, 160);
            CB_Item3.Name = "CB_Item3";
            CB_Item3.Size = new Size(118, 23);
            CB_Item3.TabIndex = 6;
            CB_Item3.Text = "None";
            CB_Item3.Leave += CB_Leave;
            // 
            // L_Item2
            // 
            L_Item2.AutoSize = true;
            L_Item2.Location = new Point(39, 138);
            L_Item2.Name = "L_Item2";
            L_Item2.Size = new Size(43, 15);
            L_Item2.TabIndex = 72;
            L_Item2.Text = "Item 2:";
            // 
            // CB_Item2
            // 
            CB_Item2.FormattingEnabled = true;
            CB_Item2.Items.AddRange(new object[] { "Black Apricorn", "Blue Apricorn", "Green Apricorn", "Pink Apricorn", "Red Apricorn", "White Apricorn", "Yellow Apricorn", "Sweet Ingredient" });
            CB_Item2.Location = new Point(88, 135);
            CB_Item2.Name = "CB_Item2";
            CB_Item2.Size = new Size(118, 23);
            CB_Item2.TabIndex = 5;
            CB_Item2.Text = "None";
            CB_Item2.Leave += CB_Leave;
            // 
            // CB_BonusOnly
            // 
            CB_BonusOnly.AutoSize = true;
            CB_BonusOnly.CheckAlign = ContentAlignment.MiddleRight;
            CB_BonusOnly.Location = new Point(114, 85);
            CB_BonusOnly.Name = "CB_BonusOnly";
            CB_BonusOnly.Size = new Size(92, 19);
            CB_BonusOnly.TabIndex = 3;
            CB_BonusOnly.Tag = "";
            CB_BonusOnly.Text = "Bonus Only?";
            CB_BonusOnly.UseVisualStyleBackColor = true;
            // 
            // L_Item1
            // 
            L_Item1.AutoSize = true;
            L_Item1.Location = new Point(39, 113);
            L_Item1.Name = "L_Item1";
            L_Item1.Size = new Size(43, 15);
            L_Item1.TabIndex = 69;
            L_Item1.Text = "Item 1:";
            // 
            // CB_Item1
            // 
            CB_Item1.FormattingEnabled = true;
            CB_Item1.Items.AddRange(new object[] { "Black Apricorn", "Blue Apricorn", "Green Apricorn", "Pink Apricorn", "Red Apricorn", "White Apricorn", "Yellow Apricorn", "Sweet Ingredient" });
            CB_Item1.Location = new Point(88, 110);
            CB_Item1.Name = "CB_Item1";
            CB_Item1.Size = new Size(118, 23);
            CB_Item1.TabIndex = 4;
            CB_Item1.Text = "None";
            CB_Item1.Leave += CB_Leave;
            // 
            // CB_Cramomatic_MenuClose
            // 
            CB_Cramomatic_MenuClose.AutoSize = true;
            CB_Cramomatic_MenuClose.CheckAlign = ContentAlignment.MiddleRight;
            CB_Cramomatic_MenuClose.Location = new Point(62, 214);
            CB_Cramomatic_MenuClose.Name = "CB_Cramomatic_MenuClose";
            CB_Cramomatic_MenuClose.Size = new Size(144, 19);
            CB_Cramomatic_MenuClose.TabIndex = 8;
            CB_Cramomatic_MenuClose.Tag = "";
            CB_Cramomatic_MenuClose.Text = "Consider Menu Close?";
            CB_Cramomatic_MenuClose.UseVisualStyleBackColor = true;
            CB_Cramomatic_MenuClose.CheckedChanged += CB_Cramomatic_MenuClose_CheckedChanged;
            // 
            // L_Target
            // 
            L_Target.AutoSize = true;
            L_Target.Location = new Point(40, 63);
            L_Target.Name = "L_Target";
            L_Target.Size = new Size(42, 15);
            L_Target.TabIndex = 57;
            L_Target.Text = "Target:";
            // 
            // CB_Target
            // 
            CB_Target.FormattingEnabled = true;
            CB_Target.Items.AddRange(new object[] { "Sport Ball", "Safari Ball", "Apricorn Ball", "Shop Ball", "Star Sweet", "Ribbon Sweet", "Strawberry Sweet", "Any" });
            CB_Target.Location = new Point(88, 60);
            CB_Target.Name = "CB_Target";
            CB_Target.Size = new Size(118, 23);
            CB_Target.TabIndex = 2;
            CB_Target.Text = "None";
            CB_Target.Leave += CB_Leave;
            // 
            // CB_Cramomatic_MenuClose_Direction
            // 
            CB_Cramomatic_MenuClose_Direction.AutoSize = true;
            CB_Cramomatic_MenuClose_Direction.CheckAlign = ContentAlignment.MiddleRight;
            CB_Cramomatic_MenuClose_Direction.Enabled = false;
            CB_Cramomatic_MenuClose_Direction.Location = new Point(0, 236);
            CB_Cramomatic_MenuClose_Direction.Name = "CB_Cramomatic_MenuClose_Direction";
            CB_Cramomatic_MenuClose_Direction.Size = new Size(125, 19);
            CB_Cramomatic_MenuClose_Direction.TabIndex = 9;
            CB_Cramomatic_MenuClose_Direction.Tag = "";
            CB_Cramomatic_MenuClose_Direction.Text = "Holding Direction?";
            CB_Cramomatic_MenuClose_Direction.UseVisualStyleBackColor = true;
            // 
            // L_Cramomatic_NPCs
            // 
            L_Cramomatic_NPCs.AutoSize = true;
            L_Cramomatic_NPCs.Enabled = false;
            L_Cramomatic_NPCs.Location = new Point(126, 237);
            L_Cramomatic_NPCs.Name = "L_Cramomatic_NPCs";
            L_Cramomatic_NPCs.Size = new Size(39, 15);
            L_Cramomatic_NPCs.TabIndex = 52;
            L_Cramomatic_NPCs.Text = "NPCs:";
            // 
            // TB_Cramomatic_NPCs
            // 
            TB_Cramomatic_NPCs.CharacterCasing = CharacterCasing.Upper;
            TB_Cramomatic_NPCs.Enabled = false;
            TB_Cramomatic_NPCs.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Cramomatic_NPCs.Location = new Point(171, 235);
            TB_Cramomatic_NPCs.MaxLength = 2;
            TB_Cramomatic_NPCs.Name = "TB_Cramomatic_NPCs";
            TB_Cramomatic_NPCs.Size = new Size(35, 22);
            TB_Cramomatic_NPCs.TabIndex = 10;
            TB_Cramomatic_NPCs.Text = "22";
            TB_Cramomatic_NPCs.TextAlign = HorizontalAlignment.Right;
            // 
            // L_Timeline_Plus
            // 
            L_Timeline_Plus.AutoSize = true;
            L_Timeline_Plus.Location = new Point(67, 38);
            L_Timeline_Plus.Name = "L_Timeline_Plus";
            L_Timeline_Plus.Size = new Size(15, 15);
            L_Timeline_Plus.TabIndex = 24;
            L_Timeline_Plus.Text = "+";
            // 
            // B_Cramomatic_Search
            // 
            B_Cramomatic_Search.Location = new Point(6, 259);
            B_Cramomatic_Search.Name = "B_Cramomatic_Search";
            B_Cramomatic_Search.Size = new Size(200, 25);
            B_Cramomatic_Search.TabIndex = 11;
            B_Cramomatic_Search.Text = "Search!";
            B_Cramomatic_Search.UseVisualStyleBackColor = true;
            B_Cramomatic_Search.Click += B_Cramomatic_Search_Click;
            // 
            // L_Timeline_Initial
            // 
            L_Timeline_Initial.AutoSize = true;
            L_Timeline_Initial.Location = new Point(19, 15);
            L_Timeline_Initial.Name = "L_Timeline_Initial";
            L_Timeline_Initial.Size = new Size(63, 15);
            L_Timeline_Initial.TabIndex = 23;
            L_Timeline_Initial.Text = "Initial Adv.";
            // 
            // TB_Cramomatic_Advances
            // 
            TB_Cramomatic_Advances.CharacterCasing = CharacterCasing.Upper;
            TB_Cramomatic_Advances.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Cramomatic_Advances.Location = new Point(88, 36);
            TB_Cramomatic_Advances.MaxLength = 16;
            TB_Cramomatic_Advances.Name = "TB_Cramomatic_Advances";
            TB_Cramomatic_Advances.Size = new Size(118, 22);
            TB_Cramomatic_Advances.TabIndex = 1;
            TB_Cramomatic_Advances.Text = "5000";
            TB_Cramomatic_Advances.TextAlign = HorizontalAlignment.Right;
            // 
            // TB_Cramomatic_Initial
            // 
            TB_Cramomatic_Initial.CharacterCasing = CharacterCasing.Upper;
            TB_Cramomatic_Initial.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Cramomatic_Initial.Location = new Point(88, 13);
            TB_Cramomatic_Initial.MaxLength = 16;
            TB_Cramomatic_Initial.Name = "TB_Cramomatic_Initial";
            TB_Cramomatic_Initial.Size = new Size(118, 22);
            TB_Cramomatic_Initial.TabIndex = 0;
            TB_Cramomatic_Initial.Text = "0";
            TB_Cramomatic_Initial.TextAlign = HorizontalAlignment.Right;
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
            DGV_Results.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGV_Results.Columns.AddRange(new DataGridViewColumn[] { advancesDataGridViewTextBoxColumn, jumpDataGridViewTextBoxColumn, animationDataGridViewTextBoxColumn, prizeDataGridViewTextBoxColumn, bonusDataGridViewCheckBoxColumn, seed0DataGridViewTextBoxColumn, seed1DataGridViewTextBoxColumn });
            DGV_Results.DataSource = CramomaticResultsSource;
            DGV_Results.Location = new Point(218, 11);
            DGV_Results.Name = "DGV_Results";
            DGV_Results.ReadOnly = true;
            DGV_Results.RowHeadersVisible = false;
            DGV_Results.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV_Results.Size = new Size(570, 427);
            DGV_Results.TabIndex = 0;
            DGV_Results.CellFormatting += DGV_Results_CellFormatting;
            // 
            // advancesDataGridViewTextBoxColumn
            // 
            advancesDataGridViewTextBoxColumn.DataPropertyName = "Advances";
            advancesDataGridViewTextBoxColumn.HeaderText = "Advances";
            advancesDataGridViewTextBoxColumn.Name = "advancesDataGridViewTextBoxColumn";
            advancesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // jumpDataGridViewTextBoxColumn
            // 
            jumpDataGridViewTextBoxColumn.DataPropertyName = "Jump";
            jumpDataGridViewTextBoxColumn.HeaderText = "Jump";
            jumpDataGridViewTextBoxColumn.Name = "jumpDataGridViewTextBoxColumn";
            jumpDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // animationDataGridViewTextBoxColumn
            // 
            animationDataGridViewTextBoxColumn.DataPropertyName = "Animation";
            animationDataGridViewTextBoxColumn.HeaderText = "Animation";
            animationDataGridViewTextBoxColumn.Name = "animationDataGridViewTextBoxColumn";
            animationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // prizeDataGridViewTextBoxColumn
            // 
            prizeDataGridViewTextBoxColumn.DataPropertyName = "Prize";
            prizeDataGridViewTextBoxColumn.HeaderText = "Prize";
            prizeDataGridViewTextBoxColumn.Name = "prizeDataGridViewTextBoxColumn";
            prizeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bonusDataGridViewCheckBoxColumn
            // 
            bonusDataGridViewCheckBoxColumn.DataPropertyName = "Bonus";
            bonusDataGridViewCheckBoxColumn.HeaderText = "Bonus";
            bonusDataGridViewCheckBoxColumn.Name = "bonusDataGridViewCheckBoxColumn";
            bonusDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // seed0DataGridViewTextBoxColumn
            // 
            seed0DataGridViewTextBoxColumn.DataPropertyName = "Seed0";
            seed0DataGridViewTextBoxColumn.HeaderText = "Seed0";
            seed0DataGridViewTextBoxColumn.Name = "seed0DataGridViewTextBoxColumn";
            seed0DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // seed1DataGridViewTextBoxColumn
            // 
            seed1DataGridViewTextBoxColumn.DataPropertyName = "Seed1";
            seed1DataGridViewTextBoxColumn.HeaderText = "Seed1";
            seed1DataGridViewTextBoxColumn.Name = "seed1DataGridViewTextBoxColumn";
            seed1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // CramomaticResultsSource
            // 
            CramomaticResultsSource.DataSource = typeof(Core.Interfaces.CramomaticFrame);
            // 
            // Cramomatic
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DGV_Results);
            Controls.Add(GB_Seed);
            Controls.Add(GB_SearchSettings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Cramomatic";
            Text = "Cramomatic";
            FormClosing += MenuCloseTimeline_FormClosing;
            GB_Seed.ResumeLayout(false);
            GB_Seed.PerformLayout();
            GB_SearchSettings.ResumeLayout(false);
            GB_SearchSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).EndInit();
            ((System.ComponentModel.ISupportInitialize)CramomaticResultsSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox GB_Seed;
        private Label L_Seed1;
        private Label L_Seed0;
        private TextBox TB_Seed1;
        private TextBox TB_Seed0;
        private GroupBox GB_SearchSettings;
        private Label L_Timeline_Plus;
        private Button B_Cramomatic_Search;
        private Label L_Timeline_Initial;
        private TextBox TB_Cramomatic_Advances;
        private TextBox TB_Cramomatic_Initial;
        private CheckBox CB_Cramomatic_MenuClose_Direction;
        private Label L_Cramomatic_NPCs;
        private TextBox TB_Cramomatic_NPCs;
        private DataGridView DGV_Results;
        private Label L_Target;
        private ComboBox CB_Target;
        private CheckBox CB_Cramomatic_MenuClose;
        private Label L_Item1;
        private ComboBox CB_Item1;
        private CheckBox CB_BonusOnly;
        private Label L_Item4;
        private ComboBox CB_Item4;
        private Label L_Item3;
        private ComboBox CB_Item3;
        private Label L_Item2;
        private ComboBox CB_Item2;
        private DataGridViewTextBoxColumn advancesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn jumpDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn animationDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn prizeDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn bonusDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn seed0DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn seed1DataGridViewTextBoxColumn;
        private BindingSource CramomaticResultsSource;
    }
}