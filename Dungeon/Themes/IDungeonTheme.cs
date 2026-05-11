using OODProject.Dungeon.Layouts;
using OODProject.Entities;
namespace OODProject.Dungeon.Themes;

public interface IDungeonTheme{
    string GetIntroMessage();
    IDungeonLayout GetLayoutStrategy();
    
    Item CreateRandomItem();
    
    Enemy CreateRandomEnemy(Position pos);

    Item CreateArtifact();
    
    Item CreateRandomWeapon();
}