using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

    public int damage;

    public void SetEggDamage(int dmg) {
        damage = dmg;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(!collision.gameObject.GetComponent<PlayerModel>() || !collision.gameObject.GetComponent<Egg>())
            Destroy(this.gameObject);
    }
}
