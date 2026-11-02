using UnityEngine;
using UnityEngine.InputSystem;

public class HealerNPCScript : MonoBehaviour
{
    private bool playerNear = false;
    private PlayerScript playerRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNear && Keyboard.current != null && Keyboard.current.zKey.wasPressedThisFrame)
        {
            if (playerRef != null) {
                playerRef.SetHealth(100f);
                Debug.Log("Player healed");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = true;
            playerRef = collision.GetComponent<PlayerScript>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = false;
            playerRef = null;
        }
    }
}
