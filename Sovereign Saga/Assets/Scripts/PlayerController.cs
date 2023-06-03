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

    private int state = 2;

    private bool lastCollisionBuilding = false;

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
        if(state == 0) speed = 5f;
        if(state == 1) speed = 7f;
        if(state == 2) speed = 8f;
        */
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
        if(lastCollisionBuilding) {
        if(rightDisabled && transform.position.y > yEdgeTop + 0.5f) rightDisabled = false;
        if(rightDisabled && transform.position.y < yEdgeBottom - 0.5f) rightDisabled = false;
        if(leftDisabled && transform.position.y > yEdgeTop + 0.5f) leftDisabled = false;
        if(leftDisabled && transform.position.y < yEdgeBottom - 0.5f) leftDisabled = false;
        if(upDisabled && transform.position.x < xEdgeLeft - 0.5f) upDisabled = false;
        if(upDisabled && transform.position.x > xEdgeRight + 0.5f) upDisabled = false;
        if(downDisabled && transform.position.x < xEdgeLeft - 0.5f) downDisabled = false;
        if(downDisabled && transform.position.x > xEdgeRight + 0.5f) downDisabled = false;
        }
        if(rightDisabled) moveHorizontal = 0.0f;
        if(leftDisabled) moveHorizontal = 0.0f;
        if(upDisabled) moveVertical = 0.0f;
        if(downDisabled) moveVertical = 0.0f;
    
        Vector3 movement = new Vector3(moveHorizontal, moveVertical,0);
        //rb.velocity = movement * speed;
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
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
        if(collision.collider.name == "Water_Right")
        {
            Debug.Log("hdfgjdfg");
            /*
            lastCollisionBuilding = false;
            Debug.Log("ok");
            collided = true;
            Debug.Log(collision.collider.bounds);
            Debug.Log(collision.contacts[0].point);
            if(transform.position.x > prevXPos) rightDisabled = true;
            if(transform.position.x < prevXPos) leftDisabled = true;
            if(transform.position.y > prevYPos) upDisabled = true;
            if(transform.position.y < prevYPos) downDisabled = true;
            Debug.Log("yes");
            if(state == 0) state = state + 3 - 1;
            else state = state - 1;
            Debug.Log(state);
            LateUpdate();
            */
        }
        else {
            lastCollisionBuilding = true;
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
        if(state == 0) state = state + 3 - 1;
        else state = state - 1;
        Debug.Log(state);
        LateUpdate();
        //movement = new Vector2(prevXPos - transform.position.x, prevYPos - transform.position.y);
        //rb.velocity = movement * speed;
        }
    }
}