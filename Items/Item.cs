using OODProject.Entities;
namespace OODProject;

public abstract class Item : IDrawable
{
    public abstract void OnPickUp(Player player);
    public abstract char GetSymbol();
    public abstract string GetName();
    public abstract bool IsEquipable();
    public virtual bool IsTwoHanded()
    {
        return false;
    }
    public virtual int GetWeight()
    {
        return 0;
    }

    public virtual int GetDamage()
    {
        return 0;
    }
    public virtual int GetDefenseBonus()
    {
        return 0;
    }
    public virtual int GetLuckBonus()
    {
        return 0;
    }

    public virtual int GetStrengthBonus()
    {
        return 0;
    }

    public virtual int GetDexterityBonus()
    {
        return 0;
    }

    public virtual int GetWisdomBonus()
    {
        return 0;
    }

    public virtual int GetAggressionBonus()
    {
        return 0;
    }
}
