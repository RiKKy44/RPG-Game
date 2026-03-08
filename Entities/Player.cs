namespace OODProject.Entities;

public class Player : Entity
{
    private int _inventorySize;

    private List<Item> _inventory;
    
    public IReadOnlyList<Item> Inventory => _inventory;

    public int InventorySize
    {
        get{ return _inventorySize;}
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentException("Inventory size can't be negative");
            }
        }
    }

    public Player(int inventorySize, Position position = default) : base(position)
    {
        InventorySize = inventorySize;
        Strength = 5;
        Health = 100;
        Dexterity = 5;
        Wisdom = 5;
        Luck = 5;
        Aggression = 5;
    }

    public void Equip(Item item, HandSlot slot)
    {
        if (item.IsTwoHanded())
        {
            LeftHand = item;
            RightHand = item;
        }
        else
        {
            if (slot == HandSlot.Right)
            {
                RightHand = item;
            }

            else if (slot == HandSlot.Left)
            {
                LeftHand = item;
            }
        }
    }

    public override char GetSymbol()
    {
        return Symbols.Player;
    }
    
    
}