namespace OODProject.Entities;

public abstract class Entity : IDrawable
{
    private int _strength;

    private int _dexterity;

    private int _health;

    private int _luck;

    private int _aggression;

    private int _wisdom;

    private int _maxHealth;

    private Position _position;

    private Item? _leftHand;
    
    private Item? _rightHand;

    public virtual int Strength
    {
        get { return _strength; }

        protected set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException($"Strength cannot be less than 0");
            }
            _strength = value;
        }
    }
    public virtual int Dexterity
    {
        get { return _dexterity; }
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException($"Dexterity cannot be less than 0");
            }
            _dexterity = value;
        }
    }

    public virtual int Health
    {
        get { return _health; }
        protected set
        {
            _health = value;
        }
    }

    public virtual int Luck
    {
        get { return _luck; }
        protected set
        {
            
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException($"Luck cannot be less than 0");
            }
            _luck = value;
        }
    }

    public virtual int Aggression
    {
        get { return _aggression; }
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException($"Aggression cannot be less than 0");
            }
            _aggression = value;
        }
    }


    public int MaxHealth
    {
        get
            { return _maxHealth; }
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException($"Max Health cannot be less than 0");
            }

            _maxHealth = value;
        }
    }
    public virtual int Wisdom
    {
        get { return _wisdom; }
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException($"Wisdom cannot be less than 0");
            }
            _wisdom = value;
        }
    }

    public Item? LeftHand
    {
        get { return _leftHand; }
        protected set { _leftHand = value; }
    }

    public Item? RightHand
    {
        get { return _rightHand; }
        protected set { _rightHand = value; }
    }
    public Position CurrentPosition
    {
        get { return _position; }
        
        protected set {_position = value; }
    }

    public Entity(Position position, int strength, int dexterity, int health, int luck, int aggression, int wisdom, int maxHealh)
    {
        CurrentPosition = position;
        
        Strength = strength;
        
        Dexterity = dexterity;
        
        Health = health;
        
        Luck = luck;
        
        Aggression = aggression;
        
        Wisdom = wisdom;
        
        LeftHand = null;
        
        RightHand = null;

        MaxHealth = maxHealh;
    }

    public Entity(Position position) : this(position, 0, 0, 0, 0, 0, 0, 0) { }

    public abstract char GetSymbol();

    public abstract void Heal(int value);
    public abstract string GetName();
    public void MoveTo(Position newPosition)
    {
        CurrentPosition = newPosition;
    }
    public virtual void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0) Health = 0;
    }
    
}