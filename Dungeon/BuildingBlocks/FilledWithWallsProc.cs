using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Dungeon.BuildingBlocks;

public class FilledWithWallsProc : IBuildingBlock
{
    public void Apply(Board board)
    {
        for(int x = 0; x < GameConfig.Width; x++)
        {
            for(int y = 0; y < GameConfig.Height; y++)
            {
                board.SetField(new Position(x, y), new Wall());
            }
        }
    }
}
