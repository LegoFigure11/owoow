namespace owoow.WinForms.Subforms
{
    partial class VideoFeed
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoFeed));
            CB_SourceSelect = new ComboBox();
            L_SourceSelect = new Label();
            B_Start = new Button();
            B_Stop = new Button();
            B_RefreshSources = new Button();
            B_ScreenshotIdle = new Button();
            B_ScreenshotSpecial = new Button();
            PB_Physical = new PictureBox();
            PB_Special = new PictureBox();
            B_LoadPhys = new Button();
            B_LoadSpec = new Button();
            B_Compare = new Button();
            B_ScreenshotPhysical = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PB_Physical).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB_Special).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // CB_SourceSelect
            // 
            CB_SourceSelect.FormattingEnabled = true;
            CB_SourceSelect.Location = new Point(130, 10);
            CB_SourceSelect.Name = "CB_SourceSelect";
            CB_SourceSelect.Size = new Size(283, 23);
            CB_SourceSelect.TabIndex = 1;
            // 
            // L_SourceSelect
            // 
            L_SourceSelect.AutoSize = true;
            L_SourceSelect.Location = new Point(11, 13);
            L_SourceSelect.Name = "L_SourceSelect";
            L_SourceSelect.Size = new Size(113, 15);
            L_SourceSelect.TabIndex = 2;
            L_SourceSelect.Text = "Select Video Source:";
            // 
            // B_Start
            // 
            B_Start.Location = new Point(10, 39);
            B_Start.Name = "B_Start";
            B_Start.Size = new Size(482, 25);
            B_Start.TabIndex = 3;
            B_Start.Text = "Start";
            B_Start.UseVisualStyleBackColor = true;
            B_Start.Click += B_Start_Click;
            // 
            // B_Stop
            // 
            B_Stop.Enabled = false;
            B_Stop.Location = new Point(10, 66);
            B_Stop.Name = "B_Stop";
            B_Stop.Size = new Size(482, 25);
            B_Stop.TabIndex = 4;
            B_Stop.Text = "Stop";
            B_Stop.UseVisualStyleBackColor = true;
            B_Stop.Click += B_Stop_Click;
            // 
            // B_RefreshSources
            // 
            B_RefreshSources.Location = new Point(418, 8);
            B_RefreshSources.Name = "B_RefreshSources";
            B_RefreshSources.Size = new Size(74, 25);
            B_RefreshSources.TabIndex = 5;
            B_RefreshSources.Text = "Refresh";
            B_RefreshSources.UseVisualStyleBackColor = true;
            B_RefreshSources.Click += B_RefreshSources_Click;
            // 
            // B_ScreenshotIdle
            // 
            B_ScreenshotIdle.Location = new Point(171, 93);
            B_ScreenshotIdle.Name = "B_ScreenshotIdle";
            B_ScreenshotIdle.Size = new Size(160, 25);
            B_ScreenshotIdle.TabIndex = 11;
            B_ScreenshotIdle.Text = "Screenshot (Idle)";
            B_ScreenshotIdle.UseVisualStyleBackColor = true;
            B_ScreenshotIdle.Click += B_ScreenshotPhysical_Click;
            // 
            // B_ScreenshotSpecial
            // 
            B_ScreenshotSpecial.Location = new Point(332, 93);
            B_ScreenshotSpecial.Name = "B_ScreenshotSpecial";
            B_ScreenshotSpecial.Size = new Size(160, 25);
            B_ScreenshotSpecial.TabIndex = 12;
            B_ScreenshotSpecial.Text = "Screenshot (Special)";
            B_ScreenshotSpecial.UseVisualStyleBackColor = true;
            B_ScreenshotSpecial.Click += B_ScreenshotSpecial_Click;
            // 
            // PB_Physical
            // 
            PB_Physical.BorderStyle = BorderStyle.FixedSingle;
            PB_Physical.Location = new Point(10, 124);
            PB_Physical.Name = "PB_Physical";
            PB_Physical.Size = new Size(160, 90);
            PB_Physical.SizeMode = PictureBoxSizeMode.Zoom;
            PB_Physical.TabIndex = 13;
            PB_Physical.TabStop = false;
            // 
            // PB_Special
            // 
            PB_Special.BorderStyle = BorderStyle.FixedSingle;
            PB_Special.Location = new Point(332, 124);
            PB_Special.Name = "PB_Special";
            PB_Special.Size = new Size(160, 90);
            PB_Special.SizeMode = PictureBoxSizeMode.Zoom;
            PB_Special.TabIndex = 14;
            PB_Special.TabStop = false;
            // 
            // B_LoadPhys
            // 
            B_LoadPhys.Location = new Point(10, 261);
            B_LoadPhys.Name = "B_LoadPhys";
            B_LoadPhys.Size = new Size(238, 25);
            B_LoadPhys.TabIndex = 15;
            B_LoadPhys.Text = "Load Image (Physical)";
            B_LoadPhys.UseVisualStyleBackColor = true;
            // 
            // B_LoadSpec
            // 
            B_LoadSpec.Location = new Point(254, 261);
            B_LoadSpec.Name = "B_LoadSpec";
            B_LoadSpec.Size = new Size(238, 25);
            B_LoadSpec.TabIndex = 16;
            B_LoadSpec.Text = "Load Image (Special)";
            B_LoadSpec.UseVisualStyleBackColor = true;
            // 
            // B_Compare
            // 
            B_Compare.Location = new Point(10, 292);
            B_Compare.Name = "B_Compare";
            B_Compare.Size = new Size(482, 25);
            B_Compare.TabIndex = 17;
            B_Compare.Text = "Scan Animations";
            B_Compare.UseVisualStyleBackColor = true;
            B_Compare.Click += B_Compare_Click;
            // 
            // B_ScreenshotPhysical
            // 
            B_ScreenshotPhysical.Location = new Point(10, 93);
            B_ScreenshotPhysical.Name = "B_ScreenshotPhysical";
            B_ScreenshotPhysical.Size = new Size(160, 25);
            B_ScreenshotPhysical.TabIndex = 18;
            B_ScreenshotPhysical.Text = "Screenshot (Physical)";
            B_ScreenshotPhysical.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(171, 124);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(160, 90);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 19;
            pictureBox1.TabStop = false;
            // 
            // VideoFeed
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(504, 449);
            Controls.Add(pictureBox1);
            Controls.Add(B_ScreenshotPhysical);
            Controls.Add(B_Compare);
            Controls.Add(B_LoadSpec);
            Controls.Add(B_LoadPhys);
            Controls.Add(PB_Special);
            Controls.Add(PB_Physical);
            Controls.Add(B_ScreenshotSpecial);
            Controls.Add(B_ScreenshotIdle);
            Controls.Add(B_RefreshSources);
            Controls.Add(B_Stop);
            Controls.Add(B_Start);
            Controls.Add(L_SourceSelect);
            Controls.Add(CB_SourceSelect);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(520, 488);
            MinimumSize = new Size(520, 488);
            Name = "VideoFeed";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Video Feed";
            FormClosing += VideoFeed_FormClosing;
            Load += VideoFeed_Load;
            ((System.ComponentModel.ISupportInitialize)PB_Physical).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB_Special).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox CB_SourceSelect;
        private Label L_SourceSelect;
        private Button B_Start;
        private Button B_Stop;
        private Button B_RefreshSources;
        private Button B_ScreenshotIdle;
        private Button B_ScreenshotSpecial;
        private PictureBox PB_Physical;
        private PictureBox PB_Special;
        private Button B_LoadPhys;
        private Button B_LoadSpec;
        private Button B_Compare;
        private Button B_ScreenshotPhysical;
        private PictureBox pictureBox1;
    }
}
