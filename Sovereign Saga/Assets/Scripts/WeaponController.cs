using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    private Vector2 pointerPosition;
    private float attackDelay = 0.5f;
    private float timer = 0.0f;
    private bool attackDebounce = false;
    [SerializeField]
    private Animator animator;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (pointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        var currentScale = transform.localScale;
        currentScale.y = 1;
        if (direction.x < 0)
        {
            currentScale.y = -1;
        }

        transform.localScale = currentScale;

        if (attackDebounce)
        {
            timer += Time.deltaTime;
            if (timer >= attackDelay)
            {
                attackDebounce = false;
                timer = 0.0f;
            }
        }
    }

    public void setPointerPosition(Vector2 pointerPosition)
    {
        this.pointerPosition = pointerPosition;
    }

    public void Attack()
    {
        if (attackDebounce)
        {
            return;
        }
        animator.SetTrigger("Attack");
        attackDebounce = true;
    }
}
