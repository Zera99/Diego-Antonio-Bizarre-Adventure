using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHazard : MonoBehaviour, IHazard {
    public int Damage;

    public void MakeDamage(PlayerModel player) {
        player.TakeDamage(Damage);
    }
}
