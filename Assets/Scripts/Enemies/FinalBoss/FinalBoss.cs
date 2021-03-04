using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : MonoBehaviour {

    public int HP;
    public float WaitTimeOnHurt;
    public int ClawDamage;
    public GameObject MyClaw;
    public GameObject FastMissilePrefab;
    public GameObject endingEffect;
    public GameObject Vultur;
    public Transform fastMissileSpawn;
    public Transform slowMissileSpawn;
    public List<ElectricArea> ElectricAreas;
    public float TimeUntilShock;
    bool flipped;
    public float MoveSpeed;
    public float MissileMoveSpeed;
    public float ElectricMoveSpeed;
    public List<PhaseBase> allPhases;

    PhaseBase currentPhase;
    int currentPhaseIndex;
    Animator anim;
    AudioSource Source;
    public AudioClip HitClip;
    public AudioClip ClawAttack;
    public AudioClip ElectricAttack;

    private void Awake() {
        anim = GetComponent<Animator>();
        flipped = false;
        currentPhaseIndex = 0;
        currentPhase = allPhases[currentPhaseIndex];
        //GoToNextPhase();
    }

    private void Update() {
        currentPhase.OnUpdate();
        if (Input.GetKeyDown(KeyCode.X)) {
            Die();
        }
    }

    public void GoToNextPhase() {
        anim.ResetTrigger("Attack");
        anim.ResetTrigger("Hurt");
        anim.SetTrigger("Transition");
        currentPhaseIndex++;
        currentPhase = allPhases[currentPhaseIndex];

        if (currentPhaseIndex == 1) {
            Destroy(MyClaw);
        }

        if (currentPhaseIndex == 2)
            EnableShockers();

    }

    public void FinishHurt() {
        anim.ResetTrigger("Hurt");
        currentPhase.FinishHurt();
    }

    public void ClawSound() {
        Source.PlayOneShot(ClawAttack);
    }

    public void ExecuteElectricAttack() {
        StartCoroutine(ElectricDischarge());
    }

    public void EnableShockers() {
        foreach (ElectricArea s in ElectricAreas) {
            Source.PlayOneShot(ElectricAttack);

            s.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void TakeDamage(int d) {
        HP -= d;
        anim.SetTrigger("Hurt");
        Source.PlayOneShot(HitClip);
        if (HP == 7 || HP == 3) {
            GoToNextPhase();
            StartCoroutine(ResetTransition());
        }

        if (HP == 0) {
            Die();
        }
    }

    void Die() {
        GameObject o = Instantiate(endingEffect);
        GameObject v = Instantiate(Vultur);
        o.transform.parent = this.transform;
        v.transform.position = this.transform.position;
        o.transform.localScale = new Vector3(5, 5, 5);
        Destroy(this.gameObject, 0.5f);
    }

    public void FlipDir() {
        currentPhase.ChangeDir();
        FlipSprite();
    }

    public void Attack() {
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttack());
    }



    public void FlipSprite() {
        Vector3 newScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
        this.transform.localScale = newScale;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall" && !flipped) {
            FlipDir();
            flipped = true;
            StartCoroutine(ResetFlip());
        }
    }

    IEnumerator ElectricDischarge() {
        yield return new WaitForSeconds(1.0f);
        foreach (ElectricArea e in ElectricAreas) {
            e.Activate();
        }
    }

    IEnumerator ResetFlip() {
        yield return new WaitForSeconds(1.5f);
        flipped = false;
    }

    IEnumerator ResetAttack() {
        yield return new WaitForSeconds(1.0f);
        anim.ResetTrigger("Attack");
    }

    IEnumerator ResetTransition() {
        yield return new WaitForSeconds(1.5f);
        anim.ResetTrigger("Transition");
    }


}
