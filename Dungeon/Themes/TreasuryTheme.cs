using OODProject.Dungeon.Layouts;
using OODProject.Items.Modifiers;
using OODProject.Items.UnusableItems;
using OODProject.Items.Weapons.WeaponTypes.DoubleHanded;
using OODProject.Entities;
using OODProject.Items.Weapons.WeaponTypes.SingleHanded;
namespace OODProject.Dungeon.Themes;
public class TreasuryTheme : IDungeonTheme
{
    private Random _random = new Random();
    public string GetIntroMessage() => "You feel an itch in your wallet...";
    public IDungeonLayout GetLayoutStrategy() => new TreasuryLayout(this); 

    public Item CreateRandomItem() => _random.Next(100) < 50 ? new Gold() : new Coin();
    public Enemy CreateRandomEnemy(Position pos) => new Enemy(pos, "Aggressive Safe", 'S', 100, 20, 15);
    public Item CreateArtifact() => new GreatAxe(70, 50); 
    
    public Item CreateRandomWeapon()
    {
        Item baseWeapon = _random.Next(100) < 50 
            ? new Sword() 
            : new GreatSword();

        if (_random.Next(100) < 50) return new UnluckyDecorator(baseWeapon);
        return baseWeapon;
    }
}