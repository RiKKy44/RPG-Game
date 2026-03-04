namespace OODProject;

public abstract class Item : IDrawable, IInteractable
{
    public abstract void Interact();
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
}
