namespace owoow.WinForms.Subforms;

partial class VideoFeedLog
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoFeedLog));
        TB_Logs = new TextBox();
        CB_Reject = new CheckBox();
        CB_Accept = new CheckBox();
        CB_Topmost = new CheckBox();
        SuspendLayout();
        // 
        // TB_Logs
        // 
        TB_Logs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        TB_Logs.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        TB_Logs.Location = new Point(10, 10);
        TB_Logs.Multiline = true;
        TB_Logs.Name = "TB_Logs";
        TB_Logs.ReadOnly = true;
        TB_Logs.ScrollBars = ScrollBars.Vertical;
        TB_Logs.Size = new Size(674, 257);
        TB_Logs.TabIndex = 0;
        // 
        // CB_Reject
        // 
        CB_Reject.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        CB_Reject.AutoSize = true;
        CB_Reject.Location = new Point(118, 273);
        CB_Reject.Name = "CB_Reject";
        CB_Reject.Size = new Size(103, 19);
        CB_Reject.TabIndex = 1;
        CB_Reject.Text = "Log Rejections";
        CB_Reject.UseVisualStyleBackColor = true;
        CB_Reject.CheckedChanged += CB_Reject_CheckedChanged;
        // 
        // CB_Accept
        // 
        CB_Accept.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        CB_Accept.AutoSize = true;
        CB_Accept.Checked = true;
        CB_Accept.CheckState = CheckState.Checked;
        CB_Accept.Location = new Point(12, 273);
        CB_Accept.Name = "CB_Accept";
        CB_Accept.Size = new Size(91, 19);
        CB_Accept.TabIndex = 2;
        CB_Accept.Text = "Log Accepts";
        CB_Accept.UseVisualStyleBackColor = true;
        CB_Accept.CheckedChanged += CB_Accept_CheckedChanged;
        // 
        // CB_Topmost
        // 
        CB_Topmost.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        CB_Topmost.AutoSize = true;
        CB_Topmost.Location = new Point(606, 273);
        CB_Topmost.Name = "CB_Topmost";
        CB_Topmost.Size = new Size(78, 19);
        CB_Topmost.TabIndex = 3;
        CB_Topmost.Text = "Pin to top";
        CB_Topmost.UseVisualStyleBackColor = true;
        CB_Topmost.CheckedChanged += this.CB_Topmost_CheckedChanged;
        // 
        // VideoFeedLog
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(694, 301);
        Controls.Add(CB_Topmost);
        Controls.Add(CB_Accept);
        Controls.Add(CB_Reject);
        Controls.Add(TB_Logs);
        FormBorderStyle = FormBorderStyle.SizableToolWindow;
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "VideoFeedLog";
        Text = "VideoFeedLog";
        FormClosing += VideoFeedDebugLog_FormClosing;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox TB_Logs;
    private CheckBox CB_Reject;
    private CheckBox CB_Accept;
    private CheckBox CB_Topmost;
}
