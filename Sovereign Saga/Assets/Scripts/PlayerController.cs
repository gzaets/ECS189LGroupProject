using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    // Current passive income of player
    public static int incomeGenerationRate = 0;

    [SerializeField]
    private int currentPassiveIncome = 0;

    private float timeElapsed = 0f;

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

        animator.SetFloat("Speed", movement.sqrMagnitude);

        timeElapsed += Time.deltaTime;

        if (timeElapsed > 1f)
        {
            currentPassiveIncome = currentPassiveIncome + incomeGenerationRate;
            Debug.Log("Current Income: " + currentPassiveIncome);
            timeElapsed = 0f;
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    public int GetCurrentIncome()
    {
        return currentPassiveIncome;
    }

    public void SetCurrentIncome(int newIncome)
    {
        currentPassiveIncome = newIncome;
    }
}