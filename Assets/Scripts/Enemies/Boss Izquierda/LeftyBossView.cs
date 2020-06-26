using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeftyBossView : MonoBehaviour
{
    Animator _anim;
    AudioSource _audSource;

    public AudioClip getHitClip;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _audSource = GetComponent<AudioSource>();

        foreach (var beh in _anim.GetBehaviours<LeftyAudioAnimation>())
        {
            beh.audSource = _audSource;
        }
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
        _anim.GetBehaviour<DeactiveElectricityAnimation>().action += () =>  _audSource.Stop();
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
