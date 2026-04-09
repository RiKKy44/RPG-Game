using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Dungeon.BuildingBlocks;


public class AddPathsProc : IBuildingBlock
{
    private Random _random = new Random();
    private void Clear(Board board, Position pos)
    {
        board.SetField(pos, new EmptyField());
    }

    private void ClearWallBetween(Board board, Position current, Position next)
    {
        Position between = new Position(

            (current.X + next.X) / 2,
            (current.Y + next.Y) / 2);
        Clear(board, between);
    }
    private bool IsInBounds(Position pos)
    {
        return pos.X > 0 && pos.X<GameConfig.Width - 1 && pos.Y > 0 && pos.Y < GameConfig.Height - 1;
    }


    private List<Position> GetUnvisitedNeighbors(Position pos, HashSet<Position> visited)
    {
        var neighbors = new List<Position>();

        Position[] directions  = {Direction.Up, Direction.Down, Direction.Left, Direction.Right };

        foreach (var direction in directions) {

            Position neighbor = pos + 2 * direction;

            if (IsInBounds(neighbor)&& !visited.Contains(neighbor))
            {
                neighbors.Add(neighbor);
            }
        }
        return neighbors;

    }
    public void Apply(Board board)
    {

        var visited = new HashSet<Position>();
        var stack = new Stack<Position>();
        Position start = new Position(1, 1);

        Clear(board, start);

        visited.Add(start);

        stack.Push(start);

        while (stack.Count > 0)
        {
            var current = stack.Peek();
            
            var neighbors = GetUnvisitedNeighbors(current, visited);

            if (neighbors.Count >0) {
                Position next = neighbors[_random.Next(neighbors.Count)];
                ClearWallBetween(board, current, next);

                Clear(board, next);

                visited.Add(next);

                stack.Push(next);
            }
            else
            {
                stack.Pop();
            }
        }
    }
}
