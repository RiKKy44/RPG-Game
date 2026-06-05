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
    private readonly ConfigurationData _config;


    public Game(ConfigurationData config)
    {
        _config = config;
    }
    public void Run()
    {
        IDungeonTheme[] themes = { new LibraryTheme(), new MetalTheme(), new TreasuryTheme() };
        IDungeonTheme selectedTheme = themes[new Random().Next(themes.Length)];
        IDungeonLayout layout = selectedTheme.GetLayoutStrategy();
        Board board = new DungeonBuilder().StartFilled().Apply(layout).Build();

        GameState state = new GameState(board);

        Player player = new Player(_config.PlayerName, position: new Position(1, 1));

        player.Id = 1;

        state.Players.Add(player);

        state.LocalPlayerId = player.Id;

        GameLogger.Instance.Log($"Player: {player.Name} Entered dungeon: {selectedTheme.GetType().Name}");
        GameLogger.Instance.Log(selectedTheme.GetIntroMessage());

        var actions = new Dictionary<ConsoleKey, IAction>();
        foreach (var (key, action) in layout.GetActions())
        {
            actions[key] = action;
        }
        state.ActionDescriptions = actions.Select(kvp => $"[{kvp.Key}] - {kvp.Value.Description}").Distinct().ToList();

        ConsoleView view = new ConsoleView(state);

        LocalGameController controller = new LocalGameController(state, view, actions);
        controller.RunGameLoop();
    }
}