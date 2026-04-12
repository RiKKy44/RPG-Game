using System;

namespace OODProject;

public abstract class Weapon : Item
{
    private int _damage;
    private int _weight;

    public int Weight
    {
        get { return _weight; }
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Weight cannot be less than 0");
            }
            _weight = value;
        }
    }

    public int Damage
    {
        get { return _damage; }
        protected set
        {
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
        if (this.IsTwoHanded())
        {
            return Symbols.DoubleHanded;
        }
        else
        {
            return Symbols.SingleHanded;
        }
    }

    public override bool IsEquipable()
    {
        return true;
    }

    public override int GetDamage()
    {
        return _damage;
    }

    public override int GetWeight()
    {
        return _weight;
    }
}