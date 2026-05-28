using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.GameLogic.Events;




public interface IGameObserver<T>
{
    void OnNotify(T eventData);
}
