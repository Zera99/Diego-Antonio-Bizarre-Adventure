﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLeftyBoss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerModel>())
        {
            FindObjectOfType<LeftyBossModel>().StartFight();
            FindObjectOfType<ShootyHyenaBro>().StartFight();
            Destroy(gameObject);
        }
    }
}