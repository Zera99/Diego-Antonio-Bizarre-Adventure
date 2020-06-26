using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityManager : MonoBehaviour, IObserver//, IObservable
{
    [Tooltip("Las que van a deshabilitarse cuando se rompa la primer bateria")]
    public Electricity[] firstBatteryElectricity;

    [Tooltip("Las que van a deshabilitarse cuando se rompa la segunda bateria")]
    public Electricity[] secondBatteryElectricity;

    [Tooltip("Electricidad que defiende al boss")]
    public Electricity bossDefense;

    System.Action<bool> useElectricity = delegate { };

    List<IObserver> _allObserver = new List<IObserver>();

    void Awake()
    {
        useElectricity += UseFirstBatteryElectricity;
        useElectricity += UseSecondBatteryElectricity;
    }

    private void Start()
    {
        FindObjectOfType<LeftyBossModel>().Subscribe(this);
        FindObjectOfType<BatteryManager>().Subscribe(this);

        bossDefense.SwitchActive(true);
        useElectricity(false);
    }

    void UseFirstBatteryElectricity(bool boolean)
    {
        foreach (var elect in firstBatteryElectricity)
        {
            elect.SwitchActive(boolean);
        }
    }

    void UseSecondBatteryElectricity(bool boolean)
    {
        foreach (var elect in secondBatteryElectricity)
        {
            elect.SwitchActive(boolean);
        }
    }

    //public void Subscribe(IObserver obs)
    //{
    //    if (!_allObserver.Contains(obs))
    //    {
    //        _allObserver.Add(obs);
    //    }
    //}

    //public void Unsubscribe(IObserver obs)
    //{
    //    if (_allObserver.Contains(obs))
    //    {
    //        _allObserver.Remove(obs);
    //    }
    //}

    //public void NotifyToObservers(string action)
    //{
    //    for (int i = _allObserver.Count - 1; i >= 0; i--)
    //    {
    //        _allObserver[i].Notify(action);
    //    }
    //}

    public void Notify(string action)
    {
        if (action == "FirstBatteryDestroyed")
        {
            useElectricity -= UseFirstBatteryElectricity;
        }
        else if (action == "AllBatteriesDestroyed")
        {
            useElectricity -= UseSecondBatteryElectricity;
            bossDefense.SwitchActive(false);
        }
        else if (action == "TurnOn")
        {
            useElectricity(true);
        }
        else if (action == "TurnOff")
        {
            useElectricity(false);
        }
    }
    
}
