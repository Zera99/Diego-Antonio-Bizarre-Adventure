using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : EventObject {
    public EventTrigger trigger;
    public Animator anim;

    private void Awake() {
        trigger.AddObject(this);
    }

    public override void TriggerEvent() {
        anim.SetTrigger("openDoor");
        Destroy(gameObject.GetComponent<Collider2D>(), 1.5f);
        trigger.RemoveObject(this);
        Destroy(this);
    }

}
