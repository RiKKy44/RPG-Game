using OODProject.Entities;

namespace OODProject;

public class Board
{

    private Field[,] _fields;
    public List<Enemy> Enemies { get; } = new List<Enemy>();
    public Board()
    {
        _fields = new Field[GameConfig.Width, GameConfig.Height];
        for (int i = 0; i < GameConfig.Width; i++)
        {
            for (int j = 0; j < GameConfig.Height; j++)
            {
                Position position = new Position(i, j);
                if (i == GameConfig.Width - 1 || j == GameConfig.Height - 1|| i==0 || j==0)
                {
                    SetField(position, new Wall());
                }
                else
                {
                    SetField(position, new EmptyField());
                }
            }
        }
    }

    internal void SetField(Position position, Field field)
    {
        if (position.X < 0 || position.X >= GameConfig.Width || position.Y < 0 || position.Y >= GameConfig.Height)
        {
            throw new ArgumentOutOfRangeException("Set field position out of bounds");
        }
        _fields[position.X, position.Y] = field;
    }

    public Field GetField(Position position)
    {
        if (position.X < 0 || position.X >= GameConfig.Width ||
            position.Y < 0 || position.Y >= GameConfig.Height)
        {
            throw new ArgumentOutOfRangeException("Position out of bounds");
        }
        return _fields[position.X, position.Y];
    }
    public IEnumerable<Field> GetAllFields()
    {
        return _fields.Cast<Field>();
    }
    public bool CanEnter(Position position)
    {
        return _fields[position.X, position.Y].CanEnter();
    }

    public int CalculateDistance(Position start, Position target)
    {
        if (start == target) return 0;

        var queue = new Queue<(Position Pos, int Dist)>();
        var visited = new HashSet<Position>();

        queue.Enqueue((start, 0));
        visited.Add(start);

        Position[] directions = { Direction.Up, Direction.Down, Direction.Left, Direction.Right };

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Pos == target)
                return current.Dist;

            foreach (var dir in directions)
            {
                Position next = current.Pos + dir;

                if (next.X >= 0 && next.X < GameConfig.Width &&
                    next.Y >= 0 && next.Y < GameConfig.Height &&
                    GetField(next).CanEnter() &&
                    !visited.Contains(next))
                {
                    visited.Add(next);
                    queue.Enqueue((next, current.Dist + 1));
                }
            }
        }

        return -1; 
    }
}