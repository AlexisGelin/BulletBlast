using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject _renderer;
    [SerializeField] BoxCollider2D _coll;
    [SerializeField] ParticleSystem _onDestroyParticle, _onHitParticle;


    [Space(10)]

    //Data
    [SerializeField] int _value = 1, _recycleEnnemyY;
    [SerializeField] float _health = 3;


    //Cache
    bool _isReady, _isRecycle = false;
    float _maxHealth;

    public void Init()
    {
        transform.DOMoveY(WorldManager.Instance._afterSpawnEnnemyPosTransform.position.y, 0.3f).OnComplete(() => _isReady = true);

        _maxHealth = _health;
    }

    void Update()
    {
        if (transform.position.y <= _recycleEnnemyY && _isRecycle == false)
        {
            _isRecycle = true;

            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (_isReady == false || _health <= 0) return;

        transform.position -= new Vector3(0, .1f, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            PlayerData.Instance.UpdateScore(_value);

            Destroy(collision.gameObject);

            TakeDamage(collision.gameObject.GetComponent<Missile>().Damage);
        }

        if (collision.tag == "Player")
        {
            PlayerController.Instance.TakeDamage();

            Destroy(gameObject);
        }
    }


    public void TakeDamage(int amount)
    {
        if (_health > 0)
        {
            _health -= amount;
        }

        //UIManager.Instance.GameCanvas.GameScreen.UpdateHeart();

        if (_health <= 0)
        {
            _renderer.SetActive(false);

            _coll.enabled = false;

            _onHitParticle.Stop();

            _onDestroyParticle.Play();

            Destroy(gameObject, _onDestroyParticle.main.duration);
        }
        else
        {
            var actualBurst = _onHitParticle.emission.GetBurst(0);
            var newBurst = new ParticleSystem.Burst(actualBurst.time, actualBurst.count.constant + 20f);
            _onHitParticle.emission.SetBurst(0, newBurst);

#pragma warning disable CS0618 // Le type ou le membre est obsolète
            if (_health > _maxHealth / 3)
            {
                _onHitParticle.startColor = ColorManager.Instance.LightGrey;
            }
            else
            {
                _onHitParticle.startColor = ColorManager.Instance.BrightGrey;
            }
#pragma warning restore CS0618 // Le type ou le membre est obsolète
            _onHitParticle.Play();
        }
    }
}
