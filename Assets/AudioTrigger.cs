using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip theSound;
    public float volume; //de 0 a 1
    AudioSource audioSource;
    Coroutine fadeCoroutine;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();     
    }

    /*void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerModel>())
        {           
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }           
        }
    }*/

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerModel>())
        {

            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            if (!audioSource.isPlaying)
            {               
                audioSource.clip = theSound;
                audioSource.volume = volume;
                audioSource.Play();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerModel>())
        {

            if (audioSource.isPlaying)
            {
                fadeCoroutine = StartCoroutine(FadeOutSound());                
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