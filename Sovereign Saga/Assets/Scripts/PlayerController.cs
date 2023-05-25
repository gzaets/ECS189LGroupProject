using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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
        rb.velocity = movement * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if(System.Math.Abs(moveHorizontal) > 0.0f)
        {
            movement = new Vector2(0f, moveVertical);
        }
        if(System.Math.Abs(moveVertical) > 0.0f)
        {
            movement = new Vector2(moveHorizontal, 0f);
        }
        if(System.Math.Abs(moveVertical) > 0.0f && System.Math.Abs(moveHorizontal) > 0.0f)
        {
            movement = new Vector2(0f, 0f);
        }
        rb.velocity = movement * speed;
        Debug.Log("seeing");
        
    }
}