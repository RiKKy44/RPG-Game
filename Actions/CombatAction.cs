using OODProject.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Actions;

public class CombatAction : IAction
{
    private AttackStyle _attackStyle;

    public string Description => $"Attack: {_attackStyle}";

    public CombatAction(AttackStyle style)
    {
        _attackStyle = style;
    }
    public void Execute(GameState state)
    {
        if (state.CurrentView != ViewMode.Combat || state.CurrentEnemy == null) return;

        var player = state.Player;
        var enemy = state.CurrentEnemy;

        IAttackMethod attackVisitor = _attackStyle switch
        {
            AttackStyle.Stealth => new StealthAttack(player),
            AttackStyle.Magical => new MagicalAttack(player),
            _ => new NormalAttack(player)
        };

        Item? weapon = player.RightHand ?? player.LeftHand;
        if (weapon != null) weapon.Accept(attackVisitor);
        else attackVisitor.Visit((Item)null!);

        int dmgToEnemy = Math.Max(0, attackVisitor.CalculatedDamage - enemy.Armor);
        enemy.TakeDamage(dmgToEnemy);
        state.Message = $"YOU -> {enemy.GetName()} -{dmgToEnemy} HP ({_attackStyle} attack)     ";

        if (enemy.Health <= 0)
        {
            state.Message += $"You killed {enemy.GetName()}! Click random move button to continue";
            state.Board.Enemies.Remove(enemy);
            state.CurrentEnemy = null;
            state.CurrentView = ViewMode.Map; 
            return;
        }

        int dmgToPlayer = Math.Max(0, enemy.AttackValue - attackVisitor.CalculatedDefense);
        player.TakeDamage(dmgToPlayer);
        state.Message += $"{enemy.GetName()} -> YOU -{dmgToPlayer} HP";

        if (player.Health <= 0)
        {
            state.Message += $"You were killed by {enemy.GetName()}!";
            state.IsRunning = false;
        }
    }
}