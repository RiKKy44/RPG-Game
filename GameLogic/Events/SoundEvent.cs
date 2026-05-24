using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.GameLogic.Events;


public class SoundEvent
{
    public Position Source { get; set; }

    public int Range { get; set; }

    public SoundEvent(Position soure, int range)
    {
        Source = soure; 
        Range = range;
    }
}