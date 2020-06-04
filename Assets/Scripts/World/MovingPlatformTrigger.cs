using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformTrigger : MonoBehaviour, IMovingPlatform {

    public Transform targetPosition;
    public float speed;
    Vector3 startPosition;
    Vector3 finishPosition;

    private void Start() {
        startPosition = this.transform.position;
        finishPosition = targetPosition.position;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>() != null) {
            StartCoroutine(Move());
        }
    }


    IEnumerator Move() {

        Debug.Log("Start: " + startPosition);
        Debug.Log("End: " + finishPosition);
        while (Vector3.Distance(this.transform.position, finishPosition) >= 0.1f)
        {
            transform.position += (finishPosition - startPosition).normalized * speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Vector3 temp = finishPosition;
        finishPosition = startPosition;
        startPosition = temp;

        Debug.Log("Start: " + startPosition);
        Debug.Log("End: " + finishPosition);
    }



    public void ParentToPlatform(Transform player) {
        player.parent = this.transform;
    }
}
