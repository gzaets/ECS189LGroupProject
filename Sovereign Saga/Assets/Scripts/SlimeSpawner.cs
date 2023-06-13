using UnityEngine;
using System.Collections;

public class SlimeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] slimePrefabs;

    [SerializeField]
    private GameObject Hero;

    private float playerX;
    
    private float initialSpawnDelay = 5.0f;  // initial delay between spawns
    private float spawnDelay;                // current delay between spawns
    private float spawnDelayDecrement = 0.05f; // how much we decrease the delay each time

    private void Start()
    {
        spawnDelay = initialSpawnDelay;

        StartCoroutine(SpawnSlime());
        playerX = Hero.transform.position.x;
    }

    private IEnumerator SpawnSlime()
    {
        while (spawnDelay > 0.5f) // spawn slimes infinitely
        {
            playerX = Hero.transform.position.x;

            // Check if player is in cave or in combat
            if (playerX < -59f)
            {
                GameObject selectedPrefab = slimePrefabs[Random.Range(0, slimePrefabs.Length)];
                GameObject newSlime = Instantiate(selectedPrefab, transform.position, Quaternion.identity);
                
                // Get the SlimeController component of the newly created slime
                SlimeController slimeController = newSlime.GetComponent<SlimeController>();

                // Assign the Hero instance to the slime
                slimeController.Hero = this.Hero;

                yield return new WaitForSeconds(spawnDelay);

                // Decrease the delay between spawns (to a minimum of 1 second)
                if (spawnDelay > 1f)
                    spawnDelay = Mathf.Max(1f, spawnDelay - spawnDelayDecrement);
                else
                    spawnDelay = 1f;

            }
            else
            {
                yield return null;
            }
        }
    }
}
