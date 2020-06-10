using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour, IMovingPlatform {

    public Transform targetPosition;
    public float speed;
    Vector3 startPosition;
    Vector3 finishPosition;
    bool _isMoving;

    private void Start() {
        startPosition = this.transform.position;
        finishPosition = targetPosition.position;
        _isMoving = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>() != null && !_isMoving) {
            StartCoroutine(Move());
            _isMoving = true;
        }
    }

    IEnumerator Move() {
        while (Vector3.Distance(this.transform.position, finishPosition) >= 0.1f)
        {
            transform.position += (finishPosition - startPosition).normalized * speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Vector3 temp = finishPosition;
        finishPosition = startPosition;
        startPosition = temp;
        _isMoving = false;
    }



    public void ParentToPlatform(Transform player) {
        player.parent = this.transform;
    }
}
