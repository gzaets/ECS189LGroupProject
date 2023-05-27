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

    private bool rightDisabled = false;
    private bool leftDisabled = false;
    private bool upDisabled = false;
    private bool downDisabled = false;

    private float xEdgeLeft;
    private float xEdgeRight;
    private float yEdgeTop;
    private float yEdgeBottom;

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

        if(rightDisabled && moveHorizontal < 0.0f) 
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            rightDisabled = false;
            Debug.Log("here?");
        }
        if(leftDisabled && moveHorizontal > 0.0f) 
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            leftDisabled = false;
        }
        if(upDisabled && moveVertical < 0.0f) 
        {
            moveVertical = Input.GetAxis("Vertical");
            upDisabled = false;
        }
        if(downDisabled && moveVertical > 0.0f) 
        {
            moveVertical = Input.GetAxis("Vertical");
            downDisabled = false;
        }

        if(rightDisabled && transform.position.y > yEdgeTop) rightDisabled = false;
        if(rightDisabled && transform.position.y < yEdgeBottom) rightDisabled = false;
        if(leftDisabled && transform.position.y > yEdgeTop) leftDisabled = false;
        if(leftDisabled && transform.position.y < yEdgeBottom) leftDisabled = false;
        if(upDisabled && transform.position.x < xEdgeLeft) upDisabled = false;
        if(upDisabled && transform.position.x > xEdgeRight) upDisabled = false;
        if(downDisabled && transform.position.x < xEdgeLeft) downDisabled = false;
        if(downDisabled && transform.position.x > xEdgeRight) downDisabled = false;

        if(rightDisabled) moveHorizontal = 0.0f;
        if(leftDisabled) moveHorizontal = 0.0f;
        if(upDisabled) moveVertical = 0.0f;
        if(downDisabled) moveVertical = 0.0f;

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
                transform.position = new Vector2(prevXPos, prevYPos);
            }
        }
        prevXPos = transform.position.x;
        prevYPos = transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        Debug.Log(collision.collider.bounds);
        Debug.Log(collision.contacts[0].point);
        xEdgeLeft = collision.collider.bounds.center.x - collision.collider.bounds.extents.x - 0.01f;
        xEdgeRight = collision.collider.bounds.center.x + collision.collider.bounds.extents.x + 0.01f;
        yEdgeTop = collision.collider.bounds.center.y + collision.collider.bounds.extents.y + 0.01f;
        yEdgeBottom = collision.collider.bounds.center.y - collision.collider.bounds.extents.y - 0.01f;
        if(collision.contacts[0].point.x >= xEdgeLeft && transform.position.x > prevXPos && prevXPos < xEdgeLeft) rightDisabled = true;
        if(collision.contacts[0].point.x <= xEdgeRight && transform.position.x < prevXPos && prevXPos > xEdgeRight) leftDisabled = true;
        if(collision.contacts[0].point.y >= yEdgeBottom && transform.position.y > prevYPos && prevYPos < yEdgeBottom) upDisabled = true;
        if(collision.contacts[0].point.y <= yEdgeTop && transform.position.y < prevYPos && prevYPos > yEdgeTop) downDisabled = true;
        Debug.Log("yes");
        LateUpdate();
        //movement = new Vector2(prevXPos - transform.position.x, prevYPos - transform.position.y);
        //rb.velocity = movement * speed;
    }
}