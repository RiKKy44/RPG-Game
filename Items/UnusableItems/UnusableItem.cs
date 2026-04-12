using OODProject.Combat;
using OODProject.Entities;
namespace OODProject;

public abstract class UnusableItem : Item
{
    public override bool IsEquipable()
    {
        return false;
    }
    public override void Accept(IAttackMethod visitor) => visitor.Visit(this);
}