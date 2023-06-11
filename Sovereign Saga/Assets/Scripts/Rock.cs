using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private float duration;
    private float durationCounter;
    private int damage = 20;

    // Start is called before the first frame update
    void Start()
    {
        duration = 10.0f;
        durationCounter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        durationCounter += Time.deltaTime;
        if (durationCounter >= duration)
        {
            Destroy(gameObject);
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for SlimeController, if it exists, then that means we have hit a 
        SlimeController slimeController = collision.collider.GetComponent<SlimeController>();
        if (slimeController != null)
        {
            //Debug.Log("Hit");

            // Slime takes damage.
            slimeController.TakeDamage(damage);

            // Destroy rock.
            Destroy(gameObject);

        }
    }
    */
}
