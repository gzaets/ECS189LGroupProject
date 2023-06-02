using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform TargetPosition; // the position the player should be teleported to

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = TargetPosition.position;
        }
    }
}