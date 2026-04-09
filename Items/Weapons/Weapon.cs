using OODProject.Items.Weapons.Modifiers;

namespace OODProject;

public abstract class Weapon : Item
{
    private int _baseDamage;
    private int _baseWeight;

    private List<IWeaponModifier> _modifiers = new List<IWeaponModifier>();

    public int Damage
    {
        get { return _baseDamage + _modifiers.Sum(m => m.DamageBonus); }
        protected set
        {
            if (value < 0) throw new ArgumentOutOfRangeException("Damage cannot be less than 0");
            _baseDamage = value;
        }
    }

    public int Weight
    {
        get { 
            return _baseWeight + _modifiers.Sum(m => m.WeightBonus); 
        }
        protected set
        {
            if (value < 0) throw new ArgumentOutOfRangeException("Weight cannot be less than 0");
            _baseWeight = value;
        }
    }
    public abstract string BaseName { get; }

    public Weapon(int damage, int weight)
    {
        Damage = damage;
        Weight = weight;
    }

    public void AddModifier(IWeaponModifier modifier)
    {
        _modifiers.Add(modifier);
    }

    public override string GetName()
    {
        string prefixes = string.Join("", _modifiers.Select(m => m.NamePrefix));
        return $"{prefixes}{BaseName}";
    }

    public override char GetSymbol() => Symbols.Weapon;
    public override bool IsEquipable() => true;
    public override int GetDamage() => Damage; 
    public override int GetWeight() => Weight; 
}