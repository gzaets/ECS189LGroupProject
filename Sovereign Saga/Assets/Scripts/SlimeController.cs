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

    private float knockBackTime = 0.3f;
    private bool collided = false;
    private bool weapon = false;

    private GameObject suckTarget;

    [SerializeField]
    public GameObject Hero;

    private float prevXPos;
    private float prevYPos;

    private float playerX;

    private float collideSpeedX;
    private float collideSpeedY;

    void Start()
    {
        // No existing suck on spawn. 
        suckTarget = null;

        // Get the sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Depending on the slime type, set the damage, health and coinReward values
        Debug.Log(gameObject.name);
        if(gameObject.name == "Green Slime" || gameObject.name == "Green Slime(Clone)")
        {
            speed = 2f;
            damage = 20;
            health = 30;
            coinReward = 30;
        }
        if(gameObject.name == "Blue Slime" || gameObject.name == "Blue Slime(Clone)")
        {
            speed = 4f;
                damage = 10;
                health = 20;
                coinReward = 20;
        }
        if(gameObject.name == "Red Slime" || gameObject.name == "Red Slime(Clone)")
        {
            speed = 2f;
            damage = 20;
            health = 30;
            coinReward = 30;
        }
        else
        {
            speed = 1f;
                damage = 20;
                health = 15;
                coinReward = 30;
        }
        /*
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
        }*/
        prevXPos = transform.position.x;
        prevYPos = transform.position.y;
        playerX = Hero.transform.position.x;
    }

    void Update()
    {
        playerX = Hero.transform.position.x;
        // Only move the slime if the player is in the cave
        if (playerX < -59f)
        {
            // Move towards the player
            if(collided == false)
            {
            //Vector2 targetPosition = target.position;
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
            if(collided)
            {
                //Debug.Log("Getting here???SDGFdfg");
                knockBackTime -= Time.deltaTime;
                if(knockBackTime <= 0.0f)
                {
                    knockBackTime = 0.3f;
                    collided = false;
                    weapon = false;
                }
                else
                {
                    transform.position = new Vector2(transform.position.x + 2 * collideSpeedX, transform.position.y + 2 * collideSpeedY);
                }
            }
            prevXPos = transform.position.x;
            prevYPos = transform.position.y;
        }
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
        PlayerController pController = collision.collider.GetComponent<PlayerController>();
        if (pController != null)
        {

            //Debug.Log("uhhh");
            pController.TakeDamage(damage);

            //Debug.Log("Player has taken damage");
        }
    }

    
    // This checks for collision with the collider but does not simulate the physics and pushback. 
    void OnTriggerEnter2D(Collider2D collision)
    {
        collided = true;
        weapon = true;
        Debug.Log("here??dfgdfg");
        GameObject gb = collision.gameObject;
        Debug.Log(gb.tag);
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
                Debug.Log(damage);
                TakeDamage(damage);
                Destroy(gb);
                break;
            case "Sword":
                // Hardcoded damage for now not balanced?
                TakeDamage(12);
                break;
            case "Tornado":
                damage = gb.GetComponent<Tornado>().GetDamage();
                TakeDamage(damage);
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