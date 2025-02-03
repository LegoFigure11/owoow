namespace owoow.WinForms.Subforms
{
    partial class IDList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IDList));
            LB_IDs = new ListBox();
            B_Add = new Button();
            L_ID = new Label();
            TB_ID = new TextBox();
            B_Remove = new Button();
            SuspendLayout();
            // 
            // LB_IDs
            // 
            LB_IDs.FormattingEnabled = true;
            LB_IDs.Location = new Point(8, 8);
            LB_IDs.Name = "LB_IDs";
            LB_IDs.Size = new Size(120, 139);
            LB_IDs.TabIndex = 0;
            LB_IDs.SelectedIndexChanged += LB_IDs_SelectedIndexChanged;
            // 
            // B_Add
            // 
            B_Add.Location = new Point(134, 32);
            B_Add.Name = "B_Add";
            B_Add.Size = new Size(75, 25);
            B_Add.TabIndex = 1;
            B_Add.Text = "Add";
            B_Add.UseVisualStyleBackColor = true;
            B_Add.Click += B_Add_Click;
            // 
            // L_ID
            // 
            L_ID.AutoSize = true;
            L_ID.Location = new Point(134, 11);
            L_ID.Name = "L_ID";
            L_ID.Size = new Size(21, 15);
            L_ID.TabIndex = 2;
            L_ID.Text = "ID:";
            // 
            // TB_ID
            // 
            TB_ID.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_ID.Location = new Point(161, 8);
            TB_ID.MaxLength = 6;
            TB_ID.Name = "TB_ID";
            TB_ID.Size = new Size(48, 22);
            TB_ID.TabIndex = 3;
            TB_ID.Text = "123456";
            TB_ID.KeyPress += TB_ID_KeyPress;
            // 
            // B_Remove
            // 
            B_Remove.Location = new Point(134, 59);
            B_Remove.Name = "B_Remove";
            B_Remove.Size = new Size(75, 25);
            B_Remove.TabIndex = 4;
            B_Remove.Text = "Remove";
            B_Remove.UseVisualStyleBackColor = true;
            B_Remove.Click += B_Remove_Click;
            // 
            // IDList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(215, 153);
            Controls.Add(B_Remove);
            Controls.Add(TB_ID);
            Controls.Add(L_ID);
            Controls.Add(B_Add);
            Controls.Add(LB_IDs);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "IDList";
            Text = "IDList";
            FormClosing += IDList_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox LB_IDs;
        private Button B_Add;
        private Label L_ID;
        private TextBox TB_ID;
        private Button B_Remove;
    }
}