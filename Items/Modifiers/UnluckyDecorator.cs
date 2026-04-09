using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Items.Modifiers;


public class UnluckyDecorator: ItemDecorator
{
    public UnluckyDecorator(Item innerItem) : base(innerItem)
    {
    }

    public override string GetName() => _innerItem.GetName() + " (Unlucky)";

    public override int GetLuckBonus() => _innerItem.GetLuckBonus() - 5;
}