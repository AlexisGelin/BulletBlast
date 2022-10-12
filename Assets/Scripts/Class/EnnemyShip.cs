using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyShip : MonoBehaviour
{
    public int NumberOfMissile, SpreadOfMissile, Value;
    public Sprite Sprite;
    public float Health = 3;
    public bool isAggresive = false;
    public float NextFire = 0f, FireRate = 2f, SmoothSpeed = 0.125f;

}