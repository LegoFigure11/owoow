using owoow.Core.Connection;
using SysBot.Base;
using static owoow.Core.Util;

namespace owoow.WinForms;

public partial class MainWindow : Form
{
    private static CancellationTokenSource Source = new();
    private static readonly object _connectLock = new();

    //private readonly ClientConfig Config;
    private ConnectionWrapperAsync ConnectionWrapper = default!;
    private readonly SwitchConnectionConfig ConnectionConfig;

    bool stop;

    public MainWindow()
    {
        ConnectionConfig = new()
        {
            IP = "192.168.0.0", //Config.IP,
            Port = 6000, //protocol is SwitchProtocol.WiFi ? 6000 : Config.UsbPort,
            Protocol = SwitchProtocol.WiFi //Config.Protocol,
        };

        InitializeComponent();
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
        TB_SwitchIP.Text = "192.168.0.0";

        SetTextBoxText("0", TB_Seed0, TB_Seed1);
        SetTextBoxText(string.Empty, TB_CurrentAdvances, TB_CurrentS0, TB_CurrentS1);

        TB_Status.Text = "Not Connected.";
    }

    private void Connect(CancellationToken token)
    {
        Task.Run(
            async () =>
            {
                SetButtonState(false, B_Connect);
                try
                {
                    ConnectionConfig.IP = TB_SwitchIP.Text;
                    (bool success, string err) = await ConnectionWrapper
                        .Connect(token)
                        .ConfigureAwait(false);
                    if (!success)
                    {
                        SetButtonState(true, B_Connect);
                        this.DisplayMessageBox(err);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    SetButtonState(true, B_Connect);
                    this.DisplayMessageBox(ex.Message);
                    return;
                }

                UpdateStatus("Detecting game version...");
                string id = await ConnectionWrapper.Connection
                    .GetTitleID(token)
                    .ConfigureAwait(false);
                var game = id switch
                {
                    Offsets.SwordID => "Sword",
                    Offsets.ShieldID => "Shield",
                    _ => "",
                };

                if (game is "")
                {
                    try
                    {
                        (bool success, string err) = await ConnectionWrapper
                            .DisconnectAsync(token)
                            .ConfigureAwait(false);
                        if (!success)
                        {
                            SetButtonState(true, B_Connect);
                            this.DisplayMessageBox(err);
                            return;
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                    finally
                    {
                        SetButtonState(true, B_Connect);
                        this.DisplayMessageBox("Unable to detect Pokémon Sword or Pokémon Shield running on your Switch!");
                    }
                    return;
                }

                SetCheckBoxState(ConnectionWrapper.GetHasShinyCharm(), CB_ShinyCharm);
                SetCheckBoxState(ConnectionWrapper.GetHasMarkCharm(), CB_MarkCharm);
                var (tid, sid) = ConnectionWrapper.GetIDs();
                SetTextBoxText(tid, TB_TID);
                SetTextBoxText(sid, TB_SID);

                UpdateStatus("Reading RNG State...");
                ulong _s0, _s1;
                try
                {
                    (_s0, _s1) = await ConnectionWrapper.ReadRNGState(token).ConfigureAwait(false);
                    SetTextBoxText($"{_s0:X16}", TB_Seed0, TB_CurrentS0);
                    SetTextBoxText($"{_s1:X16}", TB_Seed1, TB_CurrentS1);
                    SetTextBoxText("0", TB_CurrentAdvances);

                }
                catch (Exception ex)
                {
                    this.DisplayMessageBox($"Error occurred while reading initial RNG state: {ex.Message}");
                    return;
                }

                SetButtonState(true, B_Disconnect, B_CopyToInitial);

                UpdateStatus("Monitoring RNG State...");
                try
                {
                    ulong total = 0;
                    stop = false;
                    while (!stop)
                    {
                        if (ConnectionWrapper.Connected)
                        {
                            var (s0, s1) = await ConnectionWrapper.ReadRNGState(token).ConfigureAwait(false);
                            var adv = GetAdvancesPassed(_s0, _s1, s0, s1);
                            if (adv > 0)
                            {
                                total += adv;

                                _s0 = s0;
                                _s1 = s1;

                                SetTextBoxText($"{_s0:X16}", TB_CurrentS0);
                                SetTextBoxText($"{_s1:X16}", TB_CurrentS1);
                                SetTextBoxText($"{total:N0}", TB_CurrentAdvances);
                            }
                        }
                    }
                }
                catch
                {
                    // Ignored
                }
            },
            token
        );
    }

    private void Disconnect(CancellationToken token)
    {
        Task.Run(
            async () =>
            {
                SetButtonState(false, B_Disconnect);
                stop = true;
                try
                {
                    (bool success, string err) = await ConnectionWrapper.DisconnectAsync(token).ConfigureAwait(false);
                    if (!success) this.DisplayMessageBox(err);
                }
                catch (Exception ex)
                {
                    this.DisplayMessageBox(ex.Message);
                }
                await Source.CancelAsync();
                Source = new();
                SetButtonState(true, B_Connect);
            },
            token
        );
    }


    private void UpdateStatus(string status)
    {
        SetTextBoxText(status, [TB_Status]);
    }

    private void SetCheckBoxState(bool state, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not CheckBox cb)
                continue;

            if (InvokeRequired)
                Invoke(() => cb.Checked = state);
            else
                cb.Checked = state;
        }
    }

    private void SetButtonState(bool state, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not Button b)
                continue;

            if (InvokeRequired)
                Invoke(() => b.Enabled = state);
            else
                b.Enabled = state;
        }
    }

    private void SetTextBoxText(string text, params object[] obj)
    {
        foreach (object o in obj)
        {
            if (o is not TextBox tb)
                continue;
            if (InvokeRequired)
            {
                Invoke(() => tb.Text = text);
            }
            else tb.Text = text;
        }
    }

    private void B_Connect_Click(object sender, EventArgs e)
    {
        lock (_connectLock)
        {
            if (ConnectionWrapper is { Connected: true })
                return;

            ConnectionWrapper = new(ConnectionConfig, UpdateStatus);
            Connect(Source.Token);
        }
    }

    private void B_Disconnect_Click(object sender, EventArgs e)
    {
        lock (_connectLock)
        {
            if (ConnectionWrapper is not { Connected: true })
                return;

            Disconnect(Source.Token);
        }
    }

    private void B_CopyToInitial_Click(object sender, EventArgs e)
    {
        SetTextBoxText(TB_CurrentS0.Text, TB_Seed0);
        SetTextBoxText(TB_CurrentS1.Text, TB_Seed1);
    }

    private void KeyPress_AllowOnlyHex(object sender, KeyPressEventArgs e)
    {
        var c = e.KeyChar;
        if (c != (char)Keys.Back && !char.IsControl(c))
        {
            if (!char.IsBetween(c, '0', '9') && !char.IsBetween(c, 'a', 'f') && !char.IsBetween(c, 'A', 'F')) e.Handled = true;
        }
    }
}
