using OODProject.Entities;
namespace OODProject;

public abstract class UnusableItem : Item
{
    public override bool IsEquipable()
    {
        return false;
    }
}