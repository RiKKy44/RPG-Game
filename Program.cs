using System.Text;


namespace OODProject;

public class Program
{
    public static void Main(string[] args)
    {

        Console.WindowWidth = 120;
        Console.BufferWidth = 120;
        Console.WindowHeight = 35;
        Console.BufferHeight = 35;
        Console.OutputEncoding = Encoding.UTF8;

        ConfigurationData config;

        try
        {
            ConfigLoader.Load("config.ini");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading config.ini: {e.Message}");
            return;
        }
        Game game = new Game();
        game.Run();
    }
}