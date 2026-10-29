using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartScreenScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.zKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Beginning of Game");
        }
    }
}
