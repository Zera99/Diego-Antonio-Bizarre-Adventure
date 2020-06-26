using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    protected Animator _anim;
    protected BoxCollider2D _collider;
    protected AudioSource _audSource;

    public AudioClip turnOnAudio;

    protected virtual void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
        _audSource = GetComponent<AudioSource>();

        SwitchCollider(false);
    }

    protected void Start()
    {
        _anim.GetBehaviour<ElectricityDamageAnimation>().switchCollider += SwitchCollider;
    }

    public void SwitchActive(bool boolean)
    {
        _anim.SetBool("Active", boolean);

        if (boolean)
        {
            _audSource.clip = turnOnAudio;
            _audSource.Play();
        }
        else
        {
            _audSource.Stop();
        }
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
