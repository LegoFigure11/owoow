﻿using PKHeX.Core;
using SysBot.Base;
using System.Net.Sockets;

namespace owoow.Core.Connection;

public class ConnectionWrapperAsync(SwitchConnectionConfig Config, Action<string> StatusUpdate) : Offsets
{
    public readonly ISwitchConnectionAsync Connection = new SwitchSocketAsync(Config); // No USB Support for now, but can easily add here

    public bool Connected => IsConnected;
    private bool IsConnected { get; set; }
    private readonly bool CRLF = true; // false if USB, not implemented

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
            await Connection.SendAsync(SwitchCommand.DetachController(CRLF), token).ConfigureAwait(false);

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
}
