using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : MonoBehaviour {

    public int HP;
    public int ClawDamage;
    public GameObject FastMissilePrefab;
    public GameObject SlowMissilePrefab;
    public Transform fastMissileSpawn;
    public Transform slowMissileSpawn;
    public float MoveSpeed;
    public float MissileMoveSpeed;
    public List<PhaseBase> allPhases;

    PhaseBase currentPhase;
    int currentPhaseIndex;

    private void Awake() {
        currentPhaseIndex = 0;
        GoToNextPhase();
    }

    private void Update() {
        currentPhase.OnUpdate();
    }

    public void GoToNextPhase() {
        currentPhaseIndex++;
        currentPhase = allPhases[currentPhaseIndex];
    }

    public void FinishHurt() {
        currentPhase.FinishHurt();
    }

    public void ClawAttack() {
        // Feedback de Claw
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
}
