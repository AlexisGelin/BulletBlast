using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    void Awake()
    {
        PoolManager.Instance.Init();

        UIManager.Instance.Init();

        PlayerData.Instance.Init();

        PlayerController.Instance.Init();


        LevelGenerator.Instance.Init();
    }

    

    void Start()
    {

    }

}
