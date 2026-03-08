using OODProject.Entities;
namespace OODProject;

public abstract class Item : IDrawable
{
    public abstract char GetSymbol();
    public virtual bool IsEquipable()
    {
        return false;
    }
    public virtual bool IsTwoHanded()
    {
        return false;
    }
    public virtual int GetDamage()
    {
        return 0;
    }

    public virtual int GetWeight()
    {
        return 0;
    }

    public abstract void OnPickUp(Player player);
}
