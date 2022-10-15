using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public int _destroyCollectibleY = -12;
    public  Rigidbody2D rb;

    public void  Init()
    {
        rb.AddForce(new Vector2(Random.Range(-5, 5), Random.Range(5, 10)), ForceMode2D.Impulse);
    }

}
