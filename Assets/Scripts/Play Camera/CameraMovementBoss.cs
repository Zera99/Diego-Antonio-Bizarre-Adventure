using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementBoss : MonoBehaviour
{
    public CameraStatsSO stats;
    public Transform target;
    Vector3 modifierPos = new Vector3();
    public float _shakeAmount = 3;
    public float offsetY;

    //Vector3 velocity;

    //public float speed, xOffset, yOffset;


    // Start is called before the first frame update
    void Start()
    {
        EventsManager.SubscribeToEvent(Constants.EVENT_SHAKECAMERA, ShakeCamera);
    }


    // Update is called once per frame
    void LateUpdate()
    {
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

        float yDif = transform.position.y - target.position.y + offsetY;
        if (yDif > stats.yOffset || yDif < -stats.yOffset)
        {
            if (yDif > 0)
            {
                desiredPos.y = target.position.y + stats.yOffset + offsetY;
            }
            else
            {
                desiredPos.y = target.position.y - stats.yOffset + offsetY;
            }
        }

        Vector3 finalPos = Vector3.Lerp(transform.position, desiredPos, stats.speed * Time.deltaTime);
        transform.position = finalPos + modifierPos;
    }

    void ShakeCamera(params object[] p)
    {
        StartCoroutine(Shake((float)p[0]));
    }

    IEnumerator Shake(float shakeDuration)
    {
        modifierPos = Vector3.zero;

        while (shakeDuration > 0)
        {
            modifierPos = Random.insideUnitSphere * _shakeAmount;
            modifierPos.z = 0;

            shakeDuration -= Time.deltaTime;
            yield return null;
        }

        modifierPos = Vector3.zero;
    }

    void OnDestroy()
    {
        EventsManager.UnsubscribeToEvent(Constants.EVENT_SHAKECAMERA, ShakeCamera);
    }

}
