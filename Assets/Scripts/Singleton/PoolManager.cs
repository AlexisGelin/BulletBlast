using BaseTemplate.Behaviours;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField] List<Pool> gameobjectPools;


    public Dictionary<string, ObjectPool<GameObject>> gameobjectPoolDictionary;

    public Stack<GameObject> pools = new Stack<GameObject>();

    public void Init()
    {
        gameobjectPoolDictionary = new Dictionary<string, ObjectPool<GameObject>>();

        foreach (var pool in gameobjectPools)
        {
            var _pool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(pool.prefab, transform);
            }, poolObj =>
            {
                poolObj.SetActive(true);
            },
            poolObj =>
            {
                poolObj.transform.parent = transform;
                poolObj.SetActive(false);
            },
            poolObj =>
            {
                Destroy(poolObj);
            },
            false, pool.size, pool.size);

            for (int i = 0; i < pool.size; i++)
            {
                pools.Push(_pool.Get());
            }

            for (int i = 0; i < pool.size; i++)
            {
                _pool.Release(pools.Pop());
            }

            pools.Clear();

            gameobjectPoolDictionary.Add(pool.tag, _pool);
        }
    }
}



[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}
