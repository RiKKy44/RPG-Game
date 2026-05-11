using OODProject.Items.Modifiers;
using OODProject.Items.Weapons.WeaponTypes.DoubleHanded;
using OODProject.Items.Weapons.WeaponTypes.SingleHanded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OODProject.Dungeon.Themes;
namespace OODProject.Dungeon.BuildingBlocks;


public class AddWeaponsProc : IBuildingBlock
{
    private readonly IDungeonTheme _theme;
    private readonly int _count;
    private Random _random = new Random();


    private readonly List<Func<Item>> _weaponFactories = new()
    {
        () => new Dagger(),
        () => new Sword(),
        () => new GreatAxe(),
        () => new GreatSword()
    };


    private readonly List<Func<Item, Item>> _modifierFactories = new()
    {
        (baseItem) => new StrongDecorator(baseItem),
        (baseItem) => new UnluckyDecorator(baseItem),
        (baseItem) => new SmartDecorator(baseItem)
    };

    public AddWeaponsProc(IDungeonTheme theme, int count = 5)
    {
        _count = count;
        _theme = theme;
    }

    public void Apply(Board board)
    {
        var emptyFields = board.GetAllFields().Where(f => f.CanEnter() && f.Items.Count == 0)
            .ToList();
        int artifactIndex = _random.Next(emptyFields.Count);
        Field artifactField = emptyFields[artifactIndex];
        
        artifactField.AddItem(_theme.CreateArtifact());
        
        emptyFields.RemoveAt(artifactIndex);
        int placed = 1;

        while(placed < _count && emptyFields.Count > 0)
        {
            int index = _random.Next(emptyFields.Count);
            Field field = emptyFields[index];

            Item weapon = _theme.CreateRandomWeapon();
            
            field.AddItem(weapon);
            
            emptyFields.RemoveAt(index);
            placed++;
        }
    }
}