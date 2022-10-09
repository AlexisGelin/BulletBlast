using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoSingleton<LevelGenerator>
{
    public List<Ennemy> EnnemyList;
    
    [SerializeField] Spawners _spawners;

    public void Init()
    {
        _spawners.Init();
    }
}
