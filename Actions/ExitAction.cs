using OODProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Actions;

public class ExitAction : IAction
{
    public void Execute(GameState state)
    {
        state.IsRunning = false;
    }

    public string Description => " Exit game";
}