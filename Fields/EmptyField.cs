namespace OODProject;

public class EmptyField : Field
{
    public override bool CanEnter()
    {
        return true;
    }
    public override char GetSymbol()
    {
        return Symbols.EmptyField;
    }
    public override string GetName()
    {
        return "EmptyField";
    }
}