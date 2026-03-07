namespace OODProject;

public abstract class Currency : Item
{
    private int _value;

    private int _count;
    
    public int Value
    {
        get {return _value;}
        
        init
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException($"Value must be greater or equal to 0\n");
            }
            _value = value;
        }
    }

    public int Count
    {
        get {return _count;}

        protected set
        {
            _count = value;
        }
        
    }
    public Currency(int value)
    {
        _value = value;
    }
    
    public abstract void Add(int value);
    
    public abstract void Remove(int value);
    
}