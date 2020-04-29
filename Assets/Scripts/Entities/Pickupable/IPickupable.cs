using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable {

    void OnPickUp(PlayerModel p);
    void Float(float startingY, float amp);

}
