using UnityEngine;
using System.Collections;

public class SlimeSpawner : MonoBehaviour
{
    // The slime prefab to spawn
    [SerializeField]
    public GameObject slimePrefab;

    private void Start()
    {
        // Start the SpawnSlime coroutine
        StartCoroutine(SpawnSlime());
    }

    private IEnumerator SpawnSlime()
    {
        while (true)
        {
            // Instantiate a new slime at the spawner's location
            GameObject newSlime = Instantiate(slimePrefab, transform.position, Quaternion.identity);

            // Assuming that the slime prefab has a SlimeController component attached to it
            SlimeController slimeController = newSlime.GetComponent<SlimeController>();

            // Set the target of the new slime to the player (assuming the player has the "Player" tag)
            slimeController.target = GameObject.FindGameObjectWithTag("Player").transform;

            // Wait for 10 seconds before the next iteration
            yield return new WaitForSeconds(10);
        }
    }
}