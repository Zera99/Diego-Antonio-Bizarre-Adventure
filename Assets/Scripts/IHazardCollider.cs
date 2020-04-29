using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHazardCollider
{
    void MakeCollisionDamage(PlayerModel player);
}
