using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    protected Animator _anim;
    protected BoxCollider2D _collider;

    protected virtual void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();

        SwitchCollider(false);
    }

    protected void Start()
    {
        _anim.GetBehaviour<ElectricityDamageAnimation>().switchCollider += SwitchCollider;
    }

    public void SwitchActive(bool boolean)
    {
        _anim.SetBool("Active", boolean);
    }

    protected virtual void SwitchCollider(bool boolean)
    {
        _collider.enabled = boolean;
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        PlayerModel pl = collision.GetComponent<PlayerModel>();
        if (pl)
            pl.TakeDamage(1);
    }
}
