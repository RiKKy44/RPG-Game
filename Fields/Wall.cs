namespace OODProject;

public class Wall : Field
{
    public override bool CanEnter()
    {
        return false;
    }

    public override char GetSymbol()
    {
        return Symbols.Wall;
    }
    public override string GetName()
    {
        return "Wall";
    }

}