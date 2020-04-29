using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicSmoke : MonoBehaviour, IHazard
{
    public int damage;

    public void MakeDamage(PlayerModel player)
    {
        player.TakeDamage(damage);
    }

    //private void OnTriggerEnter2D(Collider2D collision) {
    //    if (collision.gameObject.GetComponent<PlayerModel>())
    //        collision.gameObject.GetComponent<PlayerModel>().TakeDamage(damage);
    //}
}
