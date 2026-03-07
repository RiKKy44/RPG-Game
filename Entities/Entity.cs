namespace OODProject.Entities;

public abstract class Entity
{
    private int _strength;

    private int _dexterity;

    private int _health;

    private int _luck;

    private int _aggression;

    private int _wisdom;

    private Position _position;
    
    public int Strength
    {
        get { return _strength; }
        
        protected set { _strength = value; }
    }

    public int Dexterity
    {
        get { return _dexterity; }
        protected set { _dexterity = value; }
    }

    public int Health
    {
        get { return _health; }
        protected set { _health = value; }
    }

    public int Luck
    {
        get { return _luck; }
        protected set { _luck = value; }
    }

    public int Aggression
    {
        get { return _aggression; }
        protected set { _aggression = value; }
    }

    public int Wisdom
    {
        get { return _wisdom; }
        protected set { _wisdom = value; }
    }

    public Position CurrentPosition
    {
        get { return _position; }
        
        protected set {_position = value; }
    }

    Entity(Position position, int strength, int dexterity, int health, int luck, int aggression, int wisdom)
    {
        CurrentPosition = position;
        
        Strength = strength;
        
        Dexterity = dexterity;
        
        Health = health;
        
        Luck = luck;
        
        Aggression = aggression;
        
        Wisdom = wisdom;
    }
    
    
}