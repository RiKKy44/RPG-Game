using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODProject.GameLogic.Events;



public abstract class BaseSubject<T> : ISubject<T>
{
    private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

    public void Attach(IObserver<T> observer)
    {
        if (!_observers.Contains(observer)){
            _observers.Add(observer);
        }
    }


    public void Detach(IObserver<T> observer)
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