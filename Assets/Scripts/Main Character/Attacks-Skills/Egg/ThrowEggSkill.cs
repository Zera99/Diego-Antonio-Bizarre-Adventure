using UnityEngine;
using System;

public class ThrowEggSkill : ISkill {

    GameObject _eggPrefab;
    Transform _eggSpawnPoint;
    float _throwForce;
    int _eggDamage;

    public ThrowEggSkill(GameObject prefab, PlayerModel pl, float force, int dmg)
    {
        _eggPrefab = prefab;
        _eggSpawnPoint = pl.eggSpawnPoint;
        _throwForce = force;
        _eggDamage = dmg;
    }

    public void PrepareSkill(PlayerModel pl, System.Action execute)
    {
        execute();
    }

    public void UseSkill() {
        Egg egg = MonoBehaviour.Instantiate(_eggPrefab).GetComponent<Egg>();
        egg.SetEggDamage(_eggDamage);
        //_view.PlayFartSound();
        egg.transform.position = _eggSpawnPoint.position;
        egg.GetComponent<Rigidbody2D>().AddForce(new Vector2(-_eggSpawnPoint.right.x, 1).normalized * _throwForce, ForceMode2D.Impulse);
    }
    public void SecondSkill() {

    }
}
