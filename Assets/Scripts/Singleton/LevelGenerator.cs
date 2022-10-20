using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoSingleton<LevelGenerator>
{
    public List<Ennemy> EnnemyList;

    [SerializeField] Spawners _spawners;

    //Cache
    Ennemy ennemy;

    public void Init()
    {
        _spawners.Init();
    }

    public Ennemy GetEnnemy()
    {
        int chance = Random.Range(0, 100);

        if (chance <= 30) ennemy = EnnemyList[0];
        else if (chance <= 50) ennemy = EnnemyList[1];
        else ennemy = EnnemyList[2];

        return ennemy;
    }
}
