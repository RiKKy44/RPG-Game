using OODProject.Entities;
namespace OODProject;

public class Coin : Currency
{
    public const int Value = 5;
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
        return Symbols.Coin;
    }
    public override void OnPickUp(Player player)
    {
        player.AddCoin(Value);
    }


    public override string GetName()
    {
        return "Coin";
    }
}