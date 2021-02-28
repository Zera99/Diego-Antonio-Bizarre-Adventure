using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHazard : MonoBehaviour, IHazard, IHazardCollider {
    public int Damage;

    public void MakeCollisionDamage(PlayerModel player) {
        player.TakeDamage(Damage);
    }

    public void MakeDamage(PlayerModel player) {
        player.TakeDamage(Damage);
    }
}
