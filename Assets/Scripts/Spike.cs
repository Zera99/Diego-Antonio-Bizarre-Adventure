using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour, IHazard
{
    public void MakeDamage(PlayerModel player)
    {
        player.TakeDamage(100);
    }
}
