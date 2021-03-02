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

    AudioSource audioSource;
    private void Awake() {
        currentIndex = 0;
        audioSource = GetComponent<AudioSource>();
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
            //yield return new WaitForSeconds(RadioSound.length + 0.5f);
            yield return new WaitForSeconds(1.0f);
        }

        if (currentIndex < TextToDisplay.Count) {
            typer.TypeText(TextToDisplay[currentIndex], DelayBetweenLetters);
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
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

        if (newIndex == 0) {
            audioSource.Stop();
            audioSource.PlayOneShot(DiegoSounds[Random.Range(0, DiegoSounds.Count)]);
        } else {
            audioSource.Stop();
            audioSource.PlayOneShot(OtaconSounds[Random.Range(0, OtaconSounds.Count)]);
        }
    }

}
