namespace OODProject;

public record struct Position(int X, int Y)
{
    public int X;
    public int Y;

    public static Position operator +(Position a, Position b)
    {
        return new Position(a.X + b.X, a.Y + b.Y);
    }
}

public static class Direction
{
    public static Position Left = new Position(-1, 0);
    public static Position Right = new Position(1, 0);
    public static Position Down = new Position(0, -1);
    public static Position Up = new Position(0, 1);
}