using OODProject.Items.Weapons.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Combat;

public interface IAttackMethod
{
    void Visit(LightWeapon weapon);
    void Visit(HeavyWeapon weapon);
    void Visit(MagicalWeapon weapon);
    void Visit(Item item);
}