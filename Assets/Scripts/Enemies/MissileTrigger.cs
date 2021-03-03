using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTrigger : MonoBehaviour
{

    public Transform spawnPoint;
    public GameObject missilePrefab;

    private void Awake() {
        Destroy(this.gameObject, 4.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>()) {
            ShootMissile();
            Destroy(this.gameObject);
        }
    }

    private void ShootMissile() {
        GameObject m = Instantiate(missilePrefab);
        m.transform.position = spawnPoint.position;
        Missile mM = m.GetComponent<Missile>();
        mM.Direction = -Vector3.right;
    }

}
