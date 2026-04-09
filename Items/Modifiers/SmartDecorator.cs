using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Items.Modifiers;


public class SmartDecorator : ItemDecorator
{
    public SmartDecorator(Item innerItem) : base(innerItem) { }

    public override string GetName() => _innerItem.GetName() + " (Smart)";

    public override int GetWisdomBonus() => _innerItem.GetWisdomBonus() + 2;
    
}