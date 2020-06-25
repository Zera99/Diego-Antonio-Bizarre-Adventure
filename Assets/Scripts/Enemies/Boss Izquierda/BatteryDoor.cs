using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryDoor : MonoBehaviour
{
    Animator _anim;
    BoxCollider2D _collider;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _collider = GetComponentInChildren<BoxCollider2D>();
    }

    void Open()
    {

    }

    void Close()
    {

    }
}
