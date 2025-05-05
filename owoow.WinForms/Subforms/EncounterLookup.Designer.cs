namespace owoow.WinForms.Subforms
{
    partial class EncounterLookup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncounterLookup));
            L_Species = new Label();
            CB_Species = new ComboBox();
            DGV_Results = new DataGridView();
            speciesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            encounterTypeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            areaDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            weatherDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            encounterRateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            slotMinDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            slotMaxDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            levelDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            minLevelDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            maxLevelDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            isShinyLockedDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            isAbilityLockedDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            abilityDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            guaranteedIVsDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            ResultsSource = new BindingSource(components);
            L_Game = new Label();
            CB_Game = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)DGV_Results).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ResultsSource).BeginInit();
            SuspendLayout();
            // 
            // L_Species
            // 
            L_Species.AutoSize = true;
            L_Species.Location = new Point(8, 7);
            L_Species.Name = "L_Species";
            L_Species.Size = new Size(49, 15);
            L_Species.TabIndex = 0;
            L_Species.Text = "Species:";
            // 
            // CB_Species
            // 
            CB_Species.FormattingEnabled = true;
            CB_Species.Location = new Point(63, 4);
            CB_Species.Name = "CB_Species";
            CB_Species.Size = new Size(190, 23);
            CB_Species.TabIndex = 0;
            CB_Species.SelectedIndexChanged += CB_Species_SelectedIndexChanged;
            // 
            // DGV_Results
            // 
            DGV_Results.AllowUserToAddRows = false;
            DGV_Results.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.WhiteSmoke;
            DGV_Results.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DGV_Results.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DGV_Results.AutoGenerateColumns = false;
            DGV_Results.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGV_Results.Columns.AddRange(new DataGridViewColumn[] { speciesDataGridViewTextBoxColumn, encounterTypeDataGridViewTextBoxColumn, areaDataGridViewTextBoxColumn, weatherDataGridViewTextBoxColumn, encounterRateDataGridViewTextBoxColumn, slotMinDataGridViewTextBoxColumn, slotMaxDataGridViewTextBoxColumn, levelDataGridViewTextBoxColumn, minLevelDataGridViewTextBoxColumn, maxLevelDataGridViewTextBoxColumn, isShinyLockedDataGridViewCheckBoxColumn, isAbilityLockedDataGridViewCheckBoxColumn, abilityDataGridViewTextBoxColumn, guaranteedIVsDataGridViewTextBoxColumn });
            DGV_Results.DataSource = ResultsSource;
            DGV_Results.Location = new Point(8, 31);
            DGV_Results.Name = "DGV_Results";
            DGV_Results.ReadOnly = true;
            DGV_Results.RowHeadersVisible = false;
            DGV_Results.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV_Results.Size = new Size(784, 411);
            DGV_Results.TabIndex = 2;
            // 
            // speciesDataGridViewTextBoxColumn
            // 
            speciesDataGridViewTextBoxColumn.DataPropertyName = "Species";
            speciesDataGridViewTextBoxColumn.HeaderText = "Species";
            speciesDataGridViewTextBoxColumn.Name = "speciesDataGridViewTextBoxColumn";
            speciesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // encounterTypeDataGridViewTextBoxColumn
            // 
            encounterTypeDataGridViewTextBoxColumn.DataPropertyName = "EncounterType";
            encounterTypeDataGridViewTextBoxColumn.HeaderText = "Encounter Type";
            encounterTypeDataGridViewTextBoxColumn.Name = "encounterTypeDataGridViewTextBoxColumn";
            encounterTypeDataGridViewTextBoxColumn.ReadOnly = true;
            encounterTypeDataGridViewTextBoxColumn.Width = 120;
            // 
            // areaDataGridViewTextBoxColumn
            // 
            areaDataGridViewTextBoxColumn.DataPropertyName = "Area";
            areaDataGridViewTextBoxColumn.HeaderText = "Area";
            areaDataGridViewTextBoxColumn.Name = "areaDataGridViewTextBoxColumn";
            areaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // weatherDataGridViewTextBoxColumn
            // 
            weatherDataGridViewTextBoxColumn.DataPropertyName = "Weather";
            weatherDataGridViewTextBoxColumn.HeaderText = "Weather";
            weatherDataGridViewTextBoxColumn.Name = "weatherDataGridViewTextBoxColumn";
            weatherDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // encounterRateDataGridViewTextBoxColumn
            // 
            encounterRateDataGridViewTextBoxColumn.DataPropertyName = "EncounterRate";
            encounterRateDataGridViewTextBoxColumn.HeaderText = "Encounter Rate (%)";
            encounterRateDataGridViewTextBoxColumn.Name = "encounterRateDataGridViewTextBoxColumn";
            encounterRateDataGridViewTextBoxColumn.ReadOnly = true;
            encounterRateDataGridViewTextBoxColumn.Width = 140;
            // 
            // slotMinDataGridViewTextBoxColumn
            // 
            slotMinDataGridViewTextBoxColumn.DataPropertyName = "SlotMin";
            slotMinDataGridViewTextBoxColumn.HeaderText = "SlotMin";
            slotMinDataGridViewTextBoxColumn.Name = "slotMinDataGridViewTextBoxColumn";
            slotMinDataGridViewTextBoxColumn.ReadOnly = true;
            slotMinDataGridViewTextBoxColumn.Visible = false;
            // 
            // slotMaxDataGridViewTextBoxColumn
            // 
            slotMaxDataGridViewTextBoxColumn.DataPropertyName = "SlotMax";
            slotMaxDataGridViewTextBoxColumn.HeaderText = "SlotMax";
            slotMaxDataGridViewTextBoxColumn.Name = "slotMaxDataGridViewTextBoxColumn";
            slotMaxDataGridViewTextBoxColumn.ReadOnly = true;
            slotMaxDataGridViewTextBoxColumn.Visible = false;
            // 
            // levelDataGridViewTextBoxColumn
            // 
            levelDataGridViewTextBoxColumn.DataPropertyName = "Level";
            levelDataGridViewTextBoxColumn.HeaderText = "Level";
            levelDataGridViewTextBoxColumn.Name = "levelDataGridViewTextBoxColumn";
            levelDataGridViewTextBoxColumn.ReadOnly = true;
            levelDataGridViewTextBoxColumn.Visible = false;
            // 
            // minLevelDataGridViewTextBoxColumn
            // 
            minLevelDataGridViewTextBoxColumn.DataPropertyName = "MinLevel";
            minLevelDataGridViewTextBoxColumn.HeaderText = "MinLevel";
            minLevelDataGridViewTextBoxColumn.Name = "minLevelDataGridViewTextBoxColumn";
            minLevelDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // maxLevelDataGridViewTextBoxColumn
            // 
            maxLevelDataGridViewTextBoxColumn.DataPropertyName = "MaxLevel";
            maxLevelDataGridViewTextBoxColumn.HeaderText = "MaxLevel";
            maxLevelDataGridViewTextBoxColumn.Name = "maxLevelDataGridViewTextBoxColumn";
            maxLevelDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isShinyLockedDataGridViewCheckBoxColumn
            // 
            isShinyLockedDataGridViewCheckBoxColumn.DataPropertyName = "IsShinyLocked";
            isShinyLockedDataGridViewCheckBoxColumn.HeaderText = "Shiny Locked?";
            isShinyLockedDataGridViewCheckBoxColumn.Name = "isShinyLockedDataGridViewCheckBoxColumn";
            isShinyLockedDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // isAbilityLockedDataGridViewCheckBoxColumn
            // 
            isAbilityLockedDataGridViewCheckBoxColumn.DataPropertyName = "IsAbilityLocked";
            isAbilityLockedDataGridViewCheckBoxColumn.HeaderText = "Ability Locked?";
            isAbilityLockedDataGridViewCheckBoxColumn.Name = "isAbilityLockedDataGridViewCheckBoxColumn";
            isAbilityLockedDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // abilityDataGridViewTextBoxColumn
            // 
            abilityDataGridViewTextBoxColumn.DataPropertyName = "Ability";
            abilityDataGridViewTextBoxColumn.HeaderText = "Locked Ability";
            abilityDataGridViewTextBoxColumn.Name = "abilityDataGridViewTextBoxColumn";
            abilityDataGridViewTextBoxColumn.ReadOnly = true;
            abilityDataGridViewTextBoxColumn.Visible = false;
            // 
            // guaranteedIVsDataGridViewTextBoxColumn
            // 
            guaranteedIVsDataGridViewTextBoxColumn.DataPropertyName = "GuaranteedIVs";
            guaranteedIVsDataGridViewTextBoxColumn.HeaderText = "Guaranteed IVs";
            guaranteedIVsDataGridViewTextBoxColumn.Name = "guaranteedIVsDataGridViewTextBoxColumn";
            guaranteedIVsDataGridViewTextBoxColumn.ReadOnly = true;
            guaranteedIVsDataGridViewTextBoxColumn.Width = 140;
            // 
            // ResultsSource
            // 
            ResultsSource.DataSource = typeof(Core.Interfaces.EncounterLookupEntry);
            // 
            // L_Game
            // 
            L_Game.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_Game.AutoSize = true;
            L_Game.Location = new Point(610, 7);
            L_Game.Name = "L_Game";
            L_Game.Size = new Size(41, 15);
            L_Game.TabIndex = 13;
            L_Game.Text = "Game:";
            // 
            // CB_Game
            // 
            CB_Game.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CB_Game.FormattingEnabled = true;
            CB_Game.Items.AddRange(new object[] { "Sword", "Shield" });
            CB_Game.Location = new Point(657, 4);
            CB_Game.Name = "CB_Game";
            CB_Game.Size = new Size(135, 23);
            CB_Game.TabIndex = 1;
            CB_Game.SelectedIndexChanged += CB_Game_SelectedIndexChanged;
            // 
            // EncounterLookup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(L_Game);
            Controls.Add(CB_Game);
            Controls.Add(DGV_Results);
            Controls.Add(CB_Species);
            Controls.Add(L_Species);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EncounterLookup";
            Text = "Encounter Lookup";
            FormClosing += EncounterLookup_FormClosing;
            ((System.ComponentModel.ISupportInitialize)DGV_Results).EndInit();
            ((System.ComponentModel.ISupportInitialize)ResultsSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label L_Species;
        private ComboBox CB_Species;
        private DataGridView DGV_Results;
        private Label L_Game;
        private ComboBox CB_Game;
        private BindingSource ResultsSource;
        private DataGridViewTextBoxColumn speciesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn encounterTypeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn areaDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn weatherDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn encounterRateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn slotMinDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn slotMaxDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn levelDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn minLevelDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn maxLevelDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn isShinyLockedDataGridViewCheckBoxColumn;
        private DataGridViewCheckBoxColumn isAbilityLockedDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn abilityDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn guaranteedIVsDataGridViewTextBoxColumn;
    }
}