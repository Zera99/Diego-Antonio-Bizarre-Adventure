
using UnityEngine;

public class BowserFireSkill : ISkill {

    GameObject _bowserFirePrefab;
    Transform _fireSpawnPoint;
    int _fireDamage;

    public BowserFireSkill(GameObject prefab, PlayerModel pl, int dmg) {
        _bowserFirePrefab = prefab;
        _fireSpawnPoint = pl.fireSpawnPoint;
        _fireDamage = dmg;
    }

    public void PrepareSkill(PlayerModel pl, System.Action execute)
    {
        execute();
    }

    public void UseSkill() {
        BowserFire fire = MonoBehaviour.Instantiate(_bowserFirePrefab).GetComponent<BowserFire>();
        fire.transform.position = _fireSpawnPoint.position;
        fire.SetDirection(_fireSpawnPoint.right);
        fire.SetDamage(_fireDamage);
    }
    public void SecondSkill() {

    }
}
