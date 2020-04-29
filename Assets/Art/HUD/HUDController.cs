﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour {
    public HUDIcon[] allIcons = new HUDIcon[4];
    int currentNum;
    HUDIcon currentIcon;

    private void Start() {
        currentIcon = allIcons[0];
        currentNum = 0;
    }

    public void ChangeIcon(int skillNum) {
        currentIcon.TurnOff();
        currentNum = skillNum;
        currentIcon = allIcons[currentNum];
        currentIcon.TurnOn();
    }

    public void ChangeToBase() {
        foreach(HUDIcon i in allIcons) {
            i.TurnOff();
        }
    }

}
