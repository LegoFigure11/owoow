using owoow.Core.Interfaces;
using PKHeX.Core;
using SysBot.Base;
using System.Net.Sockets;
using static SysBot.Base.SwitchButton;
using static SysBot.Base.SwitchCommand;

namespace owoow.Core.Connection;

public class ConnectionWrapperAsync(SwitchConnectionConfig Config, Action<string> StatusUpdate) : Offsets
{
    public readonly ISwitchConnectionAsync Connection = Config.Protocol switch
    {
        SwitchProtocol.USB => new SwitchUSBAsync(Config.Port),
        _ => new SwitchSocketAsync(Config),
    };

    public bool Connected => IsConnected;
    private bool IsConnected { get; set; }
    private readonly bool CRLF = Config.Protocol is SwitchProtocol.WiFi;

    private readonly SAV8SWSH sav = new();

    public async Task<(bool, string)> Connect(CancellationToken token)
    {
        if (Connected) return (true, "");

        try
        {
            StatusUpdate("Connecting...");
            Connection.Connect();
            IsConnected = true;

            StatusUpdate("Reading SAV...");
            await ReadMyStatusAsync(token).ConfigureAwait(false);
            await ReadMyItemAsync(token).ConfigureAwait(false);

            StatusUpdate("Connected!");
            return (true, "");
        }
        catch (SocketException e)
        {
            IsConnected = false;
            return (false, e.Message);
        }
    }

    public async Task<(bool, string)> DisconnectAsync(CancellationToken token)
    {
        if (!Connected) return (true, "");

        try
        {
            StatusUpdate("Disconnecting controller");
            await Connection.SendAsync(DetachController(CRLF), token).ConfigureAwait(false);

            StatusUpdate("Disconnecting...");
            Connection.Disconnect();
            IsConnected = false;
            StatusUpdate("Disconnected!");
            return (true, "");
        }
        catch (SocketException e)
        {
            IsConnected = false;
            return (false, e.Message);
        }
    }

    public async Task<(ulong, ulong)> ReadRNGState(CancellationToken token)
    {
        var data = await Connection.ReadBytesAsync(MainRNG, MainRNG_Size, token).ConfigureAwait(false);
        return (BitConverter.ToUInt64(data, 0), BitConverter.ToUInt64(data, 8));
    }

    public async Task WriteRNGState(ulong _s0, ulong _s1, CancellationToken token)
    {
        var s0 = BitConverter.GetBytes(_s0);
        var s1 = BitConverter.GetBytes(_s1);
        await Connection.WriteBytesAsync(s0, MainRNG, token).ConfigureAwait(false);
        await Connection.WriteBytesAsync(s1, MainRNG + (MainRNG_Size / 2), token).ConfigureAwait(false);
    }

    public async Task<ushort[]> ReadDexRecommendation(CancellationToken token)
    {
        ushort[] dexrec = [0, 0, 0, 0];
        for (uint i = 0; i < dexrec.Length; i++)
        {
            var data = await Connection.ReadBytesAsync(DexRecommendation + (DexRecommendation_Size * i), 2, token).ConfigureAwait(false);
            dexrec[i] = BitConverter.ToUInt16(data, 0);
        }
        return dexrec;
    }

    public async Task<PK8> ReadWildPokemon(CancellationToken token)
    {
        var data = await Connection.ReadBytesAsync(WildPokemon, WildPokemon_Size, token).ConfigureAwait(false);
        return new PK8(data);
    }

    private async Task ReadMyStatusAsync(CancellationToken token)
    {
        var data = await Connection.ReadBytesAsync(MyStatus, MyStatus_Size, token).ConfigureAwait(false);
        data.AsSpan().CopyTo(sav.MyStatus.Data);
    }

    private async Task ReadMyItemAsync(CancellationToken token)
    {
        var data = await Connection.ReadBytesAsync(MyItems, MyItems_Size, token).ConfigureAwait(false);
        data.AsSpan().CopyTo(sav.Blocks.Items.Data);
    }

    public async Task DaySkip(CancellationToken token)
    {
        await Connection.DaySkip(token).ConfigureAwait(false);
    }

