using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Vector3 Direction { get; set; }
    public float speed;
    public int damage;
    private void Start() {
        Destroy(this.gameObject, 30.0f);
    }

    // Update is called once per frame
    void Update() {
        this.transform.position += Direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>()) {
            collision.gameObject.GetComponent<PlayerModel>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
