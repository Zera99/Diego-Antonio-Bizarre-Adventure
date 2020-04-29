using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicEnemy : MonoBehaviour, IHazardCollider
{
    public int damage;

    public abstract void TakeDamage(int damage);
    public virtual void Die() {
        Destroy(this.gameObject);
    }

    public virtual void MakeCollisionDamage(PlayerModel player)
    {
        player.TakeDamage(damage);
    }
}
