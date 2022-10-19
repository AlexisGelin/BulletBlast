using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TMP_Text _score, _ennemyKilled, _coinCollected;

    PlayerData playerData;

    public void Init()
    {
        playerData = PlayerData.Instance;

        UpdateScore();
        UpdateEnnemyKilled();
        UpdateCoinCollected();
    }

    public void UpdateScore()
    {
        _score.text = playerData.Score.ToString();
    }

    public void UpdateEnnemyKilled()
    {
        _ennemyKilled.text = playerData.EnnemyKilledInRun.ToString();
    }

    public void UpdateCoinCollected()
    {
        _coinCollected.text = playerData.CollectedCoinInRun.ToString();
    }
}
