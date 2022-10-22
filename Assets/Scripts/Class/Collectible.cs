using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public int _destroyCollectibleY = -12;
    public Rigidbody2D rb;
    public ParticleSystem OnCollectFX;


    //Cache
    bool _left;

    public virtual void Init()
    {
        _left = Random.Range(0,2) == 1 ? false : true;

        if (_left) rb.AddForce(new Vector2(Random.Range(-7, -3), Random.Range(5, 10)), ForceMode2D.Impulse);
        else rb.AddForce(new Vector2(Random.Range(3, 7), Random.Range(5, 10)), ForceMode2D.Impulse);

    }

}
