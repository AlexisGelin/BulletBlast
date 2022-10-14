using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Base Data")]
    public Sprite Sprite;
    public string Name;
    public int NumberOfMissile, SpreadOfMissile, MissileDamage;
    public float MoveSpeed, FireRate;

    [Header("Garage")]
    public int CoeffPriceForUpgrade;
    public int LevelHealth,LevelMoveSpeed, LevelDamage, LevelFireRate;
    public int MaxLevelHealth, MaxLevelMoveSpeed, MaxLevelDamage, MaxLevelFireRate;


    [Header("Shop")]
    public bool isUnlocked = false;
    public int shipPrice;
}
