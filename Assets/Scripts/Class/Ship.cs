using UnityEngine;

public class Ship : MonoBehaviour, ISavable
{
    [Header("Base Data")]
    public int ID;
    public Sprite Sprite;
    public string Name;
    public int NumberOfMissile, MaxBullet, SpreadOfMissile, MissileDamage;
    public float MoveSpeed, FireRate;

    [Header("Garage")]
    public int CoeffPriceForUpgrade;
    public int LevelHealth, LevelMaxBullet, LevelDamage, LevelFireRate;
    public int MaxLevelHealth, MaxLevelMaxBullet, MaxLevelDamage, MaxLevelFireRate;

    [Header("Shop")]
    public bool isUnlocked = false;
    public int shipPrice;

    public object CaptureState()
    {
        var saveData = new ShipSaveData()
        {
            LevelHealth = LevelHealth,
            LevelMaxBullet = LevelMaxBullet,
            LevelDamage = LevelDamage,
            LevelFireRate = LevelFireRate,

            IsUnlocked = isUnlocked,
        };

        return saveData;
    }

    public void RestoreState(object state)
    {
        var saveData = (ShipSaveData)state;

        LevelHealth = saveData.LevelHealth;
        LevelMaxBullet = saveData.LevelMaxBullet;
        LevelDamage = saveData.LevelDamage;
        LevelFireRate = saveData.LevelFireRate;

        isUnlocked = saveData.IsUnlocked;
    }
}

[System.Serializable]
public class ShipSaveData
{
    public int LevelHealth, LevelMaxBullet, LevelDamage, LevelFireRate;
    public bool IsUnlocked;

}
