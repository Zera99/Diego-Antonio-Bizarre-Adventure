using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerModel pl = collision.GetComponent<PlayerModel>();
        if (pl)
        {
            pl.EnterChain(transform.position.x - pl.transform.position.x);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerModel pl = collision.GetComponent<PlayerModel>();
        if (pl)
        {
            pl.ExitChain();
        }
    }
}
