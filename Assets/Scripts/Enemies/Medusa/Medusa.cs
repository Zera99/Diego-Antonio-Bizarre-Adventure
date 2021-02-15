using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medusa : BasicEnemy {

    public int hp;
    public float hSpeed;

    public float amplitude;
    public float arcSpeed;

    public override void TakeDamage(int damage) {
        hp -= damage;
        if (hp == 0)
            Die();
    }
    private void Awake() {

    }

    // Start is called before the first frame update
    void Start() {
        //Destroy(this.gameObject, 10);
    }

    // Update is called once per frame
    void Update() {
        //Vector3 newPosition = new Vector3(-1 * hSpeed, Mathf.Sin((Mathf.PI * Time.time) * arcSpeed) * amplitude);
        //transform.position += newPosition * Time.deltaTime;

        transform.position += ((Vector3.left * hSpeed) + (Vector3.up * Mathf.Sin(Time.time * arcSpeed) * amplitude)) * Time.deltaTime;
    }



    void Die() {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerModel p = collision.GetComponent<PlayerModel>();
        if(p != null) {
            p.TakeDamage(damage);
        }
    }
}
