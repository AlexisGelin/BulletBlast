using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipButton : MonoBehaviour
{
    [SerializeField] Ship _ship;

    public void Init()
    {

        if (_ship.isUnlocked == false) GetComponent<Button>().interactable = false;
        else GetComponent<Button>().interactable = true;
    }

    public void EquipShip()
    {
        PlayerData.Instance.PlayerShip = _ship;
        PlayerData.Instance.Init();
        UIManager.Instance.MenuCanvas.LoadGarageScreen();
    }
}
