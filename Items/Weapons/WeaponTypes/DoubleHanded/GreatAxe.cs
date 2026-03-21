using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Items.Weapons.WeaponTypes.DoubleHanded;




public class GreatAxe : DoubleHanded
{
    public override string GetName()
    {
        return "Great Axe";
    }
    public override char GetSymbol()
    {
        return base.GetSymbol();
    }

    public GreatAxe(int damage = 60, int weight = 45) : base(damage, weight) { }
}
