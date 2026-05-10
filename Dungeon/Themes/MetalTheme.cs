using OODProject.Dungeon.Layouts;
using OODProject.Items.Modifiers;
using OODProject.Items.UnusableItems;
using OODProject.Items.Weapons.WeaponTypes.SingleHanded;
using OODProject.Entities;
namespace OODProject.Dungeon.Themes;
public class MetalTheme : IDungeonTheme
{
    private Random _random = new Random();
    public string GetIntroMessage() => "The clang of metal echoes off the walls...";
    public IDungeonLayout GetLayoutStrategy() => new MetalLayout(this);

    public Item CreateRandomItem() => _random.Next(100) < 40 ? new Rock() : new HealingPlus(); 
    public Enemy CreateRandomEnemy(Position pos) => new Enemy(pos, "Cleaning Robot", 'r', 40, 12, 5);
    public Item CreateArtifact() => new StrongDecorator(new Sword(50, 15)); 
}