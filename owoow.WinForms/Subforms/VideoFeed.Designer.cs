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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoFeed));
            SpreadFinderResultsSource = new BindingSource(components);
            CB_SourceSelect = new ComboBox();
            L_SourceSelect = new Label();
            B_Start = new Button();
            B_Stop = new Button();
            B_RefreshSources = new Button();
            B_ScreenshotPhysical = new Button();
            B_ScreenshotSpecial = new Button();
            ((System.ComponentModel.ISupportInitialize)SpreadFinderResultsSource).BeginInit();
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
            // B_ScreenshotPhysical
            // 
            B_ScreenshotPhysical.Location = new Point(9, 93);
            B_ScreenshotPhysical.Name = "B_ScreenshotPhysical";
            B_ScreenshotPhysical.Size = new Size(238, 25);
            B_ScreenshotPhysical.TabIndex = 11;
            B_ScreenshotPhysical.Text = "Screenshot (Physical)";
            B_ScreenshotPhysical.UseVisualStyleBackColor = true;
            B_ScreenshotPhysical.Click += B_ScreenshotPhysical_Click;
            // 
            // B_ScreenshotSpecial
            // 
            B_ScreenshotSpecial.Location = new Point(253, 93);
            B_ScreenshotSpecial.Name = "B_ScreenshotSpecial";
            B_ScreenshotSpecial.Size = new Size(238, 25);
            B_ScreenshotSpecial.TabIndex = 12;
            B_ScreenshotSpecial.Text = "Screenshot (Special)";
            B_ScreenshotSpecial.UseVisualStyleBackColor = true;
            // 
            // VideoFeed
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(504, 449);
            Controls.Add(B_ScreenshotSpecial);
            Controls.Add(B_ScreenshotPhysical);
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
            ((System.ComponentModel.ISupportInitialize)SpreadFinderResultsSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private BindingSource SpreadFinderResultsSource;
        private ComboBox CB_SourceSelect;
        private Label L_SourceSelect;
        private Button B_Start;
        private Button B_Stop;
        private Button B_RefreshSources;
        private Button B_ScreenshotPhysical;
        private Button B_ScreenshotSpecial;
    }
}
