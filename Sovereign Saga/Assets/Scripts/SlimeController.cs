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
    private float knockBackTime = 0.3f;
    private bool collided = false;

    private float prevXPos;
    private float prevYPos;

    private float collideSpeedX;
    private float collideSpeedY;
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
        prevXPos = transform.position.x;
        prevYPos = transform.position.y;
    }

    void Update()
    {
        // Move towards the player
        if(collided == false)
        {
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
        if(collided)
        {
            //Debug.Log("Getting here???SDGFdfg");
            knockBackTime -= Time.deltaTime;
            if(knockBackTime <= 0.0f)
            {
                knockBackTime = 0.3f;
                collided = false;
            }
            transform.position = new Vector2(transform.position.x + 2 * collideSpeedX, transform.position.y + 2 * collideSpeedY);
        }
        prevXPos = transform.position.x;
        prevYPos = transform.position.y;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        knockBackTime = 0.3f;
        collideSpeedX = transform.position.x - prevXPos;
        collideSpeedY = transform.position.y - prevYPos;
        //Debug.Log(collideSpeedX);
        //Debug.Log("okLetsMakeSureTheCallWasHere");
        // Check if the slime collided with the player
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player != null)
        {
            //Debug.Log("uhhh");
            player.TakeDamage(damage);

            //Debug.Log("Player has taken damage");
        }
    }
}