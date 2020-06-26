using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {
    public EventTrigger eventTrigger;
    public Sprite deactivatedSprite;
    public Sprite activatedSprite;
    public AudioClip doorOpenAudio;
    AudioSource _audSource;

    SpriteRenderer sr;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = deactivatedSprite;
    }

    private void Start()
    {
        _audSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<PlayerModel>() != null) {
            sr.sprite = activatedSprite;
            eventTrigger.TriggerEvents();
            _audSource.clip = doorOpenAudio;
            _audSource.Play();
        }
    }

}
