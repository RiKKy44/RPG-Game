using OODProject.Dungeon.BuildingBlocks;
using OODProject.Dungeon.Themes;

namespace OODProject.Dungeon.Layouts;

public class TreasuryLayout : BaseDungeonLayout
{
    private IDungeonTheme _theme;

    public TreasuryLayout(IDungeonTheme theme) { _theme = theme; }

    public override void Apply(Board board)
    {
        new AddCentralRoomProc(width: 14, height: 14).Apply(board);
        
        new AddPathsProc().Apply(board);
        new AddPathsProc().Apply(board);

        new AddWeaponsProc(_theme, count: 10).Apply(board);
        new AddItemsProc(_theme, count: 5).Apply(board); 
        new AddEnemiesProc(_theme, count: 12).Apply(board);
    }
}