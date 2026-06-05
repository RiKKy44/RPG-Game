using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Network;

public class GameServer
{
    private const int Port = 5555;
    private TcpListener _listener;
    private GameState _serverState;
    private int _nextPlayerId = 1;
}
