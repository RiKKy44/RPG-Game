using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Dungeon;

public interface IBuildingBlock {
    void Apply(Board board);
}


