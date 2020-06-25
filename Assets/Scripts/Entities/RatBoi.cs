using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBoi : MonoBehaviour {
    public GameObject hpPrefab;
    public GameObject effect;
    public Transform itemSpawnPoint;
    public ShootyHyenaBro hyena;
    Animator _anim;
    bool _hasLanded;

    private void Awake() {
        _anim = GetComponent<Animator>();
    }

    private void Update() {
        if(!_hasLanded)
            transform.Rotate(0, 0, 20);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>() != null && _hasLanded) {
            Destroy(GetComponent<BoxCollider2D>());
            _anim.SetTrigger("spawnItem");
            Destroy(GetComponent<Rigidbody2D>());
            hyena.DecrementRatCount();
            Destroy(this.gameObject, 3.0f);
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

}
