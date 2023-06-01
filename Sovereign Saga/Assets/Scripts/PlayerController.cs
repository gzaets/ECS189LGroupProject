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

    private WeaponController weaponController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponController = GetComponentInChildren<WeaponController>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") * 1f;
        movement.y = Input.GetAxisRaw("Vertical") * 1f;

        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);

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


    private Vector2 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z += Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}