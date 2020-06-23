using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyHyenaBro : MonoBehaviour {
    FSM _fsm;
    MoveAndShootState       _moveState;
    MoveWithoutShieldState  _moveVulnerableState;

    public float timeBetweenStateChange;
    //float _currentTime;

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
    // Start is called before the first frame update
    void Start() {
        _fsm = new FSM();
        _moveState = new MoveAndShootState(this, wp1, wp2).SetParameters(shootingCooldown, moveSpeed, breathingTime, roundsForRatSlug);
        _moveVulnerableState = new MoveWithoutShieldState(this, vulnerableMoveSpeed, wp1, wp2);
        _fsm.ChangeState(_moveState);
    }

    // Update is called once per frame
    void Update() {
        _fsm.Update();
        //_currentTime += Time.deltaTime;

        //if(_currentTime >= timeBetweenStateChange) {
        //    if(_fsm.PeekCurrentState() == _moveState) {
        //        _fsm.ChangeState(_moveVulnerableState);
        //    } else {
        //        _fsm.ChangeState(_moveState);
        //    }
        //    _currentTime = 0;
        //}

        if(Input.GetKeyDown(KeyCode.B)) {
            ThrowBox();
        }

        if (Input.GetKeyDown(KeyCode.N)) {
            ShootBottle();
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            ShootMissile();
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
        Debug.Log(forceVector);
        Box.GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Impulse);
    }

    public void SpawnRatBoi() {
        // TODO: Spawn it D:<
    }
}
