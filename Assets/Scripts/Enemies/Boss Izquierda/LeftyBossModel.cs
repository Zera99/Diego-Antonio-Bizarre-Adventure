using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeftyBossModel : BaseBoss
{
    LeftyBossController _controller;

    [Tooltip("Cuanto va a durar la electricidad")]
    public float electricityTime;
    [Tooltip("Cuanto va a durar la espera para volver a electrificar")]
    public float waitingTime;

    public event Action onGetHit = delegate { };
    public event Action onDeath = delegate { };

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        _controller = new LeftyBossController(this, GetComponent<LeftyBossView>());
    }

    // Update is called once per frame
    void Update()
    {
        _controller.UpdateController();
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
        onDeath();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Egg>() || collision.GetComponent<BowserFire>())
            TakeDamage();
    }
}
