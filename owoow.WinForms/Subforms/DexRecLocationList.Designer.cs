namespace owoow.WinForms.Subforms
{
    partial class DexRecLocationList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DexRecLocationList));
            LB_Locations = new ListBox();
            B_Add = new Button();
            L_Map = new Label();
            B_Remove = new Button();
            CB_IgnoreMap = new ComboBox();
            B_Clear = new Button();
            SuspendLayout();
            // 
            // LB_Locations
            // 
            LB_Locations.FormattingEnabled = true;
            LB_Locations.Location = new Point(8, 8);
            LB_Locations.Name = "LB_Locations";
            LB_Locations.Size = new Size(291, 139);
            LB_Locations.TabIndex = 3;
            LB_Locations.SelectedIndexChanged += LB_Locations_SelectedIndexChanged;
            // 
            // B_Add
            // 
            B_Add.Location = new Point(305, 32);
            B_Add.Name = "B_Add";
            B_Add.Size = new Size(300, 25);
            B_Add.TabIndex = 1;
            B_Add.Text = "Add";
            B_Add.UseVisualStyleBackColor = true;
            B_Add.Click += B_Add_Click;
            // 
            // L_Map
            // 
            L_Map.AutoSize = true;
            L_Map.Location = new Point(305, 11);
            L_Map.Name = "L_Map";
            L_Map.Size = new Size(85, 15);
            L_Map.TabIndex = 2;
            L_Map.Text = "Map to ignore:";
            // 
            // B_Remove
            // 
            B_Remove.Location = new Point(305, 59);
            B_Remove.Name = "B_Remove";
            B_Remove.Size = new Size(300, 25);
            B_Remove.TabIndex = 2;
            B_Remove.Text = "Remove";
            B_Remove.UseVisualStyleBackColor = true;
            B_Remove.Click += B_Remove_Click;
            // 
            // CB_IgnoreMap
            // 
            CB_IgnoreMap.FormattingEnabled = true;
            CB_IgnoreMap.Location = new Point(396, 8);
            CB_IgnoreMap.Name = "CB_IgnoreMap";
            CB_IgnoreMap.Size = new Size(209, 23);
            CB_IgnoreMap.TabIndex = 48;
            // 
            // B_Clear
            // 
            B_Clear.Location = new Point(305, 122);
            B_Clear.Name = "B_Clear";
            B_Clear.Size = new Size(300, 25);
            B_Clear.TabIndex = 49;
            B_Clear.Text = "Clear";
            B_Clear.UseVisualStyleBackColor = true;
            B_Clear.Click += B_Clear_Click;
            // 
            // DexRecLocationList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(612, 153);
            Controls.Add(B_Clear);
            Controls.Add(CB_IgnoreMap);
            Controls.Add(B_Remove);
            Controls.Add(L_Map);
            Controls.Add(B_Add);
            Controls.Add(LB_Locations);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DexRecLocationList";
            Text = "Location List";
            FormClosing += DexRecLocationList_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox LB_Locations;
        private Button B_Add;
        private Label L_Map;
        private Button B_Remove;
        private ComboBox CB_IgnoreMap;
        private Button B_Clear;
    }
}
