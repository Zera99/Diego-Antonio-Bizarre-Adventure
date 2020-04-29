using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public GameObject myPlatformTop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerModel pl = collision.GetComponent<PlayerModel>();
        if (pl)
        {
            pl.EnterLadderZone(myPlatformTop);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerModel pl = collision.GetComponent<PlayerModel>();
        if (pl)
        {
            pl.ExitLadderZone();
            myPlatformTop.SetActive(true);
        }
    }
}
