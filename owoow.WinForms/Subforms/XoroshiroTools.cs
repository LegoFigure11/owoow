using owoow.Core.Enums;
using PKHeX.Core;
using System.Globalization;
using static owoow.Core.RNG.Util;

namespace owoow.WinForms.Subforms;

public partial class XoroshiroTools : Form
{
    private readonly MainWindow main;
    private bool isHex;
    private ulong? res;

    public XoroshiroTools(MainWindow f)
    {
        InitializeComponent();
        main = f;

        TB_Seed0.Text = main.TB_Seed0.Text;
        TB_Seed1.Text = main.TB_Seed1.Text;

        TB_Seed0.KeyPress += f.KeyPress_AllowOnlyHex!;
        TB_Seed1.KeyPress += f.KeyPress_AllowOnlyHex!;
        TB_N.KeyPress += KeyPress_AllowOnlyHexExtended!;
    }

    private void XoroshiroTools_Load(object sender, EventArgs e)
    {
        CB_Operation.SelectedIndex = 0;
    }

    private void B_Calculate_Click(object sender, EventArgs e)
    {
        var op = (XoroshiroToolsOperation)CB_Operation.SelectedIndex;
        ValidateInputs(op == XoroshiroToolsOperation.NextInt);

        var s0 = ulong.Parse(TB_Seed0.Text, NumberStyles.AllowHexSpecifier);
        var s1 = ulong.Parse(TB_Seed1.Text, NumberStyles.AllowHexSpecifier);
        var text = TB_N.Text.ToLower();
        isHex = text.Contains("0x");
        var n = ulong.Parse(text.Replace("0x", string.Empty), isHex ? NumberStyles.AllowHexSpecifier : NumberStyles.None);

        var rng = new Xoroshiro128Plus(s0, s1);
        res = null;
        var sign = "+";

        switch (op)
        {
            case XoroshiroToolsOperation.Next:
                (s0, s1) = XoroshiroJump(s0, s1, n);
                break;

            case XoroshiroToolsOperation.Prev:
                var jump = UInt128.MaxValue - n; // Xoroshiro128Plus has a period of UInt128.MaxValue
                (s0, s1) = XoroshiroLongJump(s0, s1, jump);
                sign = "-";
                break;

            case XoroshiroToolsOperation.NextInt:
                res = rng.NextInt(n);
                var (_s0, _s1) = rng.GetState();
                n = GetAdvancesPassed(s0, s1, _s0, _s1, 0xFFFF);
                (s0, s1) = (_s0, _s1);
                break;
        }

        TB_Result_S0.Text = $"{s0:X16}";
        TB_Result_S1.Text = $"{s1:X16}";
        TB_Value.Text = res is null ? string.Empty : isHex ? $"0x{res:X}" : $"{res:N0}";
        TB_Distance.Text = $"{(n is not 0 ? sign : string.Empty)}{n:N0}";
    }

    private void TB_Value_Click(object sender, EventArgs e)
    {
        isHex = !isHex;
        TB_Value.Text = res is null ? string.Empty : isHex ? $"0x{res:X}" : $"{res:N0}";
    }

    private void XoroshiroTools_FormClosing(object sender, FormClosingEventArgs e)
    {
        main.XoroshiroToolsFormOpen = false;
    }

    private void ValidateInputs(bool allowOnlyInt)
    {
        // Seed
        if (string.IsNullOrEmpty(main.GetControlText(TB_Seed0))) main.SetTextBoxText("0", TB_Seed0);
        if (string.IsNullOrEmpty(main.GetControlText(TB_Seed1))) main.SetTextBoxText("0", TB_Seed1);
        main.SetTextBoxText(main.GetControlText(TB_Seed0).PadLeft(16, '0'), TB_Seed0);
        main.SetTextBoxText(main.GetControlText(TB_Seed1).PadLeft(16, '0'), TB_Seed1);

        // Operation already assumed to be valid

        // n
        var n = main.GetControlText(TB_N).ToLower();
        var nIsHex = n.Contains("0x");
        var sanitized = n.Replace("0x", string.Empty).Replace("x", string.Empty);
        if (!nIsHex)
        {
            // TODO: convert this to regex (?)
            sanitized = sanitized
                .Replace("a", string.Empty)
                .Replace("b", string.Empty)
                .Replace("c", string.Empty)
                .Replace("d", string.Empty)
                .Replace("e", string.Empty)
                .Replace("f", string.Empty);
        }
        sanitized = sanitized.TrimStart('0');
        main.SetTextBoxText($"{(nIsHex ? "0x" : string.Empty)}{sanitized.ToUpper()}", TB_N);

        if (string.IsNullOrEmpty(sanitized) || sanitized is "0" || sanitized is "0x") main.SetTextBoxText("1", TB_N);
        n = main.GetControlText(TB_N).ToLower();
        if (allowOnlyInt)
        {
            var val = ulong.Parse(n.Replace("0x", string.Empty), nIsHex ? NumberStyles.AllowHexSpecifier : NumberStyles.None);
            if (val > uint.MaxValue) main.SetTextBoxText(nIsHex ? $"0x{uint.MaxValue:X}" : $"{uint.MaxValue}", TB_N);
        }
        else
        {
            var success = ulong.TryParse(n.Replace("0x", string.Empty), nIsHex ? NumberStyles.AllowHexSpecifier : NumberStyles.None, null, out _);
            if (!success) main.SetTextBoxText(nIsHex ? $"0x{ulong.MaxValue:X}" : $"{ulong.MaxValue}");
        }
    }

    public void KeyPress_AllowOnlyHexExtended(object sender, KeyPressEventArgs e)
    {
        var c = e.KeyChar;
        if (c != (char)Keys.Back && !char.IsControl(c))
        {
            if (
                !char.IsBetween(c, '0', '9') &&
                !char.IsBetween(c, 'a', 'f') &&
                !char.IsBetween(c, 'A', 'F') &&
                c != 'x' && c != 'X'
            )
            {
                System.Media.SystemSounds.Asterisk.Play();
                e.Handled = true;
            }
        }
    }

    private void TB_Result_State_Click(object sender, EventArgs e)
    {
        if (ModifierKeys == Keys.Shift)
        {
            try
            {
                Clipboard.SetText($"0x{TB_Result_S0.Text}, 0x{TB_Result_S1.Text}");
            }
            catch
            {
                // ignored
            }
        }
        else
        {
            main.SetTextBoxText(TB_Result_S0.Text, main.TB_Seed0);
            main.SetTextBoxText(TB_Result_S1.Text, main.TB_Seed1);
        }
    }
}
