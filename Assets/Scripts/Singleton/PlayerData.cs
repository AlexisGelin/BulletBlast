using BaseTemplate.Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoSingleton<PlayerData>, ISavable
{

    [SerializeField] int _health, _score, _highScore, _coin, _collectedCoinInRun;

    int _tempBonusOfNumberOfMissile;

    float _moveSpeed, _maxHealth;

    public int Health { get => _health; }
    public int Score { get => _score; }
    public int HighScore { get => _highScore; }
    public int Coin { get => _coin; }
    public float MoveSpeed { get => _moveSpeed; }
    public int TempBonusOfNumberOfMissile { get => _tempBonusOfNumberOfMissile; }
    public float MaxHealth { get => _maxHealth; }
    public int CollectedCoinInRun { get => _collectedCoinInRun; }

    public int EnnemyKilledInRun;
    public float TimeToVanishSurcharge = 10f;

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

        PlayerController.Instance.UpdateHitParticle();
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
        _collectedCoinInRun += amount;

        UIManager.Instance.PermCanvas.permScreen.UpdateCoin();
    }

    public void UpdateNumberOfMissile(int amount)
    {
        _tempBonusOfNumberOfMissile += amount;

        TimeBeforeVanishSurcharge = TimeToVanishSurcharge;

        UIManager.Instance.GameCanvas.GameScreen.InitSurchargeBar();

        tempBonusOfMissile = true;
    }

    public int GetNumberOfMissile()
    {
        if (PlayerShip.NumberOfMissile + _tempBonusOfNumberOfMissile >= 12) return 12;
        else return PlayerShip.NumberOfMissile + _tempBonusOfNumberOfMissile;
    }

    public object CaptureState()
    {
        var saveData = new PlayerDataSave()
        {
            HighScore = _highScore,
            Coin = _coin,
        };

        return saveData;
    }

    public void RestoreState(object state)
    {
        var saveData = (PlayerDataSave)state;

        _highScore = saveData.HighScore;
        _coin = saveData.Coin;

    }
}


[Serializable]
public class PlayerDataSave
{
    public int HighScore, Coin;
}
