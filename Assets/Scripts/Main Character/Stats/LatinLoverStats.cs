using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skins/Stats/Latin Lover Stats")]
public class LatinLoverStats : ScriptableObject {
    public RuntimeAnimatorController currentAnimator;

    public int maxHP;
    public int hp;
    public int maxLives;
    public int lives;
    public float jumpHeight;
    public float timeToApex;
    public float fallingSpeed;
    public int maxJumps;
    public float walkingSpeed;
    public float runningSpeed;
    public float climbingSpeed;
    public float throwForce;
    public int eggDamage;
    public float damageForce;

}
