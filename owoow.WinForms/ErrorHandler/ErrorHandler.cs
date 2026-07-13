namespace owoow.WinForms;

public static class ErrorHandler
{
    public static void DisplayMessageBox(this Form form, string msg, string caption = "")
    {
        msg = ChineseLocalizer.TranslateMessage(msg);
        caption = caption.Length == 0 ? "owoow 错误" : ChineseLocalizer.TranslateMessage(caption);
        if (form.InvokeRequired)
            form.Invoke(() => MessageBox.Show(msg, caption, MessageBoxButtons.OK));
        else
            MessageBox.Show(msg, caption, MessageBoxButtons.OK);
    }
}
