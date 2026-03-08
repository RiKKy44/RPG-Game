namespace OODProject.Entities;

public class Player : Entity
{
    private int _inventorySize;

    private List<Item> _inventory;

    private Gold _gold;
    
    private Coin _coin;
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
            _inventorySize = value;
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
        _inventory = new List<Item>();
    }

    public void Equip(Item item, HandSlot slot)
    {
        if (!item.IsEquipable())
        {
            throw new InvalidOperationException("Cannot equip an item that is not equipable");
        }
        if (!_inventory.Contains(item))
        {
            throw new InvalidOperationException($"Item is not in inventory");
        }
        if (item.IsTwoHanded())
        {
            Unequip(HandSlot.Left);
            Unequip(HandSlot.Right);
            RightHand = item;
            LeftHand = item;
            _inventory.Remove(item);
        }
        else
        {
            if (slot == HandSlot.Right)
            {
                if (RightHand != null)
                {
                    _inventory.Add(RightHand);
                }
                RightHand = item;
                _inventory.Remove(item);
            }

            else if (slot == HandSlot.Left)
            {
                if (LeftHand != null)
                {
                    _inventory.Add(LeftHand);
                }
                LeftHand = item;
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
        if (_inventory.Count >= _inventorySize)
        {
            throw new InvalidOperationException("Inventory is full");
        }
        _inventory.Add(item);
    }

    public Item DropItem(Item item)
    {
        if (!_inventory.Remove(item))
        {
            throw new InvalidOperationException("Item is not in inventory");
        }
        return item;
    }

    public void Unequip(HandSlot slot)
    {
        if ((RightHand!=null && RightHand.IsTwoHanded()) || (LeftHand!=null && LeftHand.IsTwoHanded()))
        {
            Item twoHanded = RightHand ?? LeftHand;
            _inventory.Add(twoHanded);
            RightHand = null;
            LeftHand = null;
        }
        else if (slot == HandSlot.Right && RightHand != null)
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