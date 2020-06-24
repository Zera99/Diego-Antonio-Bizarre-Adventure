using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBoi : MonoBehaviour {
    public GameObject hpPrefab;
    public GameObject effect;
    public Transform itemSpawnPoint;
    Animator _anim;
    bool _hasLanded;

    private void Awake() {
        _anim = GetComponent<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>() != null && _hasLanded) {
            Destroy(GetComponent<BoxCollider2D>());
            _anim.SetTrigger("spawnItem");
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(this.gameObject, 3.0f);
        }

    }

    public void Land() {
        _anim.SetTrigger("hasLanded");
        _hasLanded = true;
    }

    public void RatboiSpawnItem() {
        GameObject hp = Instantiate(hpPrefab);
        hp.transform.position = itemSpawnPoint.position;
    }

}
