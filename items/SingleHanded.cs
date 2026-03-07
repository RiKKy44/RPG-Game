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
}