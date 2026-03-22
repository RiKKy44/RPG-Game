using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Actions;

public class MoveAction : IAction
{
    private Position _direction;
    public MoveAction(Position direction)
    {
        _direction = direction;
    }

    public string Description => "Press WSAD to move your character";

    public void Execute(GameState state)
    {
        Position newPosition = state.Player.CurrentPosition + _direction;
        if (state.Board.CanEnter(newPosition))
        {
            state.Player.MoveTo(newPosition);
        }
    }
}