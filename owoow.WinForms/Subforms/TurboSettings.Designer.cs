﻿namespace owoow.WinForms.Subforms
{
    partial class TurboSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TurboSettings));
            LB_List = new ListBox();
            CB_Input = new ComboBox();
            B_Add = new Button();
            B_Up = new Button();
            B_Down = new Button();
            L_Input = new Label();
            B_Remove = new Button();
            CB_Loop = new CheckBox();
            SuspendLayout();
            // 
            // LB_List
            // 
            LB_List.FormattingEnabled = true;
            LB_List.Location = new Point(12, 12);
            LB_List.Name = "LB_List";
            LB_List.Size = new Size(158, 124);
            LB_List.TabIndex = 0;
            // 
            // CB_Input
            // 
            CB_Input.FormattingEnabled = true;
            CB_Input.Location = new Point(56, 142);
            CB_Input.Name = "CB_Input";
            CB_Input.Size = new Size(81, 23);
            CB_Input.TabIndex = 4;
            // 
            // B_Add
            // 
            B_Add.Location = new Point(12, 167);
            B_Add.Name = "B_Add";
            B_Add.Size = new Size(189, 25);
            B_Add.TabIndex = 6;
            B_Add.Text = "Add";
            B_Add.UseVisualStyleBackColor = true;
            B_Add.Click += B_Add_Click;
            // 
            // B_Up
            // 
            B_Up.Location = new Point(176, 12);
            B_Up.Name = "B_Up";
            B_Up.Size = new Size(25, 39);
            B_Up.TabIndex = 1;
            B_Up.Text = "↑";
            B_Up.UseVisualStyleBackColor = true;
            B_Up.Click += B_Up_Click;
            // 
            // B_Down
            // 
            B_Down.Location = new Point(176, 96);
            B_Down.Name = "B_Down";
            B_Down.Size = new Size(25, 39);
            B_Down.TabIndex = 3;
            B_Down.Text = "↓";
            B_Down.UseVisualStyleBackColor = true;
            B_Down.Click += B_Down_Click;
            // 
            // L_Input
            // 
            L_Input.AutoSize = true;
            L_Input.Location = new Point(12, 145);
            L_Input.Name = "L_Input";
            L_Input.Size = new Size(38, 15);
            L_Input.TabIndex = 7;
            L_Input.Text = "Input:";
            // 
            // B_Remove
            // 
            B_Remove.Location = new Point(176, 54);
            B_Remove.Name = "B_Remove";
            B_Remove.Size = new Size(25, 39);
            B_Remove.TabIndex = 2;
            B_Remove.Text = "×";
            B_Remove.UseVisualStyleBackColor = true;
            B_Remove.Click += B_Remove_Click;
            // 
            // CB_Loop
            // 
            CB_Loop.AutoSize = true;
            CB_Loop.Location = new Point(143, 144);
            CB_Loop.Name = "CB_Loop";
            CB_Loop.Size = new Size(58, 19);
            CB_Loop.TabIndex = 5;
            CB_Loop.Text = "Loop?";
            CB_Loop.UseVisualStyleBackColor = true;
            CB_Loop.CheckedChanged += CB_Loop_CheckedChanged;
            // 
            // TurboSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(211, 201);
            Controls.Add(CB_Loop);
            Controls.Add(B_Remove);
            Controls.Add(L_Input);
            Controls.Add(B_Down);
            Controls.Add(B_Up);
            Controls.Add(B_Add);
            Controls.Add(CB_Input);
            Controls.Add(LB_List);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "TurboSettings";
            Text = "TurboSettings";
            FormClosing += TurboSettings_FormClosing;
            Load += TurboSettings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox LB_List;
        private ComboBox CB_Input;
        private Button B_Add;
        private Button B_Up;
        private Button B_Down;
        private Label L_Input;
        private Button B_Remove;
        private CheckBox CB_Loop;
    }
}