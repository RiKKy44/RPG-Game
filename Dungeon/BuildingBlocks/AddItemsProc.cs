using OODProject.Items.UnusableItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Dungeon.BuildingBlocks;

public class AddItemsProc : IBuildingBlock
{
    private readonly int _count;
    private Random _random = new Random();

    private readonly List<Func<Item>> _itemFactories = new()
    {
        () => new Rock(),
        () => new HealingPlus(),
        () => new Gold(),
        () => new Coin()
    };

    public AddItemsProc(int count = 20)
    {
        _count = count;
    }


    public void Apply(Board board)
    {
        var emptyFields = board.GetAllFields().
            Where(f => f.CanEnter() && f.Items.Count == 0).ToList();
        int placed = 0;


        while(placed < _count && emptyFields.Count > 0)
        {
            int index = _random.Next(emptyFields.Count);

            Field field = emptyFields[index];

            Item item = _itemFactories[_random.Next(_itemFactories.Count)]();

            field.AddItem(item);

            emptyFields.RemoveAt(index);

            placed++;
        }
    }

}