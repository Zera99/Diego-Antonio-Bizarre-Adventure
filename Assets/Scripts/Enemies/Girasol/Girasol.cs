using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girasol : MonoBehaviour {

    public int damage;
    public float damageRadius;
    Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerModel p = collision.GetComponent<PlayerModel>();
        if (p != null)
            StartCoroutine(ExecuteAttack(p));
    }

    IEnumerator ExecuteAttack(PlayerModel player) {
        anim.SetBool("Attacking", true);
        if(transform.position.x - Mathf.Abs(player.transform.position.x) < 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        yield return new WaitForSeconds(1f);
        if (Vector3.Distance(transform.position, player.transform.position) <= damageRadius) {
            player.TakeDamage(damage);
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }


}
