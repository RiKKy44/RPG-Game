using OODProject.Entities;
using OODProject.Items.Weapons.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Combat;

public class StealthAttack : IAttackMethod
{
    private Entity _attacker;
    public int CalculatedDamage { get; private set; }

    public int CalculatedDefense { get; private set; }

    public StealthAttack(Entity attacker)
    {
        _attacker = attacker;
    }
    public void Visit(LightWeapon weapon, Item decorator)
    {
        CalculatedDamage = weapon.GetDamage()*2;

        CalculatedDefense = _attacker.Dexterity + decorator.GetDefenseBonus();
    }
    public void Visit(HeavyWeapon weapon, Item decorator)
    {
        CalculatedDamage = weapon.GetDamage()/2;

        CalculatedDefense = _attacker.Strength + decorator.GetDefenseBonus();
    }
    public void Visit(MagicalWeapon weapon, Item decorator)
    {
        CalculatedDamage = 1;

        CalculatedDefense = 0;
    }
    public void Visit(Item item, Item decorator)
    {
        CalculatedDamage = 1;

        CalculatedDefense = 0;
    }
}