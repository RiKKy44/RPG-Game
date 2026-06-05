using System.Text;
using OODProject.Logs;
using OODProject.Network;


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

        Console.Clear();
        Console.WriteLine("[S] - Start as Server (Host)");
        Console.WriteLine("[C] - Start as Client (Player)");
        Console.Write("Choose mode: ");

        var key = Console.ReadKey(true).Key;

        if(key == ConsoleKey.S)
        {
            Console.Clear();

            Console.WriteLine("Starting server...");


            GameServer server = new GameServer(config);

            server.Start();
        }
        else if(key == ConsoleKey.C)
        {
            Console.Clear();
            Console.WriteLine("Starting client...");    

            Console.Write("Enter server IP (default: localhost): ");
            string? ip = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ip)) ip = "127.0.0.1";

            GameClient client = new GameClient();

            client.ConnectAsync(ip, 5555).GetAwaiter().GetResult();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Starting LOCAL game...");
            Game game = new Game(config);
            game.Run();
        }
    }
}