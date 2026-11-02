using UnityEngine;

public class SkeletonController : MonoBehaviour
{

    public int damageAmount = 20;
    public float bounceForce = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            if (player != null)
            {
                player.SetHealth(-damageAmount);
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Defeat(Rigidbody2D playerRb)
    {
        if (playerRb != null)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
        }

        Destroy(gameObject);
    }
}
