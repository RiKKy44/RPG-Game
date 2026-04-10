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
    public void Visit(LightWeapon weapon)
    {
        CalculatedDamage = weapon.GetDamage()*2;

        CalculatedDefense = _attacker.Dexterity + weapon.GetDefenseBonus();
    }
    public void Visit(HeavyWeapon weapon)
    {
        CalculatedDamage = weapon.GetDamage()/2;

        CalculatedDefense = _attacker.Strength + weapon.GetDefenseBonus();
    }
    public void Visit(MagicalWeapon weapon)
    {
        CalculatedDamage = 1;

        CalculatedDefense = 0;
    }
    public void Visit(Item item)
    {
        CalculatedDamage = 1;

        CalculatedDefense = 0;
    }
}