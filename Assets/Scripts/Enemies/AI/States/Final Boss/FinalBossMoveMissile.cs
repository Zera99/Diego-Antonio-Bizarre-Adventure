using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossMoveMissile : IState {
    private FinalBoss boss;
    private GameObject fastMissilePrefab;
    private float moveSpeed;
    private Transform playerTransform;
    float internalTimeFast;

    Vector3 dir;
    float timeForFast;


    public FinalBossMoveMissile(FinalBoss b, float move, GameObject fast, Transform p) {
        this.boss = b;
        moveSpeed = move;
        this.fastMissilePrefab = fast;
        playerTransform = p;
        dir = new Vector3(-1.0f, 0f, 0f);
    }

    public void Enter() {
        timeForFast = Random.Range(1.0f, 3.0f);
    }

    public void Exec() {
        boss.transform.position += dir.normalized * moveSpeed * Time.deltaTime;
        internalTimeFast += Time.deltaTime;

        if (internalTimeFast >= timeForFast) {
            SpawnFastMissiles();
            internalTimeFast = 0;
        }
    }

    public void Exit() {

    }

    public void FlipDirection() {

        dir *= -1;
    }

    void SpawnFastMissiles() {
        FastMissile o = GameObject.Instantiate(fastMissilePrefab).GetComponent<FastMissile>();
        o.PlayerTransform = playerTransform;
        o.transform.position = boss.fastMissileSpawn.position;
        boss.Attack();
    }

}
