using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipButton : MonoBehaviour
{
    [SerializeField] Ship _ship;
    [SerializeField] TMP_Text _shipName;
    [SerializeField] Image _shipImage;


    public void Init()
    {
        if (_ship.isUnlocked == false) GetComponent<Button>().interactable = false;
        else GetComponent<Button>().interactable = true;

        _shipName.text = _ship.Name;
        _shipImage.sprite = _ship.Sprite;
    }

    public void EquipShip()
    {
        PlayerData.Instance.PlayerShip = _ship;
        PlayerData.Instance.Init();
        UIManager.Instance.MenuCanvas.LoadGarageScreen();
    }
}
