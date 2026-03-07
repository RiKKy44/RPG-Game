namespace OODProject;

public class EmptyField : Field
{
    public EmptyField(Position position) : base(position){}

    public override bool CanEnter()
    {
        return true;
    }

    public override char GetSymbol()
    {
        return Symbols.EmptyField;
    }
    
}