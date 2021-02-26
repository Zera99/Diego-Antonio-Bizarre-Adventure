using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcoHazards : MonoBehaviour {
    public List<GameObject> PrefabsToSpawn;
    public Vector3 MaxArea;
    public Vector3 MinArea;
    public float TimeBetweenSpawns;
    public int NumberOfItemsPerWave;

    private void Awake() {

    }

    IEnumerator SpawnObjects() {
        int index;
        for (int j = 0; j < 5; j++) {
            for (int i = 0; i < NumberOfItemsPerWave; i++) {
                index = Random.Range(0, PrefabsToSpawn.Count);
                GameObject o = Instantiate(PrefabsToSpawn[index]);
                o.transform.position = new Vector3(Random.Range(MinArea.x, MaxArea.x), Random.Range(MinArea.y, MaxArea.y), Random.Range(MinArea.z, MaxArea.z));
                o.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1.0f) * Random.Range(2, 8), ForceMode2D.Impulse);
                yield return new WaitForSeconds(TimeBetweenSpawns);
            }
        }
    }

    public void StartJumpPhase() {
        StartCoroutine(SpawnObjects());
    }

    public void StopPhase() {
        StopCoroutine(SpawnObjects());
    }


}
