namespace owoow.WinForms.Subforms
{
    partial class DiggingPa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiggingPa));
            GB_Seed = new GroupBox();
            L_Seed1 = new Label();
            L_Seed0 = new Label();
            TB_Seed1 = new TextBox();
            TB_Seed0 = new TextBox();
            GB_SearchSettings = new GroupBox();
            TB_Target = new TextBox();
            L_DiggingPa_Weather = new Label();
            CB_DiggingPa_Weather = new ComboBox();
            CB_DiggingPa_MenuClose = new CheckBox();
            L_Target = new Label();
            CB_DiggingPa_MenuClose_Direction = new CheckBox();
            L_DiggingPa_NPCs = new Label();
            TB_DiggingPa_NPCs = new TextBox();
            L_Timeline_Plus = new Label();
            B_DiggingPa_Search = new Button();
            L_Timeline_Initial = new Label();
            TB_DiggingPa_Advances = new TextBox();
            TB_DiggingPa_Initial = new TextBox();
            DGV_Results = new DataGridView();
            advancesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            jumpDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            animationDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            actualDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            reportedDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            seed0DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            seed1DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            DiggingPaResultsSource = new BindingSource(components);
            GB_Seed.SuspendLayout();
            GB_SearchSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DiggingPaResultsSource).BeginInit();
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
            GB_SearchSettings.Controls.Add(TB_Target);
            GB_SearchSettings.Controls.Add(L_DiggingPa_Weather);
            GB_SearchSettings.Controls.Add(CB_DiggingPa_Weather);
            GB_SearchSettings.Controls.Add(CB_DiggingPa_MenuClose);
            GB_SearchSettings.Controls.Add(L_Target);
            GB_SearchSettings.Controls.Add(CB_DiggingPa_MenuClose_Direction);
            GB_SearchSettings.Controls.Add(L_DiggingPa_NPCs);
            GB_SearchSettings.Controls.Add(TB_DiggingPa_NPCs);
            GB_SearchSettings.Controls.Add(L_Timeline_Plus);
            GB_SearchSettings.Controls.Add(B_DiggingPa_Search);
            GB_SearchSettings.Controls.Add(L_Timeline_Initial);
            GB_SearchSettings.Controls.Add(TB_DiggingPa_Advances);
            GB_SearchSettings.Controls.Add(TB_DiggingPa_Initial);
            GB_SearchSettings.Location = new Point(0, 53);
            GB_SearchSettings.Name = "GB_SearchSettings";
            GB_SearchSettings.Size = new Size(212, 194);
            GB_SearchSettings.TabIndex = 3;
            GB_SearchSettings.TabStop = false;
            // 
            // TB_Target
            // 
            TB_Target.CharacterCasing = CharacterCasing.Upper;
            TB_Target.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Target.Location = new Point(88, 64);
            TB_Target.MaxLength = 16;
            TB_Target.Name = "TB_Target";
            TB_Target.Size = new Size(118, 22);
            TB_Target.TabIndex = 2;
            TB_Target.Text = "500000";
            TB_Target.TextAlign = HorizontalAlignment.Right;
            // 
            // L_DiggingPa_Weather
            // 
            L_DiggingPa_Weather.AutoSize = true;
            L_DiggingPa_Weather.Location = new Point(12, 95);
            L_DiggingPa_Weather.Name = "L_DiggingPa_Weather";
            L_DiggingPa_Weather.Size = new Size(54, 15);
            L_DiggingPa_Weather.TabIndex = 71;
            L_DiggingPa_Weather.Text = "Weather:";
            // 
            // CB_DiggingPa_Weather
            // 
            CB_DiggingPa_Weather.FormattingEnabled = true;
            CB_DiggingPa_Weather.Items.AddRange(new object[] { "All Weather", "Normal Weather", "Overcast", "Raining", "Thunderstorm", "Intense Sun", "Snowing", "Snowstorm", "Sandstorm", "Heavy Fog" });
            CB_DiggingPa_Weather.Location = new Point(88, 92);
            CB_DiggingPa_Weather.Name = "CB_DiggingPa_Weather";
            CB_DiggingPa_Weather.Size = new Size(118, 23);
            CB_DiggingPa_Weather.TabIndex = 3;
            CB_DiggingPa_Weather.Text = "None";
            CB_DiggingPa_Weather.Leave += CB_Leave;
            // 
            // CB_DiggingPa_MenuClose
            // 
            CB_DiggingPa_MenuClose.AutoSize = true;
            CB_DiggingPa_MenuClose.CheckAlign = ContentAlignment.MiddleRight;
            CB_DiggingPa_MenuClose.Location = new Point(62, 117);
            CB_DiggingPa_MenuClose.Name = "CB_DiggingPa_MenuClose";
            CB_DiggingPa_MenuClose.Size = new Size(144, 19);
            CB_DiggingPa_MenuClose.TabIndex = 4;
            CB_DiggingPa_MenuClose.Tag = "";
            CB_DiggingPa_MenuClose.Text = "Consider Menu Close?";
            CB_DiggingPa_MenuClose.UseVisualStyleBackColor = true;
            CB_DiggingPa_MenuClose.CheckedChanged += CB_DiggingPa_MenuClose_CheckedChanged;
            // 
            // L_Target
            // 
            L_Target.AutoSize = true;
            L_Target.Location = new Point(12, 66);
            L_Target.Name = "L_Target";
            L_Target.Size = new Size(67, 15);
            L_Target.TabIndex = 57;
            L_Target.Text = "Min. Watts:";
            // 
            // CB_DiggingPa_MenuClose_Direction
            // 
            CB_DiggingPa_MenuClose_Direction.AutoSize = true;
            CB_DiggingPa_MenuClose_Direction.CheckAlign = ContentAlignment.MiddleRight;
            CB_DiggingPa_MenuClose_Direction.Enabled = false;
            CB_DiggingPa_MenuClose_Direction.Location = new Point(0, 139);
            CB_DiggingPa_MenuClose_Direction.Name = "CB_DiggingPa_MenuClose_Direction";
            CB_DiggingPa_MenuClose_Direction.Size = new Size(125, 19);
            CB_DiggingPa_MenuClose_Direction.TabIndex = 5;
            CB_DiggingPa_MenuClose_Direction.Tag = "";
            CB_DiggingPa_MenuClose_Direction.Text = "Holding Direction?";
            CB_DiggingPa_MenuClose_Direction.UseVisualStyleBackColor = true;
            // 
            // L_DiggingPa_NPCs
            // 
            L_DiggingPa_NPCs.AutoSize = true;
            L_DiggingPa_NPCs.Enabled = false;
            L_DiggingPa_NPCs.Location = new Point(126, 140);
            L_DiggingPa_NPCs.Name = "L_DiggingPa_NPCs";
            L_DiggingPa_NPCs.Size = new Size(39, 15);
            L_DiggingPa_NPCs.TabIndex = 52;
            L_DiggingPa_NPCs.Text = "NPCs:";
            // 
            // TB_DiggingPa_NPCs
            // 
            TB_DiggingPa_NPCs.CharacterCasing = CharacterCasing.Upper;
            TB_DiggingPa_NPCs.Enabled = false;
            TB_DiggingPa_NPCs.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_DiggingPa_NPCs.Location = new Point(171, 138);
            TB_DiggingPa_NPCs.MaxLength = 2;
            TB_DiggingPa_NPCs.Name = "TB_DiggingPa_NPCs";
            TB_DiggingPa_NPCs.Size = new Size(35, 22);
            TB_DiggingPa_NPCs.TabIndex = 6;
            TB_DiggingPa_NPCs.Text = "4";
            TB_DiggingPa_NPCs.TextAlign = HorizontalAlignment.Right;
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
            // B_DiggingPa_Search
            // 
            B_DiggingPa_Search.Location = new Point(6, 162);
            B_DiggingPa_Search.Name = "B_DiggingPa_Search";
            B_DiggingPa_Search.Size = new Size(200, 25);
            B_DiggingPa_Search.TabIndex = 7;
            B_DiggingPa_Search.Text = "Search!";
            B_DiggingPa_Search.UseVisualStyleBackColor = true;
            B_DiggingPa_Search.Click += B_DiggingPa_Search_Click;
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
            // TB_DiggingPa_Advances
            // 
            TB_DiggingPa_Advances.CharacterCasing = CharacterCasing.Upper;
            TB_DiggingPa_Advances.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_DiggingPa_Advances.Location = new Point(88, 36);
            TB_DiggingPa_Advances.MaxLength = 16;
            TB_DiggingPa_Advances.Name = "TB_DiggingPa_Advances";
            TB_DiggingPa_Advances.Size = new Size(118, 22);
            TB_DiggingPa_Advances.TabIndex = 1;
            TB_DiggingPa_Advances.Text = "5000";
            TB_DiggingPa_Advances.TextAlign = HorizontalAlignment.Right;
            // 
            // TB_DiggingPa_Initial
            // 
            TB_DiggingPa_Initial.CharacterCasing = CharacterCasing.Upper;
            TB_DiggingPa_Initial.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_DiggingPa_Initial.Location = new Point(88, 13);
            TB_DiggingPa_Initial.MaxLength = 16;
            TB_DiggingPa_Initial.Name = "TB_DiggingPa_Initial";
            TB_DiggingPa_Initial.Size = new Size(118, 22);
            TB_DiggingPa_Initial.TabIndex = 0;
            TB_DiggingPa_Initial.Text = "0";
            TB_DiggingPa_Initial.TextAlign = HorizontalAlignment.Right;
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
            DGV_Results.Columns.AddRange(new DataGridViewColumn[] { advancesDataGridViewTextBoxColumn, jumpDataGridViewTextBoxColumn, animationDataGridViewTextBoxColumn, actualDataGridViewTextBoxColumn, reportedDataGridViewTextBoxColumn, seed0DataGridViewTextBoxColumn, seed1DataGridViewTextBoxColumn });
            DGV_Results.DataSource = DiggingPaResultsSource;
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
            // actualDataGridViewTextBoxColumn
            // 
            actualDataGridViewTextBoxColumn.DataPropertyName = "Actual";
            actualDataGridViewTextBoxColumn.HeaderText = "Total Watts";
            actualDataGridViewTextBoxColumn.Name = "actualDataGridViewTextBoxColumn";
            actualDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // reportedDataGridViewTextBoxColumn
            // 
            reportedDataGridViewTextBoxColumn.DataPropertyName = "Reported";
            reportedDataGridViewTextBoxColumn.HeaderText = "Counted Watts";
            reportedDataGridViewTextBoxColumn.Name = "reportedDataGridViewTextBoxColumn";
            reportedDataGridViewTextBoxColumn.ReadOnly = true;
            reportedDataGridViewTextBoxColumn.Width = 120;
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
            // DiggingPaResultsSource
            // 
            DiggingPaResultsSource.DataSource = typeof(Core.Interfaces.DiggingPaFrame);
            // 
            // DiggingPa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DGV_Results);
            Controls.Add(GB_Seed);
            Controls.Add(GB_SearchSettings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DiggingPa";
            Text = "DiggingPa";
            FormClosing += MenuCloseTimeline_FormClosing;
            GB_Seed.ResumeLayout(false);
            GB_Seed.PerformLayout();
            GB_SearchSettings.ResumeLayout(false);
            GB_SearchSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).EndInit();
            ((System.ComponentModel.ISupportInitialize)DiggingPaResultsSource).EndInit();
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
        private Button B_DiggingPa_Search;
        private Label L_Timeline_Initial;
        private TextBox TB_DiggingPa_Advances;
        private TextBox TB_DiggingPa_Initial;
        private CheckBox CB_DiggingPa_MenuClose_Direction;
        private Label L_DiggingPa_NPCs;
        private TextBox TB_DiggingPa_NPCs;
        private DataGridView DGV_Results;
        private Label L_Target;
        private CheckBox CB_DiggingPa_MenuClose;
        private ComboBox CB_DiggingPa_Weather;
        private Label L_DiggingPa_Weather;
        private TextBox TB_Target;
        private BindingSource DiggingPaResultsSource;
        private DataGridViewTextBoxColumn advancesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn jumpDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn animationDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn actualDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn reportedDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn seed0DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn seed1DataGridViewTextBoxColumn;
    }
}