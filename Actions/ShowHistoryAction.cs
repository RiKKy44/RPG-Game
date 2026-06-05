using OODProject.Logs;
namespace OODProject.Actions;

public class ShowHistoryAction : IAction
{
    public string Description => "Show full history";
    public void Execute(GameState state)
    {
        state.CurrentView = ViewMode.History;
    }
}