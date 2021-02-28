using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSign : MonoBehaviour {
    private float _startingY;
    public float amplitude;
    public float sineSpeed;

    void Float(float startingY, float amp) {
        transform.position = new Vector3(transform.position.x,
                                    startingY + (Mathf.Sin(Time.time * sineSpeed) * amp), 0);
    }

    // Start is called before the first frame update
    void Start() {
        _startingY = transform.position.y;
    }

    // Update is called once per frame
    private void Update() {
        Float(_startingY, amplitude);
    }
}
