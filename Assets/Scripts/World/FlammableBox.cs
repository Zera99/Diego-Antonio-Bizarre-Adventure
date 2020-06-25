using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableBox : MonoBehaviour {

    Animator anim;
    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.GetComponent<BowserFire>() != null) {
            Destroy(collision.gameObject);
            SetOnFire();
        }
    }

    public void SetOnFire() {
        Debug.Log("Set on fire. My pos is: " + transform.position);
        anim.SetTrigger("setOnFire");
        Destroy(this.gameObject, 0.70f);
    }

}
