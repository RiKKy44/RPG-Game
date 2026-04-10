using System.Net.Mime;
using System.Runtime.Versioning;
using OODProject.Actions;
using OODProject.Entities;
using OODProject.Dungeon;
using OODProject.Dungeon.Layouts;
namespace OODProject;

public class Game
{
    private Renderer _renderer;

    private GameState _state;

    private IDictionary<ConsoleKey, IAction> actions;
    public Game()
    {
        var layout = new DungeonLayouts();

        Board board = new DungeonBuilder().StartFilled().Apply(layout).Build();

        Player player = new Player( position: new Position(1, 1)); 

        _state = new GameState(player, board);

        _renderer = new Renderer(_state);

        actions = new Dictionary<ConsoleKey, IAction>();

        foreach (var (key,action) in layout.GetActions())
        {
            actions[key] = action;
        }

        _state.ActionDescriptions = actions
            .Select(kvp => $"[{kvp.Key}] - {kvp.Value.Description}")
            .Distinct()
            .ToList();
    }
    private void HandleInput()
    {       
        ConsoleKey key = Console.ReadKey(true).Key;

        if (actions.TryGetValue(key, out var action))
        {
            action.Execute(_state);
        }
        else
        {
            _state.Message = "Unknown key";
        }
        

    }

    public void Run()
    {
        Console.Clear();
        Console.CursorVisible = false;

        var exitAction = new ExitAction();

        exitAction.OnExit += () => _state.IsRunning = false;

        while (_state.IsRunning)
        {
            _renderer.Render();
            HandleInput();
        }
    }
}