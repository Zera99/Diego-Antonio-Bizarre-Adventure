using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour {

    public Light myLight;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(BlinkLight());
    }

    IEnumerator BlinkLight() {
        myLight.enabled = false;
        for(int i = 0; i < Random.Range(2, 6); i++) {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.6f));
            myLight.enabled = true;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
            myLight.enabled = false;
        }
        StartCoroutine(StayOn());
    }

    IEnumerator StayOn() {
        myLight.enabled = true;
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        StartCoroutine(BlinkLight());
    }
}
