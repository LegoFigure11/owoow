using owoow.Core;
using owoow.Core.Connection;
using owoow.Core.Structures;
using PKHeX.Core;
using PKHeX.Drawing.Misc;
using PKHeX.Drawing.PokeSprite;
using SysBot.Base;

namespace owoow.WinForms.Subforms;

public partial class SpawnWatcher : Form
{
    readonly MainWindow MainWindow;
    private readonly SwitchConnectionConfig ConnectionConfig;
    private readonly ConnectionWrapperAsync ConnectionWrapper = default!;

    private static CancellationTokenSource Source = new();

    private readonly Dictionary<string, LiveSpawn> LiveMons = [];

    public SpawnWatcher(MainWindow f, SwitchConnectionConfig cc)
    {
        InitializeComponent();

        MainWindow = f;
        ConnectionConfig = cc;
        ConnectionWrapper = new ConnectionWrapperAsync(ConnectionConfig, UpdateStatus);

        L_PokemonPresent.Text = $"Pokémon Seen: 0";

        CB_ViewSelect.Items.Clear();

        f.SetControlText(string.Empty, CB_ViewSelect, TB_EC, TB_Seed, TB_PID, TB_Ability, TB_Gender, TB_Height, TB_IVs, TB_Nature);

        Task.Run(StartMonitoring);
    }

    private async void StartMonitoring()
    {
        try
        {
            await ConnectionWrapper.Connect(Source.Token).ConfigureAwait(false);
            while (ConnectionWrapper.Connected && !Source.Token.IsCancellationRequested)
            {
                var mon = await ConnectionWrapper.ReadLiveSpawnAsync(MainWindow.Strings, Source.Token).ConfigureAwait(false);
                var n = LiveMons.TryAdd(mon.Identifier, mon);
                if (n && mon.IsPokemon)
                {
                    if (InvokeRequired) { Invoke(() => CB_ViewSelect.Items.Add(mon.Identifier)); }
                    else { CB_ViewSelect.Items.Add(mon.Identifier); }
                    L_PokemonPresent.SetText($"Pokémon Seen: {LiveMons.Count}");
                }
            }
        }
        catch (Exception ex)
        {
            if (ex is not TaskCanceledException and not ObjectDisposedException) this.DisplayMessageBox(ex.Message);
        }
    }

    private void OverworldScanner_FormClosing(object sender, FormClosingEventArgs e)
    {
        Task.Run(async () => ConnectionWrapper.DisconnectAsync(Source.Token).ConfigureAwait(false));
        Source.Cancel();
        Source = new();
        MainWindow.SpawnWatcherFormOpen = false;
        MainWindow.SetControlEnabledState(true, MainWindow.B_ReadEncounter);
    }

    private void CB_ViewSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (LiveMons.Count > 0)
        {
            var pkm = LiveMons.Where((kv) => kv.Key == $"{CB_ViewSelect.SelectedItem}").FirstOrDefault().Value;
            if (pkm is not null and { IsPokemon: true, PK8: not null })
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

    private void OverworldScanner_Load(object sender, EventArgs e)
    {
        Activate();
    }

    internal static void UpdateStatus(string status)
    {
        // Method body intentionally left blank
    }
}
