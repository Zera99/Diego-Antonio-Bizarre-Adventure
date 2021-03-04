using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBoiStanding : MonoBehaviour {
    public GameObject hpPrefab;
    public GameObject deathEffect;
    public Transform itemSpawnPoint;
    public Transform effectSpawnPoint;
    public AudioSource audSource;
    public AudioClip triggerClip;
    public float time;
    Animator _anim;

    private void Awake() {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>() != null) {
            Destroy(GetComponent<BoxCollider2D>());
            _anim.SetTrigger("spawnItem");
            Destroy(GetComponent<Rigidbody2D>());

            audSource.clip = triggerClip;
            audSource.Play();

            StartCoroutine(Die());
        }

    }

    public void RatboiSpawnItem() {
        GameObject hp = Instantiate(hpPrefab);
        hp.transform.position = itemSpawnPoint.position;
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(time);
        GameObject effect = Instantiate(deathEffect, effectSpawnPoint);
        effect.transform.localPosition = Vector3.zero;
        Destroy(this.gameObject, 0.75f);
    }
}
