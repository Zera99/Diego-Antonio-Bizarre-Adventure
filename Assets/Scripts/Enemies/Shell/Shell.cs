using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour, IHazardCollider
{
    public GameObject bulletPrefab;
    public Transform spawnPosition;
    public bool isFacingRight;
    public int damage;

    public AudioSource audioSource;
    public AudioClip shootSound;

    Animator _anim;

    private void Awake() {
        _anim = GetComponent<Animator>();
    }


    public void ShootBullet() {
        audioSource.PlayOneShot(shootSound);
        GameObject b = Instantiate(bulletPrefab);
        b.transform.position = spawnPosition.position;
        ShellBullet sB = b.GetComponent<ShellBullet>();
        sB.Direction = (isFacingRight) ? Vector3.right : -Vector3.right;
    }

    public void DieFRQ() {
        this.gameObject.AddComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        //GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().gravityScale = 4;
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 10.0f);
        _anim.SetTrigger("DieFRQ");
    }

    public void MakeCollisionDamage(PlayerModel player)
    {
        player.TakeDamage(damage);
    }
    //private void OnCollisionEnter2D(Collision2D collision) {
    //    if (collision.gameObject.GetComponent<PlayerModel>()) {
    //        collision.gameObject.GetComponent<PlayerModel>().TakeDamage(damage);
    //    }
    //}

}
