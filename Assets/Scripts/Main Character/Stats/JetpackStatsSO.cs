using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skins/Stats/Jetpack Stats")]
public class JetpackStatsSO : ScriptableObject
{
    public float upSpeed = 3;
    public float topSpeed = 10;
    public float maxPoints = 100;
    public float rechargePointsPerSecond = 0.2f;
    public float decayPointsPerSecond = 0.2f;
}
