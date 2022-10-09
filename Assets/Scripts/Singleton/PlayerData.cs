using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoSingleton<PlayerData>
{

    [SerializeField] int _health = 3, _score, _highScore, _coin;

    public int Health { get => _health; }
    public int Score { get => _score; }
    public int HighScore { get => _highScore; }
    public int Coin { get => _coin; }

    public void Init()
    {

    }

    public void UpdateScore(int amount)
    {
        _score += amount;

        UIManager.Instance.GameCanvas.GameScreen.UpdateScore();

        if (_score >= _highScore)
        {
            _highScore = _score;
            UpdateHighScore();
        }
    }

    public void UpdateHealth(int amount)
    {
        _health += amount;

        UIManager.Instance.GameCanvas.GameScreen.UpdateHeart();
    }

    public void UpdateHighScore(int amount = 0)
    {
        _highScore += amount;

        UIManager.Instance.GameCanvas.GameScreen.UpdateHighScore();

    }

    public void UpdateCoin(int amount)
    {
        _highScore += amount;

        UIManager.Instance.PermCanvas.permScreen.UpdateCoin();
    }
}
