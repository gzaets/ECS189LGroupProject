using UnityEngine;
using System.Collections;

public class SlimeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] slimePrefabs;

    [SerializeField]
    public PlayerController player; // Reference to the player status script

    private void Start()
    {
        StartCoroutine(SpawnSlime());
    }

    private IEnumerator SpawnSlime()
    {
        while (true)
        {
            // Check if player is in cave or in combat
            if (player.GetInCave())
            {
                GameObject selectedPrefab = slimePrefabs[Random.Range(0, slimePrefabs.Length)];
                GameObject newSlime = Instantiate(selectedPrefab, transform.position, Quaternion.identity);
                SlimeController slimeController = newSlime.GetComponent<SlimeController>();
                yield return new WaitForSeconds(5);
            }
            else
            {
                yield return null;
            }
        }
    }
}