using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1f;
    public int damage = 20;
    public float lifeTime = 2f;

    public Transform player;
    public Vector2 target = new Vector2(0, 0);

    void Start()
    {
        target = (player.position - transform.position).normalized;
        // Destroy the projectile after 1.5 seconds
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move the projectile in the direction of the player at the time of shooting
        transform.position += (Vector3)target * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the projectile collided with the player
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}