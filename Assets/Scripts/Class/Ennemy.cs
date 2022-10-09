using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    //Data
    [SerializeField] int _value = 1, _health = 3, _recycleEnnemyY;


    //Cache
    [SerializeField] bool _isReady, _isRecycle = false;

    public void Init()
    {
        transform.DOMoveY(WorldManager.Instance._afterSpawnEnnemyPosTransform.position.y, 0.3f).OnComplete(() => _isReady = true);
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
        if (_isReady == false) return;

        transform.position -= new Vector3(0, .1f, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            Debug.Log("Bullet");

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
            Destroy(gameObject);
        }
    }


}
