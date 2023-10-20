using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Subject
{
    List<Observer> observers = new List<Observer>();

    public void Attach(Observer observer)
    {
        observers.Add(observer);
    }

    public void Dettach(Observer observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        foreach(Observer observer in observers)
        {
            observer.updateObserver();
        }
    }
}

public interface Observer
{
    public void updateObserver();

}
