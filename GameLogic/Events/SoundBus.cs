using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.GameLogic.Events;



public class SoundBus: BaseSubject<SoundEvent>
{
    private static SoundBus? _instance;

    public static SoundBus Instance => _instance ??= new SoundBus();

    private SoundBus() { }
}