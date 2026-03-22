using System.Net.Mime;
using System.Runtime.Versioning;
using OODProject.Actions;
using OODProject.Entities;

namespace OODProject;

public class Game
{
    private Renderer _renderer;
    private bool _running;
    private GameState _state;

    private IDictionary<ConsoleKey, IAction> actions;
    public Game()
    {
        Board board = new LevelLoader().Load();
        Player player = new Player( position: new Position(1, 1)); 
        _state = new GameState(player, board);
        _renderer = new Renderer(_state);
        _running = true;
        actions = new Dictionary<ConsoleKey, IAction>
        {
            [ConsoleKey.E] = new PickupAction(),
            [ConsoleKey.Escape] = new ExitAction(),
            [ConsoleKey.Q] = new DropAction(),
            [ConsoleKey.H] = new UnequipAction(),
            [ConsoleKey.F] = new EquipAction(HandSlot.Left),
            [ConsoleKey.G] = new EquipAction(HandSlot.Right),
            [ConsoleKey.UpArrow] = new InventoryScrollAction(1),
            [ConsoleKey.DownArrow] = new InventoryScrollAction(-1),
            [ConsoleKey.W] = new MoveAction(Direction.Up),
            [ConsoleKey.S] = new MoveAction(Direction.Down),
            [ConsoleKey.A] = new MoveAction(Direction.Left),
            [ConsoleKey.D] = new MoveAction(Direction.Right),
        };
    }
    private void HandleInput()
    {       
        ConsoleKey key = Console.ReadKey(true).Key;

        if (actions.ContainsKey(key))
        {
            actions.TryGetValue(key, out var action);
            action.Execute(_state);
        }
        

    }

    public void Run()
    {
        Console.Clear();
        Console.CursorVisible = false;

        var exitAction = new ExitAction();

        exitAction.OnExit += () => _running = false;

        while (_running)
        {
            _renderer.Render();
            HandleInput();
        }
    }
}