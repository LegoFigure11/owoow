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

    extension(char c)
    {
        internal bool IsHex(bool allowHexPrefix = false) => char.IsBetween(c, '0', '9') || char.IsBetween(c, 'a', 'f') || char.IsBetween(c, 'A', 'F') || (allowHexPrefix && c is 'x' or 'X');
        internal bool IsDec(bool allowPeriod = false) => char.IsBetween(c, '0', '9') || (allowPeriod && c == '.');
        internal bool IsBin() => c is '0' or '1';
        internal bool IsBin0() => c is '0' or ',' or 'p' or 'P';
        internal bool IsBin1() => c is '1' or '.' or 's' or 'S';
    }
}
