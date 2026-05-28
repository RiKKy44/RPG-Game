using System.Text;
using OODProject.Logs;


namespace OODProject;

public class Program
{
    public static void Main(string[] args)
    {

        if (OperatingSystem.IsWindows())
        {
            Console.WindowWidth = 120;
            Console.BufferWidth = 120;
            Console.WindowHeight = 40; 
            Console.BufferHeight = 40; 
        }
        Console.OutputEncoding = Encoding.UTF8;

        ConfigurationData config;

        try
        {
            config = ConfigLoader.Load("config.ini");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading config.ini: {e.Message}");
            return;
        }
        
        GameLogger.Instance.SetStrategy(new FileLogStrategy(config.LogPath, config.PlayerName));
        Game game = new Game(config);
        game.Run();
    }
}