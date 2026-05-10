using System.Text;
using OODProject.Logs;


namespace OODProject;

public class Program
{
    public static void Main(string[] args)
    {

        // Console.WindowWidth = 120;
        // Console.BufferWidth = 120;
        // Console.WindowHeight = 35;
        // Console.BufferHeight = 35;
        Console.OutputEncoding = Encoding.UTF8;

        ConfigurationData config;

        try
        {
            config = ConfigLoader.Load("/Users/kacper/Documents/Studia/Projects/OOD/OODProject/OODProject/GameLogic/Configuration/config.ini");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading config.ini: {e.Message}");
            return;
        }
        
        GameLogger.Instance.SetStrategy(new FileLogStrategy(config.LogPath, config.PlayerName));
        Game game = new Game();
        game.Run();
    }
}