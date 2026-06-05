using OODProject.Actions;
using OODProject.Dungeon;
using OODProject.Dungeon.Layouts;
using OODProject.Dungeon.Themes;
using OODProject.Entities;
using OODProject.Logs;
using OODProject.Network.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace OODProject.Network;

public class GameServer
{
    private const int Port = 5555;

    private TcpListener _listener;

    private GameState _serverState;

    private int _nextPlayerId = 1;

    private List<TcpClient> _connectedClients = new List<TcpClient>();

    private readonly object _stateLock = new object();

    private IDictionary<ConsoleKey, IAction> _actions;
    public GameServer(ConfigurationData config)
    {
        IDungeonTheme[] themes = { new LibraryTheme(), new MetalTheme(), new TreasuryTheme() };
        IDungeonTheme selectedTheme = themes[new Random().Next(themes.Length)];
        IDungeonLayout layout = selectedTheme.GetLayoutStrategy();

        Board board = new DungeonBuilder().StartFilled().Apply(layout).Build();

        _serverState = new GameState(board);

        GameLogger.Instance.Log($"[SERVER] Created map with theme: {selectedTheme.GetType().Name}");
    }

    public void Start()
    {
        _listener = new TcpListener(IPAddress.Any, Port);
        _listener.Start();
        Console.WriteLine($"[SERVER] Listening for incoming players on port {Port}...");

        while (true)
        {
            TcpClient client = _listener.AcceptTcpClient();

            if (_connectedClients.Count >= 9)
            {
                Console.WriteLine("[SERVER] Server full. Rejecting connection");
                client.Close();
                continue;
            }

            _connectedClients.Add(client);
            int playerId = _nextPlayerId++;
            Console.WriteLine($"[SERVER] Player {playerId} connected, IP: {client.Client.RemoteEndPoint}");

            Task.Run(() => HandleClientAsync(client, playerId));
        }
    }
    private async Task HandleClientAsync(TcpClient client, int playerId)
    {
        NetworkStream stream = client.GetStream();
        using StreamReader reader = new StreamReader(stream);
        using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

        try
        {

            var idMsg = new NetworkMessage
            {
                Type = MessageType.AssignId,
                Payload = playerId.ToString()
            };

            await writer.WriteLineAsync(JsonSerializer.Serialize(idMsg));

            lock (_stateLock)
            {
                Player newPlayer = new Player($"Player {playerId}", position: new Position(1, 1));
                newPlayer.Id = playerId;
                _serverState.Players.Add(newPlayer);
            }

            Console.WriteLine($"[SERVER] Player {playerId} joined the game");

            await BroadcastStateAsync();

            while (client.Connected)
            {
                string? jsonLine = await reader.ReadLineAsync();

                if (string.IsNullOrEmpty(jsonLine))
                    break;

                var message = JsonSerializer.Deserialize<NetworkMessage>(jsonLine);

                if (message != null && message.Type == MessageType.PlayerCommand)
                {
                    var command = JsonSerializer.Deserialize<ClientCommandDTO>(message.Payload);
                    if (command != null)
                    {
                        ProcessClientCommand(command);

                        await BroadcastStateAsync();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SERVER] Connection error with Player {playerId}: {ex.Message}");
        }
        finally
        {            
            Console.WriteLine($"[SERVER] Player {playerId} disconnected. Removing from map.");
            lock (_stateLock)
            {
                _serverState.Players.RemoveAll(p => p.Id == playerId);
                _connectedClients.Remove(client);
            }
            client.Close();

            await BroadcastStateAsync();
        }
    }

    private void ProcessClientCommand(ClientCommandDTO command)
    {
        lock (_stateLock)
        {

            _serverState.LocalPlayerId = command.PlayerId;

            ConsoleKey key = (ConsoleKey)command.KeyCode;

            if (_actions.TryGetValue(key, out var action))
            {
                action.Execute(_serverState);

                if (_serverState.CurrentView == ViewMode.Map)
                {
                    foreach (var enemy in _serverState.Board.Enemies.ToList())
                    {
                        enemy.MoveRandomly(_serverState.Board, _serverState.Player.CurrentPosition);
                    }
                }
            }
            else
            {
                Console.WriteLine($"[SERVER] Invalid key command {key} from Player {command.PlayerId}");
            }
        }
    }

    private async Task BroadcastStateAsync()
    {
        string jsonToSend = "";

        lock (_stateLock)
        {
            GameStateDTO stateDto = ToDto(_serverState);

            var networkMessage = new NetworkMessage
            {
                Type = MessageType.StateUpdate,
                Payload = JsonSerializer.Serialize(stateDto)
            };

            jsonToSend = JsonSerializer.Serialize(networkMessage);
        }

        var clients = _connectedClients.ToArray();
        foreach (var client in clients)
        {
            try
            {
                var writer = new StreamWriter(client.GetStream()) { 
                    AutoFlush = true 
                };
                await writer.WriteLineAsync(jsonToSend); 
            }
            catch
            {
            }
        }
    }


    public GameStateDTO ToDto(GameState state)
    {
        var dto = new GameStateDTO();

        dto.MapGrid = new string[GameConfig.Height];

        for (int y = 0; y < GameConfig.Height; y++)
        {
            char[] row = new char[GameConfig.Width];
            for (int x = 0; x < GameConfig.Width; x++)
            {
                var field = state.Board.GetField(new Position(x, y));

                row[x] = field.Items.Count > 0 ? field.Items[0].GetSymbol() : field.GetSymbol();
            }
            dto.MapGrid[y] = new string(row);
        }

        foreach (var p in state.Players)
        {
            dto.Players.Add(new PlayerDTO
            {
                Id = p.Id,
                Name = p.Name,
                X = p.CurrentPosition.X,
                Y = p.CurrentPosition.Y,
                Health = p.Health,
                MaxHealth = p.MaxHealth,
                Symbol = p.GetSymbol()
            });
        }

        foreach (var e in state.Board.Enemies)
        {
            dto.Enemies.Add(new EnemyDTO
            {
                Name = e.GetName(),
                X = e.CurrentPosition.X,
                Y = e.CurrentPosition.Y,
                Symbol = e.GetSymbol()
            });
        }
        return dto;
    }

}
