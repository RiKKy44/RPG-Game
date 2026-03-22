using OODProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Actions;

public class PickupAction : IAction
{
    public void Execute(GameState state)
    {
        var board = state.Board;
        var player = state.Player;
        Field field = board.GetField(player.CurrentPosition);

        if(field.Items.Count > 0)
        {
            Item item = field.Items[0];
            player.PickUpItem(item);
            field.RemoveItem(item);
        }
    }
    public string Description => "E - Pick up item";

}
