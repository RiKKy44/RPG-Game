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

    public Board DungeonBoard { get; }

    public SoundEvent(Position soure, int range, Board board)
    {
        Source = soure; 
        Range = range;
        DungeonBoard = board;
    }
}