using OODProject.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Items.Weapons.Categories;

public abstract class MagicalWeapon : Weapon
{
    public MagicalWeapon(int damage, int weight) : base(damage, weight) { }
    public override void Accept(IAttackMethod visitor) => visitor.Visit(this);
}