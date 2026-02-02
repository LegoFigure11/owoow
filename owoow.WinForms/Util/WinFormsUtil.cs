namespace owoow.WinForms;

public static class WinFormsUtil
{
    internal static string GetText(this Control c) => c.InvokeRequired ? c.Invoke(() => c.Text) : c.Text;

    internal static uint GetValue(this NumericUpDown nud) =>
        (uint)(nud.InvokeRequired ? nud.Invoke(() => nud.Value) : nud.Value);

    internal static bool GetIsChecked(this CheckBox cb) => cb.InvokeRequired ? cb.Invoke(() => cb.Checked) : cb.Checked;

    internal static int GetSelectedIndex(this ComboBox cb) =>
        cb.InvokeRequired ? cb.Invoke(() => cb.SelectedIndex) : cb.SelectedIndex;

    internal static TabPage? GetSelectedTab(this TabControl tc) => tc.InvokeRequired ? tc.Invoke(() => tc.SelectedTab) : tc.SelectedTab;

}
