using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TMP_Text _score, _highScore, _ennemyKilled, _coinCollected;

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
        _highScore.text = "HighScore : " + playerData.HighScore;
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
