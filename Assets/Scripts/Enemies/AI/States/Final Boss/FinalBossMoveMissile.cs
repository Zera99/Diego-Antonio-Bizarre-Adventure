using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossMoveMissile : IState {
    private FinalBoss boss;
    private GameObject fastMissilePrefab;
    private GameObject slowMissilePrefab;
    private float moveSpeed;
    private Transform playerTransform;
    bool inMoveState;
    float internalTimeFast;
    float internalTimeSlow;
    Vector3 dir;
    float timeForFast;
    float timeForSlow;

    public FinalBossMoveMissile(FinalBoss b, float move, GameObject fast, GameObject slow, Transform p) {
        this.boss = b;
        moveSpeed = move;
        this.fastMissilePrefab = fast;
        this.slowMissilePrefab = slow;
        playerTransform = p;
        dir = new Vector3(-1.0f, 0f, 0f);
    }

    public void Enter() {
        Debug.Log("Entered Move missile");
        inMoveState = true;
        timeForFast = Random.Range(3.0f, 8.0f);
        timeForSlow = Random.Range(1.0f, 4.0f);
    }

    public void Exec() {
        boss.transform.position += dir.normalized * moveSpeed * Time.deltaTime;
        internalTimeFast += Time.deltaTime;
        internalTimeSlow += Time.deltaTime;

        if (internalTimeFast >= timeForFast) {
            SpawnFastMissiles();
            internalTimeFast = 0;
        }

        if(internalTimeSlow >= timeForSlow) {
            SpawnSlowMissiles();
            internalTimeSlow = 0;
        }
    }

    public void Exit() {
        inMoveState = false;
    }

    public void FlipDirection() {
        Debug.Log("Flipped!");
        dir *= -1;
    }

    void SpawnFastMissiles() {
        FastMissile o = GameObject.Instantiate(fastMissilePrefab).GetComponent<FastMissile>();
        o.PlayerTransform = playerTransform;
        o.transform.position = boss.fastMissileSpawn.position;
    }

    void SpawnSlowMissiles() {
        SlowMissile o = GameObject.Instantiate(slowMissilePrefab).GetComponent<SlowMissile>();
        o.PlayerTransform = playerTransform;
        o.transform.position = boss.slowMissileSpawn.position;

    }

}
