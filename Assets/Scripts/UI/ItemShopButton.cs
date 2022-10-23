using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopButton : MonoBehaviour
{
    [SerializeField] TMP_Text title, price;
    [SerializeField] Image preview;

    [SerializeField] Ship _ship;

    public void Init()
    {
        title.text = _ship.Name;
        price.text = _ship.shipPrice.ToString();
        preview.sprite = _ship.Sprite;

        if (_ship.isUnlocked) GetComponent<Button>().interactable = false;
    }

    public void BuyShip()
    {
        if (PlayerData.Instance.Coin >= _ship.shipPrice)
        {
            _ship.isUnlocked = true;
            PlayerData.Instance.UpdateCoin(-_ship.shipPrice);
            GetComponent<Button>().interactable = false;
        }
    }
}
