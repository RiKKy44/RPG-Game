using OODProject.Dungeon.Layouts;
using OODProject.Entities;
using OODProject.Network.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject;


public enum AttackStyle { 
    Normal, 
    Stealth, 
    Magical 
}
public enum ViewMode
{
    Map,
    Inventory,
    Combat,
    History
}

public class GameState
{
    public List<Player> Players { get; } = new List<Player>();

    public int LocalPlayerId { get; set; }

    public Player Player => Players.FirstOrDefault(p => p.Id == LocalPlayerId);
    public Board Board { get;  }

    public IEnumerable<string> ActionDescriptions;
    public bool IsRunning { get; set; } = true;
    public AttackStyle CurrentAttack { get; set; } = AttackStyle.Normal;
    public ViewMode CurrentView { get; set; } = ViewMode.Map;
    public string Message { get; set; } = "";

    public Enemy? CurrentEnemy { get; set; }

    private int _inventoryPointer;
    public int InventoryPointer
    {
        get { return _inventoryPointer; }
        set
        {
            if (Player == null)
            {
                _inventoryPointer = 0;
                return;
            }
            if (value < 0)
                _inventoryPointer = 0;
            else if (value > Player.InventorySize - 1)
                _inventoryPointer = Player.InventorySize - 1;
            else
                _inventoryPointer = value;
        }
    }

    public GameState(Board board)
    {
        Board = board;
        _inventoryPointer = 0;
        CurrentEnemy = null;
    }


    public GameStateDTO ToDto(GameState state)
    {
        var dto = new GameStateDTO();

        dto.MapGrid = new string[GameConfig.Height];

        for ( int y = 0; y< GameConfig.Height; y++)
        {
            char[] row = new char[GameConfig.Width];
            for(int x =0; x<GameConfig.Width; x++)
            {
                var field = state.Board.GetField(new Position(x, y));

                row[x] = field.Items.Count > 0 ? field.Items[0].GetSymbol() : field.GetSymbol();
            }
            dto.MapGrid[y] = new string(row);
        }
        
        foreach(var p in state.Players)
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

        foreach(var e in state.Board.Enemies)
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