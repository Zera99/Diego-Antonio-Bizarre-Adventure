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

    private void Awake() {
        currentIndex = 0;
    }

    private void Start() {
        StartCoroutine(RunCutscene());
    }

    public void StartWaitForInput() {
        StartCoroutine(WaitForInput());
    }

    IEnumerator RunCutscene() {
        if(currentIndex == 0)
            yield return new WaitForSeconds(InitialCutsceneDelay);

        if (currentIndex < TextToDisplay.Count) {
            typer.TypeText(TextToDisplay[currentIndex], DelayBetweenLetters);
            currentIndex++;
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator WaitForInput() {
        Debug.Log("Started Wait for input");
        yield return new WaitUntil(() => Input.anyKeyDown);
        //bool notSkipping = true;
        //while (notSkipping) {
        //    if (Input.anyKeyDown) {
        //        notSkipping = false;
        //    }
        //    yield return new WaitForEndOfFrame();
        //}
        StartCoroutine(RunCutscene());
    }

}
