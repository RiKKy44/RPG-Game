using OODProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OODProject.Dungeon.Themes;
namespace OODProject.Dungeon.BuildingBlocks;


public class AddEnemiesProc : IBuildingBlock
{
    private readonly IDungeonTheme _theme;
    private readonly int _count;
    private Random _random = new Random();


    public AddEnemiesProc(IDungeonTheme theme, int count = 10)
    {
        _count = count;
        _theme = theme;
    }

    public void Apply(Board board)
    {
        var availablePositions = new List<Position>();

        for (int x = 0; x < GameConfig.Width; x++)
        {
            for (int y = 0; y < GameConfig.Height; y++)
            {
                Position pos = new Position(x, y);
                Field field = board.GetField(pos);

                if (pos.X == 1 && pos.Y == 1) continue;

                if (field.CanEnter() && field.Items.Count == 0)
                {
                    availablePositions.Add(pos);
                }
            }
        }

        int placed = 0;
        while (placed < _count && availablePositions.Count > 0)
        {
            int index = _random.Next(availablePositions.Count);
            Position randomPos = availablePositions[index];

            Enemy enemy = _theme.CreateRandomEnemy(randomPos);

            board.Enemies.Add(enemy);

            availablePositions.RemoveAt(index);
            placed++;
        }
    }
}
