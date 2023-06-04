using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    private Vector2 mouseLocation;
    private int lastMovement;

    private WeaponController weaponController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponController = GetComponentInChildren<WeaponController>();


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
        
        if (Input.GetButton("Fire1"))
        {
            weaponController.Attack();
        }

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;
        rb.velocity = movement * speed;

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