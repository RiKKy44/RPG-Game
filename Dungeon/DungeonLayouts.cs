using OODProject.Dungeon.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Dungeon;

public class DungeonLayouts : IBuildingBlock
{
    public void Apply(Board board)
    {
        new AddPathsProc().Apply(board);
        new AddCentralRoomProc().Apply(board);
        new AddChambersProc().Apply(board);

        new AddWeaponsProc().Apply(board);

        new AddItemsProc().Apply(board);
    }
}