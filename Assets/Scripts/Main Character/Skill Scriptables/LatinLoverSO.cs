using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Skins/Latin Lover")]
public class LatinLoverSO : Skin {
    public GameObject eggPrefab;
    public LatinLoverStats stats;
    

    public override void GetAttack(PlayerModel pl)
    {
        ISkill eggSkill = new ThrowEggSkill(eggPrefab, pl, stats.throwForce, stats.eggDamage);
        pl.SetAttack(eggSkill.PrepareSkill, eggSkill.UseSkill);
    }


}
