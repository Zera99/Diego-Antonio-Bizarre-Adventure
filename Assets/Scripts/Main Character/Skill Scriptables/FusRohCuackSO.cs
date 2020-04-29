using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skins/FusRohCuack")]
public class FusRohCuackSO : Skin {
    public FusRohStatsSO fusRohStats;

    public override void GetAttack(PlayerModel pl) {
        ISkill fusRoSkill = new FusRohSkill(pl, fusRohStats.baseHeightCone, fusRohStats.lenghtCone, fusRohStats.endHeightCone);
        pl.SetAttack(fusRoSkill.PrepareSkill, fusRoSkill.UseSkill);
    }
}