using System.Runtime.CompilerServices;
using owoow.Core;
using owoow.Core.Structures;
using PKHeX.Core;
using PKHeX.Drawing.Misc;
using PKHeX.Drawing.PokeSprite;

namespace owoow.WinForms.Subforms;

public partial class OverworldScanner : Form
{
    readonly MainWindow MainWindow;
    private readonly FieldObject[] pks;

    public OverworldScanner(MainWindow f, FieldObject[] pkl, float x, float y, float z, ulong m)
    {
        InitializeComponent();

        MainWindow = f;
        pks = pkl;

        L_X.Text = $"Player X: {x}";
        L_Y.Text = $"Player Y: {y}";
        L_Z.Text = $"Player Z: {z}";
        L_Map.Text = $"Map ID: {m:X16}";

        L_PokemonPresent.Text = $"Pokémon Present: {pks.Length}";

        CB_ViewSelect.Items.Clear();

        pks = [.. pks.OrderBy(pkm => pkm.Species)];
        foreach (var pkm in pks)
        {
            var dex = $"{pkm.PK8!.Species:D4} | ";
            var species = f.Strings.Species[pkm.PK8.Species];
            var form = pkm.PK8.Form != 0 ? $"-{pkm.PK8.Form}" : string.Empty;
            string shiny = pkm.PK8.ShinyXor switch
            {
                0 => "■ - ",
                < 16 => "★ - ",
                _ => string.Empty,
            };
            string gender = pkm.PK8.Gender switch
            {
                0 => " (M)",
                1 => " (F)",
                _ => string.Empty,
            };

            CB_ViewSelect.Items.Add($"{dex}{shiny}{species}{form}{gender}");
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
            var pkm = pks[CB_ViewSelect.SelectedIndex];
            if (pkm is { IsPokemon: true, PK8: not null })
            {
                TB_Seed.Text = $"{pkm.FixedSeed:X8}";
                TB_EC.Text = $"{pkm.PK8.EncryptionConstant:X8}";
                TB_PID.Text = $"{pkm.PK8.PID:X8}";

                string gender = pkm.PK8.Gender switch
                {
                    0 => "Male",
                    1 => "Female",
                    _ => "Unknown",
                };

                TB_Gender.Text = gender;
                TB_Nature.Text = MainWindow.Strings.Natures[(int)pkm.PK8.Nature];
                TB_Ability.Text = MainWindow.Strings.Ability[pkm.PK8.Ability];
                TB_IVs.Text = $"{pkm.PK8.IV_HP}/{pkm.PK8.IV_ATK}/{pkm.PK8.IV_DEF}/{pkm.PK8.IV_SPA}/{pkm.PK8.IV_SPD}/{pkm.PK8.IV_SPE}";
                TB_Height.Text = $"{PokeSizeDetailedUtil.GetSizeRating(pkm.PK8.HeightScalar)} ({pkm.PK8.HeightScalar})";

                bool HasRibbon = Utils.HasMark(pkm.PK8, out RibbonIndex mark);

                if (HasRibbon)
                {
                    PB_MarkSprite.Image = RibbonSpriteUtil.GetRibbonSprite(mark)!;
                }
                else
                {
                    PB_MarkSprite.Image = null;
                }

                PB_PokemonSprite.Image = pkm.PK8.Sprite();

                TB_MonX.Text = $"{pkm.X}";
                TB_MonY.Text = $"{pkm.Y}";
                TB_MonZ.Text = $"{pkm.Z}";
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
            TB_MonX.Text = string.Empty;
            TB_MonY.Text = string.Empty;
            TB_MonZ.Text = string.Empty;
        }
    }
}
