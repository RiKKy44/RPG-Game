using System;
using System.Collections.Generic;
using System.Linq;
using OODProject.Actions;
using OODProject.Logs;

namespace OODProject;


public class LocalGameController
{
    private readonly GameState _state;
    private readonly ConsoleView _view;
    private readonly IDictionary<ConsoleKey, IAction> _actions;

    public LocalGameController(GameState state, ConsoleView view, IDictionary<ConsoleKey, IAction> actions)
    {
        _state = state;
        _view = view;
        _actions = actions;
    }

    public void RunGameLoop()
    {
        Console.Clear();
        Console.CursorVisible = false;

        while (_state.IsRunning)
        {
            _view.Render(); 
            HandleInput();

            if (_state.Player.Health <= 0)
            {
                _state.IsRunning = false;
            }
        }

        if (_state.Player.Health <= 0)
        {
            _view.RenderGameOverScreen(); 
            Console.ReadKey(true);      
        }
    }


    private void HandleInput()
    {
        ConsoleKey key = Console.ReadKey(true).Key;
        if (_actions.TryGetValue(key, out var action))
        {
            action.Execute(_state);
            if (_state.CurrentView == ViewMode.Map)
            {
                foreach (var enemy in _state.Board.Enemies.ToList())
                {
                    enemy.MoveRandomly(_state.Board, _state.Player.CurrentPosition);
                }
            }
        }
        else
        {
            _state.Message = "Unknown key";
            GameLogger.Instance.Log($"Player pressed unknown key: {key}");
        }
    }
}