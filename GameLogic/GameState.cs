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

    public Player? Player => Players.FirstOrDefault(p => p.Id == LocalPlayerId);
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


}