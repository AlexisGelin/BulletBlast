using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] TrailRenderer _trailRenderer;

    [SerializeField] float _recycleBulletY;

    //Cache 
    bool _isRecycle = false;

    public void Init()
    {
        _trailRenderer.enabled = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (transform.localPosition.y >= _recycleBulletY && _isRecycle == false)
        {
            _isRecycle = true;
            StartCoroutine(RecycleBullet());
        }
    }

    void FixedUpdate()
    {
        if (transform.position.y < _recycleBulletY) transform.localPosition += (transform.up / 5);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

    }

    IEnumerator RecycleBullet()
    {
        _trailRenderer.enabled = false;
        yield return new WaitUntil(() => _trailRenderer.enabled = false);
        PoolManager.Instance.gameobjectPoolDictionary["Missile"].Release(gameObject);
    }
}

