using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaSpawner : MonoBehaviour {

    // Spawn limits x: 25, Y: 15 (With some padding)
    // Start is called before the first frame update

    float xSpawn = 25.0f;
    float ySpawn = 15.0f;
    float padding = 5.0f;
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    Vector3 GetRandomPosition() {
        return new Vector3(xSpawn, Random.Range(-ySpawn, ySpawn), 0);
    }


}
