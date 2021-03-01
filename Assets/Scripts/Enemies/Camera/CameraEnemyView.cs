using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnemyView : MonoBehaviour {

    public Light myLight;
    Animator _anim;

    private void Awake() {
        _anim = GetComponent<Animator>();
    }

    public void ChangeColor(Color color) {
        myLight.color = color;
    }

    public void DieFRQ() {
        _anim.SetTrigger("DieFRQ");
        Destroy(myLight);
    }

}
