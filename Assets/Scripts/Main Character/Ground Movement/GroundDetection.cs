using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour {

    public float distance;
    public Vector2 boxSize;
    public LayerMask groundLayers;
    

    public bool GroundDetect()
    {
        return (Physics2D.BoxCast(transform.position, boxSize, 0,Vector2.right,0, groundLayers));
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
