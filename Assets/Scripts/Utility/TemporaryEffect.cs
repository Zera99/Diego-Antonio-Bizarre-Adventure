using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryEffect : MonoBehaviour {

    public float timeToDestroy;
    // Start is called before the first frame update
    void Start() {
        transform.localPosition = Vector3.zero;
        Destroy(this.gameObject, timeToDestroy);
    }
}
