using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowserUnlock : MonoBehaviour, IPickupable {
    private float _startingY;
    public float amplitude;
    public float sineSpeed;
    public GameObject hudIconReference;
    Skin skinToAdd;

    public void Float(float startingY, float amp) {
        transform.position = new Vector3(transform.position.x,
                                    startingY + (Mathf.Sin(Time.time * sineSpeed) * amp), 0);
    }

    public void OnPickUp(PlayerModel p) {
        p.AddNewSkin(skinToAdd);
        hudIconReference.SetActive(true);
        Debug.Log("Picked up: " + this.gameObject.name);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    private void Start() {
        _startingY = transform.position.y;
        hudIconReference = GameObject.Find("BOW");
        hudIconReference.SetActive(false);
        skinToAdd = Resources.Load<Skin>("Skins/Bowser_Skin");
    }

    // Update is called once per frame
    private void Update() {
        Float(_startingY, amplitude);
    }
}
