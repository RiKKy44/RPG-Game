namespace OODProject;

public class DoubleHanded : Weapon
{
    public DoubleHanded(int damage, int weight) : base(damage, weight){}

    public override bool IsTwoHanded()
    {
        return true;
    }
    
}