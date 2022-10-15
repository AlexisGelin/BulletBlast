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

       LoadGame();

        PoolManager.Instance.Init();

        UIManager.Instance.Init();

        WorldManager.Instance.Init();

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

        LevelGenerator.Instance.Init();

        PlayerController.Instance.Init();

        PlayerData.Instance.Init();

        UIManager.Instance.StartGame();
    }

    public void EndGame()
    {
        gameState = GameState.END;

        UIManager.Instance.GameCanvas.EndGame();

    }

    public void SaveGame()
    {
        SavingSystem.i.Save("BBSave");
    }

    public void LoadGame()
    {
        SavingSystem.i.Load("BBSave");
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

}
