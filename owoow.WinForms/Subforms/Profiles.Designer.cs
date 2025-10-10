namespace owoow.WinForms.Subforms
{
    partial class Profiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Profiles));
            LB_ProfileList = new ListBox();
            B_Add = new Button();
            B_Remove = new Button();
            B_Select = new Button();
            L_Game = new Label();
            CB_MarkCharm = new CheckBox();
            CB_Game = new ComboBox();
            L_SID = new Label();
            L_TID = new Label();
            TB_SID = new TextBox();
            CB_ShinyCharm = new CheckBox();
            TB_TID = new TextBox();
            TB_Name = new TextBox();
            L_Name = new Label();
            SuspendLayout();
            // 
            // LB_ProfileList
            // 
            LB_ProfileList.FormattingEnabled = true;
            LB_ProfileList.Location = new Point(8, 8);
            LB_ProfileList.Name = "LB_ProfileList";
            LB_ProfileList.Size = new Size(120, 109);
            LB_ProfileList.TabIndex = 0;
            LB_ProfileList.SelectedIndexChanged += LB_IDs_SelectedIndexChanged;
            // 
            // B_Add
            // 
            B_Add.Location = new Point(347, 8);
            B_Add.Name = "B_Add";
            B_Add.Size = new Size(75, 25);
            B_Add.TabIndex = 7;
            B_Add.Text = "Add";
            B_Add.UseVisualStyleBackColor = true;
            B_Add.Click += B_Add_Click;
            // 
            // B_Remove
            // 
            B_Remove.Location = new Point(347, 34);
            B_Remove.Name = "B_Remove";
            B_Remove.Size = new Size(75, 25);
            B_Remove.TabIndex = 8;
            B_Remove.Text = "Delete";
            B_Remove.UseVisualStyleBackColor = true;
            B_Remove.Click += B_Remove_Click;
            // 
            // B_Select
            // 
            B_Select.DialogResult = DialogResult.OK;
            B_Select.Location = new Point(347, 88);
            B_Select.Name = "B_Select";
            B_Select.Size = new Size(75, 25);
            B_Select.TabIndex = 9;
            B_Select.Text = "Select";
            B_Select.UseVisualStyleBackColor = true;
            // 
            // L_Game
            // 
            L_Game.AutoSize = true;
            L_Game.Location = new Point(146, 91);
            L_Game.Name = "L_Game";
            L_Game.Size = new Size(41, 15);
            L_Game.TabIndex = 26;
            L_Game.Text = "Game:";
            // 
            // CB_MarkCharm
            // 
            CB_MarkCharm.AutoSize = true;
            CB_MarkCharm.CheckAlign = ContentAlignment.MiddleRight;
            CB_MarkCharm.Location = new Point(147, 63);
            CB_MarkCharm.Name = "CB_MarkCharm";
            CB_MarkCharm.Size = new Size(97, 19);
            CB_MarkCharm.TabIndex = 4;
            CB_MarkCharm.Text = "Mark Charm?";
            CB_MarkCharm.UseVisualStyleBackColor = true;
            // 
            // CB_Game
            // 
            CB_Game.FormattingEnabled = true;
            CB_Game.Items.AddRange(new object[] { "Sword", "Shield" });
            CB_Game.Location = new Point(206, 88);
            CB_Game.Name = "CB_Game";
            CB_Game.Size = new Size(135, 23);
            CB_Game.TabIndex = 6;
            // 
            // L_SID
            // 
            L_SID.AutoSize = true;
            L_SID.Location = new Point(267, 64);
            L_SID.Name = "L_SID";
            L_SID.Size = new Size(27, 15);
            L_SID.TabIndex = 28;
            L_SID.Text = "SID:";
            // 
            // L_TID
            // 
            L_TID.AutoSize = true;
            L_TID.Location = new Point(267, 39);
            L_TID.Name = "L_TID";
            L_TID.Size = new Size(27, 15);
            L_TID.TabIndex = 27;
            L_TID.Text = "TID:";
            // 
            // TB_SID
            // 
            TB_SID.CharacterCasing = CharacterCasing.Upper;
            TB_SID.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_SID.Location = new Point(300, 62);
            TB_SID.MaxLength = 5;
            TB_SID.Name = "TB_SID";
            TB_SID.Size = new Size(41, 22);
            TB_SID.TabIndex = 5;
            TB_SID.Text = "54321";
            TB_SID.KeyPress += ID_KeyPress;
            // 
            // CB_ShinyCharm
            // 
            CB_ShinyCharm.AutoSize = true;
            CB_ShinyCharm.CheckAlign = ContentAlignment.MiddleRight;
            CB_ShinyCharm.Location = new Point(145, 38);
            CB_ShinyCharm.Name = "CB_ShinyCharm";
            CB_ShinyCharm.Size = new Size(99, 19);
            CB_ShinyCharm.TabIndex = 2;
            CB_ShinyCharm.Tag = "";
            CB_ShinyCharm.Text = "Shiny Charm?";
            CB_ShinyCharm.UseVisualStyleBackColor = true;
            // 
            // TB_TID
            // 
            TB_TID.CharacterCasing = CharacterCasing.Upper;
            TB_TID.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_TID.Location = new Point(300, 37);
            TB_TID.MaxLength = 5;
            TB_TID.Name = "TB_TID";
            TB_TID.Size = new Size(41, 22);
            TB_TID.TabIndex = 3;
            TB_TID.Text = "12345";
            TB_TID.KeyPress += ID_KeyPress;
            // 
            // TB_Name
            // 
            TB_Name.Location = new Point(206, 8);
            TB_Name.Name = "TB_Name";
            TB_Name.Size = new Size(135, 23);
            TB_Name.TabIndex = 1;
            TB_Name.TextChanged += TB_Name_TextChanged;
            // 
            // L_Name
            // 
            L_Name.AutoSize = true;
            L_Name.Location = new Point(145, 11);
            L_Name.Name = "L_Name";
            L_Name.Size = new Size(42, 15);
            L_Name.TabIndex = 30;
            L_Name.Text = "Name:";
            // 
            // Profiles
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(429, 124);
            Controls.Add(L_Name);
            Controls.Add(TB_Name);
            Controls.Add(L_Game);
            Controls.Add(CB_MarkCharm);
            Controls.Add(CB_Game);
            Controls.Add(L_SID);
            Controls.Add(L_TID);
            Controls.Add(TB_SID);
            Controls.Add(CB_ShinyCharm);
            Controls.Add(TB_TID);
            Controls.Add(B_Select);
            Controls.Add(B_Remove);
            Controls.Add(B_Add);
            Controls.Add(LB_ProfileList);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Profiles";
            Text = "Profile Manager";
            FormClosing += Profiles_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox LB_ProfileList;
        private Button B_Add;
        private Button B_Remove;
        private Button B_Select;
        private Label L_Game;
        private CheckBox CB_MarkCharm;
        private ComboBox CB_Game;
        private Label L_SID;
        private Label L_TID;
        private TextBox TB_SID;
        private CheckBox CB_ShinyCharm;
        private TextBox TB_TID;
        private TextBox TB_Name;
        private Label L_Name;
    }
}
