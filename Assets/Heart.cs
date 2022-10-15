using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Collectible
{
    void Update()
    {
        if (transform.position.y <= _destroyCollectibleY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerData.Instance.UpdateHealth(1);
            Destroy(gameObject);
        }

        if (collision.tag == "ScreenBorder")
        {
            rb.velocity = new Vector2(rb.velocity.x * -1, rb.velocity.y);
        }
    }
}
