using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skins/Jetpack")]
public class JetpackSO : Skin {

    public JetpackStatsSO jetpackStats;

    public override void GetAttack(PlayerModel pl)
    {
        ISkill JetSkill = new JetpackSkill(pl, jetpackStats);
        pl.SetAttack(JetSkill.PrepareSkill, JetSkill.UseSkill, JetSkill.SecondSkill);

    }
}
