using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour {

    public Text livesText;
    public Text hpText;
    public Animator doomDuckAnim;
    public HUDController hudControl;

    public void UpdateLivesText(int lives) {
        livesText.text = "" + lives;
    }

    public void UpdateHPText(int HP) {
        hpText.text = "" + HP;
        SetDoomDuckHPValue(HP);
    }

    public void SetDoomDuckHPValue(float val) {
        doomDuckAnim.SetFloat("DuckHP", val);
    }

    // ----------------------------- TEST ------------------------------------
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            hudControl.ChangeToBase();
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            hudControl.ChangeIcon(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            hudControl.ChangeIcon(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            hudControl.ChangeIcon(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            hudControl.ChangeIcon(3);
        }
    }
}
