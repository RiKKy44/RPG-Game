using OODProject.Actions;
using OODProject.Dungeon.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Dungeon.Layouts;

public class DungeonLayouts : IDungeonLayout
{
    public void Apply(Board board)
    {
        new AddPathsProc().Apply(board);

        new AddCentralRoomProc().Apply(board);

        new AddChambersProc().Apply(board);

        new AddWeaponsProc().Apply(board);

        new AddItemsProc().Apply(board);

        new AddEnemiesProc().Apply(board);
    }


    public IEnumerable<(ConsoleKey,IAction)> GetActions()
    {
        yield return (ConsoleKey.W, new MoveAction(Direction.Up));
        yield return (ConsoleKey.S, new MoveAction(Direction.Down));
        yield return (ConsoleKey.D, new MoveAction(Direction.Right));
        yield return (ConsoleKey.A, new MoveAction(Direction.Left));
        yield return (ConsoleKey.E, new PickupAction());
        yield return (ConsoleKey.F, new EquipLeftHand());
        yield return (ConsoleKey.G, new EquipRightHand());
        yield return (ConsoleKey.Escape, new ExitAction());
        yield return (ConsoleKey.Q, new DropAction());
        yield return (ConsoleKey.H, new UnequipAction());
        yield return (ConsoleKey.UpArrow, new InventoryScrollAction(-1));
        yield return (ConsoleKey.DownArrow, new InventoryScrollAction(1));
        yield return (ConsoleKey.H, new UnequipAction());
        yield return (ConsoleKey.I, new ToggleInventoryAction());
        yield return (ConsoleKey.D1, new CombatAction(AttackStyle.Normal));
        yield return (ConsoleKey.D2, new CombatAction(AttackStyle.Stealth));
        yield return (ConsoleKey.D3, new CombatAction(AttackStyle.Magical));
    }
}