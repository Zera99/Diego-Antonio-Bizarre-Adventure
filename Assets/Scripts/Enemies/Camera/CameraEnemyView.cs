using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnemyView : MonoBehaviour {

    public Light light;
    Animator _anim;

    private void Awake() {
        _anim = GetComponent<Animator>();
    }

    public void ChangeColor(Color color) {
        light.color = color;
    }

    public void DieFRQ() {
        _anim.SetTrigger("DieFRQ");
        Destroy(light);
    }

}
