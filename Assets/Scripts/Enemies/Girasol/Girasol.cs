using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girasol : MonoBehaviour {

    public int damage;
    public float damageRadius;
    Animator anim;
    AudioSource Source;
    public AudioClip explosion;

    private void Awake() {
        anim = GetComponent<Animator>();
        Source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerModel p = collision.GetComponent<PlayerModel>();
        if (p != null)
            StartCoroutine(ExecuteAttack(p));
        GetComponent<Collider2D>().enabled = false;
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
        Source.PlayOneShot(explosion);
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.gameObject, 3.0f);
    }


}
