namespace OODProject;

public class Wall : Field
{
    public Wall(Position position) : base(position)
    {
    }
    public override bool CanEnter()
    {
        return false;
    }

    public override char GetSymbol()
    {
        return Symbols.Wall;
    }
}