using OODProject.Entities;

namespace OODProject;

public class Gold : Currency
{
    public const int Value = 1;
    public override void Remove(int amount)
    {
        int newValue = Count - amount;

        if (newValue < 0)
        {
            newValue = 0;
        }
        Count = newValue;
    }

    public override void Add(int amount)
    {
        Count += amount;
    }
    
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