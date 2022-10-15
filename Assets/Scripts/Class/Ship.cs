using UnityEngine;

public class Ship : MonoBehaviour, ISavable
{
    [Header("Base Data")]
    public Sprite Sprite;
    public string Name;
    public int NumberOfMissile, SpreadOfMissile, MissileDamage;
    public float MoveSpeed, FireRate;

    [Header("Garage")]
    public int CoeffPriceForUpgrade;
    public int LevelHealth, LevelMoveSpeed, LevelDamage, LevelFireRate;
    public int MaxLevelHealth, MaxLevelMoveSpeed, MaxLevelDamage, MaxLevelFireRate;

    [Header("Shop")]
    public bool isUnlocked = false;
    public int shipPrice;

    public object CaptureState()
    {
        var saveData = new ShipSaveData()
        {
            LevelHealth = LevelHealth,
            LevelMoveSpeed = LevelMoveSpeed,
            LevelDamage = LevelDamage,
            LevelFireRate = LevelFireRate,
        };

        return saveData;
    }

    public void RestoreState(object state)
    {
        var saveData = (ShipSaveData)state;

        LevelHealth = saveData.LevelHealth;
        LevelMoveSpeed = saveData.LevelMoveSpeed;
        LevelDamage = saveData.LevelDamage;
        LevelFireRate = saveData.LevelFireRate;
    }
}

[System.Serializable]
public class ShipSaveData
{
    public int LevelHealth, LevelMoveSpeed, LevelDamage, LevelFireRate;
}
