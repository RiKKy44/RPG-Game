using OODProject.Items.Weapons.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Combat;

public class StealthAttack : IAttackMethod
{
    public int CalculatedDamage { get; private set; }
    public void Visit(LightWeapon weapon)
    {
        CalculatedDamage = weapon.GetDamage()*2;
    }
    public void Visit(HeavyWeapon weapon)
    {
        CalculatedDamage = weapon.GetDamage()/2;
    }
    public void Visit(MagicalWeapon weapon)
    {
        CalculatedDamage = 1;
    }
    public void Visit(Item item)
    {
        CalculatedDamage = 1;
    }
}