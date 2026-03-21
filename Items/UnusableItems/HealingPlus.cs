using OODProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Items.UnusableItems;
public class HealingPlus : UnusableItem
{
    public override string GetName()
    {
        return "Healing Plus";
    }
    public override char GetSymbol()
    {
        return Symbols.Heal;
    }

    public override void OnPickUp(Player player)
    {
        player.Heal(5);
    }
}