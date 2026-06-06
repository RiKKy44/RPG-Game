using OODProject.Dungeon;
using OODProject.Entities;
using OODProject.Logs;
using OODProject.Network.DTOs;
using OODProject.Network.Proxy;
using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace OODProject.Network;

public class GameClient
{
    private int _myPlayerId = -1;
    private GameState? _localState;
    private ConsoleView? _view;

    public async Task ConnectAsync(string ip, int port)
    {
        TcpClient client = new TcpClient();
        await client.ConnectAsync(ip, port);

        _localState = new GameState(new Board());
        _view = new ConsoleView(_localState); 

        var stream = client.GetStream();
        StreamReader reader = new StreamReader(stream);
        StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

        _ = Task.Run(() => ReceiveLoopAsync(reader));
        InputLoop(writer);
    }

    private async Task ReceiveLoopAsync(StreamReader reader)
    {
        while (true)
        {
            string? jsonLine = await reader.ReadLineAsync();
            if (string.IsNullOrEmpty(jsonLine)) break;

            var message = JsonSerializer.Deserialize<NetworkMessage>(jsonLine);
            if (message != null && message.Type == MessageType.AssignId)
            {
                _myPlayerId = int.Parse(message.Payload);
                _localState.LocalPlayerId = _myPlayerId;
            }
            else if (message != null && message.Type == MessageType.StateUpdate)
            {
                var dto = JsonSerializer.Deserialize<GameStateDTO>(message.Payload);

                SyncLocalState(dto);
                if(_view != null)
                {
                    _view.Render();
                }
            }
        }
    }

    private void SyncLocalState(GameStateDTO dto)
    {
        _localState.Message = dto.Message ?? "";
        _localState.ActionDescriptions = dto.ActionDescriptions ?? new List<string>();

        for (int y = 0; y < GameConfig.Height; y++)
        {
            for (int x = 0; x < GameConfig.Width; x++)
            {
                char symbol = dto.MapGrid[y][x];
                if (symbol == '#') _localState.Board.SetField(new Position(x, y), new Wall());
                else _localState.Board.SetField(new Position(x, y), new EmptyField());
            }
        }
        _localState.Players.Clear();
        foreach (var pDto in dto.Players)
        {
            var p = new Player(pDto.Name, position: new Position(pDto.X, pDto.Y));

            Item? netLeftHand = pDto.LeftHandDisplay != "empty"
                ? new NetworkItem(pDto.LeftHandDisplay.Substring(2), pDto.LeftHandDisplay[0])
                : null;

            Item? netRightHand = pDto.RightHandDisplay != "empty"
                ? new NetworkItem(pDto.RightHandDisplay.Substring(2), pDto.RightHandDisplay[0])
                : null;

            p.SyncFromNetwork(pDto.Id, pDto.Health, pDto.MaxHealth, pDto.Coins, pDto.Gold, netLeftHand!, netRightHand!);

            _localState.Players.Add(p);
        }

        _localState.Board.Enemies.Clear();
        foreach (var eDto in dto.Enemies)
        {
            _localState.Board.Enemies.Add(new Enemy(new Position(eDto.X, eDto.Y), eDto.Name, eDto.Symbol, 100, 0, 0, GameLogic.Events.Species.Goblin));
        }

    
        if (dto.Logs != null)
        {
            GameLogger.Instance.AllLogs.Clear();
            foreach (var log in dto.Logs) GameLogger.Instance.Log(log);
        }
    }

    private void InputLoop(StreamWriter writer)
    {
        while (true)
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.I)
            {
                _localState.CurrentView = _localState.CurrentView == ViewMode.Inventory ? ViewMode.Map : ViewMode.Inventory;
                _view.Render(); 
                continue;
            }
            if (key == ConsoleKey.H)
            {
                _localState.CurrentView = _localState.CurrentView == ViewMode.History ? ViewMode.Map : ViewMode.History;
                _view.Render();
                continue;
            }

            if (_myPlayerId != -1)
            {
                var cmd = new ClientCommandDTO { 
                    PlayerId = _myPlayerId, 
                    KeyCode = (int)key };
                var msg = new NetworkMessage { 
                    Type = MessageType.PlayerCommand, 
                    Payload = JsonSerializer.Serialize(cmd) };

                writer.WriteLine(JsonSerializer.Serialize(msg));
            }
        }
    }
}