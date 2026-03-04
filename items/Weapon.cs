namespace OODProject;

public abstract class Weapon : Item
{
    private int _damage;

    private int _weight;

    protected int Weight
    {
        get
        {
            return _weight;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Weight cannot be less than 0");
            }
            _weight = value;
        }
    }
    protected int Damage
    {
        get { return _damage; }
        set {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Damage cannot be less than 0");
            }
            _damage = value;
        }
    }
    public Weapon(int damage, int weight)
    {
        Damage = damage;
        Weight = weight;
    }
    public override char GetSymbol()
    {
        return 'W';
    }

    public override void Interact()
    {
    }
    public override bool IsEquipable()
    {
        return true;
    }

    public override int GetDamage()
    {
        return _damage;
    }

    public int GetWeight()
    {
        return _weight;
    }

    public abstract bool IsTwoHanded();
}