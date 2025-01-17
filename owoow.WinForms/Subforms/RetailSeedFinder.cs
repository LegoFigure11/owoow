using System.Text;
using static owoow.Core.RNG.Generators.Misc.SeedFinder;

namespace owoow.WinForms.Subforms
{
    public partial class RetailSeedFinder : Form
    {
        public string Seed0
        {
            get
            {
                return TB_Seed0.Text;
            }
        }

        public string Seed1
        {
            get
            {
                return TB_Seed1.Text;
            }
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

        private void KeyPress_AllowOnlyBinary(object sender, KeyPressEventArgs e)
        {
            string s = string.Empty;

            if (e.KeyChar is ',' or 'p' or 'P')
            {
                e.KeyChar = '0';
            }
            else if (e.KeyChar is '.' or 's' or 'S')
            {
                e.KeyChar = '1';
            }

            s += e.KeyChar;

            byte[] b = Encoding.ASCII.GetBytes(s);

            if (e.KeyChar != (char)Keys.Back && !char.IsControl(e.KeyChar))
            {
                if (!(('0' <= b[0]) && (b[0] <= '1')))
                {
                    e.Handled = true;
                }
            }
        }
    }
}