using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Skins/Bowser")]
public class BowserSO : Skin {
    public BowserStatsSO bowserStats;
    public GameObject firePrefab;

    public override void GetAttack(PlayerModel pl)
    {
        ISkill bowserSkill = new BowserFireSkill(firePrefab, pl, bowserStats.fireDamage);
        pl.SetAttack(bowserSkill.PrepareSkill, bowserSkill.UseSkill);
    }
}
