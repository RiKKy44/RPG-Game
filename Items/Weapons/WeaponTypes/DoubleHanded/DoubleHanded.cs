using OODProject.Entities;

namespace OODProject.Items.Weapons.WeaponTypes.DoubleHanded;

public abstract class DoubleHanded : Weapon
{
    public DoubleHanded(int damage, int weight) : base(damage, weight){}

    public override bool IsTwoHanded()
    {
        return true;
    }

    public override void OnPickUp(Player player)
    {
        player.AddToInventory(this);
    }

    public override char GetSymbol()
    {
        return Symbols.DoubleHanded;
    }
}