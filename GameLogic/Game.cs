using System.Net.Mime;
using System.Runtime.Versioning;
using OODProject.Actions;
using OODProject.Entities;
using OODProject.Dungeon;
using OODProject.Dungeon.Layouts;
using OODProject.Logs;
using OODProject.Dungeon.Themes;

namespace OODProject;

public class Game
{
    private Renderer _renderer;

    private GameState _state;

    private IDictionary<ConsoleKey, IAction> actions;
    public Game(ConfigurationData config)
    {
        IDungeonTheme[] themes = { new LibraryTheme(), new MetalTheme(), new TreasuryTheme() };
        IDungeonTheme selectedTheme = themes[new Random().Next(themes.Length)];
        IDungeonLayout layout = selectedTheme.GetLayoutStrategy();
        Board board = new DungeonBuilder().StartFilled().Apply(selectedTheme.GetLayoutStrategy()).Build();

        Player player = new Player(config.PlayerName, position: new Position(1, 1));

        _state = new GameState(player, board);
        
        GameLogger.Instance.Log($"Entered dungeon: {selectedTheme.GetType().Name}");
        
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
            GameLogger.Instance.Log($"Player pressed unknown key: {key}");
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