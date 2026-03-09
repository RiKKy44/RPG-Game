using OODProject.Entities;

namespace OODProject;

public class SingleHanded : Weapon
{
    public override bool IsEquipable()
    {
        return true;
    }
    public override bool IsTwoHanded()
    {
        return false;
    }

    public SingleHanded(int damage, int weight) : base(damage, weight) {
    }

    public override void OnPickUp(Player player)
    {
        player.AddToInventory(this);
    }

    public override char GetSymbol()
    {
        return Symbols.SingleHanded;
    }
}