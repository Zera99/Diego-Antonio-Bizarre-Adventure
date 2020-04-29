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


    public void ShootBullet() {
        audioSource.PlayOneShot(shootSound);
        GameObject b = Instantiate(bulletPrefab);
        b.transform.position = spawnPosition.position;
        ShellBullet sB = b.GetComponent<ShellBullet>();
        sB.Direction = (isFacingRight) ? Vector3.right : -Vector3.right;
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
