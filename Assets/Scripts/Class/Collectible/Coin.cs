using UnityEngine;

public class Coin : Collectible
{
    public override void Init()
    {
        rb.angularVelocity = Random.Range(45, 270);

        base.Init();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerData.Instance.UpdateCoin(10);

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

        if (collision.tag == "WorldBorder") Destroy(gameObject);

    }
}
