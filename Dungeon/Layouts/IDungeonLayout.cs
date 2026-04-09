using OODProject.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Dungeon.Layouts;

public interface IDungeonLayout : IBuildingBlock
{
    IEnumerable<(ConsoleKey, IAction)> GetActions();
}
