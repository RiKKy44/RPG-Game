using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Actions;



public class InventoryScrollAction : IAction
{
    private int _change;

    public string Description => "↑↓ - Navigate inventory";

    public InventoryScrollAction(int change)
    {
        _change = change;
    }

    public void Execute(GameState state)
    {
        state.InventoryPointer += _change;
    }
}
