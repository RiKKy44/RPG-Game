using OODProject.Items.Weapons.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Combat;

public interface IAttackMethod
{
    int CalculatedDamage { get; }
    int CalculatedDefense { get; }
    void Visit(LightWeapon weapon, Item decorator);
    void Visit(HeavyWeapon weapon, Item decorator);
    void Visit(MagicalWeapon weapon, Item decorator);
    void Visit(Item item, Item decorator);
}