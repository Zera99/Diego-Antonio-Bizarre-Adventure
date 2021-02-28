using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMissile : MonoBehaviour {

    public int Damage;
    public float Speed;
    public float TimeToArm;
    public float TimeToLaunch;
    float internalTime;
    public Transform PlayerTransform;
    bool hasLaunched;
    private void Awake() {
        Destroy(this.gameObject, 15.0f);
    }

    // Start is called before the first frame update
    void Start() {
        hasLaunched = false;
        StartCoroutine(MoveOverSeconds(transform.position + Vector3.up * 10, TimeToArm));
    }

    // Update is called once per frame
    void Update() {
        if (hasLaunched)
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

    public IEnumerator MoveOverSeconds(Vector3 end, float seconds) {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        while (elapsedTime < seconds) {
            transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = end;
        StartCoroutine(RotateOverSeconds(TimeToLaunch));

        //hasLaunched = true;
    }

    public IEnumerator RotateOverSeconds(float seconds) {


        Vector3 vectorToTarget = PlayerTransform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime);



        float elapsedTime = 0;

        while (elapsedTime < seconds) {
            transform.rotation = Quaternion.Slerp(transform.rotation, q, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        hasLaunched = true;
    }

}
