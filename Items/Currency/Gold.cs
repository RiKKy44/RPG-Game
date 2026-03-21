using OODProject.Entities;

namespace OODProject;

public class Gold : Currency
{
    public const int Value = 1;
    public override char GetSymbol()
    {
        return Symbols.Gold;
    }

    public override void OnPickUp(Player player)
    {
        player.AddGold(Value);
    }

    public override string GetName()
    {
        return "Gold";
    }
}