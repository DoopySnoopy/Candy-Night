using UnityEngine;

public class SkeletonHeadScript : MonoBehaviour
{
    private SkeletonController skeletonController;

    void Start()
    {
        skeletonController = GetComponentInParent<SkeletonController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

            if (playerRb != null && playerRb.linearVelocity.y < 0)
            {
                if (skeletonController != null)
                {
                    skeletonController.Defeat(playerRb);
                }
                else
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }
}
