using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityDefenseBoss : Electricity
{
    public BoxCollider2D colliderBlock;

    protected override void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
        SwitchActive(true);
    }

    protected override void SwitchCollider(bool boolean)
    {
        base.SwitchCollider(boolean);
        colliderBlock.enabled = false;
    }
}
