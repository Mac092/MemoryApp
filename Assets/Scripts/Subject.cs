using System.Collections.Generic;
using UnityEngine;

public class Subject
{
    private List<Observer> _observers = new List<Observer>();

    public void Subscribe(Observer observer)
    {
        _observers.Add(observer);
    }

    public void UnSubscribe(Observer observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        for (int i = 0; i < _observers.Count; i++)
        {
            _observers[i].NotifyUpdate();
        }
    }
}