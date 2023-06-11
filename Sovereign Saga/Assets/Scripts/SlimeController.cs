using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float speed;
    public int damage;
    public int health;
    public int coinReward;
    private bool stopMoving;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;  // Used to flip the sprite
    private GameObject suckTarget;

    [SerializeField]
    private GameObject Hero;


    void Start()
    {
        // No existing suck on spawn. 
        suckTarget = null;

        // Get the sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Depending on the slime type, set the damage, health and coinReward values
        switch (gameObject.name)
        {
            case "Green Slime":
                speed = 2f;
                damage = 20;
                health = 30;
                coinReward = 30;
                break;
            case "Blue Slime":
                speed = 4f;
                damage = 10;
                health = 20;
                coinReward = 20;
                break;
            case "Red Slime":
                speed = 1f;
                damage = 20;
                health = 15;
                coinReward = 30;
                break;
            default:
                Debug.LogError("Unknown slime type: " + gameObject.name);
                break;
        }
    }

    void Update()
    {
        // Move towards the player or Suck
        Vector2 targetPosition = Hero.transform.position;
        if (suckTarget != null)
        {
            targetPosition = suckTarget.transform.position;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        
        // Flip the sprite based on the direction of movement
        if (targetPosition.x > transform.position.x)
        {
            // Target is to the right of the slime, so slime should face right
            spriteRenderer.flipX = false;
        }
        else if (targetPosition.x < transform.position.x)
        {
            // Target is to the left of the slime, so slime should face left
            spriteRenderer.flipX = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the slime collided with the player
        PlayerController pController = collision.collider.GetComponent<PlayerController>();
        if (pController != null)
        {
            pController.TakeDamage(damage);

            //Debug.Log("Player has taken damage");
        }
    }

    
    // This checks for collision with the collider but does not simulate the physics and pushback. 
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gb = collision.gameObject;
        switch (gb.tag)
        {
            case "Suck":
                ChangeTarget(collision.gameObject);
                break;
            case "Rock":
                var damage = gb.GetComponent<Rock>().GetDamage();
                TakeDamage(damage);
                Destroy(gb);
                break;
            case "Fireball":
                damage = gb.GetComponent<Fireball>().GetDamage();
                TakeDamage(damage);
                Destroy(gb);
                break;
            case "Sword":
                Debug.Log("works");
                TakeDamage(12);
                break;
            default:
                // Do literally nothing.
                break;
        }

    }
    

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
           // Reward player
           Hero.GetComponent<PlayerController>().AddIncome(coinReward);
           
           // Destroy current GameObject
           Destroy(gameObject);
        }
    }

    // This is for the suck skill, it changes the current target to suck. 
    public void ChangeTarget(GameObject suck)
    {
        suckTarget = suck;
    }
    
}