using OODProject.Combat;
using OODProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Items.UnusableItems;

public class Rock : UnusableItem
{
    public override string GetName()
    {
        return "Rock";
    }

    public override char GetSymbol()
    {
        return Symbols.Rock;
    }
    public override void Accept(IAttackMethod visitor, Item? decorator = null) => visitor.Visit(this, decorator ?? this);
}