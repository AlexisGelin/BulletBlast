using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyShip : MonoBehaviour
{
    public int NumberOfMissile, SpreadOfMissile, Value;
    public Sprite Sprite;
    public int _health = 3;
    public bool isAggresive = false, isMeteor = false;
    public float NextFire = 0f, FireRate = 2f, SmoothSpeed = 0.125f;

    public int getBaseHealth()
    {
        return _health + Mathf.RoundToInt(PlayerData.Instance.Score / 100);
    }

}
