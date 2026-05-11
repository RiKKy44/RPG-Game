using OODProject.Dungeon.Layouts;
using OODProject.Items.Modifiers;
using OODProject.Items.UnusableItems;
using OODProject.Items.Weapons.WeaponTypes.SingleHanded;
using OODProject.Items.Weapons.WeaponTypes;
using OODProject.Entities;
namespace OODProject.Dungeon.Themes;

public class LibraryTheme : IDungeonTheme
{
    private Random _random = new Random();
    public IDungeonLayout GetLayoutStrategy() => new MetalLayout(this);
    
    public string GetIntroMessage() => "The smell of old books fills the air...";
    public Item CreateRandomItem()
    {
        int roll = _random.Next(100);
        if (roll < 50)
        {
            return new SmartDecorator(new Rock());
        }

        return new HealingPlus();
    }

    public Enemy CreateRandomEnemy(Position pos)
    {
        int roll = _random.Next(100);
        if (roll < 70) return new Enemy(pos, "Apprentice", 'a', health: 30, attackValue: 15, armor: 0);
        return new Enemy(pos, "Mage", 'M', health: 50, attackValue: 25, armor: 2);
    }
    public Item CreateArtifact()
    {
        return new StrongDecorator(new BlackWand());
    }
    
    public Item CreateRandomWeapon()
    {
        Item baseWeapon = _random.Next(100) < 60 
            ? new Dagger() 
            : new Sword();

        if (_random.Next(100) < 50) return new SmartDecorator(baseWeapon);
        return baseWeapon;
    }
}