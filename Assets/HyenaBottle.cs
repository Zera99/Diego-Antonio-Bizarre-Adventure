using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyenaBottle : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public int damage;

    // Update is called once per frame
    void Update() {
        this.transform.position += Vector3.left * speed * Time.deltaTime;
        transform.Rotate(0, 0, rotationSpeed);
        //transform.rotation = new Quaternion(0, 0, transform.rotation.z + Time.deltaTime * rotationSpeed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>()) {
            collision.gameObject.GetComponent<PlayerModel>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
