using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjaBossAudioStop : MonoBehaviour {
    AudioSource source;
    bool gameIsPaused;

    private void Awake() {
        source = GetComponent<AudioSource>();
        gameIsPaused = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gameIsPaused) {
                if (!source.isPlaying) {
                    source.UnPause();
                    source.Play();

                }

            } else {
                if (source.isPlaying)
                    source.Pause();
            }
        }
    }
}
