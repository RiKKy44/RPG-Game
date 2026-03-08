using OODProject.Entities;
namespace OODProject;

public class Renderer
{
    private Board _board;
    private Player _player;
    private const int SidebarWidth = 30;
    public Renderer(Board board, Player player)
    {
        _board = board;
        _player = player;
    }

    public void Render()
    {
        Console.SetCursorPosition(0, 0);
        for (int row = 0; row < GameConfig.Height; row++)
        {
            for (int col = 0; col < GameConfig.Width; col++)
            {
                Position position = new Position(col, row);
                Console.Write(GetSymbolAt(position));
                Console.Write(' ');
            }
            Console.Write("     |   ");
            RenderSidebar(row);
            Console.Write("\r\n");
        }
    }

    private char GetSymbolAt(Position position)
    {
        if (_player.CurrentPosition.X == position.X &&
            _player.CurrentPosition.Y == position.Y)
        {
            return _player.GetSymbol();
        }
        Field field = _board.GetField(position);
        if (field.Items.Count > 0)
        {
            return field.Items[0].GetSymbol();
        }
        return field.GetSymbol();
    }
    
    public void RenderSidebar(int row)
    {
        string content = GetSidebarContent(row);
        Console.Write(content.PadRight(SidebarWidth));
    }

    private string GetSidebarContent(int row)
    {
        int inventoryStart = 1;
        int inventoryEnd = inventoryStart + _player.InventorySize;
        int equippedStart = inventoryEnd + 1;
        int attributeStart = equippedStart + 3;
        int currencyStart = attributeStart + 6;

        return row switch
        {
            0 => "---- INVENTORY ----",
            _ when row >= inventoryStart && row < inventoryEnd => GetInventoryRow(row - inventoryStart),
            _ when row == equippedStart => "---- EQUIPPED ----",
            _ when row == equippedStart + 1 => $"L: {GetHandDisplay(_player.LeftHand)}",
            _ when row == equippedStart + 2 => $"R: {GetHandDisplay(_player.RightHand)}",
            _ when row == attributeStart => "---- ATTRIBUTES ----",
            _ when row == attributeStart + 1 => $"Health:    {_player.Health}",
            _ when row == attributeStart + 2 => $"Strength:  {_player.Strength}",
            _ when row == attributeStart + 3 => $"Dexterity: {_player.Dexterity}",
            _ when row == attributeStart + 4 => $"Luck:      {_player.Luck}",
            _ when row == attributeStart + 5 => $"Wisdom:    {_player.Wisdom}",
            _ when row == attributeStart + 6 => $"Aggression:{_player.Aggression}",
            _ when row == currencyStart => "---- CURRENCY ----",
            _ when row == currencyStart + 1 => $"Coins: {_player.CoinCount}",
            _ when row == currencyStart + 2 => $"Gold:  {_player.GoldCount}",
            _ when row == currencyStart + 4 => GetStandingOnInfo(),
            _ => ""
        };
        
    }
    private string GetInventoryRow(int index)
    {
        if (index >= _player.Inventory.Count)
            return $"{index + 1}. ---";
        Item item = _player.Inventory[index];
        return $"{index + 1}. {item.GetSymbol()}";
    }
    private string GetHandDisplay(Item? item)
    {
        if (item == null)
        {
            return "empty";
        }
        return $"{item.GetSymbol()} {(item.IsTwoHanded() ? "(2H)" : "(1H)")}";
    }
    
    private string GetStandingOnInfo()
    {
        Field field = _board.GetField(_player.CurrentPosition);
        if (field.Items.Count == 0)
        {
            return "";
        }
        Item item = field.Items[0];
        return $"[E] Pick up: {item.GetSymbol()}";
    }
}
