namespace OODProject;

public class LevelLoader
{
    public Board Load()
    {
        Board board = new Board();

        board.SetField(new Position(3, 3), new Wall());
        
        board.GetField(new Position (7,6)).AddItem(new Coin());
        
        board.SetField(new Position(4, 3), new Wall());
        
        board.GetField(new Position (8,21)).AddItem(new Coin());

        board.SetField(new Position(5, 3), new Wall());
        
        return board;
    }    
}