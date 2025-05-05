namespace owoow.WinForms.Subforms
{
    partial class LotoID
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LotoID));
            GB_Seed = new GroupBox();
            L_Seed1 = new Label();
            L_Seed0 = new Label();
            TB_Seed1 = new TextBox();
            TB_Seed0 = new TextBox();
            GB_SearchSettings = new GroupBox();
            L_LoadedIDs = new Label();
            B_IDList = new Button();
            CB_LotoID_MenuClose = new CheckBox();
            L_Target = new Label();
            CB_Target = new ComboBox();
            CB_LotoID_MenuClose_Direction = new CheckBox();
            L_LotoID_NPCs = new Label();
            TB_LotoID_NPCs = new TextBox();
            L_Timeline_Plus = new Label();
            B_LotoID_Search = new Button();
            L_Timeline_Initial = new Label();
            TB_LotoID_Advances = new TextBox();
            TB_LotoID_Initial = new TextBox();
            DGV_Results = new DataGridView();
            advancesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            jumpDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            animationDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            iDDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            prizeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            seed0DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            seed1DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            LotoIDResultsSource = new BindingSource(components);
            GB_Seed.SuspendLayout();
            GB_SearchSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LotoIDResultsSource).BeginInit();
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
            GB_SearchSettings.Controls.Add(L_LoadedIDs);
            GB_SearchSettings.Controls.Add(B_IDList);
            GB_SearchSettings.Controls.Add(CB_LotoID_MenuClose);
            GB_SearchSettings.Controls.Add(L_Target);
            GB_SearchSettings.Controls.Add(CB_Target);
            GB_SearchSettings.Controls.Add(CB_LotoID_MenuClose_Direction);
            GB_SearchSettings.Controls.Add(L_LotoID_NPCs);
            GB_SearchSettings.Controls.Add(TB_LotoID_NPCs);
            GB_SearchSettings.Controls.Add(L_Timeline_Plus);
            GB_SearchSettings.Controls.Add(B_LotoID_Search);
            GB_SearchSettings.Controls.Add(L_Timeline_Initial);
            GB_SearchSettings.Controls.Add(TB_LotoID_Advances);
            GB_SearchSettings.Controls.Add(TB_LotoID_Initial);
            GB_SearchSettings.Location = new Point(0, 53);
            GB_SearchSettings.Name = "GB_SearchSettings";
            GB_SearchSettings.Size = new Size(212, 209);
            GB_SearchSettings.TabIndex = 3;
            GB_SearchSettings.TabStop = false;
            // 
            // L_LoadedIDs
            // 
            L_LoadedIDs.AutoSize = true;
            L_LoadedIDs.Location = new Point(129, 113);
            L_LoadedIDs.Name = "L_LoadedIDs";
            L_LoadedIDs.Size = new Size(77, 15);
            L_LoadedIDs.TabIndex = 67;
            L_LoadedIDs.Text = "Loaded IDs: 0";
            L_LoadedIDs.TextAlign = ContentAlignment.TopRight;
            // 
            // B_IDList
            // 
            B_IDList.Location = new Point(6, 85);
            B_IDList.Name = "B_IDList";
            B_IDList.Size = new Size(200, 25);
            B_IDList.TabIndex = 3;
            B_IDList.Text = "ID List";
            B_IDList.UseVisualStyleBackColor = true;
            B_IDList.Click += B_IDList_Click;
            // 
            // CB_LotoID_MenuClose
            // 
            CB_LotoID_MenuClose.AutoSize = true;
            CB_LotoID_MenuClose.CheckAlign = ContentAlignment.MiddleRight;
            CB_LotoID_MenuClose.Location = new Point(62, 131);
            CB_LotoID_MenuClose.Name = "CB_LotoID_MenuClose";
            CB_LotoID_MenuClose.Size = new Size(144, 19);
            CB_LotoID_MenuClose.TabIndex = 4;
            CB_LotoID_MenuClose.Tag = "";
            CB_LotoID_MenuClose.Text = "Consider Menu Close?";
            CB_LotoID_MenuClose.UseVisualStyleBackColor = true;
            CB_LotoID_MenuClose.CheckedChanged += CB_LotoID_MenuClose_CheckedChanged;
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
            CB_Target.Items.AddRange(new object[] { "Master Ball", "Rare Candy", "PP Max", "PP Up", "Moomoo Milk", "All" });
            CB_Target.Location = new Point(88, 60);
            CB_Target.Name = "CB_Target";
            CB_Target.Size = new Size(118, 23);
            CB_Target.TabIndex = 2;
            CB_Target.Text = "None";
            CB_Target.Leave += CB_Leave;
            // 
            // CB_LotoID_MenuClose_Direction
            // 
            CB_LotoID_MenuClose_Direction.AutoSize = true;
            CB_LotoID_MenuClose_Direction.CheckAlign = ContentAlignment.MiddleRight;
            CB_LotoID_MenuClose_Direction.Enabled = false;
            CB_LotoID_MenuClose_Direction.Location = new Point(0, 153);
            CB_LotoID_MenuClose_Direction.Name = "CB_LotoID_MenuClose_Direction";
            CB_LotoID_MenuClose_Direction.Size = new Size(125, 19);
            CB_LotoID_MenuClose_Direction.TabIndex = 5;
            CB_LotoID_MenuClose_Direction.Tag = "";
            CB_LotoID_MenuClose_Direction.Text = "Holding Direction?";
            CB_LotoID_MenuClose_Direction.UseVisualStyleBackColor = true;
            CB_LotoID_MenuClose_Direction.CheckedChanged += CB_LotoID_MenuClose_Direction_CheckedChanged;
            // 
            // L_LotoID_NPCs
            // 
            L_LotoID_NPCs.AutoSize = true;
            L_LotoID_NPCs.Enabled = false;
            L_LotoID_NPCs.Location = new Point(126, 154);
            L_LotoID_NPCs.Name = "L_LotoID_NPCs";
            L_LotoID_NPCs.Size = new Size(39, 15);
            L_LotoID_NPCs.TabIndex = 52;
            L_LotoID_NPCs.Text = "NPCs:";
            // 
            // TB_LotoID_NPCs
            // 
            TB_LotoID_NPCs.CharacterCasing = CharacterCasing.Upper;
            TB_LotoID_NPCs.Enabled = false;
            TB_LotoID_NPCs.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_LotoID_NPCs.Location = new Point(171, 152);
            TB_LotoID_NPCs.MaxLength = 16;
            TB_LotoID_NPCs.Name = "TB_LotoID_NPCs";
            TB_LotoID_NPCs.Size = new Size(35, 22);
            TB_LotoID_NPCs.TabIndex = 6;
            TB_LotoID_NPCs.Text = "3";
            TB_LotoID_NPCs.TextAlign = HorizontalAlignment.Right;
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
            // B_LotoID_Search
            // 
            B_LotoID_Search.Location = new Point(6, 176);
            B_LotoID_Search.Name = "B_LotoID_Search";
            B_LotoID_Search.Size = new Size(200, 25);
            B_LotoID_Search.TabIndex = 7;
            B_LotoID_Search.Text = "Search!";
            B_LotoID_Search.UseVisualStyleBackColor = true;
            B_LotoID_Search.Click += B_LotoID_Search_Click;
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
            // TB_LotoID_Advances
            // 
            TB_LotoID_Advances.CharacterCasing = CharacterCasing.Upper;
            TB_LotoID_Advances.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_LotoID_Advances.Location = new Point(88, 36);
            TB_LotoID_Advances.MaxLength = 16;
            TB_LotoID_Advances.Name = "TB_LotoID_Advances";
            TB_LotoID_Advances.Size = new Size(118, 22);
            TB_LotoID_Advances.TabIndex = 1;
            TB_LotoID_Advances.Text = "5000";
            TB_LotoID_Advances.TextAlign = HorizontalAlignment.Right;
            // 
            // TB_LotoID_Initial
            // 
            TB_LotoID_Initial.CharacterCasing = CharacterCasing.Upper;
            TB_LotoID_Initial.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_LotoID_Initial.Location = new Point(88, 13);
            TB_LotoID_Initial.MaxLength = 16;
            TB_LotoID_Initial.Name = "TB_LotoID_Initial";
            TB_LotoID_Initial.Size = new Size(118, 22);
            TB_LotoID_Initial.TabIndex = 0;
            TB_LotoID_Initial.Text = "0";
            TB_LotoID_Initial.TextAlign = HorizontalAlignment.Right;
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
            DGV_Results.Columns.AddRange(new DataGridViewColumn[] { advancesDataGridViewTextBoxColumn, jumpDataGridViewTextBoxColumn, animationDataGridViewTextBoxColumn, iDDataGridViewTextBoxColumn, prizeDataGridViewTextBoxColumn, seed0DataGridViewTextBoxColumn, seed1DataGridViewTextBoxColumn });
            DGV_Results.DataSource = LotoIDResultsSource;
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
            // iDDataGridViewTextBoxColumn
            // 
            iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            iDDataGridViewTextBoxColumn.HeaderText = "ID";
            iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            iDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // prizeDataGridViewTextBoxColumn
            // 
            prizeDataGridViewTextBoxColumn.DataPropertyName = "Prize";
            prizeDataGridViewTextBoxColumn.HeaderText = "Prize";
            prizeDataGridViewTextBoxColumn.Name = "prizeDataGridViewTextBoxColumn";
            prizeDataGridViewTextBoxColumn.ReadOnly = true;
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
            // LotoIDResultsSource
            // 
            LotoIDResultsSource.DataSource = typeof(Core.Interfaces.LotoIDFrame);
            // 
            // LotoID
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DGV_Results);
            Controls.Add(GB_Seed);
            Controls.Add(GB_SearchSettings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LotoID";
            Text = "Loto-ID";
            FormClosing += MenuCloseTimeline_FormClosing;
            GB_Seed.ResumeLayout(false);
            GB_Seed.PerformLayout();
            GB_SearchSettings.ResumeLayout(false);
            GB_SearchSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).EndInit();
            ((System.ComponentModel.ISupportInitialize)LotoIDResultsSource).EndInit();
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
        private Button B_LotoID_Search;
        private Label L_Timeline_Initial;
        private TextBox TB_LotoID_Advances;
        private TextBox TB_LotoID_Initial;
        private CheckBox CB_LotoID_MenuClose_Direction;
        private Label L_LotoID_NPCs;
        private TextBox TB_LotoID_NPCs;
        private DataGridView DGV_Results;
        private Label L_Target;
        private ComboBox CB_Target;
        private CheckBox CB_LotoID_MenuClose;
        private Button B_IDList;
        public Label L_LoadedIDs;
        private DataGridViewTextBoxColumn advancesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn jumpDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn animationDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn prizeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn seed0DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn seed1DataGridViewTextBoxColumn;
        private BindingSource LotoIDResultsSource;
    }
}