using RedBlueGames.Tools.TextTyper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTextControl : MonoBehaviour {

    public List<string> TextToDisplay;
    public TextTyper typer;
    public float InitialCutsceneDelay;
    public float DelayBetweenLetters;
    int currentIndex;
    int newIndex;
    public List<AudioClip> DiegoSounds;
    public List<AudioClip> OtaconSounds;
    public AudioClip RadioSound;

    public Animator DiegoAnimator;
    public Animator OtaconAnimator;
    public Animator RadioAnimator;
    int letters;


    AudioSource audioSource;
    private void Awake() {
        currentIndex = 0;
        letters = 0;
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 1.0f;
    }

    private void Start() {
        StartCoroutine(RunCutscene());
    }

    public void StartWaitForInput() {
        StartCoroutine(WaitForInput());
    }

    IEnumerator RunCutscene() {
        if (currentIndex == 0) {
            audioSource.PlayOneShot(RadioSound);
            yield return new WaitForSeconds(RadioSound.length);
            audioSource.pitch = 1.4f;
        }

        if (currentIndex < TextToDisplay.Count) {
            typer.TypeText(TextToDisplay[currentIndex], 0);
            currentIndex++;
            newIndex = currentIndex % 2;

            if (newIndex == 0) {
                DiegoAnimator.ResetTrigger("Idle");
                DiegoAnimator.SetTrigger("Talk");

            } else {
                OtaconAnimator.ResetTrigger("Idle");
                OtaconAnimator.SetTrigger("Talk");
            }

        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator WaitForInput() {
        Debug.Log("Started Wait for input");
        DiegoAnimator.SetTrigger("Idle");
        DiegoAnimator.ResetTrigger("Talk");
        OtaconAnimator.SetTrigger("Idle");
        OtaconAnimator.ResetTrigger("Talk");
        yield return new WaitUntil(() => Input.anyKeyDown);
        StartCoroutine(RunCutscene());
    }

    public void PlaySound() {
        letters++;

        if(letters >= 5) {
            if (newIndex == 0) {
                //audioSource.Stop();
                audioSource.PlayOneShot(DiegoSounds[Random.Range(0, DiegoSounds.Count)]);
            } else {
                //audioSource.Stop();
                audioSource.PlayOneShot(OtaconSounds[Random.Range(0, OtaconSounds.Count)]);
            }

            letters = 0;
        }

       
    }

}
