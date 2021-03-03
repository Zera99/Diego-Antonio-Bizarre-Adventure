using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    Animator anim;
    AudioSource Source;
    public AudioClip checkpointAudio;
    public bool activated;

    private void Awake() {
        anim = GetComponent<Animator>();
        Source = GetComponent<AudioSource>();
    }

    private void Start() {
        activated = false;
    }
    public Vector3 GetCheckpointPosition() {
        return transform.position;
    }

    public void ActivateCheckpoint() {
        if (!activated) {
            activated = true;
            anim.SetBool("isActivated", true);
            Source.PlayOneShot(checkpointAudio);
        }
    }
}
