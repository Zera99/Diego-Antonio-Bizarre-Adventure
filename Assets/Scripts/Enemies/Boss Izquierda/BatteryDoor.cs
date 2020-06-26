using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryDoor : MonoBehaviour, IObserver
{
    Animator _anim;
    BoxCollider2D _collider;
    SpriteRenderer _sr;

    public LayerMask playerMask;
    public Vector3 cubeCastSize;
    public Vector2 movePlayerOffset;

    Vector2 _cubeCastPos;
    Vector2 _movePlayerSafePosition;

    IObservable _bossObservable;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _collider = GetComponentInChildren<BoxCollider2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();

        _bossObservable = FindObjectOfType<LeftyBossModel>();
        _bossObservable.Subscribe(this);

        _cubeCastPos = _sr.transform.position;

        _movePlayerSafePosition = _cubeCastPos + movePlayerOffset;
    }

    void Open()
    {
        _sr.enabled = false;
        _collider.enabled = false;
    }

    void Close()
    {
        Collider2D hitRes = Physics2D.OverlapBox(_cubeCastPos, cubeCastSize, 0, playerMask);

        if (hitRes)
        {
            _movePlayerSafePosition.y = hitRes.transform.position.y;
            hitRes.transform.position = _movePlayerSafePosition;
        }
        _sr.enabled = true;
        _collider.enabled = true;
    }

    public void Canceled()
    {
        _bossObservable.Unsubscribe(this);
    }

    public void Notify(string action)
    {
        if (action == "TurnOff")
        {
            Open();
        }
        else if(action == "TurnOn")
        {
            Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_cubeCastPos, cubeCastSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_movePlayerSafePosition, Vector2.one * 0.1f);
    }
}
