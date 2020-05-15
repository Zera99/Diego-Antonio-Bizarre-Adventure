using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }
    public Vector3 GetCheckpointPosition()
    {
        return transform.position;
    }

    public void ActivateCheckpoint() {
        anim.SetBool("isActivated", true);
    }
}
