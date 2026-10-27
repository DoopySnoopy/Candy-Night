using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    public float Health, MaxHealth;

    [SerializeField]
    private HealthBarUI healthBar;
    //These are the player's Variables, the raw info that defines them
    
    //The Rigidbody2D is a component that gives the player physics, and is what we use to move
    public Rigidbody2D RB;

    //TextMeshPro is a component that draws text on the screen.
    //We use this one to show our score.
    public TextMeshPro ScoreText;
    
    //This will control how fast the player moves
    public float Speed = 3;
    
    //This is how many points we currently have
    public int Score = 0;

    public float JumpForce = 5f;
    public bool Ground = false;

    //Start automatically gets triggered once when the objects turns on/the game starts
    void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);
        //During setup we call UpdateScore to make sure our score text looks correct
        UpdateScore();
    }

    //Update is a lot like Start, but it automatically gets triggered once per frame
    //Most of an object's code will be called from Update--it controls things that happen in real time
    void Update()
    {
        //The code below controls the character's movement
        //First we make a variable that we'll use to record how we want to move
        //Changed for gravity
        Vector2 vel = RB.linearVelocity;

        //Then we use if statement to figure out what that variable should look like

        //If I hold the right arrow key, the player should move right. . .
        if (Keyboard.current != null)
        {
            if (Keyboard.current.rightArrowKey.isPressed)
            {
                vel.x = Speed;
            } else if (Keyboard.current.leftArrowKey.isPressed)
            {
                vel.x = -Speed;
            } else
            {
                vel.x = 0;
            }
        }

        // If the player is on a ground then the player can jump
        if (Keyboard.current.spaceKey.wasPressedThisFrame && Ground)
        {
            vel.y = JumpForce;
        }
        // Changed for gravity
        RB.linearVelocity = vel;

        if (transform.position.x >= 3.2f)
        {
            SceneManager.LoadScene("Scene 1");
        }
    }
    //This gets called whenever you bump into another object, like a wall or coin.
    private void OnCollisionEnter2D(Collision2D other)
    {
        //This checks to see if the thing you bumped into had the Hazard tag
        //If it does...
        if (other.gameObject.CompareTag("Hazard"))
        {
            //Run your 'you lose' function!
            Die();
        }

        if (other.gameObject.CompareTag("PainfulObstacle"))
        {
            SetHealth(-10f);
        }
        
        //This checks to see if the thing you bumped into has the CoinScript script on it
        CoinScript coin = other.gameObject.GetComponent<CoinScript>();
        //If it does, run the code block belows
        if (coin != null)
        {
            //Tell the coin that you bumped into them so they can self destruct or whatever
            coin.GetBumped();
            //Make your score variable go up by one. . .
            Score++;
            //And then update the game's score text
            UpdateScore();
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            Ground = true;
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Ground = false;
        }
    }

    //This function updates the game's score text to show how many points you have
    //Even if your 'score' variable goes up, if you don't update the text the player doesn't know
    public void UpdateScore()
    {
        ScoreText.text = "Score: " + Score;
    }

    //If this function is called, the player character dies. The game goes to a 'Game Over' screen.
    public void Die()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void SetHealth(float healthChange)
    {
        Health += healthChange;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        healthBar.SetHealth(Health);

        if (Health <= 0)
        {
            Die();
        }
    }
}
