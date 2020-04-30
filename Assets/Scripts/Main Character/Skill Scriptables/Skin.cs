using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Skin : ScriptableObject {

    public RuntimeAnimatorController newAnimator;
    public abstract void GetAttack(PlayerModel pl);
    public virtual void ExtraKeys(PlayerModel pl)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            pl.PressedRun();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            pl.ReleaseRun();
        }
    }

}
