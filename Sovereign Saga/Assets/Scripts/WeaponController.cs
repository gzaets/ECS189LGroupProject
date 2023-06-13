using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    private Vector2 pointerPosition;
    private float attackDelay = 1.0f;
    private float timer = 0.0f;
    private bool attackDebounce = false;
    private bool stallSword = false;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AudioClip swingSoundEffect;

    [SerializeField]
    private AudioClip swingMissSoundEffect;

    private AudioSource audioMusicSource;

    void Start()
    {
        audioMusicSource = gameObject.AddComponent<AudioSource>();
        audioMusicSource.volume = 1f; // Set the initial volume
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!stallSword)
        {
            Vector2 direction = (pointerPosition - (Vector2) transform.position).normalized;
            transform.right = direction;

            var currentScale = transform.localScale;
            currentScale.y = 1;
            if (direction.x < 0)
            {
                currentScale.y = -1;
            }

            transform.localScale = currentScale;
        }

        if (attackDebounce)
        {
            timer += Time.deltaTime;
            if (timer >= attackDelay)
            {
                attackDebounce = false;
                stallSword = false;
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
        audioMusicSource.clip = swingSoundEffect;
        audioMusicSource.Play();
        //IF THE ATTACK MISSES
        attackDebounce = true;
    }

    public void SetStall(bool cond)
    {
        stallSword = cond;
    }
}
