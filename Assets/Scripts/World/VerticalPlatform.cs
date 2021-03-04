using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour, IMovingPlatform {

    public Transform targetPosition;
    public float speed;
    Vector3 startPosition;
    Vector3 finishPosition;
    Rigidbody2D _rb;
    bool _isMoving;
    float _timer;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        startPosition = this.transform.position;
        finishPosition = targetPosition.position;
        _isMoving = false;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>() != null && !_isMoving) {
            _timer += Time.deltaTime;
            if (_timer >= 2.0f) {
                _timer = 0.0f;
                StartCoroutine(Move());
            }

        }
    }

    IEnumerator Move() {
        _isMoving = true;
        while (Vector3.Distance(this.transform.position, finishPosition) >= 0.5) {
            //transform.position += (finishPosition - startPosition).normalized * speed * Time.deltaTime;
            _rb.velocity = Vector2.up * speed;
            yield return new WaitForEndOfFrame();
        }
        _rb.velocity = Vector2.zero;
        Vector3 temp = finishPosition;
        finishPosition = startPosition;
        startPosition = temp;
        _isMoving = false;

        StartCoroutine(ReturnToOriginalPos());
    }

    IEnumerator ReturnToOriginalPos() {
        _isMoving = true;
        yield return new WaitForSeconds(3.0f);
        while (Vector3.Distance(this.transform.position, finishPosition) >= 0.5) {
            //transform.position += (finishPosition - startPosition).normalized * speed * Time.deltaTime;
            _rb.velocity = Vector2.down * speed;
            yield return new WaitForEndOfFrame();
        }
        _rb.velocity = Vector2.zero;
        Vector3 temp = finishPosition;
        finishPosition = startPosition;
        startPosition = temp;
        _isMoving = false;
    }

    public void ParentToPlatform(Transform player) {
        player.parent = this.transform;
    }
}
