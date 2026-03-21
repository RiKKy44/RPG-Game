using System.Net.Mime;
using System.Runtime.Versioning;
using OODProject.Entities;
namespace OODProject;

public class Game
{
    private Board _board;
    private Player _player;
    private Renderer _renderer;
    private bool _running;

    public Game()
    {
        _board = new LevelLoader().Load();
        _player = new Player( position: new Position(1, 1));
        _renderer = new Renderer(_board, _player);
        _running = true;
    }

    private void HandleInput()
    {       
        ConsoleKey key = Console.ReadKey(true).Key;
        
        if (key == ConsoleKey.E)
        {
            Field currentField = _board.GetField(_player.CurrentPosition);
            if (currentField.Items.Count > 0)
            {
                Item item = currentField.Items[0];
                _player.PickUpItem(item);
                currentField.RemoveItem(item);
            }
        }
        else if (key == ConsoleKey.Escape)
        {
            _running = false;
            Console.Clear();
            Console.WriteLine("GOODBYE!");
        }
        else if (key == ConsoleKey.UpArrow)
        {
            _renderer.InventoryPointer--;
        }
        else if (key == ConsoleKey.DownArrow)
        {
            _renderer.InventoryPointer++;
        }
        
        else if (key == ConsoleKey.Q)
        {
            if (_player.Inventory.Count - 1 >= _renderer.InventoryPointer)
            {
                Item item = _player.DropItem(_renderer.InventoryPointer);
                _board.GetField(_player.CurrentPosition).AddItem(item);
            }
        }
        
        else if (key == ConsoleKey.F)
        {
            if (_renderer.InventoryPointer <= _player.Inventory.Count - 1)
            {
                Item item = _player.Inventory[_renderer.InventoryPointer];

                if (!item.IsEquipable())
                {
                    return;
                }

                _player.Equip(item, HandSlot.Right);

            }
        }
        else if (key == ConsoleKey.G)
        {
            if (_renderer.InventoryPointer <= _player.Inventory.Count - 1)
            {
                Item item = _player.Inventory[_renderer.InventoryPointer];
                if(!item.IsEquipable())
                {
                    return;
                }
                _player.Equip(item, HandSlot.Left);

            }
        }
        
        else if (key == ConsoleKey.H)
        {
            
            _player.Unequip(HandSlot.Left);
            _player.Unequip(HandSlot.Right);
        }
        

        Position direction = key switch
        {
            ConsoleKey.W => Direction.Up,
            ConsoleKey.S => Direction.Down,
            ConsoleKey.A => Direction.Left,
            ConsoleKey.D => Direction.Right,
            _ => new Position(0, 0)
        };
        Position newPosition = _player.CurrentPosition + direction;
        if (_board.CanEnter(newPosition))
        {
            _player.MoveTo(newPosition);
        }
    }

    public void Run()
    {
        Console.Clear();
        Console.CursorVisible = false;
        while (_running)
        {
            _renderer.Render();
            HandleInput();
        }
    }
}