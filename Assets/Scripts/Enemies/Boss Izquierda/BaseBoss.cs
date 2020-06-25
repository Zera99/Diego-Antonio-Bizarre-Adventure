using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBoss : MonoBehaviour
{
    public int maxLife;
    protected int _currentLife;

    protected virtual void Start()
    {
        _currentLife = maxLife;
    }

    protected abstract void TakeDamage(int damage = 1);
    protected abstract void Die();
}
