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
        if (!_inventory.Contains(item))
        {
            throw new InvalidOperationException($"Item is not in inventory");
        }
        if (item.IsTwoHanded())
        {
            if (LeftHand != null)
            {
                _inventory.Add(LeftHand);
                LeftHand = item;
            }

            if (RightHand != null)
            {
                _inventory.Add(RightHand);
                RightHand = item;
            }
            _inventory.Remove(item);
        }
        else
        {
            if (slot == HandSlot.Right)
            {
                if (RightHand != null)
                {
                    _inventory.Add(RightHand);
                    RightHand = item;
                }
                _inventory.Remove(item);
            }

            else if (slot == HandSlot.Left)
            {
                if (LeftHand != null)
                {
                    _inventory.Add(LeftHand);
                    LeftHand = item;
                }
                _inventory.Remove(item);
            }
        }
    }

    public override char GetSymbol()
    {
        return Symbols.Player;
    }

    public void PickUpItem(Item item)
    {
        _inventory.Add(item);
    }

    public Item DropItem(Item item)
    {
        _inventory.Remove(item);
        
        return item;
    }

    public void Unequip(HandSlot slot)
    {
        if (slot == HandSlot.Right && RightHand != null)
        {
            _inventory.Add(RightHand);
            RightHand = null;
        }
        else if (slot == HandSlot.Left && LeftHand != null)
        {
            _inventory.Add(LeftHand);
            LeftHand = null;
        }
    }
}