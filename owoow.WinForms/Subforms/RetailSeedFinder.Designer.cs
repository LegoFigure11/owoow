namespace owoow.WinForms.Subforms
{
    partial class RetailSeedFinder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RetailSeedFinder));
            OKButton = new Button();
            L_Seed1 = new Label();
            TB_Seed1 = new TextBox();
            L_Seed0 = new Label();
            TB_Seed0 = new TextBox();
            TB_InputAnimations = new TextBox();
            L_InputAnimations = new Label();
            B_Physical = new Button();
            B_Special = new Button();
            L_CompletedInputs = new Label();
            CB_Advanced = new CheckBox();
            L_Max = new Label();
            TB_Max = new TextBox();
            L_Min = new Label();
            TB_Min = new TextBox();
            TB_Status = new TextBox();
            L_Status = new Label();
            B_CalcSeed = new Button();
            SuspendLayout();
            // 
            // OKButton
            // 
            OKButton.DialogResult = DialogResult.OK;
            OKButton.Location = new Point(13, 130);
            OKButton.Margin = new Padding(4, 3, 4, 3);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(175, 25);
            OKButton.TabIndex = 3;
            OKButton.Text = "Update Main Form";
            OKButton.UseVisualStyleBackColor = true;
            // 
            // L_Seed1
            // 
            L_Seed1.AutoSize = true;
            L_Seed1.Location = new Point(13, 38);
            L_Seed1.Margin = new Padding(4, 0, 4, 0);
            L_Seed1.Name = "L_Seed1";
            L_Seed1.Size = new Size(49, 15);
            L_Seed1.TabIndex = 91;
            L_Seed1.Text = "Seed[1]:";
            // 
            // TB_Seed1
            // 
            TB_Seed1.Font = new Font("Consolas", 9F);
            TB_Seed1.Location = new Point(70, 36);
            TB_Seed1.Margin = new Padding(4, 3, 4, 3);
            TB_Seed1.MaxLength = 16;
            TB_Seed1.Name = "TB_Seed1";
            TB_Seed1.ReadOnly = true;
            TB_Seed1.Size = new Size(118, 22);
            TB_Seed1.TabIndex = 90;
            TB_Seed1.TabStop = false;
            // 
            // L_Seed0
            // 
            L_Seed0.AutoSize = true;
            L_Seed0.Location = new Point(13, 14);
            L_Seed0.Margin = new Padding(4, 0, 4, 0);
            L_Seed0.Name = "L_Seed0";
            L_Seed0.Size = new Size(49, 15);
            L_Seed0.TabIndex = 89;
            L_Seed0.Text = "Seed[0]:";
            // 
            // TB_Seed0
            // 
            TB_Seed0.Font = new Font("Consolas", 9F);
            TB_Seed0.Location = new Point(70, 12);
            TB_Seed0.Margin = new Padding(4, 3, 4, 3);
            TB_Seed0.MaxLength = 16;
            TB_Seed0.Name = "TB_Seed0";
            TB_Seed0.ReadOnly = true;
            TB_Seed0.Size = new Size(118, 22);
            TB_Seed0.TabIndex = 88;
            TB_Seed0.TabStop = false;
            // 
            // TB_InputAnimations
            // 
            TB_InputAnimations.Font = new Font("Consolas", 9F);
            TB_InputAnimations.Location = new Point(13, 79);
            TB_InputAnimations.Margin = new Padding(4, 3, 4, 3);
            TB_InputAnimations.MaxLength = 128;
            TB_InputAnimations.Name = "TB_InputAnimations";
            TB_InputAnimations.Size = new Size(903, 22);
            TB_InputAnimations.TabIndex = 0;
            TB_InputAnimations.Text = "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            TB_InputAnimations.TextChanged += TB_AnimationsInput_TextChanged;
            TB_InputAnimations.KeyPress += KeyPress_AllowOnlyBinary;
            // 
            // L_InputAnimations
            // 
            L_InputAnimations.AutoSize = true;
            L_InputAnimations.Location = new Point(13, 61);
            L_InputAnimations.Margin = new Padding(4, 0, 4, 0);
            L_InputAnimations.Name = "L_InputAnimations";
            L_InputAnimations.Size = new Size(71, 15);
            L_InputAnimations.TabIndex = 93;
            L_InputAnimations.Text = "Animations:";
            // 
            // B_Physical
            // 
            B_Physical.Location = new Point(13, 103);
            B_Physical.Margin = new Padding(4, 3, 4, 3);
            B_Physical.Name = "B_Physical";
            B_Physical.Size = new Size(88, 25);
            B_Physical.TabIndex = 1;
            B_Physical.Text = "(0) Physical";
            B_Physical.UseVisualStyleBackColor = true;
            B_Physical.Click += B_Physical_Click;
            // 
            // B_Special
            // 
            B_Special.Location = new Point(100, 103);
            B_Special.Margin = new Padding(4, 3, 4, 3);
            B_Special.Name = "B_Special";
            B_Special.Size = new Size(88, 25);
            B_Special.TabIndex = 2;
            B_Special.Text = "(1) Special";
            B_Special.UseVisualStyleBackColor = true;
            B_Special.Click += B_Special_Click;
            // 
            // L_CompletedInputs
            // 
            L_CompletedInputs.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_CompletedInputs.AutoSize = true;
            L_CompletedInputs.Location = new Point(745, 108);
            L_CompletedInputs.Margin = new Padding(2, 0, 2, 0);
            L_CompletedInputs.Name = "L_CompletedInputs";
            L_CompletedInputs.Size = new Size(171, 15);
            L_CompletedInputs.TabIndex = 94;
            L_CompletedInputs.Text = "Completed Animations: 0 / 128";
            L_CompletedInputs.TextAlign = ContentAlignment.TopRight;
            // 
            // CB_Advanced
            // 
            CB_Advanced.AutoSize = true;
            CB_Advanced.Location = new Point(612, 7);
            CB_Advanced.Name = "CB_Advanced";
            CB_Advanced.Size = new Size(113, 19);
            CB_Advanced.TabIndex = 96;
            CB_Advanced.Text = "Advanced Mode";
            CB_Advanced.UseVisualStyleBackColor = true;
            CB_Advanced.CheckedChanged += CB_Advanced_CheckedChanged;
            // 
            // L_Max
            // 
            L_Max.AutoSize = true;
            L_Max.Location = new Point(732, 32);
            L_Max.Margin = new Padding(4, 0, 4, 0);
            L_Max.Name = "L_Max";
            L_Max.Size = new Size(60, 15);
            L_Max.TabIndex = 100;
            L_Max.Text = "Max Adv.:";
            // 
            // TB_Max
            // 
            TB_Max.Enabled = false;
            TB_Max.Font = new Font("Consolas", 9F);
            TB_Max.Location = new Point(798, 30);
            TB_Max.Margin = new Padding(4, 3, 4, 3);
            TB_Max.MaxLength = 16;
            TB_Max.Name = "TB_Max";
            TB_Max.Size = new Size(118, 22);
            TB_Max.TabIndex = 99;
            TB_Max.TabStop = false;
            TB_Max.Text = "527";
            TB_Max.TextAlign = HorizontalAlignment.Right;
            TB_Max.KeyDown += Dec_HandlePaste;
            TB_Max.KeyPress += Decimal_KeyPress;
            TB_Max.Leave += TB_Max_Leave;
            // 
            // L_Min
            // 
            L_Min.AutoSize = true;
            L_Min.Location = new Point(734, 8);
            L_Min.Margin = new Padding(4, 0, 4, 0);
            L_Min.Name = "L_Min";
            L_Min.Size = new Size(58, 15);
            L_Min.TabIndex = 98;
            L_Min.Text = "Min Adv.:";
            // 
            // TB_Min
            // 
            TB_Min.Enabled = false;
            TB_Min.Font = new Font("Consolas", 9F);
            TB_Min.Location = new Point(798, 6);
            TB_Min.Margin = new Padding(4, 3, 4, 3);
            TB_Min.MaxLength = 16;
            TB_Min.Name = "TB_Min";
            TB_Min.Size = new Size(118, 22);
            TB_Min.TabIndex = 97;
            TB_Min.TabStop = false;
            TB_Min.Text = "400";
            TB_Min.TextAlign = HorizontalAlignment.Right;
            TB_Min.KeyDown += Dec_HandlePaste;
            TB_Min.KeyPress += Decimal_KeyPress;
            TB_Min.Leave += TB_Min_Leave;
            // 
            // TB_Status
            // 
            TB_Status.Font = new Font("Consolas", 9F);
            TB_Status.Location = new Point(798, 54);
            TB_Status.Margin = new Padding(4, 3, 4, 3);
            TB_Status.MaxLength = 16;
            TB_Status.Name = "TB_Status";
            TB_Status.ReadOnly = true;
            TB_Status.Size = new Size(118, 22);
            TB_Status.TabIndex = 101;
            TB_Status.TabStop = false;
            // 
            // L_Status
            // 
            L_Status.AutoSize = true;
            L_Status.Location = new Point(750, 56);
            L_Status.Margin = new Padding(4, 0, 4, 0);
            L_Status.Name = "L_Status";
            L_Status.Size = new Size(42, 15);
            L_Status.TabIndex = 102;
            L_Status.Text = "Status:";
            // 
            // B_CalcSeed
            // 
            B_CalcSeed.Enabled = false;
            B_CalcSeed.Location = new Point(612, 51);
            B_CalcSeed.Name = "B_CalcSeed";
            B_CalcSeed.Size = new Size(113, 25);
            B_CalcSeed.TabIndex = 103;
            B_CalcSeed.Text = "Calculate Seed";
            B_CalcSeed.UseVisualStyleBackColor = true;
            B_CalcSeed.Click += B_CalcSeed_Click;
            // 
            // RetailSeedFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(919, 164);
            Controls.Add(B_CalcSeed);
            Controls.Add(L_Status);
            Controls.Add(TB_Status);
            Controls.Add(L_Max);
            Controls.Add(TB_Max);
            Controls.Add(L_Min);
            Controls.Add(TB_Min);
            Controls.Add(CB_Advanced);
            Controls.Add(L_CompletedInputs);
            Controls.Add(B_Special);
            Controls.Add(B_Physical);
            Controls.Add(L_InputAnimations);
            Controls.Add(TB_InputAnimations);
            Controls.Add(L_Seed1);
            Controls.Add(TB_Seed1);
            Controls.Add(L_Seed0);
            Controls.Add(TB_Seed0);
            Controls.Add(OKButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "RetailSeedFinder";
            Text = "Retail Seed Finder";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label L_Seed1;
        private System.Windows.Forms.TextBox TB_Seed1;
        private System.Windows.Forms.Label L_Seed0;
        private System.Windows.Forms.TextBox TB_Seed0;
        private System.Windows.Forms.TextBox TB_InputAnimations;
        private System.Windows.Forms.Label L_InputAnimations;
        private System.Windows.Forms.Button B_Physical;
        private System.Windows.Forms.Button B_Special;
        private System.Windows.Forms.Label L_CompletedInputs;
        private CheckBox CB_Advanced;
        private Label L_Max;
        private TextBox TB_Max;
        private Label L_Min;
        private TextBox TB_Min;
        private TextBox TB_Status;
        private Label L_Status;
        private Button B_CalcSeed;
    }
}