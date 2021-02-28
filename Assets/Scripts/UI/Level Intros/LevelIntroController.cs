using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelIntroController : MonoBehaviour
{
    public PlayerModel player;
    public float MaxTime;
    public float OpacityTime;

    public RectTransform redBand;
    public RectTransform stageInfo;
    public Transform redBandTargetPos;
    public Transform stageInfoTargetPos;
    public Image backgroundImage;

    Vector3 initialRedBandPos;
    Vector3 initialStageInfoPos;

    private void Awake() {
        initialRedBandPos = redBand.position;
        initialStageInfoPos = stageInfo.position;
        player = FindObjectOfType<PlayerModel>();
        player.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SlideRedBandIn());
        StartCoroutine(SlideStageInfoIn());
    }

    IEnumerator SlideRedBandIn() {
        float currentTime = 0;
        Vector3 currentPos = redBand.position;
        while (currentTime < MaxTime) {
            redBand.position = Vector3.Lerp(currentPos, redBandTargetPos.position, (currentTime / MaxTime));
            currentTime += Time.deltaTime;
            yield return null;
        }
        redBand.position = redBandTargetPos.position;
        
    }

    IEnumerator SlideStageInfoIn() {
        yield return new WaitForSeconds(0.2f);
        float currentTime = 0;
        Vector3 currentPos = stageInfo.position;
        while (currentTime < MaxTime) {
            stageInfo.position = Vector3.Lerp(currentPos, stageInfoTargetPos.position, (currentTime / MaxTime));
            currentTime += Time.deltaTime;
            yield return null;
        }
        stageInfo.position = stageInfoTargetPos.position;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(SlideRedBandOut());
        StartCoroutine(SlideStageInfoOut());
    }

    IEnumerator SlideRedBandOut() {
        float currentTime = 0;
        Vector3 currentPos = redBand.position;
        while (currentTime < MaxTime) {
            redBand.position = Vector3.Lerp(currentPos, initialRedBandPos, (currentTime / MaxTime));
            currentTime += Time.deltaTime;
            yield return null;
        }
        redBand.position = initialRedBandPos;
    }

    IEnumerator SlideStageInfoOut() {
        yield return new WaitForSeconds(0.2f);
        float currentTime = 0;
        Vector3 currentPos = stageInfo.position;
        while (currentTime < MaxTime) {
            stageInfo.position = Vector3.Lerp(currentPos, initialStageInfoPos, (currentTime / MaxTime));
            currentTime += Time.deltaTime;
            yield return null;
        }
        stageInfo.position = initialStageInfoPos;
        StartCoroutine(OpacityLerp());
    }

    IEnumerator OpacityLerp() {
        float currentTime = 0;
        float currentOpacity = backgroundImage.color.a;
        while (currentTime < MaxTime) {
            backgroundImage.color = new Color(0, 0, 0, Mathf.Lerp(currentOpacity, 0, (currentTime / MaxTime)));
            currentTime += Time.deltaTime;
            if (currentOpacity <= 50 && !player.enabled)
                player.enabled = true;
            yield return null;
        }

        //player.enabled = true;
        StopAllCoroutines();
        Destroy(this.gameObject);

    }
}
