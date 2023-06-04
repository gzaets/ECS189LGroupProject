using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float currentMovementSpeed = 4.0f;
    private float defaultMovementSpeed = 4.0f;
    
    // Dashing Variables
    private float dashSpeed = 8.0f;
    private float dashLength = 0.5f;
    private float dashCooldown = 10.0f;
    private float dashCounter = 0.0f;
    private bool canDash = true;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private Vector2 mouseLocation;
    private WeaponController weaponController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponController = GetComponentInChildren<WeaponController>();
        animator = GetComponentInChildren<Animator>();

        // Set it so the player faces down at the beginning of the game. 
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", -1);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            animator.SetFloat("Speed", rb.velocity.magnitude);
        }

        mouseLocation = GetMousePosition();
        weaponController.setPointerPosition(mouseLocation);
        
        // Attacking
        if (Input.GetButton("Fire1"))
        {
            weaponController.Attack();
        }

        // Dashing
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            canDash = false;
            currentMovementSpeed = dashSpeed;
        }

        if (!canDash)
        {   
            dashCounter += Time.deltaTime;
            if (dashCounter >= dashLength)
            {
                currentMovementSpeed = defaultMovementSpeed;
            }

            if (dashCounter >= dashCooldown)
            {
                canDash = true;
                dashCounter = 0.0f;
            }
        }

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(moveHorizontal, moveVertical).normalized;
        rb.velocity = direction * currentMovementSpeed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    // Borrowed logic, need to put citation here later. 
    private Vector2 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z += Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}