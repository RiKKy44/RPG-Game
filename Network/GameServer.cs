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
        
        _actions = new Dictionary<ConsoleKey, IAction>();
        foreach (var (key, action) in layout.GetActions())
        {
            _actions[key] = action;
        }

        _serverState.ActionDescriptions = _actions
            .Select(kvp => $"[{kvp.Key}] - {kvp.Value.Description}")
            .Distinct()
            .ToList();

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

            BroadcastState();

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

                        BroadcastState();
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

            BroadcastState();
        }
    }

    private void ProcessClientCommand(ClientCommandDTO command)
    {
        lock (_stateLock)
        {
            _serverState.LocalPlayerId = command.PlayerId;

            _serverState.CurrentView = (ViewMode)command.ViewMode;
            _serverState.InventoryPointer = command.InventoryPointer;

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
        }
    }

    private void BroadcastState()
    {
        string jsonToSend;

        lock (_stateLock)
        {
            GameStateDTO stateDto = ToDto(_serverState);
            var networkMessage = new NetworkMessage
            {
                Type = MessageType.StateUpdate,
                Payload = JsonSerializer.Serialize(stateDto)
            };
            jsonToSend = JsonSerializer.Serialize(networkMessage) + "\n";
        }

        byte[] data = Encoding.UTF8.GetBytes(jsonToSend);

        var clients = _connectedClients.ToArray();
        foreach (var client in clients)
        {
            try
            {
                lock (client)
                {
                    var stream = client.GetStream();
                    stream.Write(data, 0, data.Length);
                }
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
                Symbol = p.GetSymbol(),
                Coins = p.CoinCount,
                Gold = p.GoldCount,
                LeftHandDisplay = p.LeftHand != null ? $"{p.LeftHand.GetSymbol()} {p.LeftHand.GetName()}" : "empty",
                RightHandDisplay = p.RightHand != null ? $"{p.RightHand.GetSymbol()} {p.RightHand.GetName()}" : "empty",
                InventoryItems = p.Inventory.Select(i => $"{i.GetSymbol()}|{i.GetName()}").ToList()
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

        dto.MapItems = new List<string>();
        for (int y = 0; y < GameConfig.Height; y++)
        {
            for (int x = 0; x < GameConfig.Width; x++)
            {
                var field = state.Board.GetField(new Position(x, y));
                foreach (var item in field.Items)
                {
                    dto.MapItems.Add($"{item.GetSymbol()}|{item.GetName()}|{x}|{y}");
                }
            }
        }
        dto.Message = state.Message;
        dto.ActionDescriptions = state.ActionDescriptions.ToList();
        return dto;
    }

}
