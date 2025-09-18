using owoow.Core.Interfaces;
using owoow.Core.Structures;
using PKHeX.Core;
using SysBot.Base;
using System.Net.Sockets;
using System.Text;
using static SysBot.Base.SwitchButton;
using static SysBot.Base.SwitchCommand;
using static System.Buffers.Binary.BinaryPrimitives;

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

    public async Task ReadKCoordinatesAsync(CancellationToken token)
    {
        var data = await Connection.ReadBytesAsync(KCoordinates, KCoordinates_Size, token).ConfigureAwait(false);
        data.AsSpan().CopyTo(sav.Blocks.Coordinates.Data);
    }

    public async Task<ulong> ReadSaveLocation(CancellationToken token)
    {
        var data = await Connection.ReadBytesAsync(SaveLocation_7BE8A4C6, SaveLocation_7BE8A4C6_Size, token)
            .ConfigureAwait(false);
        return ReadUInt64LittleEndian(data.AsSpan()[0x08..]);
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

    public async Task<ulong> GetCurrentTime(CancellationToken token)
    {
        var command = Encoding.ASCII.GetBytes($"getCurrentTime{(CRLF ? "\r\n" : "")}");
        var res = await Connection.ReadRaw(command, 17, token).ConfigureAwait(false);
        ulong.TryParse(Encoding.ASCII.GetString(res).Trim('\n'), System.Globalization.NumberStyles.AllowHexSpecifier, null, out var time);
        return time;
    }

    public async Task SetCurrentTime(ulong tick, CancellationToken token)
    {
        var command = Encoding.ASCII.GetBytes($"setCurrentTime {tick}{(CRLF ? "\r\n" : "")}");
        await Connection.SendAsync(command, token).ConfigureAwait(false);
    }

    public async Task PressL3(CancellationToken token)
    {
        await Connection.SendAsync(Click(LSTICK, CRLF), token).ConfigureAwait(false);
    }

    public async Task DoTurboCommand(string command, CancellationToken token)
    {
        switch (command)
        {
            case "Wait (100ms)":
                await Task.Delay(100, token).ConfigureAwait(false);
                break;
            case "Wait (500ms)":
                await Task.Delay(500, token).ConfigureAwait(false);
                break;
            case "Wait (1000ms)":
                await Task.Delay(1_000, token).ConfigureAwait(false);
                break;
            default:
                _ = command switch
                {
                    "A" => await Connection.SendAsync(Click(A, CRLF), token).ConfigureAwait(false),
                    "B" => await Connection.SendAsync(Click(B, CRLF), token).ConfigureAwait(false),
                    "X" => await Connection.SendAsync(Click(X, CRLF), token).ConfigureAwait(false),
                    "Y" => await Connection.SendAsync(Click(Y, CRLF), token).ConfigureAwait(false),

                    "Left Stick (L3)" => await Connection.SendAsync(Click(LSTICK, CRLF), token).ConfigureAwait(false),
                    "Right Stick (R3)" => await Connection.SendAsync(Click(RSTICK, CRLF), token).ConfigureAwait(false),

                    "L" => await Connection.SendAsync(Click(L, CRLF), token).ConfigureAwait(false),
                    "R" => await Connection.SendAsync(Click(R, CRLF), token).ConfigureAwait(false),
                    "ZL" => await Connection.SendAsync(Click(ZL, CRLF), token).ConfigureAwait(false),
                    "ZR" => await Connection.SendAsync(Click(ZR, CRLF), token).ConfigureAwait(false),

                    "+" => await Connection.SendAsync(Click(PLUS, CRLF), token).ConfigureAwait(false),
                    "-" => await Connection.SendAsync(Click(MINUS, CRLF), token).ConfigureAwait(false),

                    "Up (Hold)" => await Connection.SendAsync(SetStick(SwitchStick.LEFT, 0, 30000, CRLF), token).ConfigureAwait(false),
                    "Down (Hold)" => await Connection.SendAsync(SetStick(SwitchStick.LEFT, 0, -30000, CRLF), token).ConfigureAwait(false),
                    "Left (Hold)" => await Connection.SendAsync(SetStick(SwitchStick.LEFT, -30000, 0, CRLF), token).ConfigureAwait(false),
                    "Right (Hold)" => await Connection.SendAsync(SetStick(SwitchStick.LEFT, 30000, 0, CRLF), token).ConfigureAwait(false),
                    "Release Stick" => await Connection.SendAsync(SwitchCommand.ResetStick(SwitchStick.LEFT, CRLF), token).ConfigureAwait(false),

                    "D-Pad Up" => await Connection.SendAsync(Click(DUP, CRLF), token).ConfigureAwait(false),
                    "D-Pad Down" => await Connection.SendAsync(Click(DDOWN, CRLF), token).ConfigureAwait(false),
                    "D-Pad Left" => await Connection.SendAsync(Click(DLEFT, CRLF), token).ConfigureAwait(false),
                    "D-Pad Right" => await Connection.SendAsync(Click(DRIGHT, CRLF), token).ConfigureAwait(false),

                    "HOME" => await Connection.SendAsync(Click(HOME, CRLF), token).ConfigureAwait(false),
                    "Screenshot" => await Connection.SendAsync(Click(CAPTURE, CRLF), token).ConfigureAwait(false),

                    _ => throw new NotImplementedException(),
                };
                break;
        }
    }

    public async Task ResetStick(CancellationToken token)
    {
        await Connection.SendAsync(SwitchCommand.ResetStick(SwitchStick.LEFT, CRLF), token).ConfigureAwait(false);
    }

    public (string, string) GetIDs()
    {
        var myStatus = sav.MyStatus;
        return ($"{myStatus.TID16:D05}", $"{myStatus.SID16:D05}");
    }

    public bool GetHasShinyCharm()
    {
        return sav.Blocks.Items.Inventory[8].Items.Any(item => item.Index == 0x0278);
    }

    public bool GetHasMarkCharm()
    {
        return sav.Blocks.Items.Inventory[8].Items.Any(item => item.Index == 0x0635);
    }

    // Adapted from https://github.com/Lincoln-LM/PyNXReader/blob/master/structure/KCoordinates.py
    public (List<FieldObject> pkms, float x, float y, float z) ParseCoordinatesBlock()
    {
        var block = sav.Blocks.Coordinates;
        List<FieldObject> pks = [];
        var data = block.Data;

        for (var i = 0; i < 128; i++)
        {
            var start = i * FieldObject.SIZE;
            var chunk = data[start..(start + FieldObject.SIZE)];
            FieldObject fo = new(chunk.ToArray(), sav.Blocks.MyStatus);
            if (fo is { IsPokemon: true, IsFollowingPokemon: false }) pks.Add(fo);
        }

        return (pks, block.X, block.Y, block.Z);
    }

    // Modified from https://github.com/kwsch/SysBot.NET/blob/8a82453e96ed5724e175b1a44464c70eca266df0/SysBot.Pokemon/SWSH/PokeRoutineExecutor8SWSH.cs#L223
    public async Task CloseGame(ISeedResetConfig config, bool first, CancellationToken token)
    {
        if (first)
        {
            StatusUpdate("Returning HOME...");
            await Connection.SendAsync(Click(HOME, CRLF), token).ConfigureAwait(false);
            await Task.Delay(2_000 + config.ExtraTimeReturnHome, token).ConfigureAwait(false);
        }
        StatusUpdate("Closing game...");
        await Connection.SendAsync(Click(X, CRLF), token).ConfigureAwait(false);
        await Task.Delay(1_000, token).ConfigureAwait(false);
        await Connection.SendAsync(Click(A, CRLF), token).ConfigureAwait(false);
        await Task.Delay(5_000 + config.ExtraTimeCloseGame, token).ConfigureAwait(false);
    }

    // Modified from https://github.com/kwsch/SysBot.NET/blob/8a82453e96ed5724e175b1a44464c70eca266df0/SysBot.Pokemon/SWSH/PokeRoutineExecutor8SWSH.cs#L233
    public async Task OpenGame(ISeedResetConfig config, CancellationToken token)
    {
        StatusUpdate("Loading profile...");
        await Connection.SendAsync(Click(A, CRLF), token).ConfigureAwait(false);
        await Task.Delay(1_000 + config.ExtraTimeLoadProfile, token).ConfigureAwait(false);

        if (config.AvoidSystemUpdate)
        {
            StatusUpdate("Avoiding System Update...");
            await Connection.SendAsync(Click(DUP, CRLF), token).ConfigureAwait(false);
            await Task.Delay(0_600, token).ConfigureAwait(false);
            await Connection.SendAsync(Click(A, CRLF), token).ConfigureAwait(false);
            await Task.Delay(1_000 + config.ExtraTimeLoadProfile, token).ConfigureAwait(false);
        }

        StatusUpdate("Opening the game...");
        await Connection.SendAsync(Click(A, CRLF), token).ConfigureAwait(false);
        await Task.Delay(0_600, token).ConfigureAwait(false);

        StatusUpdate("Loading game...");
        await Task.Delay(10_000 + config.ExtraTimeLoadGame, token).ConfigureAwait(false);

        StatusUpdate("Waiting on HOME Menu...");
        await Connection.SendAsync(Click(HOME, CRLF), token).ConfigureAwait(false);
        await Task.Delay(2_000 + config.ExtraTimeReturnHome, token).ConfigureAwait(false);
    }
}
