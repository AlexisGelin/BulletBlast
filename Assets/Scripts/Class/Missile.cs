using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] TrailRenderer _trailRenderer;

    [SerializeField] float _recycleBulletY;

    [SerializeField] bool _isEnnemyMissile;

    //Cache 
    bool _isRecycle = false;

    public void Init()
    {
        _isRecycle = false;
        _trailRenderer.Clear();


        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (_isEnnemyMissile)
        {
            if (transform.localPosition.y <= _recycleBulletY && _isRecycle == false)
            {
                _isRecycle = true;
                RecycleBullet();
            }
        }
        else
        {
            if (transform.localPosition.y >= _recycleBulletY && _isRecycle == false)
            {
                _isRecycle = true;
                RecycleBullet();
            }
        }

    }

    void FixedUpdate()
    {
        if (_isEnnemyMissile)
        {
            if (transform.position.y > _recycleBulletY) transform.localPosition -= (transform.up / 5);
        }
        else
        {
            if (transform.position.y < _recycleBulletY) transform.localPosition += (transform.up / 5);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isEnnemyMissile)
        {
            if (collision.tag == "Player")
            {
                RecycleBullet();

                PlayerController.Instance.TakeDamage();
            }
        }
    }

    public void RecycleBullet()
    {
        if (_isEnnemyMissile) PoolManager.Instance.gameobjectPoolDictionary["EnnemyMissile"].Release(gameObject);
        else PoolManager.Instance.gameobjectPoolDictionary["PlayerMissile"].Release(gameObject);
    }
}

