namespace owoow.WinForms.Subforms
{
    partial class WattTrader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WattTrader));
            GB_Seed = new GroupBox();
            L_Seed1 = new Label();
            L_Seed0 = new Label();
            TB_Seed1 = new TextBox();
            TB_Seed0 = new TextBox();
            GB_SearchSettings = new GroupBox();
            L_WattTrader_Weather = new Label();
            CB_WattTrader_Weather = new ComboBox();
            L_SlotRange = new Label();
            L_Slot_Spacer = new Label();
            TB_SlotMin = new TextBox();
            TB_SlotMax = new TextBox();
            CB_WattTrader_MenuClose = new CheckBox();
            L_Target = new Label();
            CB_Target = new ComboBox();
            CB_WattTrader_MenuClose_Direction = new CheckBox();
            L_WattTrader_NPCs = new Label();
            TB_WattTrader_NPCs = new TextBox();
            L_Timeline_Plus = new Label();
            B_WattTrader_Search = new Button();
            L_Timeline_Initial = new Label();
            TB_WattTrader_Advances = new TextBox();
            TB_WattTrader_Initial = new TextBox();
            DGV_Results = new DataGridView();
            advancesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            jumpDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            animationDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            highlightDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            regularDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            seed0DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            seed1DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            WattTraderResultsSource = new BindingSource(components);
            GB_Seed.SuspendLayout();
            GB_SearchSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WattTraderResultsSource).BeginInit();
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
            L_Seed1.Location = new Point(12, 35);
            L_Seed1.Name = "L_Seed1";
            L_Seed1.Size = new Size(49, 15);
            L_Seed1.TabIndex = 7;
            L_Seed1.Text = "Seed[1]:";
            // 
            // L_Seed0
            // 
            L_Seed0.AutoSize = true;
            L_Seed0.Location = new Point(12, 11);
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
            GB_SearchSettings.Controls.Add(L_WattTrader_Weather);
            GB_SearchSettings.Controls.Add(CB_WattTrader_Weather);
            GB_SearchSettings.Controls.Add(L_SlotRange);
            GB_SearchSettings.Controls.Add(L_Slot_Spacer);
            GB_SearchSettings.Controls.Add(TB_SlotMin);
            GB_SearchSettings.Controls.Add(TB_SlotMax);
            GB_SearchSettings.Controls.Add(CB_WattTrader_MenuClose);
            GB_SearchSettings.Controls.Add(L_Target);
            GB_SearchSettings.Controls.Add(CB_Target);
            GB_SearchSettings.Controls.Add(CB_WattTrader_MenuClose_Direction);
            GB_SearchSettings.Controls.Add(L_WattTrader_NPCs);
            GB_SearchSettings.Controls.Add(TB_WattTrader_NPCs);
            GB_SearchSettings.Controls.Add(L_Timeline_Plus);
            GB_SearchSettings.Controls.Add(B_WattTrader_Search);
            GB_SearchSettings.Controls.Add(L_Timeline_Initial);
            GB_SearchSettings.Controls.Add(TB_WattTrader_Advances);
            GB_SearchSettings.Controls.Add(TB_WattTrader_Initial);
            GB_SearchSettings.Location = new Point(0, 53);
            GB_SearchSettings.Name = "GB_SearchSettings";
            GB_SearchSettings.Size = new Size(212, 215);
            GB_SearchSettings.TabIndex = 3;
            GB_SearchSettings.TabStop = false;
            // 
            // L_WattTrader_Weather
            // 
            L_WattTrader_Weather.AutoSize = true;
            L_WattTrader_Weather.Location = new Point(12, 116);
            L_WattTrader_Weather.Name = "L_WattTrader_Weather";
            L_WattTrader_Weather.Size = new Size(54, 15);
            L_WattTrader_Weather.TabIndex = 71;
            L_WattTrader_Weather.Text = "Weather:";
            // 
            // CB_WattTrader_Weather
            // 
            CB_WattTrader_Weather.FormattingEnabled = true;
            CB_WattTrader_Weather.Items.AddRange(new object[] { "All Weather", "Normal Weather", "Overcast", "Raining", "Thunderstorm", "Intense Sun", "Snowing", "Snowstorm", "Sandstorm", "Heavy Fog" });
            CB_WattTrader_Weather.Location = new Point(88, 113);
            CB_WattTrader_Weather.Name = "CB_WattTrader_Weather";
            CB_WattTrader_Weather.Size = new Size(118, 23);
            CB_WattTrader_Weather.TabIndex = 5;
            CB_WattTrader_Weather.Text = "None";
            CB_WattTrader_Weather.Leave += CB_Leave;
            // 
            // L_SlotRange
            // 
            L_SlotRange.AutoSize = true;
            L_SlotRange.Location = new Point(12, 87);
            L_SlotRange.Name = "L_SlotRange";
            L_SlotRange.Size = new Size(66, 15);
            L_SlotRange.TabIndex = 69;
            L_SlotRange.Text = "Slot Range:";
            // 
            // L_Slot_Spacer
            // 
            L_Slot_Spacer.AutoSize = true;
            L_Slot_Spacer.Location = new Point(165, 87);
            L_Slot_Spacer.Name = "L_Slot_Spacer";
            L_Slot_Spacer.Size = new Size(12, 15);
            L_Slot_Spacer.TabIndex = 68;
            L_Slot_Spacer.Text = "-";
            // 
            // TB_SlotMin
            // 
            TB_SlotMin.CharacterCasing = CharacterCasing.Upper;
            TB_SlotMin.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_SlotMin.Location = new Point(136, 85);
            TB_SlotMin.MaxLength = 16;
            TB_SlotMin.Name = "TB_SlotMin";
            TB_SlotMin.Size = new Size(27, 22);
            TB_SlotMin.TabIndex = 3;
            TB_SlotMin.Text = "0";
            TB_SlotMin.TextAlign = HorizontalAlignment.Right;
            // 
            // TB_SlotMax
            // 
            TB_SlotMax.CharacterCasing = CharacterCasing.Upper;
            TB_SlotMax.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_SlotMax.Location = new Point(179, 85);
            TB_SlotMax.MaxLength = 16;
            TB_SlotMax.Name = "TB_SlotMax";
            TB_SlotMax.Size = new Size(27, 22);
            TB_SlotMax.TabIndex = 4;
            TB_SlotMax.Text = "999";
            TB_SlotMax.TextAlign = HorizontalAlignment.Right;
            // 
            // CB_WattTrader_MenuClose
            // 
            CB_WattTrader_MenuClose.AutoSize = true;
            CB_WattTrader_MenuClose.CheckAlign = ContentAlignment.MiddleRight;
            CB_WattTrader_MenuClose.Location = new Point(62, 138);
            CB_WattTrader_MenuClose.Name = "CB_WattTrader_MenuClose";
            CB_WattTrader_MenuClose.Size = new Size(144, 19);
            CB_WattTrader_MenuClose.TabIndex = 6;
            CB_WattTrader_MenuClose.Tag = "";
            CB_WattTrader_MenuClose.Text = "Consider Menu Close?";
            CB_WattTrader_MenuClose.UseVisualStyleBackColor = true;
            CB_WattTrader_MenuClose.CheckedChanged += CB_WattTrader_MenuClose_CheckedChanged;
            // 
            // L_Target
            // 
            L_Target.AutoSize = true;
            L_Target.Location = new Point(12, 63);
            L_Target.Name = "L_Target";
            L_Target.Size = new Size(42, 15);
            L_Target.TabIndex = 57;
            L_Target.Text = "Target:";
            // 
            // CB_Target
            // 
            CB_Target.FormattingEnabled = true;
            CB_Target.Items.AddRange(new object[] { "(None)", "Beast or Dream Ball", "Beast Ball x1", "Dream Ball x1", "Bottle Cap x1", "Bottle Cap x3", "Gold Bottle Cap x1", "Red Apricorn x5", "Blue Apricorn x5", "Yellow Apricorn x5", "Green Apricorn x5", "White Apricorn x5", "Black Apricorn x5", "Pink Apricorn x5", "Red Apricorn x10", "Blue Apricorn x10", "Yellow Apricorn x10", "Green Apricorn x10", "White Apricorn x10", "Black Apricorn x10", "Pink Apricorn x10", "PP Up x1", "PP Up x2", "PP Max x1", "Rare Candy x1", "Rare Candy x5", "Gigantamix x1", "Armorite Ore x1", "Armorite Ore x3", "Armorite Ore x8", "Dynite Ore x1", "Dynite Ore x5", "Max Mushrooms x1", "Max Elixir x1", "Galarica Twig x3", "Galarica Twig x5", "Strawberry Sweet x1", "Love Sweet x1", "Berry Sweet x1", "Clover Sweet x1", "Flower Sweet x1", "Star Sweet x1", "Ribbon Sweet x1", "Big Nugget x1", "Lansat Berry x1", "Starf Berry x1", "Lucky Egg x1", "Electirizer x1", "Magmarizer x1", "Cracked Pot x1", "Chipped Pot x1", "King's Rock x1" });
            CB_Target.Location = new Point(60, 60);
            CB_Target.Name = "CB_Target";
            CB_Target.Size = new Size(146, 23);
            CB_Target.TabIndex = 2;
            CB_Target.Text = "None";
            CB_Target.SelectedIndexChanged += CB_Target_SelectedIndexChanged;
            CB_Target.Leave += CB_Leave;
            // 
            // CB_WattTrader_MenuClose_Direction
            // 
            CB_WattTrader_MenuClose_Direction.AutoSize = true;
            CB_WattTrader_MenuClose_Direction.CheckAlign = ContentAlignment.MiddleRight;
            CB_WattTrader_MenuClose_Direction.Enabled = false;
            CB_WattTrader_MenuClose_Direction.Location = new Point(0, 160);
            CB_WattTrader_MenuClose_Direction.Name = "CB_WattTrader_MenuClose_Direction";
            CB_WattTrader_MenuClose_Direction.Size = new Size(125, 19);
            CB_WattTrader_MenuClose_Direction.TabIndex = 7;
            CB_WattTrader_MenuClose_Direction.Tag = "";
            CB_WattTrader_MenuClose_Direction.Text = "Holding Direction?";
            CB_WattTrader_MenuClose_Direction.UseVisualStyleBackColor = true;
            CB_WattTrader_MenuClose_Direction.CheckedChanged += CB_WattTrader_MenuClose_Direction_CheckedChanged;
            // 
            // L_WattTrader_NPCs
            // 
            L_WattTrader_NPCs.AutoSize = true;
            L_WattTrader_NPCs.Enabled = false;
            L_WattTrader_NPCs.Location = new Point(126, 161);
            L_WattTrader_NPCs.Name = "L_WattTrader_NPCs";
            L_WattTrader_NPCs.Size = new Size(39, 15);
            L_WattTrader_NPCs.TabIndex = 52;
            L_WattTrader_NPCs.Text = "NPCs:";
            // 
            // TB_WattTrader_NPCs
            // 
            TB_WattTrader_NPCs.CharacterCasing = CharacterCasing.Upper;
            TB_WattTrader_NPCs.Enabled = false;
            TB_WattTrader_NPCs.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_WattTrader_NPCs.Location = new Point(171, 159);
            TB_WattTrader_NPCs.MaxLength = 2;
            TB_WattTrader_NPCs.Name = "TB_WattTrader_NPCs";
            TB_WattTrader_NPCs.Size = new Size(35, 22);
            TB_WattTrader_NPCs.TabIndex = 8;
            TB_WattTrader_NPCs.Text = "4";
            TB_WattTrader_NPCs.TextAlign = HorizontalAlignment.Right;
            // 
            // L_Timeline_Plus
            // 
            L_Timeline_Plus.AutoSize = true;
            L_Timeline_Plus.Location = new Point(60, 38);
            L_Timeline_Plus.Name = "L_Timeline_Plus";
            L_Timeline_Plus.Size = new Size(15, 15);
            L_Timeline_Plus.TabIndex = 24;
            L_Timeline_Plus.Text = "+";
            // 
            // B_WattTrader_Search
            // 
            B_WattTrader_Search.Location = new Point(6, 183);
            B_WattTrader_Search.Name = "B_WattTrader_Search";
            B_WattTrader_Search.Size = new Size(200, 25);
            B_WattTrader_Search.TabIndex = 9;
            B_WattTrader_Search.Text = "Search!";
            B_WattTrader_Search.UseVisualStyleBackColor = true;
            B_WattTrader_Search.Click += B_WattTrader_Search_Click;
            // 
            // L_Timeline_Initial
            // 
            L_Timeline_Initial.AutoSize = true;
            L_Timeline_Initial.Location = new Point(12, 15);
            L_Timeline_Initial.Name = "L_Timeline_Initial";
            L_Timeline_Initial.Size = new Size(63, 15);
            L_Timeline_Initial.TabIndex = 23;
            L_Timeline_Initial.Text = "Initial Adv.";
            // 
            // TB_WattTrader_Advances
            // 
            TB_WattTrader_Advances.CharacterCasing = CharacterCasing.Upper;
            TB_WattTrader_Advances.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_WattTrader_Advances.Location = new Point(88, 36);
            TB_WattTrader_Advances.MaxLength = 16;
            TB_WattTrader_Advances.Name = "TB_WattTrader_Advances";
            TB_WattTrader_Advances.Size = new Size(118, 22);
            TB_WattTrader_Advances.TabIndex = 1;
            TB_WattTrader_Advances.Text = "5000";
            TB_WattTrader_Advances.TextAlign = HorizontalAlignment.Right;
            // 
            // TB_WattTrader_Initial
            // 
            TB_WattTrader_Initial.CharacterCasing = CharacterCasing.Upper;
            TB_WattTrader_Initial.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_WattTrader_Initial.Location = new Point(88, 13);
            TB_WattTrader_Initial.MaxLength = 16;
            TB_WattTrader_Initial.Name = "TB_WattTrader_Initial";
            TB_WattTrader_Initial.Size = new Size(118, 22);
            TB_WattTrader_Initial.TabIndex = 0;
            TB_WattTrader_Initial.Text = "0";
            TB_WattTrader_Initial.TextAlign = HorizontalAlignment.Right;
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
            DGV_Results.Columns.AddRange(new DataGridViewColumn[] { advancesDataGridViewTextBoxColumn, jumpDataGridViewTextBoxColumn, animationDataGridViewTextBoxColumn, highlightDataGridViewTextBoxColumn, regularDataGridViewTextBoxColumn, seed0DataGridViewTextBoxColumn, seed1DataGridViewTextBoxColumn });
            DGV_Results.DataSource = WattTraderResultsSource;
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
            // highlightDataGridViewTextBoxColumn
            // 
            highlightDataGridViewTextBoxColumn.DataPropertyName = "Highlight";
            highlightDataGridViewTextBoxColumn.HeaderText = "Highlight";
            highlightDataGridViewTextBoxColumn.Name = "highlightDataGridViewTextBoxColumn";
            highlightDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // regularDataGridViewTextBoxColumn
            // 
            regularDataGridViewTextBoxColumn.DataPropertyName = "Regular";
            regularDataGridViewTextBoxColumn.HeaderText = "Regular";
            regularDataGridViewTextBoxColumn.Name = "regularDataGridViewTextBoxColumn";
            regularDataGridViewTextBoxColumn.ReadOnly = true;
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
            // WattTraderResultsSource
            // 
            WattTraderResultsSource.DataSource = typeof(Core.Interfaces.WattTraderFrame);
            // 
            // WattTrader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DGV_Results);
            Controls.Add(GB_Seed);
            Controls.Add(GB_SearchSettings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "WattTrader";
            Text = "Watt Trader";
            FormClosing += MenuCloseTimeline_FormClosing;
            GB_Seed.ResumeLayout(false);
            GB_Seed.PerformLayout();
            GB_SearchSettings.ResumeLayout(false);
            GB_SearchSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).EndInit();
            ((System.ComponentModel.ISupportInitialize)WattTraderResultsSource).EndInit();
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
        private Button B_WattTrader_Search;
        private Label L_Timeline_Initial;
        private TextBox TB_WattTrader_Advances;
        private TextBox TB_WattTrader_Initial;
        private CheckBox CB_WattTrader_MenuClose_Direction;
        private Label L_WattTrader_NPCs;
        private TextBox TB_WattTrader_NPCs;
        private DataGridView DGV_Results;
        private Label L_Target;
        private ComboBox CB_Target;
        private CheckBox CB_WattTrader_MenuClose;
        private BindingSource WattTraderResultsSource;
        private DataGridViewTextBoxColumn advancesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn jumpDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn animationDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn highlightDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn regularDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn seed0DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn seed1DataGridViewTextBoxColumn;
        private TextBox TB_SlotMin;
        private TextBox TB_SlotMax;
        private Label L_SlotRange;
        private Label L_Slot_Spacer;
        private ComboBox CB_WattTrader_Weather;
        private Label L_WattTrader_Weather;
    }
}