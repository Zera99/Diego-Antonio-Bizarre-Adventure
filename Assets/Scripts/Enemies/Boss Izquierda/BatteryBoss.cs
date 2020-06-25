using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBoss : MonoBehaviour
{
    public int maxLife;
    int _currentLife;

    bool _isDeath = false;

    Animator _anim;

    // Start is called before the first frame update
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
            //OBSERVER
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Egg>() || collision.gameObject.GetComponent<BowserFire>())
            TakeDamage();
    }
}
