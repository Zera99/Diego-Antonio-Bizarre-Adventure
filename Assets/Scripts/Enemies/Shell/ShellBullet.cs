using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBullet : MonoBehaviour, IHazard {
    public Vector3 Direction { get; set; }
    public float speed;
    public int damage;

    private void Start() {
        Destroy(this, 20.0f);
    }

    // Update is called once per frame
    void Update() {
        this.transform.position += Direction * speed * Time.deltaTime;
    }

    //private void OnTriggerEnter2D(Collision2D collision) {
    //    if (collision.gameObject.GetComponent<PlayerModel>()) {
    //        collision.gameObject.GetComponent<PlayerModel>().TakeDamage(damage);
    //    }
    //    Destroy(this.gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }

    public void MakeDamage(PlayerModel player)
    {
        player.TakeDamage(damage);
    }
}
