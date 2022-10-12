using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] TrailRenderer _trailRenderer;

    [SerializeField] int _damage = 1;

    [SerializeField] float _recycleBulletY;

    [SerializeField] bool _isEnnemyMissile;

    //Cache 
    bool _isRecycle = false;

    public int Damage { get => _damage; }

    public void Init()
    {
        _trailRenderer.enabled = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (_isEnnemyMissile)
        {
            if (transform.localPosition.y <= _recycleBulletY  && _isRecycle == false)
            {
                _isRecycle = true;
                StartCoroutine(RecycleBullet());
            }
        } else
        {
            if (transform.localPosition.y >= _recycleBulletY && _isRecycle == false)
            {
                _isRecycle = true;
                StartCoroutine(RecycleBullet());
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
                StartCoroutine(RecycleBullet());

                PlayerController.Instance.TakeDamage();
            }
        }
    }

    public IEnumerator RecycleBullet()
    {
        _trailRenderer.enabled = false;
        yield return new WaitUntil(() => _trailRenderer.enabled = false);
        if (_isEnnemyMissile)
        {
            PoolManager.Instance.gameobjectPoolDictionary["EnnemyMissile"].Release(gameObject);
        }
        else
        {
            PoolManager.Instance.gameobjectPoolDictionary["Missile"].Release(gameObject);
        }
    }
}

