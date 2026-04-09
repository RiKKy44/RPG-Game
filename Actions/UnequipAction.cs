using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Actions;



public class UnequipAction : IAction
{
    public void Execute(GameState state)
    {
        
        state.Player.Unequip(HandSlot.Left);
        state.Player.Unequip(HandSlot.Right);
    }

    public string Description => "Unequip hands";
}
