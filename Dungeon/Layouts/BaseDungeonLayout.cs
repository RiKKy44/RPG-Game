using OODProject.Actions;

namespace OODProject.Dungeon.Layouts;

public abstract class BaseDungeonLayout : IDungeonLayout
{
    public abstract void Apply(Board board);

    public IEnumerable<(ConsoleKey, IAction)> GetActions()
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
        yield return (ConsoleKey.J, new ShowHistoryAction());

    }
}