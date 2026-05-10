using OODProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OODProject.Logs;

namespace OODProject.Actions;

public class DropAction : IAction
{
    public void Execute(GameState state)
    {
        var player = state.Player;
        var board = state.Board;
        if (player.Inventory.Count - 1 >= state.InventoryPointer)
        {
            Item item = player.DropItem(state.InventoryPointer);
            board.GetField(player.CurrentPosition).AddItem(item);
            GameLogger.Instance.Log($"Player dropped {item.GetName} from {board.GetField(player.CurrentPosition)}");
        }
    }

    public string Description => "Drop item";
}
