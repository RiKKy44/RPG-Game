using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Items.Modifiers;

public class StrongDecorator : ItemDecorator
{
    public StrongDecorator(Item innerItem) : base(innerItem) { }

    public override string GetName() => _innerItem.GetName() + " (Strong)";

    public override int GetDamage() => _innerItem.GetDamage() + 5;
}
