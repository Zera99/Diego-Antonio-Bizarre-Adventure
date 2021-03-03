using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public SimpleDoor DoorToOpen;
    public Sprite deactivatedSprite;
    public Sprite activatedSprite;
    public AudioClip doorOpenAudio;
    AudioSource _audSource;

    SpriteRenderer sr;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = deactivatedSprite;
    }

    private void Start() {
        _audSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<PlayerModel>() != null) {
            sr.sprite = activatedSprite;
            _audSource.clip = doorOpenAudio;
            _audSource.Play();
            if(DoorToOpen != null)
                DoorToOpen.OpenDoor();
        }
    }
}
