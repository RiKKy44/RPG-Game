using OODProject.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Actions;

public class MoveAction : IAction
{
    private Position _direction;
    public MoveAction(Position direction)
    {
        _direction = direction;

        Description = direction switch
        {
            _ when direction == Direction.Up => "Move up",
            _ when direction == Direction.Down => "Move down",
            _ when direction == Direction.Left => "Move left",
            _ when direction == Direction.Right => "Move right",
        };
    }

    public string Description { get; init; }

    public void Execute(GameState state)
    {
        Position newPosition = state.Player.CurrentPosition + _direction;

        var enemy = state.Enemies.FirstOrDefault(e => e.CurrentPosition == newPosition);

        if (enemy != null) {
            IAttackMethod attackVisitor = state.CurrentAttack switch
            {
                AttackStyle.Stealth => new StealthAttack(state.Player),
                AttackStyle.Magical => new MagicalAttack(state.Player),
                _ => new NormalAttack(state.Player)
            };


            Item? weapon = state.Player.RightHand ?? state.Player.LeftHand;
            if (weapon != null)
            {
                weapon.Accept(attackVisitor);
            }
            else
            {
                attackVisitor.Visit((Item)null!);
            }

            int damageToEnemy = Math.Max(0, attackVisitor.CalculatedDamage - enemy.Armor);
            enemy.TakeDamage(damageToEnemy);
            state.Message = $"YOU -> {enemy.GetName()}: -{damageToEnemy}";

            if (enemy.Health <= 0) {
                state.Message += $"You killed {enemy.GetName()}!";
                state.Enemies.Remove(enemy);
                return;
            }


            int damageToPlayer = Math.Max(0, enemy.AttackValue - attackVisitor.CalculatedDefense);
            state.Player.TakeDamage(damageToPlayer);
            state.Message += $"{enemy.GetName()} -> You: -{damageToPlayer}HP.";


            if (state.Player.Health <= 0)
            {
                state.Message = "YOU DIED! GAME OVER.";
                state.IsRunning = false;
            }

        }
        else if (state.Board.CanEnter(newPosition))
        {
            state.Player.MoveTo(newPosition);
            state.Message = "";
        }
    }
}