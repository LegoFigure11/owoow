namespace owoow.WinForms.Subforms
{
    partial class XoroshiroTools
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XoroshiroTools));
            L_Seed1 = new Label();
            L_Seed0 = new Label();
            TB_Seed1 = new TextBox();
            TB_Seed0 = new TextBox();
            L_Operation = new Label();
            CB_Operation = new ComboBox();
            TB_N = new TextBox();
            L_N = new Label();
            B_Calculate = new Button();
            L_Result_S1 = new Label();
            L_Result_S0 = new Label();
            TB_Result_S1 = new TextBox();
            TB_Result_S0 = new TextBox();
            L_Value = new Label();
            TB_Value = new TextBox();
            TB_Distance = new TextBox();
            L_Distance = new Label();
            SuspendLayout();
            // 
            // L_Seed1
            // 
            L_Seed1.AutoSize = true;
            L_Seed1.Location = new Point(25, 33);
            L_Seed1.Name = "L_Seed1";
            L_Seed1.Size = new Size(50, 15);
            L_Seed1.TabIndex = 11;
            L_Seed1.Text = "State[1]:";
            // 
            // L_Seed0
            // 
            L_Seed0.AutoSize = true;
            L_Seed0.Location = new Point(25, 9);
            L_Seed0.Name = "L_Seed0";
            L_Seed0.Size = new Size(50, 15);
            L_Seed0.TabIndex = 10;
            L_Seed0.Text = "State[0]:";
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
            // L_Operation
            // 
            L_Operation.AutoSize = true;
            L_Operation.Location = new Point(12, 58);
            L_Operation.Name = "L_Operation";
            L_Operation.Size = new Size(63, 15);
            L_Operation.TabIndex = 12;
            L_Operation.Text = "Operation:";
            // 
            // CB_Operation
            // 
            CB_Operation.FormattingEnabled = true;
            CB_Operation.Items.AddRange(new object[] { "Advance 𝑛", "Backwards 𝑛", "NextInt(𝑛)", "Find Initial" });
            CB_Operation.Location = new Point(89, 55);
            CB_Operation.Name = "CB_Operation";
            CB_Operation.Size = new Size(118, 23);
            CB_Operation.TabIndex = 27;
            // 
            // TB_N
            // 
            TB_N.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_N.Location = new Point(89, 80);
            TB_N.MaxLength = 16;
            TB_N.Name = "TB_N";
            TB_N.Size = new Size(118, 22);
            TB_N.TabIndex = 28;
            // 
            // L_N
            // 
            L_N.AutoSize = true;
            L_N.Location = new Point(58, 82);
            L_N.Name = "L_N";
            L_N.Size = new Size(17, 15);
            L_N.TabIndex = 29;
            L_N.Text = "𝑛:";
            // 
            // B_Calculate
            // 
            B_Calculate.Location = new Point(12, 108);
            B_Calculate.Name = "B_Calculate";
            B_Calculate.Size = new Size(195, 25);
            B_Calculate.TabIndex = 30;
            B_Calculate.Text = "Calculate";
            B_Calculate.UseVisualStyleBackColor = true;
            B_Calculate.Click += B_Calculate_Click;
            // 
            // L_Result_S1
            // 
            L_Result_S1.AutoSize = true;
            L_Result_S1.Location = new Point(25, 165);
            L_Result_S1.Name = "L_Result_S1";
            L_Result_S1.Size = new Size(50, 15);
            L_Result_S1.TabIndex = 34;
            L_Result_S1.Text = "State[1]:";
            // 
            // L_Result_S0
            // 
            L_Result_S0.AutoSize = true;
            L_Result_S0.Location = new Point(25, 141);
            L_Result_S0.Name = "L_Result_S0";
            L_Result_S0.Size = new Size(50, 15);
            L_Result_S0.TabIndex = 33;
            L_Result_S0.Text = "State[0]:";
            // 
            // TB_Result_S1
            // 
            TB_Result_S1.CharacterCasing = CharacterCasing.Upper;
            TB_Result_S1.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Result_S1.Location = new Point(89, 163);
            TB_Result_S1.MaxLength = 16;
            TB_Result_S1.Name = "TB_Result_S1";
            TB_Result_S1.ReadOnly = true;
            TB_Result_S1.Size = new Size(118, 22);
            TB_Result_S1.TabIndex = 32;
            TB_Result_S1.Click += TB_Result_State_Click;
            // 
            // TB_Result_S0
            // 
            TB_Result_S0.CharacterCasing = CharacterCasing.Upper;
            TB_Result_S0.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Result_S0.Location = new Point(89, 139);
            TB_Result_S0.MaxLength = 16;
            TB_Result_S0.Name = "TB_Result_S0";
            TB_Result_S0.ReadOnly = true;
            TB_Result_S0.Size = new Size(118, 22);
            TB_Result_S0.TabIndex = 31;
            TB_Result_S0.Click += TB_Result_State_Click;
            // 
            // L_Value
            // 
            L_Value.AutoSize = true;
            L_Value.Location = new Point(37, 189);
            L_Value.Name = "L_Value";
            L_Value.Size = new Size(38, 15);
            L_Value.TabIndex = 36;
            L_Value.Text = "Value:";
            // 
            // TB_Value
            // 
            TB_Value.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Value.Location = new Point(89, 187);
            TB_Value.MaxLength = 16;
            TB_Value.Name = "TB_Value";
            TB_Value.ReadOnly = true;
            TB_Value.Size = new Size(118, 22);
            TB_Value.TabIndex = 35;
            TB_Value.Click += TB_Value_Click;
            // 
            // TB_Distance
            // 
            TB_Distance.CharacterCasing = CharacterCasing.Upper;
            TB_Distance.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Distance.Location = new Point(89, 211);
            TB_Distance.MaxLength = 16;
            TB_Distance.Name = "TB_Distance";
            TB_Distance.ReadOnly = true;
            TB_Distance.Size = new Size(118, 22);
            TB_Distance.TabIndex = 37;
            // 
            // L_Distance
            // 
            L_Distance.AutoSize = true;
            L_Distance.Location = new Point(14, 213);
            L_Distance.Name = "L_Distance";
            L_Distance.Size = new Size(61, 15);
            L_Distance.TabIndex = 38;
            L_Distance.Text = "Advances:";
            // 
            // XoroshiroTools
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(217, 243);
            Controls.Add(L_Distance);
            Controls.Add(TB_Distance);
            Controls.Add(L_Value);
            Controls.Add(TB_Value);
            Controls.Add(L_Result_S1);
            Controls.Add(L_Result_S0);
            Controls.Add(TB_Result_S1);
            Controls.Add(TB_Result_S0);
            Controls.Add(B_Calculate);
            Controls.Add(L_N);
            Controls.Add(TB_N);
            Controls.Add(CB_Operation);
            Controls.Add(L_Operation);
            Controls.Add(L_Seed1);
            Controls.Add(L_Seed0);
            Controls.Add(TB_Seed1);
            Controls.Add(TB_Seed0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "XoroshiroTools";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Xoroshiro Tools";
            FormClosing += XoroshiroTools_FormClosing;
            Load += XoroshiroTools_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label L_Seed1;
        private Label L_Seed0;
        public TextBox TB_Seed1;
        public TextBox TB_Seed0;
        private Label L_Operation;
        private ComboBox CB_Operation;
        public TextBox TB_N;
        private Label L_N;
        private Button B_Calculate;
        private Label L_Result_S1;
        private Label L_Result_S0;
        public TextBox TB_Result_S1;
        public TextBox TB_Result_S0;
        private Label L_Value;
        public TextBox TB_Value;
        public TextBox TB_Distance;
        private Label L_Distance;
    }
}
