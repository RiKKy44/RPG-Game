using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Items.Weapons.WeaponTypes.SingleHanded;

public class Sword : SingleHanded
{
    public override string GetName()
    {
        return "Sword";
    }
    public override char GetSymbol()
    {
        return base.GetSymbol();
    }

    public Sword(int damage = 30, int weight = 10) : base(damage, weight) { }

    
}
