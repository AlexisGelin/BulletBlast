using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    //Data
    public int NumberOfMissile, SpreadOfMissile,MissileDamage;
    public Sprite Sprite;

    public int CoeffPrice;

    public int LevelHealth, LevelMoveSpeed, LevelDamage, LevelFireRate = 0;
    public int MaxLevelHealth, MaxLevelMoveSpeed, MaxLevelDamage, MaxLevelFireRate = 3;
    public float MoveSpeed, FireRate;
}
