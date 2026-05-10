using OODProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OODProject.Logs;

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
            GameLogger.Instance.Log($"Player picked up: {item.GetName()}");
        }
    }
    public string Description => "Pick up item";

}
