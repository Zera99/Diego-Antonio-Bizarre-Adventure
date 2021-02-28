using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMissile : MonoBehaviour {

    public int Damage;
    public float Speed;
    public Transform PlayerTransform;

    private void Awake() {
        Destroy(this.gameObject, 15.0f);
    }

    // Start is called before the first frame update
    void Start() {
        Vector3 vectorToTarget = PlayerTransform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }

    // Update is called once per frame
    void Update() {
        transform.position += transform.right * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerModel p = collision.gameObject.GetComponent<PlayerModel>();
        if (p != null)
            p.TakeDamage(Damage);
        else if (collision.gameObject.tag == "Wall") {
            //TODO: Feedback de explosion
            Destroy(this.gameObject);
        }
    }
}
