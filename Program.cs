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
        Game game = new Game();
        game.Run();
    }
}