using OODProject.Dungeon.Layouts;
using OODProject.Entities;
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
    Inventory
}

public class GameState
{
    public Player Player { get; }
    public Board Board { get;  }

    public IEnumerable<string> ActionDescriptions;

    public List<Enemy> Enemies { get; } = new List<Enemy>();
    public bool IsRunning { get; set; } = true;
    public AttackStyle CurrentAttack { get; set; } = AttackStyle.Normal;
    public ViewMode CurrentView { get; set; } = ViewMode.Map;
    public string Message { get; set; } = "";

    private int _inventoryPointer;
    public int InventoryPointer
    {
        get { return _inventoryPointer; }
        set
        {
            if (value < 0)
                _inventoryPointer = 0;
            else if (value > Player.InventorySize - 1)
                _inventoryPointer = Player.InventorySize - 1;
            else
                _inventoryPointer = value;
        }
    }

    public GameState(Player player, Board board)
    {
        Player = player; 
        Board = board; 
        InventoryPointer = 0;
    }
}