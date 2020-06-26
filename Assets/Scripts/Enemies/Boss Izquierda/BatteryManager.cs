using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour, IObserver, IObservable
{
    int _totalBatteriesAlive, _currentBatteries;

    List<IObserver> _allObserver = new List<IObserver>();


    void Start()
    {
        IObservable[] batteries = GetComponentsInChildren<BatteryBoss>();

        _totalBatteriesAlive = _currentBatteries = batteries.Length;

        foreach (var bat in batteries)
        {
            bat.Subscribe(this);
        }

        Subscribe(FindObjectOfType<LeftyBossModel>());

    }
    
    void BatteryDead()
    {
        _currentBatteries--;

        var res = _totalBatteriesAlive - _currentBatteries;
        
        if (res == 1)
        {
            NotifyToObservers("FirstBatteryDestroyed");
        }
        else if (_currentBatteries == 0)
        {
            NotifyToObservers("AllBatteriesDestroyed");
        }
    }

    public void Notify(string action)
    {
        if (action == "BatteryDied")
        {
            BatteryDead();
        }
    }

    public void Subscribe(IObserver obs)
    {
        if (!_allObserver.Contains(obs))
        {
            _allObserver.Add(obs);
        }
    }

    public void Unsubscribe(IObserver obs)
    {
        if (_allObserver.Contains(obs))
        {
            _allObserver.Remove(obs);
        }
    }

    public void NotifyToObservers(string action)
    {
        for (int i = _allObserver.Count - 1; i >= 0; i--)
        {
            _allObserver[i].Notify(action);
        }
    }
}
