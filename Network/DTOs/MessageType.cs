using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Network.DTOs;


public enum MessageType
{
    //from server to client:
    StateUpdate,
    AssignId,

    //from client to server:
    PlayerCommand
}