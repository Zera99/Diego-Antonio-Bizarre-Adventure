using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girasol : MonoBehaviour {

    public int damage;
    public float damageRadius;

    void ExecuteAttack(PlayerModel player) {
        transform.position = transform.position + Vector3.up * 5; // Dependerá de la animación
        // Animacion
        if (Vector3.Distance(transform.position, player.transform.position) <= damageRadius)
            player.TakeDamage(damage);

        Destroy(gameObject, 5); // Duracion de la animacion
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerModel p = collision.GetComponent<PlayerModel>();
        if (p != null)
            ExecuteAttack(p);
    }

}
