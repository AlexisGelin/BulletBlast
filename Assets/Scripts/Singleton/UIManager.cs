using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] CanvasGroup MenuCanvasGroup, GameCanvasGroup;

    [SerializeField] GameCanvas GameCanvas;
    [SerializeField] MenuCanvas MenuCanvas;

    //Cache
    CanvasGroup actualCanvasGroup;
    public void Init()
    {
        actualCanvasGroup = MenuCanvasGroup;
        MenuCanvas.Init();
    }

    public void SwitchToCanvas(CanvasGroup toCanvas)
    {
        if (actualCanvasGroup == toCanvas) return;
        if (toCanvas == GameCanvasGroup) GameCanvas.Init();
        if (toCanvas == MenuCanvasGroup) MenuCanvas.Init();


        actualCanvasGroup.interactable = false;
        actualCanvasGroup.blocksRaycasts = false;
        actualCanvasGroup.alpha = 0;

        toCanvas.interactable = true;
        toCanvas.blocksRaycasts = true;
        toCanvas.alpha = 1;

        actualCanvasGroup = toCanvas;
    }

    public void StartGame()
    {
        SwitchToCanvas(GameCanvasGroup);
    }
}
