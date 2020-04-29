using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerModel : BasicEnemy {

    public float speed;
    public int HP;
    public Transform collisionCheck;

    Vector2 dir;
    bool _isFlipped;

    RoamerView _view;

    // Start is called before the first frame update
    void Start() {
        dir = new Vector2(1, 0);
        _view = this.GetComponent<RoamerView>();
    }

    // Update is called once per frame
    void Update() {
        if (HP <= 0)
            return;

        Move();
    }

    void Move() {
        if (HP <= 0)
            return;

        transform.position += (Vector3)dir * speed * Time.deltaTime;

    }

    private bool CollisionCheck(Vector2 dir) {
        RaycastHit2D result = Physics2D.Raycast(collisionCheck.position, dir, Mathf.Abs(dir.x * 0.3f));
        if (result)
            return result.transform.gameObject != this.gameObject;
        else
            return true;
    }

    public override void MakeCollisionDamage(PlayerModel player)
    {
        base.MakeCollisionDamage(player);
        _view.Attack();
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        Egg egg = collision.gameObject.GetComponent<Egg>();

        if (egg) {
            TakeDamage(egg.damage);
            return;
        }

        dir *= -1;
        _view.SpriteFlip(!_isFlipped);
        _isFlipped = !_isFlipped;

        //if (collision.gameObject.GetComponent<PlayerModel>()) {
        //    _view.Attack();
        //    collision.gameObject.GetComponent<PlayerModel>().TakeDamage(2);
        //}

    }

    public override void Die() {
        _view.Die();
        Destroy(this.gameObject, 1f);
    }

    public override void TakeDamage(int damage) {
        HP -= damage;
        //_rb.AddForce(new Vector2(1, 1).normalized * stats.damageForce, ForceMode2D.Impulse);
        _view.TakeDamage();
        if (HP <= 0) {
            Die();
        }
    }

}
