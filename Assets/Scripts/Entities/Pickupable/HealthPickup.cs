using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IPickupable {
    private float _startingY;
    public float amplitude;
    public float sineSpeed;
    public int hpToRestore;

    public void Float(float startingY, float amp) {
        transform.position = new Vector3(transform.position.x,
                                    startingY + (Mathf.Sin(Time.time * sineSpeed) * amp), 0);
    }

    public void OnPickUp(PlayerModel p) {
        p.HealthPickUp(hpToRestore);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    private void Start() {
        _startingY = transform.position.y;
    }

    // Update is called once per frame
    private void Update() {
        Float(_startingY, amplitude);
    }
}
