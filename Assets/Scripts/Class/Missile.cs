using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] TrailRenderer _trailRenderer;
    [SerializeField] bool _isEnnemyMissile;

    public void Init()
    {
        _trailRenderer.Clear();
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void FixedUpdate()
    {
        if (_isEnnemyMissile) transform.localPosition -= (transform.up / 5);
        else transform.localPosition += (transform.up / 5);
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

        if (collision.tag == "WorldBorder")
        {
            Debug.Log("WorldBorder");
            RecycleBullet();
        }
    }

    public void RecycleBullet()
    {
        if (_isEnnemyMissile) PoolManager.Instance.gameobjectPoolDictionary["EnnemyMissile"].Release(gameObject);
        else PoolManager.Instance.gameobjectPoolDictionary["PlayerMissile"].Release(gameObject);
    }
}

