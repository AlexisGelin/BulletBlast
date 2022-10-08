using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    void Awake()
    {
        PoolManager.Instance.Init();

        UIManager.Instance.Init();

        LevelGenerator.Instance.Init();

        Time.timeScale = 1;

    }

    public void ReloadScene()
    {
        SaveGame();

        SceneManager.LoadScene("MainScene");
    }

    public void StartGame()
    {
        UIManager.Instance.StartGame();

        PlayerController.Instance.Init();

        PlayerData.Instance.Init();
    }

    public void EndGame()
    {
        //UIManager.Instance.EndGame();

        Time.timeScale = 0;
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
