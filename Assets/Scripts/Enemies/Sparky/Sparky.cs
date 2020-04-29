using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparky : MonoBehaviour {
    public SparkyComponent top;
    public SparkyComponent bottom;
    public BoxCollider2D rasho;
    public SpriteRenderer rashoVisual;
    public int attackTimer;
    public int coolDown;
    bool _isInCD;
    bool _isTriggered;
    Animator _anim;

    public AudioSource audioSource;
    public AudioClip shockSound;

    // Start is called before the first frame update
    void Start() {
        top.DeactivateComponent();
        bottom.DeactivateComponent();
        _isInCD = false;
        _isTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>() == null) return;
        if(!_isTriggered)
            Trigger();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!_isInCD)
            Trigger();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        _isTriggered = false;
        StopAllCoroutines();
        top.DeactivateComponent();
        bottom.DeactivateComponent();
        rasho.enabled = false;
        rashoVisual.enabled = false;
    }

    void Trigger() {
        _isTriggered = true;
        _isInCD = true;
        top.InProximity(attackTimer);
        bottom.InProximity(attackTimer);
        StartCoroutine(Attack());
    }

    IEnumerator Attack() {
        if (_isTriggered) StopCoroutine(Attack());

        yield return new WaitForSeconds(attackTimer * 0.3f);
        audioSource.PlayOneShot(shockSound);
        rasho.enabled = true;
        rashoVisual.enabled = true;
        yield return new WaitForSeconds(0.1f);
        top.DeactivateComponent();
        bottom.DeactivateComponent();
        rasho.enabled = false;
        rashoVisual.enabled = false;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown() {
        yield return new WaitForSeconds(coolDown);
        _isInCD = false;
    }
}
