using OODProject.GameLogic.Events;
using OODProject.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Entities;


public class Enemy : Entity, IGameObserver<SoundEvent>, IGameObserver<DeathEvent>
{
    private string _name;
    private char _symbol;

    private static readonly Random _random = new Random();
    public int AttackValue { get; private set; }
    public int Armor { get; private set; }
    public Species EnemySpecies { get; }

    public Enemy(Position position, string name, char symbol, int health, int attackValue, int armor, Species species)
        : base(position, strength: 5, dexterity: 5, health: health, luck: 0, aggression: 10, wisdom: 0, maxHealh: health)
    {
        _name = name;
        _symbol = symbol;
        AttackValue = attackValue;
        Armor = armor;
        EnemySpecies = species;
        SoundBus.Instance.Attach(this);
        DeathBus.Instance.Attach(this);
    }

    public override string GetName() => _name;
    public override char GetSymbol() => _symbol;

    public override void Heal(int value)
    {
        Health = Math.Min(Health + value, MaxHealth);
    }

    public void OnNotify(SoundEvent eventData)
    {
        int distance = eventData.DungeonBoard.CalculateDistance(eventData.Source, CurrentPosition);

        if (distance != -1 && distance <= eventData.Range)
        {
            GameLogger.Instance.Log($"Enemy {_name} at {CurrentPosition} heard a noise from {eventData.Source} (Distance: {distance}).");
        }
    }


    public void OnNotify(DeathEvent eventData)
    {
        if (eventData.DeadSpecies != this.EnemySpecies) return;

        if (this.EnemySpecies == Species.Safe)
        {
            AttackValue = Math.Max(1, AttackValue - 2);
            Armor = Math.Max(0, Armor - 1);
            GameLogger.Instance.Log($"{_name} at {CurrentPosition} panics! Attack drops to {AttackValue}.");
        }
        else if (this.EnemySpecies == Species.Mage)
        {
            AttackValue += 3;
            Armor += 1;
            GameLogger.Instance.Log($"{_name} at {CurrentPosition} is enraged! Attack rises to {AttackValue}.");
        }
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        if (Health <= 0)
        {
            DeathBus.Instance.Notify(new DeathEvent(this.EnemySpecies));

            SoundBus.Instance.Detach(this);
            DeathBus.Instance.Detach(this);
        }
    }

    public void MoveRandomly(Board board, Position playerPosition)
    {

        int distanceToPlayer = Math.Abs(CurrentPosition.X - playerPosition.X) + Math.Abs(CurrentPosition.Y - playerPosition.Y);

        if(distanceToPlayer <= 1)
        {
            return;
        }

        Position[] directions = { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
        var validMoves = new List<Position>();

        foreach (var dir in directions)
        {
            Position nextPos = CurrentPosition + dir;

            if (nextPos.X >= 0 && nextPos.X < GameConfig.Width &&
                nextPos.Y >= 0 && nextPos.Y < GameConfig.Height &&
                board.GetField(nextPos).CanEnter())
            {
                if (nextPos == playerPosition) continue;

                bool isOccupied = false;
                foreach (var otherEnemy in board.Enemies)
                {
                    if (otherEnemy.CurrentPosition == nextPos)
                    {
                        isOccupied = true;
                        break;
                    }
                }

                if (!isOccupied)
                {
                    validMoves.Add(nextPos);
                }
            }
        }

        if (validMoves.Count > 0)
        {
            Position chosenMove = validMoves[_random.Next(validMoves.Count)];
            MoveTo(chosenMove); 
        }
    }
}