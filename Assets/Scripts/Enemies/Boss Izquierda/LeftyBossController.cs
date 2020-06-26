using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftyBossController
{
    LeftyBossModel _m;

    public LeftyBossController(LeftyBossModel m, LeftyBossView v)
    {
        _m = m;

        v.FillActiveElectricity(_m.activeElectricity);
        v.FillDeactiveElectricity(_m.deactiveElectricity);

        _m.onSwitchElectricty += v.TriggerElectricityConsole;
        _m.onGetHit += v.GetHit;
        _m.onDeath += v.Die;
    }
}
