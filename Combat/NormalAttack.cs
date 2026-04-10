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
    public void Visit(LightWeapon weapon)
    {
        CalculatedDamage = weapon.GetDamage();

        CalculatedDefense = _attacker.Dexterity + _attacker.Luck + weapon.GetDefenseBonus();
    }
    public void Visit(HeavyWeapon weapon)
    {
        CalculatedDamage = weapon.GetDamage();

        CalculatedDefense = _attacker.Strength + _attacker.Luck + weapon.GetDefenseBonus();
    }
    public void Visit(MagicalWeapon weapon)
    {
        CalculatedDamage = 1;

        CalculatedDefense = _attacker.Dexterity + _attacker.Luck + weapon.GetDefenseBonus();
    }
    public void Visit(Item item)
    {
        CalculatedDamage = 0;

        CalculatedDefense = 0;
    }
}