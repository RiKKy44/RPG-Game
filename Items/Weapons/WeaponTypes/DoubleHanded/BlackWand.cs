using OODProject.Items.Weapons.Categories;

namespace OODProject.Items.Weapons.WeaponTypes;

public class BlackWand : MagicalWeapon
{
    public BlackWand(int damage = 45, int weight = 5) : base(damage, weight) { }

    public override string GetName() => "Black Wand";
    
    public override bool IsTwoHanded() => true; 
}