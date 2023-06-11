using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float speed;
    public int damage;
    public int health;
    public int coinReward;
    public Transform target;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;  // Used to flip the sprite

    void Start()
    {
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
        // Move towards the player
        Vector2 targetPosition = target.position;
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
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);

            //Debug.Log("Player has taken damage");
        }
    }
}