using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBoi : MonoBehaviour {
    public GameObject hpPrefab;
    public GameObject deathEffect;
    public Transform itemSpawnPoint;
    public Transform effectSpawnPoint;
    //public ShootyHyenaBro hyena;
    public float timeToDestroy;
    float _currentTime;
    Animator _anim;
    bool _hasLanded;
    SpriteRenderer _renderer;

    private void Awake() {
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        _currentTime = 0;
    }

    private void Update() {
        if (!_hasLanded) {
            transform.Rotate(0, 0, 20);
        } else {
            _currentTime += Time.deltaTime;
        }

        if (_currentTime > timeToDestroy && _hasLanded) {
            StartCoroutine(StunCoroutine());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>() != null && _hasLanded) {
            Destroy(GetComponent<BoxCollider2D>());
            _anim.SetTrigger("spawnItem");
            Destroy(GetComponent<Rigidbody2D>());
            Die();
        }

    }

    public void Land() {
        _anim.SetTrigger("hasLanded");
        _hasLanded = true;
        transform.rotation = Quaternion.identity;
    }

    public void RatboiSpawnItem() {
        GameObject hp = Instantiate(hpPrefab);
        hp.transform.position = itemSpawnPoint.position;
    }

    public void Die() {
        GameObject effect = Instantiate(deathEffect, effectSpawnPoint);
        effect.transform.localPosition = Vector3.zero;
        //hyena.DecrementRatCount();
        Destroy(this.gameObject, 0.75f);
    }

    IEnumerator StunCoroutine() {
        for (int i = 0; i < 8; i++) {
            _renderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            _renderer.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
        Die();
    }

}
