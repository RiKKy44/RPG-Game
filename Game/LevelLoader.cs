using OODProject.Items.Weapons.WeaponTypes.DoubleHanded;
using OODProject.Items.Weapons.WeaponTypes.SingleHanded;

namespace OODProject;

public class LevelLoader
{
    public Board Load()
    {
        Board board = new Board();

        board.SetField(new Position(3, 3), new Wall());
        
        board.GetField(new Position (7,6)).AddItem(new Coin());
        
        board.SetField(new Position(4, 3), new Wall());

        board.GetField(new Position(12, 12)).AddItem(new Dagger());
        
        board.GetField(new Position (8,21)).AddItem(new Coin());
        
        board.GetField(new Position(14,17)).AddItem(new GreatAxe());

        board.GetField(new Position(4,16)).AddItem(new Gold());

        board.SetField(new Position(5, 3), new Wall());
        
        return board;
    }    
}