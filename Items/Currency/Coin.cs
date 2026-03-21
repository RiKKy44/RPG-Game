using OODProject.Entities;
namespace OODProject;

public class Coin : Currency
{
    public const int Value = 5;
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