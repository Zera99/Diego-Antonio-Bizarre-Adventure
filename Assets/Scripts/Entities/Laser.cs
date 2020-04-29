using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IHazard {

    public float speed;
    public int laserDamage;

    // Update is called once per frame
    void Update() {
        transform.position += this.transform.up * speed * Time.deltaTime;
    }

    //private void OnCollisionEnter2D(Collision2D collision) {
    //    PlayerModel player = collision.gameObject.GetComponent<PlayerModel>();
    //    if (player) {
    //        player.TakeDamage(laserDamage);
    //        Destroy(this.gameObject); //CARLA ESTUVO AQUÍ
    //    }
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject); //CARLA ESTUVO AQUÍ
    }

    public void MakeDamage(PlayerModel player)
    {
        player.TakeDamage(laserDamage);
    }
}
