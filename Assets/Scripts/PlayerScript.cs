using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    public CameraController cameraController;

    public float Health, MaxHealth;

    [SerializeField]
    private HealthBarUI healthBar;
    //These are the player's Variables, the raw info that defines them
    
    //The Rigidbody2D is a component that gives the player physics, and is what we use to move
    public Rigidbody2D RB;

    public TextMeshProUGUI CandyText;
    
    //This will control how fast the player moves
    public float Speed = 2f;
    
    //This is how many points we currently have
    public int Candy = 0;

    public float JumpForce = 5f;

    public Transform groundedCheck;
    public float groundedCheckRad = 0.2f;
    public LayerMask groundLayer;
    public bool Ground = false;

    void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);
        UpdateCandy();
    }

    void Update()
    {
        Ground = Physics2D.OverlapCircle(groundedCheck.position, groundedCheckRad, groundLayer);
   
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
        CandyScript candy = other.gameObject.GetComponent<CandyScript>();
        //If it does, run the code block belows
        if (candy != null)
        {
            //Tell the coin that you bumped into them so they can self destruct or whatever
            candy.GetBumped();
            //Make your score variable go up by one. . .
            Candy++;
            //And then update the game's score text
            UpdateCandy();
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            Ground = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Screen"))
        {
            cameraController.SetTarget(other.transform.position);
        }
    }

    public void UpdateCandy()
    {
        CandyText.text = "" + Candy;
    }

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
