﻿namespace owoow.WinForms.Subforms
{
    partial class MenuCloseTimeline
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuCloseTimeline));
            GB_Seed = new GroupBox();
            L_Seed1 = new Label();
            L_Seed0 = new Label();
            TB_Seed1 = new TextBox();
            TB_Seed0 = new TextBox();
            GB_SearchSettings = new GroupBox();
            CB_Timeline_MenuClose_Direction = new CheckBox();
            L_Symbol_NPCs = new Label();
            TB_Timeline_NPCs = new TextBox();
            label3 = new Label();
            B_Timeline_Search = new Button();
            label4 = new Label();
            TB_Timeline_Advances = new TextBox();
            TB_Timeline_Initial = new TextBox();
            DGV_Results = new DataGridView();
            advancesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            jumpDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            seed0DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            seed1DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            MenuCloseResultsSource = new BindingSource(components);
            GB_Seed.SuspendLayout();
            GB_SearchSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MenuCloseResultsSource).BeginInit();
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
            TB_Seed1.TabIndex = 5;
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
            TB_Seed0.TabIndex = 4;
            TB_Seed0.Text = "0123456789ABCDEF";
            // 
            // GB_SearchSettings
            // 
            GB_SearchSettings.Controls.Add(CB_Timeline_MenuClose_Direction);
            GB_SearchSettings.Controls.Add(L_Symbol_NPCs);
            GB_SearchSettings.Controls.Add(TB_Timeline_NPCs);
            GB_SearchSettings.Controls.Add(label3);
            GB_SearchSettings.Controls.Add(B_Timeline_Search);
            GB_SearchSettings.Controls.Add(label4);
            GB_SearchSettings.Controls.Add(TB_Timeline_Advances);
            GB_SearchSettings.Controls.Add(TB_Timeline_Initial);
            GB_SearchSettings.Location = new Point(0, 53);
            GB_SearchSettings.Name = "GB_SearchSettings";
            GB_SearchSettings.Size = new Size(212, 118);
            GB_SearchSettings.TabIndex = 3;
            GB_SearchSettings.TabStop = false;
            // 
            // CB_Timeline_MenuClose_Direction
            // 
            CB_Timeline_MenuClose_Direction.AutoSize = true;
            CB_Timeline_MenuClose_Direction.CheckAlign = ContentAlignment.MiddleRight;
            CB_Timeline_MenuClose_Direction.Location = new Point(0, 90);
            CB_Timeline_MenuClose_Direction.Name = "CB_Timeline_MenuClose_Direction";
            CB_Timeline_MenuClose_Direction.Size = new Size(125, 19);
            CB_Timeline_MenuClose_Direction.TabIndex = 53;
            CB_Timeline_MenuClose_Direction.Tag = "";
            CB_Timeline_MenuClose_Direction.Text = "Holding Direction?";
            CB_Timeline_MenuClose_Direction.UseVisualStyleBackColor = true;
            // 
            // L_Symbol_NPCs
            // 
            L_Symbol_NPCs.AutoSize = true;
            L_Symbol_NPCs.Location = new Point(126, 91);
            L_Symbol_NPCs.Name = "L_Symbol_NPCs";
            L_Symbol_NPCs.Size = new Size(39, 15);
            L_Symbol_NPCs.TabIndex = 52;
            L_Symbol_NPCs.Text = "NPCs:";
            // 
            // TB_Timeline_NPCs
            // 
            TB_Timeline_NPCs.CharacterCasing = CharacterCasing.Upper;
            TB_Timeline_NPCs.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Timeline_NPCs.Location = new Point(171, 89);
            TB_Timeline_NPCs.MaxLength = 16;
            TB_Timeline_NPCs.Name = "TB_Timeline_NPCs";
            TB_Timeline_NPCs.Size = new Size(35, 22);
            TB_Timeline_NPCs.TabIndex = 51;
            TB_Timeline_NPCs.Text = "3";
            TB_Timeline_NPCs.TextAlign = HorizontalAlignment.Right;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(67, 38);
            label3.Name = "label3";
            label3.Size = new Size(15, 15);
            label3.TabIndex = 24;
            label3.Text = "+";
            // 
            // B_Timeline_Search
            // 
            B_Timeline_Search.Location = new Point(6, 62);
            B_Timeline_Search.Name = "B_Timeline_Search";
            B_Timeline_Search.Size = new Size(200, 25);
            B_Timeline_Search.TabIndex = 25;
            B_Timeline_Search.Text = "Search!";
            B_Timeline_Search.UseVisualStyleBackColor = true;
            B_Timeline_Search.Click += B_Symbol_Search_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(19, 15);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 23;
            label4.Text = "Initial Adv.";
            // 
            // TB_Timeline_Advances
            // 
            TB_Timeline_Advances.CharacterCasing = CharacterCasing.Upper;
            TB_Timeline_Advances.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Timeline_Advances.Location = new Point(88, 36);
            TB_Timeline_Advances.MaxLength = 16;
            TB_Timeline_Advances.Name = "TB_Timeline_Advances";
            TB_Timeline_Advances.Size = new Size(118, 22);
            TB_Timeline_Advances.TabIndex = 22;
            TB_Timeline_Advances.Text = "5000";
            TB_Timeline_Advances.TextAlign = HorizontalAlignment.Right;
            // 
            // TB_Timeline_Initial
            // 
            TB_Timeline_Initial.CharacterCasing = CharacterCasing.Upper;
            TB_Timeline_Initial.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Timeline_Initial.Location = new Point(88, 13);
            TB_Timeline_Initial.MaxLength = 16;
            TB_Timeline_Initial.Name = "TB_Timeline_Initial";
            TB_Timeline_Initial.Size = new Size(118, 22);
            TB_Timeline_Initial.TabIndex = 21;
            TB_Timeline_Initial.Text = "0";
            TB_Timeline_Initial.TextAlign = HorizontalAlignment.Right;
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
            DGV_Results.Columns.AddRange(new DataGridViewColumn[] { advancesDataGridViewTextBoxColumn, jumpDataGridViewTextBoxColumn, seed0DataGridViewTextBoxColumn, seed1DataGridViewTextBoxColumn });
            DGV_Results.DataSource = MenuCloseResultsSource;
            DGV_Results.Location = new Point(218, 11);
            DGV_Results.Name = "DGV_Results";
            DGV_Results.ReadOnly = true;
            DGV_Results.RowHeadersVisible = false;
            DGV_Results.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV_Results.Size = new Size(570, 427);
            DGV_Results.TabIndex = 13;
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
            // MenuCloseResultsSource
            // 
            MenuCloseResultsSource.DataSource = typeof(Core.Interfaces.MenuCloseFrame);
            // 
            // MenuCloseTimeline
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DGV_Results);
            Controls.Add(GB_Seed);
            Controls.Add(GB_SearchSettings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MenuCloseTimeline";
            Text = "MenuCloseTimeline";
            FormClosing += MenuCloseTimeline_FormClosing;
            GB_Seed.ResumeLayout(false);
            GB_Seed.PerformLayout();
            GB_SearchSettings.ResumeLayout(false);
            GB_SearchSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).EndInit();
            ((System.ComponentModel.ISupportInitialize)MenuCloseResultsSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox GB_Seed;
        private Label L_Seed1;
        private Label L_Seed0;
        private TextBox TB_Seed1;
        private TextBox TB_Seed0;
        private GroupBox GB_SearchSettings;
        private Label label3;
        private Button B_Timeline_Search;
        private Label label4;
        private TextBox TB_Timeline_Advances;
        private TextBox TB_Timeline_Initial;
        private CheckBox CB_Timeline_MenuClose_Direction;
        private Label L_Symbol_NPCs;
        private TextBox TB_Timeline_NPCs;
        private DataGridView DGV_Results;
        private DataGridViewTextBoxColumn advancesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn jumpDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn seed0DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn seed1DataGridViewTextBoxColumn;
        private BindingSource MenuCloseResultsSource;
    }
}