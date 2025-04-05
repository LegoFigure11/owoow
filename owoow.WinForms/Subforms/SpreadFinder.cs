using owoow.Core.Interfaces;
using static owoow.Core.RNG.FilterUtil;
using static owoow.Core.RNG.FixedSeed;
using static System.Globalization.NumberStyles;

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

            GuaranteedIVs = (int)NUD_GuaranteedIVs.Value,

            FiltersEnabled = true,
        };

        List<uint> seeds = [];

        // Check if IVs match any of our pre-calculated seed lists
        var reduced = false;
        if (config.GuaranteedIVs == 0)
        {
            var min = config.TargetMinIVs;
            var max = config.TargetMaxIVs;

            var ct31 = min.Where(iv => iv == 31).Count();
            var ct0 = max.Where(iv => iv == 0).Count();

            if (ct31 == 6) // flawless
            {
                seeds.AddRange(HexFlawless);
                reduced = true;
            }
            else if (ct0 == 6) // all 0
            {
                seeds.AddRange(Hex0);
                reduced = true;
            }
            else if (ct31 == 5) // one x
            {
                if (min[0] != 31) seeds.AddRange(xHP);
                if (min[1] != 31) seeds.AddRange(xAtk);
                if (min[2] != 31) seeds.AddRange(xDef);
                if (min[3] != 31) seeds.AddRange(xSpA);
                if (min[4] != 31) seeds.AddRange(xSpD);
                if (min[5] != 31) seeds.AddRange(xSpe);
                reduced = true;
            }
            else if (max[5] == 0) // 0 speed
            {
                if (ct31 >= 4)
                {
                    if (min[1] != 31) // x atk
                    {
                        seeds.AddRange(xAtk0Spe);
                        reduced = true;
                    }
                    if (min[3] != 31) // x spa
                    {
                        seeds.AddRange(xSpA0Spe);
                        reduced = true;
                    }
                }
            }
        }

        List<SpreadFinderFrame>[] results = [];
        List<Task<List<SpreadFinderFrame>>> tasks = [];

        if (reduced)
        {
            tasks.Add(Core.RNG.Generators.Misc.SpreadFinder.Generate(seeds, config));
        }
        else
        {
            uint numTasks = (uint)(1 << MainWindow.GetComboBoxSelectedIndex(CB_Tasks));
            uint interval = (uint)(0x100000000 / numTasks);

            for (uint i = 0; i < numTasks; i++)
            {
                var start = i * interval;
                var end = start + interval - 1;
                tasks.Add(Core.RNG.Generators.Misc.SpreadFinder.Generate(start, end, config));
            }
        }

        Task.Run(async () =>
        {
            results = await Task.WhenAll(tasks);
            List<SpreadFinderFrame> AllResults = [];
            foreach (var result in results)
            {
                AllResults.AddRange(result);
            }

            AllResults = [.. AllResults
                .OrderBy(e => uint.Parse(e.Seed, AllowHexSpecifier))
                .OrderByDescending(e => e.H)
                .OrderByDescending(e => e.A)
                .OrderByDescending(e => e.B)
                .OrderByDescending(e => e.C)
                .OrderByDescending(e => e.D)
                .OrderByDescending(e => e.S)
                ];
            Frames = AllResults;
            MainWindow.SetBindingSourceDataSource(AllResults, SpreadFinderResultsSource);

            MainWindow.SetControlEnabledState(true, sender);
        }).ContinueWith(_ =>
        {
            if (Frames.Count == 0) ErrorHandler.DisplayMessageBox(this, "No reults found!", "SpreadFinder");
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

        MessageBox.Show("Searches made with this tool may take several minutes up to multiple hours depending on your device and cause high CPU load and temperatures. Proceed at your own risk.");
    }

    private void DGV_Results_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var index = e.RowIndex;
        if (Frames.Count <= index) return;
        var row = DGV_Results.Rows[index];
        var result = Frames[index];

        var iv = 2;
        byte[] ivs = [result.H, result.A, result.B, result.C, result.D, result.S];
        for (var i = 0; i < ivs.Length; i++)
        {
            if (ivs[i] == 0)
            {
                row.Cells[iv + i].Style.Font = MainWindow.BoldFont;
                row.Cells[iv + i].Style.ForeColor = Color.OrangeRed;
            }
            else if (ivs[i] == 31)
            {
                row.Cells[iv + i].Style.Font = MainWindow.BoldFont;
                row.Cells[iv + i].Style.ForeColor = Color.SeaGreen;
            }
            else
            {
                row.Cells[iv + i].Style.ForeColor = row.DefaultCellStyle.ForeColor;
                row.Cells[iv + i].Style.Font = row.DefaultCellStyle.Font;
            }
        }

        row.Cells[8].Style.Font = result.Height is not "XXXL (255)" and not "XXXS (0)" ? row.DefaultCellStyle.Font : MainWindow.BoldFont;
    }
}
