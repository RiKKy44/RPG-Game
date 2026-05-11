using OODProject.Dungeon.BuildingBlocks;
using OODProject.Dungeon.Themes;

namespace OODProject.Dungeon.Layouts;

public class MetalLayout : BaseDungeonLayout
{
    private IDungeonTheme _theme;

    public MetalLayout(IDungeonTheme theme) { _theme = theme; }

    public override void Apply(Board board)
    {
        new AddChambersProc(count: 20, minSize: 4, maxSize: 6).Apply(board);
        new AddPathsProc().Apply(board); 
        new AddWeaponsProc(_theme, count: 10).Apply(board);
        new AddItemsProc(_theme, count: 5).Apply(board);
        new AddEnemiesProc(_theme, count: 15).Apply(board);
    }
}