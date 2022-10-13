using BaseTemplate.Behaviours;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoSingleton<PlayerController>
{
    public SpriteRenderer SpriteRenderer;

    [Header("References")]
    [SerializeField] Transform _cannonTransform;
    [SerializeField] ParticleSystem _onDestroyParticle, _onHitParticle;
    [SerializeField] Collider2D _coll;



    //Data

    //Cache
    Vector3 mousePos, worldPos;
    bool _isEnter = false;
    float _maxHealth, _nextFire = 0;

    int _increaseBurstHitParticles;


    public void Init()
    {
        EnterLevelAnimation();

        _maxHealth = PlayerData.Instance.Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isEnter == false) return;

        #region PlayerFollowMouse
        mousePos = Mouse.current.position.ReadValue();
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = transform.position.z;
        #endregion

        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + PlayerData.Instance.PlayerShip.FireRate - PlayerData.Instance.PlayerShip.LevelFireRate / 10;
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (_isEnter == false) return;

        Vector3 desiredPosition = new Vector3(worldPos.x, worldPos.y, transform.position.z);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, PlayerData.Instance.MoveSpeed + PlayerData.Instance.PlayerShip.MoveSpeed / 200);
        transform.position = smoothPosition;
    }


    void Shoot()
    {
        if (PlayerData.Instance.PlayerShip.NumberOfMissile == 1)
        {
            InitBullet();

            return;
        }

        float startRotation = PlayerData.Instance.PlayerShip.SpreadOfMissile / 2;
        float angleIncrease = PlayerData.Instance.PlayerShip.SpreadOfMissile / (PlayerData.Instance.PlayerShip.NumberOfMissile - 1);

        for (int i = 0; i < PlayerData.Instance.PlayerShip.NumberOfMissile; i++)
        {
            float tempRotation = startRotation - angleIncrease * i;

            GameObject bullet = InitBullet();

            bullet.transform.rotation = Quaternion.Euler(0, 0, tempRotation);
        }
    }

    private GameObject InitBullet()
    {
        GameObject bullet = PoolManager.Instance.gameobjectPoolDictionary["Missile"].Get();

        bullet.transform.localPosition = _cannonTransform.position;

        bullet.GetComponent<Missile>().Init();

        return bullet;
    }

    void EnterLevelAnimation()
    {
        transform.DOMoveY(WorldManager.Instance._afterStartTranform.position.y, 1f).SetEase(Ease.OutSine).OnComplete(() => _isEnter = true);
    }

    public void TakeDamage(bool oneShot = false)
    {
        if (PlayerData.Instance.Health > 0)
        {
            PlayerData.Instance.UpdateHealth(-1);
        }

        UIManager.Instance.GameCanvas.GameScreen.UpdateHeart();

        if (PlayerData.Instance.Health <= 0 || oneShot)
        {
            GameManager.Instance.EndGame();

            SpriteRenderer.gameObject.SetActive(false);

            _coll.enabled = false;

            _onHitParticle.Stop();

            _onDestroyParticle.Play();

            enabled = false;
        }
        else
        {
            var actualBurst = _onHitParticle.emission.GetBurst(0);

            if (actualBurst.count.constant <= 5) _increaseBurstHitParticles = 1;

#pragma warning disable CS0618 // Le type ou le membre est obsolète
            if (PlayerData.Instance.Health > _maxHealth / 3)
            {
                _onHitParticle.startColor = ColorManager.Instance.LightGrey;

                if (actualBurst.count.constant <= 30) _increaseBurstHitParticles = 10;

            }
            else
            {
                _onHitParticle.startColor = ColorManager.Instance.White;

                if (actualBurst.count.constant <= 50) _increaseBurstHitParticles = 20;


            }
#pragma warning restore CS0618 // Le type ou le membre est obsolète

            var newBurst = new ParticleSystem.Burst(actualBurst.time, actualBurst.count.constant + _increaseBurstHitParticles);
            _onHitParticle.emission.SetBurst(0, newBurst);

            if (_onHitParticle.isPlaying == false) _onHitParticle.Play();
        }


    }
}
