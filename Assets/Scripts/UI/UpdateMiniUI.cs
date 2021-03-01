using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextExtensions;

public class UpdateMiniUI : MonoBehaviour {
    public Text hpText;
	public Text livesText;
    public Text skillText;
    //string skillName = "EGG"; // Por defecto esta en EGG, para el inicio
    public Animator doomDuckAnim;
    public HUDController hudControl;

    public void UpdateHPText(int HP) {
        hpText.text = "" + HP;
        SetDoomDuckHPValue(HP);
    }
	
	public void UpdateLivesText(int lives) {
		livesText.text = "" + lives;
	}

    public void UpdateSkillText(int skillVal, float skillValue) {
        skillText.ChangeSkillText(skillVal, skillValue);
    }


    public void SetDoomDuckHPValue(float val) {
        doomDuckAnim.SetFloat("DuckHP", val);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            hudControl.ChangeToBase();
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            hudControl.ChangeIcon(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            hudControl.ChangeIcon(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            hudControl.ChangeIcon(2);
        }
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
