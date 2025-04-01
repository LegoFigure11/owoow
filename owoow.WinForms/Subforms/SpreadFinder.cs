using owoow.Core.Interfaces;
using static System.Globalization.NumberStyles;
using static owoow.Core.RNG.FilterUtil;

namespace owoow.WinForms.Subforms;

public partial class SpreadFinder : Form
{
    readonly MainWindow MainWindow;

    List<SpreadFinderFrame> Frames = [];

    public SpreadFinder(MainWindow f)
    {
        InitializeComponent();
        MainWindow = f;
    }

    private void B_Search_Click(object sender, EventArgs e)
    {
        MainWindow.SetControlEnabledState(false, sender);

        Core.RNG.GeneratorConfig config = new()
        {
            TargetScale = GetFilterScaleType(CB_Filter_Height.SelectedIndex),

            TargetMinIVs = [(uint)NUD_HP_Min.Value, (uint)NUD_Atk_Min.Value, (uint)NUD_Def_Min.Value, (uint)NUD_SpA_Min.Value, (uint)NUD_SpD_Min.Value, (uint)NUD_Spe_Min.Value],
            TargetMaxIVs = [(uint)NUD_HP_Max.Value, (uint)NUD_Atk_Max.Value, (uint)NUD_Def_Max.Value, (uint)NUD_SpA_Max.Value, (uint)NUD_SpD_Max.Value, (uint)NUD_Spe_Max.Value],

            RareEC = CB_RareEC.Checked,

            GuaranteedIVs = (int)numericUpDown1.Value,

            FiltersEnabled = true,
        };

        List<SpreadFinderFrame>[] results = [];

        uint numTasks = (uint)(1 << MainWindow.GetComboBoxSelectedIndex(CB_Tasks));
        uint interval = (uint)(0x100000000 / numTasks);

        List<Task<List<SpreadFinderFrame>>> tasks = [];
        for (uint i = 0; i < numTasks; i++)
        {
            var start = i * interval;
            var end = start + interval - 1;
            tasks.Add(Core.RNG.Generators.Misc.SpreadFinder.Generate(start, end, config));
        }


        Task.Run(async () =>
        {
            results = await Task.WhenAll(tasks);
            List<SpreadFinderFrame> AllResults = [];
            foreach (var result in results)
            {
                AllResults.AddRange(result);
            }

            AllResults = [.. AllResults.OrderBy(e => uint.Parse(e.Seed, AllowHexSpecifier))];
            Frames = AllResults;
            //if (AllResults.Count > 1000) AllResults = AllResults[0..1000];
            MainWindow.SetBindingSourceDataSource(AllResults, SpreadFinderResultsSource);

            MainWindow.SetControlEnabledState(true, sender);
        }).ContinueWith(_ =>
        {
            //if (Frames.Count >= 1_000) MessageBox.Show($"Too many results found, displayed results capped at 1000. Please re-run the search with more restrictive filters.");
            if (Frames.Count == 0) MessageBox.Show("No results found!");
        });
    }

    private void SpreadFinder_FormClosing(object sender, FormClosingEventArgs e)
    {
        MainWindow.SpreadFinderFormOpen = false;
    }

    private void B_IV_Max_Click(object sender, EventArgs e)
    {
        var stat = ((Button)sender).Name.Replace("B_", string.Empty).Replace("_Max", string.Empty);
        var min = (NumericUpDown)Controls.Find($"NUD_{stat}_Min", true).FirstOrDefault()!;
        var max = (NumericUpDown)Controls.Find($"NUD_{stat}_Max", true).FirstOrDefault()!;
        min.Value = 31;
        max.Value = 31;
    }

    private void B_IV_Min_Click(object sender, EventArgs e)
    {
        var stat = ((Button)sender).Name.Replace("B_", string.Empty).Replace("_Min", string.Empty);
        var min = (NumericUpDown)Controls.Find($"NUD_{stat}_Min", true).FirstOrDefault()!;
        var max = (NumericUpDown)Controls.Find($"NUD_{stat}_Max", true).FirstOrDefault()!;
        min.Value = 0;
        max.Value = 0;
    }


    private void IV_Label_Click(object sender, EventArgs e)
    {
        var stat = ((Label)sender).Name.Replace("L_", string.Empty);
        var min = (NumericUpDown)Controls.Find($"NUD_{stat}_Min", true).FirstOrDefault()!;
        var max = (NumericUpDown)Controls.Find($"NUD_{stat}_Max", true).FirstOrDefault()!;
        min.Value = 0;
        max.Value = 31;
    }

    private void SpreadFinder_Load(object sender, EventArgs e)
    {

        MainWindow.SetCheckBoxCheckedState(MainWindow.CB_RareEC.Checked, CB_RareEC);
        MainWindow.SetComboBoxSelectedIndex(MainWindow.CB_Filter_Height.SelectedIndex, CB_Filter_Height);
        MainWindow.SetComboBoxSelectedIndex(4, CB_Tasks);

        MessageBox.Show("Searches made with this tool may take multiple hours and cause high CPU load and temperatures. Proceed at your own risk.");
    }
}
