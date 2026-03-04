namespace OODProject;

public class SingleHanded : Weapon
{
    public override bool IsEquipable()
    {
        return true;
    }
    public override char GetSymbol()
    {
        return 'S';
    }
    
    public override void Interact(){}

    public override bool IsTwoHanded()
    {
        return false;
    }

    public SingleHanded(int damage, int weight) : base(damage, weight) {
    }
}