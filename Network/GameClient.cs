using System;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;
using OODProject.Network.DTOs;

namespace OODProject.Network;



public class GameClient
{
    private int _myPlayerId = -1;
    private GameStateDTO _latestState;

    public async Task ConnectAsync(string ip, int port)
    {
        TcpClient client = new TcpClient();

        Console.WriteLine($"[CLIENT] Connecting to {ip}:{port}");

        await client.ConnectAsync(ip, port);

        var stream = client.GetStream();

        StreamReader reader = new StreamReader(stream);

        StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

        _ = Task.Run(() => ReceiveLoopAsync(reader));

        InputLoop(writer);
    }


    private async Task ReceiveLoopAsync(StreamReader reader)
    {
        try
        {
            while (true)
            {
                string? jsonLine = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(jsonLine))
                    break;
                var message = JsonSerializer.Deserialize<NetworkMessage>(jsonLine);

                if(message != null && message.Type == MessageType.AssignId)
                {
                    _myPlayerId = int.Parse(message.Payload);
                }
                else if (message != null && message.Type == MessageType.StateUpdate)
                {
                    _latestState = JsonSerializer.Deserialize<GameStateDTO>(message.Payload);

                    RenderNetworkView(_latestState);
                }

            }
        }
        catch(Exception ex)
        {
            Console.Clear();
            Console.WriteLine($"[CLIENT] Connection lost: {ex.Message}");
        }
    }

    private void InputLoop(StreamWriter writer)
    {
        while (true)
        {
            var key = Console.ReadKey(true).Key;


            if(_myPlayerId == -1)
            {
                var cmd = new ClientCommandDTO
                {
                    PlayerId = _myPlayerId,
                    KeyCode = (int)key,

                };

                var msg = new NetworkMessage
                {
                    Type = MessageType.PlayerCommand,
                    Payload = JsonSerializer.Serialize(cmd)
                };

                writer.WriteLine(JsonSerializer.Serialize(msg));
            }
        }
    }



    private void RenderNetworkView(GameStateDTO state)
    {
        Console.SetCursorPosition(0, 0);

        for (int y = 0; y < state.MapGrid.Length; y++)
        {
            Console.WriteLine(state.MapGrid[y].PadRight(Console.WindowWidth - 1));
        }

        foreach (var player in state.Players)
        {
            Console.SetCursorPosition(player.X, player.Y);
            if (player.Id == _myPlayerId)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write('¶'); 
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(player.Id.ToString()); 
            }
        }
        Console.ResetColor();

        Console.SetCursorPosition(0, state.MapGrid.Length + 2);
        var myPlayer = state.Players.FirstOrDefault(p => p.Id == _myPlayerId);

        if (myPlayer != null)
        {
            Console.WriteLine($"=== MULTIPLAYER MODE - You are Player {_myPlayerId}".PadRight(50));
            Console.WriteLine($"HP: {myPlayer.Health} / {myPlayer.MaxHealth}".PadRight(50));

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Press W/A/S/D to move, ESC to quit".PadRight(50));
        }
    }

}