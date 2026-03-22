using OODProject.Entities;
namespace OODProject;

public class Renderer
{
    private GameState _state;
    private const int SidebarWidth = 30;

    public Renderer(GameState state)
    {
        _state = state;
    }

    public void Render()
    {
        for (int row = 0; row < GameConfig.Height; row++)
        {
            Console.SetCursorPosition(0, row);
            for (int col = 0; col < GameConfig.Width; col++)
            {
                Position position = new Position(col, row);
                Console.Write(GetSymbolAt(position));
            }
            Console.Write("        |         ");
            RenderSidebar(row);
        }
    }

    private char GetSymbolAt(Position position)
    {
        if (_state.Player.CurrentPosition == position)
            return _state.Player.GetSymbol();

        Field field = _state.Board.GetField(position);
        if (field.Items.Count > 0)
            return field.Items[0].GetSymbol();

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
        int inventoryEnd = inventoryStart + _state.Player.InventorySize;
        int equippedStart = inventoryEnd + 1;
        int attributeStart = equippedStart + 3;
        int currencyStart = attributeStart + 6;

        return row switch
        {
            0 => "---- INVENTORY ----",
            _ when row >= inventoryStart && row < inventoryEnd => GetInventoryRow(row - inventoryStart),
            _ when row == equippedStart => "---- EQUIPPED ----",
            _ when row == equippedStart + 1 => $"L: {GetHandDisplay(_state.Player.LeftHand)}",
            _ when row == equippedStart + 2 => $"R: {GetHandDisplay(_state.Player.RightHand)}",
            _ when row == attributeStart => "---- ATTRIBUTES ----",
            _ when row == attributeStart + 1 => $"Health:    {_state.Player.Health}",
            _ when row == attributeStart + 2 => $"Strength:  {_state.Player.Strength}",
            _ when row == attributeStart + 3 => $"Dexterity: {_state.Player.Dexterity}",
            _ when row == attributeStart + 4 => $"Luck:      {_state.Player.Luck}",
            _ when row == attributeStart + 5 => $"Wisdom:    {_state.Player.Wisdom}",
            _ when row == attributeStart + 6 => $"Aggression:{_state.Player.Aggression}",
            _ when row == currencyStart => "---- CURRENCY ----",
            _ when row == currencyStart + 1 => $"Coins: {_state.Player.CoinCount}",
            _ when row == currencyStart + 2 => $"Gold:  {_state.Player.GoldCount}",
            _ when row == currencyStart + 4 => GetStandingOnInfo(),
            _ => ""
        };
    }

    private string GetInventoryRow(int index)
    {
        string prefix = index == _state.InventoryPointer ? ">" : " ";
        if (index >= _state.Player.Inventory.Count)
            return $"{prefix} {index + 1}. ---";
        Item item = _state.Player.Inventory[index];
        return $"{prefix} {index + 1}. {item.GetSymbol()} ({item.GetName()})";
    }

    private string GetHandDisplay(Item? item)
    {
        if (item == null) return "empty";
        return $"{item.GetSymbol()} {item.GetName()}";
    }

    private string GetStandingOnInfo()
    {
        Field field = _state.Board.GetField(_state.Player.CurrentPosition);
        if (field.Items.Count == 0) return "";
        Item item = field.Items[0];
        return $"[E] Pick up: {item.GetSymbol()}";
    }
}