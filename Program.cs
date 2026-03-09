using System.Text;

namespace OODProject;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Game game = new Game();
        game.Run();
    }
}