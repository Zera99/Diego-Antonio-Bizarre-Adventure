using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowWithLightning : MonoBehaviour {

    private Animator _anim;

    // Start is called before the first frame update
    void Start() {
        _anim = this.GetComponent<Animator>();
        ResetAnimation();
    }

    IEnumerator WaitRandomTillNextLightning() {
        yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));
        _anim.Play("Window");
    }

    public void ResetAnimation() {
        _anim.Play("Waiting");
        StartCoroutine(WaitRandomTillNextLightning());
    }
}
