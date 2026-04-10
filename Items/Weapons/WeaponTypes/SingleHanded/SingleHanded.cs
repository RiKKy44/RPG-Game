using OODProject.Entities;

namespace OODProject.Items.Weapons.WeaponTypes.SingleHanded;

public abstract class SingleHanded : Weapon
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

    public override char GetSymbol()
    {
        return Symbols.SingleHanded;
    }

}