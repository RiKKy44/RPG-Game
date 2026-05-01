using OODProject.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Items.Weapons.Categories;


public abstract class LightWeapon : Weapon
{
    public LightWeapon(int damage, int weight) : base(damage, weight) { }
    public override void Accept(IAttackMethod visitor, Item? decorator = null) => visitor.Visit(this, decorator ?? this);
}