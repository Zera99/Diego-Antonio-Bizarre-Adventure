using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public CameraStatsSO stats;
    public Transform target;

    //Vector3 velocity;

    //public float speed, xOffset, yOffset;


    // Start is called before the first frame update
    //void Start() {
    //    velocity = Vector3.zero;
    //}

    // Update is called once per frame
    void LateUpdate() {
        //Vector3 point = Camera.main.WorldToViewportPoint(target.position);
        //Vector3 posDelta = target.position - Camera.main.transform.position + stats.cameraRightOffset;
        //Vector3 dest = transform.position + posDelta;

        //transform.position = Vector3.SmoothDamp(transform.position, dest, ref velocity, stats.dampTime);

        Vector3 desiredPos = transform.position;

        float xDif = transform.position.x - target.position.x;
        if (xDif > stats.xOffset || xDif < -stats.xOffset)
        {
            if (xDif > 0)
            {
                desiredPos.x = target.position.x + stats.xOffset;
            }
            else
            {
                desiredPos.x = target.position.x - stats.xOffset;
            }
        }

        float yDif = transform.position.y - target.position.y;
        if (yDif > stats.yOffset || yDif < -stats.yOffset)
        {
            if (yDif > 0)
            {
                desiredPos.y = target.position.y + stats.yOffset;
            }
            else
            {
                desiredPos.y = target.position.y - stats.yOffset;
            }
        }


        transform.position = Vector3.Lerp(transform.position, desiredPos, stats.speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position + Vector3.up * stats.yOffset + Vector3.right * stats.xOffset, transform.position + Vector3.up * stats.yOffset - Vector3.right * stats.xOffset);
        Gizmos.DrawLine(transform.position + Vector3.up * stats.yOffset - Vector3.right * stats.xOffset, transform.position - Vector3.up * stats.yOffset - Vector3.right * stats.xOffset);
        Gizmos.DrawLine(transform.position - Vector3.up * stats.yOffset - Vector3.right * stats.xOffset, transform.position - Vector3.up * stats.yOffset + Vector3.right * stats.xOffset);
        Gizmos.DrawLine(transform.position - Vector3.up * stats.yOffset + Vector3.right * stats.xOffset, transform.position + Vector3.up * stats.yOffset + Vector3.right * stats.xOffset);
    }


}

