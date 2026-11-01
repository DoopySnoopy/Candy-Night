using UnityEngine;

public class CandyScript : MonoBehaviour
{
    public AudioClip candySound;
    public void GetBumped()
    {
        AudioSource.PlayClipAtPoint(candySound, transform.position);
        Destroy(gameObject);
        
    }
}
