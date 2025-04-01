namespace owoow.Core.Interfaces
{

    internal interface IBasicFrame
    {
        string Advances { get; }
        string Jump { get; }
        string Seed0 { get; }
        string Seed1 { get; }
    }

    internal interface IRetailFrame
    {
        char Animation { get; }
    }

    internal interface ISpreadFinderFrame
    {
        string Seed { get; }
        string EC { get; }

        byte H { get; }
        byte A { get; }
        byte B { get; }
        byte C { get; }
        byte D { get; }
        byte S { get; }

        string Height { get; }
    }

    internal interface IOverworldFrame
    {
        byte Step { get; }
        string Species { get; }
        string Shiny { get; }
        char Brilliant { get; }

        byte Level { get; }
        string Ability { get; }
        string Nature { get; }
        char Gender { get; }

        byte H { get; }
        byte A { get; }
        byte B { get; }
        byte C { get; }
        byte D { get; }
        byte S { get; }

        string Mark { get; }
        string PID { get; }
        string EC { get; }
        string Height { get; }

        string Item { get; }
        string EggMove { get; }
    }

    public class OverworldFrame : IOverworldFrame, IBasicFrame, IRetailFrame
    {
        public string Advances { get; set; } = string.Empty;
        public string Jump { get; set; } = string.Empty;
        public byte Step { get; set; } = 0;
        public char Animation { get; set; } = ' ';
        public string Species { get; set; } = string.Empty;
        public string Shiny { get; set; } = string.Empty;
        public char Brilliant { get; set; } = ' ';

        public byte Level { get; set; } = 0;
        public string Ability { get; set; } = string.Empty;
        public string Nature { get; set; } = string.Empty;
        public char Gender { get; set; } = ' ';

        public byte H { get; set; } = 0;
        public byte A { get; set; } = 0;
        public byte B { get; set; } = 0;
        public byte C { get; set; } = 0;
        public byte D { get; set; } = 0;
        public byte S { get; set; } = 0;

        public string Mark { get; set; } = string.Empty;
        public string EC { get; set; } = string.Empty;
        public string PID { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;

        public string Item { get; set; } = string.Empty;
        public string EggMove { get; set; } = string.Empty;

        public string Seed0 { get; set; } = string.Empty;
        public string Seed1 { get; set; } = string.Empty;
    }

    public class SpreadFinderFrame : ISpreadFinderFrame
    {
        public string Seed { get; set; } = string.Empty;
        public string EC { get; set; } = string.Empty;

        public byte H { get; set; } = 0;
        public byte A { get; set; } = 0;
        public byte B { get; set; } = 0;
        public byte C { get; set; } = 0;
        public byte D { get; set; } = 0;
        public byte S { get; set; } = 0;

        public string Height { get; set; } = string.Empty;
    }

    public class MenuCloseFrame : IBasicFrame
    {
        public string Advances { get; set; } = string.Empty;
        public string Jump { get; set; } = string.Empty;
        public string Seed0 { get; set; } = string.Empty;
        public string Seed1 { get; set; } = string.Empty;
    }

    public class LotoIDFrame : IBasicFrame, IRetailFrame
    {
        public string Advances { get; set; } = string.Empty;
        public string Jump { get; set; } = string.Empty;
        public char Animation { get; set; } = ' ';
        public string ID { get; set; } = string.Empty;
        public string Prize { get; set; } = string.Empty;
        public string Seed0 { get; set; } = string.Empty;
        public string Seed1 { get; set; } = string.Empty;
    }

    public class CramomaticFrame : IBasicFrame, IRetailFrame
    {
        public string Advances { get; set; } = string.Empty;
        public string Jump { get; set; } = string.Empty;
        public char Animation { get; set; } = ' ';
        public string Prize { get; set; } = string.Empty;
        public bool Bonus { get; set; } = false;
        public string Seed0 { get; set; } = string.Empty;
        public string Seed1 { get; set; } = string.Empty;
    }

    public class WattTraderFrame : IBasicFrame, IRetailFrame
    {
        public string Advances { get; set; } = string.Empty;
        public string Jump { get; set; } = string.Empty;
        public char Animation { get; set; } = ' ';
        public string Highlight { get; set; } = string.Empty;
        public string Regular { get; set; } = string.Empty;
        public string Seed0 { get; set; } = string.Empty;
        public string Seed1 { get; set; } = string.Empty;
    }

    public class DiggingPaFrame : IBasicFrame, IRetailFrame
    {
        public string Advances { get; set; } = string.Empty;
        public string Jump { get; set; } = string.Empty;
        public char Animation { get; set; } = ' ';
        public ulong Watts { get; set; } = 0;
        public string Actual { get; set; } = string.Empty;
        public string Reported { get; set; } = string.Empty;
        public string Seed0 { get; set; } = string.Empty;
        public string Seed1 { get; set; } = string.Empty;
    }

    public class WailordFrame : IBasicFrame, IRetailFrame
    {
        public string Advances { get; set; } = string.Empty;
        public string Jump { get; set; } = string.Empty;
        public char Animation { get; set; } = ' ';
        public char Respawn { get; set; } = 'N';
        public string Seed0 { get; set; } = string.Empty;
        public string Seed1 { get; set; } = string.Empty;
    }
}
