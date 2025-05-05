using owoow.Core.Interfaces;
using System.Collections.Immutable;
using static owoow.Core.Encounters;

namespace owoow.WinForms.Subforms
{
    public partial class EncounterLookup : Form
    {
        private string Game;
        private ImmutableSortedDictionary<string, List<EncounterLookupEntry>> encs;

        readonly MainWindow MainWindow;

        public EncounterLookup(MainWindow f, int game)
        {
            InitializeComponent();
            MainWindow = f;
            Game = game == 0 ? "Sword" : "Shield";
            CB_Game.SelectedIndex = game;
            encs = GetEncounterLookupForGame(Game);
            SetSpeciesOptions();
        }

        private void SetSpeciesOptions()
        {
            CB_Species.Items.Clear();
            foreach (var species in encs.Keys)
            {
                CB_Species.Items.Add(species);
            }
            CB_Species.SelectedIndex = 0;
        }

        private void CB_Game_SelectedIndexChanged(object sender, EventArgs e)
        {
            Game = CB_Game.SelectedIndex == 0 ? "Sword" : "Shield";
            encs = GetEncounterLookupForGame(Game);
            SetSpeciesOptions();
        }

        private void CB_Species_SelectedIndexChanged(object sender, EventArgs e)
        {
            var entry = encs.ElementAt(CB_Species.SelectedIndex);
            var list = new List<EncounterLookupEntry>();
            foreach (var enc in entry.Value)
            {
                list.Add(enc);
            }
            ResultsSource.DataSource = list;
            ResultsSource.ResetBindings(false);
        }

        private void EncounterLookup_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainWindow.EncounterLookupFormOpen = false;
        }
    }
}
