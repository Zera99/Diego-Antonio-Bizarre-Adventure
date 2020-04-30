using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour, IHazard
{

    float xPos;
    public int damage;
    public float min;
    public float max;
    public float speed;

    private void Awake() {
        xPos = this.transform.position.x;
    }

    private void Update() {
        transform.position = new Vector3((Mathf.PingPong(Time.time * speed, max - min) + min) + xPos, transform.position.y, transform.position.z);
    }

    //private void OnCollisionEnter2D(Collision2D collision) {
    //    if (collision.gameObject.GetComponent<PlayerModel>())
    //        collision.gameObject.GetComponent<PlayerModel>().TakeDamage(damage);
    //}

    public void MakeDamage(PlayerModel player)
    {
        player.TakeDamage(damage);
    }
}
