using OODProject.Entities;
using System.Linq;
using System.Collections.Generic;
using System;

namespace OODProject;

public class Renderer
{
    private GameState _state;

    private const int TotalConsoleWidth = 120;

    public Renderer(GameState state)
    {
        _state = state;
    }

    public void Render()
    {
        Console.SetCursorPosition(0, 0);
        
        if (_state.CurrentView == ViewMode.Map)
        {
            RenderMapScreen();
        }
        else
        {
            RenderInventoryScreen();
        }
    }
    private void RenderMapScreen()
    {
        var controls = _state.ActionDescriptions.ToList();

        for (int row = 0; row < GameConfig.Height; row++)
        {
            Console.SetCursorPosition(0, row);

            string line = "";
            for (int col = 0; col < GameConfig.Width; col++)
            {
                line += GetSymbolAt(new Position(col, row));
            }

            line += "        |         ";

            line += GetMapSidebarContent(row, controls);

            Console.Write(line.PadRight(TotalConsoleWidth));
        }
        Console.SetCursorPosition(0, GameConfig.Height + 1);
        Console.Write("Last Action: ".PadRight(TotalConsoleWidth));
        Console.SetCursorPosition(0, GameConfig.Height + 2);
        Console.Write(_state.Message.PadRight(TotalConsoleWidth));
    }

    private string GetMapSidebarContent(int row, List<string> controls)
    {
        if (row == 0) return "---- EQUIPPED ----";
        if (row == 1) return $"L: {GetHandDisplay(_state.Player.LeftHand)}";
        if (row == 2) return $"R: {GetHandDisplay(_state.Player.RightHand)}";

        if (row == 4) return "---- CURRENCY ----";
        if (row == 5) return $"Coins: {_state.Player.CoinCount}";
        if (row == 6) return $"Gold:  {_state.Player.GoldCount}";

        if (row == 8) return "---- CONTROLS ----";
        if (row >= 9 && row - 9 < controls.Count)
        {
            return controls[row - 9]; 
        }

        return "";
    }

    private void RenderInventoryScreen()
    {
        var controlsList = _state.ActionDescriptions.ToList();
        var controlLines = new List<string>();
        for (int i = 0; i < controlsList.Count; i += 4)
        {
            controlLines.Add(string.Join(" | ", controlsList.Skip(i).Take(4)));
        }

        int controlsStartY = GameConfig.Height - controlLines.Count - 1;

        for (int row = 0; row < GameConfig.Height; row++)
        {
            Console.SetCursorPosition(0, row);
            string line = "";

            if (row == 1) line = "   ===== CHARACTER & INVENTORY MENU =====";

            else if (row == 3) line = $"   Left Hand:  {GetHandDisplay(_state.Player.LeftHand)}";
            else if (row == 4) line = $"   Right Hand: {GetHandDisplay(_state.Player.RightHand)}";

            else if (row == 6) line = "   --- Attributes ---                 --- Currency ---";
            else if (row == 7) line = $"   Health:    {_state.Player.Health,-20} Coins: {_state.Player.CoinCount}";
            else if (row == 8) line = $"   Strength:  {_state.Player.Strength,-20} Gold:  {_state.Player.GoldCount}";
            else if (row == 9) line = $"   Dexterity: {_state.Player.Dexterity,-20}";
            else if (row == 10) line = $"   Luck:      {_state.Player.Luck,-20}";
            else if (row == 11) line = $"   Wisdom:    {_state.Player.Wisdom,-20}";
            else if (row == 12) line = $"   Aggression:{_state.Player.Aggression,-20}";

            else if (row == 14) line = "   --- Backpack ---";
            else if (row >= 15 && row < 15 + _state.Player.InventorySize)
            {
                line = "   " + GetInventoryRow(row - 15);
            }
            Console.Write(line.PadRight(TotalConsoleWidth));
        }
    }


    private char GetSymbolAt(Position position)
    {
        if (_state.Player.CurrentPosition == position)
            return _state.Player.GetSymbol();

        var enemy = _state.Enemies.FirstOrDefault(e => e.CurrentPosition == position);
        if (enemy != null)
            return enemy.GetSymbol();

        Field field = _state.Board.GetField(position);
        if (field.Items.Count > 0)
            return field.Items[0].GetSymbol();

        return field.GetSymbol();
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

   
}