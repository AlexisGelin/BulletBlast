using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField] TMP_Text _score, _highScore;
    [SerializeField] List<Image> _hearts;

    PlayerData playerData;

    public void Init()
    {
        playerData = PlayerData.Instance;

        UpdateHeart();
        UpdateHighScore();
        UpdateScore();
    }

    public void UpdateHeart()
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            if (i < playerData.Health)
            {
                _hearts[i].gameObject.SetActive(true);
            }
            else
            {
                _hearts[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateScore()
    {
        _score.text = playerData.Score.ToString();
    }

    public void UpdateHighScore()
    {
        _highScore.text = playerData.HighScore.ToString();
    }


}
