namespace OODProject;

public class Board
{

    private Field[,] _fields;
    
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
}