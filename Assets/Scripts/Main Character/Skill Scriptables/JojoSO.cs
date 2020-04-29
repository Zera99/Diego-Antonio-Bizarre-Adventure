using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skins/Jojo")]
public class JojoSO : Skin {

    public JojoStatsSO jojoStats;
    public override void GetAttack(PlayerModel pl) {
        throw new NotImplementedException();
    }
}
