using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.GameLogic.Events;



public abstract class BaseSubject<T> : ISubject<T>
{
    private readonly List<IGameObserver<T>> _observers = new List<IGameObserver<T>>();

    public void Attach(IGameObserver<T> observer)
    {
        if (!_observers.Contains(observer)){
            _observers.Add(observer);
        }
    }


    public void Detach(IGameObserver<T> observer)
    {
        _observers?.Remove(observer);
    }

    public void Notify(T eventData)
    {
        foreach(var observer in _observers.ToArray())
        {
            observer.OnNotify(eventData);
        }
    }
}