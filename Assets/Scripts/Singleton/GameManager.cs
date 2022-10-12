using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { MENU, PLAY, END }

public class GameManager : MonoSingleton<GameManager>
{
    public GameState gameState;

    void Awake()
    {
        gameState = GameState.MENU;

        PoolManager.Instance.Init();

        UIManager.Instance.Init();

        Time.timeScale = 1;
    }

    public void ReloadScene()
    {
        SaveGame();

        SceneManager.LoadScene("MainScene");
    }

    public void StartGame()
    {
        gameState = GameState.PLAY;

        UIManager.Instance.StartGame();

        LevelGenerator.Instance.Init();

        PlayerController.Instance.Init();

        PlayerData.Instance.Init();
    }

    public void EndGame()
    {
        gameState = GameState.END;

        UIManager.Instance.GameCanvas.EndGame();

    }

    public void SaveGame()
    {
        SavingSystem.i.Save("BulletBlastSave");
    }

    public void LoadGame()
    {
        SavingSystem.i.Load("BulletBlastSave");
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

}
