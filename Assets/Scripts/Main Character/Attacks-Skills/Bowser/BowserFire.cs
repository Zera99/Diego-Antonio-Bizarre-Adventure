using UnityEngine;

public class BowserFire : MonoBehaviour {

    Vector3 dir;
    public float speed;
    public float timeToDestroy;
    public int fireDamage;

    private void Start() {
        Destroy(this.gameObject, timeToDestroy);
        if(dir.x > 0) {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Update is called once per frame
    void Update() {
        transform.position += dir * speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 d) {
        dir = d;
    }

    public void SetDamage(int value)
    {
        fireDamage = value;
    }

    //TODO: Descomentar esto si queremos que el fuego dañe a todos los enemigos basicos (No trampas/Obstaculos)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BasicEnemy>() != null)
        {
            collision.gameObject.GetComponent<BasicEnemy>().TakeDamage(fireDamage);
            Destroy(gameObject);
        } else if (collision.gameObject.GetComponent<PlayerModel>() == null) { // Si no toco el player, tiene que destruirse
            Destroy(gameObject);
        }
    }
}
