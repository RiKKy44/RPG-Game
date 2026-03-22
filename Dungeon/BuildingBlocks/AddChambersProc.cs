using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Dungeon.BuildingBlocks;

public class AddChambersProc : IBuildingBlock
{
    private readonly int _count;

    private readonly int _minSize;

    private readonly int _maxSize;

    private Random _random = new Random();

    public AddChambersProc(int count = 10, int minSize = 8, int maxSize = 10)
    {
        _count = count;
        _minSize = minSize;
        _maxSize = maxSize;
    }


    public void Apply(Board board)
    {
        for (int i = 0; i < _count; i++)
        {
            int width = _random.Next(_minSize, _maxSize + 1);

            int height = _random.Next(_minSize, _maxSize + 1);


            //randomly choosing left upper corner
            int x = _random.Next(1, GameConfig.Width - width - 1);

            int y = _random.Next(1, GameConfig.Height - height - 1);


            for (int dx = 0; dx < width; dx++)
            {
                for (int dy = 0; dy < height; dy++)
                {
                    board.SetField(new Position(x + dx, y + dy), new EmptyField());

                }
            }
        }
    }
}

