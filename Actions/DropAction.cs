using OODProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }

    public string Description => "Drop item";
}
