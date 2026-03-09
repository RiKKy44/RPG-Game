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
    
    public abstract void Add(int value);
    
    public abstract void Remove(int value);
    
}