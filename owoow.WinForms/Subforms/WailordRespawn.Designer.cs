namespace owoow.WinForms.Subforms
{
    partial class WailordRespawn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WailordRespawn));
            GB_Seed = new GroupBox();
            L_Seed1 = new Label();
            L_Seed0 = new Label();
            TB_Seed1 = new TextBox();
            TB_Seed0 = new TextBox();
            GB_SearchSettings = new GroupBox();
            CB_Wailord_MenuClose = new CheckBox();
            L_Target = new Label();
            CB_Target = new ComboBox();
            CB_Wailord_MenuClose_Direction = new CheckBox();
            L_Wailord_NPCs = new Label();
            TB_Wailord_NPCs = new TextBox();
            L_Timeline_Plus = new Label();
            B_Wailord_Search = new Button();
            L_Timeline_Initial = new Label();
            TB_Wailord_Advances = new TextBox();
            TB_Wailord_Initial = new TextBox();
            DGV_Results = new DataGridView();
            advancesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            jumpDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            animationDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            respawnDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            seed0DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            seed1DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            WailordResultsSource = new BindingSource(components);
            GB_Seed.SuspendLayout();
            GB_SearchSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WailordResultsSource).BeginInit();
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
            GB_SearchSettings.Controls.Add(CB_Wailord_MenuClose);
            GB_SearchSettings.Controls.Add(L_Target);
            GB_SearchSettings.Controls.Add(CB_Target);
            GB_SearchSettings.Controls.Add(CB_Wailord_MenuClose_Direction);
            GB_SearchSettings.Controls.Add(L_Wailord_NPCs);
            GB_SearchSettings.Controls.Add(TB_Wailord_NPCs);
            GB_SearchSettings.Controls.Add(L_Timeline_Plus);
            GB_SearchSettings.Controls.Add(B_Wailord_Search);
            GB_SearchSettings.Controls.Add(L_Timeline_Initial);
            GB_SearchSettings.Controls.Add(TB_Wailord_Advances);
            GB_SearchSettings.Controls.Add(TB_Wailord_Initial);
            GB_SearchSettings.Location = new Point(0, 53);
            GB_SearchSettings.Name = "GB_SearchSettings";
            GB_SearchSettings.Size = new Size(212, 166);
            GB_SearchSettings.TabIndex = 3;
            GB_SearchSettings.TabStop = false;
            // 
            // CB_Wailord_MenuClose
            // 
            CB_Wailord_MenuClose.AutoSize = true;
            CB_Wailord_MenuClose.CheckAlign = ContentAlignment.MiddleRight;
            CB_Wailord_MenuClose.Location = new Point(62, 89);
            CB_Wailord_MenuClose.Name = "CB_Wailord_MenuClose";
            CB_Wailord_MenuClose.Size = new Size(144, 19);
            CB_Wailord_MenuClose.TabIndex = 3;
            CB_Wailord_MenuClose.Tag = "";
            CB_Wailord_MenuClose.Text = "Consider Menu Close?";
            CB_Wailord_MenuClose.UseVisualStyleBackColor = true;
            CB_Wailord_MenuClose.CheckedChanged += CB_Wailord_MenuClose_CheckedChanged;
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
            CB_Target.Items.AddRange(new object[] { "Success", "Fail", "All" });
            CB_Target.Location = new Point(88, 60);
            CB_Target.Name = "CB_Target";
            CB_Target.Size = new Size(118, 23);
            CB_Target.TabIndex = 2;
            CB_Target.Text = "None";
            CB_Target.Leave += CB_Leave;
            // 
            // CB_Wailord_MenuClose_Direction
            // 
            CB_Wailord_MenuClose_Direction.AutoSize = true;
            CB_Wailord_MenuClose_Direction.CheckAlign = ContentAlignment.MiddleRight;
            CB_Wailord_MenuClose_Direction.Enabled = false;
            CB_Wailord_MenuClose_Direction.Location = new Point(0, 111);
            CB_Wailord_MenuClose_Direction.Name = "CB_Wailord_MenuClose_Direction";
            CB_Wailord_MenuClose_Direction.Size = new Size(125, 19);
            CB_Wailord_MenuClose_Direction.TabIndex = 4;
            CB_Wailord_MenuClose_Direction.Tag = "";
            CB_Wailord_MenuClose_Direction.Text = "Holding Direction?";
            CB_Wailord_MenuClose_Direction.UseVisualStyleBackColor = true;
            // 
            // L_Wailord_NPCs
            // 
            L_Wailord_NPCs.AutoSize = true;
            L_Wailord_NPCs.Enabled = false;
            L_Wailord_NPCs.Location = new Point(126, 112);
            L_Wailord_NPCs.Name = "L_Wailord_NPCs";
            L_Wailord_NPCs.Size = new Size(39, 15);
            L_Wailord_NPCs.TabIndex = 52;
            L_Wailord_NPCs.Text = "NPCs:";
            // 
            // TB_Wailord_NPCs
            // 
            TB_Wailord_NPCs.CharacterCasing = CharacterCasing.Upper;
            TB_Wailord_NPCs.Enabled = false;
            TB_Wailord_NPCs.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Wailord_NPCs.Location = new Point(171, 110);
            TB_Wailord_NPCs.MaxLength = 16;
            TB_Wailord_NPCs.Name = "TB_Wailord_NPCs";
            TB_Wailord_NPCs.Size = new Size(35, 22);
            TB_Wailord_NPCs.TabIndex = 5;
            TB_Wailord_NPCs.Text = "3";
            TB_Wailord_NPCs.TextAlign = HorizontalAlignment.Right;
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
            // B_Wailord_Search
            // 
            B_Wailord_Search.Location = new Point(6, 134);
            B_Wailord_Search.Name = "B_Wailord_Search";
            B_Wailord_Search.Size = new Size(200, 25);
            B_Wailord_Search.TabIndex = 6;
            B_Wailord_Search.Text = "Search!";
            B_Wailord_Search.UseVisualStyleBackColor = true;
            B_Wailord_Search.Click += B_Wailord_Search_Click;
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
            // TB_Wailord_Advances
            // 
            TB_Wailord_Advances.CharacterCasing = CharacterCasing.Upper;
            TB_Wailord_Advances.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Wailord_Advances.Location = new Point(88, 36);
            TB_Wailord_Advances.MaxLength = 16;
            TB_Wailord_Advances.Name = "TB_Wailord_Advances";
            TB_Wailord_Advances.Size = new Size(118, 22);
            TB_Wailord_Advances.TabIndex = 1;
            TB_Wailord_Advances.Text = "5000";
            TB_Wailord_Advances.TextAlign = HorizontalAlignment.Right;
            // 
            // TB_Wailord_Initial
            // 
            TB_Wailord_Initial.CharacterCasing = CharacterCasing.Upper;
            TB_Wailord_Initial.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Wailord_Initial.Location = new Point(88, 13);
            TB_Wailord_Initial.MaxLength = 16;
            TB_Wailord_Initial.Name = "TB_Wailord_Initial";
            TB_Wailord_Initial.Size = new Size(118, 22);
            TB_Wailord_Initial.TabIndex = 0;
            TB_Wailord_Initial.Text = "0";
            TB_Wailord_Initial.TextAlign = HorizontalAlignment.Right;
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
            DGV_Results.Columns.AddRange(new DataGridViewColumn[] { advancesDataGridViewTextBoxColumn, jumpDataGridViewTextBoxColumn, animationDataGridViewTextBoxColumn, respawnDataGridViewTextBoxColumn, seed0DataGridViewTextBoxColumn, seed1DataGridViewTextBoxColumn });
            DGV_Results.DataSource = WailordResultsSource;
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
            // respawnDataGridViewTextBoxColumn
            // 
            respawnDataGridViewTextBoxColumn.DataPropertyName = "Respawn";
            respawnDataGridViewTextBoxColumn.HeaderText = "Respawn";
            respawnDataGridViewTextBoxColumn.Name = "respawnDataGridViewTextBoxColumn";
            respawnDataGridViewTextBoxColumn.ReadOnly = true;
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
            // WailordResultsSource
            // 
            WailordResultsSource.DataSource = typeof(Core.Interfaces.WailordFrame);
            // 
            // WailordRespawn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DGV_Results);
            Controls.Add(GB_Seed);
            Controls.Add(GB_SearchSettings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "WailordRespawn";
            Text = "WailordRespawn";
            FormClosing += MenuCloseTimeline_FormClosing;
            GB_Seed.ResumeLayout(false);
            GB_Seed.PerformLayout();
            GB_SearchSettings.ResumeLayout(false);
            GB_SearchSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).EndInit();
            ((System.ComponentModel.ISupportInitialize)WailordResultsSource).EndInit();
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
        private Button B_Wailord_Search;
        private Label L_Timeline_Initial;
        private TextBox TB_Wailord_Advances;
        private TextBox TB_Wailord_Initial;
        private CheckBox CB_Wailord_MenuClose_Direction;
        private Label L_Wailord_NPCs;
        private TextBox TB_Wailord_NPCs;
        private DataGridView DGV_Results;
        private Label L_Target;
        private ComboBox CB_Target;
        private CheckBox CB_Wailord_MenuClose;
        private DataGridViewTextBoxColumn advancesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn jumpDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn animationDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn respawnDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn seed0DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn seed1DataGridViewTextBoxColumn;
        private BindingSource WailordResultsSource;
    }
}