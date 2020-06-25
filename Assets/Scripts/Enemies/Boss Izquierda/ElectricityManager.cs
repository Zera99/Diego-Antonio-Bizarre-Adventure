using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityManager : MonoBehaviour
{
    [Tooltip("Las que van a deshabilitarse cuando se rompa la primer bateria")]
    public Electricity[] firstBatteryElectricity;
    [Tooltip("Las que van a deshabilitarse cuando se rompa la segunda bateria")]
    public Electricity[] secondBatteryElectricity;
    [Tooltip("Electricidad que defiende al boss")]
    public Electricity bossDefense;
    System.Action<bool> useElectricity = delegate { };
    
    void Awake()
    {
        useElectricity += UseFirstBatteryElectricity;
        useElectricity += UseSecondBatteryElectricity;
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

    //En observer decir que si se destruyo una, se  useElectricity -= UseFirstBatteryElectricity;
}
