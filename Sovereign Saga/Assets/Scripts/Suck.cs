using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suck : MonoBehaviour
{
    private float duration;
    private float durationCounter;

    // Start is called before the first frame update
    void Start()
    {
        duration = 15.0f;
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

    /*
    void OnTriggerEnter2D(Collision2D collision)
    {
        // Check for SlimeController, if it exists, then that means we have hit a 
        SlimeController slimeController = collision.collider.GetComponent<SlimeController>();
        if (slimeController != null)
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAA");
            // Change the slime's current target to the suck. 
            slimeController.ChangeTarget(gameObject);
        }
    }
    */
    
}
