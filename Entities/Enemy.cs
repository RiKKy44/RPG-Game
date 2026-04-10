using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Entities;


public class Enemy : Entity
{
    private string _name;
    private char _symbol;

    public int AttackValue { get; private set; }
    public int Armor { get; private set; }

    public Enemy(Position position, string name, char symbol, int health, int attackValue, int armor)
        : base(position, strength: 5, dexterity: 5, health: health, luck: 0, aggression: 10, wisdom: 0, maxHealh: health)
    {
        _name = name;
        _symbol = symbol;
        AttackValue = attackValue;
        Armor = armor;
    }

    public override string GetName() => _name;
    public override char GetSymbol() => _symbol;

    public override void Heal(int value)
    {
        Health = Math.Min(Health + value, MaxHealth);
    }
}