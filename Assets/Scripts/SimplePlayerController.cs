using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimplePlayerController : MonoBehaviour
{
    //These are the player's Variables, the raw info that defines them
    
    //The Rigidbody2D is a component that gives the player physics, and is what we use to move
    public Rigidbody2D RB;
    
    //This will control how fast the player moves
    public float Speed = 3f;
    public float Jump = 6f;

    public bool Ground = false;
    private float Move;


    //Start automatically gets triggered once when the objects turns on/the game starts
    void Start()
    {
        //This is empty for now.
    }

    //Update is a lot like Start, but it automatically gets triggered once per frame
    //Most of an object's code will be called from Update--it controls things that happen in real time
    void Update()
    {
        //The code below controls the character's movement
        //First we make a variable that we'll use to record how we want to move
        Vector2 vel = RB.linearVelocity;
        //Then we use if statement to figure out what that variable should look like
        
        //If I hold the right arrow key, the player should move right. . .
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            vel.x = Speed;
        }
        //If I hold the left arrow, the player should move left. . .
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            vel.x = -Speed;
        }
        //If I hold the up arrow, the player should move up. . .
        // if (Keyboard.current.upArrowKey.isPressed)
        // {
        //    vel.y = Speed;
        // }
        //If I hold the down arrow, the player should move down. . .
        // if (Keyboard.current.downArrowKey.isPressed)
        //{
        //    vel.y = -Speed;
        //}
        
        // Horizontal velocity is 0 if player is not moving left or irght
        if (!Keyboard.current.leftArrowKey.isPressed && !Keyboard.current.rightArrowKey.isPressed)
        {
            vel.x = 0;
        }

        // If player is grounded and presses space, they jump
        if (Keyboard.current.spaceKey.wasPressedThisFrame && Ground)
        {
            vel.y = Jump;
        }
        //Finally, I take that variable and I feed it to the component in charge of movement
        RB.linearVelocity = vel;
    }

    // Checks if you are colliding with a "Ground" object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Ground = true;
        }
    }

    // Checks if you exited the collision and are still touching the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Ground = false;
        }
    }
}
