using System.Collections;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [SerializeField] int NumberOfLine;
    [SerializeField] float SpawnTime;

    [SerializeField] int _offSetLine, _chanceToSpawn;

    [SerializeField] float _offSetTimeSpawn = .3f, _offSetPosX = 2f;

    public void Init()
    {
        int numberOfSpawner = Random.Range(GetNumberOfLine() - _offSetLine, GetNumberOfLine() + _offSetLine);

        float width = GetComponent<BoxCollider2D>().size.x;

        float startPos = width / 2;
        float posIncrease = width / (numberOfSpawner - 1);

        for (int i = 0; i < numberOfSpawner; i++)
        {
            StartCoroutine(SpawnEnnemy(startPos, posIncrease, i));
        }
    }

    IEnumerator SpawnEnnemy(float startPos, float posIncrease, int i)
    {
        float tempPos = startPos - posIncrease * i;
        float tempPosOffSet = tempPos + Random.Range(-_offSetPosX, _offSetPosX);

        Vector3 actualPos = new Vector3(tempPosOffSet, transform.position.y, 0);

        float _SpawnTime = Random.Range(GetSpawnTime() - _offSetTimeSpawn, GetSpawnTime() + _offSetTimeSpawn);
        yield return new WaitForSeconds(_SpawnTime);

        Ennemy ennemy = Instantiate(LevelGenerator.Instance.GetEnnemy(), actualPos, Quaternion.identity, transform);

        ennemy.Init();
    }


    int GetNumberOfLine()
    {
        return NumberOfLine + Mathf.RoundToInt(PlayerData.Instance.Score / 100) + Mathf.RoundToInt(PlayerData.Instance.TempBonusOfNumberOfMissile);
    }

    float GetSpawnTime()
    {
        float spawnTime = SpawnTime - (PlayerData.Instance.Score / 1000) - (PlayerData.Instance.TempBonusOfNumberOfMissile / 10);
        if (spawnTime < 0.3f) spawnTime = 0.3f;
        return spawnTime;
    }
}
