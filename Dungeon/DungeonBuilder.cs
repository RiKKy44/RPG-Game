using OODProject.Dungeon.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Dungeon;


public class DungeonBuilder
{
    private Board _board;

    private bool _initialized = false;


    public DungeonBuilder StartEmpty()
    {
        _board = new Board();
        _initialized = true;
        return this;
    }

    public DungeonBuilder StartFilled()
    {
        _board = new Board();
        new FilledWithWallsProc().Apply(_board);
        _initialized = true;
        return this;
    }

    public DungeonBuilder Apply(IBuildingBlock buildingBlock)
    {
        if (!_initialized)
        {
            throw new InvalidOperationException("Must call StartEmpty or StartFilled");
        }

        buildingBlock.Apply(_board);
        return this;
    }

    public Board Build() => _board;
}
