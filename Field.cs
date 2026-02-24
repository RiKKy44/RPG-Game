namespace OODProject;

public abstract class Field : IDrawable
{
    private Position _position;
    protected Position FieldPosition
    {
        get
        {
            return _position;
        }
        set
        {
            if (value.X < 0 || value.Y < 0 || value.X >= GameConfig.Width || value.Y >= GameConfig.Height)
            {
                throw new ArgumentOutOfRangeException($"No such position: {value}");
            }
            _position = value;
        }
    }

    private List<Item> _items;
    
    public IReadOnlyList<Item> Items => _items;
    
    public Field(Position fieldPosition)
    {
        _items = new List<Item>();
        FieldPosition = fieldPosition;
    }
    
    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        if (!_items.Remove(item))
        {
            throw new InvalidOperationException($"Cannot remove item {item}");
        }
    }
    public abstract bool CanEnter();

    public abstract void Draw();
}