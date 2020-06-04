using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IMovingPlatform {
    private Vector2 startPosition;
    private Vector2 newPosition;
    public float speed = 3;
    public int maxDistance = 1;

    void Start() {
        startPosition = transform.position;
        newPosition = transform.position;
    }

    void Update() {
        newPosition.x = startPosition.x + (maxDistance * Mathf.Sin(Time.time * speed));
        transform.position = newPosition;
    }

    public void ParentToPlatform(Transform player) {
        player.parent = this.transform;
    }
}
