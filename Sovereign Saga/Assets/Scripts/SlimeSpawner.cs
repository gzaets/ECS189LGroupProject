using UnityEngine;
using System.Collections;

public class SlimeSpawner : MonoBehaviour
{
    // The slime prefabs to spawn
    [SerializeField]
    private GameObject[] slimePrefabs;

    private void Start()
    {
        // Start the SpawnSlime coroutine
        StartCoroutine(SpawnSlime());
    }

    private IEnumerator SpawnSlime()
    {
        while (true)
        {
            // Select a random prefab
            GameObject selectedPrefab = slimePrefabs[Random.Range(0, slimePrefabs.Length)];
            
            // Instantiate a new slime at the spawner's location
            GameObject newSlime = Instantiate(selectedPrefab, transform.position, Quaternion.identity);

            // Assuming that the slime prefab has a SlimeController component attached to it
            SlimeController slimeController = newSlime.GetComponent<SlimeController>();

            // Wait for 10 seconds before the next iteration
            yield return new WaitForSeconds(5);
        }
    }
}