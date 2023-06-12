using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    private float duration;
    private float durationCounter;
    private int damage = 12;

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

    public int GetDamage()
    {
        return damage;
    }
}
