using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.GameLogic.Events;



public interface ISubject<T>
{
    void Attach(IGameObserver<T> observer);

    void Detach(IGameObserver<T> observer);

    void Notify(T eventData);
}