using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Items.Weapons.WeaponTypes.SingleHanded;



public class Dagger : SingleHanded
{
    public override string GetName()
    {
        return "Dagger";
    }
    public override char GetSymbol()
    {
        return base.GetSymbol();
    }

    public Dagger(int damage = 15, int weight = 5) : base(damage, weight) { }

}
