using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float speed = 2.0f;
    public int damage = 10;
    public Transform target;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;  // Used to flip the sprite

    void Start()
    {
        // Assuming the player has a tag of "Player"
        // target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // Get the sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
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