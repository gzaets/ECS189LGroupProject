using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    private float prevXPos;
    private float prevYPos;
    private bool collided = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        prevXPos = transform.position.x;
        prevYPos = transform.position.y;
    }

    void Update()
    {
        collided = false;
        movement.x = Input.GetAxisRaw("Horizontal") * 0.75f;
        movement.y = Input.GetAxisRaw("Vertical") * 0.75f;

        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        /*
        if(transform.position.x > 14.69f)
        {
            speed = 0f;
        }
        else
        {
            speed = 5f;
        }
        */
        //animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;
    }

    void LateUpdate()
    {
        if(collided)
        {
            Debug.Log("are we here");
            /*
            if(prevXPos - transform.position.x > 0)
            {
                transform.position = new Vector2(prevXPos + 0.1f, prevYPos);
            }
            if(prevXPos - transform.position.x < 0)
            {
                transform.position = new Vector2(prevXPos - 0.1f, prevYPos);
            }
            if(prevYPos - transform.position.y > 0)
            {
                transform.position = new Vector2(prevXPos, prevYPos + 0.1f);
            }
            if(prevYPos - transform.position.y < 0)
            {
                transform.position = new Vector2(prevXPos, prevYPos - 0.1f);
            }
            */
            if(prevXPos - transform.position.x > 0 && prevYPos - transform.position.y > 0)
            {
                transform.position = new Vector2(prevXPos + 0.1f, prevYPos + 0.1f);
            }
            else if(prevXPos - transform.position.x > 0 && prevYPos - transform.position.y < 0)
            {
                transform.position = new Vector2(prevXPos + 0.1f, prevYPos - 0.1f);
            }
            else if(prevXPos - transform.position.x < 0 && prevYPos - transform.position.y > 0)
            {
                transform.position = new Vector2(prevXPos - 0.1f, prevYPos + 0.1f);
            }
            else if(prevXPos - transform.position.x < 0 && prevYPos - transform.position.y < 0)
            {
                transform.position = new Vector2(prevXPos - 0.1f, prevYPos - 0.1f);
            }
            else if(prevXPos - transform.position.x == 0 && prevYPos - transform.position.y > 0)
            {
                Debug.Log("here???");
                transform.position = new Vector2(prevXPos, prevYPos + 0.1f);
            }
            else if(prevXPos - transform.position.x == 0 && prevYPos - transform.position.y < 0)
            {
                Debug.Log("here???");
                transform.position = new Vector2(prevXPos, prevYPos - 0.1f);
            }
            else if(prevXPos - transform.position.x > 0 && prevYPos - transform.position.y == 0)
            {
                Debug.Log("here");
                transform.position = new Vector2(prevXPos + 0.1f, prevYPos);
            }
            else if(prevXPos - transform.position.x < 0 && prevYPos - transform.position.y == 0)
            {
                transform.position = new Vector2(prevXPos - 0.1f, prevYPos);
            }
            else
            {
                //whatever
            }
        }
        prevXPos = transform.position.x;
        prevYPos = transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        Debug.Log("yes");
        LateUpdate();
        //movement = new Vector2(prevXPos - transform.position.x, prevYPos - transform.position.y);
        //rb.velocity = movement * speed;
    }
}