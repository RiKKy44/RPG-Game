using OODProject.Combat;

namespace OODProject;

public abstract class Currency : Item
{
    private int _count;

    public int Count
    {
        get {return _count;}

        protected set
        {
            _count = value;
        }
        
    }

    public override bool IsEquipable()
    {
        return false;
    }

}