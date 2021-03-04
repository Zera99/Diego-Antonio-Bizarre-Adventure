using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenController : MonoBehaviour
{

    public float speed;

    //21.71
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveUp());
    }

    IEnumerator MoveUp() {
        while(transform.position.y < 21.71) {
            transform.position += Vector3.up * speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
