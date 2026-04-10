using OODProject.Entities;
using OODProject.Items.Weapons.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Combat;
public class MagicalAttack : IAttackMethod
{

    private Entity _attacker;
    public int CalculatedDefense { get; private set; }
    public int CalculatedDamage { get; private set; }


    public MagicalAttack(Entity attacker)
    {
        _attacker = attacker;
    }
    public void Visit(LightWeapon weapon)
    {
        CalculatedDamage = 1;

        CalculatedDefense = _attacker.Luck + weapon.GetDefenseBonus();
    }
    public void Visit(HeavyWeapon weapon)
    {
        CalculatedDamage = 1;

        CalculatedDefense = _attacker.Luck + weapon.GetDefenseBonus();
    }
    public void Visit(MagicalWeapon weapon)
    {
        CalculatedDamage = weapon.GetDamage();

        CalculatedDefense = _attacker.Wisdom *2 + weapon.GetDefenseBonus();
    }
    public void Visit(Item item)
    {
        CalculatedDamage = 1;

        CalculatedDefense = _attacker.Luck;
    }
}