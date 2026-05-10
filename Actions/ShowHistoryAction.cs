using OODProject.Logs;
namespace OODProject.Actions;

public class ShowHistoryAction : IAction
{
    public string Description => "Show full history";
    public void Execute(GameState state)
    {
        Console.Clear();
        Console.WriteLine("=== GAME HISTORY ===");
        foreach(var log in GameLogger.Instance.AllLogs) Console.WriteLine(log);
        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
    }
}