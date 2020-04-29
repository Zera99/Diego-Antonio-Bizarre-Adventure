using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class HUDIcon : MonoBehaviour {

    public Sprite offMode;
    public Sprite onMode;
    public Sprite numberOff;
    public Sprite numberOn;
    public Image spriteRenderer;
    public Image numberRenderer;

    bool isOn = false;

    public void Toggle() {
        isOn = !isOn;
        if (isOn) {
            TurnOn();
        } else {
            TurnOff();
        }
    }

    public void TurnOn() {
        spriteRenderer.sprite = onMode;
        numberRenderer.sprite = numberOn;
    }

    public void TurnOff() {
        spriteRenderer.sprite = offMode;
        numberRenderer.sprite = numberOff;
    }


}
