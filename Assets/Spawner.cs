using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void Spawn()
    {
        Ennemy ennemy = Instantiate(LevelGenerator.Instance.EnnemyList[Random.Range(0, LevelGenerator.Instance.EnnemyList.Count)],transform.position,Quaternion.identity,transform.parent);

        ennemy.Init();

        Destroy(gameObject);
    }

}
