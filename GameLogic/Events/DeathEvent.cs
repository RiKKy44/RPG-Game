using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.GameLogic.Events;


public class DeathEvent
{
    public Species DeadSpecies { get; set; }

    public DeathEvent(Species deadSpecies)
    {
        DeadSpecies = deadSpecies;
    }
}
