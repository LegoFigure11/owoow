namespace owoow.Core.Interfaces
{
    internal interface IFrame
    {
        string Advances { get; }
        string Jump { get; }
        char Animation { get; }
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

        string Seed0 { get; }
        string Seed1 { get; }
    }

    internal interface IMenuCloseFrame
    {
        string Advances { get; }
        string Jump { get; }
        string Seed0 { get; }
        string Seed1 { get; }
    }

    public class Frame : IFrame
    {
        public string Advances { get; set; } = string.Empty;
        public string Jump { get; set; } = string.Empty;
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

    public class MenuCloseFrame : IMenuCloseFrame
    {
        public string Advances { get; set; } = string.Empty;
        public string Jump { get; set; } = string.Empty;
        public string Seed0 { get; set; } = string.Empty;
        public string Seed1 { get; set; } = string.Empty;
    }
}
