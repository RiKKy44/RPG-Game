using OODProject.Entities;
using OODProject.Items.Weapons.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Combat;
public class NormalAttack : IAttackMethod
{
    private Entity _attacker;
    public int CalculatedDefense { get; private set; }
    public int CalculatedDamage { get; private set; }

    public NormalAttack(Entity attacker)
    {
        _attacker = attacker;
    }
    public void Visit(LightWeapon weapon, Item decorator)
    {
        CalculatedDamage = weapon.GetDamage();

        CalculatedDefense = _attacker.Dexterity + _attacker.Luck + decorator.GetDefenseBonus();
    }
    public void Visit(HeavyWeapon weapon, Item decorator)
    {
        CalculatedDamage = weapon.GetDamage();

        CalculatedDefense = _attacker.Strength + _attacker.Luck + decorator.GetDefenseBonus();
    }
    public void Visit(MagicalWeapon weapon, Item decorator)
    {
        CalculatedDamage = 1;

        CalculatedDefense = _attacker.Dexterity + _attacker.Luck + decorator.GetDefenseBonus();
    }
    public void Visit(Item item, Item decorator)
    {
        CalculatedDamage = 0;

        CalculatedDefense = 0;
    }
}