    public async Task DaySkipBack(CancellationToken token)
    {
        await Connection.DaySkipBack(token).ConfigureAwait(false);
    }

    public async Task ResetTimeNTP(CancellationToken token)
    {
        await Connection.ResetTimeNTP(token).ConfigureAwait(false);
    }

    public async Task PressL3(CancellationToken token)
    {
        await Connection.SendAsync(Click(LSTICK, CRLF), token).ConfigureAwait(false);
    }

    public async Task HoldUp(CancellationToken token)
    {
        await Connection.SendAsync(SetStick(SwitchStick.LEFT, 0, 30000, CRLF), token);
    }

    public async Task PressHome(CancellationToken token)
    {
        await Connection.SendAsync(Click(HOME, CRLF), token).ConfigureAwait(false);
    }

    public async Task ResetStick(CancellationToken token)
    {
        await Connection.SendAsync(SwitchCommand.ResetStick(SwitchStick.LEFT, CRLF), token).ConfigureAwait(false);
    }

    public (string, string) GetIDs()
    {
        var MyStatus = sav.MyStatus;
        return ($"{MyStatus.TID16:D05}", $"{MyStatus.SID16:D05}");
    }

    public bool GetHasShinyCharm()
    {
        return sav.Blocks.Items.Inventory[8].Items.Any(Item => Item.Index == 0x0278);
    }

    public bool GetHasMarkCharm()
    {
        return sav.Blocks.Items.Inventory[8].Items.Any(Item => Item.Index == 0x0635);
    }

    // From https://github.com/kwsch/SysBot.NET/blob/8a82453e96ed5724e175b1a44464c70eca266df0/SysBot.Pokemon/SWSH/PokeRoutineExecutor8SWSH.cs#L223
    public async Task CloseGame(ISeedResetConfig config, CancellationToken token)
    {
        StatusUpdate("Closing the game...");
        await Connection.SendAsync(Click(HOME, CRLF), token).ConfigureAwait(false);
        await Task.Delay(2_000 + config.ExtraTimeReturnHome, token).ConfigureAwait(false);
        await Connection.SendAsync(Click(X, CRLF), token).ConfigureAwait(false);
        await Task.Delay(1_000, token).ConfigureAwait(false);
        await Connection.SendAsync(Click(A, CRLF), token).ConfigureAwait(false);
        await Task.Delay(5_000 + config.ExtraTimeCloseGame, token);
    }

    // From https://github.com/kwsch/SysBot.NET/blob/8a82453e96ed5724e175b1a44464c70eca266df0/SysBot.Pokemon/SWSH/PokeRoutineExecutor8SWSH.cs#L233
    public async Task OpenGame(ISeedResetConfig config, CancellationToken token)
    {
        StatusUpdate("Opening the game...");
        await Connection.SendAsync(Click(A, CRLF), token).ConfigureAwait(false);
        await Task.Delay(1_000 + config.ExtraTimeLoadProfile, token).ConfigureAwait(false);

        if (config.AvoidSystemUpdate)
        {
            await Connection.SendAsync(Click(DUP, CRLF), token).ConfigureAwait(false);
            await Task.Delay(0_600, token).ConfigureAwait(false);
            await Connection.SendAsync(Click(A, CRLF), token).ConfigureAwait(false);
            await Task.Delay(1_000 + config.ExtraTimeLoadProfile, token).ConfigureAwait(false);
        }

        await Connection.SendAsync(Click(A, CRLF), token).ConfigureAwait(false);
        await Task.Delay(1_000 + config.ExtraTimeCheckDLC, token).ConfigureAwait(false);

        await Connection.SendAsync(Click(DUP, CRLF), token).ConfigureAwait(false);
        await Task.Delay(0_600, token).ConfigureAwait(false);
        await Connection.SendAsync(Click(A, CRLF), token).ConfigureAwait(false);
        await Task.Delay(0_600, token).ConfigureAwait(false);

        await Task.Delay(10_000 + config.ExtraTimeLoadGame, token).ConfigureAwait(false);
    }
}
