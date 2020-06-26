﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLeftyBoss : MonoBehaviour
{
    public GameObject door;
    public GameObject wall;
    public GameObject loz;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerModel>())
        {
            FindObjectOfType<LeftyBossModel>().StartFight();
            FindObjectOfType<ShootyHyenaBro>().StartFight();
            door.SetActive(true);
            wall.SetActive(false);
            loz.SetActive(false);
            Destroy(gameObject);
        }
    }
}
