using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.GameLogic.Events;


public class DeathBus : BaseSubject<DeathEvent>
{

    private static DeathBus? _instance;

    public static DeathBus Instance => _instance ??= new DeathBus();

    private DeathBus() { }
}