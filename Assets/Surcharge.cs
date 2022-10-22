using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surcharge : Collectible
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
            PlayerData.Instance.UpdateNumberOfMissile(1);

            OnCollectFX.Play();
            OnCollectFX.transform.parent = collision.transform;
            OnCollectFX.transform.localPosition = Vector3.zero;
            PlayerController.Instance.DestroyParticle(OnCollectFX);

            Destroy(gameObject);
        }

        if (collision.tag == "ScreenBorder")
        {
            rb.velocity = new Vector2(rb.velocity.x * -1, rb.velocity.y);
        }
    }
}
