namespace OODProject;

public abstract class Field : IDrawable
{
    

    private List<Item> _items;
    
    public IReadOnlyList<Item> Items => _items;
    
    public Field()
    {
        _items = new List<Item>();
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

    public abstract char GetSymbol();
}