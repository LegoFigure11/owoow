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
            B_ScreenshotPhysical = new Button();
            B_ScreenshotSpecial = new Button();
            PB_Physical = new PictureBox();
            PB_Special = new PictureBox();
            B_LoadPhys = new Button();
            B_LoadSpec = new Button();
            B_ScreenshotIdle = new Button();
            PB_Idle = new PictureBox();
            L_Obs = new Label();
            TB_Obs = new TextBox();
            B_LoadIdle = new Button();
            B_ObserveStop = new Button();
            B_ObserveStart = new Button();
            CB_TopMost = new CheckBox();
            L_Thresh = new Label();
            L_Time = new Label();
            NUD_Thresh = new NumericUpDown();
            NUD_Time = new NumericUpDown();
            B_Thresh = new Button();
            B_Time = new Button();
            CB_ShowLog = new CheckBox();
            B_Copy = new Button();
            B_Clear = new Button();
            ((System.ComponentModel.ISupportInitialize)PB_Physical).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB_Special).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB_Idle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Thresh).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Time).BeginInit();
            SuspendLayout();
            // 
            // CB_SourceSelect
            // 
            CB_SourceSelect.FormattingEnabled = true;
            CB_SourceSelect.Location = new Point(130, 10);
            CB_SourceSelect.Name = "CB_SourceSelect";
            CB_SourceSelect.Size = new Size(201, 23);
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
            B_Start.Text = "Start Feed";
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
            B_Stop.Text = "Stop Feed";
            B_Stop.UseVisualStyleBackColor = true;
            B_Stop.Click += B_Stop_Click;
            // 
            // B_RefreshSources
            // 
            B_RefreshSources.Location = new Point(337, 8);
            B_RefreshSources.Name = "B_RefreshSources";
            B_RefreshSources.Size = new Size(71, 25);
            B_RefreshSources.TabIndex = 5;
            B_RefreshSources.Text = "Refresh";
            B_RefreshSources.UseVisualStyleBackColor = true;
            B_RefreshSources.Click += B_RefreshSources_Click;
            // 
            // B_ScreenshotPhysical
            // 
            B_ScreenshotPhysical.Location = new Point(10, 93);
            B_ScreenshotPhysical.Name = "B_ScreenshotPhysical";
            B_ScreenshotPhysical.Size = new Size(160, 25);
            B_ScreenshotPhysical.TabIndex = 11;
            B_ScreenshotPhysical.Text = "Screenshot (Physical)";
            B_ScreenshotPhysical.UseVisualStyleBackColor = true;
            B_ScreenshotPhysical.Click += B_ScreenshotPhysical_Click;
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
            B_LoadPhys.Location = new Point(10, 220);
            B_LoadPhys.Name = "B_LoadPhys";
            B_LoadPhys.Size = new Size(160, 25);
            B_LoadPhys.TabIndex = 15;
            B_LoadPhys.Text = "Load Image (Physical)";
            B_LoadPhys.UseVisualStyleBackColor = true;
            B_LoadPhys.Click += B_LoadPhys_Click;
            // 
            // B_LoadSpec
            // 
            B_LoadSpec.Location = new Point(332, 220);
            B_LoadSpec.Name = "B_LoadSpec";
            B_LoadSpec.Size = new Size(160, 25);
            B_LoadSpec.TabIndex = 16;
            B_LoadSpec.Text = "Load Image (Special)";
            B_LoadSpec.UseVisualStyleBackColor = true;
            B_LoadSpec.Click += B_LoadSpec_Click;
            // 
            // B_ScreenshotIdle
            // 
            B_ScreenshotIdle.Location = new Point(171, 93);
            B_ScreenshotIdle.Name = "B_ScreenshotIdle";
            B_ScreenshotIdle.Size = new Size(160, 25);
            B_ScreenshotIdle.TabIndex = 18;
            B_ScreenshotIdle.Text = "Screenshot (Idle)";
            B_ScreenshotIdle.UseVisualStyleBackColor = true;
            B_ScreenshotIdle.Click += B_ScreenshotIdle_Click;
            // 
            // PB_Idle
            // 
            PB_Idle.BorderStyle = BorderStyle.FixedSingle;
            PB_Idle.Location = new Point(171, 124);
            PB_Idle.Name = "PB_Idle";
            PB_Idle.Size = new Size(160, 90);
            PB_Idle.SizeMode = PictureBoxSizeMode.Zoom;
            PB_Idle.TabIndex = 19;
            PB_Idle.TabStop = false;
            // 
            // L_Obs
            // 
            L_Obs.AutoSize = true;
            L_Obs.Location = new Point(12, 360);
            L_Obs.Name = "L_Obs";
            L_Obs.Size = new Size(88, 15);
            L_Obs.TabIndex = 20;
            L_Obs.Text = "Observations: 0";
            // 
            // TB_Obs
            // 
            TB_Obs.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB_Obs.Location = new Point(10, 378);
            TB_Obs.Name = "TB_Obs";
            TB_Obs.ReadOnly = true;
            TB_Obs.Size = new Size(482, 22);
            TB_Obs.TabIndex = 21;
            TB_Obs.TextChanged += TB_Obs_TextChanged;
            // 
            // B_LoadIdle
            // 
            B_LoadIdle.Location = new Point(171, 220);
            B_LoadIdle.Name = "B_LoadIdle";
            B_LoadIdle.Size = new Size(160, 25);
            B_LoadIdle.TabIndex = 22;
            B_LoadIdle.Text = "Load Image (Idle)";
            B_LoadIdle.UseVisualStyleBackColor = true;
            B_LoadIdle.Click += B_LoadIdle_Click;
            // 
            // B_ObserveStop
            // 
            B_ObserveStop.Enabled = false;
            B_ObserveStop.Location = new Point(10, 329);
            B_ObserveStop.Name = "B_ObserveStop";
            B_ObserveStop.Size = new Size(482, 25);
            B_ObserveStop.TabIndex = 24;
            B_ObserveStop.Text = "Stop Monitoring";
            B_ObserveStop.UseVisualStyleBackColor = true;
            B_ObserveStop.Click += B_ObserveStop_Click;
            // 
            // B_ObserveStart
            // 
            B_ObserveStart.Enabled = false;
            B_ObserveStart.Location = new Point(10, 302);
            B_ObserveStart.Name = "B_ObserveStart";
            B_ObserveStart.Size = new Size(482, 25);
            B_ObserveStart.TabIndex = 23;
            B_ObserveStart.Text = "Monitor Animations";
            B_ObserveStart.UseVisualStyleBackColor = true;
            B_ObserveStart.Click += B_Compare_Click;
            // 
            // CB_TopMost
            // 
            CB_TopMost.AutoSize = true;
            CB_TopMost.Location = new Point(414, 12);
            CB_TopMost.Name = "CB_TopMost";
            CB_TopMost.Size = new Size(78, 19);
            CB_TopMost.TabIndex = 25;
            CB_TopMost.Text = "Pin to top";
            CB_TopMost.UseVisualStyleBackColor = true;
            CB_TopMost.CheckedChanged += CB_TopMost_CheckedChanged;
            // 
            // L_Thresh
            // 
            L_Thresh.AutoSize = true;
            L_Thresh.Location = new Point(10, 248);
            L_Thresh.Name = "L_Thresh";
            L_Thresh.Size = new Size(205, 15);
            L_Thresh.TabIndex = 26;
            L_Thresh.Text = "Acceptable Difference Threshold (px):";
            // 
            // L_Time
            // 
            L_Time.AutoSize = true;
            L_Time.Location = new Point(254, 248);
            L_Time.Name = "L_Time";
            L_Time.Size = new Size(129, 15);
            L_Time.TabIndex = 29;
            L_Time.Text = "Match Cooldown (ms):";
            // 
            // NUD_Thresh
            // 
            NUD_Thresh.Location = new Point(10, 266);
            NUD_Thresh.Maximum = new decimal(new int[] { 2073600, 0, 0, 0 });
            NUD_Thresh.Name = "NUD_Thresh";
            NUD_Thresh.Size = new Size(161, 23);
            NUD_Thresh.TabIndex = 30;
            NUD_Thresh.TextAlign = HorizontalAlignment.Right;
            NUD_Thresh.ValueChanged += NUD_Thresh_ValueChanged;
            // 
            // NUD_Time
            // 
            NUD_Time.Location = new Point(254, 266);
            NUD_Time.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            NUD_Time.Name = "NUD_Time";
            NUD_Time.Size = new Size(161, 23);
            NUD_Time.TabIndex = 31;
            NUD_Time.TextAlign = HorizontalAlignment.Right;
            NUD_Time.ValueChanged += NUD_Time_ValueChanged;
            // 
            // B_Thresh
            // 
            B_Thresh.Location = new Point(177, 266);
            B_Thresh.Name = "B_Thresh";
            B_Thresh.Size = new Size(71, 25);
            B_Thresh.TabIndex = 32;
            B_Thresh.Text = "Update";
            B_Thresh.UseVisualStyleBackColor = true;
            B_Thresh.Click += B_Thresh_Click;
            // 
            // B_Time
            // 
            B_Time.Location = new Point(421, 266);
            B_Time.Name = "B_Time";
            B_Time.Size = new Size(71, 25);
            B_Time.TabIndex = 33;
            B_Time.Text = "Update";
            B_Time.UseVisualStyleBackColor = true;
            B_Time.Click += B_Time_Click;
            // 
            // CB_ShowLog
            // 
            CB_ShowLog.AutoSize = true;
            CB_ShowLog.Location = new Point(378, 359);
            CB_ShowLog.Name = "CB_ShowLog";
            CB_ShowLog.Size = new Size(114, 19);
            CB_ShowLog.TabIndex = 34;
            CB_ShowLog.Text = "Show CV Output";
            CB_ShowLog.UseVisualStyleBackColor = true;
            CB_ShowLog.CheckedChanged += CB_ShowLog_CheckedChanged;
            // 
            // B_Copy
            // 
            B_Copy.Location = new Point(171, 402);
            B_Copy.Name = "B_Copy";
            B_Copy.Size = new Size(321, 25);
            B_Copy.TabIndex = 35;
            B_Copy.Text = "Copy";
            B_Copy.UseVisualStyleBackColor = true;
            B_Copy.Click += B_Copy_Click;
            // 
            // B_Clear
            // 
            B_Clear.Location = new Point(10, 402);
            B_Clear.Name = "B_Clear";
            B_Clear.Size = new Size(160, 25);
            B_Clear.TabIndex = 36;
            B_Clear.Text = "Clear";
            B_Clear.UseVisualStyleBackColor = true;
            B_Clear.Click += B_Clear_Click;
            // 
            // VideoFeed
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(504, 434);
            Controls.Add(B_Clear);
            Controls.Add(B_Copy);
            Controls.Add(CB_ShowLog);
            Controls.Add(B_Time);
            Controls.Add(B_Thresh);
            Controls.Add(NUD_Time);
            Controls.Add(NUD_Thresh);
            Controls.Add(L_Time);
            Controls.Add(L_Thresh);
            Controls.Add(CB_TopMost);
            Controls.Add(B_ObserveStop);
            Controls.Add(B_ObserveStart);
            Controls.Add(B_LoadIdle);
            Controls.Add(TB_Obs);
            Controls.Add(L_Obs);
            Controls.Add(PB_Idle);
            Controls.Add(B_ScreenshotIdle);
            Controls.Add(B_LoadSpec);
            Controls.Add(B_LoadPhys);
            Controls.Add(PB_Special);
            Controls.Add(PB_Physical);
            Controls.Add(B_ScreenshotSpecial);
            Controls.Add(B_ScreenshotPhysical);
            Controls.Add(B_RefreshSources);
            Controls.Add(B_Stop);
            Controls.Add(B_Start);
            Controls.Add(L_SourceSelect);
            Controls.Add(CB_SourceSelect);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "VideoFeed";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Video Feed";
            FormClosing += VideoFeed_FormClosing;
            Load += VideoFeed_Load;
            ((System.ComponentModel.ISupportInitialize)PB_Physical).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB_Special).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB_Idle).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Thresh).EndInit();
            ((System.ComponentModel.ISupportInitialize)NUD_Time).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox CB_SourceSelect;
        private Label L_SourceSelect;
        private Button B_Start;
        private Button B_Stop;
        private Button B_RefreshSources;
        private Button B_ScreenshotPhysical;
        private Button B_ScreenshotSpecial;
        private PictureBox PB_Physical;
        private PictureBox PB_Special;
        private Button B_LoadPhys;
        private Button B_LoadSpec;
        private Button B_ScreenshotIdle;
        private PictureBox PB_Idle;
        private Label L_Obs;
        private TextBox TB_Obs;
        private Button B_LoadIdle;
        private Button B_ObserveStop;
        private Button B_ObserveStart;
        private CheckBox CB_TopMost;
        private Label L_Thresh;
        private Label L_Time;
        private NumericUpDown NUD_Thresh;
        private NumericUpDown NUD_Time;
        private Button B_Thresh;
        private Button B_Time;
        private CheckBox CB_ShowLog;
        private Button B_Copy;
        private Button B_Clear;
    }
}
