using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Actions;


public class ToggleAttackAction : IAction
{
    public string Description => "Change Attack Style";


    public void Execute(GameState state)
    {
        state.CurrentAttack = (AttackStyle)(((int)state.CurrentAttack + 1) % 3);
    }
}