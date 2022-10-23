using UnityEngine;
public enum EnnemyShipType { Passive, Agressive, Meteor }

public class EnnemyShip : MonoBehaviour
{

    public int NumberOfMissile, SpreadOfMissile, Value;
    public Sprite Sprite;
    public EnnemyShipType EnnemyType;
    public float FireRate = 2f, SmoothSpeed = 0.125f;

    [SerializeField] int _health = 3;

    public int getBaseHealth()
    {
        return _health + Mathf.RoundToInt(PlayerData.Instance.Score / 100);
    }

}
