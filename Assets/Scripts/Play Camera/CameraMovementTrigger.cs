using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementTrigger : MonoBehaviour {

    Camera mainCamera;

    public float newCameraDistance;

    public float movementSeconds;
    float originalSize;

    void Awake() {
        mainCamera = Camera.main;
        if(movementSeconds <= 0) {
            movementSeconds = 1.0f;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>() != null) {
            originalSize = mainCamera.orthographicSize;
            StartCoroutine(MoveSmooth());
        }
    }

    IEnumerator MoveSmooth() {
        float t = 0.0f;
        while (t <= 1.0f) {
            mainCamera.orthographicSize = Mathf.Lerp(originalSize, newCameraDistance, t);
            t += Time.deltaTime / movementSeconds;
            yield return new WaitForEndOfFrame();
        }
    }

}
