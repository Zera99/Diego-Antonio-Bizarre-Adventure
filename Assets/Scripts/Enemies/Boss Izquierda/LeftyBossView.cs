using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeftyBossView : MonoBehaviour
{
    Animator _anim;
    
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void FillActiveElectricity(Action action)
    {
        if (!_anim)
            _anim = GetComponent<Animator>();
        _anim.GetBehaviour<ActiveElectricityAnimation>().action += action;
    }

    public void FillDeactiveElectricity(Action action)
    {
        if (!_anim)
            _anim = GetComponent<Animator>();
        _anim.GetBehaviour<DeactiveElectricityAnimation>().action += action;
    }

    public void TriggerElectricityConsole()
    {
        _anim.SetTrigger("UseConsole");
    }

    public void GetHit()
    {
        _anim.SetTrigger("GetHit");
    }

    public void Die()
    {
        _anim.SetTrigger("Die");
    }
}
