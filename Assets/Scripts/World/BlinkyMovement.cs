using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyMovement : MonoBehaviour {

    public Transform[] waypoints;
    List<Transform> _pathToTravel;
    System.Random _random = new System.Random();
    int _currentIndex;
    Vector3 _dir;
    SpriteRenderer _spriteRenderer;

    public float speed;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start() {
        transform.position = waypoints[Random.Range(0, waypoints.Length)].position;
        ResetPath();
    }

    void Update() {
        //Debug.Log(Vector3.Distance(transform.position, pathToTravel[currentIndex].position));
        if (Vector3.Distance(transform.position, _pathToTravel[_currentIndex].position) <= 0.3f) {
            DestinationReached();
        }
        transform.position += _dir * speed * Time.deltaTime;
    }

    List<Transform> CalculatePath() {
        List<Transform> path = new List<Transform>(waypoints);

        for (int i = 0; i < waypoints.Length - 1; i++) {
            int random = i + _random.Next(waypoints.Length - i);
            Transform n = path[random];
            path[random] = path[i];
            path[i] = n;
        }

        return path;
    }

    void DestinationReached() {
        if (_currentIndex < waypoints.Length - 1) {
            _currentIndex++;
            _dir = SetDirection();
        } else {
            ResetPath();
        }
    }

    void ResetPath() {
        _pathToTravel = CalculatePath();
        _currentIndex = 0;
        _dir = SetDirection();

    }
    Vector3 SetDirection() {
        Vector3 newDir = (_pathToTravel[_currentIndex].position - transform.position).normalized;

        if (newDir.x < 0)
            _spriteRenderer.flipX = false;
        else
            _spriteRenderer.flipX = true;

        return newDir;
    }
}
