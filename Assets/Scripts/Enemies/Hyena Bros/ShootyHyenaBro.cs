using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyHyenaBro : MonoBehaviour {
    FSM _fsm;
    MoveAndShootState       _moveState;

    BoxCollider2D _collider;

    public int HP;
    public int maxRatSlug;
    int ratCount;

    public Transform wp1, wp2;
    public float moveSpeed;
    public float vulnerableMoveSpeed;

    public GameObject hyenaMissilePrefab;
    public GameObject hyenaBottlePrefab;
    public GameObject hyenaBoxPrefab;
    public GameObject ratSlugPrefab;
    public Transform bulletSpawnPoint;
    public float shootingCooldown;
    public float breathingTime;
    public float maxThrowForce;
    public int roundsForRatSlug;

    private void Awake() {
        _collider = GetComponent<BoxCollider2D>();
        _collider.enabled = false;
    }

    // Start is called before the first frame update
    void Start() {
        _fsm = new FSM();
        _moveState = new MoveAndShootState(this, wp1, wp2).SetParameters(shootingCooldown, moveSpeed, breathingTime, roundsForRatSlug);
        _fsm.ChangeState(_moveState);
    }

    // Update is called once per frame
    void Update() {
        _fsm.Update();


        if(Input.GetKeyDown(KeyCode.B)) {
            OnLeftBossDeath();
        }
    }

    public void ShootMissile() {
        GameObject bullet = Instantiate(hyenaMissilePrefab);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.GetComponent<Missile>().Direction = Vector3.left;
    }

    public void ShootBottle() {
        GameObject bottle = Instantiate(hyenaBottlePrefab);
        bottle.transform.position = bulletSpawnPoint.position;
    }

    public void ThrowBox() {
        GameObject Box = Instantiate(hyenaBoxPrefab);
        Box.transform.position = bulletSpawnPoint.position;
        Vector2 forceVector = new Vector2(Random.Range(-1, 0), 1).normalized * Random.Range(5, maxThrowForce);
        Box.GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Impulse);
    }

    public void SpawnRatBoi() {
        if (ratCount + 1 > maxRatSlug)
            return;

        GameObject rat = Instantiate(ratSlugPrefab);
        rat.transform.position = bulletSpawnPoint.position;
        rat.GetComponent<RatBoi>().hyena = this;
        ratCount++;
        Vector2 forceVector = new Vector2(Random.Range(-1, 0), 1).normalized * Random.Range(5, maxThrowForce);
        rat.GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Impulse);

    }

    public void DecrementRatCount() {
        ratCount--;
    }


    public void OnLeftBossDeath() {
        _collider.enabled = true;
        _moveState.ChangeSpeed(vulnerableMoveSpeed);
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Egg>() != null || collision.gameObject.GetComponent<BowserFire>() != null) {
            Destroy(collision.gameObject);
            HP--;
            Debug.Log("Boss Recibio Daño");

            if (HP <= 0) {
                Destroy(this.gameObject);
                Debug.Log("Boss destruido");
            }
        }
    }
}
