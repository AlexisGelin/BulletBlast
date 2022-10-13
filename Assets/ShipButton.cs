using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipButton : MonoBehaviour
{
    [SerializeField] Ship _ship;

    public void EquipShip()
    {
        PlayerData.Instance.PlayerShip = _ship;
        PlayerData.Instance.Init();
        UIManager.Instance.MenuCanvas.LoadGarageScreen();
    }
}
