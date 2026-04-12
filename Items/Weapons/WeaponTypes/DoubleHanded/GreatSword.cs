using OODProject.Items.Weapons.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Items.Weapons.WeaponTypes.DoubleHanded;

public class GreatSword : HeavyWeapon
{
    public override string GetName()
    {
        return "Great Sword";
    }
    public override char GetSymbol()
    {
        return base.GetSymbol();
    }

    public GreatSword(int damage = 55, int weight = 40) : base(damage, weight) { }
}
