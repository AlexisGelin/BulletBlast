using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    public GameObject FightScreen, GarageScreen, ShopScreen;

    public NavBarButton FightScreenButton;


    [Header("Shop Screen")]
    public List<ItemShopButton> ItemShopButtons;


    [Header("Garage Screen")]
    public List<UpgradeButton> Upgrades;
    public List<ShipButton> ShipButtons;

    // Cache
    GameObject actualMenuScreen;
    NavBarButton actualButtonNav;

    public void Init()
    {
        actualMenuScreen = FightScreen;
        actualButtonNav = FightScreenButton;
    }

    public void SwitchMenuScreen(GameObject toScreen)
    {
        actualMenuScreen.SetActive(false);
        toScreen.SetActive(true);

        if (toScreen == GarageScreen) LoadGarageScreen();
        if (toScreen == ShopScreen) LoadShopScreen();

        actualMenuScreen = toScreen;

    }

    public void ChangeBtnSize(NavBarButton toBtnNav)
    {
        if (actualButtonNav == toBtnNav) return;

        RectTransform previousRt = (RectTransform)actualButtonNav.GO.transform;
        RectTransform rt = (RectTransform)toBtnNav.GO.transform;


        previousRt.DOSizeDelta(new Vector2(150, 120), .2f).SetEase(Ease.OutQuad);
        rt.DOSizeDelta(new Vector2(192, 150), .2f).SetEase(Ease.OutQuad);

        actualButtonNav.text.DOFade(0, .1f).SetEase(Ease.OutSine);
        toBtnNav.text.DOFade(1, .1f).SetEase(Ease.OutSine);

        toBtnNav.image.transform.DOMoveY(toBtnNav.image.transform.position.y + 1.5f, .2f);
        actualButtonNav.image.transform.DOMoveY(actualButtonNav.image.transform.position.y - 1.5f, .2f);

        actualButtonNav = toBtnNav;
    }

    public void LoadGarageScreen()
    {
        foreach (var upgrade in Upgrades)
        {
            upgrade.Init();
        }

        foreach( var shipButton in ShipButtons)
        {
            shipButton.Init();
        }
    }    
    
    void LoadShopScreen()
    {
        foreach (var itemShop in ItemShopButtons)
        {
            itemShop.Init();
        }
    }
}
