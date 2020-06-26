using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip theSound;
    public float volume; //de 0 a 1
    AudioSource audioSource;
    public BoxCollider2D c1;
    public BoxCollider2D c2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        c1.enabled = true;
        c2.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerModel>())
        {
            c1.enabled = !c1.enabled;
            c2.enabled = !c2.enabled;
            if (audioSource.clip != null)
            {
                StartCoroutine(FadeOutSound());
            }
            else
            {
                audioSource.clip = theSound;
                audioSource.volume = volume;
                audioSource.Play();
            }
        }
    }

    IEnumerator FadeOutSound()
    {
        float ticks = 0;
        while (ticks < 1)
        {
            ticks += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(volume, 0, ticks);
            yield return null;
        }
        audioSource.volume = 0;
        audioSource.Stop();
        audioSource.clip = null;
    }
}