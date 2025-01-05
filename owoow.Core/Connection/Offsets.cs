namespace owoow.Core.Connection;

public abstract class Offsets
{
    public const string SwordID = "0100ABF008968000";
    public const string ShieldID = "01008DB008C2C000";

    public const uint MainRNG = 0x4c2aac18;
    public const int MainRNG_Size = 0x10;

    public const uint WildPokemon = 0x8fea3648;
    public const int WildPokemon_Size = 0x158;

    public const uint MyStatus = 0x45068f18;
    public const int MyStatus_Size = 0x110;

    public const uint MyItems = 0x45067a98;
    public const int MyItems_Size = 0x12f8;

    public const uint DexRecommendation = 0x45072b18;
    public const int DexRecommendation_Size = 0x20;
}
