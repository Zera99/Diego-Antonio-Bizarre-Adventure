using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    Animator _anim;
    BoxCollider2D _collider;


    void Start()
    {
        _anim = GetComponent<Animator>();

        //Buscar el animator behaviour y setearle SwitchCollider
    }

    public void SwitchActive(bool boolean)
    {
        _anim.SetBool("Active", boolean);
    }

    void SwitchCollider(bool boolean)
    {
        _collider.enabled = boolean;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerModel pl = collision.GetComponent<PlayerModel>();
        if (pl)
            pl.TakeDamage(1);
    }
}
