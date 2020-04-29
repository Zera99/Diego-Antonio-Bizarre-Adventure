using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnemyView : MonoBehaviour {

    public Light light;

    public void ChangeColor(Color color) {
        light.color = color;
    }

}
