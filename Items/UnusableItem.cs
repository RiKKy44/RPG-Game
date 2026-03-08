using OODProject.Entities;
namespace OODProject;

public abstract class UnusableItem : Item
{
    public override void OnPickUp(Player player)
    {
        player.AddToInventory(this);
    }
}