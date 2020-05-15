using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextExtensions;

public class UpdateMiniUI : MonoBehaviour {
    public Text hpText;
    public Text skillText;
    string skillName = "EGG"; // Por defecto esta en EGG, para el inicio
    public Animator doomDuckAnim;

    public void UpdateHPText(int HP) {
        hpText.text = "" + HP;
        SetDoomDuckHPValue(HP);
    }

    public void UpdateSkillText(int skillVal, float skillValue) {
        skillText.ChangeSkillText(skillVal, skillValue);
    }


    public void SetDoomDuckHPValue(float val) {
        doomDuckAnim.SetFloat("DuckHP", val);
    }

    // ----------------------------- TEST ------------------------------------
    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.Alpha1)) {
    //        skillName = "EGG";
    //    } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
    //        skillName = "BOW";
    //    } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
    //        Debug.Log("FRQ Set");
    //        skillName = "FRQ";
    //    } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
    //        skillName = "JET";
    //    }
    //}

}
