using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Porco : MonoBehaviour {

    public float WalkSpeed;
    public float WalkingSpeedIncrement;
    public BoxCollider2D VulnerableArea;
    public float TimeStuck;
    public float TimeHurt;
    public float TimeIdle;
    public float JumpForce;
    public int JumpCount;
    public int HP;
    public Collider2D WeaponCollider;
    public PorcoHazards Hazards;

    bool _isVulnerable;
    bool _isBlinded;
    public bool firstGo;
    bool flipped;

    SpriteRenderer sr;
    Rigidbody2D rb;
    FSM fsm;
    Move moveState;
    Hurt hurtState;
    Jump jumpState;
    Stuck stuckState;
    Idle idleState;
    Dead deadState;

    Animator anim;
    AudioSource mySource;
    public AudioSource StageSource;

    public AudioClip walkSound;
    public AudioClip trembleSound;
    public AudioClip stuckSound;
    public AudioClip landSound;
    public AudioClip hitSound;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        mySource = GetComponent<AudioSource>();

        fsm = new FSM();
        moveState = new Move(this, WalkSpeed);
        hurtState = new Hurt(this, TimeHurt);
        jumpState = new Jump(this, JumpForce, rb);
        jumpState.SetMaxJumps(JumpCount);
        stuckState = new Stuck(this, TimeStuck, VulnerableArea);
        idleState = new Idle(this, 3.0f);
        deadState = new Dead();

    }

    // Start is called before the first frame update
    void Start() {
        fsm.ChangeState(idleState);
        anim.SetTrigger("Idle");
        _isVulnerable = false;
        VulnerableArea.enabled = false;
        sr.flipX = true;
        firstGo = true;
        flipped = false;
        //FlipSprite();
    }

    // Update is called once per frame
    void Update() {
        fsm.Update();
    }

    public void GetStuck() {
        if (_isBlinded) {
            mySource.Stop();
            mySource.loop = false;
            mySource.clip = null;
            anim.ResetTrigger("Blinded");
            anim.SetTrigger("Stuck");
            mySource.PlayOneShot(stuckSound);
            fsm.ChangeState(stuckState);
            WeaponCollider.enabled = false;
            _isVulnerable = true;
        } else if (!flipped) {
            StartCoroutine(WaitTurnAround());
        }
    }

    public void Unstuck() {
        fsm.ChangeState(idleState);
        anim.SetTrigger("Idle");
        FlipSprite();
    }

    public void JumpState() {
        mySource.clip = landSound;
        mySource.loop = true;
        StageSource.Play();
        fsm.ChangeState(jumpState);
        anim.SetTrigger("Jump");
        anim.ResetTrigger("Landed");
        Hazards.StartJumpPhase();
    }

    public void Move(bool wasStuck) {
        Hazards.StopPhase();
        StartCoroutine(FadeOutSound());

        anim.ResetTrigger("Idle");
        mySource.clip = walkSound;
        mySource.loop = true;
        mySource.Play();

        if (wasStuck) {
            if (transform.localScale.x > 0) {
                rb.AddForce(new Vector2(-1, 1).normalized * 7, ForceMode2D.Impulse);
            } else {
                rb.AddForce(new Vector2(1, 1).normalized * 7, ForceMode2D.Impulse);
            }
            StartCoroutine(FlipInSeconds(0.7f));
        } else {
            if (!firstGo) {
                WalkSpeed += WalkingSpeedIncrement;
                moveState.SetMoveSpeed(WalkSpeed);
            }
        }

        fsm.ChangeState(moveState);
        anim.SetTrigger("Walking");
        StartCoroutine(WaitToEnableWeapon(1.0f));
    }

    public void HasLanded() {
        anim.SetTrigger("Landed");
    }

    public void ToggleVulnerable() {
        _isVulnerable = !_isVulnerable;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        PlayerModel p = collision.gameObject.GetComponent<PlayerModel>();
        
        if (p != null && _isVulnerable) {
            fsm.ChangeState(hurtState);
            mySource.PlayOneShot(hitSound);
            HP--;
            if (HP <= 0) {
                anim.SetTrigger("Dead");
                StartCoroutine(WaitToEndScene());
            } else {
                if (transform.localScale.x > 0) {
                    rb.AddForce(new Vector2(-1, 1).normalized * 7, ForceMode2D.Impulse);
                } else {
                    rb.AddForce(new Vector2(1, 1).normalized * 7, ForceMode2D.Impulse);
                }
                anim.SetTrigger("Hurt");
            }
        }


    }

    public void GetBlinded() {
        _isBlinded = true;
        anim.SetTrigger("Blinded");
        anim.ResetTrigger("Walking");
    }

    public void Unblind() {
        _isBlinded = false;
    }

    public void FlipSprite() {
        Vector3 newScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
        this.transform.localScale = newScale;
    }

    //IEnumerator StartBoss() {
    //    yield return new WaitForSeconds(2.0f);
    //    fsm.ChangeState(moveState);
    //    anim.SetTrigger("Walking");
    //}

    IEnumerator WaitToEnableWeapon(float t) {
        yield return new WaitForSeconds(t);
        WeaponCollider.enabled = true;
    }

    IEnumerator WaitToEndScene() {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator FlipInSeconds(float t) {
        yield return new WaitForSeconds(t);
        FlipSprite();

    }

    IEnumerator WaitTurnAround() {
        flipped = true;
        fsm.ChangeState(moveState);
        FlipSprite();
        yield return new WaitForSeconds(1.0f);
        flipped = false;
    }

    IEnumerator FadeOutSound() {
        float ticks = 0;
        while (ticks < 1) {
            ticks += Time.deltaTime;
            StageSource.volume = Mathf.Lerp(1, 0, ticks);
            yield return null;
        }
        StageSource.volume = 0;
        StageSource.Stop();
        StageSource.volume = 1;
    }
}
