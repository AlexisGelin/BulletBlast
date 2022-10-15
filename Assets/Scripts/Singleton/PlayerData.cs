using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoSingleton<PlayerData>
{

    [SerializeField] int _health, _score, _highScore, _coin, _tempBonusOfNumberOfMissile;

    float _moveSpeed, _maxHealth;

    public int Health { get => _health; }
    public int Score { get => _score; }
    public int HighScore { get => _highScore; }
    public int Coin { get => _coin; }
    public float MoveSpeed { get => _moveSpeed; }
    public int TempBonusOfNumberOfMissile { get => _tempBonusOfNumberOfMissile; }
    public float MaxHealth { get => _maxHealth; }

    public Ship PlayerShip;


    //Cache
    float TimeBeforeVanishSurcharge;
    bool tempBonusOfMissile = false;

    public void Init()
    {
        _health = PlayerShip.LevelHealth + 3;
        _moveSpeed = PlayerShip.MoveSpeed - (PlayerShip.LevelMoveSpeed / 80);
        _maxHealth = Health;


        PlayerController.Instance.SpriteRenderer.sprite = PlayerShip.Sprite;
    }

    private void Update()
    {
        if (tempBonusOfMissile)
        {
            TimeBeforeVanishSurcharge -= Time.deltaTime;

            //Faire afficher le Chronoou juste start un dotween qui dure 15f

            if (TimeBeforeVanishSurcharge <= 0)
            {
                _tempBonusOfNumberOfMissile = 0;
                tempBonusOfMissile = false;
            }
        }
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
        if (_health + amount > MaxHealth) return;

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
        _coin += amount;

        UIManager.Instance.PermCanvas.permScreen.UpdateCoin();
    }

    public void UpdateNumberOfMissile(int amount)
    {
        _tempBonusOfNumberOfMissile += amount;

        TimeBeforeVanishSurcharge = 15f;

        tempBonusOfMissile = true;
    }

    public int GetNumberOfMissile()
    {
        return PlayerShip.NumberOfMissile + _tempBonusOfNumberOfMissile;
    }
}
