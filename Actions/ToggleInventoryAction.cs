using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Actions;

public class ToggleInventoryAction : IAction
{
    public string Description => "Open/Close Inventory";

    public void Execute(GameState state)
    {
        if(state.CurrentView == ViewMode.Inventory)
        {
            state.CurrentView = ViewMode.Map;
        }
        else
        {
            state.CurrentView = ViewMode.Inventory;
        }
    }
}