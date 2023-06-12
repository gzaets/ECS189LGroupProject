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
    public float money = 1000000f;  
    public Slider healthUI;
    public Slider staminaUI;
    public TextMeshProUGUI moneyUI;
    public GameObject gameOverUI;
    public GameObject winUI;
    public TextMeshProUGUI strengthUI;
    public TextMeshProUGUI intelligenceUI;

    public float strength = 0f;
    public float intelligence = 0f;

    private float currentMovementSpeed = 6.0f;
    private float defaultMovementSpeed = 6.0f;

    public int numBuildingsPurchased = 0;

    // Combat variable
    private bool inCombat;
    private bool inCave;
    private bool canFireball;
    private bool canTornado;
    private bool canSuck;
    private bool canRock;
    
    // Dashing Variables
    private float dashSpeed = 8.0f;
    private float dashLength = 0.5f;
    private float dashCooldown = 10.0f;
    private float dashCounter = 0.0f;
    private bool canDash = true;

    // Variable to indicate death and trigger death scene. 
    private bool isDead = false;

    private Vector2 movement;
    private Vector2 mouseLocation;
    private WeaponController weaponController;
    private MagicController magicController;
    public Ghost ghostFX; 

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

    private float updateMoney = 0.0f;

    [SerializeField]
    private int currentPassiveIncome = 0;

    private void Awake()
    {
        money = 1000000f;
        rb = GetComponent<Rigidbody2D>();
        prevXPos = transform.position.x;
        prevYPos = transform.position.y;
        weaponController = GetComponentInChildren<WeaponController>();
        magicController = GetComponentInChildren<MagicController>();
        animator = GetComponentInChildren<Animator>();
        ghostFX = GetComponent<Ghost>();
        
        // IMPORTANT NEED TO SET THIS WHEN IN DUNGEON!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! we do not have this yet
        inCombat = false;

        // Player initially cannot use magic until learned so false.
        canFireball = false;
        canTornado = false;
        canSuck = false;
        canRock = false;

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
        
        // Only accept inputs if we are alive AND in combat (George, please set this when you are done with dungeon).
        if (!isDead && inCombat)
        {
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
                Debug.Log("hi");
            }

            // Magic
            if (Input.GetKeyDown(KeyCode.Z) && canFireball)
            {
                magicController.Execute("Fireball");
            }

            if (Input.GetKeyDown(KeyCode.X) && canRock)
            {
                magicController.Execute("Rock");
            }

            if (Input.GetKeyDown(KeyCode.C) && canTornado)
            {
                magicController.Execute("Tornado");
            }

            if (Input.GetKeyDown(KeyCode.Q) && canSuck)
            {
                magicController.Execute("Suck");
            }
        }


        // Dash Cooldown
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
        updateMoney += Time.deltaTime;
        if(updateMoney >= 1.0f)
        {
            money += incomeGenerationRate;
            moneyUI.text = "$" + money;
            updateMoney = 0.0f;
        }

        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            isDead = true;
        }

        if(health < 70)
        {
            currentMovementSpeed = 5.0f;
            defaultMovementSpeed = 5.0f;
        }
        if(health < 40)
        {
            currentMovementSpeed = 4.0f;
            defaultMovementSpeed = 4.0f;
        }
    }

    void FixedUpdate()
    {
        // ============================= MOVEMENT =============================
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(moveHorizontal, moveVertical).normalized;
        
        // We do not move if we are dead. 
        if (!isDead)
        {
            rb.velocity = direction * currentMovementSpeed;
        }
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
        healthUI.value = health;

        // Stamima Bar changes depending on currentMovementSpeed.
        staminaUI.value = currentMovementSpeed;
        strength = numBuildingsPurchased <= 0 ? 0 : numBuildingsPurchased - 1;
        intelligence = 1 + (canSuck ? 1 : 0) + (canTornado ? 1 : 0) + (canFireball ? 1 : 0) + (canRock ? 1 : 0);
        //moneyUI.text = "$" + money;
        // Strength value changes depending on strength variable.
        strengthUI.text = strength.ToString();

        // Intelligence value changes depending on intelligence variable.
        intelligenceUI.text = intelligence.ToString();
        if (isDead) {
            gameOverUI.SetActive(true);
            leftDisabled = true;
            downDisabled = true;
            rightDisabled = true;
            upDisabled = true;
        }

        if(numBuildingsPurchased == 11)
        {
            winUI.SetActive(true);
        }
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
        /*
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
        }*/
        prevXPos = transform.position.x;
        prevYPos = transform.position.y;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void Heal(int heal)
    {
        health += heal;
    }

    public void IncreaseStamina(int stamina)
    {
        currentMovementSpeed += stamina;
    }

    public void IncreaseStrength(int strength)
    {
        this.strength += strength;
    }

    public void IncreaseIntelligence(int intelligence)
    {
        this.intelligence += intelligence;
    }

    public bool GetInCave()
    {
        return inCave;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Cave cave = collision.collider.GetComponent<Cave>();
        if(cave != null)
        {
            inCombat = !inCombat;
            inCave = !inCave;
            transform.position = cave.exitPoint;
        }
        else if(cave == null)
        {
            //inCombat = false;
            inCave = false;
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

    public void BoughtMagic(string magictyp)
    {
        switch (magictyp)
        {
            case "Fireball":
                canFireball = true;
                break;
            case "Suck":
                canSuck = true;
                break;
            case "Tornado":
                canTornado = true;
                break;
            case "Rock":
                canRock = true;
                break;
        }
    }

}