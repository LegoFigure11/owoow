using owoow.Core;
using System.Text.Json;
using static owoow.Core.Encounters;

namespace owoow.WinForms.Subforms;

public partial class DexRecLocationList : Form
{
    private readonly List<string> Maps;
    private readonly BindingSource bs = [];
    private readonly DexRecSearcher parent;

    public DexRecLocationList(ref List<string> Maps, DexRecSearcher parent)
    {
        InitializeComponent();

        this.Maps = Maps;
        this.parent = parent;

        foreach (var item in Zones)
        {
            var val = item.Value;
            if (
                val.Contains("Surfing") ||
                val.Contains("Sharpedo") ||
                val.Contains("Flying") ||
                val.Contains("Ground") ||
                val.Contains("Puddle") ||
                val.Contains("Garbage") ||
                val.Contains("(2)") ||
                val.Contains("(3)") ||
                (val.Contains("High Level") && !val.Contains("Slumbering Weald")) ||
                val.Contains("Beach") ||
                val.Contains("Lunatone")
                )
            {
                continue;
            }

            if (CB_IgnoreMap.Items.Contains(val)) continue;
            CB_IgnoreMap.Items.Add(val);
        }
        CB_IgnoreMap.Sorted = true;
        CB_IgnoreMap.SelectedIndex = 0;

        ReloadList();
    }

    private void ReloadList()
    {
        if (bs.DataSource is null)
        {
            bs.DataSource = Maps;
            LB_Locations.DataSource = bs;
        }
        else
        {
            bs.ResetBindings(false);
        }
    }

    private void B_Add_Click(object sender, EventArgs e)
    {
        var Map = CB_IgnoreMap.Text.Trim();
        if (!string.IsNullOrEmpty(Map))
        {
            if (!Maps.Contains(Map))
            {
                Maps.Add(Map);
                ReloadList();
                LB_Locations.SelectedIndex = Maps.Count - 1;
            }
        }
    }

    private void B_Remove_Click(object sender, EventArgs e)
    {
        var Map = CB_IgnoreMap.Text.Trim();
        if (!string.IsNullOrEmpty(Map))
        {
            Maps.Remove(Map);
            ReloadList();
            LB_Locations_SelectedIndexChanged(sender, e);
        }
    }

    private void LB_Locations_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (LB_Locations.SelectedIndex is -1) return;
        var idx = LB_Locations.SelectedIndex;
        CB_IgnoreMap.SelectedIndex = CB_IgnoreMap.Items.IndexOf(Maps[idx]);
    }

    private void DexRecLocationList_FormClosing(object sender, FormClosingEventArgs e)
    {
        Maps.Sort();
        string output = JsonSerializer.Serialize(Maps);
        using StreamWriter sw = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ignore-locations-list.json"));
        sw.Write(output);
        parent.Maps = Maps;
        parent.SubformOpen = false;
        parent.L_IgnoredMaps.Text = $"Excluded Maps: {Maps.Count}";
    }

    private void B_Clear_Click(object sender, EventArgs e)
    {
        Maps.Clear();
        ReloadList();
    }
}
