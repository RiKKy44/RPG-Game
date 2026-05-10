using OODProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OODProject.Logs;

namespace OODProject.Actions;

public class EquipRightHand : IAction
{

    public void Execute(GameState state)
    {
        if (state.InventoryPointer <= state.Player.Inventory.Count - 1)
        {
            Item item = state.Player.Inventory[state.InventoryPointer];
            if (item.IsEquipable())
            {
                state.Player.Equip(item, HandSlot.Right);
                GameLogger.Instance.Log($"Player equipped right hand with {item.GetName()}");
            }
        }
    }

    public string Description => "Equip left hand";
}

