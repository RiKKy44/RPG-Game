using OODProject.Actions;
using OODProject.Dungeon;
using OODProject.Dungeon.Layouts;
using OODProject.Dungeon.Themes;
using OODProject.Entities;
using OODProject.Logs;
using OODProject.Network.DTOs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OODProject.Network;

public class GameServer
{
    private const int Port = 5555;
    private TcpListener _listener;
    private GameState _serverState;
    private int _nextPlayerId = 1;
    private readonly object _stateLock = new object();
    private IDictionary<ConsoleKey, IAction> _actions;


    private Dictionary<int, TcpClient> _clients = new Dictionary<int, TcpClient>();
    private Dictionary<int, PlayerUIContext> _playerContexts = new Dictionary<int, PlayerUIContext>();
    private ConcurrentQueue<ClientCommandDTO> _commandQueue = new ConcurrentQueue<ClientCommandDTO>();


    private class PlayerUIContext
    {
        public ViewMode CurrentView { get; set; } = ViewMode.Map;
        public string Message { get; set; } = "";
        public Enemy? CurrentEnemy { get; set; } = null;
    }

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

        Task.Run(() => ServerUpdateLoop());

        while (true)
        {
            TcpClient client = _listener.AcceptTcpClient();

            if (_clients.Count >= 9)
            {
                Console.WriteLine("[SERVER] Server full. Rejecting connection");
                client.Close();
                continue;
            }

            int playerId = _nextPlayerId++;
            _clients[playerId] = client;
            _playerContexts[playerId] = new PlayerUIContext(); 

            Console.WriteLine($"[SERVER] Player {playerId} connected, IP: {client.Client.RemoteEndPoint}");

            Task.Run(() => HandleClientAsync(client, playerId));
        }
    }

    private async Task ServerUpdateLoop()
    {
        while (true)
        {
            bool stateChanged = false;

            while (_commandQueue.TryDequeue(out var command))
            {
                ProcessClientCommand(command);
                stateChanged = true;
            }

            if (stateChanged)
            {
                BroadcastState();
            }

            await Task.Delay(16);
        }
    }

    private async Task HandleClientAsync(TcpClient client, int playerId)
    {
        NetworkStream stream = client.GetStream();
        using StreamReader reader = new StreamReader(stream);
        using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

        try
        {
            var idMsg = new NetworkMessage { Type = MessageType.AssignId, Payload = playerId.ToString() };
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
                if (string.IsNullOrEmpty(jsonLine)) break;

                var message = JsonSerializer.Deserialize<NetworkMessage>(jsonLine);

                if (message != null && message.Type == MessageType.PlayerCommand)
                {
                    var command = JsonSerializer.Deserialize<ClientCommandDTO>(message.Payload);
                    if (command != null)
                    {
                        _commandQueue.Enqueue(command);
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
                _clients.Remove(playerId);
                _playerContexts.Remove(playerId); 
            }
            client.Close();
            BroadcastState();
        }
    }

    private void ProcessClientCommand(ClientCommandDTO command)
    {
        lock (_stateLock)
        {
            if (!_playerContexts.TryGetValue(command.PlayerId, out var ctx)) return;

            _serverState.LocalPlayerId = command.PlayerId;
            _serverState.CurrentView = (ViewMode)command.ViewMode;
            _serverState.InventoryPointer = command.InventoryPointer;
            _serverState.Message = ctx.Message;
            _serverState.CurrentEnemy = ctx.CurrentEnemy;

            ConsoleKey key = (ConsoleKey)command.KeyCode;
            bool actionExecuted = false;

            if (_actions.TryGetValue(key, out var action))
            {
                action.Execute(_serverState);
                actionExecuted = true;

                if(_serverState.Player != null && _serverState.Player.Health <= 0)
                {
                    _serverState.CurrentView = ViewMode.GameOver;


                    _serverState.Player.MoveTo(new Position(-1, -1));
                }
            }

            if (actionExecuted && _serverState.CurrentView == ViewMode.Map &&
                (key == ConsoleKey.W || key == ConsoleKey.A || key == ConsoleKey.S || key == ConsoleKey.D))
            {
                foreach (var enemy in _serverState.Board.Enemies.ToList())
                {
                    enemy.MoveRandomly(_serverState.Board, _serverState.Player.CurrentPosition);
                }
            }

            ctx.CurrentView = _serverState.CurrentView;
            ctx.Message = _serverState.Message;
            ctx.CurrentEnemy = _serverState.CurrentEnemy;

            foreach (var otherCtx in _playerContexts.Values)
            {
                if (otherCtx.CurrentEnemy != null && !_serverState.Board.Enemies.Contains(otherCtx.CurrentEnemy))
                {
                    otherCtx.CurrentEnemy = null;
                    otherCtx.CurrentView = ViewMode.Map;
                    otherCtx.Message = "Przeciwnik zginął z rąk innego gracza!";
                }
            }
        }
    }

    private void BroadcastState()
    {
        lock (_stateLock)
        {
            foreach (var kvp in _clients)
            {
                int playerId = kvp.Key;
                TcpClient client = kvp.Value;

                GameStateDTO stateDto = ToDto(_serverState, playerId);

                var networkMessage = new NetworkMessage
                {
                    Type = MessageType.StateUpdate,
                    Payload = JsonSerializer.Serialize(stateDto)
                };

                string jsonToSend = JsonSerializer.Serialize(networkMessage) + "\n";
                byte[] data = Encoding.UTF8.GetBytes(jsonToSend);

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
    }

    public GameStateDTO ToDto(GameState state, int targetPlayerId)
    {
        var dto = new GameStateDTO();

        dto.MapGrid = new string[GameConfig.Height];
        for (int y = 0; y < GameConfig.Height; y++)
        {
            char[] row = new char[GameConfig.Width];
            for (int x = 0; x < GameConfig.Width; x++)
            {
                var field = state.Board.GetField(new Position(x, y));
                row[x] = field.GetSymbol();
            }
            dto.MapGrid[y] = new string(row);
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
                Coins = p.CoinCount,
                Gold = p.GoldCount,
                LeftHandDisplay = p.LeftHand != null ? $"{p.LeftHand.GetSymbol()} {p.LeftHand.GetName()}" : "empty",
                RightHandDisplay = p.RightHand != null ? $"{p.RightHand.GetSymbol()} {p.RightHand.GetName()}" : "empty",
                InventoryItems = p.Inventory.Select(i => $"{i.GetSymbol()}|{i.GetName()}").ToList()
            });
        }

        foreach (var e in state.Board.Enemies)
        {
            dto.Enemies.Add(new EnemyDTO { Name = e.GetName(), X = e.CurrentPosition.X, Y = e.CurrentPosition.Y, Symbol = e.GetSymbol() });
        }

        dto.Logs = GameLogger.Instance.AllLogs.TakeLast(5).ToList();
        dto.ActionDescriptions = state.ActionDescriptions.ToList();

        if (_playerContexts.TryGetValue(targetPlayerId, out var ctx))
        {
            dto.Message = ctx.Message;
            dto.CurrentView = (int)ctx.CurrentView;

            if (ctx.CurrentEnemy != null)
            {
                dto.CurrentEnemy = new EnemyDTO
                {
                    Name = ctx.CurrentEnemy.GetName(),
                    X = ctx.CurrentEnemy.CurrentPosition.X,
                    Y = ctx.CurrentEnemy.CurrentPosition.Y,
                    Symbol = ctx.CurrentEnemy.GetSymbol(),
                    Health = ctx.CurrentEnemy.Health,
                    MaxHealth = ctx.CurrentEnemy.MaxHealth,
                    AttackValue = ctx.CurrentEnemy.AttackValue,
                    Armor = ctx.CurrentEnemy.Armor
                };
            }
        }

        return dto;
    }
}