using OODProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Dungeon.BuildingBlocks;


public class AddEnemiesProc : IBuildingBlock
{
    private readonly int _count;
    private Random _random = new Random();


    public AddEnemiesProc(int count = 10)
    {
        _count = count;
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

            int roll = _random.Next(100);
            Enemy enemy;

            if (roll < 50) enemy = new Enemy(randomPos, "Goblin", 'g', 30, 10, 2);
            else enemy = new Enemy(randomPos, "Orc", 'o', 50, 15, 4);

            board.Enemies.Add(enemy);

            availablePositions.RemoveAt(index);
            placed++;
        }
    }
}
