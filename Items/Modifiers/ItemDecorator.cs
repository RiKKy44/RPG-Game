using OODProject.Combat;
using OODProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Items.Modifiers;


public abstract class ItemDecorator : Item
{
    protected Item _innerItem;

    public ItemDecorator(Item innerItem)
    {
        _innerItem = innerItem;
    }

    public override void Accept(IAttackMethod visitor) => _innerItem.Accept(visitor);
    public override char GetSymbol() => _innerItem.GetSymbol();
    public override bool IsEquipable() => _innerItem.IsEquipable();
    public override bool IsTwoHanded() => _innerItem.IsTwoHanded();
    public override string GetName() => _innerItem.GetName();
    public override int GetDamage() => _innerItem.GetDamage();
    public override int GetDefenseBonus() => _innerItem.GetDefenseBonus();
    public override int GetLuckBonus() => _innerItem.GetLuckBonus();
    public override int GetStrengthBonus() => _innerItem.GetStrengthBonus();
    public override int GetDexterityBonus() => _innerItem.GetDexterityBonus();
    public override int GetWisdomBonus() => _innerItem.GetWisdomBonus();
    public override int GetAggressionBonus() => _innerItem.GetAggressionBonus();
}