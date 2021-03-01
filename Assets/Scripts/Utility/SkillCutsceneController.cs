using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCutsceneController : MonoBehaviour {

    public List<Transform> waypoints;
    public Transform pato;
    public Transform HumoPosition;
    public float patoSpeed;
    public GameObject HumoPrefab;
    public Transform Fulton;
    public Sprite boxlessFulton;
    public float FultonSpeed;
    public GameObject Bottle;
    public GameObject Box;
    public Animator patoAnim;
    public Animator ScreenAnimator;
    public string TriggerSet;
    public float animationTime;

    private void Awake() {
        Box.GetComponent<SpriteRenderer>().enabled = false;
        Bottle.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(DropFulton());
    }


    IEnumerator MoveToWP(int w) {

        while(Vector2.Distance(pato.position, waypoints[w].position) >= 0.2f) {
            pato.position += Vector3.right * patoSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        pato.position = waypoints[w].position;
        SpawnSmoke();
        StartCoroutine(WaitTillEndOfAnimation());
    }

    IEnumerator DropFulton() {
        Vector3 initialFultonPos = Fulton.position;
        Vector3 targetFulton = waypoints[0].position + (Vector3.up * 2.3f);
        while (Vector2.Distance(Fulton.position, targetFulton) >= 0.2f) {
            Fulton.position -= Vector3.up * FultonSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        ReleaseBox();
        while (Vector2.Distance(Fulton.position, initialFultonPos) >= 0.2f) {
            Fulton.position += Vector3.up * FultonSpeed * 3 * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator WaitTillEndOfAnimation() {
        yield return new WaitForSeconds(animationTime);
        patoAnim.SetTrigger("Walk");
        StartCoroutine(MoveToWP(1));
    }

    public void ReleaseBox() {
        Fulton.GetComponent<SpriteRenderer>().sprite = boxlessFulton;
        Box.GetComponent<SpriteRenderer>().enabled = true;
        Bottle.GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(MoveToWP(0));
    }

    public void SpawnSmoke() {
        GameObject H = Instantiate(HumoPrefab);
        H.transform.parent = HumoPosition;
        H.transform.localScale *= 8;
        patoAnim.SetTrigger(TriggerSet);
        Destroy(Bottle);
        Destroy(Box);
        ScreenAnimator.SetTrigger("Start");
    }

}
