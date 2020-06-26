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

    public virtual void DieFRQ() {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().gravityScale = 4;
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 10.0f);
    }

    public virtual void MakeCollisionDamage(PlayerModel player)
    {
        player.TakeDamage(damage);
    }
}
