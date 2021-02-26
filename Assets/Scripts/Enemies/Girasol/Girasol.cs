using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girasol : MonoBehaviour {

    public int damage;
    public float damageRadius;
    bool isAttacking;
    Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerModel p = collision.GetComponent<PlayerModel>();
        if (p != null)
            StartCoroutine(ExecuteAttack(p));
    }

    IEnumerator ExecuteAttack(PlayerModel player) {
        isAttacking = true;
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(1f);
        Debug.Log("Distance is: " + Vector3.Distance(transform.position, player.transform.position));
        if (Vector3.Distance(transform.position, player.transform.position) <= damageRadius) {
            player.TakeDamage(damage);

        }
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }


}
