using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] int _destroyCollectibleY = -12;
    [SerializeField] Rigidbody2D rb;

    public void Init()
    {
        rb.AddForce(new Vector2(Random.Range(-5, 5), Random.Range(5, 10)),ForceMode2D.Impulse);
    }

    void Update()
    {
        if (transform.position.y <= _destroyCollectibleY)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(0, .1f, 0);
    }
}
