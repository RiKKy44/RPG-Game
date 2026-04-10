using OODProject.Items.Weapons.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Combat;
public class MagicalAttack : IAttackMethod
{
    public int CalculatedDamage { get; private set; }
    public void Visit(LightWeapon weapon)
    {
        CalculatedDamage = 1;
    }
    public void Visit(HeavyWeapon weapon)
    {
        CalculatedDamage = 1;
    }
    public void Visit(MagicalWeapon weapon)
    {
        CalculatedDamage = weapon.GetDamage();
    }
    public void Visit(Item item)
    {
        CalculatedDamage = 1;
    }
}