using OODProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Actions;


public interface IAction
{
    public void Execute(GameState gameState);
    string Description { get; }
}