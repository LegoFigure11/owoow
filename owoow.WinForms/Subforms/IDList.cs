using System.Text.Json;

namespace owoow.WinForms.Subforms;

public partial class IDList : Form
{
    private readonly List<string> IDs;
    private readonly BindingSource bs = [];
    private readonly LotoID parent;

    public IDList(ref List<string> IDs, LotoID parent)
    {
        InitializeComponent();

        this.IDs = IDs;
        this.parent = parent;

        ReloadList();
    }

    private void ReloadList()
    {
        if (bs.DataSource is null)
        {
            bs.DataSource = IDs;
            LB_IDs.DataSource = bs;
        }
        else
        {
            bs.ResetBindings(false);
        }
    }

    private void B_Add_Click(object sender, EventArgs e)
    {
        var ID = TB_ID.Text;
        if (!string.IsNullOrEmpty(ID))
        {
            ID = ID.PadLeft(6, '0');
            if (!IDs.Contains(ID))
            {
                IDs.Add(ID);
                ReloadList();
                LB_IDs.SelectedIndex = IDs.Count - 1;
            }
        }
    }

    private void B_Remove_Click(object sender, EventArgs e)
    {
        var ID = TB_ID.Text;
        if (!string.IsNullOrEmpty(ID))
        {
            ID = ID.PadLeft(6, '0');
            IDs.Remove(ID);
            ReloadList();
            LB_IDs_SelectedIndexChanged(sender, e);
        }
    }

    private void LB_IDs_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (LB_IDs.SelectedIndex is -1) return;
        var idx = LB_IDs.SelectedIndex;
        TB_ID.Text = IDs[idx];
    }

    private void IDList_FormClosing(object sender, FormClosingEventArgs e)
    {
        for (var i = 0; i < IDs.Count; i++)
        {
            IDs[i] = IDs[i].PadLeft(6, '0');
            if (IDs[i].Length > 6) IDs[i] = IDs[i][..6];
        }

        IDs.Sort();
        string output = JsonSerializer.Serialize(IDs);
        using StreamWriter sw = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "id-list.json"));
        sw.Write(output);
        parent.IDs = IDs;
        parent.SubformOpen = false;
        parent.L_LoadedIDs.Text = $"Loaded IDs: {IDs.Count}";
    }

    private void TB_ID_KeyPress(object sender, KeyPressEventArgs e)
    {
     
        var c = e.KeyChar;
        if (c != (char)Keys.Back && !char.IsControl(c))
        {
            if (!char.IsBetween(c, '0', '9'))
            {
                System.Media.SystemSounds.Asterisk.Play();
                e.Handled = true;
            }
        }
    }
}
