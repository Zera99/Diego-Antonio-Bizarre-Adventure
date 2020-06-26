using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeftyBossModel : BaseBoss, IObservable, IObserver
{
    LeftyBossController _controller;

    [Tooltip("Cuanto va a durar la electricidad")]
    public float electricityTime;
    [Tooltip("Cuanto va a durar la espera para volver a electrificar")]
    public float waitingTime;

    List<IObserver> _allObserver = new List<IObserver>();

    public event Action onSwitchElectricty = delegate { };
    public event Action onGetHit = delegate { };
    public event Action onDeath = delegate { };
    public Action activeElectricity;
    public Action deactiveElectricity;
    
    protected override void Start()
    {
        base.Start();

        activeElectricity = ExecuteElectricity;
        deactiveElectricity = StopElectricity;

        _controller = new LeftyBossController(this, GetComponent<LeftyBossView>());
    }

    public void StartFight()
    {
        SwitchElectricity();
        StartCoroutine(KeepElectricityOn());
    }

    void SwitchElectricity()
    {
        onSwitchElectricty();
    }

    void ExecuteElectricity()
    {
        NotifyToObservers("TurnOn");
    }

    void StopElectricity()
    {
        NotifyToObservers("TurnOff");
    }

    protected override void TakeDamage(int damage = 1)
    {
        _currentLife -= damage;

        if (_currentLife <= 0)
            Die();
        else
            onGetHit();
    }

    protected override void Die()
    {
        NotifyToObservers("LeftyDied");
        onDeath();
    }

    IEnumerator KeepElectricityOn()
    {
        float tick = 0;

        while (tick <= electricityTime)
        {
            tick += Time.deltaTime;
            yield return null;
        }

        SwitchElectricity();

        StartCoroutine(WaitToStartElectricity());
    }

    IEnumerator WaitToStartElectricity()
    {
        float tick = 0;

        while (tick <= waitingTime)
        {
            tick += Time.deltaTime;
            yield return null;
        }

        SwitchElectricity();

        StartCoroutine(KeepElectricityOn());
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

    public void Notify(string action)
    {
        if (action == "AllBatteriesDestroyed")
        {
            StopAllCoroutines();
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
