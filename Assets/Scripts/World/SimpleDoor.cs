using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    public Animator anim;

    private void Awake() {
        
    }

    public void OpenDoor() {
        anim.SetTrigger("openDoor");
        Destroy(gameObject.GetComponent<Collider2D>(), 1.5f);
        Destroy(this);
    }
}
