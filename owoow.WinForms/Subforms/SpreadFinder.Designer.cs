namespace owoow.WinForms.Subforms
{
    partial class SpreadFinder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpreadFinder));
            DGV_Results = new DataGridView();
            seedDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            eCDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            hDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            aDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            bDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            cDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            sDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            heightDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            SpreadFinderResultsSource = new BindingSource(components);
            CB_RareEC = new CheckBox();
            L_Filter_Height = new Label();
            CB_Filter_Height = new ComboBox();
            B_Spe_Max = new Button();
            B_Spe_Min = new Button();
            L_Spe = new Label();
            L_SpeSpacer = new Label();
            NUD_Spe_Max = new NumericUpDown();
            NUD_Spe_Min = new NumericUpDown();
            B_SpD_Max = new Button();
            B_SpD_Min = new Button();
            L_SpD = new Label();
            L_SpDSpacer = new Label();
            NUD_SpD_Max = new NumericUpDown();
            NUD_SpD_Min = new NumericUpDown();
            B_SpA_Max = new Button();
            B_SpA_Min = new Button();
            L_SpA = new Label();
            L_SpASpacer = new Label();
            NUD_SpA_Max = new NumericUpDown();
            NUD_SpA_Min = new NumericUpDown();
            B_Def_Max = new Button();
            B_Def_Min = new Button();
            L_Def = new Label();
            L_DefSpacer = new Label();
            NUD_Def_Max = new NumericUpDown();
            NUD_Def_Min = new NumericUpDown();
            B_Atk_Max = new Button();
            B_Atk_Min = new Button();
            L_Atk = new Label();
            L_AtkSpacer = new Label();
            NUD_Atk_Max = new NumericUpDown();
            NUD_Atk_Min = new NumericUpDown();
            B_HP_Max = new Button();
            B_HP_Min = new Button();
            L_HP = new Label();
            L_HPSpacer = new Label();
            NUD_HP_Max = new NumericUpDown();
            NUD_HP_Min = new NumericUpDown();
            label1 = new Label();
            numericUpDown1 = new NumericUpDown();
            B_Search = new Button();
            CB_Tasks = new ComboBox();
            L_Tasks = new Label();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpreadFinderResultsSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Spe_Max).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Spe_Min).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_SpD_Max).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_SpD_Min).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_SpA_Max).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_SpA_Min).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Def_Max).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Def_Min).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Atk_Max).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Atk_Min).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_HP_Max).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_HP_Min).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
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
            DGV_Results.Columns.AddRange(new DataGridViewColumn[] { seedDataGridViewTextBoxColumn, eCDataGridViewTextBoxColumn, hDataGridViewTextBoxColumn, aDataGridViewTextBoxColumn, bDataGridViewTextBoxColumn, cDataGridViewTextBoxColumn, dDataGridViewTextBoxColumn, sDataGridViewTextBoxColumn, heightDataGridViewTextBoxColumn });
            DGV_Results.DataSource = SpreadFinderResultsSource;
            DGV_Results.Location = new Point(218, 11);
            DGV_Results.Name = "DGV_Results";
            DGV_Results.ReadOnly = true;
            DGV_Results.RowHeadersVisible = false;
            DGV_Results.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV_Results.Size = new Size(570, 427);
            DGV_Results.TabIndex = 29;
            DGV_Results.CellFormatting += DGV_Results_CellFormatting;
            // 
            // seedDataGridViewTextBoxColumn
            // 
            seedDataGridViewTextBoxColumn.DataPropertyName = "Seed";
            seedDataGridViewTextBoxColumn.HeaderText = "Seed";
            seedDataGridViewTextBoxColumn.Name = "seedDataGridViewTextBoxColumn";
            seedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // eCDataGridViewTextBoxColumn
            // 
            eCDataGridViewTextBoxColumn.DataPropertyName = "EC";
            eCDataGridViewTextBoxColumn.HeaderText = "EC";
            eCDataGridViewTextBoxColumn.Name = "eCDataGridViewTextBoxColumn";
            eCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // hDataGridViewTextBoxColumn
            // 
            hDataGridViewTextBoxColumn.DataPropertyName = "H";
            hDataGridViewTextBoxColumn.HeaderText = "HP";
            hDataGridViewTextBoxColumn.Name = "hDataGridViewTextBoxColumn";
            hDataGridViewTextBoxColumn.ReadOnly = true;
            hDataGridViewTextBoxColumn.Width = 48;
            // 
            // aDataGridViewTextBoxColumn
            // 
            aDataGridViewTextBoxColumn.DataPropertyName = "A";
            aDataGridViewTextBoxColumn.HeaderText = "Atk";
            aDataGridViewTextBoxColumn.Name = "aDataGridViewTextBoxColumn";
            aDataGridViewTextBoxColumn.ReadOnly = true;
            aDataGridViewTextBoxColumn.Width = 48;
            // 
            // bDataGridViewTextBoxColumn
            // 
            bDataGridViewTextBoxColumn.DataPropertyName = "B";
            bDataGridViewTextBoxColumn.HeaderText = "Def";
            bDataGridViewTextBoxColumn.Name = "bDataGridViewTextBoxColumn";
            bDataGridViewTextBoxColumn.ReadOnly = true;
            bDataGridViewTextBoxColumn.Width = 48;
            // 
            // cDataGridViewTextBoxColumn
            // 
            cDataGridViewTextBoxColumn.DataPropertyName = "C";
            cDataGridViewTextBoxColumn.HeaderText = "SpA";
            cDataGridViewTextBoxColumn.Name = "cDataGridViewTextBoxColumn";
            cDataGridViewTextBoxColumn.ReadOnly = true;
            cDataGridViewTextBoxColumn.Width = 48;
            // 
            // dDataGridViewTextBoxColumn
            // 
            dDataGridViewTextBoxColumn.DataPropertyName = "D";
            dDataGridViewTextBoxColumn.HeaderText = "SpD";
            dDataGridViewTextBoxColumn.Name = "dDataGridViewTextBoxColumn";
            dDataGridViewTextBoxColumn.ReadOnly = true;
            dDataGridViewTextBoxColumn.Width = 48;
            // 
            // sDataGridViewTextBoxColumn
            // 
            sDataGridViewTextBoxColumn.DataPropertyName = "S";
            sDataGridViewTextBoxColumn.HeaderText = "Spe";
            sDataGridViewTextBoxColumn.Name = "sDataGridViewTextBoxColumn";
            sDataGridViewTextBoxColumn.ReadOnly = true;
            sDataGridViewTextBoxColumn.Width = 48;
            // 
            // heightDataGridViewTextBoxColumn
            // 
            heightDataGridViewTextBoxColumn.DataPropertyName = "Height";
            heightDataGridViewTextBoxColumn.HeaderText = "Height";
            heightDataGridViewTextBoxColumn.Name = "heightDataGridViewTextBoxColumn";
            heightDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // SpreadFinderResultsSource
            // 
            SpreadFinderResultsSource.DataSource = typeof(Core.Interfaces.SpreadFinderFrame);
            // 
            // CB_RareEC
            // 
            CB_RareEC.AutoSize = true;
            CB_RareEC.CheckAlign = ContentAlignment.MiddleRight;
            CB_RareEC.Location = new Point(12, 194);
            CB_RareEC.Name = "CB_RareEC";
            CB_RareEC.Size = new Size(71, 19);
            CB_RareEC.TabIndex = 25;
            CB_RareEC.Tag = "";
            CB_RareEC.Text = "Rare EC?";
            CB_RareEC.UseVisualStyleBackColor = true;
            // 
            // L_Filter_Height
            // 
            L_Filter_Height.AutoSize = true;
            L_Filter_Height.Location = new Point(12, 171);
            L_Filter_Height.Name = "L_Filter_Height";
            L_Filter_Height.Size = new Size(46, 15);
            L_Filter_Height.TabIndex = 95;
            L_Filter_Height.Text = "Height:";
            // 
            // CB_Filter_Height
            // 
            CB_Filter_Height.FormattingEnabled = true;
            CB_Filter_Height.Items.AddRange(new object[] { "Ignore", "XXXS", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "XXXS or XXXL" });
            CB_Filter_Height.Location = new Point(74, 168);
            CB_Filter_Height.Name = "CB_Filter_Height";
            CB_Filter_Height.Size = new Size(138, 23);
            CB_Filter_Height.TabIndex = 24;
            // 
            // B_Spe_Max
            // 
            B_Spe_Max.Location = new Point(185, 137);
            B_Spe_Max.Name = "B_Spe_Max";
            B_Spe_Max.Size = new Size(27, 25);
            B_Spe_Max.TabIndex = 23;
            B_Spe_Max.Text = "31";
            B_Spe_Max.UseVisualStyleBackColor = true;
            B_Spe_Max.Click += B_IV_Max_Click;
            // 
            // B_Spe_Min
            // 
            B_Spe_Min.Location = new Point(156, 137);
            B_Spe_Min.Name = "B_Spe_Min";
            B_Spe_Min.Size = new Size(27, 25);
            B_Spe_Min.TabIndex = 22;
            B_Spe_Min.Text = "0";
            B_Spe_Min.UseVisualStyleBackColor = true;
            B_Spe_Min.Click += B_IV_Min_Click;
            // 
            // L_Spe
            // 
            L_Spe.AutoSize = true;
            L_Spe.Location = new Point(12, 140);
            L_Spe.Name = "L_Spe";
            L_Spe.Size = new Size(29, 15);
            L_Spe.TabIndex = 91;
            L_Spe.Text = "Spe:";
            L_Spe.Click += IV_Label_Click;
            // 
            // L_SpeSpacer
            // 
            L_SpeSpacer.AutoSize = true;
            L_SpeSpacer.Location = new Point(107, 140);
            L_SpeSpacer.Name = "L_SpeSpacer";
            L_SpeSpacer.Size = new Size(12, 15);
            L_SpeSpacer.TabIndex = 65;
            L_SpeSpacer.Text = "-";
            // 
            // NUD_Spe_Max
            // 
            NUD_Spe_Max.Location = new Point(119, 138);
            NUD_Spe_Max.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            NUD_Spe_Max.Name = "NUD_Spe_Max";
            NUD_Spe_Max.Size = new Size(32, 23);
            NUD_Spe_Max.TabIndex = 11;
            NUD_Spe_Max.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // NUD_Spe_Min
            // 
            NUD_Spe_Min.Location = new Point(74, 138);
            NUD_Spe_Min.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            NUD_Spe_Min.Name = "NUD_Spe_Min";
            NUD_Spe_Min.Size = new Size(32, 23);
            NUD_Spe_Min.TabIndex = 10;
            // 
            // B_SpD_Max
            // 
            B_SpD_Max.Location = new Point(185, 112);
            B_SpD_Max.Name = "B_SpD_Max";
            B_SpD_Max.Size = new Size(27, 25);
            B_SpD_Max.TabIndex = 21;
            B_SpD_Max.Text = "31";
            B_SpD_Max.UseVisualStyleBackColor = true;
            B_SpD_Max.Click += B_IV_Max_Click;
            // 
            // B_SpD_Min
            // 
            B_SpD_Min.Location = new Point(156, 112);
            B_SpD_Min.Name = "B_SpD_Min";
            B_SpD_Min.Size = new Size(27, 25);
            B_SpD_Min.TabIndex = 20;
            B_SpD_Min.Text = "0";
            B_SpD_Min.UseVisualStyleBackColor = true;
            B_SpD_Min.Click += B_IV_Min_Click;
            // 
            // L_SpD
            // 
            L_SpD.AutoSize = true;
            L_SpD.Location = new Point(12, 115);
            L_SpD.Name = "L_SpD";
            L_SpD.Size = new Size(31, 15);
            L_SpD.TabIndex = 89;
            L_SpD.Text = "SpD:";
            L_SpD.Click += IV_Label_Click;
            // 
            // L_SpDSpacer
            // 
            L_SpDSpacer.AutoSize = true;
            L_SpDSpacer.Location = new Point(107, 115);
            L_SpDSpacer.Name = "L_SpDSpacer";
            L_SpDSpacer.Size = new Size(12, 15);
            L_SpDSpacer.TabIndex = 62;
            L_SpDSpacer.Text = "-";
            // 
            // NUD_SpD_Max
            // 
            NUD_SpD_Max.Location = new Point(119, 113);
            NUD_SpD_Max.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            NUD_SpD_Max.Name = "NUD_SpD_Max";
            NUD_SpD_Max.Size = new Size(32, 23);
            NUD_SpD_Max.TabIndex = 9;
            NUD_SpD_Max.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // NUD_SpD_Min
            // 
            NUD_SpD_Min.Location = new Point(74, 113);
            NUD_SpD_Min.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            NUD_SpD_Min.Name = "NUD_SpD_Min";
            NUD_SpD_Min.Size = new Size(32, 23);
            NUD_SpD_Min.TabIndex = 8;
            // 
            // B_SpA_Max
            // 
            B_SpA_Max.Location = new Point(185, 87);
            B_SpA_Max.Name = "B_SpA_Max";
            B_SpA_Max.Size = new Size(27, 25);
            B_SpA_Max.TabIndex = 19;
            B_SpA_Max.Text = "31";
            B_SpA_Max.UseVisualStyleBackColor = true;
            B_SpA_Max.Click += B_IV_Max_Click;
            // 
            // B_SpA_Min
            // 
            B_SpA_Min.Location = new Point(156, 87);
            B_SpA_Min.Name = "B_SpA_Min";
            B_SpA_Min.Size = new Size(27, 25);
            B_SpA_Min.TabIndex = 18;
            B_SpA_Min.Text = "0";
            B_SpA_Min.UseVisualStyleBackColor = true;
            B_SpA_Min.Click += B_IV_Min_Click;
            // 
            // L_SpA
            // 
            L_SpA.AutoSize = true;
            L_SpA.Location = new Point(12, 90);
            L_SpA.Name = "L_SpA";
            L_SpA.Size = new Size(31, 15);
            L_SpA.TabIndex = 82;
            L_SpA.Text = "SpA:";
            L_SpA.Click += IV_Label_Click;
            // 
            // L_SpASpacer
            // 
            L_SpASpacer.AutoSize = true;
            L_SpASpacer.Location = new Point(107, 90);
            L_SpASpacer.Name = "L_SpASpacer";
            L_SpASpacer.Size = new Size(12, 15);
            L_SpASpacer.TabIndex = 60;
            L_SpASpacer.Text = "-";
            // 
            // NUD_SpA_Max
            // 
            NUD_SpA_Max.Location = new Point(119, 88);
            NUD_SpA_Max.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            NUD_SpA_Max.Name = "NUD_SpA_Max";
            NUD_SpA_Max.Size = new Size(32, 23);
            NUD_SpA_Max.TabIndex = 7;
            NUD_SpA_Max.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // NUD_SpA_Min
            // 
            NUD_SpA_Min.Location = new Point(74, 88);
            NUD_SpA_Min.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            NUD_SpA_Min.Name = "NUD_SpA_Min";
            NUD_SpA_Min.Size = new Size(32, 23);
            NUD_SpA_Min.TabIndex = 6;
            // 
            // B_Def_Max
            // 
            B_Def_Max.Location = new Point(185, 62);
            B_Def_Max.Name = "B_Def_Max";
            B_Def_Max.Size = new Size(27, 25);
            B_Def_Max.TabIndex = 17;
            B_Def_Max.Text = "31";
            B_Def_Max.UseVisualStyleBackColor = true;
            B_Def_Max.Click += B_IV_Max_Click;
            // 
            // B_Def_Min
            // 
            B_Def_Min.Location = new Point(156, 62);
            B_Def_Min.Name = "B_Def_Min";
            B_Def_Min.Size = new Size(27, 25);
            B_Def_Min.TabIndex = 16;
            B_Def_Min.Text = "0";
            B_Def_Min.UseVisualStyleBackColor = true;
            B_Def_Min.Click += B_IV_Min_Click;
            // 
            // L_Def
            // 
            L_Def.AutoSize = true;
            L_Def.Location = new Point(12, 65);
            L_Def.Name = "L_Def";
            L_Def.Size = new Size(28, 15);
            L_Def.TabIndex = 75;
            L_Def.Text = "Def:";
            L_Def.Click += IV_Label_Click;
            // 
            // L_DefSpacer
            // 
            L_DefSpacer.AutoSize = true;
            L_DefSpacer.Location = new Point(107, 65);
            L_DefSpacer.Name = "L_DefSpacer";
            L_DefSpacer.Size = new Size(12, 15);
            L_DefSpacer.TabIndex = 55;
            L_DefSpacer.Text = "-";
            // 
            // NUD_Def_Max
            // 
            NUD_Def_Max.Location = new Point(119, 63);
            NUD_Def_Max.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            NUD_Def_Max.Name = "NUD_Def_Max";
            NUD_Def_Max.Size = new Size(32, 23);
            NUD_Def_Max.TabIndex = 5;
            NUD_Def_Max.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // NUD_Def_Min
            // 
            NUD_Def_Min.Location = new Point(74, 63);
            NUD_Def_Min.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            NUD_Def_Min.Name = "NUD_Def_Min";
            NUD_Def_Min.Size = new Size(32, 23);
            NUD_Def_Min.TabIndex = 4;
            // 
            // B_Atk_Max
            // 
            B_Atk_Max.Location = new Point(185, 37);
            B_Atk_Max.Name = "B_Atk_Max";
            B_Atk_Max.Size = new Size(27, 25);
            B_Atk_Max.TabIndex = 15;
            B_Atk_Max.Text = "31";
            B_Atk_Max.UseVisualStyleBackColor = true;
            B_Atk_Max.Click += B_IV_Max_Click;
            // 
            // B_Atk_Min
            // 
            B_Atk_Min.Location = new Point(156, 37);
            B_Atk_Min.Name = "B_Atk_Min";
            B_Atk_Min.Size = new Size(27, 25);
            B_Atk_Min.TabIndex = 14;
            B_Atk_Min.Text = "0";
            B_Atk_Min.UseVisualStyleBackColor = true;
            B_Atk_Min.Click += B_IV_Min_Click;
            // 
            // L_Atk
            // 
            L_Atk.AutoSize = true;
            L_Atk.Location = new Point(12, 40);
            L_Atk.Name = "L_Atk";
            L_Atk.Size = new Size(28, 15);
            L_Atk.TabIndex = 68;
            L_Atk.Text = "Atk:";
            L_Atk.Click += IV_Label_Click;
            // 
            // L_AtkSpacer
            // 
            L_AtkSpacer.AutoSize = true;
            L_AtkSpacer.Location = new Point(107, 40);
            L_AtkSpacer.Name = "L_AtkSpacer";
            L_AtkSpacer.Size = new Size(12, 15);
            L_AtkSpacer.TabIndex = 52;
            L_AtkSpacer.Text = "-";
            // 
            // NUD_Atk_Max
            // 
            NUD_Atk_Max.Location = new Point(119, 38);
            NUD_Atk_Max.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            NUD_Atk_Max.Name = "NUD_Atk_Max";
            NUD_Atk_Max.Size = new Size(32, 23);
            NUD_Atk_Max.TabIndex = 3;
            NUD_Atk_Max.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // NUD_Atk_Min
            // 
            NUD_Atk_Min.Location = new Point(74, 38);
            NUD_Atk_Min.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            NUD_Atk_Min.Name = "NUD_Atk_Min";
            NUD_Atk_Min.Size = new Size(32, 23);
            NUD_Atk_Min.TabIndex = 2;
            // 
            // B_HP_Max
            // 
            B_HP_Max.Location = new Point(185, 12);
            B_HP_Max.Name = "B_HP_Max";
            B_HP_Max.Size = new Size(27, 25);
            B_HP_Max.TabIndex = 13;
            B_HP_Max.Text = "31";
            B_HP_Max.UseVisualStyleBackColor = true;
            B_HP_Max.Click += B_IV_Max_Click;
            // 
            // B_HP_Min
            // 
            B_HP_Min.Location = new Point(156, 12);
            B_HP_Min.Name = "B_HP_Min";
            B_HP_Min.Size = new Size(27, 25);
            B_HP_Min.TabIndex = 12;
            B_HP_Min.Text = "0";
            B_HP_Min.UseVisualStyleBackColor = true;
            B_HP_Min.Click += B_IV_Min_Click;
            // 
            // L_HP
            // 
            L_HP.AutoSize = true;
            L_HP.Location = new Point(12, 15);
            L_HP.Name = "L_HP";
            L_HP.Size = new Size(26, 15);
            L_HP.TabIndex = 58;
            L_HP.Text = "HP:";
            L_HP.Click += IV_Label_Click;
            // 
            // L_HPSpacer
            // 
            L_HPSpacer.AutoSize = true;
            L_HPSpacer.Location = new Point(107, 15);
            L_HPSpacer.Name = "L_HPSpacer";
            L_HPSpacer.Size = new Size(12, 15);
            L_HPSpacer.TabIndex = 56;
            L_HPSpacer.Text = "-";
            // 
            // NUD_HP_Max
            // 
            NUD_HP_Max.Location = new Point(119, 13);
            NUD_HP_Max.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            NUD_HP_Max.Name = "NUD_HP_Max";
            NUD_HP_Max.Size = new Size(32, 23);
            NUD_HP_Max.TabIndex = 1;
            NUD_HP_Max.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // NUD_HP_Min
            // 
            NUD_HP_Min.Location = new Point(74, 13);
            NUD_HP_Min.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            NUD_HP_Min.Name = "NUD_HP_Min";
            NUD_HP_Min.Size = new Size(32, 23);
            NUD_HP_Min.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(85, 195);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 97;
            label1.Text = "Guaranteed IVs:";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(180, 193);
            numericUpDown1.Maximum = new decimal(new int[] { 6, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(32, 23);
            numericUpDown1.TabIndex = 26;
            // 
            // B_Search
            // 
            B_Search.Location = new Point(12, 218);
            B_Search.Name = "B_Search";
            B_Search.Size = new Size(200, 25);
            B_Search.TabIndex = 28;
            B_Search.Text = "Search!";
            B_Search.UseVisualStyleBackColor = true;
            B_Search.Click += B_Search_Click;
            // 
            // CB_Tasks
            // 
            CB_Tasks.FormattingEnabled = true;
            CB_Tasks.Items.AddRange(new object[] { "1", "2", "4", "8", "16", "32", "64", "128" });
            CB_Tasks.Location = new Point(156, 245);
            CB_Tasks.Name = "CB_Tasks";
            CB_Tasks.Size = new Size(56, 23);
            CB_Tasks.TabIndex = 27;
            // 
            // L_Tasks
            // 
            L_Tasks.AutoSize = true;
            L_Tasks.Location = new Point(12, 248);
            L_Tasks.Name = "L_Tasks";
            L_Tasks.Size = new Size(75, 15);
            L_Tasks.TabIndex = 100;
            L_Tasks.Text = "Search Tasks:";
            // 
            // SpreadFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(L_Tasks);
            Controls.Add(CB_Tasks);
            Controls.Add(B_Search);
            Controls.Add(label1);
            Controls.Add(numericUpDown1);
            Controls.Add(CB_RareEC);
            Controls.Add(L_Filter_Height);
            Controls.Add(CB_Filter_Height);
            Controls.Add(B_Spe_Max);
            Controls.Add(B_Spe_Min);
            Controls.Add(L_Spe);
            Controls.Add(L_SpeSpacer);
            Controls.Add(NUD_Spe_Max);
            Controls.Add(NUD_Spe_Min);
            Controls.Add(B_SpD_Max);
            Controls.Add(B_SpD_Min);
            Controls.Add(L_SpD);
            Controls.Add(L_SpDSpacer);
            Controls.Add(NUD_SpD_Max);
            Controls.Add(NUD_SpD_Min);
            Controls.Add(B_SpA_Max);
            Controls.Add(B_SpA_Min);
            Controls.Add(L_SpA);
            Controls.Add(L_SpASpacer);
            Controls.Add(NUD_SpA_Max);
            Controls.Add(NUD_SpA_Min);
            Controls.Add(B_Def_Max);
            Controls.Add(B_Def_Min);
            Controls.Add(L_Def);
            Controls.Add(L_DefSpacer);
            Controls.Add(NUD_Def_Max);
            Controls.Add(NUD_Def_Min);
            Controls.Add(B_Atk_Max);
            Controls.Add(B_Atk_Min);
            Controls.Add(L_Atk);
            Controls.Add(L_AtkSpacer);
            Controls.Add(NUD_Atk_Max);
            Controls.Add(NUD_Atk_Min);
            Controls.Add(B_HP_Max);
            Controls.Add(B_HP_Min);
            Controls.Add(L_HP);
            Controls.Add(L_HPSpacer);
            Controls.Add(NUD_HP_Max);
            Controls.Add(NUD_HP_Min);
            Controls.Add(DGV_Results);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SpreadFinder";
            Text = "SpreadFinder";
            FormClosing += SpreadFinder_FormClosing;
            Load += SpreadFinder_Load;
            ((System.ComponentModel.ISupportInitialize)DGV_Results).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpreadFinderResultsSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Spe_Max).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Spe_Min).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_SpD_Max).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_SpD_Min).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_SpA_Max).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_SpA_Min).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Def_Max).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Def_Min).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Atk_Max).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Atk_Min).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_HP_Max).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_HP_Min).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView DGV_Results;
        private CheckBox CB_RareEC;
        private Label L_Filter_Height;
        private ComboBox CB_Filter_Height;
        private Button B_Spe_Max;
        private Button B_Spe_Min;
        private Label L_Spe;
        private Label L_SpeSpacer;
        private NumericUpDown NUD_Spe_Max;
        private NumericUpDown NUD_Spe_Min;
        private Button B_SpD_Max;
        private Button B_SpD_Min;
        private Label L_SpD;
        private Label L_SpDSpacer;
        private NumericUpDown NUD_SpD_Max;
        private NumericUpDown NUD_SpD_Min;
        private Button B_SpA_Max;
        private Button B_SpA_Min;
        private Label L_SpA;
        private Label L_SpASpacer;
        private NumericUpDown NUD_SpA_Max;
        private NumericUpDown NUD_SpA_Min;
        private Button B_Def_Max;
        private Button B_Def_Min;
        private Label L_Def;
        private Label L_DefSpacer;
        private NumericUpDown NUD_Def_Max;
        private NumericUpDown NUD_Def_Min;
        private Button B_Atk_Max;
        private Button B_Atk_Min;
        private Label L_Atk;
        private Label L_AtkSpacer;
        private NumericUpDown NUD_Atk_Max;
        private NumericUpDown NUD_Atk_Min;
        private Button B_HP_Max;
        private Button B_HP_Min;
        private Label L_HP;
        private Label L_HPSpacer;
        private NumericUpDown NUD_HP_Max;
        private NumericUpDown NUD_HP_Min;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private Button B_Search;
        private BindingSource SpreadFinderResultsSource;
        private DataGridViewTextBoxColumn seedDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn eCDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn hDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn aDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn bDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn cDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn sDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn heightDataGridViewTextBoxColumn;
        private ComboBox CB_Threads;
        private Label L_Threads;
        private ComboBox CB_Tasks;
        private Label L_Tasks;
    }
}
