using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] BoxCollider2D _coll;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] ParticleSystem _onDestroyParticle, _onHitParticle;
    [SerializeField] Transform _cannonTransform;
    [SerializeField] EnnemyShip ennemyShipData;

    [Space(10)]

    //Data
    [SerializeField] int _recycleEnnemyY;

    //Cache
    bool _isReady, _isRecycle = false;
    float _maxHealth;
    int _increaseBurstHitParticles;

    public void Init()
    {
        transform.DOMoveY(WorldManager.Instance._afterSpawnEnnemyPosTransform.position.y, 0.3f).OnComplete(() => _isReady = true);

        _spriteRenderer.sprite = ennemyShipData.Sprite;

        if (ennemyShipData.isMeteor)
        {
            int size = Random.Range(1, 5);
            transform.localScale = new Vector3(size,size,size);
            GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-90, 90);
        }
        _maxHealth = ennemyShipData.Health;
    }

    void Update()
    {
        if (ennemyShipData.Health <= 0) return;

        if (transform.position.y <= _recycleEnnemyY && _isRecycle == false)
        {
            _isRecycle = true;

            Destroy(gameObject);
        }

        if (ennemyShipData.isAggresive)
        {
            if (Time.time > ennemyShipData.NextFire)
            {
                ennemyShipData.NextFire = Time.time + ennemyShipData.FireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {

        if (ennemyShipData.NumberOfMissile == 1)
        {
            InitBullet();

            return;
        }

        float startRotation = ennemyShipData.SpreadOfMissile / 2;
        float angleIncrease = ennemyShipData.SpreadOfMissile / (ennemyShipData.NumberOfMissile - 1);

        for (int i = 0; i < ennemyShipData.NumberOfMissile; i++)
        {
            float tempRotation = startRotation - angleIncrease * i;

            GameObject bullet = InitBullet();

            bullet.transform.rotation = Quaternion.Euler(0, 0, tempRotation);
        }
    }

    private GameObject InitBullet()
    {
        GameObject bullet = PoolManager.Instance.gameobjectPoolDictionary["EnnemyMissile"].Get();

        bullet.transform.localPosition = _cannonTransform.position;

        bullet.GetComponent<Missile>().Init();

        return bullet;
    }



    void FixedUpdate()
    {
        if (_isReady == false || ennemyShipData.Health <= 0) return;

        transform.position -= new Vector3(0, .1f, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (ennemyShipData.Health <= 0) return;

        if (collision.tag == "PlayerBullet")
        {
            PlayerData.Instance.UpdateScore(ennemyShipData.Value);

            StartCoroutine(collision.gameObject.GetComponent<Missile>().RecycleBullet());

            TakeDamage(PlayerData.Instance.PlayerShip.MissileDamage + PlayerData.Instance.PlayerShip.LevelDamage / 2);
        }

        if (collision.tag == "Player")
        {

            if (ennemyShipData.isMeteor) PlayerController.Instance.TakeDamage(true);
            else PlayerController.Instance.TakeDamage();

            Destroy(gameObject);
        }
    }


    public void TakeDamage(float amount)
    {

        if (ennemyShipData.Health > 0)
        {
            ennemyShipData.Health -= amount;
        }

        if (ennemyShipData.Health > 0)
        {
            if (ennemyShipData.isMeteor) return;

            var actualBurst = _onHitParticle.emission.GetBurst(0);

            if (actualBurst.count.constant <= 5) _increaseBurstHitParticles = 1;

#pragma warning disable CS0618 // Le type ou le membre est obsolète
            if (ennemyShipData.Health > _maxHealth / 3)
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
        else
        {

            _spriteRenderer.gameObject.SetActive(false);

            _coll.enabled = false;

            _onHitParticle.Stop();

            _onDestroyParticle.Play();

            Destroy(gameObject, _onDestroyParticle.main.duration);

            if (ennemyShipData.isMeteor) return;

            PlayerData.Instance.EnnemyKilledInRun++;

            GameObject Coll = Instantiate(CollectibleManager.Instance.GetRandomCollectible().gameObject, transform);

            Coll.transform.parent = WorldManager.Instance.transform;

            Coll.GetComponent<Collectible>().Init();
        }
    }
}
