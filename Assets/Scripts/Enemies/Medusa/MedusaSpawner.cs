using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaSpawner : MonoBehaviour {

    // Spawn limits x: 25, Y: 15 (With some padding)
    // Start is called before the first frame update

    float xSpawn = 25.0f;
    float ySpawn = 15.0f;
    //float padding = 5.0f;

    Vector3 GetRandomPosition() {
        return new Vector3(xSpawn, Random.Range(-ySpawn, ySpawn), 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerModel p = collision.gameObject.GetComponent<PlayerModel>();
        if(p != null) {
            SpawnMedusa(GetRandomPosition());
        }
    }

    void SpawnMedusa(Vector3 spawnPoint) {

    }

}
