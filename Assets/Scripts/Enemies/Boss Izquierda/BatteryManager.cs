using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    int _totalBatteries;
    // Start is called before the first frame update
    void Start()
    {
        var batteries = GetComponentsInChildren<BatteryBoss>();
        _totalBatteries = batteries.Length;
        foreach (var bat in batteries)
        {
            //Subscribe
        }
    }
    
}
