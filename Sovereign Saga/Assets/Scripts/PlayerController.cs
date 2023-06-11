using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float health = 100f;
    private Rigidbody2D rb;
    public Animator animator;
    public float money = 0f;
    
    public Slider healthBar;
    public Slider staminaBar;
    public TextMeshProUGUI moneyUI;

    private float currentMovementSpeed = 4.0f;
    private float defaultMovementSpeed = 4.0f;

    // Combat variable
    public bool inCombat { get; set; }
    
    // Dashing Variables
    private float dashSpeed = 8.0f;
    private float dashLength = 0.5f;
    private float dashCooldown = 10.0f;
    private float dashCounter = 0.0f;
    private bool canDash = true;

    private Vector2 movement;
    private Vector2 mouseLocation;
    private WeaponController weaponController;
    private MagicController magicController;
    private Ghost ghostFX; 

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

    public static int incomeGenerationRate = 0;

    [SerializeField]
    private int currentPassiveIncome = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        prevXPos = transform.position.x;
        prevYPos = transform.position.y;
        weaponController = GetComponentInChildren<WeaponController>();
        magicController = GetComponentInChildren<MagicController>();
        animator = GetComponentInChildren<Animator>();
        ghostFX = GetComponent<Ghost>();
        inCombat = false;

        // Set it so the player faces down at the beginning of the game. 
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", -1);
    }

    void Update()
    {
        collided = false;
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

        //Debug.Log(mouseLocation);

        weaponController.setPointerPosition(mouseLocation);
        magicController.setPointerPosition(mouseLocation);
        
        // Attacking
        if (Input.GetButton("Fire1"))
        {
            weaponController.Attack();
        }

        // Dashing
        if (Input.GetKeyDown(KeyCode.Space) && canDash && movement != Vector2.zero)
        {
            canDash = false;
            ghostFX.setGhost(true);
            currentMovementSpeed = dashSpeed;
        }

        // Magic Testing
        if (Input.GetButton("Fire2"))
        {
            magicController.Execute("Fireball");
        }

        if (!canDash)
        {   
            dashCounter += Time.deltaTime;
            if (dashCounter >= dashLength)
            {
                ghostFX.setGhost(false);
                currentMovementSpeed = defaultMovementSpeed;
            }

            if (dashCounter >= dashCooldown)
            {
                canDash = true;
                dashCounter = 0.0f;
            }
        }

        moneyUI.text = "$" + money;

    }

    void FixedUpdate()
    {
        // ============================= MOVEMENT =============================
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(moveHorizontal, moveVertical).normalized;
        rb.velocity = direction * currentMovementSpeed;
        // ====================================================================

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
        rb.MovePosition(transform.position + movement * currentMovementSpeed * Time.deltaTime);

        // Health Bar changes depending on HP.
        healthBar.value = health;

        // Stamima Bar changes depending on HP.
        staminaBar.value = currentMovementSpeed;

        staminaUI.value = speed;

        moneyUI.text = "$" + money;
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

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Cave cave = collision.collider.GetComponent<Cave>();
        if(cave != null)
        {
            transform.position = cave.exitPoint;
        }
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
        else if(collision.collider.tag == "building") {
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

    // Borrowed logic, need to put citation here later. 
    private Vector2 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z += Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public int GetCurrentIncome()
    {
        return currentPassiveIncome;
    }

    public void SetCurrentIncome(int newIncome)
    {
        currentPassiveIncome = newIncome;
    }

    public void AddIncome(int reward)
    {
        // Need to add a variable that keeps track of current money. 
    }

}