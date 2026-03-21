namespace OODProject.Entities;

public class Player : Entity
{
    private int _inventorySize;

    private List<Item> _inventory;

    private int _goldCount;

    private int _coinCount;
    public int GoldCount => _goldCount;
    public int CoinCount => _coinCount;
    public IReadOnlyList<Item> Inventory => _inventory;

    public int InventorySize
    {
        get { return _inventorySize; }
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentException("Inventory size can't be negative");
            }

            _inventorySize = value;
        }
    }

    public Player(int inventorySize = 10, Position position = default) : base(position)
    {
        InventorySize = inventorySize;
        Strength = 5;
        Health = 50;
        Dexterity = 5; 
        Wisdom = 5;
        Luck = 5;
        Aggression = 5;
        _inventory = new List<Item>();
        _goldCount = 0;
        _coinCount = 0;
        MaxHealth = 100;
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
                    if (RightHand.IsTwoHanded())
                    {
                        _inventory.Add(RightHand);
                        LeftHand = null;
                    }
                    else
                    {
                        _inventory.Add(RightHand);
                    }
                }
                RightHand = item;
                _inventory.Remove(item);
            }

            else if (slot == HandSlot.Left)
            {
                if (LeftHand != null)
                {
                    if (LeftHand.IsTwoHanded())
                    {
                        _inventory.Add(LeftHand);
                        RightHand = null;
                    }
                    else
                    {
                        _inventory.Add(LeftHand);
                    }
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

    public override string GetName()
    {
        return "Player";
    }
    public void PickUpItem(Item item)
    {
        item.OnPickUp(this);
    }
    public Item DropItem(Item item)
    {
        if (!_inventory.Contains(item) && !LeftHand.Equals(item) && !RightHand.Equals(item))
        {
            throw new InvalidOperationException("Item is not in inventory or hands");
        }

        if (LeftHand.Equals(item))
        {
            LeftHand = null;
        }

        else if (RightHand.Equals(item))
        {
            RightHand = null;
        }
        else
        {
            _inventory.Remove(item);
        }
        return item;
    }

    public Item DropItem(int index)
    {
        Item item = _inventory[index];
        _inventory.RemoveAt(index);
        return item;
    }
    public void Unequip(HandSlot slot)
    {
        if ((RightHand!=null && RightHand.IsTwoHanded()) || (LeftHand!=null && LeftHand.IsTwoHanded()))
        {
            Item twoHanded = RightHand ?? LeftHand;
            AddToInventory(twoHanded);
            RightHand = null;
            LeftHand = null;
        }
        else if (slot == HandSlot.Right && RightHand != null)
        {
            AddToInventory(RightHand);
            RightHand = null;
        }
        else if (slot == HandSlot.Left && LeftHand != null)
        {
            AddToInventory(LeftHand);
            LeftHand = null;
        }
    }

    public void AddGold(int gold)
    {
        if (gold > 0)
        {
            _goldCount += gold;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(gold), "Gold cannot be negative");
        }
    }

    public void AddCoin(int coin)
    {
        if (coin > 0)
        {
            _coinCount += coin;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(coin), "Coin cannot be negative");
        }
    }

    internal void AddToInventory(Item item)
    {
        if (item != null)
        {
            if (_inventory.Count < _inventorySize)
            {
                _inventory.Add(item);
            }
            else
            {
                throw new InvalidOperationException("Inventory is full");
            }
        }
    }


    public override void Heal(int value)
    {
        if(Health + value > MaxHealth)
        {
            Health = MaxHealth;
        }
        else
        {
            Health += value;
        }
    }
}