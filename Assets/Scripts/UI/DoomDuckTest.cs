using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoomDuckTest : MonoBehaviour
{
    public Animator anim;

    public void SetDoomDuckHPValue(float val) {
        anim.SetFloat("DuckHP", val);
    }


}
