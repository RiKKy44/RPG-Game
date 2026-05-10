using OODProject.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OODProject.Logs;

namespace OODProject.Actions;

public class MoveAction : IAction
{
    private Position _direction;
    public MoveAction(Position direction)
    {
        _direction = direction;

        Description = direction switch
        {
            _ when direction == Direction.Up => "Move up",
            _ when direction == Direction.Down => "Move down",
            _ when direction == Direction.Left => "Move left",
            _ when direction == Direction.Right => "Move right",
        };
    }

    public string Description { get; init; }

    public void Execute(GameState state)
    {
        Position newPosition = state.Player.CurrentPosition + _direction;

        var enemy = state.Board.Enemies.FirstOrDefault(e => e.CurrentPosition == newPosition);

        if (enemy != null) {
            state.CurrentEnemy = enemy;
            state.CurrentView = ViewMode.Combat;
            state.Message = $"You entered the fight with {enemy.GetName()}!";

        }
        else if (state.Board.CanEnter(newPosition))
        {
            state.Player.MoveTo(newPosition);
            state.Message = "";
        } 
        else if (!state.Board.CanEnter(newPosition))
        {
            GameLogger.Instance.Log($"Player tried to enter the wall");
        }
    }
}