using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Skin : ScriptableObject {

    public RuntimeAnimatorController newAnimator;
    public abstract void GetAttack(PlayerModel pl);

}
