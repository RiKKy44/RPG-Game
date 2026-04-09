using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Items.Weapons.Modifiers;


public class IWeaponModifier
{
    string NamePrefix { get; }
    int DamageBonus { get; }
    int WeightBonus { get; }
}