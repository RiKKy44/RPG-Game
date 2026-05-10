namespace OODProject.Logs;

public class GameLogger
{
    private ILogStrategy _logStrategy;

    private static GameLogger? _instance;

    public static GameLogger Instance => _instance ??= new GameLogger();

    public event Action<string>? OnLog;
    
    private readonly List<string> _allLogs = new List<string>();
    
    public IReadOnlyList<string> AllLogs => _allLogs;

    private GameLogger()
    {
    }

    public void SetStrategy(ILogStrategy logStrategy)
    {
        _logStrategy = logStrategy;
    }

    public void Log(string message)
    {
        string formattedMessage = $"[{DateTime.Now:HH:mm:ss}] {message}";
        
        _allLogs.Add(formattedMessage);
        
        _logStrategy?.Write(formattedMessage);

        OnLog?.Invoke(formattedMessage);
    }
}