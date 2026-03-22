using OODProject.Items.Weapons.WeaponTypes.DoubleHanded;
using OODProject.Items.Weapons.WeaponTypes.SingleHanded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Dungeon.BuildingBlocks;


public class AddWeaponsProc : IBuildingBlock
{

    private readonly int _count;
    private Random _random = new Random();


    private readonly List<Func<Item>> _weaponFactories = new()
    {
        () => new Dagger(),
        () => new Sword(),
        () => new GreatAxe(),
        () => new GreatSword()
    };

    public AddWeaponsProc(int count = 5)
    {
        _count = count;
    }

    public void Apply(Board board)
    {
        var emptyFields = board.GetAllFields().Where(f => f.CanEnter() && f.Items.Count == 0)
            .ToList();

        int placed = 0;

        while(placed < _count && emptyFields.Count > 0)
        {
            int index = _random.Next(emptyFields.Count);
            Field field = emptyFields[index];
            Item item = _weaponFactories[_random.Next(_weaponFactories.Count)]();

            field.AddItem(item);

            emptyFields.RemoveAt(index);
            placed++;
        }
    }
}