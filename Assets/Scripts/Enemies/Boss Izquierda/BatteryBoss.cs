using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBoss : MonoBehaviour, IObservable
{
    public int maxLife;
    int _currentLife;

    bool _isDeath = false;

    Animator _anim;

    List<IObserver> _allObserver = new List<IObserver>();

    void Start()
    {
        _currentLife = maxLife;
        _anim = GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        if (_isDeath) return;

        _currentLife--;
        _anim.SetTrigger("Hit");

        if (_currentLife <= 0)
        {
            _isDeath = true;

            GetComponentInChildren<BatteryDoor>().Canceled();

            NotifyToObservers("BatteryDied");
        }
    }

    public void Subscribe(IObserver obs)
    {
        if (!_allObserver.Contains(obs))
        {
            _allObserver.Add(obs);
        }
    }

    public void Unsubscribe(IObserver obs)
    {
        if (_allObserver.Contains(obs))
        {
            _allObserver.Remove(obs);
        }
    }

    public void NotifyToObservers(string action)
    {
        for (int i = _allObserver.Count - 1; i >= 0; i--)
        {
            _allObserver[i].Notify(action);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Egg>())
            TakeDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BowserFire>())
            TakeDamage();
    }

}
