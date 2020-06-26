using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnemy : MonoBehaviour {

    CameraEnemyView _view;
    bool canAttack;
    float currentTime;

    public CameraEnemyStats stats;
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;

    public AudioSource audioSource;
    public AudioClip laserSound;
    
    // Start is called before the first frame update
    void Start() {
        _view = this.GetComponent<CameraEnemyView>();
        canAttack = true;
    }

    private void Update() {
        currentTime += Time.deltaTime;
    }

    public void DieFRQ() {
        _view.DieFRQ();
        this.gameObject.AddComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().gravityScale = 4;
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 10.0f);
        Destroy(this);
    }

    void Attack(Transform t) {
        canAttack = false;
        Vector3 dir = (t.position - this.transform.position).normalized;
        audioSource.PlayOneShot(laserSound);
        GameObject laser = Instantiate(laserPrefab);
        laser.transform.position = laserSpawnPoint.position;
        laser.transform.up = dir;
        Destroy(laser, 3.0f);
        StartCoroutine(ShootCooldown());
    }

    IEnumerator ShootCooldown() {
        yield return new WaitForSeconds(stats.shootingCoolDown);
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.GetComponent<PlayerModel>())
            _view.ChangeColor(stats.alertColor);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        PlayerModel target = collision.gameObject.GetComponent<PlayerModel>();
        if (!target) return;

        if(canAttack) {
            Attack(target.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>())
            _view.ChangeColor(stats.standByColor);
    }
}
