using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class FusRohSkill : ISkill {
    Transform _spawnPoint;
    float _baseCone, _lengthCone, _edgeCone, _costPoints;
    Skin _thisSkin;
    PlayerView view;

    public FusRohSkill(PlayerModel pl, float baseCone, float lengthCone, float edgeCone, float costPoints, Skin thisSkin) {
        _spawnPoint = pl.fusRohSpawnPoint;
        _baseCone = baseCone;
        _lengthCone = lengthCone;
        _edgeCone = edgeCone;
        _costPoints = costPoints;

        _thisSkin = thisSkin;

        view = pl.gameObject.GetComponent<PlayerView>();
    }

    public void PrepareSkill(PlayerModel pl, Action execute) {
        if (pl.currentPointsFRC >= _costPoints) {
            pl.currentPointsFRC -= _costPoints;
            execute();
            pl.ChangePointsValue(_thisSkin, pl.currentPointsFRC);
            view.PlayFusRohSound();
        }
    }

    public void UseSkill() {
        EventsManager.TriggerEvent(Constants.EVENT_SHAKECAMERA, 0.5f);
        var sad = GameObject.FindObjectsOfType<MonoBehaviour>().Select(x => x.GetComponent<IFusRohCuack>()).ToList();
        Debug.Log("FusRo Enter");
        Collider2D[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<Collider2D>()).ToArray();
        Debug.Log("Enemies: " + allEnemies.Length.ToString());
        List<GameObject> enemiesToDestroy = new List<GameObject>();
        Debug.Log("To Destroy: " + enemiesToDestroy.Count.ToString());
        Camera cam = Camera.main;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);

        for (int i = 0; i < allEnemies.Length; i++) {
            Debug.Log("Name: " + allEnemies[i].name);
            if (GeometryUtility.TestPlanesAABB(planes, allEnemies[i].bounds)) { // Chequea si el collider esta dentro de los planos de vision de la camara.
                enemiesToDestroy.Add(allEnemies[i].gameObject);
            }
        }

        foreach (GameObject e in enemiesToDestroy) {
            if (e.GetComponent<Shell>() != null)
                e.GetComponent<Shell>().DieFRQ();
            else if(e.GetComponent<CameraEnemy>() != null) {
                e.GetComponent<CameraEnemy>().DieFRQ();
            } else {
                e.GetComponent<BasicEnemy>().DieFRQ();
            }
        }
        enemiesToDestroy.Clear();
    }

    public void SecondSkill() {
    }

}