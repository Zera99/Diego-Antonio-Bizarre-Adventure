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
    public GameObject SlowMissilePrefab;
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

    private void Awake() {
        flipped = false;
        currentPhaseIndex = -1;
        GoToNextPhase();
    }

    private void Update() {
        currentPhase.OnUpdate();
    }

    public void GoToNextPhase() {
        currentPhaseIndex++;
        currentPhase = allPhases[currentPhaseIndex];

        if(currentPhaseIndex == 1) {

            Destroy(MyClaw);
        }

        if (currentPhaseIndex == 2)
            EnableShockers();
    }

    public void FinishHurt() {
        currentPhase.FinishHurt();
    }

    public void ClawAttack() {
        // Feedback de Claw
    }

    public void PrepareElectricAttack() {
        //Animacion de charge up
        // cuando termina
        ExecuteElectricAttack();
    }

    public void ExecuteElectricAttack() {
        foreach(ElectricArea e in ElectricAreas) {
            e.Activate();
        }
    }

    public void EnableShockers() {
        foreach(ElectricArea s in ElectricAreas) {
            s.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void TakeDamage(int d) {
        HP -= d;
        if (HP == 7 || HP == 3)
            GoToNextPhase();
           
        if(HP == 0) {
            // Die
            StartCoroutine(WaitToEndScene());
            Destroy(this.gameObject);
        }
    }

    public void FlipDir() {
        currentPhase.ChangeDir();
        FlipSprite();
    }

    IEnumerator WaitToEndScene() {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    IEnumerator ResetFlip() {
        yield return new WaitForSeconds(1.5f);
        flipped = false;
    }


}
