namespace OODProject;

public class Gold : Currency
{
    public override void Remove(int amount)
    {
        int newValue = Count - amount;

        if (newValue < 0)
        {
            newValue = 0;
        }
        Count = newValue;
    }

    public override void Add(int amount)
    {
        Count += amount;
    }

    public Gold(int value) : base(value){}

    public override char GetSymbol()
    {
        return Symbols.Gold;
    }
}