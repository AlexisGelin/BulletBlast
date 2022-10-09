using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    public int NumberOfLine;

    [SerializeField] int _offSetLine, _chanceToSpawn;

    [SerializeField] float _offSetTimeSpawn = .3f, _offSetPosX = 2f;

    [SerializeField] Spawner Spawner;

    //Data
    [SerializeField] float _nextSpawn = 0f, _spawnRate = 2f;

    //Cache

    public void Init()
    {
        _nextSpawn = Time.time + _spawnRate;

        int numberOfSpawner = Random.Range(NumberOfLine - _offSetLine, NumberOfLine + _offSetLine);

        float width = GetComponent<BoxCollider2D>().size.x;

        float startPos = width / 2;
        float posIncrease = width / (numberOfSpawner - 1);

        for (int i = 0; i < numberOfSpawner; i++)
        {
            _chanceToSpawn = Random.Range(0, 100);

            if (_chanceToSpawn > 30) StartCoroutine(SpawnEnnemy(startPos, posIncrease, i));
        }
    }

    IEnumerator SpawnEnnemy(float startPos, float posIncrease, int i)
    {
        float tempPos = startPos - posIncrease * i;
        float tempPosOffSet = tempPos + Random.Range(-_offSetPosX, _offSetPosX);

        Vector3 actualPos = new Vector3(tempPosOffSet, transform.position.y, 0);

        Spawner spawner = Instantiate(Spawner, transform);

        spawner.transform.position = actualPos;

        float _SpawnTime = Random.Range(-_offSetTimeSpawn, _offSetTimeSpawn);

        yield return new WaitForSeconds(_SpawnTime);

        spawner.Spawn();
    }

    void Update()
    {
        if (GameManager.Instance.gameState != GameState.PLAY) return;

        if (Time.time > _nextSpawn)
        {
            Init();
        }
    }
}
