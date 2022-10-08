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

    //[Header("Fight Screen")]



    //[Header("Garage Screen")]




    // Cache
    GameObject actualMenuScreen;
    NavBarButton actualButtonNav;

    public void Init()
    {
        actualMenuScreen = FightScreen;
        actualButtonNav = FightScreenButton;

        //topNavBar.UpdateNavBar();

        //LoadFightScreen();
    }

    public void SwitchMenuScreen(GameObject toScreen)
    {
        actualMenuScreen.SetActive(false);
        toScreen.SetActive(true);

/*        if (toScreen == ShopScreen) LoadShopScreen();
        if (toScreen == ShopScreen) LoadGarageScreen();
        if (toScreen == ShopScreen) LoadFightScreen();*/

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

        toBtnNav.image.transform.DOMoveY(toBtnNav.image.transform.position.y + 50, .2f);
        actualButtonNav.image.transform.DOMoveY(actualButtonNav.image.transform.position.y - 50, .2f);

        actualButtonNav = toBtnNav;
    }
}
