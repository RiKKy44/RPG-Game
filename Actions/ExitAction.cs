using OODProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Actions;

public class ExitAction : IAction
{

    public event Action? OnExit;
    public void Execute(GameState state)
    {
        OnExit?.Invoke();
    }

    public string Description => "Escape - Exit game";
}