using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.Network.Proxy;

public class NetworkItem : Item
{
    private string _name;
    private char _symbol;

    public NetworkItem(string name, char symbol)
    {
        _name = name;
        _symbol = symbol;
    }

    public override string GetName() => _name;
    public override char GetSymbol() => _symbol;
    public override bool IsEquipable() => false;
}