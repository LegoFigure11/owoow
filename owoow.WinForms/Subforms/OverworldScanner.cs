using owoow.Core;
using PKHeX.Core;
using PKHeX.Drawing.Misc;
using PKHeX.Drawing.PokeSprite;

namespace owoow.WinForms.Subforms;

public partial class OverworldScanner : Form
{
    readonly MainWindow MainWindow;
    private readonly PK8[] pks;

    public OverworldScanner(MainWindow f, PK8[] pkl, float x, float y, float z, ulong m)
    {
        InitializeComponent();

        MainWindow = f;
        pks = pkl;

        L_X.Text = $"X: {x}";
        L_Y.Text = $"Y: {y}";
        L_Z.Text = $"Z: {z}";
        L_Map.Text = $"Map ID: {m:X16}";

        L_PokemonPresent.Text = $"Pokémon Present: {pks.Length}";

        CB_ViewSelect.Items.Clear();

        pks = [.. pks.OrderBy(pk => pk.Species)];
        foreach (var pk in pks)
        {
            var dexno = $"{pk.Species:D4} | ";
            var species = f.Strings.Species[pk.Species];
            var form = pk.Form != 0 ? $"-{pk.Form}" : string.Empty;
            string shiny = pk.ShinyXor switch
            {
                0 => "■ - ",
                < 16 => "★ - ",
                _ => string.Empty,
            };
            string gender = pk.Gender switch
            {
                0 => " (M)",
                1 => " (F)",
                _ => string.Empty,
            };

            CB_ViewSelect.Items.Add($"{dexno}{shiny}{species}{form}{gender}");
        }
        CB_ViewSelect.SelectedIndex = 0;
    }

    private void OverworldScanner_FormClosing(object sender, FormClosingEventArgs e)
    {
        MainWindow.OverworldScannerFormOpen = false;
    }

    private void CB_ViewSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (pks.Length > 0)
        {
            var pk = pks[CB_ViewSelect.SelectedIndex];
            if (pk is not null)
            {
                TB_Seed.Text = $"{Overworld8RNG.GetOriginalSeed(pk):X8}";
                TB_EC.Text = $"{pk.EncryptionConstant:X8}";
                TB_PID.Text = $"{pk.PID:X8}";

                string gender = pk.Gender switch
                {
                    0 => "Male",
                    1 => "Female",
                    _ => "Unknown",
                };

                TB_Gender.Text = gender;
                TB_Nature.Text = MainWindow.Strings.Natures[(int)pk.Nature];
                TB_Ability.Text = MainWindow.Strings.Ability[pk.Ability];
                TB_IVs.Text = $"{pk.IV_HP}/{pk.IV_ATK}/{pk.IV_DEF}/{pk.IV_SPA}/{pk.IV_SPD}/{pk.IV_SPE}";
                TB_Height.Text = $"{PokeSizeDetailedUtil.GetSizeRating(pk.HeightScalar)} ({pk.HeightScalar})";

                bool HasRibbon = Utils.HasMark(pk, out RibbonIndex mark);

                if (HasRibbon)
                {
                    PB_MarkSprite.Image = RibbonSpriteUtil.GetRibbonSprite(mark)!;
                }
                else
                {
                    PB_MarkSprite.Image = null;
                }

                PB_PokemonSprite.Image = pk.Sprite();
            }
        }
        else
        {
            PB_PokemonSprite.Image = null;
            PB_MarkSprite.Image = null;
            TB_Seed.Text = string.Empty;
            TB_EC.Text = string.Empty;
            TB_PID.Text = string.Empty;
            TB_Gender.Text = string.Empty;
            TB_Nature.Text = string.Empty;
            TB_Ability.Text = string.Empty;
            TB_IVs.Text = string.Empty;
            TB_Height.Text = string.Empty;
        }
    }
}
