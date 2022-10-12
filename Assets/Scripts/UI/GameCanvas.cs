using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public GameScreen GameScreen;
    public EndScreen EndScreen;

    public void Init()
    {
        GameScreen.Init();
    }

    public void EndGame()
    {
        UIManager.Instance.PermCanvas.gameObject.SetActive(false);
        GameScreen.gameObject.SetActive(false);
        EndScreen.gameObject.SetActive(true);
        EndScreen.Init();
    }
}
