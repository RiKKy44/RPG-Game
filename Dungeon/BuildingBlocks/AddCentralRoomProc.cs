using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Dungeon.BuildingBlocks;

public class AddCentralRoomProc : IBuildingBlock
{
    private readonly int _width;
    private readonly int _height;


    public AddCentralRoomProc(int width = 6, int height = 6)
    {
        _width = width;
        _height = height;
    }
    public void Apply(Board board)
    {
        int startX = (GameConfig.Width - _width) / 2;
        int startY = (GameConfig.Height - _height) / 2;


        for (int x = startX; x < startX + _width; x++)
        {
            for (int y = startY; y < startY + _height; y++)
            {
                board.SetField(new Position(x, y), new EmptyField());
            }
        }
    }
}
