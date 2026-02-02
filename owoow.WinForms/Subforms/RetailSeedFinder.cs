using static owoow.Core.RNG.Generators.Misc.SeedFinder;

namespace owoow.WinForms.Subforms
{
    public partial class RetailSeedFinder : Form
    {
        public string Seed0
        {
            get => TB_Seed0.Text;
        }

        public string Seed1
        {
            get => TB_Seed1.Text;
        }

        public RetailSeedFinder()
        {
            InitializeComponent();
            TB_InputAnimations.Text = string.Empty;
        }

        private void B_Physical_Click(object sender, EventArgs e)
        {
            if (TB_InputAnimations.Text.Length != 128)
                TB_InputAnimations.Text += "0";
        }

        private void B_Special_Click(object sender, EventArgs e)
        {
            if (TB_InputAnimations.Text.Length != 128)
                TB_InputAnimations.Text += "1";
        }

        private void TB_AnimationsInput_TextChanged(object sender, EventArgs e)
        {
            int l = TB_InputAnimations.Text.Length;
            L_CompletedInputs.Text = $"Completed Motions: {l} / 128";
            if (CB_Advanced.Checked)
            {
                if (l >= 64)
                {
                    var text = TB_InputAnimations.Text;
                    var arr = new byte[l];
                    for (var i = 0; i < l; i++)
                    {
                        arr[i] = (byte)(text[i] - '0');
                    }

                    var min = int.Parse(TB_Min.Text);
                    var max = int.Parse(TB_Max.Text);

                    if (max - min <= 150)
                    {
                        B_CalcSeed.Enabled = false;
                        var seeds = GetInitialSeedFromRange(min, max, arr);
                        switch (seeds.Count)
                        {
                            case > 1:
                                TB_Status.Text = $"~{Math.Floor(Math.Log2(seeds.Count))} more inputs";
                                break;
                            case 1:
                                TB_Status.Text = "Result found!";
                                TB_Seed0.Text = $"{seeds[0].s0:X16}";
                                TB_Seed1.Text = $"{seeds[0].s1:X16}";
                                break;
                            case 0:
                                TB_Status.Text = "No seeds found.";
                                break;
                        }
                    }
                    else
                    {
                        B_CalcSeed.Enabled = true;
                    }
                }
                else
                {
                    TB_Status.Text = "";
                }
            }
            else
            {
                if (l == 128)
                {
                    var (s0, s1) = CalculateRetailSeed(TB_InputAnimations.Text);
                    TB_Seed0.Text = s0.ToString("X16");
                    TB_Seed1.Text = s1.ToString("X16");
                }
                else
                {
                    TB_Seed0.Text = string.Empty;
                    TB_Seed1.Text = string.Empty;
                }
            }
        }

        private void KeyPress_AllowOnlyBinary(object sender, KeyPressEventArgs e)
        {
            string s = string.Empty;

            if (e.KeyChar.IsBin0()) e.KeyChar = '0';
            else if (e.KeyChar.IsBin1()) e.KeyChar = '1';

            if (e.KeyChar != (char)Keys.Back && !char.IsControl(e.KeyChar))
            {
                if (!e.KeyChar.IsBin())
                {
                    e.Handled = true;
                }
            }
        }

        private void CB_Advanced_CheckedChanged(object sender, EventArgs e)
        {
            TB_Min.Enabled = CB_Advanced.Checked;
            TB_Max.Enabled = CB_Advanced.Checked;
        }
        private void Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            var c = e.KeyChar;
            if (c != (char)Keys.Back && !char.IsControl(c))
            {
                if (!c.IsDec())
                {
                    System.Media.SystemSounds.Asterisk.Play();
                    e.Handled = true;
                }
            }
        }

        private void Dec_HandlePaste(object sender, KeyEventArgs e)
        {
            if (e is not { Modifiers: Keys.Control, KeyCode: Keys.V } && e is not { Modifiers: Keys.Shift, KeyCode: Keys.Insert }) return;
            var n = string.Empty;

            foreach (char c in Clipboard.GetText())
            {
                if (c.IsDec()) n += c;
            }

            var l = n.Length;
            var tb = (TextBox)sender;
            var max = tb.MaxLength;
            if (l == 0)
            {
                Clipboard.Clear();
            }
            else if (l > max)
            {
                tb.Text = n[..max];
            }
            else
            {
                Clipboard.SetText(n);
            }
        }

        private void TB_Min_Leave(object sender, EventArgs e)
        {
            var val = int.Parse(TB_Min.Text);
            var max = int.Parse(TB_Max.Text);
            if (val > max)
            {
                MessageBox.Show("Min Advances cannot be greater than Max Advances");
                TB_Max.Text = TB_Min.Text;
                max = val;
            }

            B_CalcSeed.Enabled = max - val > 150;
        }

        private void TB_Max_Leave(object sender, EventArgs e)
        {
            var val = int.Parse(TB_Max.Text);
            var min = int.Parse(TB_Min.Text);
            if (val < min)
            {
                MessageBox.Show("Max Advances cannot be less than Min Advances");
                TB_Min.Text = TB_Max.Text;
                val = min;
            }

            B_CalcSeed.Enabled = val - min > 150;
        }

        private void B_CalcSeed_Click(object sender, EventArgs e)
        {
            var l = TB_InputAnimations.Text.Length;
            TB_InputAnimations.Enabled = false;
            B_CalcSeed.Enabled = false;
            B_Physical.Enabled = false;
            B_Special.Enabled = false;
            TB_Status.Text = "Calculating...";
            if (CB_Advanced.Checked)
            {
                if (l >= 64)
                {
                    var text = TB_InputAnimations.Text;
                    var arr = new byte[l];
                    for (var i = 0; i < l; i++)
                    {
                        arr[i] = (byte)(text[i] - '0');
                    }

                    var min = int.Parse(TB_Min.Text);
                    var max = int.Parse(TB_Max.Text);

                    var seeds = GetInitialSeedFromRange(min, max, arr);
                    switch (seeds.Count)
                    {
                        case > 1:
                            TB_Status.Text = $"~{Math.Floor(Math.Log2(seeds.Count))} more inputs";
                            break;
                        case 1:
                            TB_Status.Text = "Result found!";
                            TB_Seed0.Text = $"{seeds[0].s0:X16}";
                            TB_Seed1.Text = $"{seeds[0].s1:X16}";
                            break;
                        case 0:
                            TB_Status.Text = "No seeds found.";
                            break;
                    }

                    TB_InputAnimations.Enabled = true;
                    B_CalcSeed.Enabled = true;
                    B_Physical.Enabled = true;
                    B_Special.Enabled = true;
                }
                else
                {
                    TB_Status.Text = "";
                }
            }
        }
    }
}
